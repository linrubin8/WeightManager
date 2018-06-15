namespace LB.Controls.LBEditor
{
	partial class frmFullPictureViewer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFullPictureViewer));
			this.picMain = new TSPictureZoomer();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
			this.SuspendLayout();
			// 
			// picMain
			// 
			this.picMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picMain.DoubleClickViewPicture = true;
			this.picMain.Image = null;
			this.picMain.ImageName = "";
			this.picMain.LeftButton4Select = true;
			this.picMain.LoadFilePromptSize = 0;
			this.picMain.Location = new System.Drawing.Point(3, 30);
			this.picMain.Name = "picMain";
			this.picMain.Size = new System.Drawing.Size(688, 480);
			this.picMain.TabIndex = 0;
			this.picMain.TabStop = false;
			this.picMain.ZoomPercent = 100;
			// 
			// checkBox1
			// 
			this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(634, 8);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(48, 16);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "置顶";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// frmFullPictureViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(694, 513);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.picMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmFullPictureViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "图片浏览";
			this.TopMost = true;
			this.SizeChanged += new System.EventHandler(this.frmFullPictureViewer_SizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TSPictureZoomer picMain;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}