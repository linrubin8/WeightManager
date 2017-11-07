using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.IO;
using FastReport.Utils;

namespace FastReport.Export.Email
{
    /// <summary>
    /// Represents the email export.
    /// </summary>
    /// <remarks>
    /// In order to use this class, you need to set up at least the following properties:
    /// <see cref="Address"/>, <see cref="Subject"/>, <see cref="Account"/>. Use the <see cref="Export"/>
    /// property to choose the format of an attachment. If you leave it empty, the attachment will be
    /// in the .FRP format (FastReport prepared report). When you done with settings, call the
    /// <see cref="SendEmail"/> method to send an email.
    /// </remarks>
    /// <example>
    /// This example demonstrates the bare minimum required to send an email.
    /// <code>
    /// EmailExport export = new EmailExport();
    /// export.Account.Address = "my@address.net";
    /// export.Account.Host = "myhost";
    /// export.Address = "recipient@address.net";
    /// export.Subject = "Re: analysis report";
    /// // the report1 report must be prepared at this moment
    /// export.SendEmail(report1);
    /// </code>
    /// </example>
    public class EmailExport : IFRSerializable
  {
    #region Fields
    private string FAddress;
    private string[] FCC;
    private string FSubject;
    private string FMessageBody;
    private ExportBase FExport;
    private EmailSettings FAccount;
    private Report FReport;
    #endregion
    
    #region Properties
    /// <summary>
    /// Gets or sets the recipient's address.
    /// </summary>
    /// <remarks>
    /// This property must contain value in form "john@url.com".
    /// </remarks>
    public string Address
    {
      get { return FAddress; }
      set { FAddress = value; }
    }

    /// <summary>
    /// Gets or sets the carbon copy adresses.
    /// </summary>
    /// <remarks>
    /// This property must contain an array of values in form "john@url.com".
    /// </remarks>
    public string[] CC
    {
      get { return FCC; }
      set { FCC = value; }
    }
    
    /// <summary>
    /// Gets or sets the subject of the message.
    /// </summary>
    public string Subject
    {
      get { return FSubject; }
      set { FSubject = value; }
    }

    /// <summary>
    /// Gets or sets the message body.
    /// </summary>
    public string MessageBody
    {
      get { return FMessageBody; }
      set { FMessageBody = value; }
    }

    /// <summary>
    /// Gets or sets the export filter which will be used to export a report.
    /// </summary>
    /// <remarks>
    /// Set this property to instance of any export filter. When you send the email, the report
    /// will be exported using that export filter. 
    /// <para/>By default, this property is set to <b>null</b>. In this case the report will be send
    /// in .FRP format.
    /// </remarks>
    public ExportBase Export
    {
      get { return FExport; }
      set { FExport = value; }
    }

    /// <summary>
    /// Gets the email account settings such as host, user name, password.
    /// </summary>
    public EmailSettings Account
    {
      get { return FAccount; }
      set 
      { 
        if (value == null)
          throw new ArgumentNullException("Account");
        FAccount = value; 
      }
    }

    /// <summary>
    /// Gets the parent Report object
    /// </summary>
    public Report Report
    {
      get { return FReport; }
    }
    #endregion
    
    #region Public Methods
    /// <summary>
    /// Displays the dialog box in which you can set up all parameters.
    /// </summary>
    /// <returns><b>true</b> if user pressed OK button in the dialog.</returns>
    public DialogResult ShowDialog()
    {
      using (EmailExportForm form = new EmailExportForm(this))
      {
        return form.ShowDialog();
      }
    }

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="reports">Reports that will be sent as attachments.</param>
    /// <remarks>
    /// Before using this method, set up the following properties (it's a bare minimum):
    /// <see cref="Address"/>, <see cref="Subject"/>, <see cref="Account"/>.
    /// <para/>The report that you pass in this method must be prepared using the <b>Prepare</b> method.
    /// </remarks>
    public void SendEmail(params Report[] reports)
    {
      if (reports == null || reports.Length == 0)
        return;

      SmtpClient email = new SmtpClient();
      using (MailMessage message = new MailMessage(new MailAddress(Account.Address, Account.Name), new MailAddress(Address)))
      {
        if (CC != null)
          foreach (string cc in CC)
            message.CC.Add(new MailAddress(cc));

        message.Subject = Subject;
        message.SubjectEncoding = Encoding.UTF8;
        message.Body = MessageBody;
        message.BodyEncoding = Encoding.UTF8;

        foreach (Report report in reports)
        {
          MemoryStream attachStream = new MemoryStream();

          // export the report
          if (Export != null)
          {
            Export.OpenAfterExport = false;
            if (Export.HasMultipleFiles)
              Export.ExportAndZip(report, attachStream);
            else
              Export.Export(report, attachStream);
          }
          else
          {
            report.PreparedPages.Save(attachStream);
          }

          // form an attachment name
          string attachName = report.FileName;
          if (String.IsNullOrEmpty(attachName))
            attachName = "Report";
          else
            attachName = Path.GetFileNameWithoutExtension(attachName);

          string extension = ".fpx";
          if (Export != null)
          {
            extension = Export.FileFilter;
            extension = extension.Substring(extension.LastIndexOf('.'));
            if (Export.HasMultipleFiles)
              extension = ".zip";
          }

          attachName += extension;
          attachStream.Position = 0;

          message.Attachments.Add(new Attachment(attachStream, attachName));
        }

        email.Host = Account.Host;
        email.Port = Account.Port;

        if (!String.IsNullOrEmpty(Account.UserName) && !String.IsNullOrEmpty(Account.Password))
          email.Credentials = new NetworkCredential(Account.UserName, Account.Password);
        if (Account.EnableSSL)
          email.EnableSsl = true;

        email.Send(message);
      }
    }

    /// <inheritdoc/>
    public void Serialize(FRWriter writer)
    {
      writer.WriteStr("Address", Address);
      writer.WriteStr("CC", CC == null ? "" : string.Join(";", CC));
      writer.WriteStr("Subject", Subject);
      writer.WriteStr("MessageBody", MessageBody);

      writer.WriteStr("AccountAddress", Account.Address);
      writer.WriteStr("AccountName", Account.Name);
      writer.WriteStr("AccountHost", Account.Host);
      writer.WriteInt("AccountPort", Account.Port);
      writer.WriteStr("AccountUserName", Account.UserName);
      writer.WriteStr("AccountPassword", Account.Password);
      writer.WriteStr("AccountMessageTemplate", Account.MessageTemplate);
      writer.WriteBool("AccountEnableSSL", Account.EnableSSL);
    }

    /// <inheritdoc/>
    public void Deserialize(FRReader reader)
    {
      Address = reader.ReadStr("Address");
      CC = reader.ReadStr("CC").Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
      Subject = reader.ReadStr("Subject");
      MessageBody = reader.ReadStr("MessageBody");

      Account.Address = reader.ReadStr("AccountAddress");
      Account.Name = reader.ReadStr("AccountName");
      Account.Host = reader.ReadStr("AccountHost");

      int port;
      if (int.TryParse(reader.ReadStr("AccountPort"), out port))
        Account.Port = port;

      Account.UserName = reader.ReadStr("AccountUserName");
      Account.Password = reader.ReadStr("AccountPassword");
      Account.MessageTemplate = reader.ReadStr("AccountMessageTemplate");
      Account.EnableSSL = reader.ReadBool("AccountEnableSSL");
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailExport"/> class with default settings.
    /// </summary>
    public EmailExport(Report report)
    {
        FReport = report;
        FAddress = "";
        FSubject = "";
        FMessageBody = "";
        FAccount = new EmailSettings();        
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailExport"/> class with default settings.
    /// </summary>
    public EmailExport()
    {
      FAddress = "";
      FSubject = "";
      FMessageBody = "";
      FAccount = new EmailSettings();
    }
  }
}
