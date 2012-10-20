namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Collections.Specialized;
    using System.IO;
    using System.Threading;
    using System.Text.RegularExpressions;
    using System.Xml.Serialization;

    #region 委托方法定义
    public delegate void OnTaskStatusChanges(object sender, Config.Action action);      //任务状态改变    
    public delegate void OnTaskComplete(object sender, Config.LogEventArgs e);          //任务完成
    public delegate void OnTaskStart(object sender, Config.LogEventArgs e);             //任务开始
    public delegate void OnTaskPause(object sender, Config.LogEventArgs e);             //任务暂停
    public delegate void OnTaskStop(object sender, Config.LogEventArgs e);              //任务停止
    public delegate void OnAppendSingileLog(object sender, Config.LogEventArgs e);      //追加日志
    public delegate void OnAppendSingleResult(object sender, params object[] values);    //追加结果
    public delegate void OnPublishResult(object sender, Config.LogEventArgs e);         //发布结果
    #endregion

    /// <summary>
    /// 任务控制单元
    /// </summary>
    public class TaskUnit : IDisposable {
        #region 公共事件定义
        /// <summary>
        /// 当任务状态改变时执行的事件
        /// </summary>
        public event OnTaskStatusChanges OnTaskStatusChanges;
        /// <summary>
        /// 当任务完成时产生的事件
        /// </summary>
        public event OnTaskComplete OnTaskComplete;
        /// <summary>
        /// 任务开始
        /// </summary>
        public event OnTaskStart OnTaskStart;
        /// <summary>
        /// 任务暂停
        /// </summary>
        public event OnTaskPause OnTaskPause;
        /// <summary>
        /// 任务暂停
        /// </summary>
        public event OnTaskStop OnTaskStop;
        /// <summary>
        /// 追加日志
        /// </summary>
        public event OnAppendSingileLog OnAppendSingileLog;
        /// <summary>
        /// 追加结果
        /// </summary>
        public event OnAppendSingleResult OnAppendSingleResult;
        /// <summary>
        /// 发布结果
        /// </summary>
        public event OnPublishResult OnPublishResult;
        #endregion

        #region 私有变量定义
        private Task _TaskConfig = new Task();
        private Action _Action = new Action();
        private DataTable _Results = new DataTable();
        private DataTable _ErrorRow;  //错误行
        private DataTable _RepeatedRow;   //重复行
        private HttpHelper _HttpHelper;
        private string _ConfigPath = "";
        private string _ConfigDir = "";
        private StringCollection NavigationUrls = new StringCollection();
        public Timer time;
        #endregion

        #region 公共方法定义

        #region 任务控制

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskUnit() {
            this._HttpHelper = new HttpHelper(Encoding.GetEncoding(this._TaskConfig.UrlListManager.UrlEncoding));
            time = new Timer(new TimerCallback(Start), "", Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// 开始
        /// </summary>
        private void Start(object sender) {
            string timeTick = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            if (this.Action == Config.Action.Running) {
                this.AppendLog(string.Format("{0} 任务正在运行中,启动任务失败...", timeTick));
                return;
            }
            this.AppendLog(string.Format("{0} 开始任务", timeTick));

            /*
             * 设定任务开始时间和状态
             */
            this._TaskConfig.StartingTime = DateTime.Now;
            this.Action = Config.Action.Running;

            #region 构造采集结果数据表结构
            this._Results = new DataTable();
            foreach (ExtractionRule rule in this._TaskConfig.ExtractionRules) {
                DataColumn colume = new DataColumn();
                colume.DataType = typeof(string);
                colume.ColumnName = rule.Name;
                colume.Caption = rule.DataColumn;
                //colume.Unique = rule.DataUnique;
                this._Results.Columns.Add(colume);
            }
            #endregion

            /*
             * 功能:
             * 以下代码完成解析导航地址功能。
             * 
             * 步骤：
             * 1.根据UrlListManager配置创建ParseNavigationRules导航地址解析对象.
             * 2.注册一个每增加导航地址时相应的事件.
             * 3.Exec开始解析导航地址.
             * 
             * 修改标志：王亚 20120424
             */
            ParseNavigationRules parseNav = new ParseNavigationRules(this._TaskConfig.UrlListManager);
            parseNav.onSingleComplete += new onSingleComplete(parseNav_onSingleComplete);
            parseNav.OnAppendSingileLog += new Config.OnAppendSingileLog(parseNav_OnAppendSingileLog);
            parseNav.Exec();

            //设置任务状态为完成
            this.Action = Config.Action.Finish;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        public void DeleteTask() {
            this.AppendLog("删除任务配置文件" + _ConfigPath);

            //删除任务配置文件，并销毁对象自身。
            File.Delete(this._ConfigPath);
            this.Dispose();
        }

        /// <summary>
        /// 销毁资源
        /// </summary>
        public void Dispose() {
            //保存采集结果
            SaveResult();

            this._HttpHelper = null;
            this._Results = null;
            this._TaskConfig = null;
        }
        #endregion

        #region 采集结果处理
        //当增加一条导航地址时触发的事件
        private void parseNav_onSingleComplete(object sender, string url) {
            this.AppendLog(url);
            //提取页面内容
            ExtractContents(url);
        }

        /// <summary>
        /// 提取请求结果内容
        /// </summary>
        /// <param name="param">导航地址Url</param>
        private void ExtractContents(string contentUrl) {
            try {
                if (this.Action == Config.Action.Stop) return;
                string htmlText = string.Empty;
                string[] resultRow;

                /*
                 * 描述:
                 * 根据Url地址请求Web服务器并返回一段Html文本，然后根据返回的文本和提取参数构造内容解析对象。
                 * 
                 * 步骤:
                 * 1.请求web服务器并返回一段html文本
                 * 2.根据html文本和提取规则实例化一个内容解析对象.
                 * 3.执行Exec方法并返回解析结果。
                 * 
                 * 修改标志:王亚 20120424
                 */
                this._HttpHelper._encoding = Encoding.GetEncoding(_TaskConfig.UrlListManager.UrlEncoding);
                htmlText = this._HttpHelper.RequestResult(contentUrl);
                ParseExtractRoles parseHtml = new ParseExtractRoles(
                    _TaskConfig.ExtractionRules,    //提取规则
                    htmlText,                       //html文本
                    _HttpHelper.WebResponse);       //response对象
                resultRow = parseHtml.Exec();       //提取内容


                /*
                 * 描述:
                 * 采集结果处理。
                 * 
                 * 步骤:
                 * 1.将采集结果追加
                 * 2.否直接发布采集结果
                 * 3.引发完成一条采集结果事件
                 * 
                 * 修改标志: 王亚 20120424
                 */
                if (resultRow != null) {
                    this._Results.Rows.Add(resultRow);              //追加采集结果
                }
                if (this.TaskConfig.PublishResultDircetly) {
                    PublishResult();                            //发布采集结果
                    Results.Rows.Clear();                       //清除现有的采集结果
                }
                if (this.OnAppendSingleResult != null) {
                    this.OnAppendSingleResult(this, resultRow); //引发完成一条采集结果事件
                }
            }
            catch (Exception e) {
                this.AppendLog(string.Format("请求失败:{0} 原因:{1}", contentUrl, e.Message));
            }
        }

        /// <summary>
        /// 追加日志记录信息
        /// </summary>
        private void AppendLog(string loginfo) {
            if (this.OnAppendSingileLog != null) {
                LogEventArgs logevent = new LogEventArgs(loginfo);
                this.OnAppendSingileLog(this, logevent);
            }
        }
        #endregion

        #region 发布结果

        /// <summary>
        /// 发布采集结果
        /// </summary>
        /// <returns>发布成功记录条数</returns>
        public int PublishResult() {
            this.AppendLog("开始发布采集结果");

            if (this.TaskConfig.DatabaseType == DatabaseType.Access) {
                this.AppendLog("发布到Access数据库...");
                return PublishResultToAccess();
            }
            else if (this.TaskConfig.DatabaseType == DatabaseType.MySql) {
                this.AppendLog("发布到MySql数据库...");
                return PublishResultToMySql();
            }
            else if (this.TaskConfig.DatabaseType == DatabaseType.Oracle) {
                this.AppendLog("发布到Oracle数据库...");
                return PublishResultToOracle();
            }
            else if (this.TaskConfig.DatabaseType == DatabaseType.SqlLite) {
                this.AppendLog("发布到SqlLite数据库...");
                return PublishResultToSqlLite();
            }
            else if (this.TaskConfig.DatabaseType == DatabaseType.SqlServer) {
                this.AppendLog("发布到SqlServer数据库...");
                return PublishResultToSqlServer();
            }
            return 0;
        }

        /// <summary>
        /// 发布结果到Access数据库
        /// </summary>
        /// <returns>发布成功记录数</returns>
        public int PublishResultToAccess() {
            //string cnStr = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source = " + "c:\\data.mdb";

            //ADOX.Catalog catalog = new Catalog();
            //try{
            //    //创建数据库
            //    catalog.Create(cnStr);

            //    //链接数据库
            //    ADODB.Connection adodbcn = new ADODB.Connection();
            //    adodbcn.Open(cnStr, null, null, -1);
            //    catalog.ActiveConnection = adodbcn;

            //    //新建表
            //    ADOX.Table table = new ADOX.Table();
            //    table.Name = "Results";

            //    ADOX.Column column = new ADOX.Column();
            //    column.ParentCatalog = catalog;
            //    column.Type = ADOX.DataTypeEnum.adInteger;
            //    column.Name = "GID";
            //    column.DefinedSize = 9;
            //    column.Properties["AutoIncrement"].Value = true;

            //    //设置主键
            //    table.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, "GID", "", "");
            //    foreach (DataColumn col in this.Results.Columns) {
            //        table.Columns.Append(col.ColumnName, DataTypeEnum.adVarChar, 8000);
            //    }

            //    catalog.Tables.Append(table);
            //    adodbcn.Close();
            //    table = null;
            //    catalog = null;
            //}
            //catch{
            //}

            //OleDbConnection cn = new OleDbConnection(cnStr);
            //OleDbDataAdapter da = new OleDbDataAdapter("select * from Results", cn);
            //OleDbCommandBuilder cmb = new OleDbCommandBuilder(da);
            //int res = da.Update(Results);
            //cn.Close();
            //cn.Dispose();
            //return res;
            return 0;
        }

        /// <summary>
        /// 发布结果到MySql数据库
        /// </summary>
        /// <returns>发布成功记录数</returns>
        public int PublishResultToMySql() {
            return 0;
        }

        /// <summary>
        /// 发布结果到Oracle数据库
        /// </summary>
        /// <returns>发布成功记录数</returns>
        public int PublishResultToOracle() {
            return 0;
        }

        /// <summary>
        /// 发布结果到SqlLite数据库
        /// </summary>
        /// <returns>发布成功记录数</returns>
        public int PublishResultToSqlLite() {
            return 0;
        }

        /// <summary>
        /// 发布结果到SqlServer数据库
        /// </summary>
        /// <returns>发布成功记录数</returns>
        public int PublishResultToSqlServer() {
            int publishResultCount = 0;
            SqlConnection sqlConn = new SqlConnection(this.TaskConfig.ConnectionString);
            try {
                sqlConn.Open();
                if (this.TaskConfig.UseProcedure) {
                    #region 使用存储过程发布结果
                    this.AppendLog("使用存储过程发布...");

                    foreach (DataRow row in this.Results.Rows) {
                        #region 忽略不存在的参数
                        try {
                            SqlCommand cmd = new SqlCommand();
                            foreach (DataColumn col in this.Results.Columns) {
                                SqlParameter colParam = new SqlParameter("@" + col.Caption, row[col.ColumnName].ToString().Replace('\'', '\"'));
                                cmd.Parameters.Add(colParam);
                            }
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = sqlConn;
                            cmd.CommandTimeout = 10;
                            cmd.CommandText = this.TaskConfig.PublicationTarget;
                            publishResultCount = cmd.ExecuteNonQuery();

                            #region 保存重复行
                            if (publishResultCount == -1 && TaskConfig.SaveRepeatedRows) {
                                if (RepeatedRow == null) {
                                    RepeatedRow = Results.Clone();
                                }
                                RepeatedRow.Rows.Add(row);
                            }
                            #endregion
                        }
                        catch (Exception ex) {
                            #region 保存出错行
                            if (TaskConfig.SaveErrorRows) {
                                if (ErrorRow == null) {
                                    ErrorRow = Results.Clone();
                                }
                                ErrorRow.Rows.Add(row);
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                }
                else {
                    #region 初始化发布结果数据库连接对象
                    this.AppendLog("发布到数据库表...");
                    DataTable dt = new DataTable();
                    string selectCommand = string.Format("SELECT top 0 * FROM {0}", this.TaskConfig.PublicationTarget);
                    SqlDataAdapter da = new SqlDataAdapter(selectCommand, sqlConn);
                    da.Fill(dt);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    #endregion

                    #region 处理重复行(暂未实现)
                    //过滤掉重复行,监测是否有唯一的列
                    //List<string> uniqeColumn = new List<string>();
                    //foreach (DataColumn col in Results.Columns) {
                    //    uniqeColumn.Add(col.ColumnName);
                    //}
                    //Results = Results.DefaultView.ToTable(true, uniqeColumn.ToArray());
                    ///*保存重复的行*/
                    //if (TaskConfig.SaveRepeatedRows) {
                    //    if (RepeatedRow == null) {
                    //        RepeatedRow = Results.Clone();
                    //    }
                    //}
                    #endregion

                    #region 忽略不存在的字段&保存出错行
                    foreach (DataRow row in Results.Rows) {
                        DataRow newRow = dt.NewRow();   //新行对象
                        bool errorIdentity = false;     //错误标示
                        foreach (DataColumn col in Results.Columns) {
                            #region 忽略不存在的数据列选项,找不到对应的字段则忽略
                            try {
                                newRow[col.Caption] = row[col.ColumnName];
                            }
                            catch (Exception ex) {
                                errorIdentity = true;   //设定当前行错误标志
                                if (TaskConfig.IgnoreDataColumnNotFound) {
                                    continue;
                                }
                                else {
                                    throw ex;
                                }
                            }
                            #endregion
                        }
                        dt.Rows.Add(newRow);

                        /*保存出错行*/
                        if (errorIdentity && TaskConfig.SaveErrorRows) {
                            if (ErrorRow == null) {
                                ErrorRow = Results.Clone();
                            }
                            ErrorRow.Rows.Add(row);
                        }
                    }
                    #endregion

                    #region 发布采集结果到数据库
                    publishResultCount = da.Update(dt);
                    #endregion
                }
            }
            catch (Exception e) {
                this.AppendLog(string.Format("发布出错:{0}", e.Message));
            }
            finally {
                sqlConn.Close();
                sqlConn.Dispose();
                this.AppendLog(string.Format("操作完成: 共 {0} 条记录。", publishResultCount.ToString()));
            }

            #region 结果文件发布到数据库后，删除结果文件数据
            if (TaskConfig.DeleteResultAfterPublication) {
                Results.Rows.Clear();
            }
            #endregion

            return publishResultCount;
        }

        /// <summary>
        /// 保存采集结果
        /// </summary>
        public void SaveResult() {
            string resultFileName = this.ConfigPath + ".result.txt";
            if (File.Exists(resultFileName)) {
                File.Delete(resultFileName);
            }

            foreach (DataRow row in this.Results.Rows) {
                string rowData = string.Empty;
                for (int i = 0; i < this.Results.Columns.Count; i++) {
                    if (i <= this.Results.Columns.Count - 2) {
                        rowData += row[this.Results.Columns[i].ColumnName].ToString() + ",";
                    }
                    else {
                        rowData += row[this.Results.Columns[i].ColumnName].ToString();
                    }
                }
                rowData += "\r\n";
                File.AppendAllText(resultFileName, rowData, Encoding.UTF8);
            }
        }
        #endregion

        #region 保存配置文件
        /// <summary>
        /// 保存任务配置到Xml文件(默认路径)
        /// </summary>
        public void SaveTaskConfiguration() {
            SaveTaskConfiguration(this.ConfigPath, true);
        }

        /// <summary>
        /// 保存任务配置文件(xml)到 filePath 路径
        /// </summary>
        /// <param name="filePath">xml文件路径</param>
        public void SaveTaskConfiguration(string filePath) {
            SaveTaskConfiguration(filePath, true);
        }

        /// <summary>
        /// 保存任务配置文件(xml)到 filePath 路径,允许指定一个选项 isCover 指示是否覆盖现有的文件
        /// </summary>
        /// <param name="filePath">xml保存路径</param>
        /// <param name="isCover">是否覆盖现有的文件(如果存在)</param>
        public void SaveTaskConfiguration(string filePath, bool isCover) {
            SaveTaskConfiguration(filePath, isCover, TaskConfig);
        }

        /// <summary>
        /// 保存任务配置文件(xml)到 filePath 路径,允许指定一个选项 isCover 指示是否覆盖现有的文件
        /// </summary>
        /// <param name="filePath">xml保存路径</param>
        /// <param name="isCover">是否覆盖现有的文件(如果存在)</param>
        /// <param name="task">task任务配置信息对象</param>
        public void SaveTaskConfiguration(string filePath, bool isCover, Task task) {
            //以覆盖方式保存配置文件
            if (isCover) {
                try {
                    //保存配置文件
                    XmlSerializer xs = new XmlSerializer(typeof(Task));
                    Stream writeStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write);
                    xs.Serialize(writeStream, task);
                    writeStream.Close();
                    writeStream.Dispose();
                }
                catch (Exception e) {
                    throw e;
                }
            }
        }
        #endregion

        #endregion

        #region 私有方法定义
        //导航地址解析日志追加事件
        private void parseNav_OnAppendSingileLog(object sender, Config.LogEventArgs e) {
            this.OnAppendSingileLog(this, e);
        }
        #endregion

        #region 公共属性定义
        /// <summary>
        /// 任务配置
        /// </summary>
        public Task TaskConfig {
            get {
                return _TaskConfig;
            }
            set {
                _TaskConfig = value;
            }
        }

        /// <summary>
        /// 任务状态
        /// </summary>
        public Action Action {
            get {
                return _Action;
            }
            set {
                _Action = value;
                if (OnTaskStatusChanges != null) {
                    this.OnTaskStatusChanges(this, value);
                }

                string timeTick = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();

                /*监测是否满足任务启动条件*/
                string loginfo = string.Empty;
                switch (this.Action) {
                    case Config.Action.Ready:   //准备
                        time.Change(Timeout.Infinite, Timeout.Infinite);   //停止
                        loginfo = string.Format("{0} 任务准备就绪...", timeTick);
                        this.OnTaskPause(this, new LogEventArgs("准备就绪"));
                        break;

                    case Config.Action.Start:   //开始
                        StartTask();
                        this.OnTaskStart(this, new LogEventArgs("开始任务"));
                        break;

                    case Config.Action.Pause:   //暂停
                        time.Change(Timeout.Infinite, Timeout.Infinite);   //停止,暂时没有好的办法来停止任务的执行
                        loginfo = string.Format("{0} 暂停任务...", timeTick);
                        this.OnTaskPause(this, new LogEventArgs("暂停任务"));
                        break;

                    case Config.Action.Stop:    //停止
                        time.Change(Timeout.Infinite, Timeout.Infinite);   //停止
                        loginfo = string.Format("{0} 停止任务...", timeTick);
                        this.OnTaskStop(this, new LogEventArgs("停止任务"));
                        break;

                    case Config.Action.Finish:  //完成
                        //time.Change(Timeout.Infinite, Timeout.Infinite);   //停止
                        loginfo = string.Format("{0} 任务完成...", timeTick);
                        this.OnTaskComplete(this, new LogEventArgs("任务完成"));
                        break;

                    case Config.Action.Running: //运行中
                        loginfo = string.Format("{0} 任务运行中...", timeTick);
                        break;
                }
                this.AppendLog(loginfo);
            }
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        private void StartTask() {
            if (TaskConfig.ScheduleEnabled) {
                /*调度模式:每间隔时间段*/
                if (TaskConfig.ScheduleMode == ScheduleMode.Time) {
                    long tick = TaskConfig.ScheduleDays * 86400000;
                    tick += TaskConfig.ScheduleHours * 3600000;
                    tick += TaskConfig.ScheduleMinutes * 60000;
                    time.Change(0, tick);

                    string loginfo = string.Format("{0} {1} 定时采集将于 {2} 秒后再次启动任务...",
                        DateTime.Now.ToLongDateString(),
                        DateTime.Now.ToLongTimeString(),
                        (tick / 1000).ToString());
                    this.AppendLog(loginfo);
                }

                /*调度模式:每当经过每星期几中的时间范围*/
                if (TaskConfig.ScheduleMode == ScheduleMode.Day) {
                    /*
                     * 1.获取当前时间
                     * 2.判断当前时间是否位于指定的时间段内(开始时间、结束时间)区间
                     * 3.开始任务
                     */
                }
            }
            else {
                time.Change(0, Timeout.Infinite);   //普通方式启动任务,紧执行一次
            }
        }

        /// <summary>
        /// 采集结果
        /// </summary>
        public DataTable Results {
            get {
                return _Results;
            }
            set {
                _Results = value;
            }
        }

        /// <summary>
        /// Http助手
        /// </summary>
        public HttpHelper HttpHelper {
            get {
                return _HttpHelper;
            }
            set {
                _HttpHelper = value;
            }
        }

        /// <summary>
        /// Task配置文件路径(存储路径+文件名称.xml)
        /// </summary>
        public string ConfigPath {
            get { return _ConfigPath; }
            set { _ConfigPath = value; }
        }

        /// <summary>
        /// 配置文件目录(存储路径)
        /// </summary>
        public string ConfigDir {
            get {
                if (string.IsNullOrEmpty(_ConfigDir)) {
                    _ConfigDir = AppDomain.CurrentDomain.BaseDirectory + "Task";
                }
                return _ConfigDir;
            }
            set { _ConfigDir = value; }
        }

        /// <summary>
        /// 采集结果出错行
        /// </summary>
        public DataTable ErrorRow {
            get { return _ErrorRow; }
            set { _ErrorRow = value; }
        }

        /// <summary>
        /// 采集结果重复行
        /// </summary>
        public DataTable RepeatedRow {
            get { return _RepeatedRow; }
            set { _RepeatedRow = value; }
        }
        #endregion
    }
}