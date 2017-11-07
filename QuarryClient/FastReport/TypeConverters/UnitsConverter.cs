using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using FastReport.Design;
using FastReport.Utils;
using FastReport.Design.PageDesigners.Page;

namespace FastReport.TypeConverters
{
  /// <summary>
  /// Provides a type converter for a property representing a value measured in the current report units.
  /// </summary>
  public class UnitsConverter : TypeConverter
  {
    /// <inheritdoc/>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if (sourceType == typeof(string))
        return true;
      return base.CanConvertFrom(context, sourceType);
    }

    /// <inheritdoc/>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if (destinationType == typeof(string))
        return true;
      return base.CanConvertTo(context, destinationType);
    }

    /// <inheritdoc/>
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      if (value is string)
      {
        float result = Converter.StringToFloat((string)value, true);
        switch (ReportWorkspace.Grid.GridUnits)
        {
          case PageUnits.Millimeters:
            result *= Units.Millimeters;
            break;
          case PageUnits.Centimeters:
            result *= Units.Centimeters;
            break;
          case PageUnits.Inches:
            result *= Units.Inches;
            break;
          case PageUnits.HundrethsOfInch:
            result *= Units.HundrethsOfInch; 
            break;      
        }
        return Converter.DecreasePrecision(result, 2);
      }
      return base.ConvertFrom(context, culture, value);
    }

    /// <inheritdoc/>
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (destinationType == typeof(string))
      {
        float result = (float)value;
        string unit = "";
        switch (ReportWorkspace.Grid.GridUnits)
        {
          case PageUnits.Millimeters:
            result /= Units.Millimeters;
            unit = Res.Get("Misc,ShortUnitsMm");
            break;
          case PageUnits.Centimeters:
            result /= Units.Centimeters;
            unit = Res.Get("Misc,ShortUnitsCm");
            break;
          case PageUnits.Inches:
            result /= Units.Inches;
            unit = Res.Get("Misc,ShortUnitsIn");
            break;
          case PageUnits.HundrethsOfInch:
            result /= Units.HundrethsOfInch;
            unit = Res.Get("Misc,ShortUnitsHi");
            break;
        }
        return Converter.DecreasePrecision(result, 2).ToString() + " " + unit;
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }

}
