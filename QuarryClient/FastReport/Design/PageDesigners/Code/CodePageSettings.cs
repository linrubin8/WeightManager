using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;

namespace FastReport.Design.PageDesigners.Code
{
  internal class CodePageSettings
  {
    #region Fields
    private static bool FEnableCodeCompletion;
    private static bool FEnableVirtualSpace;
    private static bool FUseSpaces;
    private static bool FAllowOutlining;
    private static int FTabSize;
    private static bool FLineNumbers;
    #endregion

    #region Properties
    public static bool EnableCodeCompletion
    {
        get { return FEnableCodeCompletion; }
        set { FEnableCodeCompletion = value; }
    }
    
    public static bool EnableVirtualSpace
    {
      get { return FEnableVirtualSpace; }
      set { FEnableVirtualSpace = value; }
    }
    
    public static bool UseSpaces
    {
      get { return FUseSpaces; }
      set { FUseSpaces = value; }
    }
    
    public static bool AllowOutlining
    {
      get { return FAllowOutlining; }
      set { FAllowOutlining = value; }
    }
    
    public static int TabSize
    {
      get { return FTabSize; }
      set { FTabSize = value; }
    }

    public static bool LineNumbers
    {
      get { return FLineNumbers; }
      set { FLineNumbers = value; }
    }
    #endregion

    #region Public Methods
    public static void SaveState()
    {
      XmlItem xi = Config.Root.FindItem("Designer").FindItem("Code");
      xi.SetProp("EnableCodeCompletion", EnableCodeCompletion ? "1" : "0");
      xi.SetProp("EnableVirtualSpace", EnableVirtualSpace ? "1" : "0");
      xi.SetProp("UseSpaces", UseSpaces ? "1" : "0");
      xi.SetProp("AllowOutlining", AllowOutlining ? "1" : "0");
      xi.SetProp("TabSize", TabSize.ToString());
      xi.SetProp("LineNumbers", LineNumbers ? "1" : "0");
    }

    static CodePageSettings()
    {
      XmlItem xi = Config.Root.FindItem("Designer").FindItem("Code");

      EnableCodeCompletion = xi.GetProp("EnableCodeCompletion") != "0";
      EnableVirtualSpace = xi.GetProp("EnableVirtualSpace") != "0";
      UseSpaces = xi.GetProp("UseSpaces") != "0";
      AllowOutlining = xi.GetProp("AllowOutlining") != "0";
      string tabSize = xi.GetProp("TabSize");
      if (String.IsNullOrEmpty(tabSize))
        tabSize = "2";
      TabSize = int.Parse(tabSize);
      LineNumbers = xi.GetProp("LineNumbers") != "0";
    }
    #endregion
  }

}
