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
using LB.Page.Helper;
using LB.MI.MI;

namespace LB.MI
{
    public partial class frmCustomerEdit : LBUIPageBase
    {
        private long mlCustomerID;
        public frmCustomerEdit(long lCustomerID)
        {
            mlCustomerID = lCustomerID;
            InitializeComponent();

            this.grdCar.CellDoubleClick += GrdCar_CellDoubleClick;
        }

        private void GrdCar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataRowView drvSelect = this.grdCar.Rows[e.RowIndex].DataBoundItem as DataRowView;
                    long lCarID = drvSelect["CarID"] == DBNull.Value ?
                        0 : Convert.ToInt64(drvSelect["CarID"]);
                    if (lCarID > 0)
                    {
                        frmCarEdit frmCar = new frmCarEdit(mlCustomerID,lCarID);
                        LBShowForm.ShowDialog(frmCar);

                        LoadCarDataSource();
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

            this.txtAmountType.DataSource = LB.Common.LBConst.GetConstData("AmountType");//金额格式
            this.txtAmountType.DisplayMember = "ConstText";
            this.txtAmountType.ValueMember = "ConstValue";

            this.txtReceiveType.DataSource = LB.Common.LBConst.GetConstData("ReceiveType");//收款方式
            this.txtReceiveType.DisplayMember = "ConstText";
            this.txtReceiveType.ValueMember = "ConstValue";

            ReadFieldValue();

            SetButtonStatus();

            LoadCarDataSource();//加载车辆清单
            LoadModifyBillQuery();//添加单管理

            this.grdMain.LBLoadConst();
            LoadItemPrice(mlCustomerID);//客户物料价格表信息

            //bool bolPriceManager_Query = LBPermission.GetUserPermission("PriceManager_Query");
            //if (!bolPriceManager_Query)
            //{

            //}
        }

        #region -- 根据客户状态显示或者隐藏相关按钮 --

        private void SetButtonStatus()
        {
            this.btnSave.Visible = true;
            this.btnDelete.Visible = true;
            this.btnAdd.Visible = true;
            this.btnAddCar.Visible = true;
            this.btnCopy.Visible = true;
            this.btnReflesh.Visible = true;

            if (mlCustomerID == 0)
            {
                this.btnSave.Visible = true;
                this.btnDelete.Visible = false;
                this.btnAdd.Visible = false;
                this.btnAddCar.Visible = true;
                this.btnCopy.Visible = false;
                this.btnReflesh.Visible = false;

                ClearFieldValue();
            }
        }

        private void ClearFieldValue()
        {
            this.txtAddress.Text = "";
            this.txtAmountType.SelectedValue = 2;
            this.txtChangeBy.Text = "";
            this.txtChangeTime.Text = "";
            this.txtContact.Text = "";
            this.txtCreateBy.Text = "";
            this.txtCreateTime.Text = "";
            this.txtCreditAmount.Text = "0";
            this.txtDescription.Text = "";
            this.txtCustomerName.Text = "";
            this.txtPhone.Text = "";
            this.txtReceiveType.SelectedValue = 0;

            this.chkCarIsLimit.Checked = false;
            this.chkIsAllowOverFul.Checked = false;
            this.chkIsDisplayAmount.Checked = false;
            this.chkIsDisplayPrice.Checked = false;
            this.chkIsPrintAmount.Checked = false;

            mlCustomerID = 0;

            LoadCarDataSource();
        }

        #endregion

        #region -- 按钮事件 --

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            try
            {
                if (mlCustomerID == 0)
                    throw new Exception("请先保存客户资料，再添加车辆！");

                frmCarEdit frmCar = new frmCarEdit(mlCustomerID,0);
                LBShowForm.ShowDialog(frmCar);
                LoadCarDataSource();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.VerifyTextBoxIsEmpty();//校验控件值是否为空

                SaveCustomer();
                SetButtonStatus();
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除该客户？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlCustomerID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, mlCustomerID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(13402, parmCol, out dsReturn, out dictValue);
                    }
                    this.Close();
                }
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
                this.ClearFieldValue();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        
        private void btnRefCar_Click(object sender, EventArgs e)
        {
            try
            {
                frmCarRefCustomerQuery frmCarQuery = new frmCarRefCustomerQuery(mlCustomerID);
                LBShowForm.ShowDialog(frmCarQuery);
                if (frmCarQuery.LstReturn.Count > 0)
                {
                    //将车辆关联当前客户
                    DataTable dtSPIN = new DataTable();
                    dtSPIN.Columns.Add("CarID", typeof(long));
                    dtSPIN.Columns.Add("CustomerID", typeof(long));
                    foreach(DataRow dr in frmCarQuery.LstReturn)
                    {
                        DataRow drNew = dtSPIN.NewRow();
                        drNew["CarID"] = dr["CarID"];
                        drNew["CustomerID"] = mlCustomerID;
                        dtSPIN.Rows.Add(drNew);
                    }
                    dtSPIN.AcceptChanges();
                    DataSet dsReturn;
                    DataTable dtReturn;
                    Dictionary<string, object> dictValue;
                    ExecuteSQL.CallSP(13503, dtSPIN, out dsReturn, out dtReturn);

                    LoadCarDataSource();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- 按钮事件 --

        #region --保存客户信息 --

        private void SaveCustomer()
        {
            int iSPType = 13400;
            if (mlCustomerID > 0)
            {
                iSPType = 13401;
            }

            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, mlCustomerID));
            parmCol.Add(new LBParameter("CustomerName", enLBDbType.String, this.txtCustomerName.Text));
            parmCol.Add(new LBParameter("CustomerCode", enLBDbType.String, this.txtCustomerCode.Text));
            parmCol.Add(new LBParameter("Contact", enLBDbType.String, this.txtContact.Text));
            parmCol.Add(new LBParameter("Phone", enLBDbType.String, this.txtPhone.Text));
            parmCol.Add(new LBParameter("Address", enLBDbType.String, this.txtAddress.Text));
            parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));
            parmCol.Add(new LBParameter("CarIsLimit", enLBDbType.Boolean, this.chkCarIsLimit.Checked));
            parmCol.Add(new LBParameter("AmountType", enLBDbType.Int32, this.txtAmountType.SelectedValue));
            //parmCol.Add(new LBParameter("CarIsLimit", enLBDbType.Boolean, this.txtLicenceNum.te));
            //parmCol.Add(new LBParameter("IsForbid", enLBDbType.Boolean, this.chkIsForbid.Checked));
            parmCol.Add(new LBParameter("ReceiveType", enLBDbType.Int32, this.txtReceiveType.SelectedValue));
            parmCol.Add(new LBParameter("CreditAmount", enLBDbType.Decimal, this.txtCreditAmount.Text));
            parmCol.Add(new LBParameter("IsDisplayPrice", enLBDbType.Boolean, this.chkIsDisplayPrice.Checked));
            parmCol.Add(new LBParameter("IsDisplayAmount", enLBDbType.Boolean, this.chkIsDisplayAmount.Checked));
            parmCol.Add(new LBParameter("IsPrintAmount", enLBDbType.Boolean, this.chkIsPrintAmount.Checked));
            parmCol.Add(new LBParameter("IsAllowOverFul", enLBDbType.Boolean, this.chkIsAllowOverFul.Checked));
            parmCol.Add(new LBParameter("IsAllowEmptyIn", enLBDbType.Boolean, this.chkIsAllowEmptyIn.Checked));
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("CustomerID"))
            {
                mlCustomerID = LBConverter.ToInt64(dictValue["CustomerID"]);
            }
            if (dictValue.ContainsKey("CustomerCode"))
            {
                this.txtCustomerCode.Text = dictValue["CustomerCode"].ToString();
            }
        }

        #endregion --保存客户信息 --

        #region --  读取界面参数值 --

        private void ReadFieldValue()
        {
            if (mlCustomerID > 0)
            {
                DataTable dtHeader = ExecuteSQL.CallView(112, "", "CustomerID=" + mlCustomerID, "");
                if (dtHeader.Rows.Count > 0)
                {
                    DataRow drHeader = dtHeader.Rows[0];

                    this.txtCustomerCode.Text = drHeader["CustomerCode"].ToString();
                    this.txtCustomerName.Text = drHeader["CustomerName"].ToString();
                    this.txtDescription.Text = drHeader["Description"].ToString();
                    this.txtContact.Text = drHeader["Contact"].ToString();
                    this.txtPhone.Text = drHeader["Phone"].ToString();
                    this.txtAddress.Text = drHeader["Address"].ToString();
                    this.txtCreditAmount.Text = drHeader["CreditAmount"].ToString();
                    this.txtAmountType.SelectedValue = LBConverter.ToInt32(drHeader["AmountType"]);
                    this.txtReceiveType.SelectedValue = LBConverter.ToInt32(drHeader["ReceiveType"]);

                    this.chkCarIsLimit.Checked = LBConverter.ToBoolean(drHeader["CarIsLimit"]);
                    this.chkIsDisplayPrice.Checked = LBConverter.ToBoolean(drHeader["IsDisplayPrice"]);
                    this.chkIsDisplayAmount.Checked = LBConverter.ToBoolean(drHeader["IsDisplayAmount"]);
                    this.chkIsPrintAmount.Checked = LBConverter.ToBoolean(drHeader["IsPrintAmount"]);
                    this.chkIsAllowOverFul.Checked = LBConverter.ToBoolean(drHeader["IsAllowOverFul"]);
                    this.chkIsAllowEmptyIn.Checked = LBConverter.ToBoolean(drHeader["IsAllowEmptyIn"]);

                    this.txtCreateBy.Text = drHeader["CreateBy"].ToString();
                    this.txtCreateTime.Text = drHeader["CreateTime"].ToString();
                    this.txtChangeBy.Text = drHeader["ChangeBy"].ToString();
                    this.txtChangeTime.Text = drHeader["ChangeTime"].ToString();

                    decimal decSalesReceivedAmount = LBConverter.ToDecimal(drHeader["SalesReceivedAmount"]);
                    decimal decTotalReceivedAmount = LBConverter.ToDecimal(drHeader["TotalReceivedAmount"]);
                    decimal decOverRangeAmount = decSalesReceivedAmount - decTotalReceivedAmount;
                    this.lblOverRangeAmount.Text = decOverRangeAmount > 0 ? decOverRangeAmount.ToString("0.00") : "0";
                    this.lblRemainAmount.Text = decTotalReceivedAmount > decSalesReceivedAmount ? (decTotalReceivedAmount - decSalesReceivedAmount).ToString("0.00") : "0";
                }
            }
        }

        #endregion --  读取界面参数值 --

        #region -- 加载车辆数据 --

        private void LoadCarDataSource()
        {
            DataTable dtCar = ExecuteSQL.CallView(117, "", "CustomerID="+mlCustomerID, "");
            this.grdCar.DataSource = dtCar.DefaultView;
        }

        #endregion -- 加载车辆数据 --

        #region -- 加载调价单管理清单 --

        private void LoadModifyBillQuery()
        {
            frmModifyBillHeaderQuery modifyQuery = new frmModifyBillHeaderQuery();
            modifyQuery.GetCustomFilterEvent += ModifyQuery_GetCustomFilterEvent;
            
            //tabPage2.Controls.Add(modifyQuery);
            //modifyQuery.Dock = DockStyle.Fill;
        }

        private void ModifyQuery_GetCustomFilterEvent(Common.Args.LBQueryFilterArgs e)
        {
            try
            {
                e.Filter = "CustomerID=" + mlCustomerID;
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion

        #region -- 加载物料价格信息 --

        private void LoadItemPrice(long lCustomerID)
        {
            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, lCustomerID));

            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(13607, parmCol, out dsReturn, out dictValue);
            if(dsReturn!=null && dsReturn.Tables.Count > 0)
            {
                dsReturn.Tables[0].DefaultView.Sort = "ItemName asc,CarID asc";
                this.grdMain.DataSource = dsReturn.Tables[0].DefaultView;
            }
        }

        #endregion -- 加载物料价格信息 --

    }
}
