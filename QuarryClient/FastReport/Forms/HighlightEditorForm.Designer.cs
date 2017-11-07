namespace FastReport.Forms
{
  partial class HighlightEditorForm
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
      this.gbConditions = new System.Windows.Forms.GroupBox();
      this.lvConditions = new System.Windows.Forms.ListView();
      this.btnDown = new System.Windows.Forms.Button();
      this.btnUp = new System.Windows.Forms.Button();
      this.btnEdit = new System.Windows.Forms.Button();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnAdd = new System.Windows.Forms.Button();
      this.gbStyle = new System.Windows.Forms.GroupBox();
      this.cbVisible = new System.Windows.Forms.CheckBox();
      this.cbApplyFont = new System.Windows.Forms.CheckBox();
      this.cbApplyTextFill = new System.Windows.Forms.CheckBox();
      this.cbApplyBorder = new System.Windows.Forms.CheckBox();
      this.cbApplyFill = new System.Windows.Forms.CheckBox();
      this.pnSample = new System.Windows.Forms.Panel();
      this.btnFont = new System.Windows.Forms.Button();
      this.btnTextColor = new System.Windows.Forms.Button();
      this.btnBorder = new System.Windows.Forms.Button();
      this.btnFill = new System.Windows.Forms.Button();
      this.gbConditions.SuspendLayout();
      this.gbStyle.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(308, 256);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(388, 256);
      // 
      // gbConditions
      // 
      this.gbConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.gbConditions.Controls.Add(this.lvConditions);
      this.gbConditions.Controls.Add(this.btnDown);
      this.gbConditions.Controls.Add(this.btnUp);
      this.gbConditions.Controls.Add(this.btnEdit);
      this.gbConditions.Controls.Add(this.btnDelete);
      this.gbConditions.Controls.Add(this.btnAdd);
      this.gbConditions.Location = new System.Drawing.Point(8, 4);
      this.gbConditions.Name = "gbConditions";
      this.gbConditions.Size = new System.Drawing.Size(281, 241);
      this.gbConditions.TabIndex = 1;
      this.gbConditions.TabStop = false;
      this.gbConditions.Text = "Conditions";
      // 
      // lvConditions
      // 
      this.lvConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lvConditions.HideSelection = false;
      this.lvConditions.Location = new System.Drawing.Point(12, 20);
      this.lvConditions.Name = "lvConditions";
      this.lvConditions.Size = new System.Drawing.Size(171, 210);
      this.lvConditions.TabIndex = 6;
      this.lvConditions.UseCompatibleStateImageBehavior = false;
      this.lvConditions.View = System.Windows.Forms.View.List;
      this.lvConditions.SelectedIndexChanged += new System.EventHandler(this.lvConditions_SelectedIndexChanged);
      // 
      // btnDown
      // 
      this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDown.Location = new System.Drawing.Point(193, 205);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(24, 23);
      this.btnDown.TabIndex = 5;
      this.btnDown.UseVisualStyleBackColor = true;
      this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
      // 
      // btnUp
      // 
      this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnUp.Location = new System.Drawing.Point(193, 177);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(24, 23);
      this.btnUp.TabIndex = 4;
      this.btnUp.UseVisualStyleBackColor = true;
      this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
      // 
      // btnEdit
      // 
      this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnEdit.Location = new System.Drawing.Point(193, 76);
      this.btnEdit.Name = "btnEdit";
      this.btnEdit.Size = new System.Drawing.Size(75, 23);
      this.btnEdit.TabIndex = 3;
      this.btnEdit.Text = "Edit";
      this.btnEdit.UseVisualStyleBackColor = true;
      this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
      // 
      // btnDelete
      // 
      this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDelete.Location = new System.Drawing.Point(193, 48);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(75, 23);
      this.btnDelete.TabIndex = 2;
      this.btnDelete.Text = "Delete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnAdd
      // 
      this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAdd.Location = new System.Drawing.Point(193, 20);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(75, 23);
      this.btnAdd.TabIndex = 1;
      this.btnAdd.Text = "Add";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // gbStyle
      // 
      this.gbStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.gbStyle.Controls.Add(this.cbVisible);
      this.gbStyle.Controls.Add(this.cbApplyFont);
      this.gbStyle.Controls.Add(this.cbApplyTextFill);
      this.gbStyle.Controls.Add(this.cbApplyBorder);
      this.gbStyle.Controls.Add(this.cbApplyFill);
      this.gbStyle.Controls.Add(this.pnSample);
      this.gbStyle.Controls.Add(this.btnFont);
      this.gbStyle.Controls.Add(this.btnTextColor);
      this.gbStyle.Controls.Add(this.btnBorder);
      this.gbStyle.Controls.Add(this.btnFill);
      this.gbStyle.Location = new System.Drawing.Point(297, 4);
      this.gbStyle.Name = "gbStyle";
      this.gbStyle.Size = new System.Drawing.Size(167, 241);
      this.gbStyle.TabIndex = 2;
      this.gbStyle.TabStop = false;
      this.gbStyle.Text = "Style";
      // 
      // cbVisible
      // 
      this.cbVisible.AutoSize = true;
      this.cbVisible.Location = new System.Drawing.Point(12, 136);
      this.cbVisible.Name = "cbVisible";
      this.cbVisible.Size = new System.Drawing.Size(55, 17);
      this.cbVisible.TabIndex = 5;
      this.cbVisible.Text = "Visible";
      this.cbVisible.UseVisualStyleBackColor = true;
      this.cbVisible.CheckedChanged += new System.EventHandler(this.cbVisible_CheckedChanged);
      // 
      // cbApplyFont
      // 
      this.cbApplyFont.AutoSize = true;
      this.cbApplyFont.Location = new System.Drawing.Point(12, 108);
      this.cbApplyFont.Name = "cbApplyFont";
      this.cbApplyFont.Size = new System.Drawing.Size(15, 14);
      this.cbApplyFont.TabIndex = 4;
      this.cbApplyFont.UseVisualStyleBackColor = true;
      this.cbApplyFont.CheckedChanged += new System.EventHandler(this.cbApplyFont_CheckedChanged);
      // 
      // cbApplyTextFill
      // 
      this.cbApplyTextFill.AutoSize = true;
      this.cbApplyTextFill.Location = new System.Drawing.Point(12, 80);
      this.cbApplyTextFill.Name = "cbApplyTextFill";
      this.cbApplyTextFill.Size = new System.Drawing.Size(15, 14);
      this.cbApplyTextFill.TabIndex = 4;
      this.cbApplyTextFill.UseVisualStyleBackColor = true;
      this.cbApplyTextFill.CheckedChanged += new System.EventHandler(this.cbApplyTextFill_CheckedChanged);
      // 
      // cbApplyBorder
      // 
      this.cbApplyBorder.AutoSize = true;
      this.cbApplyBorder.Location = new System.Drawing.Point(12, 24);
      this.cbApplyBorder.Name = "cbApplyBorder";
      this.cbApplyBorder.Size = new System.Drawing.Size(15, 14);
      this.cbApplyBorder.TabIndex = 4;
      this.cbApplyBorder.UseVisualStyleBackColor = true;
      this.cbApplyBorder.CheckedChanged += new System.EventHandler(this.cbApplyBorder_CheckedChanged);
      // 
      // cbApplyFill
      // 
      this.cbApplyFill.AutoSize = true;
      this.cbApplyFill.Location = new System.Drawing.Point(12, 52);
      this.cbApplyFill.Name = "cbApplyFill";
      this.cbApplyFill.Size = new System.Drawing.Size(15, 14);
      this.cbApplyFill.TabIndex = 4;
      this.cbApplyFill.UseVisualStyleBackColor = true;
      this.cbApplyFill.CheckedChanged += new System.EventHandler(this.cbApplyFill_CheckedChanged);
      // 
      // pnSample
      // 
      this.pnSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.pnSample.Location = new System.Drawing.Point(12, 161);
      this.pnSample.Name = "pnSample";
      this.pnSample.Size = new System.Drawing.Size(144, 68);
      this.pnSample.TabIndex = 3;
      this.pnSample.Paint += new System.Windows.Forms.PaintEventHandler(this.pnSample_Paint);
      // 
      // btnFont
      // 
      this.btnFont.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFont.Location = new System.Drawing.Point(36, 104);
      this.btnFont.Name = "btnFont";
      this.btnFont.Size = new System.Drawing.Size(120, 23);
      this.btnFont.TabIndex = 2;
      this.btnFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFont.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnFont.UseVisualStyleBackColor = true;
      this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
      // 
      // btnTextColor
      // 
      this.btnTextColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnTextColor.Location = new System.Drawing.Point(36, 76);
      this.btnTextColor.Name = "btnTextColor";
      this.btnTextColor.Size = new System.Drawing.Size(120, 23);
      this.btnTextColor.TabIndex = 2;
      this.btnTextColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnTextColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnTextColor.UseVisualStyleBackColor = true;
      this.btnTextColor.Click += new System.EventHandler(this.btnTextColor_Click);
      // 
      // btnBorder
      // 
      this.btnBorder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnBorder.Location = new System.Drawing.Point(36, 20);
      this.btnBorder.Name = "btnBorder";
      this.btnBorder.Size = new System.Drawing.Size(120, 23);
      this.btnBorder.TabIndex = 0;
      this.btnBorder.Text = "Border";
      this.btnBorder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnBorder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnBorder.UseVisualStyleBackColor = true;
      this.btnBorder.Click += new System.EventHandler(this.btnBorder_Click);
      // 
      // btnFill
      // 
      this.btnFill.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFill.Location = new System.Drawing.Point(36, 48);
      this.btnFill.Name = "btnFill";
      this.btnFill.Size = new System.Drawing.Size(120, 23);
      this.btnFill.TabIndex = 0;
      this.btnFill.Text = "Fill";
      this.btnFill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnFill.UseVisualStyleBackColor = true;
      this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
      // 
      // HighlightEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.ClientSize = new System.Drawing.Size(472, 288);
      this.Controls.Add(this.gbConditions);
      this.Controls.Add(this.gbStyle);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
      this.MinimumSize = new System.Drawing.Size(480, 322);
      this.Name = "HighlightEditorForm";
      this.ShowIcon = false;
      this.Text = "Highlight Conditions";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HighlightEditorForm_FormClosed);
      this.Controls.SetChildIndex(this.gbStyle, 0);
      this.Controls.SetChildIndex(this.gbConditions, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.gbConditions.ResumeLayout(false);
      this.gbStyle.ResumeLayout(false);
      this.gbStyle.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbConditions;
    private System.Windows.Forms.GroupBox gbStyle;
    private System.Windows.Forms.Button btnFill;
    private System.Windows.Forms.Panel pnSample;
    private System.Windows.Forms.Button btnTextColor;
    private System.Windows.Forms.Button btnDown;
    private System.Windows.Forms.Button btnUp;
    private System.Windows.Forms.Button btnEdit;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.ListView lvConditions;
    private System.Windows.Forms.CheckBox cbApplyFill;
    private System.Windows.Forms.Button btnFont;
    private System.Windows.Forms.CheckBox cbApplyFont;
    private System.Windows.Forms.CheckBox cbApplyTextFill;
    private System.Windows.Forms.CheckBox cbVisible;
    private System.Windows.Forms.CheckBox cbApplyBorder;
    private System.Windows.Forms.Button btnBorder;
  }
}
