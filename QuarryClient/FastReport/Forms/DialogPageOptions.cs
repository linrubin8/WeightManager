using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Design.PageDesigners.Dialog;
using FastReport.Utils;

namespace FastReport.Forms
{
  internal partial class DialogPageOptions : DesignerOptionsPage
  {
    private DialogPageDesigner FPageDesigner;

    private void Localize()
    {
      tab1.Text = Res.Get("Forms,DialogPageOptions");
      MyRes res = new MyRes("Forms,ReportPageOptions");
      cbShowGrid.Text = res.Get("ShowGrid");
      cbSnapToGrid.Text = res.Get("SnapToGrid");
      lblSnapSize.Text = res.Get("Size");
    }

    public override void Init()
    {
      cbShowGrid.Checked = DialogWorkspace.ShowGrid;
      cbSnapToGrid.Checked = DialogWorkspace.SnapToGrid;
      udSnapSize.Value = (decimal)DialogWorkspace.Grid.SnapSize;
    }

    public override void Done(DialogResult result)
    {
      if (result == DialogResult.OK)
      {
        DialogWorkspace.ShowGrid = cbShowGrid.Checked;
        DialogWorkspace.SnapToGrid = cbSnapToGrid.Checked;
        DialogWorkspace.Grid.SnapSize = (int)udSnapSize.Value;
      }
    }

    public DialogPageOptions(DialogPageDesigner pd) : base()
    {
      FPageDesigner = pd;
      InitializeComponent();
      Localize();
    }
  }
}

