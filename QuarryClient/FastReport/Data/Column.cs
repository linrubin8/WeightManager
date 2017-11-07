using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.TypeConverters;
using FastReport.Format;
using FastReport.Controls;

namespace FastReport.Data
{
  /// <summary>
  /// Specifies the format for the column value.
  /// </summary>
  public enum ColumnFormat
  {
    /// <summary>
    /// The format will be determined automatically depending on the column's DataType.
    /// </summary>
    Auto,
    
    /// <summary>
    /// Specifies the General format (no formatting).
    /// </summary>
    General,
    
    /// <summary>
    /// Specifies the Number format.
    /// </summary>
    Number,
    
    /// <summary>
    /// Specifies the Currency format.
    /// </summary>
    Currency,
    
    /// <summary>
    /// Specifies the Date format.
    /// </summary>
    Date,
    
    /// <summary>
    /// Specifies the Time format.
    /// </summary>
    Time,
    
    /// <summary>
    /// Specifies the Percent format.
    /// </summary>
    Percent,
    
    /// <summary>
    /// Specifies the Boolean format.
    /// </summary>
    Boolean
  }
  
  /// <summary>
  /// Specifies the type of an object that will be created when you drop the 
  /// data column on a report page.
  /// </summary>
  public enum ColumnBindableControl 
  { 
    /// <summary>
    /// The column will create the <see cref="TextObject"/> object.
    /// </summary>
    Text,

    /// <summary>
    /// The column will create the <see cref="RichObject"/> object.
    /// </summary>
    RichText,

    /// <summary>
    /// The column will create the <see cref="PictureObject"/> object.
    /// </summary>
    Picture,

    /// <summary>
    /// The column will create the <see cref="CheckBoxObject"/> object.
    /// </summary>
    CheckBox,

    /// <summary>
    /// The column will create the custom object, specified in the 
    /// <see cref="Column.CustomBindableControl"/> property.
    /// </summary>
    Custom
  }
  
  /// <summary>
  /// This class represents a single data column in a <see cref="DataSourceBase"/>.
  /// </summary>
  public partial class Column : DataComponentBase, IParent
  {
    #region Fields
    private string FPropName;
    private PropertyDescriptor FPropDescriptor;
    private Type FDataType;
    private ColumnBindableControl FBindableControl;
    private ColumnFormat FFormat;
    private string FCustomBindableControl;
    private bool FCalculated;
    private string FExpression;
    private ColumnCollection FColumns;
    private object FTag;
    #endregion
    
    #region Properties
    /// <summary>
    /// Gets or sets the business object property name which this column is bound to.
    /// </summary>
    [Browsable(false)]
    public string PropName
    {
      get { return FPropName; }
      set { FPropName = value; }
    }

    /// <summary>
    /// Gets or sets the business object property descriptor which this column is bound to.
    /// </summary>
    [Browsable(false)]
    public PropertyDescriptor PropDescriptor
    {
      get { return FPropDescriptor; }
      set { FPropDescriptor = value; }
    }
    
    /// <summary>
    /// Gets or sets the type of data supplied by this column.
    /// </summary>
    [Editor(typeof(DataTypeEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(DataTypeConverter))]
    [Category("Data")]
    public Type DataType
    {
      get { return FDataType; }
      set { FDataType = value; }
    }

    /// <summary>
    /// Gets or sets a value that specifies the type of a control that will be created 
    /// when you drop this column on a report page.
    /// </summary>
    /// <remarks>
    /// If you need to specify the custom type, use the <see cref="CustomBindableControl"/> property instead.
    /// </remarks>
    [DefaultValue(ColumnBindableControl.Text)]
    [Category("Design")]
    public ColumnBindableControl BindableControl
    {
      get { return FBindableControl; }
      set { FBindableControl = value; }
    }

    /// <summary>
    /// Gets or sets a name of custom bindable control.
    /// </summary>
    /// <remarks>
    /// Use this property if you want to bind a column to custom object type. You need to 
    /// specify the type name of your object; that object must be registered in FastReport using the
    /// <b>RegisteredObjects.Add</b> method.
    /// </remarks>
    [Category("Design")]
    public string CustomBindableControl
    {
      get { return FCustomBindableControl; }
      set 
      { 
        FCustomBindableControl = value;
        if (!String.IsNullOrEmpty(value))
          BindableControl = ColumnBindableControl.Custom;
      }
    }

    /// <summary>
    /// Gets or sets the format of this column.
    /// </summary>
    /// <remarks>
    /// This property is used when you drag a column from the Data window to the report page.
    /// FastReport will create a "Text" object and set its "Format" property to the corresponding format.
    /// By default, this property is set to <b>Auto</b>. It means that the format will be determined 
    /// automatically depending on the <see cref="DataType"/> property.
    /// </remarks>
    [DefaultValue(ColumnFormat.Auto)]
    [Category("Design")]
    public ColumnFormat Format
    {
      get { return FFormat; }
      set { FFormat = value; }
    }

    /// <summary>
    /// Gets or sets expression of the calculated column.
    /// </summary>
    /// <remarks>
    /// This property is used if the <see cref="Calculated"/> property is <b>true</b>.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string Expression
    {
      get { return FExpression; }
      set { FExpression = value; }
    }
    
    /// <summary>
    /// Gets or sets a value that indicates whether this column is calculated.
    /// </summary>
    /// <remarks>
    /// You should specify the <see cref="Expression"/> property for calculated columns.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Data")]
    public bool Calculated
    {
      get { return FCalculated; }
      set { FCalculated = value; }
    }
    
    /// <summary>
    /// Gets the collection of child columns.
    /// </summary>
    [Browsable(false)]
    public ColumnCollection Columns
    {
      get { return FColumns; }
    }

    /// <summary>
    /// Gets or sets the tag value.
    /// </summary>
    [Browsable(false)]
    internal object Tag
    {
      get { return FTag; }
      set { FTag = value; }
    }
    
    internal object Value
    {
      get
      {
        if (Calculated)
        {
          if (!String.IsNullOrEmpty(Expression))
            return Report.Calc(Expression);
        }
        else
        {
          DataSourceBase dataSource = ParentDataSource;
          if (dataSource != null)
            return dataSource[this];
        }
        return null;
      }
    }

    internal DataSourceBase ParentDataSource
    {
      get
      {
        Base parent = Parent;
        while (parent != null)
        {
          if (parent is DataSourceBase)
            return (parent as DataSourceBase);
          parent = parent.Parent;
        }
        return null;
      }
    }

    internal string FullName
    {
      get
      {
        if (Parent is Column)
          return (Parent as Column).FullName + "." + Alias;
        return Alias;
      }
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void SetName(string value)
    {
      base.SetName(value);
      if (String.IsNullOrEmpty(PropName))
        PropName = Name;
    }

    /// <inheritdoc/>
    public override void Delete()
    {
      if (Calculated)
        Dispose();
      else
        base.Delete();
    }

    internal int GetImageIndex()
    {
      if (Calculated)
        return 230;
      if (this is DataSourceBase)
        return 222;
      if (Columns.Count > 0)
        return 233;
      return DataTreeHelper.GetTypeImageIndex(DataType);  
    }

    internal Column FindByPropName(string propName)
    {
      foreach (Column c in Columns)
      {
        if (c.PropName == propName)
          return c;
      }
      return null;
    }
    
    internal Type GetBindableControlType()
    {
      switch (BindableControl)
      {
        case ColumnBindableControl.Text:
          return typeof(TextObject);

        case ColumnBindableControl.Picture:
          return typeof(PictureObject);

        case ColumnBindableControl.CheckBox:
          return typeof(CheckBoxObject);

        case ColumnBindableControl.RichText:
          return typeof(RichObject);    

        case ColumnBindableControl.Custom:
          Type controlType = RegisteredObjects.FindType(CustomBindableControl);
          if (controlType == null)
            return typeof(TextObject);
          return controlType;  
      }
      return null;
    }

    internal void SetBindableControlType(Type type)
    {
      if (type == typeof(byte[]) || typeof(Image).IsAssignableFrom(type))
        BindableControl = ColumnBindableControl.Picture;
      else if (type == typeof(bool))
        BindableControl = ColumnBindableControl.CheckBox;
      else
        BindableControl = ColumnBindableControl.Text;
    }

    internal FormatBase GetFormat()
    {
      switch (Format)
      {
        case ColumnFormat.Auto:
          if (DataType == typeof(decimal))
            return new CurrencyFormat();
          else if (DataType == typeof(float) || DataType == typeof(double))
            return new NumberFormat();
          else if (DataType == typeof(DateTime))
            return new DateFormat();
          break;

        case ColumnFormat.Number:
          return new NumberFormat();

        case ColumnFormat.Currency:
          return new CurrencyFormat();

        case ColumnFormat.Date:
          return new DateFormat();

        case ColumnFormat.Time:
          return new TimeFormat();

        case ColumnFormat.Percent:
          return new PercentFormat();

        case ColumnFormat.Boolean:
          return new BooleanFormat();
      }
      
      return new GeneralFormat();
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      base.Serialize(writer);
      writer.WriteValue("DataType", DataType);
      if (PropName != Name)
        writer.WriteStr("PropName", PropName);
      if (BindableControl != ColumnBindableControl.Text)
        writer.WriteValue("BindableControl", BindableControl);
      if (!String.IsNullOrEmpty(CustomBindableControl))
        writer.WriteStr("CustomBindableControl", CustomBindableControl);
      if (Format != ColumnFormat.Auto)
        writer.WriteValue("Format", Format);
      if (Calculated)
      {
        writer.WriteBool("Calculated", Calculated);
        writer.WriteStr("Expression", Expression);
      }  
    }

    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      if (Calculated)
        return new string[] { Expression };
      return null;
    }
    #endregion

    #region IParent Members
    /// <inheritdoc/>
    public virtual bool CanContain(Base child)
    {
      return child is Column;
    }

    /// <inheritdoc/>
    public virtual void GetChildObjects(ObjectCollection list)
    {
      foreach (Column c in Columns)
      {
        list.Add(c);
      }
    }

    /// <inheritdoc/>
    public virtual void AddChild(Base child)
    {
      if (child is Column)
        Columns.Add(child as Column);
    }

    /// <inheritdoc/>
    public virtual void RemoveChild(Base child)
    {
      if (child is Column)
        Columns.Remove(child as Column);
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

    /// <summary>
    /// Initializes a new instance of the <b>Column</b> class with default settings. 
    /// </summary>
    public Column()
    {
      DataType = typeof(int);
      PropName = "";
      BindableControl = ColumnBindableControl.Text;
      CustomBindableControl = "";
      Expression = "";
      FColumns = new ColumnCollection(this);
    }
  }
}
