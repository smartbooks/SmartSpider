namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 数据库类型
    /// </summary>
    [Serializable]
    public enum DatabaseType {
        /// <summary>
        /// Access数据库
        /// </summary>
        [XmlEnum("Access")]
        Access = 0,
        /// <summary>
        /// SqlServer数据库
        /// </summary>
        [XmlEnum("SqlServer")]
        SqlServer = 1,
        /// <summary>
        /// MySql数据库
        /// </summary>
        [XmlEnum("MySql")]
        MySql = 2,
        /// <summary>
        /// Oracle数据库
        /// </summary>
        [XmlEnum("Oracle")]
        Oracle = 3,
        /// <summary>
        /// SqlLite数据库
        /// </summary>
        [XmlEnum("SqlLite")]
        SqlLite = 4,
    }
}
