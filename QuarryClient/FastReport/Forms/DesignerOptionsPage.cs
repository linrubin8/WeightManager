using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace FastReport.Forms
{
  /// <summary>
  /// The base class for designer plugin's options page.
  /// </summary>
  /// <remarks>
  /// Use this class if you develop a designer plugin that may be configured in the 
  /// "View|Options..." menu. You need to implement an options page for your
  /// plugin and return it in the <b>IDesignerPlugin.GetOptionsPage</b> method.
  /// </remarks>
  public class DesignerOptionsPage : Form
  {
    private Label lblWarn;
    /// <summary>
    /// The <b>TabControl</b> control.
    /// </summary>
    public TabControl tc1;
    
    /// <summary>
    /// The <b>TabPage</b> control.
    /// </summary>
    public TabPage tab1;
  
    private void InitializeComponent()
    {
      this.tc1 = new System.Windows.Forms.TabControl();
      this.tab1 = new System.Windows.Forms.TabPage();
      this.lblWarn = new System.Windows.Forms.Label();
      this.tc1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tc1
      // 
      this.tc1.Controls.Add(this.tab1);
      this.tc1.Location = new System.Drawing.Point(12, 12);
      this.tc1.Name = "tc1";
      this.tc1.SelectedIndex = 0;
      this.tc1.Size = new System.Drawing.Size(376, 276);
      this.tc1.TabIndex = 0;
      // 
      // tab1
      // 
      this.tab1.Location = new System.Drawing.Point(4, 22);
      this.tab1.Name = "tab1";
      this.tab1.Padding = new System.Windows.Forms.Padding(3);
      this.tab1.Size = new System.Drawing.Size(368, 250);
      this.tab1.TabIndex = 0;
      this.tab1.Text = "tabPage1";
      this.tab1.UseVisualStyleBackColor = true;
      // 
      // lblWarn
      // 
      this.lblWarn.AutoSize = true;
      this.lblWarn.Location = new System.Drawing.Point(8, 300);
      this.lblWarn.Name = "lblWarn";
      this.lblWarn.Size = new System.Drawing.Size(328, 13);
      this.lblWarn.TabIndex = 1;
      this.lblWarn.Text = "Place your controls on tab page only! Add new pages if necessary.";
      // 
      // DesignerOptionsPage
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(398, 323);
      this.Controls.Add(this.lblWarn);
      this.Controls.Add(this.tc1);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.Name = "DesignerOptionsPage";
      this.tc1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    /// <summary>
    /// Initializes controls on this options page.
    /// </summary>
    /// <remarks>
    /// Override this method to fill options page's controls with initial values.
    /// </remarks>
    public virtual void Init()
    {
    }

    /// <summary>
    /// Finalizes the options page.
    /// </summary>
    /// <param name="result">The dialog result.</param>
    /// <remarks>
    /// Override this method to pass controls' values to the plugin. Do this if <b>result</b> is
    /// <b>DialogResult.OK</b>.
    /// </remarks>
    public virtual void Done(DialogResult result)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <b>DesignerOptionsPage</b> class with default settings.
    /// </summary>
    /// <remarks>
    /// Usually you need to define another contructor which takes one parameter - the plugin.
    /// </remarks>
    /// <example>This example shows how to define own constructor which takes a plugin:
    /// <code>
    /// public DialogPageOptions(DialogPageDesigner pd) : base()
    /// {
    ///   FPageDesigner = pd;
    ///   InitializeComponent();
    /// }
    /// </code>
    /// </example>
    public DesignerOptionsPage()
    {
      InitializeComponent();
    }
  }

}
