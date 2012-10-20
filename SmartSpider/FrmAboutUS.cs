using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartSpider {
    public partial class FrmAboutUS : Form {
        public FrmAboutUS() {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e) {
            this.Close();
            this.Dispose();
        }
    }
}
