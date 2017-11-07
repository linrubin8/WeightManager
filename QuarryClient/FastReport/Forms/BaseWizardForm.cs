using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Design;

namespace FastReport.Forms
{
  internal partial class BaseWizardForm : BaseDialogForm
  {
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual int VisiblePanelIndex
    {
      get { return pcPages.ActivePageIndex; }
      set
      {
        pcPages.ActivePageIndex = value;
        btnPrevious.Enabled = value > 0;
        btnNext.Enabled = value < pcPages.Pages.Count - 1;
        btnFinish.Enabled = value == pcPages.Pages.Count - 1;
        lblCaption.Text = pcPages.Pages[value].Text;
      }
    }

    private void btnPrevious_Click(object sender, EventArgs e)
    {
      VisiblePanelIndex--;
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      VisiblePanelIndex++;
    }

    private void pnTop_Paint(object sender, PaintEventArgs e)
    {
      e.Graphics.DrawLine(Pens.Silver, 0, pnTop.Height - 1, pnTop.Width, pnTop.Height - 1);
    }

    private void pnBottom_Paint(object sender, PaintEventArgs e)
    {
      e.Graphics.DrawLine(Pens.Silver, 0, 0, pnBottom.Width, 0);
    }

    public override void Localize()
    {
      base.Localize();
      btnPrevious.Text = Res.Get("Buttons,Previous");
      btnNext.Text = Res.Get("Buttons,Next");
      btnFinish.Text = Res.Get("Buttons,Finish");
      btnCancel1.Text = Res.Get("Buttons,Cancel");
    }
    
    public BaseWizardForm()
    {
      InitializeComponent();
    }
  }
}

