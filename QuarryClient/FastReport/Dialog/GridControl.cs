using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.Data;
using FastReport.TypeConverters;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Dialog
{
  /// <summary>
  /// Displays data in a customizable grid.
  /// Wraps the <see cref="System.Windows.Forms.DataGridView"/> control.
  /// </summary>
  public class GridControl : DialogControl, IParent
  {
    private DataGridView FDataGridView;
    private GridControlColumnCollection FColumns;
    private DataSourceBase FDataSource;

    #region Properties
    /// <summary>
    /// Gets an internal <b>DataGridView</b>.
    /// </summary>
    [Browsable(false)]
    public DataGridView DataGridView
    {
      get { return FDataGridView; }
    }

    /// <summary>
    /// Gets or sets the data source that the DataGridView is displaying data for.
    /// </summary>
    [Category("Data")]
    public DataSourceBase DataSource
    {
      get { return FDataSource; }
      set
      {
        if (FDataSource != value)
        {
          if (FDataSource != null)
            FDataSource.Disposed -= new EventHandler(DataSource_Disposed);
          if (value != null)
            value.Disposed += new EventHandler(DataSource_Disposed);
        }
        FDataSource = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the option to add rows is displayed to the user.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.AllowUserToAddRows"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool AllowUserToAddRows
    {
      get { return DataGridView.AllowUserToAddRows; }
      set { DataGridView.AllowUserToAddRows = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user is allowed to delete rows from the DataGridView.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.AllowUserToDeleteRows"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool AllowUserToDeleteRows
    {
      get { return DataGridView.AllowUserToDeleteRows; }
      set { DataGridView.AllowUserToDeleteRows = value; }
    }

    /// <summary>
    /// Gets or sets the default cell style applied to odd-numbered rows of the DataGridView.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.AlternatingRowsDefaultCellStyle"/> property.
    /// </summary>
    [Category("Appearance")]
    public DataGridViewCellStyle AlternatingRowsDefaultCellStyle
    {
      get { return DataGridView.AlternatingRowsDefaultCellStyle; }
      set { DataGridView.AlternatingRowsDefaultCellStyle = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating how column widths are determined.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.AutoSizeColumnsMode"/> property.
    /// </summary>
    [DefaultValue(DataGridViewAutoSizeColumnsMode.None)]
    [Category("Layout")]
    public DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode
    {
      get { return DataGridView.AutoSizeColumnsMode; }
      set { DataGridView.AutoSizeColumnsMode = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating how row heights are determined.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.AutoSizeRowsMode"/> property.
    /// </summary>
    [DefaultValue(DataGridViewAutoSizeRowsMode.None)]
    [Category("Layout")]
    public DataGridViewAutoSizeRowsMode AutoSizeRowsMode
    {
      get { return DataGridView.AutoSizeRowsMode; }
      set { DataGridView.AutoSizeRowsMode = value; }
    }

    /// <summary>
    /// Gets or sets the background color of the DataGridView.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.BackgroundColor"/> property.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    [Category("Appearance")]
    public Color BackgroundColor
    {
      get { return DataGridView.BackgroundColor; }
      set { DataGridView.BackgroundColor = value; }
    }

    /// <summary>
    /// Gets or sets the border style for the DataGridView.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.BorderStyle"/> property.
    /// </summary>
    [DefaultValue(BorderStyle.FixedSingle)]
    [Category("Appearance")]
    public BorderStyle BorderStyle
    {
      get { return DataGridView.BorderStyle; }
      set { DataGridView.BorderStyle = value; }
    }

    /// <summary>
    /// Gets the cell border style for the DataGridView.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.CellBorderStyle"/> property.
    /// </summary>
    [DefaultValue(DataGridViewCellBorderStyle.Single)]
    [Category("Appearance")]
    public DataGridViewCellBorderStyle CellBorderStyle 
    {
      get { return DataGridView.CellBorderStyle; }
      set { DataGridView.CellBorderStyle = value; }
    }

    /// <summary>
    /// Gets the border style applied to the column headers.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.ColumnHeadersBorderStyle"/> property.
    /// </summary>
    [DefaultValue(DataGridViewHeaderBorderStyle.Raised)]
    [Category("Appearance")]
    public DataGridViewHeaderBorderStyle ColumnHeadersBorderStyle
    {
      get { return DataGridView.ColumnHeadersBorderStyle; }
      set { DataGridView.ColumnHeadersBorderStyle = value; }
    }

    /// <summary>
    /// Gets or sets the default column header style.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.ColumnHeadersDefaultCellStyle"/> property.
    /// </summary>
    [Category("Appearance")]
    public DataGridViewCellStyle ColumnHeadersDefaultCellStyle
    {
      get { return DataGridView.ColumnHeadersDefaultCellStyle; }
      set { DataGridView.ColumnHeadersDefaultCellStyle = value; }
    }

    /// <summary>
    /// Gets or sets the height, in pixels, of the column headers row.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.ColumnHeadersHeight"/> property.
    /// </summary>
    [DefaultValue(18)]
    [Category("Layout")]
    public int ColumnHeadersHeight
    {
      get { return DataGridView.ColumnHeadersHeight; }
      set { DataGridView.ColumnHeadersHeight = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the height of the column headers is adjustable and whether it can be adjusted by the user or is automatically adjusted to fit the contents of the headers.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.ColumnHeadersHeightSizeMode"/> property.
    /// </summary>
    [DefaultValue(DataGridViewColumnHeadersHeightSizeMode.EnableResizing)]
    [Category("Behavior")]
    public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode 
    {
      get { return DataGridView.ColumnHeadersHeightSizeMode; }
      set { DataGridView.ColumnHeadersHeightSizeMode = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the column header row is displayed.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.ColumnHeadersVisible"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    public bool ColumnHeadersVisible
    {
      get { return DataGridView.ColumnHeadersVisible; }
      set { DataGridView.ColumnHeadersVisible = value; }
    }

    /// <summary>
    /// Gets the collection of <see cref="GridControlColumn"/> objects that represents the grid columns.
    /// </summary>
    [Editor(typeof(GridControlColumnsEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public GridControlColumnCollection Columns
    {
      get { return FColumns; }
    }

    /// <summary>
    /// Gets or sets the default cell style to be applied to the cells in the DataGridView if no other cell style properties are set.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.DefaultCellStyle"/> property.
    /// </summary>
    [Category("Appearance")]
    public DataGridViewCellStyle DefaultCellStyle
    {
      get { return DataGridView.DefaultCellStyle; }
      set { DataGridView.DefaultCellStyle = value; }
    }

    /// <summary>
    /// Gets or sets the color of the grid lines separating the cells of the DataGridView.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.GridColor"/> property.
    /// </summary>
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    [Category("Appearance")]
    public Color GridColor
    {
      get { return DataGridView.GridColor; }
      set { DataGridView.GridColor = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user is allowed to select more than one cell, row, or column of the DataGridView at a time.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.MultiSelect"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool MultiSelect
    {
      get { return DataGridView.MultiSelect; }
      set { DataGridView.MultiSelect = value; }
    }

    /// <summary>
    /// Gets a value indicating whether the user can edit the cells of the DataGridView control.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.ReadOnly"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool ReadOnly
    {
      get { return DataGridView.ReadOnly; }
      set { DataGridView.ReadOnly = value; }
    }

    /// <summary>
    /// Gets or sets the border style of the row header cells.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.RowHeadersBorderStyle"/> property.
    /// </summary>
    [DefaultValue(DataGridViewHeaderBorderStyle.Raised)]
    [Category("Appearance")]
    public DataGridViewHeaderBorderStyle RowHeadersBorderStyle
    {
      get { return DataGridView.RowHeadersBorderStyle; }
      set { DataGridView.RowHeadersBorderStyle = value; }
    }

    /// <summary>
    /// Gets or sets the default style applied to the row header cells.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.RowHeadersDefaultCellStyle"/> property.
    /// </summary>
    [Category("Appearance")]
    public DataGridViewCellStyle RowHeadersDefaultCellStyle
    {
      get { return DataGridView.RowHeadersDefaultCellStyle; }
      set { DataGridView.RowHeadersDefaultCellStyle = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the column that contains row headers is displayed.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.RowHeadersVisible"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    public bool RowHeadersVisible
    {
      get { return DataGridView.RowHeadersVisible; }
      set { DataGridView.RowHeadersVisible = value; }
    }

    /// <summary>
    /// Gets or sets the width, in pixels, of the column that contains the row headers.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.RowHeadersWidth"/> property.
    /// </summary>
    [DefaultValue(41)]
    [Category("Layout")]
    public int RowHeadersWidth
    {
      get { return DataGridView.RowHeadersWidth; }
      set { DataGridView.RowHeadersWidth = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the width of the row headers is adjustable and whether it can be adjusted by the user or is automatically adjusted to fit the contents of the headers.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.RowHeadersWidthSizeMode"/> property.
    /// </summary>
    [DefaultValue(DataGridViewRowHeadersWidthSizeMode.EnableResizing)]
    [Category("Behavior")]
    public DataGridViewRowHeadersWidthSizeMode RowHeadersWidthSizeMode
    {
      get { return DataGridView.RowHeadersWidthSizeMode; }
      set { DataGridView.RowHeadersWidthSizeMode = value; }
    }

    /// <summary>
    /// Gets or sets the default style applied to the row cells of the DataGridView.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.RowsDefaultCellStyle"/> property.
    /// </summary>
    [Category("Appearance")]
    public DataGridViewCellStyle RowsDefaultCellStyle
    {
      get { return DataGridView.RowsDefaultCellStyle; }
      set { DataGridView.RowsDefaultCellStyle = value; }
    }

    /// <summary>
    /// Gets or sets the type of scroll bars to display for the DataGridView control.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.ScrollBars"/> property.
    /// </summary>
    [DefaultValue(ScrollBars.Both)]
    [Category("Layout")]
    public ScrollBars ScrollBars
    {
      get { return DataGridView.ScrollBars; }
      set { DataGridView.ScrollBars = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating how the cells of the DataGridView can be selected.
    /// Wraps the <see cref="System.Windows.Forms.DataGridView.SelectionMode"/> property.
    /// </summary>
    [DefaultValue(DataGridViewSelectionMode.RowHeaderSelect)]
    [Category("Behavior")]
    public DataGridViewSelectionMode SelectionMode
    {
      get { return DataGridView.SelectionMode; }
      set { DataGridView.SelectionMode = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override Color BackColor
    {
      get { return base.BackColor; }
      set { base.BackColor = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override Color ForeColor
    {
      get { return base.ForeColor; }
      set { base.ForeColor = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override string Text
    {
      get { return base.Text; }
      set { base.Text = value; }
    }
    #endregion

    #region Private Methods
    private bool ShouldSerializeBackgroundColor()
    {
      return BackgroundColor != SystemColors.AppWorkspace;
    }

    private bool ShouldSerializeGridColor()
    {
      return GridColor != SystemColors.ControlDark;
    }

    private void DataSource_Disposed(object sender, EventArgs e)
    {
      FDataSource = null;
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override void DeserializeSubItems(FRReader reader)
    {
      if (String.Compare(reader.ItemName, "Columns", true) == 0)
        reader.Read(Columns);
      else
        base.DeserializeSubItems(reader);
    }
    #endregion

    #region IParent Members
    /// <inheritdoc/>
    public bool CanContain(Base child)
    {
      return child is GridControlColumn;
    }

    /// <inheritdoc/>
    public void GetChildObjects(ObjectCollection list)
    {
      // do nothing, prevent displaying columns in the report tree
    }

    /// <inheritdoc/>
    public void AddChild(Base child)
    {
      if (child is GridControlColumn)
        Columns.Add(child as GridControlColumn);
    }

    /// <inheritdoc/>
    public void RemoveChild(Base child)
    {
      if (child is GridControlColumn)
        Columns.Remove(child as GridControlColumn);
    }

    /// <inheritdoc/>
    public int GetChildOrder(Base child)
    {
      return 0;
    }

    /// <inheritdoc/>
    public void SetChildOrder(Base child, int order)
    {
      // do nothing
    }

    /// <inheritdoc/>
    public void UpdateLayout(float dx, float dy)
    {
      // do nothing
    }
    #endregion

    #region Public Methods
    internal static void WriteCellStyle(FRWriter writer, string prefix,
      DataGridViewCellStyle style, DataGridViewCellStyle diff)
    {
      if (style.Alignment != diff.Alignment)
        writer.WriteValue(prefix + ".Alignment", style.Alignment);
      if (style.BackColor != diff.BackColor)
        writer.WriteValue(prefix + ".BackColor", style.BackColor);
      if (style.Font != null && !style.Font.Equals(diff.Font))
        writer.WriteValue(prefix + ".Font", style.Font);
      if (style.ForeColor != diff.ForeColor)
        writer.WriteValue(prefix + ".ForeColor", style.ForeColor);
      if (style.Format != diff.Format)
        writer.WriteStr(prefix + ".Format", style.Format);
      if (style.NullValue != diff.NullValue)
        writer.WriteStr(prefix + ".NullValue", style.NullValue == null ? "" : style.NullValue.ToString());
      if (style.Padding != diff.Padding)
        writer.WriteValue(prefix + ".Padding", style.Padding);
      if (style.SelectionBackColor != diff.SelectionBackColor)
        writer.WriteValue(prefix + ".SelectionBackColor", style.SelectionBackColor);
      if (style.SelectionForeColor != diff.SelectionForeColor)
        writer.WriteValue(prefix + ".SelectionForeColor", style.SelectionForeColor);
      if (style.WrapMode != diff.WrapMode)
        writer.WriteValue(prefix + ".WrapMode", style.WrapMode);
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      GridControl c = writer.DiffObject as GridControl;
      base.Serialize(writer);

      if (DataSource != c.DataSource)
        writer.WriteRef("DataSource", DataSource);
      if (AllowUserToAddRows != c.AllowUserToAddRows)
        writer.WriteBool("AllowUserToAddRows", AllowUserToAddRows);
      if (AllowUserToDeleteRows != c.AllowUserToDeleteRows)
        writer.WriteBool("AllowUserToDeleteRows", AllowUserToDeleteRows);
      WriteCellStyle(writer, "AlternatingRowsDefaultCellStyle",
        AlternatingRowsDefaultCellStyle, c.AlternatingRowsDefaultCellStyle);
      if (AutoSizeColumnsMode != c.AutoSizeColumnsMode)
        writer.WriteValue("AutoSizeColumnsMode", AutoSizeColumnsMode);
      if (AutoSizeRowsMode != c.AutoSizeRowsMode)
        writer.WriteValue("AutoSizeRowsMode", AutoSizeRowsMode);
      if (BackgroundColor != c.BackgroundColor)
        writer.WriteValue("BackgroundColor", BackgroundColor);
      if (BorderStyle != c.BorderStyle)
        writer.WriteValue("BorderStyle", BorderStyle);
      if (CellBorderStyle != c.CellBorderStyle)
        writer.WriteValue("CellBorderStyle", CellBorderStyle);
      if (ColumnHeadersBorderStyle != c.ColumnHeadersBorderStyle)
        writer.WriteValue("ColumnHeadersBorderStyle", ColumnHeadersBorderStyle);
      WriteCellStyle(writer, "ColumnHeadersDefaultCellStyle", 
        ColumnHeadersDefaultCellStyle, c.ColumnHeadersDefaultCellStyle);
      if (ColumnHeadersHeight != c.ColumnHeadersHeight)
        writer.WriteInt("ColumnHeadersHeight", ColumnHeadersHeight);
      if (ColumnHeadersHeightSizeMode != c.ColumnHeadersHeightSizeMode)
        writer.WriteValue("ColumnHeadersHeightSizeMode", ColumnHeadersHeightSizeMode);
      if (ColumnHeadersVisible != c.ColumnHeadersVisible)
        writer.WriteBool("ColumnHeadersVisible", ColumnHeadersVisible);
      WriteCellStyle(writer, "DefaultCellStyle",
        DefaultCellStyle, c.DefaultCellStyle);
      if (GridColor != c.GridColor)
        writer.WriteValue("GridColor", GridColor);
      if (MultiSelect != c.MultiSelect)
        writer.WriteBool("MultiSelect", MultiSelect);
      if (ReadOnly != c.ReadOnly)
        writer.WriteBool("ReadOnly", ReadOnly);
      if (RowHeadersBorderStyle != c.RowHeadersBorderStyle)
        writer.WriteValue("RowHeadersBorderStyle", RowHeadersBorderStyle);
      WriteCellStyle(writer, "RowHeadersDefaultCellStyle",
        RowHeadersDefaultCellStyle, c.RowHeadersDefaultCellStyle);
      if (RowHeadersVisible != c.RowHeadersVisible)
        writer.WriteBool("RowHeadersVisible", RowHeadersVisible);
      if (RowHeadersWidth != c.RowHeadersWidth)
        writer.WriteInt("RowHeadersWidth", RowHeadersWidth);
      if (RowHeadersWidthSizeMode != c.RowHeadersWidthSizeMode)
        writer.WriteValue("RowHeadersWidthSizeMode", RowHeadersWidthSizeMode);
      WriteCellStyle(writer, "RowsDefaultCellStyle",
        RowsDefaultCellStyle, c.RowsDefaultCellStyle);
      if (ScrollBars != c.ScrollBars)
        writer.WriteValue("ScrollBars", ScrollBars);
      if (SelectionMode != c.SelectionMode)
        writer.WriteValue("SelectionMode", SelectionMode);
      if (Columns.Count > 0)
        writer.Write(Columns);  
    }

    /// <inheritdoc/>
    public override void Clear()
    {
      base.Clear();
      Columns.Clear();
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return new GridControlMenu(Report.Designer);
    }

    /// <inheritdoc/>
    public override void InitializeControl()
    {
      base.InitializeControl();
      foreach (GridControlColumn column in Columns)
      {
        column.InitColumn();
      }
      if (DataSource != null)
      {
        DataSource.Init();
        if (DataSource is TableDataSource)
          DataGridView.DataSource = (DataSource as TableDataSource).Table;
        else
          DataGridView.DataSource = DataSource.Rows;
      }
    }

    /// <inheritdoc/>
    public override void FinalizeControl()
    {
      base.FinalizeControl();
      DataGridView.DataSource = null;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>GridControl</b> class with default settings. 
    /// </summary>
    public GridControl()
    {
      FDataGridView = new DataGridView();
      Control = FDataGridView;
      FColumns = new GridControlColumnCollection(this);
      DataGridView.AutoGenerateColumns = false;
      DataGridView.AllowUserToAddRows = false;
      DataGridView.AllowUserToDeleteRows = false;
      DataGridView.ReadOnly = true;
      DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
    }
  }
}
