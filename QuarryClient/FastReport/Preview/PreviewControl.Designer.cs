namespace FastReport.Preview
{
  partial class PreviewControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;


    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
	private void InitializeComponent_inner()
    {
		// Simon: InitializeComponent ¸ÄÎª InitializeComponent_inner
            this.tabControl = new FastReport.DevComponents.DotNetBar.TabControl();
            this.splitter = new FastReport.DevComponents.DotNetBar.ExpandableSplitter();
            this.toolBar = new FastReport.DevComponents.DotNetBar.Bar();
            this.tbPageNo = new FastReport.DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnDesign = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnPrint = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnOpen = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnSave = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnEmail = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnEmailMapi = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnFind = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnOutline = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnPageSetup = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnEdit = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnWatermark = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnFirst = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnPrior = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.controlContainerItem1 = new FastReport.DevComponents.DotNetBar.ControlContainerItem();
            this.lblTotalPages = new FastReport.DevComponents.DotNetBar.LabelItem();
            this.btnNext = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnLast = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.statusBar = new FastReport.DevComponents.DotNetBar.Bar();
            this.lblStatus = new FastReport.DevComponents.DotNetBar.LabelItem();
            this.lblUrl = new FastReport.DevComponents.DotNetBar.LabelItem();
            this.lblPerformance = new FastReport.DevComponents.DotNetBar.LabelItem();
            this.itemContainer1 = new FastReport.DevComponents.DotNetBar.ItemContainer();
            this.itemContainer2 = new FastReport.DevComponents.DotNetBar.ItemContainer();
            this.btnZoomPageWidth = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnZoomWholePage = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.btnZoom100 = new FastReport.DevComponents.DotNetBar.ButtonItem();
            this.slZoom = new FastReport.DevComponents.DotNetBar.SliderItem();
            this.outlineControl = new FastReport.Preview.OutlineControl();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolBar)).BeginInit();
            this.toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBar)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.AutoCloseTabs = true;
            this.tabControl.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabControl.CanReorderTabs = true;
            this.tabControl.CloseButtonOnTabsVisible = true;
            this.tabControl.CloseButtonVisible = true;
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl.ForeColor = System.Drawing.Color.Black;
            this.tabControl.Location = new System.Drawing.Point(153, 28);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl.SelectedTabIndex = -1;
            this.tabControl.Size = new System.Drawing.Size(490, 226);
            this.tabControl.Style = FastReport.DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabControl.TabIndex = 2;
            this.tabControl.TabLayoutType = FastReport.DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl.TabsVisible = false;
            this.tabControl.Text = "tabControl1";
            this.tabControl.SelectedTabChanged += new FastReport.DevComponents.DotNetBar.TabStrip.SelectedTabChangedEventHandler(this.tabControl1_SelectedTabChanged);
            this.tabControl.TabItemClose += new FastReport.DevComponents.DotNetBar.TabStrip.UserActionEventHandler(this.tabControl1_TabItemClose);
            this.tabControl.Resize += new System.EventHandler(this.tabControl_Resize);
            // 
            // splitter
            // 
            this.splitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(242)))));
            this.splitter.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(229)))));
            this.splitter.BackColor2SchemePart = FastReport.DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitter.BackColorSchemePart = FastReport.DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.splitter.Expandable = false;
            this.splitter.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(229)))));
            this.splitter.ExpandFillColorSchemePart = FastReport.DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitter.ExpandLineColor = System.Drawing.Color.Black;
            this.splitter.ExpandLineColorSchemePart = FastReport.DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.splitter.ForeColor = System.Drawing.Color.Black;
            this.splitter.GripDarkColor = System.Drawing.Color.Black;
            this.splitter.GripDarkColorSchemePart = FastReport.DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.splitter.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(242)))));
            this.splitter.GripLightColorSchemePart = FastReport.DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.splitter.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.splitter.HotBackColor2 = System.Drawing.Color.Empty;
            this.splitter.HotBackColor2SchemePart = FastReport.DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.splitter.HotBackColorSchemePart = FastReport.DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.splitter.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(229)))));
            this.splitter.HotExpandFillColorSchemePart = FastReport.DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitter.HotExpandLineColor = System.Drawing.Color.Black;
            this.splitter.HotExpandLineColorSchemePart = FastReport.DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.splitter.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(229)))));
            this.splitter.HotGripDarkColorSchemePart = FastReport.DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitter.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(242)))));
            this.splitter.HotGripLightColorSchemePart = FastReport.DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.splitter.Location = new System.Drawing.Point(150, 28);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(3, 226);
            this.splitter.Style = FastReport.DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.splitter.TabIndex = 4;
            this.splitter.TabStop = false;
            // 
            // toolBar
            // 
            this.toolBar.AccessibleDescription = "bar1 (toolBar)";
            this.toolBar.AccessibleName = "bar1";
            this.toolBar.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.toolBar.Controls.Add(this.tbPageNo);
            this.toolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolBar.Font = new System.Drawing.Font("Tahoma", 9F);            
            this.toolBar.Items.AddRange(new FastReport.DevComponents.DotNetBar.BaseItem[] {
            this.btnDesign,
            this.btnPrint,
            this.btnOpen,
            this.btnSave,
            this.btnEmail,
            this.btnEmailMapi,
            this.btnFind,
            this.btnOutline,
            this.btnPageSetup,
            this.btnEdit,
            this.btnWatermark,
            this.btnFirst,
            this.btnPrior,
            this.controlContainerItem1,
            this.lblTotalPages,
            this.btnNext,
            this.btnLast,
            this.btnClose});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.RoundCorners = false;
            this.toolBar.Size = new System.Drawing.Size(643, 28);
            this.toolBar.Stretch = true;
            this.toolBar.Style = FastReport.DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.toolBar.TabIndex = 6;
            this.toolBar.TabStop = false;
            this.toolBar.Text = "bar1";
            //this.toolBar.ImageSize = DevComponents.DotNetBar.eBarImageSize.Medium;
                    
            // 
            // tbPageNo
            // 
            this.tbPageNo.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tbPageNo.Border.Class = "TextBoxBorder";
            this.tbPageNo.Border.CornerType = FastReport.DevComponents.DotNetBar.eCornerType.Square;
            this.tbPageNo.DisabledBackColor = System.Drawing.Color.White;
            this.tbPageNo.ForeColor = System.Drawing.Color.Black;
            this.tbPageNo.Location = new System.Drawing.Point(547, 2);
            this.tbPageNo.Name = "tbPageNo";
            this.tbPageNo.Size = new System.Drawing.Size(40, 23);
            this.tbPageNo.TabIndex = 1;
            this.tbPageNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPageNo.Click += new System.EventHandler(this.tbPageNo_Click);
            this.tbPageNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPageNo_KeyDown);
            this.tbPageNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPageNo_KeyPress);
            // 
            // btnPrint
            // 
            this.btnDesign.ButtonStyle = FastReport.DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnDesign.Name = "btnDesign";
            this.btnDesign.Text = "Design";
            this.btnDesign.Click += new System.EventHandler(this.btnDesign_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.ButtonStyle = FastReport.DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoExpandOnClick = true;
            this.btnSave.ButtonStyle = FastReport.DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnSave.Name = "btnSave";
            this.btnSave.Text = "Save";
            // 
            // btnEmail
            // 
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Text = "Email";
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // btnEmailMapi
            // 
            this.btnEmailMapi.AutoExpandOnClick = true;
            this.btnEmailMapi.Name = "btnEmailMapi";
            this.btnEmailMapi.Text = "Email";
            // 
            // btnFind
            // 
            this.btnFind.Name = "btnFind";
            this.btnFind.Text = "Find";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnOutline
            // 
            this.btnOutline.AutoCheckOnClick = true;
            this.btnOutline.BeginGroup = true;
            this.btnOutline.Name = "btnOutline";
            this.btnOutline.Text = "Outline";
            this.btnOutline.Click += new System.EventHandler(this.btnOutline_Click);
            // 
            // btnPageSetup
            // 
            this.btnPageSetup.Name = "btnPageSetup";
            this.btnPageSetup.Text = "Page Setup";
            this.btnPageSetup.Click += new System.EventHandler(this.btnPageSetup_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnWatermark
            // 
            this.btnWatermark.Name = "btnWatermark";
            this.btnWatermark.Text = "Watermark";
            this.btnWatermark.Click += new System.EventHandler(this.btnWatermark_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.BeginGroup = true;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Text = "First";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrior
            // 
            this.btnPrior.Name = "btnPrior";
            this.btnPrior.Text = "Prior";
            this.btnPrior.Click += new System.EventHandler(this.btnPrior_Click);
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = false;
            this.controlContainerItem1.Control = this.tbPageNo;
            this.controlContainerItem1.MenuVisibility = FastReport.DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            // 
            // lblTotalPages
            // 
            this.lblTotalPages.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblTotalPages.Name = "lblTotalPages";
            this.lblTotalPages.Text = "ofM";
            // 
            // btnNext
            // 
            this.btnNext.Name = "btnNext";
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Name = "btnLast";
            this.btnLast.Text = "Last";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnClose
            // 
            this.btnClose.BeginGroup = true;
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // statusBar
            // 
            this.statusBar.AccessibleDescription = "DotNetBar Bar (statusBar)";
            this.statusBar.AccessibleName = "DotNetBar Bar";
            this.statusBar.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar;
            this.statusBar.BarType = FastReport.DevComponents.DotNetBar.eBarType.StatusBar;
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBar.Font = new System.Drawing.Font("Tahoma", 9F);
            this.statusBar.Items.AddRange(new FastReport.DevComponents.DotNetBar.BaseItem[] {
            this.lblStatus,
            this.lblUrl,
            this.lblPerformance,
            this.itemContainer1});
            this.statusBar.Location = new System.Drawing.Point(0, 254);
            this.statusBar.Name = "statusBar";
            this.statusBar.PaddingBottom = 2;
            this.statusBar.PaddingRight = 16;
            this.statusBar.PaddingTop = 3;
            this.statusBar.Size = new System.Drawing.Size(643, 29);
            this.statusBar.Stretch = true;
            this.statusBar.Style = FastReport.DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.statusBar.TabIndex = 5;
            this.statusBar.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Height = 19;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "   ";
            this.lblStatus.Width = 200;
            // 
            // lblUrl
            // 
            this.lblUrl.Height = 19;
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Stretch = true;
            this.lblUrl.Text = "   ";
            // 
            // lblPerformance
            // 
            this.lblPerformance.Height = 19;
            this.lblPerformance.Name = "lblPerformance";
            this.lblPerformance.Text = "   ";
            // 
            // itemContainer1
            // 
            // 
            // 
            // 
            this.itemContainer1.BackgroundStyle.Class = "Office2007StatusBarBackground2";
            this.itemContainer1.BackgroundStyle.CornerType = FastReport.DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new FastReport.DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer2,
            this.slZoom});
            // 
            // 
            // 
            this.itemContainer1.TitleStyle.CornerType = FastReport.DevComponents.DotNetBar.eCornerType.Square;
            // 
            // itemContainer2
            // 
            // 
            // 
            // 
            this.itemContainer2.BackgroundStyle.CornerType = FastReport.DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer2.BeginGroup = true;
            this.itemContainer2.Name = "itemContainer2";
            this.itemContainer2.SubItems.AddRange(new FastReport.DevComponents.DotNetBar.BaseItem[] {
            this.btnZoomPageWidth,
            this.btnZoomWholePage,
            this.btnZoom100});
            // 
            // 
            // 
            this.itemContainer2.TitleStyle.CornerType = FastReport.DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer2.VerticalItemAlignment = FastReport.DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            // 
            // btnZoomPageWidth
            // 
            this.btnZoomPageWidth.Name = "btnZoomPageWidth";
            this.btnZoomPageWidth.Text = "PageWidth";
            this.btnZoomPageWidth.Click += new System.EventHandler(this.btnZoomPageWidth_Click);
            // 
            // btnZoomWholePage
            // 
            this.btnZoomWholePage.Name = "btnZoomWholePage";
            this.btnZoomWholePage.Text = "WholePage";
            this.btnZoomWholePage.Click += new System.EventHandler(this.btnZoomWholePage_Click);
            // 
            // btnZoom100
            // 
            this.btnZoom100.Name = "btnZoom100";
            this.btnZoom100.Text = "Zoom100";
            this.btnZoom100.Click += new System.EventHandler(this.btnZoom100_Click);
            // 
            // slZoom
            // 
            this.slZoom.Maximum = 200;
            this.slZoom.Name = "slZoom";
            this.slZoom.Step = 5;
            this.slZoom.Text = "100%";
            this.slZoom.Value = 100;
            this.slZoom.Width = 120;
            this.slZoom.ValueChanged += new System.EventHandler(this.slZoom_ValueChanged);
            // 
            // outlineControl
            // 
            this.outlineControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.outlineControl.Location = new System.Drawing.Point(0, 28);
            this.outlineControl.Name = "outlineControl";
            this.outlineControl.Size = new System.Drawing.Size(150, 226);
            this.outlineControl.TabIndex = 3;
            // 
            // PreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.outlineControl);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.statusBar);
            this.Name = "PreviewControl";
            this.Size = new System.Drawing.Size(643, 283);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolBar)).EndInit();
            this.toolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusBar)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private FastReport.DevComponents.DotNetBar.TabControl tabControl;
    private OutlineControl outlineControl;
    private FastReport.DevComponents.DotNetBar.ExpandableSplitter splitter;
    private FastReport.DevComponents.DotNetBar.LabelItem lblStatus;
    private FastReport.DevComponents.DotNetBar.LabelItem lblUrl;
    private FastReport.DevComponents.DotNetBar.LabelItem lblPerformance;
    private FastReport.DevComponents.DotNetBar.ItemContainer itemContainer1;
    private FastReport.DevComponents.DotNetBar.ItemContainer itemContainer2;
    private FastReport.DevComponents.DotNetBar.SliderItem slZoom;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnDesign;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnPrint;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnOpen;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnSave;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnEmail;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnEmailMapi;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnFind;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnOutline;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnPageSetup;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnEdit;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnWatermark;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnFirst;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnPrior;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnNext;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnLast;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnClose;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnZoomPageWidth;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnZoomWholePage;
    private FastReport.DevComponents.DotNetBar.ButtonItem btnZoom100;
    private FastReport.DevComponents.DotNetBar.Controls.TextBoxX tbPageNo;
    private FastReport.DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
    private FastReport.DevComponents.DotNetBar.LabelItem lblTotalPages;
    private FastReport.DevComponents.DotNetBar.Bar statusBar;
    private FastReport.DevComponents.DotNetBar.Bar toolBar;
  }
}
