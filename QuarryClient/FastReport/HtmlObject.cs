using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Globalization;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.Design;
using FastReport.Design.PageDesigners.Page;
using FastReport.Forms;
using FastReport.Format;
using FastReport.Data;
using FastReport.Code;
using FastReport.TypeConverters;
using FastReport.Engine;
using FastReport.DevComponents.DotNetBar;
using System.Text;

namespace FastReport
{

    /// <summary>
    /// Represents the Text object that may display one or several text lines.
    /// </summary>
    /// <remarks>
    /// Specify the object's text in the <see cref="TextObjectBase.Text">Text</see> property. 
    /// Text may contain expressions and data items, for example: "Today is [Date]". When report 
    /// is running, all expressions are calculated and replaced with actual values, so the text 
    /// would be "Today is 01.01.2008".
    /// <para/>The symbols used to find expressions in a text are set in the 
    /// <see cref="TextObjectBase.Brackets">Brackets</see> property. You also may disable expressions 
    /// using the <see cref="TextObjectBase.AllowExpressions">AllowExpressions</see> property.
    /// <para/>To format an expression value, use the <see cref="Format"/> property.
    /// </remarks>
    public class HtmlObject : TextObjectBase, IHasEditor
    {
        #region Fields
        private bool FRightToLeft;
        private TextBox FTextBox;
        private bool FDragAccept;
        private string FSavedText;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value that indicates whether the component should draw right-to-left for RTL languages.
        /// </summary>
        [DefaultValue(false)]
        [Category("Behavior")]
        public bool RightToLeft
        {
            get { return FRightToLeft; }
            set { FRightToLeft = value; }
        }

        /// <inheritdoc/>
        public override float Left
        {
            get { return base.Left; }
            set
            {
                base.Left = value;
                if (IsEditing)
                    UpdateEditorPosition();
            }
        }

        /// <inheritdoc/>
        public override float Top
        {
            get { return base.Top; }
            set
            {
                base.Top = value;
                if (IsEditing)
                    UpdateEditorPosition();
            }
        }

        /// <inheritdoc/>
        public override float Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                if (IsEditing)
                    UpdateEditorPosition();
            }
        }

        /// <inheritdoc/>
        public override float Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                if (IsEditing)
                    UpdateEditorPosition();
            }
        }

        private bool IsEditing
        {
            get { return IsDesigning && FTextBox != null; }
        }
        #endregion

        #region Private Methods

        private void UpdateEditorPosition()
        {
            FTextBox.Location = new Point((int)Math.Round(AbsLeft * ReportWorkspace.Scale) + 1,
              (int)Math.Round(AbsTop * ReportWorkspace.Scale) + 1);
            FTextBox.Size = new Size((int)Math.Round(Width * ReportWorkspace.Scale) - 1,
              (int)Math.Round(Height * ReportWorkspace.Scale) - 1);
        }

        private void FTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                FinishEdit(false);

            if (e.Control && e.KeyCode == Keys.Enter)
                FinishEdit(true);
        }

        private TextRenderingHint GetTextQuality(TextQuality quality)
        {
            switch (quality)
            {
                case TextQuality.Regular:
                    return TextRenderingHint.AntiAliasGridFit;

                case TextQuality.ClearType:
                    return TextRenderingHint.ClearTypeGridFit;

                case TextQuality.AntiAlias:
                    return TextRenderingHint.AntiAlias;
            }

            return TextRenderingHint.SystemDefault;
        }

        private float InternalCalcWidth()
        {
            return this.Width;
        }

        private float InternalCalcHeight()
        {
            return this.Height;
        }

        private string BreakText()
        {
            return null;
        }

        #endregion

        #region Protected Methods
        /// <inheritdoc/>
        protected override void DeserializeSubItems(FRReader reader)
        {
            base.DeserializeSubItems(reader);
        }
        #endregion

        #region Public Methods
        internal StringFormat GetStringFormat(GraphicCache cache, StringFormatFlags flags)
        {
            return GetStringFormat(cache, flags, 1);
        }

        internal StringFormat GetStringFormat(GraphicCache cache, StringFormatFlags flags, float scale)
        {
            if (RightToLeft)
                flags |= StringFormatFlags.DirectionRightToLeft;

            return cache.GetStringFormat(StringAlignment.Near, StringAlignment.Near, StringTrimming.None, flags, 0 * scale, 0 * scale);
        }

        /// <inheritdoc/>
        public override void Assign(Base source)
        {
            base.Assign(source);

            HtmlObject src = source as HtmlObject;
            RightToLeft = src.RightToLeft;
        }

        /// <inheritdoc/>
        public override void AssignFormat(ReportComponentBase source)
        {
            base.AssignFormat(source);

            HtmlObject src = source as HtmlObject;
            if (src == null)
                return;
        }

        /// <summary>
        /// Draws a text.
        /// </summary>
        /// <param name="e">Paint event data.</param>
        public void DrawText(FRPaintEventArgs e)
        {
            string text = Text;
            if (!String.IsNullOrEmpty(text))
            {
                Graphics g = e.Graphics;
                RectangleF textRect = new RectangleF(
                  (AbsLeft + Padding.Left) * e.ScaleX,
                  (AbsTop + Padding.Top) * e.ScaleY,
                  (Width - Padding.Horizontal) * e.ScaleX,
                  (Height - Padding.Vertical) * e.ScaleY);

                StringFormat format = GetStringFormat(e.Cache, 0, e.ScaleX);

                Font font = DrawUtils.DefaultTextObjectFont;

                Brush textBrush = e.Cache.GetBrush(Color.Black);

                Report report = Report;
                if (report != null && report.TextQuality != TextQuality.Default)
                    g.TextRenderingHint = GetTextQuality(report.TextQuality);

                if (textRect.Width > 0 && textRect.Height > 0)
                {
                        // use simple rendering
                        g.DrawString(text, font, textBrush, textRect, format);
                }

                if (report != null && report.TextQuality != TextQuality.Default)
                    g.TextRenderingHint = TextRenderingHint.SystemDefault;
            }
        }

        /// <inheritdoc/>
        public override void Draw(FRPaintEventArgs e)
        {
            base.Draw(e);
            DrawText(e);
            DrawMarkers(e);
            Border.Draw(e, new RectangleF(AbsLeft, AbsTop, Width, Height));

            if (FDragAccept)
                DrawDragAcceptFrame(e, Color.Silver);
        }

        /// <inheritdoc/>
        public override void HandleDragOver(FRMouseEventArgs e)
        {
            if (PointInObject(new PointF(e.X, e.Y)) && e.DragSource is TextObject)
                e.Handled = true;
            FDragAccept = e.Handled;
        }

        /// <inheritdoc/>
        public override void HandleDragDrop(FRMouseEventArgs e)
        {
            Text = (e.DragSource as TextObject).Text;
            FDragAccept = false;
        }

        /// <inheritdoc/>
        public override void HandleKeyDown(Control sender, KeyEventArgs e)
        {
            if (IsSelected && e.KeyCode == Keys.Enter && HasFlag(Flags.CanEdit) && !HasRestriction(Restrictions.DontEdit))
            {
                FTextBox = new TextBox();
                FTextBox.Font = DrawUtils.DefaultTextObjectFont;
                FTextBox.BorderStyle = BorderStyle.None;
                FTextBox.Multiline = true;
                FTextBox.AcceptsTab = true;
                if (Fill is SolidFill)
                    FTextBox.BackColor = Color.FromArgb(255, (Fill as SolidFill).Color);
                FTextBox.ForeColor = Color.FromArgb(255, Color.Black);

                FTextBox.Text = Text;
                FTextBox.KeyDown += new KeyEventHandler(FTextBox_KeyDown);
                UpdateEditorPosition();
                sender.Controls.Add(FTextBox);
                FTextBox.SelectAll();
                FTextBox.Focus();
                e.Handled = true;
            }
        }

        /// <inheritdoc/>
        public override void SelectionChanged()
        {
            FinishEdit(true);
        }

        /// <inheritdoc/>
        public override void ApplyStyle(Style style)
        {
            base.ApplyStyle(style);
        }

        /// <inheritdoc/>
        public override void SaveStyle()
        {
            base.SaveStyle();
        }

        /// <inheritdoc/>
        public override void RestoreStyle()
        {
            base.RestoreStyle();
        }

        /// <inheritdoc/>
        public override void Serialize(FRWriter writer)
        {
            HtmlObject c = writer.DiffObject as HtmlObject;
            base.Serialize(writer);

            if (writer.SerializeTo != SerializeTo.Preview)
            {
                if (Style != c.Style)
                    writer.WriteStr("Style", Style);
            }
        }

        /// <inheritdoc/>
        public override ContextMenuBar GetContextMenu()
        {
            return new HtmlObjectMenu(Report.Designer);
        }

        /// <inheritdoc/>
        public override SmartTagBase GetSmartTag()
        {
            return new HtmlObjectSmartTag(this);
        }

        /// <inheritdoc/>
        public virtual bool InvokeEditor()
        {
            using (TextEditorForm form = new TextEditorForm(Report))
            {
                form.ExpressionText = Text;
                form.Brackets = Brackets;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Text = form.ExpressionText;
                    return true;
                }
            }
            return false;
        }

        internal virtual void FinishEdit(bool accept)
        {
            if (FTextBox == null)
                return;

            if (FTextBox.Modified && accept)
            {
                Text = FTextBox.Text;
                if (Report != null)
                    Report.Designer.SetModified(null, "Change", Name);
            }
            FTextBox.Dispose();
            FTextBox = null;
        }

        internal void ApplyCondition(HighlightCondition c)
        {
            if (c.ApplyBorder)
                Border = c.Border.Clone();
            if (c.ApplyFill)
                Fill = c.Fill.Clone();
            if (!c.Visible)
                Visible = false;
        }
        #endregion

        #region Report Engine
        /// <inheritdoc/>
        public override string[] GetExpressions()
        {
            List<string> expressions = new List<string>();
            expressions.AddRange(base.GetExpressions());

            if (AllowExpressions && !String.IsNullOrEmpty(Brackets))
            {
                string[] brackets = Brackets.Split(new char[] { ',' });
                // collect expressions found in the text
                expressions.AddRange(CodeUtils.GetExpressions(Text, brackets[0], brackets[1]));
            }

            return expressions.ToArray();
        }

        /// <inheritdoc/>
        public override void SaveState()
        {
            base.SaveState();
            FSavedText = Text;
        }

        /// <inheritdoc/>
        public override void RestoreState()
        {
            base.RestoreState();
            Text = FSavedText;
        }

        /// <summary>
        /// Calculates the object's width.
        /// </summary>
        /// <returns>The width, in pixels.</returns>
        public float CalcWidth()
        {
            return InternalCalcWidth();
        }

        /// <inheritdoc/>
        public override float CalcHeight()
        {
            return InternalCalcHeight();
        }

        /// <inheritdoc/>
        public override void GetData()
        {
            base.GetData();

            // process expressions
            if (AllowExpressions)
            {
                if (!String.IsNullOrEmpty(Brackets))
                {
                    string[] brackets = Brackets.Split(new char[] { ',' });
                    FindTextArgs args = new FindTextArgs();
                    args.Text = new FastString(Text);
                    args.OpenBracket = brackets[0];
                    args.CloseBracket = brackets[1];
                    int expressionIndex = 0;

                    while (args.StartIndex < args.Text.Length)
                    {
                        string expression = CodeUtils.GetExpression(args, false);
                        if (expression == "")
                            break;

                        string formattedValue = CalcAndFormatExpression(expression, expressionIndex);
                        args.Text = args.Text.Remove(args.StartIndex, args.EndIndex - args.StartIndex);
                        args.Text = args.Text.Insert(args.StartIndex, formattedValue);
                        args.StartIndex += formattedValue.Length;
                        expressionIndex++;
                    }
                    Text = args.Text.ToString();
                }
            }

            // process highlight
            Variant varValue = new Variant(Value);
        }

        /// <inheritdoc/>
        public override bool Break(BreakableComponent breakTo)
        {
            string breakText = BreakText();
            if (breakText != null && breakTo != null)
                (breakTo as TextObject).Text = breakText;
            return breakText != null;
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlObject"/> class with default settings.
        /// </summary>
        public HtmlObject()
        {
            FlagSerializeStyle = false;
            SetFlags(Flags.HasSmartTag, true);
        }
    }
}