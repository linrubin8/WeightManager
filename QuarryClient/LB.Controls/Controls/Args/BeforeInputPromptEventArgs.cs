using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;

namespace LB.Controls.Args
{
	public delegate void BeforeInputPromptEventHandler( object sender, BeforeInputPromptEventArgs e );

	public class BeforeInputPromptEventArgs
	{
		private string _inputText;
		public string InputText
		{
			get
			{
				return _inputText;
			}
		}

		private DataView _promptData = null;
		public DataView PromptData
		{
			get
			{
				return _promptData;
			}
			set
			{
				_promptData = value;
			}
		}

		private bool _cancel = false;
		public bool Cancel
		{
			get
			{
				return _cancel;
			}
			set
			{
				_cancel = value;
			}
		}

		private int _popupWidth;
		public int PopupWidth
		{
			get
			{
				return _popupWidth;
			}
			set
			{
				_popupWidth = value;
			}
		}

		private Point _popupLocation = Point.Empty;
		public Point PopupLocation
		{
			get
			{
				return _popupLocation;
			}
			set
			{
				_popupLocation = value;
			}
		}

		public BeforeInputPromptEventArgs( string inputText, Point popupLocation, int popupWidth )
		{
			_inputText = inputText;
			_popupLocation = popupLocation;
			_popupWidth = popupWidth;
		}
	}
}
