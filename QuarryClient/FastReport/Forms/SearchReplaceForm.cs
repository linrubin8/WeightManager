using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Design;
using FastReport.Design.PageDesigners.Code;
using FastReport.Editor;

namespace FastReport.Forms
{
  internal partial class SearchReplaceForm : BaseDialogForm
  {
    private Designer FDesigner;
    private bool FReplace;
    private bool FFirstSearch;
    private int FIndex;
    
    private static string[] FLastSearch;
    private static string[] FLastReplace;
    private static bool FMatchCase;
    private static bool FMatchWholeWord;
    
    public bool Replace
    {
      get { return FReplace; }
      set
      {
        FReplace = value;
        
        pnReplace.Visible = value;
        pnReplaceButton.Visible = value;
        PerformLayout();
        ClientSize = new Size(gbOptions.Right + gbOptions.Left, (value ? pnReplaceButton.Bottom : pnFindButton.Bottom) + 4);
      }
    }

    private List<CharacterRange> FindText(string text, int startIndex)
    {
      string textToFind = cbxFind.Text;
      bool matchCase = cbMatchCase.Checked;
      bool wholeWord = cbMatchWholeWord.Checked;
      
      List<CharacterRange> ranges = new List<CharacterRange>();
      string nonWordChars = " `-=[];',./~!@#$%^&*()+{}:\"<>?\\|";

      while (startIndex < text.Length)
      {
        int i = text.IndexOf(textToFind, startIndex, matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
        if (i >= 0)
        {
          bool skip = false;
          if (wholeWord)
          {
            if (i > 0 && nonWordChars.IndexOf(text[i - 1]) != -1)
              skip = true;
            if (i < text.Length - 1 && nonWordChars.IndexOf(text[i + 1]) != -1)
              skip = true;
          }
          if (!skip)
            ranges.Add(new CharacterRange(i, textToFind.Length));
          startIndex = i + textToFind.Length;
        }
        else
          break;
      }
      
      return ranges;
    }

    private void FindNext(bool replace, bool replaceAll)
    {
      bool found = false;
      
      if (FDesigner.ActiveReportTab.ActivePage == null)
      {
        // search in the code tab
        SyntaxEdit Edit = (FDesigner.ActiveReportTab.ActivePageDesigner as CodePageDesigner).Edit;
        if (FFirstSearch)
          Edit.FirstSearch = true;
          
        SearchOptions options = SearchOptions.None;
        if (cbMatchCase.Checked)
          options |= SearchOptions.CaseSensitive;
        if (cbMatchWholeWord.Checked)
          options |= SearchOptions.WholeWordsOnly;
        
        if (Edit.Selection.SelectedText == cbxFind.Text && replace && !replaceAll)
        {
          Edit.Selection.SelectedText = cbxReplace.Text;
          found = true;
        }
        else
        {
          while (true)
          {
            if (Edit.Find(cbxFind.Text, options))
            {
              found = true;

              if (replace)
                Edit.Selection.SelectedText = cbxReplace.Text;
                
              if (!replaceAll)
                break;  
            }
            else
              break;
          }
        }
      }
      else
      {
        ObjectCollection allObjects = FDesigner.ActiveReportTab.ActivePage.AllObjects;
        for (; FIndex < allObjects.Count; FIndex++)
        {
          Base c = allObjects[FIndex];
          string text = "";
          if (c is TextObject)
            text = (c as TextObject).Text;
          else if (c is RichObject)
            text = (c as RichObject).Text;  

          List<CharacterRange> ranges = FindText(text, 0);
          if (ranges.Count > 0)
          {
            found = true;
            
            if (!replaceAll)
            {
              FDesigner.SelectedObjects.Clear();
              FDesigner.SelectedObjects.Add(c);
              FDesigner.SelectionChanged(null);
            }

            if (replace)
            {
              int correction = 0;
              foreach (CharacterRange range in ranges)
              {
                text = text.Remove(range.First + correction, range.Length);
                text = text.Insert(range.First + correction, cbxReplace.Text);
                correction += cbxReplace.Text.Length - range.Length;
              }

              if (c is TextObject)
                (c as TextObject).Text = text;
              else if (c is RichObject)
                (c as RichObject).Text = text;
            }
            
            if (!replaceAll)
            {
              FIndex++;
              break;
            }  
          }
        }
      }

      if (found && replace)
        FDesigner.SetModified(null, "Change");

      if (FFirstSearch)
      {
        if (!found)
          FRMessageBox.Information(Res.Get("Forms,SearchReplace,NotFound"));
        FFirstSearch = false;
      }
      else
      {
        if (!found)
          FRMessageBox.Information(Res.Get("Forms,SearchReplace,NoMoreFound"));
      }
    }

    private void cbxFind_TextChanged(object sender, EventArgs e)
    {
      bool enabled = !String.IsNullOrEmpty(cbxFind.Text);
      btnFindNext.Enabled = enabled;
      btnReplace.Enabled = enabled;
      btnReplaceAll.Enabled = enabled;
    }
    
    private void btnFindNext_Click(object sender, EventArgs e)
    {
      FindNext(false, false);
    }

    private void btnReplace_Click(object sender, EventArgs e)
    {
      FindNext(true, false);
    }

    private void btnReplaceAll_Click(object sender, EventArgs e)
    {
      FindNext(true, true);
    }

    private void SearchReplaceForm_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Escape)
        Close();
    }

    private void SearchReplaceForm_FormClosed(object sender, FormClosedEventArgs e)
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
      if (FLastReplace != null)
      {
        foreach (string s in FLastReplace)
        {
          cbxReplace.Items.Add(s);
        }
      }
      cbMatchCase.Checked = FMatchCase;
      cbMatchWholeWord.Checked = FMatchWholeWord;
      
      FFirstSearch = true;
      FIndex = 0;
      cbxFind_TextChanged(this, EventArgs.Empty);
      Config.RestoreFormState(this);
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

      foreach (object item in cbxReplace.Items)
      {
        items.Add(item.ToString());
      }
      if (items.IndexOf(cbxReplace.Text) == -1)
        items.Add(cbxReplace.Text);
      FLastReplace = items.ToArray();

      FMatchCase = cbMatchCase.Checked;
      FMatchWholeWord = cbMatchWholeWord.Checked;
      Config.SaveFormState(this);
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,SearchReplace");
      Text = res.Get("");
      
      lblFind.Text = res.Get("Find");
      lblReplace.Text = res.Get("Replace");
      gbOptions.Text = res.Get("Options");
      cbMatchCase.Text = res.Get("MatchCase");
      cbMatchWholeWord.Text = res.Get("MatchWholeWord");
      btnFindNext.Text = res.Get("FindNext");
      btnReplace.Text = res.Get("ReplaceBtn");
      btnReplaceAll.Text = res.Get("ReplaceAll");
    }

    public SearchReplaceForm(Designer designer)
    {
      FDesigner = designer;
      InitializeComponent();
      Localize();
      Init();
    }
  }
}

