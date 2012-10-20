
namespace Smart.Utility
{
    using System;
    using System.Text;
    using System.Configuration;
    using Smart.Security;

    /// <summary>
    /// 数据库连接字符串加密解密
    /// </summary>
    public class ConnectionInfo
    {
        public static string connstring = string.Empty;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string CONN_STRING
        {
            get
            {
                if (string.Empty == connstring)
                {
                    try
                    {
                        connstring = ConfigurationManager.AppSettings["ConnString"];
                        connstring = ConnectionInfo.DecryptDBConnectionString(connstring);
                    } 
                    catch(Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
                return connstring;
            }
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="InputConnectionString">加密的连接字符串</param>
        /// <returns>string</returns>
        public static string DecryptDBConnectionString(string InputConnectionString)
        {
            return Encrypter.Decrypt(InputConnectionString);
        }

        /// <summary>
        /// 加密数据库连接字符串
        /// </summary>
        /// <param name="encryptedString">加密字符串</param>
        /// <returns></returns>
        public static string EncryptDBConnectionString(string encryptedString)
        {
            return Encrypter.Encrypt(encryptedString);
        }
    }
}
