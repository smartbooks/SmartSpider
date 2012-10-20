namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 任务调度模式
    /// </summary>
    [Serializable]
    public enum ScheduleMode {
        /// <summary>
        /// 每间隔X时间段
        /// </summary>
        [XmlEnum("Time")]
        Time = 0,
        /// <summary>
        /// 每间隔X天
        /// </summary>
        [XmlEnum("Day")]
        Day = 1,
    }
}