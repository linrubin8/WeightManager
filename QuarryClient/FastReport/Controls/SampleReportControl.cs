using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Controls
{
  internal class SampleReportControl : Control
  {
    private Report FReport;
    private float FZoom;
    private bool FFullPagePreview;
    
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Report Report
    {
      get { return FReport; }
      set
      {
        FReport = value;
        Refresh();
      }
    }
    
    [DefaultValue(1f)]
    public float Zoom
    {
      get { return FZoom; }
      set
      {
        FZoom = value;
        Refresh();
      }
    }
    
    [DefaultValue(false)]
    public bool FullPagePreview
    {
      get { return FFullPagePreview; }
      set
      {
        FFullPagePreview = value;
        Refresh();
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      
      if (FReport != null && FReport.Pages.Count > 0 && FReport.Pages[0] is ReportPage)
      {
        ReportPage page = FReport.Pages[0] as ReportPage;
        
        float zoom = FZoom;
        if (FullPagePreview)
        {
          float pageWidth = page.WidthInPixels;
          float pageHeight = page.HeightInPixels;
          zoom = Math.Min((Width - 20) / pageWidth, (Height - 20) / pageHeight);
        }  
        
        FRPaintEventArgs args = new FRPaintEventArgs(g, zoom, zoom, FReport.GraphicCache);
        g.TranslateTransform(10, 10);
        page.Draw(args);
        g.TranslateTransform(-10, -10);
      }

      // draw control frame
      using (Pen p = new Pen(Color.FromArgb(127, 157, 185)))
      {
        g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
      }
    }
    
    public SampleReportControl()
    {
      FZoom = 1;
      BackColor = SystemColors.AppWorkspace;
      SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }
  }
}
