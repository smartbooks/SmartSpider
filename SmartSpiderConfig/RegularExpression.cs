namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("RegularExpression")]
    public class RegularExpression {
        public RegularExpression() {
            this._Name = "";
            this._Expression = "";
        }

        #region 私有变量定义
        private string _Expression;
        private string _Name;
        #endregion

        #region 公共属性定义

        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }

        public string Expression {
            get {
                return _Expression;
            }
            set {
                _Expression = value;
            }
        }

        #endregion
    }
}
