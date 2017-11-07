using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Matrix
{
  internal class MatrixCellMenu : MatrixCellMenuBase
  {
    private ButtonItem miFunction;
    private ButtonItem miFunctionNone;
    private ButtonItem miFunctionSum;
    private ButtonItem miFunctionMin;
    private ButtonItem miFunctionMax;
    private ButtonItem miFunctionAvg;
    private ButtonItem miFunctionCount;
    private ButtonItem miFunctionCustom;
    private ButtonItem miPercent;
    private ButtonItem miPercentNone;
    private ButtonItem miPercentColumnTotal;
    private ButtonItem miPercentRowTotal;
    private ButtonItem miPercentGrandTotal;

    private void Function_Click(object sender, EventArgs e)
    {
      MatrixAggregateFunction function = (MatrixAggregateFunction)(sender as ButtonItem).Tag;
      (Descriptor as MatrixCellDescriptor).Function = function;
      Change();
    }

    private void Percent_Click(object sender, EventArgs e)
    {
      MatrixPercent percent = (MatrixPercent)(sender as ButtonItem).Tag;
      (Descriptor as MatrixCellDescriptor).Percent = percent;
      Change();
    }

    public MatrixCellMenu(MatrixObject matrix, MatrixElement element, MatrixDescriptor descriptor)
      : base(matrix, element, descriptor)
    {
      MyRes res = new MyRes("Forms,TotalEditor");

      miFunction = CreateMenuItem(Res.GetImage(132), Res.Get("ComponentMenu,MatrixCell,Function"), null);
      miFunctionNone = CreateMenuItem(null, Res.Get("Misc,None"), new EventHandler(Function_Click));
      miFunctionNone.AutoCheckOnClick = true;
      miFunctionNone.Tag = MatrixAggregateFunction.None;
      miFunctionSum = CreateMenuItem(null, res.Get("Sum"), new EventHandler(Function_Click));
      miFunctionSum.AutoCheckOnClick = true;
      miFunctionSum.Tag = MatrixAggregateFunction.Sum;
      miFunctionMin = CreateMenuItem(null, res.Get("Min"), new EventHandler(Function_Click));
      miFunctionMin.AutoCheckOnClick = true;
      miFunctionMin.Tag = MatrixAggregateFunction.Min;
      miFunctionMax = CreateMenuItem(null, res.Get("Max"), new EventHandler(Function_Click));
      miFunctionMax.AutoCheckOnClick = true;
      miFunctionMax.Tag = MatrixAggregateFunction.Max;
      miFunctionAvg = CreateMenuItem(null, res.Get("Avg"), new EventHandler(Function_Click));
      miFunctionAvg.AutoCheckOnClick = true;
      miFunctionAvg.Tag = MatrixAggregateFunction.Avg;
      miFunctionCount = CreateMenuItem(null, res.Get("Count"), new EventHandler(Function_Click));
      miFunctionCount.AutoCheckOnClick = true;
      miFunctionCount.Tag = MatrixAggregateFunction.Count;
      miFunctionCustom = CreateMenuItem(null, res.Get("Custom"), new EventHandler(Function_Click));
      miFunctionCustom.AutoCheckOnClick = true;
      miFunctionCustom.Tag = MatrixAggregateFunction.Custom;

      miFunction.SubItems.AddRange(new BaseItem[] {
        miFunctionNone, miFunctionSum, miFunctionMin, miFunctionMax, miFunctionAvg, miFunctionCount, miFunctionCustom });

      res = new MyRes("ComponentMenu,MatrixCell");
      miPercent = CreateMenuItem(null, res.Get("Percent"), null);
      miPercentNone = CreateMenuItem(null, Res.Get("Misc,None"), new EventHandler(Percent_Click));
      miPercentNone.AutoCheckOnClick = true;
      miPercentNone.Tag = MatrixPercent.None;
      miPercentColumnTotal = CreateMenuItem(null, res.Get("PercentColumnTotal"), new EventHandler(Percent_Click));
      miPercentColumnTotal.AutoCheckOnClick = true;
      miPercentColumnTotal.Tag = MatrixPercent.ColumnTotal;
      miPercentRowTotal = CreateMenuItem(null, res.Get("PercentRowTotal"), new EventHandler(Percent_Click));
      miPercentRowTotal.AutoCheckOnClick = true;
      miPercentRowTotal.Tag = MatrixPercent.RowTotal;
      miPercentGrandTotal = CreateMenuItem(null, res.Get("PercentGrandTotal"), new EventHandler(Percent_Click));
      miPercentGrandTotal.AutoCheckOnClick = true;
      miPercentGrandTotal.Tag = MatrixPercent.GrandTotal;

      miPercent.SubItems.AddRange(new BaseItem[] {
        miPercentNone, miPercentColumnTotal, miPercentRowTotal, miPercentGrandTotal });
      
      int insertIndex = Items.IndexOf(miDelete);
      Items.Insert(insertIndex, miFunction);
      Items.Insert(insertIndex + 1, miPercent);
      
      MatrixAggregateFunction function = (Descriptor as MatrixCellDescriptor).Function;
      foreach (BaseItem item in miFunction.SubItems)
      {
        if ((MatrixAggregateFunction)item.Tag == function)
        {
          (item as ButtonItem).Checked = true;
          break;
        }
      }

      MatrixPercent percent = (Descriptor as MatrixCellDescriptor).Percent;
      foreach (BaseItem item in miPercent.SubItems)
      {
        if ((MatrixPercent)item.Tag == percent)
        {
          (item as ButtonItem).Checked = true;
          break;
        }
      }
    }
  }
}
