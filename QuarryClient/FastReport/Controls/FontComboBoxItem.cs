using System;
using System.Collections.Generic;
using System.Text;
using FastReport.DevComponents.DotNetBar;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.Controls
{
  internal class FontComboBoxItem : ComboBoxItem
  {
    private List<string> FMruFonts;
    private List<string> FExistingFonts;
    private FontStyle[] FStyles;
    private string FFontName;

    public event EventHandler FontSelected;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string FontName
    {
      get { return (string)SelectedItem; }
      set
      {
        FFontName = value;
        int i = Items.IndexOf(value);
        if (i != -1)
          SelectedIndex = i;
        else
          SelectedItem = null;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string MruFonts
    {
      get
      {
        string result = "";
        foreach (string s in FMruFonts)
          result += "," + s;
        if (result.StartsWith(","))
          result = result.Substring(1);
        return result;
      }
      set
      {
        FMruFonts.Clear();
        if (!String.IsNullOrEmpty(value))
        {
          string[] fonts = value.Split(new char[] { ',' });
          foreach (string s in fonts)
            FMruFonts.Add(s);
        }
        UpdateFonts();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new System.Windows.Forms.ComboBox.ObjectCollection Items
    {
      get { return base.Items; }
    }

    private FontStyle GetFirstAvailableFontStyle(FontFamily family)
    {
      foreach (FontStyle style in FStyles)
      {
        if (family.IsStyleAvailable(style))
          return style;
      }
      return FontStyle.Regular;
    }

    private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
    {
      if ((e.State & DrawItemState.Disabled) > 0)
        return;

      Graphics g = e.Graphics;
      e.DrawBackground();

      if ((e.State & DrawItemState.ComboBoxEdit) > 0)
      {
        TextRenderer.DrawText(g, Text, e.Font, e.Bounds.Location, e.ForeColor);
      }
      else if (e.Index >= 0)
      {
        string name = (string)Items[e.Index];
        if (!FExistingFonts.Contains(name))
          return;

        using (FontFamily family = new FontFamily(name))
        using (Font font = new Font(name, 14, GetFirstAvailableFontStyle(family)))
        {
          g.DrawImage(Res.GetImage(59), e.Bounds.X + 2, e.Bounds.Y + 2);

          LOGFONT lf = new LOGFONT();
          font.ToLogFont(lf);
          SizeF sz;

          if (lf.lfCharSet == 2)
          {
            sz = g.MeasureString(name, e.Font);
            int w = (int)sz.Width;
            TextRenderer.DrawText(g, name, e.Font, new Point(e.Bounds.X + 20, e.Bounds.Y + (e.Bounds.Height - (int)sz.Height) / 2), e.ForeColor);
            sz = g.MeasureString(name, font);
            TextRenderer.DrawText(g, name, font, new Point(e.Bounds.X + w + 28, e.Bounds.Y + (e.Bounds.Height - (int)sz.Height) / 2), e.ForeColor);
          }
          else
          {
            sz = g.MeasureString(name, font);
            TextRenderer.DrawText(g, name, font, new Point(e.Bounds.X + 20, e.Bounds.Y + (e.Bounds.Height - (int)sz.Height) / 2), e.ForeColor);
          }

          if (e.Index == FMruFonts.Count - 1)
          {
            g.DrawLine(Pens.Gray, e.Bounds.Left, e.Bounds.Bottom - 3, e.Bounds.Right, e.Bounds.Bottom - 3);
            g.DrawLine(Pens.Gray, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
          }
        }
      }
    }

    private void ComboBox_MeasureItem(object sender, MeasureItemEventArgs e)
    {
      e.ItemHeight = 20;
    }

    private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
    {
      OnFontSelected();

      string fontName = FontName;
      if (FMruFonts.Contains(fontName))
        FMruFonts.Remove(fontName);
      FMruFonts.Insert(0, fontName);
      while (FMruFonts.Count > 5)
        FMruFonts.RemoveAt(5);
      UpdateFonts();
      Text = fontName;
    }

    private void ComboBoxEx_EnabledChanged(object sender, EventArgs e)
    {
      if (ComboBoxEx.Enabled)
        ComboBoxEx.DropDownStyle = ComboBoxStyle.DropDown;
      else
        ComboBoxEx.DropDownStyle = ComboBoxStyle.DropDownList;
    }

    private void OnFontSelected()
    {
      if (FontSelected != null)
        FontSelected(this, EventArgs.Empty);
    }

    private void UpdateFonts()
    {
      Items.Clear();
      foreach (string s in FMruFonts)
      {
        if (FExistingFonts.Contains(s))
          Items.Add(s);
      }
      foreach (string s in FExistingFonts)
      {
        Items.Add(s);
      }
    }

    public FontComboBoxItem()
    {
      FMruFonts = new List<string>();
      FExistingFonts = new List<string>();
      FStyles = new FontStyle[] { FontStyle.Regular, FontStyle.Bold, FontStyle.Italic, 
        FontStyle.Strikeout, FontStyle.Underline };

      foreach (FontFamily family in FontFamily.Families)
      {
        FExistingFonts.Add(family.Name);
      }

      ComboBoxEx.DrawMode = DrawMode.OwnerDrawVariable;
      ComboBoxEx.DropDownStyle = ComboBoxStyle.DropDown;
      ItemHeight = 14;
      ComboBoxEx.DrawItem += new DrawItemEventHandler(ComboBox_DrawItem);
      ComboBoxEx.MeasureItem += new MeasureItemEventHandler(ComboBox_MeasureItem);
      ComboBoxEx.SelectionChangeCommitted += new EventHandler(ComboBox_SelectionChangeCommitted);
      ComboBoxEx.EnabledChanged += new EventHandler(ComboBoxEx_EnabledChanged);
      ComboWidth = 130;
      DropDownHeight = 302;
      DropDownWidth = 370;
      ComboBoxEx.DisableInternalDrawing = true;
      UpdateFonts();
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private class LOGFONT
    {
      public int lfHeight;
      public int lfWidth;
      public int lfEscapement;
      public int lfOrientation;
      public int lfWeight;
      public byte lfItalic;
      public byte lfUnderline;
      public byte lfStrikeOut;
      public byte lfCharSet;
      public byte lfOutPrecision;
      public byte lfClipPrecision;
      public byte lfQuality;
      public byte lfPitchAndFamily;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      public string lfFaceName;
    }
  }
}
