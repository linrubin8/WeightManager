using LB.Web.Encrypt;
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
                    //先校验注册码是否合法
                    string strDiskID = LBEncrypt.GetDiskID();//硬盘ID
                    try
                    {
                        string strDecrypt = LBEncrypt.DecryptAes(this.txtRegister.Text, "linrubin" + strDiskID);
                        if (!strDecrypt.Contains("ProductType"))
                        {
                            throw new Exception("注册码不合法！");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("注册码不合法！");
                    }

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
