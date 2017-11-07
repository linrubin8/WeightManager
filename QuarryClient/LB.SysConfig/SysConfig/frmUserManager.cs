using LB.Controls;
using LB.Page.Helper;
using LB.Report.Report;
using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LB.Controls.Args;
using LB.Controls.Report;

namespace LB.SysConfig
{
    public partial class frmUserManager : LBUIPageBase
    {
        public frmUserManager()
        {
            InitializeComponent();
            this.grdMain.AutoGenerateColumns = false;
            //this.grdMain.LBLoadConst();
            this.grdMain.DataError += delegate (object sender, DataGridViewDataErrorEventArgs e) { };
            this.grdMain.LBCellButtonClick += GrdMain_LBCellButtonClick;
            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;

            
        }

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    DataGridViewRow dgvr = this.grdMain.Rows[e.RowIndex];
                    if (dgvr.DataBoundItem != null)
                    {
                        DataRowView drv = dgvr.DataBoundItem as DataRowView;
                        long lUserID = drv["UserID"] == DBNull.Value ?
                            0 : Convert.ToInt64(drv["UserID"]);

                        if (lUserID > 0)
                        {
                            frmAddUser frm = new frmAddUser(lUserID);
                            frm.ShowDialog();

                            LoadDataSource();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void GrdMain_LBCellButtonClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.grdMain.Columns[e.ColumnIndex].Name.Equals("Delete"))
                {
                    DataGridViewRow dgvr = this.grdMain.Rows[e.RowIndex];
                    if (dgvr.IsNewRow)
                    {
                        return;
                    }

                    if (dgvr.DataBoundItem != null)
                    {
                        DataRowView drv = dgvr.DataBoundItem as DataRowView;
                        long lUserID = drv["UserID"] == DBNull.Value ?
                            0 : Convert.ToInt64(drv["UserID"]);

                        if (lUserID > 0)
                        {
                            if (LB.WinFunction.LBCommonHelper.ConfirmMessage("确定删除？", "提示", MessageBoxButtons.YesNo) ==
                                 DialogResult.Yes)
                            {
                                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                                parmCol.Add(new LBParameter("UserID", enLBDbType.Int64, lUserID));
                                DataSet dsReturn;
                                Dictionary<string, object> dictValue;
                                try
                                {
                                    ExecuteSQL.CallSP(10002, parmCol, out dsReturn, out dictValue);
                                    LoadDataSource();
                                }
                                catch (Exception ex)
                                {
                                    dgvr.ErrorText = ex.Message;
                                }
                            }
                        }
                        else
                        {
                            this.grdMain.Rows.Remove(dgvr);
                        }
                    }
                }
                else if (this.grdMain.Columns[e.ColumnIndex].Name.Equals("btnSetUserPermission"))//设置用户权限
                {
                    DataGridViewRow dgvr = this.grdMain.Rows[e.RowIndex];
                    if (dgvr.DataBoundItem != null)
                    {
                        DataRowView drv = dgvr.DataBoundItem as DataRowView;
                        long lUserID = drv["UserID"] == DBNull.Value ?
                            0 : Convert.ToInt64(drv["UserID"]);

                        if (lUserID > 0)
                        {
                            frmUserPermission frmPermission = new frmUserPermission(lUserID);
                            LBShowForm.ShowDialog(frmPermission);
                        }
                        else
                        {
                            this.grdMain.Rows.Remove(dgvr);
                        }
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
            LoadDataSource();//加载数据源

            this.ctlSearcher1.SetGridView(this.grdMain, "UserName");
        }

        private void LoadDataSource()
        {
            string strFilter = this.ctlSearcher1.GetFilter();
            DataTable dtUser = ExecuteSQL.CallView(100,"", strFilter,"");
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
                frmAddUser frm = new frmAddUser(0);
                frm.ShowDialog();

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

        protected override void OnInitToolStripControl(ToolStripReportArgs args)
        {
            args.LBToolStrip = skinToolStrip1;
            args.ReportTypeID = 1;//客户管理
            base.OnInitToolStripControl(args);
            
        }

        protected override void OnReportRequest(ReportRequestArgs args)
        {
            base.OnReportRequest(args);
            DataTable dtSource = ((DataView)this.grdMain.DataSource).Table.Copy();
            dtSource.TableName = "T001";
            DataSet dsSource = new DataSet("Report");
            dsSource.Tables.Add(dtSource);
            args.DSDataSource = dsSource;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
