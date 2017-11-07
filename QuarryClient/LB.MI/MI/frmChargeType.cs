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
    public partial class frmChargeType : LBUIPageBase
    {
        long mlChargeTypeID;
        public frmChargeType(long lChargeTypeID)
        {
            InitializeComponent();

            mlChargeTypeID = lChargeTypeID;
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

            if (mlChargeTypeID == 0)
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
            this.txtChargeTypeName.Text = "";

            mlChargeTypeID = 0;
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
                
                int iSPType = 14700;
                if (mlChargeTypeID > 0)
                {
                    iSPType = 14701;
                }
                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("ChargeTypeID", enLBDbType.Int64, mlChargeTypeID));
                parmCol.Add(new LBParameter("ChargeTypeCode", enLBDbType.String, ""));
                parmCol.Add(new LBParameter("ChargeTypeName", enLBDbType.String, this.txtChargeTypeName.Text));

                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                if (dictValue.ContainsKey("ChargeTypeID"))
                {
                    mlChargeTypeID = LBConverter.ToInt64(dictValue["ChargeTypeID"]);
                }
                if (dictValue.ContainsKey("ChargeTypeCode"))
                {
                    this.txtChargeTypeCode.Text = dictValue["ChargeTypeCode"].ToString();
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除该充值方式？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlChargeTypeID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ChargeTypeID", enLBDbType.Int64, mlChargeTypeID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(14702, parmCol, out dsReturn, out dictValue);
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
            if (mlChargeTypeID > 0)
            {
                DataTable dtBank = ExecuteSQL.CallView(138, "", "ChargeTypeID=" + mlChargeTypeID, "");
                if (dtBank.Rows.Count > 0)
                {
                    DataRow drCar = dtBank.Rows[0];

                    this.txtChargeTypeCode.Text = drCar["ChargeTypeCode"].ToString();
                    this.txtChargeTypeName.Text = drCar["ChargeTypeName"].ToString();
                }
            }
        }

        #endregion --  读取界面参数值 --
    }
}
