using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LB.Controls;

namespace LB.MI.MI
{
    public partial class frmSaleCarInOutCancel : LBUIPageBase
    {
        public bool IsAllowCancel = false;
        public string CancelDesc
        {
            get
            {
                return this.txtCancelDesc.Text;
            }
        }
        public frmSaleCarInOutCancel(string strCarNum)
        {
            InitializeComponent();
            this.lblCnacelCarNum.Text = strCarNum;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.txtCancelDesc.Text.TrimEnd() == "")
            {
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("请填写作废原因！");
                return;
            }
            IsAllowCancel = true;
            this.Close();
        }
    }
}
