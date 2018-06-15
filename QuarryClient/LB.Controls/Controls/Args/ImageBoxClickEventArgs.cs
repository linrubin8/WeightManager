using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace LB.Controls.Args
{
	public class ImageBoxClickEventArgs : HandledEventArgs
	{
		private Image mimgNormalImage = null;

		public Image NormalImage
		{
			get
			{
				return mimgNormalImage;
			}
			set
			{
				mimgNormalImage = value;
			}
		}
	}
}
