using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.TypeEditors;

namespace FastReport.Gauge
{
    /// <summary>
    /// Represents a gauge object.
    /// </summary>
    public class GaugeObject : ReportComponentBase
    {
        #region Fields

        private double maximum;
        private double minimum;
        private double value;
        private GaugeScale scale;
        private GaugePointer pointer;
        private string expression;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the minimal value of gauge.
        /// </summary>
        [Category("Layout")]
        public double Minimum
        {
            get { return minimum; }
            set
            {
                if (value < maximum)
                {
                    minimum = value;
                    if (this.value < minimum)
                    {
                        this.value = minimum;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximal value of gauge.
        /// </summary>
        [Category("Layout")]
        public double Maximum
        {
            get { return maximum; }
            set
            {
                if (value > minimum)
                {
                    maximum = value;
                    if (this.value > maximum)
                    {
                        this.value = maximum;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the currenta value of gauge.
        /// </summary>
        [Category("Layout")]
        public double Value
        {
            get { return value; }
            set
            {
                if ((value >= minimum) && (value <= maximum))
                {
                    this.value = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets scale of gauge.
        /// </summary>
        [Category("Appearance")]
        public GaugeScale Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// Gets or sets pointer of gauge.
        /// </summary>
        [Category("Appearance")]
        public GaugePointer Pointer
        {
            get { return pointer; }
            set { pointer = value; }
        }

        /// <summary>
        /// Gets or sets an expression that determines the value of gauge object.
        /// </summary>
        [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
        [Category("Data")]
        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        /// <summary>
        /// Gets a value that specifies is gauge vertical or not.
        /// </summary>
        [Browsable(false)]
        public bool Vertical
        {
            get { return Width < Height; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GaugeObject"/> class.
        /// </summary>
        public GaugeObject()
        {
            minimum = 0;
            maximum = 100;
            value = 10;
            scale = new GaugeScale(this);
            pointer = new GaugePointer(this);
            expression = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GaugeObject"/> class.
        /// </summary>
        /// <param name="minimum">Minimum value of gauge.</param>
        /// <param name="maximum">Maximum value of gauge.</param>
        /// <param name="value">Current value of gauge.</param>
        public GaugeObject(double minimum, double maximum, double value)
        {
            this.minimum = minimum;
            this.maximum = maximum;
            this.value = value;
            scale = new GaugeScale(this);
            pointer = new GaugePointer(this);
            expression = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GaugeObject"/> class.
        /// </summary>
        /// <param name="minimum">Minimum value of gauge.</param>
        /// <param name="maximum">Maximum value of gauge.</param>
        /// <param name="value">Current value of gauge.</param>
        /// <param name="scale">Scale of gauge.</param>
        /// <param name="pointer">Pointer of gauge.</param>
        public GaugeObject(double minimum, double maximum, double value, GaugeScale scale, GaugePointer pointer)
        {
            this.minimum = minimum;
            this.maximum = maximum;
            this.value = value;
            this.scale = scale;
            this.pointer = pointer;
            expression = "";
        }

        #endregion // Constructors

        #region Report Engine

        /// <inheritdoc/>
        public override string[] GetExpressions()
        {
            List<string> expressions = new List<string>();
            expressions.AddRange(base.GetExpressions());

            if (!String.IsNullOrEmpty(Expression))
            {
                expressions.Add(Expression);
            }
            return expressions.ToArray();
        }

        /// <inheritdoc/>
        public override void GetData()
        {
            base.GetData();

            if (!String.IsNullOrEmpty(Expression))
            {
                object val = Report.Calc(Expression);
                if (val != null)
                {
                    try
                    {
                        Value = Converter.StringToFloat(val.ToString());
                    }
                    catch
                    {
                        Value = 0.0;
                    }
                }
            }
        }

        #endregion // Report Engine

        #region Public Methods

        /// <inheritdoc/>
        public override void Assign(Base source)
        {
            base.Assign(source);

            GaugeObject src = source as GaugeObject;
            Maximum = src.Maximum;
            Minimum = src.Minimum;
            Value = src.Value;
            Expression = src.Expression;
            Scale.Assign(src.Scale);
            Pointer.Assign(src.Pointer);
        }

        /// <summary>
        /// Draws the gauge.
        /// </summary>
        /// <param name="e">Draw event arguments.</param>
        public override void Draw(FRPaintEventArgs e)
        {
            base.Draw(e);
            scale.Draw(e);
            pointer.Draw(e);
        }

        /// <inheritdoc/>
        public override void Serialize(FRWriter writer)
        {
            GaugeObject c = writer.DiffObject as GaugeObject;
            base.Serialize(writer);

            if (Maximum != c.Maximum)
            {
                writer.WriteDouble("Maximum", Maximum);
            }
            if (Minimum != c.Minimum)
            {
                writer.WriteDouble("Minimum", Minimum);
            }
            if (Value != c.Value)
            {
                writer.WriteDouble("Value", Value);
            }
            if (Expression != c.Expression)
            {
                writer.WriteStr("Expression", Expression);
            }
            Scale.Serialize(writer, "Scale", c.Scale);
            Pointer.Serialize(writer, "Pointer", c.Pointer);
        }

        #endregion // Public Methods
    }
}
