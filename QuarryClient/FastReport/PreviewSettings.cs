using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.TypeConverters;
using FastReport.TypeEditors;
using FastReport.Preview;

namespace FastReport
{
	/// <summary>
	/// Specifies the set of buttons available in the preview.
	/// </summary>
	[Flags]
	[Editor( typeof( FlagsEditor ), typeof( UITypeEditor ) )]
	public enum PreviewButtons
	{
		/// <summary>
		/// No buttons visible.
		/// </summary>
		None = 0,

		/// <summary>
		/// The "Print" button is visible.
		/// </summary>
		Print = 1,

		/// <summary>
		/// The "Open" button is visible.
		/// </summary>
		Open = 2,

		/// <summary>
		/// The "Save" button is visible.
		/// </summary>
		Save = 4,

		/// <summary>
		/// The "Email" button is visible.
		/// </summary>
		Email = 8,

		/// <summary>
		/// The "Find" button is visible.
		/// </summary>
		Find = 16,

		/// <summary>
		/// The zoom buttons are visible.
		/// </summary>
		Zoom = 32,

		/// <summary>
		/// The "Outline" button is visible.
		/// </summary>
		Outline = 64,

		/// <summary>
		/// The "Page setup" button is visible.
		/// </summary>
		PageSetup = 128,

		/// <summary>
		/// The "Edit" button is visible.
		/// </summary>
		Edit = 256,

		/// <summary>
		/// The "Watermark" button is visible.
		/// </summary>
		Watermark = 512,

		/// <summary>
		/// The page navigator buttons are visible.
		/// </summary>
		Navigator = 1024,

		/// <summary>
		/// The "Close" button is visible.
		/// </summary>
		Close = 2048,

		/// <summary>
		/// The "Design" button is visible.
		/// </summary>
		Design = 4096,

		/// <summary>
		/// The "Fax" button is visible.
		/// </summary>
		Fax = 8192,  // Simon: 增加 Fax

		/// <summary>
		/// The "Ambassador" button is visible.
		/// </summary>
		Ambassador = 16384,  // Simon: 增加 Ambassador

		/// <summary>
		/// All buttons are visible.
		/// </summary>
		// if you add something to this enum, don't forget to correct "All" member
		All = Print | Open | Save | Email | Find | Zoom | Outline | PageSetup | Edit |
		 Watermark | Navigator | Close | Fax | Ambassador
	}

	/// <summary>
	/// Specifies the set of export buttons available in the preview.
	/// </summary>
	[Flags]
	[Editor( typeof( FlagsEditor ), typeof( UITypeEditor ) )]
	public enum PreviewExports
	{
		/// <summary>
		/// No exports visible.
		/// </summary>
		None = 0,
		/// <summary>
		/// The "RTFExport" button is visible.
		/// </summary>
		RTFExport = 1,
		/// <summary>
		/// The "ImageExport" button is visible.
		/// </summary>
		ImageExport = 2,
		/// <summary>
		/// The "HTMLExport" button is visible.
		/// </summary>
		HTMLExport = 4,
		/// <summary>
		/// The "MHTExport" button is visible.
		/// </summary>
		MHTExport = 8,
		/// <summary>
		/// The "ODTExport" button is visible.
		/// </summary>
		ODTExport = 16,
		/// <summary>
		/// The "ODSExport" button is visible.
		/// </summary>
		ODSExport = 32,
		/// <summary>
		/// The "PDFExport" button is visible.
		/// </summary>
		PDFExport = 64,
		/// <summary>
		/// The "TextExport" button is visible.
		/// </summary>
		TextExport = 128,
		/// <summary>
		/// The "CSVExport" button is visible.
		/// </summary>
		CSVExport = 256,
		/// <summary>
		/// The "XMLExport" export button is visible.
		/// </summary>
		XMLExport = 512,
		/// <summary>
		/// The "Excel2007Export" button is visible.
		/// </summary>
		Excel2007Export = 1024,
		/// <summary>
		/// The "PowerPoint2007Export" button is visible.
		/// </summary>
		PowerPoint2007Export = 2048,
		/// <summary>
		/// The "XAMLExport" button is visible.
		/// </summary>
		XAMLExport = 4096,
		/// <summary>
		/// The "SVGExport" button is visible.
		/// </summary>
		SVGExport = 8192,
		/// <summary>
		/// The "PPMLExport" button is visible.
		/// </summary>
		PPMLExport = 16384,
		/// <summary>
		/// The "PSExport" button is visible.
		/// </summary>
		PSExport = 32768,
		/// <summary>
		/// The "Word2007Export" button is visible.
		/// </summary>
		Word2007Export = 65536,
		/// <summary>
		/// The "XPSExport" export button is visible.
		/// </summary>
		XPSExport = 131072,
		/// <summary>
		/// The "JsonExport" button is visible.
		/// </summary>
		JsonExport = 262144,
		/// <summary>
		/// The "DBFExport" button is visible.
		/// </summary>
		DBFExport = 524288,
		/// <summary>
		/// The All export buttons is visible.
		/// </summary>
		All = RTFExport | ImageExport | HTMLExport | MHTExport | ODTExport | ODSExport | PDFExport | TextExport |
		CSVExport | XMLExport | Excel2007Export | PowerPoint2007Export | XAMLExport | SVGExport | PPMLExport |
		PSExport | Word2007Export | XPSExport | JsonExport | DBFExport
	}

	/// <summary>
	/// Specifies the set of export in clouds buttons available in the preview.
	/// </summary>
	[Flags]
	[Editor( typeof( FlagsEditor ), typeof( UITypeEditor ) )]
	public enum PreviewClouds
	{
		/// <summary>
		/// No exports in clouds visible.
		/// </summary>
		None = 0,
		/// <summary>
		/// The "Box" button is visible.
		/// </summary>
		Box = 1,
		/// <summary>
		/// The "Dropbox" button is visible.
		/// </summary>
		Dropbox = 2,
		/// <summary>
		/// The "FastCloud" button is visible.
		/// </summary>
		FastCloud = 4,
		/// <summary>
		/// The "Ftp" button is visible.
		/// </summary>
		Ftp = 8,
		/// <summary>
		/// The "GoogleDrive" button is visible.
		/// </summary>
		GoogleDrive = 16,
		/// <summary>
		/// The "SkyDrive" button is visible.
		/// </summary>
		SkyDrive = 32,
		/// <summary>
		/// The "Xmpp" button is visible.
		/// </summary>
		Xmpp = 64,
		/// <summary>
		/// The All export in clouds buttons is visible.
		/// </summary>
		All = Box | Dropbox | FastCloud | Ftp | GoogleDrive | SkyDrive | Xmpp
	}

	/// <summary>
	/// Contains some settings of the preview window.
	/// </summary>
	[TypeConverter( typeof( FRExpandableObjectConverter ) )]
	public partial class PreviewSettings
	{
		#region Fields
		private PreviewButtons FButtons;
		private PreviewExports FExports;
		private PreviewClouds FClouds;
		private int FPagesInCache;
		private bool FShowInTaskbar;
		private bool FTopMost;
		private Icon FIcon;
		private string FText;
		private bool FFastScrolling;
		private bool FAllowPrintToFile;
		#endregion

		#region Properties
		/// <summary>
		/// Occurs when the standard preview window opened.
		/// </summary>
		/// <remarks>
		/// You may use this event to change the standard preview window, for example, add an own button to it.
		/// The <b>sender</b> parameter in this event is the <b>PreviewControl</b>.
		/// </remarks>
		public event EventHandler PreviewOpened;

		/// <summary>
		/// Gets or sets a set of buttons that will be visible in the preview's toolbar.
		/// </summary>
		/// <remarks>
		/// Here is an example how you can disable the "Print" and "EMail" buttons:
		/// Config.PreviewSettings.Buttons = PreviewButtons.Open | 
		/// PreviewButtons.Save | 
		/// PreviewButtons.Find | 
		/// PreviewButtons.Zoom | 
		/// PreviewButtons.Outline | 
		/// PreviewButtons.PageSetup | 
		/// PreviewButtons.Edit | 
		/// PreviewButtons.Watermark | 
		/// PreviewButtons.Navigator | 
		/// PreviewButtons.Close; 
		/// </remarks>
		[DefaultValue( PreviewButtons.All )]
		public PreviewButtons Buttons
		{
			get
			{
				return FButtons;
			}
			set
			{
				FButtons = value;
			}
		}

		/// <summary>
		/// Specifies the set of exports that will be available in the preview's "save" menu.
		/// </summary>
		[DefaultValue( PreviewExports.All )]
		public PreviewExports Exports
		{
			get
			{
				return FExports;
			}
			set
			{
				FExports = value;
			}
		}

		/// <summary>
		/// Specifies the set of exports in clouds that will be available in the preview's "save" menu.
		/// </summary>
		[DefaultValue( PreviewClouds.All )]
		public PreviewClouds Clouds
		{
			get
			{
				return FClouds;
			}
			set
			{
				FClouds = value;
			}
		}

		/// <summary>
		/// Gets or sets the number of prepared pages that can be stored in the memory cache during preview.
		/// </summary>
		/// <remarks>
		/// Decrease this value if your prepared report contains a lot of pictures. This will
		/// save the RAM memory.
		/// </remarks>
		[DefaultValue( 50 )]
		public int PagesInCache
		{
			get
			{
				return FPagesInCache;
			}
			set
			{
				FPagesInCache = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the preview window is displayed in the Windows taskbar. 
		/// </summary>
		[DefaultValue( false )]
		public bool ShowInTaskbar
		{
			get
			{
				return FShowInTaskbar;
			}
			set
			{
				FShowInTaskbar = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the preview window should be displayed as a topmost form. 
		/// </summary>
		[DefaultValue( false )]
		public bool TopMost
		{
			get
			{
				return FTopMost;
			}
			set
			{
				FTopMost = value;
			}
		}

		/// <summary>
		/// Gets or sets the icon for the preview window.
		/// </summary>
		public Icon Icon
		{
			get
			{
				if( FIcon == null )
					FIcon = ResourceLoader.GetIcon( "icon16.ico" );
				return FIcon;
			}
			set
			{
				FIcon = value;
			}
		}

		/// <summary>
		/// Gets or sets the text for the preview window.
		/// </summary>
		/// <remarks>
		/// If no text is set, the default text "Preview" will be used.
		/// </remarks>
		public string Text
		{
			get
			{
				return FText;
			}
			set
			{
				FText = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the fast scrolling method should be used.
		/// </summary>
		/// <remarks>
		/// If you enable this property, the gradient background will be disabled.
		/// </remarks>
		[DefaultValue( false )]
		public bool FastScrolling
		{
			get
			{
				return FFastScrolling;
			}
			set
			{
				FFastScrolling = value;
			}
		}

		/// <summary>
		/// Enables or disables the "Print to file" feature in the print dialog.
		/// </summary>
		[DefaultValue( true )]
		public bool AllowPrintToFile
		{
			get
			{
				return FAllowPrintToFile;
			}
			set
			{
				FAllowPrintToFile = value;
			}
		}
		#endregion

		internal void OnPreviewOpened( PreviewControl sender )
		{
			if( PreviewOpened != null )
				PreviewOpened( sender, EventArgs.Empty );
		}

		/// <summary>
		/// Initializes a new instance of the <b>PreviewSettings</b> class with default settings. 
		/// </summary>
		public PreviewSettings()
		{
			FButtons = PreviewButtons.All;
			FExports = PreviewExports.All;
			FClouds = PreviewClouds.All;
			FPagesInCache = 50;
			FText = "";
			FAllowPrintToFile = true;
		}
	}
}