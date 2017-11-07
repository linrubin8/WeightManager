using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport
{
  internal class DataBandSmartTag : DataSourceSmartTag
  {
    protected override void ItemClicked()
    {
      (Obj as DataBand).DataSource = DataSource;
      base.ItemClicked();
    }
    
    public DataBandSmartTag(ComponentBase obj) : base(obj)
    {
      DataSource = (Obj as DataBand).DataSource;
    }
  }
}
