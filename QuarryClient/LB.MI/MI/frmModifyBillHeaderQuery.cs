using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using LB.WinFunction;
using LB.Controls.Args;
using LB.Controls.Report;
using LB.Page.Helper;
using LB.Common;
using static LB.Common.Args.LBQueryFilterArgs;
using LB.Common.Args;
using System.IO;

namespace LB.MI.MI
{
    public partial class frmModifyBillHeaderQuery : LBUIPageBase
    {
        public event GetCustomFilterEventHandle GetCustomFilterEvent;
        public frmModifyBillHeaderQuery()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DataTable dtCustom = ExecuteSQL.CallView(110, "", "", "SortLevel desc,CustomerName asc");
            this.txtCustomerID.TextBox.LBViewType = 110;
            this.txtCustomerID.TextBox.LBSort = "SortLevel desc,CustomerName asc";
            this.txtCustomerID.TextBox.IDColumnName = "CustomerID";
            this.txtCustomerID.TextBox.TextColumnName = "CustomerName";
            this.txtCustomerID.TextBox.PopDataSource = dtCustom.DefaultView;

            DataTable dtItem = ExecuteSQL.CallView(203);
            this.txtItemID.TextBox.LBViewType = 203;
            this.txtItemID.TextBox.IDColumnName = "ItemID";
            this.txtItemID.TextBox.TextColumnName = "ItemName";
            this.txtItemID.TextBox.PopDataSource = dtItem.DefaultView;


            LoadDataSource();

            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;
        }

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    DataRowView drv = this.grdMain.Rows[e.RowIndex].DataBoundItem as DataRowView;
                    long lModifyBillHeaderID = LBConverter.ToInt64(drv["ModifyBillHeaderID"]);
                    if (lModifyBillHeaderID > 0)
                    {
                        frmModifyBillHeaderEdit frmEdit = new MI.frmModifyBillHeaderEdit(lModifyBillHeaderID);
                        frmEdit.PageAutoSize = true;
                        LBShowForm.ShowDialog(frmEdit);
                    }
                }
                LoadDataSource();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void LoadDataSource()
        {
            string strFilter = "";

            if (this.txtCustomerID.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "CustomerName like '%" + this.txtCustomerID.Text + "%'";
            }

            if (this.txtItemID.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "ItemName like '%" + this.txtItemID.Text + "%'";
            }

            if (rbApproved.Checked)//已审核
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "isnull(IsApprove,0) =1";
            }
            if (rbUnApprove.Checked)//未审核
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "isnull(IsApprove,0) <>1";
            }

            DataTable dtUser = ExecuteSQL.CallView(114, "", strFilter, "");
            this.grdMain.DataSource = dtUser.DefaultView;

            this.grdMain.HiddenSaveColumnValue("CustomerName", "ModifyBillCode", "BillDate", "EffectDate", "HeaderDescription", 
                "ApproveDate", "ApproveBy", "CreateBy", "CreateTime", "ChangeBy", "ChangeTime",
                "IsApprove", "IsCancel", "CancelBy", "CancelTime");
        }

        #region -- 报表 --

        protected override void OnInitToolStripControl(ToolStripReportArgs args)
        {
            args.LBToolStrip = skinToolStrip1;
            args.ReportTypeID = 4;//调价单序时簿
            base.OnInitToolStripControl(args);

        }

        protected override void OnReportRequest(ReportRequestArgs args)
        {
            base.OnReportRequest(args);
            DataTable dtSource = ((DataView)this.grdMain.DataSource).Table.Copy();
            dtSource.TableName = "T004";
            DataSet dsSource = new DataSet("Report");
            dsSource.Tables.Add(dtSource);
            args.DSDataSource = dsSource;
        }

        #endregion

        #region -- 按钮事件 --

        private void btnReflesh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDataSource();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmModifyBillHeaderEdit frm = new frmModifyBillHeaderEdit(0);
                frm.PageAutoSize = true;
                LBShowForm.ShowDialog(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- 按钮事件 --

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDataSource();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtExport = new DataTable("Result");
                foreach (DataGridViewColumn dgvc in this.grdMain.Columns)
                {
                    string strColumnName = dgvc.HeaderText;
                    dtExport.Columns.Add(strColumnName, dgvc.ValueType);
                }

                foreach (DataGridViewRow dgvr in this.grdMain.Rows)
                {
                    DataRow drNew = dtExport.NewRow();
                    DataRowView drvData = dgvr.DataBoundItem as DataRowView;
                    foreach (DataGridViewColumn dgvc in this.grdMain.Columns)
                    {
                        drNew[dgvc.HeaderText] = dgvr.Cells[dgvc.DataPropertyName].Value;
                    }
                    dtExport.Rows.Add(drNew);
                }

                LBExportExcel.ExcelExport(dtExport, "调价单清单");
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
