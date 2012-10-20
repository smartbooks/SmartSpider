using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartSpider {
    public partial class FrmReplace : Form {
        public List<Config.Replacement> Replace = new List<Config.Replacement>();
        
        public FrmReplace(List<Config.Replacement> replace) {
            InitializeComponent();

            this.Replace = replace;

            BindDataToFormControle();
        }

        #region 按钮事件
        //关闭
        private void btnSubmit_Click(object sender, EventArgs e) {
            this.Close();
        }
        //创建
        private void btnCreate_Click(object sender, EventArgs e) {
            this.txtNewValue.Text = "";
            this.txtOldValue.Text = "";
            this.chbOnlyOldValue.Checked = false;
            this.chbRepeatedReplacement.Checked = false;
            this.chbUseRegex.Checked = false;
            this.txtOldValue.Focus();
        }
        //保存
        private void btnSave_Click(object sender, EventArgs e) {
            if (CheckInput()) {
                Config.Replacement elm = new Config.Replacement();
                elm.NewValue = this.txtNewValue.Text;
                elm.OldValue = this.txtOldValue.Text;
                elm.OnlyMatchOldValue = this.chbOnlyOldValue.Checked;
                elm.Repeatable = this.chbRepeatedReplacement.Checked;
                elm.UseRegex = this.chbUseRegex.Checked;

                Replace.Add(elm);
                BindDataToFormControle();

                this.txtNewValue.Clear();
                this.txtOldValue.Clear();
                this.chbOnlyOldValue.Checked = false;
                this.chbRepeatedReplacement.Checked = false;
                this.chbUseRegex.Checked = false;
                this.txtOldValue.Focus();
            }
        }
        //删除
        private void btnDelete_Click(object sender, EventArgs e) {            
            foreach (ListViewItem item in this.livReplaceElement.SelectedItems) {
                foreach (Config.Replacement r in Replace) {
                    if (r.OldValue.Equals(item.Text)) {
                        Replace.Remove(r);
                        break;
                    }
                }
            }
            BindDataToFormControle();
        }
        //上移
        private void btnMoveUp_Click(object sender, EventArgs e) {

        }
        //下移
        private void btnMoveDown_Click(object sender, EventArgs e) {

        }
        //清空
        private void btnClear_Click(object sender, EventArgs e) {
            Replace.Clear();
            BindDataToFormControle();
        }
        //导入
        private void btnImport_Click(object sender, EventArgs e) {

        }
        //导出
        private void btnExport_Click(object sender, EventArgs e) {

        }
        #endregion

        #region 私有方法定义
        /// <summary>
        /// 绑定数据到控件
        /// </summary>
        private void BindDataToFormControle() {
            this.livReplaceElement.Items.Clear();
            foreach (Config.Replacement e in this.Replace) {
                ListViewItem item = new ListViewItem();
                item.Text = e.OldValue;
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, e.NewValue));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, e.UseRegex.ToString()));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, e.OnlyMatchOldValue.ToString()));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, e.Repeatable.ToString()));

                this.livReplaceElement.Items.Add(item);
            }
        }

        /// <summary>
        /// 校验用户输入
        /// </summary>
        /// <returns></returns>
        private bool CheckInput() {
            if (string.IsNullOrEmpty(txtOldValue.Text)) {
                MessageBox.Show("旧值不能为空", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                this.txtOldValue.Focus();
                return false;
            }
            return true;
        }
        #endregion
    }
}