using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;
using System.ComponentModel;
using FastReport.Data;

namespace FastReport.Controls
{
  internal class ParametersComboBox : UserControl
  {
    private TextBox FTextBox;
    private Button FComboButton;
    private int FTextBoxHeight;
    private Report FReport;
    private DataColumnDropDown FDropDown;
    private Timer FTimer;
    private int FClosedTicks;

    public new event EventHandler TextChanged;
    
    public event EventHandler DropDownOpening;

    public override string Text
    {
      get { return FTextBox.Text; }
      set { FTextBox.Text = value; }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Report Report
    {
      get { return FReport; }
      set
      {
        FReport = value;
        FDropDown.CreateNodes(value);
      }
    }

    private void FTextBox_TextChanged(object sender, EventArgs e)
    {
      if (TextChanged != null)
        TextChanged(this, EventArgs.Empty);
    }

    private void FDropDown_ColumnSelected(object sender, EventArgs e)
    {
      Text = String.IsNullOrEmpty(FDropDown.Column) ? "" : FDropDown.Column;
      FTimer.Start();
    }

    private void FDropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
      FClosedTicks = Environment.TickCount;
    }

    private void FComboButton_Click(object sender, EventArgs e)
    {
      if (Math.Abs(Environment.TickCount - FClosedTicks) < 100)
        return;

      if (DropDownOpening != null)
        DropDownOpening(this, EventArgs.Empty);
        
      FDropDown.Column = Text;
      FDropDown.SetSize(Width, 250);
      FDropDown.Show(this, new Point(0, Height));
    }

    private void FTimer_Tick(object sender, EventArgs e)
    {
      FindForm().BringToFront();
      FTextBox.Focus();
      FTextBox.Select(Text.Length, 0);
      FTimer.Stop();
    }

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
        FTextBox.Width = Width - Height - 3;
      }
      if (FComboButton != null)
      {
        FComboButton.Location = new Point(Width - Height + 1, 1);
        FComboButton.Size = new Size(Height - 2, Height - 2);
      }
    }

    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
      height = FTextBoxHeight + 1;
      base.SetBoundsCore(x, y, width, height, specified);
      LayoutControls();
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
      base.OnLayout(e);
      LayoutControls();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (FTimer != null)
          FTimer.Dispose();
        FTimer = null;
      }
      base.Dispose(disposing);
    }

    public ParametersComboBox()
    {
      FTextBox = new TextBox();
      FTextBox.BorderStyle = BorderStyle.None;
      FTextBoxHeight = FTextBox.Height;
      FTextBox.TextChanged += new EventHandler(FTextBox_TextChanged);
      Controls.Add(FTextBox);

      FComboButton = new Button();
      FComboButton.FlatStyle = FlatStyle.Flat;
      FComboButton.FlatAppearance.BorderSize = 0;
      FComboButton.Image = Res.GetImage(182);
      FComboButton.Click += new EventHandler(FComboButton_Click);
      Controls.Add(FComboButton);

      FDropDown = new DataColumnDropDown();
      FDropDown.DataTree.ShowColumns = false;
      FDropDown.DataTree.ShowDataSources = false;
      FDropDown.DataTree.ShowRelations = false;
      FDropDown.DataTree.ShowParameters = true;
      FDropDown.ColumnSelected += new EventHandler(FDropDown_ColumnSelected);
      FDropDown.Closed += new ToolStripDropDownClosedEventHandler(FDropDown_Closed);

      FTimer = new Timer();
      FTimer.Interval = 50;
      FTimer.Tick += new EventHandler(FTimer_Tick);

      SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      OnResize(EventArgs.Empty);
    }
  }
}
