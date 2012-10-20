using System;
using System.Collections.Generic;
using System.Text;

namespace Smart.Utility {
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    public class TypeParse {
        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsInt32Num(object Expression) {
            int ret;
            return int.TryParse(Expression.ToString(), out ret);
        }

        /// <summary>
        /// 判断是否为字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLetter(string str) {
            return Regex.IsMatch(str, @"^[A-Za-z]+$");
        }

        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str) {
            if (str == null || str.Length == 0)
                return false;
            foreach (char c in str) {
                if (!Char.IsNumber(c)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断是整数或小数
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsDouble(object Expression) {
            int ret;
            return int.TryParse(Expression.ToString(), out ret);
        }

        ///   <summary> 
        ///   判断一个字符串是否是正数型的字符串 
        ///   </summary> 
        ///   <param name= "strValue "> 字符串 </param> 
        ///   <returns> 是则返回true，否则返回false </returns> 
        public static bool IsUnsign(string strValue) {
            int ret;
            if (strValue.Substring(0, 1) == "-") {
                return false;
            } else {
                return int.TryParse(strValue, out ret);
            }
        }

        ///   <summary> 
        ///   判断一个字符串是否是正数型的字符串 
        ///   </summary> 
        ///   <param name= "strValue "> 字符串 </param> 
        ///   <returns> 是则返回true，否则返回false </returns> 
        public static bool IsUnsignDouble(string strValue) {
            double ret;
            if (strValue.Substring(0, 1) == "-") {
                return false;
            } else {
                return double.TryParse(strValue, out ret);
            }
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object Expression, bool defValue) {
            if (Expression != null) {
                if (string.Compare(Expression.ToString(), "true", true) == 0) {
                    return true;
                } else if (string.Compare(Expression.ToString(), "false", true) == 0) {
                    return false;
                }
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object Expression, int defValue) {
            if (Expression != null) {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*$")) {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1')) {
                        return Convert.ToInt32(str);
                    }
                }
            }
            return defValue;
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(object strValue, float defValue) {
            if ((strValue == null) || (strValue.ToString().Length > 10)) {
                return defValue;
            }

            float intValue = defValue;
            if (strValue != null) {
                bool IsFloat = Regex.IsMatch(strValue.ToString(), @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat) {
                    intValue = Convert.ToSingle(strValue);
                }
            }
            return intValue;
        }

        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] strNumber) {
            if (strNumber == null) {
                return false;
            }
            if (strNumber.Length < 1) {
                return false;
            }
            foreach (string id in strNumber) {
                if (!IsNumeric(id)) {
                    return false;
                }
            }
            return true;

        }

        /// <summary>
        /// 将字符串数组转成字符串
        /// </summary>
        /// <param name="strArray">字符串数组</param>        
        /// <param name="Separator">分隔符</param>
        /// <returns>字符串</returns>
        public static string StringArrayToString(string[] strArray, char Separator) {
            string str = string.Empty;
            for (int i = 0; i < strArray.Length; i++) {
                str = str + strArray[i] + Separator;
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 判断字符串是否在字符串数组中存在
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="strArray">字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsStringArray(string str, string[] strArray) {
            bool s = false;
            for (int i = 0; i < strArray.Length; i++) {
                if (strArray[i] == str) {
                    s = true;
                }
            }
            return s;
        }

        /// <summary>
        /// 判断字符串在字符串数组中的位置
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="strArray">字符串数组</param>
        /// <returns>返回位置</returns>
        public static int ReturnStringSeat(string str, string[] strArray) {
            int ret = 0;
            for (int i = 0; i < strArray.Length; i++) {
                if (strArray[i] == str) {
                    ret = i;
                }
            }
            return ret;
        }

        /// <summary>
        /// 取数组中最大值
        /// </summary>
        /// <param name="n">数组</param>
        /// <returns></returns>
        public static int MaxInt(int[] n) {
            int ret = 0;
            if (n.Length != 0) {
                ret = n[0];
            }
            for (int i = 1; i < n.Length; i++) {
                if (n[i] > ret) {
                    ret = n[i];
                }
            }
            return ret;
        }

        /// <summary>
        /// 取数组中最大值
        /// </summary>
        /// <param name="n">数组</param>
        /// <returns></returns>
        public static string MaxStr(string[] str) {
            string ret = "0";
            if (str[0] != null) {
                ret = str[0];
            }
            for (int i = 1; i < str.Length; i++) {
                if (int.Parse(str[i]) > int.Parse(ret)) {
                    ret = str[i];
                }
            }
            return ret;
        }

        /// <summary>
        /// 取数组中最小值
        /// </summary>
        /// <param name="n">数组</param>
        /// <returns></returns>
        public static int MinInt(int[] n) {
            int ret = 0;
            if (n.Length != 0) {
                ret = n[0];
            }
            for (int i = 1; i < n.Length; i++) {
                if (n[i] < ret) {
                    ret = n[i];
                }
            }
            return ret;
        }

        /// <summary>
        /// 取数组中最小值
        /// </summary>
        /// <param name="n">数组</param>
        /// <returns></returns>
        public static string MinStr(string[] str) {
            string ret = "0";
            if (str[0] != null) {
                ret = str[0];
            }
            for (int i = 1; i < str.Length; i++) {
                if (int.Parse(str[i]) < int.Parse(ret)) {
                    ret = str[i];
                }
            }
            return ret;
        }
    }
}
