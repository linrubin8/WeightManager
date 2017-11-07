using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport
{
  internal class ZipCodeSmartTag : DataColumnSmartTag
  {
    protected override void ItemClicked()
    {
      (Obj as ZipCodeObject).DataColumn = DataColumn;
      base.ItemClicked();
    }

    public ZipCodeSmartTag(ComponentBase obj)
      : base(obj)
    {
      DataColumn = (Obj as ZipCodeObject).DataColumn;
    }
  }
}
