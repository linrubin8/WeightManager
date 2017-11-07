namespace FastReport.Forms
{
  partial class FillEditorForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.pnSolid = new FastReport.Controls.PageControlPage();
      this.gbSolidColors = new System.Windows.Forms.GroupBox();
      this.cbxSolidColor = new FastReport.Controls.ColorComboBox();
      this.lblSolidColor = new System.Windows.Forms.Label();
      this.pnLinear = new FastReport.Controls.PageControlPage();
      this.gbLinearOptions = new System.Windows.Forms.GroupBox();
      this.trbLinearContrast = new System.Windows.Forms.TrackBar();
      this.lblLinearContrast = new System.Windows.Forms.Label();
      this.lblLinearFocus = new System.Windows.Forms.Label();
      this.trbLinearFocus = new System.Windows.Forms.TrackBar();
      this.gbLinearAngle = new System.Windows.Forms.GroupBox();
      this.acLinearAngle = new FastReport.Controls.AngleControl();
      this.gbLinearColors = new System.Windows.Forms.GroupBox();
      this.cbxLinearEndColor = new FastReport.Controls.ColorComboBox();
      this.lblLinearEndColor = new System.Windows.Forms.Label();
      this.cbxLinearStartColor = new FastReport.Controls.ColorComboBox();
      this.lblLinearStartColor = new System.Windows.Forms.Label();
      this.pnPath = new FastReport.Controls.PageControlPage();
      this.gbPathShape = new System.Windows.Forms.GroupBox();
      this.rbPathRectangle = new System.Windows.Forms.RadioButton();
      this.rbPathEllipse = new System.Windows.Forms.RadioButton();
      this.gbPathColors = new System.Windows.Forms.GroupBox();
      this.cbxPathEdgeColor = new FastReport.Controls.ColorComboBox();
      this.lblPathEdgeColor = new System.Windows.Forms.Label();
      this.cbxPathCenterColor = new FastReport.Controls.ColorComboBox();
      this.lblPathCenterColor = new System.Windows.Forms.Label();
      this.pnHatch = new FastReport.Controls.PageControlPage();
      this.gbHatchStyle = new System.Windows.Forms.GroupBox();
      this.cbxHatch = new FastReport.Controls.HatchComboBox();
      this.gbHatchColors = new System.Windows.Forms.GroupBox();
      this.cbxHatchBackColor = new FastReport.Controls.ColorComboBox();
      this.lblHatchBackColor = new System.Windows.Forms.Label();
      this.cbxHatchForeColor = new FastReport.Controls.ColorComboBox();
      this.lblHatchForeColor = new System.Windows.Forms.Label();
      this.pnSample = new FastReport.Forms.FillEditorForm.FillSample();
      this.pcPanels = new FastReport.Controls.PageControl();
      this.pnGlass = new FastReport.Controls.PageControlPage();
      this.gbGlassOptions = new System.Windows.Forms.GroupBox();
      this.trbGlassBlend = new System.Windows.Forms.TrackBar();
      this.cbxGlassColor = new FastReport.Controls.ColorComboBox();
      this.lblGlassBlend = new System.Windows.Forms.Label();
      this.lblGlassColor = new System.Windows.Forms.Label();
      this.cbGlassHatch = new System.Windows.Forms.CheckBox();
      this.pnSolid.SuspendLayout();
      this.gbSolidColors.SuspendLayout();
      this.pnLinear.SuspendLayout();
      this.gbLinearOptions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trbLinearContrast)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trbLinearFocus)).BeginInit();
      this.gbLinearAngle.SuspendLayout();
      this.gbLinearColors.SuspendLayout();
      this.pnPath.SuspendLayout();
      this.gbPathShape.SuspendLayout();
      this.gbPathColors.SuspendLayout();
      this.pnHatch.SuspendLayout();
      this.gbHatchStyle.SuspendLayout();
      this.gbHatchColors.SuspendLayout();
      this.pcPanels.SuspendLayout();
      this.pnGlass.SuspendLayout();
      this.gbGlassOptions.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trbGlassBlend)).BeginInit();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(352, 288);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(432, 288);
      // 
      // pnSolid
      // 
      this.pnSolid.BackColor = System.Drawing.SystemColors.Window;
      this.pnSolid.Controls.Add(this.gbSolidColors);
      this.pnSolid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnSolid.Location = new System.Drawing.Point(139, 1);
      this.pnSolid.Name = "pnSolid";
      this.pnSolid.Size = new System.Drawing.Size(356, 262);
      this.pnSolid.TabIndex = 2;
      this.pnSolid.Text = "Solid";
      // 
      // gbSolidColors
      // 
      this.gbSolidColors.Controls.Add(this.cbxSolidColor);
      this.gbSolidColors.Controls.Add(this.lblSolidColor);
      this.gbSolidColors.Location = new System.Drawing.Point(16, 12);
      this.gbSolidColors.Name = "gbSolidColors";
      this.gbSolidColors.Size = new System.Drawing.Size(324, 164);
      this.gbSolidColors.TabIndex = 3;
      this.gbSolidColors.TabStop = false;
      this.gbSolidColors.Text = "Colors";
      // 
      // cbxSolidColor
      // 
      this.cbxSolidColor.Color = System.Drawing.Color.Transparent;
      this.cbxSolidColor.Location = new System.Drawing.Point(108, 20);
      this.cbxSolidColor.Name = "cbxSolidColor";
      this.cbxSolidColor.Size = new System.Drawing.Size(70, 21);
      this.cbxSolidColor.TabIndex = 3;
      this.cbxSolidColor.ColorSelected += new System.EventHandler(this.cbxSolidColor_ColorSelected);
      // 
      // lblSolidColor
      // 
      this.lblSolidColor.AutoSize = true;
      this.lblSolidColor.Location = new System.Drawing.Point(8, 24);
      this.lblSolidColor.Name = "lblSolidColor";
      this.lblSolidColor.Size = new System.Drawing.Size(32, 13);
      this.lblSolidColor.TabIndex = 2;
      this.lblSolidColor.Text = "Color";
      // 
      // pnLinear
      // 
      this.pnLinear.BackColor = System.Drawing.SystemColors.Window;
      this.pnLinear.Controls.Add(this.gbLinearOptions);
      this.pnLinear.Controls.Add(this.gbLinearAngle);
      this.pnLinear.Controls.Add(this.gbLinearColors);
      this.pnLinear.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnLinear.Location = new System.Drawing.Point(139, 1);
      this.pnLinear.Name = "pnLinear";
      this.pnLinear.Size = new System.Drawing.Size(356, 262);
      this.pnLinear.TabIndex = 2;
      this.pnLinear.Text = "Linear gradient";
      // 
      // gbLinearOptions
      // 
      this.gbLinearOptions.Controls.Add(this.trbLinearContrast);
      this.gbLinearOptions.Controls.Add(this.lblLinearContrast);
      this.gbLinearOptions.Controls.Add(this.lblLinearFocus);
      this.gbLinearOptions.Controls.Add(this.trbLinearFocus);
      this.gbLinearOptions.Location = new System.Drawing.Point(16, 96);
      this.gbLinearOptions.Name = "gbLinearOptions";
      this.gbLinearOptions.Size = new System.Drawing.Size(192, 80);
      this.gbLinearOptions.TabIndex = 5;
      this.gbLinearOptions.TabStop = false;
      this.gbLinearOptions.Text = "Options";
      // 
      // trbLinearContrast
      // 
      this.trbLinearContrast.AutoSize = false;
      this.trbLinearContrast.Location = new System.Drawing.Point(104, 48);
      this.trbLinearContrast.Maximum = 100;
      this.trbLinearContrast.Name = "trbLinearContrast";
      this.trbLinearContrast.Size = new System.Drawing.Size(81, 20);
      this.trbLinearContrast.TabIndex = 0;
      this.trbLinearContrast.TickFrequency = 25;
      this.trbLinearContrast.ValueChanged += new System.EventHandler(this.trbLinearContrast_ValueChanged);
      // 
      // lblLinearContrast
      // 
      this.lblLinearContrast.AutoSize = true;
      this.lblLinearContrast.Location = new System.Drawing.Point(8, 48);
      this.lblLinearContrast.Name = "lblLinearContrast";
      this.lblLinearContrast.Size = new System.Drawing.Size(49, 13);
      this.lblLinearContrast.TabIndex = 2;
      this.lblLinearContrast.Text = "Contrast";
      // 
      // lblLinearFocus
      // 
      this.lblLinearFocus.AutoSize = true;
      this.lblLinearFocus.Location = new System.Drawing.Point(8, 24);
      this.lblLinearFocus.Name = "lblLinearFocus";
      this.lblLinearFocus.Size = new System.Drawing.Size(35, 13);
      this.lblLinearFocus.TabIndex = 2;
      this.lblLinearFocus.Text = "Focus";
      // 
      // trbLinearFocus
      // 
      this.trbLinearFocus.AutoSize = false;
      this.trbLinearFocus.Location = new System.Drawing.Point(104, 24);
      this.trbLinearFocus.Maximum = 100;
      this.trbLinearFocus.Name = "trbLinearFocus";
      this.trbLinearFocus.Size = new System.Drawing.Size(81, 20);
      this.trbLinearFocus.TabIndex = 0;
      this.trbLinearFocus.TickFrequency = 25;
      this.trbLinearFocus.ValueChanged += new System.EventHandler(this.trbLinearFocus_ValueChanged);
      // 
      // gbLinearAngle
      // 
      this.gbLinearAngle.Controls.Add(this.acLinearAngle);
      this.gbLinearAngle.Location = new System.Drawing.Point(216, 12);
      this.gbLinearAngle.Name = "gbLinearAngle";
      this.gbLinearAngle.Size = new System.Drawing.Size(124, 164);
      this.gbLinearAngle.TabIndex = 4;
      this.gbLinearAngle.TabStop = false;
      this.gbLinearAngle.Text = "Angle";
      // 
      // acLinearAngle
      // 
      this.acLinearAngle.Angle = 0;
      this.acLinearAngle.BackColor = System.Drawing.SystemColors.Window;
      this.acLinearAngle.Changed = false;
      this.acLinearAngle.Location = new System.Drawing.Point(12, 20);
      this.acLinearAngle.Name = "acLinearAngle";
      this.acLinearAngle.ShowBorder = true;
      this.acLinearAngle.Size = new System.Drawing.Size(100, 130);
      this.acLinearAngle.TabIndex = 0;
      this.acLinearAngle.Text = "angleControl1";
      this.acLinearAngle.AngleChanged += new System.EventHandler(this.acLinearAngle_AngleChanged);
      // 
      // gbLinearColors
      // 
      this.gbLinearColors.Controls.Add(this.cbxLinearEndColor);
      this.gbLinearColors.Controls.Add(this.lblLinearEndColor);
      this.gbLinearColors.Controls.Add(this.cbxLinearStartColor);
      this.gbLinearColors.Controls.Add(this.lblLinearStartColor);
      this.gbLinearColors.Location = new System.Drawing.Point(16, 12);
      this.gbLinearColors.Name = "gbLinearColors";
      this.gbLinearColors.Size = new System.Drawing.Size(192, 80);
      this.gbLinearColors.TabIndex = 3;
      this.gbLinearColors.TabStop = false;
      this.gbLinearColors.Text = "Colors";
      // 
      // cbxLinearEndColor
      // 
      this.cbxLinearEndColor.Color = System.Drawing.Color.Transparent;
      this.cbxLinearEndColor.Location = new System.Drawing.Point(108, 44);
      this.cbxLinearEndColor.Name = "cbxLinearEndColor";
      this.cbxLinearEndColor.Size = new System.Drawing.Size(70, 21);
      this.cbxLinearEndColor.TabIndex = 3;
      this.cbxLinearEndColor.ColorSelected += new System.EventHandler(this.cbxLinearEndColor_ColorSelected);
      // 
      // lblLinearEndColor
      // 
      this.lblLinearEndColor.AutoSize = true;
      this.lblLinearEndColor.Location = new System.Drawing.Point(8, 48);
      this.lblLinearEndColor.Name = "lblLinearEndColor";
      this.lblLinearEndColor.Size = new System.Drawing.Size(25, 13);
      this.lblLinearEndColor.TabIndex = 2;
      this.lblLinearEndColor.Text = "End";
      // 
      // cbxLinearStartColor
      // 
      this.cbxLinearStartColor.Color = System.Drawing.Color.Transparent;
      this.cbxLinearStartColor.Location = new System.Drawing.Point(108, 20);
      this.cbxLinearStartColor.Name = "cbxLinearStartColor";
      this.cbxLinearStartColor.Size = new System.Drawing.Size(70, 21);
      this.cbxLinearStartColor.TabIndex = 3;
      this.cbxLinearStartColor.ColorSelected += new System.EventHandler(this.cbxLinearStartColor_ColorSelected);
      // 
      // lblLinearStartColor
      // 
      this.lblLinearStartColor.AutoSize = true;
      this.lblLinearStartColor.Location = new System.Drawing.Point(8, 24);
      this.lblLinearStartColor.Name = "lblLinearStartColor";
      this.lblLinearStartColor.Size = new System.Drawing.Size(31, 13);
      this.lblLinearStartColor.TabIndex = 2;
      this.lblLinearStartColor.Text = "Start";
      // 
      // pnPath
      // 
      this.pnPath.BackColor = System.Drawing.SystemColors.Window;
      this.pnPath.Controls.Add(this.gbPathShape);
      this.pnPath.Controls.Add(this.gbPathColors);
      this.pnPath.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnPath.Location = new System.Drawing.Point(139, 1);
      this.pnPath.Name = "pnPath";
      this.pnPath.Size = new System.Drawing.Size(356, 262);
      this.pnPath.TabIndex = 2;
      this.pnPath.Text = "Path gradient";
      // 
      // gbPathShape
      // 
      this.gbPathShape.Controls.Add(this.rbPathRectangle);
      this.gbPathShape.Controls.Add(this.rbPathEllipse);
      this.gbPathShape.Location = new System.Drawing.Point(16, 96);
      this.gbPathShape.Name = "gbPathShape";
      this.gbPathShape.Size = new System.Drawing.Size(324, 80);
      this.gbPathShape.TabIndex = 5;
      this.gbPathShape.TabStop = false;
      this.gbPathShape.Text = "Shape";
      // 
      // rbPathRectangle
      // 
      this.rbPathRectangle.AutoSize = true;
      this.rbPathRectangle.Location = new System.Drawing.Point(12, 44);
      this.rbPathRectangle.Name = "rbPathRectangle";
      this.rbPathRectangle.Size = new System.Drawing.Size(73, 17);
      this.rbPathRectangle.TabIndex = 0;
      this.rbPathRectangle.TabStop = true;
      this.rbPathRectangle.Text = "Rectangle";
      this.rbPathRectangle.UseVisualStyleBackColor = true;
      this.rbPathRectangle.CheckedChanged += new System.EventHandler(this.rbPathEllipse_CheckedChanged);
      // 
      // rbPathEllipse
      // 
      this.rbPathEllipse.AutoSize = true;
      this.rbPathEllipse.Location = new System.Drawing.Point(12, 20);
      this.rbPathEllipse.Name = "rbPathEllipse";
      this.rbPathEllipse.Size = new System.Drawing.Size(54, 17);
      this.rbPathEllipse.TabIndex = 0;
      this.rbPathEllipse.TabStop = true;
      this.rbPathEllipse.Text = "Ellipse";
      this.rbPathEllipse.UseVisualStyleBackColor = true;
      this.rbPathEllipse.CheckedChanged += new System.EventHandler(this.rbPathEllipse_CheckedChanged);
      // 
      // gbPathColors
      // 
      this.gbPathColors.Controls.Add(this.cbxPathEdgeColor);
      this.gbPathColors.Controls.Add(this.lblPathEdgeColor);
      this.gbPathColors.Controls.Add(this.cbxPathCenterColor);
      this.gbPathColors.Controls.Add(this.lblPathCenterColor);
      this.gbPathColors.Location = new System.Drawing.Point(16, 12);
      this.gbPathColors.Name = "gbPathColors";
      this.gbPathColors.Size = new System.Drawing.Size(324, 80);
      this.gbPathColors.TabIndex = 3;
      this.gbPathColors.TabStop = false;
      this.gbPathColors.Text = "Colors";
      // 
      // cbxPathEdgeColor
      // 
      this.cbxPathEdgeColor.Color = System.Drawing.Color.Transparent;
      this.cbxPathEdgeColor.Location = new System.Drawing.Point(108, 44);
      this.cbxPathEdgeColor.Name = "cbxPathEdgeColor";
      this.cbxPathEdgeColor.Size = new System.Drawing.Size(70, 21);
      this.cbxPathEdgeColor.TabIndex = 3;
      this.cbxPathEdgeColor.ColorSelected += new System.EventHandler(this.cbxPathEdgeColor_ColorSelected);
      // 
      // lblPathEdgeColor
      // 
      this.lblPathEdgeColor.AutoSize = true;
      this.lblPathEdgeColor.Location = new System.Drawing.Point(8, 48);
      this.lblPathEdgeColor.Name = "lblPathEdgeColor";
      this.lblPathEdgeColor.Size = new System.Drawing.Size(31, 13);
      this.lblPathEdgeColor.TabIndex = 2;
      this.lblPathEdgeColor.Text = "Edge";
      // 
      // cbxPathCenterColor
      // 
      this.cbxPathCenterColor.Color = System.Drawing.Color.Transparent;
      this.cbxPathCenterColor.Location = new System.Drawing.Point(108, 20);
      this.cbxPathCenterColor.Name = "cbxPathCenterColor";
      this.cbxPathCenterColor.Size = new System.Drawing.Size(70, 21);
      this.cbxPathCenterColor.TabIndex = 3;
      this.cbxPathCenterColor.ColorSelected += new System.EventHandler(this.cbxPathCenterColor_ColorSelected);
      // 
      // lblPathCenterColor
      // 
      this.lblPathCenterColor.AutoSize = true;
      this.lblPathCenterColor.Location = new System.Drawing.Point(8, 24);
      this.lblPathCenterColor.Name = "lblPathCenterColor";
      this.lblPathCenterColor.Size = new System.Drawing.Size(40, 13);
      this.lblPathCenterColor.TabIndex = 2;
      this.lblPathCenterColor.Text = "Center";
      // 
      // pnHatch
      // 
      this.pnHatch.BackColor = System.Drawing.SystemColors.Window;
      this.pnHatch.Controls.Add(this.gbHatchStyle);
      this.pnHatch.Controls.Add(this.gbHatchColors);
      this.pnHatch.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnHatch.Location = new System.Drawing.Point(139, 1);
      this.pnHatch.Name = "pnHatch";
      this.pnHatch.Size = new System.Drawing.Size(356, 262);
      this.pnHatch.TabIndex = 2;
      this.pnHatch.Text = "Hatch";
      // 
      // gbHatchStyle
      // 
      this.gbHatchStyle.Controls.Add(this.cbxHatch);
      this.gbHatchStyle.Location = new System.Drawing.Point(16, 96);
      this.gbHatchStyle.Name = "gbHatchStyle";
      this.gbHatchStyle.Size = new System.Drawing.Size(324, 80);
      this.gbHatchStyle.TabIndex = 5;
      this.gbHatchStyle.TabStop = false;
      this.gbHatchStyle.Text = "Style";
      // 
      // cbxHatch
      // 
      this.cbxHatch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.cbxHatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbxHatch.FormattingEnabled = true;
      this.cbxHatch.IntegralHeight = false;
      this.cbxHatch.ItemHeight = 24;
      this.cbxHatch.Location = new System.Drawing.Point(12, 20);
      this.cbxHatch.Name = "cbxHatch";
      this.cbxHatch.Size = new System.Drawing.Size(300, 30);
      this.cbxHatch.TabIndex = 0;
      this.cbxHatch.HatchSelected += new System.EventHandler(this.cbxHatch_HatchSelected);
      // 
      // gbHatchColors
      // 
      this.gbHatchColors.Controls.Add(this.cbxHatchBackColor);
      this.gbHatchColors.Controls.Add(this.lblHatchBackColor);
      this.gbHatchColors.Controls.Add(this.cbxHatchForeColor);
      this.gbHatchColors.Controls.Add(this.lblHatchForeColor);
      this.gbHatchColors.Location = new System.Drawing.Point(16, 12);
      this.gbHatchColors.Name = "gbHatchColors";
      this.gbHatchColors.Size = new System.Drawing.Size(324, 80);
      this.gbHatchColors.TabIndex = 3;
      this.gbHatchColors.TabStop = false;
      this.gbHatchColors.Text = "Colors";
      // 
      // cbxHatchBackColor
      // 
      this.cbxHatchBackColor.Color = System.Drawing.Color.Transparent;
      this.cbxHatchBackColor.Location = new System.Drawing.Point(108, 44);
      this.cbxHatchBackColor.Name = "cbxHatchBackColor";
      this.cbxHatchBackColor.Size = new System.Drawing.Size(70, 21);
      this.cbxHatchBackColor.TabIndex = 3;
      this.cbxHatchBackColor.ColorSelected += new System.EventHandler(this.cbxHatchBackColor_ColorSelected);
      // 
      // lblHatchBackColor
      // 
      this.lblHatchBackColor.AutoSize = true;
      this.lblHatchBackColor.Location = new System.Drawing.Point(8, 48);
      this.lblHatchBackColor.Name = "lblHatchBackColor";
      this.lblHatchBackColor.Size = new System.Drawing.Size(29, 13);
      this.lblHatchBackColor.TabIndex = 2;
      this.lblHatchBackColor.Text = "Back";
      // 
      // cbxHatchForeColor
      // 
      this.cbxHatchForeColor.Color = System.Drawing.Color.Transparent;
      this.cbxHatchForeColor.Location = new System.Drawing.Point(108, 20);
      this.cbxHatchForeColor.Name = "cbxHatchForeColor";
      this.cbxHatchForeColor.Size = new System.Drawing.Size(70, 21);
      this.cbxHatchForeColor.TabIndex = 3;
      this.cbxHatchForeColor.ColorSelected += new System.EventHandler(this.cbxHatchForeColor_ColorSelected);
      // 
      // lblHatchForeColor
      // 
      this.lblHatchForeColor.AutoSize = true;
      this.lblHatchForeColor.Location = new System.Drawing.Point(8, 24);
      this.lblHatchForeColor.Name = "lblHatchForeColor";
      this.lblHatchForeColor.Size = new System.Drawing.Size(29, 13);
      this.lblHatchForeColor.TabIndex = 2;
      this.lblHatchForeColor.Text = "Fore";
      // 
      // pnSample
      // 
      this.pnSample.Location = new System.Drawing.Point(168, 203);
      this.pnSample.Name = "pnSample";
      this.pnSample.Size = new System.Drawing.Size(323, 56);
      this.pnSample.TabIndex = 3;
      this.pnSample.Paint += new System.Windows.Forms.PaintEventHandler(this.pnSample_Paint);
      // 
      // pcPanels
      // 
      this.pcPanels.Controls.Add(this.pnSolid);
      this.pcPanels.Controls.Add(this.pnLinear);
      this.pcPanels.Controls.Add(this.pnPath);
      this.pcPanels.Controls.Add(this.pnHatch);
      this.pcPanels.Controls.Add(this.pnGlass);
      this.pcPanels.Location = new System.Drawing.Point(12, 12);
      this.pcPanels.Name = "pcPanels";
      this.pcPanels.SelectorWidth = 139;
      this.pcPanels.Size = new System.Drawing.Size(496, 264);
      this.pcPanels.TabIndex = 4;
      this.pcPanels.Text = "pageControl1";
      this.pcPanels.PageSelected += new System.EventHandler(this.pcPanels_PageSelected);
      // 
      // pnGlass
      // 
      this.pnGlass.BackColor = System.Drawing.SystemColors.Window;
      this.pnGlass.Controls.Add(this.gbGlassOptions);
      this.pnGlass.Controls.Add(this.cbGlassHatch);
      this.pnGlass.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnGlass.Location = new System.Drawing.Point(139, 1);
      this.pnGlass.Name = "pnGlass";
      this.pnGlass.Size = new System.Drawing.Size(356, 262);
      this.pnGlass.TabIndex = 3;
      this.pnGlass.Text = "Glass";
      // 
      // gbGlassOptions
      // 
      this.gbGlassOptions.Controls.Add(this.trbGlassBlend);
      this.gbGlassOptions.Controls.Add(this.cbxGlassColor);
      this.gbGlassOptions.Controls.Add(this.lblGlassBlend);
      this.gbGlassOptions.Controls.Add(this.lblGlassColor);
      this.gbGlassOptions.Location = new System.Drawing.Point(16, 12);
      this.gbGlassOptions.Name = "gbGlassOptions";
      this.gbGlassOptions.Size = new System.Drawing.Size(324, 80);
      this.gbGlassOptions.TabIndex = 4;
      this.gbGlassOptions.TabStop = false;
      this.gbGlassOptions.Text = "Options";
      // 
      // trbGlassBlend
      // 
      this.trbGlassBlend.AutoSize = false;
      this.trbGlassBlend.Location = new System.Drawing.Point(104, 48);
      this.trbGlassBlend.Maximum = 100;
      this.trbGlassBlend.Name = "trbGlassBlend";
      this.trbGlassBlend.Size = new System.Drawing.Size(81, 20);
      this.trbGlassBlend.TabIndex = 6;
      this.trbGlassBlend.TickFrequency = 25;
      this.trbGlassBlend.ValueChanged += new System.EventHandler(this.trbGlassBlend_ValueChanged);
      // 
      // cbxGlassColor
      // 
      this.cbxGlassColor.Color = System.Drawing.Color.Transparent;
      this.cbxGlassColor.Location = new System.Drawing.Point(108, 20);
      this.cbxGlassColor.Name = "cbxGlassColor";
      this.cbxGlassColor.Size = new System.Drawing.Size(70, 21);
      this.cbxGlassColor.TabIndex = 3;
      this.cbxGlassColor.ColorSelected += new System.EventHandler(this.cbxGlassColor_ColorSelected);
      // 
      // lblGlassBlend
      // 
      this.lblGlassBlend.AutoSize = true;
      this.lblGlassBlend.Location = new System.Drawing.Point(8, 48);
      this.lblGlassBlend.Name = "lblGlassBlend";
      this.lblGlassBlend.Size = new System.Drawing.Size(33, 13);
      this.lblGlassBlend.TabIndex = 2;
      this.lblGlassBlend.Text = "Blend";
      // 
      // lblGlassColor
      // 
      this.lblGlassColor.AutoSize = true;
      this.lblGlassColor.Location = new System.Drawing.Point(8, 24);
      this.lblGlassColor.Name = "lblGlassColor";
      this.lblGlassColor.Size = new System.Drawing.Size(32, 13);
      this.lblGlassColor.TabIndex = 2;
      this.lblGlassColor.Text = "Color";
      // 
      // cbGlassHatch
      // 
      this.cbGlassHatch.AutoSize = true;
      this.cbGlassHatch.Location = new System.Drawing.Point(16, 104);
      this.cbGlassHatch.Name = "cbGlassHatch";
      this.cbGlassHatch.Size = new System.Drawing.Size(82, 17);
      this.cbGlassHatch.TabIndex = 5;
      this.cbGlassHatch.Text = "Show hatch";
      this.cbGlassHatch.UseVisualStyleBackColor = true;
      this.cbGlassHatch.CheckedChanged += new System.EventHandler(this.cbGlassHatch_CheckedChanged);
      // 
      // FillEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(519, 322);
      this.Controls.Add(this.pnSample);
      this.Controls.Add(this.pcPanels);
      this.Name = "FillEditorForm";
      this.Text = "Fill Editor";
      this.Controls.SetChildIndex(this.pcPanels, 0);
      this.Controls.SetChildIndex(this.pnSample, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.pnSolid.ResumeLayout(false);
      this.gbSolidColors.ResumeLayout(false);
      this.gbSolidColors.PerformLayout();
      this.pnLinear.ResumeLayout(false);
      this.gbLinearOptions.ResumeLayout(false);
      this.gbLinearOptions.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trbLinearContrast)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trbLinearFocus)).EndInit();
      this.gbLinearAngle.ResumeLayout(false);
      this.gbLinearColors.ResumeLayout(false);
      this.gbLinearColors.PerformLayout();
      this.pnPath.ResumeLayout(false);
      this.gbPathShape.ResumeLayout(false);
      this.gbPathShape.PerformLayout();
      this.gbPathColors.ResumeLayout(false);
      this.gbPathColors.PerformLayout();
      this.pnHatch.ResumeLayout(false);
      this.gbHatchStyle.ResumeLayout(false);
      this.gbHatchColors.ResumeLayout(false);
      this.gbHatchColors.PerformLayout();
      this.pcPanels.ResumeLayout(false);
      this.pnGlass.ResumeLayout(false);
      this.pnGlass.PerformLayout();
      this.gbGlassOptions.ResumeLayout(false);
      this.gbGlassOptions.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trbGlassBlend)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private FastReport.Controls.PageControlPage pnSolid;
    private FastReport.Controls.PageControlPage pnLinear;
    private System.Windows.Forms.GroupBox gbLinearColors;
    private FastReport.Controls.ColorComboBox cbxLinearEndColor;
    private System.Windows.Forms.Label lblLinearEndColor;
    private FastReport.Controls.ColorComboBox cbxLinearStartColor;
    private System.Windows.Forms.Label lblLinearStartColor;
    private System.Windows.Forms.GroupBox gbLinearAngle;
    private FastReport.Controls.AngleControl acLinearAngle;
    private System.Windows.Forms.GroupBox gbLinearOptions;
    private System.Windows.Forms.TrackBar trbLinearContrast;
    private System.Windows.Forms.Label lblLinearContrast;
    private System.Windows.Forms.Label lblLinearFocus;
    private System.Windows.Forms.TrackBar trbLinearFocus;
    private System.Windows.Forms.GroupBox gbSolidColors;
    private FastReport.Controls.ColorComboBox cbxSolidColor;
    private System.Windows.Forms.Label lblSolidColor;
    private FastReport.Controls.PageControlPage pnPath;
    private System.Windows.Forms.GroupBox gbPathShape;
    private System.Windows.Forms.RadioButton rbPathRectangle;
    private System.Windows.Forms.RadioButton rbPathEllipse;
    private System.Windows.Forms.GroupBox gbPathColors;
    private FastReport.Controls.ColorComboBox cbxPathEdgeColor;
    private System.Windows.Forms.Label lblPathEdgeColor;
    private FastReport.Controls.ColorComboBox cbxPathCenterColor;
    private System.Windows.Forms.Label lblPathCenterColor;
    private FastReport.Controls.PageControlPage pnHatch;
    private System.Windows.Forms.GroupBox gbHatchStyle;
    private System.Windows.Forms.GroupBox gbHatchColors;
    private FastReport.Controls.ColorComboBox cbxHatchBackColor;
    private System.Windows.Forms.Label lblHatchBackColor;
    private FastReport.Controls.ColorComboBox cbxHatchForeColor;
    private System.Windows.Forms.Label lblHatchForeColor;
    private FillSample pnSample;
    private FastReport.Controls.PageControl pcPanels;
    private FastReport.Controls.PageControlPage pnGlass;
    private System.Windows.Forms.GroupBox gbGlassOptions;
    private FastReport.Controls.ColorComboBox cbxGlassColor;
    private System.Windows.Forms.Label lblGlassColor;
    private System.Windows.Forms.CheckBox cbGlassHatch;
    private FastReport.Controls.HatchComboBox cbxHatch;
    private System.Windows.Forms.TrackBar trbGlassBlend;
    private System.Windows.Forms.Label lblGlassBlend;
  }
}