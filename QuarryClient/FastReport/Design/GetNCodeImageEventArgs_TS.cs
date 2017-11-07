using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FastReport.Design
{
	public delegate void GetNCodeImageEventHandler( object sender, GetNCodeImageEventArgs e );

	public class GetNCodeImageEventArgs
	{
		private string _NCodeName;
		public string NCodeName
		{
			get
			{
				return _NCodeName;
			}
		}

		private int _NCodeClassID;
		public int NCodeClassID
		{
			get
			{
				return _NCodeClassID;
			}
		}

		private Image _NCodeImage = null;
		public Image NCodeImage
		{
			get
			{
				return _NCodeImage;
			}
			set
			{
				_NCodeImage = value;
			}
		}

		public GetNCodeImageEventArgs( string nCodeName, int nCodeClassID )
		{
			_NCodeName = nCodeName;
			_NCodeClassID = nCodeClassID;
		}
	}
}
