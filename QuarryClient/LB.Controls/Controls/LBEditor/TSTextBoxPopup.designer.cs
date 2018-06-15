namespace LB.Controls.LBEditor
{
	partial class TSTextBoxPopup
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
			this.pnlButton = new System.Windows.Forms.Panel();
			this.lstBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// pnlButton
			// 
			this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlButton.Location = new System.Drawing.Point( 0, 195 );
			this.pnlButton.Name = "pnlButton";
			this.pnlButton.Size = new System.Drawing.Size( 152, 35 );
			this.pnlButton.TabIndex = 1;
			this.pnlButton.Paint += new System.Windows.Forms.PaintEventHandler( this.pnlButton_Paint );
			this.pnlButton.MouseMove += new System.Windows.Forms.MouseEventHandler( this.pnlButton_MouseMove );
			this.pnlButton.MouseDown += new System.Windows.Forms.MouseEventHandler( this.pnlButton_MouseDown );
			this.pnlButton.MouseUp += new System.Windows.Forms.MouseEventHandler( this.pnlButton_MouseUp );
			// 
			// lstBox
			// 
			this.lstBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lstBox.FormattingEnabled = true;
			this.lstBox.IntegralHeight = false;
			this.lstBox.ItemHeight = 17;
			this.lstBox.Location = new System.Drawing.Point( 0, 0 );
			this.lstBox.Name = "lstBox";
			this.lstBox.Size = new System.Drawing.Size( 152, 195 );
			this.lstBox.TabIndex = 2;
			this.lstBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler( this.lstBox_DrawItem );
			this.lstBox.Click += new System.EventHandler( this.lstBox_Click );
			// 
			// TSTextBoxPopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 152, 230 );
			this.Controls.Add( this.lstBox );
			this.Controls.Add( this.pnlButton );
			this.MinimumSize = new System.Drawing.Size( 160, 200 );
			this.Name = "TSTextBoxPopup";
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.Panel pnlButton;
		private System.Windows.Forms.ListBox lstBox;

	}
}