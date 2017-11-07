using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace FastReport
{
  internal class PrinterConverter : StringConverter
  {
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      return false;
    }

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new StandardValuesCollection(System.Drawing.Printing.PrinterSettings.InstalledPrinters);
    }
  }
}
