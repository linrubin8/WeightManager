using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FastReport
{
  internal class TextObjectSmartTag : DataColumnSmartTag
  {
    protected override void ItemClicked()
    {
      (Obj as TextObject).Text = DataColumn == "" ? "" : (Obj as TextObject).GetTextWithBrackets(DataColumn);
      base.ItemClicked();
    }
    
    public TextObjectSmartTag(ComponentBase obj) : base(obj)
    {
      DataColumn = (Obj as TextObject).GetTextWithoutBrackets((Obj as TextObject).Text);
    }
  }
}
