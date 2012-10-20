using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSpider.IUtility
{
    public interface IDomain
    {
        /// <summary>
        /// 域名删除时间
        /// </summary>
        /// <param name="domain">域名</param>
        DateTime GetDeleteTime(string domain);

        /// <summary>
        /// 获取域名Whois信息
        /// </summary>
        /// <param name="domain">域名</param>
        string GetWhoisInfo(string domain);

        /// <summary>
        /// 获取域名IP地址
        /// </summary>
        /// <param name="domain">域名</param>
        string GetIPAddress(string domain);
    }
}
