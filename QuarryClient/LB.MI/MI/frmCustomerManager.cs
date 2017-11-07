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

namespace LB.MI
{
    public partial class frmCustomerManager : LBUIPageBase
    {
        public frmCustomerManager()
        {
            InitializeComponent();
            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;
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
            this.txtCustomerID.TextBox.IsAllowNotExists = true;

            DataTable dtReceiveType = LB.Common.LBConst.GetConstData("ReceiveType");//收款方式
            dtReceiveType.Rows.InsertAt(dtReceiveType.NewRow(),0);
            this.txtReceiveType.DataSource = dtReceiveType;
            this.txtReceiveType.DisplayMember = "ConstText";
            this.txtReceiveType.ValueMember = "ConstValue";

            LoadDataSource();
        }

        private void LoadDataSource()
        {
            string strFilter = "";

            if (this.txtCustomerID.TextBox.Text != "")
            {
                if (strFilter != "")
                    strFilter += " and ";
                strFilter += "CustomerName like '%"+ this.txtCustomerID.TextBox.Text + "%'";
            }

            if (this.txtAmountFrom.Text != "")
            {
                if (strFilter != "")
                    strFilter += " and ";
                strFilter += "RemainReceivedAmount >= " + this.txtAmountFrom.Text + "";
            }

            if (this.txtAmountTo.Text != "")
            {
                if (strFilter != "")
                    strFilter += " and ";
                strFilter += "RemainReceivedAmount <= " + this.txtAmountTo.Text + "";
            }

            if (this.txtReceiveType.SelectedValue!=null&& this.txtReceiveType.SelectedValue.ToString()!="")
            {
                if (strFilter != "")
                    strFilter += " and ";
                strFilter += "ReceiveType = " + LBConverter.ToInt32(this.txtReceiveType.SelectedValue) + "";
            }

            DataTable dtUser = ExecuteSQL.CallView(112, "", strFilter, "");
            this.grdMain.DataSource = dtUser.DefaultView;
        }

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataRowView drvSelect = this.grdMain.Rows[e.RowIndex].DataBoundItem as DataRowView;
                    long lCustomerID = drvSelect["CustomerID"] == DBNull.Value ?
                        0 : Convert.ToInt64(drvSelect["CustomerID"]);
                    if (lCustomerID > 0)
                    {
                        frmCustomerEdit frm = new frmCustomerEdit(lCustomerID);
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

        #region -- 添加用户 --

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustomerEdit frmEdit = new frmCustomerEdit(0);
                LBShowForm.ShowDialog(frmEdit);
                LoadDataSource();
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

        private void btnOpenEdit_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        #endregion -- 添加用户 --

        #region -- 报表 --

        protected override void OnInitToolStripControl(ToolStripReportArgs args)
        {
            args.LBToolStrip = skinToolStrip1;
            args.ReportTypeID = 3;//客户管理
            base.OnInitToolStripControl(args);

        }

        protected override void OnReportRequest(ReportRequestArgs args)
        {
            base.OnReportRequest(args);
            DataTable dtSource = ((DataView)this.grdMain.DataSource).Table.Copy();
            dtSource.TableName = "T003";
            DataSet dsSource = new DataSet("Report");
            dsSource.Tables.Add(dtSource);
            args.DSDataSource = dsSource;
        }

        #endregion

        private void txtCustomerID_Load(object sender, EventArgs e)
        {

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

                LBExportExcel.ExcelExport(dtExport, "客户清单");
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

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
