using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using FastReport.Utils;
using FastReport.Design;
using FastReport.Design.PageDesigners.Page;

namespace FastReport.TypeConverters
{
  internal class PaperConverter : TypeConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if (sourceType == typeof(string))
        return true;
      return base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if (destinationType == typeof(string))
        return true;
      return base.CanConvertTo(context, destinationType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      if (value is string)
      {
        float result = Converter.StringToFloat((string)value, true);
        switch (ReportWorkspace.Grid.GridUnits)
        {
          case PageUnits.Millimeters:
            result *= 1;
            break;
          case PageUnits.Centimeters:
            result *= 10;
            break;
          case PageUnits.Inches:
            result *= 25.4f;
            break;
          case PageUnits.HundrethsOfInch:
            result *= 0.254f;
            break;
        }
        return Converter.DecreasePrecision(result, 2);
      }
      return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (destinationType == typeof(string))
      {
        float result = (float)value;
        string unit = "";
        int precision = 2;
        switch (ReportWorkspace.Grid.GridUnits)
        {
          case PageUnits.Millimeters:
            result /= 1;
            unit = Res.Get("Misc,ShortUnitsMm");
            break;
          case PageUnits.Centimeters:
            result /= 10;
            unit = Res.Get("Misc,ShortUnitsCm");
            break;
          case PageUnits.Inches:
            result /= 25.4f;
            unit = Res.Get("Misc,ShortUnitsIn");
            precision = 3;
            break;
          case PageUnits.HundrethsOfInch:
            result /= 0.254f;
            unit = Res.Get("Misc,ShortUnitsHi");
            precision = 1;
            break;
        }
        return Converter.DecreasePrecision(result, precision).ToString() + " " + unit;
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
