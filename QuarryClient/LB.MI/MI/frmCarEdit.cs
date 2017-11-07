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

namespace LB.MI
{
    public partial class frmCarEdit : LBUIPageBase
    {
        long mlCustomerID;
        long mlCarID;
        public frmCarEdit(long lCustomerID,long lCarID)
        {
            InitializeComponent();

            mlCarID = lCarID;
            mlCustomerID = lCustomerID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            DataTable dtCustomer= ExecuteSQL.CallView(112);//读取客户信息
            DataRow drNew = dtCustomer.NewRow();
            drNew["CustomerID"] = 0;
            drNew["CustomerName"] = "无";
            dtCustomer.Rows.InsertAt(drNew, 0);

            this.txtCustomerID.DataSource = dtCustomer;//读取客户信息
            this.txtCustomerID.DisplayMember = "CustomerName";
            this.txtCustomerID.ValueMember = "CustomerID";
            if(mlCustomerID>0)
                this.txtCustomerID.SelectedValue = mlCustomerID;

            ReadFieldValue();

            SetButtonStatus();
        }

        #region -- 根据客户状态显示或者隐藏相关按钮 --

        private void SetButtonStatus()
        {
            this.btnSave.Visible = true;
            this.btnDelete.Visible = true;
            this.btnAdd.Visible = true;

            if (mlCarID == 0)
            {
                this.btnSave.Visible = true;
                this.btnDelete.Visible = false;
                this.btnAdd.Visible = false;

                ClearFieldValue();
            }
        }

        private void ClearFieldValue()
        {
            this.txtCarNum.Text = "";

            mlCarID = 0;
        }

        #endregion

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
                ClearFieldValue();
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
                this.VerifyTextBoxIsEmpty();//校验控件是否为空
                
                int iSPType = 13500;
                if (mlCarID > 0)
                {
                    iSPType = 13501;
                }
                mlCustomerID = LBConverter.ToInt64(this.txtCustomerID.SelectedValue);
                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("CustomerID", enLBDbType.Int64, mlCustomerID));
                parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, mlCarID));
                parmCol.Add(new LBParameter("CarNum", enLBDbType.String, this.txtCarNum.Text));
                parmCol.Add(new LBParameter("CarCode", enLBDbType.String, this.txtCarCode.Text));
                parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));
                parmCol.Add(new LBParameter("DefaultCarWeight", enLBDbType.Decimal,LBConverter.ToDecimal( this.txtDefaultCarWeight.Text)));

                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                if (dictValue.ContainsKey("CarID"))
                {
                    mlCarID = LBConverter.ToInt64(dictValue["CarID"]);
                }
                if (dictValue.ContainsKey("CarCode"))
                {
                    this.txtCarCode.Text = dictValue["CarCode"].ToString();
                }
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
                SetButtonStatus();
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除该车辆？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlCarID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, mlCarID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(13502, parmCol, out dsReturn, out dictValue);
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #region --  读取界面参数值 --

        private void ReadFieldValue()
        {
            if (mlCarID > 0)
            {
                DataTable dtCar = ExecuteSQL.CallView(113, "", "CarID=" + mlCarID, "");
                if (dtCar.Rows.Count > 0)
                {
                    DataRow drCar = dtCar.Rows[0];

                    this.txtCarNum.Text = drCar["CarNum"].ToString();
                    this.txtCarCode.Text = drCar["CarCode"].ToString();
                    this.txtDescription.Text = drCar["Description"].ToString();
                    this.txtDefaultCarWeight.Text = drCar["DefaultCarWeight"].ToString();
                }
            }
        }

        #endregion --  读取界面参数值 --
    }
}
