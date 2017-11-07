using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Matrix
{
  internal class MatrixHeaderMenu : MatrixCellMenuBase
  {
    private ButtonItem miSort;
    private ButtonItem miSortAsc;
    private ButtonItem miSortDesc;
    private ButtonItem miSortNone;
    private ButtonItem miTotals;
    private ButtonItem miPageBreak;
    private ButtonItem miSuppressTotals;
    private ButtonItem miTotalsFirst;

    private void Sort_Click(object sender, EventArgs e)
    {
      (Descriptor as MatrixHeaderDescriptor).Sort = (SortOrder)(sender as ButtonItem).Tag;
      Change();
    }

    private void miTotals_Click(object sender, EventArgs e)
    {
      (Descriptor as MatrixHeaderDescriptor).Totals = miTotals.Checked;
      Change();
    }

    private void miPageBreak_Click(object sender, EventArgs e)
    {
      (Descriptor as MatrixHeaderDescriptor).PageBreak = miPageBreak.Checked;
      Change();
    }

    private void miSuppressTotals_Click(object sender, EventArgs e)
    {
      (Descriptor as MatrixHeaderDescriptor).SuppressTotals = miSuppressTotals.Checked;
      Change();
    }

    private void miTotalsFirst_Click(object sender, EventArgs e)
    {
      (Descriptor as MatrixHeaderDescriptor).TotalsFirst = miTotalsFirst.Checked;
      Change();
    }

    public MatrixHeaderMenu(MatrixObject matrix, MatrixElement element, MatrixDescriptor descriptor)
      : base(matrix, element, descriptor)
    {
      MyRes res = new MyRes("ComponentMenu,MatrixCell");

      miSort = CreateMenuItem(null, Res.Get("Forms,DataBandEditor,Sort"), null);
      miSortAsc = CreateMenuItem(null, Res.Get("Forms,GroupBandEditor,Ascending"), Sort_Click);
      miSortAsc.AutoCheckOnClick = true;
      miSortAsc.Tag = SortOrder.Ascending;
      miSortDesc = CreateMenuItem(null, Res.Get("Forms,GroupBandEditor,Descending"), Sort_Click);
      miSortDesc.AutoCheckOnClick = true;
      miSortDesc.Tag = SortOrder.Descending;
      miSortNone = CreateMenuItem(null, Res.Get("Forms,GroupBandEditor,NoSort"), Sort_Click);
      miSortNone.AutoCheckOnClick = true;
      miSortNone.Tag = SortOrder.None;
      miTotals = CreateMenuItem(null, res.Get("Totals"), miTotals_Click);
      miTotals.AutoCheckOnClick = true;
      miPageBreak = CreateMenuItem(null, Res.Get("ComponentMenu,Band,StartNewPage"), miPageBreak_Click);
      miPageBreak.AutoCheckOnClick = true;
      miSuppressTotals = CreateMenuItem(null, res.Get("SuppressTotals"), miSuppressTotals_Click);
      miSuppressTotals.AutoCheckOnClick = true;
      miTotalsFirst = CreateMenuItem(null, res.Get("TotalsFirst"), miTotalsFirst_Click);
      miTotalsFirst.AutoCheckOnClick = true;

      miSort.SubItems.AddRange(new BaseItem[] { miSortAsc, miSortDesc, miSortNone });
      
      int insertIndex = Items.IndexOf(miDelete);
      Items.Insert(insertIndex, miSort);
      Items.Insert(insertIndex + 1, miTotals);
      Items.Insert(insertIndex + 2, miPageBreak);
      Items.Insert(insertIndex + 3, miSuppressTotals);
      Items.Insert(insertIndex + 4, miTotalsFirst);

      SortOrder sort = (Descriptor as MatrixHeaderDescriptor).Sort;
      miSortAsc.Checked = sort == SortOrder.Ascending;
      miSortDesc.Checked = sort == SortOrder.Descending;
      miSortNone.Checked = sort == SortOrder.None;
      miTotals.Checked = (Descriptor as MatrixHeaderDescriptor).Totals;
      miTotals.Enabled = !matrix.IsAncestor;
      miPageBreak.Checked = (Descriptor as MatrixHeaderDescriptor).PageBreak;
      miSuppressTotals.Checked = (Descriptor as MatrixHeaderDescriptor).SuppressTotals;
      miTotalsFirst.Checked = (Descriptor as MatrixHeaderDescriptor).TotalsFirst;
    }
  }
}
