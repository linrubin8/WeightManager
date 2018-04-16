using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Web.ServiceMonitor
{
    public partial class frmDisk : Form
    {
        public frmDisk()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                this.txtDisk.Text = LB.Web.Encrypt.LBEncrypt.GetDiskID();
                this.txtRegister.Text = LB.Web.Encrypt.LBEncrypt.GetRegisterCode();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.txtDisk.Text.TrimEnd()!="" && this.txtRegister.Text.TrimEnd() != "")
                {
                    string strIniPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LBTSR.ini");
                    IniClass iniClass = new IniClass(strIniPath);
                    iniClass.WriteValue("TSR", "value", this.txtRegister.Text.TrimEnd());

                    MessageBox.Show("注册成功！");
                    this.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
