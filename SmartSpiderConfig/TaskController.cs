namespace SmartSpider.Config {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Serialization;

    public class TaskController {
        #region 私有变量定义
        private Configuration _Configuration = new Configuration();
        private List<TaskUnit> _TaskUnit = new List<TaskUnit>();
        #endregion

        #region 公共属性定义
        /// <summary>
        /// 任务单元集合
        /// </summary>
        public List<TaskUnit> TaskUnit {
            get {
                return _TaskUnit;
            }
            set {
                _TaskUnit = value;
            }
        }

        /// <summary>
        /// 系统配置
        /// </summary>
        public Configuration Configuration {
            get {
                return _Configuration;
            }
            set {
                _Configuration = value;
            }
        }
        #endregion

        #region 公共方法定义
        public TaskController() {
        }

        /// <summary>
        /// 增加一个任务单元到任务控制器
        /// </summary>
        public void Add(TaskUnit task) {
            if (!this._TaskUnit.Contains(task)) {
                this._TaskUnit.Add(task);
            }
        }

        /// <summary>
        /// 移除一个任务单元
        /// </summary>
        /// <param name="task">任务单元</param>
        public void Remove(TaskUnit task) {
            this._TaskUnit.Remove(task);
        }

        /// <summary>
        /// 移除指定索引处的任务单元
        /// </summary>
        /// <param name="index">索引位置</param>
        public void Remove(int index) {
            this._TaskUnit.RemoveAt(index);
        }
        #endregion
    }
}
