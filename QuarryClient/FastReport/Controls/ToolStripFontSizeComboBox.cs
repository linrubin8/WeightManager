using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;

namespace FastReport.Controls
{
  internal class ToolStripFontSizeComboBox : FRToolStripComboBox
  {
    private float FFontSize;
    private bool FUpdating;
    
    public event EventHandler SizeSelected;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float FontSize
    {
      get 
      { 
        FFontSize = Converter.StringToFloat(Text, true);
        UpdateText();
        return FFontSize; 
      }
      set 
      { 
        FFontSize = value;
        UpdateText();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new System.Windows.Forms.ComboBox.ObjectCollection Items 
    { 
      get { return base.Items; }
    }

    private void UpdateText()
    {
      FUpdating = true;
      Text = Converter.DecreasePrecision(FFontSize, 2).ToString();
      FUpdating = false;
    }
    
    private void OnSizeSelected()
    {
      if (FUpdating)
        return;
      if (SizeSelected != null)
        SizeSelected(this, EventArgs.Empty);
    }
    
    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        OnSizeSelected();
    }

    protected override void OnSelectedIndexChanged(EventArgs e)
    {
      OnSizeSelected();
    }
    
    public ToolStripFontSizeComboBox()
    {
      Size = new Size(40, 25);
      Items.AddRange(new string[] {
        "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", 
        "22", "24", "26", "28", "36", "48", "72"});
    }

  }
}
