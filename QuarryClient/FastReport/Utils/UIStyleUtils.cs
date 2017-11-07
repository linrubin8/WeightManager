using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.DevComponents.DotNetBar;
using FastReport.DevComponents.DotNetBar.Rendering;
using FastReport.DevComponents.AdvTree;

namespace FastReport.Utils
{
    /// <summary>
    /// The style of FastReport user interface.
    /// </summary>
    public enum UIStyle
    {
    /// <summary>
    /// Specifies the Microsoft Office 2003 style (blue).
    /// </summary>
    Office2003,

    /// <summary>
    /// Specifies the Microsoft Office 2007 style (blue).
    /// </summary>
    Office2007Blue,

    /// <summary>
    /// Specifies the Microsoft Office 2007 style (silver).
    /// </summary>
    Office2007Silver,

    /// <summary>
    /// Specifies the Microsoft Office 2007 style (black).
    /// </summary>
    Office2007Black,

    /// <summary>
    /// Specifies the Office 2010 (Blue) style.
    /// </summary>
    Office2010Blue,

    /// <summary>
    /// Specifies the Office 2010 (Silver) style.
    /// </summary>
    Office2010Silver,

    /// <summary>
    /// Specifies the Office 2010 (Black) style.
    /// </summary>
    Office2010Black,

    /// <summary>
    /// Specifies the Office 2013 style.
    /// </summary>
    Office2013,

    /// <summary>
    /// Specifies the Microsoft Visual Studio 2005 style.
    /// </summary>
    VisualStudio2005,

    /// <summary>
    /// Specifies the Visual Studio 2010 style.
    /// </summary>
    VisualStudio2010,

    /// <summary>
    /// Specifies the Visual Studio 2012 (Light) style.
    /// </summary>
    VisualStudio2012Light,

    /// <summary>
    /// Specifies the Microsoft Vista style (black).
    /// </summary>
    VistaGlass
  }

  /// <summary>
  /// Contains conversion methods between FastReport's UIStyle to various enums.
  /// </summary>
  public static class UIStyleUtils
  {
      /// <summary>
      /// Contains visual style names.
      /// </summary>
      public static readonly string[] UIStyleNames = new string[] {
        "Office 2003",
        "Office 2007 Blue",
        "Office 2007 Silver",
        "Office 2007 Black",
        "Office 2010 Blue",
        "Office 2010 Silver",
        "Office 2010 Black",
        "Office 2013",
        "VisualStudio 2005",  
        "VisualStudio 2010",
        "VisualStudio 2012",
        "VistaGlass"
      };

      internal static bool IsOffice2007Scheme(UIStyle style)
      {
        return style == UIStyle.Office2007Blue ||
          style == UIStyle.Office2007Silver ||
          style == UIStyle.Office2007Black ||
          style == UIStyle.Office2010Blue ||
          style == UIStyle.Office2010Silver ||
          style == UIStyle.Office2010Black ||
          style == UIStyle.VistaGlass;
      }    

    /// <summary>
    /// Converts FastReport's UIStyle to eDotNetBarStyle.
    /// </summary>
    /// <param name="style">Style to convert.</param>
    /// <returns>Value of eDotNetBarStyle type.</returns>
    public static eDotNetBarStyle GetDotNetBarStyle(UIStyle style)
    {
      switch (style)
      {
        case UIStyle.VisualStudio2005:
          return eDotNetBarStyle.VS2005;

        case UIStyle.Office2003:
          return eDotNetBarStyle.Office2003;

        case UIStyle.Office2007Blue:
          ((Office2007Renderer)GlobalManager.Renderer).ColorTable = new Office2007ColorTable(eOffice2007ColorScheme.Blue);
          return eDotNetBarStyle.Office2007;

        case UIStyle.Office2007Silver:
          ((Office2007Renderer)GlobalManager.Renderer).ColorTable = new Office2007ColorTable(eOffice2007ColorScheme.Silver);
          return eDotNetBarStyle.Office2007;

        case UIStyle.Office2007Black:
          ((Office2007Renderer)GlobalManager.Renderer).ColorTable = new Office2007ColorTable(eOffice2007ColorScheme.Black);
          return eDotNetBarStyle.Office2007;

        case UIStyle.Office2010Blue:
          ((Office2007Renderer)GlobalManager.Renderer).ColorTable = new Office2010ColorTable(eOffice2010ColorScheme.Blue);
          return eDotNetBarStyle.Office2010;

        case UIStyle.Office2010Silver:
          ((Office2007Renderer)GlobalManager.Renderer).ColorTable = new Office2010ColorTable(eOffice2010ColorScheme.Silver);
          return eDotNetBarStyle.Office2010;

        case UIStyle.Office2010Black:
          ((Office2007Renderer)GlobalManager.Renderer).ColorTable = new Office2010ColorTable(eOffice2010ColorScheme.Black);
          return eDotNetBarStyle.Office2010;

        case UIStyle.VistaGlass:
          ((Office2007Renderer)GlobalManager.Renderer).ColorTable = new Office2007ColorTable(eOffice2007ColorScheme.VistaGlass);
          return eDotNetBarStyle.Office2007;
      }
      
      return eDotNetBarStyle.StyleManagerControlled;
    }

    /// <summary>
    /// Converts FastReport's UIStyle to eTabStripStyle.
    /// </summary>
    /// <param name="style">Style to convert.</param>
    /// <returns>Value of eTabStripStyle type.</returns>
    public static eTabStripStyle GetTabStripStyle(UIStyle style)
    {
        switch (style)
        {
          case UIStyle.VisualStudio2005:
            return eTabStripStyle.VS2005Document;

          case UIStyle.Office2003:
            return eTabStripStyle.Office2003;

          case UIStyle.Office2007Blue:
          case UIStyle.Office2007Silver:
          case UIStyle.Office2007Black:
          case UIStyle.VistaGlass:
            return eTabStripStyle.Office2007Document;

          case UIStyle.Office2010Black:
          case UIStyle.Office2010Blue:
          case UIStyle.Office2010Silver:
          case UIStyle.VisualStudio2010:
            return eTabStripStyle.Office2007Dock;
        }

        return eTabStripStyle.Metro;
    }

    /// <summary>
    /// Converts FastReport's UIStyle to eTabStripStyle.
    /// </summary>
    /// <param name="style">Style to convert.</param>
    /// <returns>Value of eTabStripStyle type.</returns>
    public static eTabStripStyle GetTabStripStyle1(UIStyle style)
    {
        switch (style)
        {
          case UIStyle.VisualStudio2005:
            return eTabStripStyle.VS2005;

          case UIStyle.Office2003:
            return eTabStripStyle.Office2003;

          case UIStyle.Office2007Blue:
          case UIStyle.Office2007Silver:
          case UIStyle.Office2007Black:
          case UIStyle.VistaGlass:
            return eTabStripStyle.Office2007Dock;
          
          case UIStyle.Office2010Black:
          case UIStyle.Office2010Blue:
          case UIStyle.Office2010Silver:
          case UIStyle.VisualStudio2010:
            return eTabStripStyle.Office2007Dock;
        }

        return eTabStripStyle.Metro;
    }

    /// <summary>
    /// Converts FastReport's UIStyle to eOffice2007ColorScheme.
    /// </summary>
    /// <param name="style">Style to convert.</param>
    /// <returns>Value of eOffice2007ColorScheme type.</returns>
    public static eOffice2007ColorScheme GetOffice2007ColorScheme(UIStyle style)
    {
      switch (style)
      {
        case UIStyle.Office2007Blue:
        case UIStyle.Office2010Blue:
          return eOffice2007ColorScheme.Blue;

        case UIStyle.Office2007Silver:
        case UIStyle.Office2010Silver:
          return eOffice2007ColorScheme.Silver;

        case UIStyle.Office2007Black:
        case UIStyle.Office2010Black:
          return eOffice2007ColorScheme.Black;

        case UIStyle.VistaGlass:
          return eOffice2007ColorScheme.VistaGlass;
      }

      return eOffice2007ColorScheme.Blue;
    }

    /// <summary>
    /// Converts FastReport's UIStyle to eColorSchemeStyle.
    /// </summary>
    /// <param name="style">Style to convert.</param>
    /// <returns>Value of eColorSchemeStyle type.</returns>
    public static eColorSchemeStyle GetColorSchemeStyle(UIStyle style)
    {
      switch (style)
      {
        case UIStyle.Office2003:
          return eColorSchemeStyle.Office2003;

        case UIStyle.VisualStudio2005:
          return eColorSchemeStyle.VS2005;
      }

      return eColorSchemeStyle.Office2007;
    }

    /// <summary>
    /// Returns app workspace color for the given style.
    /// </summary>
    /// <param name="style">UI style.</param>
    /// <returns>The color.</returns>
    public static Color GetAppWorkspaceColor(UIStyle style)
    {
      switch (style)
      {
        case UIStyle.Office2003:
          return Color.FromArgb(144, 153, 174);

        case UIStyle.Office2007Blue:
          return Color.FromArgb(101, 145, 205);

        case UIStyle.Office2007Silver:
          return Color.FromArgb(156, 160, 167);

        case UIStyle.Office2007Black:
          return Color.FromArgb(90, 90, 90);

        case UIStyle.Office2010Blue:
          return Color.FromArgb(101, 145, 205);

        case UIStyle.Office2010Silver:
          return Color.FromArgb(156, 160, 167);

        case UIStyle.Office2010Black:
          return Color.FromArgb(90, 90, 90);

        case UIStyle.VistaGlass:
          return Color.FromArgb(90, 90, 90);
      }
      
      return Color.FromArgb(239, 239, 242);
    }

    /// <summary>
    /// Returns control color for the given style.
    /// </summary>
    /// <param name="style">UI style.</param>
    /// <returns>The color.</returns>
    public static Color GetControlColor(UIStyle style)
    {
      switch (style)
      {
        case UIStyle.Office2003:
          return Color.FromArgb(182, 208, 248);

        case UIStyle.Office2007Blue:
          return Color.FromArgb(180, 212, 255);

        case UIStyle.Office2007Silver:
          return Color.FromArgb(215, 214, 228);

        case UIStyle.Office2007Black:
          return Color.FromArgb(204, 207, 212);

        case UIStyle.Office2010Blue:
          return Color.FromArgb(215, 228, 242);
        
        case UIStyle.Office2010Silver:
          return Color.FromArgb(229, 233, 237);

        case UIStyle.Office2010Black:
          return Color.FromArgb(200, 200, 200);

        case UIStyle.VisualStudio2010:
          return Color.FromArgb(188, 199, 216);

        case UIStyle.VisualStudio2012Light:
          return Color.FromArgb(230, 231, 232);

        case UIStyle.VistaGlass:
          return Color.FromArgb(218, 224, 239);
      }
      
      return GetAppWorkspaceColor(style);
    }
  }
}
