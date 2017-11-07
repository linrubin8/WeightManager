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
    /// Represents a simple pointer.
    /// </summary>
    public class SimplePointer : GaugePointer
    {
        #region Fields

        private float left;
        private float top;
        private float height;
        private float width;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets o sets the height of gauge pointer.
        /// </summary>
        [Browsable(false)]
        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Gets or sets the width of a pointer.
        /// </summary>
        [Browsable(false)]
        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplePointer"/> class.
        /// </summary>
        /// <param name="parent">The parent gauge object.</param>
        public SimplePointer(GaugeObject parent) : base(parent)
        {
            height = 4.0f;
            width = 8.0f;
        }

        #endregion // Constructors

        #region Private Methods

        private void DrawHorz(FRPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = e.Cache.GetPen(BorderColor, BorderWidth * e.ScaleX, DashStyle.Solid);
            Brush brush = e.Cache.GetBrush(Color);

            left = (Parent.AbsLeft + 0.5f * Units.Centimeters) * e.ScaleX;
            top = (Parent.AbsTop + Parent.Height / 2 - 3) * e.ScaleY;
            height = 6 * e.ScaleY;
            width = (float)((Parent.Width - 1.0f * Units.Centimeters) * (Parent.Value - Parent.Minimum) / (Parent.Maximum - Parent.Minimum) * e.ScaleX);

            g.FillRectangle(brush, left, top, width, height);
            g.DrawRectangle(pen, left, top, width, height);
        }

        private void DrawVert(FRPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = e.Cache.GetPen(BorderColor, BorderWidth * e.ScaleY, DashStyle.Solid);
            Brush brush = e.Cache.GetBrush(Color);

            left = (Parent.AbsLeft + Parent.Width / 2 - 3) * e.ScaleX;
            width = 6 * e.ScaleX;
            height = (float)((Parent.Height - 1.0f * Units.Centimeters) * (Parent.Value - Parent.Minimum) / (Parent.Maximum - Parent.Minimum) * e.ScaleY);
            top = (Parent.AbsTop + Parent.Height - 0.5f * Units.Centimeters) * e.ScaleY - height;

            g.FillRectangle(brush, left, top, width, height);
            g.DrawRectangle(pen, left, top, width, height);
        }

        #endregion // Private Methods

        #region Public Methods

        /// <inheritdoc/>
        public override void Assign(GaugePointer src)
        {
            base.Assign(src);

            SimplePointer s = src as SimplePointer;
            Height = s.Height;
            Width = s.Width;
        }

        /// <inheritdoc/>
        public override void Draw(FRPaintEventArgs e)
        {
            base.Draw(e);

            if (Parent.Vertical)
            {
                DrawVert(e);
            }
            else
            {
                DrawHorz(e);
            }
        }

        /// <inheritdoc/>
        public override void Serialize(FRWriter writer, string prefix, GaugePointer diff)
        {
            base.Serialize(writer, prefix, diff);

            SimplePointer dc = diff as SimplePointer;
            if (Height != dc.Height)
            {
                writer.WriteFloat(prefix + ".Height", Height);
            }
            if (Width != dc.Width)
            {
                writer.WriteFloat(prefix + ".Width", Width);
            }
        }

        #endregion // Public Methods
    }
}
