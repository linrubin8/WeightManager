using System;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using FastReport.Design;
using System.Web;

namespace FastReport.Utils
{
  /// <summary>
  /// Contains some configuration properties and settings that will be applied to the FastReport.Net 
  /// environment, including Report, Designer and Preview components.
  /// </summary>
  public static class Config
  {
    private static XmlDocument FDoc = new XmlDocument();
    private static string FFolder = null;
    private static string FTempFolder = null;
    private static string FLogs = "";
    private static bool FWebMode;
    private static ReportSettings FReportSettings = new ReportSettings();
    private static PreviewSettings FPreviewSettings = new PreviewSettings();
    private static DesignerSettings FDesignerSettings = new DesignerSettings();
    private static FastReport.Export.Email.EmailSettings FEmailSettings = new FastReport.Export.Email.EmailSettings();
    private static UIStyle FUIStyle = UIStyle.VisualStudio2012Light;
    private static bool FUseRibbon = true;
    private static bool FSplashScreenEnabled = false;
    private static CultureInfo engCultureInfo = new CultureInfo("en-US");
    private static bool FRightToLeft = false;

    /// <summary>
    /// Gets a value indicating that the ASP.NET hosting permission level is set to full trust.
    /// </summary>
    public static bool FullTrust 
    {
      get { return GetCurrentTrustLevel() == AspNetHostingPermissionLevel.Unrestricted; }
    }

    /// <summary>
    /// Gets or sets the settings for the Report component.
    /// </summary>
    public static ReportSettings ReportSettings
    {
      get { return FReportSettings; }
      set { FReportSettings = value; }
    }

    /// <summary>
    /// Gets or sets the settings for the preview window.
    /// </summary>
    public static PreviewSettings PreviewSettings
    {
      get { return FPreviewSettings; }
      set { FPreviewSettings = value; }
    }

    /// <summary>
    /// Gets or sets the settings for the report designer window.
    /// </summary>
    public static DesignerSettings DesignerSettings
    {
      get { return FDesignerSettings; }
      set { FDesignerSettings = value; }
    }

    /// <summary>
    /// Gets or sets the settings for the "Send Email" window.
    /// </summary>
    public static FastReport.Export.Email.EmailSettings EmailSettings
    {
      get { return FEmailSettings; }
      set { FEmailSettings = value; }
    }

    /// <summary>
    /// Gets or sets the UI style.
    /// </summary>
    /// <remarks>
    /// This property affects both designer and preview windows.
    /// </remarks>
    public static UIStyle UIStyle
    {
      get { return FUIStyle; }
      set { FUIStyle = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the Ribbon UI should be used
    /// </summary>
    public static bool UseRibbon
    {
      get { return FUseRibbon; }
      set { FUseRibbon = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether SplashScreen should be displayed while loading designer
    /// </summary>
    public static bool SplashScreenEnabled
    {
      get { return FSplashScreenEnabled; }
      set { FSplashScreenEnabled = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether Welcome window feature enabled.
    /// If false, interface elements associated with the Welcome window will not be visible.
    /// </summary>
    public static bool WelcomeEnabled
    {
      get { return Root.FindItem("Designer").FindItem("Welcome").GetProp("Enabled") != "False"; }
      set { Root.FindItem("Designer").FindItem("Welcome").SetProp("Enabled", Converter.ToString(value)); }
    }

    /// <summary>
    /// Gets or sets a value indicating whether Welcome window shoud be displayed on startup
    /// </summary>
    public static bool WelcomeShowOnStartup
    {
      get { return Root.FindItem("Designer").FindItem("Welcome").GetProp("ShowOnStartup") != "False"; }
      set { Root.FindItem("Designer").FindItem("Welcome").SetProp("ShowOnStartup", Converter.ToString(value)); }
    }
    
    /// <summary>
    /// Gets FastReport version.
    /// </summary>
    public static string Version
    {
      get { return typeof(Report).Assembly.GetName().Version.ToString(3); }
    }
    
    /// <summary>
    /// Gets the root item of config xml.
    /// </summary>
    public static XmlItem Root
    {
      get { return FDoc.Root; }
    }
    
    /// <summary>
    /// Gets or sets the path used to load/save the configuration file.
    /// </summary>
    /// <remarks>
    /// By default, the configuration file is saved to the application local data folder
    /// (C:\Documents and Settings\User_Name\Local Settings\Application Data\FastReport\).
    /// Set this property to "" if you want to store the configuration file in the application folder.
    /// </remarks>
    public static string Folder
    {
      get { return FFolder; }
      set { FFolder = value; }
    }

    /// <summary>
    /// Gets or sets the path to the temporary folder used to store temporary files.
    /// </summary>
    /// <remarks>
    /// The default value is <b>null</b>, so the system temp folder will be used.
    /// </remarks>
    public static string TempFolder
    {
      get { return FTempFolder; }
      set { FTempFolder = value; }
    }

    /// <summary>
    /// Gets the application folder.
    /// </summary>
    public static string ApplicationFolder
    {
      get { return AppDomain.CurrentDomain.BaseDirectory; }
    }

    /// <summary>
    /// Gets the folder to store auto save files
    /// </summary>
    public static string AutoSaveFolder
    {
        get { return Path.Combine(Path.GetTempPath(), "FastReport"); }
    }

    /// <summary>
    /// Gets the autosaved report
    /// </summary>
    public static string AutoSaveFile
    {
        get { return Path.Combine(AutoSaveFolder, "autosave.frx"); }
    }

    /// <summary>
    /// Gets the atuosaved report path
    /// </summary>
    public static string AutoSaveFileName
    {
        get { return Path.Combine(AutoSaveFolder, "autosave.txt"); }
    }

    /// <summary>
    /// Gets or sets a value that determines whether to disable some functionality to run in web mode.
    /// </summary>
    /// <remarks>
    /// Use this property if you use FastReport in ASP.Net. Set this property to <b>true</b> <b>before</b>
    /// you access any FastReport.Net objects.
    /// </remarks>
    public static bool WebMode
    {
      get { return FWebMode; }
      set 
      { 
        FWebMode = value;
        if (value)
          ReportSettings.ShowProgress = false;
      }
    }

    /// <summary>
    /// Gets an english culture information for localization purposes 
    /// </summary>
    public static CultureInfo EngCultureInfo
    {
        get { return engCultureInfo; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether RTL layout should be used.
    /// </summary>
    public static bool RightToLeft
    {
        get { return FRightToLeft; }
        set { FRightToLeft = value; }
    }

    private static AspNetHostingPermissionLevel GetCurrentTrustLevel()
    {
      foreach (AspNetHostingPermissionLevel trustLevel in
          new AspNetHostingPermissionLevel[] {
              AspNetHostingPermissionLevel.Unrestricted,
              AspNetHostingPermissionLevel.High,
              AspNetHostingPermissionLevel.Medium,
              AspNetHostingPermissionLevel.Low,
              AspNetHostingPermissionLevel.Minimal
      })
      {
        try
        {
          new AspNetHostingPermission(trustLevel).Demand();
        }
        catch (System.Security.SecurityException)
        {
          continue;
        }
        return trustLevel;
      }
      return AspNetHostingPermissionLevel.None;
    }

    private static void ProcessAssemblies()
    {
      foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
      {
        ProcessAssembly(a);
      }
    }

    private static void ProcessAssembly(Assembly a)
    {
      foreach (Type t in a.GetTypes())
      {
        if (t != typeof(AssemblyInitializerBase) && t.IsSubclassOf(typeof(AssemblyInitializerBase)))
          Activator.CreateInstance(t);
      }
    }

    private static void LoadPlugins()
    {
      // main assembly initializer
      new AssemblyInitializer();

      XmlItem pluginsItem = Root.FindItem("Plugins");
      for (int i = 0; i < pluginsItem.Count; i++)
      {
        XmlItem item = pluginsItem[i];
        string pluginName = item.GetProp("Name");

        try
        {
          ProcessAssembly(Assembly.LoadFrom(pluginName));
        }
        catch
        {
        }
      }
    }

    private static void LoadConfig()
    {
        bool configLoaded = false;
        if (!Config.WebMode)
        {
            try
            {
                if (Folder == null)
                {
                    string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); 
                    Folder = Path.Combine(baseFolder, "FastReport");
                    // commented by Samuray
                    //if (!Directory.Exists(Folder))
                    //    Directory.CreateDirectory(Folder);
                }
                else if (Folder == "")
                    Folder = ApplicationFolder;
            }
            catch
            {
            }

            string fileName = Path.Combine(Folder, "FastReport.config");

            if (File.Exists(fileName))
            {
                try
                {
                    FDoc.Load(fileName);
                    configLoaded = true;
                }
                catch
                {
                }
            }

            RestoreUIStyle();
            RestoreRightToLeft();
            Res.LoadDefaultLocale();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
        }
        if (!configLoaded)
        {
            // load default config
            using (Stream stream = ResourceLoader.GetStream("FastReport.config"))
            {
                FDoc.Load(stream);
            }
        }
    }

    private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    {      
      FDoc.Root.Name = "Config";
      FDoc.AutoIndent = true;
      SaveUIStyle();
      if (!WebMode)
      {
          try
          {
              // added by Samuray
              if (!Directory.Exists(Folder))
                  Directory.CreateDirectory(Folder);
              FDoc.Save(Path.Combine(Folder, "FastReport.config"));
              if (FLogs != "")
                  File.WriteAllText(Path.Combine(Folder, "FastReport.logs"), FLogs);
          }
          catch
          {
          }
      }
    }

    private static void SaveUIStyle()
    {
      XmlItem xi = Root.FindItem("UIStyleNew");
      xi.SetProp("Style", Converter.ToString(UIStyle));
      xi.SetProp("Ribbon", Converter.ToString(UseRibbon));
    }

    private static void RestoreUIStyle()
    {
        XmlItem xi = Root.FindItem("UIStyleNew");
        string style = xi.GetProp("Style");

        if (!String.IsNullOrEmpty(style))
        {
            try
            {
                UIStyle = (UIStyle)Converter.FromString(typeof(UIStyle), style);
            }
            catch
            {
                UIStyle = Utils.UIStyle.VisualStudio2012Light;
            }
        }

        string ribbon = xi.GetProp("Ribbon");

        if (!String.IsNullOrEmpty(ribbon))
        {
            FUseRibbon = ribbon != "False";
        }
    }

    private static void RestoreRightToLeft()
    {
        XmlItem xi = Root.FindItem("UIOptions");
        string rtl = xi.GetProp("RightToLeft");

        if (!String.IsNullOrEmpty(rtl))
        {
            switch (rtl)
            {
                case "Auto":
                    RightToLeft = CultureInfo.CurrentCulture.TextInfo.IsRightToLeft;
                    break;
                case "No":
                    RightToLeft = false;
                    break;
                case "Yes":
                    RightToLeft = true;
                    break;
                default:
                    RightToLeft = false;
                    break;
            }
        }
    }

    internal static void WriteLogString(string s)
    {
      WriteLogString(s, false);
    }

    internal static void WriteLogString(string s, bool distinct)
    {
      if (distinct)
      {
        if (FLogs.IndexOf(s + "\r\n") != -1)
          return;
      }
      FLogs += s + "\r\n";
    }

    internal static string CreateTempFile(string dir)
    {
      if (String.IsNullOrEmpty(dir))
        return Path.GetTempFileName();
      if (dir[dir.Length - 1] != '\\')
        dir += '\\';
      return dir + Path.GetRandomFileName();
    }

    internal static string GetTempFolder()
    {
      return TempFolder == null ? Path.GetTempPath() : TempFolder;
    }

    internal static void Init()
    {
      if (!WebMode)
        LoadConfig();

      LoadPlugins();
      
      // init TextRenderingHint.SystemDefault
      // bug in .Net: if you use any other hint before SystemDefault, the SystemDefault will 
      // look like SingleBitPerPixel
      using (Bitmap bmp = new Bitmap(1, 1))
      using (Graphics g = Graphics.FromImage(bmp))
      {
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
        g.DrawString(" ", SystemFonts.DefaultFont, Brushes.Black, 0, 0);
      }
    }
    
    /// <summary>
    /// Saves the form state to the configuration file.
    /// </summary>
    /// <param name="form">The form to save.</param>
    public static void SaveFormState(Form form)
    {
      XmlItem xi = FDoc.Root.FindItem("Forms").FindItem(form.Name);
      xi.SetProp("Maximized", form.WindowState == FormWindowState.Maximized ? "1" : "0");
      xi.SetProp("Left", form.WindowState == FormWindowState.Minimized ? "0" : form.Location.X.ToString());
      xi.SetProp("Top", form.WindowState == FormWindowState.Minimized ? "0" : form.Location.Y.ToString());
      xi.SetProp("Width", form.Size.Width.ToString());
      xi.SetProp("Height", form.Size.Height.ToString());
    }

    /// <summary>
    /// Restores the form state from the configuration file.
    /// </summary>
    /// <param name="form">The form to restore.</param>
    public static void RestoreFormState(Form form)
    {
      RestoreFormState(form, false);
    }

    // we need this to prevent form.Load event to be fired *after* the form is displayed.
    // Used in the StandardDesignerForm.Load event
    internal static bool RestoreFormState(Form form, bool ignoreWindowState)
    {
      XmlItem xi = FDoc.Root.FindItem("Forms").FindItem(form.Name);
      if (!ignoreWindowState)
        form.WindowState = xi.GetProp("Maximized") == "1" ? FormWindowState.Maximized : FormWindowState.Normal;
      string left = xi.GetProp("Left");
      string top = xi.GetProp("Top");
      string width = xi.GetProp("Width");
      string height = xi.GetProp("Height");
      if (left != "" && top != "")
        form.Location = new Point(int.Parse(left), int.Parse(top));
      if (width != "" && height != "")
        form.Size = new Size(int.Parse(width), int.Parse(height));
      return xi.GetProp("Maximized") == "1";  
    }
  }
}