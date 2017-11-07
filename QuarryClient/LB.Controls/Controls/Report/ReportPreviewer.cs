using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls.Report
{
    public partial class ReportPreviewer : LBUIPageBase
    {
        FastReport.Report mReport=null;
        FastReport.Preview.PreviewControl proviewControl;
        public ReportPreviewer(FastReport.Report report)
        {
            mReport = report;
            InitializeComponent();
            IntiPreviewer();
            report.Preview = proviewControl;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            mReport.Show();
        }

        private void IntiPreviewer()
        {
            proviewControl = new FastReport.Preview.PreviewControl();

            this.proviewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
            this.proviewControl.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.proviewControl.Buttons = ((FastReport.PreviewButtons)(((((((((((FastReport.PreviewButtons.Print | FastReport.PreviewButtons.Open)
                        | FastReport.PreviewButtons.Save)
                        | FastReport.PreviewButtons.Email)
                        | FastReport.PreviewButtons.Find)
                        | FastReport.PreviewButtons.Zoom)
                        | FastReport.PreviewButtons.Outline)
                        | FastReport.PreviewButtons.PageSetup)
                        | FastReport.PreviewButtons.Edit)
                        | FastReport.PreviewButtons.Watermark)
                        | FastReport.PreviewButtons.Navigator)));
            this.proviewControl.Font = new System.Drawing.Font("Tahoma", 8F);
            this.proviewControl.Location = new System.Drawing.Point(268, 80);
            this.proviewControl.Name = "previewControl1";
            this.proviewControl.PageOffset = new System.Drawing.Point(10, 10);
            this.proviewControl.Size = new System.Drawing.Size(660, 676);
            this.proviewControl.TabIndex = 8;
            this.proviewControl.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            this.proviewControl.Dock = DockStyle.Fill;
            this.Controls.Add(proviewControl);
        }
    }
}
