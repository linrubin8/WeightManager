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
    public partial class frmAddCarWeight : LBUIPageBase
    {
        private System.Windows.Forms.Timer mTimer = null;
        long mlCarID;
        public frmAddCarWeight(long lCarID)
        {
            InitializeComponent();

            mTimer = new System.Windows.Forms.Timer();
            mTimer.Interval = 100;
            mTimer.Enabled = true;
            mTimer.Tick += MTimer_Tick;

            mlCarID = lCarID;
        }

        private void MTimer_Tick(object sender, EventArgs e)
        {
            this.lblWeight.Text = LBSerialHelper.WeightValue.ToString();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LBSerialHelper.StartSerial();//启动串口

            ReadFieldValue();
        }

        #region -- 根据客户状态显示或者隐藏相关按钮 --

        //private void SetButtonStatus()
        //{
        //    this.btnSave.Visible = true;
        //    this.btnDelete.Visible = true;
        //    this.btnAdd.Visible = true;

        //    if (mlCarID == 0)
        //    {
        //        this.btnSave.Visible = true;
        //        this.btnDelete.Visible = false;
        //        this.btnAdd.Visible = false;

        //        ClearFieldValue();
        //    }
        //}

        //private void ClearFieldValue()
        //{
        //    this.txtCarNum.Text = "";

        //    mlCarID = 0;
        //}

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

                string strWeightTypeDesc = "";
                decimal decCarWeight;
                if (rbDevice.Checked)
                {
                    decimal.TryParse(this.lblWeight.Text, out decCarWeight);
                    strWeightTypeDesc = "地磅称重";
                }
                else
                {
                    decimal.TryParse(this.txtCarWeight.Text, out decCarWeight);
                    strWeightTypeDesc = "手动输入重量";
                }

                if (decCarWeight <= 0)
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("皮重值必须大于0！");
                    return;
                }

                int iSPType = 20400;
                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("CarID", enLBDbType.Int64, mlCarID));
                parmCol.Add(new LBParameter("CarWeight", enLBDbType.Decimal, decCarWeight));
                parmCol.Add(new LBParameter("Description", enLBDbType.String, "皮重值来源：手工新增车辆皮重-["+ strWeightTypeDesc + "]"));

                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
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
                }
            }
        }

        #endregion --  读取界面参数值 --
    }
}
