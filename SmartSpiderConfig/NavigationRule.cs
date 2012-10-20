namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 导航规则配置类
    /// 版  本:V1.0
    /// 标  志:20120228
    /// </summary>
    [Serializable]
    [XmlRoot("NavigationRule")]
    public class  NavigationRule {

        #region 私有变量定义
        private string _ContentEncoding;
        private string _ExtractionEndFlag;
        private string _ExtractionStartFlag;
        private bool _HistoryUrlEnabled;
        private bool _HistoryUrlOptimization;
        private string _IterationFlag;
        private bool _JsDecoding;
        private string _Name;
        private bool _NextLayerUrlEncoded;
        private string _NextLayerUrlPattern;
        private string _NextLayerUrlReferer;
        private int _NextPageLargest;
        private string _NextPageUrlPattern;
        private string _PickingEndFlag;
        private string _PickingStartFlag;
        private bool _PickNextLayerUrls;
        private bool _PickNextPageUrl;
        private bool _ProccessScripts;
        private int _RestInterval;
        private string _SkipToIfPickingFailed;
        private bool _Terminal;
        private bool _UsePluginOfPickNextLayerUrls;
        private bool _UsePluginOfPickNextPageUrl;
        private bool _UsePluginOfVisit;
        private bool _UseRegularExpression;
        private List<Replacement> _Replacements;
        #endregion

        #region 公共属性定义
        /// <summary>
        /// 层次名称
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
        /// 最终页面
        /// </summary>
        public bool Terminal {
            get {
                return _Terminal;
            }
            set {
                _Terminal = value;
            }
        }

        /// <summary>
        /// 是否提取下一层的网址
        /// </summary>
        public bool PickNextLayerUrls {
            get {
                return _PickNextLayerUrls;
            }
            set {
                _PickNextLayerUrls = value;
            }
        }

        /// <summary>
        /// 下一层网址模板
        /// </summary>
        public string NextLayerUrlPattern {
            get {
                return _NextLayerUrlPattern;
            }
            set {
                _NextLayerUrlPattern = value;
            }
        }

        /// <summary>
        /// 使用正则表达式
        /// </summary>
        public bool UseRegularExpression {
            get {
                return _UseRegularExpression;
            }
            set {
                _UseRegularExpression = value;
            }
        }

        /// <summary>
        /// 是否允许历史网址重复
        /// </summary>
        public bool HistoryUrlEnabled {
            get {
                return _HistoryUrlEnabled;
            }
            set {
                _HistoryUrlEnabled = value;
            }
        }

        /// <summary>
        /// 是否优化历史网址记录
        /// </summary>
        public bool HistoryUrlOptimization {
            get {
                return _HistoryUrlOptimization;
            }
            set {
                _HistoryUrlOptimization = value;
            }
        }

        /// <summary>
        /// 是否提取下一页的网址
        /// </summary>
        public bool PickNextPageUrl {
            get {
                return _PickNextPageUrl;
            }
            set {
                _PickNextPageUrl = value;
            }
        }

        /// <summary>
        /// 下一页网址模板
        /// </summary>
        public string NextPageUrlPattern {
            get {
                return _NextPageUrlPattern;
            }
            set {
                _NextPageUrlPattern = value;
            }
        }

        /// <summary>
        /// 最大页数
        /// </summary>
        public int NextPageLargest {
            get {
                return _NextPageLargest;
            }
            set {
                _NextPageLargest = value;
            }
        }

        /// <summary>
        /// 网址提取范围-开始标志
        /// </summary>
        public string PickingStartFlag {
            get {
                return _PickingStartFlag;
            }
            set {
                _PickingStartFlag = value;
            }
        }

        /// <summary>
        /// 网址提取范围-结束标志
        /// </summary>
        public string PickingEndFlag {
            get {
                return _PickingEndFlag;
            }
            set {
                _PickingEndFlag = value;
            }
        }

        /// <summary>
        /// 内容采集范围-开始标志
        /// </summary>
        public string ExtractionStartFlag {
            get {
                return _ExtractionStartFlag;
            }
            set {
                _ExtractionStartFlag = value;
            }
        }

        /// <summary>
        /// 内容采集范围-结束标志
        /// </summary>
        public string ExtractionEndFlag {
            get {
                return _ExtractionEndFlag;
            }
            set {
                _ExtractionEndFlag = value;
            }
        }

        /// <summary>
        /// 循环标志
        /// </summary>
        public string IterationFlag {
            get {
                return _IterationFlag;
            }
            set {
                _IterationFlag = value;
            }
        }

        /// <summary>
        /// 访问休息间隔（秒）
        /// </summary>
        public int RestInterval {
            get {
                return _RestInterval;
            }
            set {
                _RestInterval = value;
            }
        }

        /// <summary>
        /// 内容编码
        /// </summary>
        public string ContentEncoding {
            get {
                return _ContentEncoding;
            }
            set {
                _ContentEncoding = value;
            }
        }

        /// <summary>
        /// 提取下一页网址-是否提取（下一页标志）
        /// </summary>
        public bool ProccessScripts {
            get {
                return _ProccessScripts;
            }
            set {
                _ProccessScripts = value;
            }
        }

        /// <summary>
        /// 下一层网址的Referer
        /// </summary>
        public string NextLayerUrlReferer {
            get {
                return _NextLayerUrlReferer;
            }
            set {
                _NextLayerUrlReferer = value;
            }
        }

        /// <summary>
        /// 下一层网址已编码
        /// </summary>
        public bool NextLayerUrlEncoded {
            get {
                return _NextLayerUrlEncoded;
            }
            set {
                _NextLayerUrlEncoded = value;
            }
        }

        /// <summary>
        /// 对源文件进行JS解密
        /// </summary>
        public bool JsDecoding {
            get {
                return _JsDecoding;
            }
            set {
                _JsDecoding = value;
            }
        }

        /// <summary>
        /// 如果下一层网址提取失败则跳转到层？
        /// </summary>
        public string SkipToIfPickingFailed {
            get {
                return _SkipToIfPickingFailed;
            }
            set {
                _SkipToIfPickingFailed = value;
            }
        }

        /// <summary>
        /// 使用插件请求本层URL
        /// </summary>
        public bool UsePluginOfVisit {
            get {
                return _UsePluginOfVisit;
            }
            set {
                _UsePluginOfVisit = value;
            }
        }

        /// <summary>
        /// 使用插件提取下一层网址
        /// </summary>
        public bool UsePluginOfPickNextLayerUrls {
            get {
                return _UsePluginOfPickNextLayerUrls;
            }
            set {
                _UsePluginOfPickNextLayerUrls = value;
            }
        }

        /// <summary>
        /// 使用插件提取下一页网址
        /// </summary>
        public bool UsePluginOfPickNextPageUrl {
            get {
                return _UsePluginOfPickNextPageUrl;
            }
            set {
                _UsePluginOfPickNextPageUrl = value;
            }
        }

        /// <summary>
        /// 源文件替换
        /// </summary>
        public List<Replacement> Replacements {
            get {
                return _Replacements;
            }
            set {
                _Replacements = value;
            }
        }
        #endregion
    }
}
