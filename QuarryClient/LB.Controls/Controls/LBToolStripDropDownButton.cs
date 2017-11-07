using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls
{
    public partial class LBToolStripDropDownButton : ToolStripDropDownButton
    {
        private string _LBPermissionCode = "";
        [Description("权限名称")]//
        public string LBPermissionCode
        {
            set
            {
                _LBPermissionCode = value;
            }
            get
            {
                return _LBPermissionCode;
            }
        }

        public LBToolStripDropDownButton()
        {
            InitializeComponent();
        }

        public LBToolStripDropDownButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

    }
}
