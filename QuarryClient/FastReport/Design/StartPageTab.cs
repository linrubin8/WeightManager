using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Design
{
  internal class StartPageTab : DocumentWindow
  {
    private Designer FDesigner;

    public StartPageTab(Designer designer) : base()
    {
      FDesigner = designer;
      ParentControl.BackColor = SystemColors.Window;
      Text = Res.Get("Designer,StartPage");
    }
  }
}
