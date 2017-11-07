using FastReport.DevComponents.DotNetBar;
using FastReport.Controls;
using FastReport.Design.PageDesigners.Page;
using FastReport.Design.ToolWindows;
using FastReport.Forms;
using FastReport.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FastReport.Design.StandardDesigner
{  
    /// <summary>
    /// Represents standard designer's form.
    /// </summary>
    /// <remarks>
    /// This form contains the <see cref="DesignerControl"/>. Use the <see cref="Designer"/> 
    /// property to get access to this control.
    /// <para/>Usually you don't need to create an instance of this class. The designer can be called
    /// using the <see cref="FastReport.Report.Design()"/> method of 
    /// the <see cref="FastReport.Report"/> instance.
    /// <para/>If you decided to use this class, you need:
    /// <list type="bullet">
    ///   <item>
    ///     <description>create an instance of this class;</description>
    ///   </item>
    ///   <item>
    ///     <description>set the <b>Designer.Report</b> property to report that you need to design;</description>
    ///   </item>
    ///   <item>
    ///     <description>call either <b>ShowModal</b> or <b>Show</b> methods to display a form.</description>
    ///   </item>
    /// </list>
    /// </remarks>
    public partial class DesignerForm : Form, IDesignerPlugin
    {
        #region Vars
        /// <summary>
        /// Gets a reference to the <see cref="Designer"/> control which is actually a designer.
        /// </summary>
        public DesignerControl Designer
        {
            get { return designer; }
        }

        /// <summary>
        /// Gets a list of File menu buttons
        /// </summary>
        public Dictionary<string, ButtonItem> Items
        {
            get
            {
                if (items == null)
                {
                    items = new Dictionary<string, ButtonItem>();
                    items.Add("btnFileNew", this.btnFileNew);
                    items.Add("btnFileOpen", this.btnFileOpen);
                    items.Add("btnFileSave", this.btnFileSave);
                    items.Add("btnFileSaveAs", this.btnFileSaveAs);
                    items.Add("btnFileSaveAll", this.btnFileSaveAll);
                    items.Add("btnFilePreview", this.btnFilePreview);
                    items.Add("btnFilePrinterSetup", this.btnFilePrinterSetup);
                    items.Add("btnFileSelectLanguage", this.btnFileSelectLanguage);
                    items.Add("btnWelcome", this.btnWelcome);
                    items.Add("btnHelp", this.btnHelp);
                    items.Add("btnAbout", this.btnAbout);
                    items.Add("btnFileClose", this.btnFileClose);
                }

                return items;
            }
        }

        private float Zoom
        {
            get { return ReportWorkspace.Scale; }
            set
            {
                if (Workspace != null)
                    Workspace.Zoom(value);
            }
        }

        private ReportWorkspace Workspace
        {
            get
            {
                if (designer.ActiveReportTab != null && designer.ActiveReportTab.ActivePageDesigner is ReportPageDesigner)
                    return (Designer.ActiveReportTab.ActivePageDesigner as ReportPageDesigner).Workspace;
                return null;
            }
        }

        private ReportPageDesigner ReportPageDesigner
        {
            get
            {
                //Fixed try catch 66fcd219-30f1-45e6-8ee5-ce65cfb9d35d
                //rly annoying freezes on designer start
                ReportTab tab = Designer.ActiveReportTab;
                if (tab != null)
                    return tab.ActivePageDesigner as ReportPageDesigner;
                return null;
            }
        }

        private PageBase Page
        {
            get
            {
                //Fixed try catch 66fcd219-30f1-45e6-8ee5-ce65cfb9d35d
                //rly annoying freezes on designer start
                ReportPageDesigner d = ReportPageDesigner;
                if (d != null)
                    return d.Page;
                return null;
            }
        }

        private DesignerControl designer;
        private Dictionary<string, ButtonItem> items;
        private Timer clipboardTimer;
        private Timer previewTimer;
        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="DesignerForm"/> class with default settings.
        /// </summary>
        public DesignerForm()
        {
            InitDesigner();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DesignerForm"/> class with default settings.
        /// </summary>
        /// <param name="welcome">enables welcome window</param>
        public DesignerForm(bool welcome)
        {
            InitDesigner();
            if (welcome)
                Shown += showWelcomeWindow;
        }

        private void InitDesigner()
        {
            InitializeComponent();

            Font = DrawUtils.DefaultFont;
            Icon = Config.DesignerSettings.Icon;

            designer = new DesignerControl(location, size, text);
            Controls.Add(designer);
            designer.Dock = DockStyle.Fill;
            designer.BringToFront();
            designer.Plugins.Add(this);
            designer.UIStyle = Config.UIStyle;
            designer.ShowStatusBar = false;

            setupStatusBar();
            setupControls();

            Localize();

            RightToLeft = Config.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
        }

        private void showWelcomeWindow(object s, EventArgs e)
        {
            Shown -= showWelcomeWindow;

            if (Config.WelcomeEnabled &&
                Config.WelcomeShowOnStartup &&
                String.IsNullOrEmpty(designer.Report.FileName))
            {
                Designer.cmdWelcome.Invoke();
            }
        }

        #region Utils
        private void createButton(ButtonItem button, Bitmap image, EventHandler click)
        {
            button.Image = image;
            button.Click += click;
        }

        private void setItemText(BaseItem item, string text)
        {
            setItemText(item, text, text);
        }

        private void setItemText(BaseItem item, string text, string tooltip)
        {
            item.Text = text;
            item.Tooltip = tooltip;
        }
        #endregion

        #region Setup Controls
        private void setupControls()
        {
            setupFileControls();
            setupHomeControls();
            setupReportControls();
            setupLayoutControls();
            setupViewControls();
        }

        private void setupFileControls()
        {
            Bitmap cap = new Bitmap(32, 32);

            btnFile.PopupOpen += miFile_PopupOpen;

            btnFileNew.Click += Designer.cmdNew.Invoke;

            //btnFileOpen.Image = Res.GetImage(1);
            btnFileOpen.Click += Designer.cmdOpen.Invoke;

            btnFileClose.Click += Designer.cmdClose.Invoke;
            
            //btnFileSave.Image = Res.GetImage(2);
            btnFileSave.Click += Designer.cmdSave.Invoke;

            btnFileSaveAs.Click += Designer.cmdSaveAs.Invoke;
            
            //btnFileSaveAll.Image = Res.GetImage(178);
            btnFileSaveAll.Click += Designer.cmdSaveAll.Invoke;

            //btnFilePageSetup = CreateMenuItem(Designer.cmdPageSetup.Invoke);

            btnFilePrinterSetup.Click += Designer.cmdPrinterSetup.Invoke;

            btnFilePreview.Image = cap;
            btnFilePreview.Click += btnPreview_Click;

            btnFileSelectLanguage.Click += Designer.cmdSelectLanguage.Invoke;
            btnFileSelectLanguage.Image = cap;

            btnFileExit.Click += Designer.Exit;

            btnOptions.Click += Designer.cmdOptions.Invoke;

            btnWelcome.Visible = Designer.cmdWelcome.Enabled;
            btnWelcome.Click += Designer.cmdWelcome.Invoke;
            btnWelcome.Image = cap;

            btnHelp.Click += Designer.cmdHelpContents.Invoke;
            btnHelp.Image = cap;

            btnAbout.Click += Designer.cmdAbout.Invoke;
            btnAbout.Image = cap;
        }

        private void setupHomeControls()
        {
            //-------------------------------------------------------------------
            // Undo
            //-------------------------------------------------------------------

            createButton(btnUndo, Res.GetImage(8), Designer.cmdUndo.Invoke);
            createButton(btnRedo, Res.GetImage(9), Designer.cmdRedo.Invoke);

            //-------------------------------------------------------------------
            // Clipboard
            //-------------------------------------------------------------------

            createButton(btnCut, Res.GetImage(5), Designer.cmdCut.Invoke);
            createButton(btnCopy, Res.GetImage(6), Designer.cmdCopy.Invoke);
            createButton(btnPaste, ResourceLoader.GetBitmap("buttons.007.png"), Designer.cmdPaste.Invoke);
            createButton(btnFormatPainter, Res.GetImage(18), Designer.cmdFormatPainter.Invoke);

            clipboardTimer = new Timer();
            clipboardTimer.Interval = 500;
            clipboardTimer.Tick += clipboardTimer_Tick;
            clipboardTimer.Start();

            //-------------------------------------------------------------------
            // Text
            //-------------------------------------------------------------------

            cbxFontName.FontSelected += cbxName_FontSelected;
            cbxFontSize.SizeSelected += cbxSize_SizeSelected;
            btnTextColor.Click += btnColor_Click;
            btnTextColor.ImageIndex = 23;
            btnTextColor.SetStyle(designer.UIStyle);

            createButton(btnBold, Res.GetImage(20), btnBold_Click);
            createButton(btnItalic, Res.GetImage(21), btnItalic_Click);
            createButton(btnUnderline, Res.GetImage(22), btnUnderline_Click);
            createButton(btnAlignLeft, Res.GetImage(25), btnLeft_Click);
            createButton(btnAlignCenter, Res.GetImage(26), btnCenter_Click);
            createButton(btnAlignRight, Res.GetImage(27), btnRight_Click);
            createButton(btnJustify, Res.GetImage(28), btnJustify_Click);
            createButton(btnAlignTop, Res.GetImage(29), btnTop_Click);
            createButton(btnAlignMiddle, Res.GetImage(30), btnMiddle_Click);
            createButton(btnAlignBottom, Res.GetImage(31), btnBottom_Click);
            createButton(btnTextRotation, Res.GetImage(64), btnRotation_Click);

            //-------------------------------------------------------------------
            // Border and Fill
            //-------------------------------------------------------------------

            createButton(btnTopLine, Res.GetImage(32), btnTopLine_Click);
            createButton(btnBottomLine, Res.GetImage(33), btnBottomLine_Click);
            createButton(btnLeftLine, Res.GetImage(34), btnLeftLine_Click);
            createButton(btnRightLine, Res.GetImage(35), btnRightLine_Click);
            createButton(btnAllLines, Res.GetImage(36), btnAll_Click);
            createButton(btnNoLines, Res.GetImage(37), btnNone_Click);

            btnFillColor.ImageIndex = 38;
            btnFillColor.DefaultColor = Color.Transparent;
            btnFillColor.Click += btnFillColor_Click;

            createButton(btnFillProps, Res.GetImage(141), btnFillProps_Click);

            btnLineColor.ImageIndex = 39;
            btnLineColor.DefaultColor = Color.Black;
            btnLineColor.Click += btnLineColor_Click;

            btnLineWidth.Image = Res.GetImage(71);
            btnLineWidth.WidthSelected += cbxWidth_WidthSelected;

            btnLineStyle.Image = Res.GetImage(85);
            btnLineStyle.StyleSelected += cbxLineStyle_StyleSelected;

            createButton(btnBorderProps, Res.GetImage(40), btnBorderProps_Click);

            //-------------------------------------------------------------------
            // Format
            //-------------------------------------------------------------------

            createButton(btnHighlight, ResourceLoader.GetBitmap("buttons.024.png"), btnHighlight_Click);
            createButton(btnFormat, ResourceLoader.GetBitmap("buttons.019.png"), btnFormat_Click);

            //-------------------------------------------------------------------
            // Styles
            //-------------------------------------------------------------------

            cbxStyles.StyleSelected += cbxStyle_StyleSelected;
            createButton(btnStyles, Res.GetImage(87), Designer.cmdReportStyles.Invoke);

            //-------------------------------------------------------------------
            // Editing
            //-------------------------------------------------------------------

            createButton(btnFind, Res.GetImage(181), Designer.cmdFind.Invoke);
            createButton(btnReplace, ResourceLoader.GetBitmap("buttons.069.png"), Designer.cmdReplace.Invoke);
            createButton(btnSelectAll, ResourceLoader.GetBitmap("buttons.100.png"), Designer.cmdSelectAll.Invoke);

            //-------------------------------------------------------------------
        }

        private void setupReportControls()
        {
            createButton(btnReportOptions, ResourceLoader.GetBitmap("buttons.Report1.png"), Designer.cmdReportSettings.Invoke);
            createButton(btnPreview, ResourceLoader.GetBitmap("buttons.report.png"), btnPreview_Click);

            createButton(btnDataChoose, ResourceLoader.GetBitmap("buttons.ChooseData1.png"), Designer.cmdChooseData.Invoke);
            createButton(btnDataAdd, ResourceLoader.GetBitmap("buttons.AddDataSource.png"), Designer.cmdAddData.Invoke);

            createButton(btnAddPage, Res.GetImage(10), Designer.cmdNewPage.Invoke);
            createButton(btnAddDialog, Res.GetImage(11), Designer.cmdNewDialog.Invoke);
            createButton(btnDeletePage, Res.GetImage(12), Designer.cmdDeletePage.Invoke);
            createButton(btnPageSetup, ResourceLoader.GetBitmap("buttons.PageSetup.png"), Designer.cmdPageSetup.Invoke);

            createButton(btnConfigureBands, ResourceLoader.GetBitmap("buttons.Bands.png"), miInsertBands_Click);
            createButton(btnGroupExpert, ResourceLoader.GetBitmap("buttons.Grouping.png"), miReportGroupExpert_Click);

            btnReportTitle.Click += miReportTitle_Click;
            btnReportSummary.Click += miReportSummary_Click;
            btnPageHeader.Click += miPageHeader_Click;
            btnPageFooter.Click += miPageFooter_Click;
            btnColumnHeader.Click += miColumnHeader_Click;
            btnColumnFooter.Click += miColumnFooter_Click;
            btnOverlay.Click += miOverlay_Click;

            previewTimer = new Timer();
            previewTimer.Interval = 20;
            previewTimer.Tick += previewTimer_Tick;
        }

        private void setupLayoutControls()
        {
            createButton(btnAlignToGrid, ResourceLoader.GetBitmap("buttons.098.png"), btnAlignToGrid_Click);
            createButton(btnFitToGrid, ResourceLoader.GetBitmap("buttons.FitToGrid.png"), btnSizeToGrid_Click);
            createButton(btnAlignLefts, Res.GetImage(41), btnAlignLefts_Click);
            createButton(btnAlignCenters, Res.GetImage(42), btnAlignCenters_Click);
            createButton(btnAlignRights, Res.GetImage(45), btnAlignRights_Click);
            createButton(btnAlignTops, Res.GetImage(46), btnAlignTops_Click);
            createButton(btnAlignMiddles, Res.GetImage(47), btnAlignMiddles_Click);
            createButton(btnAlignBottoms, Res.GetImage(50), btnAlignBottoms_Click);
            createButton(btnSameWidth, Res.GetImage(83), btnSameWidth_Click);
            createButton(btnSameHeight, Res.GetImage(84), btnSameHeight_Click);
            createButton(btnSameSize, Res.GetImage(91), btnSameSize_Click);
            createButton(btnSpaceHorizontally, Res.GetImage(44), btnSpaceHorizontally_Click);
            createButton(btnIncreaseHorizontalSpacing, Res.GetImage(92), btnIncreaseHorizontalSpacing_Click);
            createButton(btnDecreaseHorizontalSpacing, Res.GetImage(93), btnDecreaseHorizontalSpacing_Click);
            createButton(btnRemoveHorizontalSpacing, Res.GetImage(94), btnRemoveHorizontalSpacing_Click);
            createButton(btnSpaceVertically, Res.GetImage(49), btnSpaceVertically_Click);
            createButton(btnIncreaseVerticalSpacing, Res.GetImage(95), btnIncreaseVerticalSpacing_Click);
            createButton(btnDecreaseVerticalSpacing, Res.GetImage(96), btnDecreaseVerticalSpacing_Click);
            createButton(btnRemoveVerticalSpacing, Res.GetImage(97), btnRemoveVerticalSpacing_Click);
            createButton(btnCenterHorizontally, Res.GetImage(43), btnCenterHorizontally_Click);
            createButton(btnCenterVertically, Res.GetImage(48), btnCenterVertically_Click);
            createButton(btnBringToFront, ResourceLoader.GetBitmap("buttons.BringToFront.png"), Designer.cmdBringToFront.Invoke);
            createButton(btnSendToBack, ResourceLoader.GetBitmap("buttons.SendToBack.png"), Designer.cmdSendToBack.Invoke);

            btnAlignment.Image = ResourceLoader.GetBitmap("buttons.AlignMenu.png");
            btnSize.Image = ResourceLoader.GetBitmap("buttons.SizeMenu.png");
            btnSpacing.Image = ResourceLoader.GetBitmap("buttons.SpacingMenu.png");

            createButton(btnGroup, ResourceLoader.GetBitmap("buttons.Group.png"), Designer.cmdGroup.Invoke);
            createButton(btnUngroup, ResourceLoader.GetBitmap("buttons.Ungroup.png"), Designer.cmdUngroup.Invoke);
        }

        private void setupViewControls()
        {
            createButton(btnViewGrid, ResourceLoader.GetBitmap("buttons.ViewGridlines.png"), MenuViewGrid_Click);
            createButton(btnViewGuides, ResourceLoader.GetBitmap("buttons.ViewGuides.png"), MenuViewGuides_Click);
            btnAutoGuides.Click += MenuViewAutoGuides_Click;
            btnDeleteHGuides.Click += MenuViewDeleteHGuides_Click;
            btnDeleteVGuides.Click += MenuViewDeleteVGuides_Click;

            btnPanels.Image = ResourceLoader.GetBitmap("buttons.Panels.png");

            btnProperties.Image = Res.GetImage(68);
            btnProperties.Click += delegate(object s, EventArgs e)
            {
                if(designer.PropertiesWindow.Visible)
                    designer.PropertiesWindow.Hide();
                else
                    designer.PropertiesWindow.Show();
            };
            btnData.Image = Res.GetImage(72);
            btnData.Click += delegate(object s, EventArgs e)
            {
                if (designer.DataWindow.Visible)
                    designer.DataWindow.Hide();
                else
                    designer.DataWindow.Show();
            };
            btnReportTree.Image = Res.GetImage(189);
            btnReportTree.Click += delegate(object s, EventArgs e)
            {
                if (designer.ReportTreeWindow.Visible)
                    designer.ReportTreeWindow.Hide();
                else
                    designer.ReportTreeWindow.Show();
            };
            btnMessages.Image = Res.GetImage(70);
            btnMessages.Click += delegate(object s, EventArgs e)
            {
                if (designer.MessagesWindow.Visible)
                    designer.MessagesWindow.Hide();
                else
                    designer.MessagesWindow.Show();
            };

            btnUnits.Image = ResourceLoader.GetBitmap("buttons.013.png");
            btnUnitsMillimeters.Click += miViewUnits_Click;
            btnUnitsCentimeters.Click += miViewUnits_Click;
            btnUnitsInches.Click += miViewUnits_Click;
            btnUnitsHundrethsOfInch.Click += miViewUnits_Click;
        }
        #endregion

        #region Update Controls
        private void updateControls()
        {
            updateFileControls();
            updateHomeControls();
            updateReportControls();
            updateLayoutControls();
            updateViewControls();
        }

        private void updateFileControls()
        {
            btnFileNew.Enabled = Designer.cmdNew.Enabled;
            btnFileOpen.Enabled = Designer.cmdOpen.Enabled;
            btnFileClose.Enabled = Designer.cmdClose.Enabled;
            btnFileClose.Visible = Designer.MdiMode;
            btnFileSave.Enabled = Designer.cmdSave.Enabled;
            btnFileSaveAs.Enabled = Designer.cmdSaveAs.Enabled;
            btnFileSaveAll.Visible = Designer.MdiMode;
            btnFileSaveAll.Enabled = Designer.cmdSaveAll.Enabled;
            //btnFilePageSetup.Enabled = Designer.cmdPageSetup.Enabled;
            btnFilePrinterSetup.Enabled = Designer.cmdPrinterSetup.Enabled;
            btnFilePreview.Enabled = Designer.cmdPreview.Enabled;
            btnWelcome.Enabled = Designer.cmdWelcome.Enabled;
            btnHelp.Enabled = Designer.cmdHelpContents.Enabled;
        }

        private void updateHomeControls()
        {
            //-------------------------------------------------------------------
            // Undo
            //-------------------------------------------------------------------

            btnUndo.Enabled = Designer.cmdUndo.Enabled;
            btnRedo.Enabled = Designer.cmdRedo.Enabled;

            //-------------------------------------------------------------------
            // Clipboard
            //-------------------------------------------------------------------

            btnCut.Enabled = Designer.cmdCut.Enabled;
            btnCopy.Enabled = Designer.cmdCopy.Enabled;
            btnPaste.Enabled = Designer.cmdPaste.Enabled;
            btnFormatPainter.Enabled = Designer.cmdFormatPainter.Enabled;
            btnFormatPainter.Checked = Designer.FormatPainter;

            //-------------------------------------------------------------------
            // Text
            //-------------------------------------------------------------------

            bool enabled = Designer.SelectedTextObjects.Enabled;

            cbxFontName.Enabled = enabled;
            cbxFontSize.Enabled = enabled;
            btnBold.Enabled = enabled;
            btnItalic.Enabled = enabled;
            btnUnderline.Enabled = enabled;
            btnAlignLeft.Enabled = enabled;
            btnAlignCenter.Enabled = enabled;
            btnAlignRight.Enabled = enabled;
            btnJustify.Enabled = enabled;
            btnAlignTop.Enabled = enabled;
            btnAlignMiddle.Enabled = enabled;
            btnAlignBottom.Enabled = enabled;
            btnTextColor.Enabled = enabled;
            btnTextRotation.Enabled = enabled;

            if (enabled)
            {
                TextObject text = Designer.SelectedTextObjects.First;

                cbxFontName.FontName = text.Font.Name;
                cbxFontSize.FontSize = text.Font.Size;
                btnBold.Checked = text.Font.Bold;
                btnItalic.Checked = text.Font.Italic;
                btnUnderline.Checked = text.Font.Underline;
                btnAlignLeft.Checked = text.HorzAlign == HorzAlign.Left;
                btnAlignCenter.Checked = text.HorzAlign == HorzAlign.Center;
                btnAlignRight.Checked = text.HorzAlign == HorzAlign.Right;
                btnJustify.Checked = text.HorzAlign == HorzAlign.Justify;
                btnAlignTop.Checked = text.VertAlign == VertAlign.Top;
                btnAlignMiddle.Checked = text.VertAlign == VertAlign.Center;
                btnAlignBottom.Checked = text.VertAlign == VertAlign.Bottom;
                if (text.TextFill is SolidFill)
                    btnTextColor.Color = (text.TextFill as SolidFill).Color;
            }
            else
            {
                btnBold.Checked = false;
                btnItalic.Checked = false;
                btnUnderline.Checked = false;
                btnAlignLeft.Checked = false;
                btnAlignCenter.Checked = false;
                btnAlignRight.Checked = false;
                btnJustify.Checked = false;
                btnAlignTop.Checked = false;
                btnAlignMiddle.Checked = false;
                btnAlignBottom.Checked = false;
            }

            //-------------------------------------------------------------------
            // Border and Fill
            //-------------------------------------------------------------------

            enabled = Designer.SelectedReportComponents.Enabled;
            bool simple = Designer.SelectedReportComponents.SimpleBorder;
            bool useBorder = Designer.SelectedReportComponents.BorderEnabled;

            bool borderEnabled = enabled && !simple && useBorder;
            btnTopLine.Enabled = borderEnabled;
            btnBottomLine.Enabled = borderEnabled;
            btnLeftLine.Enabled = borderEnabled;
            btnRightLine.Enabled = borderEnabled;
            btnAllLines.Enabled = borderEnabled;
            btnNoLines.Enabled = borderEnabled;
            btnFillColor.Enabled = enabled && Designer.SelectedReportComponents.FillEnabled;
            btnFillProps.Enabled = enabled && Designer.SelectedReportComponents.FillEnabled;
            btnLineColor.Enabled = enabled && useBorder;
            btnLineWidth.Enabled = enabled && useBorder;
            btnLineStyle.Enabled = enabled && useBorder;
            btnBorderProps.Enabled = borderEnabled;

            if (enabled)
            {
                Border border = Designer.SelectedReportComponents.First.Border;
                btnTopLine.Checked = (border.Lines & BorderLines.Top) != 0;
                btnBottomLine.Checked = (border.Lines & BorderLines.Bottom) != 0;
                btnLeftLine.Checked = (border.Lines & BorderLines.Left) != 0;
                btnRightLine.Checked = (border.Lines & BorderLines.Right) != 0;
                btnLineColor.Color = border.Color;
                if (Designer.SelectedReportComponents.First.Fill is SolidFill)
                    btnFillColor.Color = (Designer.SelectedReportComponents.First.Fill as SolidFill).Color;
                btnLineWidth.LineWidth = border.Width;
                btnLineStyle.LineStyle = border.Style;
            }

            //-------------------------------------------------------------------
            // Format
            //-------------------------------------------------------------------

            btnHighlight.Enabled = Designer.SelectedTextObjects.Enabled;
            btnFormat.Enabled = Designer.SelectedTextObjects.Enabled;

            //-------------------------------------------------------------------
            // Editing
            //-------------------------------------------------------------------

            btnFind.Enabled = Designer.cmdFind.Enabled;
            btnReplace.Enabled = Designer.cmdReplace.Enabled;
            btnSelectAll.Enabled = Designer.cmdSelectAll.Enabled;

            //-------------------------------------------------------------------
            // Styles
            //-------------------------------------------------------------------

            enabled = Designer.SelectedReportComponents.Enabled;

            cbxStyles.Enabled = enabled;
            cbxStyles.Report = Designer.ActiveReport;
            if (enabled)
                cbxStyles.Style = Designer.SelectedReportComponents.First.Style;

            //-------------------------------------------------------------------
        }

        private void updateReportControls()
        {
            btnPreview.Enabled = Designer.cmdPreview.Enabled;
            btnReportOptions.Enabled = Designer.cmdReportSettings.Enabled;
            btnDataChoose.Enabled = Designer.cmdChooseData.Enabled;
            btnDataAdd.Enabled = Designer.cmdAddData.Enabled;

            btnAddPage.Enabled = Designer.cmdNewPage.Enabled;
            btnAddDialog.Enabled = Designer.cmdNewDialog.Enabled;
            btnDeletePage.Enabled = Designer.cmdDeletePage.Enabled;
            btnPageSetup.Enabled = Designer.cmdPageSetup.Enabled;

            bool bandsEnabled = Designer.cmdInsertBand.Enabled;
            btnConfigureBands.Enabled = bandsEnabled;
            btnGroupExpert.Enabled = bandsEnabled;

            ReportPage page = null;

            try
            {
                //see fix # 66fcd219-30f1-45e6-8ee5-ce65cfb9d35d
                //rly annoying freezes on designer start
                page = Page as ReportPage;
            }
            catch
            {

            }

            if (page != null)
            {
                bool isSubreport = page.Subreport != null;

                btnReportTitle.Enabled = bandsEnabled && !isSubreport;
                btnReportSummary.Enabled = bandsEnabled && !isSubreport;
                btnPageHeader.Enabled = bandsEnabled && !isSubreport;
                btnPageFooter.Enabled = bandsEnabled && !isSubreport;
                btnColumnHeader.Enabled = bandsEnabled && !isSubreport;
                btnColumnFooter.Enabled = bandsEnabled && !isSubreport;
                btnOverlay.Enabled = bandsEnabled && !isSubreport;

                btnReportTitle.Checked = page.ReportTitle != null;
                btnReportSummary.Checked = page.ReportSummary != null;
                btnPageHeader.Checked = page.PageHeader != null;
                btnPageFooter.Checked = page.PageFooter != null;
                btnColumnHeader.Checked = page.ColumnHeader != null;
                btnColumnFooter.Checked = page.ColumnFooter != null;
                btnOverlay.Checked = page.Overlay != null;
            }
        }

        private void updateLayoutControls()
        {
            bool oneObjSelected = Designer.SelectedComponents.Count > 0;
            bool threeObjSelected = Designer.SelectedComponents.Count >= 3;
            bool severalObjSelected = Designer.SelectedComponents.Count > 1;
            bool canChangeOrder = Designer.SelectedComponents.Count > 0 &&
              Designer.SelectedComponents.First.HasFlag(Flags.CanChangeOrder);
            bool canMove = Designer.SelectedComponents.Count > 0 &&
              Designer.SelectedComponents.First.HasFlag(Flags.CanMove);
            bool canResize = Designer.SelectedComponents.Count > 0 &&
              Designer.SelectedComponents.First.HasFlag(Flags.CanResize);

            btnAlignToGrid.Enabled = oneObjSelected && canMove;
            btnAlignLefts.Enabled = severalObjSelected && canMove;
            btnAlignCenters.Enabled = severalObjSelected && canMove;
            btnAlignRights.Enabled = severalObjSelected && canMove;
            btnAlignTops.Enabled = severalObjSelected && canMove;
            btnAlignMiddles.Enabled = severalObjSelected && canMove;
            btnAlignBottoms.Enabled = severalObjSelected && canMove;
            btnSameWidth.Enabled = severalObjSelected && canResize;
            btnSameHeight.Enabled = severalObjSelected && canResize;
            btnSameSize.Enabled = severalObjSelected && canResize;
            btnFitToGrid.Enabled = oneObjSelected && canResize;
            btnSpaceHorizontally.Enabled = threeObjSelected && canMove;
            btnIncreaseHorizontalSpacing.Enabled = severalObjSelected && canMove;
            btnDecreaseHorizontalSpacing.Enabled = severalObjSelected && canMove;
            btnRemoveHorizontalSpacing.Enabled = severalObjSelected && canMove;
            btnSpaceVertically.Enabled = threeObjSelected && canMove;
            btnIncreaseVerticalSpacing.Enabled = severalObjSelected && canMove;
            btnDecreaseVerticalSpacing.Enabled = severalObjSelected && canMove;
            btnRemoveVerticalSpacing.Enabled = severalObjSelected && canMove;
            btnCenterHorizontally.Enabled = oneObjSelected && canMove;
            btnCenterVertically.Enabled = oneObjSelected && canMove;
            btnBringToFront.Enabled = canChangeOrder;
            btnSendToBack.Enabled = canChangeOrder;

            btnGroup.Enabled = Designer.cmdGroup.Enabled;
            btnUngroup.Enabled = Designer.cmdUngroup.Enabled;
        }

        private void updateViewControls()
        {
            btnViewGrid.Checked = ReportWorkspace.ShowGrid;
            btnViewGuides.Checked = ReportWorkspace.ShowGuides;
            bool autoGuides = ReportWorkspace.AutoGuides;
            btnAutoGuides.Checked = autoGuides;
            btnDeleteHGuides.Enabled = !autoGuides;
            btnDeleteVGuides.Enabled = !autoGuides;

            btnProperties.Checked = designer.PropertiesWindow.Visible;
            btnData.Checked = designer.DataWindow.Visible;
            btnReportTree.Checked = designer.ReportTreeWindow.Visible;
            btnMessages.Checked = designer.MessagesWindow.Visible;

            btnUnitsMillimeters.Checked = ReportWorkspace.Grid.GridUnits == PageUnits.Millimeters;
            btnUnitsCentimeters.Checked = ReportWorkspace.Grid.GridUnits == PageUnits.Centimeters;
            btnUnitsInches.Checked = ReportWorkspace.Grid.GridUnits == PageUnits.Inches;
            btnUnitsHundrethsOfInch.Checked = ReportWorkspace.Grid.GridUnits == PageUnits.HundrethsOfInch;
        }
        #endregion

        #region Localization
        private void localizeFile()
        {
            MyRes res = new MyRes("Designer,Menu");

            btnFile.Text = res.Get("File");
            btnFileNew.Text = res.Get("File,New");

            btnFileOpen.Text = res.Get("File,Open");
            btnFileOpen.Tooltip = Res.Get("Designer,Toolbar,Standard,Open");

            btnFileClose.Text = res.Get("File,Close");

            btnFileSave.Text = res.Get("File,Save");
            btnFileSave.Tooltip = Res.Get("Designer,Toolbar,Standard,Save");

            btnFileSaveAs.Text = res.Get("File,SaveAs");

            btnFileSaveAll.Text = res.Get("File,SaveAll");
            btnFileSaveAll.Tooltip = Res.Get("Designer,Toolbar,Standard,SaveAll");

            //btnFilePageSetup.Text = res.Get("File,PageSetup");
            btnFilePrinterSetup.Text = res.Get("File,PrinterSetup");
            btnFilePreview.Text = res.Get("File,Preview");
            btnFileSelectLanguage.Text = res.Get("File,SelectLanguage");
            btnFileExit.Text = res.Get("File,Exit");
            btnOptions.Text = res.Get("View,Options");

            btnWelcome.Text = Res.Get("Designer,Welcome,Button");
            btnHelp.Text = res.Get("Help,Contents");
            btnAbout.Text = res.Get("Help,About");
        }

        private void localizeHome()
        {
            MyRes res = new MyRes("Designer,Toolbar,Standard");

            //setItemText(btnNew, res.Get("New"));
            //setItemText(btnOpen, res.Get("Open"));
            //setItemText(btnSave, res.Get("Save"));
            //setItemText(btnSaveAll, res.Get("SaveAll"));
            //setItemText(btnPreview, res.Get("Preview"));

            setItemText(btnAddPage, res.Get("NewPage"));
            setItemText(btnAddDialog, res.Get("NewDialog"));
            setItemText(btnDeletePage, res.Get("DeletePage"));
            setItemText(btnPageSetup, res.Get("PageSetup"));
            setItemText(btnFormatPainter, res.Get("FormatPainter"));

            res = new MyRes("Designer,Menu,Edit");

            setItemText(btnCut, res.Get("Cut"), Res.Get("Designer,Toolbar,Standard,Cut"));
            setItemText(btnCopy, res.Get("Copy"), Res.Get("Designer,Toolbar,Standard,Copy"));
            setItemText(btnPaste, res.Get("Paste"), Res.Get("Designer,Toolbar,Standard,Paste"));
            setItemText(btnUndo, res.Get("Undo"), Res.Get("Designer,Toolbar,Standard,Undo"));
            setItemText(btnRedo, res.Get("Redo"), Res.Get("Designer,Toolbar,Standard,Redo"));

            res = new MyRes("Designer,Toolbar,Text");

            setItemText(cbxFontName, res.Get("Name"));
            setItemText(cbxFontSize, res.Get("Size"));
            setItemText(btnBold, res.Get("Bold"));
            setItemText(btnItalic, res.Get("Italic"));
            setItemText(btnUnderline, res.Get("Underline"));
            setItemText(btnAlignLeft, res.Get("Left"));
            setItemText(btnAlignCenter, res.Get("Center"));
            setItemText(btnAlignRight, res.Get("Right"));
            setItemText(btnJustify, res.Get("Justify"));
            setItemText(btnAlignTop, res.Get("Top"));
            setItemText(btnAlignMiddle, res.Get("Middle"));
            setItemText(btnAlignBottom, res.Get("Bottom"));
            setItemText(btnTextColor, res.Get("Color"));
            setItemText(btnHighlight, res.Get("Highlight"));
            setItemText(btnTextRotation, res.Get("Angle"));

            res = new MyRes("Designer,Toolbar,Border");

            setItemText(btnTopLine, res.Get("Top"));
            setItemText(btnBottomLine, res.Get("Bottom"));
            setItemText(btnLeftLine, res.Get("Left"));
            setItemText(btnRightLine, res.Get("Right"));
            setItemText(btnAllLines, res.Get("All"));
            setItemText(btnNoLines, res.Get("None"));
            setItemText(btnFillColor, res.Get("FillColor"));
            setItemText(btnFillProps, res.Get("FillStyle"));
            setItemText(btnLineColor, res.Get("LineColor"));
            setItemText(btnLineWidth, res.Get("Width"));
            setItemText(btnLineStyle, res.Get("Style"));
            setItemText(btnBorderProps, res.Get("Props"));

            setItemText(btnStyles, Res.Get("Designer,Menu,Report,Styles"));
            setItemText(btnFormat, Res.Get("ComponentMenu,TextObject,Format"));
            setItemText(btnSelectAll, Res.Get("Designer,Menu,Edit,SelectAll"));
            setItemText(btnFind, Res.Get("Designer,Menu,Edit,Find"));
            setItemText(btnReplace, Res.Get("Designer,Menu,Edit,Replace"));
        }

        private void localizeReport()
        {
            setItemText(btnPreview, Res.Get("Designer,Menu,File,Preview"), Res.Get("Designer,Toolbar,Standard,Preview"));
            setItemText(btnReportOptions, Res.Get("Designer,Menu,Report,Options"));

            setItemText(btnDataChoose, Res.Get("Designer,Menu,Data,Choose"));
            setItemText(btnDataAdd, Res.Get("Designer,Menu,Data,Add"));

            setItemText(btnAddPage, Res.Get("Designer,Toolbar,Standard,NewPage"));
            setItemText(btnAddDialog, Res.Get("Designer,Toolbar,Standard,NewDialog"));
            setItemText(btnDeletePage, Res.Get("Designer,Toolbar,Standard,DeletePage"));
            setItemText(btnPageSetup, Res.Get("Designer,Toolbar,Standard,PageSetup"));

            MyRes res = new MyRes("Designer,Menu,Report");
            setItemText(btnConfigureBands, res.Get("Bands"));
            setItemText(btnGroupExpert, res.Get("GroupExpert"));

            res = new MyRes("Objects,Bands");
            setItemText(btnReportTitle, res.Get("ReportTitle"));
            setItemText(btnReportSummary, res.Get("ReportSummary"));
            setItemText(btnPageHeader, res.Get("PageHeader"));
            setItemText(btnPageFooter, res.Get("PageFooter"));
            setItemText(btnColumnHeader, res.Get("ColumnHeader"));
            setItemText(btnColumnFooter, res.Get("ColumnFooter"));
            setItemText(btnOverlay, res.Get("Overlay"));
        }

        private void localizeLayout()
        {
            MyRes res = new MyRes("Designer,Toolbar,Layout");

            setItemText(btnAlignToGrid, res.Get("AlignToGrid"));
            setItemText(btnAlignLefts, res.Get("Left"));
            setItemText(btnAlignCenters, res.Get("Center"));
            setItemText(btnAlignRights, res.Get("Right"));
            setItemText(btnAlignTops, res.Get("Top"));
            setItemText(btnAlignMiddles, res.Get("Middle"));
            setItemText(btnAlignBottoms, res.Get("Bottom"));
            setItemText(btnSameWidth, res.Get("SameWidth"));
            setItemText(btnSameHeight, res.Get("SameHeight"));
            setItemText(btnSameSize, res.Get("SameSize"));
            setItemText(btnFitToGrid, res.Get("SizeToGrid"));
            setItemText(btnSpaceHorizontally, res.Get("SpaceHorizontally"));
            setItemText(btnIncreaseHorizontalSpacing, res.Get("IncreaseHorizontalSpacing"));
            setItemText(btnDecreaseHorizontalSpacing, res.Get("DecreaseHorizontalSpacing"));
            setItemText(btnRemoveHorizontalSpacing, res.Get("RemoveHorizontalSpacing"));
            setItemText(btnSpaceVertically, res.Get("SpaceVertically"));
            setItemText(btnIncreaseVerticalSpacing, res.Get("IncreaseVerticalSpacing"));
            setItemText(btnDecreaseVerticalSpacing, res.Get("DecreaseVerticalSpacing"));
            setItemText(btnRemoveVerticalSpacing, res.Get("RemoveVerticalSpacing"));
            setItemText(btnCenterHorizontally, res.Get("CenterHorizontally"));
            setItemText(btnCenterVertically, res.Get("CenterVertically"));
            setItemText(btnBringToFront, res.Get("BringToFront"));
            setItemText(btnSendToBack, res.Get("SendToBack"));

            setItemText(btnGroup, Res.Get("Designer,Toolbar,Standard,Group"));
            setItemText(btnUngroup, Res.Get("Designer,Toolbar,Standard,Ungroup"));
        }

        private void localizeView()
        {
            MyRes res = new MyRes("Designer,Menu,View");
            setItemText(btnViewGrid, res.Get("Grid"));
            setItemText(btnViewGuides, res.Get("Guides"));
            setItemText(btnAutoGuides, res.Get("AutoGuides"));
            setItemText(btnDeleteHGuides, res.Get("DeleteHGuides"));
            setItemText(btnDeleteVGuides, res.Get("DeleteVGuides"));

            setItemText(btnProperties, Res.Get("Designer,ToolWindow,Properties"));
            setItemText(btnData, Res.Get("Designer,ToolWindow,Dictionary"));
            setItemText(btnReportTree, Res.Get("Designer,ToolWindow,ReportTree"));
            setItemText(btnMessages, Res.Get("Designer,ToolWindow,Messages"));

            res = new MyRes("Designer,Menu,View");
            btnUnits.Text = res.Get("Units");

            res = new MyRes("Forms,ReportPageOptions");
            btnUnitsMillimeters.Text = res.Get("Millimeters");
            btnUnitsCentimeters.Text = res.Get("Centimeters");
            btnUnitsInches.Text = res.Get("Inches");
            btnUnitsHundrethsOfInch.Text = res.Get("HundrethsOfInch");
        }
        #endregion

        #region Methods
        #region Form Methods
        private void DesignerForm_Load(object sender, EventArgs e)
        {
            // bug/inconsistent behavior in .Net: if we set WindowState to Maximized, the
            // Load event will be fired *after* the form is shown.
            bool maximized = Config.RestoreFormState(this, true);
            // under some circumstances, the config file may contain wrong winodw position (-32000)
            if (!maximized && (Left < -10 || Top < -10))
                maximized = true;
            Designer.RestoreConfig();
            if (maximized)
                WindowState = FormWindowState.Maximized;

            Config.DesignerSettings.OnDesignerLoaded(Designer, EventArgs.Empty);
            Designer.StartAutoSave();
        }

        private void DesignerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Designer.CloseAll())
            {
                e.Cancel = true;
            }
            else
            {
                Config.SaveFormState(this);
                Designer.SaveConfig();
            }
        }

        private void DesignerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Config.DesignerSettings.OnDesignerClosed(Designer, EventArgs.Empty);
            Designer.StopAutoSave();
        }

        private void setupStatusBar()
        {
            slider.ValueChanged += zoom_ValueChanged;

            location.Image = Res.GetImage(62);
            size.Image = Res.GetImage(63);

            zoom1.Image = ResourceLoader.GetBitmap("ZoomPageWidth.png");
            zoom1.Click += delegate(object sender, EventArgs e)
            {
                if (Workspace != null)
                    Workspace.FitPageWidth();
            };

            zoom2.Image = ResourceLoader.GetBitmap("ZoomWholePage.png");
            zoom2.Click += delegate(object sender, EventArgs e)
            {
                if (Workspace != null)
                    Workspace.FitWholePage();
            };

            zoom3.Image = ResourceLoader.GetBitmap("Zoom100.png");
            zoom3.Click += delegate(object sender, EventArgs e)
            {
                if (Workspace != null)
                    Zoom = 1;
            };
        }

        private void zoom_ValueChanged(object sender, EventArgs e)
        {
            //if (FUpdatingZoom)
            //    return;

            int val = slider.Value;
            if (val < 100)
                val = (int)Math.Round(val * 0.75f) + 25;
            else
                val = (val - 100) * 4 + 100;

            zoomLabel.Text = val.ToString() + "%";
            Zoom = val / 100f;
            //FZoomTimer.Start();
        }

        private void updateZoom()
        {
            //FUpdatingZoom = true;

            int zoom = (int)(Zoom * 100);
            zoomLabel.Text = zoom.ToString() + "%";
            if (zoom < 100)
                zoom = (int)Math.Round((zoom - 25) / 0.75f);
            else if (zoom > 100)
                zoom = (zoom - 100) / 4 + 100;
            this.slider.Value = zoom;

            //FUpdatingZoom = false;
        }
        #endregion
        #region File Methods
        private void miFile_PopupOpen(object sender, PopupOpenEventArgs e)
        {
            // clear existing recent items
            for (int i = 0; i < itemContainer23.SubItems.Count; i++)
            {
                BaseItem item = itemContainer23.SubItems[i];

                if (item is ButtonItem)
                {
                    item.Dispose();
                    itemContainer23.SubItems.Remove(item);
                    i--;
                }
            }

            // add new items
            if (Designer.cmdRecentFiles.Enabled && Designer.RecentFiles.Count > 0)
            {
                foreach (string file in Designer.RecentFiles)
                {
                    ButtonItem menuItem = new ButtonItem();
                    menuItem.Text = Path.GetFileName(file);
                    menuItem.Tag = file;
                    menuItem.Tooltip = file;
                    menuItem.Click += recentFile_Click;
                    itemContainer23.SubItems.Insert(1, menuItem);
                }
            }
        }

        private void recentFile_Click(object sender, EventArgs e)
        {
            Designer.UpdatePlugins(null);
            Designer.cmdOpen.LoadFile((sender as ButtonItem).Tag as string);
        }
        #endregion
        #region Home Methods
        //-------------------------------------------------------------------
        // Clipboard
        //-------------------------------------------------------------------

        private void clipboardTimer_Tick(object sender, EventArgs e)
        {
            btnPaste.Enabled = Designer.cmdPaste.Enabled;
        }

        //-------------------------------------------------------------------
        // Text
        //-------------------------------------------------------------------

        private void cbxName_FontSelected(object sender, EventArgs e)
        {
            (Designer.ActiveReportTab.ActivePageDesigner as ReportPageDesigner).Workspace.Focus();
            Designer.SelectedTextObjects.SetFontName(cbxFontName.FontName);
        }

        private void cbxSize_SizeSelected(object sender, EventArgs e)
        {
            (Designer.ActiveReportTab.ActivePageDesigner as ReportPageDesigner).Workspace.Focus();
            Designer.SelectedTextObjects.SetFontSize(cbxFontSize.FontSize);
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            btnBold.Checked = !btnBold.Checked;
            Designer.SelectedTextObjects.ToggleFontStyle(FontStyle.Bold, btnBold.Checked);
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            btnItalic.Checked = !btnItalic.Checked;
            Designer.SelectedTextObjects.ToggleFontStyle(FontStyle.Italic, btnItalic.Checked);
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            btnUnderline.Checked = !btnUnderline.Checked;
            Designer.SelectedTextObjects.ToggleFontStyle(FontStyle.Underline, btnUnderline.Checked);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            Designer.SelectedTextObjects.SetHAlign(HorzAlign.Left);
        }

        private void btnCenter_Click(object sender, EventArgs e)
        {
            Designer.SelectedTextObjects.SetHAlign(HorzAlign.Center);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            Designer.SelectedTextObjects.SetHAlign(HorzAlign.Right);
        }

        private void btnJustify_Click(object sender, EventArgs e)
        {
            Designer.SelectedTextObjects.SetHAlign(HorzAlign.Justify);
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            Designer.SelectedTextObjects.SetVAlign(VertAlign.Top);
        }

        private void btnMiddle_Click(object sender, EventArgs e)
        {
            Designer.SelectedTextObjects.SetVAlign(VertAlign.Center);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            Designer.SelectedTextObjects.SetVAlign(VertAlign.Bottom);
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            Designer.SelectedTextObjects.SetTextColor(btnTextColor.DefaultColor);
        }

        private void btnRotation_Click(object sender, EventArgs e)
        {
            AnglePopup popup = new AnglePopup(Designer.FindForm());
            popup.Angle = Designer.SelectedTextObjects.First.Angle;
            popup.AngleChanged += popup_RotationChanged;
            popup.Show(this, barText.Right, barText.Bottom);
        }

        private void popup_RotationChanged(object sender, EventArgs e)
        {
            Designer.SelectedTextObjects.SetAngle((sender as AnglePopup).Angle);
        }

        //-------------------------------------------------------------------
        // Border and Fill
        //-------------------------------------------------------------------

        private void btnTopLine_Click(object sender, EventArgs e)
        {
            btnTopLine.Checked = !btnTopLine.Checked;
            Designer.SelectedReportComponents.ToggleLine(BorderLines.Top, btnTopLine.Checked);
        }

        private void btnBottomLine_Click(object sender, EventArgs e)
        {
            btnBottomLine.Checked = !btnBottomLine.Checked;
            Designer.SelectedReportComponents.ToggleLine(BorderLines.Bottom, btnBottomLine.Checked);
        }

        private void btnLeftLine_Click(object sender, EventArgs e)
        {
            btnLeftLine.Checked = !btnLeftLine.Checked;
            Designer.SelectedReportComponents.ToggleLine(BorderLines.Left, btnLeftLine.Checked);
        }

        private void btnRightLine_Click(object sender, EventArgs e)
        {
            btnRightLine.Checked = !btnRightLine.Checked;
            Designer.SelectedReportComponents.ToggleLine(BorderLines.Right, btnRightLine.Checked);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            Designer.SelectedReportComponents.ToggleLine(BorderLines.All, true);
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            Designer.SelectedReportComponents.ToggleLine(BorderLines.All, false);
        }

        private void btnLineColor_Click(object sender, EventArgs e)
        {
            Designer.SelectedReportComponents.SetLineColor(btnLineColor.DefaultColor);
        }

        private void btnFillColor_Click(object sender, EventArgs e)
        {
            Designer.SelectedReportComponents.SetColor(btnFillColor.DefaultColor);
        }

        private void btnFillProps_Click(object sender, EventArgs e)
        {
            Designer.SelectedReportComponents.InvokeFillEditor();
        }

        private void cbxWidth_WidthSelected(object sender, EventArgs e)
        {
            Designer.SelectedReportComponents.SetWidth(btnLineWidth.LineWidth);
        }

        private void cbxLineStyle_StyleSelected(object sender, EventArgs e)
        {
            Designer.SelectedReportComponents.SetLineStyle(btnLineStyle.LineStyle);
        }

        private void btnBorderProps_Click(object sender, EventArgs e)
        {
            Designer.SelectedReportComponents.InvokeBorderEditor();
        }

        //-------------------------------------------------------------------
        // Format
        //-------------------------------------------------------------------

        private void btnHighlight_Click(object sender, EventArgs e)
        {
            Designer.SelectedTextObjects.InvokeHighlightEditor();
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            using (FormatEditorForm form = new FormatEditorForm())
            {
                SelectedTextBaseObjects FTextObjects = new SelectedTextBaseObjects(designer);
                FTextObjects.Update();

                form.TextObject = FTextObjects.First;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    FTextObjects.SetFormat(form.Formats);
                    Designer.SetModified(null, "Change");
                    //Change();
                }
            }
        }

        //-------------------------------------------------------------------
        // Styles
        //-------------------------------------------------------------------

        private void cbxStyle_StyleSelected(object sender, EventArgs e)
        {
            (Designer.ActiveReportTab.ActivePageDesigner as ReportPageDesigner).Workspace.Focus();
            Designer.SelectedReportComponents.SetStyle(cbxStyles.Style);
        }

        //-------------------------------------------------------------------
        #endregion
        #region Report Methods

        private void btnPreview_Click(object sender, EventArgs e)
        {
            previewTimer.Start();
        }

        private void previewTimer_Tick(object sender, EventArgs e)
        {
            previewTimer.Stop();
            Designer.cmdPreview.Invoke(sender, e);
        }

        private void miInsertBands_Click(object sender, EventArgs e)
        {
            using (ConfigureBandsForm form = new ConfigureBandsForm(Designer))
            {
                form.Page = Page as ReportPage;
                form.ShowDialog();
            }
        }

        private void miReportGroupExpert_Click(object sender, EventArgs e)
        {
            using (GroupExpertForm form = new GroupExpertForm(Designer))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    Designer.SetModified(null, "ChangeReport");
            }
        }

        private void miReportTitle_Click(object sender, EventArgs e)
        {
            ReportPage page = Page as ReportPage;
            if ((sender as CheckBoxItem).Checked)
            {
                page.ReportTitle = new ReportTitleBand();
                ReportPageDesigner.SetDefaults(page.ReportTitle);
            }
            else
            {
                page.ReportTitle = null;
            }
            ReportPageDesigner.Change();
        }

        private void miReportSummary_Click(object sender, EventArgs e)
        {
            ReportPage page = Page as ReportPage;
            if ((sender as CheckBoxItem).Checked)
            {
                page.ReportSummary = new ReportSummaryBand();
                ReportPageDesigner.SetDefaults(page.ReportSummary);
            }
            else
            {
                page.ReportSummary = null;
            }
            ReportPageDesigner.Change();
        }

        private void miPageHeader_Click(object sender, EventArgs e)
        {
            ReportPage page = Page as ReportPage;
            if ((sender as CheckBoxItem).Checked)
            {
                page.PageHeader = new PageHeaderBand();
                ReportPageDesigner.SetDefaults(page.PageHeader);
            }
            else
            {
                page.PageHeader = null;
            }
            ReportPageDesigner.Change();
        }

        private void miPageFooter_Click(object sender, EventArgs e)
        {
            ReportPage page = Page as ReportPage;
            if ((sender as CheckBoxItem).Checked)
            {
                page.PageFooter = new PageFooterBand();
                ReportPageDesigner.SetDefaults(page.PageFooter);
            }
            else
            {
                page.PageFooter = null;
            }
            ReportPageDesigner.Change();
        }

        private void miColumnHeader_Click(object sender, EventArgs e)
        {
            ReportPage page = Page as ReportPage;
            if ((sender as CheckBoxItem).Checked)
            {
                page.ColumnHeader = new ColumnHeaderBand();
                ReportPageDesigner.SetDefaults(page.ColumnHeader);
            }
            else
            {
                page.ColumnHeader = null;
            }
            ReportPageDesigner.Change();
        }

        private void miColumnFooter_Click(object sender, EventArgs e)
        {
            ReportPage page = Page as ReportPage;
            if ((sender as CheckBoxItem).Checked)
            {
                page.ColumnFooter = new ColumnFooterBand();
                ReportPageDesigner.SetDefaults(page.ColumnFooter);
            }
            else
            {
                page.ColumnFooter = null;
            }
            ReportPageDesigner.Change();
        }

        private void miOverlay_Click(object sender, EventArgs e)
        {
            ReportPage page = Page as ReportPage;
            if ((sender as CheckBoxItem).Checked)
            {
                page.Overlay = new OverlayBand();
                ReportPageDesigner.SetDefaults(page.Overlay);
            }
            else
            {
                page.Overlay = null;
            }
            ReportPageDesigner.Change();
        }

        #endregion
        #region Layout Methods
        private void btnAlignToGrid_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.AlignToGrid();
        }

        private void btnAlignLefts_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.AlignLeft();
        }

        private void btnAlignCenters_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.AlignCenter();
        }

        private void btnAlignRights_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.AlignRight();
        }

        private void btnAlignTops_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.AlignTop();
        }

        private void btnAlignMiddles_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.AlignMiddle();
        }

        private void btnAlignBottoms_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.AlignBottom();
        }

        private void btnSameWidth_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.SameWidth();
        }

        private void btnSameHeight_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.SameHeight();
        }

        private void btnSameSize_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.SameSize();
        }

        private void btnCenterHorizontally_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.CenterHorizontally();
        }

        private void btnCenterVertically_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.CenterVertically();
        }

        private void btnSizeToGrid_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.SizeToGrid();
        }

        private void btnSpaceHorizontally_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.SpaceHorizontally();
        }

        private void btnIncreaseHorizontalSpacing_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.IncreaseHorizontalSpacing();
        }

        private void btnDecreaseHorizontalSpacing_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.DecreaseHorizontalSpacing();
        }

        private void btnRemoveHorizontalSpacing_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.RemoveHorizontalSpacing();
        }

        private void btnSpaceVertically_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.SpaceVertically();
        }

        private void btnIncreaseVerticalSpacing_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.IncreaseVerticalSpacing();
        }

        private void btnDecreaseVerticalSpacing_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.DecreaseVerticalSpacing();
        }

        private void btnRemoveVerticalSpacing_Click(object sender, EventArgs e)
        {
            Designer.SelectedComponents.RemoveVerticalSpacing();
        }
        #endregion
        #region View Methods
        private void MenuViewGrid_Click(object sender, EventArgs e)
        {
            ReportWorkspace.ShowGrid = btnViewGrid.Checked;
            Workspace.Refresh();
        }

        private void MenuViewGuides_Click(object sender, EventArgs e)
        {
            ReportWorkspace.ShowGuides = btnViewGuides.Checked;
            Workspace.Refresh();
        }

        private void MenuViewAutoGuides_Click(object sender, EventArgs e)
        {
            ReportWorkspace.AutoGuides = btnAutoGuides.Checked;
            Workspace.Refresh();
            updateControls();
        }

        private void MenuViewDeleteHGuides_Click(object sender, EventArgs e)
        {
            Workspace.DeleteHGuides();
        }

        private void MenuViewDeleteVGuides_Click(object sender, EventArgs e)
        {
            Workspace.DeleteVGuides();
        }

        private void miViewUnits_Click(object sender, EventArgs e)
        {
            if (sender == btnUnitsMillimeters)
                ReportWorkspace.Grid.GridUnits = PageUnits.Millimeters;
            else if (sender == btnUnitsCentimeters)
                ReportWorkspace.Grid.GridUnits = PageUnits.Centimeters;
            else if (sender == btnUnitsInches)
                ReportWorkspace.Grid.GridUnits = PageUnits.Inches;
            else
                ReportWorkspace.Grid.GridUnits = PageUnits.HundrethsOfInch;

            UpdateContent();
        }
        #endregion
        #endregion

        #region IDesignerPlugin
        /// <inheritdoc/>
        public string PluginName
        {
            get { return Name; }
        }

        /// <inheritdoc/>
        public void SaveState()
        {
        }

        /// <inheritdoc/>
        public void RestoreState()
        {
        }

        /// <inheritdoc/>
        public void SelectionChanged()
        {
            UpdateContent();
        }

        /// <inheritdoc/>
        public void UpdateContent()
        {
            updateZoom();
            updateControls();
        }

        /// <inheritdoc/>
        public void Lock()
        {
        }

        /// <inheritdoc/>
        public void Unlock()
        {
            UpdateContent();
        }

        /// <inheritdoc/>
        public void Localize()
        {
            localizeFile();
            localizeHome();
            localizeReport();
            localizeLayout();
            localizeView();

            MyRes res = new MyRes("Designer,Ribbon");

            btnFile.Text = res.Get("File");//.ToUpper();
            tabHome.Text = res.Get("Home");//.ToUpper();
            tabReport.Text = res.Get("Report");//.ToUpper();
            tabLayout.Text = res.Get("Layout");//.ToUpper();
            tabView.Text = res.Get("View");//.ToUpper();

            lblRecent.Text = res.Get("Recent");
            barReport.Text = res.Get("Report");
            barLayout.Text = res.Get("Layout");
            barView.Text = res.Get("View");
            barClipboard.Text = res.Get("Clipboard");
            barText.Text = res.Get("Text");
            barBorderAndFill.Text = res.Get("BorderAndFill");
            barFormat.Text = res.Get("Format");
            barStyles.Text = res.Get("Styles");
            barEditing.Text = res.Get("Editing");
            barData.Text = res.Get("Data");
            barPages.Text = res.Get("Pages");
            barBands.Text = res.Get("Bands");
            btnAlignment.Text = res.Get("Alignment");
            btnSize.Text = res.Get("Size");
            btnSpacing.Text = res.Get("Spacing");
            btnPanels.Text = res.Get("Panels");
            
            //trying to refresh controls
            //RibbonTabItem tab = ribbonControl.SelectedRibbonTabItem;
            //ribbonControl.SelectedRibbonTabItem = tabHome;
            //ribbonControl.SelectedRibbonTabItem = tabReport;
            //ribbonControl.SelectedRibbonTabItem = tabLayout;
            //ribbonControl.SelectedRibbonTabItem = tabView;
            //ribbonControl.SelectedRibbonTabItem = tab;
            //tabHome.Select();
            //tabReport.Select();
            //tabLayout.Select();
            //tabView.Select();
            //tab.Select();

            UpdateContent();
        }

        /// <inheritdoc/>
        public DesignerOptionsPage GetOptionsPage()
        {
            return null;
        }

        /// <inheritdoc/>
        public void UpdateUIStyle()
        {
            if (Config.UseRibbon)
            {
                designer.ShowMainMenu = false;
                ribbonControl.Visible = true;

                foreach (Bar bar in designer.DotNetBarManager.Bars)
                    if (bar is ToolbarBase)
                        bar.Hide();
            }
            else
            {
                designer.ShowMainMenu = true;
                ribbonControl.Visible = false;
            }

            btnTextColor.SetStyle(designer.UIStyle);
            btnFillColor.SetStyle(designer.UIStyle);
            btnLineColor.SetStyle(designer.UIStyle);

            statusBar.Refresh();
        }
        #endregion
    }
}
