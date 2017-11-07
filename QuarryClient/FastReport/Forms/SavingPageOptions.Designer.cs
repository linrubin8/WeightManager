namespace FastReport.Forms
{
    partial class SavingPageOptions
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
            this.cbAutoSave = new System.Windows.Forms.CheckBox();
            this.nudMinutes = new System.Windows.Forms.NumericUpDown();
            this.tc1.SuspendLayout();
            this.tab1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).BeginInit();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.nudMinutes);
            this.tab1.Controls.Add(this.cbAutoSave);
            // 
            // cbAutoSave
            // 
            this.cbAutoSave.AutoSize = true;
            this.cbAutoSave.Location = new System.Drawing.Point(16, 16);
            this.cbAutoSave.Name = "cbAutoSave";
            this.cbAutoSave.Size = new System.Drawing.Size(188, 17);
            this.cbAutoSave.TabIndex = 5;
            this.cbAutoSave.Text = "Enable auto save every (minutes)";
            this.cbAutoSave.UseVisualStyleBackColor = true;
            // 
            // nudMinutes
            // 
            this.nudMinutes.Location = new System.Drawing.Point(210, 14);
            this.nudMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinutes.Name = "nudMinutes";
            this.nudMinutes.Size = new System.Drawing.Size(43, 21);
            this.nudMinutes.TabIndex = 6;
            this.nudMinutes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SavingPageOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 331);
            this.Name = "SavingPageOptions";
            this.tc1.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudMinutes;
        private System.Windows.Forms.CheckBox cbAutoSave;
    }
}