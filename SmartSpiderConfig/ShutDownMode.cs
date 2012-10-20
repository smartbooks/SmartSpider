namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 完成所有任务后关机模式
    /// </summary>
    [Serializable]
    public enum ShutDownMode {
        /// <summary>
        /// 休眠计算机
        /// </summary>
        [XmlEnum("Sleep")]
        Sleep = 0,
        /// <summary>
        /// 关闭计算机
        /// </summary>
        [XmlEnum("PowerOff")]
        PowerOff = 1,
    }
}
