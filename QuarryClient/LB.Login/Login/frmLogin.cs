using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using LB.WinFunction;
using System.Threading;

namespace LB.Login
{
    public partial class frmLogin : LBForm
    {
        

        public frmLogin()
        {
            InitializeComponent();

            this.txtLoginName.DisplayMember = "LoginName";
            LoginInfo.IsVerifySuccess = false;
            LoginInfo.LoginName = "";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //IFaxBusiness.IMyFaxBusiness webservice = (IFaxBusiness.IMyFaxBusiness)Activator.GetObject(typeof(IFaxBusiness.IMyFaxBusiness),
            // "http://211.149.203.13:2017/LBProject");
            //webservice.SendFax("eee");
            SetUserDataSource();
        }

        #region -- 读取所有用户名称 --

        private void SetUserDataSource()
        {
            this.txtLoginName.DataSource = GetAllUserName();
        }

        private DataTable GetAllUserName()
        {
            DataTable dtUser = ExecuteSQL.CallView(100, "UserID,LoginName", "isnull(ForbidLogin,0)=0 and IsManager=0", "");
            return dtUser;
        }


        #endregion -- 读取所有用户名称 --

        private void btnLoginIn_Click(object sender, EventArgs e)
        {
            try
            {
                //校验权限
                bool IsRegister;
                DateTime DeadLine;
                int ProductType;
                string RegisterInfoJson;
                ExecuteSQL.ReadRegister(out IsRegister,out ProductType, out RegisterInfoJson, out DeadLine);
                if (ProductType != 0 || !IsRegister)
                {
                    throw new Exception("该系统未注册，请与供应商联系！");
                }
                else if (DeadLine.Subtract(DateTime.Now).TotalHours <= 0)
                {
                    throw new Exception("该系统注册已过期，请与供应商联系！");
                }

                VerisyIsEmpty();

                string strLoginName = this.txtLoginName.Text.TrimEnd();
                string strPassword = this.txtPassword.Text.TrimEnd();
                //Thread.Sleep(2000);
                //throw new Exception("无法连接服务器，请检查网络是否畅通！");
                bool bolPass = LoginInfo.VerifyLogin(strLoginName, strPassword);
                
                if (bolPass)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        /// <summary>
        /// 校验用户名称和密码是否为空
        /// </summary>
        private void VerisyIsEmpty()
        {
            if (this.txtLoginName.Text.TrimEnd() == "")
            {
                throw new Exception("用户名称不能为空！");
            }

            if (this.txtPassword.Text.TrimEnd() == "")
            {
                throw new Exception("用户密码不能为空！");
            }

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.btnLoginIn.PerformClick();
                }
            }
            catch (Exception ex)
            {
                LBCommonHelper.DealWithErrorMessage(ex);
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
                LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
