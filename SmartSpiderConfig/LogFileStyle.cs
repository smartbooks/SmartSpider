
namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 日志文件样式
    /// </summary>
    [Serializable]
    public enum LogFileStyle {
        /// <summary>
        /// 单个文件
        /// </summary>
        [XmlEnum("Single")]
        Single = 0,
        /// <summary>
        /// 多个文件
        /// </summary>
        [XmlEnum("Pairs")]
        Pairs = 1,
    }
}
