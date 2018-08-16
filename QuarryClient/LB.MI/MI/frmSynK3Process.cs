using LB.Common;
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
    public partial class frmSynK3Process : LBUIPageBase
    {
        string strMsg = "";
        System.Windows.Forms.Timer mtimer = new System.Windows.Forms.Timer();
        Thread mThread = null;
        int TotalSynRows = 0;
        int _SynType = 0;
        int _SuccessCount = 0;
        int _FailCount = 0;
        private List<DataRow> mlstRows = new List<DataRow>();
        List<SynInfo> mlstSynInfo = new List<SynInfo>();
        public frmSynK3Process(List<DataRow> lstRows,int iSynType)
        {
            InitializeComponent();
            mlstRows = lstRows;
            this.lblTotal.Text = lstRows.Count.ToString();
            _SynType = iSynType;
            this.FormClosing += FrmSynK3Process_FormClosing;
        }

        private void FrmSynK3Process_FormClosing(object sender, FormClosingEventArgs e)
        {
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
            mThread = new Thread(SynK3);
            mThread.Start();
        }
        

        private void Mtimer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.lblSuccess.Text = _SuccessCount.ToString();
                this.lblFail.Text = _FailCount.ToString();
                //this.lblOutBillCode.Text = strMsg;
                if(TotalSynRows== mlstRows.Count)
                {
                    this.mtimer.Enabled = false;
                    //this.Close();
                    MessageBox.Show("同步完成！");
                }
            }
            catch (Exception ex)
            {
                this.mtimer.Enabled = false;
            }
        }

        private void SynK3()
        {
            foreach(DataRow dr in mlstRows)
            {
                string strSaleCarOutBillCode = dr["SaleCarOutBillCode"].ToString().TrimEnd();
                try
                {
                    long lSaleCarInBillID = LBConverter.ToInt64(dr["SaleCarInBillID"]);
                    LBDbParameterCollection parmCol = new LBDbParameterCollection();
                    parmCol.Add(new LBParameter("SaleCarInBillID", enLBDbType.Int64, lSaleCarInBillID));
                    parmCol.Add(new LBParameter("SynType", enLBDbType.Int64, _SynType));

                    DataSet dsReturn;
                    Dictionary<string, object> dictValue;
                    ExecuteSQL.CallSP(14124, parmCol, out dsReturn, out dictValue);
                    strMsg = "已同步磅单：" + strSaleCarOutBillCode;

                    bool bolOutBillIsSuccess = false;
                    bool bolReceiveIsSuccess = false;
                    string strOutBillSynError = "";
                    string strReceiveSynError = "";

                    if (dictValue.ContainsKey("OutBillIsSuccess"))
                    {
                        bolOutBillIsSuccess = LBConverter.ToBoolean( dictValue["OutBillIsSuccess"]);
                    }
                    if (dictValue.ContainsKey("ReceiveIsSuccess"))
                    {
                        bolReceiveIsSuccess = LBConverter.ToBoolean(dictValue["ReceiveIsSuccess"]);
                    }
                    if (dictValue.ContainsKey("OutBillSynError"))
                    {
                        strOutBillSynError = dictValue["OutBillSynError"].ToString().TrimEnd();
                    }
                    if (dictValue.ContainsKey("ReceiveSynError"))
                    {
                        strReceiveSynError = dictValue["ReceiveSynError"].ToString().TrimEnd();
                    }

                    bool bolIsSuccess = false;
                    string strSynError = "";
                    if (_SynType == 0)//应收
                    {
                        bolIsSuccess = bolReceiveIsSuccess;
                        strSynError = strReceiveSynError;
                    }
                    else if (_SynType == 1)//出库
                    {
                        bolIsSuccess = bolOutBillIsSuccess;
                        strSynError = strOutBillSynError;
                    }

                    if (this.listView1.InvokeRequired)
                    {
                        this.listView1.Invoke((MethodInvoker)delegate {
                            ListViewItem item = new ListViewItem(strSaleCarOutBillCode);
                            if (bolIsSuccess)
                            {
                                strSynError = strSynError != "" ? strSynError : "同步成功";
                                _SuccessCount++;
                            }
                            else
                            {
                                strSynError = "同步失败：" + strSynError;
                                _FailCount++;
                            }
                            item.SubItems.Add(strSynError);
                            this.listView1.Items.Add(item);
                        });
                    }
                }
                catch (Exception ex)
                {
                    strMsg = "磅单同步错误："+ strSaleCarOutBillCode;
                }
                finally
                {
                    TotalSynRows++;
                }
            }
        }
    }

    public class SynInfo
    {
        public string BillCode = "";
        public bool OutSynIsSuccess = false;
        public bool ReceiveSynIsSuccess = false;
        public string OutSynResult = "";
        public string ReceiveResult = "";
    }
}
