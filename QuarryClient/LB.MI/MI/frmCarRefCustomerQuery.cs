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

namespace LB.MI.MI
{
    public partial class frmCarRefCustomerQuery : LBUIPageBase
    {
        public List<DataRow> LstReturn = new List<DataRow>();
        private long mlCustomerID;
        public frmCarRefCustomerQuery(long lCustomerID)
        {
            InitializeComponent();
            mlCustomerID = lCustomerID;
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
            if (strFilter != "")
                strFilter += " and ";
            strFilter += "isnull(CustomerID,0) = 0 ";

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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                PerformReturn();
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
                PerformReturn();
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
                foreach (DataGridViewCell dgvc in this.grdMain.SelectedCells)
                {
                    DataRowView drv = this.grdMain.Rows[dgvc.RowIndex].DataBoundItem as DataRowView;
                    if(!LstReturn.Contains(drv.Row))
                        LstReturn.Add(drv.Row);
                }
            }

            this.Close();
        }

    }
}
