using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Data;

namespace FastReport.Dialog
{
  /// <summary>
  /// Displays a ListBox in which a check box is displayed to the left of each item.
  /// Wraps the <see cref="System.Windows.Forms.CheckedListBox"/> control.
  /// </summary>
  public class CheckedListBoxControl : ListBoxBaseControl
  {
    private CheckedListBox FCheckedListBox;
    private string FItemCheckEvent;
    private Timer FTimer;

    #region Properties
    /// <summary>
    /// Occurs after item's check state was changed.
    /// Wraps the <see cref="System.Windows.Forms.CheckedListBox.ItemCheck"/> event.
    /// </summary>
    public event ItemCheckEventHandler ItemCheck;

    /// <summary>
    /// Gets an internal <b>CheckedListBox</b>.
    /// </summary>
    [Browsable(false)]
    public CheckedListBox CheckedListBox
    {
      get { return FCheckedListBox; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the check box should be toggled when an item is selected.
    /// Wraps the <see cref="System.Windows.Forms.CheckedListBox.CheckOnClick"/> property.
    /// </summary>
    [DefaultValue(false)]
    public bool CheckOnClick
    {
      get { return CheckedListBox.CheckOnClick; }
      set { CheckedListBox.CheckOnClick = value; }
    }

    /// <summary>
    /// Collection of checked indexes in this CheckedListBox.
    /// Wraps the <see cref="System.Windows.Forms.CheckedListBox.CheckedIndices"/> property.
    /// </summary>
    [Browsable(false)]
    public System.Windows.Forms.CheckedListBox.CheckedIndexCollection CheckedIndices
    {
      get { return CheckedListBox.CheckedIndices; }
    }

    /// <summary>
    /// Collection of checked items in this CheckedListBox.
    /// Wraps the <see cref="System.Windows.Forms.CheckedListBox.CheckedItems"/> property.
    /// </summary>
    [Browsable(false)]
    public CheckedListBox.CheckedItemCollection CheckedItems
    {
      get { return CheckedListBox.CheckedItems; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="ItemCheck"/> event.
    /// </summary>
    [Category("Events")]
    public string ItemCheckEvent
    {
      get { return FItemCheckEvent; }
      set { FItemCheckEvent = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override DrawMode DrawMode
    {
      get { return base.DrawMode; }
      set { base.DrawMode = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override int ItemHeight
    {
      get { return base.ItemHeight; }
      set { base.ItemHeight = value; }
    }
    #endregion

    #region Private Methods
    private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      OnItemCheck(e);
    }

    private void FTimer_Tick(object sender, EventArgs e)
    {
      FTimer.Stop();
      OnFilterChanged();
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      FTimer.Dispose();
    }
    
    /// <inheritdoc/>
    protected override void AttachEvents()
    {
      base.AttachEvents();
      CheckedListBox.ItemCheck += new ItemCheckEventHandler(CheckedListBox_ItemCheck);
    }

    /// <inheritdoc/>
    protected override void DetachEvents()
    {
      base.DetachEvents();
      CheckedListBox.ItemCheck -= new ItemCheckEventHandler(CheckedListBox_ItemCheck);
    }

    /// <inheritdoc/>
    protected override object GetValue()
    {
      List<string> list = new List<string>();
      foreach (object item in CheckedItems)
      {
        list.Add((string)item);
      }
      return list.ToArray();
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      CheckedListBoxControl c = writer.DiffObject as CheckedListBoxControl;
      base.Serialize(writer);
      
      if (CheckOnClick != c.CheckOnClick)
        writer.WriteBool("CheckOnClick", CheckOnClick);
      if (ItemCheckEvent != c.ItemCheckEvent)
        writer.WriteStr("ItemCheckEvent", ItemCheckEvent);
    }

    /// <summary>
    /// This method fires the <b>ItemCheck</b> event and the script code connected to the <b>ItemCheckEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnItemCheck(ItemCheckEventArgs e)
    {
      if (ItemCheck != null)
        ItemCheck(this, e);
      InvokeEvent(ItemCheckEvent, e);
      FTimer.Start();
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>CheckedListBoxControl</b> class with default settings. 
    /// </summary>
    public CheckedListBoxControl()
    {
      FCheckedListBox = new CheckedListBox();
      Control = FCheckedListBox;
      CheckedListBox.IntegralHeight = false;
      FTimer = new Timer();
      FTimer.Interval = 50;
      FTimer.Tick += new EventHandler(FTimer_Tick);
    }
  }
}
