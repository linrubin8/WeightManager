using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using FastReport.Design;
using System.Windows.Forms;
using FastReport.Forms;

namespace FastReport
{
  /// <summary>
  /// Holds the list of <see cref="ReportComponentBase"/> objects currently selected in the designer.
  /// </summary>
  /// <remarks>
  /// This class is used by the "Border and Fill" toolbar. Use methods of this class to perform some 
  /// operations on the selected objects. 
  /// <para/>Note: after calling any method in this class, call the 
  /// <see cref="Designer.SetModified()">Designer.SetModified</see> method to reflect changes.
  /// <para/>Note: this list contains only objects of <see cref="ReportComponentBase"/> type. 
  /// If you want to access all selected objects, use the <see cref="Designer.SelectedObjects"/> property.
  /// </remarks>
  public class SelectedReportComponents
  {
    private List<ReportComponentBase> FList;
    private Designer FDesigner;

    /// <summary>
    /// Gets the first selected object.
    /// </summary>
    public ReportComponentBase First
    {
      get { return FList.Count > 0 ? FList[0] : null; }
    }

    /// <summary>
    /// Gets the number of selected objects.
    /// </summary>
    public int Count
    {
      get { return FList.Count; }
    }
    
    /// <summary>
    /// Gets a value indicating whether the operations are enabled.
    /// </summary>
    public bool Enabled
    {
      get
      {
        return Count > 1 || (Count == 1 && !First.HasRestriction(Restrictions.DontModify));
      }
    }
    
    /// <summary>
    /// Gets a value indicating whether the object with simple border is selected.
    /// </summary>
    /// <remarks>
    /// When the object has a simple border, you cannot change individual border lines.
    /// Example of such an object is the "Shape" and "Line" objects.
    /// </remarks>
    public bool SimpleBorder
    {
      get
      {
        foreach (ReportComponentBase c in FList)
        {
          if (!c.FlagSimpleBorder)
            return false;
        }
        return true;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the border operations are enabled.
    /// </summary>
    public bool BorderEnabled
    {
      get
      {
        foreach (ReportComponentBase c in FList)
        {
          if (c.FlagUseBorder)
            return true;
        }
        return false;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the fill operations are enabled.
    /// </summary>
    public bool FillEnabled
    {
      get
      {
        foreach (ReportComponentBase c in FList)
        {
          if (c.FlagUseFill)
            return true;
        }
        return false;
      }
    }

    private List<ReportComponentBase> ModifyList
    {
      get { return FList.FindAll(CanModify); }
    }

    private bool CanModify(ReportComponentBase c)
    {
      return !c.HasRestriction(Restrictions.DontModify);
    }

    internal void Update()
    {
      FList.Clear();
      if (FDesigner.SelectedObjects != null)
      {
        foreach (Base c in FDesigner.SelectedObjects)
        {
          if (c is ReportComponentBase)
            FList.Add(c as ReportComponentBase);
        }
      }
    }

    /// <summary>
    /// Sets the solid fill color for the selected objects.
    /// </summary>
    /// <param name="color">Fill color.</param>
    public void SetColor(Color color)
    {
      foreach (ReportComponentBase c in ModifyList)
      {
        c.Fill = new SolidFill(color);
      }
      FDesigner.LastFormatting.Fill = new SolidFill(color);
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the fill for the selected objects.
    /// </summary>
    /// <param name="fill">Fill.</param>
    public void SetFill(FillBase fill)
    {
      foreach (ReportComponentBase c in ModifyList)
      {
        c.Fill = fill.Clone();
      }
      FDesigner.LastFormatting.Fill = fill.Clone();
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the style for the selected objects.
    /// </summary>
    /// <param name="style">Style name.</param>
    public void SetStyle(string style)
    {
      foreach (ReportComponentBase c in ModifyList)
      {
        c.Style = style;
      }
      FDesigner.SetModified();
    }
    
    /// <summary>
    /// Sets the hyperlink for the selected objects.
    /// </summary>
    /// <param name="hyperlink">Hyperlink.</param>
    /// <param name="modifyAppearance">Indicates whether to modify the object's appearance.</param>
    /// <param name="setModified">Indicates whether it is necessary to change designer's modified state.</param>
    public void SetHyperlink(Hyperlink hyperlink, bool modifyAppearance, bool setModified)
    {
      foreach (ReportComponentBase c in ModifyList)
      {
        c.Hyperlink.Assign(hyperlink);
        if (modifyAppearance)
        {
          c.Cursor = Cursors.Hand;
          if (c is TextObject)
          {
            (c as TextObject).TextFill = new SolidFill(Color.Blue);
            (c as TextObject).Font = new Font((c as TextObject).Font, (c as TextObject).Font.Style | FontStyle.Underline);
          }
        }
      }
      if (setModified)
        FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the CanGrow flag for the selected objects.
    /// </summary>
    /// <param name="value">Flag value.</param>
    public void SetCanGrow(bool value)
    {
      foreach (ReportComponentBase c in ModifyList)
      {
        c.CanGrow = value;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the CanShrink flag for the selected objects.
    /// </summary>
    /// <param name="value">Flag value.</param>
    public void SetCanShrink(bool value)
    {
      foreach (ReportComponentBase c in ModifyList)
      {
        c.CanShrink = value;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the GrowToBottom flag for the selected objects.
    /// </summary>
    /// <param name="value">Flag value.</param>
    public void SetGrowToBottom(bool value)
    {
      foreach (ReportComponentBase c in ModifyList)
      {
        c.GrowToBottom = value;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Toggles the specified border line for the selected objects.
    /// </summary>
    /// <param name="line">Border line.</param>
    /// <param name="toggle">Toggle value.</param>
    public void ToggleLine(BorderLines line, bool toggle)
    {
      foreach (ReportComponentBase c in ModifyList)
      {
        if (toggle)
          c.Border.Lines |= line;
        else
          c.Border.Lines &= ~line;
      }
      FDesigner.LastFormatting.Border.Lines = First.Border.Lines;
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the border color for the selected objects.
    /// </summary>
    /// <param name="color">Border color.</param>
    public void SetLineColor(Color color)
    {
      foreach (ReportComponentBase c in ModifyList)
      {
        c.Border.Color = color;
      }
      FDesigner.LastFormatting.Border.Color = color;
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the border width for the selected objects.
    /// </summary>
    /// <param name="width">Border width.</param>
    public void SetWidth(float width)
    {
      foreach (ReportComponentBase c in ModifyList)
      {
        c.Border.Width = width;
      }
      FDesigner.LastFormatting.Border.Width = width;
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the border style for the selected objects.
    /// </summary>
    /// <param name="style">Border style.</param>
    public void SetLineStyle(LineStyle style)
    {
      foreach (ReportComponentBase c in ModifyList)
      {
        c.Border.Style = style;
      }
      FDesigner.LastFormatting.Border.Style = style;
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the border for the selected objects.
    /// </summary>
    /// <param name="border">Border.</param>
    public void SetBorder(Border border)
    {
      foreach (ReportComponentBase c in ModifyList)
      {
        c.Border = border.Clone();
      }
      FDesigner.LastFormatting.Border = border.Clone();
      FDesigner.SetModified();
    }

    /// <summary>
    /// Invokes the fill editor for the selected objects.
    /// </summary>
    /// <returns><b>true</b> if editor was closed by the OK button.</returns>
    public bool InvokeFillEditor()
    {
      using (FillEditorForm editor = new FillEditorForm())
      {
        editor.Fill = First.Fill.Clone();
        if (editor.ShowDialog() == DialogResult.OK)
        {
          SetFill(editor.Fill);
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Invokes the border editor for the selected objects.
    /// </summary>
    /// <returns><b>true</b> if editor was closed by the OK button.</returns>
    public bool InvokeBorderEditor()
    {
      using (BorderEditorForm editor = new BorderEditorForm())
      {
        editor.Border = First.Border.Clone();
        if (editor.ShowDialog() == DialogResult.OK)
        {
          SetBorder(editor.Border);
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Invokes the hyperlink editor for the selected objects.
    /// </summary>
    /// <returns><b>true</b> if editor was closed by the OK button.</returns>
    public bool InvokeHyperlinkEditor()
    {
      using (HyperlinkEditorForm form = new HyperlinkEditorForm())
      {
        form.ReportComponent = First;
        if (form.ShowDialog() == DialogResult.OK)
        {
          SetHyperlink(form.Hyperlink, form.ModifyAppearance, true);
          return true;
        }
      }
      return false;
    }

    internal SelectedReportComponents(Designer designer)
    {
      FDesigner = designer;
      FList = new List<ReportComponentBase>();
    }

  }
}
