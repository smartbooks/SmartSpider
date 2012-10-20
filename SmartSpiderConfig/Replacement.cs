namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("Replacement")]
    public class Replacement {
        public Replacement() {
            this._NewValue = "";
            this._OldValue = "";
            this._OnlyMatchOldValue = false;
            this._Repeatable = false;
            this._UseRegex = false;
        }

        #region 私有变量定义
        private string _NewValue;
        private string _OldValue;
        private bool _OnlyMatchOldValue;
        private bool _Repeatable;
        private bool _UseRegex;
        #endregion

        #region 公共属性定义
        /// <summary>
        /// 旧值
        /// </summary>
        public string OldValue {
            get {
                return _OldValue;
            }
            set {
                _OldValue = value;
            }
        }

        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue {
            get {
                return _NewValue;
            }
            set {
                _NewValue = value;
            }
        }

        /// <summary>
        /// 使用正则表达式
        /// </summary>
        public bool UseRegex {
            get {
                return _UseRegex;
            }
            set {
                _UseRegex = value;
            }
        }

        /// <summary>
        /// 只匹配旧值
        /// </summary>
        public bool OnlyMatchOldValue {
            get {
                return _OnlyMatchOldValue;
            }
            set {
                _OnlyMatchOldValue = value;
            }
        }

        /// <summary>
        /// 重复替换
        /// </summary>
        public bool Repeatable {
            get {
                return _Repeatable;
            }
            set {
                _Repeatable = value;
            }
        }
        #endregion
    }
}
