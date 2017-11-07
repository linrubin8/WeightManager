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
    public partial class frmBankEdit : LBUIPageBase
    {
        long mlBankID;
        public frmBankEdit(long lBankID)
        {
            InitializeComponent();

            mlBankID = lBankID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ReadFieldValue();
            SetButtonStatus();
        }

        #region -- 根据客户状态显示或者隐藏相关按钮 --

        private void SetButtonStatus()
        {
            this.btnEditSave.Visible = true;
            this.btnAddSave.Visible = false;
            this.btnDelete.Visible = true;
            this.btnAdd.Visible = true;

            if (mlBankID == 0)
            {
                this.btnEditSave.Visible = false;
                this.btnAddSave.Visible = true;
                this.btnDelete.Visible = false;
                this.btnAdd.Visible = false;

                ClearFieldValue();
            }
        }

        private void ClearFieldValue()
        {
            this.txtBankName.Text = "";

            mlBankID = 0;
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
                
                int iSPType = 14600;
                if (mlBankID > 0)
                {
                    iSPType = 14601;
                }
                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("ReceiveBankID", enLBDbType.Int64, mlBankID));
                parmCol.Add(new LBParameter("BankCode", enLBDbType.String, ""));
                parmCol.Add(new LBParameter("BankName", enLBDbType.String, this.txtBankName.Text));

                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                if (dictValue.ContainsKey("ReceiveBankID"))
                {
                    mlBankID = LBConverter.ToInt64(dictValue["ReceiveBankID"]);
                }
                if (dictValue.ContainsKey("BankCode"))
                {
                    this.txtBankCode.Text = dictValue["BankCode"].ToString();
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除该收款银行？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlBankID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ReceiveBankID", enLBDbType.Int64, mlBankID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(14602, parmCol, out dsReturn, out dictValue);
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
            if (mlBankID > 0)
            {
                DataTable dtBank = ExecuteSQL.CallView(136, "", "ReceiveBankID=" + mlBankID, "");
                if (dtBank.Rows.Count > 0)
                {
                    DataRow drCar = dtBank.Rows[0];

                    this.txtBankName.Text = drCar["BankName"].ToString();
                }
            }
        }

        #endregion --  读取界面参数值 --
    }
}
