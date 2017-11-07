using LB.Common;
using LB.Controls.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls
{
    public partial class LBToolStripButton : ToolStripButton
    {
        private string _LBPermissionCode = "";
        [Description("权限校验码")]//
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

        public LBToolStripButton()
        {
            InitializeComponent();
        }

        

        public LBToolStripButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnClick(EventArgs e)
        {
            try
            {
                LBPermission.VerifyUserPermission(this.Text,LBPermissionCode);
                //LBLog.InsertSysLog(LBPermissionCode);
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
                return;
            }
            base.OnClick(e);
        }
    }
}
