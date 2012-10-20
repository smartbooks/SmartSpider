namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SmartSpiderWebProxy {

        #region 私有变量定义
        private string _Host;
        private string _Password;
        private int _Port;
        private string _Username;
        #endregion

        #region 构造函数定义

        public SmartSpiderWebProxy(string host, int port, string username, string password) {
            this._Host = host;
            this._Port = port;
            this._Username = username;
            this._Password = password;
        }

        public SmartSpiderWebProxy(string host, int port) {
            this._Host = host;
            this._Port = port;
        }

        #endregion

        #region 公共属性定义

        public string Host {
            get {
                return _Host;
            }
            set {
                _Host = value;
            }
        }

        public string Password {
            get {
                return _Password;
            }
            set {
                _Password = value;
            }
        }

        public int Port {
            get {
                return _Port;
            }
            set {
                _Port = value;
            }
        }

        public string Username {
            get {
                return _Username;
            }
            set {
                _Username = value;
            }
        }

        #endregion
    }
}