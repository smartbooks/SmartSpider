namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("Filter")]
    public class Filter {

        #region 私有变量定义
        private string _JoinMode;
        private string _Operator;
        private string _Value;
        #endregion

        #region 公共属性定义
        public string Operator {
            get {
                return _Operator;
            }
            set {
                _Operator = value;
            }
        }

        public string Value {
            get {
                return _Value;
            }
            set {
                _Value = value;
            }
        }

        public string JoinMode {
            get {
                return _JoinMode;
            }
            set {
                _JoinMode = value;
            }
        }
        #endregion
    }
}
