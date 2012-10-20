namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Net;
    using System.IO;
    using System.IO.Compression;

    public class HttpHelper {
        #region 私有变量定义
        private HttpWebRequest _WebRequest;
        private HttpWebResponse _WebResponse;
        private CookieContainer _Cookie;
        #endregion

        #region 公共字段定义
        /// <summary>
        /// Http请求内容编码
        /// </summary>
        public Encoding _encoding = Encoding.GetEncoding("gb2312");
        #endregion

        #region 公共方法定义
        public HttpHelper() {
            //this._WebRequest.Timeout = 5000;   //默认超时5秒钟
        }

        /// <param name="handers">Hander集合</param>
        /// <param name="method">Http请求方式</param>
        /// <param name="encoding">网页数据编码</param>
        /// <param name="timeOut">请求超时单位/秒</param>
        public HttpHelper(WebHeaderCollection handers, HttpMethod method, string encoding, int timeOut) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="encoding">请求编码</param>
        public HttpHelper(Encoding encoding) {
            if (encoding == null) {
                this._encoding = Encoding.Default;
            } else {
                this._encoding = encoding;
            }
            this._Cookie = new CookieContainer();
        }

        /// <summary>
        /// 发送一次HTTP请求
        /// </summary>
        /// <param name="url">Url地址</param>
        public string RequestResult(string url) {
            return this.RequestResult(url, "");
        }

        /// <summary>
        /// 发送一次HTTP请求
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="referer">referer地址</param>
        public string RequestResult(string url, string referer) {
            return this.RequestResult(url, referer, HttpMethod.GET, "");
        }

        /// <summary>
        /// 发送一次HTTP请求
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="referer">referer地址</param>
        /// <param name="method">请求模式</param>
        /// <param name="data">post数据(仅用于POST模式)</param>
        public string RequestResult(string url, string referer, HttpMethod method, string data) {
            try
            {
                this._WebRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
                this._WebRequest.CookieContainer = this._Cookie;
                this._WebRequest.Method = method.ToString();
                this._WebRequest.Accept = "*/*";
                this._WebRequest.ContentType = "application/x-www-form-urlencoded";
                if (method == HttpMethod.POST)
                {
                    byte[] buffer = this._encoding.GetBytes(data);
                    this._WebRequest.ContentLength = buffer.Length;
                    this._WebRequest.GetRequestStream().Write(buffer, 0, buffer.Length);
                }
                this._WebResponse = (HttpWebResponse)this._WebRequest.GetResponse();

                /*
                 * gzip内容解压缩
                 */
                StreamReader read = null;
                if (_WebResponse.ContentEncoding.ToUpper().Equals("GZIP")) {
                    read = new StreamReader(new GZipStream(this._WebResponse.GetResponseStream(), CompressionMode.Decompress));
                }
                else {
                    read = new StreamReader(this._WebResponse.GetResponseStream(), this._encoding);
                }
                //this._Cookie.Add(this._WebResponse.Cookies);
                string htmlText = read.ReadToEnd();
                read.Close();
                read.Dispose();
                return htmlText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Stream GetResponse(string url)
        {
            this._WebRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            this._WebRequest.CookieContainer = this._Cookie;
            this._WebRequest.Method = "GET";
            this._WebRequest.Accept = "*/*";
            this._WebRequest.ContentType = "application/x-www-form-urlencoded";
            this._WebResponse = (HttpWebResponse)this._WebRequest.GetResponse();
            return _WebResponse.GetResponseStream();
        }

        #endregion

        #region 公共属性定义
        /// <summary>
        /// HttpWebRequest对象
        /// </summary>
        public HttpWebRequest WebRequest {
            get {
                return _WebRequest;
            }
            set {
                _WebRequest = value;
            }
        }

        /// <summary>
        /// HttpWebResponse对象
        /// </summary>
        public HttpWebResponse WebResponse {
            get {
                return _WebResponse;
            }
            set {
                _WebResponse = value;
            }
        }
        #endregion
    }
}
