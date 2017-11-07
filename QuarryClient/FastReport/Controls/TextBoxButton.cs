using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;

namespace FastReport.Controls
{
  /// <summary>
  /// Represents the control that combines a textbox and a button.
  /// </summary>
  [ToolboxItem(false)]
  public class TextBoxButton : UserControl
  {
    private TextBox FTextBox;
    private Button FButton;
    private int FTextBoxHeight;
    
    /// <summary>
    /// Occurs when the textbox is changed.
    /// </summary>
    public event EventHandler TextBoxChanged;
    
    /// <summary>
    /// Occurs when the button is clicked.
    /// </summary>
    public event EventHandler ButtonClick;

    /// <inheritdoc/>
    public override string Text
    {
      get { return FTextBox.Text; }
      set { FTextBox.Text = value; }
    }

    /// <summary>
    /// Gets or sets the button's image.
    /// </summary>
    public Image Image
    {
      get { return FButton.Image; }
      set { FButton.Image = value; }
    }


    private void FTextBox_TextChanged(object sender, EventArgs e)
    {
      if (TextBoxChanged != null)
        TextBoxChanged(this, EventArgs.Empty);
    }

    private void FButton_Click(object sender, EventArgs e)
    {
      if (ButtonClick != null)
        ButtonClick(this, EventArgs.Empty);
    }

    /// <inheritdoc/>
    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      g.FillRectangle(Enabled ? SystemBrushes.Window : SystemBrushes.Control, DisplayRectangle);
      if (Enabled)
        ControlPaint.DrawVisualStyleBorder(g, new Rectangle(0, 0, Width - 1, Height - 1));
      else
        g.DrawRectangle(SystemPens.InactiveBorder, new Rectangle(0, 0, Width - 1, Height - 1));
    }

    private void LayoutControls()
    {
      if (FTextBox != null)
      {
        FTextBox.Location = new Point(3, 3);
        FTextBox.Width = Width - Height - 5;
      }
      if (FButton != null)
      {
        FButton.Location = new Point(Width - Height + 1, 1);
        FButton.Size = new Size(Height - 2, Height - 2);
      }
    }

    /// <inheritdoc/>
    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
      height = FTextBoxHeight + 1;
      base.SetBoundsCore(x, y, width, height, specified);
      LayoutControls();
    }

    /// <inheritdoc/>
    protected override void OnLayout(LayoutEventArgs e)
    {
      base.OnLayout(e);
      LayoutControls();
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="TextBoxButton"/> class.
    /// </summary>
    public TextBoxButton()
    {
      FTextBox = new TextBox();
      FTextBox.BorderStyle = BorderStyle.None;
      FTextBoxHeight = FTextBox.Height;
      FTextBox.TextChanged += new EventHandler(FTextBox_TextChanged);
      Controls.Add(FTextBox);
      FButton = new Button();
      FButton.FlatStyle = FlatStyle.Flat;
      FButton.FlatAppearance.BorderSize = 0;
      FButton.Click += new EventHandler(FButton_Click);
      Controls.Add(FButton);
      SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      OnResize(EventArgs.Empty);
    }
  }
}
