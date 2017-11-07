using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.Forms
{
  internal partial class ProgressForm : Form
  {
    private Report FReport;
    private bool FAborted;
    private long ticks = 0;

    /// <summary>
    /// Gets Aborted state
    /// </summary>
    public bool Aborted
    {
      get { return FAborted; }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (FReport != null)
        FReport.Abort();
      FAborted = true;
    }

    private void ProgressForm_Paint(object sender, PaintEventArgs e)
    {
      using (Pen p = new Pen(Color.Gray, 2))
      {
        p.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
        e.Graphics.DrawRectangle(p, DisplayRectangle);
      }
    }

    private void ProgressForm_Shown(object sender, EventArgs e)
    {
      lblProgress.Width = Width - lblProgress.Left * 2;
    }

    public void ShowProgressMessage(string message)
    {
      lblProgress.Text = message;
      lblProgress.Refresh();
      if (ticks++ % 500 == 0)
        Application.DoEvents();
    }
    
    public ProgressForm(Report report)
    {
      FAborted = false;
      FReport = report;
      InitializeComponent();
      btnCancel.Text = Res.Get("Buttons,Cancel");
    }
  }
}