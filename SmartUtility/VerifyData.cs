

namespace Smart.Utility {
    using System.Text.RegularExpressions;

    public class VerifyData {
        /// <summary>
        /// 判断是否为Email
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string s) {
            return Regex.IsMatch(s, @"^(.+)@(.+)$");
        }

        /// <summary>
        /// 判断是否为IP地址
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsIP(string s) {
            return Regex.IsMatch(s, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
        }

        /// <summary>
        /// 判断是否为URL地址
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsURL(string s) {
            return Regex.IsMatch(s, @"^[a-zA-z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$");
        }

        /// <summary>
        /// 判断是否为正确的身份证号码
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsID(string s) {
            return Regex.IsMatch(s, @"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$");
        }

        /// <summary>
        /// 判断是否为电话号码格式
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsPhone(string s) {
            return Regex.IsMatch(s, @"^(\d{2,4}[-_－—]?)?\d{3,8}([-_－—]?\d{3,8})?([-_－—]?\d{1,7})?$)|(^0?1[35]\d{9}$");
        }

        /// <summary>
        /// 判断是否为日期
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsDate(string s) {
            return Regex.IsMatch(s, @"^(\d{4})(-)(\d{2})(-)(\d{2})$");
        }

        /// <summary>
        /// 判断是否为中文字符
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsChinese(string s) {
            return Regex.IsMatch(s, @"^[\u4e00-\u9fa5]+$");
        }

        /// <summary>
        /// 判断是否为字母数字组合
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsCharNumber(string s) {
            return Regex.IsMatch(s, @"^\w+$");
        }

        /// <summary>
        /// 判断是否为字母数字下划线组合
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsName(string s) {
            return Regex.IsMatch(s, @"^[a-zA-Z0-9_]+$");
        }

        /// <summary>
        /// 判断是否为字符或是数字
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsCharNum(string s) {
            return Regex.IsMatch(s, @"^[A-Za-z0-9]+$");
        }

    }
}
