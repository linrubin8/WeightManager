using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using System.Globalization;

namespace FastReport.Forms
{
  internal partial class AboutForm : BaseDialogForm
  {
    private void label5_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start(label5.Text);
    }

    private void AboutForm_Shown(object sender, EventArgs e)
    {
      int labelWidth = ClientSize.Width - label3.Left * 2;
      label3.Width = labelWidth;
      label4.Width = labelWidth;
      label5.Width = labelWidth;
    }

    private void AboutForm_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Escape)
        DialogResult = DialogResult.Cancel;
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,About");
      Text = res.Get("");
      label2.Text = res.Get("Version") + " " + Config.Version
#if Demo
      + " " + typeof(DataBand).Name[0].ToString() + typeof(Exception).Name[3].ToString() +
          typeof(Math).Name[0].ToString().ToLower() + typeof(Object).Name[0].ToString().ToLower()
#endif

#if Academic
      + " " + typeof(AboutForm).Name[0].ToString() + typeof(Char).Name[0].ToString().ToLower() +
          typeof(Char).Name[2].ToString() + typeof(DataFormats).Name[0].ToString().ToLower() + 
          typeof(Exception).Name[0].ToString().ToLower() + typeof(Math).Name[0].ToString().ToLower() +
          typeof(Exception).Name[6].ToString() + typeof(Char).Name[0].ToString().ToLower()
#endif
      ;
            string company;
            if (CultureInfo.CurrentCulture.Name == "ru-RU")
			{
				company = " Fast Reports Inc.";
                label5.Text = "https://www.fastreport.ru";
            }
            else
            {
                company = " Fast Reports Inc.";
                label5.Text = "https://www.fast-report.com";
            }
      label3.Text = "?2008-" + DateTime.Now.Year.ToString() + company;
      label4.Text = res.Get("Visit");
      
    }

    public AboutForm()
    {
      InitializeComponent();
      Localize();
    }
  }
}

