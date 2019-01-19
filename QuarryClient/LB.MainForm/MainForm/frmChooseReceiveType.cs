using LB.Common;
using LB.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.MainForm.MainForm
{
    public partial class frmChooseReceiveType : LBUIPageBase
    {
        public bool IsSubmit = false;
        public int ReceiveType = 0;
        public decimal CustomerPayAmount = 0;
        decimal decAmount = 0;
        bool bolIsDisplayAmount;
        decimal decCustomerLeftAmount;
        public frmChooseReceiveType(int iReceiveType,
            string strCustomerName,
            string strCarNum,
            string strItem,
            string strAmount,
            string strCustomerLeftAmount,
            bool bolIsDisplayAmount)
        {
            InitializeComponent();
            ReceiveType = iReceiveType;
            decimal.TryParse(strAmount, out decAmount);
            if(bolIsDisplayAmount)
                this.lblAmount.Text = strAmount;
            this.lblCarNum.Text = strCarNum;
            this.lblCustomerName.Text = strCustomerName;
            this.lblItem.Text = strItem;
            this.lblRemainAmount.Text = strCustomerLeftAmount;
            this.bolIsDisplayAmount = bolIsDisplayAmount;
            //this.txtReceiveType.SelectedValue = ReceiveType;
            decCustomerLeftAmount = LBConverter.ToDecimal(strCustomerLeftAmount);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            this.txtReceiveType.DataSource = LB.Common.LBConst.GetConstData("ReceiveType");//收款方式
            this.txtReceiveType.DisplayMember = "ConstText";
            this.txtReceiveType.ValueMember = "ConstValue";

            this.txtReceiveType.SelectedValue = ReceiveType;

            this.txtReceiveType.SelectedValueChanged += TxtReceiveType_SelectedValueChanged;

            SetReceiveTypeVisible();
        }

        private void TxtReceiveType_SelectedValueChanged(object sender, EventArgs e)
        {
            SetReceiveTypeVisible();
        }

        private void SetReceiveTypeVisible()
        {
            ReceiveType = LBConverter.ToInt32(this.txtReceiveType.SelectedValue);
            lblWeixin.Visible = false;
            if (ReceiveType == 0 || ReceiveType == 5)//现金
            {
                lblPayAmount.Visible= this.txtPayAmount.Visible=this.lblNeedPay.Visible=this.lblNeedPayAmount.Visible = false;
                if(ReceiveType == 5)
                {
                    lblWeixin.Visible = true;
                    this.lblAmount.Text = (decAmount + decAmount * (decimal)0.005).ToString();
                }
            }
            else if (ReceiveType == 1)//预付
            {
                lblPayAmount.Visible = this.txtPayAmount.Visible = true;
                if (decAmount - decCustomerLeftAmount > 0)
                {
                    this.lblNeedPayAmount.Text = (decAmount - decCustomerLeftAmount).ToString("0.00");
                    this.lblNeedPay.Visible = this.lblNeedPayAmount.Visible = true;
                }
                else
                {
                    this.lblNeedPay.Visible = this.lblNeedPayAmount.Visible = false;
                }
            }
            else if (ReceiveType == 2)//挂账
            {
                lblPayAmount.Visible = this.txtPayAmount.Visible = this.lblNeedPay.Visible = this.lblNeedPayAmount.Visible = false;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                IsSubmit = true;
                ReceiveType = LBConverter.ToInt32(this.txtReceiveType.SelectedValue);
                decimal decPayAmount = 0;//充值金额
                if (ReceiveType == 0)//现金
                {
                    decPayAmount = decAmount;
                }
                else if (ReceiveType == 5)//微信支付宝
                {
                    decPayAmount = decAmount+ decAmount * (decimal)0.005;
                }
                else if (ReceiveType == 1)//预付
                {
                    decimal.TryParse(this.txtPayAmount.Text, out decPayAmount);
                }
                else if (ReceiveType == 2)//挂账
                {
                    decimal.TryParse(this.txtPayAmount.Text, out decPayAmount);
                }

                if (decPayAmount > 0)
                {
                    CustomerPayAmount = decPayAmount;
                }

                this.Close();
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
                IsSubmit = false;
                this.Close();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
