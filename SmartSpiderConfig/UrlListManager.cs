namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// 起始地址配置类
    /// 版  本:V1.0
    /// 标  志:20120228
    /// </summary>
    [Serializable]
    [XmlRoot("UrlListManager")]
    public class UrlListManager {
        public UrlListManager() {
            this._StartingUrlListPosition = 0;
            this._PickedUrlsCount = 0;
            this._PickedUrlsPosition = 0;
            this._HistoryUrlsCount = 0;
            this._HistoryUrlCapacity = 1000;
            this._UrlEncoding = "gb2312";
            this._StartingUrlEncoded = false;
            this._UsePluginOfLoadStartingUrl = false;
            this._StartingUrlTemplate = "";
        }

        #region 私有变量定义
        private int _HistoryUrlCapacity = 0;
        private int _HistoryUrlsCount = 0;
        private List<NavigationRule> _NavigationRules = new List<NavigationRule>();
        private List<PagedUrlPatterns> _PagedUrlPattern = new List<PagedUrlPatterns>();
        private int _PickedUrlsCount = 0;
        private int _PickedUrlsPosition = 0;
        private bool _StartingUrlEncoded = false;
        private List<string> _StartingUrlList = new List<string>();
        private int _StartingUrlListPosition = 0;
        private string _StartingUrlTemplate = "";
        private string _UrlEncoding = "gb2312";
        private bool _UsePluginOfLoadStartingUrl = false;
        #endregion

        #region 公共属性定义
        /// <summary>
        /// 起始URL列表中的位置(从第几条网址开始)
        /// </summary>
        public int StartingUrlListPosition {
            get {
                return _StartingUrlListPosition;
            }
            set {
                _StartingUrlListPosition = value;
            }
        }

        /// <summary>
        /// 导航Url总数
        /// </summary>
        public int PickedUrlsCount {
            get {
                return _PickedUrlsCount;
            }
            set {
                _PickedUrlsCount = value;
            }
        }

        /// <summary>
        /// 导航URL地址
        /// </summary>
        public int PickedUrlsPosition {
            get {
                return _PickedUrlsPosition;
            }
            set {
                _PickedUrlsPosition = value;
            }
        }

        /// <summary>
        /// 历史网址总数
        /// </summary>
        public int HistoryUrlsCount {
            get {
                return _HistoryUrlsCount;
            }
            set {
                _HistoryUrlsCount = value;
            }
        }

        /// <summary>
        /// 允许最大的历史记录网址数
        /// </summary>
        public int HistoryUrlCapacity {
            get {
                return _HistoryUrlCapacity;
            }
            set {
                _HistoryUrlCapacity = value;
            }
        }

        /// <summary>
        /// 网址编码(默认gb2312)
        /// </summary>
        public string UrlEncoding {
            get {
                return _UrlEncoding;
            }
            set {
                _UrlEncoding = value;
            }
        }

        /// <summary>
        /// 起始地址已编码
        /// </summary>
        public bool StartingUrlEncoded {
            get {
                return _StartingUrlEncoded;
            }
            set {
                _StartingUrlEncoded = value;
            }
        }

        /// <summary>
        /// 使用插件加载起始网址
        /// </summary>
        public bool UsePluginOfLoadStartingUrl {
            get {
                return _UsePluginOfLoadStartingUrl;
            }
            set {
                _UsePluginOfLoadStartingUrl = value;
            }
        }

        /// <summary>
        /// 起始Url地址模板
        /// </summary>
        public string StartingUrlTemplate {
            get {
                return _StartingUrlTemplate;
            }
            set {
                _StartingUrlTemplate = value;
            }
        }

        /// <summary>
        /// 分页模式URL模板
        /// </summary>
        /// <remarks>针对起始地址中的“{0,200,50}”参数。</remarks>
        public List<PagedUrlPatterns> PagedUrlPattern {
            get {
                return _PagedUrlPattern;
            }
            set {
                _PagedUrlPattern = value;
            }
        }

        /// <summary>
        /// 导航规则
        /// </summary>
        public List<NavigationRule> NavigationRules {
            get {
                return _NavigationRules;
            }
            set {
                _NavigationRules = value;
            }
        }

        /// <summary>
        /// 起始URL地址集合
        /// </summary>
        /// <remarks>保存起Url地址集合，包括模板网址。</remarks>
        public List<string> StartingUrlList {
            get {
                return _StartingUrlList;
            }
            set {
                _StartingUrlList = value;
            }
        }
        #endregion
    }
}
