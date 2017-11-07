using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.ComponentModel;
using FastReport.Utils;

namespace FastReport.Controls
{
  internal class ToolStripFontComboBox : ToolStripComboBox
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
      get { return (string)ComboBox.SelectedItem; }
      set 
      { 
        FFontName = value;
        int i = ComboBox.Items.IndexOf(value);
        if (i != -1)
          ComboBox.SelectedIndex = i;
        else
          ComboBox.SelectedItem = null;
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
        e.DrawBackground();
        TextRenderer.DrawText(g, FFontName, e.Font, new Point(e.Bounds.X, e.Bounds.Y), e.ForeColor);
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

      if (FMruFonts.Contains(FontName))
        FMruFonts.Remove(FontName);
      FMruFonts.Insert(0, FontName);
      while (FMruFonts.Count > 5)
        FMruFonts.RemoveAt(5);
      UpdateFonts();
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

    public ToolStripFontComboBox()
    {
      FMruFonts = new List<string>();
      FExistingFonts = new List<string>();
      FStyles = new FontStyle[] { FontStyle.Regular, FontStyle.Bold, FontStyle.Italic, 
        FontStyle.Strikeout, FontStyle.Underline };

      foreach (FontFamily family in FontFamily.Families)
      {
        FExistingFonts.Add(family.Name);
      }

      AutoSize = false;
      ComboBox.DrawMode = DrawMode.OwnerDrawVariable;
      ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      ComboBox.ItemHeight = 13;
      ComboBox.DrawItem += new DrawItemEventHandler(ComboBox_DrawItem);
      ComboBox.MeasureItem += new MeasureItemEventHandler(ComboBox_MeasureItem);
      ComboBox.SelectionChangeCommitted += new EventHandler(ComboBox_SelectionChangeCommitted);
      Size = new Size(131, 25);
      DropDownHeight = 302;
      DropDownWidth = 270;
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
