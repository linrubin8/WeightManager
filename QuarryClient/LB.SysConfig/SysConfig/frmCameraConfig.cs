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

namespace LB.SysConfig.SysConfig
{
    public partial class frmCameraConfig : LBUIPageBase
    {
        public frmCameraConfig()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ReadFieldValue();

            this.FormClosing += FrmCameraConfig_FormClosing;
        }

        private void FrmCameraConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                viewCamera1.CloseCamera();
                viewCamera2.CloseCamera();
                viewCamera3.CloseCamera();
                viewCamera4.CloseCamera();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void ReadFieldValue()
        {
            DataTable dtCamera = ExecuteSQL.CallView(122, "", "MachineName='"+ LoginInfo.MachineName + "'", "");
            if (dtCamera.Rows.Count > 0)
            {
                DataRow dr = dtCamera.Rows[0];
                this.txtIP1.Text = dr["IPAddress1"].ToString().TrimEnd();
                this.txtPort1.Text =LBConverter.ToInt32( dr["Port1"]).ToString();
                this.txtAccount1.Text = dr["Account1"].ToString().TrimEnd();
                this.txtPassword1.Text = dr["Password1"].ToString().TrimEnd();
                this.txtIP2.Text = dr["IPAddress2"].ToString().TrimEnd();
                this.txtPort2.Text = LBConverter.ToInt32(dr["Port2"]).ToString();
                this.txtAccount2.Text = dr["Account2"].ToString().TrimEnd();
                this.txtPassword2.Text = dr["Password2"].ToString().TrimEnd();
                this.txtIP3.Text = dr["IPAddress3"].ToString().TrimEnd();
                this.txtPort3.Text = LBConverter.ToInt32(dr["Port3"]).ToString();
                this.txtAccount3.Text = dr["Account3"].ToString().TrimEnd();
                this.txtPassword3.Text = dr["Password3"].ToString().TrimEnd();
                this.txtIP4.Text = dr["IPAddress4"].ToString().TrimEnd();
                this.txtPort4.Text = LBConverter.ToInt32(dr["Port4"]).ToString();
                this.txtAccount4.Text = dr["Account4"].ToString().TrimEnd();
                this.txtPassword4.Text = dr["Password4"].ToString().TrimEnd();

                this.cbUserCamera1.Checked = LBConverter.ToBoolean(dr["UseCamera1"]);
                this.cbUserCamera2.Checked = LBConverter.ToBoolean(dr["UseCamera2"]);
                this.cbUserCamera3.Checked = LBConverter.ToBoolean(dr["UseCamera3"]);
                this.cbUserCamera4.Checked = LBConverter.ToBoolean(dr["UseCamera4"]);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("MachineName", enLBDbType.String, LoginInfo.MachineName));
                parmCol.Add(new LBParameter("IPAddress1", enLBDbType.String, this.txtIP1.Text));
                parmCol.Add(new LBParameter("Port1", enLBDbType.Int32, LBConverter.ToInt32(this.txtPort1.Text)));
                parmCol.Add(new LBParameter("Account1", enLBDbType.String, this.txtAccount1.Text));
                parmCol.Add(new LBParameter("Password1", enLBDbType.String, this.txtPassword1.Text));
                parmCol.Add(new LBParameter("IPAddress2", enLBDbType.String, this.txtIP2.Text));
                parmCol.Add(new LBParameter("Port2", enLBDbType.Int32, LBConverter.ToInt32(this.txtPort2.Text)));
                parmCol.Add(new LBParameter("Account2", enLBDbType.String, this.txtAccount2.Text));
                parmCol.Add(new LBParameter("Password2", enLBDbType.String, this.txtPassword2.Text));
                parmCol.Add(new LBParameter("IPAddress3", enLBDbType.String, this.txtIP3.Text));
                parmCol.Add(new LBParameter("Port3", enLBDbType.Int32, LBConverter.ToInt32(this.txtPort3.Text)));
                parmCol.Add(new LBParameter("Account3", enLBDbType.String, this.txtAccount3.Text));
                parmCol.Add(new LBParameter("Password3", enLBDbType.String, this.txtPassword3.Text));
                parmCol.Add(new LBParameter("IPAddress4", enLBDbType.String, this.txtIP4.Text));
                parmCol.Add(new LBParameter("Port4", enLBDbType.Int32, LBConverter.ToInt32(this.txtPort4.Text)));
                parmCol.Add(new LBParameter("Account4", enLBDbType.String, this.txtAccount4.Text));
                parmCol.Add(new LBParameter("Password4", enLBDbType.String, this.txtPassword4.Text));
                parmCol.Add(new LBParameter("UseCamera1", enLBDbType.Boolean, this.cbUserCamera1.Checked?1:0));
                parmCol.Add(new LBParameter("UseCamera2", enLBDbType.Boolean, this.cbUserCamera2.Checked ? 1 : 0));
                parmCol.Add(new LBParameter("UseCamera3", enLBDbType.Boolean, this.cbUserCamera3.Checked ? 1 : 0));
                parmCol.Add(new LBParameter("UseCamera4", enLBDbType.Boolean, this.cbUserCamera4.Checked ? 1 : 0));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(13900, parmCol, out dsReturn, out dictValue);

                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnStart1_Click(object sender, EventArgs e)
        {
            try
            {
                viewCamera1.IPAddress = this.txtIP1.Text;
                viewCamera1.Port = LBConverter.ToInt32(this.txtPort1.Text);
                viewCamera1.Account = this.txtAccount1.Text;
                viewCamera1.Password = this.txtPassword1.Text;

                viewCamera1.OpenCamera(6);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnStart2_Click(object sender, EventArgs e)
        {
            try
            {
                viewCamera2.IPAddress = this.txtIP2.Text;
                viewCamera2.Port = LBConverter.ToInt32(this.txtPort2.Text);
                viewCamera2.Account = this.txtAccount2.Text;
                viewCamera2.Password = this.txtPassword2.Text;

                viewCamera2.OpenCamera(7);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnStart3_Click(object sender, EventArgs e)
        {
            try
            {
                viewCamera3.IPAddress = this.txtIP3.Text;
                viewCamera3.Port = LBConverter.ToInt32(this.txtPort3.Text);
                viewCamera3.Account = this.txtAccount3.Text;
                viewCamera3.Password = this.txtPassword3.Text;

                viewCamera3.OpenCamera(8);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnStart4_Click(object sender, EventArgs e)
        {
            try
            {
                viewCamera4.IPAddress = this.txtIP4.Text;
                viewCamera4.Port = LBConverter.ToInt32(this.txtPort4.Text);
                viewCamera4.Account = this.txtAccount4.Text;
                viewCamera4.Password = this.txtPassword4.Text;

                viewCamera4.OpenCamera(9);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
