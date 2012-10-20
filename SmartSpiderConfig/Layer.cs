namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 采集页面层次
    /// </summary>
    [Serializable]
    public enum Layer {
        /// <summary>
        /// 最终页面
        /// </summary>
        [XmlEnum("Terminator")]
        Terminator = 0,
        /// <summary>
        /// 中间页面
        /// </summary>
        [XmlEnum("Center")]
        Center = 1
    }
}
