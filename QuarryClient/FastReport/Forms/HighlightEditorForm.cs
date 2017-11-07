using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Controls;

namespace FastReport.Forms
{
  internal partial class HighlightEditorForm : BaseDialogForm
  {
    private Report FReport;
    private ImageList FImages;
    private ConditionCollection FConditions;
    private Timer FRefreshTimer;
    private bool FUpdating;
    
    public ConditionCollection Conditions
    {
      get { return FConditions; }
      set
      {
        FConditions = new ConditionCollection();
        FConditions.Assign(value);
        PopulateConditions();
      }
    }
    
    private HighlightCondition CurrentCondition
    {
      get
      {
        if (lvConditions.SelectedItems.Count == 0)
          return null;
        return lvConditions.SelectedItems[0].Tag as HighlightCondition;
      }
    }

    private Bitmap GetImage(HighlightCondition c)
    {
      Bitmap bmp = new Bitmap(16, 16);
      using (Graphics g = Graphics.FromImage(bmp))
      {
        g.FillRectangle(Brushes.White, 0, 0, 16, 16);

        using (TextObject sample = new TextObject())
        {
          sample.Bounds = new RectangleF(0, 0, 15, 15);
          sample.ApplyCondition(c);
          sample.Font = new Font("Times New Roman", 12, FontStyle.Bold);
          sample.Text = "A";
          sample.HorzAlign = HorzAlign.Center;
          sample.VertAlign = VertAlign.Center;
          
          using (GraphicCache cache = new GraphicCache())
          {
            sample.Draw(new FRPaintEventArgs(g, 1, 1, cache));
          }
        }
      }

      return bmp;
    }

    private int GetImageIndex(HighlightCondition c)
    {
      Bitmap bmp = GetImage(c);
      FImages.Images.Add(bmp, bmp.GetPixel(0, 15));
      return FImages.Images.Count - 1;
    }

    private void RefreshSample()
    {
      pnSample.Refresh();
    }
    
    private void UpdateControls()
    {
      bool enabled = CurrentCondition != null;
      btnDelete.Enabled = enabled;
      btnEdit.Enabled = enabled;
      btnUp.Enabled = enabled;
      btnDown.Enabled = enabled;
      gbStyle.Enabled = enabled;
      
      if (enabled)
      {
        FUpdating = true;
        cbApplyBorder.Checked = CurrentCondition.ApplyBorder;
        cbApplyFill.Checked = CurrentCondition.ApplyFill;
        cbApplyTextFill.Checked = CurrentCondition.ApplyTextFill;
        cbApplyFont.Checked = CurrentCondition.ApplyFont;
        cbVisible.Checked = CurrentCondition.Visible;

        btnBorder.Enabled = CurrentCondition.ApplyBorder;
        btnFill.Enabled = CurrentCondition.ApplyFill;
        btnTextColor.Enabled = CurrentCondition.ApplyTextFill;
        btnFont.Enabled = CurrentCondition.ApplyFont;
        FUpdating = false;
      }
    }

    private void SetApply()
    {
      foreach (ListViewItem li in lvConditions.SelectedItems)
      {
        HighlightCondition c = li.Tag as HighlightCondition;
        c.ApplyBorder = cbApplyBorder.Checked;
        c.ApplyFill = cbApplyFill.Checked;
        c.ApplyTextFill = cbApplyTextFill.Checked;
        c.ApplyFont = cbApplyFont.Checked;
        c.Visible = cbVisible.Checked;
        li.ImageIndex = GetImageIndex(c);
      }
      RefreshSample();
    }
    
    private void PopulateConditions()
    {
      foreach (HighlightCondition c in FConditions)
      {
        ListViewItem li = lvConditions.Items.Add(c.Expression, GetImageIndex(c));
        li.Tag = c;
      }
      if (lvConditions.Items.Count > 0)
        lvConditions.Items[0].Selected = true;
      UpdateControls();  
    }

    private void pnSample_Paint(object sender, PaintEventArgs e)
    {
      if (CurrentCondition == null)
        return;
      
      TextObject sample = new TextObject();
      sample.Text = Res.Get("Misc,Sample");
      sample.ApplyCondition(CurrentCondition);
      sample.Bounds = new RectangleF(2, 2, pnSample.Width - 4, pnSample.Height - 4);
      sample.HorzAlign = HorzAlign.Center;
      sample.VertAlign = VertAlign.Center;
      using (GraphicCache cache = new GraphicCache())
      {
        sample.Draw(new FRPaintEventArgs(e.Graphics, 1, 1, cache));
      }
    }

    private void FTimer_Tick(object sender, EventArgs e)
    {
      UpdateControls();
      RefreshSample();
      FRefreshTimer.Stop();
    }

    private void lvConditions_SelectedIndexChanged(object sender, EventArgs e)
    {
      FRefreshTimer.Start();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      using (ExpressionEditorForm form = new ExpressionEditorForm(FReport))
      {
        form.ExpressionText = FReport.ScriptLanguage == Language.CSharp ? "Value == 0" : "Value = 0";
        if (form.ShowDialog() == DialogResult.OK)
        {
          HighlightCondition c = new HighlightCondition();
          FConditions.Add(c);
          c.Expression = form.ExpressionText;

          ListViewItem li = lvConditions.Items.Add(c.Expression, GetImageIndex(c));
          li.Tag = c;
          lvConditions.SelectedItems.Clear();
          li.Selected = true;
        }
      }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      while (lvConditions.SelectedItems.Count > 0)
      {
        HighlightCondition c = lvConditions.SelectedItems[0].Tag as HighlightCondition;
        FConditions.Remove(c);
        lvConditions.Items.Remove(lvConditions.SelectedItems[0]);
      }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
      if (lvConditions.SelectedItems.Count == 1)
      {
        using (ExpressionEditorForm form = new ExpressionEditorForm(FReport))
        {
          form.ExpressionText = CurrentCondition.Expression;
          if (form.ShowDialog() == DialogResult.OK)
          {
            CurrentCondition.Expression = form.ExpressionText;
            lvConditions.SelectedItems[0].Text = CurrentCondition.Expression;
          }
        }
      }  
    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      if (lvConditions.SelectedItems.Count != 1)
        return;
      int index = lvConditions.SelectedIndices[0];
      if (index > 0)
      {
        ListViewItem li = lvConditions.SelectedItems[0];
        lvConditions.Items.Remove(li);
        lvConditions.Items.Insert(index - 1, li);
        HighlightCondition c = li.Tag as HighlightCondition;
        FConditions.Remove(c);
        FConditions.Insert(index - 1, c);
      }
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      if (lvConditions.SelectedItems.Count != 1)
        return;
      int index = lvConditions.SelectedIndices[0];
      if (index < lvConditions.Items.Count - 1)
      {
        ListViewItem li = lvConditions.SelectedItems[0];
        lvConditions.Items.Remove(li);
        lvConditions.Items.Insert(index + 1, li);
        HighlightCondition c = li.Tag as HighlightCondition;
        FConditions.Remove(c);
        FConditions.Insert(index + 1, c);
      }
    }

    private void cbApplyBorder_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating || CurrentCondition == null)
        return;
      btnBorder.Enabled = cbApplyBorder.Checked;
      SetApply();
    }

    private void cbApplyFill_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating || CurrentCondition == null)
        return;
      btnFill.Enabled = cbApplyFill.Checked;
      SetApply();
    }

    private void cbApplyTextFill_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating || CurrentCondition == null)
        return;
      btnTextColor.Enabled = cbApplyTextFill.Checked;
      SetApply();
    }

    private void cbApplyFont_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating || CurrentCondition == null)
        return;
      btnFont.Enabled = cbApplyFont.Checked;
      SetApply();
    }

    private void btnBorder_Click(object sender, EventArgs e)
    {
      if (CurrentCondition == null)
        return;

      using (BorderEditorForm editor = new BorderEditorForm())
      {
        editor.Border = CurrentCondition.Border.Clone();
        if (editor.ShowDialog() == DialogResult.OK)
        {
          foreach (ListViewItem li in lvConditions.SelectedItems)
          {
            HighlightCondition c = li.Tag as HighlightCondition;
            c.Border = editor.Border.Clone();
            li.ImageIndex = GetImageIndex(c);
          }
          RefreshSample();
        }
      }
    }

    private void btnFill_Click(object sender, EventArgs e)
    {
      if (CurrentCondition == null)
        return;
      using (FillEditorForm editor = new FillEditorForm())
      {
        editor.Fill = CurrentCondition.Fill.Clone();
        if (editor.ShowDialog() == DialogResult.OK)
        {
          foreach (ListViewItem li in lvConditions.SelectedItems)
          {
            HighlightCondition c = li.Tag as HighlightCondition;
            c.Fill = editor.Fill.Clone();
            li.ImageIndex = GetImageIndex(c);
          }
          RefreshSample();
        }
      }
    }

    private void btnTextColor_Click(object sender, EventArgs e)
    {
      if (CurrentCondition == null)
        return;
      ColorPopup popup = new ColorPopup(this);
      if (CurrentCondition.TextFill is SolidFill)
        popup.Color = (CurrentCondition.TextFill as SolidFill).Color;
      popup.ColorSelected += new EventHandler(popup_ColorSelected);  
      popup.Show(btnTextColor, 0, btnTextColor.Height);
    }

    private void popup_ColorSelected(object sender, EventArgs e)
    {
      Color color = (sender as ColorPopup).Color;
      foreach (ListViewItem li in lvConditions.SelectedItems)
      {
        HighlightCondition c = li.Tag as HighlightCondition;
        c.TextFill = new SolidFill(color);
        li.ImageIndex = GetImageIndex(c);
      }
      RefreshSample();
    }

    private void btnFont_Click(object sender, EventArgs e)
    {
      if (CurrentCondition == null)
        return;
      using (FontDialog dialog = new FontDialog())
      {
        dialog.Font = CurrentCondition.Font;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          foreach (ListViewItem li in lvConditions.SelectedItems)
          {
            HighlightCondition c = li.Tag as HighlightCondition;
            c.Font = dialog.Font;
            li.ImageIndex = GetImageIndex(c);
          }
          RefreshSample();
        }
      }
    }

    private void cbVisible_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating || CurrentCondition == null)
        return;
      SetApply();
    }

    private void HighlightEditorForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Done();
    }

    private void Init()
    {
      FImages = new ImageList();
      FImages.ImageSize = new Size(16, 16);
      FImages.ColorDepth = ColorDepth.Depth24Bit;
      lvConditions.SmallImageList = FImages;
      FRefreshTimer = new Timer();
      FRefreshTimer.Interval = 50;
      FRefreshTimer.Tick += new EventHandler(FTimer_Tick);
 
      Config.RestoreFormState(this);
    }
    
    private void Done()
    {
      FImages.Dispose();
      Config.SaveFormState(this);
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,HighlightEditor");
      Text = res.Get("");

      gbConditions.Text = res.Get("Conditions");
      btnAdd.Text = res.Get("Add");
      btnDelete.Text = res.Get("Delete");
      btnEdit.Text = res.Get("Edit");
      btnUp.Image = Res.GetImage(208);
      btnDown.Image = Res.GetImage(209);

      gbStyle.Text = res.Get("Style");
      btnBorder.Image = Res.GetImage(36);
      btnBorder.Text = Res.Get("Forms,Style,Border");
      btnFill.Image = Res.GetImage(38);
      btnFill.Text = res.Get("Fill");
      btnTextColor.Image = Res.GetImage(23);
      btnTextColor.Text = res.Get("TextColor");
      btnFont.Text = res.Get("Font");
      btnFont.Image = Res.GetImage(59);
      cbVisible.Text = res.Get("Visible");
    }
    
    public HighlightEditorForm(Report report)
    {
      FReport = report;
      InitializeComponent();
      Init();
      Localize();
    }
  }
}

