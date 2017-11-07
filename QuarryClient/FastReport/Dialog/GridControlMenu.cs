using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Dialog
{
  internal class GridControlMenu : ComponentMenuBase
  {
    #region Fields
    public ButtonItem miEditColumns;
    #endregion

    #region Private Methods
    private void miEditColumns_Click(object sender, EventArgs e)
    {
      GridControl grid = Designer.SelectedObjects[0] as GridControl;
      using (GridControlColumnsEditorForm form = new GridControlColumnsEditorForm())
      {
        form.Grid = grid;
        if (form.ShowDialog() == DialogResult.OK)
          Change();
      }
    }
    #endregion

    public GridControlMenu(Designer designer) : base(designer)
    {
      miEditColumns = CreateMenuItem(Res.Get("ComponentMenu,GridControl,EditColumns"), new EventHandler(miEditColumns_Click));
      miEditColumns.BeginGroup = true;
      
      int insertPos = Items.IndexOf(miCut);
      Items.Insert(insertPos, miEditColumns);
    }
  }
}
