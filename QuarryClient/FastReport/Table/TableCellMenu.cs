using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Design;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Table
{
  internal class TableCellMenu : TableMenuBase
  {
    #region Fields
    private SelectedObjectCollection FSelection;
    private TableBase FTable;
    private TableCell FTopCell;
    public ButtonItem miFormat;
    public ButtonItem miJoinSplit;
    public ButtonItem miClear;
    public ButtonItem miCut;
    public ButtonItem miCopy;
    public ButtonItem miPaste;
    #endregion

    #region Private Methods
    private void miFormat_Click(object sender, EventArgs e)
    {
      using (FormatEditorForm form = new FormatEditorForm())
      {
        form.TextObject = FTopCell;
        if (form.ShowDialog() == DialogResult.OK)
        {
          SelectedTextBaseObjects components = new SelectedTextBaseObjects(Designer);
          components.Update();
          components.SetFormat(form.Formats);
          Change();
        }
      }
    }

    private void miJoinSplit_Click(object sender, EventArgs e)
    {
      if (miJoinSplit.Checked)
      {
        Rectangle rect = FTable.GetSelectionRect();
        
        // reset spans inside selection
        for (int x = 0; x < rect.Width; x++)
        {
          for (int y = 0; y < rect.Height; y++)
          {
            TableCell cell = FTable[x + rect.X, y + rect.Y];
            cell.ColSpan = 1;
            cell.RowSpan = 1;
            if (cell != FTopCell)
              cell.Text = "";
          }
        }
        
        FTopCell.ColSpan = rect.Width;
        FTopCell.RowSpan = rect.Height;
        FSelection.Clear();
        FSelection.Add(FTopCell);
      }
      else
      {
        FTopCell.ColSpan = 1;
        FTopCell.RowSpan = 1;
      }

      Change();
    }

    private void miClear_Click(object sender, EventArgs e)
    {
      foreach (Base c in FSelection)
      {
        if (c is TableCell)
          (c as TableCell).Text = "";
      }
      Change();
    }

    private void miCut_Click(object sender, EventArgs e)
    {
      FTable.Clipboard.CutCells(FTable.GetSelectionRect());
      FTable.CreateUniqueNames();
      Change();
    }

    private void miCopy_Click(object sender, EventArgs e)
    {
      FTable.Clipboard.CopyCells(FTable.GetSelectionRect());
    }

    private void miPaste_Click(object sender, EventArgs e)
    {
      FTable.Clipboard.PasteCells(FTopCell.Address);
      FTable.CreateUniqueNames();
      FSelection.Clear();
      FSelection.Add(FTable);
      Change();
    }

    #endregion

    public TableCellMenu(Designer designer) : base(designer)
    {
      FSelection = Designer.SelectedObjects;
      FTopCell = FSelection[0] as TableCell;
      FTable = FTopCell.Parent.Parent as TableBase;

      miFormat = CreateMenuItem(null, Res.Get("ComponentMenu,TextObject,Format"), new EventHandler(miFormat_Click));
      miJoinSplit = CreateMenuItem(Res.GetImage(217), Res.Get("ComponentMenu,TableCell,Join"), new EventHandler(miJoinSplit_Click));
      miJoinSplit.AutoCheckOnClick = true;
      miClear = CreateMenuItem(Res.GetImage(82), Res.Get("ComponentMenu,TextObject,Clear"), new EventHandler(miClear_Click));
      miCut = CreateMenuItem(Res.GetImage(5), Res.Get("Designer,Menu,Edit,Cut"), new EventHandler(miCut_Click));
      miCut.BeginGroup = true;
      miCopy = CreateMenuItem(Res.GetImage(6), Res.Get("Designer,Menu,Edit,Copy"), new EventHandler(miCopy_Click));
      miPaste = CreateMenuItem(Res.GetImage(7), Res.Get("Designer,Menu,Edit,Paste"), new EventHandler(miPaste_Click));

      bool canJoin = FSelection.Count > 1;
      bool canSplit = FSelection.Count == 1 && FTopCell != null && (FTopCell.ColSpan > 1 || FTopCell.RowSpan > 1);
      miJoinSplit.Enabled = canJoin || canSplit;
      miJoinSplit.Checked = canSplit;
      if (miJoinSplit.Checked)
        miJoinSplit.Text = Res.Get("ComponentMenu,TableCell,Split");
      miPaste.Enabled = FTable.Clipboard.CanPasteCells;

      Items.AddRange(new BaseItem[] {
        miFormat, miJoinSplit, miClear, 
        miCut, miCopy, miPaste });
    }
  }
}
