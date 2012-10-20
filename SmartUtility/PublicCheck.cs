
namespace Smart.Utility
{
    using System;

    /// <summary>
    /// 公共检查
    /// </summary>
    public class PublicCheck
    {
        #region 判断起止日期


        /// <summary>
        /// 判断结束日期不能超过开始日期2个月，没有超过返回true ，超过则抛出异常。
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static bool CheckDate(string beginDate, string endDate)
        {
            DateTime begin = new DateTime();
            DateTime end = new DateTime();
            try
            {
                begin = DateTime.Parse(beginDate);
                end = DateTime.Parse(endDate);
            }
            catch
            {
                throw new Exception("日期格式错误。");
            }

            if (end.AddMonths(-2) > begin)
            {
                throw new Exception("结束日期不能超过开始日期2个月。");
            }
            return true;
        }


        /// <summary>
        /// 判断起止日期格式是否正确，正确返回true ， 不正确抛出异常。
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static bool CheckBeginEndDate(string beginDate, string endDate)
        {
            return CheckBeginEndDate(beginDate, endDate, false);
        }

        /// <summary>
        /// 判断起止日期格式是否正确，正确返回true ， 不正确抛出异常。
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static bool CheckBeginEndDate(DateTime beginDate, DateTime endDate)
        {
            if (beginDate > endDate)
            {
                // 结束日期大于开始日期
                throw new Exception("开始日期不能晚于结束日期。");
            }
            return true;
        }

        /// <summary>
        /// 检查开始结束日期
        /// </summary>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="IsCheckCurrentDate">是否检查当前日期
        /// （如果此条件为True，则查询结束日期不等于或晚于当前日期）</param>
        /// <returns></returns>
        public static bool CheckBeginEndDate(string beginDate, string endDate, bool IsCheckCurrentDate)
        {
            DateTime begin = new DateTime();
            DateTime end = new DateTime();
            try
            {
                begin = DateTime.Parse(beginDate);
                end = DateTime.Parse(endDate);
            }
            catch
            {
                throw new Exception("日期格式错误。");
                //return false;
            }

            if (begin > end)
            {
                // 结束日期大于开始日期
                throw new Exception("开始日期不能晚于结束日期。");
                //return false;
            }

            if (IsCheckCurrentDate)
            {
                if (end >= DateTime.Today)
                {
                    throw new Exception("数据还未统计。不能查看当前日，以及当前日之后的统计数据。");
                }
            }


            return true;
        }
        #endregion

        #region 判断是否是数字

        /// <summary>
        /// 判断是否是数字,是数字，返回真true，不是数字返回假false
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是数字，返回真true，不是数字返回假false</returns>
        public static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region 验证输入字符串是否是金额类型

        /// <summary>
        /// 验证输入字符串是否是金额类型,是金额类型，返回true,如：0、0.1　0.01等;否则返回false
        /// </summary>
        /// <param name="money">待判断的值</param>
        /// <returns>是金额类型，返回true,如：0、0.1　0.01等;否则返回false</returns>
        public static bool IsMoney(string money)
        {
            bool result = true;
            int count = 0;
            bool startxs = false;
            int xiaoshu = 0;
            if (money.Length == 0)
            {
                return false;
            }
            try
            {
                char[] x = money.ToCharArray();
                for (int i = 0; i < x.Length; i++)
                {
                    if (!char.IsNumber(x[i]) && x[i] != '.')
                    {
                        result = false;
                        break;
                    }
                    if (x[i] == '.')
                    {
                        count++;
                        if (i == 0 || i == money.Length - 1)
                        {
                            result = false;
                            break;
                        }
                        if (count > 1)
                        {
                            result = false;
                            break;
                        }
                        startxs = true;

                    }
                    if (startxs)
                    {
                        xiaoshu++;
                        if (xiaoshu > 3)    //小数点后超过两位
                        {
                            result = false;
                            break;
                        }
                    }
                } // for 

                return result;
            } //try
            catch
            {
                result = false;
                return result;
            }
        }

        #endregion

        public static string GetChkStr(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return string.Empty;
            }
            else
            {

                return strValue.Replace("'", "").Replace("%", "").Replace("and", "").Replace("And", "").Replace("select", "").Replace("or", "").Replace("OR", "").Trim();
            }

        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="VcodeNum">生成字母的个数</param>
        /// <returns>string</returns>
        public static string RndNum(int VcodeNum)
        {
            string Vchar = "0,1,2,3,4,5,6,7,8,9";
            string[] VcArray = Vchar.Split(',');
            string VNum = ""; //由于字符串很短，就不用StringBuilder了
            int temp = -1; //记录上次随机数值，尽量避免生产几个一样的随机数

            //采用一个简单的算法以保证生成随机数的不同
            Random rand = new Random();
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(VcArray.Length);
                if (temp != -1 && temp == t)
                {
                    return RndNum(VcodeNum);
                }
                temp = t;
                VNum += VcArray[t];
            }
            return VNum;
        }
    }
}
