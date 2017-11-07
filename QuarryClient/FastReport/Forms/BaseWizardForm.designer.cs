namespace FastReport.Forms
{
  partial class BaseWizardForm
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
      this.pnTop = new System.Windows.Forms.Panel();
      this.lblCaption = new System.Windows.Forms.Label();
      this.picIcon = new System.Windows.Forms.PictureBox();
      this.pnBottom = new System.Windows.Forms.Panel();
      this.btnCancel1 = new System.Windows.Forms.Button();
      this.btnFinish = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.btnPrevious = new System.Windows.Forms.Button();
      this.pcPages = new FastReport.Controls.PageControl();
      this.pnTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
      this.pnBottom.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(8, 360);
      this.btnOk.Visible = false;
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(88, 360);
      this.btnCancel.Visible = false;
      // 
      // pnTop
      // 
      this.pnTop.BackColor = System.Drawing.Color.White;
      this.pnTop.Controls.Add(this.lblCaption);
      this.pnTop.Controls.Add(this.picIcon);
      this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnTop.Location = new System.Drawing.Point(0, 0);
      this.pnTop.Name = "pnTop";
      this.pnTop.Size = new System.Drawing.Size(463, 68);
      this.pnTop.TabIndex = 2;
      this.pnTop.Paint += new System.Windows.Forms.PaintEventHandler(this.pnTop_Paint);
      // 
      // lblCaption
      // 
      this.lblCaption.AutoSize = true;
      this.lblCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblCaption.Location = new System.Drawing.Point(16, 24);
      this.lblCaption.Name = "lblCaption";
      this.lblCaption.Size = new System.Drawing.Size(62, 17);
      this.lblCaption.TabIndex = 4;
      this.lblCaption.Text = "Caption";
      // 
      // picIcon
      // 
      this.picIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.picIcon.Location = new System.Drawing.Point(404, 8);
      this.picIcon.Name = "picIcon";
      this.picIcon.Size = new System.Drawing.Size(48, 48);
      this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.picIcon.TabIndex = 3;
      this.picIcon.TabStop = false;
      // 
      // pnBottom
      // 
      this.pnBottom.Controls.Add(this.btnCancel1);
      this.pnBottom.Controls.Add(this.btnFinish);
      this.pnBottom.Controls.Add(this.btnNext);
      this.pnBottom.Controls.Add(this.btnPrevious);
      this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnBottom.Location = new System.Drawing.Point(0, 408);
      this.pnBottom.Name = "pnBottom";
      this.pnBottom.Size = new System.Drawing.Size(463, 45);
      this.pnBottom.TabIndex = 4;
      this.pnBottom.Paint += new System.Windows.Forms.PaintEventHandler(this.pnBottom_Paint);
      // 
      // btnCancel1
      // 
      this.btnCancel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel1.Location = new System.Drawing.Point(377, 12);
      this.btnCancel1.Name = "btnCancel1";
      this.btnCancel1.Size = new System.Drawing.Size(75, 23);
      this.btnCancel1.TabIndex = 0;
      this.btnCancel1.Text = "Cancel";
      this.btnCancel1.UseVisualStyleBackColor = true;
      // 
      // btnFinish
      // 
      this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnFinish.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnFinish.Location = new System.Drawing.Point(293, 12);
      this.btnFinish.Name = "btnFinish";
      this.btnFinish.Size = new System.Drawing.Size(75, 23);
      this.btnFinish.TabIndex = 0;
      this.btnFinish.Text = "Finish";
      this.btnFinish.UseVisualStyleBackColor = true;
      // 
      // btnNext
      // 
      this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnNext.Location = new System.Drawing.Point(209, 12);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 0;
      this.btnNext.Text = "Next >";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // btnPrevious
      // 
      this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnPrevious.Location = new System.Drawing.Point(125, 12);
      this.btnPrevious.Name = "btnPrevious";
      this.btnPrevious.Size = new System.Drawing.Size(75, 23);
      this.btnPrevious.TabIndex = 0;
      this.btnPrevious.Text = "< Previous";
      this.btnPrevious.UseVisualStyleBackColor = true;
      this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
      // 
      // pcPages
      // 
      this.pcPages.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pcPages.Location = new System.Drawing.Point(0, 68);
      this.pcPages.Name = "pcPages";
      this.pcPages.Size = new System.Drawing.Size(463, 340);
      this.pcPages.TabIndex = 6;
      this.pcPages.Text = "pageControl1";
      // 
      // BaseWizardForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(463, 453);
      this.Controls.Add(this.pcPages);
      this.Controls.Add(this.pnBottom);
      this.Controls.Add(this.pnTop);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
      this.MinimumSize = new System.Drawing.Size(400, 400);
      this.Name = "BaseWizardForm";
      this.ShowIcon = false;
      this.Text = "Wizard";
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.pnTop, 0);
      this.Controls.SetChildIndex(this.pnBottom, 0);
      this.Controls.SetChildIndex(this.pcPages, 0);
      this.pnTop.ResumeLayout(false);
      this.pnTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
      this.pnBottom.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.Panel pnTop;
    public System.Windows.Forms.Label lblCaption;
    public System.Windows.Forms.PictureBox picIcon;
    public System.Windows.Forms.Panel pnBottom;
    public System.Windows.Forms.Button btnCancel1;
    public System.Windows.Forms.Button btnFinish;
    public System.Windows.Forms.Button btnNext;
    public System.Windows.Forms.Button btnPrevious;
    public FastReport.Controls.PageControl pcPages;

  }
}
