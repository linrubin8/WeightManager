using LB.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LB.MainForm.MainForm
{
    public partial class frmReceiveSerialData : Form
    {
        public frmReceiveSerialData()
        {
            InitializeComponent();
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
        }

        public void AppendReceiveData(string strData)
        {
            try
            {
                richTextBox1.Invoke(new ThreadStart(delegate () {
                    this.richTextBox1.AppendText(strData);
                    this.richTextBox1.AppendText("\n");
                }));
                
            }
            catch(Exception ex)
            {

            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            
        }
    }
}
