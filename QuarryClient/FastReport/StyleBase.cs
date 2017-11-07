using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FastReport.Utils;

namespace FastReport
{
  /// <summary>
  /// Represents the base class for the report style or the highlight condition.
  /// </summary>
  public class StyleBase : IFRSerializable
  {
    private Border FBorder;
    private FillBase FFill;
    private FillBase FTextFill;
    private Font FFont;
    private bool FApplyBorder;
    private bool FApplyFill;
    private bool FApplyTextFill;
    private bool FApplyFont;

    /// <summary>
    /// Gets or sets a border.
    /// </summary>
    public Border Border
    {
      get { return FBorder; }
      set { FBorder = value; }
    }

    /// <summary>
    /// Gets or sets a fill.
    /// </summary>
    public FillBase Fill
    {
      get { return FFill; }
      set { FFill = value; }
    }

    /// <summary>
    /// Gets or sets a text fill.
    /// </summary>
    public FillBase TextFill
    {
      get { return FTextFill; }
      set { FTextFill = value; }
    }

    /// <summary>
    /// Gets or sets a font.
    /// </summary>
    public Font Font
    {
      get { return FFont; }
      set { FFont = value; }
    }

    /// <summary>
    /// Gets or sets a value determines that the border must be applied.
    /// </summary>
    public bool ApplyBorder
    {
      get { return FApplyBorder; }
      set { FApplyBorder = value; }
    }

    /// <summary>
    /// Gets or sets a value determines that the fill must be applied.
    /// </summary>
    public bool ApplyFill
    {
      get { return FApplyFill; }
      set { FApplyFill = value; }
    }

    /// <summary>
    /// Gets or sets a value determines that the text fill must be applied.
    /// </summary>
    public bool ApplyTextFill
    {
      get { return FApplyTextFill; }
      set { FApplyTextFill = value; }
    }

    /// <summary>
    /// Gets or sets a value determines that the font must be applied.
    /// </summary>
    public bool ApplyFont
    {
      get { return FApplyFont; }
      set { FApplyFont = value; }
    }

    /// <summary>
    /// Serializes the style.
    /// </summary>
    /// <param name="writer">Writer object.</param>
    /// <remarks>
    /// This method is for internal use only.
    /// </remarks>
    public virtual void Serialize(FRWriter writer)
    {
      StyleBase c = writer.DiffObject as StyleBase;

      Border.Serialize(writer, "Border", c.Border);
      Fill.Serialize(writer, "Fill", c.Fill);
      TextFill.Serialize(writer, "TextFill", c.TextFill);
      if (!Font.Equals(c.Font))
        writer.WriteValue("Font", Font);
      if (ApplyBorder != c.ApplyBorder)
        writer.WriteBool("ApplyBorder", ApplyBorder);
      if (ApplyFill != c.ApplyFill)
        writer.WriteBool("ApplyFill", ApplyFill);
      if (ApplyTextFill != c.ApplyTextFill)
        writer.WriteBool("ApplyTextFill", ApplyTextFill);
      if (ApplyFont != c.ApplyFont)
        writer.WriteBool("ApplyFont", ApplyFont);
    }

    /// <summary>
    /// Deserializes the style.
    /// </summary>
    /// <param name="reader">Reader object.</param>
    /// <remarks>
    /// This method is for internal use only.
    /// </remarks>
    public void Deserialize(FRReader reader)
    {
      reader.ReadProperties(this);
    }

    /// <summary>
    /// Assigns values from another source.
    /// </summary>
    /// <param name="source">Source to assign from.</param>
    public virtual void Assign(StyleBase source)
    {
      Border = source.Border.Clone();
      Fill = source.Fill.Clone();
      TextFill = source.TextFill.Clone();
      Font = source.Font;
      ApplyBorder = source.ApplyBorder;
      ApplyFill = source.ApplyFill;
      ApplyTextFill = source.ApplyTextFill;
      ApplyFont = source.ApplyFont;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StyleBase"/> class with default settings.
    /// </summary>
    public StyleBase()
    {
      Border = new Border();
      Fill = new SolidFill();
      TextFill = new SolidFill(Color.Black);
      Font = Config.DesignerSettings.DefaultFont;
    }
  }
}

