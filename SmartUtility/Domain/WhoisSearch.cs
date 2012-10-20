using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace Smart.Utility.Domain {

    /// <summary>
    /// 域名Whois信息查询
    /// 调用方式：GetDomainStatus("qq.com", "com") 或 GetDomainContract("qq.com")
    /// </summary>
    public class WhoisSearch {
        /// <summary>
        /// 域名注册信息
        /// </summary>
        /// <param name="domain">输入域名，不包含www</param>
        /// <returns></returns>
        public static string GetDomain(string domain) {
            string strServer;
            string whoisServer = "whois.internic.net,whois.cnnic.net.cn,whois.publicinterestregistry.net,whois.nic.gov,whois.hkdnr.net.hk,whois.nic.name";
            string[] whoisServerList = Regex.Split(whoisServer, ",", RegexOptions.IgnoreCase);

            if (domain == null)
                throw new ArgumentNullException();
            int ccStart = domain.LastIndexOf(".");
            if (ccStart < 0 || ccStart == domain.Length)
                throw new ArgumentException();

            //根据域名后缀选择服务器
            string domainEnd = domain.Substring(ccStart + 1).ToLower();
            switch (domainEnd) {
                default:    //.COM, .NET, .EDU 
                    strServer = whoisServerList[0];
                    break;
                case "cn":  //所有.cn的域名
                    strServer = whoisServerList[1];
                    break;
                case "org":  //所有.org的域名
                    strServer = whoisServerList[2];
                    break;
                case "gov":  //所有.gov的域名
                    strServer = whoisServerList[3];
                    break;
                case "hk":  //所有.hk的域名
                    strServer = whoisServerList[4];
                    break;
                case "name":  //所有.name的域名
                    strServer = whoisServerList[5];
                    break;
            }

            string ret = "";
            Socket s = null;
            try {
                string cc = domain.Substring(ccStart + 1);
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.SendTimeout = 900;
                s.Connect(new IPEndPoint(Dns.Resolve(strServer).AddressList[0], 43));
                s.Send(Encoding.ASCII.GetBytes(domain + "\r\n"));
                byte[] buffer = new byte[1024];
                int recv = s.Receive(buffer);
                while (recv > 0) {
                    ret += Encoding.UTF8.GetString(buffer, 0, recv);
                    recv = s.Receive(buffer);
                }
                s.Shutdown(SocketShutdown.Both);


            } catch (SocketException ex) {
                return ex.Message;
            } finally {
                if (s != null)
                    s.Close();
            }

            return ret;
        }

        /// <summary>
        /// 指定whois查询域名信息
        /// </summary>
        /// <param name="domain">输入域名，不包含www</param>
        /// <param name="strServer">输入whois</param>
        /// <returns></returns>
        public static string GetDomain(string domain, string strServer) {

            if (domain == null)
                throw new ArgumentNullException();
            int ccStart = domain.LastIndexOf(".");
            if (ccStart < 0 || ccStart == domain.Length)
                throw new ArgumentException();

            string ret = "";
            Socket s = null;
            try {
                string cc = domain.Substring(ccStart + 1);
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.SendTimeout = 900;
                s.Connect(new IPEndPoint(Dns.Resolve(strServer).AddressList[0], 43));
                s.Send(Encoding.ASCII.GetBytes(domain + "\r\n"));
                byte[] buffer = new byte[1024];
                int recv = s.Receive(buffer);
                while (recv > 0) {
                    ret += Encoding.UTF8.GetString(buffer, 0, recv);
                    recv = s.Receive(buffer);
                }
                s.Shutdown(SocketShutdown.Both);
            } catch (SocketException ex) {
                return ex.Message;
            } finally {
                if (s != null)
                    s.Close();
            }

            return ret;
        }

        /// <summary>
        /// 域名注册信息和联系人信息
        /// </summary>
        /// <param name="domain">输入域名，不包含www</param>
        /// <returns></returns>
        public string GetDomainContract(string domain) {
            string whoisInfo = "";

            whoisInfo = GetDomain(domain);

            //取域名联系人
            string str = "Whois Server:";
            StringReader sr = new StringReader(whoisInfo);
            string newline = "";
            while (sr.Peek() > 0) {
                newline = sr.ReadLine();
                newline = newline.Trim();
                if (newline.StartsWith(str))
                    break;
            }
            sr.Close();
            int retPos = newline.IndexOf(str);
            if (retPos != -1) {
                string newWhois = newline.Substring(str.Length).Trim();
                whoisInfo += "\r\n" + GetDomain(domain, newWhois);
            }
            return whoisInfo;
        }
        
        /// <summary>
        /// 域名状态
        /// </summary>
        /// <param name="domain">输入域名，不包含www</param>
        /// <param name="domainType">域名类型（国际域名：com|国内域名：cn）</param>
        /// <returns></returns>
        public string GetDomainStatus(string domain, string domainType) {
            string whoisInfo = "";
            string Status = "";

            whoisInfo = GetDomain(domain);

            //取域名联系人
            string str = "Status:";
            if (domainType == "cn")
                str = "Domain Status:";

            StringReader sr = new StringReader(whoisInfo);
            string newline = "";
            while (sr.Peek() > 0) {
                newline = sr.ReadLine();
                newline = newline.Trim();
                if (newline.StartsWith(str))
                    break;
            }
            sr.Close();

            int retPos = newline.IndexOf(str);
            if (retPos != -1)
                Status = newline.Substring(str.Length).Trim();
            else
                Status = whoisInfo;

            return Status;
        }
    }
}
