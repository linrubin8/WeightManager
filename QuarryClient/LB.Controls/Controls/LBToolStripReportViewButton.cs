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
    public partial class LBToolStripReportViewButton : LBToolStripButton
    {
        private enToolStripType _ToolStripType = enToolStripType.EditReportButton;
        [Description("按钮类型")]//
        public enToolStripType ToolStripType
        {
            set
            {
                _ToolStripType = value;
            }
            get
            {
                return _ToolStripType;
            }
        }

        private long _ReportTypeID;
        public long ReportTypeID
        {
            get
            {
                return _ReportTypeID;
            }
            set
            {
                _ReportTypeID = value;
            }
        }

        public LBToolStripReportViewButton()
        {
            InitializeComponent();
        }

        public LBToolStripReportViewButton(IContainer container)
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
