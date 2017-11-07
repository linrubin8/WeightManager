using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using FastReport.Controls;

namespace FastReport.TypeEditors
{
  /// <summary>
  /// Provides a user interface for choosing a color.
  /// </summary>
  public class ColorEditor : UITypeEditor
  {
    private IWindowsFormsEditorService edSvc = null;

    private void selector_ColorSelected(object sender, EventArgs e)
    {
      edSvc.CloseDropDown();
    }

    /// <inheritdoc/>
    public override void PaintValue(PaintValueEventArgs e)
    {
      if (e.Value != null)
      {
        using (Brush brush = new SolidBrush((Color)e.Value)) 
          e.Graphics.FillRectangle(brush, e.Bounds);
      }  
    }

    /// <inheritdoc/>
    public override bool GetPaintValueSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    /// <inheritdoc/>
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    /// <inheritdoc/>
    public override object EditValue(ITypeDescriptorContext context,
      IServiceProvider provider, object Value)
    {
      // this method is called when we click on drop-down arrow
      edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
      ColorSelector selector = new ColorSelector();
      selector.Color = Value == null ? Color.Transparent : (Color)Value;
      selector.ColorSelected += new EventHandler(selector_ColorSelected);
      edSvc.DropDownControl(selector);
      return selector.Color;
    }

  }
}
