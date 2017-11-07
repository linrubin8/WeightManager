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
    public partial class frmUOMManager : LBUIPageBase
    {
        public frmUOMManager()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            LoadDataSource();
            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;
        }

        private void LoadDataSource()
        {
            DataTable dtView = ExecuteSQL.CallView(202);
            //this.grdMain.DataSource = dtView.DefaultView;
            //string strFilter = this.ctlSearcher1.GetFilter();
            //DataTable dtUser = ExecuteSQL.CallView(112, "", strFilter, "");
            this.grdMain.DataSource = dtView.DefaultView;
        }

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataRowView drvSelect = this.grdMain.Rows[e.RowIndex].DataBoundItem as DataRowView;
                    long lUOMID = LBConverter.ToInt64(drvSelect["UOMID"]);
                    if (lUOMID == 0)
                    {
                        return;
                    }
                    frmUOM frm = new frmUOM(lUOMID);
                    LBShowForm.ShowDialog(frm);
                    LoadDataSource();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #region -- 添加单位 --

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmUOM frm = new frmUOM(0);
                LBShowForm.ShowDialog(frm);
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
                long lUOMID = 0;
                if (grdMain.CurrentRow != null)
                {
                    DataRowView drv = grdMain.CurrentRow.DataBoundItem as DataRowView;
                    lUOMID = LBConverter.ToInt64(drv["UOMID"]);
                }
                if (lUOMID == 0)
                {
                    return;
                }
                frmUOM frm = new frmUOM(lUOMID);
                LBShowForm.ShowDialog(frm);
                LoadDataSource();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                long lUOMID = 0;
                string strUOMName = "";
                if (grdMain.CurrentRow != null)
                {
                    DataRowView drv = grdMain.CurrentRow.DataBoundItem as DataRowView;
                    lUOMID = LBConverter.ToInt64(drv["UOMID"]);
                    strUOMName = drv["UOMName"].ToString().TrimEnd();
                }
                if (lUOMID == 0)
                {
                    return;
                }

                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除计量单位："+ strUOMName + "？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    LBDbParameterCollection parmCol = new LBDbParameterCollection();
                    parmCol.Add(new LBParameter("UOMID", enLBDbType.Int64, lUOMID));
                    DataSet dsReturn;
                    Dictionary<string, object> dictValue;
                    ExecuteSQL.CallSP(20202, parmCol, out dsReturn, out dictValue);
                    LoadDataSource();
                }
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
        #endregion -- 添加单位 --
    }
}
