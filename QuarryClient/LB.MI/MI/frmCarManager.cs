using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using LB.Common;
using LB.WinFunction;
using LB.Page.Helper;

namespace LB.MI.MI
{
    public partial class frmCarManager : LBUIPageBase
    {
        public List<DataRow> LstReturn = new List<DataRow>();
        public frmCarManager()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadDataSource();
            
            this.txtCarNum.SkinTxt.TextChanged += SkinTxt_TextChanged;
            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;
        }

        private void SkinTxt_TextChanged(object sender, EventArgs e)
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

        private void TextBox_TextChanged(object sender, EventArgs e)
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

        private void LoadDataSource()
        {
            string strFilter = "";
            if (this.txtCarNum.Text.TrimEnd() != "")
            {
                if (strFilter != "")
                    strFilter += " and ";
                strFilter += "CarNum like '%" + this.txtCarNum.Text.TrimEnd() + "%'";
            }
            DataTable dtDetail = ExecuteSQL.CallView(117, "", strFilter, "");
            this.grdMain.DataSource = dtDetail.DefaultView;
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
        
        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void PerformReturn()
        {

            if (this.grdMain.SelectedCells.Count == 0)
            {
                throw new Exception("请选择有效的物料行！");
            }
            else
            {
                DataGridViewCell dgvc = this.grdMain.SelectedCells[0];
                DataRowView drv = this.grdMain.Rows[dgvc.RowIndex].DataBoundItem as DataRowView;
                LstReturn.Add(drv.Row);
            }

            this.Close();
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            try
            {
                frmCarEdit frmCar = new frmCarEdit(0, 0);
                LBShowForm.ShowDialog(frmCar);

                PerformReturn();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnCarWeightManager_Click(object sender, EventArgs e)
        {
            try
            {
                frmCarWeightManager frm = new MI.frmCarWeightManager();
                LBShowForm.ShowDialog(frm);
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
    }
}
