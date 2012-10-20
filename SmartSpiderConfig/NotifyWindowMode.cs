namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 悬浮窗窗口应用模式
    /// </summary>
    [Serializable]
    public enum NotifyWindowMode {
        /// <summary>
        /// 所有任务
        /// </summary>
        [XmlEnum("AllTasks")]
        AllTasks = 0,
        /// <summary>
        /// 当前文件夹中的任务
        /// </summary>
        [XmlEnum("CurrentFolder")]
        CurrentFolder = 1,
        /// <summary>
        /// 当前任务
        /// </summary>
        [XmlEnum("CurrentTask")]
        CurrentTask = 2,
    }
}
