
namespace Smart.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 字符串处理
    /// </summary>
    public class StringHelper
    {
        #region 截取字符串
        /// <summary>
        /// 获取子字符串
        /// </summary>
        /// <param name="inputString">输入字符串</param>
        /// <param name="len">截取长度</param>
        /// <returns>截取字符串</returns>
        public static string GetSubStr(string inputString, int len)
        {
            if (string.IsNullOrEmpty(inputString))
                return string.Empty;

            if (len == 0)
                return inputString;


            byte[] str = Encoding.Default.GetBytes(inputString);

            if (str.Length <= inputString.Length)
                return inputString;

            if (str.Length > len)
                Array.Clear(str, len, str.Length - len);

            return Encoding.Default.GetString(str).TrimEnd('\0');
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="htmlText">源字符串</param>
        /// <param name="PreviousFlag">信息前标志</param>
        /// <param name="FollowingFlag">信息后标志</param>
        /// <returns>截取结果</returns>
        public static string SubString(string htmlText, string PreviousFlag, string FollowingFlag)
        {
            string result = htmlText;
            PreviousFlag = PreviousFlag.Trim();
            FollowingFlag = FollowingFlag.Trim();

            int indexPreviousFlag = result.IndexOf(PreviousFlag);    //信息前标志
            int indexFollowingFlag = result.IndexOf(FollowingFlag);  //信息后标志

            //如果开始或结束标记不存在，那么返回空串
            if (indexFollowingFlag == -1 && indexPreviousFlag == -1) {
                return "";
            }

            int indexLength = indexFollowingFlag - indexPreviousFlag;
            if (indexLength > 1 && indexLength >= FollowingFlag.Length)
            {
                result = result.Substring(
                    indexPreviousFlag + PreviousFlag.Length,
                    indexLength - FollowingFlag.Length);
            }
            return result;
        }
        #endregion


        #region hex与hexbin的转换
        public static void hexbin2hex(byte[] bhexbin, byte[] bhex, int nlen)
        {
            for (int i = 0; i < nlen / 2; i++)
            {
                if (bhexbin[2 * i] < 0x41)
                {
                    bhex[i] = Convert.ToByte(((bhexbin[2 * i] - 0x30) << 4) & 0xf0);
                }
                else
                {
                    bhex[i] = Convert.ToByte(((bhexbin[2 * i] - 0x37) << 4) & 0xf0);
                }

                if (bhexbin[2 * i + 1] < 0x41)
                {
                    bhex[i] |= Convert.ToByte((bhexbin[2 * i + 1] - 0x30) & 0x0f);
                }
                else
                {
                    bhex[i] |= Convert.ToByte((bhexbin[2 * i + 1] - 0x37) & 0x0f);
                }
            }
        }
        public static byte[] hexbin2hex(byte[] bhexbin, int nlen)
        {
            if (nlen % 2 != 0)
                return null;
            byte[] bhex = new byte[nlen / 2];
            hexbin2hex(bhexbin, bhex, nlen);
            return bhex;
        }
        public static void hex2hexbin(byte[] bhex, byte[] bhexbin, int nlen)
        {
            byte c;
            for (int i = 0; i < nlen; i++)
            {
                c = Convert.ToByte((bhex[i] >> 4) & 0x0f);
                if (c < 0x0a)
                {
                    bhexbin[2 * i] = Convert.ToByte(c + 0x30);
                }
                else
                {
                    bhexbin[2 * i] = Convert.ToByte(c + 0x37);
                }
                c = Convert.ToByte(bhex[i] & 0x0f);
                if (c < 0x0a)
                {
                    bhexbin[2 * i + 1] = Convert.ToByte(c + 0x30);
                }
                else
                {
                    bhexbin[2 * i + 1] = Convert.ToByte(c + 0x37);
                }
            }
        }
        public static byte[] hex2hexbin(byte[] bhex, int nlen)
        {
            byte[] bhexbin = new byte[nlen * 2];
            hex2hexbin(bhex, bhexbin, nlen);
            return bhexbin;
        }
        #endregion

        #region 数组和字符串之间的转化
        public static byte[] str2arr(string s)
        {
            return (new UnicodeEncoding()).GetBytes(s);
        }
        public static string arr2str(byte[] buffer)
        {
            return (new UnicodeEncoding()).GetString(buffer, 0, buffer.Length);
        }

        public static byte[] str2ascarr(string s)
        {
            return System.Text.UnicodeEncoding.Convert(System.Text.Encoding.Unicode, System.Text.Encoding.ASCII, str2arr(s));
        }

        public static byte[] str2hexascarr(string s)
        {
            byte[] hex = str2ascarr(s);
            byte[] hexbin = hex2hexbin(hex, hex.Length);
            return hexbin;
        }
        public static string ascarr2str(byte[] b)
        {
            return System.Text.UnicodeEncoding.Unicode.GetString(System.Text.ASCIIEncoding.Convert(System.Text.Encoding.ASCII, System.Text.Encoding.Unicode, b));
        }

        public static string hexascarr2str(byte[] buffer)
        {
            byte[] b = hex2hexbin(buffer, buffer.Length);
            return ascarr2str(b);
        }
        #endregion 
    }
}
