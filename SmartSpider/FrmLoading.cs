namespace SmartSpider {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.IO;
    using System.Xml.Serialization;
    using Config;

    public partial class FrmLoading : Form {
        public FrmLoading() {
            InitializeComponent();
            CheckAppdomainPath();

            //Task t = new Task();
            //t.Name = "新浪新闻-国内";
            //t.Description = "新浪新闻采集";
            //t.UrlListManager.PagedUrlPattern.Add(new PagedUrlPatterns() {
            //    PagedUrlPattern = @"http://roll.news.sina.com.cn/news/gjxw/hqqw/index_{0,5,1}.shtml",
            //    Format = PagedUrlPatternsMode.Increment,
            //    StartPage = 0,
            //    EndPage = 5,
            //    Step = 1
            //});
            //t.UrlListManager.NavigationRules.Add(new NavigationRule() {
            //    Name = "列表页",
            //    NextLayerUrlPattern = @"http://[a-z.]*\.sina\.com\.cn/w[0-9a-zA-Z-_/]*\.[a-z]+",
            //    ExtractionStartFlag = "<html>",
            //    ExtractionEndFlag = "</html>"
            //});
            //t.ExtractionRules.Add(new ExtractionRule() {
            //    Name = "标题",
            //    DataColumn = "title",
            //    PreviousFlag = "<title>",
            //    FollowingFlag = "</title>"
            //});
            //t.ExtractionRules.Add(new ExtractionRule() {
            //    Name = "内容",
            //    DataColumn = "Content",
            //    PreviousFlag = @"<!-- 正文内容 begin -->",
            //    FollowingFlag = @"<!-- 分享 begin -->"
            //});
            //t.ExtractionRules.Add(new ExtractionRule() {
            //    Name = "网址",
            //    DataColumn = "Urls",
            //    UrlAsResult = true
            //});

            //XmlSerializer xs = new XmlSerializer(typeof(Task));
            //Stream readStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "task\\sin.xml", FileMode.Create, FileAccess.Write, FileShare.Write);
            //xs.Serialize(readStream, t);
            //readStream.Close();
            //readStream.Dispose();
        }

        private void FrmLoading_Load(object sender, EventArgs e) {
            this.timerLoading.Interval = 1800;
            this.timerLoading.Start();
        }

        private void timerLoading_Tick(object sender, EventArgs e) {
            if (this.Opacity % 1 == 0) {
                this.Opacity += 0.001;
            } else {
                this.timerLoading.Stop();
                this.Hide();
                FrmMain main = new FrmMain();
                main.Show();
            }
        }

        private void CheckAppdomainPath() {
            string taskPath = AppDomain.CurrentDomain.BaseDirectory + "Task";
            string configFile = AppDomain.CurrentDomain.BaseDirectory + "Configuration.xml";
            string htmlMarkFile = AppDomain.CurrentDomain.BaseDirectory + "HtmlMark.xml";

            //任务目录是否存在
            if (!Directory.Exists(taskPath)) {
                Directory.CreateDirectory(taskPath);
            }

            //配置文件是否存在
            if (!File.Exists(configFile)) {
                XmlSerializer xs = new XmlSerializer(typeof(Config.Configuration));
                Stream WriteStream = new FileStream(configFile, FileMode.Create, FileAccess.Write, FileShare.Read);
                xs.Serialize(WriteStream, new Config.Configuration());
                WriteStream.Close();
                WriteStream.Dispose();
            }

            //HtmlMark是否存在
            if (!File.Exists(htmlMarkFile)) {
                XmlSerializer xs = new XmlSerializer(typeof(List<Config.HtmlMark>));
                Stream WriteStream = new FileStream(htmlMarkFile, FileMode.Create, FileAccess.Write, FileShare.Read);
                xs.Serialize(WriteStream, new List<Config.HtmlMark>());
                WriteStream.Close();
                WriteStream.Dispose();
            }
        }
    }
}
