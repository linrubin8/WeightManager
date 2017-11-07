namespace FastReport.FastQueryBuilder
{
    partial class InputBox
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
          this.TextBox = new System.Windows.Forms.TextBox();
          this.TextLabel = new System.Windows.Forms.Label();
          this.button1 = new System.Windows.Forms.Button();
          this.button2 = new System.Windows.Forms.Button();
          this.SuspendLayout();
          // 
          // TextBox
          // 
          this.TextBox.Location = new System.Drawing.Point(11, 28);
          this.TextBox.Name = "TextBox";
          this.TextBox.Size = new System.Drawing.Size(244, 21);
          this.TextBox.TabIndex = 0;
          // 
          // TextLabel
          // 
          this.TextLabel.AutoSize = true;
          this.TextLabel.Location = new System.Drawing.Point(12, 12);
          this.TextLabel.Name = "TextLabel";
          this.TextLabel.Size = new System.Drawing.Size(35, 13);
          this.TextLabel.TabIndex = 1;
          this.TextLabel.Text = "label1";
          // 
          // button1
          // 
          this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
          this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
          this.button1.Location = new System.Drawing.Point(54, 63);
          this.button1.Name = "button1";
          this.button1.Size = new System.Drawing.Size(75, 23);
          this.button1.TabIndex = 1;
          this.button1.Text = "Ok";
          this.button1.UseVisualStyleBackColor = true;
          // 
          // button2
          // 
          this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
          this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.button2.Location = new System.Drawing.Point(135, 63);
          this.button2.Name = "button2";
          this.button2.Size = new System.Drawing.Size(75, 23);
          this.button2.TabIndex = 3;
          this.button2.Text = "Cancel";
          this.button2.UseVisualStyleBackColor = true;
          // 
          // InputBox
          // 
          this.AcceptButton = this.button1;
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(267, 98);
          this.Controls.Add(this.button2);
          this.Controls.Add(this.button1);
          this.Controls.Add(this.TextLabel);
          this.Controls.Add(this.TextBox);
          this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.Name = "InputBox";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
          this.Text = "InputBox";
          this.Load += new System.EventHandler(this.InputBox_Load);
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox TextBox;
        public System.Windows.Forms.Label TextLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}