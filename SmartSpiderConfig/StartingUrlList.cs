namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 起始网址配置类
    /// 版  本:V1.0
    /// 标  志:20120228
    /// </summary>
    [Serializable]
    [XmlRoot("StartingUrlList")]
    public class StartingUrlList {
        private string _String;

        /// <summary>
        /// 起始Url地址
        /// </summary>
        public string String {
            get {
                return _String;
            }
            set {
                _String = value;
            }
        }
    }
}
