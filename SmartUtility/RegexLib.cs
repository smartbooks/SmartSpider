namespace Smart.Utility
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 验证类
    /// </summary>
    public class RegexLib
    {
        public RegexLib()
        {
        }
        /// <summary>
        /// 验证空字符或“”字符
        /// </summary>
        /// <param name="strIn">待验证字符串</param>
        /// <returns>true:验证通过</returns>
        public static bool IsEmpty(string strIn)
        {
            return string.IsNullOrEmpty(strIn);
        }
        /// <summary>
        /// 是否Bool型
        /// </summary>
        /// <param name="strIn">待验证字符串</param>
        /// <returns>true:验证通过</returns>
        public static bool IsBool(string strIn)
        {
            bool result;
            return Boolean.TryParse(strIn, out result);
        }
        /// <summary>
        /// 验证IP 
        /// </summary>
        /// <param name="strIn">待验证字符串</param>
        /// <returns>true：验证通过</returns>
        public static bool IsValidIp(string strIn)
        {
            return Regex.IsMatch(strIn, @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
        }
        /// <summary>
        /// 验证Date：年月日 
        /// </summary>
        /// <param name="strIn">待验证字符串</param>
        /// <returns>true：验证通过</returns>
        public static bool IsValidDate(string strIn)
        {
            return Regex.IsMatch(strIn, @"[\d]{4}-[0-1][0-9]-[0-3][0-9]");
        }
        /// <summary>
        /// 验证值大小：是否在minValue至maxValue之间 
        /// </summary>
        /// <param name="strIn">待验证字符串</param>
        /// <param name="minValue">最小值</param>
        /// <param name="strIn">最大值</param>
        /// <returns>true：验证通过</returns>
        public static bool IsValidNumber(string strIn, int minValue, int maxValue)
        {
            return Regex.IsMatch(strIn, @"^[a-z]{" + minValue.ToString() + "," + maxValue.ToString() + "}$");
        }
        /// <summary>
        /// 验证是否为小数 
        /// </summary>
        /// <param name="strIn">待验证字符串</param>
        /// <returns>true：验证通过</returns>
        public static bool IsValidDecimal(string strIn)
        {
            return Regex.IsMatch(strIn, @"[0].\d{1,2}|[1]");
        }
        /// <summary>
        /// 验证Email地址 
        /// </summary>      
        /// <param name="strIn">待验证字符串</param>
        /// <returns>true：验证通过</returns>
        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        /// <summary>
        /// 验证后缀名是否为有效的图片 
        /// </summary>
        /// <param name="strIn">待验证字符串</param>
        /// <returns>true：验证通过</returns>
        public static bool IsValidPostfix(string strIn)
        {
            return Regex.IsMatch(strIn, @"\.(?i:gif|jpg)$");
        }
        /// <summary>
        /// dd-mm-yy 的日期形式代替 mm/dd/yy 的日期形式。 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>替换后的格式</returns>
        public static string MDYToDMY(String input)
        {
            return Regex.Replace(input, "\\b(?\\d{1,2})/(?\\d{1,2})/(?\\d{2,4})\\b", "${day}-${month}-${year}");
        }
        /// <summary>
        /// 验证是否为电话号码 
        /// </summary>
        /// <param name="strIn">待验证字符串</param>
        /// <returns>true：验证通过</returns>
        public static bool IsValidTel(string strIn)
        {
            return Regex.IsMatch(strIn, @"(\d+-)?(\d{4}-?\d{7}|\d{3}-?\d{8}|^\d{7,8})(-\d+)?");
        }
        /// <summary>
        /// 验证是否为数字和字母组成的字符串，禁止输入其他非法字符
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidStr(string strIn)
        {
            return Regex.IsMatch(strIn, @"[A-Za-z0-9]");
        }
        /// <summary>
        /// 验证是否为字母数字汉字
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidName(string strIn)
        {
            return Regex.IsMatch(strIn, @"[a-zA-Z0-9_\u4e00-\u9fa5]");
        }
    }
}