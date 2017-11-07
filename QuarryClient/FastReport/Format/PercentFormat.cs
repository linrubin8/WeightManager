using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using FastReport.Utils;
using System.Globalization;

namespace FastReport.Format
{
  /// <summary>
  /// Defines how percent values are formatted and displayed.
  /// </summary>
  public class PercentFormat : FormatBase
  {
    #region Fields
    private bool FUseLocale;
    private int FDecimalDigits;
    private string FDecimalSeparator;
    private string FGroupSeparator;
    private string FPercentSymbol;
    private int FPositivePattern;
    private int FNegativePattern;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets a value that determines whether to use system locale settings to format a value.
    /// </summary>
    [DefaultValue(true)]
    public bool UseLocale
    {
      get { return FUseLocale; }
      set { FUseLocale = value; }
    }

    /// <summary>
    /// Gets or sets the number of decimal places to use in percent values. 
    /// </summary>
    [DefaultValue(2)]
    public int DecimalDigits
    {
      get { return FDecimalDigits; }
      set { FDecimalDigits = value; }
    }

    /// <summary>
    /// Gets or sets the string to use as the decimal separator in percent values.
    /// </summary>
    public string DecimalSeparator
    {
      get { return FDecimalSeparator; }
      set { FDecimalSeparator = value; }
    }

    /// <summary>
    /// Gets or sets the string that separates groups of digits to the left of the decimal in percent values. 
    /// </summary>
    public string GroupSeparator
    {
      get { return FGroupSeparator; }
      set { FGroupSeparator = value; }
    }
    
    /// <summary>
    /// Gets or sets the string to use as the percent symbol.
    /// </summary>
    public string PercentSymbol
    {
      get { return FPercentSymbol; }
      set { FPercentSymbol = value; }
    }

    /// <summary>
    /// Gets or sets the format pattern for positive percent values.
    /// </summary>
    /// <remarks>This property can have one of the values in the following table. 
    /// The symbol "%" is the <b>PercentSymbol</b> and <i>n</i> is a number.
    /// <list type="table">
    ///   <listheader><term>Value</term><description>Associated Pattern</description></listheader>
    ///   <item><term>0</term><description>n %</description></item>
    ///   <item><term>1</term><description>n%</description></item>
    ///   <item><term>2</term><description>%n</description></item>
    ///   <item><term>3</term><description>% n</description></item>
    /// </list>
    /// </remarks>
    [DefaultValue(0)]
    public int PositivePattern
    {
      get { return FPositivePattern; }
      set { FPositivePattern = value; }
    }

    /// <summary>
    /// Gets or sets the format pattern for negative percent values.
    /// </summary>
    /// <remarks>This property can have one of the values in the following table. 
    /// The symbol "%" is the <b>PercentSymbol</b> and <i>n</i> is a number.
    /// <list type="table">
    ///   <listheader><term>Value</term><description>Associated Pattern</description></listheader>
    ///   <item><term>0</term> <description>-n %</description></item>
    ///   <item><term>1</term> <description>-n%</description></item>
    ///   <item><term>2</term> <description>-%n</description></item>
    ///   <item><term>3</term> <description>%-n</description></item>
    ///   <item><term>4</term> <description>%n-</description></item>
    ///   <item><term>5</term> <description>n-%</description></item>
    ///   <item><term>6</term> <description>n%-</description></item>
    ///   <item><term>7</term> <description>-%n</description></item>
    ///   <item><term>8</term> <description>n %-</description></item>
    ///   <item><term>9</term> <description>% n-</description></item>
    ///   <item><term>10</term><description>% -n</description></item>
    ///   <item><term>11</term><description>n- %</description></item>
    /// </list>
    /// </remarks>
    [DefaultValue(0)]
    public int NegativePattern
    {
      get { return FNegativePattern; }
      set { FNegativePattern = value; }
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override FormatBase Clone()
    {
      PercentFormat result = new PercentFormat();
      result.UseLocale = UseLocale;
      result.DecimalDigits = DecimalDigits;
      result.DecimalSeparator = DecimalSeparator;
      result.GroupSeparator = GroupSeparator;
      result.PercentSymbol = PercentSymbol;
      result.PositivePattern = PositivePattern;
      result.NegativePattern = NegativePattern;
      return result;
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      PercentFormat f = obj as PercentFormat;
      return f != null &&
        UseLocale == f.UseLocale &&
        DecimalDigits == f.DecimalDigits &&
        DecimalSeparator == f.DecimalSeparator &&
        GroupSeparator == f.GroupSeparator &&
        PercentSymbol == f.PercentSymbol &&
        PositivePattern == f.PositivePattern &&
        NegativePattern == f.NegativePattern;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <inheritdoc/>
    public override string FormatValue(object value)
    {
      if (value is Variant)
        value = ((Variant)value).Value;
      if (UseLocale)
        return String.Format("{0:p}", value);
      else
      {
        NumberFormatInfo info = new NumberFormatInfo();
        info.PercentDecimalDigits = DecimalDigits;
        info.PercentDecimalSeparator = DecimalSeparator;
        info.PercentGroupSizes = new int[] { 3 };
        info.PercentGroupSeparator = GroupSeparator;
        info.PercentSymbol = PercentSymbol;
        info.PercentPositivePattern = PositivePattern;
        info.PercentNegativePattern = NegativePattern;
        return String.Format(info, "{0:p}", new object[] { value });
      }
    }

    internal override string GetSampleValue()
    {
      return FormatValue(1.23f);
    }

    internal override void Serialize(FRWriter writer, string prefix, FormatBase format)
    {
      base.Serialize(writer, prefix, format);
      PercentFormat c = format as PercentFormat;
      
      if (c == null || UseLocale != c.UseLocale)
        writer.WriteBool(prefix + "UseLocale", UseLocale);
      if (!UseLocale)
      {  
        if (c == null || DecimalDigits != c.DecimalDigits)
          writer.WriteInt(prefix + "DecimalDigits", DecimalDigits);
        if (c == null || DecimalSeparator != c.DecimalSeparator)
          writer.WriteStr(prefix + "DecimalSeparator", DecimalSeparator);
        if (c == null || GroupSeparator != c.GroupSeparator)
          writer.WriteStr(prefix + "GroupSeparator", GroupSeparator);
        if (c == null || PercentSymbol != c.PercentSymbol)
          writer.WriteStr(prefix + "PercentSymbol", PercentSymbol);
        if (c == null || PositivePattern != c.PositivePattern)
          writer.WriteInt(prefix + "PositivePattern", PositivePattern);
        if (c == null || NegativePattern != c.NegativePattern)
          writer.WriteInt(prefix + "NegativePattern", NegativePattern);
      }
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>PercentFormat</b> class with default settings. 
    /// </summary>
    public PercentFormat()
    {
      UseLocale = true;
      DecimalDigits = 2;
      DecimalSeparator = ".";
      GroupSeparator = ",";
      PercentSymbol = "%";
    }
  }
}
