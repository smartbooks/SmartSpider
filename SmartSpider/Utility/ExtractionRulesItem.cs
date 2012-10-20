namespace SmartSpider.Utility {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using Config;

    /// <summary>
    /// 提取规则项
    /// </summary>
    public class ExtractionRulesItem : ListViewItem {

        public ExtractionRule rule;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rule">采集规则</param>
        ///<param name="index">层次编号</param>
        public ExtractionRulesItem(ExtractionRule rule, int index) {
            this.SubItems.Clear();
            this.rule = rule;
            this.Text = index.ToString();
            this.ImageKey = "taskmin.png";
            this.Font = new System.Drawing.Font("宋体", 10);
            this.SubItems.Add(new ListViewSubItem(this, rule.Name));
            this.SubItems.Add(new ListViewSubItem(this, rule.Layer.ToString()));
            this.SubItems.Add(new ListViewSubItem(this, rule.DataColumn));
            this.SubItems.Add(new ListViewSubItem(this, rule.DataUnique.ToString()));
            this.SubItems.Add(new ListViewSubItem(this, rule.PreviousFlag));
            this.SubItems.Add(new ListViewSubItem(this, rule.FollowingFlag));
        }

        /// <summary>
        /// 刷新项状态
        /// </summary>
        public void Referer() {
            string t = this.Text;
            this.SubItems.Clear();
            this.Text = t;
            this.Font = new System.Drawing.Font("宋体", 10);
            this.SubItems.Add(new ListViewSubItem(this, rule.Name));
            this.SubItems.Add(new ListViewSubItem(this, rule.Layer.ToString()));
            this.SubItems.Add(new ListViewSubItem(this, rule.DataColumn));
            this.SubItems.Add(new ListViewSubItem(this, rule.DataUnique.ToString()));
            this.SubItems.Add(new ListViewSubItem(this, rule.PreviousFlag));
            this.SubItems.Add(new ListViewSubItem(this, rule.FollowingFlag));
        }
    }
}
