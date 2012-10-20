using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSpider.Config {
    /// <summary>
    /// Html标记字典类
    /// 用于采集结果Html保留标记正则替换后重新组合文章
    /// </summary>
    public class HtmlMarkDictionary :IComparer<HtmlMarkDictionary> {
        private int _Index = 0;
        private string _Text = "";
        /// <summary>
        /// 位置索引
        /// </summary>
        public int Index {
            get {
                return _Index;
            }
            set {
                _Index = value;
            }
        }

        /// <summary>
        /// Html文本
        /// </summary>
        public string Text {
            get {
                return _Text;
            }
            set {
                _Text = value;
            }
        }
        
        int IComparer<HtmlMarkDictionary>.Compare(HtmlMarkDictionary x, HtmlMarkDictionary y) {
            if (x.Index < y.Index) {
                return x.Index;
            } else {
                return y.Index;
            }
        }
    }
}
