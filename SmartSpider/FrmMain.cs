namespace SmartSpider {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.IO;
    using System.Xml.Serialization;
    using Config;

    public partial class FrmMain : Form {
        #region 构造方法
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMain() {
            InitializeComponent();

            //初始化为默认Ui
            SetDefaultUI();

            //加载系统配置文件
            LoadConfiguration();

            /*加载DBTaskList*/
            //LoadDBTaskItem();

            this.Text = "网络信息智能采集系统 V1.0.1 内部测试版";
            //this.Icon = new System.Drawing.Icon("mainProgram.ico");
        }
        #endregion

        #region 菜单栏事件
        #region 文件菜单
        //文件菜单：发布结果
        private void FileItemPublishResults_Click(object sender, EventArgs e) {
            splitContainerRight.livTaskView.PublishResult();
        }
        //文件菜单：结果另存为Excel
        private void FileItemSaveAsExcel_Click(object sender, EventArgs e) {

        }
        //文件菜单：结果另存在文本文件
        private void FileItemSaveAsTextFile_Click(object sender, EventArgs e) {

        }
        //文件菜单：结果另存为Access
        private void FileItemSaveAsAccessDB_Click(object sender, EventArgs e) {

        }
        //文件菜单：查看结果
        private void FileItemViewResult_Click(object sender, EventArgs e) {

        }
        //文件菜单：清空结果
        private void FileItemClearResult_Click(object sender, EventArgs e) {
            splitContainerRight.livTaskView.ClearResult();
        }
        //文件菜单：发布时重复行-查看
        private void FileItemPublishResultRepeatView_Click(object sender, EventArgs e) {

        }
        //文件菜单：发布时重复行-清空
        private void FileItemPublishResultRepeatClear_Click(object sender, EventArgs e) {

        }
        //文件菜单：发布时出错行-查看
        private void FileItemPublishResultErrorView_Click(object sender, EventArgs e) {

        }
        //文件菜单：发布时出错行-清空
        private void FileItemPublishResultErrorClear_Click(object sender, EventArgs e) {

        }
        //文件菜单：历史记录-查看
        private void FileItemHistoryView_Click(object sender, EventArgs e) {

        }
        //文件菜单：历史记录-清空
        private void FileItemHistoryClear_Click(object sender, EventArgs e) {

        }
        //文件菜单：历史记录-启用
        private void FileItemHistoryEnable_Click(object sender, EventArgs e) {

        }
        //文件菜单：任务日志-查看
        private void FileItemTaskLogView_Click(object sender, EventArgs e) {

        }
        //文件菜单：任务日志-清空
        private void FileItemTaskLogClear_Click(object sender, EventArgs e) {

        }
        //文件菜单：退出
        private void FileItemExit_Click(object sender, EventArgs e) {
            /*保存配置文件*/
            SaveConfiguration();

            /*保存任务信息到数据库*/
            //SaveTaskItemToDB();

            /*保存任务信息到本地Task目录下*/
            //SaveLoactionTaskItem();

            Application.ExitThread();
            Application.Exit();
        }
        #endregion

        #region 查看菜单
        //查看菜单：显示/关闭工具栏
        private void ViewItemShowToolBar_Click(object sender, EventArgs e) {

        }
        //查看菜单：显示关闭浮动窗口
        private void ViewItemShowFloatFrom_Click(object sender, EventArgs e) {

        }
        #endregion

        #region 文件夹菜单
        //文件夹菜单：新建
        private void FolderItemAdd_Click(object sender, EventArgs e) {

        }
        //文件夹菜单：重命名
        private void FolderItemRename_Click(object sender, EventArgs e) {

        }
        //文件夹菜单：删除
        private void FolderItemDelete_Click(object sender, EventArgs e) {

        }
        //文件夹菜单：刷新
        private void FolderItemRefresh_Click(object sender, EventArgs e) {
            //刷新任务
            trwTaskFolder.ReLoad(); //刷新树节点
        }
        //文件夹菜单：导出
        private void FolderItemExport_Click(object sender, EventArgs e) {

        }
        #endregion

        #region 任务菜单
        //任务菜单：显示运行信息
        private void TaskItemShowInfo_Click(object sender, EventArgs e) {

        }
        //任务菜单：开始
        private void TaskItemStart_Click(object sender, EventArgs e) {

        }
        //任务菜单：暂停
        private void TaskItemSpace_Click(object sender, EventArgs e) {

        }
        //任务菜单：停止
        private void TaskItemStop_Click(object sender, EventArgs e) {

        }
        //任务菜单：新建
        private void TaskItemAdd_Click(object sender, EventArgs e) {
            splitContainerRight.livTaskView.CreateTask((string)trwTaskFolder.SelectedNode.Tag);
        }
        //任务菜单：编辑
        private void TaskItemEdit_Click(object sender, EventArgs e) {
            splitContainerRight.livTaskView.EditTask();
        }
        //任务菜单：复制
        private void TaskItemCopy_Click(object sender, EventArgs e) {
            
        }
        //任务菜单：删除
        private void TaskItemDelete_Click(object sender, EventArgs e) {
            splitContainerRight.livTaskView.DeleteTask();
        }
        //任务菜单：导出
        private void TaskItemExport_Click(object sender, EventArgs e) {

        }
        //任务菜单：导入
        private void TaskItemImport_Click(object sender, EventArgs e) {

        }
        //任务菜单：全选
        private void TaskItemSelectAll_Click(object sender, EventArgs e) {

        }
        #endregion

        #region 设置菜单
        //设置菜单：Html标记
        private void configItemHtmlLable_Click(object sender, EventArgs e) {

        }
        //设置菜单：正则表达式
        private void configItemRegex_Click(object sender, EventArgs e) {

        }
        //设置菜单：预置正则名称
        private void configItemPreviewRulesName_Click(object sender, EventArgs e) {

        }
        //设置菜单：选项
        private void configItemOption_Click(object sender, EventArgs e) {
            FrmOption option = new FrmOption(this._Configuration);
            option.ShowDialog();
        }
        #endregion

        #region 工具菜单
        //工具菜单：源文件查看器
        private void ToolItemSourceView_Click(object sender, EventArgs e) {

        }
        //工具菜单：正则式测试器
        private void ToolItemRegesTest_Click(object sender, EventArgs e) {

        }
        //工具菜单：网址编码器
        private void ToolItemUrlEncoding_Click(object sender, EventArgs e) {

        }
        //工具菜单：任务升级器
        private void ToolItemTaskUpdate_Click(object sender, EventArgs e) {

        }
        //工具菜单：在线发布器
        private void ToolItemOnLinePublish_Click(object sender, EventArgs e) {

        }
        #endregion

        #region 帮助菜单
        //帮助菜单：在线帮助
        private void HelpItemOnLineHelp_Click(object sender, EventArgs e) {

        }
        //帮助菜单：网站
        private void HelpItemSite_Click(object sender, EventArgs e) {

        }
        //帮助菜单：论坛
        private void HelpItemBBS_Click(object sender, EventArgs e) {

        }
        //帮助菜单：购买
        private void HelpItemBuy_Click(object sender, EventArgs e) {

        }
        //帮助菜单：关于SmartSpider
        private void HelpItemAboutUS_Click(object sender, EventArgs e) {
            FrmAboutUS about = new FrmAboutUS();
            about.ShowDialog();
        }
        #endregion

        #region 私有变量定义
        #endregion
        #endregion

        #region 工具栏事件
        //工具栏：开始
        private void tolStartTask_Click(object sender, EventArgs e) {
            //开始任务
            splitContainerRight.livTaskView.StartTask();

            //显示日志窗口
            int index = splitContainerRight.livTaskView.GetSelectedIndex();
            if (index != -1)
            {
                //this.ShowTaskRuntimesInfo(ref livTaskView._TaskItem[index]);
            }

            //初始化按钮
            this.tolStartTask.Enabled = false;
            this.tolPauseTask.Enabled = true;
            this.tolStopTask.Enabled = true;
            this.tolAddTask.Enabled = true;
            this.tolEditTask.Enabled = false;
            this.tolDeleteTask.Enabled = false;
        }
        //工具栏：暂停
        private void tolPauseTask_Click(object sender, EventArgs e) {
            //暂停任务
            splitContainerRight.livTaskView.PauseTask();
        }
        //工具栏：停止
        private void tolStopTask_Click(object sender, EventArgs e) {
            splitContainerRight.livTaskView.StopTask();
        }
        //工具栏：新建任务
        private void tolAddTask_Click(object sender, EventArgs e) {
            //新建任务
            splitContainerRight.livTaskView.CreateTask((string)trwTaskFolder.SelectedNode.Tag);
        }
        //工具栏：编辑任务
        private void tolEditTask_Click(object sender, EventArgs e) {
            splitContainerRight.livTaskView.EditTask();
        }
        //工具栏：删除任务
        private void tolDeleteTask_Click(object sender, EventArgs e) {
            splitContainerRight.livTaskView.DeleteTask();
        }
        //工具栏：所有任务完成后关机
        private void tolAllTaskSuccessShutdown_Click(object sender, EventArgs e) {

        }
        //工具栏：禁用定时采集
        private void TolDisableAllTimingAcquisitionTask_Click(object sender, EventArgs e) {

        }
        //工具栏：选项
        private void tolOption_Click(object sender, EventArgs e) {
            FrmOption option = new FrmOption(this._Configuration);
            option.ShowDialog();
        }
        //工具栏：在线帮助
        private void TolOnLineHelp_Click(object sender, EventArgs e) {

        }
        //工具栏：关于SmartSpider
        private void TolAboutUS_Click(object sender, EventArgs e) {
            FrmAboutUS about = new FrmAboutUS();
            about.ShowDialog();
        }
        //工具栏：退出
        private void TolExit_Click(object sender, EventArgs e) {
            /*保存配置文件*/
            SaveConfiguration();

            /*保存任务信息到数据库*/
            //SaveTaskItemToDB();

            /*保存任务信息到本地Task目录下*/
            //SaveLoactionTaskItem();

            Application.ExitThread();
            Application.Exit();
        }
        //工具栏：导出到Excel
        private void ExportToExcel_Click(object sender, EventArgs e) {

        }
        //工具栏：导出到文本文件
        private void ExportToTextFile_Click(object sender, EventArgs e) {

        }
        //工具栏：导出到Access
        private void ExportToAccessDB_Click(object sender, EventArgs e) {

        }
        #endregion

        #region 窗体控件事件
        //主窗体：点击X按钮，退出程序
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e) {
            /*保存配置文件*/
            SaveConfiguration();

            /*保存任务信息到数据库*/
            //SaveTaskItemToDB();

            /*保存任务信息到本地Task目录下*/
            //SaveLoactionTaskItem();

            //保存采集结果数据
            splitContainerRight.livTaskView.SaveResult();

            Application.ExitThread();
            Application.Exit();
        }
        //任务文件夹：单击节点显示到任务信息到任务窗口
        private void trwTaskFolder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //筛选出tag分类下任务显示到任务详细信息窗口
            string dir = (string)this.trwTaskFolder.SelectedNode.Tag;

            //显示任务项到控件视图中
            this.splitContainerRight.livTaskView.ShowGroupItem(dir);
        }
        //任务运行信息窗口：双击编辑任务配置
        private void livTaskView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            splitContainerRight.livTaskView.EditTask();
        }
        //任务运行信息窗口：单击任务
        private void livTaskView_MouseClick(object sender, MouseEventArgs e)
        {
            //获取选定项的任务状态
            SetTaskStatusUi(splitContainerRight.livTaskView.GetSelectedItemStatus());
        }
        //任务运行信息窗口：选定项改变
        private void livTaskView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.splitContainerRight.livTaskView.SelectedItems.Count == 0)
            {
                SetDefaultUI();
            }
            else if (this.splitContainerRight.livTaskView.SelectedItems.Count == 1)
            {
                foreach (Utility.TaskViewItem item in this.splitContainerRight.livTaskView.SelectedItems)
                {
                    SetTaskStatusUi(splitContainerRight.livTaskView.GetSelectedItemStatus());
                }
            }
            else
            {
                SetSelectMultiTask();
            }
        }
        #endregion

        #region 私有方法定义
        /// <summary>
        /// 加载系统配置文件
        /// </summary>
        private void LoadConfiguration() {
            string configPath = AppDomain.CurrentDomain.BaseDirectory + "Configuration.xml";
            Stream readStream;
            try {
                XmlSerializer xs = new XmlSerializer(typeof(Config.Configuration));
                readStream = new FileStream(configPath, FileMode.Open, FileAccess.Read, FileShare.Delete);
                this._Configuration = (Config.Configuration)xs.Deserialize(readStream);
                readStream.Close();
                readStream.Dispose();
            } catch {
                readStream = null;

                SaveConfiguration();
            }
        }
        /// <summary>
        /// 保存配置文件
        /// </summary>
        private void SaveConfiguration() {
            string configPath = AppDomain.CurrentDomain.BaseDirectory + "Configuration.xml";
            File.Delete(configPath);
            XmlSerializer xs = new XmlSerializer(typeof(Config.Configuration));
            Stream WriteStream = new FileStream(configPath, FileMode.Create, FileAccess.Write, FileShare.Read);
            xs.Serialize(WriteStream, this._Configuration);
            WriteStream.Close();
            WriteStream.Dispose();
        }
        private TreeNode RecursiveCreateNode(string id, DataTable dt) {
            TreeNode node = new TreeNode();
            foreach (DataRow row in dt.Rows) {
                if (row["pid"].ToString().Equals(id)) {
                    node.Text = row["nodeName"].ToString();
                } else {
                    return RecursiveCreateNode(row[""].ToString(), dt);
                }
            }
            return node;
        }        
        #endregion

        #region 私有字段定义
        private Configuration _Configuration = new Configuration();
        #endregion

        #region 公共属性定义
        /// <summary>
        /// 系统配置
        /// </summary>
        public Configuration Configuration {
            get { return _Configuration; }
            set { _Configuration = value; }
        }
        #endregion
    }
}