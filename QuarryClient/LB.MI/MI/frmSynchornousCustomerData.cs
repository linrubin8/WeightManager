using LB.Common;
using LB.Common.Synchronous;
using LB.Controls;
using LB.WinFunction;
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
    public partial class frmSynchornousCustomerData : LBUIPageBase
    {
        Dictionary<int, DataRow> _dictDownLoadToClient = new Dictionary<int, DataRow>();
        //Dictionary<int, DataRow> _dictUploadToServer = new Dictionary<int, DataRow>();
        Dictionary<int,string> _dictDownLoadMsg = new Dictionary<int, string>();
        //Dictionary<int, string> _dictUploadMsg = new Dictionary<int, string>();
        private System.Windows.Forms.Timer mTimer =null;
        public frmSynchornousCustomerData()
        {
            InitializeComponent();
            mTimer = new System.Windows.Forms.Timer();
            mTimer.Interval = 2000;
            mTimer.Tick += MTimer_Tick;
            this.grdDownLoadToClient.CellFormatting += GrdMain_CellFormatting;
        }

        private void GrdMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (this.grdDownLoadToClient["SynMsgClient", e.RowIndex].Value != null)
                    {
                        string strSynMessage = this.grdDownLoadToClient["SynMsgClient", e.RowIndex].Value.ToString().TrimEnd();
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

        private void MTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //bool bolIsSynAll_Server = true;
                ////将本地客户数据同步至服务器
                //for (int i = 0, j = this.grdUploadToServer.Rows.Count; i < j; i++)
                //{
                //    DataGridViewRow dgvr = this.grdUploadToServer.Rows[i];
                //    if (this._dictUploadMsg.ContainsKey(i))
                //    {
                //        dgvr.Cells["SynMsgServer"].Value = _dictUploadMsg[i];
                //    }
                //    if(dgvr.Cells["SynMsgServer"].Value==null|| dgvr.Cells["SynMsgServer"].Value.ToString() == "")
                //    {
                //        bolIsSynAll_Server = false;
                //    }
                //}

                bool bolIsSynAll_Client = true;
                //将服务器数据同步至本地客户
                for (int i = 0, j = this.grdDownLoadToClient.Rows.Count; i < j; i++)
                {
                    DataGridViewRow dgvr = this.grdDownLoadToClient.Rows[i];
                    if (_dictDownLoadMsg.ContainsKey(i))
                    {
                        dgvr.Cells["SynMsgClient"].Value = _dictDownLoadMsg[i];
                    }
                    if (dgvr.Cells["SynMsgClient"].Value == null || dgvr.Cells["SynMsgClient"].Value.ToString() == "")
                    {
                        bolIsSynAll_Client = false;
                    }
                }

                if(bolIsSynAll_Client)
                {
                    mTimer.Enabled = false;
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("同步完毕！");
                    ReadFlesh();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
                mTimer.Enabled = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ReadFlesh();
        }

        private void ReadFlesh()
        {
            try
            {
                DataTable dtAddCustomer;
                DataTable dtEditCustomer;
                SynchronousCustomer.CompareClientAndServer(out dtAddCustomer, out dtEditCustomer);
                dtAddCustomer.Columns.Add("IsAdd", typeof(int));
                dtEditCustomer.Columns.Add("IsAdd", typeof(int));

                foreach (DataRow dr in dtAddCustomer.Rows)
                {
                    dr["IsAdd"] = 1;
                }

                DataTable dtCustomer = dtAddCustomer.Clone();
                dtCustomer.Merge(dtAddCustomer);
                dtCustomer.Merge(dtEditCustomer);
                this.grdDownLoadToClient.DataSource = dtCustomer.DefaultView;

                //DataTable dtUploadToServer;
                //SynchronousCustomer.CompareClientDiffServer(out dtUploadToServer);
                //this.grdUploadToServer.DataSource = dtUploadToServer.DefaultView;
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                _dictDownLoadToClient = new Dictionary<int, DataRow>();
                //_dictUploadToServer = new Dictionary<int, DataRow>();
                _dictDownLoadMsg = new Dictionary<int, string>();
                //将本地客户数据同步至服务器
                //for (int i=0,j=this.grdUploadToServer.Rows.Count;i<j;i++)
                //{
                //    DataGridViewRow dgvr = this.grdUploadToServer.Rows[i];
                //    _dictUploadToServer.Add(i, ((DataRowView)dgvr.DataBoundItem).Row);
                //}

                //将服务器客户数据同步至本地
                for (int i = 0, j = this.grdDownLoadToClient.Rows.Count; i < j; i++)
                {
                    DataGridViewRow dgvr = this.grdDownLoadToClient.Rows[i];
                    _dictDownLoadToClient.Add(i, ((DataRowView)dgvr.DataBoundItem).Row);
                }

                Thread thread = new Thread(SynToServer);
                thread.Start();

                mTimer.Enabled = true;
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

        private void SynToServer()
        {
            try
            {
                #region -- 将客户资料下载到本地 --

                foreach (KeyValuePair<int, DataRow> keyvalue in _dictDownLoadToClient)
                {
                    DataRow drCustomer = keyvalue.Value;
                    DataTable dtCustomer = drCustomer.Table.Clone();
                    dtCustomer.ImportRow(drCustomer);
                    bool bolIsAdd = LBConverter.ToBoolean(drCustomer["IsAdd"]);//是否添加
                    try
                    {
                        if (bolIsAdd)
                        {
                            //添加
                            DataSet dsReturn;
                            DataTable dtOut;
                            if (dtCustomer != null)
                            {
                                ExecuteSQL.CallSP(13403, dtCustomer, out dsReturn, out dtOut);
                                long lCustomerID_Out = 0;
                                if (dtOut != null && dtOut.Rows.Count > 0)
                                {
                                    foreach (DataColumn dc in dtOut.Columns)
                                    {
                                        if (dc.ColumnName.Contains("CustomerID"))
                                        {
                                            lCustomerID_Out = LBConverter.ToInt64(dtOut.Rows[0]["CustomerID"]);
                                            break;
                                        }
                                    }
                                }
                                if (lCustomerID_Out > 0)
                                {
                                    this._dictDownLoadMsg.Add(keyvalue.Key, "成功");
                                }
                            }
                        }
                        else
                        {
                            //编辑
                            if (dtCustomer != null)
                            {
                                DataSet dsReturn;
                                DataTable dtOut;
                                ExecuteSQL.CallSP(13404, dtCustomer, out dsReturn, out dtOut);
                                _dictDownLoadMsg.Add(keyvalue.Key, "成功");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _dictDownLoadMsg.Add(keyvalue.Key, ex.Message);
                    }
                }

                #endregion -- 将客户资料下载到本地 --
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
