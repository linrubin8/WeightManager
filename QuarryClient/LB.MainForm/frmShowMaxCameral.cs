using LB.Common.Camera;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.MainForm
{
    public partial class frmShowMaxCameral : Form
    {
        private ViewCamera mviewCamera;
        public frmShowMaxCameral(ViewCamera viewCamera)
        {
            InitializeComponent();
            mviewCamera = viewCamera;
            this.Controls.Add(viewCamera);
        }
    }
}
