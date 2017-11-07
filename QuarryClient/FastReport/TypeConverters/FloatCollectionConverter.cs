using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using FastReport.Utils;

namespace FastReport.TypeConverters
{
  internal class FloatCollectionConverter : TypeConverter
  {
    private FastString builder = new FastString();

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
        FloatCollection result = new FloatCollection();
        string[] values = (value as string).Split(new char[] { ',' });
        foreach (string s in values)
        {
          result.Add((float)Converter.FromString(typeof(float), s));
        }
        return result;
      }
      return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (destinationType == typeof(string))
      {
        if (value == null)
          return "";
        builder.Clear();
        FloatCollection list = value as FloatCollection;
        foreach (float f in list)
          builder.Append(Converter.ToString(f)).Append(",");
        if (builder.Length > 0)
          builder.Remove(builder.Length - 1, 1);
        return builder.ToString();
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
