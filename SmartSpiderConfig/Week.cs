namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 周、星期枚举定义
    /// </summary>
    [Serializable]
    public enum Week {
        /// <summary>
        /// 星期一
        /// </summary>
        [XmlEnum("Monday")]
        Monday = 0,
        /// <summary>
        /// 星期二
        /// </summary>
        [XmlEnum("Tuesday")]
        Tuesday = 1,
        /// <summary>
        /// 星期三
        /// </summary>
        [XmlEnum("Wednesday")]
        Wednesday = 2,
        /// <summary>
        /// 星期四
        /// </summary>
        [XmlEnum("Thursday")]
        Thursday = 3,
        /// <summary>
        /// 星期五
        /// </summary>
        [XmlEnum("Friday")]
        Friday = 4,
        /// <summary>
        /// 星期六
        /// </summary>
        [XmlEnum("Saturday")]
        Saturday = 5,
        /// <summary>
        /// 星期日
        /// </summary>
        [XmlEnum("Sunday")]
        Sunday = 6,
    }
}
