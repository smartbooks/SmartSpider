namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 插件所需的选项
    /// </summary>
    public enum RequiredOptions {
        /// <summary>
        /// 0.NULL无状态
        /// </summary>
        None = 0,
        /// <summary>
        /// 1.加载起始URL
        /// </summary>
        LoadStartingUrl = 2,
        /// <summary>
        /// 2.登录
        /// </summary>
        Login = 4,
        /// <summary>
        /// 3.访问
        /// </summary>
        Visit = 8,
        /// <summary>
        /// 4.提取下一层网址
        /// </summary>
        PickNextLayerUrls = 16,
        /// <summary>
        /// 5.提取下一页网址
        /// </summary>
        PickNextPageUrl = 32,
        /// <summary>
        /// 6.获取Web代理
        /// </summary>
        GetWebProxy = 64,
        /// <summary>
        /// 7.提取结果
        /// </summary>
        ExtractResult = 128,
        /// <summary>
        /// 8.过滤
        /// </summary>
        Filter = 256,
        /// <summary>
        /// 9.处理结果行
        /// </summary>
        ProcessResultRow = 512,
        /// <summary>
        /// 10.下载内容文件
        /// </summary>
        DownloadContentFile = 1024,
        /// <summary>
        /// 11.处理内容文件
        /// </summary>
        ProcessContentFile = 2048,
        /// <summary>
        /// 12.下载单个文件
        /// </summary>
        DownloadSingleFile = 4096,
        /// <summary>
        /// 13.处理单个文件
        /// </summary>
        ProcessSingleFile = 8192,        
    }
}
