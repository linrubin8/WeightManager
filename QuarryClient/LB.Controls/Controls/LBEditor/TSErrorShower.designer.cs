namespace LB.Controls.LBEditor
{
	partial class TSErrorShower
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.txtMoreInfo = new System.Windows.Forms.TextBox();
			this.txtMainInfo = new System.Windows.Forms.TextBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.sep = new TSSepLine();
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::LB.Properties.Resources.ErrorInfo;
			this.pictureBox1.Location = new System.Drawing.Point( 12, 9 );
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size( 35, 35 );
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// txtMoreInfo
			// 
			this.txtMoreInfo.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.txtMoreInfo.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 239 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 236 ) ) ) ) );
			this.txtMoreInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtMoreInfo.Location = new System.Drawing.Point( 12, 77 );
			this.txtMoreInfo.Multiline = true;
			this.txtMoreInfo.Name = "txtMoreInfo";
			this.txtMoreInfo.ReadOnly = true;
			this.txtMoreInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtMoreInfo.Size = new System.Drawing.Size( 442, 162 );
			this.txtMoreInfo.TabIndex = 4;
			this.txtMoreInfo.Visible = false;
			// 
			// txtMainInfo
			// 
			this.txtMainInfo.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 239 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 236 ) ) ) ) );
			this.txtMainInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMainInfo.Location = new System.Drawing.Point( 61, 4 );
			this.txtMainInfo.Multiline = true;
			this.txtMainInfo.Name = "txtMainInfo";
			this.txtMainInfo.ReadOnly = true;
			this.txtMainInfo.Size = new System.Drawing.Size( 393, 43 );
			this.txtMainInfo.TabIndex = 2;
			// 
			// btnSave
			// 
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSave.Location = new System.Drawing.Point( 199, 77 );
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size( 103, 23 );
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "保存为文件";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler( this.btnSave_Click );
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Location = new System.Drawing.Point( 330, 77 );
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size( 72, 23 );
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "关闭";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler( this.btnClose_Click );
			// 
			// sep
			// 
			this.sep.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.sep.ForeColor = System.Drawing.Color.DarkBlue;
			this.sep.Location = new System.Drawing.Point( 12, 50 );
			this.sep.Name = "sep";
			this.sep.Size = new System.Drawing.Size( 442, 23 );
			this.sep.TabIndex = 5;
			this.sep.Text = "更多信息";
			this.sep.Click += new System.EventHandler( this.sep_Click );
			// 
			// TSErrorShower
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 239 ) ) ) ), ( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 236 ) ) ) ) );
			this.ClientSize = new System.Drawing.Size( 470, 108 );
			this.Controls.Add( this.sep );
			this.Controls.Add( this.btnClose );
			this.Controls.Add( this.btnSave );
			this.Controls.Add( this.pictureBox1 );
			this.Controls.Add( this.txtMoreInfo );
			this.Controls.Add( this.txtMainInfo );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "TSErrorShower";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "错误信息";
			this.Load += new System.EventHandler( this.TSErrorShower_Load );
			this.BackColorChanged += new System.EventHandler( this.TSErrorShower_BackColorChanged );
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox txtMoreInfo;
		private System.Windows.Forms.TextBox txtMainInfo;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnClose;
		private TSSepLine sep;
	}
}