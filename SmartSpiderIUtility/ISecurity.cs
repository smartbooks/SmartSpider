using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSpider.IUtility
{
    public interface ISecurity
    {

        /// <summary>
        /// 获取由MD5算法生成的密文字符串
        /// </summary>
        /// <param name="expresslyText">明文</param>
        /// <returns>MD5密文文本</returns>
        string GetMD5CipherText(string expresslyText);

        /// <summary>
        /// 获取由BASE64算法生成的密文字符串
        /// </summary>
        /// <param name="expresslyText">明文</param>
        /// <returns>base64密文文本</returns>
        string GetBase64CipherText(string expresslyText);
    }
}
