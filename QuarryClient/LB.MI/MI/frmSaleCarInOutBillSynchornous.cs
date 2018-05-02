using LB.Common.Synchronous;
using LB.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LB.MI.MI
{
    public partial class frmSaleCarInOutBillSynchornous : LBUIPageBase
    {
        bool bolIsSynFinish = true;//是否同步结束
        public frmSaleCarInOutBillSynchornous()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.grdMain.LBLoadConst();
            LoadAllSalesBill();//磅单清单

            this.grdMain.CellPainting += GrdMain_CellPainting;
            this.grdMain.CellFormatting += GrdMain_CellFormatting;
        }

        private void GrdMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (this.grdMain["SynMessage", e.RowIndex].Value != null)
                    {
                        string strSynMessage = this.grdMain["SynMessage", e.RowIndex].Value.ToString().TrimEnd();
                        if (strSynMessage == "成功")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                        }
                        else if (strSynMessage != "")
                        {
                            e.CellStyle.BackColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void GrdMain_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                //if(e.RowIndex>=0 && e.ColumnIndex >= 0)
                //{
                //    string strSynMessage = this.grdMain["SynMessage", e.RowIndex].Value.ToString().TrimEnd();
                //    if (strSynMessage == "成功")
                //    {
                //        e.CellStyle.BackColor = Color.LightGreen;
                //    }
                //    else if (strSynMessage != "")
                //    {
                //        e.CellStyle.BackColor = Color.Red;
                //    }
                //}
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void LoadAllSalesBill()
        {
            DataView dvData = SynchronousBill.ReadUnSynchronousBill().DefaultView;
            this.grdMain.DataSource = dvData;
        }

        private void btnReflesh_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadAllSalesBill();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnCancelSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(DataGridViewRow dgvr in this.grdMain.Rows)
                {
                    dgvr.Cells["LBSelect"].Value = false;
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dgvr in this.grdMain.Rows)
                {
                    dgvr.Cells["LBSelect"].Value = true;
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSynchronous_Click(object sender, EventArgs e)
        {
            try
            {
                bolIsSynFinish = false;
                Thread thread = new Thread(SynchronousThread);
                thread.Start();
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

        private void SynchronousThread()
        {
            if (this.grdMain.InvokeRequired)
            {
                this.grdMain.Invoke((MethodInvoker)delegate
                {
                    foreach (DataGridViewRow dgvr in this.grdMain.Rows)
                    {
                        if (dgvr.Cells["LBSelect"].Value!=null && 
                            Convert.ToBoolean(dgvr.Cells["LBSelect"].Value.ToString()))
                        {
                            try
                            {
                                DataRowView drv = dgvr.DataBoundItem as DataRowView;
                                DataTable dtUpload = drv.Row.Table.Clone();
                                dtUpload.ImportRow(drv.Row);
                                dgvr.Cells["SynMessage"].Value = "";
                                SynchronousBill.SynchronousBillToServer(dtUpload);
                                dgvr.Cells["SynMessage"].Value = "成功";
                                dgvr.Cells["LBSelect"].Value = false;
                            }
                            catch (Exception ex)
                            {
                                dgvr.Cells["SynMessage"].Value = ex.Message;
                            }
                            this.grdMain.Invalidate();
                        }
                    }
                    bolIsSynFinish = true;
                });
            }
        }
    }
}
