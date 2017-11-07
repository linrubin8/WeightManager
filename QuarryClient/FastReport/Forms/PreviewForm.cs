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
  internal partial class PreviewForm : Form
  {
    private void PreviewForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Done();
    }

    private void PreviewForm_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Escape)
        Close();
    }

    private void PreviewForm_Shown(object sender, EventArgs e)
    {
      Preview.Focus();
    }

    private void Init()
    {
      Config.RestoreFormState(this);
    }

    private void Done()
    {
      Config.SaveFormState(this);
    }

    public PreviewForm()
    {
      InitializeComponent();
      Init();
    }
  }
}