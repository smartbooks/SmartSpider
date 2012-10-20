

namespace SmartSpider {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using Config;

    partial class FrmMain {
        /// <summary>
        /// 设置任务状态UI
        /// </summary>
        /// <param name="status">任务状态</param>
        private void SetTaskStatusUi(Config.Action status) {
            #region 根据任务状态设置UI
            this.tolAddTask.Enabled = true;
            this.tolEditTask.Enabled = true;
            this.tolDeleteTask.Enabled = true;
            switch (status) {
                case Config.Action.Finish:
                case Config.Action.Ready:
                case Config.Action.Stop:   //停止状态
                    this.tolStartTask.Enabled = true;
                    this.tolPauseTask.Enabled = false;
                    this.tolStopTask.Enabled = false;
                    break;
                case Config.Action.Start:  //运行状态
                    this.tolStartTask.Enabled = false;
                    this.tolPauseTask.Enabled = true;
                    this.tolStopTask.Enabled = true;

                    this.tolEditTask.Enabled = false;
                    this.tolDeleteTask.Enabled = false;
                    break;
                case Config.Action.Pause:  //暂停状态
                    this.tolStartTask.Enabled = true;
                    this.tolPauseTask.Enabled = false;
                    this.tolStopTask.Enabled = true;
                    break;
            }
            #endregion
        }

        /// <summary>
        /// 初始化主窗体为默认UI
        /// </summary>
        private void SetDefaultUI() {
            #region 工具栏
            this.tolStartTask.Enabled = false;  //开始任务
            this.tolStopTask.Enabled = false;   //停止任务
            this.tolPauseTask.Enabled = false;  //暂停/继续任务

            this.tolAddTask.Enabled = true; //新建任务
            this.tolEditTask.Enabled = false;   //编辑任务
            this.tolDeleteTask.Enabled = false; //删除任务

            this.tolExportTo.Enabled = true;   //导出结果

            this.tolAllTaskSuccessShutdown.Enabled = true; //所有任务完成后关机
            this.TolDisableAllTimingAcquisitionTask.Enabled = true;    //禁用定时采集
            this.tolOption.Enabled = true;  //选项

            this.TolOnLineHelp.Enabled = true;  //在线帮助
            this.TolAboutUS.Enabled = true; //关于我们
            this.TolExit.Enabled = true;   //退出
            #endregion

            #region 菜单栏
            #region 文件菜单
            this.FileItemPublishResults.Enabled = true;
            this.FileItemPublishResultErrorClear.Enabled = true;   //发布结果
            this.FileItemSaveAsTo.Enabled = false;  //将结果另存为
            this.FileItemViewResult.Enabled = false;    //查看结果
            this.FileItemClearResult.Enabled = false;   //清空结果
            this.FileItemPublishResultRepeat.Enabled = false;   //发布时重复行
            this.FileItemPublishResultError.Enabled = false;    //发布时出错行
            this.FileItemHistory.Enabled = false;   //历史记录
            this.FileItemTaskLog.Enabled = false;   //任务日志
            this.FileItemExit.Enabled = true;   //退出
            #endregion

            #region 查看菜单
            this.ViewItemShowToolBar.Enabled = true;    //工具栏
            this.ViewItemShowFloatFrom.Enabled = true;  //浮动窗
            #endregion

            #region 文件夹菜单
            this.FolderItemAdd.Enabled = true;  //新建
            this.FolderItemRename.Enabled = false;  //重命名
            this.FolderItemDelete.Enabled = false;  //删除
            this.FolderItemRefresh.Enabled = false; //刷新
            this.FolderItemExport.Enabled = false;  //导出
            #endregion

            #region 任务菜单
            this.TaskItemShowInfo.Enabled = false;  //显示运行信息
            this.TaskItemStart.Enabled = false; //开始
            this.TaskItemSpace.Enabled = false; //暂停
            this.TaskItemStop.Enabled = false;  //停止

            this.TaskItemAdd.Enabled = true;   //新建
            this.TaskItemEdit.Enabled = false;  //编辑
            this.TaskItemCopy.Enabled = false;  //复制
            this.TaskItemDelete.Enabled = false;    //删除
            this.TaskItemExport.Enabled = false;    //导出
            this.TaskItemImport.Enabled = true; //导入
            this.TaskItemSelectAll.Enabled = false; //全选
            #endregion

            #region 设置菜单
            this.configItemHtmlLable.Enabled = true;    //Html标记
            this.configItemRegex.Enabled = true;    //正则表达式
            this.configItemPreviewRulesName.Enabled = true; //预置规则名称
            this.configItemOption.Enabled = true;   //选项
            #endregion

            #region 工具菜单
            this.ToolItemSourceView.Enabled = true; //源文件查看器
            this.ToolItemRegesTest.Enabled = true;  //正则式测试器
            this.ToolItemUrlEncoding.Enabled = true;    //Url编码工具
            this.ToolItemTaskUpdate.Enabled = true; //任务升级器
            this.ToolItemOnLinePublish.Enabled = true;  //在线发布
            #endregion

            #region 帮助菜单
            this.HelpItemOnLineHelp.Enabled = true; //在线帮助
            this.HelpItemSite.Enabled = true;   //网站
            this.HelpItemBBS.Enabled = true;    //论坛
            this.HelpItemBuy.Enabled = true;    //购买
            this.HelpItemAboutUS.Enabled = true;    //关于我闷
            #endregion
            #endregion
        }

        /// <summary>
        /// 选择多个任务项UI界面
        /// </summary>
        private void SetSelectMultiTask() {
            #region 工具栏
            this.tolStartTask.Enabled = true;  //开始任务
            this.tolStopTask.Enabled = true;   //停止任务
            this.tolPauseTask.Enabled = true;  //暂停/继续任务

            this.tolAddTask.Enabled = true; //新建任务
            this.tolEditTask.Enabled = false;   //编辑任务
            this.tolDeleteTask.Enabled = true; //删除任务

            this.tolExportTo.Enabled = true;   //导出结果

            this.tolAllTaskSuccessShutdown.Enabled = true; //所有任务完成后关机
            this.TolDisableAllTimingAcquisitionTask.Enabled = true;    //禁用定时采集
            this.tolOption.Enabled = true;  //选项

            this.TolOnLineHelp.Enabled = true;  //在线帮助
            this.TolAboutUS.Enabled = true; //关于我们
            this.TolExit.Enabled = true;   //退出
            #endregion

            #region 菜单栏
            #region 文件菜单
            this.FileItemPublishResults.Enabled = true;
            this.FileItemPublishResultErrorClear.Enabled = true;   //发布结果
            this.FileItemSaveAsTo.Enabled = true;  //将结果另存为
            this.FileItemViewResult.Enabled = true;    //查看结果
            this.FileItemClearResult.Enabled = true;   //清空结果
            this.FileItemPublishResultRepeat.Enabled = true;   //发布时重复行
            this.FileItemPublishResultError.Enabled = true;    //发布时出错行
            this.FileItemHistory.Enabled = true;   //历史记录
            this.FileItemTaskLog.Enabled = true;   //任务日志
            this.FileItemExit.Enabled = true;   //退出
            #endregion

            #region 查看菜单
            this.ViewItemShowToolBar.Enabled = true;    //工具栏
            this.ViewItemShowFloatFrom.Enabled = true;  //浮动窗
            #endregion

            #region 文件夹菜单
            this.FolderItemAdd.Enabled = true;  //新建
            this.FolderItemRename.Enabled = false;  //重命名
            this.FolderItemDelete.Enabled = false;  //删除
            this.FolderItemRefresh.Enabled = true; //刷新
            this.FolderItemExport.Enabled = true;  //导出
            #endregion

            #region 任务菜单
            this.TaskItemShowInfo.Enabled = true;  //显示运行信息
            this.TaskItemStart.Enabled = true; //开始
            this.TaskItemSpace.Enabled = true; //暂停
            this.TaskItemStop.Enabled = true;  //停止

            this.TaskItemAdd.Enabled = true;   //新建
            this.TaskItemEdit.Enabled = false;  //编辑
            this.TaskItemCopy.Enabled = true;  //复制
            this.TaskItemDelete.Enabled = true;    //删除
            this.TaskItemExport.Enabled = true;    //导出
            this.TaskItemImport.Enabled = true; //导入
            this.TaskItemSelectAll.Enabled = true; //全选
            #endregion

            #region 设置菜单
            this.configItemHtmlLable.Enabled = true;    //Html标记
            this.configItemRegex.Enabled = true;    //正则表达式
            this.configItemPreviewRulesName.Enabled = true; //预置规则名称
            this.configItemOption.Enabled = true;   //选项
            #endregion

            #region 工具菜单
            this.ToolItemSourceView.Enabled = true; //源文件查看器
            this.ToolItemRegesTest.Enabled = true;  //正则式测试器
            this.ToolItemUrlEncoding.Enabled = true;    //Url编码工具
            this.ToolItemTaskUpdate.Enabled = true; //任务升级器
            this.ToolItemOnLinePublish.Enabled = true;  //在线发布
            #endregion

            #region 帮助菜单
            this.HelpItemOnLineHelp.Enabled = true; //在线帮助
            this.HelpItemSite.Enabled = true;   //网站
            this.HelpItemBBS.Enabled = true;    //论坛
            this.HelpItemBuy.Enabled = true;    //购买
            this.HelpItemAboutUS.Enabled = true;    //关于我闷
            #endregion
            #endregion
        }
    }
}