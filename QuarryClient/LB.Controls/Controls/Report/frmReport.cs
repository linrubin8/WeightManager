using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using LB.Page.Helper;
using LB.WinFunction;

namespace LB.Controls.Report
{
    public partial class frmReport : LBUIPageBase
    {
        private ReportRequestArgs mReportArgs;
        public frmReport(ReportRequestArgs reportArgs)
        {
            InitializeComponent();
            mReportArgs = reportArgs;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadReportData();//加载报表模板数据
        }

        private void LoadReportData()
        {
            DataTable dtUser = ReportHelper.GetReportTemplateRowByType(mReportArgs.ReportTypeID);

            this.grdMain.DataSource = dtUser.DefaultView;
        }

        private void btnAddReport_Click(object sender, EventArgs e)
        {
            try
            {
                frmEditReport frm = new frmEditReport( mReportArgs);
                mReportArgs.ReportTemplateID = 0;
                LBShowForm.ShowDialog(frm);

                LoadReportData();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnEditReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.grdMain.SelectedCells.Count > 0)
                {
                    DataGridViewRow dgSelected = this.grdMain.Rows[this.grdMain.SelectedCells[0].RowIndex];
                    long lReportTemplateID = Convert.ToInt64(dgSelected.Cells["ReportTemplateID"].Value);
                    mReportArgs.ReportTemplateID = lReportTemplateID;
                    frmEditReport frm = new frmEditReport( mReportArgs);
                    LBShowForm.ShowDialog(frm);

                    LoadReportData();
                }
                else
                {
                    throw new Exception("请选择需要编辑的数据行！");
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnDesignerReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.grdMain.SelectedCells.Count > 0)
                {
                    DataGridViewRow dgSelected = this.grdMain.Rows[this.grdMain.SelectedCells[0].RowIndex];
                    long lReportTemplateID = Convert.ToInt64(dgSelected.Cells["ReportTemplateID"].Value);
                    mReportArgs.ReportTemplateID = lReportTemplateID;
                    ReportHelper.OpenReportDesign( mReportArgs);
                }
                else
                {
                    throw new Exception("请选择需要编辑的数据行！");
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnReLoadParm_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.grdMain.SelectedCells.Count > 0)
                {
                    DataGridViewRow dgSelected = this.grdMain.Rows[this.grdMain.SelectedCells[0].RowIndex];
                    long lReportTemplateID = Convert.ToInt64(dgSelected.Cells["ReportTemplateID"].Value);
                    ReportHelper.ResetLocalReport(lReportTemplateID, mReportArgs.DSDataSource, mReportArgs.RecordDR);
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("报表数据源更新完毕！");
                }
                else
                {
                    throw new Exception("请选择需要编辑的数据行！");
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
