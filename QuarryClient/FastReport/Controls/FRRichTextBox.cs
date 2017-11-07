using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.ComponentModel;

namespace FastReport.Controls
{
  /// <summary>
  /// Specifies how text in a <see cref="FRRichTextBox"/> is horizontally aligned.
  /// </summary>
  internal enum TextAlign
  {
    /// <summary>
    /// The text is aligned to the left.
    /// </summary>
    Left = 1,

    /// <summary>
    /// The text is aligned to the right.
    /// </summary>
    Right = 2,

    /// <summary>
    /// The text is aligned in the center.
    /// </summary>
    Center = 3,

    /// <summary>
    /// The text is justified.
    /// </summary>
    Justify = 4
  }

  internal class FRRichTextBox : RichTextBox
  {
    #region Fields, Structures
    // Constants from the Platform SDK.
    private const int EM_GETPARAFORMAT = 1085;
    private const int EM_SETPARAFORMAT = 1095;
    private const int EM_SETCHARFORMAT = 1092;
    private const int EM_SETTYPOGRAPHYOPTIONS = 1226;
    private const int EM_FORMATRANGE = 1081;
    private const int EM_SETTARGETDEVICE = 1096;
    private const int TO_ADVANCEDTYPOGRAPHY = 1;
    private const int PFM_ALIGNMENT = 8;
    private const int SCF_SELECTION = 1;
    private const int CFM_BOLD = 1;
    private const int CFM_ITALIC = 2;
    private const int CFM_UNDERLINE = 4;
    private const uint CFM_SIZE = 0x80000000;
    private const uint CFM_FACE = 0x20000000;
    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_TRANSPARENT = 0x20;


    [StructLayout(LayoutKind.Sequential)]
    private struct PARAFORMAT
    {
      public int cbSize;
      public uint dwMask;
      public short wNumbering;
      public short wReserved;
      public int dxStartIndent;
      public int dxRightIndent;
      public int dxOffset;
      public short wAlignment;
      public short cTabCount;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
      public int[] rgxTabs;

      // PARAFORMAT2 from here onwards.
      public int dySpaceBefore;
      public int dySpaceAfter;
      public int dyLineSpacing;
      public short sStyle;
      public byte bLineSpacingRule;
      public byte bOutlineLevel;
      public short wShadingWeight;
      public short wShadingStyle;
      public short wNumberingStart;
      public short wNumberingStyle;
      public short wNumberingTab;
      public short wBorderSpace;
      public short wBorderWidth;
      public short wBorders;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct CHARFORMAT
    {
      public int cbSize;
      public uint dwMask;
      public uint dwEffects;
      public int yHeight;
      public int yOffset;
      public int crTextColor;
      public byte bCharSet;
      public byte bPitchAndFamily;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
      public char[] szFaceName;

      // CHARFORMAT2 from here onwards.

      public short wWeight;
      public short sSpacing;
      public int crBackColor;
      public int LCID;
      public uint dwReserved;
      public short sStyle;
      public short wKerning;
      public byte bUnderlineType;
      public byte bAnimation;
      public byte bRevAuthor;
      public byte bReserved1;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    private struct STRUCT_CHARRANGE
    {
      public int cpMin;
      public int cpMax;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct STRUCT_FORMATRANGE
    {
      public IntPtr hdc;
      public IntPtr hdcTarget;
      public STRUCT_RECT rc;
      public STRUCT_RECT rcPage;
      public STRUCT_CHARRANGE chrg;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct STRUCT_RECT
    {
      public int left;
      public int top;
      public int right;
      public int bottom;
    }

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref PARAFORMAT fmt);

    [DllImport("user32", CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref CHARFORMAT fmt);

    [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
     static extern IntPtr LoadLibrary(string lpFileName);

    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the alignment to apply to the current
    /// selection or insertion point.
    /// </summary>
    /// <remarks>
    /// Replaces the SelectionAlignment from <see cref="RichTextBox"/>.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new TextAlign SelectionAlignment
    {
      get
      {
        PARAFORMAT fmt = new PARAFORMAT();
        fmt.cbSize = Marshal.SizeOf(fmt);

        // Get the alignment.
        SendMessage(Handle, EM_GETPARAFORMAT, SCF_SELECTION, ref fmt);

        // Default to Left align.
        if ((fmt.dwMask & PFM_ALIGNMENT) == 0)
          return TextAlign.Left;

        return (TextAlign)fmt.wAlignment;
      }
      set
      {
        PARAFORMAT fmt = new PARAFORMAT();
        fmt.cbSize = Marshal.SizeOf(fmt);
        fmt.dwMask = PFM_ALIGNMENT;
        fmt.wAlignment = (short)value;

        // Set the alignment.
        SendMessage(Handle, EM_SETPARAFORMAT, SCF_SELECTION, ref fmt);
      }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Convert between screen pixels and twips (1/1440 inch, used by Win32 API calls)
    /// </summary>
    /// <param name="n">Value in screen pixels</param>
    /// <returns>Value in twips</returns>
    private int PixelsToTwips(float n)
    {
      return (int)(n / 96 * 1440);
    }

    /// <summary>
    /// Convert between screen pixels and twips (1/1440 inch, used by Win32 API calls)
    /// </summary>
    /// <param name="twips">Value in twips</param>
    /// <returns>Value in screen pixels</returns>
    private float TwipsToPixels(int twips)
    {
      return twips / 1440f * 96f;
    }

    private void SetCharFormatMessage(ref CHARFORMAT fmt)
    {
      SendMessage(Handle, EM_SETCHARFORMAT, SCF_SELECTION, ref fmt);
    }

    private void ApplyStyle(uint style, bool on)
    {
      CHARFORMAT fmt = new CHARFORMAT();
      fmt.cbSize = Marshal.SizeOf(fmt);
      fmt.dwMask = style;

      if (on)
        fmt.dwEffects = style;
      SetCharFormatMessage(ref fmt);
    }
    #endregion

    #region Protected Methods
    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams prams = base.CreateParams;
        if (LoadLibrary("msftedit.dll") != IntPtr.Zero)
        {
          //prams.ExStyle |= 0x020; // transparent
          prams.ClassName = "RICHEDIT50W";
        }
        return prams;
      }
    }
    
    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);

      // Enable support for justification.
      SendMessage(Handle, EM_SETTYPOGRAPHYOPTIONS, TO_ADVANCEDTYPOGRAPHY, TO_ADVANCEDTYPOGRAPHY);
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Calculate or render the contents of RichTextBox for printing
    /// </summary>
    /// <param name="g">Graphics object</param>
    /// <param name="measureGraphics">Graphics object to measure richtext for</param>
    /// <param name="displayRect">Bonding rectangle of the RichTextBox</param>
    /// <param name="charFrom">Index of first character to be printed</param>
    /// <param name="charTo">Index of last character to be printed</param>
    /// <param name="measureOnly">If true, only the calculation is performed,
    /// otherwise the text is rendered as well</param>
    /// <returns>(Index of last character that fitted on the page) + 1</returns>
    public int FormatRange(Graphics g, Graphics measureGraphics, RectangleF displayRect,
      int charFrom, int charTo, bool measureOnly)
    {
      int i;
      return FormatRange(g, measureGraphics, displayRect, charFrom, charTo, measureOnly, out i);
    }

    /// <summary>
    /// Calculate or render the contents of RichTextBox for printing
    /// </summary>
    /// <param name="g">Graphics object</param>
    /// <param name="measureGraphics">Graphics object to measure richtext for</param>
    /// <param name="displayRect">Bonding rectangle of the RichTextBox</param>
    /// <param name="charFrom">Index of first character to be printed</param>
    /// <param name="charTo">Index of last character to be printed</param>
    /// <param name="measureOnly">If true, only the calculation is performed,
    /// otherwise the text is rendered as well</param>
    /// <param name="height">The calculated text height</param>
    /// <returns>(Index of last character that fitted on the page) + 1</returns>
    public int FormatRange(Graphics g, Graphics measureGraphics, RectangleF displayRect,
      int charFrom, int charTo, bool measureOnly, out int height)
    {
      // Specify which characters to print
      STRUCT_CHARRANGE cr;
      cr.cpMin = charFrom;
      cr.cpMax = charTo;

      // Specify the area inside page margins
      STRUCT_RECT rc;
      rc.left = PixelsToTwips(displayRect.Left);
      rc.top = PixelsToTwips(displayRect.Top);
      rc.right = PixelsToTwips(displayRect.Right);
      rc.bottom = PixelsToTwips(displayRect.Bottom);

      // Specify the page area
      STRUCT_RECT rcPage;
      rcPage.left = rc.left;
      rcPage.top = rc.top;
      rcPage.right = rc.right;
      rcPage.bottom = rc.bottom;

      // Get device context of output device
      IntPtr hdc = g.GetHdc();
      IntPtr measureHdc = g == measureGraphics ? hdc : measureGraphics.GetHdc();

      // Fill in the FORMATRANGE struct
      STRUCT_FORMATRANGE fr;
      fr.chrg = cr;
      fr.hdc = hdc;
      fr.hdcTarget = measureHdc;
      fr.rc = rc;
      fr.rcPage = rcPage;

      // Non-Zero wParam means render, Zero means measure
      int wParam = measureOnly ? 0 : 1;

      // Allocate memory for the FORMATRANGE struct and
      // copy the contents of our struct to this memory
      IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr));
      Marshal.StructureToPtr(fr, lParam, false);

      // Send the actual Win32 message
      int res = SendMessage(Handle, EM_FORMATRANGE, wParam, lParam);
      height = 0;
      if (measureOnly)
      {
        fr = (STRUCT_FORMATRANGE)Marshal.PtrToStructure(lParam, typeof(STRUCT_FORMATRANGE));
        height = (int)Math.Round(fr.rc.bottom / (1440f / 96f));
      }

      // Free allocated memory
      Marshal.FreeCoTaskMem(lParam);

      // and release the device context
      g.ReleaseHdc(hdc);
      if (g != measureGraphics)
        measureGraphics.ReleaseHdc(measureHdc);

      // finish the formatting
      SendMessage(Handle, EM_FORMATRANGE, 0, 0);
      
      return res;
    }
    
    public void SetSelectionFont(string face)
    {
      CHARFORMAT fmt = new CHARFORMAT();
      fmt.cbSize = Marshal.SizeOf(fmt);
      fmt.dwMask = CFM_FACE;
      fmt.szFaceName = new char[32];
      face.CopyTo(0, fmt.szFaceName, 0, Math.Min(face.Length, 31));

      SetCharFormatMessage(ref fmt);
    }

    public void SetSelectionSize(int size)
    {
      CHARFORMAT fmt = new CHARFORMAT();
      fmt.cbSize = Marshal.SizeOf(fmt);
      fmt.dwMask = CFM_SIZE;
      fmt.yHeight = size * 20;

      SetCharFormatMessage(ref fmt);
    }

    public void SetSelectionBold(bool value)
    {
      ApplyStyle(CFM_BOLD, value);
    }

    public void SetSelectionItalic(bool value)
    {
      ApplyStyle(CFM_ITALIC, value);
    }

    public void SetSelectionUnderline(bool value)
    {
      ApplyStyle(CFM_UNDERLINE, value);
    }
    #endregion
  }
}
