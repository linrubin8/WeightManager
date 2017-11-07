using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Table
{
  internal class TableRowMenu : TableMenuBase
  {
    #region Fields
    private SelectedObjectCollection FSelection;
    private TableBase FTable;
    private TableRow FRow;
    public ButtonItem miInsertRowAbove;
    public ButtonItem miInsertRowBelow;
    public ButtonItem miAutoSize;
    public ButtonItem miCut;
    public ButtonItem miPaste;
    public ButtonItem miDelete;
    #endregion

    #region Properties
    #endregion

    #region Private Methods
    private void miInsertRowAbove_Click(object sender, EventArgs e)
    {
      TableRow row = new TableRow();
      FTable.Rows.Insert(FTable.Rows.IndexOf(FRow), row);
      FTable.CreateUniqueNames();
      FSelection.Clear();
      FSelection.Add(row);
      Change();
    }

    private void miInsertRowBelow_Click(object sender, EventArgs e)
    {
      TableRow row = new TableRow();
      FTable.Rows.Insert(FTable.Rows.IndexOf(FRow) + 1, row);
      FTable.CreateUniqueNames();
      FSelection.Clear();
      FSelection.Add(row);
      Change();
    }

    private void miAutoSize_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < FSelection.Count; i++)
      {
        (FSelection[i] as TableRow).AutoSize = miAutoSize.Checked;
      }
      Change();
    }

    private void miCut_Click(object sender, EventArgs e)
    {
      TableRow[] rows = new TableRow[FSelection.Count];
      for (int i = 0; i < FSelection.Count; i++)
      {
        rows[i] = FSelection[i] as TableRow;
      }
      FTable.Clipboard.CutRows(rows);
      FSelection.Clear();
      FSelection.Add(FTable);
      Change();
    }

    private void miPaste_Click(object sender, EventArgs e)
    {
      FTable.Clipboard.PasteRows(FTable.Rows.IndexOf(FRow));
      FSelection.Clear();
      FSelection.Add(FTable);
      Change();
    }

    private void miDelete_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < FSelection.Count; i++)
      {
        TableRow row = FSelection[i] as TableRow;
        if (row != null)
        {
          if (row.IsAncestor)
          {
            FRMessageBox.Error(String.Format(Res.Get("Messages,DeleteAncestor"), row.Name));
            break;
          }
          else
            FTable.Rows.Remove(row);
        }  
      }
      FSelection.Clear();
      FSelection.Add(FTable);
      Change();
    }
    #endregion

    public TableRowMenu(Designer designer) : base(designer)
    {
      FSelection = Designer.SelectedObjects;
      FRow = FSelection[0] as TableRow;
      FTable = FRow.Parent as TableBase;

      miInsertRowAbove = CreateMenuItem(Res.GetImage(218), Res.Get("ComponentMenu,TableRow,InsertAbove"), new EventHandler(miInsertRowAbove_Click));
      miInsertRowBelow = CreateMenuItem(Res.GetImage(219), Res.Get("ComponentMenu,TableRow,InsertBelow"), new EventHandler(miInsertRowBelow_Click));
      miAutoSize = CreateMenuItem(null, Res.Get("ComponentMenu,TableRow,AutoSize"), new EventHandler(miAutoSize_Click));
      miAutoSize.BeginGroup = true;
      miAutoSize.AutoCheckOnClick = true;
      miCut = CreateMenuItem(Res.GetImage(5), Res.Get("Designer,Menu,Edit,Cut"), new EventHandler(miCut_Click));
      miCut.BeginGroup = true;
      miPaste = CreateMenuItem(Res.GetImage(7), Res.Get("Designer,Menu,Edit,Paste"), new EventHandler(miPaste_Click));
      miDelete = CreateMenuItem(Res.GetImage(51), Res.Get("Designer,Menu,Edit,Delete"), new EventHandler(miDelete_Click));
      
      miAutoSize.Checked = FRow.AutoSize;
      miCut.Enabled = FTable.Rows.Count > 1;
      miPaste.Enabled = FTable.Clipboard.CanPasteRows;
      miDelete.Enabled = FTable.Rows.Count > 1;

      Items.AddRange(new BaseItem[] {
        miInsertRowAbove, miInsertRowBelow, 
        miAutoSize,
        miCut, miPaste, miDelete });
    }
  }
}
