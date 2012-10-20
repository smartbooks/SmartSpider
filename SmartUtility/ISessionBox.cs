using System;
using System.Collections.Generic;
using System.Text;
using System.Web.SessionState;

namespace Smart.Utility {
    public interface ISessionBox {

        /// <summary>
        /// 检查Session是否有效
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <param name="value">Session值</param>
        /// <returns>结果：true有效，false无效</returns>
        bool Checked(string name, object value);

        /// <summary>
        /// 增加一个Session
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <param name="value">Session值</param>
        /// <returns>结果：true成功，false失败</returns>
        bool Add(string name, object value);

        /// <summary>
        /// 移除一个Session
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <returns>结果：true成功，false失败</returns>
        bool Remove(string name);

        /// <summary>
        /// 更新一个Session
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <param name="value">Session值</param>
        /// <returns>结果：true成功，false失败</returns>
        bool Update(string name, object value);

        /// <summary>
        /// 获取一个Session
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <returns>Session Object值对象</returns>
        object Get(string name);
    }
}
