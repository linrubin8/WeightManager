namespace FastReport.Forms
{
  partial class StyleEditorForm
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
      this.gbSettings = new System.Windows.Forms.GroupBox();
      this.pnSample = new System.Windows.Forms.Panel();
      this.btnTextColor = new System.Windows.Forms.Button();
      this.btnFont = new System.Windows.Forms.Button();
      this.btnFill = new System.Windows.Forms.Button();
      this.btnBorder = new System.Windows.Forms.Button();
      this.gbStyles = new System.Windows.Forms.GroupBox();
      this.btnDown = new System.Windows.Forms.Button();
      this.btnUp = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnEdit = new System.Windows.Forms.Button();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnAdd = new System.Windows.Forms.Button();
      this.lvStyles = new System.Windows.Forms.ListView();
      this.cbApplyBorder = new System.Windows.Forms.CheckBox();
      this.cbApplyFill = new System.Windows.Forms.CheckBox();
      this.cbApplyFont = new System.Windows.Forms.CheckBox();
      this.cbApplyTextFill = new System.Windows.Forms.CheckBox();
      this.gbSettings.SuspendLayout();
      this.gbStyles.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(312, 264);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(392, 264);
      // 
      // gbSettings
      // 
      this.gbSettings.Controls.Add(this.cbApplyTextFill);
      this.gbSettings.Controls.Add(this.cbApplyFont);
      this.gbSettings.Controls.Add(this.cbApplyFill);
      this.gbSettings.Controls.Add(this.cbApplyBorder);
      this.gbSettings.Controls.Add(this.pnSample);
      this.gbSettings.Controls.Add(this.btnTextColor);
      this.gbSettings.Controls.Add(this.btnFont);
      this.gbSettings.Controls.Add(this.btnFill);
      this.gbSettings.Controls.Add(this.btnBorder);
      this.gbSettings.Location = new System.Drawing.Point(300, 4);
      this.gbSettings.Name = "gbSettings";
      this.gbSettings.Size = new System.Drawing.Size(168, 250);
      this.gbSettings.TabIndex = 3;
      this.gbSettings.TabStop = false;
      this.gbSettings.Text = "Style settings";
      // 
      // pnSample
      // 
      this.pnSample.BackColor = System.Drawing.SystemColors.Control;
      this.pnSample.Location = new System.Drawing.Point(12, 168);
      this.pnSample.Name = "pnSample";
      this.pnSample.Size = new System.Drawing.Size(144, 68);
      this.pnSample.TabIndex = 0;
      this.pnSample.Paint += new System.Windows.Forms.PaintEventHandler(this.pnSample_Paint);
      // 
      // btnTextColor
      // 
      this.btnTextColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnTextColor.Location = new System.Drawing.Point(36, 104);
      this.btnTextColor.Name = "btnTextColor";
      this.btnTextColor.Size = new System.Drawing.Size(120, 23);
      this.btnTextColor.TabIndex = 3;
      this.btnTextColor.Text = "Text color";
      this.btnTextColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnTextColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnTextColor.UseVisualStyleBackColor = true;
      this.btnTextColor.Click += new System.EventHandler(this.btnTextColor_Click);
      // 
      // btnFont
      // 
      this.btnFont.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFont.Location = new System.Drawing.Point(36, 76);
      this.btnFont.Name = "btnFont";
      this.btnFont.Size = new System.Drawing.Size(120, 23);
      this.btnFont.TabIndex = 2;
      this.btnFont.Text = "Font";
      this.btnFont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFont.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnFont.UseVisualStyleBackColor = true;
      this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
      // 
      // btnFill
      // 
      this.btnFill.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFill.Location = new System.Drawing.Point(36, 48);
      this.btnFill.Name = "btnFill";
      this.btnFill.Size = new System.Drawing.Size(120, 23);
      this.btnFill.TabIndex = 1;
      this.btnFill.Text = "Fill";
      this.btnFill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnFill.UseVisualStyleBackColor = true;
      this.btnFill.Click += new System.EventHandler(this.btnColor_Click);
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
      // gbStyles
      // 
      this.gbStyles.Controls.Add(this.btnDown);
      this.gbStyles.Controls.Add(this.btnUp);
      this.gbStyles.Controls.Add(this.btnSave);
      this.gbStyles.Controls.Add(this.btnLoad);
      this.gbStyles.Controls.Add(this.btnEdit);
      this.gbStyles.Controls.Add(this.btnDelete);
      this.gbStyles.Controls.Add(this.btnAdd);
      this.gbStyles.Controls.Add(this.lvStyles);
      this.gbStyles.Location = new System.Drawing.Point(8, 4);
      this.gbStyles.Name = "gbStyles";
      this.gbStyles.Size = new System.Drawing.Size(284, 250);
      this.gbStyles.TabIndex = 5;
      this.gbStyles.TabStop = false;
      this.gbStyles.Text = "Styles";
      // 
      // btnDown
      // 
      this.btnDown.Location = new System.Drawing.Point(180, 144);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(23, 23);
      this.btnDown.TabIndex = 14;
      this.btnDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnDown.UseVisualStyleBackColor = true;
      this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
      // 
      // btnUp
      // 
      this.btnUp.Location = new System.Drawing.Point(180, 116);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(23, 23);
      this.btnUp.TabIndex = 13;
      this.btnUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnUp.UseVisualStyleBackColor = true;
      this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
      // 
      // btnSave
      // 
      this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnSave.Location = new System.Drawing.Point(180, 213);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(92, 23);
      this.btnSave.TabIndex = 12;
      this.btnSave.Text = "Save...";
      this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnLoad
      // 
      this.btnLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnLoad.Location = new System.Drawing.Point(180, 184);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(92, 23);
      this.btnLoad.TabIndex = 11;
      this.btnLoad.Text = "Load...";
      this.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // btnEdit
      // 
      this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnEdit.Location = new System.Drawing.Point(180, 76);
      this.btnEdit.Name = "btnEdit";
      this.btnEdit.Size = new System.Drawing.Size(92, 23);
      this.btnEdit.TabIndex = 10;
      this.btnEdit.Text = "Edit";
      this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnEdit.UseVisualStyleBackColor = true;
      this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
      // 
      // btnDelete
      // 
      this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnDelete.Location = new System.Drawing.Point(180, 48);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(92, 23);
      this.btnDelete.TabIndex = 9;
      this.btnDelete.Text = "Delete";
      this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnAdd
      // 
      this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnAdd.Location = new System.Drawing.Point(180, 20);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(92, 23);
      this.btnAdd.TabIndex = 8;
      this.btnAdd.Text = "Add";
      this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // lvStyles
      // 
      this.lvStyles.HideSelection = false;
      this.lvStyles.LabelEdit = true;
      this.lvStyles.Location = new System.Drawing.Point(12, 20);
      this.lvStyles.Name = "lvStyles";
      this.lvStyles.Size = new System.Drawing.Size(157, 216);
      this.lvStyles.TabIndex = 0;
      this.lvStyles.UseCompatibleStateImageBehavior = false;
      this.lvStyles.View = System.Windows.Forms.View.List;
      this.lvStyles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvStyles_AfterLabelEdit);
      this.lvStyles.SelectedIndexChanged += new System.EventHandler(this.lvStyles_SelectedIndexChanged);
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
      // cbApplyFont
      // 
      this.cbApplyFont.AutoSize = true;
      this.cbApplyFont.Location = new System.Drawing.Point(12, 80);
      this.cbApplyFont.Name = "cbApplyFont";
      this.cbApplyFont.Size = new System.Drawing.Size(15, 14);
      this.cbApplyFont.TabIndex = 4;
      this.cbApplyFont.UseVisualStyleBackColor = true;
      this.cbApplyFont.CheckedChanged += new System.EventHandler(this.cbApplyFont_CheckedChanged);
      // 
      // cbApplyTextFill
      // 
      this.cbApplyTextFill.AutoSize = true;
      this.cbApplyTextFill.Location = new System.Drawing.Point(12, 108);
      this.cbApplyTextFill.Name = "cbApplyTextFill";
      this.cbApplyTextFill.Size = new System.Drawing.Size(15, 14);
      this.cbApplyTextFill.TabIndex = 4;
      this.cbApplyTextFill.UseVisualStyleBackColor = true;
      this.cbApplyTextFill.CheckedChanged += new System.EventHandler(this.cbApplyTextFill_CheckedChanged);
      // 
      // StyleEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(476, 295);
      this.Controls.Add(this.gbStyles);
      this.Controls.Add(this.gbSettings);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.Name = "StyleEditorForm";
      this.Text = "StyleEditorForm";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StyleEditorForm_FormClosed);
      this.Controls.SetChildIndex(this.btnOk, 0);
      this.Controls.SetChildIndex(this.gbSettings, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.Controls.SetChildIndex(this.gbStyles, 0);
      this.gbSettings.ResumeLayout(false);
      this.gbSettings.PerformLayout();
      this.gbStyles.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbSettings;
    private System.Windows.Forms.Panel pnSample;
    private System.Windows.Forms.Button btnTextColor;
    private System.Windows.Forms.Button btnFont;
    private System.Windows.Forms.Button btnFill;
    private System.Windows.Forms.Button btnBorder;
    private System.Windows.Forms.GroupBox gbStyles;
    private System.Windows.Forms.ListView lvStyles;
    private System.Windows.Forms.Button btnDown;
    private System.Windows.Forms.Button btnUp;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Button btnEdit;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.CheckBox cbApplyFont;
    private System.Windows.Forms.CheckBox cbApplyFill;
    private System.Windows.Forms.CheckBox cbApplyBorder;
    private System.Windows.Forms.CheckBox cbApplyTextFill;
  }
}