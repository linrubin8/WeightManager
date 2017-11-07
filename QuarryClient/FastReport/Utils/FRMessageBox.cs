using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Utils
{
  internal static class FRMessageBox
  {
    public static void Error(string msg)
    {
      // MessageBoxEx works incorrect in some cases (when using FR in Outlook plugin)
      //MessageBoxEx.UseSystemLocalizedString = true;
      MessageBox.Show(msg, Res.Get("Messages,Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static DialogResult Confirm(string msg, MessageBoxButtons buttons)
    {
      //MessageBoxEx.UseSystemLocalizedString = true;
      return MessageBox.Show(msg, Res.Get("Messages,Confirmation"), buttons, MessageBoxIcon.Question);
    }

    public static void Information(string msg)
    {
      //MessageBoxEx.UseSystemLocalizedString = true;
      MessageBox.Show(msg, Res.Get("Messages,Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

  }
}