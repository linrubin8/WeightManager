using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace LB.Controls.LBEditor
{
	class TSTransparentControl : Control
	{
		private const short WS_EX_TRANSPARENT = 0x20;

		public TSTransparentControl()
		{
			this.SetStyle( ControlStyles.Opaque, true );
			this.UpdateStyles();
			//this.AutoScaleMode = AutoScaleMode.None;
			this.TabStop = false;
		}

		protected override System.Windows.Forms.CreateParams CreateParams
		{
			[SecurityPermission( SecurityAction.LinkDemand, UnmanagedCode = true )]
			get
			{
				System.Windows.Forms.CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x20;
				return cp;
			}
		}
	}
}
