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

namespace LB.MI.MI
{
    public partial class frmCarWeightManager : LBUIPageBase
    {
        private long mlSelectedCarID = 0;
        public frmCarWeightManager()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.txtBillDateFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.txtBillDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");

            LoadDataSource();
            this.ctlSearcher1.SetGridView(this.grdMain, "CarNum");

            this.grdMain.SelectionChanged += GrdMain_SelectionChanged;

            
            LoadCarDataSource(0);
        }

        private void GrdMain_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.grdMain.SelectedCells.Count>0)
                {
                    DataRowView drvSelect = this.grdMain.Rows[this.grdMain.SelectedCells[0].RowIndex].DataBoundItem as DataRowView;
                    long lCarID = drvSelect["CarID"] == DBNull.Value ?
                        0 : Convert.ToInt64(drvSelect["CarID"]);
                    if (lCarID > 0)
                    {
                        mlSelectedCarID = lCarID;
                        LoadCarDataSource(lCarID);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void LoadDataSource()
        {
            string strFilter = this.ctlSearcher1.GetFilter();
            DataTable dtCar = ExecuteSQL.CallView(130, "", strFilter, "");
            this.grdMain.DataSource = dtCar.DefaultView;
        }

        private void LoadCarDataSource(long lCarID)
        {
            string strFilter = "CarID="+ lCarID;
            if (this.txtBillDateFrom.Text.TrimEnd() != "")
            {
                if (strFilter != "")
                    strFilter += " and ";
                strFilter += "CreateTime >= '" + this.txtBillDateFrom.Text.TrimEnd() + "'";
            }
            if (this.txtBillDateTo.Text.TrimEnd() != "")
            {
                if (strFilter != "")
                    strFilter += " and ";
                strFilter += "CreateTime < '" + Convert.ToDateTime(this.txtBillDateTo.Text.TrimEnd()).AddDays(1) + "'";
            }

            DataTable dtDetail = ExecuteSQL.CallView(131, "", strFilter, "CreateTime desc");
            this.grdCarWeight.DataSource = dtDetail.DefaultView;
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

        private void btnAddCarWeight_Click(object sender, EventArgs e)
        {
            try
            {
                long lCarID = 0;
                if (this.grdMain.SelectedCells.Count == 0)
                {
                    throw new Exception("请选择有效的物料行！");
                }
                else
                {
                    DataGridViewCell dgvc = this.grdMain.SelectedCells[0];
                    DataRowView drv = this.grdMain.Rows[dgvc.RowIndex].DataBoundItem as DataRowView;
                    long.TryParse(drv["CarID"].ToString(), out lCarID);
                }

                frmAddCarWeight frm = new frmAddCarWeight(lCarID);
                LBShowForm.ShowDialog(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void txtBillDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCarDataSource(mlSelectedCarID);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            try
            {
                frmCarEdit frmCar = new frmCarEdit(0, 0);
                LBShowForm.ShowDialog(frmCar);
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

        private void grdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataRowView drvSelect = this.grdMain.Rows[e.RowIndex].DataBoundItem as DataRowView;
                    long lCarID = drvSelect["CarID"] == DBNull.Value ?
                        0 : Convert.ToInt64(drvSelect["CarID"]);
                    long lCustomerID = drvSelect["CustomerID"] == DBNull.Value ?
                        0 : Convert.ToInt64(drvSelect["CustomerID"]);
                    if (lCarID > 0)
                    {
                        frmCarEdit frmCar = new frmCarEdit(lCustomerID, lCarID);
                        LBShowForm.ShowDialog(frmCar);

                        LoadDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
