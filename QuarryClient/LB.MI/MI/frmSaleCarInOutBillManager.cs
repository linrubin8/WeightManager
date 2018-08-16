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
using LB.Controls.Args;
using LB.Controls.Report;
using System.IO;
using LB.Common.Synchronous;

namespace LB.MI.MI
{
    public partial class frmSaleCarInOutBillManager : LBUIPageBase
    {
        public frmSaleCarInOutBillManager()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.grdMain.LBLoadConst();
            InitData();
            LoadAllSalesBill();//磅单清单

            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;

            this.grdMain.Columns["TotalWeight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void InitData()
        {
            //DataTable dtCustom = ExecuteSQL.CallView(110, "", "", "SortLevel desc,CustomerName asc");
            this.txtCustomerID.TextBox.LBViewType = 110;
            this.txtCustomerID.TextBox.LBSort = "SortLevel desc,CustomerName asc";
            this.txtCustomerID.TextBox.IDColumnName = "CustomerID";
            this.txtCustomerID.TextBox.TextColumnName = "CustomerName";
            //this.txtCustomerID.TextBox.PopDataSource = dtCustom.DefaultView;

            //DataTable dtCar = ExecuteSQL.CallView(113, "", "", "SortLevel desc,CarNum asc");
            this.txtCarID.TextBox.LBViewType = 113;
            this.txtCarID.TextBox.LBSort = "SortLevel desc,CarNum asc";
            this.txtCarID.TextBox.IDColumnName = "CarID";
            this.txtCarID.TextBox.TextColumnName = "CarNum";
            //this.txtCarID.TextBox.PopDataSource = dtCar.DefaultView;

            //DataTable dtItem = ExecuteSQL.CallView(203);
            this.txtItemID.TextBox.LBViewType = 203;
            this.txtItemID.TextBox.IDColumnName = "ItemID";
            this.txtItemID.TextBox.TextColumnName = "ItemName";
            //this.txtItemID.TextBox.PopDataSource = dtItem.DefaultView;

            this.txtInBillTimeFrom.Text = "00:00:00";
            this.txtOutBillTimeFrom.Text = "00:00:00";
            this.txtInBillTimeTo.Text = "23:59:59";
            this.txtOutBillTimeTo.Text = "23:59:59";

            this.grdMain.Visible = true;
            this.grdMain.Dock = DockStyle.Fill;
            this.grdSumMain.Visible = false;
        }

        #region -- 双击打开清单  --

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    long lSaleCarInBillID = LBConverter.ToInt64(this.grdMain["SaleCarInBillID", e.RowIndex].Value);
                    if (lSaleCarInBillID > 0)
                    {
                        frmSaleCarInOutEdit frmEdit = new frmSaleCarInOutEdit(lSaleCarInBillID);
                        LBShowForm.ShowDialog(frmEdit);
                        LoadAllSalesBill();//磅单清单
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- 双击打开清单  --

        #region -- 查询磅单清单 --

        private void LoadAllSalesBill()
        {
            string strFilter = "";

            if (this.txtCustomerID.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "CustomerName like '%" + this.txtCustomerID.Text + "%'";
            }

            if (this.txtCarID.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "CarNum like '%" + this.txtCarID.Text + "%'";
            }

            if (this.txtItemID.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "ItemName like '%" + this.txtItemID.Text + "%'";
            }

            if (this.txtBillCode.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "SaleCarInBillCode like '%" + this.txtBillCode.Text + "%'";
            }


            if (this.txtBillCodeOut.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "SaleCarOutBillCode like '%" + this.txtBillCodeOut.Text + "%'";
            }

            if (this.txtOutBillCraeteBy.Text != "")
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "CreateByOut like '%" + this.txtOutBillCraeteBy.Text + "%'";
            }

            if (cbUseInDate.Checked)
            {
                if (this.txtInBillDateFrom.Text != "")
                {
                    if (strFilter != "")
                    {
                        strFilter += " and ";
                    }
                    strFilter += "CreateTimeIn >= '" + this.txtInBillDateFrom.Text + " " + this.txtInBillTimeFrom.Text + "'";
                }

                if (this.txtInBillDateTo.Text != "")
                {
                    if (strFilter != "")
                    {
                        strFilter += " and ";
                    }
                    strFilter += "CreateTimeIn <= '" + this.txtInBillDateTo.Text + " " + this.txtInBillTimeTo.Text + "'";
                }
            }

            if (cbUseOutDate.Checked)
            {
                if (this.txtOutBillDateFrom.Text != "")
                {
                    if (strFilter != "")
                    {
                        strFilter += " and ";
                    }
                    strFilter += "CreateTimeOut >= '" + this.txtOutBillDateFrom.Text + " " + this.txtOutBillTimeFrom.Text + "'";
                }

                if (this.txtOutBillDateTo.Text != "")
                {
                    if (strFilter != "")
                    {
                        strFilter += " and ";
                    }
                    strFilter += "CreateTimeOut <= '" + this.txtOutBillDateTo.Text + " " + this.txtOutBillTimeTo.Text + "'";
                }
            }

            if (rbFinished.Checked)//已完成
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "SaleCarOutBillID is not null";
            }

            if (rbUnFinish.Checked)//未完成
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "SaleCarOutBillID is null";
            }

            if (rbCanceled.Checked)//已作废
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "IsCancel =1";
            }
            if (rbUnCancel.Checked)//未作废
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "isnull(IsCancel,0) =0";
            }
            if (rbApproved.Checked)//已审核
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "isnull(BillStatus,0) =2";
            }
            if (rbUnApprove.Checked)//未审核
            {
                if (strFilter != "")
                {
                    strFilter += " and ";
                }
                strFilter += "isnull(BillStatus,0) <>2";
            }

            // strFilter += "(BillDateIn>='" + dtBillDateFrom.ToString("yyyy-MM-dd") + "' and BillDateIn<='" + dtBillDateTo.AddDays(1).ToString("yyyy-MM-dd") + "')";
            DataTable dtBill = ExecuteSQL.CallView(125, "", strFilter, "SaleCarOutBillID desc,SaleCarInBillID desc");

            decimal decTotalWeight = 0;
            decimal decCarTare = 0;
            decimal decSuttleWeight = 0;
            decimal decAmount = 0;
            foreach (DataRow dr in dtBill.Rows)
            {
                decTotalWeight += LBConverter.ToDecimal(dr["TotalWeight"]);
                decCarTare += LBConverter.ToDecimal(dr["CarTare"]);
                decSuttleWeight += LBConverter.ToDecimal(dr["SuttleWeight"]);
                decAmount += LBConverter.ToDecimal(dr["Amount"]);
            }

            this.txtTotalWeight.Text = (decTotalWeight / 1000).ToString("0.000");
            this.txtTareWeight.Text = (decCarTare / 1000).ToString("0.000");
            this.txtStuffWeight.Text = (decSuttleWeight / 1000).ToString("0.000");
            this.txtTotalAmount.Text = decAmount.ToString("0.00");
            this.txtTotalCar.Text = dtBill.Rows.Count.ToString();

            //如果当前登录用户为地磅文员，则将非现金客户的单价和金额隐藏
            if (LoginInfo.UserType == 0)
            {
                foreach (DataRow dr in dtBill.Rows)
                {
                    int iReceiveType = LBConverter.ToInt32(dr["ReceiveType"]);
                    if (iReceiveType != 0)
                    {
                        dr["PriceT"] = DBNull.Value;
                        dr["Price"] = DBNull.Value;
                        dr["Amount"] = DBNull.Value;
                        dr["MaterialPrice"] = DBNull.Value;
                        dr["FarePrice"] = DBNull.Value;
                        dr["TaxPrice"] = DBNull.Value;
                        dr["BrokerPrice"] = DBNull.Value;
                    }
                }
            }

            this.grdMain.DataSource = dtBill.DefaultView;

            string strSelectField = "sum(Amount) as Amount,sum(TotalWeight) as TotalWeight,sum(CarTare) as CarTare,sum(SuttleWeight) as SuttleWeight,count(1) as CarCount";
            string strGroupBy = "";

            if (this.cbSumCar.Checked)
            {
                if (strGroupBy != "")
                {
                    strGroupBy += ",";
                }
                strGroupBy += "CarNum";
                strSelectField += ",CarNum";
                //strSelectField += ",count(CarNum) as CarCount";
            }
            if (this.cbSumCustomer.Checked)
            {
                if (strGroupBy != "")
                {
                    strGroupBy += ",";
                }
                strGroupBy += "CustomerName";
                strSelectField += ",CustomerName";
            }
            if (this.cbSumItem.Checked)
            {
                if (strGroupBy != "")
                {
                    strGroupBy += ",";
                }
                strGroupBy += "ItemName";
                strSelectField += ",ItemName";
            }

            this.grdSumMain.Columns["CarNumSum"].Visible = this.cbSumCar.Checked;
            //this.grdSumMain.Columns["CarCount"].Visible = this.cbSumCar.Checked;
            this.grdSumMain.Columns["CustomerNameSum"].Visible = this.cbSumCustomer.Checked;
            this.grdSumMain.Columns["ItemNameSum"].Visible = this.cbSumItem.Checked;

            DataTable dtSumBill = ExecuteSQL.CallDirectSQL("select "+ strSelectField + " from View_SaleCarInOutBill "+
                (strFilter==""?"":"where "+ strFilter)+
                (strGroupBy==""?"":" group by "+ strGroupBy));

            //如果当前登录用户为地磅文员，则将非现金客户的单价和金额隐藏
            if (LoginInfo.UserType == 0)
            {
                foreach (DataRow dr in dtSumBill.Rows)
                {
                    dr["Amount"] = DBNull.Value;
                }
            }

            this.grdSumMain.DataSource = dtSumBill;
        }

        #endregion

        #region -- 按钮事件 --

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                List<DataRow> lstSelected = ReadSelectedRows();

            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnUnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnUnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                
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
                LoadAllSalesBill();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.grdMain.Visible = true;
                this.grdMain.Dock = DockStyle.Fill;
                this.grdSumMain.Visible = false;
                LoadAllSalesBill();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSumSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.grdMain.Visible = false;
                this.grdSumMain.Visible = true;
                this.grdSumMain.Dock = DockStyle.Fill;
                LoadAllSalesBill();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        #endregion -- 按钮事件 --

        private List<DataRow> ReadSelectedRows()
        {
            List<DataRow> lstSelected = new List<DataRow>();
            foreach(DataGridViewRow dgvr in this.grdMain.SelectedRows)
            {
                DataRow dr = ((DataRowView)dgvr.DataBoundItem).Row;
                lstSelected.Add(dr);
            }
            return lstSelected;
        }

        #region -- 报表 --

        protected override void OnInitToolStripControl(ToolStripReportArgs args)
        {
            args.LBToolStrip = skinToolStrip1;
            args.ReportTypeID = 9;//磅单查询清单
            base.OnInitToolStripControl(args);

        }

        protected override void OnReportRequest(ReportRequestArgs args)
        {
            base.OnReportRequest(args);
            DataTable dtSource = ((DataView)this.grdMain.DataSource).Table.Copy();
            dtSource.TableName = "T009";
            DataSet dsSource = new DataSet("Report");
            dsSource.Tables.Add(dtSource);
            args.DSDataSource = dsSource;
        }

        #endregion

        private void btnAddInBill_Click(object sender, EventArgs e)
        {
            try
            {
                frmAddInBill frm = new LB.MI.frmAddInBill();
                LBShowForm.ShowDialog(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnAddOutBill_Click(object sender, EventArgs e)
        {
            try
            {
                frmAddOutBill frm = new LB.MI.frmAddOutBill();
                LBShowForm.ShowDialog(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnExportXML_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder strSaleCarInBillID = new StringBuilder();
                DataView dvSource = this.grdMain.DataSource as DataView;
                foreach (DataRowView drv in dvSource)
                {
                    long lSaleCarInBillID = LBConverter.ToInt64(drv["SaleCarInBillID"]);
                    long lSaleCarOutBillID = LBConverter.ToInt64(drv["SaleCarOutBillID"]);
                    bool IsCancel = LBConverter.ToBoolean(drv["IsCancel"]);

                    if ((lSaleCarInBillID > 0 && lSaleCarOutBillID > 0) ||
                       (lSaleCarInBillID > 0 && IsCancel))
                    {
                        if (strSaleCarInBillID.ToString() != "")
                        {
                            strSaleCarInBillID.Append(",");
                        }
                        strSaleCarInBillID.Append(lSaleCarInBillID.ToString());
                    }
                }

                if (strSaleCarInBillID.ToString() != "")
                {
                    DataSet dsBill = new DataSet("Bill");
                    //查询入场单
                    DataTable dtInBill = ExecuteSQL.CallView(128, "", "SaleCarInBillID in (" + strSaleCarInBillID.ToString() + ")", "SaleCarInBillID asc");
                    dtInBill.TableName = "InBill";
                    //查询出场单
                    DataTable dtOutBill = ExecuteSQL.CallView(124, "", "SaleCarInBillID in (" + strSaleCarInBillID.ToString() + ")", "SaleCarInBillID asc");
                    dtOutBill.TableName = "OutBill";
                    //客户资料
                    DataTable dtCustomer = ExecuteSQL.CallView(110, "", "", "");
                    dtCustomer.TableName = "Customer";
                    //车牌号码
                    DataTable dtCar = ExecuteSQL.CallView(113, "", "", "");
                    dtCar.TableName = "Car";

                    dsBill.Tables.Add(dtInBill);
                    dsBill.Tables.Add(dtOutBill);
                    dsBill.Tables.Add(dtCustomer);
                    dsBill.Tables.Add(dtCar);

                    string localFilePath = "";
                    //string localFilePath, fileNameExt, newFileName, FilePath; 
                    SaveFileDialog sfd = new SaveFileDialog();
                    //设置文件类型 
                    sfd.Filter = "BAK文件（*.bak）|*.bak";

                    //设置默认文件类型显示顺序 
                    sfd.FilterIndex = 1;

                    //保存对话框是否记忆上次打开的目录 
                    sfd.RestoreDirectory = true;

                    //点了保存按钮进入 
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        localFilePath = sfd.FileName.ToString(); //获得文件路径 
                        dsBill.WriteXml(localFilePath);
                        LB.WinFunction.LBCommonHelper.ShowCommonMessage("导出成功！");
                    }
                    //dsBill.WriteXml()
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnImportXML_Click(object sender, EventArgs e)
        {
            try
            {
                //OpenFileDialog fileDialog = new OpenFileDialog();
                //fileDialog.Multiselect = true;
                //fileDialog.Title = "请选择文件";
                //fileDialog.Filter = "BAK文件（*.bak）|*.bak";
                //if (fileDialog.ShowDialog() == DialogResult.OK)
                //{
                //    string file = fileDialog.FileName;
                //    DataSet ds = new DataSet();
                //    ds.ReadXml(file);

                //    DataTable dtInBill = null;
                //    DataTable dtOutBill = null;

                //    if (ds.Tables.Contains("InBill"))
                //    {
                //        dtInBill = ds.Tables["InBill"].Copy();
                //    }
                //    if (ds.Tables.Contains("OutBill"))
                //    {
                //        dtOutBill = ds.Tables["OutBill"].Copy();
                //    }

                //    LBDbParameterCollection parmCol = new LBDbParameterCollection();
                //    parmCol.Add(new LBParameter("DTInBill", enLBDbType.Object, dtInBill));
                //    parmCol.Add(new LBParameter("DTOutBill", enLBDbType.Object, dtOutBill));

                //    DataSet dsReturn;
                //    Dictionary<string, object> dictValue;
                //    ExecuteSQL.CallSP(14117, parmCol, out dsReturn, out dictValue);
                //    string strMsg = "";
                //    if (dictValue.ContainsKey("ErrorExistsBillCode"))
                //    {
                //        if (dictValue["ErrorExistsBillCode"].ToString() != "")
                //        {
                //            strMsg += "\n以下单据已存在于系统，不能重复导入：\n"+ dictValue["ErrorExistsBillCode"].ToString();
                //        }
                //    }
                //    if (dictValue.ContainsKey("ErrorUnFinishBillCode"))
                //    {
                //        if (dictValue["ErrorUnFinishBillCode"].ToString() != "")
                //        {
                //            strMsg += "\n以下单据未完成，不能导入：\n" + dictValue["ErrorUnFinishBillCode"].ToString();
                //        }
                //    }
                //    if (dictValue.ContainsKey("ErrorCount"))
                //    {
                //        string strErrorCount = dictValue["ErrorCount"].ToString();
                //        int iErrorCount = LBConverter.ToInt32(strErrorCount);
                //        if (iErrorCount > 0)
                //        {
                //            LB.WinFunction.LBCommonHelper.ShowCommonMessage("导入完毕，导入失败的单据数为："+ iErrorCount.ToString()+"张！想知道失败单据的单号，请留意打开的文本文件。");

                //        }else
                //        {
                //            LB.WinFunction.LBCommonHelper.ShowCommonMessage("全部数据导入完毕！");
                //        }
                //    }

                //    if (strMsg != "")
                //    {
                //        string strFile=Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),DateTime.Now.ToString("yyMMddHHmmss")+ ".txt");
                //        FileStream fs = new FileStream(strFile, FileMode.Create);
                //        //获得字节数组
                //        byte[] data = System.Text.Encoding.Default.GetBytes(strMsg);
                //        //开始写入
                //        fs.Write(data, 0, data.Length);
                //        //清空缓冲区、关闭流
                //        fs.Flush();
                //        fs.Close();

                //        System.Diagnostics.Process.Start(strFile);
                //    }

                //}
                //DataTable dtBill = SynchronousBill.ReadUnSynchronousBill();
                //SynchronousBill.SynchronousBillToServer(dtBill);

                frmSaleCarInOutBillSynchornous frm = new frmSaleCarInOutBillSynchornous();
                LBShowForm.ShowMainPage(frm);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSynToK3_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dvSource = this.grdMain.DataSource as DataView;
                foreach (DataRowView drv in dvSource)
                {
                    long lSaleCarInBillID = LBConverter.ToInt64(drv["SaleCarInBillID"]);
                    long lSaleCarOutBillID = LBConverter.ToInt64(drv["SaleCarOutBillID"]);
                    bool IsCancel = LBConverter.ToBoolean(drv["IsCancel"]);
                    string strSaleCarOutBillCode = drv["SaleCarOutBillCode"].ToString().TrimEnd();
                    string strSaleCarInBillCode = drv["SaleCarInBillCode"].ToString().TrimEnd();
                    string strCarNum = drv["CarNum"].ToString().TrimEnd();
                    decimal CarTare = LBConverter.ToDecimal( drv["CarTare"].ToString().TrimEnd());
                    decimal TotalWeight = LBConverter.ToDecimal(drv["TotalWeight"].ToString().TrimEnd());
                    decimal SuttleWeight = LBConverter.ToDecimal(drv["SuttleWeight"].ToString().TrimEnd());
                    decimal Price = LBConverter.ToDecimal(drv["Price"].ToString().TrimEnd());
                    decimal Amount = LBConverter.ToDecimal(drv["Amount"].ToString().TrimEnd());
                    K3Cloud.InsertK3Bill(strSaleCarOutBillCode, strSaleCarInBillCode,
                       "", "", strCarNum, "", TotalWeight,
                       CarTare, SuttleWeight, Price, Amount);
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
}
    }
}
