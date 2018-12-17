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
    public partial class frmSessionManager : LBUIPageBase
    {
        public frmSessionManager()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadDataSource();
        }
        
        private void LoadDataSource()
        {
            DataTable dtDesc = ExecuteSQL.CallView(140, "", "", "");
            this.grdMain.DataSource = dtDesc.DefaultView;
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否删除选中记录？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (this.grdMain.SelectedRows.Count == 0)
                    {
                        LB.WinFunction.LBCommonHelper.ShowCommonMessage("请选择需要删除的记录行！");
                        return;
                    }

                    DataTable dtSPIN = new DataTable();
                    dtSPIN.Columns.Add("SessionID", typeof(long));

                    DataView dvSource = this.grdMain.DataSource as DataView;
                    List<DataRow> lstDelete = new List<DataRow>();
                    foreach (DataGridViewRow dgvr in this.grdMain.SelectedRows)
                    {
                        DataRowView drv = dgvr.DataBoundItem as DataRowView;
                        long lSessionID = LBConverter.ToInt64(drv["SessionID"]);

                        if (lSessionID > 0)
                        {
                            DataRow drNew = dtSPIN.NewRow();
                            drNew["SessionID"] = lSessionID;
                            dtSPIN.Rows.Add(drNew);
                            dtSPIN.AcceptChanges();
                        }
                    }

                    DataSet dsReturn;
                    DataTable dtReturn;
                    ExecuteSQL.CallSP(30002, dtSPIN, out dsReturn, out dtReturn);

                    LoadDataSource();
                }
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
    }
}
