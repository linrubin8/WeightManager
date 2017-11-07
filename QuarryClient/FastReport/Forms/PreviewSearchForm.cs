using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Preview;

namespace FastReport.Forms
{
  internal partial class PreviewSearchForm : BaseDialogForm
  {
    private PreviewTab FPreviewTab;
    private bool FFirstSearch;
    private static string[] FLastSearch;
    private static bool FMatchCase;
    private static bool FMatchWholeWord;
    
    public PreviewTab PreviewTab
    {
      get { return FPreviewTab; }
      set { FPreviewTab = value; }
    }

    private void cbxFind_TextChanged(object sender, EventArgs e)
    {
      FFirstSearch = true;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      if (cbxFind.Text == "")
        return;

      btnOk.Enabled = false;
      btnCancel.Enabled = false;
      
      if (FFirstSearch)
      {
        if (!PreviewTab.Find(cbxFind.Text, cbMatchCase.Checked, cbMatchWholeWord.Checked))
          FRMessageBox.Information(Res.Get("Forms,SearchReplace,NotFound"));
        else
          FFirstSearch = false;
      }  
      else
      {
        if (!PreviewTab.FindNext(cbxFind.Text, cbMatchCase.Checked, cbMatchWholeWord.Checked))
        {
          FRMessageBox.Information(Res.Get("Forms,SearchReplace,NoMoreFound"));
          FFirstSearch = true;
        }
      }
      
      btnOk.Enabled = true;
      btnCancel.Enabled = true;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void PreviewSearchForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Done();
    }
    
    private void Init()
    {
      if (FLastSearch != null)
      {
        foreach (string s in FLastSearch)
        {
          cbxFind.Items.Add(s);
        }
      }  
      cbMatchCase.Checked = FMatchCase;
      cbMatchWholeWord.Checked = FMatchWholeWord;
      FFirstSearch = true;
    }
    
    private void Done()
    {
      List<string> items = new List<string>();
      foreach (object item in cbxFind.Items)
      {
        items.Add(item.ToString());
      }
      if (items.IndexOf(cbxFind.Text) == -1)
        items.Add(cbxFind.Text);
      FLastSearch = items.ToArray();
      FMatchCase = cbMatchCase.Checked;
      FMatchWholeWord = cbMatchWholeWord.Checked;
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,SearchReplace");
      Text = Res.Get("Forms,PreviewSearch");
      lblFind.Text = res.Get("Find");
      gbOptions.Text = res.Get("Options");
      cbMatchCase.Text = res.Get("MatchCase");
      cbMatchWholeWord.Text = res.Get("MatchWholeWord");
      btnOk.Text = res.Get("FindNext");
      btnCancel.Text = Res.Get("Buttons,Close");
    }

    public PreviewSearchForm()
    {
      InitializeComponent();
      Localize();
      Init();
    }
  }
}

