using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using FastReport.Utils;
using System.Drawing;

namespace FastReport.Dialog
{
  /// <summary>
  /// Represents a Windows list view control, which displays a collection of items that can be displayed using one of four different views.
  /// Wraps the <see cref="System.Windows.Forms.ListView"/> control.
  /// </summary>
  public class ListViewControl : DialogControl
  {
    private ListView FListView;
    private string FItemCheckedEvent;
    private string FSelectedIndexChangedEvent;
    
    #region Properties
    /// <summary>
    /// Occurs when the checked state of an item changes.
    /// Wraps the <see cref="System.Windows.Forms.ListView.ItemChecked"/> event.
    /// </summary>
    public event ItemCheckedEventHandler ItemChecked;

    /// <summary>
    /// Occurs when the index of the selected item in the list view control changes.
    /// Wraps the <see cref="System.Windows.Forms.ListView.SelectedIndexChanged"/> event.
    /// </summary>
    public event EventHandler SelectedIndexChanged;

    /// <summary>
    /// Gets an internal <b>ListView</b>.
    /// </summary>
    [Browsable(false)]
    public ListView ListView
    {
      get { return FListView; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether a check box appears next to each item in the control.
    /// Wraps the <see cref="System.Windows.Forms.ListView.CheckBoxes"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool CheckBoxes
    {
      get { return ListView.CheckBoxes; }
      set { ListView.CheckBoxes = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether multiple items can be selected.
    /// Wraps the <see cref="System.Windows.Forms.ListView.MultiSelect"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool MultiSelect
    {
      get { return ListView.MultiSelect; }
      set { ListView.MultiSelect = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether items are displayed in groups.
    /// Wraps the <see cref="System.Windows.Forms.ListView.ShowGroups"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool ShowGroups
    {
      get { return ListView.ShowGroups; }
      set { ListView.ShowGroups = value; }
    }

    /// <summary>
    /// Gets or sets how items are displayed in the control.
    /// Wraps the <see cref="System.Windows.Forms.ListView.View"/> property.
    /// </summary>
    [DefaultValue(View.LargeIcon)]
    [Category("Appearance")]
    public View View
    {
      get { return ListView.View; }
      set { ListView.View = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="ItemChecked"/> event.
    /// </summary>
    [Category("Events")]
    public string ItemCheckedEvent
    {
      get { return FItemCheckedEvent; }
      set { FItemCheckedEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="SelectedIndexChanged"/> event.
    /// </summary>
    [Category("Events")]
    public string SelectedIndexChangedEvent
    {
      get { return FSelectedIndexChangedEvent; }
      set { FSelectedIndexChangedEvent = value; }
    }

    /// <summary>
    /// Gets the indexes of the currently checked items in the control.
    /// Wraps the <see cref="System.Windows.Forms.ListView.CheckedIndices"/> property.
    /// </summary>
    [Browsable(false)]
    public ListView.CheckedIndexCollection CheckedIndices
    {
      get { return ListView.CheckedIndices; }
    }

    /// <summary>
    /// Gets the currently checked items in the control.
    /// Wraps the <see cref="System.Windows.Forms.ListView.CheckedItems"/> property.
    /// </summary>
    [Browsable(false)]
    public ListView.CheckedListViewItemCollection CheckedItems 
    {
      get { return ListView.CheckedItems; }
    }

    /// <summary>
    /// Gets the collection of all column headers that appear in the control.
    /// Wraps the <see cref="System.Windows.Forms.ListView.Columns"/> property.
    /// </summary>
    [Browsable(false)]
    public ListView.ColumnHeaderCollection Columns
    {
      get { return ListView.Columns; }
    }

    /// <summary>
    /// Gets the collection of ListViewGroup objects assigned to the control.
    /// Wraps the <see cref="System.Windows.Forms.ListView.Groups"/> property.
    /// </summary>
    [Browsable(false)]
    public ListViewGroupCollection Groups
    {
      get { return ListView.Groups; }
    }

    /// <summary>
    /// Gets a collection containing all items in the control.
    /// Wraps the <see cref="System.Windows.Forms.ListView.Items"/> property.
    /// </summary>
    [Browsable(false)]
    public ListView.ListViewItemCollection Items
    {
      get { return ListView.Items; }
    }

    /// <summary>
    /// Gets or sets the ImageList to use when displaying items as large icons in the control.
    /// Wraps the <see cref="System.Windows.Forms.ListView.LargeImageList"/> property.
    /// </summary>
    [Browsable(false)]
    public ImageList LargeImageList
    {
      get { return ListView.LargeImageList; }
      set { ListView.LargeImageList = value; }
    }

    /// <summary>
    /// Gets the indexes of the selected items in the control. 
    /// Wraps the <see cref="System.Windows.Forms.ListView.SelectedIndices"/> property.
    /// </summary>
    [Browsable(false)]
    public ListView.SelectedIndexCollection SelectedIndices
    {
      get { return ListView.SelectedIndices; }
    }

    /// <summary>
    /// Gets the items that are selected in the control.
    /// Wraps the <see cref="System.Windows.Forms.ListView.SelectedItems"/> property.
    /// </summary>
    [Browsable(false)]
    public ListView.SelectedListViewItemCollection SelectedItems
    {
      get { return ListView.SelectedItems; }
    }

    /// <summary>
    /// Gets or sets the ImageList to use when displaying items as small icons in the control.
    /// Wraps the <see cref="System.Windows.Forms.ListView.SmallImageList"/> property.
    /// </summary>
    [Browsable(false)]
    public ImageList SmallImageList
    {
      get { return ListView.SmallImageList; }
      set { ListView.SmallImageList = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public override string Text
    {
      get { return base.Text; }
      set { base.Text = value; }
    }
    #endregion

    #region Private Methods
    private void ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      OnItemChecked(e);
    }

    private void ListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      OnSelectedIndexChanged(e);
    }
    #endregion

    #region Protected Methods
    /// <inheritdoc/>
    protected override bool ShouldSerializeBackColor()
    {
      return BackColor != SystemColors.Window;
    }

    /// <inheritdoc/>
    protected override bool ShouldSerializeForeColor()
    {
      return ForeColor != SystemColors.WindowText;
    }

    /// <inheritdoc/>
    protected override void AttachEvents()
    {
      base.AttachEvents();
      ListView.ItemChecked += new ItemCheckedEventHandler(ListView_ItemChecked);
      ListView.SelectedIndexChanged += new EventHandler(ListView_SelectedIndexChanged);
    }

    /// <inheritdoc/>
    protected override void DetachEvents()
    {
      base.DetachEvents();
      ListView.ItemChecked -= new ItemCheckedEventHandler(ListView_ItemChecked);
      ListView.SelectedIndexChanged -= new EventHandler(ListView_SelectedIndexChanged);
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      ListViewControl c = writer.DiffObject as ListViewControl;
      base.Serialize(writer);

      if (CheckBoxes != c.CheckBoxes)
        writer.WriteBool("CheckBoxes", CheckBoxes);
      if (MultiSelect != c.MultiSelect)
        writer.WriteBool("MultiSelect", MultiSelect);
      if (ShowGroups != c.ShowGroups)
        writer.WriteBool("ShowGroups", ShowGroups);
      if (View != c.View)
        writer.WriteValue("View", View);
    }

    /// <summary>
    /// This method fires the <b>ItemChecked</b> event and the script code connected to the <b>ItemCheckedEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnItemChecked(ItemCheckedEventArgs e)
    {
      if (ItemChecked != null)
        ItemChecked(this, e);
      InvokeEvent(ItemCheckedEvent, e);
    }

    /// <summary>
    /// This method fires the <b>SelectedIndexChanged</b> event and the script code connected to the <b>SelectedIndexChangedEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnSelectedIndexChanged(EventArgs e)
    {
      if (SelectedIndexChanged != null)
        SelectedIndexChanged(this, e);
      InvokeEvent(SelectedIndexChangedEvent, e);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>ListViewControl</b> class with default settings. 
    /// </summary>
    public ListViewControl()
    {
      FListView = new ListView();
      Control = FListView;
      ListView.HideSelection = false;
    }
  }
}
