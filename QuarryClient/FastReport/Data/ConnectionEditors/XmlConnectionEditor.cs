using System;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.Data.ConnectionEditors
{
  internal partial class XmlConnectionEditor : ConnectionEditorBase
  {
    private void Localize()
    {
      MyRes res = new MyRes("ConnectionEditors,Xml");
      gbSelect.Text = Res.Get("ConnectionEditors,Common,Database");
      lblSelectXsd.Text = res.Get("Xsd");
      lblSelectXml.Text = res.Get("Xml");
      tbXsd.Image = Res.GetImage(1);
      tbXml.Image = Res.GetImage(1);
    }

    private void tbXsd_ButtonClick(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,XsdFile");
        if (dialog.ShowDialog() == DialogResult.OK)
          tbXsd.Text = dialog.FileName;
      }
    }

    private void tbXml_ButtonClick(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,XmlFile");
        if (dialog.ShowDialog() == DialogResult.OK)
          tbXml.Text = dialog.FileName;
      }
    }

    protected override string GetConnectionString()
    {
      XmlConnectionStringBuilder builder = new XmlConnectionStringBuilder();
      builder.XsdFile = tbXsd.Text;
      builder.XmlFile = tbXml.Text;
      return builder.ToString();
    }

    protected override void SetConnectionString(string value)
    {
      XmlConnectionStringBuilder builder = new XmlConnectionStringBuilder(value);
      tbXsd.Text = builder.XsdFile;
      tbXml.Text = builder.XmlFile;
    }

    public XmlConnectionEditor()
    {
        InitializeComponent();
        Localize();

        // apply Right to Left layout
        if (Config.RightToLeft)
        {
            RightToLeft = RightToLeft.Yes;

            // move components to other side
            lblSelectXml.Left = gbSelect.Width - lblSelectXml.Left - lblSelectXml.Width;
            lblSelectXml.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblSelectXsd.Left = gbSelect.Width - lblSelectXsd.Left - lblSelectXsd.Width;
            lblSelectXsd.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
        }
    }
  }
}

