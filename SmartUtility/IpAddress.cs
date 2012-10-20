namespace Smart.Utility {
    using System.Net;
    using System;

    /// <summary>
    /// 本机IP地址相关信息的获取
    /// </summary>
    public class IpAddress {
        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocationIpAddress() {
            System.Net.IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            if (addressList.Length != 0) {
                return addressList[0].ToString();
            } else {
                throw new Exception("IP地址为空");
            }
        }
    }
}
