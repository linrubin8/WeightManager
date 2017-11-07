using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using System.Drawing.Drawing2D;

namespace FastReport.Forms
{
  internal partial class FillEditorForm : BaseDialogForm
  {
    private FillBase FFill;
    private FillBase[] FFills;
    private bool FUpdating;
    
    private SolidFill SolidFill
    {
      get { return FFills[0] as SolidFill; }
      set { FFills[0] = value; }
    }
    
    private LinearGradientFill LinearGradientFill
    {
      get { return FFills[1] as LinearGradientFill; }
      set { FFills[1] = value; }
    }

    private PathGradientFill PathGradientFill
    {
      get { return FFills[2] as PathGradientFill; }
      set { FFills[2] = value; }
    }

    private HatchFill HatchFill
    {
      get { return FFills[3] as HatchFill; }
      set { FFills[3] = value; }
    }

    private GlassFill GlassFill
    {
      get { return FFills[4] as GlassFill; }
      set { FFills[4] = value; }
    }

    public FillBase Fill
    {
      get { return FFill; }
      set 
      { 
        FFill = value; 
        UpdateControls();
      }
    }

    private void PopulateFills()
    {
      FFills = new FillBase[] { 
        new SolidFill(Color.Silver), new LinearGradientFill(), new PathGradientFill(), 
        new HatchFill(), new GlassFill() }; 
      Rectangle rect = new Rectangle(0, 0, 31, 31);
      ImageList images = new ImageList();
      images.ImageSize = new Size(32, 32);
      images.ColorDepth = ColorDepth.Depth24Bit;
      
      foreach (FillBase fill in FFills)
      {
        Bitmap bmp = new Bitmap(32, 32);
        using (Graphics g = Graphics.FromImage(bmp))
        using (Brush brush = fill.CreateBrush(new RectangleF(0, 0, 31, 31)))
        {
          g.FillRectangle(brush, rect);
          g.DrawRectangle(Pens.Black, rect);
        }
        images.Images.Add(bmp, Color.Transparent);
      }
      (FFills[0] as SolidFill).Color = Color.Transparent;
    }

    private void UpdateControls()
    {
      if (FFill is SolidFill)
      {
        SolidFill = FFill as SolidFill;
        cbxSolidColor.Color = SolidFill.Color;
        pcPanels.ActivePageIndex = 0;
      }
      else if (FFill is LinearGradientFill)
      {
        LinearGradientFill = FFill as LinearGradientFill;
        cbxLinearStartColor.Color = LinearGradientFill.StartColor;
        cbxLinearEndColor.Color = LinearGradientFill.EndColor;
        trbLinearFocus.Value = (int)Math.Round(LinearGradientFill.Focus * 100);
        trbLinearContrast.Value = (int)Math.Round(LinearGradientFill.Contrast * 100);
        acLinearAngle.Angle = LinearGradientFill.Angle;
        pcPanels.ActivePageIndex = 1;
      }
      else if (FFill is PathGradientFill)
      {
        PathGradientFill = FFill as PathGradientFill;
        cbxPathCenterColor.Color = PathGradientFill.CenterColor;
        cbxPathEdgeColor.Color = PathGradientFill.EdgeColor;
        rbPathEllipse.Checked = PathGradientFill.Style == PathGradientStyle.Elliptic;
        rbPathRectangle.Checked = PathGradientFill.Style == PathGradientStyle.Rectangular;
        pcPanels.ActivePageIndex = 2;
      }
      else if (FFill is HatchFill)
      {
        HatchFill = FFill as HatchFill;
        cbxHatchForeColor.Color = HatchFill.ForeColor;
        cbxHatchBackColor.Color = HatchFill.BackColor;
        cbxHatch.Style = HatchFill.Style;
        pcPanels.ActivePageIndex = 3;
      }
      else if (FFill is GlassFill)
      {
        GlassFill = FFill as GlassFill;
        cbxGlassColor.Color = GlassFill.Color;
        trbGlassBlend.Value = (int)Math.Round(GlassFill.Blend * 100);
        cbGlassHatch.Checked = GlassFill.Hatch;
        pcPanels.ActivePageIndex = 4;
      }
    }
    
    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,Fill");
      Text = res.Get("");

      pnSolid.Text = res.Get("Solid");
      pnLinear.Text = res.Get("LinearGradient");
      pnPath.Text = res.Get("PathGradient");
      pnHatch.Text = res.Get("Hatch");
      pnGlass.Text = res.Get("Glass");

      gbSolidColors.Text = res.Get("Color");
      lblSolidColor.Text = res.Get("Color");
      gbLinearColors.Text = res.Get("Colors");
      lblLinearStartColor.Text = res.Get("StartColor");
      lblLinearEndColor.Text = res.Get("EndColor");
      gbLinearOptions.Text = res.Get("Options");
      lblLinearFocus.Text = res.Get("Focus");
      lblLinearContrast.Text = res.Get("Contrast");
      gbLinearAngle.Text = res.Get("Angle");
      gbPathColors.Text = res.Get("Colors");
      lblPathCenterColor.Text = res.Get("CenterColor");
      lblPathEdgeColor.Text = res.Get("EdgeColor");
      gbPathShape.Text = res.Get("Shape");
      rbPathEllipse.Text = res.Get("Ellipse");
      rbPathRectangle.Text = res.Get("Rectangle");
      gbHatchColors.Text = res.Get("Colors");
      lblHatchForeColor.Text = res.Get("ForeColor");
      lblHatchBackColor.Text = res.Get("BackColor");
      gbHatchStyle.Text = res.Get("Style");
      gbGlassOptions.Text = res.Get("Options");
      lblGlassColor.Text = res.Get("Color");
      lblGlassBlend.Text = res.Get("Blend");
      cbGlassHatch.Text = res.Get("GlassHatch");
    }

    private void pcPanels_PageSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
        
      FUpdating = true;  
      int currentFill = pcPanels.ActivePageIndex;
      Fill = FFills[currentFill];
      pnSample.Refresh();
      FUpdating = false;
    }
    
    private void pnSample_Paint(object sender, PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      
      if (Fill is GlassFill)
      {
        (Fill as GlassFill).Draw(new FRPaintEventArgs(g, 1, 1, null), 
          new RectangleF(0, 0, pnSample.Width, pnSample.Height));
      }
      else if (Fill != null)
      {
        Brush brush = null;
        
        if (Fill is SolidFill && (Fill as SolidFill).Color == Color.Transparent)
          brush = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.Gainsboro);
        else
          brush = Fill.CreateBrush(new RectangleF(0, 0, pnSample.Width, pnSample.Height));
          
        g.FillRectangle(brush, 0, 0, pnSample.Width, pnSample.Height);
        brush.Dispose();
      }
      
      g.DrawRectangle(SystemPens.ControlDark, 0, 0, pnSample.Width - 1, pnSample.Height - 1);
    }
    
    private void Change()
    {
      pnSample.Refresh();
    }

    private void cbxSolidColor_ColorSelected(object sender, EventArgs e)
    {
      SolidFill.Color = cbxSolidColor.Color;
      Change();
    }

    private void cbxLinearStartColor_ColorSelected(object sender, EventArgs e)
    {
      LinearGradientFill.StartColor = cbxLinearStartColor.Color;
      Change();
    }

    private void cbxLinearEndColor_ColorSelected(object sender, EventArgs e)
    {
      LinearGradientFill.EndColor = cbxLinearEndColor.Color;
      Change();
    }

    private void trbLinearFocus_ValueChanged(object sender, EventArgs e)
    {
      LinearGradientFill.Focus = trbLinearFocus.Value / 100f;
      Change();
    }

    private void trbLinearContrast_ValueChanged(object sender, EventArgs e)
    {
      LinearGradientFill.Contrast = trbLinearContrast.Value / 100f;
      Change();
    }

    private void acLinearAngle_AngleChanged(object sender, EventArgs e)
    {
      LinearGradientFill.Angle = acLinearAngle.Angle;
      Change();
    }

    private void cbxPathCenterColor_ColorSelected(object sender, EventArgs e)
    {
      PathGradientFill.CenterColor = cbxPathCenterColor.Color;
      Change();
    }

    private void cbxPathEdgeColor_ColorSelected(object sender, EventArgs e)
    {
      PathGradientFill.EdgeColor = cbxPathEdgeColor.Color;
      Change();
    }

    private void rbPathEllipse_CheckedChanged(object sender, EventArgs e)
    {
      PathGradientFill.Style = rbPathEllipse.Checked ? PathGradientStyle.Elliptic : PathGradientStyle.Rectangular;
      Change();
    }

    private void cbxHatchForeColor_ColorSelected(object sender, EventArgs e)
    {
      HatchFill.ForeColor = cbxHatchForeColor.Color;
      Change();
    }

    private void cbxHatchBackColor_ColorSelected(object sender, EventArgs e)
    {
      HatchFill.BackColor = cbxHatchBackColor.Color;
      Change();
    }

    private void cbxHatch_HatchSelected(object sender, EventArgs e)
    {
      HatchFill.Style = cbxHatch.Style;
      Change();
    }

    private void cbxGlassColor_ColorSelected(object sender, EventArgs e)
    {
      GlassFill.Color = cbxGlassColor.Color;
      Change();
    }

    private void trbGlassBlend_ValueChanged(object sender, EventArgs e)
    {
      GlassFill.Blend = trbGlassBlend.Value / 100f;
      Change();
    }

    private void cbGlassHatch_CheckedChanged(object sender, EventArgs e)
    {
      GlassFill.Hatch = cbGlassHatch.Checked;
      Change();
    }

    public FillEditorForm()
    {
      InitializeComponent();
      PopulateFills();
      Localize();
    }

    internal class FillSample : Panel
    {
      public FillSample()
      {
        Size = new Size(156, 156);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      }
    }
  }
}