namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 采集规则配置类
    /// 版本:V1.0
    /// 标志:20120228
    /// </summary>
    [Serializable]
    [XmlRoot("ExtractionRule")]
    public class ExtractionRule {

        public ExtractionRule() {
            _ReservedHtmlMarks = new List<HtmlMark>();
        }

        #region 私有变量定义
        private string _AttachmentUrlIdentifier = "";
        private string _ClassDirectoryField = "";
        private bool _ConstantAsResult;
        private string _ConstantValue;
        private bool _CreateSubDirectories;
        private string _CurrentSubDirectory;
        private string _DataColumn;
        private bool _DataUnique;
        private bool _DetectRealUrl;
        private bool _DownloadAttachments;
        private string _DownloadDirectory;
        private bool _DownloadFlashes;
        private bool _DownloadImages;
        private bool _Essential;
        private string _FileNameExtension;
        private int _FilesPerSubDirectory;
        private List<Filter> _Filters;
        private string _FollowingFlag;
        private bool _Global;
        private bool _IsDownloadUrl;
        private Layer _Layer;
        private bool _LinkTextAsResult;
        private string _MergenceSeparator;
        private bool _MergePages;
        private string _Name;
        private bool _PostParametersAsResult;
        private string _PreviousFlag;
        private List<Replacement> _Replacements;
        private bool _ReserveAllHtmlMarks;
        private List<HtmlMark> _ReservedHtmlMarks;
        private bool _ResponseHeaderAsResult;
        private string _ResponseHeaderName;
        private bool _SkipIfFileExisted;
        private bool _Static;
        private bool _TimeAsResult;
        private bool _UrlAsResult;
        private bool _UseClassDirectory;
        private bool _UsePlugin;
        private bool _UseRandomFileName;
        private string _VirtualPath;
        #endregion

        #region 公共属性定义

        /// <summary>
        /// 规则名称
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
        /// 数据库字段
        /// </summary>
        public string DataColumn {
            get {
                return _DataColumn;
            }
            set {
                _DataColumn = value;
            }
        }

        /// <summary>
        /// 唯一数据
        /// </summary>
        public bool DataUnique {
            get {
                return _DataUnique;
            }
            set {
                _DataUnique = value;
            }
        }

        /// <summary>
        /// 页面层次
        /// </summary>
        public Layer Layer {
            get {
                return _Layer;
            }
            set {
                _Layer = value;
            }
        }

        /// <summary>
        /// 信息前标志
        /// </summary>
        public string PreviousFlag {
            get {
                return _PreviousFlag;
            }
            set {
                _PreviousFlag = value;
            }
        }

        /// <summary>
        /// 信息后标志
        /// </summary>
        public string FollowingFlag {
            get {
                return _FollowingFlag;
            }
            set {
                _FollowingFlag = value;
            }
        }

        /// <summary>
        /// 全局规则
        /// </summary>
        public bool Global {
            get {
                return _Global;
            }
            set {
                _Global = value;
            }
        }

        /// <summary>
        /// 静态规则
        /// </summary>
        public bool Static {
            get {
                return _Static;
            }
            set {
                _Static = value;
            }
        }

        /// <summary>
        /// 必要规则
        /// </summary>
        public bool Essential {
            get {
                return _Essential;
            }
            set {
                _Essential = value;
            }
        }

        /// <summary>
        /// 使用插件采集数据
        /// </summary>
        public bool UsePlugin {
            get {
                return _UsePlugin;
            }
            set {
                _UsePlugin = value;
            }
        }

        /// <summary>
        /// 下载网址
        /// </summary>
        public bool IsDownloadUrl {
            get {
                return _IsDownloadUrl;
            }
            set {
                _IsDownloadUrl = value;
            }
        }

        /// <summary>
        /// 探测真实的URL
        /// </summary>
        public bool DetectRealUrl {
            get {
                return _DetectRealUrl;
            }
            set {
                _DetectRealUrl = value;
            }
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        public bool DownloadImages {
            get {
                return _DownloadImages;
            }
            set {
                _DownloadImages = value;
            }
        }

        /// <summary>
        /// 下载Flash
        /// </summary>
        public bool DownloadFlashes {
            get {
                return _DownloadFlashes;
            }
            set {
                _DownloadFlashes = value;
            }
        }

        /// <summary>
        /// 下载附件
        /// </summary>
        public bool DownloadAttachments {
            get {
                return _DownloadAttachments;
            }
            set {
                _DownloadAttachments = value;
            }
        }

        /// <summary>
        /// 附件网址标志
        /// </summary>
        public string AttachmentUrlIdentifier {
            get {
                return _AttachmentUrlIdentifier;
            }
            set {
                _AttachmentUrlIdentifier = value;
            }
        }

        /// <summary>
        /// 下载目录
        /// </summary>
        public string DownloadDirectory {
            get {
                return _DownloadDirectory;
            }
            set {
                _DownloadDirectory = value;
            }
        }

        /// <summary>
        /// 虚拟路径
        /// </summary>
        public string VirtualPath {
            get {
                return _VirtualPath;
            }
            set {
                _VirtualPath = value;
            }
        }

        /// <summary>
        /// 使用随机文件名
        /// </summary>
        public bool UseRandomFileName {
            get {
                return _UseRandomFileName;
            }
            set {
                _UseRandomFileName = value;
            }
        }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileNameExtension {
            get {
                return _FileNameExtension;
            }
            set {
                _FileNameExtension = value;
            }
        }

        /// <summary>
        /// 创建子目录
        /// </summary>
        public bool CreateSubDirectories {
            get {
                return _CreateSubDirectories;
            }
            set {
                _CreateSubDirectories = value;
            }
        }

        /// <summary>
        /// 当前子目录
        /// </summary>
        public string CurrentSubDirectory {
            get {
                return _CurrentSubDirectory;
            }
            set {
                _CurrentSubDirectory = value;
            }
        }

        /// <summary>
        /// 每个子目录文件数量
        /// </summary>
        public int FilesPerSubDirectory {
            get {
                return _FilesPerSubDirectory;
            }
            set {
                _FilesPerSubDirectory = value;
            }
        }

        /// <summary>
        /// 使用分类目录
        /// </summary>
        public bool UseClassDirectory {
            get {
                return _UseClassDirectory;
            }
            set {
                _UseClassDirectory = value;
            }
        }

        public string ClassDirectoryField {
            get {
                return _ClassDirectoryField;
            }
            set {
                _ClassDirectoryField = value;
            }
        }

        /// <summary>
        /// 文件存在则跳过
        /// </summary>
        public bool SkipIfFileExisted {
            get {
                return _SkipIfFileExisted;
            }
            set {
                _SkipIfFileExisted = value;
            }
        }

        /// <summary>
        /// 记录当前网址
        /// </summary>
        public bool UrlAsResult {
            get {
                return _UrlAsResult;
            }
            set {
                _UrlAsResult = value;
            }
        }

        /// <summary>
        /// POST参数作为结果
        /// </summary>
        public bool PostParametersAsResult {
            get {
                return _PostParametersAsResult;
            }
            set {
                _PostParametersAsResult = value;
            }
        }

        /// <summary>
        /// 记录采集时间
        /// </summary>
        public bool TimeAsResult {
            get {
                return _TimeAsResult;
            }
            set {
                _TimeAsResult = value;
            }
        }

        /// <summary>
        /// 链接文本作为结果
        /// </summary>
        public bool LinkTextAsResult {
            get {
                return _LinkTextAsResult;
            }
            set {
                _LinkTextAsResult = value;
            }
        }

        /// <summary>
        /// Http响应头作为结果
        /// </summary>
        public bool ResponseHeaderAsResult {
            get {
                return _ResponseHeaderAsResult;
            }
            set {
                _ResponseHeaderAsResult = value;
            }
        }

        /// <summary>
        /// 响应头名
        /// </summary>
        public string ResponseHeaderName {
            get {
                return _ResponseHeaderName;
            }
            set {
                _ResponseHeaderName = value;
            }
        }

        /// <summary>
        /// 将固定值作为结果
        /// </summary>
        public bool ConstantAsResult {
            get {
                return _ConstantAsResult;
            }
            set {
                _ConstantAsResult = value;
            }
        }

        /// <summary>
        /// 将固定值作为结果[值]
        /// </summary>
        public string ConstantValue {
            get {
                return _ConstantValue;
            }
            set {
                _ConstantValue = value;
            }
        }

        /// <summary>
        /// 合并页
        /// </summary>
        public bool MergePages {
            get {
                return _MergePages;
            }
            set {
                _MergePages = value;
            }
        }

        /// <summary>
        /// 合并后的页面分隔符
        /// </summary>
        public string MergenceSeparator {
            get {
                return _MergenceSeparator;
            }
            set {
                _MergenceSeparator = value;
            }
        }

        /// <summary>
        /// 保留所有HTML标记
        /// </summary>
        public bool ReserveAllHtmlMarks {
            get {
                return _ReserveAllHtmlMarks;
            }
            set {
                _ReserveAllHtmlMarks = value;
            }
        }

        /// <summary>
        /// 保留的HTML标记
        /// </summary>
        public List<HtmlMark> ReservedHtmlMarks {
            get {
                return _ReservedHtmlMarks;
            }
            set {
                _ReservedHtmlMarks = value;
            }
        }

        /// <summary>
        /// 采集结果替换
        /// </summary>
        public List<Replacement> Replacements {
            get {
                return _Replacements;
            }
            set {
                _Replacements = value;
            }
        }

        public List<Filter> Filters {
            get {
                return _Filters;
            }
            set {
                _Filters = value;
            }
        }

        #endregion
    }
}
