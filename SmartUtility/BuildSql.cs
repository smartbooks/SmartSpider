
namespace Smart.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Text;

	/// <summary>
	/// 构造Sql语句(插入)
	/// </summary>
    public static class BuildSql
    {
        /// <summary>
        /// 通过参数和表明生成插入语句
        /// </summary>
        /// <param name="paraList">key=字段名，value= 字段值的字典</param>
        /// <param name="tableName">表名</param>
        /// <returns>Insert 语句</returns>
        public static StringBuilder BuildQueryString(Dictionary<string, string> paraList, string tableName)
        {
            StringBuilder insertString = new StringBuilder();
            insertString.Append("insert into ");
            insertString.Append(tableName);
            insertString.Append("( ");
            List<string> columnValue = new List<string>(paraList.Keys.Count);
            foreach (string key in paraList.Keys)
            {
                insertString.Append(key);
                insertString.Append(",");
                columnValue.Add(paraList[key]);
            }
            //去掉末尾的","
            insertString.Remove(insertString.Length - 1, 1);
            insertString.Append(" ) values( ");
            foreach (string value in columnValue)
            {
                if (value.StartsWith("to_date('"))
                {
                    //如果是日期类型的不加"'"
                    insertString.Append(value);
                    insertString.Append(",");
                }
                else
                {
                    insertString.Append("'" + value + "'");
                    insertString.Append(",");
                }
            }
            //去掉末尾的","
            insertString.Remove(insertString.Length - 1, 1);
            insertString.Append(" ) ");
            return insertString;
        }
    }
}
