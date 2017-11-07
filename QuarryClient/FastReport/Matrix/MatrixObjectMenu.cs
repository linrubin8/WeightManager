using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using FastReport.Table;
using FastReport.Design;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Matrix
{
  internal class MatrixObjectMenu : TableObjectMenu
  {
    private MatrixObject FMatrix;
    private ButtonItem miAutoSize;
    private ButtonItem miShowTitle;
    private ButtonItem miCellsSideBySide;
    private ButtonItem miStyle;

    private new void Change()
    {
      FMatrix.BuildTemplate();
      base.Change();
    }

    private void BuildStyleMenu()
    {
      ButtonItem styleItem = CreateMenuItem(Res.GetImage(76), Res.Get("Designer,Toolbar,Style,NoStyle"), new EventHandler(styleItem_Click));
      styleItem.Tag = "";
      styleItem.Checked = FMatrix.Style == "";
      styleItem.AutoCheckOnClick = true;
      miStyle.SubItems.Add(styleItem);

      for (int i = 0; i < FMatrix.StyleSheet.Count; i++)
      {
        string text = FMatrix.StyleSheet[i].Name;
        styleItem = CreateMenuItem(FMatrix.StyleSheet.GetStyleBitmap(i),
          Res.Get("ComponentsMisc,Matrix," + text), new EventHandler(styleItem_Click));
        styleItem.Tag = text;
        styleItem.Checked = FMatrix.Style == text;
        styleItem.AutoCheckOnClick = true;
        miStyle.SubItems.Add(styleItem);
      }
    }

    private void styleItem_Click(object sender, EventArgs e)
    {
      FMatrix.Style = (string)(sender as ButtonItem).Tag;
      Change();
    }
    
    private void miAutoSize_Click(object sender, EventArgs e)
    {
      FMatrix.AutoSize = miAutoSize.Checked;
      Change();
    }

    private void miShowTitle_Click(object sender, EventArgs e)
    {
      FMatrix.ShowTitle = miShowTitle.Checked;
      Change();
    }

    private void miCellsSideBySide_Click(object sender, EventArgs e)
    {
      FMatrix.CellsSideBySide = miCellsSideBySide.Checked;
      Change();
    }
    
    public MatrixObjectMenu(Designer designer) : base(designer)
    {
      FMatrix = Designer.SelectedObjects[0] as MatrixObject;
      MyRes res = new MyRes("ComponentMenu,MatrixObject");

      miAutoSize = CreateMenuItem(Res.Get("ComponentMenu,TableRow,AutoSize"), new EventHandler(miAutoSize_Click));
      miAutoSize.BeginGroup = true;
      miAutoSize.AutoCheckOnClick = true;
      miShowTitle = CreateMenuItem(res.Get("ShowTitle"), new EventHandler(miShowTitle_Click));
      miShowTitle.AutoCheckOnClick = true;
      miCellsSideBySide = CreateMenuItem(res.Get("CellsSideBySide"), new EventHandler(miCellsSideBySide_Click));
      miCellsSideBySide.AutoCheckOnClick = true;
      miStyle = CreateMenuItem(Res.GetImage(87), res.Get("Style"), null);
      miStyle.BeginGroup = true;

      int insertIndex = Items.IndexOf(miRepeatHeaders);
      Items.Insert(insertIndex, miAutoSize);
      Items.Insert(insertIndex + 1, miShowTitle);
      Items.Insert(insertIndex + 3, miCellsSideBySide);
      Items.Insert(insertIndex + 4, miStyle);
      
      miRepeatHeaders.BeginGroup = false;
      miCanBreak.Visible = false;
      miGrowToBottom.Visible = false;
      
      miAutoSize.Checked = FMatrix.AutoSize;
      miShowTitle.Checked = FMatrix.ShowTitle;
      miCellsSideBySide.Checked = FMatrix.CellsSideBySide;
      BuildStyleMenu();
    }
  }
}
