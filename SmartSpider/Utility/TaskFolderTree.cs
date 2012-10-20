namespace SmartSpider.Utility {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.IO;
    using System.Xml.Serialization;
    using Config;

    public class TaskFolderTree : TreeView {
        #region 公共方法定义
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskFolderTree() {

            //初始化组件
            InitializeComponent();

            ReLoad();
        }

        /// <summary>
        /// 重新加载并刷新任务夹树
        /// </summary>
        public void ReLoad() {
            this.Nodes.Clear();            
            this.Nodes.Add(Load(_defaultTaskPath, "根文件夹"));
            this.Refresh();
        }

        /// <summary>
        /// 加载Task任务目录树
        /// </summary>
        /// <param name="taskPath">Task配置文件目录</param>
        /// <param name="parentNodeName">根节点名称</param>
        /// <returns>Task任务节点树</returns>
        private TreeNode Load(string rootPath, string parentNodeName) {
            string[] dirs = Directory.GetDirectories(rootPath);
            TreeNode node = new TreeNode(parentNodeName);
            node.Expand();
            node.Tag = rootPath;
            node.ImageKey = "foldermax.png";
            
            foreach (string path in dirs)
            {
                string[] curDir = path.Split('\\');
                string nodeName = curDir[curDir.Length - 1];
                node.Nodes.Add(Load(path, nodeName));
            }
            return node;
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitializeComponent()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.ImageIndex = 8;            
            this.ItemHeight = 20;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "trwTaskFolder";
            this.SelectedImageIndex = 8;
            this.Size = new System.Drawing.Size(200, 288);
            this.TabIndex = 0;
            this.HideSelection = false;

            //快捷菜单
            //this.ContextMenu = null;
        }
        #endregion

        #region 私有变量定义
        /// <summary>
        /// 默认Task任务文件夹路径
        /// </summary>
        private string _defaultTaskPath = AppDomain.CurrentDomain.BaseDirectory + "Task";
        #endregion
    }
}
