namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 采集任务XML配置文件类
    /// 版   本:V1.0
    /// 创建日志:20120225
    /// </summary>
    [Serializable]
    [XmlRoot("Task")]
    public class Task {

        public Task() {
            this._State = Config.Action.Ready;
            this._ElapsedTime = 0;
            this._StartingTime = new DateTime(1, 1, 1);
            this._ThreadNumber = 1;
            this._OutputDetailedLog = true;
            this._SaveLogToFile = false;
            this._lastLogFileName = "";
            this._Cookie = "";
            this._LoginUrl = "";
            this._LoginAutomatically = false;
            this._LoginTargetUrl = "";
            this._LoginSuccessFlag = "";
            this._LoginAtRegularIntervals = false;
            this._LoginInterval = 20;
            this._UsePluginOfLogin = false;
            this._LoginUrlReferer = "";
            this._ScheduleEnabled = false;
            this._ScheduleMode = Config.ScheduleMode.Time;
            this._ScheduleDays = 0;
            this._ScheduleHours = 0;
            this._ScheduleMinutes = 0;
            this._ScheduleLimitTimeRange = false;
            this._ScheduleFromHour = 0;
            this._ScheduleToHour = 23;
            this._ScheduleFromDayOfWeek = Week.Sunday;
            this._ScheduleToDayOfWeek = Week.Saturday;
            this._ScheduleFromDay = 1;
            this._ScheduleToDay = 31;
            this._DisableScheduleAfterFinish = false;
            this._lastStoppingTime = new DateTime(1, 1, 1);
            this._ConnectionString = "";
            this._DatabaseType = Config.DatabaseType.Access;
            this._PublicationTarget = "";
            this._UseProcedure = false;
            this._PublishResultDircetly = false;
            this._DeleteResultAfterPublication = false;
            this._IgnoreDataColumnNotFound = false;
            this._SaveRepeatedRows = false;
            this._SaveErrorRows = false;
            this._UsePluginOfProcessResultRow = false;
            this._CurrentResultCount = 0;
            this._ResultCount = 0;
            this._RepeatedRowsCount = 0;
            this._ErrorRowsCount = 0;
            this._PluginPath = "";
            this._UsePluginOfDownloadContentFile = false;
            this._UsePluginOfDownloadSingleFile = false;
            this._UsePluginOfProcessContentFile = false;
            this._UsePluginOfProcessSingleFile = false;
            this._UsePluginOfFilter = false;
            this._PluginData = "";
            this._ExtractionRules = new List<ExtractionRule>();
            this._UrlListManager = new UrlListManager();
        }

        #region 私有变量定义
        private string _ConnectionString;
        private string _Cookie;
        private int _CurrentResultCount;
        private DatabaseType _DatabaseType;
        private bool _DeleteResultAfterPublication;
        private string _Description;
        private bool _DisableScheduleAfterFinish;
        private int _ElapsedTime;
        private int _ErrorRowsCount;
        private List<ExtractionRule> _ExtractionRules;
        private bool _IgnoreDataColumnNotFound;
        private string _lastLogFileName;
        private DateTime _lastStoppingTime;
        private bool _LoginAtRegularIntervals;
        private bool _LoginAutomatically;
        private int _LoginInterval;
        private string _LoginSuccessFlag;
        private string _LoginTargetUrl;
        private string _LoginUrl;
        private string _LoginUrlReferer;
        private string _Name;
        private bool _OutputDetailedLog;
        private string _PluginData;
        private string _PluginPath;
        private string _PublicationTarget;
        private bool _PublishResultDircetly;
        private int _RepeatedRowsCount;
        private int _ResultCount;
        private bool _SaveErrorRows;
        private bool _SaveLogToFile;
        private bool _SaveRepeatedRows;
        private int _ScheduleDays;
        private bool _ScheduleEnabled;
        private int _ScheduleFromDay;
        private Week _ScheduleFromDayOfWeek;
        private int _ScheduleFromHour;
        private int _ScheduleHours;
        private bool _ScheduleLimitTimeRange;
        private int _ScheduleMinutes;
        private ScheduleMode _ScheduleMode;
        private int _ScheduleToDay;
        private Week _ScheduleToDayOfWeek;
        private int _ScheduleToHour;
        private DateTime _StartingTime;
        private Action _State;
        private int _ThreadNumber;
        private UrlListManager _UrlListManager;
        private bool _UsePluginOfDownloadContentFile;
        private bool _UsePluginOfDownloadSingleFile;
        private bool _UsePluginOfFilter;
        private bool _UsePluginOfGetWebProxy;
        private bool _UsePluginOfLogin;
        private bool _UsePluginOfProcessContentFile;
        private bool _UsePluginOfProcessResultRow;
        private bool _UsePluginOfProcessSingleFile;
        private bool _UseProcedure;
        #endregion

        #region 公共属性定义

        /// <summary>
        /// 任务名称
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
        /// 任务描述
        /// </summary>
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }

        /// <summary>
        /// 任务状态
        /// </summary>
        public Action State {
            get {
                return _State;
            }
            set {
                _State = value;
            }
        }

        /// <summary>
        /// 运行时间(秒)
        /// </summary>
        public int ElapsedTime {
            get {
                return _ElapsedTime;
            }
            set {
                _ElapsedTime = value;
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartingTime {
            get {
                return _StartingTime;
            }
            set {
                _StartingTime = value;
            }
        }

        /// <summary>
        /// 线程数量
        /// </summary>
        public int ThreadNumber {
            get {
                return _ThreadNumber;
            }
            set {
                _ThreadNumber = value;
            }
        }

        /// <summary>
        /// 输出详细日志
        /// </summary>
        public bool OutputDetailedLog {
            get {
                return _OutputDetailedLog;
            }
            set {
                _OutputDetailedLog = value;
            }
        }

        /// <summary>
        /// 保存日志到文件
        /// </summary>
        public bool SaveLogToFile {
            get {
                return _SaveLogToFile;
            }
            set {
                _SaveLogToFile = value;
            }
        }

        /// <summary>
        /// 最后一个日志文件名称
        /// </summary>
        public string lastLogFileName {
            get {
                return _lastLogFileName;
            }
            set {
                _lastLogFileName = value;
            }
        }

        /// <summary>
        /// Cookie
        /// </summary>
        public string Cookie {
            get {
                return _Cookie;            
            }
            set {
                _Cookie = value;
            }
        }

        /// <summary>
        /// 登录网址
        /// </summary>
        public string LoginUrl {
            get {
                return _LoginUrl;
            }
            set {
                _LoginUrl = value;
            }
        }

        /// <summary>
        /// 自动登录
        /// </summary>
        public bool LoginAutomatically {
            get {
                return _LoginAutomatically;
            }
            set {
                _LoginAutomatically = value;
            }
        }

        /// <summary>
        /// 登录目标URL
        /// </summary>
        public string LoginTargetUrl {
            get {
                return _LoginTargetUrl;
            }
            set {
                _LoginTargetUrl = value;
            }
        }

        /// <summary>
        /// 登录成功标志
        /// </summary>
        public string LoginSuccessFlag {
            get {
                return _LoginSuccessFlag;
            }
            set {
                _LoginSuccessFlag = value;
            }
        }

        /// <summary>
        /// 定期登录
        /// </summary>
        public bool LoginAtRegularIntervals {
            get {
                return _LoginAtRegularIntervals;
            }
            set {
                _LoginAtRegularIntervals = value;
            }
        }

        /// <summary>
        /// 登录间隔时间
        /// </summary>
        public int LoginInterval {
            get {
                return _LoginInterval;
            }
            set {
                _LoginInterval = value;
            }
        }

        /// <summary>
        /// 使用插件登录
        /// </summary>
        public bool UsePluginOfLogin {
            get {
                return _UsePluginOfLogin;
            }
            set {
                _UsePluginOfLogin = value;
            }
        }

        /// <summary>
        /// 登录网址的Referer
        /// </summary>
        public string LoginUrlReferer {
            get {
                return _LoginUrlReferer;
            }
            set {
                _LoginUrlReferer = value;
            }
        }

        /// <summary>
        /// 启用定时采集
        /// </summary>
        public bool ScheduleEnabled {
            get {
                return _ScheduleEnabled;
            }
            set {
                _ScheduleEnabled = value;
            }
        }

        /// <summary>
        /// 调度模式
        /// </summary>
        public ScheduleMode ScheduleMode {
            get {
                return _ScheduleMode;
            }
            set {
                _ScheduleMode = value;
            }
        }

        /// <summary>
        /// 定时天
        /// </summary>
        public int ScheduleDays {
            get {
                return _ScheduleDays;
            }
            set {
                _ScheduleDays = value;
            }
        }

        /// <summary>
        /// 每间隔X小时
        /// </summary>
        public int ScheduleHours {
            get {
                return _ScheduleHours;
            }
            set {
                _ScheduleHours = value;
            }
        }

        /// <summary>
        /// 分钟调度
        /// </summary>
        public int ScheduleMinutes {
            get {
                return _ScheduleMinutes;
            }
            set {
                _ScheduleMinutes = value;
            }
        }

        /// <summary>
        /// 限制时间范围
        /// </summary>
        public bool ScheduleLimitTimeRange {
            get {
                return _ScheduleLimitTimeRange;
            }
            set {
                _ScheduleLimitTimeRange = value;
            }
        }

        /// <summary>
        /// 每当X小时调度
        /// </summary>
        public int ScheduleFromHour {
            get {
                return _ScheduleFromHour;
            }
            set {
                _ScheduleFromHour = value;
            }
        }

        /// <summary>
        /// 预定小时
        /// </summary>
        public int ScheduleToHour {
            get {
                return _ScheduleToHour;
            }
            set {
                _ScheduleToHour = value;
            }
        }

        /// <summary>
        /// 每当星期几开始
        /// </summary>
        public Week ScheduleFromDayOfWeek {
            get {
                return _ScheduleFromDayOfWeek;
            }
            set {
                _ScheduleFromDayOfWeek = value;
            }
        }

        /// <summary>
        /// 预定星期几
        /// </summary>
        public Week ScheduleToDayOfWeek {
            get {
                return _ScheduleToDayOfWeek;
            }
            set {
                _ScheduleToDayOfWeek = value;
            }
        }

        /// <summary>
        /// 每当X天
        /// </summary>
        public int ScheduleFromDay {
            get {
                return _ScheduleFromDay;
            }
            set {
                _ScheduleFromDay = value;
            }
        }

        /// <summary>
        /// 预定天
        /// </summary>
        public int ScheduleToDay {
            get {
                return _ScheduleToDay;
            }
            set {
                _ScheduleToDay = value;
            }
        }

        /// <summary>
        /// 任务完成后取消定时采集
        /// </summary>
        public bool DisableScheduleAfterFinish {
            get {
                return _DisableScheduleAfterFinish;
            }
            set {
                _DisableScheduleAfterFinish = value;
            }
        }

        /// <summary>
        /// 最后停止时间
        /// </summary>
        public DateTime lastStoppingTime {
            get {
                return _lastStoppingTime;
            }
            set {
                _lastStoppingTime = value;
            }
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString {
            get {
                return _ConnectionString;
            }
            set {
                _ConnectionString = value;
            }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DatabaseType {
            get {
                return _DatabaseType;
            }
            set {
                _DatabaseType = value;
            }
        }

        /// <summary>
        /// 发布目标：数据库表/存储过程
        /// </summary>
        public string PublicationTarget {
            get {
                return _PublicationTarget;
            }
            set {
                _PublicationTarget = value;
            }
        }

        /// <summary>
        /// 使用过程
        /// </summary>
        public bool UseProcedure {
            get {
                return _UseProcedure;
            }
            set {
                _UseProcedure = value;
            }
        }

        /// <summary>
        /// 发布结果选项（false保存到结果文件 true直接发布到数据库）
        /// </summary>
        public bool PublishResultDircetly {
            get {
                return _PublishResultDircetly;
            }
            set {
                _PublishResultDircetly = value;
            }
        }

        /// <summary>
        /// 发布结果后删除数据
        /// </summary>
        public bool DeleteResultAfterPublication {
            get {
                return _DeleteResultAfterPublication;
            }
            set {
                _DeleteResultAfterPublication = value;
            }
        }

        /// <summary>
        /// 忽略不存在的数据列
        /// </summary>
        public bool IgnoreDataColumnNotFound {
            get {
                return _IgnoreDataColumnNotFound;
            }
            set {
                _IgnoreDataColumnNotFound = value;
            }
        }

        /// <summary>
        /// 保存重复行
        /// </summary>
        public bool SaveRepeatedRows {
            get {
                return _SaveRepeatedRows;
            }
            set {
                _SaveRepeatedRows = value;
            }
        }

        /// <summary>
        /// 保存错误行
        /// </summary>
        public bool SaveErrorRows {
            get {
                return _SaveErrorRows;
            }
            set {
                _SaveErrorRows = value;
            }
        }

        /// <summary>
        /// 使用插件处理采集结果数据行
        /// </summary>
        public bool UsePluginOfProcessResultRow {
            get {
                return _UsePluginOfProcessResultRow;
            }
            set {
                _UsePluginOfProcessResultRow = value;
            }
        }

        /// <summary>
        /// 当前结果计数
        /// </summary>
        public int CurrentResultCount {
            get {
                return _CurrentResultCount;
            }
            set {
                _CurrentResultCount = value;
            }
        }

        /// <summary>
        /// 结果总数
        /// </summary>
        public int ResultCount {
            get {
                return _ResultCount;
            }
            set {
                _ResultCount = value;
            }
        }

        /// <summary>
        /// 重复的行数
        /// </summary>
        public int RepeatedRowsCount {
            get {
                return _RepeatedRowsCount;
            }
            set {
                _RepeatedRowsCount = value;
            }
        }

        /// <summary>
        /// 错误的行数
        /// </summary>
        public int ErrorRowsCount {
            get {
                return _ErrorRowsCount;
            }
            set {
                _ErrorRowsCount = value;
            }
        }

        /// <summary>
        /// 插件路径
        /// </summary>
        public string PluginPath {
            get {
                return _PluginPath;
            }
            set {
                _PluginPath = value;
            }
        }

        /// <summary>
        /// 使用插件下载内容文件
        /// </summary>
        public bool UsePluginOfDownloadContentFile {
            get {
                return _UsePluginOfDownloadContentFile;
            }
            set {
                _UsePluginOfDownloadContentFile = value;
            }
        }

        /// <summary>
        /// 使用插件下载独立文件
        /// </summary>
        public bool UsePluginOfDownloadSingleFile {
            get {
                return _UsePluginOfDownloadSingleFile;
            }
            set {
                _UsePluginOfDownloadSingleFile = value;
            }
        }

        /// <summary>
        /// 使用插件处理下载后的内容文件
        /// </summary>
        public bool UsePluginOfProcessContentFile {
            get {
                return _UsePluginOfProcessContentFile;
            }
            set {
                _UsePluginOfProcessContentFile = value;
            }
        }

        /// <summary>
        /// 使用插件处理单个文件
        /// </summary>
        public bool UsePluginOfProcessSingleFile {
            get {
                return _UsePluginOfProcessSingleFile;
            }
            set {
                _UsePluginOfProcessSingleFile = value;
            }
        }

        /// <summary>
        /// 使用插件处理采集结果数据行
        /// </summary>
        public bool UsePluginOfFilter {
            get {
                return _UsePluginOfFilter;
            }
            set {
                _UsePluginOfFilter = value;
            }
        }

        /// <summary>
        /// 从插件载入代理服务器
        /// </summary>
        public bool UsePluginOfGetWebProxy {
            get {
                return _UsePluginOfGetWebProxy;
            }
            set {
                _UsePluginOfGetWebProxy = value;
            }
        }

        /// <summary>
        /// 提取规则
        /// </summary>
        public List<SmartSpider.Config.ExtractionRule> ExtractionRules {
            get {
                return _ExtractionRules;
            }
            set {
                _ExtractionRules = value;
            }
        }

        /// <summary>
        /// URL列表管理器
        /// </summary>
        public UrlListManager UrlListManager {
            get {
                return _UrlListManager;
            }
            set {
                _UrlListManager = value;
            }
        }

        /// <summary>
        /// 插件数据
        /// </summary>
        public string PluginData {
            get {
                return _PluginData;
            }
            set {
                _PluginData = value;
            }
        }

        #endregion
    }
}
