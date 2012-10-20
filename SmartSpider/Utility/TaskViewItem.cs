namespace SmartSpider.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Config;

    /// <summary>
    /// 任务项
    /// </summary>
    public class TaskViewItem : ListViewItem
    {
        public TaskViewItem(string taskName, string tiptext)
        {            
            SubItems.Clear();
            Text = taskName;
            ToolTipText = tiptext;            
            SubItems.Add(new ListViewItem.ListViewSubItem(this, "0"));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, "0"));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, "0"));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, "0"));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, "0"));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, "0"));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, "0"));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, "0"));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, "0"));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, "0"));
            SubItems.Add(new ListViewItem.ListViewSubItem(this, "0"));
        }
    }
}
