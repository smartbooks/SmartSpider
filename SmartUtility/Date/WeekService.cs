using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Smart.Utility.Date {
    public static class WeekService {

        /// <summary>
        /// 获取一年中指定的一周的开始日期和结束日期。开始日期遵循ISO 8601即星期一。
        /// </summary>
        /// <remarks>Write by vrhero</remarks>
        /// <param name="year">年（1 到 9999）</param>
        /// <param name="weeks">周（1 到 53）</param>
        /// <param name="weekrule">确定首周的规则</param>
        /// <param name="first">当此方法返回时，则包含参数 year 和 weeks 指定的周的开始日期的 System.DateTime 值；如果失败，则为 System.DateTime.MinValue。如果参数 year 或 weeks 超出有效范围，则操作失败。该参数未经初始化即被传递。</param>
        /// <param name="last">当此方法返回时，则包含参数 year 和 weeks 指定的周的结束日期的 System.DateTime 值；如果失败，则为 System.DateTime.MinValue。如果参数 year 或 weeks 超出有效范围，则操作失败。该参数未经初始化即被传递。</param>
        /// <returns>成功返回 true，否则为 false。</returns>
        public static bool GetDaysOfWeeks(int year, int weeks, CalendarWeekRule weekrule, out DateTime first, out DateTime last) {
            //初始化 out 参数
            first = DateTime.MinValue;
            last = DateTime.MinValue;

            //不用解释了吧...
            if (year < 1 | year > 9999)
                return false;

            //一年最多53周地球人都知道...
            if (weeks < 1 | weeks > 53)
                return false;

            //取当年首日为基准...为什么？容易得呗...
            DateTime firstCurr = new DateTime(year, 1, 1);
            //取下一年首日用于计算...
            DateTime firstNext = new DateTime(year + 1, 1, 1);

            //将当年首日星期几转换为数字...星期日特别处理...ISO 8601 标准...
            int dayOfWeekFirst = (int)firstCurr.DayOfWeek;
            if (dayOfWeekFirst == 0) dayOfWeekFirst = 7;

            //得到未经验证的周首日...
            first = firstCurr.AddDays((weeks - 1) * 7 - dayOfWeekFirst + 1);

            //周首日是上一年日期的情况...
            if (first.Year < year) {
                switch (weekrule) {
                    case CalendarWeekRule.FirstDay:
                        //不用解释了吧...
                        first = firstCurr;
                        break;
                    case CalendarWeekRule.FirstFullWeek:
                        //顺延一周...
                        first = first.AddDays(7);
                        break;
                    case CalendarWeekRule.FirstFourDayWeek:
                        //周首日距年首日不足4天则顺延一周...
                        if (firstCurr.Subtract(first).Days > 3) {
                            first = first.AddDays(7);
                        }
                        break;
                    default:
                        break;
                }
            }

            //得到未经验证的周末日...
            last = first.AddDays(7).AddSeconds(-1);

            //周末日是下一年日期的情况...
            if (last.Year > year) {
                switch (weekrule) {
                    case CalendarWeekRule.FirstDay:
                        last = firstNext.AddSeconds(-1);
                        break;
                    case CalendarWeekRule.FirstFullWeek:
                        //不用处理
                        break;
                    case CalendarWeekRule.FirstFourDayWeek:
                        //周末日距下一年首日不足4天则提前一周...
                        if (firstNext.Subtract(first).Days < 4) {
                            first = first.AddDays(-7);
                            last = last.AddDays(-7);
                        }
                        break;
                    default:
                        break;
                }
            }
            return true;
        }
    }
}
