namespace FastReport.Forms
{
    partial class PictureEditorAdvancedForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.gbColor = new System.Windows.Forms.GroupBox();
            this.rbGrayscale = new System.Windows.Forms.RadioButton();
            this.rbMonochrome = new System.Windows.Forms.RadioButton();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.gbCrop = new System.Windows.Forms.GroupBox();
            this.nudBottom = new System.Windows.Forms.NumericUpDown();
            this.nudRight = new System.Windows.Forms.NumericUpDown();
            this.nudLeft = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudTop = new System.Windows.Forms.NumericUpDown();
            this.lblChange2 = new System.Windows.Forms.Label();
            this.rbPixelsCrop = new System.Windows.Forms.RadioButton();
            this.rbPercentCrop = new System.Windows.Forms.RadioButton();
            this.gbResize = new System.Windows.Forms.GroupBox();
            this.nudHor = new System.Windows.Forms.NumericUpDown();
            this.cbAspectRatio = new System.Windows.Forms.CheckBox();
            this.lblHor = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.nudVer = new System.Windows.Forms.NumericUpDown();
            this.lblVer = new System.Windows.Forms.Label();
            this.rbPixelsResize = new System.Windows.Forms.RadioButton();
            this.rbPercentResize = new System.Windows.Forms.RadioButton();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.panelBottom.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.gbColor.SuspendLayout();
            this.gbCrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).BeginInit();
            this.gbResize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVer)).BeginInit();
            this.panelMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(470, 13);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(659, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelBottom.Controls.Add(this.btnReset);
            this.panelBottom.Controls.Add(this.btnOK);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 499);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(746, 48);
            this.panelBottom.TabIndex = 11;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(551, 13);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(102, 23);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // panelTop
            // 
            this.panelTop.AutoScroll = true;
            this.panelTop.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelTop.Controls.Add(this.gbColor);
            this.panelTop.Controls.Add(this.gbCrop);
            this.panelTop.Controls.Add(this.gbResize);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(746, 175);
            this.panelTop.TabIndex = 12;
            // 
            // gbColor
            // 
            this.gbColor.Controls.Add(this.rbGrayscale);
            this.gbColor.Controls.Add(this.rbMonochrome);
            this.gbColor.Controls.Add(this.rbNone);
            this.gbColor.Location = new System.Drawing.Point(554, 12);
            this.gbColor.Name = "gbColor";
            this.gbColor.Padding = new System.Windows.Forms.Padding(10);
            this.gbColor.Size = new System.Drawing.Size(180, 146);
            this.gbColor.TabIndex = 19;
            this.gbColor.TabStop = false;
            this.gbColor.Text = "Color";
            // 
            // rbGrayscale
            // 
            this.rbGrayscale.AutoSize = true;
            this.rbGrayscale.Location = new System.Drawing.Point(13, 46);
            this.rbGrayscale.Name = "rbGrayscale";
            this.rbGrayscale.Size = new System.Drawing.Size(72, 17);
            this.rbGrayscale.TabIndex = 2;
            this.rbGrayscale.Text = "Grayscale";
            this.rbGrayscale.UseVisualStyleBackColor = true;
            this.rbGrayscale.CheckedChanged += new System.EventHandler(this.rbNone_CheckedChanged);
            // 
            // rbMonochrome
            // 
            this.rbMonochrome.AutoSize = true;
            this.rbMonochrome.Location = new System.Drawing.Point(13, 69);
            this.rbMonochrome.Name = "rbMonochrome";
            this.rbMonochrome.Size = new System.Drawing.Size(87, 17);
            this.rbMonochrome.TabIndex = 1;
            this.rbMonochrome.Text = "Monochrome";
            this.rbMonochrome.UseVisualStyleBackColor = true;
            this.rbMonochrome.CheckedChanged += new System.EventHandler(this.rbNone_CheckedChanged);
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Checked = true;
            this.rbNone.Location = new System.Drawing.Point(13, 23);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(51, 17);
            this.rbNone.TabIndex = 0;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "None";
            this.rbNone.UseVisualStyleBackColor = true;
            this.rbNone.CheckedChanged += new System.EventHandler(this.rbNone_CheckedChanged);
            // 
            // gbCrop
            // 
            this.gbCrop.Controls.Add(this.nudBottom);
            this.gbCrop.Controls.Add(this.nudRight);
            this.gbCrop.Controls.Add(this.nudLeft);
            this.gbCrop.Controls.Add(this.label4);
            this.gbCrop.Controls.Add(this.label3);
            this.gbCrop.Controls.Add(this.label2);
            this.gbCrop.Controls.Add(this.label1);
            this.gbCrop.Controls.Add(this.nudTop);
            this.gbCrop.Controls.Add(this.lblChange2);
            this.gbCrop.Controls.Add(this.rbPixelsCrop);
            this.gbCrop.Controls.Add(this.rbPercentCrop);
            this.gbCrop.Location = new System.Drawing.Point(283, 12);
            this.gbCrop.Name = "gbCrop";
            this.gbCrop.Padding = new System.Windows.Forms.Padding(10);
            this.gbCrop.Size = new System.Drawing.Size(265, 146);
            this.gbCrop.TabIndex = 18;
            this.gbCrop.TabStop = false;
            this.gbCrop.Text = "Crop";
            // 
            // nudBottom
            // 
            this.nudBottom.Location = new System.Drawing.Point(100, 115);
            this.nudBottom.Name = "nudBottom";
            this.nudBottom.Size = new System.Drawing.Size(62, 20);
            this.nudBottom.TabIndex = 41;
            // 
            // nudRight
            // 
            this.nudRight.Location = new System.Drawing.Point(163, 83);
            this.nudRight.Name = "nudRight";
            this.nudRight.Size = new System.Drawing.Size(62, 20);
            this.nudRight.TabIndex = 40;
            // 
            // nudLeft
            // 
            this.nudLeft.Location = new System.Drawing.Point(36, 83);
            this.nudLeft.Name = "nudLeft";
            this.nudLeft.Size = new System.Drawing.Size(62, 20);
            this.nudLeft.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(124, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 16);
            this.label4.TabIndex = 38;
            this.label4.Text = "↑";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(136, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 16);
            this.label3.TabIndex = 37;
            this.label3.Text = "←";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(104, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 16);
            this.label2.TabIndex = 36;
            this.label2.Text = "→";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(124, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 16);
            this.label1.TabIndex = 35;
            this.label1.Text = "↓";
            // 
            // nudTop
            // 
            this.nudTop.Location = new System.Drawing.Point(100, 53);
            this.nudTop.Name = "nudTop";
            this.nudTop.Size = new System.Drawing.Size(62, 20);
            this.nudTop.TabIndex = 23;
            // 
            // lblChange2
            // 
            this.lblChange2.AutoSize = true;
            this.lblChange2.Location = new System.Drawing.Point(13, 25);
            this.lblChange2.Name = "lblChange2";
            this.lblChange2.Size = new System.Drawing.Size(47, 13);
            this.lblChange2.TabIndex = 19;
            this.lblChange2.Text = "Change:";
            // 
            // rbPixelsCrop
            // 
            this.rbPixelsCrop.AutoSize = true;
            this.rbPixelsCrop.Location = new System.Drawing.Point(185, 23);
            this.rbPixelsCrop.Name = "rbPixelsCrop";
            this.rbPixelsCrop.Size = new System.Drawing.Size(51, 17);
            this.rbPixelsCrop.TabIndex = 18;
            this.rbPixelsCrop.Text = "pixels";
            this.rbPixelsCrop.UseVisualStyleBackColor = true;
            // 
            // rbPercentCrop
            // 
            this.rbPercentCrop.AutoSize = true;
            this.rbPercentCrop.Checked = true;
            this.rbPercentCrop.Location = new System.Drawing.Point(100, 23);
            this.rbPercentCrop.Name = "rbPercentCrop";
            this.rbPercentCrop.Size = new System.Drawing.Size(61, 17);
            this.rbPercentCrop.TabIndex = 17;
            this.rbPercentCrop.TabStop = true;
            this.rbPercentCrop.Text = "percent";
            this.rbPercentCrop.UseVisualStyleBackColor = true;
            // 
            // gbResize
            // 
            this.gbResize.Controls.Add(this.nudHor);
            this.gbResize.Controls.Add(this.cbAspectRatio);
            this.gbResize.Controls.Add(this.lblHor);
            this.gbResize.Controls.Add(this.lblChange);
            this.gbResize.Controls.Add(this.nudVer);
            this.gbResize.Controls.Add(this.lblVer);
            this.gbResize.Controls.Add(this.rbPixelsResize);
            this.gbResize.Controls.Add(this.rbPercentResize);
            this.gbResize.Location = new System.Drawing.Point(12, 12);
            this.gbResize.Name = "gbResize";
            this.gbResize.Padding = new System.Windows.Forms.Padding(10);
            this.gbResize.Size = new System.Drawing.Size(265, 146);
            this.gbResize.TabIndex = 17;
            this.gbResize.TabStop = false;
            this.gbResize.Text = "Resize";
            // 
            // nudHor
            // 
            this.nudHor.Location = new System.Drawing.Point(100, 53);
            this.nudHor.Name = "nudHor";
            this.nudHor.Size = new System.Drawing.Size(62, 20);
            this.nudHor.TabIndex = 15;
            // 
            // cbAspectRatio
            // 
            this.cbAspectRatio.AutoSize = true;
            this.cbAspectRatio.Checked = true;
            this.cbAspectRatio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAspectRatio.Location = new System.Drawing.Point(16, 115);
            this.cbAspectRatio.Name = "cbAspectRatio";
            this.cbAspectRatio.Size = new System.Drawing.Size(109, 17);
            this.cbAspectRatio.TabIndex = 16;
            this.cbAspectRatio.Text = "Keep aspect ratio";
            this.cbAspectRatio.UseVisualStyleBackColor = true;
            // 
            // lblHor
            // 
            this.lblHor.AutoSize = true;
            this.lblHor.Location = new System.Drawing.Point(13, 55);
            this.lblHor.Name = "lblHor";
            this.lblHor.Size = new System.Drawing.Size(57, 13);
            this.lblHor.TabIndex = 12;
            this.lblHor.Text = "Horizontal:";
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Location = new System.Drawing.Point(13, 25);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(47, 13);
            this.lblChange.TabIndex = 11;
            this.lblChange.Text = "Change:";
            // 
            // nudVer
            // 
            this.nudVer.Location = new System.Drawing.Point(100, 83);
            this.nudVer.Name = "nudVer";
            this.nudVer.Size = new System.Drawing.Size(62, 20);
            this.nudVer.TabIndex = 14;
            // 
            // lblVer
            // 
            this.lblVer.AutoSize = true;
            this.lblVer.Location = new System.Drawing.Point(13, 85);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(45, 13);
            this.lblVer.TabIndex = 13;
            this.lblVer.Text = "Vertical:";
            // 
            // rbPixelsResize
            // 
            this.rbPixelsResize.AutoSize = true;
            this.rbPixelsResize.Location = new System.Drawing.Point(185, 23);
            this.rbPixelsResize.Name = "rbPixelsResize";
            this.rbPixelsResize.Size = new System.Drawing.Size(51, 17);
            this.rbPixelsResize.TabIndex = 10;
            this.rbPixelsResize.Text = "pixels";
            this.rbPixelsResize.UseVisualStyleBackColor = true;
            // 
            // rbPercentResize
            // 
            this.rbPercentResize.AutoSize = true;
            this.rbPercentResize.Checked = true;
            this.rbPercentResize.Location = new System.Drawing.Point(100, 23);
            this.rbPercentResize.Name = "rbPercentResize";
            this.rbPercentResize.Size = new System.Drawing.Size(61, 17);
            this.rbPercentResize.TabIndex = 9;
            this.rbPercentResize.TabStop = true;
            this.rbPercentResize.Text = "percent";
            this.rbPercentResize.UseVisualStyleBackColor = true;
            // 
            // panelMiddle
            // 
            this.panelMiddle.AutoScroll = true;
            this.panelMiddle.Controls.Add(this.pictureBox);
            this.panelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMiddle.Location = new System.Drawing.Point(0, 175);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Size = new System.Drawing.Size(746, 324);
            this.panelMiddle.TabIndex = 13;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(746, 324);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 547);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(746, 22);
            this.statusBar.TabIndex = 14;
            this.statusBar.Text = "statusStrip1";
            // 
            // PictureEditorAdvancedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(746, 569);
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.statusBar);
            this.Name = "PictureEditorAdvancedForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Title";
            this.panelBottom.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.gbColor.ResumeLayout(false);
            this.gbColor.PerformLayout();
            this.gbCrop.ResumeLayout(false);
            this.gbCrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).EndInit();
            this.gbResize.ResumeLayout(false);
            this.gbResize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVer)).EndInit();
            this.panelMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.GroupBox gbColor;
        private System.Windows.Forms.GroupBox gbCrop;
        private System.Windows.Forms.GroupBox gbResize;
        private System.Windows.Forms.NumericUpDown nudHor;
        private System.Windows.Forms.CheckBox cbAspectRatio;
        private System.Windows.Forms.Label lblHor;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.NumericUpDown nudVer;
        private System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.RadioButton rbPixelsResize;
        private System.Windows.Forms.RadioButton rbPercentResize;
        private System.Windows.Forms.Panel panelMiddle;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.NumericUpDown nudTop;
        private System.Windows.Forms.Label lblChange2;
        private System.Windows.Forms.RadioButton rbPixelsCrop;
        private System.Windows.Forms.RadioButton rbPercentCrop;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudBottom;
        private System.Windows.Forms.NumericUpDown nudRight;
        private System.Windows.Forms.NumericUpDown nudLeft;
        private System.Windows.Forms.RadioButton rbGrayscale;
        private System.Windows.Forms.RadioButton rbMonochrome;
        private System.Windows.Forms.RadioButton rbNone;
    }
}