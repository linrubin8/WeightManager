using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LB.Controls.LBEditor
{
	public class TSPopupContainer
	{
		private Control _ownerControl = null;
		private Control _showControl = null;
		private ToolStripDropDown _dropDown = new ToolStripDropDown();
		private ToolStripControlHost _host;

		public TSPopupContainer( Control ownerControl, Control showControl )
		{
			_ownerControl = ownerControl;
			_showControl = showControl;

			_host = new ToolStripControlHost( _showControl );
			_host.AutoSize = false;
			_host.BackColor = _dropDown.BackColor;
			_host.Control.Location = new Point( 0, 0 );

			_dropDown.BackColor = Color.White;
			_dropDown.AutoSize = false;
			_dropDown.AutoClose = true;
			_dropDown.Margin = Padding.Empty;
			_dropDown.Padding = Padding.Empty;
			_dropDown.Items.Clear();
			_dropDown.Items.Add( _host );
		}

		public void Show( int screenLeft, int screenRight )
		{
			UnsubscribeForm( _ownerControl );
			SubscribeForm( _ownerControl );

			Point screenLocation = new Point( screenLeft, screenRight );
			Show( screenLocation );
		}

		public void Show( Point screenLocation )
		{
			if( !_dropDown.Visible )
			{
				_host.Control.Location = new Point( 0, 0 );
				_dropDown.Size = new Size( _host.Width + 1, _host.Height + 1 );
				_dropDown.Show( screenLocation );
			}
			else
			{
				_dropDown.Show( screenLocation );
			}
		}

		public void Close()
		{
			_dropDown.Close();
		}

		void SubscribeForm( Control control )
		{
			if( control == null )
			{
				return;
			}

			Form form = control.FindForm();
			if( form == null )
			{
				return;
			}

			form.LocationChanged += new EventHandler( form_LocationChanged );
			form.ResizeBegin += new EventHandler( form_LocationChanged );
			form.FormClosing += new FormClosingEventHandler( form_FormClosing );
			form.LostFocus += new EventHandler( form_LocationChanged );
		}

		void UnsubscribeForm( Control control )
		{
			if( control == null )
			{
				return;
			}

			Form form = control.FindForm();
			if( form == null )
			{
				return;
			}

			form.LocationChanged -= new EventHandler( form_LocationChanged );
			form.ResizeBegin -= new EventHandler( form_LocationChanged );
			form.FormClosing -= new FormClosingEventHandler( form_FormClosing );
			form.LostFocus -= new EventHandler( form_LocationChanged );
		}

		private void form_FormClosing( object sender, FormClosingEventArgs e )
		{
			Close();
		}

		private void form_LocationChanged( object sender, EventArgs e )
		{
			Close();
		}
	}
}
