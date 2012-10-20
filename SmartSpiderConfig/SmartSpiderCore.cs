using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSpider.Config {
    public class SmartSpiderCore : ISmartSpider {
        /// <summary>
        /// 日志记录事件
        /// </summary>
        public event OnAppendSingileLog OnAppendSingileLog;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="taskPath">Task配置文件路径</param>
        /// <param name="pluginPath">插件配置路径</param>
        /// <param name="smartSpiderInfo">客户端信息(验证用)</param>
        /// <param name="sender">任务状态</param>
        /// <param name="firstCall">完成后是否回调</param>
        public void Create(string taskPath, string pluginPath, SmartSpiderInformation smartSpiderInfo, Action action, bool firstCall) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="sender">任务状态</param>
        public void Dispose(Action action) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 下载内容文件
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="path">存放路径</param>
        /// <param name="skipIfFileExisted">文件存在则跳过</param>
        /// <param name="cookie">Cookie信息</param>
        /// <param name="referer">Referer地址</param>
        public void DownloadContentFile(string url, string path, string skipIfFileExisted, string cookie, string referer) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 下载单个文件
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="path">保存路径</param>
        /// <param name="fileNamePrefix">文件名前缀</param>
        /// <param name="skipIfFileExisted">文件存在则跳过</param>
        /// <param name="cookie">cookie</param>
        /// <param name="referer">referer地址</param>
        public string DownloadSingleFile(string url, string path, string fileNamePrefix, string skipIfFileExisted, string cookie, string referer) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 提取结果
        /// </summary>
        /// <param name="extractionRule">提取规则</param>
        /// <param name="dataColumn">数据列</param>
        /// <param name="htmlText">Html文本</param>
        /// <param name="url">Url地址</param>
        public string ExtractResult(string extractionRule, string dataColumn, string htmlText, string url) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 过滤结果
        /// </summary>
        /// <param name="result">提取结果</param>
        /// <param name="extractionRule">过滤规则</param>
        /// <param name="dataColumn">数据列</param>
        /// <param name="extractingResultRow">提取结果行</param>
        public bool Filter(string result, string extractionRule, string dataColumn, System.Data.DataRow extractingResultRow) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获得所需的选项
        /// </summary>
        public RequiredOptions GetRequiredOptions() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获得设置插件设置
        /// </summary>
        /// <param name="taskPath">任务配置文件路径</param>
        /// <param name="selectedTaskPaths">选择任务路径</param>
        /// <param name="pluginPath">插件路径</param>
        /// <param name="smartSpiderInfo">客户端信息</param>
        public void GetSettingForm(string taskPath, string[] selectedTaskPaths, string pluginPath, SmartSpiderInformation smartSpiderInfo) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取Web代理
        /// </summary>
        /// <param name="requestingUrl">请求URL</param>
        /// <param name="retryTimes">重试次数</param>
        public SmartSpiderWebProxy GetWebProxy(string requestingUrl, string retryTimes) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 加载起始Url
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="position">位置</param>
        /// <param name="cookie">Cookie</param>
        public string LoadStartingUrl(string template, ref int position, string cookie) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="url">Url登录地址</param>
        public string Login(string url) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 提取下一层网址
        /// </summary>
        /// <param name="htmlText">Html文本</param>
        /// <param name="layer">页面层次</param>
        /// <param name="url">Url地址</param>
        /// <param name="cookie">Cookie</param>
        public System.Collections.Specialized.StringCollection PickNextLayerUrls(string htmlText, string layer, string url, string cookie) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 提取下一页网址
        /// </summary>
        /// <param name="htmlText">Html文本</param>
        /// <param name="layer">页面层次</param>
        /// <param name="url">Url地址</param>
        /// <param name="cookie">Cookie</param>
        public string PickNextPageUrl(string htmlText, string layer, string url, string cookie) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 处理内容文件
        /// </summary>
        /// <param name="path">保存路径</param>
        /// <param name="skipped">是否跳过</param>
        public void ProcessContentFile(string path, bool skipped) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 处理结果行
        /// </summary>
        /// <param name="extractedResultRow">提取结果航</param>
        public bool ProcessResultRow(System.Data.DataRow extractedResultRow) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 处理单个文件
        /// </summary>
        /// <param name="path">保存路径</param>
        /// <param name="fileNamePrefix">文件名前缀</param>
        /// <param name="skipped">跳过</param>
        public string ProcessSingleFile(string path, string fileNamePrefix, bool skipped) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 访问
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="postData">post数据</param>
        /// <param name="layer">页面层次</param>
        /// <param name="cookie">cookie</param>
        /// <param name="referer">referer地址</param>
        public string Visit(string url, string[] postData, string layer, string cookie, string referer) {
            throw new NotImplementedException();
        }
    }
}
