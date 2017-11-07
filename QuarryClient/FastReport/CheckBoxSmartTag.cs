using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport
{
  internal class CheckBoxSmartTag : DataColumnSmartTag
  {
    protected override void ItemClicked()
    {
      (Obj as CheckBoxObject).DataColumn = DataColumn;
      base.ItemClicked();
    }

    public CheckBoxSmartTag(ComponentBase obj) : base(obj)
    {
      DataColumn = (Obj as CheckBoxObject).DataColumn;
    }
  }
}
