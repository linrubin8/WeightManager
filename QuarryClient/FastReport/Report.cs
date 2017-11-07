using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.ComponentModel.Design.Serialization;
using System.Text;
using System.Security;
using System.Drawing.Printing;
using FastReport.Utils;
using FastReport.Design;
using FastReport.Code;
using FastReport.Data;
using FastReport.Engine;
using FastReport.Forms;
using FastReport.Dialog;
using FastReport.Export;
using FastReport.Design.StandardDesigner;

namespace FastReport
{
	/// <summary>
	/// Specifies the language of the report's script.
	/// </summary>
	public enum Language
	{
		/// <summary>
		/// The C# language.
		/// </summary>
		CSharp,

		/// <summary>
		/// The VisualBasic.Net language.
		/// </summary>
		Vb
	}

	/// <summary>
	/// Specifies the quality of text rendering.
	/// </summary>
	public enum TextQuality
	{
		/// <summary>
		/// The default text quality, depends on system settings.
		/// </summary>
		Default,

		/// <summary>
		/// The regular quality.
		/// </summary>
		Regular,

		/// <summary>
		/// The "ClearType" quality.
		/// </summary>
		ClearType,

		/// <summary>
		/// The AntiAlias quality. This mode may be used to produce the WYSIWYG text.
		/// </summary>
		AntiAlias
	}


	/// <summary>
	/// Specifies the report operation.
	/// </summary>
	public enum ReportOperation
	{
		/// <summary>
		/// Specifies no operation.
		/// </summary>
		None,

		/// <summary>
		/// The report is running.
		/// </summary>
		Running,

		/// <summary>
		/// The report is printing.
		/// </summary>
		Printing,

		/// <summary>
		/// The report is exporting.
		/// </summary>
		Exporting
	}

	/// <summary>
	/// Represents a report object.
	/// </summary>
	/// <remarks>
	/// <para>The instance of this class contains a report. Here are some common 
	/// actions that can be performed with this object:</para>
	/// <list type="bullet">
	///   <item>
	///     <description>To load a report, use the <see cref="Load(string)"/>
	///     method or call static <see cref="FromFile"/> method. </description>
	///   </item>
	///   <item>
	///     <description>To save a report, call the <see cref="Save(string)"/> method.</description>
	///   </item>
	///   <item>
	///     <description>To register application dataset for use it in a report, call one of the
	///     <b>RegisterData</b> methods.</description>
	///   </item>
	///   <item>
	///     <description>To pass some parameter to a report, use the
	///     <see cref="SetParameterValue"/> method.</description>
	///   </item>
	///   <item>
	///     <description>To design a report, call the <see cref="Design()"/> method.</description>
	///   </item>
	///   <item>
	///     <description>To run a report and preview it, call the <see cref="Show()"/> method. 
	///     Another way is to call the <see cref="Prepare()"/> method, then call the 
	///     <see cref="ShowPrepared()"/> method.</description>
	///   </item>
	///   <item>
	///     <description>To run a report and print it, call the <see cref="Print"/> method.
	///     Another way is to call the <see cref="Prepare()"/> method, then call the 
	///     <see cref="PrintPrepared()"/> method.</description>
	///   </item>
	///   <item>
	///     <description>To load/save prepared report, use one of the <b>LoadPrepared</b> and
	///     <b>SavePrepared</b> methods.</description>
	///   </item>
	///   <item>
	///     <description>To set up some global properties, use the <see cref="Config"/> static class
	///     or <see cref="EnvironmentSettings"/> component that you can use in the Visual Studio IDE.
	///     </description>
	///   </item>
	/// </list>
	/// <para/>The report consists of one or several report pages (pages of the 
	/// <see cref="ReportPage"/> type) and/or dialog forms (pages of the <see cref="DialogPage"/> type). 
	/// They are stored in the <see cref="Pages"/> collection. In turn, each page may contain report
	/// objects. See the example below how to create a simple report in code.
	/// </remarks>
	/// <example>This example shows how to create a report instance, load it from a file, 
	/// register the application data, run and preview.
	/// <code>
	/// Report report = new Report();
	/// report.Load("reportfile.frx");
	/// report.RegisterData(application_dataset);
	/// report.Show();
	/// </code>
	/// <para/>This example shows how to create simple report in code.
	/// <code>
	/// Report report = new Report();
	/// // create the report page
	/// ReportPage page = new ReportPage();
	/// page.Name = "ReportPage1";
	/// // set paper width and height. Note: these properties are measured in millimeters.
	/// page.PaperWidth = 210;
	/// page.PaperHeight = 297;
	/// // add a page to the report
	/// report.Pages.Add(page);
	/// // create report title
	/// page.ReportTitle = new ReportTitleBand();
	/// page.ReportTitle.Name = "ReportTitle1";
	/// page.ReportTitle.Height = Units.Millimeters * 10;
	/// // create Text object and put it to the title
	/// TextObject text = new TextObject();
	/// text.Name = "Text1";
	/// text.Bounds = new RectangleF(0, 0, Units.Millimeters * 100, Units.Millimeters * 5);
	/// page.ReportTitle.Objects.Add(text);
	/// // create data band
	/// DataBand data = new DataBand();
	/// data.Name = "Data1";
	/// data.Height = Units.Millimeters * 10;
	/// // add data band to a page
	/// page.Bands.Add(data);
	/// </code>
	/// </example>
	[ToolboxItem( true ), ToolboxBitmap( typeof( Report ), "Resources.Report.bmp" )]
	[Designer( "FastReport.VSDesign.ReportComponentDesigner, FastReport.VSDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=db7e5ce63278458c, processorArchitecture=MSIL" )]
	[DesignerSerializer( "FastReport.VSDesign.ReportCodeDomSerializer, FastReport.VSDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=db7e5ce63278458c, processorArchitecture=MSIL", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" )]
	public partial class Report : Base, IParent, ISupportInitialize
	{
		#region Fields
		private PageCollection FPages;
		private Dictionary FDictionary;
		private ReportInfo FReportInfo;
		private string FBaseReport;
		private Report FBaseReportObject;
		private string FFileName;
		private string FScriptText;
		private Language FScriptLanguage;
		private bool FCompressed;
		private bool FUseFileCache;
		private TextQuality FTextQuality;
		private bool FSmoothGraphics;
		private string FPassword;
		private bool FConvertNulls;
		private bool FDoublePass;
		private bool FAutoFillDataSet;
		private int FInitialPageNumber;
		private int FMaxPages;
		private string FStartReportEvent;
		private string FFinishReportEvent;
		private PrintSettings FPrintSettings;
		private EmailSettings FEmailSettings;
		private StyleCollection FStyles;
		private Designer FDesigner;
		private CodeHelperBase FCodeHelper;
		private GraphicCache FGraphicCache;
		private string[] FReferencedAssemblies;
		private Hashtable FCachedDataItems;
		private AssemblyCollection FAssemblies;
		private FastReport.Preview.PreparedPages FPreparedPages;
		private FastReport.Preview.PreviewControl FPreview;
		private ReportEngine FEngine;
		private Form FPreviewForm;
		private Form FDesignerForm;
		private bool FAborted;
		private bool FModified;
		private Bitmap FMeasureBitmap;
		private Graphics FMeasureGraphics;
		private bool FStoreInResources;
		private PermissionSet FScriptRestrictions;
		private ReportOperation FOperation;
		private int FTickCount;
		private bool FNeedCompile;
		private bool FNeedRefresh;
		private bool FInitializing;
		private object FInitializeData;
		private string FInitializeDataName;
		private SplashForm FSplashForm;
		#endregion

		#region Properties


		/// <summary>
		/// Occurs when calc execution is started.
		/// </summary>
		public event CustomCalcEventHandler CustomCalc;

		/// <summary>
		/// Occurs when report is inherited and trying to load a base report.
		/// </summary>
		/// <remarks>
		/// Typical use of this event is to load the base report from a database instead of a file.
		/// </remarks>
		public event CustomLoadEventHandler LoadBaseReport;

		/// <summary>
		/// Occurs when report execution is started.
		/// </summary>
		public event EventHandler StartReport;

		/// <summary>
		/// Occurs when report execution is finished.
		/// </summary>
		public event EventHandler FinishReport;

		/// <summary>
		/// Gets the pages contained in this report.
		/// </summary>
		/// <remarks>
		/// This property contains pages of all types (report and dialog). Use the <b>is/as</b> operators
		/// if you want to work with pages of <b>ReportPage</b> type.
		/// </remarks>
		/// <example>The following code demonstrates how to access the first report page:
		/// <code>
		/// ReportPage page1 = report1.Pages[0] as ReportPage;
		/// </code>
		/// </example>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public PageCollection Pages
		{
			get
			{
				return FPages;
			}
		}

		/// <summary>
		/// Gets the report's data.
		/// </summary>
		/// <remarks>
		/// The dictionary contains all data items such as connections, data sources, parameters, 
		/// system variables.
		/// </remarks>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public Dictionary Dictionary
		{
			get
			{
				return FDictionary;
			}
			set
			{
				SetProp( FDictionary, value );
				FDictionary = value;
			}
		}

		/// <summary>
		/// Gets the collection of report parameters.
		/// </summary>
		/// <remarks>
		/// <para>Parameters are displayed in the "Data" window under the "Parameters" node.</para>
		/// <para>Typical use of parameters is to pass some static data from the application to the report.
		/// You can print such data, use it in the data row filter, script etc. </para>
		/// <para>Another way to use parameters is to define some reusable piece of code, for example, 
		/// to define an expression that will return the concatenation of first and second employee name. 
		/// In this case, you set the parameter's <b>Expression</b> property to something like this: 
		/// [Employees.FirstName] + " " + [Employees.LastName]. Now this parameter may be used in the report
		/// to print full employee name. Each time you access such parameter, it will calculate the expression 
		/// and return its value. </para>
		/// <para>You can create nested parameters. To do this, add the new <b>Parameter</b> to the
		/// <b>Parameters</b> collection of the root parameter. To access the nested parameter, you may use the
		/// <see cref="GetParameter"/> method.</para>
		/// <para>To get or set the parameter's value, use the <see cref="GetParameterValue"/> and
		/// <see cref="SetParameterValue"/> methods. To set the parameter's expression, use the
		/// <see cref="GetParameter"/> method that returns a <b>Parameter</b> object and set its
		/// <b>Expression</b> property.</para>
		/// </remarks>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public ParameterCollection Parameters
		{
			get
			{
				return FDictionary.Parameters;
			}
		}

		/// <summary>
		/// Gets or sets the report information such as report name, author, description etc.
		/// </summary>
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Design" )]
		public ReportInfo ReportInfo
		{
			get
			{
				return FReportInfo;
			}
			set
			{
				FReportInfo = value;
			}
		}

		/// <summary>
		/// Gets or sets the base report file name.
		/// </summary>
		/// <remarks>
		/// This property contains the name of a report file this report is inherited from.
		/// <b>Note:</b> setting this property to non-empty value will clear the report and 
		/// load the base file into it.
		/// </remarks>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public string BaseReport
		{
			get
			{
				return FBaseReport;
			}
			set
			{
				SetBaseReport( value );
			}
		}

		/// <summary>
		/// Gets or sets the name of a file the report was loaded from.
		/// </summary>
		/// <remarks>
		/// This property is used to support the FastReport.Net infrastructure; 
		/// typically you don't need to use it.
		/// </remarks>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public string FileName
		{
			get
			{
				return FFileName;
			}
			set
			{
				FFileName = value;
			}
		}

		/// <summary>
		/// Gets or sets the report script.
		/// </summary>
		/// <remarks>
		/// <para>The script contains the <b>ReportScript</b> class that contains all report objects'
		/// event handlers and own items such as private fields, properties, methods etc. The script 
		/// contains only items written by you. Unlike other report generators, the script does not 
		/// contain report objects declarations, initialization code. It is added automatically when 
		/// you run the report.</para>
		/// <para>By default this property contains an empty script text. You may see it in the designer 
		/// when you switch to the Code window.</para>
		/// <para>If you set this property programmatically, you have to declare the <b>FastReport</b> 
		/// namespace and the <b>ReportScript</b> class in it. Do not declare report items (such as bands, 
		/// objects, etc) in the <b>ReportScript</b> class: the report engine does this automatically when 
		/// you run the report.</para>
		/// <para><b>Security note:</b> since the report script is compiled into .NET assembly, it allows 
		/// you to do ANYTHING. For example, you may create a script that will read/write files from/to a disk. 
		/// To restrict such operations, use the <see cref="ScriptRestrictions"/> property.</para>
		/// </remarks>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public string ScriptText
		{
			get
			{
				return FScriptText;
			}
			set
			{
				FScriptText = value;
			}
		}

		/// <summary>
		/// Gets or sets the script language of this report.
		/// </summary>
		/// <remarks>
		/// Note: changing this property will reset the report script to default empty script.
		/// </remarks>
		[DefaultValue( Language.CSharp )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Script" )]
		public Language ScriptLanguage
		{
			get
			{
				return FScriptLanguage;
			}
			set
			{
				bool needClear = FScriptLanguage != value;
				FScriptLanguage = value;
				if( FScriptLanguage == Language.CSharp )
					FCodeHelper = new CsCodeHelper( this );
				else
					FCodeHelper = new VbCodeHelper( this );
				if( needClear )
					FScriptText = FCodeHelper.EmptyScript();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the null DB value must be converted to zero, false or  
		/// empty string depending on the data column type.
		/// </summary>
		/// <remarks>
		/// This property is <b>true</b> by default. If you set it to <b>false</b>, you should check
		/// the DB value before you do something with it (for example, typecast it to any type, use it
		/// in a expression etc.)
		/// </remarks>
		[DefaultValue( true )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Engine" )]
		public bool ConvertNulls
		{
			get
			{
				return FConvertNulls;
			}
			set
			{
				FConvertNulls = value;
			}
		}

		/// <summary>
		/// Gets or sets a value that specifies whether the report engine should perform the second pass. 
		/// </summary>
		/// <remarks>
		/// <para>Typically the second pass is necessary to print the number of total pages. It also 
		/// may be used to perform some calculations on the first pass and print its results on the 
		/// second pass.</para>
		/// <para>Use the <b>Engine.FirstPass</b>, <b>Engine.FinalPass</b> properties to determine which
		/// pass the engine is performing now.</para>
		/// </remarks>
		[DefaultValue( false )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Engine" )]
		public bool DoublePass
		{
			get
			{
				return FDoublePass;
			}
			set
			{
				FDoublePass = value;
			}
		}

		/// <summary>
		/// Gets or sets a value that specifies whether to compress the report file.
		/// </summary>
		/// <remarks>
		/// The report file is compressed using the Gzip algorithm. So you can open the 
		/// compressed report in any zip-compatible archiver.
		/// </remarks>
		[DefaultValue( false )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Misc" )]
		public bool Compressed
		{
			get
			{
				return FCompressed;
			}
			set
			{
				FCompressed = value;
			}
		}

		/// <summary>
		/// Gets or sets a value that specifies whether to use the file cache rather than memory 
		/// to store the prepared report pages.
		/// </summary>
		[DefaultValue( false )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Engine" )]
		public bool UseFileCache
		{
			get
			{
				return FUseFileCache;
			}
			set
			{
				FUseFileCache = value;
			}
		}

		/// <summary>
		/// Gets or sets a value that specifies the quality of text rendering.
		/// </summary>
		/// <remarks>
		/// <b>Note:</b> the default property value is <b>TextQuality.Default</b>. That means the report
		/// may look different depending on OS settings. This property does not affect the printout.
		/// </remarks>
		[DefaultValue( TextQuality.Default )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Misc" )]
		public TextQuality TextQuality
		{
			get
			{
				return FTextQuality;
			}
			set
			{
				FTextQuality = value;
			}
		}

		/// <summary>
		/// Gets or sets a value that specifies if the graphic objects such as bitmaps 
		/// and shapes should be displayed smoothly.
		/// </summary>
		[DefaultValue( false )]
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Misc" )]
		public bool SmoothGraphics
		{
			get
			{
				return FSmoothGraphics;
			}
			set
			{
				FSmoothGraphics = value;
			}
		}

		/// <summary>
		/// Gets or sets the report password.
		/// </summary>
		/// <remarks>
		/// <para>When you try to load the password-protected report, you will be asked
		/// for a password. You also may specify the password in this property before loading
		/// the report. In this case the report will load silently.</para>
		/// <para>Password-protected report file is crypted using Rijndael algorithm. 
		/// Do not forget your password! It will be hard or even impossible to open
		/// the protected file in this case.</para>
		/// </remarks>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public string Password
		{
			get
			{
				return FPassword;
			}
			set
			{
				FPassword = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether it is necessary to automatically fill
		/// DataSet registered with <b>RegisterData</b> call.
		/// </summary>
		/// <remarks>
		/// If this property is <b>true</b> (by default), FastReport will automatically fill 
		/// the DataSet with data when you trying to run a report. Set it to <b>false</b> if 
		/// you want to fill the DataSet by yourself.
		/// </remarks>
		[DefaultValue( true )]
		[SRCategory( "Misc" )]
		public bool AutoFillDataSet
		{
			get
			{
				return FAutoFillDataSet;
			}
			set
			{
				FAutoFillDataSet = value;
			}
		}

		/// <summary>
		/// Gets or sets the maximum number of generated pages in a prepared report.
		/// </summary>
		/// <remarks>
		/// Use this property to limit the number of pages in a prepared report.
		/// </remarks>
		[DefaultValue( 0 )]
		[SRCategory( "Misc" )]
		public int MaxPages
		{
			get
			{
				return FMaxPages;
			}
			set
			{
				FMaxPages = value;
			}
		}

		/// <summary>
		/// Gets the print settings such as printer name, copies, pages to print etc.
		/// </summary>
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Print" )]
		public PrintSettings PrintSettings
		{
			get
			{
				return FPrintSettings;
			}
		}

		/// <summary>
		/// Gets the email settings such as recipients, subject, message body.
		/// </summary>
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Email" )]
		public EmailSettings EmailSettings
		{
			get
			{
				return FEmailSettings;
			}
		}

		/// <summary>
		/// Gets or sets the collection of styles used in this report.
		/// </summary>
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Misc" )]
		public StyleCollection Styles
		{
			get
			{
				return FStyles;
			}
			set
			{
				FStyles = value;
			}
		}

		/// <summary>
		/// Gets or sets an array of assembly names that will be used to compile the report script.
		/// </summary>
		/// <remarks>
		/// By default this property contains the following assemblies: "System.dll", "System.Drawing.dll", 
		/// "System.Windows.Forms.dll", "System.Data.dll", "System.Xml.dll". If your script uses some types 
		/// from another assemblies, you have to add them to this property.
		/// </remarks>
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Script" )]
		public string[] ReferencedAssemblies
		{
			get
			{
				return FReferencedAssemblies;
			}
			set
			{
				FReferencedAssemblies = value;
			}
		}

		/// <summary>
		/// Gets or sets a script event name that will be fired when the report starts.
		/// </summary>
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Build" )]
		public string StartReportEvent
		{
			get
			{
				return FStartReportEvent;
			}
			set
			{
				FStartReportEvent = value;
			}
		}

		/// <summary>
		/// Gets or sets a script event name that will be fired when the report is finished.
		/// </summary>
		[DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		[SRCategory( "Build" )]
		public string FinishReportEvent
		{
			get
			{
				return FFinishReportEvent;
			}
			set
			{
				FFinishReportEvent = value;
			}
		}

		/// <summary>
		/// Gets a value indicating that report execution was aborted.
		/// </summary>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public bool Aborted
		{
			get
			{
				return FAborted;
			}
		}

		/// <summary>
		/// Gets or sets a value that determines whether to store the report in the application resources.
		/// Use this property in the MS Visual Studio IDE only.
		/// </summary>
		/// <remarks>
		/// By default this property is <b>true</b>. When set to <b>false</b>, you should store your report
		/// in a file.
		/// </remarks>
		[DefaultValue( true )]
		[SRCategory( "Design" )]
		public bool StoreInResources
		{
			get
			{
				return FStoreInResources;
			}
			set
			{
				FStoreInResources = value;
			}
		}

		/// <summary>
		/// Gets or sets the resource string that contains the report.
		/// </summary>
		/// <remarks>
		/// This property is used by the MS Visual Studio to store the report. Do not use it directly.
		/// </remarks>
		[Browsable( false )]
		[Localizable( true )]
		public string ReportResourceString
		{
			get
			{
				if( !StoreInResources )
					return "";
				return SaveToString();
			}
			set
			{
				if( String.IsNullOrEmpty( value ) )
				{
					Clear();
					return;
				}
				LoadFromString( value );
			}
		}

		/// <summary>
		/// Gets a value indicating that this report contains dialog forms.
		/// </summary>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public bool HasDialogs
		{
			get
			{
				foreach( PageBase page in Pages )
				{
					if( page is DialogPage )
						return true;
				}
				return false;
			}
		}

		/// <summary>
		/// Gets or sets a set of permissions that will be restricted for the script code.
		/// </summary>
		/// <remarks>
		/// Since the report script is compiled into .NET assembly, it allows you to do ANYTHING.
		/// For example, you may create a script that will read/write files from/to a disk. This property
		/// is used to restrict such operations.
		/// <example>This example shows how to restrict the file IO operations in a script:
		/// <code>
		/// using System.Security;
		/// using System.Security.Permissions;
		/// ...
		/// PermissionSet ps = new PermissionSet(PermissionState.None);
		/// ps.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
		/// report1.ScriptRestrictions = ps;
		/// report1.Prepare();
		/// </code>
		/// </example>
		/// </remarks>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public PermissionSet ScriptRestrictions
		{
			get
			{
				return FScriptRestrictions;
			}
			set
			{
				FScriptRestrictions = value;
			}
		}

		/// <summary>
		/// Gets a reference to the report designer.
		/// </summary>
		/// <remarks>
		/// This property can be used when report is designing. In other cases it returns <b>null</b>.
		/// </remarks>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public Designer Designer
		{
			get
			{
				return FDesigner;
			}
			set
			{
				FDesigner = value;
			}
		}


		/// <summary>
		/// Gets a reference to the graphics cache for this report.
		/// </summary>
		/// <remarks>
		/// This property is used to support the FastReport.Net infrastructure. Do not use it directly.
		/// </remarks>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public GraphicCache GraphicCache
		{
			get
			{
				return FGraphicCache;
			}
		}

		/// <summary>
		/// Gets a pages of the prepared report.
		/// </summary>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public Preview.PreparedPages PreparedPages
		{
			get
			{
				return FPreparedPages;
			}
		}

		/// <summary>
		/// Gets a reference to the report engine.
		/// </summary>
		/// <remarks>
		/// This property can be used when report is running. In other cases it returns <b>null</b>.
		/// </remarks>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public ReportEngine Engine
		{
			get
			{
				return FEngine;
			}
		}

		/// <summary>
		/// Gets or sets the report preview control.
		/// </summary>
		/// <remarks>
		/// Use this property to attach a custom preview to your report. To do this, place the PreviewControl
		/// control to your form and set the report's <b>Preview</b> property to this control.
		/// </remarks>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public Preview.PreviewControl Preview
		{
			get
			{
				return FPreview;
			}
			set
			{
				FPreview = value;
				if( value != null )
					value.SetReport( this );
			}
		}

		/// <summary>
		/// Gets or sets the initial page number for PageN/PageNofM system variables.
		/// </summary>
		[DefaultValue( 1 )]
		[SRCategory( "Engine" )]
		public int InitialPageNumber
		{
			get
			{
				return FInitialPageNumber;
			}
			set
			{
				FInitialPageNumber = value;
			}
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		/// <summary>
		/// This property is not relevant to this class.
		/// </summary>
		[Browsable( false ), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
		public new Restrictions Restrictions
		{
			get
			{
				return base.Restrictions;
			}
			set
			{
				base.Restrictions = value;
			}
		}

		/// <summary>
		/// Gets the report operation that is currently performed.
		/// </summary>
		[Browsable( false )]
		public ReportOperation Operation
		{
			get
			{
				return FOperation;
			}
		}

		private string[] DefaultAssemblies
		{
			get
			{
				return new string[] { "System.dll", "System.Drawing.dll", "System.Windows.Forms.dll",
          "System.Data.dll", "System.Xml.dll" };
			}
		}

		internal CodeHelperBase CodeHelper
		{
			get
			{
				return FCodeHelper;
			}
		}

		internal Graphics MeasureGraphics
		{
			get
			{
				if( FMeasureGraphics == null )
				{
					FMeasureBitmap = new Bitmap( 1, 1 );
					FMeasureGraphics = Graphics.FromImage( FMeasureBitmap );
				}
				return FMeasureGraphics;
			}
		}

		internal bool IsVSDesignMode
		{
			get
			{
				return DesignMode;
			}
		}

		internal string GetReportName
		{
			get
			{
				string result = ReportInfo.Name;
				if( String.IsNullOrEmpty( result ) )
					result = Path.GetFileNameWithoutExtension( FileName );
				return result;
			}
		}

		/// <summary>
		/// Gets or sets the flag for refresh.
		/// </summary>
		public bool NeedRefresh
		{
			get
			{
				return FNeedRefresh;
			}
			set
			{
				FNeedRefresh = value;
			}
		}

		internal ObjectCollection AllNamedObjects
		{
			get
			{
				ObjectCollection allObjects = AllObjects;
				// data objects are not included into AllObjects list. Include named items separately.
				foreach( Base c in Dictionary.AllObjects )
				{
					if( c is DataConnectionBase || c is DataSourceBase || c is Relation )
						allObjects.Add( c );
				}

				return allObjects;
			}
		}
		#endregion

		#region Private Methods
		private bool ShouldSerializeReferencedAssemblies()
		{
			return Converter.ToString( ReferencedAssemblies ) != Converter.ToString( DefaultAssemblies );
		}

		// convert absolute path to the base report to relative path (based on the main report path).
		private string GetRelativePathToBaseReport()
		{
			string path = "";
			if( !String.IsNullOrEmpty( FileName ) )
			{
				try
				{
					path = Path.GetDirectoryName( FileName );
				}
				catch
				{
				}
			}

			if( !String.IsNullOrEmpty( path ) )
			{
				try
				{
					return FileUtils.GetRelativePath( BaseReport, path );
				}
				catch
				{
				}
			}
			return BaseReport;
		}

		private void SetBaseReport( string value )
		{
			FBaseReport = value;
			if( FBaseReportObject != null )
			{
				FBaseReportObject.Dispose();
				FBaseReportObject = null;
			}

			// detach the base report
			if( String.IsNullOrEmpty( value ) )
			{
				foreach( Base c in AllObjects )
				{
					c.SetAncestor( false );
				}
				SetAncestor( false );
				return;
			}

			string saveFileName = FFileName;
			if( LoadBaseReport != null )
			{
				LoadBaseReport( this, new CustomLoadEventArgs( value, this ) );
			}
			else
			{
				// convert the relative path to absolute path (based on the main report path).
				if( !Path.IsPathRooted( value ) )
					value = Path.GetFullPath( Path.GetDirectoryName( FileName ) + Path.DirectorySeparatorChar + value );
				Load( value );
			}

			FFileName = saveFileName;
			FBaseReport = "";
			Password = "";
			FBaseReportObject = Activator.CreateInstance( GetType() ) as Report;
			FBaseReportObject.AssignAll( this, true );

			// set Ancestor & CanChangeParent flags
			foreach( Base c in AllObjects )
			{
				c.SetAncestor( true );
			}
			SetAncestor( true );
			FBaseReport = value;
		}

		private void GetDiff( object sender, DiffEventArgs e )
		{
			if( FBaseReportObject != null )
			{
				if( e.Object is Report )
					e.DiffObject = FBaseReportObject;
				else if( e.Object is Base )
					e.DiffObject = FBaseReportObject.FindObject( ( e.Object as Base ).Name );
			}
		}

		private void OnCloseDesigner( Object sender, FormClosedEventArgs e )
		{
			FModified = FDesigner.Modified;
			FDesignerForm.Dispose();
			FDesignerForm = null;
			FDesigner = null;
		}

		private void OnClosePreview( object sender, FormClosedEventArgs e )
		{
			FPreviewForm.Dispose();
			FPreviewForm = null;
			FPreview = null;
		}

		private void SavePreviewPicture()
		{
			ReportPage page = PreparedPages.GetCachedPage( 0 );
			float pageWidth = page.WidthInPixels;
			float pageHeight = page.HeightInPixels;
			float ratio = ReportInfo.PreviewPictureRatio;
			ReportInfo.Picture = new Bitmap( (int)Math.Round( pageWidth * ratio ), (int)Math.Round( pageHeight * ratio ) );

			using( Graphics g = Graphics.FromImage( ReportInfo.Picture ) )
			{
				FRPaintEventArgs args = new FRPaintEventArgs( g, ratio, ratio, GraphicCache );
				page.Draw( args );
			}
		}

		private void StartPerformanceCounter()
		{
			FTickCount = Environment.TickCount;
		}

		private void StopPerformanceCounter()
		{
			FTickCount = Environment.TickCount - FTickCount;
		}

		private void ClearReportProperties()
		{
			ReportInfo.Clear();
			Dictionary.Clear();
			ScriptLanguage = Config.ReportSettings.DefaultLanguage;
			ScriptText = FCodeHelper.EmptyScript();
			BaseReport = "";
			DoublePass = false;
			ConvertNulls = true;
			Compressed = false;
			TextQuality = TextQuality.Default;
			SmoothGraphics = false;
			Password = "";
			InitialPageNumber = 1;
			MaxPages = 0;
			PrintSettings.Clear();
			EmailSettings.Clear();
			Styles.Clear();
			Styles.Name = "";
			ReferencedAssemblies = DefaultAssemblies;
			StartReportEvent = "";
			FinishReportEvent = "";
			FNeedCompile = true;
		}

		private void ShowSplashScreen()
		{
			FSplashForm = new SplashForm();
			Application.AddMessageFilter( FSplashForm );
			FSplashForm.Show();
			Config.DesignerSettings.DesignerLoaded += HideSplashScreen;
		}

		private void HideSplashScreen( object sender, EventArgs e )
		{
			Config.DesignerSettings.DesignerLoaded -= HideSplashScreen;
			FSplashForm.Hide();

			// without message filter and delay all events that were triggered on the splashscreen will be redirected to the main form
			Timer t = new Timer();
			t.Interval = 500;
			t.Tick += delegate( object sender_, EventArgs e_ )
			{
				t.Stop();
				Application.RemoveMessageFilter( FSplashForm );
			};
			t.Start();
		}
		#endregion

		#region Protected Methods
		/// <inheritdoc/>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( FGraphicCache != null )
					FGraphicCache.Dispose();
				FGraphicCache = null;
				if( FMeasureGraphics != null )
					FMeasureGraphics.Dispose();
				FMeasureGraphics = null;
				if( FMeasureBitmap != null )
					FMeasureBitmap.Dispose();
				FMeasureBitmap = null;
				FPrintSettings.Dispose();
				if( PreparedPages != null )
					PreparedPages.Dispose();
			}
			base.Dispose( disposing );
		}

		/// <inheritdoc/>
		protected override void DeserializeSubItems( FRReader reader )
		{
			if( String.Compare( reader.ItemName, "ScriptText", true ) == 0 )
				ScriptText = reader.ReadPropertyValue();
			else if( String.Compare( reader.ItemName, "Dictionary", true ) == 0 )
				reader.Read( Dictionary );
			else if( String.Compare( reader.ItemName, "Styles", true ) == 0 )
				reader.Read( Styles );
			else
				base.DeserializeSubItems( reader );
		}
		#endregion

		#region IParent
		/// <inheritdoc/>
		public bool CanContain( Base child )
		{
			return child is PageBase || child is Dictionary;
		}

		/// <inheritdoc/>
		public void GetChildObjects( ObjectCollection list )
		{
			foreach( PageBase page in FPages )
			{
				list.Add( page );
			}
		}

		/// <inheritdoc/>
		public void AddChild( Base obj )
		{
			if( obj is PageBase )
				FPages.Add( obj as PageBase );
			else if( obj is Dictionary )
				Dictionary = obj as Dictionary;
		}

		/// <inheritdoc/>
		public void RemoveChild( Base obj )
		{
			if( obj is PageBase )
				FPages.Remove( obj as PageBase );
			else if( obj is Dictionary && ( obj as Dictionary ) == FDictionary )
				Dictionary = null;
		}

		/// <inheritdoc/>
		public virtual int GetChildOrder( Base child )
		{
			if( child is PageBase )
				return FPages.IndexOf( child as PageBase );
			return 0;
		}

		/// <inheritdoc/>
		public virtual void SetChildOrder( Base child, int order )
		{
			if( child is PageBase )
			{
				if( order > FPages.Count )
					order = FPages.Count;
				int oldOrder = child.ZOrder;
				if( oldOrder != -1 && order != -1 && oldOrder != order )
				{
					if( oldOrder <= order )
						order--;
					FPages.Remove( child as PageBase );
					FPages.Insert( order, child as PageBase );
				}
			}
		}

		/// <inheritdoc/>
		public virtual void UpdateLayout( float dx, float dy )
		{
			// do nothing
		}
		#endregion

		#region ISupportInitialize Members
		/// <inheritdoc/>
		public void BeginInit()
		{
			FInitializing = true;
		}

		/// <inheritdoc/>
		public void EndInit()
		{
			FInitializing = false;
			Dictionary.RegisterData( FInitializeData, FInitializeDataName, false );
		}
		#endregion

		#region Script related
		private void FillDataSourceCache()
		{
			FCachedDataItems.Clear();
			ObjectCollection dictionaryObjects = Dictionary.AllObjects;
			foreach( Parameter c in Dictionary.SystemVariables )
			{
				dictionaryObjects.Add( c );
			}
			foreach( Base c in dictionaryObjects )
			{
				if( c is DataSourceBase )
				{
					DataSourceBase data = c as DataSourceBase;
					CachedDataItem cachedItem = new CachedDataItem();
					cachedItem.DataSource = data;
					FCachedDataItems[data.FullName] = cachedItem;

					for( int i = 0; i < data.Columns.Count; i++ )
					{
						cachedItem = new CachedDataItem();
						cachedItem.DataSource = data;
						cachedItem.Column = data.Columns[i];
						FCachedDataItems[data.FullName + "." + data.Columns[i].Alias] = cachedItem;
					}
				}
				else if( c is Parameter )
				{
					FCachedDataItems[( c as Parameter ).FullName] = c;
				}
				else if( c is Total )
				{
					FCachedDataItems[( c as Total ).Name] = c;
				}
			}
		}

		internal void Compile()
		{
			FillDataSourceCache();

			if( FNeedCompile )
			{
				AssemblyDescriptor descriptor = new AssemblyDescriptor( this, ScriptText );
				FAssemblies.Clear();
				FAssemblies.Add( descriptor );
				descriptor.AddObjects();
				descriptor.AddExpressions();
				descriptor.AddFunctions();
				descriptor.Compile();
			}
			else
			{
				InternalInit();
			}
		}

		/// <summary>
		/// Initializes the report's fields.
		/// </summary>
		/// <remarks>
		/// This method is for internal use only.
		/// </remarks>
		protected void InternalInit()
		{
			FNeedCompile = false;

			AssemblyDescriptor descriptor = new AssemblyDescriptor( this, CodeHelper.EmptyScript() );
			FAssemblies.Clear();
			FAssemblies.Add( descriptor );
			descriptor.InitInstance( this );
		}

		/// <summary>
		/// Generates the file (.cs or .vb) that contains the report source code.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <remarks>
		/// Use this method to generate the report source code. This code can be attached to your project.
		/// In this case, you will need to call the following code to run a report:
		/// <code>
		/// SimpleListReport report = new SimpleListReport();
		/// report.RegisterData(your_dataset);
		/// report.Show();
		/// </code>
		/// </remarks>
		public void GenerateReportAssembly( string fileName )
		{
			// create the class name
			string className = "";
			string punctuation = " ~`!@#$%^&*()-=+[]{},.<>/?;:'\"\\|";
			foreach( char c in Path.GetFileNameWithoutExtension( fileName ) )
			{
				if( !punctuation.Contains( c.ToString() ) )
					className += c;
			}

			AssemblyDescriptor descriptor = new AssemblyDescriptor( this, ScriptText );
			descriptor.AddObjects();
			descriptor.AddExpressions();
			descriptor.AddFunctions();

			string reportClassText = descriptor.GenerateReportClass( className );
			File.WriteAllText( fileName, reportClassText, Encoding.UTF8 );
		}

		/// <summary>
		/// Calculates an expression and returns the result.
		/// </summary>
		/// <param name="expression">The expression to calculate.</param>
		/// <returns>If report is running, returns the result of calculation. 
		/// Otherwise returns <b>null</b>.</returns>
		/// <remarks>
		/// <para>The expression may be any valid expression such as "1 + 2". The expression 
		/// is calculated in the report script's <b>ReportScript</b> class instance context,
		/// so you may refer to any objects available in this context: private fields,
		/// methods, report objects.</para>
		/// </remarks>
		public object Calc( string expression )
		{
			return Calc( expression, 0 );
		}

		/// <summary>
		/// Calculates an expression and returns the result.
		/// </summary>
		/// <param name="expression">The expression to calculate.</param>
		/// <param name="value">The value of currently printing object.</param>
		/// <returns>If report is running, returns the result of calculation. 
		/// Otherwise returns <b>null</b>.</returns>
		/// <remarks>
		/// Do not call this method directly. Use the <b>Calc(string expression)</b> method instead.
		/// </remarks>
		public object Calc( string expression, Variant value )
		{
			if( !IsRunning )
				return null;

			string expr = expression;
			if( expr.StartsWith( "[" ) && expr.EndsWith( "]" ) )
				expr = expression.Substring( 1, expression.Length - 2 );

			// check cached items first
			object cachedObject = FCachedDataItems[expr];

			if( cachedObject is CachedDataItem )
			{
				CachedDataItem cachedItem = FCachedDataItems[expr] as CachedDataItem;
				DataSourceBase data = cachedItem.DataSource;
				Column column = cachedItem.Column;

				object val = column.Value;
				if( ConvertNulls && ( val == null || val is DBNull ) )
					val = Converter.ConvertNull( column.DataType );

				if( CustomCalc != null )
				{
					CustomCalcEventArgs e = new CustomCalcEventArgs( expr, val, this );
					CustomCalc( this, e );
					val = e.CalculatedObject;
				}

				return val;
			}
			else if( cachedObject is Parameter )
			{
				return ( cachedObject as Parameter ).Value;
			}
			else if( cachedObject is Total )
			{
				object val = ( cachedObject as Total ).Value;
				if( ConvertNulls && ( val == null || val is DBNull ) )
					val = 0;

				return val;
			}

			// calculate the expression
			return CalcExpression( expression, value );
		}

		/// <summary>
		/// Returns an expression value.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <param name="value">The value of currently printing object.</param>
		/// <returns>Returns the result of calculation.</returns>
		/// <remarks>
		/// This method is for internal use only, do not call it directly.
		/// </remarks>
		protected virtual object CalcExpression( string expression, Variant value )
		{
			// try to calculate the expression
			foreach( AssemblyDescriptor d in FAssemblies )
			{
				if( d.ContainsExpression( expression ) )
					return d.CalcExpression( expression, value );
			}

			// expression not found. Probably it was added after the start of the report.
			// Compile new assembly containing this expression.
			AssemblyDescriptor descriptor = new AssemblyDescriptor( this, CodeHelper.EmptyScript() );
			FAssemblies.Add( descriptor );
			descriptor.AddObjects();
			descriptor.AddSingleExpression( expression );
			descriptor.AddFunctions();
			descriptor.Compile();
			return descriptor.CalcExpression( expression, value );
		}

		/// <summary>
		/// Invokes the script event handler with given name.
		/// </summary>
		/// <param name="name">The name of the script method.</param>
		/// <param name="parms">The method parameters.</param>
		public void InvokeEvent( string name, object[] parms )
		{
			if( FAssemblies.Count > 0 )
				FAssemblies[0].InvokeEvent( name, parms );
		}

		private Column GetColumn( string complexName )
		{
			if( String.IsNullOrEmpty( complexName ) )
				return null;

			CachedDataItem cachedItem = FCachedDataItems[complexName] as CachedDataItem;
			if( cachedItem != null )
				return cachedItem.Column;

			string[] names = complexName.Split( new char[] { '.' } );
			cachedItem = FCachedDataItems[names[0]] as CachedDataItem;
			DataSourceBase data = cachedItem != null ? cachedItem.DataSource : GetDataSource( names[0] );

			return DataHelper.GetColumn( Dictionary, data, names, true );
		}

		private object GetColumnValue( string complexName, bool convertNull )
		{
			Column column = GetColumn( complexName );
			if( column == null )
				return null;

			object value = column.Value;

			if( convertNull && ( value == null || value is DBNull ) )
				value = Converter.ConvertNull( column.DataType );

			return value;
		}

		private Variant GetTotalValue( string name, bool convertNull )
		{
			object value = Dictionary.Totals.GetValue( name );
			if( convertNull && ( value == null || value is DBNull ) )
				value = 0;

			return new Variant( value );
		}

		/// <summary>
		/// Gets the data column's value. Automatically converts null value to 0, false or ""
		/// depending on the column type.
		/// </summary>
		/// <param name="complexName">The name of the data column including the datasource name.</param>
		/// <returns>If report is running, returns the column value. Otherwise returns <b>null</b>.</returns>
		/// <remarks>
		/// The return value of this method does not depend on the <see cref="ConvertNulls"/> property.
		/// </remarks>
		/// <example>
		/// <code>
		/// string employeeName = (string)report.GetColumnValue("Employees.FirstName");
		/// </code>
		/// </example>
		public object GetColumnValue( string complexName )
		{
			return GetColumnValue( complexName, true );
		}

		/// <summary>
		/// Gets the data column's value. This method does not convert null values.
		/// </summary>
		/// <param name="complexName">The name of the data column including the datasource name.</param>
		/// <returns>If report is running, returns the column value. 
		/// Otherwise returns <b>null</b>.</returns>
		public object GetColumnValueNullable( string complexName )
		{
			return GetColumnValue( complexName, false );
		}

		/// <summary>
		/// Gets the report parameter with given name.
		/// </summary>
		/// <param name="complexName">The name of the parameter.</param>
		/// <returns>The <see cref="Parameter"/> object if found, otherwise <b>null</b>.</returns>
		/// <remarks>
		/// To find nested parameter, use the "." separator: "MainParameter.NestedParameter"
		/// </remarks>
		public Parameter GetParameter( string complexName )
		{
			if( IsRunning )
				return FCachedDataItems[complexName] as Parameter;
			return DataHelper.GetParameter( Dictionary, complexName );
		}

		/// <summary>
		/// Gets a value of the parameter with given name.
		/// </summary>
		/// <param name="complexName">The name of the parameter.</param>
		/// <returns>The parameter's value if found, otherwise <b>null</b>.</returns>
		/// <remarks>
		/// To find nested parameter, use the "." separator: "MainParameter.NestedParameter"
		/// </remarks>
		public object GetParameterValue( string complexName )
		{
			Parameter par = GetParameter( complexName );
			if( par != null )
				return par.Value;
			return null;
		}

		/// <summary>
		/// Sets the parameter's value.
		/// </summary>
		/// <param name="complexName">The name of the parameter.</param>
		/// <param name="value">Value to set.</param>
		/// <remarks>
		/// Use this method to pass a value to the parameter that you've created in the "Data" window.
		/// Such parameter may be used everythere in a report; for example, you can print its value
		/// or use it in expressions.
		/// <para/>You should call this method <b>after</b> the report was loaded and <b>before</b> you run it.
		/// <para/>To access a nested parameter, use the "." separator: "MainParameter.NestedParameter"
		/// <note type="caution">
		/// This method will create the parameter if it does not exist.
		/// </note>
		/// </remarks>
		/// <example>This example shows how to pass a value to the parameter with "MyParam" name:
		/// <code>
		/// // load the report
		/// report1.Load("report.frx");
		/// // setup the parameter
		/// report1.SetParameterValue("MyParam", 10);
		/// // show the report
		/// report1.Show();
		/// </code>
		/// </example>
		public void SetParameterValue( string complexName, object value )
		{
			Parameter par = GetParameter( complexName );
			if( par == null )
				par = DataHelper.CreateParameter( Dictionary, complexName );
			par.Value = value;
			par.Expression = "";
		}

		/// <summary>
		/// Gets a value of the system variable with specified name.
		/// </summary>
		/// <param name="complexName">Name of a variable.</param>
		/// <returns>The variable's value if found, otherwise <b>null</b>.</returns>
		public object GetVariableValue( string complexName )
		{
			return GetParameterValue( complexName );
		}

		/// <summary>
		/// Gets a value of the total with specified name.
		/// </summary>
		/// <param name="name">Name of total.</param>
		/// <returns>The total's value if found, otherwise <b>0</b>.</returns>
		/// <remarks>This method converts null values to 0 if the <see cref="ConvertNulls"/> property is set to true. 
		/// Use the <see cref="GetTotalValueNullable"/> method if you don't want the null conversion.
		/// </remarks>
		public Variant GetTotalValue( string name )
		{
			return GetTotalValue( name, ConvertNulls );
		}

		/// <summary>
		/// Gets a value of the total with specified name.
		/// </summary>
		/// <param name="name">Name of total.</param>
		/// <returns>The total's value if found, otherwise <b>null</b>.</returns>
		public Variant GetTotalValueNullable( string name )
		{
			return GetTotalValue( name, false );
		}

		/// <summary>
		/// Gets the datasource with specified name.
		/// </summary>
		/// <param name="alias">Alias name of a datasource.</param>
		/// <returns>The datasource object if found, otherwise <b>null</b>.</returns>
		public DataSourceBase GetDataSource( string alias )
		{
			return Dictionary.FindByAlias( alias ) as DataSourceBase;
		}
		#endregion

		#region Public Methods
		/// <inheritdoc/>
		public override void Assign( Base source )
		{
			BaseAssign( source );
		}

		/// <summary>
		/// Aborts the report execution.
		/// </summary>
		public void Abort()
		{
			SetAborted( true );
		}

		/// <inheritdoc/>
		public override Base FindObject( string name )
		{
			foreach( Base c in AllNamedObjects )
			{
				if( String.Compare( name, c.Name, true ) == 0 )
					return c;
			}
			return null;
		}

		/// <inheritdoc/>
		public override void Clear()
		{
			base.Clear();
			ClearReportProperties();
		}

		/// <summary>
		/// Updates the report component's styles.
		/// </summary>
		/// <remarks>
		/// Call this method if you change the <see cref="Styles"/> collection.
		/// </remarks>
		public void ApplyStyles()
		{
			foreach( Base c in AllObjects )
			{
				if( c is ReportComponentBase )
					( c as ReportComponentBase ).Style = ( c as ReportComponentBase ).Style;
			}
		}

		/// <summary>
		/// Sets prepared pages.
		/// </summary>
		/// <param name="pages"></param>            
		public void SetPreparedPages( Preview.PreparedPages pages )
		{
			FPreparedPages = pages;
			if( pages != null )
				pages.SetReport( this );
		}

		internal void SetAborted( bool value )
		{
			FAborted = value;
		}

		internal void SetOperation( ReportOperation operation )
		{
			FOperation = operation;
		}

		/// <summary>
		/// This method fires the <b>StartReport</b> event and the script code connected 
		/// to the <b>StartReportEvent</b>.
		/// </summary>
		public void OnStartReport( EventArgs e )
		{
			SetRunning( true );
			if( StartReport != null )
				StartReport( this, e );
			InvokeEvent( StartReportEvent, new object[] { this, e } );
		}

		/// <summary>
		/// This method fires the <b>FinishReport</b> event and the script code connected 
		/// to the <b>FinishReportEvent</b>.
		/// </summary>
		public void OnFinishReport( EventArgs e )
		{
			SetRunning( false );
			if( FinishReport != null )
				FinishReport( this, e );
			InvokeEvent( FinishReportEvent, new object[] { this, e } );
		}

		/// <inheritdoc/>
		public override void Serialize( FRWriter writer )
		{
			Report c = writer.DiffObject as Report;
			writer.ItemName = IsAncestor ? "inherited" : ClassName;
			if( BaseReport != c.BaseReport )
			{
				// when save to the report file, convert absolute path to the base report to relative path
				// (based on the main report path). Do not convert when saving to the clipboard.
				string value = writer.SerializeTo != SerializeTo.Undo ? GetRelativePathToBaseReport() : BaseReport;
				writer.WriteStr( "BaseReport", value );
			}
			// always serialize ScriptLanguage because its default value depends on Config.ReportSettings.DefaultLanguage
			writer.WriteValue( "ScriptLanguage", ScriptLanguage );
			if( ScriptText != c.ScriptText )
				writer.WritePropertyValue( "ScriptText", ScriptText );
			if( !writer.AreEqual( ReferencedAssemblies, c.ReferencedAssemblies ) )
				writer.WriteValue( "ReferencedAssemblies", ReferencedAssemblies );
			if( ConvertNulls != c.ConvertNulls )
				writer.WriteBool( "ConvertNulls", ConvertNulls );
			if( DoublePass != c.DoublePass )
				writer.WriteBool( "DoublePass", DoublePass );
			if( Compressed != c.Compressed )
				writer.WriteBool( "Compressed", Compressed );
			if( UseFileCache != c.UseFileCache )
				writer.WriteBool( "UseFileCache", UseFileCache );
			if( TextQuality != c.TextQuality )
				writer.WriteValue( "TextQuality", TextQuality );
			if( SmoothGraphics != c.SmoothGraphics )
				writer.WriteBool( "SmoothGraphics", SmoothGraphics );
			if( Password != c.Password )
				writer.WriteStr( "Password", Password );
			if( InitialPageNumber != c.InitialPageNumber )
				writer.WriteInt( "InitialPageNumber", InitialPageNumber );
			if( MaxPages != c.MaxPages )
				writer.WriteInt( "MaxPages", MaxPages );
			if( StartReportEvent != c.StartReportEvent )
				writer.WriteStr( "StartReportEvent", StartReportEvent );
			if( FinishReportEvent != c.FinishReportEvent )
				writer.WriteStr( "FinishReportEvent", FinishReportEvent );
			ReportInfo.Serialize( writer, c.ReportInfo );
			PrintSettings.Serialize( writer, c.PrintSettings );
			EmailSettings.Serialize( writer, c.EmailSettings );
			if( Styles.Count > 0 )
				writer.Write( Styles );
			writer.Write( Dictionary );
			if( writer.SaveChildren )
			{
				foreach( Base child in ChildObjects )
				{
					writer.Write( child );
				}
			}
		}

		/// <inheritdoc/>
		public override void Deserialize( FRReader reader )
		{
			base.Deserialize( reader );

			// call OnAfterLoad method of each report object
			foreach( Base c in AllObjects )
			{
				c.OnAfterLoad();
			}
		}

		/// <summary>
		/// Saves the report to a stream.
		/// </summary>
		/// <param name="stream">The stream to save to.</param>
		public void Save( Stream stream )
		{
			using( FRWriter writer = new FRWriter() )
			{
				if( IsAncestor )
					writer.GetDiff += new DiffEventHandler( GetDiff );
				writer.Write( this );

				List<Stream> disposeList = new List<Stream>();

				if( Compressed )
				{
					stream = Compressor.Compress( stream );
					disposeList.Add( stream );
				}
				if( !String.IsNullOrEmpty( Password ) )
				{
					stream = Crypter.Encrypt( stream, Password );
					disposeList.Insert( 0, stream );
				}
				writer.Save( stream );

				foreach( Stream s in disposeList )
				{
					s.Dispose();
				}
			}
		}

		/// <summary>
		/// Saves the report to a file.
		/// </summary>
		/// <param name="fileName">The name of the file to save to.</param>
		public void Save( string fileName )
		{
			FileName = fileName;
			using( FileStream f = new FileStream( fileName, FileMode.Create ) )
			{
				Save( f );
			}
		}

		/// <summary>
		/// Loads report from a stream.
		/// </summary>
		/// <param name="stream">The stream to load from.</param>
		/// <remarks>
		/// When you try to load the password-protected report, you will be asked
		/// for a password. You also may specify the password in the <see cref="Password"/>
		/// property before loading the report. In this case the report will load silently.
		/// </remarks>
		public void Load( Stream stream )
		{
			string password = Password;
			Clear();

			using( FRReader reader = new FRReader( this ) )
			{
				List<Stream> disposeList = new List<Stream>();
				if( Compressor.IsStreamCompressed( stream ) )
				{
					stream = Compressor.Decompress( stream, true );
					disposeList.Add( stream );
				}
				bool crypted = Crypter.IsStreamEncrypted( stream );
				if( crypted )
				{
					if( String.IsNullOrEmpty( password ) )
					{
						using( AskPasswordForm form = new AskPasswordForm() )
						{
							if( form.ShowDialog() == DialogResult.OK )
								password = form.Password;
						}
					}
					stream = Crypter.Decrypt( stream, password );
					disposeList.Add( stream );
				}

				try
				{
					reader.Load( stream );
				}
				catch( Exception e )
				{
					if( crypted )
						throw new DecryptException();
					throw e;
				}
				finally
				{
					foreach( Stream s in disposeList )
					{
						try
						{
							s.Dispose();
						}
						catch
						{
						}
					}
				}

				reader.Read( this );
			}
		}

		/// <summary>
		/// Loads the report from a file.
		/// </summary>
		/// <param name="fileName">The name of the file to load from.</param>
		/// <remarks>
		/// When you try to load the password-protected report, you will be asked
		/// for a password. You also may specify the password in the <see cref="Password"/>
		/// property before loading the report. In this case the report will load silently.
		/// </remarks>
		public void Load( string fileName )
		{
			FFileName = "";
			using( FileStream f = new FileStream( fileName, FileMode.Open, FileAccess.Read, FileShare.Read ) )
			{
				FFileName = fileName;
				Load( f );
			}
		}

		/// <summary>
		/// Loads the report from a string.
		/// </summary>
		/// <param name="s">The string that contains a stream in UTF8 or Base64 encoding.</param>
		public void LoadFromString( string s )
		{
			if( String.IsNullOrEmpty( s ) )
				return;

			int startIndex = s.IndexOf( "<?xml version=\"1.0\" encoding=\"utf-8\"?>" );
			if( startIndex != -1 )
			{
				UTF8Encoding encoding = new UTF8Encoding();
				using( MemoryStream stream = new MemoryStream( encoding.GetBytes( s.Substring( startIndex ) ) ) )
				{
					Load( stream );
				}
			}
			else
			{
				using( MemoryStream stream = new MemoryStream( Convert.FromBase64String( s ) ) )
				{
					Load( stream );
				}
			}
		}

		/// <summary>
		/// Saves the report to a string.
		/// </summary>
		/// <returns>The string that contains a stream.</returns>
		public string SaveToString()
		{
			using( MemoryStream stream = new MemoryStream() )
			{
				Save( stream );

				if( Compressed || !String.IsNullOrEmpty( Password ) )
				{
					return Convert.ToBase64String( stream.ToArray() );
				}
				else
				{
					UTF8Encoding encoding = new UTF8Encoding();
					return encoding.GetString( stream.ToArray() );
				}
			}
		}

		/// <summary>
		/// Saves the report to a string using the Base64 encoding.
		/// </summary>
		/// <returns>The string that contains a stream.</returns>
		public string SaveToStringBase64()
		{
			using( MemoryStream stream = new MemoryStream() )
			{
				Save( stream );
				return Convert.ToBase64String( stream.ToArray() );
			}
		}

		/// <summary>
		/// Creates the report instance and loads the report from a stream.
		/// </summary>
		/// <param name="stream">The stream to load from.</param>
		/// <returns>The new report instance.</returns>
		public static Report FromStream( Stream stream )
		{
			Report result = new Report();
			result.Load( stream );
			return result;
		}

		/// <summary>
		/// Creates the report instance and loads the report from a file.
		/// </summary>
		/// <param name="fileName">The name of the file to load from.</param>
		/// <returns>The new report instance.</returns>
		public static Report FromFile( string fileName )
		{
			Report result = new Report();
			result.Load( fileName );
			return result;
		}

		/// <summary>
		/// Creates the report instance and loads the report from a string.
		/// </summary>
		/// <param name="utf8String">The string that contains a stream in UTF8 encoding.</param>
		/// <returns>The new report instance.</returns>
		public static Report FromString( string utf8String )
		{
			Report result = new Report();
			result.LoadFromString( utf8String );
			return result;
		}

		/// <summary>
		/// Runs the report designer.
		/// </summary>
		/// <returns><b>true</b> if report was modified, otherwise <b>false</b>.</returns>
		public bool Design()
		{
			return Design( true );
		}

		/// <summary>
		/// Runs the report designer.
		/// </summary>
		/// <param name="modal">A value indicates whether the designer should run modally.</param>
		/// <returns><b>true</b> if report was modified, otherwise <b>false</b>.</returns>
		public bool Design( bool modal )
		{
			return Design( modal, null );
		}

		/// <summary>
		/// Runs the report designer.
		/// </summary>
		/// <param name="mdiParent">The main MDI form which will be a parent for the designer.</param>
		/// <returns><b>true</b> if report was modified, otherwise <b>false</b>.</returns>
		public bool Design( Form mdiParent )
		{
			return Design( false, mdiParent );
		}

		private bool Design( bool modal, Form mdiParent )
		{
			if( FDesigner != null )
				return false;

			EnsureInit();

			if( Config.SplashScreenEnabled )
				ShowSplashScreen();

			FDesignerForm = new DesignerForm( true );
			( FDesignerForm as DesignerForm ).Designer.Report = this;

			FDesignerForm.MdiParent = mdiParent;
			FDesignerForm.ShowInTaskbar = Config.DesignerSettings.ShowInTaskbar;
			FDesignerForm.FormClosed += new FormClosedEventHandler( OnCloseDesigner );

			if( modal )
				FDesignerForm.ShowDialog();
			else
				FDesignerForm.Show();

			return FModified;
		}

		internal bool DesignPreviewPage()
		{
			Designer saveDesigner = Designer;

			Form designerForm = new DesignerForm();
			FDesigner = ( designerForm as DesignerForm ).Designer;

			try
			{
				FDesigner.Restrictions.DontChangePageOptions = true;
				FDesigner.Restrictions.DontChangeReportOptions = true;
				FDesigner.Restrictions.DontCreatePage = true;
				FDesigner.Restrictions.DontCreateReport = true;
				FDesigner.Restrictions.DontDeletePage = true;
				FDesigner.Restrictions.DontEditData = true;
				FDesigner.Restrictions.DontInsertBand = true;
				FDesigner.Restrictions.DontLoadReport = true;
				FDesigner.Restrictions.DontPreviewReport = true;
				FDesigner.Restrictions.DontShowRecentFiles = true;
				FDesigner.IsPreviewPageDesigner = true;
				FDesigner.Report = this;
				FDesigner.SelectionChanged( null );
				designerForm.ShowDialog();
			}
			finally
			{
				FModified = FDesigner.Modified;
				designerForm.Dispose();
				FDesigner = saveDesigner;
			}
			return FModified;
		}

		/// <summary>
		/// Registers the application dataset with all its tables and relations to use it in the report.
		/// </summary>
		/// <param name="data">The application data.</param>
		/// <remarks>
		/// If you register more than one dataset, use the <see cref="RegisterData(DataSet, string)"/> method.
		/// </remarks>
		/// <example>
		/// <code>
		/// report1.Load("report.frx");
		/// report1.RegisterData(dataSet1);
		/// </code>
		/// </example>
		public void RegisterData( DataSet data )
		{
			Dictionary.RegisterDataSet( data, "Data", false );
		}

		/// <summary>
		/// Registers the application dataset with specified name.
		/// </summary>
		/// <param name="data">The application data.</param>
		/// <param name="name">The name of the data.</param>
		/// <remarks>
		/// Use this method if you register more than one dataset. You may specify any value
		/// for the <b>name</b> parameter: it is not displayed anywhere in the designer and used only 
		/// to load/save a report. The name must be persistent and unique for each registered dataset.
		/// </remarks>
		/// <example>
		/// <code>
		/// report1.Load("report.frx");
		/// report1.RegisterData(dataSet1, "NorthWind");
		/// </code>
		/// </example>
		public void RegisterData( DataSet data, string name )
		{
			if( FInitializing )
			{
				FInitializeData = data;
				FInitializeDataName = name;
			}
			else
				Dictionary.RegisterDataSet( data, name, false );
		}

		/// <summary>
		/// Registers the application data table to use it in the report.
		/// </summary>
		/// <param name="data">The application data.</param>
		/// <param name="name">The name of the data.</param>
		/// <example>
		/// <code>
		/// report1.Load("report.frx");
		/// report1.RegisterData(dataSet1.Tables["Orders"], "Orders");
		/// </code>
		/// </example>
		public void RegisterData( DataTable data, string name )
		{
			Dictionary.RegisterDataTable( data, name, false );
		}

		/// <summary>
		/// Registers the application data view to use it in the report.
		/// </summary>
		/// <param name="data">The application data.</param>
		/// <param name="name">The name of the data.</param>
		/// <example>
		/// <code>
		/// report1.Load("report.frx");
		/// report1.RegisterData(myDataView, "OrdersView");
		/// </code>
		/// </example>
		public void RegisterData( DataView data, string name )
		{
			Dictionary.RegisterDataView( data, name, false );
		}

		/// <summary>
		/// Registers the application data relation to use it in the report.
		/// </summary>
		/// <param name="data">The application data.</param>
		/// <param name="name">The name of the data.</param>
		/// <remarks>
		/// You may specify any value for the <b>name</b> parameter: it is not displayed anywhere 
		/// in the designer and used only to load/save a report. The name must be persistent 
		/// and unique for each registered relation.
		/// </remarks>
		/// <example>
		/// <code>
		/// report1.Load("report.frx");
		/// report1.RegisterData(myDataRelation, "myRelation");
		/// </code>
		/// </example>
		public void RegisterData( DataRelation data, string name )
		{
			Dictionary.RegisterDataRelation( data, name, false );
		}

		/// <summary>
		/// <b>Obsolete</b>. Registers the application business object to use it in the report. 
		/// </summary>
		/// <param name="data">Application data.</param>
		/// <param name="name">Name of the data.</param>
		/// <param name="flags">Not used.</param>
		/// <param name="maxNestingLevel">Maximum nesting level of business objects.</param>
		/// <remarks>
		/// This method is obsolete. Use the <see cref="RegisterData(IEnumerable, string)"/> method instead.
		/// </remarks>
		public void RegisterData( IEnumerable data, string name, BOConverterFlags flags, int maxNestingLevel )
		{
			RegisterData( data, name, maxNestingLevel );
		}

		/// <summary>
		/// Registers the application business object to use it in the report.
		/// </summary>
		/// <param name="data">Application data.</param>
		/// <param name="name">Name of the data.</param>
		/// <example>
		/// <code>
		/// report1.Load("report.frx");
		/// report1.RegisterData(myBusinessObject, "Customers");
		/// </code>
		/// </example>
		public void RegisterData( IEnumerable data, string name )
		{
			if( FInitializing )
			{
				FInitializeData = data;
				FInitializeDataName = name;
			}
			else
				Dictionary.RegisterBusinessObject( data, name, 1, false );
		}

		/// <summary>
		/// Registers the application business object to use it in the report.
		/// </summary>
		/// <param name="data">Application data.</param>
		/// <param name="name">Name of the data.</param>
		/// <param name="maxNestingLevel">Maximum nesting level of business objects.</param>
		/// <remarks>
		/// This method creates initial datasource with specified nesting level. It is useful if
		/// you create a report in code. In most cases, you don't need to specify the nesting level
		/// because it may be selected in the designer's "Choose Report Data" dialog.
		/// </remarks>
		public void RegisterData( IEnumerable data, string name, int maxNestingLevel )
		{
			Dictionary.RegisterBusinessObject( data, name, maxNestingLevel, false );
		}

		/// <summary>
		/// Prepares the report.
		/// </summary>
		/// <returns><b>true</b> if report was prepared succesfully.</returns>
		public bool Prepare()
		{
			return Prepare( false );
		}

		/// <summary>
		/// Prepares the report.
		/// </summary>
		/// <param name="append">Specifies whether the new report should be added to a
		/// report that was prepared before.</param>
		/// <returns><b>true</b> if report was prepared succesfully.</returns>
		/// <remarks>
		/// Use this method to merge prepared reports.
		/// </remarks>
		/// <example>This example shows how to merge two reports and preview the result:
		/// <code>
		/// Report report = new Report();
		/// report.Load("report1.frx");
		/// report.Prepare();
		/// report.Load("report2.frx");
		/// report.Prepare(true);
		/// report.ShowPrepared();
		/// </code>
		/// </example>
		public bool Prepare( bool append )
		{
			SetRunning( true );
			try
			{
				if( PreparedPages == null || !append )
				{
					if( FPreview != null )
						FPreview.ClearTabsExceptFirst();
					else if( FPreparedPages != null )
						FPreparedPages.Clear();
					SetPreparedPages( new Preview.PreparedPages( this ) );
				}
				FEngine = new ReportEngine( this );

				if( !Config.WebMode )
					StartPerformanceCounter();

				try
				{
					Compile();
					return Engine.Run( true, append, true );
				}
				finally
				{
					if( !Config.WebMode )
						StopPerformanceCounter();
				}
			}
			finally
			{
				SetRunning( false );
			}
		}


		/// <summary>
		/// For internal use only.
		/// </summary>
		[EditorBrowsable( EditorBrowsableState.Never )]
		public void PreparePhase1()
		{
			SetRunning( true );
			if( FPreparedPages != null )
				FPreparedPages.Clear();
			SetPreparedPages( new Preview.PreparedPages( this ) );
			FEngine = new ReportEngine( this );
			Compile();
			Engine.RunPhase1();
		}

		/// <summary>
		/// For internal use only.
		/// </summary>
		[EditorBrowsable( EditorBrowsableState.Never )]
		public void PreparePhase2()
		{
			Engine.RunPhase2();
			SetRunning( false );
		}

		/// <summary>
		/// Refresh the current report.
		/// </summary>
		/// <remarks>
		/// Call this method in the Click or MouseUp event handler of a report object to refresh 
		/// the currently previewed report. Report will be generated again, but without dialog forms.
		/// </remarks>
		public void Refresh()
		{
			FNeedRefresh = true;
		}

		/// <summary>
		///  Refresh prepared report after intercative actions.
		/// </summary>
		public void InteractiveRefresh()
		{
			PreparedPages.ClearPageCache();
			InternalRefresh();
		}

		internal void InternalRefresh()
		{
			SetRunning( true );
			try
			{
				Engine.Run( false, false, false );
			}
			finally
			{
				SetRunning( false );
			}
		}

		/// <summary>
		/// Prepare page
		/// </summary>
		/// <param name="page"></param>
		public void PreparePage( ReportPage page )
		{
			SetRunning( true );
			try
			{
				Engine.Run( false, false, false, page );
			}
			finally
			{
				SetRunning( false );
			}
		}

		/// <summary>
		/// Prepares the report and shows it in the preview window.
		/// </summary>
		public void Show()
		{
			Show( true, null );
		}

		/// <summary>
		/// Prepares the report and shows it in the preview window.
		/// </summary>
		/// <param name="modal">A value that specifies whether the preview window should be modal.</param>
		public void Show( bool modal )
		{
			Show( modal, null );
		}

		/// <summary>
		/// Prepares the report and shows it in the preview window.
		/// </summary>
		/// <param name="modal">A value that specifies whether the preview window should be modal.</param>
		/// <param name="owner">The owner of the preview window.</param>
		public void Show( bool modal, IWin32Window owner )
		{
			if( Prepare() )
				ShowPrepared( modal, null, owner );
			else if( Preview != null )
			{
				Preview.Clear();
				Preview.Refresh();
			}
		}

		/// <summary>
		/// Prepares the report and shows it in the preview window.
		/// </summary>
		/// <param name="mdiParent">The main MDI form which will be a parent for the preview window.</param>
		public void Show( Form mdiParent )
		{
			if( Prepare() )
				ShowPrepared( false, mdiParent, null );
			else if( Preview != null )
			{
				Preview.Clear();
				Preview.Refresh();
			}
		}

		/// <summary>
		/// Prepares the report and prints it.
		/// </summary>
		public void Print()
		{
			if( Prepare() )
				PrintPrepared();
		}

		/// <summary>
		/// Previews the report. The report should be prepared using the <see cref="Prepare()"/> method.
		/// </summary>
		public void ShowPrepared()
		{
			ShowPrepared( true );
		}

		/// <summary>
		/// Previews the prepared report.
		/// </summary>
		/// <param name="modal">A value that specifies whether the preview window should be modal.</param>
		public void ShowPrepared( bool modal )
		{
			ShowPrepared( modal, null, null );
		}

		/// <summary>
		/// Previews the prepared report.
		/// </summary>
		/// <param name="modal">A value that specifies whether the preview window should be modal.</param>
		/// <param name="owner">The owner of the preview window.</param>
		public void ShowPrepared( bool modal, IWin32Window owner )
		{
			ShowPrepared( modal, null, owner );
		}

		/// <summary>
		/// Previews the prepared report.
		/// </summary>
		/// <param name="mdiParent">The main MDI form which will be a parent for the preview window.</param>
		public void ShowPrepared( Form mdiParent )
		{
			ShowPrepared( false, mdiParent, null );
		}

		// Simon: ×¢ÊÍÔ­·½·¨
		//private void ShowPrepared( bool modal, Form mdiParent, IWin32Window owner )
		//{
		//	// create preview form
		//	if( Preview == null )
		//	{
		//		FPreviewForm = new PreviewForm();

		//		FPreviewForm.MdiParent = mdiParent;
		//		FPreviewForm.ShowInTaskbar = Config.PreviewSettings.ShowInTaskbar;
		//		FPreviewForm.TopMost = Config.PreviewSettings.TopMost;
		//		FPreviewForm.Icon = Config.PreviewSettings.Icon;

		//		if( String.IsNullOrEmpty( Config.PreviewSettings.Text ) )
		//		{
		//			FPreviewForm.Text = String.IsNullOrEmpty( ReportInfo.Name ) ? "" : ReportInfo.Name + " - ";
		//			FPreviewForm.Text += Res.Get( "Preview" );
		//		}
		//		else
		//			FPreviewForm.Text = Config.PreviewSettings.Text;

		//		FPreviewForm.FormClosed += new FormClosedEventHandler( OnClosePreview );

		//		Preview = ( FPreviewForm as PreviewForm ).Preview;
		//		Preview.UIStyle = Config.UIStyle;
		//		Preview.FastScrolling = Config.PreviewSettings.FastScrolling;
		//		Preview.Buttons = Config.PreviewSettings.Buttons;
		//		Preview.Exports = Config.PreviewSettings.Exports;
		//		Preview.Clouds = Config.PreviewSettings.Clouds;
		//	}

		//	if( Config.ReportSettings.ShowPerformance )
		//		try
		//		{
		//			// in case the format string is wrong, use try/catch
		//			Preview.ShowPerformance( String.Format( Res.Get( "Messages,Performance" ), FTickCount ) );
		//		}
		//		catch
		//		{
		//		}
		//	Preview.ClearTabsExceptFirst();
		//	if( PreparedPages != null )
		//		Preview.AddPreviewTab( this, GetReportName, null, true );

		//	Config.PreviewSettings.OnPreviewOpened( Preview );
		//	if( ReportInfo.SavePreviewPicture && PreparedPages.Count > 0 )
		//		SavePreviewPicture();

		//	if( FPreviewForm != null && !FPreviewForm.Visible )
		//	{
		//		if( modal )
		//			FPreviewForm.ShowDialog( owner );
		//		else
		//		{
		//			if( mdiParent == null )
		//				FPreviewForm.Show( owner );
		//			else
		//				FPreviewForm.Show();
		//		}
		//	}
		//}

		/// <summary>
		/// Prints the report with the "Print" dialog. 
		/// Report should be prepared using the <see cref="Prepare()"/> method.
		/// </summary>
		public void PrintPrepared()
		{
			if( PreparedPages != null )
				PreparedPages.Print();
		}

		/// <summary>
		/// Prints the report without the "Print" dialog. 
		/// Report should be prepared using the <see cref="Prepare()"/> method.
		/// </summary>
		/// <param name="printerSettings">Printer-specific settings.</param>
		/// <example>
		/// Use the following code if you want to show the "Print" dialog, then print:
		/// <code>
		/// if (report.Prepare())
		/// {
		///   PrinterSettings printerSettings = null;
		///   if (report.ShowPrintDialog(out printerSettings))
		///   {
		///     report.PrintPrepared(printerSettings);
		///   }
		/// }
		/// </code>
		/// </example>
		public void PrintPrepared( PrinterSettings printerSettings )
		{
			if( PreparedPages != null )
				PreparedPages.Print( printerSettings, 1 );
		}

		/// <summary>
		/// Shows the "Print" dialog.
		/// </summary>
		/// <param name="printerSettings">Printer-specific settings.</param>
		/// <returns><b>true</b> if the dialog was closed by "Print" button.</returns>
		/// <example>
		/// Use the following code if you want to show the "Print" dialog, then print:
		/// <code>
		/// if (report.Prepare())
		/// {
		///   PrinterSettings printerSettings = null;
		///   if (report.ShowPrintDialog(out printerSettings))
		///   {
		///     report.PrintPrepared(printerSettings);
		///   }
		/// }
		/// </code>
		/// </example>
		public bool ShowPrintDialog( out PrinterSettings printerSettings )
		{
			printerSettings = null;

			using( PrinterSetupForm dialog = new PrinterSetupForm() )
			{
				dialog.Report = this;
				dialog.PrintDialog = true;
				if( dialog.ShowDialog() != DialogResult.OK )
					return false;
				printerSettings = dialog.PrinterSettings;
			}

			return true;
		}

		/// <summary>
		/// Exports a report. Report should be prepared using the <see cref="Prepare()"/> method.
		/// </summary>
		/// <param name="export">The export filter.</param>
		/// <param name="stream">Stream to save export result to.</param>
		public void Export( ExportBase export, Stream stream )
		{
			export.Export( this, stream );
		}

		/// <summary>
		/// Exports a report. Report should be prepared using the <see cref="Prepare()"/> method.
		/// </summary>
		/// <param name="export">The export filter.</param>
		/// <param name="fileName">File name to save export result to.</param>
		public void Export( ExportBase export, string fileName )
		{
			export.Export( this, fileName );
		}

		/// <summary>
		/// Saves the prepared report. Report should be prepared using the <see cref="Prepare()"/> method.
		/// </summary>
		/// <param name="fileName">File name to save to.</param>
		public void SavePrepared( string fileName )
		{
			if( PreparedPages != null )
				PreparedPages.Save( fileName );
		}

		/// <summary>
		/// Saves the prepared report. Report should be prepared using the <see cref="Prepare()"/> method.
		/// </summary>
		/// <param name="stream">Stream to save to.</param>
		public void SavePrepared( Stream stream )
		{
			if( PreparedPages != null )
				PreparedPages.Save( stream );
		}

		/// <summary>
		/// Loads the prepared report from a .fpx file.
		/// </summary>
		/// <param name="fileName">File name to load form.</param>
		public void LoadPrepared( string fileName )
		{
			if( PreparedPages == null )
				SetPreparedPages( new FastReport.Preview.PreparedPages( this ) );
			PreparedPages.Load( fileName );
		}

		/// <summary>
		/// Loads the prepared report from a .fpx file.
		/// </summary>
		/// <param name="stream">Stream to load from.</param>
		public void LoadPrepared( Stream stream )
		{
			if( PreparedPages == null )
				SetPreparedPages( new FastReport.Preview.PreparedPages( this ) );
			PreparedPages.Load( stream );
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="Report"/> class with default settings.
		/// </summary>
		public Report()
		{
			FPages = new PageCollection( this );
			FReportInfo = new ReportInfo();
			FPrintSettings = new PrintSettings();
			FEmailSettings = new EmailSettings();
			FStyles = new StyleCollection();
			Dictionary = new Dictionary();
			FGraphicCache = new GraphicCache();
			FAssemblies = new AssemblyCollection();
			FCachedDataItems = new Hashtable();
			FStoreInResources = true;
			FFileName = "";
			FAutoFillDataSet = true;
			ClearReportProperties();
			SetFlags( Flags.CanMove | Flags.CanResize | Flags.CanDelete | Flags.CanEdit | Flags.CanChangeOrder |
			  Flags.CanChangeParent | Flags.CanCopy, false );
			//FInlineImageCache = new InlineImageCache();
		}

		static Report()
		{
			Config.Init();
		}

		internal static void EnsureInit()
		{
			// do nothing, just ensure that static constructor is called.
		}

		/// <summary>
		/// Create name for all unnamed elements with prefix and start with number
		/// </summary>
		/// <param name="prefix">Prefix for name</param>
		/// <param name="number">Number from which to start</param>
		public void PostNameProcess( string prefix, int number )
		{
			int i = number;

			foreach( Base obj in AllObjects )
			{
				if( String.IsNullOrEmpty( obj.Name ) )
				{
					obj.SetName( prefix + i.ToString() );
					i++;
				}
			}
		}

		private class CachedDataItem
		{
			public DataSourceBase DataSource;
			public Column Column;
		}
	}
}