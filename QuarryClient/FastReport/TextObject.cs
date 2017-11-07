using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Globalization;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.Design;
using FastReport.Design.PageDesigners.Page;
using FastReport.Forms;
using FastReport.Format;
using FastReport.Data;
using FastReport.Code;
using FastReport.TypeConverters;
using FastReport.Engine;
using FastReport.DevComponents.DotNetBar;
using System.Text;

namespace FastReport
{
  /// <summary>
  /// Specifies the horizontal alignment of a text in the TextObject object.
  /// </summary>
  public enum HorzAlign
  {
    /// <summary>
    /// Specifies that text is aligned in the left of the layout rectangle.
    /// </summary>
    Left,

    /// <summary>
    /// Specifies that text is aligned in the center of the layout rectangle.
    /// </summary>
    Center,

    /// <summary>
    /// Specifies that text is aligned in the right of the layout rectangle.
    /// </summary>
    Right,

    /// <summary>
    /// Specifies that text is aligned in the left and right sides of the layout rectangle.
    /// </summary>
    Justify
  }

  /// <summary>
  /// Specifies the vertical alignment of a text in the TextObject object.
  /// </summary>
  public enum VertAlign
  {
    /// <summary>
    /// Specifies that text is aligned in the top of the layout rectangle.
    /// </summary>
    Top,

    /// <summary>
    /// Specifies that text is aligned in the center of the layout rectangle.
    /// </summary>
    Center,

    /// <summary>
    /// Specifies that text is aligned in the bottom of the layout rectangle.
    /// </summary>
    Bottom
  }

  /// <summary>
  /// Specifies the behavior of the <b>AutoShrink</b> feature of <b>TextObject</b>.
  /// </summary>
  public enum AutoShrinkMode
  {
    /// <summary>
    /// AutoShrink is disabled.
    /// </summary>
    None,

    /// <summary>
    /// AutoShrink decreases the <b>Font.Size</b> property of the <b>TextObject</b>.
    /// </summary>
    FontSize,

    /// <summary>
    /// AutoShrink decreases the <b>FontWidthRatio</b> property of the <b>TextObject</b>.
    /// </summary>
    FontWidth
  }

  /// <summary>
  /// Represents the Text object that may display one or several text lines.
  /// </summary>
  /// <remarks>
  /// Specify the object's text in the <see cref="TextObjectBase.Text">Text</see> property. 
  /// Text may contain expressions and data items, for example: "Today is [Date]". When report 
  /// is running, all expressions are calculated and replaced with actual values, so the text 
  /// would be "Today is 01.01.2008".
  /// <para/>The symbols used to find expressions in a text are set in the 
  /// <see cref="TextObjectBase.Brackets">Brackets</see> property. You also may disable expressions 
  /// using the <see cref="TextObjectBase.AllowExpressions">AllowExpressions</see> property.
  /// <para/>To format an expression value, use the <see cref="Format"/> property.
  /// </remarks>
  public class TextObject : TextObjectBase, IHasEditor
  {
    #region Fields
    private bool FAutoWidth;
    private HorzAlign FHorzAlign;
    private VertAlign FVertAlign;
    private int FAngle;
    private bool FRightToLeft;
    private bool FWordWrap;
    private bool FUnderlines;
    private Font FFont;
    private FillBase FTextFill;
    private TextOutline FTextOutline;
    private StringTrimming FTrimming;
    private float FFontWidthRatio;
    private float FFirstTabOffset;
    private float FTabWidth;
    private bool FClip;
    private ConditionCollection FHighlight;
    private bool FWysiwyg;
    private float FLineHeight;
    private bool FForceJustify;
    private bool FHtmlTags;
    private AutoShrinkMode FAutoShrink;
    private float FAutoShrinkMinSize;
    private float FParagraphOffset;
    
    private TextBox FTextBox;
    private FillBase FSavedTextFill;
    private Font FSavedFont;
    private bool FDragAccept;
    private string FSavedText;
    private FormatBase FSavedFormat;
    private InlineImageCache FInlineImageCache;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets a value that determines if the text object should handle its width automatically.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool AutoWidth
    {
      get { return FAutoWidth; }
      set { FAutoWidth = value; }
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the font size should shrink to
    /// display the longest text line without word wrap.
    /// </summary>
    /// <remarks>
    /// To limit the minimum size, use the <see cref="AutoShrinkMinSize"/> property.
    /// </remarks>
    [DefaultValue(AutoShrinkMode.None)]
    [Category("Behavior")]
    public AutoShrinkMode AutoShrink
    {
      get { return FAutoShrink; }
      set { FAutoShrink = value; }
    }

    /// <summary>
    /// Gets or sets the minimum size of font (or minimum width ratio) if the <see cref="AutoShrink"/>
    /// mode is on.
    /// </summary>
    /// <remarks>
    /// This property determines the minimum font size (in case the <see cref="AutoShrink"/> property is set to
    /// <b>FontSize</b>), or the minimum font width ratio (if <b>AutoShrink</b> is set to <b>FontWidth</b>).
    /// <para/>The default value is 0, that means no limits.
    /// </remarks>
    [DefaultValue(0f)]
    [Category("Behavior")]
    public float AutoShrinkMinSize
    {
      get { return FAutoShrinkMinSize; }
      set { FAutoShrinkMinSize = value; }
    }

    /// <summary>
    /// Gets or sets the horizontal alignment of a text in the TextObject object.
    /// </summary>
    [DefaultValue(HorzAlign.Left)]
    [Category("Appearance")]
    public HorzAlign HorzAlign
    {
      get { return FHorzAlign; }
      set { FHorzAlign = value; }
    }

    /// <summary>
    /// Gets or sets the vertical alignment of a text in the TextObject object.
    /// </summary>
    [DefaultValue(VertAlign.Top)]
    [Category("Appearance")]
    public VertAlign VertAlign
    {
      get { return FVertAlign; }
      set { FVertAlign = value; }
    }

    /// <summary>
    /// Gets or sets the text angle, in degrees.
    /// </summary>
    [DefaultValue(0)]
    [Editor(typeof(AngleEditor), typeof(UITypeEditor))]
    [Category("Appearance")]
    public int Angle
    {
      get { return FAngle; }
      set { FAngle = value; }
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the component should draw right-to-left for RTL languages.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool RightToLeft
    {
      get { return FRightToLeft; }
      set { FRightToLeft = value; }
    }

    /// <summary>
    /// Gets or sets a value that indicates if lines are automatically word-wrapped.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool WordWrap
    {
      get { return FWordWrap; }
      set { FWordWrap = value; }
    }

    /// <summary>
    /// Gets or sets a value that determines if the text object will underline each text line.
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool Underlines
    {
      get { return FUnderlines; }
      set { FUnderlines = value; }
    }

    /// <summary>
    /// Gets or sets the font settings for this object.
    /// </summary>
    [Category("Appearance")]
    public Font Font
    {
      get { return FFont; }
      set
      {
        FFont = value;
        if (!String.IsNullOrEmpty(Style))
          Style = "";
      }
    }

    /// <summary>
    /// Gets or sets the fill color used to draw a text.
    /// </summary>
    /// <remarks>
    /// Default fill is <see cref="SolidFill"/>. You may specify other fill types, for example:
    /// <code>
    /// text1.TextFill = new HatchFill(Color.Black, Color.White, HatchStyle.Cross);
    /// </code>
    /// Use the <see cref="TextColor"/> property to set the solid text color.
    /// </remarks>
    [Category("Appearance")]
    [Editor(typeof(FillEditor), typeof(UITypeEditor))]
    public FillBase TextFill
    {
      get { return FTextFill; }
      set
      {
        if (value == null)
          throw new ArgumentNullException("TextFill");
        FTextFill = value;
        if (!String.IsNullOrEmpty(Style))
          Style = "";
      }
    }

    /// <summary>
    /// Gets or sets the text outline.
    /// </summary>
    [Category("Appearance")]
    [Editor(typeof(OutlineEditor), typeof(UITypeEditor))]
    public TextOutline TextOutline
    {
        get { return FTextOutline; }
        set
        {
            if (value == null)
                throw new ArgumentNullException("TextOutline");
            FTextOutline = value;
            if (!String.IsNullOrEmpty(Style))
                Style = "";
        }
    }

    /// <summary>
    /// Gets or sets the text color in a simple manner.
    /// </summary>
    /// <remarks>
    /// This property can be used in a report script to change the text color of the object. It is 
    /// equivalent to: <code>textObject1.TextFill = new SolidFill(color);</code>
    /// </remarks>
    [Browsable(false)]
    public Color TextColor
    {
      get { return TextFill is SolidFill ? (TextFill as SolidFill).Color : Color.Black; }
      set { TextFill = new SolidFill(value); }
    }

    /// <summary>
    /// Gets or sets the string trimming options.
    /// </summary>
    [DefaultValue(StringTrimming.None)]
    [Category("Behavior")]
    public StringTrimming Trimming
    {
      get { return FTrimming; }
      set { FTrimming = value; }
    }

    /// <summary>
    /// Gets or sets the width ratio of the font. 
    /// </summary>
    /// <remarks>
    /// Default value is 1. To make a font wider, set a value grether than 1; to make a font narrower,
    /// set a value less than 1.
    /// </remarks>
    [DefaultValue(1f)]
    [Category("Appearance")]
    public float FontWidthRatio
    {
      get { return FFontWidthRatio; }
      set { FFontWidthRatio = value; }
    }

    /// <summary>
    /// Gets or sets the height of single text line, in pixels.
    /// </summary>
    [DefaultValue(0f)]
    //[TypeConverter(typeof(UnitsConverter))]
    [Category("Appearance")]
    public float LineHeight
    {
      get { return FLineHeight; }
      set { FLineHeight = value; }
    }

    /// <summary>
    /// Gets or sets the offset of the first TAB symbol.
    /// </summary>
    [DefaultValue(0f)]
    [TypeConverter(typeof(UnitsConverter))]
    [Category("Appearance")]
    public float FirstTabOffset
    {
      get { return FFirstTabOffset; }
      set { FFirstTabOffset = value; }
    }

    /// <summary>
    /// Gets or sets the width of TAB symbol, in pixels.
    /// </summary>
    [DefaultValue(58f)]
    [Category("Appearance")]
    public float TabWidth
    {
      get { return FTabWidth; }
      set { FTabWidth = value; }
    }

    /// <summary>
    /// Gets or sets a value that indicates if text should be clipped inside the object's bounds.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool Clip
    {
      get { return FClip; }
      set { FClip = value; }
    }

    /// <summary>
    /// Gets the collection of conditional highlight attributes.
    /// </summary>
    /// <remarks>
    /// Conditional highlight is used to change the visual appearance of the Text object
    /// depending on some condition(s). For example, you may highlight negative values displayed by
    /// the Text object with red color. To do this, add the highlight condition:
    /// <code>
    /// TextObject text1;
    /// HighlightCondition highlight = new HighlightCondition();
    /// highlight.Expression = "Value &lt; 0";
    /// highlight.Fill = new SolidFill(Color.Red);
    /// highlight.ApplyFill = true;
    /// text1.Highlight.Add(highlight);
    /// </code>
    /// </remarks>
    [Editor(typeof(HighlightEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public ConditionCollection Highlight
    {
      get { return FHighlight; }
    }

    /// <summary>
    /// Gets or sets a value that indicates if the text object should display its contents similar to the printout.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool Wysiwyg
    {
      get { return FWysiwyg; }
      set { FWysiwyg = value; }
    }
    
    /// <summary>
    /// Forces justify for the last text line.
    /// </summary>
    [Browsable(false)]
    public bool ForceJustify
    {
      get { return FForceJustify; }
      set { FForceJustify = value; }
    }

    /// <summary>
    /// Allows handling html tags in the text.
    /// </summary>
    /// <remarks>
    /// The following html tags can be used in the object's text: &lt;b&gt;, &lt;i&gt;, &lt;u&gt;,
    /// &lt;strike&gt;, &lt;sub&gt;, &lt;sup&gt;, &lt;/b&gt;, &lt;/i&gt;, &lt;/u&gt;,
    /// &lt;/strike&gt;, &lt;/sub&gt;, &lt;/sup&gt;,
    /// &lt;font color=&amp;...&amp;&gt;, &lt;/font&gt;. Font size cannot
    /// be changed due to limitations in the rendering engine.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool HtmlTags
    {
      get { return FHtmlTags; }
      set { FHtmlTags = value; }
    }

    /// <summary>
    /// Gets or sets the paragraph offset, in pixels.
    /// </summary>
    [DefaultValue(0f)]
    [Category("Appearance")]
    [TypeConverter(typeof(UnitsConverter))]
    public float ParagraphOffset
    {
      get { return FParagraphOffset; }
      set { FParagraphOffset = value; }
    }

    /// <inheritdoc/>
    public override float Left
    {
      get { return base.Left; }
      set
      {
        base.Left = value;
        if (IsEditing)
          UpdateEditorPosition();
      }
    }

    /// <inheritdoc/>
    public override float Top
    {
      get { return base.Top; }
      set
      {
        base.Top = value;
        if (IsEditing)
          UpdateEditorPosition();
      }
    }

    /// <inheritdoc/>
    public override float Width
    {
      get { return base.Width; }
      set
      {
        base.Width = value;
        if (IsEditing)
          UpdateEditorPosition();
      }
    }

    /// <inheritdoc/>
    public override float Height
    {
      get { return base.Height; }
      set
      {
        base.Height = value;
        if (IsEditing)
          UpdateEditorPosition();
      }
    }

    internal bool IsAdvancedRendererNeeded
    {
      get { return HorzAlign == HorzAlign.Justify || Wysiwyg || LineHeight != 0 || HtmlTags; }
    }
    
    private bool IsEditing
    {
      get { return IsDesigning && FTextBox != null; }
    }

    /// <summary>
    /// Cache for inline images
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public InlineImageCache InlineImageCache
    {
      get
      {
        if (FInlineImageCache == null)
          FInlineImageCache = new InlineImageCache();
        return FInlineImageCache;
      }
    }
    #endregion

    #region Private Methods
    private bool ShouldSerializeFont()
    {
      return Font.Name != "Arial" || Font.Size != 10 || Font.Style != FontStyle.Regular;
    }

    private bool ShouldSerializeTextFill()
    {
      return !(TextFill is SolidFill) || (TextFill as SolidFill).Color != Color.Black;
    }

    private void DrawUnderlines(FRPaintEventArgs e)
    {
      if (!Underlines || Angle != 0)
        return;
      
      Graphics g = e.Graphics;
      float lineHeight = LineHeight == 0 ? Font.GetHeight() : LineHeight;
      lineHeight *= e.ScaleY;
      float curY = AbsTop * e.ScaleY + lineHeight + 1;
      Pen pen = e.Cache.GetPen(Border.Color, Border.Width * e.ScaleY, DashStyle.Solid);
      while (curY < AbsBottom * e.ScaleY)
      {
        g.DrawLine(pen, AbsLeft * e.ScaleX, curY, AbsRight * e.ScaleY, curY);
        curY += lineHeight;
      }
    }

    private void UpdateEditorPosition()
    {
      FTextBox.Location = new Point((int)Math.Round(AbsLeft * ReportWorkspace.Scale) + 1,
        (int)Math.Round(AbsTop * ReportWorkspace.Scale) + 1);
      FTextBox.Size = new Size((int)Math.Round(Width * ReportWorkspace.Scale) - 1,
        (int)Math.Round(Height * ReportWorkspace.Scale) - 1);
    }

    private void FTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        FinishEdit(false);

      if (e.Control && e.KeyCode == Keys.Enter)
        FinishEdit(true);
    }

    private TextRenderingHint GetTextQuality(TextQuality quality)
    {
      switch (quality)
      {
        case TextQuality.Regular:
          return TextRenderingHint.AntiAliasGridFit;

        case TextQuality.ClearType:
          return TextRenderingHint.ClearTypeGridFit;

        case TextQuality.AntiAlias:
          return TextRenderingHint.AntiAlias;
      }

      return TextRenderingHint.SystemDefault;
    }

    private SizeF CalcSize()
    {
      Report report = Report;
      if (String.IsNullOrEmpty(Text) || report == null)
        return new SizeF(0, 0);

      Font font = report.GraphicCache.GetFont(Font.Name, Font.Size * 96f / DrawUtils.ScreenDpi, Font.Style);
      float width = 0;
      if (WordWrap)
      {
        if (Angle == 90 || Angle == 270)
          width = Height - Padding.Vertical;
        else
          width = Width - Padding.Horizontal;
      }

      Graphics g = report.MeasureGraphics;
      GraphicsState state = g.Save();
      try
      {
        if (report.TextQuality != TextQuality.Default)
          g.TextRenderingHint = GetTextQuality(report.TextQuality);

        if (IsAdvancedRendererNeeded)
        {
          if (width == 0)
            width = 100000;
          AdvancedTextRenderer renderer = new AdvancedTextRenderer(Text, g, font, Brushes.Black, Pens.Black,
            new RectangleF(0, 0, width, 100000), GetStringFormat(report.GraphicCache, 0),
            HorzAlign, VertAlign, LineHeight, Angle, FontWidthRatio, false, Wysiwyg, HtmlTags, false, 96f / DrawUtils.ScreenDpi
            , InlineImageCache);
          float height = renderer.CalcHeight();
          width = renderer.CalcWidth();

          width += Padding.Horizontal + 1;
          if (LineHeight == 0)
            height += Padding.Vertical + 1;
          return new SizeF(width, height);
        }
        else
        {
          if (FontWidthRatio != 1)
            width /= FontWidthRatio;
          SizeF size = g.MeasureString(Text, font, new SizeF(width, 100000));
          size.Width += Padding.Horizontal + 1;
          size.Height += Padding.Vertical + 1;
          return size;
        }  
      }
      finally
      {
        g.Restore(state);
      }
    }

    private float InternalCalcWidth()
    {
      bool saveWordWrap = WordWrap;
      WordWrap = false;
      SizeF size = CalcSize();
      WordWrap = saveWordWrap;
      return size.Width;
    }

    private float InternalCalcHeight()
    {
      return CalcSize().Height;
    }

    private string BreakText()
    {
      ForceJustify = false;
      if (String.IsNullOrEmpty(Text))
        return "";
        
      string result = null;
      Report report = Report;
      if (report == null)
        return "";

      Font font = report.GraphicCache.GetFont(Font.Name, Font.Size * 96f / DrawUtils.ScreenDpi, Font.Style);
      StringFormat format = GetStringFormat(report.GraphicCache, StringFormatFlags.LineLimit);
      RectangleF textRect = new RectangleF(0, 0, Width - Padding.Horizontal, Height - Padding.Vertical);

      int charactersFitted;
      int linesFilled;

      Graphics g = report.MeasureGraphics;
      GraphicsState state = g.Save();
      try
      {
        if (report.TextQuality != TextQuality.Default)
          g.TextRenderingHint = GetTextQuality(report.TextQuality);

        AdvancedTextRenderer.StyleDescriptor htmlStyle = null;
        if (IsAdvancedRendererNeeded)
        {
          AdvancedTextRenderer renderer = new AdvancedTextRenderer(Text, g, font, Brushes.Black, Pens.Black,
            textRect, format, HorzAlign, VertAlign, LineHeight, Angle, FontWidthRatio, false, Wysiwyg, HtmlTags, false, 96f / DrawUtils.ScreenDpi
            , InlineImageCache);
          renderer.CalcHeight(out charactersFitted, out htmlStyle);
            
          if (charactersFitted == 0)
            linesFilled = 0;
          else
            linesFilled = 2;
        }
        else
        {
          g.MeasureString(Text, font, textRect.Size, format, out charactersFitted, out linesFilled);
        }
        
        if (linesFilled == 0)
          return null;
        if (linesFilled == 1)
        {
          // check if there is enough space for one line
          float lineHeight = g.MeasureString("Wg", font).Height;
          if (textRect.Height < lineHeight)
            return null;
        }

        if (charactersFitted < Text.Length)
          result = Text.Substring(charactersFitted);
        else
          result = "";
        
        Text = Text.Substring(0, charactersFitted);
        
        if (HorzAlign == HorzAlign.Justify && !Text.EndsWith("\n") && result != "")
        {
          if (Text.EndsWith(" "))
            Text = Text.Substring(0, Text.Length - 1);
          ForceJustify = true;
        }  
        if (HtmlTags && htmlStyle != null && result != "")
          result = htmlStyle.ToString() + result;  
      }
      finally
      {
        g.Restore(state);
      }

      return result;
    }

    private void ProcessAutoShrink()
    {
      if (String.IsNullOrEmpty(Text))
        return;

      if (AutoShrink == AutoShrinkMode.FontSize)
      {
        while (CalcWidth() > Width - 1 && Font.Size > AutoShrinkMinSize)
        {
          Font = new Font(Font.Name, Font.Size - 1, Font.Style);
        }
      }
      else if (AutoShrink == AutoShrinkMode.FontWidth)
      {
        FontWidthRatio = 1;
        float ratio = Converter.DecreasePrecision((Width - 1) / CalcWidth(), 2) - 0.01f;
        if (ratio < 1)
          FontWidthRatio = Math.Max(ratio, AutoShrinkMinSize);
      }
    }

    private string MakeParagraphOffset(string text)
    {
      FirstTabOffset = ParagraphOffset;
      string[] lines = text.Split(new char[] { '\n' });
      for (int i = 0; i < lines.Length; i++)
      {
        if (!lines[i].StartsWith("\t"))
          lines[i] = "\t" + lines[i];
      }
      return String.Join("\n", lines);
    }


    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override void DeserializeSubItems(FRReader reader)
    {
      if (String.Compare(reader.ItemName, "Highlight", true) == 0)
        reader.Read(Highlight);
      else
        base.DeserializeSubItems(reader);
    }
    #endregion

    #region Public Methods
    internal StringFormat GetStringFormat(GraphicCache cache, StringFormatFlags flags)
    {
      return GetStringFormat(cache, flags, 1);
    }

    internal StringFormat GetStringFormat(GraphicCache cache, StringFormatFlags flags, float scale)
    {
      StringAlignment align = StringAlignment.Near;
      if (HorzAlign == HorzAlign.Center)
        align = StringAlignment.Center;
      else if (HorzAlign == HorzAlign.Right)
        align = StringAlignment.Far;

      StringAlignment lineAlign = StringAlignment.Near;
      if (VertAlign == VertAlign.Center)
        lineAlign = StringAlignment.Center;
      else if (VertAlign == VertAlign.Bottom)
        lineAlign = StringAlignment.Far;

      if (RightToLeft)
        flags |= StringFormatFlags.DirectionRightToLeft;
      if (!WordWrap)
        flags |= StringFormatFlags.NoWrap;
      if (!Clip)
        flags |= StringFormatFlags.NoClip;

      return cache.GetStringFormat(align, lineAlign, Trimming, flags, FFirstTabOffset * scale, FTabWidth * scale);
    }

    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);

      TextObject src = source as TextObject;
      AutoWidth = src.AutoWidth;
      HorzAlign = src.HorzAlign;
      VertAlign = src.VertAlign;
      Angle = src.Angle;
      RightToLeft = src.RightToLeft;
      WordWrap = src.WordWrap;
      Underlines = src.Underlines;
      Font = src.Font;
      TextFill = src.TextFill.Clone();
      TextOutline.Assign(src.TextOutline);
      Trimming = src.Trimming;
      FontWidthRatio = src.FontWidthRatio;
      FirstTabOffset = src.FirstTabOffset;
      TabWidth = src.TabWidth;
      Clip = src.Clip;
      Highlight.Assign(src.Highlight);
      Wysiwyg = src.Wysiwyg;
      LineHeight = src.LineHeight;
      HtmlTags = src.HtmlTags;
      AutoShrink = src.AutoShrink;
      AutoShrinkMinSize = src.AutoShrinkMinSize;
      ParagraphOffset = src.ParagraphOffset;
      FInlineImageCache = src.FInlineImageCache;
    }

    /// <inheritdoc/>
    public override void AssignFormat(ReportComponentBase source)
    {
      base.AssignFormat(source);

      TextObject src = source as TextObject;
      if (src == null)
        return;

      HorzAlign = src.HorzAlign;
      VertAlign = src.VertAlign;
      Angle = src.Angle;
      Underlines = src.Underlines;
      Font = src.Font;
      TextFill = src.TextFill.Clone();
      TextOutline.Assign(src.TextOutline);
      Trimming = src.Trimming;
      FontWidthRatio = src.FontWidthRatio;
      FirstTabOffset = src.FirstTabOffset;
      TabWidth = src.TabWidth;
      Clip = src.Clip;
      Wysiwyg = src.Wysiwyg;
      LineHeight = src.LineHeight;
      HtmlTags = src.HtmlTags;
      ParagraphOffset = src.ParagraphOffset;
    }

    /// <summary>
    /// Draws a text.
    /// </summary>
    /// <param name="e">Paint event data.</param>
    public void DrawText(FRPaintEventArgs e)
    {
      string text = Text;
      if (!String.IsNullOrEmpty(text))
      {
        Graphics g = e.Graphics;
        RectangleF textRect = new RectangleF(
          (AbsLeft + Padding.Left) * e.ScaleX,
          (AbsTop + Padding.Top) * e.ScaleY,
          (Width - Padding.Horizontal) * e.ScaleX,
          (Height - Padding.Vertical) * e.ScaleY);

        if (ParagraphOffset != 0 && IsDesigning)
          text = MakeParagraphOffset(text);
        StringFormat format = GetStringFormat(e.Cache, 0, e.ScaleX);

        Font font = e.Cache.GetFont(Font.Name,
          IsPrinting ? Font.Size : Font.Size * e.ScaleX * 96f / DrawUtils.ScreenDpi,
          Font.Style);

        Brush textBrush = null;
        if (TextFill is SolidFill)
          textBrush = e.Cache.GetBrush((TextFill as SolidFill).Color);
        else
          textBrush = TextFill.CreateBrush(textRect);

        Pen outlinePen = null;
        if (FTextOutline.Enabled)
        {
            outlinePen = e.Cache.GetPen(FTextOutline.Color, FTextOutline.Width * e.ScaleX, FTextOutline.Style);

        }

        Report report = Report;
        if (report != null && report.TextQuality != TextQuality.Default)
          g.TextRenderingHint = GetTextQuality(report.TextQuality);

        if (textRect.Width > 0 && textRect.Height > 0)
        {
          if (IsAdvancedRendererNeeded)
          {
            // use advanced rendering
            AdvancedTextRenderer renderer = new AdvancedTextRenderer(text, g, font, textBrush, outlinePen,
              textRect, format, HorzAlign, VertAlign, LineHeight * e.ScaleY, Angle, FontWidthRatio,
              ForceJustify, Wysiwyg, HtmlTags, false, e.ScaleX * 96f / DrawUtils.ScreenDpi
              , InlineImageCache);
            renderer.Draw();
          }
          else
          {
            // use simple rendering
              if (Angle == 0 && FontWidthRatio == 1)
              {
                            if (outlinePen == null)
                            {
                                g.DrawString(text, font, textBrush, textRect, format);
                            }
                            else
                            {

                                GraphicsPath path = new GraphicsPath();
                                path.AddString(text, font.FontFamily, Convert.ToInt32(font.Style), g.DpiY * font.Size / 72, textRect, format);

                                GraphicsState state = g.Save();
                                g.SetClip(textRect);
                                //g.FillPath(textBrush, path);

                                //regime for text output with drawbehind 
                                if (TextOutline.DrawBehind)
                                {
                                    g.DrawPath(outlinePen, path);
                                    g.FillPath(textBrush, path);
                                }
                                else //without. default
                                {
                                    g.FillPath(textBrush, path);
                                    g.DrawPath(outlinePen, path);
                                }
                                g.Restore(state);


                            }
                        }
              else
                  StandardTextRenderer.Draw(text, g, font, textBrush, outlinePen, textRect, format, Angle, FontWidthRatio);
          }
        }

                if (!(TextFill is SolidFill))
                {
                    textBrush.Dispose();
                    textBrush = null;
                }
        if (report != null && report.TextQuality != TextQuality.Default)
          g.TextRenderingHint = TextRenderingHint.SystemDefault;
      }
      
      DrawUnderlines(e);
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      base.Draw(e);
      DrawText(e);
      DrawMarkers(e);
      Border.Draw(e, new RectangleF(AbsLeft, AbsTop, Width, Height));

      if (FDragAccept)
        DrawDragAcceptFrame(e, Color.Silver);
    }

    /// <inheritdoc/>
    public override void HandleDragOver(FRMouseEventArgs e)
    {
      if (PointInObject(new PointF(e.X, e.Y)) && e.DragSource is TextObject)
        e.Handled = true;
      FDragAccept = e.Handled;
    }

    /// <inheritdoc/>
    public override void HandleDragDrop(FRMouseEventArgs e)
    {
      Text = (e.DragSource as TextObject).Text;
      FDragAccept = false;
    }

    /// <inheritdoc/>
    public override void HandleKeyDown(Control sender, KeyEventArgs e)
    {
      if (IsSelected && e.KeyCode == Keys.Enter && HasFlag(Flags.CanEdit) && !HasRestriction(Restrictions.DontEdit))
      {
        FTextBox = new TextBox();
        FTextBox.Font = new Font(Font.Name, Font.Size * ReportWorkspace.Scale, Font.Style);
        FTextBox.BorderStyle = BorderStyle.None;
        FTextBox.Multiline = true;
        FTextBox.AcceptsTab = true;
        if (Fill is SolidFill)
          FTextBox.BackColor = Color.FromArgb(255, (Fill as SolidFill).Color);
        if (TextFill is SolidFill)
          FTextBox.ForeColor = Color.FromArgb(255, (TextFill as SolidFill).Color);

        FTextBox.Text = Text;
        FTextBox.KeyDown += new KeyEventHandler(FTextBox_KeyDown);
        UpdateEditorPosition();
        sender.Controls.Add(FTextBox);
        FTextBox.SelectAll();
        FTextBox.Focus();
        e.Handled = true;
      }
    }

    /// <inheritdoc/>
    public override void SelectionChanged()
    {
      FinishEdit(true);
    }

    /// <inheritdoc/>
    public override void ApplyStyle(Style style)
    {
      if (style.ApplyTextFill)
        TextFill = style.TextFill.Clone();
      if (style.ApplyFont)
        Font = style.Font;
      base.ApplyStyle(style);
    }

    /// <inheritdoc/>
    public override void SaveStyle()
    {
      base.SaveStyle();
      FSavedTextFill = TextFill;
      FSavedFont = Font;
    }

    /// <inheritdoc/>
    public override void RestoreStyle()
    {
      base.RestoreStyle();
      TextFill = FSavedTextFill;
      Font = FSavedFont;
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      if (writer.SerializeTo == SerializeTo.Preview && AutoWidth)
      {
        WordWrap = false;
        float width = CalcSize().Width;
        if ((Anchor & AnchorStyles.Right) != 0)
          Left = Right - width;
        Width = width;
      }
      TextObject c = writer.DiffObject as TextObject;
      base.Serialize(writer);

      if (AutoWidth != c.AutoWidth)
        writer.WriteBool("AutoWidth", AutoWidth);
      if (AutoShrink != c.AutoShrink)
        writer.WriteValue("AutoShrink", AutoShrink);
      if (FloatDiff(AutoShrinkMinSize, c.AutoShrinkMinSize))
        writer.WriteFloat("AutoShrinkMinSize", AutoShrinkMinSize);
      if (HorzAlign != c.HorzAlign)
        writer.WriteValue("HorzAlign", HorzAlign);
      if (VertAlign != c.VertAlign)
        writer.WriteValue("VertAlign", VertAlign);
      if (Angle != c.Angle)
        writer.WriteInt("Angle", Angle);
      if (RightToLeft != c.RightToLeft)
        writer.WriteBool("RightToLeft", RightToLeft);
      if (WordWrap != c.WordWrap)
        writer.WriteBool("WordWrap", WordWrap);
      if (Underlines != c.Underlines)
        writer.WriteBool("Underlines", Underlines);
      if (!Font.Equals(c.Font))
        writer.WriteValue("Font", Font);
      TextFill.Serialize(writer, "TextFill", c.TextFill);
      if (TextOutline != null)
        TextOutline.Serialize(writer, "TextOutline", c.TextOutline);
      if (Trimming != c.Trimming)
        writer.WriteValue("Trimming", Trimming);
      if (FontWidthRatio != c.FontWidthRatio)
        writer.WriteFloat("FontWidthRatio", FontWidthRatio);
      if (FirstTabOffset != c.FirstTabOffset)
        writer.WriteFloat("FirstTabOffset", FirstTabOffset);
      if (TabWidth != c.TabWidth)
        writer.WriteFloat("TabWidth", TabWidth);
      if (Clip != c.Clip)
        writer.WriteBool("Clip", Clip);
      if (Wysiwyg != c.Wysiwyg)
        writer.WriteBool("Wysiwyg", Wysiwyg);
      if (LineHeight != c.LineHeight)
        writer.WriteFloat("LineHeight", LineHeight);
      if (HtmlTags != c.HtmlTags)
        writer.WriteBool("HtmlTags", HtmlTags);
      if (ParagraphOffset != c.ParagraphOffset)
        writer.WriteFloat("ParagraphOffset", ParagraphOffset);
      if (ForceJustify != c.ForceJustify)
        writer.WriteBool("ForceJustify", ForceJustify);
      if (writer.SerializeTo != SerializeTo.Preview)
      {
        if (Style != c.Style)
          writer.WriteStr("Style", Style);
        if (Highlight.Count > 0)
          writer.Write(Highlight);
      }

      StringBuilder sb = null;

      if(InlineImageCache != null && writer.BlobStore!=null && HtmlTags == true)
        foreach (InlineImageCache.CacheItem item in InlineImageCache.AllItems())
        {
          
          if (item.Src.StartsWith("data:")) continue;
          if (sb == null)
            sb = new StringBuilder();
          sb.Append(writer.BlobStore.AddOrUpdate(item.Stream, item.Src))
            .Append(',');
        }
      if(sb!=null)
      {
        sb.Length--;
        writer.WriteStr("InlineImageCacheIndexes", sb.ToString());
      }
    }


    /// <inheritdoc/>
    public override void Deserialize(FRReader reader)
    {
      base.Deserialize(reader);

      if (reader.BlobStore != null)
      {
        string indexes =  reader.ReadStr("InlineImageCacheIndexes");
        if (indexes != "null" && !String.IsNullOrEmpty(indexes))
        {
          string[] arr = indexes.Split(',');
          foreach(string index in arr)
          {
            int val = 0;
            if(Int32.TryParse(index,out val))
            {
              if (val >= 0 && val < reader.BlobStore.Count)
              {
                InlineImageCache.CacheItem it = new InlineImageCache.CacheItem();
                it.Set(reader.BlobStore.Get(val));
                InlineImageCache.Set(reader.BlobStore.GetSource(val), it);
              }
            }
          }
        }
      }
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return new TextObjectMenu(Report.Designer);
    }

    /// <inheritdoc/>
    public override SmartTagBase GetSmartTag()
    {
      return new TextObjectSmartTag(this);
    }

    /// <inheritdoc/>
    public virtual bool InvokeEditor()
    {
      using (TextEditorForm form = new TextEditorForm(Report))
      {
        form.ExpressionText = Text;
        form.Brackets = Brackets;
        if (form.ShowDialog() == DialogResult.OK)
        {
          Text = form.ExpressionText;
          return true;
        }
      }
      return false;
    }

    internal virtual void FinishEdit(bool accept)
    {
      if (FTextBox == null)
        return;

      if (FTextBox.Modified && accept)
      {
        Text = FTextBox.Text;
        if (Report != null)
          Report.Designer.SetModified(null, "Change", Name);
      }
      FTextBox.Dispose();
      FTextBox = null;
    }

    internal void ApplyCondition(HighlightCondition c)
    {
      if (c.ApplyBorder)
        Border = c.Border.Clone();
      if (c.ApplyFill)
        Fill = c.Fill.Clone();
      if (c.ApplyTextFill)
        TextFill = c.TextFill.Clone();
      if (c.ApplyFont)
        Font = c.Font;
      if (!c.Visible)
        Visible = false;
    }
    #endregion

    #region Report Engine
    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      List<string> expressions = new List<string>();
      expressions.AddRange(base.GetExpressions());

      if (AllowExpressions && !String.IsNullOrEmpty(Brackets))
      {
        string[] brackets = Brackets.Split(new char[] { ',' });
        // collect expressions found in the text
        expressions.AddRange(CodeUtils.GetExpressions(Text, brackets[0], brackets[1]));
      }

      // add highlight conditions
      foreach (HighlightCondition condition in Highlight)
      {
        expressions.Add(condition.Expression);
      }

      return expressions.ToArray();
    }

    /// <inheritdoc/>
    public override void SaveState()
    {
      base.SaveState();
      FSavedText = Text;
      FSavedTextFill = TextFill;
      FSavedFont = Font;
      FSavedFormat = Format;            
      
    }

    /// <inheritdoc/>
    public override void RestoreState()
    {      
      base.RestoreState();
      Text = FSavedText;
      TextFill = FSavedTextFill;
      Font = FSavedFont;
      Format = FSavedFormat;      
    }

    /// <summary>
    /// Calculates the object's width.
    /// </summary>
    /// <returns>The width, in pixels.</returns>
    public float CalcWidth()
    {
      if (Angle == 90 || Angle == 270)
        return InternalCalcHeight();
      return InternalCalcWidth();  
    }

    /// <inheritdoc/>
    public override float CalcHeight()
    {
      if (Angle == 90 || Angle == 270)
        return InternalCalcWidth();
      return InternalCalcHeight();
    }

    /// <inheritdoc/>
    public override void GetData()
    {
      base.GetData();

      // process expressions
      if (AllowExpressions)
      {
        if (!String.IsNullOrEmpty(Brackets))
        {
          string[] brackets = Brackets.Split(new char[] { ',' });
          FindTextArgs args = new FindTextArgs();
          args.Text = new FastString(Text);
          args.OpenBracket = brackets[0];
          args.CloseBracket = brackets[1];
          int expressionIndex = 0;
          while (args.StartIndex < args.Text.Length)
          {
            string expression = CodeUtils.GetExpression(args, false);
            if (expression == "")
              break;

            string formattedValue = CalcAndFormatExpression(expression, expressionIndex);

            args.Text.Remove(args.StartIndex, args.EndIndex - args.StartIndex);
            args.Text.Insert(args.StartIndex, formattedValue);

            args.StartIndex += formattedValue.Length;
            expressionIndex++;
          }
          Text = args.Text.ToString(); 
        }
      }

      // process highlight
      Variant varValue = new Variant(Value);
      foreach (HighlightCondition condition in Highlight)
      {
        try
        {
          if ((bool)Report.Calc(condition.Expression, varValue) == true)
          {
            ApplyCondition(condition);
            break;
          }
        }
        catch (Exception e)
        {
          throw new Exception(Name + ": " + Res.Get("Messages,ErrorInHighlightCondition") + ": " + condition.Expression, e.InnerException);
        }
      }

      // make paragraph offset
      if (ParagraphOffset != 0)
        Text = MakeParagraphOffset(Text);
      // process AutoShrink
      ProcessAutoShrink();
    }

    /// <inheritdoc/>
    public override bool Break(BreakableComponent breakTo)
    {
      string breakText = BreakText();
      if (breakText != null && breakTo != null)
        (breakTo as TextObject).Text = breakText;
      return breakText != null;
    }

    internal IEnumerable<PictureObject> GetPictureFromHtmlText(AdvancedTextRenderer renderer)
    {
      if (renderer == null)
      {
        using (Bitmap b = new Bitmap(1, 1))
        using (Graphics g = Graphics.FromImage(b))
        {
          RectangleF textRect = new RectangleF(
            (AbsLeft + Padding.Left),
            (AbsTop + Padding.Top),
            (Width - Padding.Horizontal),
            (Height - Padding.Vertical));
          StringFormat format = GetStringFormat(Report.GraphicCache, StringFormatFlags.LineLimit);
          renderer = new AdvancedTextRenderer(Text, g, Font, Brushes.Black, Pens.Black,
              textRect, format, HorzAlign, VertAlign, LineHeight, Angle, FontWidthRatio,
              ForceJustify, Wysiwyg, HtmlTags, false, 1,
              InlineImageCache);
          foreach (PictureObject obj in GetPictureFromHtmlText(renderer))
            yield return obj;
        }
      }
      else
      {
        RectangleF textRect = renderer.DisplayRect;
        foreach (AdvancedTextRenderer.Paragraph paragraph in renderer.Paragraphs)
          foreach (AdvancedTextRenderer.Line line in paragraph.Lines)
            foreach (AdvancedTextRenderer.Word word in line.Words)
              foreach (AdvancedTextRenderer.Run run in word.Runs)
                if (run is AdvancedTextRenderer.RunImage)
                {
                  AdvancedTextRenderer.RunImage runImage = run as AdvancedTextRenderer.RunImage;
                  PictureObject obj = new PictureObject();

                  float left = runImage.Left - textRect.Left;
                  float top = runImage.Top - textRect.Top;
                  float width = 
                    runImage.Left + runImage.Width > textRect.Right ?
                    textRect.Right - (left < 0 ? textRect.Left : runImage.Left ) :
                    (
                      runImage.Left < textRect.Left ?
                      runImage.Left + runImage.Width - textRect.Left :
                      runImage.Width
                    );
                  float height =
                    runImage.Top + runImage.Height > textRect.Bottom ?
                    textRect.Bottom - (top < 0 ? textRect.Top : runImage.Top) :
                    (
                      runImage.Top < textRect.Top ?
                      runImage.Top + runImage.Height - textRect.Top :
                      runImage.Height
                    );

                  
                  if (left < 0
                    ||
                    top < 0
                    ||
                    width < runImage.Width
                    ||
                    height < runImage.Height)
                  {
                    Bitmap bmp = new Bitmap((int)width, (int)height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                      g.DrawImage(runImage.Image, new PointF(
                        left < 0 ? left : 0,
                        top < 0 ? top : 0
                        ));
                    }
                    obj.Image = bmp;
                    obj.Left = (left < 0 ? textRect.Left : runImage.Left) / renderer.Scale;
                    obj.Top = (top < 0 ? textRect.Top : runImage.Top) / renderer.Scale;
                    obj.Width = width / renderer.Scale;
                    obj.Height = height / renderer.Scale;
                    obj.SizeMode = PictureBoxSizeMode.StretchImage;
                  }
                  else
                  {
                    obj.Image = runImage.Image;
                    obj.Left = runImage.Left / renderer.Scale;
                    obj.Top = runImage.Top / renderer.Scale;
                    obj.Width = runImage.Width / renderer.Scale;
                    obj.Height = runImage.Height / renderer.Scale;
                    obj.SizeMode = PictureBoxSizeMode.StretchImage;
                  }
                  yield return obj;
                }
      }
    }
    #endregion

    #region ISearchable Members
    /// <inheritdoc/>
    public override void DrawSearchHighlight(FRPaintEventArgs e, CharacterRange range)
    {
      if (Angle == 0 && FontWidthRatio == 1 && HorzAlign != HorzAlign.Justify)
      {
        Graphics g = e.Graphics;
        Font font = e.Cache.GetFont(Font.Name, Font.Size * e.ScaleX * 96f / DrawUtils.ScreenDpi, Font.Style);
        StringFormat format = GetStringFormat(e.Cache, 0);

        RectangleF textRect = new RectangleF(
          (AbsLeft + Padding.Left) * e.ScaleX,
          (AbsTop + Padding.Top) * e.ScaleY,
          (Width - Padding.Horizontal) * e.ScaleX,
          (Height - Padding.Vertical) * e.ScaleY);

        RectangleF rangeRect;
        if (Angle == 0 && FontWidthRatio == 1 && HorzAlign != HorzAlign.Justify)
        {
          format.SetMeasurableCharacterRanges(new CharacterRange[] { range });
          Region[] regions = g.MeasureCharacterRanges(Text, font, textRect, format);
          rangeRect = regions[0].GetBounds(g);
          regions[0].Dispose();
                    regions[0] = null;
        }
        else
          rangeRect = new RectangleF(AbsLeft * e.ScaleX, AbsTop * e.ScaleY, Width * e.ScaleX, Height * e.ScaleY);

        using (Brush brush = new SolidBrush(Color.FromArgb(128, SystemColors.Highlight)))
        {
          g.FillRectangle(brush, rangeRect);
        }
      }
      else
        base.DrawSearchHighlight(e, range);  
    }
        #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="TextObject"/> class with default settings.
    /// </summary>
    public TextObject()
    {
      FWordWrap = true;
      FFont = DrawUtils.DefaultReportFont; 
      FTextFill = new SolidFill(Color.Black);
      FTextOutline = new TextOutline();
      FTrimming = StringTrimming.None;
      FFontWidthRatio = 1;
      FTabWidth = 58;
      FClip = true;
      FHighlight = new ConditionCollection();
      FlagSerializeStyle = false;
      SetFlags(Flags.HasSmartTag, true);
    }
  }
}