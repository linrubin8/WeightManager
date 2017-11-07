using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using FastReport.Utils;

namespace FastReport.Forms
{
  internal partial class WatermarkEditorForm : BaseDialogForm
  {
    private Watermark FWatermark;
    
    public Watermark Watermark
    {
      get { return FWatermark; }
      set 
      { 
        FWatermark = value.Clone();
        UpdateControls();
      }
    }

    public bool ApplyToAll
    {
      get { return cbApplyToAll.Checked; }
    }

    private void CreateVS2005Renderer()
    {
      ProfessionalColorTable vs2005ColorTable = new ProfessionalColorTable();
      vs2005ColorTable.UseSystemColors = true;
      ToolStripRenderer renderer = new ToolStripProfessionalRenderer(vs2005ColorTable);
      toolStrip1.Renderer = renderer;
      toolStrip2.Renderer = renderer;
    }

    private void cbEnabled_CheckedChanged(object sender, EventArgs e)
    {
      bool enabled = cbEnabled.Checked;
      tabControl1.Enabled = enabled;
      gbZorder.Enabled = enabled;
      Watermark.Enabled = enabled;
      UpdateSample();
    }

    private void cbxText_TextChanged(object sender, EventArgs e)
    {
      Watermark.Text = cbxText.Text;
      UpdateSample();
    }

    private void cbxRotation_SelectedIndexChanged(object sender, EventArgs e)
    {
      Watermark.TextRotation = (WatermarkTextRotation)cbxRotation.SelectedIndex;
      UpdateSample();
    }

    private void btnFont_Click(object sender, EventArgs e)
    {
      using (FontDialog dialog = new FontDialog())
      {
        dialog.Font = Watermark.Font;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          Watermark.Font = dialog.Font;
          UpdateSample();
        }  
      }
    }

    private void btnColor_Click(object sender, EventArgs e)
    {
      using (FillEditorForm form = new FillEditorForm())
      {
        form.Fill = Watermark.TextFill;
        if (form.ShowDialog() == DialogResult.OK)
        {
          Watermark.TextFill = form.Fill;
          UpdateSample();
        }
      }
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,Images");
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          try
          {
            Watermark.Image = Image.FromFile(dialog.FileName);
          }
          catch (Exception ex)
          {
            Watermark.Image = null;
            FRMessageBox.Error(ex.Message);
          }
          UpdateSample();
        }
      }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      Watermark.Image = null;
      UpdateSample();
    }

    private void cbxSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      Watermark.ImageSize = (WatermarkImageSize)cbxSize.SelectedIndex;
      UpdateSample();
    }

    private void trbTransparency_ValueChanged(object sender, EventArgs e)
    {
      Watermark.ImageTransparency = trbTransparency.Value / 100f;
      UpdateSample();
    }

    private void cbTextOnTop_CheckedChanged(object sender, EventArgs e)
    {
      Watermark.ShowTextOnTop = cbTextOnTop.Checked;
      UpdateSample();
    }

    private void cbPictureOnTop_CheckedChanged(object sender, EventArgs e)
    {
      Watermark.ShowImageOnTop = cbPictureOnTop.Checked;
      UpdateSample();
    }

    private void UpdateSample()
    {
      panel1.Refresh();
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      g.FillRectangle(SystemBrushes.Window, panel1.DisplayRectangle);

      RectangleF pageRect = new RectangleF(0, 0, panel1.Width * 4, panel1.Height * 4);
      FRPaintEventArgs ev = new FRPaintEventArgs(g, 0.25f, 0.25f, new GraphicCache());
      if (Watermark.Enabled)
      {
        if (!Watermark.ShowImageOnTop)
          Watermark.DrawImage(ev, pageRect, null, false);
        if (!Watermark.ShowTextOnTop)
          Watermark.DrawText(ev, pageRect, null, false);
        if (Watermark.ShowImageOnTop)
          Watermark.DrawImage(ev, pageRect, null, false);
        if (Watermark.ShowTextOnTop)
          Watermark.DrawText(ev, pageRect, null, false);
      }
      using (Pen p = new Pen(Color.FromArgb(127, 157, 185)))
      {
        g.DrawRectangle(p, 0, 0, panel1.Width - 1, panel1.Height - 1);
      }
      ev.Cache.Dispose();
    }

    private void UpdateControls()
    {
      cbEnabled.Checked = Watermark.Enabled;
      cbEnabled_CheckedChanged(this, EventArgs.Empty);
      cbxText.Text = Watermark.Text;
      cbxRotation.SelectedIndex = (int)Watermark.TextRotation;
      cbxSize.SelectedIndex = (int)Watermark.ImageSize;
      trbTransparency.Value = (int)(Watermark.ImageTransparency * 100);
      cbTextOnTop.Checked = Watermark.ShowTextOnTop;
      cbPictureOnTop.Checked = Watermark.ShowImageOnTop;
    }

    public void HideApplyToAll()
    {
      cbApplyToAll.Visible = false;
    }
    
    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,WatermarkEditor");
      Text = res.Get("");
      cbEnabled.Text = res.Get("Enabled");
      pgText.Text = res.Get("Text");
      lblText.Text = res.Get("Text");
      cbxText.Items.AddRange(res.Get("TextItems").Split(new char[] { ',' }));
      cbxText.Items.Add("[COPYNAME#]");
      lblRotation.Text = res.Get("Rotation");
      cbxRotation.Items.AddRange(new string[] {
        res.Get("Horizontal"), res.Get("Vertical"), res.Get("ForwardDiagonal"), res.Get("BackwardDiagonal") });
      btnFont.Text = res.Get("Font");
      btnColor.Text = res.Get("Color");
      pgPicture.Text = res.Get("Picture");
      btnLoad.Text = res.Get("Load");
      btnClear.Text = res.Get("Clear");
      lblSize.Text = res.Get("Size");
      cbxSize.Items.AddRange(new string[] {
        res.Get("Normal"), res.Get("Center"), res.Get("Stretch"), res.Get("Zoom"), res.Get("Tile") });
      lblTransparency.Text = res.Get("Transparency");
      gbZorder.Text = res.Get("Zorder");
      cbTextOnTop.Text = res.Get("TextOnTop");
      cbPictureOnTop.Text = res.Get("PictureOnTop");
      cbApplyToAll.Text = res.Get("ApplyToAll");
      
      btnFont.Image = Res.GetImage(59);
      btnColor.Image = Res.GetImage(23);
      btnLoad.Image = Res.GetImage(1);
      btnClear.Image = Res.GetImage(51);
    }
    
    public WatermarkEditorForm()
    {
      InitializeComponent();
      Localize();
      MethodInfo mi = typeof(Panel).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
      mi.Invoke(panel1, new object[] { ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true });
      CreateVS2005Renderer();
    }
  }
}

