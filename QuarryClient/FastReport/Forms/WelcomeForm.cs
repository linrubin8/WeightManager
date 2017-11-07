using FastReport.Design;
using FastReport.Utils;
using FastReport.Wizards;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FastReport.Forms
{
    /// <summary>
    /// Represents the Welcome window displayed on the designer startup
    /// </summary>
    public partial class WelcomeForm : Form
    {
        private Designer designer;
        private Button btnOpen;
        private const int maxItemsInColumn = 9;

        /// <summary>
        /// Initializes a new instance of the <see cref="WelcomeForm"/> class.
        /// </summary>
        /// <param name="designer"></param>
        public WelcomeForm(Designer designer)
        {
            InitializeComponent();

            this.designer = designer;

            setupLeftPanel();
            setupRightPanel();
            Localize();
            cbShowOnStartup.Checked = Config.WelcomeShowOnStartup;

            Config.DesignerSettings.ReportLoaded += DesignerSettings_ReportLoaded;

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                panelLeft.Left = ClientSize.Width - panelLeft.Left - panelLeft.Width;
                panelRight.Left = ClientSize.Width - panelRight.Left - panelRight.Width;
                cbShowOnStartup.Left = ClientSize.Width - cbShowOnStartup.Left - cbShowOnStartup.Width;
                cbShowOnStartup.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            }
        }

        #region Setup
        private void setupLeftPanel()
        {
            int i = 0;

            // Open item
            btnOpen = createButton
            (
                open_Click,
                "Open...",
                66,
                getX(),
                getY(i),
                panelLeft,
                null,
                true,
                null
            );

            i++;

            // Recent items
            if (designer.cmdRecentFiles.Enabled && designer.RecentFiles.Count > 0)
            {
                for (int k = designer.RecentFiles.Count - 1; k >= 0; k--)
                {
                    string file = designer.RecentFiles[k];

                    createButton
                    (
                        recent_Click,
                        Path.GetFileName(file),
                        0,
                        getX(),
                        getY(i),
                        panelLeft,
                        file,
                        true,
                        file
                    );

                    i++;
                    if (i >= maxItemsInColumn)
                        break;
                }
            }
        }

        private void setupRightPanel()
        {
            List<ObjectInfo> objects = new List<ObjectInfo>();
            RegisteredObjects.Objects.EnumItems(objects);

            int i = 0;

            // Wizards
            foreach (ObjectInfo info in objects)
            {
                if (info.Object != null &&
                    info.Object.IsSubclassOf(typeof(WizardBase)) &&
                    info.Flags == 0)
                {

                    createButton
                    (
                        new_Click,
                        Res.TryGet(info.Text),
                        info.ImageIndex,
                        getX(),
                        getY(i),
                        panelRight,
                        null,
                        true,
                        info
                    );

                    i++;
                    if (i >= maxItemsInColumn)
                        break;
                }
            }
        }

        private void Localize()
        {
            MyRes res = new MyRes("Designer,Welcome");
            Text = res.Get("Title");
            cbShowOnStartup.Text = res.Get("Show");
            lblOpen.Text = res.Get("Open");
            lblNew.Text = res.Get("New");
            btnOpen.Text = " " + Res.Get("Designer,Menu,File,Open");
        }
        #endregion

        #region Events
        private void open_Click(object sender, EventArgs e)
        {
            designer.cmdOpen.Invoke();
        }

        private void recent_Click(object sender, EventArgs e)
        {
            designer.UpdatePlugins(null);
            designer.cmdOpen.LoadFile((sender as Button).Tag as string);
        }

        private void new_Click(object sender, EventArgs e)
        {
            (Activator.CreateInstance(((sender as Button).Tag as ObjectInfo).Object) as WizardBase).Run(designer);
        }

        private void table_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Column == 0 && e.Row == 0)
            {
                int t = 15;
                e.Graphics.DrawLine(new Pen(Color.DarkGray),
                    e.CellBounds.Right,
                    e.CellBounds.Top + t,
                    e.CellBounds.Right,
                    e.CellBounds.Bottom - t);
            }
        }

        private void bottom_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.LightGray),
                e.ClipRectangle.Left,
                e.ClipRectangle.Top,
                e.ClipRectangle.Right,
                e.ClipRectangle.Top);
        }

        private void DesignerSettings_ReportLoaded(object sender, ReportLoadedEventArgs e)
        {
            Config.DesignerSettings.ReportLoaded -= DesignerSettings_ReportLoaded;
            Close();
        }

        private void cbShowOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            Config.WelcomeShowOnStartup = cbShowOnStartup.Checked;
        }
        #endregion

        #region Utils
        private Button createButton(EventHandler onClick,
                                    string text,
                                    int icon,
                                    int x,
                                    int y,
                                    Control parent,
                                    string tooltipText,
                                    bool trimText,
                                    object tag)
        {
            Button b = new Button();

            b.Tag = tag;
            if (onClick != null)
                b.Click += onClick;

            b.Location = new Point(x, y);
            b.Height = 28;
            b.Width = parent.Width - x - x;

            b.Text = " " + text;
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.TextImageRelation = TextImageRelation.ImageBeforeText;

            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.BorderColor = parent != null ? parent.BackColor : Color.White;
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(-2628366);
            b.FlatAppearance.MouseDownBackColor = Color.FromArgb(-4599318);

            b.Image = Res.GetImage(icon);
            b.ImageAlign = ContentAlignment.TopLeft;

            if (tooltipText != null && tooltipText.Trim() != "")
            {
                ToolTip tooltip = new ToolTip();
                tooltip.SetToolTip(b, tooltipText);
            }

            if (trimText)
                trim(b);

            if (parent != null)
                parent.Controls.Add(b);

            return b;
        }

        private int getX()
        {
            return 40;
        }

        private int getY(int i)
        {
            return i * 28 + 45;
        }

        private bool trim(Control control)
        {
            string txt = control.Text;

            if (txt.Length == 0 || control.Width == 0)
                return false;

            bool trimmed = false;
            int i = txt.Length;
            int iconWidth = 30;

            while (TextRenderer.MeasureText(txt + "...", control.Font).Width > control.Width - iconWidth)
            {
                txt = control.Text.Substring(0, --i);
                trimmed = true;
                if (i == 0)
                    break;
            }

            control.Text = txt + (trimmed ? "..." : "");
            return trimmed;
        }
        #endregion
    }
}
