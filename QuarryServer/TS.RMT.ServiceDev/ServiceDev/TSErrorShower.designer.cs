namespace TS.RMT.ServiceDev
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TSErrorShower));
			this.txtMoreInfo = new System.Windows.Forms.TextBox();
			this.txtMainInfo = new System.Windows.Forms.TextBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtMoreInfo
			// 
			this.txtMoreInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMoreInfo.BackColor = System.Drawing.SystemColors.Control;
			this.txtMoreInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtMoreInfo.Location = new System.Drawing.Point(12, 77);
			this.txtMoreInfo.Multiline = true;
			this.txtMoreInfo.Name = "txtMoreInfo";
			this.txtMoreInfo.ReadOnly = true;
			this.txtMoreInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtMoreInfo.Size = new System.Drawing.Size(442, 169);
			this.txtMoreInfo.TabIndex = 4;
			this.txtMoreInfo.Visible = false;
			// 
			// txtMainInfo
			// 
			this.txtMainInfo.BackColor = System.Drawing.SystemColors.Control;
			this.txtMainInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMainInfo.Location = new System.Drawing.Point(12, 12);
			this.txtMainInfo.Multiline = true;
			this.txtMainInfo.Name = "txtMainInfo";
			this.txtMainInfo.ReadOnly = true;
			this.txtMainInfo.Size = new System.Drawing.Size(442, 43);
			this.txtMainInfo.TabIndex = 2;
			// 
			// btnSave
			// 
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSave.Location = new System.Drawing.Point(203, 266);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(103, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "保存为文件";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Location = new System.Drawing.Point(334, 266);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(72, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "关闭";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// TSErrorShower
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(470, 307);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.txtMoreInfo);
			this.Controls.Add(this.txtMainInfo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TSErrorShower";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "错误信息";
			this.BackColorChanged += new System.EventHandler(this.TSErrorShower_BackColorChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtMoreInfo;
		private System.Windows.Forms.TextBox txtMainInfo;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnClose;
	}
}