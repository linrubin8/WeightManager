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
using LB.Page.Helper;
using LB.Controls.Args;
using LB.Controls.Report;
using LB.Common;

namespace LB.RPReceive.RPReceive
{
    public partial class frmReceiveBillQuery : LBUIPageBase
    {
        public frmReceiveBillQuery()
        {
            InitializeComponent();
            this.grdMain.LBLoadConst();
            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;
        }

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataRowView drvSelect = this.grdMain.Rows[e.RowIndex].DataBoundItem as DataRowView;
                    long lReceiveBillHeaderID = drvSelect["ReceiveBillHeaderID"] == DBNull.Value ?
                        0 : Convert.ToInt64(drvSelect["ReceiveBillHeaderID"]);
                    if (lReceiveBillHeaderID > 0)
                    {
                        frmEditReceiveBill frm = new frmEditReceiveBill(lReceiveBillHeaderID);
                        LBShowForm.ShowDialog(frm);

                        LoadDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitData();
            LoadDataSource();
        }

        private void InitData()
        {
            DataTable dtCustom = ExecuteSQL.CallView(110, "", "", "SortLevel desc,CustomerName asc");
            this.txtCustomerID.TextBox.LBViewType = 110;
            this.txtCustomerID.TextBox.LBSort = "SortLevel desc,CustomerName asc";
            this.txtCustomerID.TextBox.IDColumnName = "CustomerID";
            this.txtCustomerID.TextBox.TextColumnName = "CustomerName";
            this.txtCustomerID.TextBox.PopDataSource = dtCustom.DefaultView;
            
            DataTable dtCharge = ExecuteSQL.CallView(138, "", "", "ChargeTypeID asc");
            dtCharge.Rows.InsertAt(dtCharge.NewRow(), 0);
            this.txtRPReceiveType.TextBox.LBSort = "ChargeTypeName asc";
            this.txtRPReceiveType.TextBox.IDColumnName = "ChargeTypeID";
            this.txtRPReceiveType.TextBox.TextColumnName = "ChargeTypeName";
            this.txtRPReceiveType.TextBox.PopDataSource = dtCharge.DefaultView;

            this.txtBillTimeFrom.Text = "00:00:00";
            this.txtBillTimeTo.Text = "23:59:59";
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

            if(LBConverter.ToInt32(this.txtRPReceiveType.TextBox.SelectedItemID)>0)
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "ChargeTypeID = " + this.txtRPReceiveType.TextBox.SelectedItemID.ToString();
            }

            if (this.txtBillCoding.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "ReceiveBillCode like '%" + this.txtBillCoding.Text + "%'";
            }
            if (this.txtApproveBy.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "ApproveBy like '%" + this.txtApproveBy.Text + "%'";
            }

            if (this.txtAmountFrom.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "ReceiveAmount >= " + this.txtAmountFrom.Text;
            }

            if (this.txtAmountTo.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "ReceiveAmount <= " + this.txtAmountTo.Text;
            }

            if (this.txtBillDateFrom.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "BillDate >= '" + this.txtBillDateFrom.Text + " " + this.txtBillTimeFrom.Text + "'";
            }

            if (this.txtBillDateTo.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "BillDate <= '" + this.txtBillDateTo.Text + " " + this.txtBillTimeTo.Text + "'";
            }

            if (rbCanceled.Checked)//已作废
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "IsCancel =1";
            }
            if (rbUnCancel.Checked)//未作废
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "isnull(IsCancel,0) =0";
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
                strFilter += "isnull(IsApprove,0)=0";
            }

            DataTable dtUser = ExecuteSQL.CallView(111,"", strFilter, "ApproveTime asc");
            this.grdMain.DataSource = dtUser.DefaultView;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmEditReceiveBill frm = new LB.RPReceive.frmEditReceiveBill(0);
                LBShowForm.ShowDialog(frm);

                LoadDataSource();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

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

        #region -- 报表 --

        protected override void OnInitToolStripControl(ToolStripReportArgs args)
        {
            args.LBToolStrip = skinToolStrip1;
            args.ReportTypeID = 2;//充值管理
            base.OnInitToolStripControl(args);

        }

        protected override void OnReportRequest(ReportRequestArgs args)
        {
            base.OnReportRequest(args);
            DataTable dtSource = ((DataView)this.grdMain.DataSource).Table.Copy();
            dtSource.TableName = "T002";
            DataSet dsSource = new DataSet("Report");
            dsSource.Tables.Add(dtSource);
            args.DSDataSource = dsSource;
        }

        #endregion

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
    }
}
