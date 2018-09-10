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
    public partial class frmSynBaseInfoFromServer : LBUIPageBase
    {
        string strMsg = "";
        System.Windows.Forms.Timer mtimer = new System.Windows.Forms.Timer();
        Thread mThread = null;
        private bool bolIsKillThread = false;
        private bool bolIsFinished = false;

        public frmSynBaseInfoFromServer()
        {
            InitializeComponent();
            this.FormClosing += FrmSynK3Process_FormClosing;
        }

        private void FrmSynK3Process_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!bolIsFinished)
            {
                if(MessageBox.Show("警告","同步未完成，是否停止执行同步？", MessageBoxButtons.YesNo)== DialogResult.Yes)
                {
                    bolIsKillThread = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            mtimer.Enabled = false;
            if (mThread != null)
            {
                if (mThread.IsAlive)
                    mThread.Abort();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            mtimer.Interval = 1000;
            mtimer.Tick += Mtimer_Tick;
            mtimer.Enabled = true;
            mThread = new Thread(SynBaseInfo);
            mThread.Start();
        }
        

        private void Mtimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (strMsg != "")
                {
                    this.mtimer.Enabled = false;
                    lblWaist.Text = "同步失败：" + strMsg;
                }
                else if(bolIsFinished)
                {
                    this.mtimer.Enabled = false;

                    lblWaist.Text = "";
                    MessageBox.Show("同步完成！");
                }
            }
            catch (Exception ex)
            {
                this.mtimer.Enabled = false;
            }
        }

        private void SynBaseInfo()
        {
            try
            {
                #region -- 同步客户信息 --

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

                foreach (DataRow drCustomer in dtCustomer.Rows)
                {
                    if (bolIsKillThread)
                    {
                        break;
                    }
                    string strCustomerName = drCustomer["CustomerName"].ToString().Trim();
                    DataTable dtCustomerSP = drCustomer.Table.Clone();
                    dtCustomerSP.ImportRow(drCustomer);
                    bool bolIsAdd = LBConverter.ToBoolean(drCustomer["IsAdd"]);//是否添加
                    try
                    {
                        if (bolIsAdd)
                        {
                            //添加
                            DataSet dsReturn;
                            DataTable dtOut;
                            if (dtCustomerSP != null)
                            {
                                ExecuteSQL.CallSP(13403, dtCustomerSP, out dsReturn, out dtOut);
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
                                    InsertCustomerListView(strCustomerName, "添加成功");
                                }
                            }
                        }
                        else
                        {
                            //编辑
                            if (dtCustomerSP != null)
                            {
                                DataSet dsReturn;
                                DataTable dtOut;
                                ExecuteSQL.CallSP(13404, dtCustomerSP, out dsReturn, out dtOut);
                                InsertCustomerListView(strCustomerName, "修改成功");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        InsertCustomerListView(strCustomerName, "失败：" + ex.Message);
                    }
                }

                #endregion

                #region --同步车辆信息 --

                DataTable dtAddCar;
                DataTable dtEditCar;
                SynchronousCar.CompareClientAndServer(out dtAddCar, out dtEditCar);
                dtAddCar.Columns.Add("IsAdd", typeof(int));
                dtEditCar.Columns.Add("IsAdd", typeof(int));

                foreach (DataRow dr in dtAddCar.Rows)
                {
                    dr["IsAdd"] = 1;
                }

                DataTable dtCarDownLoad = dtAddCar.Clone();
                dtCarDownLoad.Merge(dtAddCar);
                dtCarDownLoad.Merge(dtEditCar);

                DataTable dtCarUpload;
                SynchronousCar.CompareClientDiffServer(out dtCarUpload);
                if (bolIsKillThread)
                {
                    return;
                }

                #region -- 同步至服务器 --
                foreach (DataRow drCar in dtCarUpload.Rows)
                {
                    if (bolIsKillThread)
                    {
                        break;
                    }
                    string strCarNum = drCar["CarNum"].ToString().TrimEnd();
                    string strCustomerName = drCar["CustomerName"].ToString().TrimEnd();
                    DataTable dtCustomer1 = ExecuteSQL.CallView_Service(110, "CustomerID", "CustomerName='" + strCustomerName + "'", "");
                    if (dtCustomer1.Rows.Count > 0)
                    {
                        drCar["CustomerID"] = dtCustomer1.Rows[0]["CustomerID"];
                    }
                    else
                    {
                        drCar["CustomerID"] = DBNull.Value;
                    }

                    DataTable dtCar = drCar.Table.Clone();
                    dtCar.ImportRow(drCar);

                    try
                    {
                        DataSet dsResult;
                        DataTable dtResult;
                        SynchronousCar.AddServerCarData(dtCar, out dsResult, out dtResult);
                        long lCarID = 0;
                        if (dtResult != null && dtResult.Rows.Count > 0)
                        {
                            foreach (DataColumn dc in dtResult.Columns)
                            {
                                if (dc.ColumnName.Contains("CarID"))
                                {
                                    lCarID = LBConverter.ToInt64(dtResult.Rows[0]["CarID"]);
                                    break;
                                }
                            }
                        }
                        if (lCarID > 0)
                        {
                            InsertCarListView(strCarNum, "成功同步至服务器");
                        }
                    }
                    catch (Exception ex)
                    {
                        InsertCarListView(strCarNum, "失败：" + ex.Message);
                    }
                }
                #endregion -- 同步至服务器 --

                if (bolIsKillThread)
                {
                    return;
                }

                #region -- 将客户资料下载到本地 --

                foreach (DataRow drCar in dtCarDownLoad.Rows)
                {
                    if (bolIsKillThread)
                    {
                        break;
                    }
                    string strCarNum = drCar["CarNum"].ToString().TrimEnd();
                    string strCustomerName = drCar["CustomerName"].ToString().TrimEnd();
                    DataTable dtCustomer1 = ExecuteSQL.CallView(110, "CustomerID", "CustomerName='" + strCustomerName + "'", "");
                    if (dtCustomer1.Rows.Count > 0)
                    {
                        drCar["CustomerID"] = dtCustomer1.Rows[0]["CustomerID"];
                    }
                    else
                    {
                        drCar["CustomerID"] = DBNull.Value;
                    }

                    DataTable dtCarTemp = drCar.Table.Clone();
                    dtCarTemp.ImportRow(drCar);

                    bool bolIsAdd = LBConverter.ToBoolean(drCar["IsAdd"]);//是否添加
                    try
                    {
                        if (bolIsAdd)
                        {
                            //添加
                            DataSet dsReturn;
                            DataTable dtOut;
                            if (dtCarTemp != null)
                            {
                                ExecuteSQL.CallSP(13500, dtCarTemp, out dsReturn, out dtOut);
                                long lCarID_Out = 0;
                                if (dtOut != null && dtOut.Rows.Count > 0)
                                {
                                    foreach (DataColumn dc in dtOut.Columns)
                                    {
                                        if (dc.ColumnName.Contains("CarID"))
                                        {
                                            lCarID_Out = LBConverter.ToInt64(dtOut.Rows[0]["CarID"]);
                                            break;
                                        }
                                    }
                                }
                                if (lCarID_Out > 0)
                                {
                                    InsertCarListView(strCarNum, "成功同步至本地");
                                }
                            }
                        }
                        else
                        {
                            //编辑
                            if (dtCarTemp != null)
                            {
                                DataSet dsReturn;
                                DataTable dtOut;
                                ExecuteSQL.CallSP(13501, dtCarTemp, out dsReturn, out dtOut);
                                InsertCarListView(strCarNum, "成功同步至本地");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        InsertCarListView(strCarNum, "失败：" + ex.Message);
                    }
                }

                #endregion -- 将客户资料下载到本地 --

                if (bolIsKillThread)
                {
                    return;
                }
                #endregion --同步车辆信息 --

                try
                {
                    SynchronousPrice.SynchronousClientFromServer();//价格表同步
                }
                catch (Exception ex)
                {
                    InsertPriceListView("同步失败：" + ex.Message);
                }

                bolIsFinished = true;
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
            }
        }

        private void InsertCustomerListView(string strCustomerName,string strResult)
        {
            if (this.lvCustomer.InvokeRequired)
            {
                this.lvCustomer.Invoke((MethodInvoker)delegate {
                    ListViewItem item = new ListViewItem(strCustomerName);
                    item.SubItems.Add(strResult);
                    this.lvCustomer.Items.Add(item);
                });
            }
        }

        private void InsertCarListView(string strCarNum, string strResult)
        {
            if (this.lvCar.InvokeRequired)
            {
                this.lvCar.Invoke((MethodInvoker)delegate {
                    ListViewItem item = new ListViewItem(strCarNum);
                    item.SubItems.Add(strResult);
                    this.lvCar.Items.Add(item);
                });
            }
        }

        private void InsertPriceListView( string strResult)
        {
            if (this.lvPrice.InvokeRequired)
            {
                this.lvPrice.Invoke((MethodInvoker)delegate {
                    ListViewItem item = new ListViewItem(strResult);
                    this.lvPrice.Items.Add(item);
                });
            }
        }
    }
    
}
