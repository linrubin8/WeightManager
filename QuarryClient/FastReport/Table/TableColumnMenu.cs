using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;
using System.Drawing;

namespace FastReport.Table
{
  internal class TableColumnMenu : TableMenuBase
  {
    #region Fields
    private SelectedObjectCollection FSelection;
    private TableBase FTable;
    private TableColumn FColumn;
    public ButtonItem miInsertColumnToLeft;
    public ButtonItem miInsertColumnToRight;
    public ButtonItem miAutoSize;
    public ButtonItem miCut;
    public ButtonItem miPaste;
    public ButtonItem miDelete;
    #endregion

    #region Properties
    #endregion

    #region Private Methods
    private void miInsertColumnToLeft_Click(object sender, EventArgs e)
    {
      TableColumn column = new TableColumn();
      FTable.Columns.Insert(FTable.Columns.IndexOf(FColumn), column);
      FTable.CreateUniqueNames();
      FSelection.Clear();
      FSelection.Add(column);
      Change();
    }

    private void miInsertColumnToRight_Click(object sender, EventArgs e)
    {
      TableColumn column = new TableColumn();
      FTable.Columns.Insert(FTable.Columns.IndexOf(FColumn) + 1, column);
      FTable.CreateUniqueNames();
      FSelection.Clear();
      FSelection.Add(column);
      Change();
    }

    private void miAutoSize_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < FSelection.Count; i++)
      {
        (FSelection[i] as TableColumn).AutoSize = miAutoSize.Checked;
      }
      Change();
    }

    private void miCut_Click(object sender, EventArgs e)
    {
      TableColumn[] columns = new TableColumn[FSelection.Count];
      for (int i = 0; i < FSelection.Count; i++)
      {
        columns[i] = FSelection[i] as TableColumn;
      }
      FTable.Clipboard.CutColumns(columns);
      FSelection.Clear();
      FSelection.Add(FTable);
      Change();
    }

    private void miPaste_Click(object sender, EventArgs e)
    {
      FTable.Clipboard.PasteColumns(FTable.Columns.IndexOf(FColumn));
      Change();
    }

    private void miDelete_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < FSelection.Count; i++)
      {
        TableColumn column = FSelection[i] as TableColumn;
        if (column != null)
        {
          if (column.IsAncestor)
          {
            FRMessageBox.Error(String.Format(Res.Get("Messages,DeleteAncestor"), column.Name));
            break;
          }
          else
            FTable.Columns.Remove(column);
        }  
      }
      FSelection.Clear();
      FSelection.Add(FTable);
      Change();
    }
    #endregion

    public TableColumnMenu(Designer designer) : base(designer)
    {
      FSelection = Designer.SelectedObjects;
      FColumn = FSelection[0] as TableColumn;
      FTable = FColumn.Parent as TableBase;

      miInsertColumnToLeft = CreateMenuItem(Res.GetImage(220), Res.Get("ComponentMenu,TableColumn,InsertToLeft"), new EventHandler(miInsertColumnToLeft_Click));
      miInsertColumnToRight = CreateMenuItem(Res.GetImage(221), Res.Get("ComponentMenu,TableColumn,InsertToRight"), new EventHandler(miInsertColumnToRight_Click));
      miAutoSize = CreateMenuItem(null, Res.Get("ComponentMenu,TableRow,AutoSize"), new EventHandler(miAutoSize_Click));
      miAutoSize.BeginGroup = true;
      miAutoSize.AutoCheckOnClick = true;
      miCut = CreateMenuItem(Res.GetImage(5), Res.Get("Designer,Menu,Edit,Cut"), new EventHandler(miCut_Click));
      miCut.BeginGroup = true;
      miPaste = CreateMenuItem(Res.GetImage(7), Res.Get("Designer,Menu,Edit,Paste"), new EventHandler(miPaste_Click));
      miDelete = CreateMenuItem(Res.GetImage(51), Res.Get("Designer,Menu,Edit,Delete"), new EventHandler(miDelete_Click));

      miAutoSize.Checked = FColumn.AutoSize;
      miCut.Enabled = FTable.Columns.Count > 1;
      miPaste.Enabled = FTable.Clipboard.CanPasteColumns;
      miDelete.Enabled = FTable.Columns.Count > 1;

      Items.AddRange(new BaseItem[] {
        miInsertColumnToLeft, miInsertColumnToRight, 
        miAutoSize,
        miCut, miPaste, miDelete });
    }
  }
}
