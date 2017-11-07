using LB.Controls.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Report.Report.Factory
{
    internal class Page_Report
    {
        public static void GetObject(PageEventArgs e)
        {
            int iPageType = e.PageType;
            if(iPageType<100 || iPageType > 200)
            {
                return;
            }

            switch (iPageType)
            {
                case LB.PageType.PageConstants.LBReport_frmReport:
                    //e.PageResult=new 
                    break;
            }
        }
    }
}
