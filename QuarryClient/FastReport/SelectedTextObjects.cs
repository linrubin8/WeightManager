using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FastReport.Design;
using FastReport.Format;
using FastReport.Forms;
using System.Windows.Forms;

namespace FastReport
{
  /// <summary>
  /// Holds the list of <see cref="TextObject"/> objects currently selected in the designer.
  /// </summary>
  /// <remarks>
  /// This class is used by the "Text" toolbar. Use methods of this class to perform some 
  /// operations on the selected objects. 
  /// <para/>Note: after calling any method in this class, call the 
  /// <see cref="Designer.SetModified()">Designer.SetModified</see> method to reflect changes.
  /// <para/>Note: this list contains only objects of <see cref="TextObject"/> type. If you want to access all
  /// selected objects, use the <see cref="Designer.SelectedObjects"/> property.
  /// </remarks>
  public class SelectedTextObjects
  {
    private List<TextObject> FList;
    private Designer FDesigner;

    /// <summary>
    /// Gets the first selected object.
    /// </summary>
    public TextObject First
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
        return Count > 1 || (Count == 1 && CanModify(First));
      }
    }

    private List<TextObject> ModifyList
    {
      get { return FList.FindAll(CanModify); }
    }
    
    private bool CanModify(TextObject c)
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
          if (c is TextObject)
            FList.Add(c as TextObject);
        }
      }
    }
    
    /// <summary>
    /// Sets the font name for the selected objects.
    /// </summary>
    /// <param name="name">Font name.</param>
    public void SetFontName(string name)
    {
      foreach (TextObject text in ModifyList)
      {
        text.Font = new Font(name, text.Font.Size, text.Font.Style); 
      }
      FDesigner.LastFormatting.Font = First.Font;
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the font size for the selected objects.
    /// </summary>
    /// <param name="size">Font size.</param>
    public void SetFontSize(float size)
    {
      foreach (TextObject text in ModifyList)
      {
        text.Font = new Font(text.Font.Name, size, text.Font.Style);
      }
      FDesigner.LastFormatting.Font = First.Font;
      FDesigner.SetModified();
    }

    /// <summary>
    /// Toggles the specified font style for the selected objects.
    /// </summary>
    /// <param name="style">Font style.</param>
    /// <param name="toggle">Toggle value.</param>
    public void ToggleFontStyle(FontStyle style, bool toggle)
    {
      foreach (TextObject text in ModifyList)
      {
        FontStyle newStyle = text.Font.Style;
        if (toggle)
          newStyle |= style;
        else
          newStyle &= ~style;

        // some fonts do not support particular styles
        try
        {
          text.Font = new Font(text.Font, newStyle);
        }
        catch
        {
        }
      }
      FDesigner.LastFormatting.Font = First.Font;
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the horizontal text alignment for tthe selected objects.
    /// </summary>
    /// <param name="align">Alignment to set.</param>
    public void SetHAlign(HorzAlign align)
    {
      foreach (TextObject text in ModifyList)
      {
        text.HorzAlign = align;
      }
      FDesigner.LastFormatting.HorzAlign = align;
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the vertical text alignment for tthe selected objects.
    /// </summary>
    /// <param name="align">Alignment to set.</param>
    public void SetVAlign(VertAlign align)
    {
      foreach (TextObject text in ModifyList)
      {
        text.VertAlign = align;
      }
      FDesigner.LastFormatting.VertAlign = align;
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the text color for the selected objects.
    /// </summary>
    /// <param name="color">Text color.</param>
    public void SetTextColor(Color color)
    {
      foreach (TextObject text in ModifyList)
      {
        text.TextFill = new SolidFill(color);
      }
      FDesigner.LastFormatting.TextFill = new SolidFill(color);
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the angle for the selected objects.
    /// </summary>
    /// <param name="angle">Angle to set.</param>
    public void SetAngle(int angle)
    {
      foreach (TextObject text in ModifyList)
      {
        text.Angle = angle;
      }
      FDesigner.LastFormatting.Angle = angle;
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the AutoWidth property value for the selected objects.
    /// </summary>
    /// <param name="value">Value to set.</param>
    public void SetAutoWidth(bool value)
    {
      foreach (TextObject text in ModifyList)
      {
        text.AutoWidth = value;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the WordWrap property value for the selected objects.
    /// </summary>
    /// <param name="value">Value to set.</param>
    public void SetWordWrap(bool value)
    {
      foreach (TextObject text in ModifyList)
      {
        text.WordWrap = value;
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Sets the highlight conditions for the selected objects.
    /// </summary>
    /// <param name="value">Highlight conditions.</param>
    public void SetConditions(ConditionCollection value)
    {
      foreach (TextObject text in ModifyList)
      {
        text.Highlight.Assign(value);
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Clears the text of the selected objects.
    /// </summary>
    public void ClearText()
    {
      foreach (TextObject text in ModifyList)
      {
        text.Text = "";
      }
      FDesigner.SetModified();
    }

    /// <summary>
    /// Invokes the highlight editor for the selected objects.
    /// </summary>
    /// <returns><b>true</b> if editor was closed with the OK button.</returns>
    public bool InvokeHighlightEditor()
    {
      using (HighlightEditorForm form = new HighlightEditorForm(FDesigner.ActiveReport))
      {
        form.Conditions = First.Highlight;
        if (form.ShowDialog() == DialogResult.OK)
        {
          SetConditions(form.Conditions);
          return true;
        }
      }
      return false;
    }

    internal SelectedTextObjects(Designer designer)
    {
      FDesigner = designer;
      FList = new List<TextObject>();
    }
  }
}
