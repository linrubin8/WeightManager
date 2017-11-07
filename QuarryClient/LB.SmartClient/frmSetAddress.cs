using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LB.SmartClient
{
    public partial class frmSetAddress : Form
    {
        public frmSetAddress()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string strConfigName = "WebLink.ini";
            string strConfigPath = Path.Combine(Application.StartupPath, strConfigName);
            IniClass ini = new SmartClient.IniClass(strConfigPath);
            this.txtAddress.Text = ini.ReadValue("Link", "server");
            this.txtPort.Text = ini.ReadValue("Link", "port");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strConfigName = "WebLink.ini";
                string strConfigPath = Path.Combine(Application.StartupPath, strConfigName);
                IniClass ini = new SmartClient.IniClass(strConfigPath);
                ini.WriteValue("Link", "server", this.txtAddress.Text);
                ini.WriteValue("Link", "port", this.txtPort.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
