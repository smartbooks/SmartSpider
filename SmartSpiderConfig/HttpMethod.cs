using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSpider.Config {
    public enum HttpMethod {
        /// <summary>
        /// Http GET请求方式
        /// </summary>
        GET = 0,
        /// <summary>
        /// Http POST请求方式
        /// </summary>
        POST = 1,
        /// <summary>
        /// Http HEAD请求方式
        /// </summary>
        HEAD = 2,
        /// <summary>
        /// Http PUT请求方式
        /// </summary>
        PUT = 3,
        /// <summary>
        /// Http DELETE请求方式
        /// </summary>
        DELETE = 4,
    }
}
