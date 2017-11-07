using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using FastReport.Utils;

namespace FastReport.Gauge.Linear
{
    /// <summary>
    /// Represents a linear gauge.
    /// </summary>
    public partial class LinearGauge : GaugeObject
    {
        #region Fields

        private bool inverted;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the value that specifies inverted gauge or not.
        /// </summary>
        [Category("Appearance")]
        public bool Inverted
        {
            get { return inverted; }
            set { inverted = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearGauge"/> class.
        /// </summary>
        public LinearGauge() : base()
        {
            InitializeComponent();
            Scale = new LinearScale(this);
            Pointer = new LinearPointer(this);
            Height = 2.0f * Units.Centimeters;
            Width = 8.0f * Units.Centimeters;
            inverted = false;
        }

        #endregion // Constructors

        #region Public Methods

        /// <inheritdoc/>
        public override void Assign(Base source)
        {
            base.Assign(source);

            LinearGauge src = source as LinearGauge;
            Inverted = src.Inverted;
        }

        /// <inheritdoc/>
        public override void Draw(FRPaintEventArgs e)
        {
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

        /// <inheritdoc/>
        public override void Serialize(FRWriter writer)
        {
            LinearGauge c = writer.DiffObject as LinearGauge;
            base.Serialize(writer);

            if (Inverted != c.Inverted)
            {
                writer.WriteBool("Inverted", Inverted);
            }
        }

        #endregion // Public Methods
    }
}
