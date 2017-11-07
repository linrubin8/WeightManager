using System;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.TypeConverters;

namespace FastReport.Barcode
{
    /// <summary>
    /// The base class for all barcodes.
    /// </summary>
    [TypeConverter(typeof(BarcodeConverter))]
    public abstract class BarcodeBase
    {
        #region Fields
        internal string Text;
        internal int Angle;
        internal bool ShowText;
        internal float Zoom;
        private Color FColor;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of barcode.
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get { return Barcodes.GetName(GetType()); }
        }

        /// <summary>
        /// Gets or sets the color of barcode.
        /// </summary>
        public Color Color
        {
            get { return FColor; }
            set { FColor = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Creates the exact copy of this barcode.
        /// </summary>
        /// <returns>The copy of this barcode.</returns>
        public BarcodeBase Clone()
        {
            BarcodeBase result = Activator.CreateInstance(GetType()) as BarcodeBase;
            result.Assign(this);
            return result;
        }

        /// <summary>
        /// Assigns properties from other, similar barcode.
        /// </summary>
        /// <param name="source">Barcode object to assign properties from.</param>
        public virtual void Assign(BarcodeBase source)
        {
            Color = source.Color;
        }

        internal virtual void Serialize(FRWriter writer, string prefix, BarcodeBase diff)
        {
            if (diff.GetType() != GetType())
                writer.WriteStr("Barcode", Name);
            if (diff.Color != Color)
                writer.WriteValue(prefix + "Color", Color);
        }

        internal virtual void Initialize(string text, bool showText, int angle, float zoom)
        {
            Text = text;
            ShowText = showText;
            Angle = (angle / 90 * 90) % 360;
            Zoom = zoom;
        }

        internal virtual SizeF CalcBounds()
        {
            return SizeF.Empty;
        }

        internal virtual string StripControlCodes(string data)
        {
            return data;
        }

        internal virtual void DrawBarcode(Graphics g, RectangleF displayRect)
        {
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeBase"/> class with default settings.
        /// </summary>
        public BarcodeBase()
        {
            Text = "";
            FColor = Color.Black;
        }
    }
}
