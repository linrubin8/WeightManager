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

namespace LB.SysConfig.SysConfig
{
    public partial class frmDescriptionManager : LBUIPageBase
    {
        public frmDescriptionManager()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadDataSource();

            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;
        }

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ShowEdit();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void LoadDataSource()
        {
            DataTable dtDesc = ExecuteSQL.CallView(121, "", "", "");
            this.grdMain.DataSource = dtDesc.DefaultView;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmDescriptionAdd frm = new frmDescriptionAdd(0);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否删除选中行？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (this.grdMain.SelectedRows.Count == 0)
                    {
                        LB.WinFunction.LBCommonHelper.ShowCommonMessage("请选择需要删除的明细行！");
                        return;
                    }

                    DataTable dtSPIN = new DataTable();
                    dtSPIN.Columns.Add("DescriptionID", typeof(long));

                    DataView dvSource = this.grdMain.DataSource as DataView;
                    List<DataRow> lstDelete = new List<DataRow>();
                    foreach (DataGridViewRow dgvr in this.grdMain.SelectedRows)
                    {
                        DataRowView drv = dgvr.DataBoundItem as DataRowView;
                        long lDescriptionID = LBConverter.ToInt64(drv["DescriptionID"]);

                        if (lDescriptionID > 0)
                        {
                            DataRow drNew = dtSPIN.NewRow();
                            drNew["DescriptionID"] = lDescriptionID;
                            dtSPIN.Rows.Add(drNew);
                            dtSPIN.AcceptChanges();
                        }
                    }

                    DataSet dsReturn;
                    DataTable dtReturn;
                    ExecuteSQL.CallSP(14001, dtSPIN, out dsReturn, out dtReturn);

                    LoadDataSource();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                ShowEdit();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void ShowEdit()
        {
            if (this.grdMain.SelectedCells.Count == 0)
            {
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("请选择需要编辑的行！");
                return;
            }

            long lDescriptionID =LBConverter.ToInt64( this.grdMain.Rows[this.grdMain.SelectedCells[0].RowIndex].Cells["DescriptionID"].Value);
            frmDescriptionAdd frm = new frmDescriptionAdd(lDescriptionID);
            LBShowForm.ShowDialog(frm);

            LoadDataSource();
        }
    }
}
