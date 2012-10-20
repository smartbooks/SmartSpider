namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;

    /// <summary>
    /// HTTP请求头设置
    /// </summary>
    [Serializable]
    public class RequestHeader {

        private string _Name;
        private string _Value;

        /// <summary>
        /// 请求头
        /// </summary>
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        public string Value {
            get {
                return _Value;
            }
            set {
                _Value = value;
            }
        }
    }
}
