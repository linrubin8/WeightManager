using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Controls;
using FastReport.Code;
using FastReport.Data;
using System.Reflection;

namespace FastReport.Forms
{
  internal partial class RichEditorForm : Form
  {
    private RichObject FRich;
    private bool FUpdating;
    private static List<string> FExpandedNodes;

    private string GetTextWithBrackets(string text)
    {
      string[] brackets = FRich.Brackets.Split(new char[] { ',' });
      // this check is needed if Brackets property is not "[,]"
      if (InsideBrackets(rtbText.SelectionStart))
        return "[" + text + "]";
      return brackets[0] + text + brackets[1];
    }

    private bool InsideBrackets(int pos)
    {
      string[] brackets = FRich.Brackets.Split(new char[] { ',' });
      FindTextArgs args = new FindTextArgs();
      args.Text = new FastString(rtbText.Rtf);
      args.OpenBracket = brackets[0];
      args.CloseBracket = brackets[1];
      args.StartIndex = pos;
      return CodeUtils.IndexInsideBrackets(args);
    }

    private void UpdateControls()
    {
      if (FUpdating)
        return;
        
      FUpdating = true;
      Font font = rtbText.SelectionFont;
      if (font != null)
      {
        cbxFontName.FontName = font.Name;
        cbxFontSize.FontSize = font.Size;
        btnBold.Checked = font.Bold;
        btnItalic.Checked = font.Italic;
        btnUnderline.Checked = font.Underline;
      }
      else
      {
        cbxFontName.FontName = "Microsoft Sans Serif";
        cbxFontSize.FontSize = 8.5f;
      }

      TextAlign align = rtbText.SelectionAlignment;
      btnAlignLeft.Checked = align == TextAlign.Left;
      btnAlignCenter.Checked = align == TextAlign.Center;
      btnAlignRight.Checked = align == TextAlign.Right;
      btnAlignJustify.Checked = align == TextAlign.Justify;

      btnColor.Color = rtbText.SelectionColor;
      btnSubscript.Checked = rtbText.SelectionCharOffset == -4;
      btnSuperscript.Checked = rtbText.SelectionCharOffset == 4;
      btnBullets.Checked = rtbText.SelectionBullet;

      FUpdating = false;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,RtfFile");
        if (dialog.ShowDialog() == DialogResult.OK)
          rtbText.LoadFile(dialog.FileName);
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog dialog = new SaveFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,RtfFile");
        dialog.DefaultExt = "rtf";
        if (dialog.ShowDialog() == DialogResult.OK)
          rtbText.SaveFile(dialog.FileName);
      }
    }

    private void btnUndo_Click(object sender, EventArgs e)
    {
      rtbText.Undo();
    }

    private void btnRedo_Click(object sender, EventArgs e)
    {
      rtbText.Redo();
    }

    private void cbxZoom_SelectedIndexChanged(object sender, EventArgs e)
    {
      string zoom = (string)cbxZoom.SelectedItem;
      zoom = zoom.Substring(0, zoom.Length - 1);
      rtbText.ZoomFactor = int.Parse(zoom) / 100f;
      rtbText.Focus();
    }

    private void cbxFontName_FontSelected(object sender, EventArgs e)
    {
      if (!FUpdating)
      {
        rtbText.SetSelectionFont(cbxFontName.FontName);
        UpdateControls();
        rtbText.Focus();
      }  
    }

    private void cbxFontSize_SizeSelected(object sender, EventArgs e)
    {
      if (!FUpdating)
      {
        rtbText.SetSelectionSize((int)cbxFontSize.FontSize);
        rtbText.Focus();
      }  
    }

    private void btnBold_Click(object sender, EventArgs e)
    {
      if (!FUpdating)
        rtbText.SetSelectionBold(btnBold.Checked);
    }

    private void btnItalic_Click(object sender, EventArgs e)
    {
      if (!FUpdating)
        rtbText.SetSelectionItalic(btnItalic.Checked);
    }

    private void btnUnderline_Click(object sender, EventArgs e)
    {
      if (!FUpdating)
        rtbText.SetSelectionUnderline(btnUnderline.Checked);
    }

    private void btnAlignLeft_Click(object sender, EventArgs e)
    {
      if (!FUpdating)
      {
        rtbText.SelectionAlignment = TextAlign.Left;
        UpdateControls();
      }  
    }

    private void btnAlignCenter_Click(object sender, EventArgs e)
    {
      if (!FUpdating)
      {
        rtbText.SelectionAlignment = TextAlign.Center;
        UpdateControls();
      }  
    }

    private void btnAlignRight_Click(object sender, EventArgs e)
    {
      if (!FUpdating)
      {
        rtbText.SelectionAlignment = TextAlign.Right;
        UpdateControls();
      }  
    }

    private void btnAlignJustify_Click(object sender, EventArgs e)
    {
      if (!FUpdating)
      {
        rtbText.SelectionAlignment = TextAlign.Justify;
        UpdateControls();
      }  
    }

    private void btnColor_ButtonClick(object sender, EventArgs e)
    {
      if (!FUpdating)
        rtbText.SelectionColor = btnColor.DefaultColor;
    }

    private void btnSubscript_Click(object sender, EventArgs e)
    {
      if (!FUpdating)
      {
        rtbText.SelectionCharOffset = btnSubscript.Checked ? -4 : 0;
        UpdateControls();
      }  
    }

    private void btnSuperscript_Click(object sender, EventArgs e)
    {
      if (!FUpdating)
      {
        rtbText.SelectionCharOffset = btnSuperscript.Checked ? 4 : 0;
        UpdateControls();
      }  
    }

    private void btnBullets_Click(object sender, EventArgs e)
    {
      if (!FUpdating)
        rtbText.SelectionBullet = btnBullets.Checked;
    }

    private void rtbText_SelectionChanged(object sender, EventArgs e)
    {
      UpdateControls();
    }

    private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
    {
      rtbText.Focus();
    }

    private void tvData_AfterSelect(object sender, TreeViewEventArgs e)
    {
      bool descrVisible = tvData.SelectedNode != null &&
        (tvData.SelectedNode.Tag is MethodInfo || tvData.SelectedNode.Tag is SystemVariable);
      expandableSplitter1.Visible = descrVisible;
      lblDescription.Visible = descrVisible;

      if (descrVisible)
        lblDescription.ShowDescription(FRich.Report, tvData.SelectedNode.Tag);
    }

    private void tvData_ItemDrag(object sender, ItemDragEventArgs e)
    {
      tvData.SelectedNode = e.Item as TreeNode;
      if (tvData.SelectedItem != "")
        tvData.DoDragDrop(e.Item, DragDropEffects.Move);
      else
        tvData.DoDragDrop(e.Item, DragDropEffects.None);
    }

    private void rtbText_DragEnter(object sender, DragEventArgs e)
    {
      e.Effect = e.AllowedEffect;
    }

    private void rtbText_DragDrop(object sender, DragEventArgs e)
    {
      rtbText.SelectedText = GetTextWithBrackets(tvData.SelectedItem);
      rtbText.Focus();
    }

    private void tvData_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (tvData.SelectedItem != "")
      {
        rtbText.SelectedText = GetTextWithBrackets(tvData.SelectedItem);
        rtbText.Focus();
      }
    }

    private void RichEditorForm_Shown(object sender, EventArgs e)
    {
      UpdateControls();
    }

    private void RichEditorForm_FormClosing(object sender, FormClosingEventArgs e)
    {
    }

    private void RichEditorForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Done();
    }

    private void Localize()
    {
      Text = Res.Get("Forms,RichTextEditor");

      MyRes res = new MyRes("Designer,Toolbar,Standard");
      btnUndo.ToolTipText = res.Get("Undo");
      btnRedo.ToolTipText = res.Get("Redo");
      cbxZoom.ToolTipText = Res.Get("Designer,Toolbar,Zoom");

      res = new MyRes("Designer,Toolbar,Text");
      cbxFontName.ToolTipText = res.Get("Name");
      cbxFontSize.ToolTipText = res.Get("Size");
      btnBold.ToolTipText = res.Get("Bold");
      btnItalic.ToolTipText = res.Get("Italic");
      btnUnderline.ToolTipText = res.Get("Underline");
      btnAlignLeft.ToolTipText = res.Get("Left");
      btnAlignCenter.ToolTipText = res.Get("Center");
      btnAlignRight.ToolTipText = res.Get("Right");
      btnAlignJustify.ToolTipText = res.Get("Justify");
      btnColor.ToolTipText = res.Get("Color");

      res = new MyRes("Forms,RichTextEditor");
      btnOk.ToolTipText = Res.Get("Buttons,OK");
      btnCancel.ToolTipText = Res.Get("Buttons,Cancel");
      btnOpen.ToolTipText = res.Get("Open");
      btnSave.ToolTipText = res.Get("Save");
      btnSubscript.ToolTipText = res.Get("Subscript");
      btnSuperscript.ToolTipText = res.Get("Superscript");
      btnBullets.ToolTipText = res.Get("Bullets");

      btnOk.Image = Res.GetImage(210);
      btnCancel.Image = Res.GetImage(212);
      btnOpen.Image = Res.GetImage(1);
      btnSave.Image = Res.GetImage(2);
      btnUndo.Image = Res.GetImage(8);
      btnRedo.Image = Res.GetImage(9);
      btnBold.Image = Res.GetImage(20);
      btnItalic.Image = Res.GetImage(21);
      btnUnderline.Image = Res.GetImage(22);
      btnAlignLeft.Image = Res.GetImage(25);
      btnAlignCenter.Image = Res.GetImage(26);
      btnAlignRight.Image = Res.GetImage(27);
      btnAlignJustify.Image = Res.GetImage(28);
      btnSubscript.Image = Res.GetImage(171);
      btnSuperscript.Image = Res.GetImage(172);
      btnBullets.Image = Res.GetImage(170);
      btnColor.ImageIndex = 23;
    }
    
    private void Init()
    {
      ts1.Font = DrawUtils.Default96Font;
      cbxFontName.Font = ts1.Font;
      cbxFontSize.Font = ts1.Font;
      btnColor.DefaultColor = Color.Black;
      ts1.Renderer = Config.DesignerSettings.ToolStripRenderer;

      tvData.CreateNodes(FRich.Report.Dictionary);
      if (FExpandedNodes != null)
        tvData.ExpandedNodes = FExpandedNodes;

      if (FRich.Text != null && FRich.Text.StartsWith(@"{\rtf"))
        rtbText.Rtf = FRich.Text;
      else
        rtbText.Text = FRich.Text;
      if (rtbText.TextLength == 0)
      {
        rtbText.SelectAll();
        rtbText.SelectionFont = Config.DesignerSettings.DefaultFont;
      }  
      rtbText.Modified = false;
      rtbText.AllowDrop = true;
      rtbText.DragEnter += new DragEventHandler(rtbText_DragEnter);
      rtbText.DragDrop += new DragEventHandler(rtbText_DragDrop);

      Config.RestoreFormState(this);
      XmlItem xi = Config.Root.FindItem("Forms").FindItem("RichEditorForm");
      string s = xi.GetProp("Splitter");
      if (s != "")
        splitContainer1.SplitterDistance = int.Parse(s);
      s = xi.GetProp("DescriptionHeight");
      if (s != "")
        lblDescription.Height = int.Parse(s);
      s = xi.GetProp("Zoom");
      if (s == "")
        s = "100%";
      cbxZoom.SelectedIndex = cbxZoom.Items.IndexOf(s);
    }

    private void Done()
    {
      if (DialogResult == DialogResult.OK)
        FRich.Text = rtbText.Rtf;

      FExpandedNodes = tvData.ExpandedNodes;
      Config.SaveFormState(this);
      XmlItem xi = Config.Root.FindItem("Forms").FindItem("RichEditorForm");
      xi.SetProp("Splitter", splitContainer1.SplitterDistance.ToString());
      xi.SetProp("DescriptionHeight", lblDescription.Height.ToString());
      xi.SetProp("Zoom", cbxZoom.SelectedItem.ToString());
    }

    public RichEditorForm(RichObject rich)
    {
      FRich = rich;
      InitializeComponent();
      Localize();
      Init();
    }
  }
}