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
using LB.Common;
using LB.Page.Helper;
using LB.Controls.Args;
using LB.Controls.Report;
using System.IO;
using LB.Common.Synchronous;

namespace LB.MI.MI
{
    public partial class frmSaleCarInOutBillManagerSynK3 : LBUIPageBase
    {
        private int _SynType = 0;//0应收单同步 1出库单同步 2调拨单同步
        public frmSaleCarInOutBillManagerSynK3(int iSynType)
        {
            InitializeComponent();
            _SynType = iSynType;

            if (_SynType == 0)//应收,
            {
                this.LBPageTitle = "销售磅单同步至K3应收单";
                //隐藏出库列
                if (this.grdMain.Columns.Contains("SynchronousToTimeOutBill"))
                {
                    this.grdMain.Columns["SynchronousToTimeOutBill"].Visible = false;
                }
                if (this.grdMain.Columns.Contains("SynchronousK3OutBillResult"))
                {
                    this.grdMain.Columns["SynchronousK3OutBillResult"].Visible = false;
                }
            }
            else if (_SynType == 1)
            {
                this.LBPageTitle = "销售磅单同步至K3出库单";
                if (this.grdMain.Columns.Contains("SynchronousToTimeReceive"))
                {
                    this.grdMain.Columns["SynchronousToTimeReceive"].Visible = false;
                }
                if (this.grdMain.Columns.Contains("SynchronousK3ReceiveResult"))
                {
                    this.grdMain.Columns["SynchronousK3ReceiveResult"].Visible = false;
                }
                
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.grdMain.LBLoadConst();
            InitData();
            LoadAllSalesBill();//磅单清单

            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;

            this.grdMain.Columns["TotalWeight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void InitData()
        {
            //DataTable dtCustom = ExecuteSQL.CallView(110, "", "", "SortLevel desc,CustomerName asc");
            this.txtCustomerID.TextBox.LBViewType = 110;
            this.txtCustomerID.TextBox.LBSort = "SortLevel desc,CustomerName asc";
            this.txtCustomerID.TextBox.IDColumnName = "CustomerID";
            this.txtCustomerID.TextBox.TextColumnName = "CustomerName";
            //this.txtCustomerID.TextBox.PopDataSource = dtCustom.DefaultView;

            //DataTable dtCar = ExecuteSQL.CallView(113, "", "", "SortLevel desc,CarNum asc");
            this.txtCarID.TextBox.LBViewType = 113;
            this.txtCarID.TextBox.LBSort = "SortLevel desc,CarNum asc";
            this.txtCarID.TextBox.IDColumnName = "CarID";
            this.txtCarID.TextBox.TextColumnName = "CarNum";
            //this.txtCarID.TextBox.PopDataSource = dtCar.DefaultView;

            //DataTable dtItem = ExecuteSQL.CallView(203);
            this.txtItemID.TextBox.LBViewType = 203;
            this.txtItemID.TextBox.IDColumnName = "ItemID";
            this.txtItemID.TextBox.TextColumnName = "ItemName";
            //this.txtItemID.TextBox.PopDataSource = dtItem.DefaultView;
            
            this.txtOutBillTimeFrom.Text = "00:00:00";
            this.txtOutBillTimeTo.Text = "23:59:59";

            this.grdMain.Visible = true;
            this.grdMain.Dock = DockStyle.Fill;
        }

        #region -- 双击打开清单  --

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    long lSaleCarInBillID = LBConverter.ToInt64(this.grdMain["SaleCarInBillID", e.RowIndex].Value);
                    if (lSaleCarInBillID > 0)
                    {
                        frmSaleCarInOutEdit frmEdit = new frmSaleCarInOutEdit(lSaleCarInBillID);
                        LBShowForm.ShowDialog(frmEdit);
                        LoadAllSalesBill();//磅单清单
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- 双击打开清单  --

        #region -- 查询磅单清单 --

        private void LoadAllSalesBill()
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

            if (this.txtCarID.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "CarNum like '%" + this.txtCarID.Text + "%'";
            }

            if (this.txtItemID.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "ItemName like '%" + this.txtItemID.Text + "%'";
            }

            if (this.txtBillCode.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "SaleCarInBillCode like '%" + this.txtBillCode.Text + "%'";
            }


            if (this.txtBillCodeOut.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "SaleCarOutBillCode like '%" + this.txtBillCodeOut.Text + "%'";
            }

            if (this.txtOutBillCraeteBy.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "CreateByOut like '%" + this.txtOutBillCraeteBy.Text + "%'";
            }

            if (cbDisplaySynK3Bill.Checked)
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                if (_SynType == 0)
                {
                    strFilter += "IsSynchronousToK3Receive=1";
                }
                else if (_SynType == 1)
                {
                    strFilter += "IsSynchronousToK3OutBill=1";
                }
                //else if (_SynType == 2)
                //{
                //    strFilter += "IsSynchronousToK3OutBill=2";
                //}
            }
            else
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                if (_SynType == 0)
                {
                    strFilter += "isnull(IsSynchronousToK3Receive,0)=0";
                }
                else if (_SynType == 1)
                {
                    strFilter += "isnull(IsSynchronousToK3OutBill,0) = 0";
                }
                //else if (_SynType == 2)
                //{
                //    strFilter += "isnull(IsSynchronousToK3OutBill,0) = 0";
                //}
                //strFilter += "(isnull(IsSynchronousToK3OutBill,0) = 0 or isnull(IsSynchronousToK3Receive,0)=0)";
            }

            if (this.txtOutBillDateFrom.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "CreateTimeOut >= '" + this.txtOutBillDateFrom.Text + " " + this.txtOutBillTimeFrom.Text + "'";
            }

            if (this.txtOutBillDateTo.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "CreateTimeOut <= '" + this.txtOutBillDateTo.Text + " " + this.txtOutBillTimeTo.Text + "'";
            }

            if (strFilter != "")
            {
                strFilter += " and ";
            }
            strFilter += "SaleCarOutBillID is not null";

            if (strFilter != "")
            {
                strFilter += " and ";
            }
            strFilter += "isnull(K3ItemCode,'')<>'' and isnull(K3CustomerCode,'')<>'' and isnull(BillStatus,0) =2";
            
            // strFilter += "(BillDateIn>='" + dtBillDateFrom.ToString("yyyy-MM-dd") + "' and BillDateIn<='" + dtBillDateTo.AddDays(1).ToString("yyyy-MM-dd") + "')";
            DataTable dtBill = ExecuteSQL.CallView(125, "", strFilter, "SaleCarOutBillID asc,SaleCarInBillID asc");

            decimal decTotalWeight = 0;
            decimal decCarTare = 0;
            decimal decSuttleWeight = 0;
            decimal decAmount = 0;
            foreach (DataRow dr in dtBill.Rows)
            {
                decTotalWeight += LBConverter.ToDecimal(dr["TotalWeight"]);
                decCarTare += LBConverter.ToDecimal(dr["CarTare"]);
                decSuttleWeight += LBConverter.ToDecimal(dr["SuttleWeight"]);
                decAmount += LBConverter.ToDecimal(dr["Amount"]);
            }

            this.txtTotalWeight.Text = (decTotalWeight / 1000).ToString("0.000");
            this.txtTareWeight.Text = (decCarTare / 1000).ToString("0.000");
            this.txtStuffWeight.Text = (decSuttleWeight / 1000).ToString("0.000");
            this.txtTotalAmount.Text = decAmount.ToString("0.00");
            this.txtTotalCar.Text = dtBill.Rows.Count.ToString();

            //如果当前登录用户为地磅文员，则将非现金客户的单价和金额隐藏
            if (LoginInfo.UserType == 0)
            {
                foreach (DataRow dr in dtBill.Rows)
                {
                    int iReceiveType = LBConverter.ToInt32(dr["ReceiveType"]);
                    if (iReceiveType != 0)
                    {
                        dr["PriceT"] = DBNull.Value;
                        dr["Price"] = DBNull.Value;
                        dr["Amount"] = DBNull.Value;
                        dr["MaterialPrice"] = DBNull.Value;
                        dr["FarePrice"] = DBNull.Value;
                        dr["TaxPrice"] = DBNull.Value;
                        dr["BrokerPrice"] = DBNull.Value;
                    }
                }
            }

            this.grdMain.DataSource = dtBill.DefaultView;
        }

        #endregion

        #region -- 按钮事件 --

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                List<DataRow> lstSelected = ReadSelectedRows();
                frmSynK3Process frmProcess = new frmSynK3Process(lstSelected,_SynType);
                LBShowForm.ShowDialog(frmProcess);
                this.LoadAllSalesBill();
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
                LoadAllSalesBill();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.grdMain.Visible = true;
                this.grdMain.Dock = DockStyle.Fill;
                LoadAllSalesBill();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- 按钮事件 --

        private List<DataRow> ReadSelectedRows()
        {
            this.grdMain.EndEdit();
            List<DataRow> lstSelected = new List<DataRow>();
            foreach(DataGridViewRow dgvr in this.grdMain.Rows)
            {
                bool bolLBSelect = LBConverter.ToBoolean(dgvr.Cells["LBSelect"].Value);
                if (bolLBSelect)
                {
                    DataRow dr = ((DataRowView)dgvr.DataBoundItem).Row;
                    lstSelected.Add(dr);
                }
            }
            return lstSelected;
        }
        
        private void btnSynToK3_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dvSource = this.grdMain.DataSource as DataView;
                foreach (DataRowView drv in dvSource)
                {
                    long lSaleCarInBillID = LBConverter.ToInt64(drv["SaleCarInBillID"]);
                    long lSaleCarOutBillID = LBConverter.ToInt64(drv["SaleCarOutBillID"]);
                    bool IsCancel = LBConverter.ToBoolean(drv["IsCancel"]);
                    string strSaleCarOutBillCode = drv["SaleCarOutBillCode"].ToString().TrimEnd();
                    string strSaleCarInBillCode = drv["SaleCarInBillCode"].ToString().TrimEnd();
                    string strCarNum = drv["CarNum"].ToString().TrimEnd();
                    decimal CarTare = LBConverter.ToDecimal( drv["CarTare"].ToString().TrimEnd());
                    decimal TotalWeight = LBConverter.ToDecimal(drv["TotalWeight"].ToString().TrimEnd());
                    decimal SuttleWeight = LBConverter.ToDecimal(drv["SuttleWeight"].ToString().TrimEnd());
                    decimal Price = LBConverter.ToDecimal(drv["Price"].ToString().TrimEnd());
                    decimal Amount = LBConverter.ToDecimal(drv["Amount"].ToString().TrimEnd());
                    K3Cloud.InsertK3Bill(strSaleCarOutBillCode, strSaleCarInBillCode,
                       "", "", strCarNum, "", TotalWeight,
                       CarTare, SuttleWeight, Price, Amount);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
}

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataGridViewRow dgvr in this.grdMain.Rows)
                {
                    dgvr.Cells["LBSelect"].Value = cbSelectAll.Checked;
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
