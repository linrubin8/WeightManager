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
  /// <summary>
  /// Represents the FastReport exception form.
  /// </summary>
  public partial class ExceptionForm : BaseDialogForm
  {
    private void ExceptionForm_Shown(object sender, EventArgs e)
    {
      lblException.Width = ClientSize.Width - lblException.Left * 2;
    }

    private void btnCopyToClipboard_Click(object sender, EventArgs e)
    {
      string text = "FastReport.Net v" + Config.Version + "\r\n";
      text += lblException.Text + "\r\n";
      text += tbStack.Text;
      Clipboard.SetText(text);
    }

    /// <inheritdoc/>
    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,Exception");
      Text = res.Get("");
      lblHint.Text = res.Get("Hint");
      lblStack.Text = res.Get("Stack");
      btnCopyToClipboard.Text = res.Get("Copy");
    }
    
    /// <summary>
    /// Creates a new instance ofthe form.
    /// </summary>
    /// <param name="ex">The exception object which data to display in the form.</param>
    public ExceptionForm(Exception ex)
    {
      InitializeComponent();
      Localize();

      lblException.Text = ex.Message;
      tbStack.Text = ex.ToString();
    }
  }
}

