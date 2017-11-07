using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using System.Globalization;
using FastReport.Controls;

namespace FastReport.Map
{
  internal class ColorRangeEditorControl : Panel
  {
    private ColorRanges FColorRanges;

    public event EventHandler Changed;

    public ColorRanges ColorRanges
    {
      get { return FColorRanges; }
      set
      {
        FColorRanges = value;
        CreatePanels();
      }
    }

    private void OnChange()
    {
      if (Changed != null)
        Changed(this, EventArgs.Empty);
    }

    private void OnChange(object sender, EventArgs e)
    {
      OnChange();
    }
    
    private void ClearPanels()
    {
      for (int i = 0; i < Controls.Count; i++)
      {
        Controls[i].Dispose();
      }
      Controls.Clear();
    }
    
    private void CreatePanels()
    {
      ClearPanels();
      int top = 0;
      foreach (ColorRange range in ColorRanges.Ranges)
      {
        ColorRangePanel panel = new ColorRangePanel(range);
        panel.Changed += new EventHandler(OnChange);
        Controls.Add(panel);
        panel.Top = top;
        top += panel.Height;
      }
    }

    public ColorRangeEditorControl()
    {
      Size = new Size(284, 240);
    }
  }

  internal class ColorRangePanel : Panel
  {
    private TextBox tbStart;
    private TextBox tbEnd;
    private ColorComboBox cbxColor;
    private ColorRange FRange;

    public event EventHandler Changed;

    private void OnChange()
    {
      if (Changed != null)
        Changed(this, EventArgs.Empty);
    }

    private string ConvertDouble(double value)
    {
      return double.IsNaN(value) ? Res.Get("Forms,MapEditor,Common,Auto") : value.ToString(CultureInfo.InvariantCulture.NumberFormat);
    }

    private double ConvertDouble(string value)
    {
      try
      {
        return double.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
      }
      catch
      {
        return double.NaN;
      }
    }

    private void tbStart_Leave(object sender, EventArgs e)
    {
      FRange.StartValue = ConvertDouble(tbStart.Text);
      tbStart.Text = ConvertDouble(FRange.StartValue);
      OnChange();
    }

    private void tbEnd_Leave(object sender, EventArgs e)
    {
      FRange.EndValue = ConvertDouble(tbEnd.Text);
      tbEnd.Text = ConvertDouble(FRange.EndValue);
      OnChange();
    }

    private void cbxColor_ColorSelected(object sender, EventArgs e)
    {
      FRange.Color = cbxColor.Color;
      OnChange();
    }

    public ColorRangePanel(ColorRange range)
    {
      FRange = range;
      
      tbStart = new TextBox();
      tbStart.Parent = this;
      tbStart.Bounds = new Rectangle(0, 0, 60, 20);
      tbStart.Text = ConvertDouble(range.StartValue);
      tbStart.Leave += new EventHandler(tbStart_Leave);

      tbEnd = new TextBox();
      tbEnd.Parent = this;
      tbEnd.Bounds = new Rectangle(64, 0, 60, 20);
      tbEnd.Text = ConvertDouble(range.EndValue);
      tbEnd.Leave += new EventHandler(tbEnd_Leave);

      cbxColor = new ColorComboBox();
      cbxColor.ShowColorName = true;
      cbxColor.Parent = this;
      cbxColor.Bounds = new Rectangle(128, 0, 155, 21);
      cbxColor.Color = range.Color;
      cbxColor.ColorSelected += new EventHandler(cbxColor_ColorSelected);
      
      Size = new Size(284, 24);
    }
  }
}
