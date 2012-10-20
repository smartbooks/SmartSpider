using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartSpider.Utility
{
    public class SplitContainerRight : SplitContainer
    {
        #region 公共属性定义
        public TaskListView livTaskView;
        #endregion

        #region 公共方法定义
        /// <summary>
        /// 构造函数
        /// </summary>
        public SplitContainerRight()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有方法定义
        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitializeComponent()
        {
            #region 初始化选项卡组件
            this.tabContent = new System.Windows.Forms.TabControl();
            this.tabContent.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContent.Location = new System.Drawing.Point(0, 0);
            this.tabContent.Multiline = true;
            this.tabContent.Name = "tabContent";
            this.tabContent.SelectedIndex = 0;
            this.tabContent.Size = new System.Drawing.Size(389, 134);
            this.tabContent.TabIndex = 0;
            this.tabContent.ResumeLayout(false);

            TaskResultLog webPage = new TaskResultLog("SmartSpider", new List<Config.ExtractionRule>());
            webPage.Controls.Clear();
            webPage.Controls.Add(new WebBrowser()
            {
                Url = new Uri("http://www.google.com.hk"),
                Dock = DockStyle.Fill
            });
             //加入默认选项卡
            this.tabContent.Controls.Add(webPage);
            #endregion

            livTaskView = new TaskListView();

            #region 初始化面板
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "";
            this.Orientation = System.Windows.Forms.Orientation.Horizontal;

            //panel1 
            this.Panel1.Controls.Add(this.livTaskView);
            this.Panel1MinSize = 150;

            // panel2
            this.Panel2.Controls.Add(this.tabContent);
            this.Panel2MinSize = 100;
            this.Size = new System.Drawing.Size(389, 288);
            this.SplitterDistance = 150;
            this.TabIndex = 0;

            //挂起Panel1 and panel2
            this.Panel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            #endregion

            #region 任务项订阅事件
            this.livTaskView.OnTaskStatusChanges += new Config.OnTaskStatusChanges(livTaskView_OnTaskStatusChanges);
            this.livTaskView.OnTaskComplete += new Config.OnTaskComplete(livTaskView_OnTaskComplete);
            this.livTaskView.OnTaskStart += new Config.OnTaskStart(livTaskView_OnTaskStart);
            this.livTaskView.OnTaskPause += new Config.OnTaskPause(livTaskView_OnTaskPause);
            this.livTaskView.OnTaskStop += new Config.OnTaskStop(livTaskView_OnTaskStop);
            this.livTaskView.OnAppendSingileLog += new Config.OnAppendSingileLog(livTaskView_OnAppendSingileLog);
            this.livTaskView.OnAppendSingleResult += new Config.OnAppendSingleResult(livTaskView_OnAppendSingleResult);
            this.livTaskView.OnPublishResult += new Config.OnPublishResult(livTaskView_OnPublishResult);
            this.livTaskView.OnShowTaskRuntimeInfo +=new OnShowTaskRuntimeInfo(livTaskView_OnShowTaskRuntimeInfo);
            #endregion

            #region 选项卡快捷菜单初始化
            tabContentQuickMenu = new System.Windows.Forms.ContextMenu();
            tabContentQuickMenu.MenuItems.Add(new MenuItem("关闭", new EventHandler(tabContentQuickMenu_MenuItem_Close)));
            #endregion

            #region 选项卡事件订阅
            tabContent.Selected += new TabControlEventHandler(tabContent_Selected);
            tabContent.ContextMenu = tabContentQuickMenu;
            #endregion
        }

        #region 任务项事件
        //状态改变
        private void livTaskView_OnTaskStatusChanges(object sender, Config.Action action)
        {

        }
        //任务完成
        private void livTaskView_OnTaskComplete(object sender, Config.LogEventArgs e)
        {
            Config.TaskUnit unit = (Config.TaskUnit)sender;
        }
        //任务开始
        private void livTaskView_OnTaskStart(object sender, Config.LogEventArgs e)
        {
            Config.TaskUnit unit = (Config.TaskUnit)sender;

            //判断选项卡集合中是否已经存在
            foreach (TabPage page in this.tabContent.TabPages)
            {
                if (page.Text.Equals(unit.TaskConfig.Name))
                {
                    this.tabContent.SelectedTab = page;
                    return;
                }
            }
            //添加一个新的选项卡
            Utility.TaskResultLog fromLogPanel = new Utility.TaskResultLog(unit.TaskConfig.Name, unit.TaskConfig.ExtractionRules);
            this.tabContent.TabPages.Add(fromLogPanel);
            this.tabContent.SelectedTab = fromLogPanel;
        }
        //任务暂停
        private void livTaskView_OnTaskPause(object sender, Config.LogEventArgs e) { }
        //任务停止
        private void livTaskView_OnTaskStop(object sender, Config.LogEventArgs e) { }
        //追加日志
        private void livTaskView_OnAppendSingileLog(object sender, Config.LogEventArgs e)
        {
            Config.TaskUnit unit = (Config.TaskUnit)sender;
            //for (int i = 1; i < this.tabContent.TabPages.Count; i++)
            //{
            //    if (tabContent.TabPages[i].Text.Equals(unit.TaskConfig.Name))
            //    {
            //        ((TaskResultLog)tabContent.TabPages[i]).AppendLogevent(e);
            //        return;
            //    }
            //}
            foreach (TaskResultLog page in this.tabContent.TabPages)
            {
                if (page.Text.Equals(unit.TaskConfig.Name))
                {
                    page.AppendLogevent(e);
                    return;
                }
            }
        }
        //增加结果行
        private void livTaskView_OnAppendSingleResult(object sender, object[] values)
        {
            Config.TaskUnit unit = (Config.TaskUnit)sender;

            foreach (TaskResultLog page in this.tabContent.TabPages)
            {
                if (page.Text.Equals(unit.TaskConfig.Name))
                {
                    if (values != null) {
                        page.AppendRowResult(values);
                    }
                }
            }
        }
        //发布结果
        private void livTaskView_OnPublishResult(object sender, Config.LogEventArgs e) { }
        //显示任务运行信息
        private void livTaskView_OnShowTaskRuntimeInfo(object sender, EventArgs e)
        {
            //便利选定的任务项
            foreach (ListViewItem item in this.livTaskView.SelectedItems)
            {
                //与任务集合项进行匹配
                foreach (Config.TaskUnit unit in this.livTaskView._TaskItem)
                {
                    //判断名称是否一致
                    if (item.Text.Equals(unit.TaskConfig.Name))
                    {
                        bool isShow = true;
                        foreach (TabPage page in this.tabContent.TabPages)
                        {
                            if (page.Text.Equals(unit.TaskConfig.Name))
                            {
                                isShow = false;
                                break;
                            }
                        }
                        //创建一个选项卡
                        if (isShow)
                        {
                            Utility.TaskResultLog fromLogPanel = new Utility.TaskResultLog(unit.TaskConfig.Name, unit.TaskConfig.ExtractionRules);
                            this.tabContent.TabPages.Add(fromLogPanel);
                            this.tabContent.SelectedTab = fromLogPanel;
                        }
                        break;
                    }
                }
            }
        }
        #endregion

        #region 选项卡事件
        //选择当前选项卡
        private void tabContent_Selected(object sender, TabControlEventArgs e)
        {
            foreach (TaskViewItem item in this.livTaskView.Items)
            {
                if (e.TabPage != null && item.Text.Equals(e.TabPage.Text))
                {
                    item.Selected = true;
                }
                else
                {
                    item.Selected = false;
                }
            }
        }
        #endregion

        #region 选项卡菜单事件
        //关闭菜单事件
        private void tabContentQuickMenu_MenuItem_Close(object sender, EventArgs e)
        {
            if (!tabContent.TabPages[tabContent.SelectedIndex].Text.Equals("SmartSpider"))
            {
                tabContent.TabPages.RemoveAt(tabContent.SelectedIndex);
            }
        }
        #endregion
        #endregion

        #region 私有字段定义
        private TabControl tabContent;
        private ContextMenu tabContentQuickMenu;
        #endregion
    }
}
