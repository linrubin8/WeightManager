using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Gauge
{
    /// <summary>
    /// Represents a scale of a gauge.
    /// </summary>
    public class GaugeScale : Component
    {
        #region Fields

        private GaugeObject parent;
        private Font font;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the parent gauge object.
        /// </summary>
        [Browsable(false)]
        public GaugeObject Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// Gets or sets the font of scale.
        /// </summary>
        [Browsable(true)]
        public Font Font
        {
            get { return font; }
            set { font = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GaugeScale"/> class.
        /// </summary>
        /// <param name="parent">The parent gauge object.</param>
        public GaugeScale(GaugeObject parent)
        {
            this.parent = parent;
            font = new Font("Arial", 8.0f);
        }

        #endregion // Constructors

        #region Public Methods

        /// <summary>
        /// Copies the contents of another GaugeScale.
        /// </summary>
        /// <param name="src">The GaugeScale instance to copy the contents from.</param>
        public virtual void Assign(GaugeScale src)
        {
            Font = src.Font;
        }

        /// <summary>
        /// Draws the scale of gauge.
        /// </summary>
        /// <param name="e">Draw event arguments.</param>
        public virtual void Draw(FRPaintEventArgs e)
        {
        }

        /// <summary>
        /// Serializes the gauge scale.
        /// </summary>
        /// <param name="writer">Writer object.</param>
        /// <param name="prefix">Scale property name.</param>
        /// <param name="diff">Another GaugeScale to compare with.</param>
        /// <remarks>
        /// This method is for internal use only.
        /// </remarks>
        public virtual void Serialize(FRWriter writer, string prefix, GaugeScale diff)
        {
            if (!Font.Equals(diff.Font))
            {
                writer.WriteValue(prefix + ".Font", Font);
            }
        }

        #endregion // Public Methods
    }

    /// <summary>
    /// Represents a scale ticks.
    /// </summary>
    [ToolboxItem(false)]
    public class ScaleTicks : Component
    {
        #region Fields

        private float length;
        private int width;
        private Color color;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the length of ticks.
        /// </summary>
        [Browsable(false)]
        public float Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// Gets or sets the width of ticks.
        /// </summary>
        [Browsable(true)]
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Gets or sets the color of ticks.
        /// </summary>
        [Browsable(true)]
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleTicks"/> class.
        /// </summary>
        public ScaleTicks()
        {
            length = 8.0f;
            width = 1;
            color = Color.Black;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleTicks"/> class.
        /// </summary>
        /// <param name="length">Ticks length.</param>
        /// <param name="width">Ticks width.</param>
        /// <param name="color">Ticks color.</param>
        public ScaleTicks(float length, int width, Color color)
        {
            this.length = length;
            this.width = width;
            this.color = color;
        }

        #endregion // Constructors

        #region Public Methods

        /// <summary>
        /// Copies the contents of another ScaleTicks.
        /// </summary>
        /// <param name="src">The ScaleTicks instance to copy the contents from.</param>
        public virtual void Assign(ScaleTicks src)
        {
            Length = src.Length;
            Width = src.Width;
            Color = src.Color;
        }

        /// <summary>
        /// Serializes the scale ticks.
        /// </summary>
        /// <param name="writer">Writer object.</param>
        /// <param name="prefix">Scale ticks property name.</param>
        /// <param name="diff">Another ScaleTicks to compare with.</param>
        /// <remarks>
        /// This method is for internal use only.
        /// </remarks>
        public virtual void Serialize(FRWriter writer, string prefix, ScaleTicks diff)
        {
            if (Length != diff.Length)
            {
                writer.WriteFloat(prefix + ".Length", Length);
            }
            if (Width != diff.Width)
            {
                writer.WriteInt(prefix + ".Width", Width);
            }
            if (Color != diff.Color)
            {
                writer.WriteValue(prefix + ".Color", Color);
            }
        }

        #endregion // Public Methods
    }
}
