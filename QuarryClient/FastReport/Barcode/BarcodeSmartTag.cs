using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Barcode
{
  internal class BarcodeSmartTag : DataColumnSmartTag
  {
    protected override void ItemClicked()
    {
      (Obj as BarcodeObject).DataColumn = DataColumn;
      base.ItemClicked();
    }

    public BarcodeSmartTag(ComponentBase obj) : base(obj)
    {
      DataColumn = (Obj as BarcodeObject).DataColumn;
    }
  }
}
