using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FastReport.Design
{
	public delegate void GetItemImageEventHandler( object sender, GetItemImageEventArgs e );

	public class GetItemImageEventArgs
	{
		private long _ItemNCodeID;
		public long ItemNCodeID
		{
			get
			{
				return _ItemNCodeID;
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

		public GetItemImageEventArgs( long itemNCodeID )
		{
			_ItemNCodeID = itemNCodeID;
		}
	}
}
