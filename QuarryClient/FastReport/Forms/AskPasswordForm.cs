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
  internal partial class AskPasswordForm : BaseDialogForm
  {
    public string Password
    {
      get { return tbPassword.Text; }
    }

    private void AskPasswordForm_Shown(object sender, EventArgs e)
    {
      tbPassword.Focus();
    }

    private void AskPasswordForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (DialogResult != DialogResult.OK)
        tbPassword.Text = "";
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,AskPassword");
      Text = res.Get("");
      lblPassword.Text = res.Get("Password");
    }
    
    public AskPasswordForm()
    {
      InitializeComponent();
      
      pbPicture.Image = ResourceLoader.GetBitmap("password.png");
      Localize();
    }
  }
}

