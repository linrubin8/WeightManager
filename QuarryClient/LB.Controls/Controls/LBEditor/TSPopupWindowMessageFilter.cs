using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LB.Controls.LBEditor
{
	class TSPopupWindowMessageFilter : IMessageFilter
	{
		private const int WM_LBUTTONDOWN = 0x201;
		private const int WM_RBUTTONDOWN = 0x204;
		private const int WM_MBUTTONDOWN = 0x207;
		private const int WM_NCLBUTTONDOWN = 0x0A1;
		private const int WM_NCRBUTTONDOWN = 0x0A4;
		private const int WM_NCMBUTTONDOWN = 0x0A7;

		/// <summary>
		/// The popup form
		/// </summary>
		private Form popup = null;

		private Control mtxtBox = null;

		/// <summary>
		/// Constructs a new instance of this class and sets the owning
		/// object.
		/// </summary>
		/// <param name="owner">The <see cref="PopupWindowHelper"/> object
		/// which owns this class.</param>
		public TSPopupWindowMessageFilter( Form popupW, Control textbox )
		{
			popup = popupW;
			mtxtBox = textbox;
			// MessageBox.Show(popup.Bounds.ToString());
			// MessageBox.Show(textbox.Bounds.ToString());
		}

		/// <summary>
		/// Gets/sets the popup form which is being displayed.
		/// </summary>
		public Form Popup
		{
			get
			{
				return this.popup;
			}
			set
			{
				this.popup = value;
			}
		}

		/// <summary>
		/// Checks the message loop for mouse messages whilst the popup
		/// window is displayed.  If one is detected the position is
		/// checked to see if it is outside the form, and the owner
		/// is notified if so.
		/// </summary>
		/// <param name="m">Windows Message about to be processed by the
		/// message loop</param>
		/// <returns><c>true</c> to filter the message, <c>false</c> otherwise.
		/// This implementation always returns <c>false</c>.</returns>
		public bool PreFilterMessage( ref Message m )
		{
			try
			{
				if( this.popup != null )
				{
					switch( m.Msg )
					{
						case WM_LBUTTONDOWN:
						case WM_RBUTTONDOWN:
						case WM_MBUTTONDOWN:
						case WM_NCLBUTTONDOWN:
						case WM_NCRBUTTONDOWN:
						case WM_NCMBUTTONDOWN:
							OnMouseDown();
							break;
					}
				}
			}
			catch
			{
			}

			return false;
		}

		/// <summary>
		/// Checks the mouse location and calls the OnCancelPopup method
		/// if the mouse is outside the popup form.		
		/// </summary>
		private void OnMouseDown()
		{
			if( mtxtBox == null || mtxtBox.IsDisposed )
			{
				Application.RemoveMessageFilter( this );
			}

			// Get the cursor location
			Point cursorPos = Cursor.Position;
			// Check if it is within the popup form or textbox
			if( !popup.Bounds.Contains( cursorPos ) )
			{
				if( !mtxtBox.Bounds.Contains( mtxtBox.Parent.PointToClient( cursorPos ) ) )
				{
					popup.Hide();
				}
			}
		}
	}
}
