using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace SmartSpider.Config
{
    /// <summary>
    /// 当增加一个导航地址时产生的事件委托
    /// </summary>
    public delegate void onSingleComplete(object sender, string url);

    /// <summary>
    /// 解析起始和导航规则
    /// </summary>
    public class ParseNavigationRules
    {
        /// <summary>
        /// 追加日志事件
        /// </summary>
        public event OnAppendSingileLog OnAppendSingileLog;

        public ParseNavigationRules(UrlListManager urlItem)
        {
            this._urlItem = urlItem;
        }

        /// <summary>
        /// 执行解析导航规则
        /// </summary>
        public void Exec()
        {
            StringCollection urls = new StringCollection();
            //解析起始地址            
            urls = ParseStartingUrl(this._urlItem.StartingUrlList, this._urlItem.PagedUrlPattern);

            //解析导航地址
            urls = ParseNavigationRuleItem(urls);

            //此时urls中保存着最终页面url地址
        }

        /// <summary>
        /// 根据导航规则和Html字符串解析出导航Url地址
        /// </summary>
        /// <param name="rule">导航规则</param>
        /// <param name="htmlText">Html文本</param>
        /// <returns>导航地址</returns>
        private StringCollection ParseNavigationRuleHtmlText(NavigationRule rule, string htmlText)
        {
            StringCollection urls = new StringCollection();

            //内容提取范围
            if (string.IsNullOrEmpty(rule.ExtractionStartFlag) && string.IsNullOrEmpty(rule.ExtractionEndFlag))
            {
                htmlText = Smart.Utility.StringHelper.SubString(htmlText, rule.ExtractionStartFlag, rule.ExtractionEndFlag);
            }

            //网址提取范围
            if (!string.IsNullOrEmpty(rule.PickingStartFlag) && !string.IsNullOrEmpty(rule.PickingEndFlag))
            {
                htmlText = Smart.Utility.StringHelper.SubString(htmlText, rule.PickingStartFlag, rule.PickingEndFlag);
            }

            //源文件替换
            foreach (Replacement r in rule.Replacements)
            {
                if (r.UseRegex)
                {
                    htmlText = Regex.Replace(htmlText, r.OldValue, r.NewValue);
                }
                else
                {
                    htmlText = htmlText.Replace(r.OldValue, r.NewValue);
                }
            }

            //使用正则表达式
            if (rule.UseRegularExpression)
            {
                //下一页网址模板
                MatchCollection coll = Regex.Matches(htmlText, rule.NextLayerUrlPattern);
                foreach (Match m in coll)
                {
                    urls.Add(m.Value);
                }
            }

            //提取下一页网址
            //循环采集
            //高级选项
            //不能与历史记录重复
            //优化历史记录

            return urls;
        }

        /// <summary>
        /// 解析导航规则
        /// </summary>
        /// <param name="startingUrl">起始地址</param>
        /// <returns>导航地址</returns>
        private StringCollection ParseNavigationRuleItem(StringCollection startingUrl)
        {
            StringCollection urls = new StringCollection();
            foreach (string u in startingUrl)
            {
                foreach (NavigationRule rule in _urlItem.NavigationRules)
                {
                    /*
                     * 描述:
                     * 加入最终页面地址
                     * 
                     * 步骤:
                     * 1.判断是否终端页面地址，如果是则直接加入并引发事件.
                     * 2.否则,请求web服务器并返回html文本,根据导航规则解析出终端页面地址.
                     * 
                     * 修改标志:王亚 201204244
                     */
                    if (rule.Terminal)
                    {
                        urls.Add(u);                                //最终页面直接加入导航地址                        
                        if (onSingleComplete != null)
                        {
                            this.onSingleComplete(this, u);         //引发增加一条网址事件
                        }
                    }
                    else
                    {
                        try
                        {
                            HttpHelper http = new HttpHelper();
                            /*
                             *修改标志 20120601 王亚 解析导航地址时增加Http请求编码 
                             */
                            http._encoding = Encoding.GetEncoding(_urlItem.UrlEncoding);
                            string htmlText = http.RequestResult(u);    //发送Http请求获取导航地址
                            StringCollection navUrlItem = ParseNavigationRuleHtmlText(rule, htmlText);
                            foreach (string r in navUrlItem)
                            {
                                /*
                                 * 处理相对路径网址问题如：/html/gndy/jddy/20120425/37418.html
                                 * 如果不包含http://选项，则在相对路径前边加上主机地址。
                                 */
                                string path = r;
                                if (!r.Contains("http://") && r.Length > 0)
                                {
                                    path = r.Insert(0, "http://" + http.WebResponse.ResponseUri.Authority);
                                }
                                urls.Add(path);
                                if (onSingleComplete != null)
                                {
                                    this.onSingleComplete(this, path);    //引发增加一条网址事件
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (OnAppendSingileLog != null)
                            {
                                OnAppendSingileLog(this, new LogEventArgs(ex.Message));
                            }
                        }
                    }
                }
            }

            return urls;
        }

        /// <summary>
        /// 解析起始地址Url
        /// </summary>
        /// <param name="commonUrl">普通Url地址</param>
        /// <param name="templateUrl">模板Url地址(分页递增模式地址)</param>
        /// <returns></returns>
        private StringCollection ParseStartingUrl(List<string> commonUrl, List<PagedUrlPatterns> templateUrl)
        {
            StringCollection urlList = new StringCollection();

            //先加载列表地址
            foreach (string url in commonUrl)
            {
                urlList.Add(url);
            }

            //加载模板网址
            //匹配：{[0-9,-]*} {100,1,-1}            
            foreach (PagedUrlPatterns pageUrl in templateUrl)
            {
                MatchCollection regexMatch = Regex.Matches(pageUrl.PagedUrlPattern, "{[0-9,-]*}");
                if (pageUrl.Format == PagedUrlPatternsMode.Increment)
                { //递增模式
                    for (double i = pageUrl.StartPage; i <= pageUrl.EndPage; i += pageUrl.Step)
                    {
                        string url = pageUrl.PagedUrlPattern;
                        if (regexMatch.Count != 0)
                        {
                            url = url.Replace(regexMatch[0].Value, i.ToString());
                        }
                        urlList.Add(url);
                    }
                }
                else if (pageUrl.Format == PagedUrlPatternsMode.Decreasing)
                { //递减模式
                    for (double i = pageUrl.EndPage; i >= pageUrl.StartPage; i -= pageUrl.Step)
                    {
                        string url = pageUrl.PagedUrlPattern;
                        if (regexMatch.Count != 0)
                        {
                            url = url.Replace(regexMatch[0].Value, i.ToString());
                        }
                        urlList.Add(url);
                    }
                }
            }
            return urlList;
        }

        /// <summary>
        /// 当增加一个导航地址时发生的事件
        /// </summary>
        public event onSingleComplete onSingleComplete;

        /// <summary>
        /// url列表
        /// </summary>
        private UrlListManager _urlItem;
        /// <summary>
        /// Http请求助手
        /// </summary>
        private HttpHelper _HttpHelper;
    }
}
