using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SmartSpider.Utility
{
    public delegate void OnRefreshTaskStatus(int index, Config.TaskUnit task);
    public delegate void OnShowTaskRuntimeInfo(object sender, EventArgs e);

    public class TaskListView : ListView
    {
        #region 公共字段定义
        public List<Config.TaskUnit> _TaskItem = new List<Config.TaskUnit>();
        #endregion

        #region 公共事件定义
        /// <summary>
        /// 当任务状态改变时执行的事件
        /// </summary>
        public event Config.OnTaskStatusChanges OnTaskStatusChanges;

        /// <summary>
        /// 当任务完成时产生的事件
        /// </summary>
        public event Config.OnTaskComplete OnTaskComplete;
        /// <summary>
        /// 任务开始
        /// </summary>
        public event Config.OnTaskStart OnTaskStart;
        /// <summary>
        /// 任务暂停
        /// </summary>
        public event Config.OnTaskPause OnTaskPause;
        /// <summary>
        /// 任务暂停
        /// </summary>
        public event Config.OnTaskStop OnTaskStop;

        /// <summary>
        /// 追加日志
        /// </summary>
        public event Config.OnAppendSingileLog OnAppendSingileLog;
        /// <summary>
        /// 追加结果
        /// </summary>
        public event Config.OnAppendSingleResult OnAppendSingleResult;
        /// <summary>
        /// 发布结果
        /// </summary>
        public event Config.OnPublishResult OnPublishResult;

        public event OnShowTaskRuntimeInfo OnShowTaskRuntimeInfo;
        #endregion

        #region 公共属性定义
        /// <summary>
        /// 任务项集合
        /// </summary>
        public List<Config.TaskUnit> TaskItem
        {
            get { return _TaskItem; }
            set { _TaskItem = value; }
        }
        #endregion

        #region 公共方法定义
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskListView()
        {
            //初始化组建
            InitializeComponent();

            //加载Xml配置文件
            LoadLocationTaskItem();
        }
        /// <summary>
        /// 刷新任务项状态
        /// </summary>
        /// <param name="index">任务项索引</param>
        /// <param name="task">任务项</param>
        public void RefreshTaskStatus(int index, Config.TaskUnit task)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new OnRefreshTaskStatus(RefreshTaskStatus), new object[] { index, task });
            }
            else
            {
                #region 更新项状态
                if (Items.Count == 0) return;

                for (int i = 0; i < _TaskItem.Count; i++)
                {
                    for (int j = 0; j < Items.Count; j++)
                    {
                        if (_TaskItem[i].TaskConfig.Name.Equals(Items[j].Text))
                        {
                            index = j;
                            break;
                        }
                    }
                }

                //更新项文字信息
                task.TaskConfig.ElapsedTime = DateTime.Now.Subtract(task.TaskConfig.StartingTime).Seconds;
                this.Items[index].SubItems[1].Text = task.TaskConfig.UrlListManager.PickedUrlsPosition.ToString();
                this.Items[index].SubItems[2].Text = task.TaskConfig.UrlListManager.PickedUrlsCount.ToString();
                this.Items[index].SubItems[3].Text = task.TaskConfig.UrlListManager.StartingUrlListPosition.ToString();
                this.Items[index].SubItems[4].Text = task.TaskConfig.UrlListManager.StartingUrlList.Count.ToString();
                this.Items[index].SubItems[5].Text = task.TaskConfig.UrlListManager.HistoryUrlsCount.ToString();
                this.Items[index].SubItems[6].Text = task.Results.Rows.Count.ToString();
                this.Items[index].SubItems[7].Text = task.TaskConfig.ResultCount.ToString();
                this.Items[index].SubItems[8].Text = task.TaskConfig.RepeatedRowsCount.ToString();
                this.Items[index].SubItems[9].Text = task.TaskConfig.ErrorRowsCount.ToString();
                this.Items[index].SubItems[10].Text = task.TaskConfig.ElapsedTime + " 秒";
                this.Items[index].SubItems[11].Text = task.TaskConfig.StartingTime.ToString("yyyy-MM-dd HH:mm:ss");    //开始时间

                //刷新图标
                switch (task.Action)
                {
                    case Config.Action.Finish:
                        this.Items[index].ImageKey = "editmin.png";
                        break;
                    case Config.Action.Ready:
                        this.Items[index].ImageKey = "taskmin.png";
                        break;
                    case Config.Action.Start:
                        this.Items[index].ImageKey = "startmin.png";
                        break;
                    case Config.Action.Pause:
                        this.Items[index].ImageKey = "pausemin.png";
                        break;
                    case Config.Action.Stop:
                        this.Items[index].ImageKey = "stopmin.png";
                        break;
                    case Config.Action.Running:
                        this.Items[index].ImageKey = "pausemin.png";
                        break;
                }
                #endregion
            }
        }
        /// <summary>
        /// 显示分组项到items视图中
        /// </summary>
        /// <param name="groupText">分组名称</param>
        public void ShowGroupItem(string groupText)
        {
            this.currentGroupText = groupText;
            //清除现有项集合
            this.Items.Clear();

            //遍历集合项
            foreach (Config.TaskUnit task in _TaskItem)
            {
                if (task != null && task.ConfigDir.Equals(groupText))
                {
                    this.Items.Add(new Utility.TaskViewItem(task.TaskConfig.Name, task.TaskConfig.Description));
                    RefreshTaskStatus(this.Items.Count - 1, task);
                }
            }

            //刷新控件视图
            this.Refresh();
        }
        /// <summary>
        /// 开始任务
        /// </summary>
        public void StartTask()
        {
            foreach (TaskViewItem item in this.SelectedItems)
            {
                foreach (Config.TaskUnit unit in _TaskItem)
                {
                    if (unit.TaskConfig.Name.Equals(item.Text))
                    {
                        unit.Action = Config.Action.Start;
                    }
                }
            }
        }
        /// <summary>
        /// 暂停任务
        /// </summary>
        public void PauseTask()
        {
            foreach (TaskViewItem item in this.SelectedItems)
            {
                foreach (Config.TaskUnit unit in _TaskItem)
                {
                    if (unit.TaskConfig.Name.Equals(item.Text))
                    {
                        unit.Action = Config.Action.Pause;
                    }
                }
            }
        }
        /// <summary>
        /// 停止任务
        /// </summary>
        public void StopTask()
        {
            foreach (TaskViewItem item in this.SelectedItems)
            {
                foreach (Config.TaskUnit unit in _TaskItem)
                {
                    if (unit.TaskConfig.Name.Equals(item.Text))
                    {
                        unit.Action = Config.Action.Stop;
                    }
                }
            }
        }
        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="groupText">分组选项（Xml配置文件存储路径目录）</param>
        public void CreateTask(string groupText)
        {
            CreateTask(groupText, new Config.TaskUnit());
        }
        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="groupText">分组选项（Xml配置文件存储路径目录）</param>
        /// <param name="taskUnit">任务控制单元</param>
        public void CreateTask(string groupText, Config.TaskUnit taskUnit) {

            taskUnit.ConfigDir = groupText; //配置文件目录

            //显示任务编辑窗体
            FrmTask frmtask = new FrmTask(taskUnit);
            frmtask.ShowDialog();

            /*
             *此处控制逻辑有异常，待修改。
             *描述：新建一个任务之后，该任务还并未编辑完毕，任务列表视图已经显示了此任务。
             *考虑：是否能实现显示模态窗口之后，以下的代码暂停执行？
             */
            //追加新的任务项
            if (frmtask._TaskUnit != null && !string.IsNullOrEmpty(frmtask._TaskUnit.TaskConfig.Name)) {
                _TaskItem.Add(frmtask._TaskUnit);
            }

            //重新显示
            ShowGroupItem(groupText);
        }
        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="groupText">分组选项（Xml配置文件存储路径目录）</param>
        /// <param name="task">xml任务配置文件</param>
        public void CreateTask(string groupText, Config.Task task){
            Config.TaskUnit unit = new Config.TaskUnit();
            unit.TaskConfig = task;
            unit.ConfigDir = groupText;
            unit.ConfigPath = "";

            this.CreateTask(groupText, unit);
        }
        

        /// <summary>
        /// 删除任务
        /// </summary>
        public void DeleteTask()
        {
            foreach (TaskViewItem item in this.SelectedItems)
            {
                foreach (Config.TaskUnit unit in _TaskItem)
                {
                    if (unit.TaskConfig.Name.Equals(item.Text))
                    {
                        unit.DeleteTask();          //删除任务配置文件
                        _TaskItem.Remove(unit);     //删除当前类list项
                        this.Items.Remove(item);    //删除任务视图集合项
                    }
                }
            }

            //重新刷新任务列表项
            ShowGroupItem(currentGroupText);
        }
        /// <summary>
        /// 编辑任务
        /// </summary>
        public void EditTask()
        {
            if (this.SelectedItems.Count < 0)
            {
                MessageBox.Show("请选择任务项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.SelectedItems.Count > 0)
            {
                for (int i = 0; i < _TaskItem.Count; i++)
                {
                    if (this.SelectedItems[0].Text.Equals(_TaskItem[i].TaskConfig.Name))
                    {
                        FrmTask frmTask = new FrmTask(_TaskItem[i]);
                        frmTask.ShowDialog();
                        _TaskItem[i] = frmTask._TaskUnit;
                        ShowGroupItem(currentGroupText);    //重新刷新任务列表项
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// 发布采集结果
        /// </summary>
        public void PublishResult()
        {
            foreach (TaskViewItem item in this.SelectedItems)
            {
                foreach (Config.TaskUnit unit in _TaskItem)
                {
                    if (item.Text.Equals(unit.TaskConfig.Name))
                    {
                        unit.PublishResult();
                    }
                }
            }
        }
        /// <summary>
        /// 清空采集结果
        /// </summary>
        public void ClearResult()
        {
            foreach (TaskViewItem item in this.SelectedItems)
            {
                foreach (Config.TaskUnit unit in _TaskItem)
                {
                    if (item.Text.Equals(unit.TaskConfig.Name))
                    {
                        unit.Results.Rows.Clear();
                    }
                }
            }
        }
        /// <summary>
        /// 获取选定项的任务状态
        /// None状态代表没有选择项
        /// </summary>
        /// <returns>任务状态</returns>
        public Config.Action GetSelectedItemStatus()
        {
            foreach (TaskViewItem item in this.SelectedItems)
            {
                foreach (Config.TaskUnit unit in _TaskItem)
                {
                    if (unit.TaskConfig.Name.Equals(item.Text))
                    {
                        return unit.Action;
                    }
                }
            }
            return Config.Action.None;
        }
        /// <summary>
        /// 获取选定项的索引
        /// </summary>
        /// <returns>索引值:-1没有选定项</returns>
        public int GetSelectedIndex()
        {
            for (int i = 0; i < _TaskItem.Count; i++)
            {
                foreach (TaskViewItem selectItem in this.SelectedItems)
                {
                    if (_TaskItem[i].TaskConfig.Name.Equals(selectItem.Text))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// 保存采集结果
        /// </summary>
        public void SaveResult()
        {
            foreach (TaskViewItem item in this.SelectedItems)
            {
                foreach (Config.TaskUnit unit in _TaskItem)
                {
                    if (unit.TaskConfig.Name.Equals(item.Text))
                    {
                        unit.SaveResult();
                    }
                }
            }
        }
        #endregion

        #region 私有方法定义
        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitializeComponent()
        {
            #region 初始化子控件
            this.taskName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExtractCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExtractURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExtractStartSuccessUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExtractStartUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExtractHistory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExtractCurrent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExtractResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ReleaseRepeat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ReleaseError = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExtractSpaceTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));

            // 
            // taskName
            // 
            this.taskName.Text = "任务名称";
            this.taskName.Width = 150;
            this.taskName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExtractCount
            // 
            this.ExtractCount.Text = "完成提取";
            this.ExtractCount.Width = 80;
            this.ExtractCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExtractURL
            // 
            this.ExtractURL.Text = "提取网址";
            this.ExtractURL.Width = 80;
            this.ExtractURL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExtractStartSuccessUrl
            // 
            this.ExtractStartSuccessUrl.Text = "完成起始";
            this.ExtractStartSuccessUrl.Width = 80;
            this.ExtractStartSuccessUrl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExtractStartUrl
            // 
            this.ExtractStartUrl.Text = "起始地址";
            this.ExtractStartUrl.Width = 80;
            this.ExtractStartUrl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExtractHistory
            // 
            this.ExtractHistory.Text = "历史记录";
            this.ExtractHistory.Width = 80;
            this.ExtractHistory.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExtractCurrent
            // 
            this.ExtractCurrent.Text = "当前采集";
            this.ExtractCurrent.Width = 80;
            this.ExtractCurrent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExtractResult
            // 
            this.ExtractResult.Text = "采集结果";
            this.ExtractResult.Width = 80;
            this.ExtractResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ReleaseRepeat
            // 
            this.ReleaseRepeat.Text = "发布重复";
            this.ReleaseRepeat.Width = 80;
            this.ReleaseRepeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ReleaseError
            // 
            this.ReleaseError.Text = "发布出错";
            this.ReleaseError.Width = 80;
            this.ReleaseError.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExtractSpaceTime
            // 
            this.ExtractSpaceTime.Text = "采集用时";
            this.ExtractSpaceTime.Width = 120;
            this.ExtractSpaceTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StartTime
            // 
            this.StartTime.Text = "开始时间";
            this.StartTime.Width = 150;
            this.StartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            #endregion

            #region 初始化主控件属性
            this.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.FullRowSelect = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "livTaskView";
            this.Size = new System.Drawing.Size(389, 150);
            this.TabIndex = 0;
            this.HideSelection = false;
            this.UseCompatibleStateImageBehavior = false;
            this.View = System.Windows.Forms.View.Details;
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.taskName,
                this.ExtractCount,
                this.ExtractURL,
                this.ExtractStartSuccessUrl,
                this.ExtractStartUrl,
                this.ExtractHistory,
                this.ExtractCurrent,
                this.ExtractResult,
                this.ReleaseRepeat,
                this.ReleaseError,
                this.ExtractSpaceTime,
                this.StartTime});
            #endregion

            #region 快捷菜单初始化
            TaskListViewQuickMenu = new ContextMenu();
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("显示运行信息(&R)", new EventHandler(TaskListViewQuickMenu_MenuItems_ShowInfo)));
            TaskListViewQuickMenu.MenuItems.Add("-");
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("开始(&S)", new EventHandler(TaskListViewQuickMenu_MenuItems_Start)));
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("暂停(&P)", new EventHandler(TaskListViewQuickMenu_MenuItems_Pause)));
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("停止(&T)", new EventHandler(TaskListViewQuickMenu_MenuItems_Stop)));
            TaskListViewQuickMenu.MenuItems.Add("-");
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("新建(&N)", new EventHandler(TaskListViewQuickMenu_MenuItems_Create)));
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("编辑(&E)", new EventHandler(TaskListViewQuickMenu_MenuItems_Edit)));
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("复制(&C)", new EventHandler(TaskListViewQuickMenu_MenuItems_Copy)));
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("粘贴(&P)", new EventHandler(TaskListViewQuickMenu_MenuItems_Paste)));
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("删除(&D)", new EventHandler(TaskListViewQuickMenu_MenuItems_Delete)));
            TaskListViewQuickMenu.MenuItems.Add("-");
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("导出(&X)", new EventHandler(TaskListViewQuickMenu_MenuItems_Export)));
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("导入(&I)", new EventHandler(TaskListViewQuickMenu_MenuItems_Import)));
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("清空(&C)", new EventHandler(TaskListViewQuickMenu_MenuItems_Clear)));
            TaskListViewQuickMenu.MenuItems.Add("-");
            TaskListViewQuickMenu.MenuItems.Add(new MenuItem("全选(&A)", new EventHandler(TaskListViewQuickMenu_MenuItems_SelectAll)));

            this.ContextMenu = TaskListViewQuickMenu;
            #endregion
        }
        /// <summary>
        /// 加载本地xml Task配置文件
        /// </summary>
        private void LoadLocationTaskItem()
        {
            string taskRootPath = AppDomain.CurrentDomain.BaseDirectory + "Task";
            string[] files = Directory.GetFiles(taskRootPath, "*.xml", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Config.Task));
                    Stream readStream = new FileStream(files[i], FileMode.Open, FileAccess.Read, FileShare.Read);
                    Config.Task task = (Config.Task)xs.Deserialize(readStream);
                    readStream.Close();
                    readStream.Dispose();

                    Config.TaskUnit unit = new Config.TaskUnit();
                    unit.ConfigPath = files[i];
                    unit.TaskConfig = task;
                    unit.ConfigDir = Directory.GetParent(files[i]).FullName;

                    this._TaskItem.Add(unit);

                    #region 订阅事件
                    this._TaskItem[_TaskItem.Count - 1].OnTaskStatusChanges += new Config.OnTaskStatusChanges(TaskListView_OnTaskStatusChanges);
                    this._TaskItem[_TaskItem.Count - 1].OnTaskComplete += new Config.OnTaskComplete(TaskListView_OnTaskComplete);
                    this._TaskItem[_TaskItem.Count - 1].OnTaskStart += new Config.OnTaskStart(TaskListView_OnTaskStart);
                    this._TaskItem[_TaskItem.Count - 1].OnTaskPause += new Config.OnTaskPause(TaskListView_OnTaskPause);
                    this._TaskItem[_TaskItem.Count - 1].OnTaskStop += new Config.OnTaskStop(TaskListView_OnTaskStop);
                    this._TaskItem[_TaskItem.Count - 1].OnAppendSingileLog += new Config.OnAppendSingileLog(TaskListView_OnAppendSingileLog);
                    this._TaskItem[_TaskItem.Count - 1].OnAppendSingleResult += new Config.OnAppendSingleResult(TaskListView_OnAppendSingleResult);
                    this._TaskItem[_TaskItem.Count - 1].OnPublishResult += new Config.OnPublishResult(TaskListView_OnPublishResult);
                    #endregion
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        #region 任务运行事件
        //状态改变
        private void TaskListView_OnTaskStatusChanges(object sender, Config.Action action)
        {
            Config.TaskUnit unit = (Config.TaskUnit)sender;
            RefreshTaskStatus(0, unit); //刷新任务状态
        }
        //任务完成
        private void TaskListView_OnTaskComplete(object sender, Config.LogEventArgs e)
        {
            if (OnTaskComplete != null)
            {
                OnTaskComplete(sender, e);
            }
        }
        //任务开始
        private void TaskListView_OnTaskStart(object sender, Config.LogEventArgs e)
        {
            if (OnTaskStart != null)
            {
                OnTaskStart(sender, e);
            }
        }
        //任务暂停
        private void TaskListView_OnTaskPause(object sender, Config.LogEventArgs e)
        {
            if (OnTaskPause != null)
            {
                OnTaskPause(sender, e);
            }
        }
        //任务停止
        private void TaskListView_OnTaskStop(object sender, Config.LogEventArgs e)
        {
            if (OnTaskStop != null)
            {
                OnTaskStop(sender, e);
            }
        }
        //追加日志
        private void TaskListView_OnAppendSingileLog(object sender, Config.LogEventArgs e)
        {
            if (OnAppendSingileLog != null)
            {
                OnAppendSingileLog(sender, e);
            }
        }
        //增加结果行
        private void TaskListView_OnAppendSingleResult(object sender, object[] values)
        {
            if (OnAppendSingleResult != null)
            {
                //引发事件
                OnAppendSingleResult(sender, values);

                //刷新任务项
                RefreshTaskStatus(0, (Config.TaskUnit)sender);
            }
        }
        //发布结果
        private void TaskListView_OnPublishResult(object sender, Config.LogEventArgs e)
        {
            if (OnPublishResult != null)
            {
                OnPublishResult(sender, e);
            }
        }
        #endregion

        #region   快捷菜单事件
        private void TaskListViewQuickMenu_MenuItems_ShowInfo(object sender, EventArgs e) {
            if (OnShowTaskRuntimeInfo != null)
            {
                OnShowTaskRuntimeInfo(this, e);
            }
        }
        private void TaskListViewQuickMenu_MenuItems_Start(object sender, EventArgs e) {
            StartTask();
        }
        private void TaskListViewQuickMenu_MenuItems_Pause(object sender, EventArgs e) {
            PauseTask();
        }
        private void TaskListViewQuickMenu_MenuItems_Stop(object sender, EventArgs e) {
            StopTask();
        }
        private void TaskListViewQuickMenu_MenuItems_Create(object sender, EventArgs e) {
            CreateTask(this.currentGroupText);
        }
        private void TaskListViewQuickMenu_MenuItems_Edit(object sender, EventArgs e) {
            EditTask();
        }
        private void TaskListViewQuickMenu_MenuItems_Copy(object sender, EventArgs e) {
            foreach (ListViewItem item in SelectedItems)
            {
                foreach (Config.TaskUnit unit in _TaskItem)
                {
                    if (unit.TaskConfig.Name.Equals(item.Text))
                    {
                        Clipboard.Clear();
                        Clipboard.SetDataObject(unit.TaskConfig);
                        return;
                    }
                }
            }
        }
        private void TaskListViewQuickMenu_MenuItems_Paste(object sender, EventArgs e) {
            try {
                Config.Task task = (Config.Task)Clipboard.GetDataObject().GetData(typeof(Config.Task));
                task.Name = "";
                task.Description = "";

                CreateTask(currentGroupText, task);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void TaskListViewQuickMenu_MenuItems_Delete(object sender, EventArgs e) {
            DeleteTask();
        }
        private void TaskListViewQuickMenu_MenuItems_Export(object sender, EventArgs e) {
        
        }
        private void TaskListViewQuickMenu_MenuItems_Import(object sender, EventArgs e) {
        
        }
        private void TaskListViewQuickMenu_MenuItems_Clear(object sender, EventArgs e) {
            foreach (ListViewItem item in SelectedItems) {
                foreach (Config.TaskUnit unit in _TaskItem) {
                    if (unit.TaskConfig.Name.Equals(item.Text)) {
                        //清除采集结果行
                        if (unit.Results != null && unit.Results.Rows.Count != 0) {
                            unit.Results.Rows.Clear();
                        }
                        //清除重复行
                        if (unit.RepeatedRow != null && unit.RepeatedRow.Rows.Count != 0) {
                            unit.RepeatedRow.Rows.Clear();
                        }
                    }
                }
            }
        }
        private void TaskListViewQuickMenu_MenuItems_SelectAll(object sender, EventArgs e) {
            foreach (ListViewItem item in Items)
            {
                item.Selected = true;
            }
        }
        #endregion

        #endregion

        #region 私有字段定义
        private System.Windows.Forms.ColumnHeader taskName;
        private System.Windows.Forms.ColumnHeader ExtractCount;
        private System.Windows.Forms.ColumnHeader ExtractURL;
        private System.Windows.Forms.ColumnHeader ExtractStartSuccessUrl;
        private System.Windows.Forms.ColumnHeader ExtractStartUrl;
        private System.Windows.Forms.ColumnHeader ExtractHistory;
        private System.Windows.Forms.ColumnHeader ExtractCurrent;
        private System.Windows.Forms.ColumnHeader ExtractResult;
        private System.Windows.Forms.ColumnHeader ReleaseRepeat;
        private System.Windows.Forms.ColumnHeader ReleaseError;
        private System.Windows.Forms.ColumnHeader ExtractSpaceTime;
        private System.Windows.Forms.ColumnHeader StartTime;
        private string currentGroupText = "";
        private ContextMenu TaskListViewQuickMenu;
        #endregion
    }
}
