using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.Design.PageDesigners.Page;
using FastReport.Table;
using FastReport.TypeConverters;

namespace FastReport
{
  /// <summary>
  /// Represents a text object which draws each symbol of text in its own cell.
  /// </summary>
  /// <remarks>
  /// <para/>The text may be aligned to left or right side, or centered. Use the <see cref="HorzAlign"/>
  /// property to do this. The "justify" align is not supported now, as well as vertical alignment.
  /// <para/>The cell size is defined in the <see cref="CellWidth"/> and <see cref="CellHeight"/> properties.
  /// These properties are 0 by default, in this case the size of cell is calculated automatically based
  /// on the object's <b>Font</b>.
  /// <para/>To define a spacing (gap) between cells, use the <see cref="HorzSpacing"/> and
  /// <see cref="VertSpacing"/> properties.
  /// </remarks>
  public class CellularTextObject : TextObject
  {
    #region Fields
    private float FCellWidth;
    private float FCellHeight;
    private float FHorzSpacing;
    private float FVertSpacing;
    #endregion
    
    #region Properties
    /// <summary>
    /// Gets or sets the width of cell, in pixels.
    /// </summary>
    /// <remarks>
    /// If zero width and/or height specified, the object will calculate the cell size
    /// automatically based on its font.
    /// </remarks>
    [Category("Appearance")]
    [TypeConverter(typeof(UnitsConverter))]
    public float CellWidth
    {
      get { return FCellWidth; }
      set { FCellWidth = value; }
    }

    /// <summary>
    /// Gets or sets the height of cell, in pixels.
    /// </summary>
    /// <remarks>
    /// If zero width and/or height specified, the object will calculate the cell size
    /// automatically based on its font.
    /// </remarks>
    [Category("Appearance")]
    [TypeConverter(typeof(UnitsConverter))]
    public float CellHeight
    {
      get { return FCellHeight; }
      set { FCellHeight = value; }
    }

    /// <summary>
    /// Gets or sets the horizontal spacing between cells, in pixels.
    /// </summary>
    [Category("Appearance")]
    [TypeConverter(typeof(UnitsConverter))]
    public float HorzSpacing
    {
      get { return FHorzSpacing; }
      set { FHorzSpacing = value; }
    }

    /// <summary>
    /// Gets or sets the vertical spacing between cells, in pixels.
    /// </summary>
    [Category("Appearance")]
    [TypeConverter(typeof(UnitsConverter))]
    public float VertSpacing
    {
      get { return FVertSpacing; }
      set { FVertSpacing = value; }
    }
    #endregion

    #region Property hiding
    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new bool AutoWidth
    {
      get { return base.AutoWidth; }
      set { base.AutoWidth = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new AutoShrinkMode AutoShrink
    {
      get { return base.AutoShrink; }
      set { base.AutoShrink = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new float AutoShrinkMinSize
    {
      get { return base.AutoShrinkMinSize; }
      set { base.AutoShrinkMinSize = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new int Angle
    {
      get { return base.Angle; }
      set { base.Angle = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new bool Underlines
    {
      get { return base.Underlines; }
      set { base.Underlines = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new bool RightToLeft
    {
      get { return base.RightToLeft; }
      set { base.RightToLeft = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new StringTrimming Trimming
    {
      get { return base.Trimming; }
      set { base.Trimming = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new float FontWidthRatio
    {
      get { return base.FontWidthRatio; }
      set { base.FontWidthRatio = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new float LineHeight
    {
      get { return base.LineHeight; }
      set { base.LineHeight = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new float FirstTabOffset
    {
      get { return base.FirstTabOffset; }
      set { base.FirstTabOffset = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new float TabWidth
    {
      get { return base.TabWidth; }
      set { base.TabWidth = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new bool Clip
    {
      get { return base.Clip; }
      set { base.Clip = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new bool Wysiwyg
    {
      get { return base.Wysiwyg; }
      set { base.Wysiwyg = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new bool ForceJustify
    {
      get { return base.ForceJustify; }
      set { base.ForceJustify = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new bool HtmlTags
    {
      get { return base.HtmlTags; }
      set { base.HtmlTags = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new Padding Padding
    {
      get { return base.Padding; }
      set { base.Padding = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new bool CanBreak
    {
      get { return base.CanBreak; }
      set { base.CanBreak = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public new BreakableComponent BreakTo
    {
      get { return base.BreakTo; }
      set { base.BreakTo = value; }
    }
    #endregion
    
    #region Private Methods
    // use the TableObject to represent the contents. It's easier to export it later.
    private TableObject GetTable(bool autoRows)
    {
      TableObject table = new TableObject();
      table.SetPrinting(IsPrinting);
      table.SetReport(Report);

      float cellWidth = CellWidth;
      float cellHeight = CellHeight;
      // calculate cellWidth, cellHeight automatically
      if (cellWidth == 0 || cellHeight == 0)
      {
        float fontHeight = Font.GetHeight() * 96f / DrawUtils.ScreenDpi;
        cellWidth = (int)Math.Round((fontHeight + 10) / Page.SnapSize.Width) * Page.SnapSize.Width;
        cellHeight = cellWidth;
      }

      int colCount = (int)((Width + HorzSpacing + 1) / (cellWidth + HorzSpacing));
      if (colCount == 0)
        colCount = 1;
      int rowCount = (int)((Height + VertSpacing + 1) / (cellHeight + VertSpacing));
      if (rowCount == 0 || autoRows)
        rowCount = 1;

      table.ColumnCount = colCount;
      table.RowCount = rowCount;

      // process the text
      int row = 0;
      int lineBegin = 0;
      int lastSpace = 0;
      string text = Text.Replace("\r\n", "\n");

      for (int i = 0; i < text.Length; i++)
      {
        bool isCRLF = text[i] == '\n';
        if (text[i] == ' ' || isCRLF)
          lastSpace = i;

        if (i - lineBegin + 1 > colCount || isCRLF)
        {
          if (WordWrap && lastSpace > lineBegin)
          {
            AddText(table, row, text.Substring(lineBegin, lastSpace - lineBegin));
            lineBegin = lastSpace + 1;
          }
          else if (i - lineBegin > 0)
          {
            AddText(table, row, text.Substring(lineBegin, i - lineBegin));
            lineBegin = i;
          }
          else
            lineBegin = i + 1;

          lastSpace = lineBegin;
          row++;
          if (autoRows && row >= rowCount)
          {
            rowCount++;
            table.RowCount++;
          }
        }
      }

      // finish the last line
      if (lineBegin < text.Length)
        AddText(table, row, text.Substring(lineBegin, text.Length - lineBegin));

      // set up cells appearance
      for (int i = 0; i < colCount; i++)
      {
        for (int j = 0; j < rowCount; j++)
        {
          TableCell cell = table[i, j];
          cell.Border = Border.Clone();
          cell.Fill = Fill.Clone();
          cell.Font = Font;
          cell.TextFill = TextFill.Clone();
          cell.HorzAlign = HorzAlign.Center;
          cell.VertAlign = VertAlign.Center;
        }
      }

      // set cell's width and height
      for (int i = 0; i < colCount; i++)
      {
        table.Columns[i].Width = cellWidth;
      }

      for (int i = 0; i < rowCount; i++)
      {
        table.Rows[i].Height = cellHeight;
      }

      // insert spacing between cells
      if (HorzSpacing > 0)
      {
        for (int i = 0; i < colCount - 1; i++)
        {
          TableColumn newColumn = new TableColumn();
          newColumn.Width = HorzSpacing;
          table.Columns.Insert(i * 2 + 1, newColumn);
        }
      }

      if (VertSpacing > 0)
      {
        for (int i = 0; i < rowCount - 1; i++)
        {
          TableRow newRow = new TableRow();
          newRow.Height = VertSpacing;
          table.Rows.Insert(i * 2 + 1, newRow);
        }
      }

      table.Left = AbsLeft;
      table.Top = AbsTop;
      table.Width = table.Columns[table.ColumnCount - 1].Right;
      table.Height = table.Rows[table.RowCount - 1].Bottom;
      return table;
    }

    private void AddText(TableObject table, int row, string text)
    {
      if (row >= table.RowCount)
        return;

      text = text.TrimEnd(new char[] { ' ' });
      if (text.Length > table.ColumnCount)
        text = text.Substring(0, table.ColumnCount);

      int offset = 0;
      if (HorzAlign == HorzAlign.Right)
        offset = table.ColumnCount - text.Length;
      else if (HorzAlign == HorzAlign.Center)
        offset = (table.ColumnCount - text.Length) / 2;

      for (int i = 0; i < text.Length; i++)
      {
        table[i + offset, row].Text = text[i].ToString();
      }
    }
    #endregion
    
    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);

      CellularTextObject src = source as CellularTextObject;
      CellWidth = src.CellWidth;
      CellHeight = src.CellHeight;
      HorzSpacing = src.HorzSpacing;
      VertSpacing = src.VertSpacing;
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      CellularTextObject c = writer.DiffObject as CellularTextObject;
      base.Serialize(writer);

      if (FloatDiff(CellWidth, c.CellWidth))
        writer.WriteFloat("CellWidth", CellWidth);
      if (FloatDiff(CellHeight, c.CellHeight))
        writer.WriteFloat("CellHeight", CellHeight);
      if (FloatDiff(HorzSpacing, c.HorzSpacing))
        writer.WriteFloat("HorzSpacing", HorzSpacing);
      if (FloatDiff(VertSpacing, c.VertSpacing))
        writer.WriteFloat("VertSpacing", VertSpacing);
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      using (TableObject table = GetTable())
      {
        table.Draw(e);
      }
    }
        
    internal TableObject GetTable()
    {
      return GetTable(false);
    }
    
    /// <inheritdoc/>
    public override void OnBeforeInsert(int flags)
    {
      base.OnBeforeInsert(flags);
      // to avoid applying last formatting
      Border.Lines = BorderLines.All;
    }

    /// <inheritdoc/>
    public override SizeF GetPreferredSize()
    {
      if ((Page as ReportPage).IsImperialUnitsUsed)
        return new SizeF(Units.Inches * 2.5f, Units.Inches * 0.3f);
      return new SizeF(Units.Centimeters * 6, Units.Centimeters * 0.75f);
    }
    #endregion

    #region Report Engine
    /// <inheritdoc/>
    public override float CalcHeight()
    {
      using (TableObject table = GetTable(true))
      {
        return table.Height;
      }
    }
    #endregion
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CellularTextObject"/> class with the default settings.
    /// </summary>
    public CellularTextObject()
    {
      CanBreak = false;
      Border.Lines = BorderLines.All;
    }
  }
}
