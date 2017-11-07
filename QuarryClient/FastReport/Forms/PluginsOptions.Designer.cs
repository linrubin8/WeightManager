namespace FastReport.Forms
{
  partial class PluginsOptions
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
            this.tab2 = new System.Windows.Forms.TabPage();
            this.lbPlugins = new System.Windows.Forms.ListBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblUIStyle = new System.Windows.Forms.Label();
            this.cbxUIStyle = new System.Windows.Forms.ComboBox();
            this.cbxRibbon = new System.Windows.Forms.CheckBox();
            this.cbxWelcome = new System.Windows.Forms.CheckBox();
            this.lblRightToLeft = new System.Windows.Forms.Label();
            this.cbxRightToLeft = new System.Windows.Forms.ComboBox();
            this.tc1.SuspendLayout();
            this.tab1.SuspendLayout();
            this.tab2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tc1
            // 
            this.tc1.Controls.Add(this.tab2);
            this.tc1.Controls.SetChildIndex(this.tab2, 0);
            this.tc1.Controls.SetChildIndex(this.tab1, 0);
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.cbxRightToLeft);
            this.tab1.Controls.Add(this.lblRightToLeft);
            this.tab1.Controls.Add(this.cbxWelcome);
            this.tab1.Controls.Add(this.cbxRibbon);
            this.tab1.Controls.Add(this.cbxUIStyle);
            this.tab1.Controls.Add(this.lblUIStyle);
            this.tab1.Text = "User Interface";
            // 
            // tab2
            // 
            this.tab2.Controls.Add(this.lbPlugins);
            this.tab2.Controls.Add(this.lblNote);
            this.tab2.Controls.Add(this.btnDown);
            this.tab2.Controls.Add(this.btnUp);
            this.tab2.Controls.Add(this.btnRemove);
            this.tab2.Controls.Add(this.btnAdd);
            this.tab2.Controls.Add(this.lblHint);
            this.tab2.Location = new System.Drawing.Point(4, 22);
            this.tab2.Name = "tab2";
            this.tab2.Padding = new System.Windows.Forms.Padding(3);
            this.tab2.Size = new System.Drawing.Size(368, 250);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "Plugins";
            this.tab2.UseVisualStyleBackColor = true;
            // 
            // lbPlugins
            // 
            this.lbPlugins.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbPlugins.FormattingEnabled = true;
            this.lbPlugins.IntegralHeight = false;
            this.lbPlugins.ItemHeight = 19;
            this.lbPlugins.Location = new System.Drawing.Point(16, 36);
            this.lbPlugins.Name = "lbPlugins";
            this.lbPlugins.Size = new System.Drawing.Size(252, 176);
            this.lbPlugins.TabIndex = 14;
            this.lbPlugins.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbPlugins_DrawItem);
            this.lbPlugins.SelectedIndexChanged += new System.EventHandler(this.lbPlugins_SelectedIndexChanged);
            // 
            // lblNote
            // 
            this.lblNote.Location = new System.Drawing.Point(16, 216);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(336, 28);
            this.lblNote.TabIndex = 13;
            this.lblNote.Text = "Note: changes made will take effect on FastReport restart.";
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(280, 188);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(24, 23);
            this.btnDown.TabIndex = 12;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(280, 160);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(24, 23);
            this.btnUp.TabIndex = 11;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(280, 64);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(280, 36);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Location = new System.Drawing.Point(16, 16);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(175, 13);
            this.lblHint.TabIndex = 8;
            this.lblHint.Text = "List of installed FastReport plugins:";
            // 
            // lblUIStyle
            // 
            this.lblUIStyle.AutoSize = true;
            this.lblUIStyle.Location = new System.Drawing.Point(16, 20);
            this.lblUIStyle.Name = "lblUIStyle";
            this.lblUIStyle.Size = new System.Drawing.Size(105, 13);
            this.lblUIStyle.TabIndex = 0;
            this.lblUIStyle.Text = "User interface style:";
            // 
            // cbxUIStyle
            // 
            this.cbxUIStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUIStyle.FormattingEnabled = true;
            this.cbxUIStyle.Location = new System.Drawing.Point(192, 16);
            this.cbxUIStyle.Name = "cbxUIStyle";
            this.cbxUIStyle.Size = new System.Drawing.Size(160, 21);
            this.cbxUIStyle.TabIndex = 1;
            // 
            // cbxRibbon
            // 
            this.cbxRibbon.AutoSize = true;
            this.cbxRibbon.Location = new System.Drawing.Point(19, 56);
            this.cbxRibbon.Name = "cbxRibbon";
            this.cbxRibbon.Size = new System.Drawing.Size(168, 17);
            this.cbxRibbon.TabIndex = 2;
            this.cbxRibbon.Text = "Use the new Ribbon interface";
            this.cbxRibbon.UseVisualStyleBackColor = true;
            // 
            // cbxWelcome
            // 
            this.cbxWelcome.AutoSize = true;
            this.cbxWelcome.Location = new System.Drawing.Point(19, 88);
            this.cbxWelcome.Name = "cbxWelcome";
            this.cbxWelcome.Size = new System.Drawing.Size(188, 17);
            this.cbxWelcome.TabIndex = 3;
            this.cbxWelcome.Text = "Show welcome window on startup";
            this.cbxWelcome.UseVisualStyleBackColor = true;
            // 
            // lblRightToLeft
            // 
            this.lblRightToLeft.AutoSize = true;
            this.lblRightToLeft.Location = new System.Drawing.Point(16, 120);
            this.lblRightToLeft.Name = "lblRightToLeft";
            this.lblRightToLeft.Size = new System.Drawing.Size(141, 13);
            this.lblRightToLeft.TabIndex = 4;
            this.lblRightToLeft.Text = "Right to Left user interface:";
            // 
            // cbxRightToLeft
            // 
            this.cbxRightToLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRightToLeft.FormattingEnabled = true;
            this.cbxRightToLeft.Location = new System.Drawing.Point(231, 117);
            this.cbxRightToLeft.Name = "cbxRightToLeft";
            this.cbxRightToLeft.Size = new System.Drawing.Size(121, 21);
            this.cbxRightToLeft.TabIndex = 5;
            // 
            // PluginsOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(398, 323);
            this.Name = "PluginsOptions";
            this.tc1.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TabPage tab2;
    private System.Windows.Forms.ListBox lbPlugins;
    private System.Windows.Forms.Label lblNote;
    private System.Windows.Forms.Button btnDown;
    private System.Windows.Forms.Button btnUp;
    private System.Windows.Forms.Button btnRemove;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Label lblHint;
    private System.Windows.Forms.ComboBox cbxUIStyle;
    private System.Windows.Forms.Label lblUIStyle;
    private System.Windows.Forms.CheckBox cbxRibbon;
    private System.Windows.Forms.CheckBox cbxWelcome;
    private System.Windows.Forms.ComboBox cbxRightToLeft;
    private System.Windows.Forms.Label lblRightToLeft;
  }
}
