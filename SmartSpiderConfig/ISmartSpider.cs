using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSpider.Config {
    public interface ISmartSpider {
        event OnAppendSingileLog OnAppendSingileLog;
    
        void Create(string taskPath, string pluginPath, SmartSpiderInformation smartSpiderInfo, Action action, bool firstCall);

        void Dispose(Action action);

        void DownloadContentFile(string url, string path, string skipIfFileExisted, string cookie, string referer);

        string DownloadSingleFile(string url, string path, string fileNamePrefix, string skipIfFileExisted, string cookie, string referer);

        string ExtractResult(string extractionRule, string dataColumn, string htmlText, string url);

        bool Filter(string result, string extractionRule, string dataColumn, System.Data.DataRow extractingResultRow);

        RequiredOptions GetRequiredOptions();

        void GetSettingForm(string taskPath, string[] selectedTaskPaths, string pluginPath, SmartSpiderInformation smartSpiderInfo);

        SmartSpiderWebProxy GetWebProxy(string requestingUrl, string retryTimes);

        string LoadStartingUrl(string template, ref int position, string cookie);

        string Login(string url);

        System.Collections.Specialized.StringCollection PickNextLayerUrls(string htmlText, string layer, string url, string cookie);

        string PickNextPageUrl(string htmlText, string layer, string url, string cookie);

        void ProcessContentFile(string path, bool skipped);

        bool ProcessResultRow(System.Data.DataRow extractedResultRow);

        string ProcessSingleFile(string path, string fileNamePrefix, bool skipped);

        string Visit(string url, string[] postData, string layer, string cookie, string referer);
    }
}
