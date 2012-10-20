namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SmartSpiderInformation {
        #region 私有变量
        private string _ComputerID;
        private string _Language;
        private string _ProductName;
        private string _EditionNumber;
        private string _EditionType;
        #endregion

        #region 构造函数定义
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="productName">产品名称</param>
        /// <param name="editionType"></param>
        /// <param name="editionNumber"></param>
        /// <param name="language">语言</param>
        /// <param name="computerID">计算机标示</param>
        public SmartSpiderInformation(string productName, string editionType, string editionNumber, string language, string computerID) {
            this._ProductName = productName;
            this._EditionType = editionType;
            this._EditionNumber = editionNumber;
            this._Language = language;
            this._ComputerID = computerID;
        }
        #endregion

        #region 公共属性定义

        public string ComputerID {
            get {
                return _ComputerID;
            }
            set {
                _ComputerID = value;
            }
        }

        public string Language {
            get {
                return _Language;
            }
            set {
                _Language = value;
            }
        }

        public string ProductName {
            get {
                return _ProductName;
            }
            set {
                _ProductName = value;
            }
        }

        public string EditionNumber {
            get {
                return _EditionNumber;
            }
            set {
                _EditionNumber = value;
            }
        }

        public string EditionType {
            get {
                return _EditionType;
            }
            set {
                _EditionType = value;
            }
        }

        #endregion
    }
}
