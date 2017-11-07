using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using System.Data;
using FastReport.Utils;
using FastReport.TypeConverters;
using FastReport.TypeEditors;
using FastReport.Data;

namespace FastReport.Dialog
{
  /// <summary>
  /// Represents the control with two lists (available items and selected items).
  /// </summary>
  /// <remarks>
  /// The control allows to select one or several items and then filter the datasource which it is connected to.
  /// All you need is to setup the <b>DataColumn</b> property.
  /// </remarks>
  public class DataSelectorControl : DataFilterBaseControl
  {
    private Panel pnPanel;
    private ListBox lvAvailableItems;
    private ListBox lvSelectedItems;
    private Button btnAddItem;
    private Button btnAddItems;
    private Button btnRemoveItem;
    private Button btnRemoveItems;

    #region Properties
    /// <summary>
    /// Gets or sets a value indicating that the items must be sorted.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool Sorted
    {
      get { return lvAvailableItems.Sorted; }
      set 
      { 
        lvAvailableItems.Sorted = value;
        lvSelectedItems.Sorted = value;
      }
    }

    /// <inheritdoc/>
    public override float Width
    {
      get { return base.Width; }
      set
      {
        base.Width = value;
        UpdateLayout();
      }
    }

    /// <inheritdoc/>
    public override float Height
    {
      get { return base.Height; }
      set
      {
        base.Height = value;
        UpdateLayout();
      }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool AutoFill
    {
      get { return base.AutoFill; }
      set { base.AutoFill = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool AutoFilter
    {
      get { return base.AutoFilter; }
      set { base.AutoFilter = value; }
    }
    #endregion
    
    #region Private Methods
    private void UpdateLayout()
    {
      lvAvailableItems.Size = new Size(pnPanel.Width / 2 - 28, pnPanel.Height);
      
      lvSelectedItems.Size = lvAvailableItems.Size;
      lvSelectedItems.Left = pnPanel.Width - lvSelectedItems.Width;
      
      btnAddItem.Size = new Size(32, 24);
      btnAddItem.Location = new Point(lvSelectedItems.Left - 44, 0);

      btnAddItems.Size = btnAddItem.Size;
      btnAddItems.Location = new Point(btnAddItem.Left, btnAddItem.Height + 1);

      btnRemoveItem.Size = btnAddItem.Size;
      btnRemoveItem.Location = new Point(btnAddItem.Left, btnAddItem.Height * 3);

      btnRemoveItems.Size = btnAddItem.Size;
      btnRemoveItems.Location = new Point(btnAddItem.Left, btnRemoveItem.Bottom + 1);
    }

    private void UpdateButtons()
    {
      btnAddItem.Enabled = lvAvailableItems.SelectedItems.Count > 0;
      btnAddItems.Enabled = lvAvailableItems.Items.Count > 0;
      btnRemoveItem.Enabled = lvSelectedItems.SelectedItems.Count > 0;
      btnRemoveItems.Enabled = lvSelectedItems.Items.Count > 0;
    }

    private void pnPanel_Resize(object sender, EventArgs e)
    {
      UpdateLayout();
    }

    private void btnAddItem_Click(object sender, EventArgs e)
    {
      int index = 0;
      while (lvAvailableItems.SelectedItems.Count > 0)
      {
        object item = lvAvailableItems.SelectedItems[0];
        index = lvAvailableItems.Items.IndexOf(item);
        lvAvailableItems.Items.Remove(item);
        lvSelectedItems.Items.Add(item);
      }
      if (index >= lvAvailableItems.Items.Count)
        index = lvAvailableItems.Items.Count - 1;
      if (index < 0)
        index = 0;
      if (index < lvAvailableItems.Items.Count)
        lvAvailableItems.SelectedIndex = index;

      OnFilterChanged();
      UpdateButtons();
    }

    private void btnAddItems_Click(object sender, EventArgs e)
    {
      while (lvAvailableItems.Items.Count > 0)
      {
        object item = lvAvailableItems.Items[0];
        lvAvailableItems.Items.Remove(item);
        lvSelectedItems.Items.Add(item);
      }

      OnFilterChanged();
      UpdateButtons();
    }

    private void btnRemoveItem_Click(object sender, EventArgs e)
    {
      int index = 0;
      while (lvSelectedItems.SelectedItems.Count > 0)
      {
        object item = lvSelectedItems.SelectedItems[0];
        index = lvSelectedItems.Items.IndexOf(item);
        lvSelectedItems.Items.Remove(item);
        lvAvailableItems.Items.Add(item);
      }
      if (index >= lvSelectedItems.Items.Count)
        index = lvSelectedItems.Items.Count - 1;
      if (index < 0)
        index = 0;
      if (index < lvSelectedItems.Items.Count)
        lvSelectedItems.SelectedIndex = index;

      OnFilterChanged();
      UpdateButtons();
    }

    private void btnRemoveItems_Click(object sender, EventArgs e)
    {
      while (lvSelectedItems.Items.Count > 0)
      {
        object item = lvSelectedItems.Items[0];
        lvSelectedItems.Items.Remove(item);
        lvAvailableItems.Items.Add(item);
      }

      OnFilterChanged();
      UpdateButtons();
    }

    private void lvSelectedItems_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateButtons();
    }

    private void lvAvailableItems_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateButtons();
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override void FillData(DataSourceBase dataSource, Column column)
    {
      lvAvailableItems.Items.Clear();
      lvAvailableItems.Items.AddRange(GetListOfData(dataSource, column));

      if (lvAvailableItems.Items.Count > 0)
        lvAvailableItems.SelectedIndex = 0;
      UpdateButtons();
    }

    /// <inheritdoc/>
    protected override object GetValue()
    {
      List<string> list = new List<string>();
      foreach (object item in lvSelectedItems.Items)
      {
        list.Add((string)item);
      }
      return list.ToArray();
    }
    #endregion
    
    #region Public Methods
    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      base.Draw(e);
      if (Control.Width > 0 && Control.Height > 0)
      {
        foreach (Control c in pnPanel.Controls)
        {
          using (Bitmap bmp = DrawUtils.DrawToBitmap(c, true))
          {
            e.Graphics.DrawImage(bmp, (int)AbsLeft + c.Left, (int)AbsTop + c.Top);
          }
        }
      }
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      DataSelectorControl c = writer.DiffObject as DataSelectorControl;
      base.Serialize(writer);

      if (Sorted != c.Sorted)
        writer.WriteBool("Sorted", Sorted);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>DataSelectorControl</b> class with default settings. 
    /// </summary>
    public DataSelectorControl()
    {
      pnPanel = new Panel();
      pnPanel.Resize += new EventHandler(pnPanel_Resize);
      
      lvAvailableItems = new ListBox();
      lvAvailableItems.Parent = pnPanel;
      lvAvailableItems.IntegralHeight = false;
      lvAvailableItems.SelectionMode = SelectionMode.MultiExtended;
      lvAvailableItems.SelectedIndexChanged += lvAvailableItems_SelectedIndexChanged;
      
      lvSelectedItems = new ListBox();
      lvSelectedItems.Parent = pnPanel;
      lvSelectedItems.IntegralHeight = false;
      lvSelectedItems.SelectionMode = SelectionMode.MultiExtended;
      lvSelectedItems.SelectedIndexChanged += lvSelectedItems_SelectedIndexChanged;
      
      btnAddItem = new Button();
      btnAddItem.Parent = pnPanel;
      btnAddItem.Text = ">";
      btnAddItem.Click += new EventHandler(btnAddItem_Click);

      btnAddItems = new Button();
      btnAddItems.Parent = pnPanel;
      btnAddItems.Text = ">>";
      btnAddItems.Click += new EventHandler(btnAddItems_Click);

      btnRemoveItem = new Button();
      btnRemoveItem.Parent = pnPanel;
      btnRemoveItem.Text = "<";
      btnRemoveItem.Click += new EventHandler(btnRemoveItem_Click);

      btnRemoveItems = new Button();
      btnRemoveItems.Parent = pnPanel;
      btnRemoveItems.Text = "<<";
      btnRemoveItems.Click += new EventHandler(btnRemoveItems_Click);

      Control = pnPanel;
      UpdateButtons();
      UpdateLayout();
    }
  }
}
