using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FastReport
{
  internal class HtmlObjectSmartTag : DataColumnSmartTag
  {
    protected override void ItemClicked()
    {
      (Obj as HtmlObject).Text = DataColumn == "" ? "" : (Obj as HtmlObject).GetTextWithBrackets(DataColumn);
      base.ItemClicked();
    }
    
    public HtmlObjectSmartTag(ComponentBase obj) : base(obj)
    {
      DataColumn = (Obj as HtmlObject).GetTextWithoutBrackets((Obj as HtmlObject).Text);
    }
  }
}
