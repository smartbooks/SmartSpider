namespace SmartSpider {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Config;

    public partial class FrmOption : Form {
        public FrmOption(Configuration config) {
            InitializeComponent();
            this._config = config;
        }

        private Configuration _config = new Configuration();
    }
}
