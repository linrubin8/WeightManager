using LB.Controls;
using LB.Controls.Args;
using System;
using System.Collections.Generic;
using System.Text;
using static LB.Controls.Args.PageEventArgs;

namespace LB.Page.Helper
{
    public class LBShowForm
    {
        public static event EventHandler LBUIPageBaseAdded;
        /// <summary>
        /// 弹出页面
        /// </summary>
        /// <param name="page"></param>
        public static void ShowDialog(LBUIPageBase page)
        {
            LBForm form = new Controls.LBForm(page);
            form.ShowDialog();
        }

        /// <summary>
        /// 弹出页面
        /// </summary>
        /// <param name="page"></param>
        public static void Show(LBUIPageBase page,bool bolIsTopLevel)
        {
            LBForm form = new Controls.LBForm(page);
            form.TopLevel = bolIsTopLevel;
            form.Show();
        }

        /// <summary>
        /// 不弹出界面，在主界面显示
        /// </summary>
        /// <param name="page"></param>
        public static void ShowMainPage(LBUIPageBase page)
        {
            LBUIPageBaseAdded?.Invoke(page, null);
        }

        public static event GetPageEventHandle GetPageEvent;
        /// <summary>
        /// 不弹出界面，在主界面显示
        /// </summary>
        /// <param name="page"></param>
        public static LBUIPageBase GetPageByType(int PageTypeID,Dictionary<string,object> pageParm)
        {
            PageEventArgs args = new Controls.Args.PageEventArgs(PageTypeID, pageParm);
            GetPageEvent(args);
            LBUIPageBase page = args.PageResult;
            if(page== null)
            {
                throw new Exception("页面好["+PageTypeID+"]不存在！");
            }
            return page;
        }
    }
}
