using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FastReport.Utils;
using FastReport.Design.PageDesigners.Page;
using FastReport.TypeConverters;

namespace FastReport.Forms
{
  internal partial class LabelWizardForm : BaseDialogForm
  {
    private XmlDocument FLabels;
    private Report FReport;

    public string SelectedManufacturer
    {
      get { return (string)cbxLabels.SelectedItem; }
    }

    public string SelectedLabelName
    {
      get { return SelectedLabelParameters.GetProp("Name"); }
    }

    public XmlItem SelectedLabelParameters
    {
      get { return lbxLabels.SelectedItem as XmlItem; }
    }

    private void cbxLabels_SelectedIndexChanged(object sender, EventArgs e)
    {
      // find the corresponding xml node
      XmlItem labelItem = null;
      string label = (string)cbxLabels.SelectedItem;
      for (int i = 0; i < FLabels.Root.Count; i++)
      {
        if (FLabels.Root[i].GetProp("Name") == label)
        {
          labelItem = FLabels.Root[i];
          break;
        }
      }

      lbxLabels.Items.Clear();
      if (labelItem != null)
      {
        for (int i = 0; i < labelItem.Count; i++)
        {
          lbxLabels.Items.Add(labelItem[i]);
        }
      }

      if (lbxLabels.Items.Count > 0)
        lbxLabels.SelectedIndex = 0;
      else
        lbxLabels_SelectedIndexChanged(null, EventArgs.Empty);
      
      btnOk.Enabled = lbxLabels.Items.Count > 0;  
      btnDelete.Visible = cbxLabels.SelectedIndex == cbxLabels.Items.Count - 1;
    }

    private void lbxLabels_DrawItem(object sender, DrawItemEventArgs e)
    {
      e.DrawBackground();
      if (e.Index >= 0)
      {
        XmlItem item = lbxLabels.Items[e.Index] as XmlItem;
        using (Brush brush = new SolidBrush(e.ForeColor))
        {
          e.Graphics.DrawString(item.GetProp("Name"), e.Font, brush, e.Bounds.X, e.Bounds.Y);
        }
      }
    }

    private void lbxLabels_SelectedIndexChanged(object sender, EventArgs e)
    {
      XmlItem item = lbxLabels.SelectedItem as XmlItem;
      if (item != null)
      {
        float width = Converter.StringToFloat(item.GetProp("Width")) * Units.Inches;
        float height = Converter.StringToFloat(item.GetProp("Height")) * Units.Inches;
        float paperWidth = Converter.StringToFloat(item.GetProp("PaperWidth")) * Units.Inches;
        float paperHeight = Converter.StringToFloat(item.GetProp("PaperHeight")) * Units.Inches;
        string rows = item.GetProp("Rows");
        if (String.IsNullOrEmpty(rows))
          rows = "1";
        string columns = item.GetProp("Columns");
        if (String.IsNullOrEmpty(columns))
          columns = "1";

        lblSize1.Text = Converter.ToString(width, typeof(UnitsConverter)) + " x " +
          Converter.ToString(height, typeof(UnitsConverter));
        lblPaperSize1.Text = Converter.ToString(paperWidth, typeof(UnitsConverter)) + " x " +
          Converter.ToString(paperHeight, typeof(UnitsConverter));
        lblRows1.Text = rows;
        lblColumns1.Text = columns;
      }

      btnDelete.Enabled = item != null;
    }

    private void btnCustom_Click(object sender, EventArgs e)
    {
      using (CustomLabelForm form = new CustomLabelForm())
      {
        XmlItem customManufacturer = FLabels.Root[FLabels.Root.Count - 1];
        form.Init(Res.Get("Forms,LabelWizard,Label") + (customManufacturer.Count + 1).ToString());
        if (form.ShowDialog() == DialogResult.OK)
        {
          customManufacturer.AddItem(form.LabelParameters);
          cbxLabels.SelectedIndex = cbxLabels.Items.Count - 1;
          cbxLabels_SelectedIndexChanged(this, EventArgs.Empty);
          lbxLabels.SelectedIndex = lbxLabels.Items.Count - 1;
        }
      }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      XmlItem customManufacturer = FLabels.Root[FLabels.Root.Count - 1];
      customManufacturer.Items.RemoveAt(lbxLabels.SelectedIndex);
      cbxLabels.SelectedIndex = cbxLabels.Items.Count - 1;
      cbxLabels_SelectedIndexChanged(this, EventArgs.Empty);
      lbxLabels.SelectedIndex = lbxLabels.Items.Count - 1;
    }

    private void LabelWizardForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (DialogResult == DialogResult.OK)
        Done();
    }

    private void Init()
    {
      // load standard labels
      FLabels = new XmlDocument();
      using (Stream labelStream = ResourceLoader.UnpackStream("labels.dat"))
      {
        FLabels.Load(labelStream);
      }

      // localize the manufacturers/labels
      MyRes manRes = new MyRes("Forms,LabelWizard,Manufacturers");
      MyRes labelRes = new MyRes("Forms,LabelWizard,Labels");
      for (int i = 0; i < FLabels.Root.Count; i++)
      {
        XmlItem xi = FLabels.Root[i];
        xi.SetProp("Name", manRes.Get(xi.GetProp("Name")));
        for (int j = 0; j < xi.Count; j++)
        {
          XmlItem xi1 = xi[j];
          string labelName = xi1.GetProp("Name");
          if (labelName.Contains("["))
          {
            int start = labelName.IndexOf('[');
            int end = labelName.IndexOf(']');
            string localizedName = labelName.Substring(start + 1, end - start - 1);
            localizedName = labelRes.Get(localizedName);
            labelName = labelName.Remove(start, end - start + 1).Insert(start, localizedName);
            xi1.SetProp("Name", labelName);
          }
        }
      }
      
      // add user-defined labels
      XmlItem customManufacturer = FLabels.Root.Add();
      customManufacturer.SetProp("Name", Res.Get("Forms,LabelWizard,Custom"));

      // get the list of user labels from config file
      XmlItem configLabels = Config.Root.FindItem("LabelWizard");
      for (int i = 0; i < configLabels.Count; i++)
      {
        XmlItem addItem = customManufacturer.Add();
                configLabels[i].CopyPropsTo(addItem);
      }
      
      // make a list of manufacturers
      lbxLabels.ItemHeight = DrawUtils.DefaultItemHeight;
      for (int i = 0; i < FLabels.Root.Count; i++)
      {
        cbxLabels.Items.Add(FLabels.Root[i].GetProp("Name"));
      }
      cbxLabels.DropDownHeight = cbxLabels.ItemHeight * cbxLabels.Items.Count;

      // position to last used items
      int index = cbxLabels.Items.IndexOf(configLabels.GetProp("Manufacturer"));
      if (index >= 0)
      {
        cbxLabels.SelectedIndex = index;

        for (int i = 0; i < lbxLabels.Items.Count; i++)
        {
          XmlItem item = lbxLabels.Items[i] as XmlItem;
          if (item.GetProp("Name") == configLabels.GetProp("Label"))
          {
            lbxLabels.SelectedIndex = i;
            break;
          }
        }
      }
      else
        cbxLabels.SelectedIndex = 0;
    }
    
    private void Done()
    {
      // update label parameters and report page layout
      XmlItem labelParameters = lbxLabels.SelectedItem as XmlItem;
      SetReportPageLayout(labelParameters, FReport.Pages[0] as ReportPage);

      // save user-defined labels to config file
      XmlItem customManufacturer = FLabels.Root[FLabels.Root.Count - 1];
      XmlItem configLabels = Config.Root.FindItem("LabelWizard");
      configLabels.Clear();
      for (int i = 0; i < customManufacturer.Count; i++)
      {
        XmlItem addItem = configLabels.Add();
        addItem.Name = "CustomLabel";
                customManufacturer[i].CopyPropsTo(addItem);
      }

      // write last used items
      configLabels.SetProp("Manufacturer", SelectedManufacturer);
      configLabels.SetProp("Label", SelectedLabelName);

      // tell the designer to reflect changes
      FReport.Designer.SetModified(null, "ChangeReport");
    }

    private void SetReportPageLayout(XmlItem labelParameters, ReportPage page)
    {
      float paperWidth = Converter.StringToFloat(labelParameters.GetProp("PaperWidth"), true) * 25.4f;
      float paperHeight = Converter.StringToFloat(labelParameters.GetProp("PaperHeight"), true) * 25.4f;
      float leftMargin = Converter.StringToFloat(labelParameters.GetProp("LeftMargin"), true) * 25.4f;
      float topMargin = Converter.StringToFloat(labelParameters.GetProp("TopMargin"), true) * 25.4f;

      float labelWidth = Converter.StringToFloat(labelParameters.GetProp("Width"), true) * 25.4f;
      float labelHeight = Converter.StringToFloat(labelParameters.GetProp("Height"), true) * 25.4f;
      int rows = (int)Converter.StringToFloat(labelParameters.GetProp("Rows"), true);
      if (rows == 0)
        rows = 1;
      int columns = (int)Converter.StringToFloat(labelParameters.GetProp("Columns"), true);
      if (columns == 0)
        columns = 1;
      float rowGap = Converter.StringToFloat(labelParameters.GetProp("RowGap"), true) * 25.4f;
      float columnGap = Converter.StringToFloat(labelParameters.GetProp("ColumnGap"), true) * 25.4f;

      // setup paper
      page.Clear();
      page.Landscape = labelParameters.GetProp("Landscape") == "true";
      page.PaperWidth = paperWidth;
      page.PaperHeight = paperHeight;
      page.LeftMargin = leftMargin;
      page.RightMargin = 0;
      page.TopMargin = topMargin;
      page.BottomMargin = 0;

      // setup columns
      page.Columns.Count = columns;
      page.Columns.Width = labelWidth;
      page.Columns.Positions.Clear();
      for (int i = 0; i < columns; i++)
      {
        page.Columns.Positions.Add(i * (labelWidth + columnGap));
      }

      // setup data band
      DataBand band = new DataBand();
      page.Bands.Add(band);
      band.CreateUniqueName();
      band.Width = labelWidth * Units.Millimeters;
      band.Height = labelHeight * Units.Millimeters;
      // setup row gap (use child band)
      if (rowGap > 0)
      {
        band.Child = new ChildBand();
        band.Child.CreateUniqueName();
        band.Child.Height = rowGap * Units.Millimeters;
      }
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,LabelWizard");
      Text = res.Get("");
      lblHint.Text = res.Get("Hint");
      gbLabel.Text = res.Get("Label");
      lblManufacturer.Text = res.Get("Manufacturer");
      lblProduct.Text = res.Get("Product");
      gbParameters.Text = res.Get("Parameters");
      lblSize.Text = res.Get("Size");
      lblPaperSize.Text = res.Get("PaperSize");
      lblRows.Text = res.Get("Rows");
      lblColumns.Text = res.Get("Columns");
      btnCustom.Text = res.Get("CustomLabel");
      btnDelete.Text = res.Get("Delete");
    }
    
    public void InitWizard(Report report)
    {
      FReport = report;
    }

    public LabelWizardForm()
    {
      InitializeComponent();
      Init();
      Localize();
    }
  }
}

