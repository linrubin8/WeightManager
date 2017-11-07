using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.PageDesigners.Page
{
    internal class RulerPanel : SplitContainer
    {
        #region Fields
        private HorzRuler FHorzRuler;
        private VertRuler FVertRuler;
        private Button btnSwitchView;
        private ControlContainer FControlContainer;
        private BandStructure FStructure;
        private ReportPageDesigner FPageDesigner;
        private ReportWorkspace FWorkspace;
        private ReportPage FPage;
        #endregion

        #region Properties
        public HorzRuler HorzRuler
        {
            get { return FHorzRuler; }
        }

        public VertRuler VertRuler
        {
            get { return FVertRuler; }
        }

        public BandStructure Structure
        {
            get { return FStructure; }
        }

        public ReportWorkspace Workspace
        {
            get { return FWorkspace; }
        }

        private Designer Designer
        {
            get { return FPageDesigner.Designer; }
        }
        #endregion

        #region Private Methods
        private void AdjustOffset()
        {
            FHorzRuler.Offset = FWorkspace.Left + 24;
            FHorzRuler.Refresh();
            FVertRuler.Offset = FWorkspace.Top;
            FVertRuler.Refresh();
            FStructure.Offset = FWorkspace.Top;
            FStructure.Refresh();
        }

        private void Workspace_LocationChanged(object sender, EventArgs e)
        {
            AdjustOffset();
        }

        private void btnSwitchView_Click(object sender, EventArgs e)
        {
            ReportWorkspace.ClassicView = !ReportWorkspace.ClassicView;
            FPageDesigner.UpdateContent();
        }

        private void RulerPanel_SplitterMoved(object sender, SplitterEventArgs e)
        {
            FWorkspace.Focus();
        }
        #endregion

        public override void Refresh()
        {
            base.Refresh();
            FWorkspace.Refresh();
            FHorzRuler.Refresh();
            FVertRuler.Refresh();
            FStructure.Refresh();
        }

        public void SetStructureVisible(bool visible)
        {
            FStructure.Visible = visible;
            Panel1Collapsed = !visible;
        }

        public void UpdateUIStyle()
        {
            FControlContainer.ColorSchemeStyle = UIStyleUtils.GetDotNetBarStyle(Designer.UIStyle);
            FControlContainer.Office2007ColorTable = UIStyleUtils.GetOffice2007ColorScheme(Designer.UIStyle);
            Color color = UIStyleUtils.GetControlColor(Designer.UIStyle);

            FStructure.BackColor = color;
            FHorzRuler.BackColor = color;
            FVertRuler.BackColor = color;
            FWorkspace.BackColor = color;
            BackColor = color;
        }

        public RulerPanel(ReportPageDesigner pd) : base()
        {
            FPageDesigner = pd;
            FPage = pd.Page as ReportPage;
            FWorkspace = new ReportWorkspace(FPageDesigner);
            FWorkspace.LocationChanged += new EventHandler(Workspace_LocationChanged);

            FHorzRuler = new HorzRuler(pd);
            FHorzRuler.Height = 24;
            FHorzRuler.Dock = DockStyle.Top;
            FVertRuler = new VertRuler(pd);
            FVertRuler.Width = 24;

            btnSwitchView = new Button();
            btnSwitchView.Location = new Point(4, 4);
            btnSwitchView.Size = new Size(16, 16);

            // apply Right to Left layout if needed
            FVertRuler.Dock = Config.RightToLeft ? DockStyle.Right : DockStyle.Left;
            if (Config.RightToLeft)
            {
                btnSwitchView.Dock = DockStyle.Right;
                FHorzRuler.Left -= btnSwitchView.Width;
            }

            btnSwitchView.FlatStyle = FlatStyle.Flat;
            btnSwitchView.FlatAppearance.BorderColor = SystemColors.ButtonFace;
            btnSwitchView.FlatAppearance.BorderSize = 0;
            btnSwitchView.Cursor = Cursors.Hand;
            btnSwitchView.Image = Res.GetImage(81);
            btnSwitchView.Click += new EventHandler(btnSwitchView_Click);
            FHorzRuler.Controls.Add(btnSwitchView);

            FStructure = new BandStructure(FPageDesigner);
            FStructure.Dock = DockStyle.Fill;

            FControlContainer = new ControlContainer(this);
            FControlContainer.Dock = DockStyle.Fill;

            Panel1.Controls.Add(FStructure);
            Panel2.Controls.AddRange(new Control[] { FControlContainer, FVertRuler, FHorzRuler });
            Panel1MinSize = 20;
            FixedPanel = FixedPanel.Panel1;
            SplitterDistance = 120;
            SplitterMoved += new SplitterEventHandler(RulerPanel_SplitterMoved);

            AdjustOffset();
        }


        private class ControlContainer : PanelX
        {
            private RulerPanel FRulerPanel;
            private float oldScale;
            private Point scrollPos;
            private Point mousePositionOnContent;
            private bool FAllowPaint = true;
            private const int WM_PAINT = 0x000F;

            private void UpdateContentLocation()
            {
                FRulerPanel.Workspace.Location = new Point(AutoScrollPosition.X, AutoScrollPosition.Y);
            }

            private void content_Resize(object sender, EventArgs e)
            {
                Size maxSize = new Size(FRulerPanel.Workspace.Width + 10, FRulerPanel.Workspace.Height + 10);
                if (maxSize.Width > Width)
                    maxSize.Height += SystemInformation.HorizontalScrollBarHeight;
                if (maxSize.Height > Height)
                    maxSize.Width += SystemInformation.VerticalScrollBarWidth;
                AutoScrollMinSize = maxSize;
                AutoScrollPosition = AutoScrollPosition;
                UpdateContentLocation();
                Refresh();
            }

            protected override void OnScroll(ScrollEventArgs se)
            {
                base.OnScroll(se);
                UpdateContentLocation();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                // draw shadow around page
                ShadowPaintInfo pi = new ShadowPaintInfo();
                pi.Graphics = e.Graphics;
                pi.Rectangle = FRulerPanel.Workspace.Bounds;
                pi.Size = 6;
                ShadowPainter.Paint2(pi);
            }

            protected override void WndProc(ref Message m)
            {
                if ((m.Msg != WM_PAINT) || (FAllowPaint && m.Msg == WM_PAINT))
                {
                    base.WndProc(ref m);
                }
            }

            public ControlContainer(RulerPanel rulerPanel)
            {
                FRulerPanel = rulerPanel;
                AutoScroll = true;
                FastScrolling = false;
                Controls.Add(FRulerPanel.Workspace);
                FRulerPanel.Workspace.Resize += new EventHandler(content_Resize);
                FRulerPanel.Workspace.BeforeZoom += new EventHandler(content_BeforeZoom);
                FRulerPanel.Workspace.AfterZoom += new EventHandler(content_AfterZoom);
            }

            private void content_BeforeZoom(object sender, EventArgs e)
            {
                // avoid unnecessary redraws while zooming
                FAllowPaint = false;
                FRulerPanel.VertRuler.AllowPaint = false;
                FRulerPanel.HorzRuler.AllowPaint = false;
                FRulerPanel.Structure.AllowPaint = false;
                //TODO ¡÷»Í±Û ‘› ±◊¢ Õ
                //VScrollBar.AllowPaint = false;
                //HScrollBar.AllowPaint = false;

                oldScale = ReportWorkspace.Scale;
                scrollPos = FRulerPanel.Workspace.Location;

                Point mousePositionOnScreen = PointToClient(Cursor.Position);
                if (mousePositionOnScreen.X < 0 ||
                    mousePositionOnScreen.X > Width ||
                    mousePositionOnScreen.Y < 0 ||
                    mousePositionOnScreen.Y > Height)
                    mousePositionOnScreen = new Point(Width / 2, Height / 2);

                mousePositionOnContent = new Point(
                    (int)((mousePositionOnScreen.X - scrollPos.X) / oldScale),
                    (int)((mousePositionOnScreen.Y - scrollPos.Y) / oldScale));
            }

            private void content_AfterZoom(object sender, EventArgs e)
            {
                float zoomchange = ReportWorkspace.Scale - oldScale;
                float offsetX = mousePositionOnContent.X * zoomchange;
                float offsetY = mousePositionOnContent.Y * zoomchange;
                scrollPos.X -= (int)offsetX;
                scrollPos.Y -= (int)offsetY;
                AutoScrollPosition = scrollPos;

                // redraw
                FRulerPanel.VertRuler.AllowPaint = true;
                FRulerPanel.HorzRuler.AllowPaint = true;
                FRulerPanel.Structure.AllowPaint = true;
                //TODO ¡÷»Í±Û ‘› ±◊¢ Õ
                //VScrollBar.AllowPaint = true;
                //HScrollBar.AllowPaint = true;
                UpdateContentLocation();
                FAllowPaint = true;
                Refresh();
            }
        }
    }
}
