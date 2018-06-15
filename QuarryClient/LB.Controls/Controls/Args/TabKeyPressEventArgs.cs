using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Controls.Args
{
	public delegate void TabKeyPressEventHandler( object sender, TabKeyPressEventArgs e );

	public class TabKeyPressEventArgs
	{
		private bool _handled = false;

		public bool Handled
		{
			get
			{
				return _handled;
			}
			set
			{
				_handled = value;
			}
		}
	}
}
