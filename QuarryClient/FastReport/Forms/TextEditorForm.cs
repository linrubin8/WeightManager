using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Data;
using FastReport.Code;
using System.Reflection;
using FastReport.Controls;

namespace FastReport.Forms
{
  internal partial class TextEditorForm : BaseDialogForm
  {
    private Report FReport;
    private static List<string> FExpandedNodes;
    private string FBrackets;
    
    public string ExpressionText
    {
      get { return tbText.Text; }
      set { tbText.Text = value; }
    }
    
    public string Brackets
    {
      get { return FBrackets; }
      set { FBrackets = value; }
    }
    
    private string GetTextWithBrackets()
    {
      string text = tvData.SelectedItem;
      string[] brackets = Brackets.Split(new char[] { ',' });
      // this check is needed if Brackets property is not "[,]"
      if (InsideBrackets(tbText.SelectionStart))
      {
        if (tvData.SelectedItemType == DataTreeSelectedItemType.Function ||
          tvData.SelectedItemType == DataTreeSelectedItemType.DialogControl)
          return text;
        return "[" + text + "]";
      }
      return brackets[0] + text + brackets[1];
    }
    
    private bool InsideBrackets(int pos)
    {
      string[] brackets = Brackets.Split(new char[] { ',' });
      FindTextArgs args = new FindTextArgs();
      args.Text = new FastString(tbText.Text);
      args.OpenBracket = brackets[0];
      args.CloseBracket = brackets[1];
      args.StartIndex = pos;
      return CodeUtils.IndexInsideBrackets(args);
    }
    
    private void tbText_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter && e.Control)
        DialogResult = DialogResult.OK;
      else if (e.KeyCode == Keys.A && e.Control)
        tbText.SelectAll();
    }

    private void tvData_ItemDrag(object sender, ItemDragEventArgs e)
    {
      tvData.SelectedNode = e.Item as TreeNode;
      if (tvData.SelectedItem != "")
        tvData.DoDragDrop(e.Item, DragDropEffects.Move);
      else
        tvData.DoDragDrop(e.Item, DragDropEffects.None);
    }

    private void tbText_DragOver(object sender, DragEventArgs e)
    {
      int index = tbText.GetCharIndexFromPosition(tbText.PointToClient(new Point(e.X, e.Y)));
      if (index == tbText.Text.Length - 1)
        index++;
      tbText.Focus();
      tbText.Select(index, 0);
      e.Effect = e.AllowedEffect;
    }

    private void tbText_DragDrop(object sender, DragEventArgs e)
    {
      tbText.SelectedText = GetTextWithBrackets();
      tbText.Focus();
    }

    private void tvData_AfterSelect(object sender, TreeViewEventArgs e)
    {
      bool descrVisible = tvData.SelectedNode != null &&
        (tvData.SelectedNode.Tag is MethodInfo || tvData.SelectedNode.Tag is SystemVariable);
      expandableSplitter1.Visible = descrVisible;
      lblDescription.Visible = descrVisible;

      if (descrVisible)
        lblDescription.ShowDescription(FReport, tvData.SelectedNode.Tag);
    }

    private void tvData_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (tvData.SelectedItem != "")
      {
        tbText.SelectedText = GetTextWithBrackets();
        tbText.Focus();
      }
    }

    private void cbWordWrap_CheckedChanged(object sender, EventArgs e)
    {
      tbText.WordWrap = cbWordWrap.Checked;
    }

    private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
    {
      tbText.Focus();
    }

    private void TextEditorForm_Shown(object sender, EventArgs e)
    {
      tbText.Focus();
      tbText.Select(0, 0);
    }

    private void TextEditorForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Done();
    }
    
    private void Init()
    {
      tbText.Font = DrawUtils.DefaultReportFont;
      tvData.CreateNodes(FReport.Dictionary);
      if (FExpandedNodes != null)
        tvData.ExpandedNodes = FExpandedNodes;
      
      Config.RestoreFormState(this);
      XmlItem xi = Config.Root.FindItem("Forms").FindItem("TextEditorForm");
      cbWordWrap.Checked = xi.GetProp("WordWrap") != "0";
      string s = xi.GetProp("Splitter");
      if (s != "")
        splitContainer1.SplitterDistance = int.Parse(s);
      s = xi.GetProp("DescriptionHeight");
      if (s != "")
        lblDescription.Height = int.Parse(s);
    }
    
    private void Done()
    {
      FExpandedNodes = tvData.ExpandedNodes;
      Config.SaveFormState(this);
      XmlItem xi = Config.Root.FindItem("Forms").FindItem("TextEditorForm");
      xi.SetProp("WordWrap", cbWordWrap.Checked ? "1" : "0");
      xi.SetProp("Splitter", splitContainer1.SplitterDistance.ToString());
      xi.SetProp("DescriptionHeight", lblDescription.Height.ToString());
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,TextEditor");
      Text = res.Get("");
      cbWordWrap.Text = res.Get("WordWrap");
    }
    
    public TextEditorForm(Report report)
    {
      FReport = report;
      FBrackets = "[,]";
      InitializeComponent();
      Localize();
      Init();
    }
  }
}

