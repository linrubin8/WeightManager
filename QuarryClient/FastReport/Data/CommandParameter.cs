using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.TypeConverters;

namespace FastReport.Data
{
  /// <summary>
  /// This class represents a single parameter to use in the "select" command.
  /// </summary>
  public class CommandParameter : Base
  {
    private enum ParamValue { Uninitialized }
    
    #region Fields
    private int FDataType;
    private int FSize;
    private string FExpression;
    private string FDefaultValue;
    private object FValue;
    private object FLastValue;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the parameter's data type.
    /// </summary>
    [Editor(typeof(ParameterDataTypeEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(ParameterDataTypeConverter))]
    [Category("Data")]
    public int DataType
    {
      get { return FDataType; }
      set { FDataType = value; }
    }
    
    /// <summary>
    /// Gets or sets the size of parameter's data. 
    /// </summary>
    /// <remarks>
    /// This property is used if the <see cref="DataType"/> property is set to <b>String</b>.
    /// </remarks>
    [DefaultValue(0)]
    [Category("Data")]
    public int Size
    {
      get { return FSize; }
      set { FSize = value; }
    }

    /// <summary>
    /// Gets or sets an expression that returns the parameter's value.
    /// </summary>
    /// <remarks>
    /// If this property is not set, the <see cref="DefaultValue"/> property will be used 
    /// to obtain a parameter's value.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string Expression
    {
      get { return FExpression; }
      set { FExpression = value; }
    }
    
    /// <summary>
    /// Gets or sets a default value for this parameter.
    /// </summary>
    /// <remarks>
    /// This value is used when you designing a report. Also it is used when report is running
    /// in case if you don't provide a value for the <see cref="Expression"/> property.
    /// </remarks>
    public string DefaultValue
    {
      get { return FDefaultValue; }
      set 
      { 
        FDefaultValue = value;
        FValue = null;
      }
    }

    /// <summary>
    /// Gets or sets the parameter's value.
    /// </summary>
    [Browsable(false)]
    public object Value
    {
      get
      {
        if (!String.IsNullOrEmpty(Expression) && Report.IsRunning)
          FValue = Report.Calc(Expression);
        if (FValue == null)
          FValue = new Variant(DefaultValue);
        return FValue;
      }
      set { FValue = value; }
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

    internal Type GetUnderlyingDataType
    {
      get
      {
        if (Parent is TableDataSource && Parent.Parent is DataConnectionBase)
          return (Parent.Parent as DataConnectionBase).GetParameterType();
        return null;  
      }
    }
    
    internal object LastValue
    {
      get { return FLastValue; }
      set { FLastValue = value; }
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      CommandParameter c = writer.DiffObject as CommandParameter;
      base.Serialize(writer);
      
      if (DataType != c.DataType)
        writer.WriteInt("DataType", DataType);
      if (Size != c.Size)
        writer.WriteInt("Size", Size);
      if (Expression != c.Expression)
        writer.WriteStr("Expression", Expression);
      if (DefaultValue != c.DefaultValue)
        writer.WriteStr("DefaultValue", DefaultValue);
    }

    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      return new string[] { Expression };
    }
    
    internal void ResetLastValue()
    {
      LastValue = ParamValue.Uninitialized;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandParameter"/> class with default settings.
    /// </summary>
    public CommandParameter()
    {
      Expression = "";
      DefaultValue = "";
      SetFlags(Flags.CanEdit | Flags.CanCopy, false);
    }
  }
}
