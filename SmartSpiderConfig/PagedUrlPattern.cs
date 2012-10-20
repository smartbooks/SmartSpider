namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("PagedUrlPattern")]
    public class PagedUrlPatterns {
        public PagedUrlPatterns() {
            this._StartPage = 0;
            this._EndPage = 0;
            this._Step = 1;
            this._Format = PagedUrlPatternsMode.Increment;
            this._PagedUrlPattern = "";
        }

        #region 私有变量定义
        private double _EndPage;
        private SmartSpider.Config.PagedUrlPatternsMode _Format;
        private string _PagedUrlPattern;
        private double _StartPage;
        private double _Step;
        #endregion

        #region 公共属性定义

        /// <summary>
        /// 开始页码
        /// </summary>
        public double StartPage {
            get {
                return _StartPage;
            }
            set {
                _StartPage = value;
            }
        }

        /// <summary>
        /// 结束页码
        /// </summary>
        public double EndPage {
            get {
                return _EndPage;
            }
            set {
                _EndPage = value;
            }
        }

        /// <summary>
        /// 递增或递减量
        /// </summary>
        public double Step {
            get {
                return _Step;
            }
            set {
                _Step = value;
            }
        }

        /// <summary>
        /// Url递增模式
        /// </summary>
        public PagedUrlPatternsMode Format {
            get {
                return _Format;
            }
            set {
                _Format = value;
            }
        }

        /// <summary>
        /// 分页URL模板
        /// </summary>
        /// <remarks>示例格式：baidu.com{0,200,50}</remarks>
        public string PagedUrlPattern {
            get {
                return _PagedUrlPattern;
            }
            set {
                _PagedUrlPattern = value;
            }
        }
        #endregion
    }
}