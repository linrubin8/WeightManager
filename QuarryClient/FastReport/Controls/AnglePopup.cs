using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;

namespace FastReport.Controls
{
  internal class AnglePopup : PopupWindow
  {
    private AngleControl FControl;

    public event EventHandler AngleChanged;

    public int Angle
    {
      get { return FControl.Angle; }
      set { FControl.Angle = value; }
    }

    private void FControl_AngleChanged(object sender, EventArgs e)
    {
      if (AngleChanged != null)
        AngleChanged(this, EventArgs.Empty);
    }

    public AnglePopup(Form ownerForm) : base(ownerForm)
    {
      FControl = new AngleControl();
      FControl.ShowBorder = false;
      FControl.AngleChanged += new EventHandler(FControl_AngleChanged);
      Controls.Add(FControl);
      Font = ownerForm.Font;
      ClientSize = FControl.Size;
    }

  }
}
