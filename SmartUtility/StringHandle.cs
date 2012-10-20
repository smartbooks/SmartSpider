
namespace Smart.Utility {
    using System;
    using System.Drawing;
    using System.Web;
    using System.Text.RegularExpressions;

    public class StringHandle {
        /// <summary>
        /// 取字符串中的非数字字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string ReturnStr(string str) {
            string ret = "";
            for (int i = 0; i < str.Length; i++) {
                string tmp = str.Substring(i, 1);
                if (!TypeParse.IsNumeric(tmp)) {
                    ret = ret + tmp;
                }
            }
            return ret;
        }

        /// <summary>
        /// 四舍五入方法
        /// </summary>
        /// <param name="Value">需要四舍五入的值</param>
        /// <param name="Digits">需要精确的位数，整数位为正，小数位为负。例：精确到小数第二位就是-2。精确到整数第二位就是2</param>
        /// <returns>四舍五入后的值</returns>
        public static double MathRound(double Value, int Digits) {
            double d = Convert.ToDouble(Value);
            if (Digits > 0)
                d = d / Math.Pow(10, Digits);
            string fractionString = "";   //小数部分字符串
            //取得小数点的位置，将字符串分为整数部分和小数部分，没有小数部分就为空。
            int dotPosition = d.ToString().IndexOf(".");
            if (dotPosition != -1)
                fractionString = d.ToString().Substring(dotPosition + 1);
            if (fractionString.Length <= -Digits + 1)
                d += Math.Pow(10, Digits - 2);
            if (Digits <= 0)
                return Math.Round(d, -Digits);
            else
                return Math.Round(d, 0) * Math.Pow(10, Digits);
        }

        /// <summary>
        /// 将字符串转换为Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ToColor(string color) {
            int red, green, blue = 0;
            char[] rgb;
            color = color.TrimStart('#');
            color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
            switch (color.Length) {
                case 3:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
                    green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
                    blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
                    return Color.FromArgb(red, green, blue);
                case 6:
                    rgb = color.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
                    green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
                    blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
                    return Color.FromArgb(red, green, blue);
                default:
                    return Color.FromName(color);

            }
        }

        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="str">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(string str) {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }

        /// <summary>
        /// 移除Html标记
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content) {
            string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content) {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str) {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str) {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }

        /// <summary>
        /// 取得系统路径
        /// </summary>
        public static string AppPath {
            get {
                string args = null;
                try {
                    args = System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
                } catch (Exception) { ;}
                if (String.IsNullOrEmpty(args)) args = System.Environment.CurrentDirectory;
                return args;
            }
        }

        /// <summary>
        /// 获取面源网面的地址
        /// </summary>
        /// <returns></returns>
        public static string GetFurl() {
            string ret = string.Empty;
            string[] url = HttpContext.Current.Request.ServerVariables["URL"].Split('/');
            if (HttpContext.Current.Request.ServerVariables["QUERY_STRING"] == "") {
                ret = url[url.Length - 1];
            } else {
                ret = url[url.Length - 1] + "?" + HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
            }

            return ret;
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP() {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty) {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty) {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (null == result || result == String.Empty || !VerifyData.IsIP(result)) {
                return "0.0.0.0";
            }

            return result;

        }

        /// <summary>
        /// 功能:对表 表单内容进行转换HTML操作,
        /// </summary>
        /// <param name="s">html字符串</param>
        /// <returns></returns>
        public static string HtmlCode(string s) {
            string str = "";
            str = s.Replace(">", "&gt;");
            str = s.Replace("<", "&lt;");
            str = s.Replace(" ", "&nbsp;");
            str = s.Replace("\n", "<br />");
            str = s.Replace("\r", "<br />");
            str = s.Replace("\r\n", "<br />");

            return str;
        }

        /// <summary>
        /// 功能:对表 表单内容进行转换HTML操作,
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string CodeHtml(string s) {
            string str = "";
            str = s.Replace("&gt;", ">");
            str = s.Replace("&lt;", "<");
            str = s.Replace("&nbsp;", " ");
            str = s.Replace("<br />", "\n");
            str = s.Replace("<br />", "\r");
            str = s.Replace("<br />", "\r\n");

            return str;
        }

        /// <summary>     
        /// 过滤xss攻击脚本     
        /// </summary>     
        /// <param name="input">传入字符串</param>     
        /// <returns>过滤后的字符串</returns>     
        public static string FilterXSS(string html) {
            if (string.IsNullOrEmpty(html)) return string.Empty;

            // CR(0a) ，LF(0b) ，TAB(9) 除外，过滤掉所有的不打印出来字符.     
            // 目的防止这样形式的入侵 ＜java\0script＞     
            // 注意：\n, \r,  \t 可能需要单独处理，因为可能会要用到     
            string ret = System.Text.RegularExpressions.Regex.Replace(
                html, "([\x00-\x08][\x0b-\x0c][\x0e-\x20])", string.Empty);

            ret = ret.Replace("\t", "");  //(补充，过滤TAB空格，那也是危险的XSS字符)

            //替换所有可能的16和10进制构建的恶意代码     
            //<IMG SRC=&#X40&#X61&#X76&#X61&#X73&#X63&#X72&#X69&#X70&#X74&#X3A&#X61&_#X6C&#X65&#X72&#X74&#X28&#X27&#X58&#X53&#X53&#X27&#X29>            
            ret = System.Text.RegularExpressions.Regex.Replace(ret, @"(&#[x|X]?\d+);?", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //过滤Javascript事件触发的恶意代码   
            string[] keywords = {
                                    "javascript", "vbscript", "expression", "applet", "meta", 
                                    "xml", "blink", "script", "embed", "object", 
                                    "iframe", "frame", "frameset", "ilayer", "layer", "bgsound", "title", "base",   
                                    "onabort", "onactivate", "onafterprint", "onafterupdate", "onbeforeactivate", 
                                    "onbeforecopy", "onbeforecut", "onbeforedeactivate", "onbeforeeditfocus", "onbeforepaste", 
                                    "onbeforeprint", "onbeforeunload", "onbeforeupdate", "onblur", "onbounce", "oncellchange", 
                                    "onchange", "onclick", "oncontextmenu", "oncontrolselect", "oncopy", "oncut", "ondataavailable", 
                                    "ondatasetchanged", "ondatasetcomplete", "ondblclick", "ondeactivate", "ondrag", "ondragend",
                                    "ondragenter", "ondragleave", "ondragover", "ondragstart", "ondrop", "onerror", "onerrorupdate", 
                                    "onfilterchange", "onfinish", "onfocus", "onfocusin", "onfocusout", "onhelp", "onkeydown", 
                                    "onkeypress", "onkeyup", "onlayoutcomplete", "onload", "onlosecapture", "onmousedown", 
                                    "onmouseenter", "onmouseleave", "onmousemove", "onmouseout", "onmouseover", "onmouseup", 
                                    "onmousewheel", "onmove", "onmoveend", "onmovestart", "onpaste", "onpropertychange", 
                                    "onreadystatechange", "onreset", "onresize", "onresizeend", "onresizestart", 
                                    "onrowenter", "onrowexit", "onrowsdelete", "onrowsinserted", "onscroll", "onselect", 
                                    "onselectionchange", "onselectstart", "onstart", "onstop", "onsubmit", "onunload"};

            bool found = true;
            while (found) {
                var retBefore = ret;
                for (int i = 0; i < keywords.Length; i++) {
                    //string pattern = "/"; (补允, 正则前台加/过滤不到)
                    string pattern = "";
                    for (int j = 0; j < keywords[i].Length; j++) {
                        if (j > 0)
                            pattern = string.Concat(pattern, '(', "(&#[x|X]0{0,8}([9][a][b]);?)?", "|(&#0{0,8}([9][10][13]);?)?",
                                ")?");
                        pattern = string.Concat(pattern, keywords[i][j]);
                    }
                    string replacement = string.Concat(keywords[i].Substring(0, 2), "＜x＞", keywords[i].Substring(2));
                    ret = System.Text.RegularExpressions.Regex.Replace(ret, pattern, replacement, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    if (ret == retBefore)
                        found = false;
                }

            }

            return ret;
        }
    }
}
