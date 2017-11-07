using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FastReport.Utils;

namespace FastReport.Forms
{
  internal partial class StringCollectionEditorForm : BaseDialogForm
  {
    private IList FList;
    
    public IList List
    {
      get { return FList; }
      set 
      { 
        FList = value;
        tbLines.Text = Converter.IListToString(FList);
      }
    }
    
    public string PlainText
    {
      get { return tbLines.Text; }
      set { tbLines.Text = value; }
    }

    private void StringCollectionEditorForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (DialogResult == DialogResult.OK && FList != null)
        Converter.StringToIList(tbLines.Text, FList);
    }

    private void StringCollectionEditorForm_Shown(object sender, EventArgs e)
    {
      // needed for 120dpi mode
      tbLines.Width = ClientSize.Width - tbLines.Left * 2;
      tbLines.Height = btnOk.Top - tbLines.Top * 2;
    }

    public override void Localize()
    {
      base.Localize();
      Text = Res.Get("Forms,StringCollectionEditor");
    }
    
    public StringCollectionEditorForm()
    {
      InitializeComponent();
      Localize();
    }
  }
}

