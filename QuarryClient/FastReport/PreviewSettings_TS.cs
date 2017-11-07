using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using FastReport.TypeEditors;

namespace FastReport
{
	[Flags]
	[Editor( typeof( FlagsEditor ), typeof( UITypeEditor ) )]
	public enum enExportButtons
	{
		Html = 1,
		Pdf = 2,
		Office = 4,
        Image = 8,
        Xps = 16,
        Frx = 32,

        All = Html | Pdf | Office | Image | Xps | Frx
    }

	public partial class PreviewSettings
	{
		private enExportButtons FExportButtons = enExportButtons.All;

		[DefaultValue( enExportButtons.All )]
		public enExportButtons ExportButtons
		{
			get
			{
				return FExportButtons;
			}
			set
			{
				FExportButtons = value;
			}
		}
	}
}
