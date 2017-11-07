using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Forms
{
  internal partial class ReportOptionsForm : BaseDialogForm
  {
    private Report FReport;
    
    public Report Report
    {
      get { return FReport; }
      set { FReport = value; }
    }

    private void ReportOptionsForm_Shown(object sender, EventArgs e)
    {
      // needed for 120dpi mode
      tbDescription.Height = btnLoad.Top - tbDescription.Top - 14;
      tbRefAssemblies.Height = btnAdd.Top - tbRefAssemblies.Top - 12;
      lblScriptNote.Width = tbRefAssemblies.Width;
      tbRecipients.Height = lblSubject.Top - tbRecipients.Top - 8;
      tbMessage.Height = pnEmail.Height - tbMessage.Top - 16;
    }

    private void ReportOptionsForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!Done())
        e.Cancel = true;
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,Images");
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          pbPicture.Image = Image.FromFile(dialog.FileName);
        }
      }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      pbPicture.Image = null;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,Assembly");
        if (dialog.ShowDialog() == DialogResult.OK)
          tbRefAssemblies.Text += "\r\n" + Path.GetFileName(dialog.FileName);
      }
    }

    private void tbPasswordLoad_TextChanged(object sender, EventArgs e)
    {
      tbRetypePassword.Text = "";
    }

    private void rbInherit_CheckedChanged(object sender, EventArgs e)
    {
      btnBrowse.Enabled = rbInherit.Checked;
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,Report");
        if (dialog.ShowDialog() == DialogResult.OK)
          lblBaseName.Text = dialog.FileName;
        else
          lblBaseName.Text = "";  
      }
    }

    private void tbDescription_ButtonClick(object sender, EventArgs e)
    {
      using (StringCollectionEditorForm form = new StringCollectionEditorForm())
      {
        form.PlainText = tbDescription.Text;
        if (form.ShowDialog() == DialogResult.OK)
          tbDescription.Text = form.PlainText;
      }
    }

    private void Init()
    {
      cbDoublePass.Checked = Report.DoublePass;
      cbCompress.Checked = Report.Compressed;
      cbUseFileCache.Checked = Report.UseFileCache;
      cbConvertNulls.Checked = Report.ConvertNulls;
      cbxTextQuality.SelectedIndex = (int)Report.TextQuality;
      cbSmoothGraphics.Checked = Report.SmoothGraphics;

      tbName.Text = Report.ReportInfo.Name;
      tbAuthor.Text = Report.ReportInfo.Author;
      tbVersion.Text = Report.ReportInfo.Version;
      tbDescription.Text = Report.ReportInfo.Description;
      pbPicture.Image = Report.ReportInfo.Picture;
      cbSavePreviewPicture.Checked = Report.ReportInfo.SavePreviewPicture;
      lblCreated1.Text = Report.ReportInfo.Created.ToString();
      lblModified1.Text = Report.ReportInfo.Modified.ToString();
      
      rbC.Checked = Report.ScriptLanguage == Language.CSharp;
      rbVB.Checked = Report.ScriptLanguage == Language.Vb;
      tbRefAssemblies.Lines = Report.ReferencedAssemblies;
      
      tbPassword.Text = Report.Password;
      tbRetypePassword.Text = tbPassword.Text;
      
      if (Report.IsAncestor)
      {
        lblInheritance.Text = Res.Get("Forms,ReportOptions,Inherited") + "\r\n" + Report.BaseReport;
      }
      else
      {
        lblInheritance.Text = Res.Get("Forms,ReportOptions,NotInherited");
        rbDetach.Enabled = false;
      }

      tbRecipients.Text = Report.EmailSettings.RecipientsText;
      tbSubject.Text = Report.EmailSettings.Subject;
      tbMessage.Text = Report.EmailSettings.Message;
    }

    private bool Done()
    {
      if (DialogResult == DialogResult.OK)
      {
        if (tbPassword.Text != tbRetypePassword.Text)
        {
          FRMessageBox.Error(Res.Get("Forms,ReportOptions,PasswordError"));
          pcPages.ActivePage = pnSecurity;
          tbRetypePassword.Focus();
          return false;
        }

        Report.DoublePass = cbDoublePass.Checked;
        Report.Compressed = cbCompress.Checked;
        Report.UseFileCache = cbUseFileCache.Checked;
        Report.ConvertNulls = cbConvertNulls.Checked;
        Report.TextQuality = (TextQuality)cbxTextQuality.SelectedIndex;
        Report.SmoothGraphics = cbSmoothGraphics.Checked;

        Report.ReportInfo.Name = tbName.Text;
        Report.ReportInfo.Author = tbAuthor.Text;
        Report.ReportInfo.Version = tbVersion.Text;
        Report.ReportInfo.Description = tbDescription.Text;
        Report.ReportInfo.Picture = pbPicture.Image;
        Report.ReportInfo.SavePreviewPicture = cbSavePreviewPicture.Checked;
        
        Report.ScriptLanguage = rbC.Checked ? Language.CSharp : Language.Vb;
        Report.ReferencedAssemblies = tbRefAssemblies.Lines;
        Report.Password = tbPassword.Text;

        if (rbDetach.Checked)
        {
          Report.BaseReport = "";
        }
        else if (rbInherit.Checked && !String.IsNullOrEmpty(lblBaseName.Text.Trim()))
        {
          Report.BaseReport = lblBaseName.Text;
        }

        Report.EmailSettings.RecipientsText = tbRecipients.Text;
        Report.EmailSettings.Subject = tbSubject.Text;
        Report.EmailSettings.Message = tbMessage.Text;
      }
      return true;
    }
    
    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,ReportOptions");
      Text = res.Get("");

      pnGeneral.Text = res.Get("General");
      pnDescription.Text = res.Get("Description");
      pnScript.Text = res.Get("Script");
      pnSecurity.Text = res.Get("Security");
      pnInheritance.Text = res.Get("Inheritance");
      pnEmail.Text = res.Get("Email");

      cbDoublePass.Text = res.Get("DoublePass");
      cbCompress.Text = res.Get("Compress");
      cbUseFileCache.Text = res.Get("UseFileCache");
      cbConvertNulls.Text = res.Get("ConvertNulls");
      lblTextQuality.Text = res.Get("TextQuality");
      cbxTextQuality.Items.AddRange(new string[] {
        res.Get("QualityDefault"), res.Get("QualityRegular"), 
        res.Get("QualityClearType"), res.Get("QualityAntiAlias") });
      cbSmoothGraphics.Text = res.Get("SmoothGraphics");

      lblName.Text = res.Get("Name");
      lblAuthor.Text = res.Get("Author");
      lblDescription.Text = res.Get("Description1");
      lblVersion.Text = res.Get("Version");
      lblPicture.Text = res.Get("Picture");
      btnLoad.Text = res.Get("Load");
      btnClear.Text = res.Get("Clear");
      cbSavePreviewPicture.Text = res.Get("SavePreviewPicture");
      lblCreated.Text = res.Get("Created");
      lblModified.Text = res.Get("Modified");
      tbDescription.Image = Res.GetImage(68);
      
      lblLanguage.Text = res.Get("Language");
      lblScriptNote.Text = res.Get("Note");
      lblRefAssemblies.Text = res.Get("RefAssemblies");
      btnAdd.Text = res.Get("Add");

      lblPassword.Text = res.Get("Password");
      lblRetypePassword.Text = res.Get("RetypePassword");
      
      lblChooseInheritance.Text = res.Get("Choose");
      rbDontChange.Text = res.Get("DontChange");
      rbDetach.Text = res.Get("Detach");
      rbInherit.Text = res.Get("Inherit");
      btnBrowse.Text = res.Get("Browse");

      lblRecipients.Text = res.Get("Recipients");
      lblSubject.Text = res.Get("Subject");
      lblMessage.Text = res.Get("Message");
    }

    public ReportOptionsForm(Report report)
    {
        FReport = report;
        InitializeComponent();
        Localize();
      
        Init();

        // apply Right to Left layout
        if (Config.RightToLeft)
        {
            RightToLeft = RightToLeft.Yes;

            // General tab
            cbDoublePass.Left = pnGeneral.Width - cbDoublePass.Left - cbDoublePass.Width;
            cbDoublePass.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbCompress.Left = pnGeneral.Width - cbCompress.Left - cbCompress.Width;
            cbCompress.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbUseFileCache.Left = pnGeneral.Width - cbUseFileCache.Left - cbUseFileCache.Width;
            cbUseFileCache.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbConvertNulls.Left = pnGeneral.Width - cbConvertNulls.Left - cbConvertNulls.Width;
            cbConvertNulls.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblTextQuality.Left = pnGeneral.Width - lblTextQuality.Left - lblTextQuality.Width;
            lblTextQuality.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbxTextQuality.Left = pnGeneral.Width - cbxTextQuality.Left - cbxTextQuality.Width;
            cbxTextQuality.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbSmoothGraphics.Left = pnGeneral.Width - cbSmoothGraphics.Left - cbSmoothGraphics.Width;
            cbSmoothGraphics.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            
            // Description tab
            lblName.Left = pnDescription.Width - lblName.Left - lblName.Width;
            lblName.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbName.Left = pnDescription.Width - tbName.Left - tbName.Width;
            tbName.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblAuthor.Left = pnDescription.Width - lblAuthor.Left - lblAuthor.Width;
            lblAuthor.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbAuthor.Left = pnDescription.Width - tbAuthor.Left - tbAuthor.Width;
            tbAuthor.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblVersion.Left = pnDescription.Width - lblVersion.Left - lblVersion.Width;
            lblVersion.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbVersion.Left = pnDescription.Width - tbVersion.Left - tbVersion.Width;
            tbVersion.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblDescription.Left = pnDescription.Width - lblDescription.Left - lblDescription.Width;
            lblDescription.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbDescription.Left = pnDescription.Width - tbDescription.Left - tbDescription.Width;
            tbDescription.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblPicture.Left = pnDescription.Width - lblPicture.Left - lblPicture.Width;
            lblPicture.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pbPicture.Left = pnDescription.Width - pbPicture.Left - pbPicture.Width;
            pbPicture.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            btnLoad.Left = pnDescription.Width - btnLoad.Left - btnLoad.Width;
            btnClear.Left = pnDescription.Width - btnClear.Left - btnClear.Width;
            cbSavePreviewPicture.Left = pnDescription.Width - cbSavePreviewPicture.Left - cbSavePreviewPicture.Width;
            cbSavePreviewPicture.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblCreated.Left = pnDescription.Width - lblCreated.Left - lblCreated.Width;
            lblCreated.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblCreated1.Left = pnDescription.Width - lblCreated1.Left - lblCreated1.Width;
            lblCreated1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblModified.Left = pnDescription.Width - lblModified.Left - lblModified.Width;
            lblModified.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblModified1.Left = pnDescription.Width - lblModified1.Left - lblModified1.Width;
            lblModified1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            // Script tab
            lblLanguage.Left = pnScript.Width - lblLanguage.Left - lblLanguage.Width;
            lblLanguage.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            rbC.Left = pnScript.Width - rbC.Left - rbC.Width;
            rbC.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            rbVB.Left = pnScript.Width - rbVB.Left - rbVB.Width;
            rbVB.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblScriptNote.Left = pnScript.Width - lblScriptNote.Left - lblScriptNote.Width;
            lblScriptNote.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblRefAssemblies.Left = pnScript.Width - lblRefAssemblies.Left - lblRefAssemblies.Width;
            lblRefAssemblies.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbRefAssemblies.Left = pnScript.Width - tbRefAssemblies.Left - tbRefAssemblies.Width;
            tbRefAssemblies.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            btnAdd.Left = pnScript.Width - btnAdd.Left - btnAdd.Width;

            // Security tab
            lblPassword.Left = pnSecurity.Width - lblPassword.Left - lblPassword.Width;
            lblPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbPassword.Left = pnSecurity.Width - tbPassword.Left - tbPassword.Width;
            tbPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblRetypePassword.Left = pnSecurity.Width - lblRetypePassword.Left - lblRetypePassword.Width;
            lblRetypePassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbRetypePassword.Left = pnSecurity.Width - tbRetypePassword.Left - tbRetypePassword.Width;
            tbRetypePassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            // Inheritance tab
            lblInheritance.Left = pnInheritance.Width - lblInheritance.Left - lblInheritance.Width;
            lblInheritance.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblChooseInheritance.Left = pnInheritance.Width - lblChooseInheritance.Left - lblChooseInheritance.Width;
            lblChooseInheritance.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            rbDontChange.Left = pnInheritance.Width - rbDontChange.Left - rbDontChange.Width;
            rbDontChange.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            rbDetach.Left = pnInheritance.Width - rbDetach.Left - rbDetach.Width;
            rbDetach.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            // Email tab
            lblRecipients.Left = pnEmail.Width - lblRecipients.Left - lblRecipients.Width;
            lblRecipients.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblSubject.Left = pnEmail.Width - lblSubject.Left - lblSubject.Width;
            lblSubject.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblMessage.Left = pnEmail.Width - lblMessage.Left - lblMessage.Width;
            lblMessage.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

            btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
            btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
        }
    }
  }
}
