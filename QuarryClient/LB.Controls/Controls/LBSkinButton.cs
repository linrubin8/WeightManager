using CCWin.SkinControl;
using LB.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace LB.Controls
{
    public partial class LBSkinButton : SkinButton
    {
        private string _LBPermissionCode = "";
        public LBSkinButton()
        {
            InitializeComponent();
            LBInitializeComponent();
        }

        public LBSkinButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            LBInitializeComponent();
        }

        /// <summary>
        /// 权限校验码
        /// </summary>
       [Description("权限校验码")]
        public string LBPermissionCode
        {
            get
            {
                return _LBPermissionCode;
            }
            set
            {
                _LBPermissionCode = value;
            }
        }

        private void LBInitializeComponent()
        {
            this.BaseColor = System.Drawing.Color.LightGray;
            this.BorderColor = System.Drawing.Color.Gray;
        }

        protected override void OnClick(EventArgs e)
        {
            try
            {
                LBPermission.VerifyUserPermission(this.Text, LBPermissionCode);
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
