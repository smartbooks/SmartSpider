namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 任务状态
    /// </summary>
    [Serializable]
    public enum Action {
        /// <summary>
        /// 准备
        /// </summary>
        [XmlEnum("Ready")]
        Ready = 0,
        /// <summary>
        /// 停止
        /// </summary>
        [XmlEnum("Stop")]
        Stop = 1,
        /// <summary>
        /// 开始
        /// </summary>
        [XmlEnum("Start")]
        Start = 2,
        /// <summary>
        /// 暂停
        /// </summary>
        [XmlEnum("Pause")]
        Pause = 3,
        /// <summary>
        /// 完成
        /// </summary>
        [XmlEnum("Finish")]
        Finish = 4,
        /// <summary>
        /// 运行中
        /// </summary>
        [XmlEnum("Running")]
        Running = 5,
        /// <summary>
        /// 无状态
        /// </summary>
        [XmlEnum("None")]
        None = 6,
    }
}
