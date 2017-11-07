using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.Controls;

namespace FastReport.Forms
{
  internal partial class StyleEditorForm : BaseDialogForm
  {
    private Report FReport;
    private StyleCollection FStyles;
    private TextObject FSample;
    private Timer FRefreshTimer;
    private bool FUpdating;
    
    private Style CurrentStyle
    {
      get
      {
        if (lvStyles.SelectedItems.Count == 0)
          return null;
        return lvStyles.SelectedItems[0].Tag as Style;
      }
    }
    
    private StyleCollection Styles
    {
      get { return FStyles; }
      set 
      { 
        FStyles = value.Clone();
        PopulateStyles();
      }
    }
    
    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,Style");
      Text = res.Get("");
      
      gbStyles.Text = res.Get("Styles");
      btnAdd.Text = res.Get("Add");
      btnDelete.Text = res.Get("Delete");
      btnEdit.Text = res.Get("Edit");
      btnUp.Image = Res.GetImage(208);
      btnDown.Image = Res.GetImage(209);
      btnLoad.Text = res.Get("Load");
      btnSave.Text = res.Get("Save");
      
      gbSettings.Text = res.Get("Settings");
      btnBorder.Image = Res.GetImage(36);
      btnBorder.Text = res.Get("Border");
      btnFill.Image = Res.GetImage(38);
      btnFill.Text = res.Get("Fill");
      btnFont.Image = Res.GetImage(59);
      btnFont.Text = res.Get("Font");
      btnTextColor.Image = Res.GetImage(23);
      btnTextColor.Text = res.Get("TextColor");
      FSample.Text = Res.Get("Misc,Sample");
    }

    private void pnSample_Paint(object sender, PaintEventArgs e)
    {
      if (CurrentStyle == null)
        return;
      FSample.ApplyStyle(CurrentStyle);
      FSample.Bounds = new RectangleF(2, 2, pnSample.Width - 4, pnSample.Height - 4);
      FSample.HorzAlign = HorzAlign.Center;
      FSample.VertAlign = VertAlign.Center;
      using (GraphicCache cache = new GraphicCache())
      {
        FSample.Draw(new FRPaintEventArgs(e.Graphics, 1, 1, cache));
      }
    }

    private void RefreshSample()
    {
      pnSample.Refresh();
    }

    private void UpdateControls()
    {
      bool enabled = CurrentStyle != null;
      btnDelete.Enabled = enabled;
      btnEdit.Enabled = enabled;
      btnUp.Enabled = enabled;
      btnDown.Enabled = enabled;
      gbSettings.Enabled = enabled;

      if (enabled)
      {
        FUpdating = true;
        cbApplyBorder.Checked = CurrentStyle.ApplyBorder;
        cbApplyFill.Checked = CurrentStyle.ApplyFill;
        cbApplyTextFill.Checked = CurrentStyle.ApplyTextFill;
        cbApplyFont.Checked = CurrentStyle.ApplyFont;

        btnBorder.Enabled = CurrentStyle.ApplyBorder;
        btnFill.Enabled = CurrentStyle.ApplyFill;
        btnTextColor.Enabled = CurrentStyle.ApplyTextFill;
        btnFont.Enabled = CurrentStyle.ApplyFont;
        FUpdating = false;
      }
    }

    private void SetApply()
    {
      foreach (ListViewItem li in lvStyles.SelectedItems)
      {
        Style c = li.Tag as Style;
        c.ApplyBorder = cbApplyBorder.Checked;
        c.ApplyFill = cbApplyFill.Checked;
        c.ApplyTextFill = cbApplyTextFill.Checked;
        c.ApplyFont = cbApplyFont.Checked;
      }
      RefreshSample();
    }

    private void PopulateStyles()
    {
      lvStyles.Items.Clear();
      foreach (Style s in FStyles)
      {
        ListViewItem li = lvStyles.Items.Add(s.Name, 87);
        li.Tag = s;
      }
      if (lvStyles.Items.Count > 0)
        lvStyles.Items[0].Selected = true;
      UpdateControls();  
      lvStyles.Focus();  
    }

    private void FTimer_Tick(object sender, EventArgs e)
    {
      UpdateControls();
      RefreshSample();
      FRefreshTimer.Stop();
    }

    private void lvStyles_SelectedIndexChanged(object sender, EventArgs e)
    {
      FRefreshTimer.Start();
    }
    
    private void lvStyles_AfterLabelEdit(object sender, LabelEditEventArgs e)
    {
      if (e.Label == null)
        return;
      if (e.Label == "")
      {
        e.CancelEdit = true;
        return;
      }
      Style s = lvStyles.Items[e.Item].Tag as Style;
      bool nameExists = false;
      foreach (Style s1 in FStyles)
      {
        if (s1 != s && String.Compare(s1.Name, e.Label) == 0)
        {
          nameExists = true;
          break;
        }
      }
      if (!nameExists)
        s.Name = e.Label;
      else
      {
        FRMessageBox.Error(Res.Get("Messages,DuplicateStyle"));
        e.CancelEdit = true;
      }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      Style s = new Style();
      int styleNumber = 1;
      string baseName = Res.Get("Forms,Style,StyleName");
      while (FStyles.IndexOf(baseName + styleNumber.ToString()) != -1)
        styleNumber++;
      s.Name = baseName + styleNumber.ToString();
      FStyles.Add(s);
      ListViewItem li = lvStyles.Items.Add(s.Name, 87);
      li.Tag = s;
      li.BeginEdit();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
      if (lvStyles.SelectedItems.Count == 1)
        lvStyles.SelectedItems[0].BeginEdit();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      while (lvStyles.SelectedItems.Count > 0)
      {
        Style s = lvStyles.SelectedItems[0].Tag as Style;
        FStyles.Remove(s);
        lvStyles.Items.Remove(lvStyles.SelectedItems[0]);
      }
    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      if (lvStyles.SelectedItems.Count != 1)
        return;
      int index = lvStyles.SelectedIndices[0];
      if (index > 0)
      {
        ListViewItem li = lvStyles.SelectedItems[0];
        lvStyles.Items.Remove(li);
        lvStyles.Items.Insert(index - 1, li);
        Style s = FStyles[index];
        FStyles.Remove(s);
        FStyles.Insert(index - 1, s);
      }  
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      if (lvStyles.SelectedItems.Count != 1)
        return;
      int index = lvStyles.SelectedIndices[0];
      if (index < lvStyles.Items.Count - 1)
      {
        ListViewItem li = lvStyles.SelectedItems[0];
        lvStyles.Items.Remove(li);
        lvStyles.Items.Insert(index + 1, li);
        Style s = FStyles[index];
        FStyles.Remove(s);
        FStyles.Insert(index + 1, s);
      }
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,Style");
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          FStyles.Load(dialog.FileName);
          PopulateStyles();
        }
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      using (SaveFileDialog dialog = new SaveFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,Style");
        dialog.DefaultExt = "frs";
        if (dialog.ShowDialog() == DialogResult.OK)
          FStyles.Save(dialog.FileName);
      }  
    }

    private void cbApplyBorder_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating || CurrentStyle == null)
        return;
      btnBorder.Enabled = cbApplyBorder.Checked;
      SetApply();
    }

    private void cbApplyFill_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating || CurrentStyle == null)
        return;
      btnFill.Enabled = cbApplyFill.Checked;
      SetApply();
    }

    private void cbApplyFont_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating || CurrentStyle == null)
        return;
      btnFont.Enabled = cbApplyFont.Checked;
      SetApply();
    }

    private void cbApplyTextFill_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating || CurrentStyle == null)
        return;
      btnTextColor.Enabled = cbApplyTextFill.Checked;
      SetApply();
    }

    private void btnBorder_Click(object sender, EventArgs e)
    {
      if (CurrentStyle == null)
        return;
      using (BorderEditorForm editor = new BorderEditorForm())
      {
        editor.Border = CurrentStyle.Border.Clone();
        if (editor.ShowDialog() == DialogResult.OK)
        {
          foreach (ListViewItem li in lvStyles.SelectedItems)
          {
            (li.Tag as Style).Border = editor.Border.Clone();
          }
          RefreshSample();
        }
      }
    }

    private void btnColor_Click(object sender, EventArgs e)
    {
      if (CurrentStyle == null)
        return;
      using (FillEditorForm editor = new FillEditorForm())
      {
        editor.Fill = CurrentStyle.Fill.Clone();
        if (editor.ShowDialog() == DialogResult.OK)
        {
          foreach (ListViewItem li in lvStyles.SelectedItems)
          {
            (li.Tag as Style).Fill = editor.Fill.Clone();
          }
          RefreshSample();
        }
      }
    }

    private void btnFont_Click(object sender, EventArgs e)
    {
      if (CurrentStyle == null)
        return;
      using (FontDialog dialog = new FontDialog())
      {
        dialog.Font = CurrentStyle.Font;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          foreach (ListViewItem li in lvStyles.SelectedItems)
          {
              (li.Tag as Style).Font = new Font(dialog.Font.FontFamily, (float)Math.Round(dialog.Font.Size), dialog.Font.Style);
          }
          RefreshSample();
        }
      }
    }

    private void btnTextColor_Click(object sender, EventArgs e)
    {
      if (CurrentStyle == null)
        return;
      using (FillEditorForm editor = new FillEditorForm())
      {
        editor.Fill = CurrentStyle.TextFill.Clone();
        if (editor.ShowDialog() == DialogResult.OK)
        {
          foreach (ListViewItem li in lvStyles.SelectedItems)
          {
            (li.Tag as Style).TextFill = editor.Fill.Clone();
          }
          RefreshSample();
        }
      }
    }

    private void StyleEditorForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (DialogResult == DialogResult.OK)
      {
        FReport.Styles = Styles;
        FReport.ApplyStyles();
      }
      FSample.Dispose();
      FRefreshTimer.Dispose();
    }

    public StyleEditorForm(Report report)
    {
      FReport = report;
      FSample = new TextObject();
      FRefreshTimer = new Timer();
      FRefreshTimer.Interval = 50;
      FRefreshTimer.Tick += new EventHandler(FTimer_Tick);
      InitializeComponent();
      lvStyles.SmallImageList = Res.GetImages();
      Localize();
      Styles = FReport.Styles;
      this.Font = DrawUtils.DefaultFont;
    }
  }
}