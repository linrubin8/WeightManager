using LB.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls.Report
{
    public class ReportCommon
    {
        public void ReportControlEvent(ToolStrip toolStrip,long lReportTypeID)
        {


            foreach(ToolStripItem stripItem in toolStrip.Items)
            {
                if (stripItem is LBToolStripReportViewButton)
                {
                    LBToolStripReportViewButton btnReport = (LBToolStripReportViewButton)stripItem;
                    if (btnReport.ToolStripType == Controls.Helper.enToolStripType.ViewSingleButton)
                    {
                        ((LBToolStripReportViewButton)stripItem).Click += LBToolStripReportView_Click;
                    }
                    else if (btnReport.ToolStripType == Controls.Helper.enToolStripType.EditReportButton)
                    {
                        ((LBToolStripReportViewButton)stripItem).Click += LBToolStripReportEdit_Click;
                    }
                }
            }
        }

        private void LBToolStripReportView_Click(object sender, EventArgs e)
        {
            
        }

        private void LBToolStripReportEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //ReportArgs reportArgs = new ReportArgs((int)1, (DataView)this.grdMain.DataSource, null);
                //frmReport frm = new frmReport(reportArgs);
                //LBShowForm.ShowDialog(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        public void GetReportTempleteUserData()
        {

        }
    }
}
