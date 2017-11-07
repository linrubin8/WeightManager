using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.MainForm.MainForm.Temp
{
    public partial class frmImportCustomer : Form
    {
        public frmImportCustomer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] strs = this.richTextBox1.Text.Split('\n');

            StringBuilder strSQL = new StringBuilder();
            foreach (string ss in strs)
            {
                string[] ssss = ss.Split('\t');
                if (ssss.Length == 2)
                {
                    string strCustomer = ssss[0].TrimEnd();
                    string strAmount = ssss[1].TrimEnd();
                }
            }
        }
    }
}
