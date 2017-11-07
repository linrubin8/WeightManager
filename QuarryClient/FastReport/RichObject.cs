using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using FastReport.TypeEditors;
using FastReport.Utils;
using FastReport.Code;
using FastReport.Controls;
using FastReport.Forms;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// Represents a RichText object that can display formatted text.
  /// </summary>
  /// <remarks>
  /// Use the <see cref="Text"/> property to set the object's text. The text may include
  /// the RTF formatting tags.
  /// </remarks>
  public 
  class RichObject : TextObjectBase, IHasEditor
  {
    #region Fields
    private string FDataColumn;
    private int FActualTextStart;
    private int FActualTextLength;
    private string FSavedText;
    private string FSavedDataColumn;
    private Metafile FCachedMetafile;
    private bool FOldBreakStyle;
    #endregion
    
    #region Properties
    /// <summary>
    /// Gets or sets the object's text.
    /// </summary>
    /// <remarks>
    /// This property returns the formatted text with rtf tags.
    /// </remarks>
    [Category("Data")]
    public override string Text
    {
      get { return base.Text; }
      set 
      {
        base.Text = value;
        FDataColumn = "";
        DestroyCachedMetafile();
      }
    }

    /// <summary>
    /// Gets or sets a name of the data column bound to this control.
    /// </summary>
    /// <remarks>
    /// Value must contain the datasource name, for example: "Datasource.Column".
    /// </remarks>
    [Editor(typeof(DataColumnEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string DataColumn
    {
      get { return FDataColumn; }
      set 
      {
        if (!String.IsNullOrEmpty(value))
        {
          if (!String.IsNullOrEmpty(Brackets))
          {
            string[] brackets = Brackets.Split(new char[] { ',' });
            Text = brackets[0] + value + brackets[1];
          }
        }
        FDataColumn = value;
      }
    }

    /// <summary>
    /// Gets the actual text start.
    /// </summary>
    /// <remarks>
    /// This property is for internal use only; you should not use it in your code.
    /// </remarks>
    [Browsable(false)]
    public int ActualTextStart
    {
      get { return FActualTextStart; }
      set { FActualTextStart = value; }
    }

    /// <summary>
    /// Gets the actual text length.
    /// </summary>
    /// <remarks>
    /// This property is for internal use only; you should not use it in your code.
    /// </remarks>
    [Browsable(false)]
    public int ActualTextLength
    {
      get { return FActualTextLength; }
      set { FActualTextLength = value; }
    }

    /// <summary>
    /// Gets or sets the break style.
    /// </summary>
    /// <remarks> 
    /// Set this property to true if you want editable rich text when you edit the prepared report page.
    /// </remarks>
    public bool OldBreakStyle
    {
      get { return FOldBreakStyle; }
      set { FOldBreakStyle = value; }
    }
    #endregion
    
    #region Private Methods
    private FRRichTextBox CreateRich()
    {
      FRRichTextBox rich = new FRRichTextBox();
      
      if (Text != null && Text.StartsWith(@"{\rtf"))
        rich.Rtf = Text;
      else
        rich.Text = Text;
      
      Color color = Color.White;
      if (Fill is SolidFill)
        color = (Fill as SolidFill).Color;
      if (color == Color.Transparent)
        color = Color.White;
      rich.BackColor = color;

      rich.DetectUrls = false;
      
      return rich;
    }

    private Metafile CreateMetafile(FRPaintEventArgs e)
    {
      Graphics measureGraphics = Report == null ? e.Graphics : Report.PrintSettings.MeasureGraphics;
      if (measureGraphics == null)
        measureGraphics = e.Graphics;
      
      float scaleX = measureGraphics.DpiX / 96f;
      float scaleY = measureGraphics.DpiY / 96f;
      IntPtr hdc = measureGraphics.GetHdc();
      Metafile emf = new Metafile(hdc,
        new RectangleF(0, 0, (Width - Padding.Horizontal) * scaleX, (Height - Padding.Vertical) * scaleY),
        MetafileFrameUnit.Pixel);
      measureGraphics.ReleaseHdc(hdc);

      // create metafile canvas and draw on it
      using (Graphics g = Graphics.FromImage(emf))
      using (FRRichTextBox rich = CreateRich())
      {
        int textStart = ActualTextStart;
        int textLength = ActualTextLength != 0 ? ActualTextLength : rich.TextLength - textStart;
        rich.FormatRange(g, measureGraphics,
          new RectangleF(0, 0, Width - Padding.Horizontal, Height - Padding.Vertical),
          textStart, textStart + textLength, false);
      }

      return emf;
    }

    private void DestroyCachedMetafile()
    {
      if (FCachedMetafile != null)
      {
        FCachedMetafile.Dispose();
        FCachedMetafile = null;
      }
    }

    private void DrawRich(FRPaintEventArgs e)
    {
      // avoid GDI+ errors
      if (Width < Padding.Horizontal + 1 || Height < Padding.Vertical + 1)
        return;
      
      // draw to emf because we need to zoom the image
      if (FCachedMetafile == null)
        FCachedMetafile = CreateMetafile(e);
      
      e.Graphics.DrawImage(FCachedMetafile, 
        new RectangleF((AbsLeft + Padding.Left) * e.ScaleX,
        (AbsTop + Padding.Top) * e.ScaleY,
        (Width - Padding.Horizontal) * e.ScaleX,
        (Height - Padding.Vertical) * e.ScaleY));

      if (IsDesigning)
        DestroyCachedMetafile();
    }

    private void PrintRich(FRPaintEventArgs e)
    {
      // FormatRange method uses GDI and does not respect transform settings of GDI+.
      RectangleF textRect = new RectangleF(
        (AbsLeft + Padding.Left) + e.Graphics.Transform.OffsetX / e.ScaleX,
        (AbsTop + Padding.Top) + e.Graphics.Transform.OffsetY / e.ScaleY,
        (Width - Padding.Horizontal),
        (Height - Padding.Vertical));

      Graphics measureGraphics = Report == null ? e.Graphics : Report.PrintSettings.MeasureGraphics;
      if (measureGraphics == null)
        measureGraphics = e.Graphics;

      using (FRRichTextBox rich = CreateRich())
      {
        int textStart = ActualTextStart;
        int textLength = ActualTextLength != 0 ? ActualTextLength : rich.TextLength - textStart;
        rich.FormatRange(e.Graphics, measureGraphics, textRect, textStart, textStart + textLength, false);
      }
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
        DestroyCachedMetafile();
      base.Dispose(disposing);
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      
      RichObject src = source as RichObject;
      DataColumn = src.DataColumn;
      ActualTextStart = src.ActualTextStart;
      ActualTextLength = src.ActualTextLength;
      OldBreakStyle = src.OldBreakStyle;
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      base.Draw(e);
      try
      {
        if (IsPrinting)
          PrintRich(e);
        else
          DrawRich(e);
      }
      catch (Exception ex)
      {
        e.Graphics.DrawString(ex.ToString(), DrawUtils.DefaultReportFont, Brushes.Red, 
          new RectangleF(AbsLeft * e.ScaleX, AbsTop * e.ScaleY, Width * e.ScaleX, Height * e.ScaleY));
      }
      DrawMarkers(e);
      Border.Draw(e, new RectangleF(AbsLeft, AbsTop, Width, Height));
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      RichObject c = writer.DiffObject as RichObject;
      base.Serialize(writer);

      if (ActualTextStart != c.ActualTextStart)
        writer.WriteInt("ActualTextStart", ActualTextStart);
      if (ActualTextLength != c.ActualTextLength)
        writer.WriteInt("ActualTextLength", ActualTextLength);
      if (writer.SerializeTo != SerializeTo.Preview)
      {
        if (DataColumn != c.DataColumn)
          writer.WriteStr("DataColumn", DataColumn);
      }
      if (OldBreakStyle != c.OldBreakStyle)
        writer.WriteBool("OldBreakStyle", OldBreakStyle);
    }

    /// <summary>
    /// Invokes object's editor.
    /// </summary>
    /// <returns></returns>
    public bool InvokeEditor()
    {
      using (RichEditorForm form = new RichEditorForm(this))
      {
        if (form.ShowDialog() == DialogResult.OK)
        {
          FActualTextStart = 0;
          FActualTextLength = 0;
          return true;
        }
      }
      
      return false;
    }

    /// <inheritdoc/>
    public override SmartTagBase GetSmartTag()
    {
      return new RichObjectSmartTag(this);
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return new TextObjectBaseMenu(Report.Designer);
    }
    #endregion

    #region Report Engine
    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      List<string> expressions = new List<string>();
      expressions.AddRange(base.GetExpressions());

      if (!String.IsNullOrEmpty(Brackets))
      {
        // collect expressions found in the text
        string[] brackets = Brackets.Split(new char[] { ',' });
        using (FRRichTextBox rich = CreateRich())
        {
          expressions.AddRange(CodeUtils.GetExpressions(rich.Text, brackets[0], brackets[1]));
        }
      }

      if (!String.IsNullOrEmpty(DataColumn))
        expressions.Add(DataColumn);
      return expressions.ToArray();
    }

    /// <inheritdoc/>
    public override void SaveState()
    {
      base.SaveState();
      FSavedText = Text;
      FSavedDataColumn = DataColumn;
    }

    /// <inheritdoc/>
    public override void RestoreState()
    {
      base.RestoreState();
      Text = FSavedText;
      FDataColumn = FSavedDataColumn;
    }

    /// <inheritdoc/>
    public override void GetData()
    {
      base.GetData();
      if (!String.IsNullOrEmpty(DataColumn))
      {
        object value = Report.GetColumnValue(DataColumn);
        if (value is byte[])
        {
          using (MemoryStream stream = new MemoryStream((byte[])value))
          using (FRRichTextBox rich = new FRRichTextBox())
          {
            rich.LoadFile(stream, RichTextBoxStreamType.RichText);
            Text = rich.Rtf;
          }
        }
        else
        {
          Text = value == null ? "" : value.ToString();
        }
      }
      else if (AllowExpressions)
      {
        // process expressions
        if (!String.IsNullOrEmpty(Brackets))
        {
          using (FRRichTextBox rich = CreateRich())
          {
            string[] brackets = Brackets.Split(new char[] { ',' });
            FindTextArgs args = new FindTextArgs();
            args.Text = new FastString(rich.Text);
            args.OpenBracket = brackets[0];
            args.CloseBracket = brackets[1];
            args.StartIndex = ActualTextStart;
            int expressionIndex = 0;

            while (args.StartIndex < args.Text.Length)
            {
              string expression = CodeUtils.GetExpression(args, false);
              if (expression == "")
                break;

              string formattedValue = CalcAndFormatExpression(expression, expressionIndex);
              // strip off the "\r" characters since rich uses only "\n" for new line
              formattedValue = formattedValue.Replace("\r", "");

              args.Text = args.Text.Remove(args.StartIndex, args.EndIndex - args.StartIndex);
              args.Text = args.Text.Insert(args.StartIndex, formattedValue);

              // fix for win8.1 and later
              // because their Selection* properties also includes control symbols
              {
                /*rich.SelectionStart = args.StartIndex;
                rich.SelectionLength = args.EndIndex - args.StartIndex;
                rich.SelectedText = formattedValue;*/

                int richIndex = rich.Find(args.OpenBracket + expression + args.CloseBracket, args.StartIndex, RichTextBoxFinds.None);
                if (richIndex != -1)
                {
                  rich.SelectedText = formattedValue;
                }
              }

              args.StartIndex += formattedValue.Length;
              expressionIndex++;
            }

            Text = rich.Rtf;
          }
        }
      }
    }

    /// <inheritdoc/>
    public override float CalcHeight()
    {
      using (FRRichTextBox rich = CreateRich())
      {
        int textStart = ActualTextStart;
        int textLength = ActualTextLength != 0 ? ActualTextLength : rich.TextLength - textStart;
        return SelectionHeight(rich, textStart, textLength);
      }
    }
    
    private int SelectionHeight(FRRichTextBox rich, int start, int length)
    {
      using (Graphics g = rich.CreateGraphics())
      {
        int n1 = 0;
        int n2 = 100000;
        Graphics measureGraphics = Report == null ? g : Report.PrintSettings.MeasureGraphics;
        if (measureGraphics == null)
          measureGraphics = g;

        // find the height using halfway point
        for (int i = 0; i < 20; i++)
        {
          int mid = (n1 + n2) / 2;

          RectangleF textRect = new RectangleF(0, 0, Width - Padding.Horizontal, mid);
          int fit = rich.FormatRange(g, measureGraphics, textRect, start, start + length, true) - start;

          if (fit >= length)
            n2 = mid;
          else
            n1 = mid;

          if (Math.Abs(n1 - n2) < 2)
            break;
        }

        int height = Math.Max(n1, n2);
        // workaround bug in richtext control (one-line text returns 0 height)
        if (rich.TextLength > 0 && height <= 2)
        {
          RectangleF textRect = new RectangleF(0, 0, Width - Padding.Horizontal, 1000000);
          rich.FormatRange(g, measureGraphics, textRect, start, start + length, true, out height);
        }
        return height + Padding.Vertical;
      }
    }

    /// <inheritdoc/>
    public override bool Break(BreakableComponent breakTo)
    {
      using (FRRichTextBox rich = CreateRich())
      using (Graphics g = rich.CreateGraphics())
      {
        // determine number of characters fit in the bounds. Set less height to prevent possible data loss.
        RectangleF textRect = new RectangleF(0, 0, Width - Padding.Horizontal, Height - Padding.Vertical - 20);
        Graphics measureGraphics = Report == null ? g : Report.PrintSettings.MeasureGraphics;
        if (measureGraphics == null)
          measureGraphics = g;

        // prevent page break when height is <= 0
        if (textRect.Height <= 0)
          return false;

        int textStart = ActualTextStart;
        int textLength = ActualTextLength != 0 ? ActualTextLength : rich.TextLength - textStart;
        int charsFit = rich.FormatRange(g, measureGraphics, textRect, textStart, textStart + textLength, true) - textStart;
        
        if (charsFit <= 0 || charsFit == textLength + 1)
          return false;
          
        // perform break
        if (breakTo != null)
        {
          RichObject richTo = breakTo as RichObject;

          if (OldBreakStyle)
          {
            // copy out-of-bounds rtf to the breakTo
            rich.SelectionStart = charsFit;
            rich.SelectionLength = rich.TextLength - charsFit;
            richTo.Text = rich.SelectedRtf;

            // leave text that fit in this object
            rich.SelectedText = "";
            Text = rich.Rtf;
          }
          else
          {
            richTo.Text = Text;
            richTo.ActualTextStart = textStart + charsFit;
            ActualTextLength = charsFit;
          }
        }

        return true;
      }
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="RichObject"/> class with default settings.
    /// </summary>
    public RichObject()
    {
      FDataColumn = "";
      SetFlags(Flags.HasSmartTag, true);
    }
  }
}
