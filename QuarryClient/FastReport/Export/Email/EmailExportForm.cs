using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FastReport.Forms;
using FastReport.Utils;

namespace FastReport.Export.Email
{
  /// <summary>
  /// Form for <see cref="EmailExport"/>.
  /// For internal use only.
  /// </summary>
  public partial class EmailExportForm : BaseDialogForm
  {
    private EmailExport FExport;
    private List<ExportBase> FExports;

    private bool IsValidEmail(string strIn)
    {
      // Return true if strIn is in valid e-mail format.
      return Regex.IsMatch(strIn,
             @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
             @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
    }

    private void EmailExportForm_Shown(object sender, EventArgs e)
    {
      if (String.IsNullOrEmpty(tbAddressFrom.Text) || String.IsNullOrEmpty(tbHost.Text))
      {
        pageControl1.ActivePageIndex = 1;
        if (String.IsNullOrEmpty(tbAddressFrom.Text))
          tbAddressFrom.Focus();
        else
          tbHost.Focus();
      }
      else
      {
        cbxAddressTo.Focus();
        if (!FExport.Account.AllowUI)
        {
          pageControl1.Width -= pageControl1.SelectorWidth;
          Width -= pageControl1.SelectorWidth;
          pageControl1.SelectorWidth = 0;
        }  
      }  
    }

    private void cbxAttachment_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnSettings.Enabled = cbxAttachment.SelectedIndex > 0;
    }

    private void btnSettings_Click(object sender, EventArgs e)
    {
      ExportBase export = FExports[cbxAttachment.SelectedIndex];
      export.SetReport(FExport.Report);
      export.ShowDialog();
    }

    private void Init()
    {
      XmlItem xi = Config.Root.FindItem("EmailExport").FindItem("AccountSettings");
      
      // restore account info from the config
      if (String.IsNullOrEmpty(FExport.Account.Address))
      {
        FExport.Account.Address = xi.GetProp("Address");
        FExport.Account.Name = xi.GetProp("Name");
        FExport.Account.MessageTemplate = xi.GetProp("Template");
        FExport.Account.Host = xi.GetProp("Host");
        string port = xi.GetProp("Port");
        if (port != "")
          FExport.Account.Port = int.Parse(port);
        FExport.Account.UserName = xi.GetProp("UserName");
        FExport.Account.Password = xi.GetProp("Password");
        FExport.Account.EnableSSL = xi.GetProp("EnableSSL") == "1";
      }
      
      // fill account info
      tbAddressFrom.Text = FExport.Account.Address;
      tbName.Text = FExport.Account.Name;
      tbTemplate.Text = FExport.Account.MessageTemplate;
      tbHost.Text = FExport.Account.Host;
      udPort.Value = FExport.Account.Port;
      tbUserName.Text = FExport.Account.UserName;
      tbPassword.Text = FExport.Account.Password;
      cbEnableSSL.Checked = FExport.Account.EnableSSL;
      
      // fill email
      string[] addresses = xi.GetProp("RecentAddresses").Split(new char[] { '\r' });
      cbxAddressTo.Items.AddRange(addresses);
      if (!String.IsNullOrEmpty(FExport.Address))
        cbxAddressTo.Text = FExport.Address;
      else if (cbxAddressTo.Items.Count > 0)
        cbxAddressTo.SelectedIndex = 0;
      string[] cc = FExport.CC;
      if (cc != null)
      {
        for (int i = 0; i < cc.Length; i++)
          tbCC.Text += cc[i] + (i < cc.Length - 1 ? ";" : "");
      }

      string[] subjects = xi.GetProp("RecentSubjects").Split(new char[] { '\r' });
      cbxSubject.Items.AddRange(subjects);
      if (!String.IsNullOrEmpty(FExport.Subject))
        cbxSubject.Text = FExport.Subject;
      else if (cbxSubject.Items.Count > 0)
        cbxSubject.SelectedIndex = 0;

      if (!String.IsNullOrEmpty(FExport.MessageBody))
        tbMessage.Text = FExport.MessageBody;
      else
        tbMessage.Text = tbTemplate.Text;

      // fill exports
      FExports = new List<ExportBase>();
      List<ObjectInfo> list = new List<ObjectInfo>();
      RegisteredObjects.Objects.EnumItems(list);

      int exportIndex = 0;
      cbxAttachment.Items.Add(Res.Get("Preview,SaveNative"));
      FExports.Add(null);

      foreach (ObjectInfo info in list)
      {
        if (info.Object != null && info.Object.IsSubclassOf(typeof(ExportBase)))
        {
          cbxAttachment.Items.Add(Res.TryGet(info.Text));
          FExports.Add(Activator.CreateInstance(info.Object) as ExportBase);
          if (FExport.Export != null && FExport.Export.GetType() == info.Object)
            exportIndex = FExports.Count - 1;
        }
      }
      
      string recentExport = xi.GetProp("RecentExport");
      if (exportIndex != 0)
        cbxAttachment.SelectedIndex = exportIndex;
      else if (recentExport != "")
        cbxAttachment.SelectedIndex = int.Parse(recentExport);
      else
        cbxAttachment.SelectedIndex = 0;
    }

    private bool Done()
    {
      if (!IsValidEmail(tbAddressFrom.Text))
      {
        pageControl1.ActivePageIndex = 1;
        FRMessageBox.Error(Res.Get("Export,Email,AddressError"));
        tbAddressFrom.Focus();
        return false;
      }
      if (String.IsNullOrEmpty(tbHost.Text))
      {
        pageControl1.ActivePageIndex = 1;
        FRMessageBox.Error(Res.Get("Export,Email,HostError"));
        tbHost.Focus();
        return false;
      }
      if (!IsValidEmail(cbxAddressTo.Text))
      {
        pageControl1.ActivePageIndex = 0;
        FRMessageBox.Error(Res.Get("Export,Email,AddressError"));
        cbxAddressTo.Focus();
        return false;
      }

      XmlItem xi = Config.Root.FindItem("EmailExport").FindItem("AccountSettings");

      // get account info
      FExport.Account.Address = tbAddressFrom.Text;
      FExport.Account.Name = tbName.Text;
      FExport.Account.MessageTemplate = tbTemplate.Text;
      FExport.Account.Host = tbHost.Text;
      FExport.Account.Port = (int)udPort.Value;
      FExport.Account.UserName = tbUserName.Text;
      FExport.Account.Password = tbPassword.Text;
      FExport.Account.EnableSSL = cbEnableSSL.Checked;

      // save account info
      xi.SetProp("Address", FExport.Account.Address);
      xi.SetProp("Name", FExport.Account.Name);
      xi.SetProp("Template", FExport.Account.MessageTemplate);
      xi.SetProp("Host", FExport.Account.Host);
      xi.SetProp("Port", FExport.Account.Port.ToString());
      xi.SetProp("UserName", FExport.Account.UserName);
      xi.SetProp("Password", FExport.Account.Password);
      xi.SetProp("EnableSSL", FExport.Account.EnableSSL ? "1" : "0");

      // get email info
      FExport.Address = cbxAddressTo.Text.Trim();
      FExport.CC = tbCC.Text.Trim() == "" ? null : tbCC.Text.Trim().Split(new char[] { ';' });
      FExport.Subject = cbxSubject.Text;
      FExport.MessageBody = tbMessage.Text;
      FExport.Export = FExports[cbxAttachment.SelectedIndex];
      
      // save email info
      string addresses = "\r" + cbxAddressTo.Text + "\r";
      foreach (object obj in cbxAddressTo.Items)
      {
        string address = obj.ToString();
        if (!addresses.Contains("\r" + address + "\r"))
          addresses += address + "\r";
      }
      
      addresses = addresses.Substring(1, addresses.Length - 2);
      xi.SetProp("RecentAddresses", addresses);

      string subjects = "\r" + cbxSubject.Text + "\r";
      foreach (object obj in cbxSubject.Items)
      {
        string subject = obj.ToString();
        if (!subjects.Contains("\r" + subject + "\r"))
          subjects += subject + "\r";
      }

      subjects = subjects.Substring(1, subjects.Length - 2);
      xi.SetProp("RecentSubjects", subjects);
      
      xi.SetProp("RecentExport", cbxAttachment.SelectedIndex.ToString());
      return true;
    }

    /// <summary>
    /// Hides attachment settings.
    /// For internal use only.
    /// </summary>
    public void HideAttachmentSettings()
    {
      lblAttachment.Visible = false;
      cbxAttachment.Visible = false;
      btnSettings.Visible = false;
    }

    /// <inheritdoc/>
    public override void Localize()
    {
      base.Localize();
      
      MyRes res = new MyRes("Export,Email");
      Text = res.Get("");
      pgEmail.Text = res.Get("Email");
      lblAddressTo.Text = res.Get("Address");
      lblCC.Text = res.Get("CC");
      lblSubject.Text = res.Get("Subject");
      lblMessage.Text = res.Get("Message");
      lblAttachment.Text= res.Get("Attachment");
      btnSettings.Text = res.Get("Settings");
      pgAccount.Text = res.Get("Account");
      lblAddressFrom.Text = res.Get("Address");
      lblName.Text = res.Get("Name");
      lblTemplate.Text = res.Get("Template");
      lblHost.Text = res.Get("Host");
      lblPort.Text = res.Get("Port");
      lblUserName.Text = res.Get("UserName");
      lblPassword.Text = res.Get("Password");
      cbEnableSSL.Text = res.Get("EnableSSL");
    }

    private void EmailExportForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (DialogResult == DialogResult.OK)
        if (!Done())
          e.Cancel = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailExportForm"/> class.
    /// </summary>
    public EmailExportForm(EmailExport export)
    {
      FExport = export;
      InitializeComponent();
      Localize();
      Init();
    }
  }
}

