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
  internal class SizeRangeEditorControl : Panel
  {
    private SizeRanges FSizeRanges;

    public event EventHandler Changed;

    public SizeRanges SizeRanges
    {
      get { return FSizeRanges; }
      set
      {
        FSizeRanges = value;
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
      foreach (SizeRange range in SizeRanges.Ranges)
      {
        SizeRangePanel panel = new SizeRangePanel(range);
        panel.Changed += new EventHandler(OnChange);
        Controls.Add(panel);
        panel.Top = top;
        top += panel.Height;
      }
    }

    public SizeRangeEditorControl()
    {
      Size = new Size(284, 240);
    }
  }

  internal class SizeRangePanel : Panel
  {
    private TextBox tbStart;
    private TextBox tbEnd;
    private TextBox tbSize;
    private SizeRange FRange;

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

    private string ConvertFloat(float value)
    {
      return float.IsNaN(value) ? Res.Get("Forms,MapEditor,Common,Auto") : value.ToString(CultureInfo.InvariantCulture.NumberFormat);
    }

    private float ConvertFloat(string value)
    {
      try
      {
        return float.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat);
      }
      catch
      {
        return float.NaN;
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

    private void tbSize_Leave(object sender, EventArgs e)
    {
      FRange.Size = ConvertFloat(tbSize.Text);
      tbSize.Text = ConvertFloat(FRange.Size);
      OnChange();
    }

    public SizeRangePanel(SizeRange range)
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

      tbSize = new TextBox();
      tbSize.Parent = this;
      tbSize.Bounds = new Rectangle(128, 0, 155, 20);
      tbSize.Text = ConvertFloat(range.Size);
      tbSize.Leave += new EventHandler(tbSize_Leave);
      
      Size = new Size(284, 24);
    }
  }
}
