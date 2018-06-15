namespace LB.Controls.LBEditor
{
	partial class TSTextBoxPopup4Calendar
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
			this.tsCalendar = new System.Windows.Forms.MonthCalendar();
			this.SuspendLayout();
			// 
			// tsCalendar
			// 
			this.tsCalendar.Location = new System.Drawing.Point( 0, 0 );
			this.tsCalendar.Name = "tsCalendar";
			this.tsCalendar.TabIndex = 2;
			this.tsCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler( this.tsCalendar_DateSelected );
			this.tsCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler( this.tsCalendar_DateChanged );
			// 
			// TSTextBoxPopup4Calendar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 267, 145 );
			this.ControlBox = false;
			this.Controls.Add( this.tsCalendar );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "TSTextBoxPopup4Calendar";
			//this.ShowInTaskbar = false;
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.MonthCalendar tsCalendar;


	}
}