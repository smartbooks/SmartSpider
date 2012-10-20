namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LogEventArgs {
        #region 私有变量定义
        private bool _Detailed;
        private int _Indent;
        private string _Message;
        #endregion 

        #region 构造函数定义

        public LogEventArgs(string message) {
            this._Message = message;
        }

        public LogEventArgs(string message, bool detailed) {
            this._Message = message;
            this._Detailed = detailed;
        }

        public LogEventArgs(string message, int indent) {
            this._Message = message;
            this._Indent = indent;
        }

        public LogEventArgs(string message, int indent, bool detailed) {
            this._Message = message;
            this._Indent = indent;
            this._Detailed = detailed;
        }
        #endregion

        #region 公共属性定义
        /// <summary>
        /// 显示详细日志
        /// </summary>
        public bool Detailed {
            get {
                return _Detailed;
            }
            set {
                _Detailed = value;
            }
        }

        /// <summary>
        /// 缩进字符个数
        /// </summary>
        public int Indent {
            get {
                return _Indent;
            }
            set {
                _Indent = value;
            }
        }

        /// <summary>
        /// 日志消息
        /// </summary>
        public string Message {
            get {
                return _Message;
            }
            set {
                _Message = value;
            }
        }
        #endregion
    }
}
