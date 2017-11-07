using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.MI.MI.MIControls
{
    public partial class SaleChangeItem : UserControl
    {
        public SaleChangeItem(string strTitle,string strChangeFrom,string strChangeTo)
        {
            InitializeComponent();

            this.lblTitle.Text = strTitle + ":";
            this.lblChangeFrom.Text = strChangeFrom;
            this.lblChangeTo.Text = strChangeTo;
        }
    }
}
