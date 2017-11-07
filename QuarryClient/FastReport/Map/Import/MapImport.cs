using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Map.Import
{
  internal class MapImport : Base
  {
    #region Properties
    public string Filter
    {
      get { return GetFilter(); }
    }
    #endregion // Properties

    #region Protected Methods
    protected virtual string GetFilter()
    {
      return "";
    }
    #endregion // Protected Methods

    #region Public Methods
    public virtual void ImportMap(MapObject map, MapLayer layer, string filename)
    {
    }
    #endregion // Public Methods
  }
}
