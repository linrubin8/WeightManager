using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Design;

namespace FastReport.Map
{
    internal class MapObjectMenu : BreakableComponentMenu
    {
        #region Constructors

        public MapObjectMenu(Designer designer) : base(designer)
        {
            miHyperlink.Visible = false;
        }

        #endregion // Constructors
    }
}
