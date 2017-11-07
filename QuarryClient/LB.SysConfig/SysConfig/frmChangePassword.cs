using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using LB.WinFunction;

namespace LB.SysConfig
{
    public partial class frmChangePassword : LBForm
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //先校验密码是否为空
                if (txtOldPassword.Text.TrimEnd() == "")
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("【旧密码】不能为空！");
                    return;
                }

                //校验密码是否为空
                if (this.txtNewPassword.Text.TrimEnd() == "")
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("【新密码】不能为空！");
                    return;
                }

                if (this.txtConfirmPassword.Text.TrimEnd() != this.txtNewPassword.Text.TrimEnd())
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("【新密码】与【确认密码】不一致，请确认两次输入的密码一样！");
                    return;
                }

                string strNewPassword = this.txtConfirmPassword.Text.TrimEnd();

                //校验旧密码是否正确
                bool bolVerifyPass = LoginInfo.VerifyPassword(LoginInfo.UserID, txtOldPassword.Text.TrimEnd());
                if (bolVerifyPass)//校验成功
                {
                    //修改密码
                    DataTable dtSP = new DataTable();
                    dtSP.Columns.Add("UserID", typeof(int));
                    dtSP.Columns.Add("UserPassword", typeof(string));
                    DataRow drNew = dtSP.NewRow();
                    drNew["UserID"] = LoginInfo.UserID;
                    drNew["UserPassword"] = LoginInfo.EncryptPassword(strNewPassword);
                    dtSP.Rows.Add(drNew);
                    dtSP.AcceptChanges();

                    DataSet dsReturn;
                    DataTable dtOut;
                    LB.WinFunction.ExecuteSQL.CallSP(10003, dtSP, out dsReturn, out dtOut);

                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("密码修改成功！");
                    this.Close();
                }
                else
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("旧密码不正确！");
                }
            }
            catch (Exception ex)
            {
                LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
