using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.Data;

namespace FastReport.Dialog
{
  /// <summary>
  /// Represents the <see cref="GridControl"/>'s column.
  /// Wraps the <see cref="System.Windows.Forms.DataGridViewTextBoxColumn"/> class.
  /// </summary>
  public class GridControlColumn : Base
  {
    private DataGridViewTextBoxColumn FColumn;
    private string FDataColumn;

    #region Properties
    /// <summary>
    /// Gets or sets the mode by which the column automatically adjusts its width.
    /// Wraps the <see cref="System.Windows.Forms.DataGridViewColumn.AutoSizeMode"/> property.
    /// </summary>
    [DefaultValue(DataGridViewAutoSizeColumnMode.NotSet)]
    [Category("Behavior")]
    public DataGridViewAutoSizeColumnMode AutoSizeMode
    {
      get { return FColumn.AutoSizeMode; }
      set { FColumn.AutoSizeMode = value; }
    }

    /// <summary>
    /// Gets or sets the data column attached to this column.
    /// </summary>
    [Editor(typeof(DataColumnEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string DataColumn
    {
      get { return FDataColumn; }
      set { FDataColumn = value; }
    }

    /// <summary>
    /// Gets or sets the caption text on the column's header cell.
    /// Wraps the <see cref="System.Windows.Forms.DataGridViewColumn.HeaderText"/> property.
    /// </summary>
    [Category("Appearance")]
    public string HeaderText
    {
      get { return FColumn.HeaderText; }
      set { FColumn.HeaderText = value; }
    }

    /// <summary>
    /// Gets or sets the column's default cell style.
    /// Wraps the <see cref="System.Windows.Forms.DataGridViewColumn.DefaultCellStyle"/> property.
    /// </summary>
    [Category("Appearance")]
    public DataGridViewCellStyle DefaultCellStyle
    {
      get { return FColumn.DefaultCellStyle; }
      set { FColumn.DefaultCellStyle = value; }
    }

    /// <summary>
    /// Gets or sets a value that represents the width of the column when it is in fill mode relative to the widths of other fill-mode columns in the control.
    /// Wraps the <see cref="System.Windows.Forms.DataGridViewColumn.FillWeight"/> property.
    /// </summary>
    [DefaultValue(100f)]
    [Category("Layout")]
    public float FillWeight
    {
      get { return FColumn.FillWeight; }
      set { FColumn.FillWeight = value; }
    }

    /// <summary>
    /// Gets or sets the current width of the column.
    /// Wraps the <see cref="System.Windows.Forms.DataGridViewColumn.Width"/> property.
    /// </summary>
    [DefaultValue(100)]
    [Category("Layout")]
    public int Width
    {
      get { return FColumn.Width; }
      set { FColumn.Width = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the column is visible.
    /// Wraps the <see cref="System.Windows.Forms.DataGridViewColumn.Visible"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool Visible
    {
      get { return FColumn.Visible; }
      set { FColumn.Visible = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new string Name
    {
      get { return base.Name; }
      set { base.Name = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new Restrictions Restrictions
    {
      get { return base.Restrictions; }
      set { base.Restrictions = value; }
    }
    
    internal DataGridViewTextBoxColumn Column
    {
      get { return FColumn; }
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      BaseAssign(source);
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      GridControlColumn c = writer.DiffObject as GridControlColumn;
      writer.ItemName = "Column";
      
      if (AutoSizeMode != c.AutoSizeMode)
        writer.WriteValue("AutoSizeMode", AutoSizeMode);
      if (DataColumn != c.DataColumn)
        writer.WriteStr("DataColumn", DataColumn);
      if (HeaderText != c.HeaderText)
        writer.WriteStr("HeaderText", HeaderText);
      GridControl.WriteCellStyle(writer, "DefaultCellStyle", DefaultCellStyle, c.DefaultCellStyle);
      if (FillWeight != c.FillWeight)
        writer.WriteFloat("FillWeight", FillWeight);
      if (Width != c.Width)
        writer.WriteInt("Width", Width);
      if (Visible != c.Visible)
        writer.WriteBool("Visible", Visible);
    }
    
    internal void InitColumn()
    {
      if (Report != null)
      {
        FastReport.Data.Column column = DataHelper.GetColumn(Report.Dictionary, DataColumn);
        if (column != null)
          FColumn.DataPropertyName = column.Name;
      }
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>GridControlColumn</b> class with default settings. 
    /// </summary>
    public GridControlColumn()
    {
      FColumn = new DataGridViewTextBoxColumn();
    }
  }
}
