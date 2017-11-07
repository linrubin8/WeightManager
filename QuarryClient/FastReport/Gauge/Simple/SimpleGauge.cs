using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using FastReport.Utils;

namespace FastReport.Gauge.Simple
{
    /// <summary>
    /// Represents a simple gauge.
    /// </summary>
    public partial class SimpleGauge : GaugeObject
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleGauge"/> class.
        /// </summary>
        public SimpleGauge() : base()
        {
            InitializeComponent();
            Value = 75;
            Scale = new SimpleScale(this);
            Pointer = new SimplePointer(this);
            Height = 2.0f * Units.Centimeters;
            Width = 8.0f * Units.Centimeters;
        }

        #endregion // Constructors

        #region Public Methods

        /// <inheritdoc/>
        public override void Draw(FRPaintEventArgs e)
        {
            base.Draw(e);

            Graphics g = e.Graphics;

            float x = (AbsLeft + Border.Width / 2) * e.ScaleX;
            float y = (AbsTop + Border.Width / 2) * e.ScaleY;
            float dx = (Width - Border.Width) * e.ScaleX - 1;
            float dy = (Height - Border.Width) * e.ScaleY - 1;
            float x1 = x + dx;
            float y1 = y + dy;

            DashStyle[] styles = new DashStyle[] { DashStyle.Solid, DashStyle.Dash, DashStyle.Dot, DashStyle.DashDot, DashStyle.DashDotDot, DashStyle.Solid };
            Pen pen = e.Cache.GetPen(Border.Color, Border.Width * e.ScaleX, styles[(int)Border.Style]);
            Brush brush = null;
            if (Fill is SolidFill)
            {
                brush = e.Cache.GetBrush((Fill as SolidFill).Color);
            }
            else
            {
                brush = Fill.CreateBrush(new RectangleF(x, y, dx, dy));
            }

            g.FillRectangle(brush, x, y, dx, dy);
            g.DrawRectangle(pen, x, y, dx, dy);

            if (!(Fill is SolidFill))
            {
                brush.Dispose();
            }

            if (Report != null && Report.SmoothGraphics)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
            }

            Scale.Draw(e);
            Pointer.Draw(e);
        }

        #endregion // Public Methods
    }
}
