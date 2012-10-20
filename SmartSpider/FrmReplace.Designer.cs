namespace SmartSpider {
    partial class FrmReplace {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbRepeatedReplacement = new System.Windows.Forms.CheckBox();
            this.chbOnlyOldValue = new System.Windows.Forms.CheckBox();
            this.txtNewValue = new System.Windows.Forms.TextBox();
            this.txtOldValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.livReplaceElement = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chbUseRegex = new System.Windows.Forms.CheckBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbRepeatedReplacement);
            this.groupBox1.Controls.Add(this.chbOnlyOldValue);
            this.groupBox1.Controls.Add(this.txtNewValue);
            this.groupBox1.Controls.Add(this.txtOldValue);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.livReplaceElement);
            this.groupBox1.Controls.Add(this.chbUseRegex);
            this.groupBox1.Controls.Add(this.btnCreate);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnMoveUp);
            this.groupBox1.Controls.Add(this.btnMoveDown);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(583, 306);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "替换列表";
            // 
            // chbRepeatedReplacement
            // 
            this.chbRepeatedReplacement.AutoSize = true;
            this.chbRepeatedReplacement.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chbRepeatedReplacement.Location = new System.Drawing.Point(385, 242);
            this.chbRepeatedReplacement.Name = "chbRepeatedReplacement";
            this.chbRepeatedReplacement.Size = new System.Drawing.Size(70, 16);
            this.chbRepeatedReplacement.TabIndex = 34;
            this.chbRepeatedReplacement.Text = "重复替换";
            this.chbRepeatedReplacement.UseVisualStyleBackColor = true;
            // 
            // chbOnlyOldValue
            // 
            this.chbOnlyOldValue.AutoSize = true;
            this.chbOnlyOldValue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chbOnlyOldValue.Location = new System.Drawing.Point(253, 244);
            this.chbOnlyOldValue.Name = "chbOnlyOldValue";
            this.chbOnlyOldValue.Size = new System.Drawing.Size(82, 16);
            this.chbOnlyOldValue.TabIndex = 33;
            this.chbOnlyOldValue.Text = "只提取旧值";
            this.chbOnlyOldValue.UseVisualStyleBackColor = true;
            // 
            // txtNewValue
            // 
            this.txtNewValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewValue.Location = new System.Drawing.Point(99, 201);
            this.txtNewValue.Multiline = true;
            this.txtNewValue.Name = "txtNewValue";
            this.txtNewValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNewValue.Size = new System.Drawing.Size(412, 35);
            this.txtNewValue.TabIndex = 32;
            // 
            // txtOldValue
            // 
            this.txtOldValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOldValue.Location = new System.Drawing.Point(99, 160);
            this.txtOldValue.Multiline = true;
            this.txtOldValue.Name = "txtOldValue";
            this.txtOldValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOldValue.Size = new System.Drawing.Size(412, 35);
            this.txtOldValue.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "新值";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "旧值";
            // 
            // livReplaceElement
            // 
            this.livReplaceElement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.livReplaceElement.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.livReplaceElement.Font = new System.Drawing.Font("宋体", 10F);
            this.livReplaceElement.FullRowSelect = true;
            this.livReplaceElement.Location = new System.Drawing.Point(6, 20);
            this.livReplaceElement.MultiSelect = false;
            this.livReplaceElement.Name = "livReplaceElement";
            this.livReplaceElement.Size = new System.Drawing.Size(571, 129);
            this.livReplaceElement.TabIndex = 14;
            this.livReplaceElement.UseCompatibleStateImageBehavior = false;
            this.livReplaceElement.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "旧值";
            this.columnHeader1.Width = 165;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "新值";
            this.columnHeader2.Width = 165;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "使用正则式";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "只提取旧值";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "重复替换";
            this.columnHeader5.Width = 80;
            // 
            // chbUseRegex
            // 
            this.chbUseRegex.AutoSize = true;
            this.chbUseRegex.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chbUseRegex.Location = new System.Drawing.Point(99, 244);
            this.chbUseRegex.Name = "chbUseRegex";
            this.chbUseRegex.Size = new System.Drawing.Size(106, 16);
            this.chbUseRegex.TabIndex = 13;
            this.chbUseRegex.Text = "使用正则表达式";
            this.chbUseRegex.UseVisualStyleBackColor = true;
            // 
            // btnCreate
            // 
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreate.Location = new System.Drawing.Point(55, 273);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(60, 27);
            this.btnCreate.TabIndex = 12;
            this.btnCreate.Text = "新建(&N)";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(121, 273);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 27);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(187, 273);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 27);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMoveUp.Location = new System.Drawing.Point(253, 273);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(60, 27);
            this.btnMoveUp.TabIndex = 9;
            this.btnMoveUp.Text = "上移(&U)";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMoveDown.Location = new System.Drawing.Point(319, 273);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(60, 27);
            this.btnMoveDown.TabIndex = 8;
            this.btnMoveDown.Text = "下移(&M)";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(385, 273);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(60, 27);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "清空(&C)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnImport
            // 
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnImport.Location = new System.Drawing.Point(451, 273);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(60, 27);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "导入(&I)";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExport.Location = new System.Drawing.Point(517, 273);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(60, 27);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "导出(&E)";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSubmit.Location = new System.Drawing.Point(520, 324);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "关闭(&C)";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // FrmReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 359);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmReplace";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.CheckBox chbUseRegex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView livReplaceElement;
        private System.Windows.Forms.CheckBox chbRepeatedReplacement;
        private System.Windows.Forms.CheckBox chbOnlyOldValue;
        private System.Windows.Forms.TextBox txtNewValue;
        private System.Windows.Forms.TextBox txtOldValue;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}