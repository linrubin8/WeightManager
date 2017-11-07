using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.Forms
{
    internal partial class OutlineEditorForm : BaseDialogForm
    {
        #region Fields

        private TextOutline outline;

        #endregion // Fields

        #region Properties

        public TextOutline Outline
        {
            get { return outline; }
            set
            {
                outline = value;
                UpdateControls();
            }
        }

        #endregion // Properties

        #region Constructors

        public OutlineEditorForm()
        {
            InitializeComponent();
            Localize();
        }

        #endregion // Constructors

        #region Private Methods

        private void UpdateControls()
        {
            cbxEnabled.Checked = Outline.Enabled;
            gbLine.Enabled = Outline.Enabled;

            cbxLineStyle.SelectedItem = Outline.Style.ToString();
            cbxLineWidth.LineWidth = Outline.Width;
            cbxLineColor.Color = Outline.Color;
            cbxDrawBehind.Checked = Outline.DrawBehind;
        }

        #endregion // Private Methods

        #region Public Methods

        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Forms,OutlineEditor");
            this.Text = res.Get("");
            this.cbxEnabled.Text = res.Get("Enabled");
            this.lblStyle.Text = res.Get("Style");
            this.lblLineColor.Text = res.Get("Color");
            this.lblLineWidth.Text = res.Get("Width");
            this.cbxDrawBehind.Text = res.Get("DrawBehind");
        }

        #endregion // Public Methods

        #region Event Handlers

        private void cbxLineStyle_SelectedValueChanged(object sender, EventArgs e)
        {
            Outline.Style = (DashStyle)Enum.Parse(typeof(DashStyle), cbxLineStyle.SelectedItem.ToString());
        }

        private void cbxLineWidth_WidthSelected(object sender, EventArgs e)
        {
            Outline.Width = cbxLineWidth.LineWidth;
        }

        private void cbxLineColor_ColorSelected(object sender, EventArgs e)
        {
            Outline.Color = cbxLineColor.Color;
        }

        private void cbxEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Outline.Enabled = cbxEnabled.Checked;
            UpdateControls();
        }

        private void cbxDrawBehind_CheckedChanged(object sender, EventArgs e)
        {
            Outline.DrawBehind = cbxDrawBehind.Checked;
        }

        #endregion // Event Handlers
    }
}