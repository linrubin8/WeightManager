using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport
{
  internal class RichObjectSmartTag : DataColumnSmartTag
  {
    protected override void ItemClicked()
    {
      (Obj as RichObject).DataColumn = DataColumn;
      base.ItemClicked();
    }

    public RichObjectSmartTag(ComponentBase obj) : base(obj)
    {
      DataColumn = (Obj as RichObject).DataColumn;
    }
  }
}
