using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSpider.IUtility
{
    public interface ISpider
    {
        /// <summary>
        /// 获取PR值
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>域名pr值</returns>
        int GetPR(string domain);

        /// <summary>
        /// 获取aleax排名
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>全球aleax排名</returns>
        double GetAleax(string domain);

        /// <summary>
        /// 获取网站收录
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>网站收录总数</returns>
        double GetRecord(string domain);

        /// <summary>
        /// 获取文章收录总量
        /// </summary>
        /// <param name="articleTitle">文章标题</param>
        /// <returns>文章收录总量</returns>
        double GetArticleRecord(string articleTitle);

        /// <summary>
        /// 获取关键字排名
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="domain">域名</param>
        int GetKeywordRanking(string keyword, string domain);
    }
}
