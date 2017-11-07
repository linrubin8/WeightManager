using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Win32;
using System.Globalization;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Utils
{
  /// <summary>
  /// Used to get localized values from the language resource file.
  /// </summary>
  /// <remarks>
  /// The resource file used by default is english. To load another locale, call 
  /// the <see cref="Res.LoadLocale(string)"/> method. It should be done at application start
  /// before you use any FastReport classes.
  /// </remarks>
  public static partial class Res
  {
    private static ImageList FImageList;
    private static List<Bitmap> FImages;
    private static XmlDocument FLocale;
    private static XmlDocument FBuiltinLocale;
    private static bool FLocaleLoaded = false;
    private static bool FImagesLoaded = false;
    private static string FBadResult = "NOT LOCALIZED!";

    /// <summary>
    /// Gets or set the folder that contains localization files (*.frl).
    /// </summary>
    public static string LocaleFolder
    {
      get 
      {
        Report.EnsureInit();
        string folder = Config.Root.FindItem("Language").GetProp("Folder");
        
        // check the registry
        if (String.IsNullOrEmpty(folder))
        {
          RegistryKey key = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("FastReports");
          if (key != null)
          {
            key = key.OpenSubKey("FastReport.Net");
            if (key != null)
              folder = (string)key.GetValue("LocalizationFolder", "");
          }
        }
        
        // get application folder
        if (String.IsNullOrEmpty(folder))
          folder = Config.ApplicationFolder;

        if (!folder.EndsWith("\\"))
          folder += "\\";
        return folder;  
      }
      set { Config.Root.FindItem("Language").SetProp("Folder", value); }
    }

    /// <summary>
    /// Returns the current UI locale name, for example "en".
    /// </summary>
    public static string LocaleName
    {
      get 
      {
        if (!FLocaleLoaded)
          LoadBuiltinLocale();
        return FLocale.Root.GetProp("Name"); 
      }
    }

    internal static string DefaultLocaleName
    {
      get { return Config.Root.FindItem("Language").GetProp("Name"); }
      set { Config.Root.FindItem("Language").SetProp("Name", value); }
    }

    private static void LoadBuiltinLocale()
    {
      FLocale = new XmlDocument();
      FBuiltinLocale = FLocale;
      using (Stream stream = ResourceLoader.GetStream("en.xml"))
      {
        FLocale.Load(stream);
      }
      FLocaleLoaded = true;
    }

	  // Simon: 注释此方法
	//private static void LoadImages()
	//{
	//  FImagesLoaded = true;
	//  FImages = new List<Bitmap>();

	//  using (Bitmap images = ResourceLoader.GetBitmap("buttons.png"))
	//  {
	//	int x = 0; 
	//	int y = 0;
	//	bool done = false;

	//	do
	//	{
	//	  Bitmap oneImage = new Bitmap(16, 16);
	//	  using (Graphics g = Graphics.FromImage(oneImage))
	//	  {
	//		g.DrawImage(images, 0, 0, new Rectangle(x, y, 16, 16), GraphicsUnit.Pixel);
	//	  }
	//	  FImages.Add(oneImage);

	//	  x += 16;
	//	  if (x >= images.Width)
	//	  {
	//		x = 0;
	//		y += 16;
	//	  }
	//	  done = y > images.Height;
	//	}
	//	while (!done);
	//  }
	//}
    
    private static void CreateImageList()
    {
      FImageList = new ImageList();
      FImageList.ImageSize = new Size(16, 16);
      FImageList.ColorDepth = ColorDepth.Depth32Bit;
      
      foreach (Bitmap bmp in FImages)
      {
        FImageList.Images.Add(bmp);
      }
    }
    
    /// <summary>
    /// Loads the locale from a file.
    /// </summary>
    /// <param name="fileName">The name of the file that contains localized strings.</param>
    public static void LoadLocale(string fileName)
    {
      Report.EnsureInit();
      if (!FLocaleLoaded)
        LoadBuiltinLocale();

      if (File.Exists(fileName))
      {
        FLocale = new XmlDocument();
        FLocale.Load(fileName);
      }
      else
        FLocale = FBuiltinLocale;
    }

    /// <summary>
    /// Loads the locale from a stream.
    /// </summary>
    /// <param name="stream">The stream that contains localized strings.</param>
    public static void LoadLocale(Stream stream)
    {
      Report.EnsureInit();
      if (!FLocaleLoaded)
        LoadBuiltinLocale();

      FLocale = new XmlDocument();
      FLocale.Load(stream);
    }

    /// <summary>
    /// Loads the english locale.
    /// </summary>
    public static void LoadEnglishLocale()
    {
      LoadBuiltinLocale();
    }

	// Simon: 注释此方法
	//internal static void LoadDefaultLocale()
	//{
	//  if (!Directory.Exists(LocaleFolder))
	//	return;
        
	//  if (String.IsNullOrEmpty(DefaultLocaleName))
	//  {
	//	// locale is set to "Auto"
	//	CultureInfo currentCulture = CultureInfo.CurrentCulture;
	//	string[] files = Directory.GetFiles(LocaleFolder, "*.frl");
        
	//	foreach (string file in files)
	//	{
	//	  // find the CultureInfo for given file
	//	  string localeName = Path.GetFileNameWithoutExtension(file);
	//	  CultureInfo localeCulture = null;
	//	  foreach (CultureInfo info in CultureInfo.GetCultures(CultureTypes.NeutralCultures))
	//	  {
	//		if (String.Compare(info.EnglishName, localeName, true) == 0)
	//		{
	//		  localeCulture = info;
	//		  break;
	//		}
	//	  }

	//	  // if current culture equals to or is subculture of localeCulture, load the locale
	//	  if (currentCulture.Equals(localeCulture) || 
	//		(currentCulture.Parent != null && currentCulture.Parent.Equals(localeCulture)))
	//	  {
	//		LoadLocale(file);
	//		break;
	//	  }
	//	}
	//  }
	//  else
	//  {
	//	// locale is set to specific name
	//	LoadLocale(LocaleFolder + DefaultLocaleName + ".frl");
	//  }
	//}

    /// <summary>
    /// Gets a string with specified ID.
    /// </summary>
    /// <param name="id">The resource ID.</param>
    /// <returns>The localized string.</returns>
    /// <remarks>
    /// Since the locale file is xml-based, it may contain several xml node levels. For example, 
    /// the file contains the following items:
    /// <code>
    /// &lt;Objects&gt;
    ///   &lt;Report Text="Report"/&gt;
    ///   &lt;Bands Text="Bands"&gt;
    ///     &lt;ReportTitle Text="Report Title"/&gt;
    ///   &lt;/Bands&gt;
    /// &lt;/Objects&gt;
    /// </code>
    /// To get the localized "ReportTitle" value, you should pass the following ID
    /// to this method: "Objects,Bands,ReportTitle".
    /// </remarks>
    public static string Get(string id)
    {
      if (!FLocaleLoaded)
        LoadBuiltinLocale();

      string result = Get(id, FLocale);
      // if no item found, try built-in (english) locale
      if (result.IndexOf(FBadResult) != -1 && FLocale != FBuiltinLocale)
        result = Get(id, FBuiltinLocale);
      return result;
    }

    private static string Get(string id, XmlDocument locale)
    {
      string[] categories = id.Split(new char[] { ',' });
      XmlItem xi = locale.Root;
      foreach (string category in categories)
      {
        int i = xi.Find(category);
        if (i == -1)
          return id + " " + FBadResult;
        xi = xi[i];
      }

      string result = xi.GetProp("Text");
      if (result == "")
        result = id + " " + FBadResult;
      return result;
    }

    /// <summary>
    /// Replaces the specified locale string with the new value.
    /// </summary>
    /// <param name="id">Comma-separated path to the existing locale string.</param>
    /// <param name="value">The new string.</param>
    /// <remarks>
    /// Use this method if you want to replace some existing locale value with the new one.
    /// </remarks>
    /// <example>
    /// <code>
    /// Res.Set("Messages,SaveChanges", "My text that will appear when you close the designer");
    /// </code>
    /// </example>
    public static void Set(string id, string value)
    {
      if (!FLocaleLoaded)
        LoadBuiltinLocale();

      string[] categories = id.Split(new char[] { ',' });
      XmlItem xi = FLocale.Root;
      foreach (string category in categories)
      {
        xi = xi.FindItem(category);
      }

      xi.SetProp("Text", value);
    }

    /// <summary>
    /// Tries to get a string with specified ID.
    /// </summary>
    /// <param name="id">The resource ID.</param>
    /// <returns>The localized value, if specified ID exists; otherwise, the ID itself.</returns>
    public static string TryGet(string id)
    {
      string result = Get(id);
      if (result.IndexOf(FBadResult) != -1)
        result = id;
      return result;  
    }
    
    /// <summary>
    /// Checks if specified ID exists.
    /// </summary>
    /// <param name="id">The resource ID.</param>
    /// <returns><b>true</b> if specified ID exists.</returns>
    public static bool StringExists(string id)
    {
      return Get(id).IndexOf(FBadResult) == -1;
    }
    
    /// <summary>
    /// Gets the standard images used in FastReport as an <b>ImageList</b>.
    /// </summary>
    /// <returns><b>ImageList</b> object that contains standard images.</returns>
    /// <remarks>
    /// FastReport contains about 240 truecolor images of 16x16 size that are stored in one 
    /// big image side-by-side. This image can be found in FastReport resources (the "buttons.png" resource).
    /// </remarks>
    public static ImageList GetImages()
    {
      if (!FImagesLoaded)
        LoadImages();
      if (FImageList == null)
        CreateImageList();
      return FImageList;  
    }

    /// <summary>
    /// Gets an image with specified index.
    /// </summary>
    /// <param name="index">Image index (zero-based).</param>
    /// <returns>The image with specified index.</returns>
    /// <remarks>
    /// FastReport contains about 240 truecolor images of 16x16 size that are stored in one 
    /// big image side-by-side. This image can be found in FastReport resources (the "buttons.png" resource).
    /// </remarks>
    public static Bitmap GetImage(int index)
    {
      if (!FImagesLoaded)
        LoadImages();
      return FImages[index];
    }
    
    /// <summary>
    /// Gets an image with specified index and converts it to <b>Icon</b>.
    /// </summary>
    /// <param name="index">Image index (zero-based).</param>
    /// <returns>The <b>Icon</b> object.</returns>
    public static Icon GetIcon(int index)
    {
      return Icon.FromHandle(GetImage(index).GetHicon());
    }

    static Res()
    {
        // for using FastReport.dll without FastReport.Bars.dll if the designer is not shown
        foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (a.ManifestModule.Name == "FastReport.Bars.dll")
            {
                RegisterLocalizeStringEventHandler();
                break;
            }
        }
    }

    private static void RegisterLocalizeStringEventHandler()
    {
        LocalizationKeys.LocalizeString += new DotNetBarManager.LocalizeStringEventHandler(LocalizationKeys_LocalizeString);
    }

    private static void LocalizationKeys_LocalizeString(object sender, LocalizeEventArgs e)
    {
      switch (e.Key)
      {
        case "barsys_autohide_tooltip":
          e.LocalizedValue = Res.Get("Designer,ToolWindow,AutoHide");
          e.Handled = true;
          break;
        case "barsys_close_tooltip":
          e.LocalizedValue = Res.Get("Designer,ToolWindow,Close");
          e.Handled = true;
          break;
        case "cust_mnu_addremove":
          e.LocalizedValue = Res.Get("Designer,Toolbar,AddOrRemove");
          e.Handled = true;
          break;
        case "sys_morebuttons":
          e.LocalizedValue = Res.Get("Designer,Toolbar,MoreButtons");
          e.Handled = true;
          break;
      }
    }
  }
  
  /// <summary>
  /// Used to access to resource IDs inside the specified branch.
  /// </summary>
  /// <remarks>
  /// Using the <see cref="Res.Get(string)"/> method, you have to specify the full path to your resource.
  /// Using this class, you can shorten the path:
  /// <code>
  /// // using the Res.Get method
  /// miKeepTogether = new ToolStripMenuItem(Res.Get("ComponentMenu,HeaderBand,KeepTogether"));
  /// miResetPageNumber = new ToolStripMenuItem(Res.Get("ComponentMenu,HeaderBand,ResetPageNumber"));
  /// miRepeatOnEveryPage = new ToolStripMenuItem(Res.Get("ComponentMenu,HeaderBand,RepeatOnEveryPage"));
  /// 
  /// // using MyRes.Get method
  /// MyRes res = new MyRes("ComponentMenu,HeaderBand");
  /// miKeepTogether = new ToolStripMenuItem(res.Get("KeepTogether"));
  /// miResetPageNumber = new ToolStripMenuItem(res.Get("ResetPageNumber"));
  /// miRepeatOnEveryPage = new ToolStripMenuItem(res.Get("RepeatOnEveryPage"));
  /// 
  /// </code>
  /// </remarks>
  public class MyRes
  {
    private string FCategory;

    /// <summary>
    /// Gets a string with specified ID inside the main branch.
    /// </summary>
    /// <param name="id">The resource ID.</param>
    /// <returns>The localized value.</returns>
    public string Get(string id)
    {
      if (id != "")
        return Res.Get(FCategory + "," + id);
      else
        return Res.Get(FCategory);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MyRes"/> class with spevified branch.
    /// </summary>
    /// <param name="category">The main resource branch.</param>
    public MyRes(string category)
    {
      FCategory = category;
    }
  }

  
  /// <summary>
  /// Localized CategoryAttribute class.
  /// </summary>
  public class SRCategory : CategoryAttribute
  {
    /// <inheritdoc/>
    protected override string GetLocalizedString(string value)
    {
      return Res.TryGet("Properties,Categories," + value);
    }

    /// <summary>
    /// Initializes a new instance of the SRCategory class.
    /// </summary>
    /// <param name="value">The category name.</param>
    public SRCategory(string value)
      : base(value)
    {
    }
  }
}
