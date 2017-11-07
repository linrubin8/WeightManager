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
  internal partial class AskLoginPasswordForm : BaseDialogForm
  {
    public string Login
    {
      get { return tbLogin.Text; }
    }
    
    public string Password
    {
      get { return tbPassword.Text; }
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,AskLoginPassword");
      Text = res.Get("");
      lblLogin.Text = res.Get("Login");
      lblPassword.Text = res.Get("Password");
    }

    public AskLoginPasswordForm()
    {
      InitializeComponent();

      pbPicture.Image = ResourceLoader.GetBitmap("password.png");
      Localize();
    }
  }
}

