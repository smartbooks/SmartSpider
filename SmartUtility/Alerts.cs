
namespace Smart.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI;


    /// <summary>
    /// 一些常用的Js调用
    /// </summary>
    public class Alerts
    {
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static void Extjs(string message)
        {
            #region
            string js = @"<Script language='JavaScript'>" + message + "</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static void Alert(string message)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        public static void RunFunction(string CallfunctionCode) {
            string js = @"<Script language='JavaScript'>" +
                CallfunctionCode + "</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toURL">连接地址</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            #region
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
            #endregion
        }

        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
            #endregion
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            #region
            string js = @"<Script language='JavaScript'>
                    parent.opener=null;window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
            #endregion
        }

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        public static void RefreshParent(string url)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    window.opener.location.href='" + url + "';window.close();</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener()
        {
            #region
            string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        /// <param name="top">头位置</param>
        /// <param name="left">左位置</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            #region
            string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";

            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// 转向Url制定的页面
        /// </summary>
        /// <param name="url">连接地址</param>
        public static void JavaScriptLocationHref(string url)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="top">距离上位置</param>
        /// <param name="left">距离左位置</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            #region
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
            #endregion
        }

        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
        }

        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            #region
            string js = @"<script language=javascript>							
							showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
            return js;
            #endregion
        }

        /// <summary>
        /// 返回错误信息格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string errinfo(string[] s)
        {
            string str = "<ul>";
            for (int i = 0; i < s.Length; i++)
            {
                str = str + "<li>" + s[i] + "</li>";
            }
            str = str + "</ul>";
            return str;
        }

        /// <summary>
        /// 返回错误信息格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string errinfo(string s)
        {
            return "<ul><li>" + s + "</li></ul>";
        }

        #region 20120514 add
        /// <summary>
        /// Ajax弹出消息
        /// </summary>
        /// <param name="message">弹出消息</param>
        /// <param name="control">控件名称(UpdatePanel,在User Control(*.ascx)中用this)</param>
        public static void AjaxAlert(string message, Control control)
        {
            ScriptManager.RegisterStartupScript(control, control.GetType(), null, "alert('" + message + "');", true);
        }
        /// <summary>
        ///  Ajax弹出消息后,跳转到另一个页面(同一个窗口),如果要跳转的页面是当前页面,则刷新当前页面
        /// </summary>
        /// <param name="message">弹出消息</param>
        /// <param name="gopage">要跳转到的页面(test.aspx)</param>
        /// <param name="control">控件名称(UpdatePanel,在User Control(*.ascx)中用this)</param>
        public static void AjaxAlertGoPage(string message, string gopage, Control control)
        {
            string strScript = "alert('" + message + "');window.window.location.href='" + gopage + "';";
            ScriptManager.RegisterStartupScript(control, control.GetType(), null, strScript, true);
        }
        /// <summary>
        /// 向页面注册javascript
        /// </summary>
        /// <param name="strScript">javascript语句</param>
        /// <param name="control">控件名称(UpdatePanel,在User Control(*.ascx)中用this)</param>
        public static void AjaxRegisterScript(string strScript, Control control)
        {
            ScriptManager.RegisterStartupScript(control, control.GetType(), null, strScript, true);
        }
        /// <summary>
        /// alert弹出消息
        /// </summary>
        /// <param name="message">弹出消息</param>
        /// <param name="page">this</param>
        public static void Alert(string message, Page page)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), null, "alert('" + message + "');", true);
        }
        /// <summary>
        /// alert弹出消息后,跳转到另一个页面(同一个窗口),如果要跳转的页面是当前页面,则刷新当前页面
        /// </summary>
        /// <param name="message">弹出消息</param>
        /// <param name="gopage">要跳转到的页面(test.aspx)</param>
        /// <param name="page">this</param>
        public static void AlertGoPage(string message, string gopage, Page page)
        {
            string strScript = "alert('" + message + "');window.window.location.href='" + gopage + "';";
            page.ClientScript.RegisterStartupScript(page.GetType(), null, strScript, true);
        }
        /// <summary>
        /// 向页面注册javascript
        /// </summary>
        /// <param name="strScript">javascript语句</param>
        /// <param name="page">this</param>
        public static void RegisterScript(string strScript, Page page)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), null, strScript, true);
        }
        #endregion

    }
}
