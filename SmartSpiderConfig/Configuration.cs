namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// SmartSpider系统配置
    /// </summary>
    [Serializable]
    [XmlRoot("Configuration")]
    public class Configuration {

        public Configuration() {
            this._LogFileStyle = Config.LogFileStyle.Single;
            this._NotifyWindowMode = Config.NotifyWindowMode.AllTasks;
            this._ShutDownMode = Config.ShutDownMode.PowerOff;
            this._RequestHeaders = new RequestHeader[0];
            this._TaskNameColumnWidth = 180;
            this._LogPanelHeight = 100;
            this._RunningInformationPageHeight = 404;
            this._FolderTreeWidth = 176;
            this._TaskEditorHeight = 550;
            this._TaskEditorWidth = 780;
            this._NotifyWindowHeight = 71;
            this._NotifyWindowWidth = 393;
            this._NotifyWindowTop = 0;
            this._NotifyWindowLeft = 0;
            this._NotifyWindowVisible = false;
            this._ResultViewPageSize = 50;
            this._LanguagePackagePath = "";
            this._DeleteConnectionStringWhenExport = false;
            this._TaskTemplate = "";
            this._DefaultResultReplacements = "";
            this._SqlTimeout = 30;
            this._RestartIfOutOfMemory = false;
            this._MemoryTrimmingInterval = 0;
            this._StateSavingInterval = 10;
            this._LogCacheSize = 1000;
            this._LogFileSize = 1024;
            this._WebProxyTestUrl = "http://www.google.com.hk";
            this._WebProxyPassword = "";
            this._WebProxyUsername = "";
            this._WebProxyPort = 8080;
            this._WebProxyHost = "127.0.0.1";
            this._WebProxyEnabled = false;
            this._StopRunningTasksWhenExit = false;
            this._RunningAreaCapacity = 50;
            this._ResultCacheSize = 1000;
            this._RunningInformationResultCapacity = 1000;
            this._RunningInformationLogCapacity = 1000;
            this._ShiftRunningInformationPageAutomatically = true;
            this._PosterNotifyIconEnabled = false;
            this._NotifyIconEnabled = false;
            this._ScheduleOffset = 5;
            this._DisableSchedule = false;
            this._TaskQueueEnabled = false;
            this._MaximumRunningTasks = 0;
            this._FailuresNotifyReceivers = "";
            this._FailuresNotifySmtpPassword = "";
            this._FailuresNotifySmtpUsername = "";
            this._FailuresNotifySmtpPort = 25;
            this._FailuresNotifySmtpServer = "";
            this._FailuresNotifyEnabled = false;
            this._AllowedExtractionFailures = 0;
            this._AllowedPickingFailures = 0;
            this._AllowedRequestFailures = 0;
            this._AllowedRetryTimes = 3;
            this._RequestTimeout = 10;
        }

        #region 私有变量定义
        private int _AllowedExtractionFailures;
        private int _AllowedPickingFailures;
        private int _AllowedRequestFailures;
        private int _AllowedRetryTimes;
        private string _DefaultResultReplacements;
        private bool _DeleteConnectionStringWhenExport;
        private bool _DisableSchedule;
        private bool _FailuresNotifyEnabled;
        private string _FailuresNotifyReceivers;
        private string _FailuresNotifySmtpPassword;
        private int _FailuresNotifySmtpPort;
        private string _FailuresNotifySmtpServer;
        private string _FailuresNotifySmtpUsername;
        private int _FolderTreeWidth;
        private string _LanguagePackagePath;
        private int _LogCacheSize;
        private int _LogFileSize;
        private LogFileStyle _LogFileStyle;
        private int _LogPanelHeight;
        private int _MaximumRunningTasks;
        private int _MemoryTrimmingInterval;
        private bool _NotifyIconEnabled;
        private int _NotifyWindowHeight;
        private int _NotifyWindowLeft;
        private NotifyWindowMode _NotifyWindowMode;
        private int _NotifyWindowTop;
        private bool _NotifyWindowVisible;
        private int _NotifyWindowWidth;
        private bool _PosterNotifyIconEnabled;
        private string _RegistrationCode;
        private RequestHeader[] _RequestHeaders;
        private int _RequestTimeout;
        private bool _RestartIfOutOfMemory;
        private int _ResultCacheSize;
        private int _ResultViewPageSize;
        private int _RunningAreaCapacity;
        private int _RunningInformationLogCapacity;
        private int _RunningInformationPageHeight;
        private int _RunningInformationResultCapacity;
        private int _ScheduleOffset;
        private bool _ShiftRunningInformationPageAutomatically;
        private ShutDownMode _ShutDownMode;
        private int _SqlTimeout;
        private int _StateSavingInterval;
        private bool _StopRunningTasksWhenExit;
        private int _TaskEditorHeight;
        private int _TaskEditorWidth;
        private int _TaskNameColumnWidth;
        private bool _TaskQueueEnabled;
        private string _TaskTemplate;
        private bool _WebProxyEnabled;
        private string _WebProxyHost;
        private string _WebProxyPassword;
        private int _WebProxyPort;
        private string _WebProxyTestUrl;
        private string _WebProxyUsername;
        #endregion

        #region 公共属性定义
        /// <summary>
        /// 请求超时
        /// </summary>
        public int RequestTimeout {
            get {
                return this._RequestTimeout;
            }
            set {
                this._RequestTimeout = value;
            }
        }

        /// <summary>
        /// 允许重试的次数（"0"表示不允许）
        /// </summary>
        public int AllowedRetryTimes {
            get {
                return _AllowedRetryTimes;
            }
            set {
                _AllowedRetryTimes = value;
            }
        }

        /// <summary>
        /// 连续请求失败多少个URL后暂停任务（"0"表示不允许）
        /// </summary>
        public int AllowedRequestFailures {
            get {
                return _AllowedRequestFailures;
            }
            set {
                _AllowedRequestFailures = value;
            }
        }

        /// <summary>
        /// 连续提取下一层网址失败达到多少次后暂停任务（"0"表示不允许）
        /// </summary>
        public int AllowedPickingFailures {
            get {
                return _AllowedPickingFailures;
            }
            set {
                _AllowedPickingFailures = value;
            }
        }

        /// <summary>
        /// 连续采集内容失败达到多少次后暂停任务（"0"表示不允许）
        /// </summary>
        public int AllowedExtractionFailures {
            get {
                return _AllowedExtractionFailures;
            }
            set {
                _AllowedExtractionFailures = value;
            }
        }

        /// <summary>
        /// 启用邮件故障通知
        /// </summary>
        public bool FailuresNotifyEnabled {
            get {
                return _FailuresNotifyEnabled;
            }
            set {
                _FailuresNotifyEnabled = value;
            }
        }

        /// <summary>
        /// 故障通知SMTP主机
        /// </summary>
        public string FailuresNotifySmtpServer {
            get {
                return _FailuresNotifySmtpServer;
            }
            set {
                _FailuresNotifySmtpServer = value;
            }
        }

        /// <summary>
        /// 故障通知SMTP端口
        /// </summary>
        public int FailuresNotifySmtpPort {
            get {
                return _FailuresNotifySmtpPort;
            }
            set {
                _FailuresNotifySmtpPort = value;
            }
        }

        /// <summary>
        /// 故障通知SMTP用户名
        /// </summary>
        public string FailuresNotifySmtpUsername {
            get {
                return _FailuresNotifySmtpUsername;
            }
            set {
                _FailuresNotifySmtpUsername = value;
            }
        }

        /// <summary>
        /// 故障通知SMTP密码
        /// </summary>
        public string FailuresNotifySmtpPassword {
            get {
                return _FailuresNotifySmtpPassword;
            }
            set {
                _FailuresNotifySmtpPassword = value;
            }
        }

        /// <summary>
        /// 故障通知邮件接收者
        /// </summary>
        public string FailuresNotifyReceivers {
            get {
                return _FailuresNotifyReceivers;
            }
            set {
                _FailuresNotifyReceivers = value;
            }
        }

        /// <summary>
        /// 最多可以同时运行多少个任务（"0"表示不允许）
        /// </summary>
        public int MaximumRunningTasks {
            get {
                return _MaximumRunningTasks;
            }
            set {
                _MaximumRunningTasks = value;
            }
        }

        /// <summary>
        /// 启用任务队列
        /// </summary>
        public bool TaskQueueEnabled {
            get {
                return _TaskQueueEnabled;
            }
            set {
                _TaskQueueEnabled = value;
            }
        }

        /// <summary>
        /// 禁用定时采集
        /// </summary>
        public bool DisableSchedule {
            get {
                return _DisableSchedule;
            }
            set {
                _DisableSchedule = value;
            }
        }

        /// <summary>
        /// 定时采集偏差（分钟）
        /// </summary>
        public int ScheduleOffset {
            get {
                return _ScheduleOffset;
            }
            set {
                _ScheduleOffset = value;
            }
        }

        /// <summary>
        /// 通知图标启用
        /// </summary>
        public bool NotifyIconEnabled {
            get {
                return _NotifyIconEnabled;
            }
            set {
                _NotifyIconEnabled = value;
            }
        }

        /// <summary>
        /// 海报通知图标启用
        /// </summary>
        public bool PosterNotifyIconEnabled {
            get {
                return _PosterNotifyIconEnabled;
            }
            set {
                _PosterNotifyIconEnabled = value;
            }
        }

        /// <summary>
        /// 转移自动运行信息页
        /// </summary>
        public bool ShiftRunningInformationPageAutomatically {
            get {
                return _ShiftRunningInformationPageAutomatically;
            }
            set {
                _ShiftRunningInformationPageAutomatically = value;
            }
        }

        /// <summary>
        /// 任务运行时下方最多显示多少行日志
        /// </summary>
        public int RunningInformationLogCapacity {
            get {
                return _RunningInformationLogCapacity;
            }
            set {
                _RunningInformationLogCapacity = value;
            }
        }

        /// <summary>
        /// 任务运行时下方最多显示多少条采集结果
        /// </summary>
        public int RunningInformationResultCapacity {
            get {
                return _RunningInformationResultCapacity;
            }
            set {
                _RunningInformationResultCapacity = value;
            }
        }

        /// <summary>
        /// 结果文件缓存大小（行）（用于发布采集结果，保存结果、重复行和出错行时）
        /// </summary>
        public int ResultCacheSize {
            get {
                return _ResultCacheSize;
            }
            set {
                _ResultCacheSize = value;
            }
        }

        /// <summary>
        /// 允许在运行区中显示多少个任务（包括最近运行过的任务）
        /// </summary>
        public int RunningAreaCapacity {
            get {
                return _RunningAreaCapacity;
            }
            set {
                _RunningAreaCapacity = value;
            }
        }

        /// <summary>
        /// 停止运行任务时退出
        /// </summary>
        public bool StopRunningTasksWhenExit {
            get {
                return _StopRunningTasksWhenExit;
            }
            set {
                _StopRunningTasksWhenExit = value;
            }
        }

        /// <summary>
        /// 使用代理服务器
        /// </summary>
        public bool WebProxyEnabled {
            get {
                return _WebProxyEnabled;
            }
            set {
                _WebProxyEnabled = value;
            }
        }

        /// <summary>
        /// Web代理主机
        /// </summary>
        public string WebProxyHost {
            get {
                return _WebProxyHost;
            }
            set {
                _WebProxyHost = value;
            }
        }

        /// <summary>
        /// Web代理端口
        /// </summary>
        public int WebProxyPort {
            get {
                return _WebProxyPort;
            }
            set {
                _WebProxyPort = value;
            }
        }

        /// <summary>
        /// Web代理用户名
        /// </summary>
        public string WebProxyUsername {
            get {
                return _WebProxyUsername;
            }
            set {
                _WebProxyUsername = value;
            }
        }

        /// <summary>
        /// Web代理密码
        /// </summary>
        public string WebProxyPassword {
            get {
                return _WebProxyPassword;
            }
            set {
                _WebProxyPassword = value;
            }
        }

        /// <summary>
        /// Web代理测试URL地址
        /// </summary>
        /// <remarks>测试代理服务器Url地址</remarks>
        public string WebProxyTestUrl {
            get {
                return _WebProxyTestUrl;
            }
            set {
                _WebProxyTestUrl = value;
            }
        }

        /// <summary>
        /// Http请求头
        /// </summary>
        public RequestHeader[] RequestHeaders {
            get {
                return _RequestHeaders;
            }
            set {
                _RequestHeaders = value;
            }
        }

        /// <summary>
        /// 关机模式
        /// </summary>
        public ShutDownMode ShutDownMode {
            get {
                return _ShutDownMode;
            }
            set {
                _ShutDownMode = value;
            }
        }

        /// <summary>
        /// 悬浮窗口应用模式
        /// </summary>
        /// <remarks>当使用“悬浮窗”时，将“悬浮窗”应用于</remarks>
        public NotifyWindowMode NotifyWindowMode {
            get {
                return _NotifyWindowMode;
            }
            set {
                _NotifyWindowMode = value;
            }
        }

        /// <summary>
        /// 日志文件样式
        /// </summary>
        public LogFileStyle LogFileStyle {
            get {
                return _LogFileStyle;
            }
            set {
                _LogFileStyle = value;
            }
        }

        /// <summary>
        /// 日志文件大小
        /// </summary>
        public int LogFileSize {
            get {
                return _LogFileSize;
            }
            set {
                _LogFileSize = value;
            }
        }

        /// <summary>
        /// 日志高速缓存大小
        /// </summary>
        public int LogCacheSize {
            get {
                return _LogCacheSize;
            }
            set {
                _LogCacheSize = value;
            }
        }

        /// <summary>
        /// 运行时每隔多少分钟保存一次任务状态(以防断电等突发情况)
        /// </summary>
        public int StateSavingInterval {
            get {
                return _StateSavingInterval;
            }
            set {
                _StateSavingInterval = value;
            }
        }

        /// <summary>
        /// 每隔多少分钟整理一次内存（“0”表示不整理，频繁整理内存将损失性能）
        /// </summary>
        public int MemoryTrimmingInterval {
            get {
                return _MemoryTrimmingInterval;
            }
            set {
                _MemoryTrimmingInterval = value;
            }
        }

        /// <summary>
        /// 重新启动如果内存不足
        /// </summary>
        public bool RestartIfOutOfMemory {
            get {
                return _RestartIfOutOfMemory;
            }
            set {
                _RestartIfOutOfMemory = value;
            }
        }

        /// <summary>
        /// SQL超时设置（秒），仅用于插入数据、检索重复行时（“0”表示不限制）
        /// </summary>
        public int SqlTimeout {
            get {
                return _SqlTimeout;
            }
            set {
                _SqlTimeout = value;
            }
        }

        /// <summary>
        /// 默认结果替换XML文件
        /// </summary>
        public string DefaultResultReplacements {
            get {
                return _DefaultResultReplacements;
            }
            set {
                _DefaultResultReplacements = value;
            }
        }

        /// <summary>
        /// 新建任务时的默认模板
        /// </summary>
        public string TaskTemplate {
            get {
                return _TaskTemplate;
            }
            set {
                _TaskTemplate = value;
            }
        }

        /// <summary>
        /// 导出任务时删除数据库连接串
        /// </summary>
        public bool DeleteConnectionStringWhenExport {
            get {
                return _DeleteConnectionStringWhenExport;
            }
            set {
                _DeleteConnectionStringWhenExport = value;
            }
        }

        /// <summary>
        /// 语言包路径
        /// </summary>
        public string LanguagePackagePath {
            get {
                return _LanguagePackagePath;
            }
            set {
                _LanguagePackagePath = value;
            }
        }

        /// <summary>
        /// 结果视图页面大小
        /// </summary>
        public int ResultViewPageSize {
            get {
                return _ResultViewPageSize;
            }
            set {
                _ResultViewPageSize = value;
            }
        }

        /// <summary>
        /// 通知窗口可见
        /// </summary>
        public bool NotifyWindowVisible {
            get {
                return _NotifyWindowVisible;
            }
            set {
                _NotifyWindowVisible = value;
            }
        }

        /// <summary>
        /// 通知窗口的左侧
        /// </summary>
        public int NotifyWindowLeft {
            get {
                return _NotifyWindowLeft;
            }
            set {
                _NotifyWindowLeft = value;
            }
        }

        /// <summary>
        /// 通知窗口的顶部
        /// </summary>
        public int NotifyWindowTop {
            get {
                return _NotifyWindowTop;
            }
            set {
                _NotifyWindowTop = value;
            }
        }

        /// <summary>
        /// 通知窗口宽度
        /// </summary>
        public int NotifyWindowWidth {
            get {
                return _NotifyWindowWidth;
            }
            set {
                _NotifyWindowWidth = value;
            }
        }

        /// <summary>
        /// 通知窗口高度
        /// </summary>
        public int NotifyWindowHeight {
            get {
                return _NotifyWindowHeight;
            }
            set {
                _NotifyWindowHeight = value;
            }
        }

        /// <summary>
        /// 任务编辑器宽度
        /// </summary>
        public int TaskEditorWidth {
            get {
                return _TaskEditorWidth;
            }
            set {
                _TaskEditorWidth = value;
            }
        }

        /// <summary>
        /// 任务编辑器高度
        /// </summary>
        public int TaskEditorHeight {
            get {
                return _TaskEditorHeight;
            }
            set {
                _TaskEditorHeight = value;
            }
        }

        /// <summary>
        /// 文件夹树宽度
        /// </summary>
        public int FolderTreeWidth {
            get {
                return _FolderTreeWidth;
            }
            set {
                _FolderTreeWidth = value;
            }
        }

        /// <summary>
        /// 运行信息页面高度
        /// </summary>
        public int RunningInformationPageHeight {
            get {
                return _RunningInformationPageHeight;
            }
            set {
                _RunningInformationPageHeight = value;
            }
        }

        /// <summary>
        /// 日志面板高度
        /// </summary>
        public int LogPanelHeight {
            get {
                return _LogPanelHeight;
            }
            set {
                _LogPanelHeight = value;
            }
        }

        /// <summary>
        /// 任务名称列宽
        /// </summary>
        public int TaskNameColumnWidth {
            get {
                return _TaskNameColumnWidth;
            }
            set {
                _TaskNameColumnWidth = value;
            }
        }

        /// <summary>
        /// 注册代码
        /// </summary>
        public string RegistrationCode {
            get {
                return _RegistrationCode;
            }
            set {
                _RegistrationCode = value;
            }
        }

        #endregion
    }
}
