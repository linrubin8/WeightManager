using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using System.Data;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.Data;
using FastReport.TypeConverters;

namespace FastReport.Dialog
{
  /// <summary>
  /// Base class for list box controls such as <b>ListBoxControl</b>, <b>CheckedListBoxControl</b>.
  /// </summary>
  public abstract class ListBoxBaseControl : DataFilterBaseControl
  {
    private string FSelectedIndexChangedEvent;
    private string FMeasureItemEvent;
    private string FDrawItemEvent;

    #region Properties
    /// <summary>
    /// Occurs when the <b>SelectedIndex</b> property has changed.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.SelectedIndexChanged"/> event.
    /// </summary>
    public event EventHandler SelectedIndexChanged;

    /// <summary>
    /// Occurs when an owner-drawn ListBox is created and the sizes of the list items are determined.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.MeasureItem"/> event.
    /// </summary>
    public event MeasureItemEventHandler MeasureItem;

    /// <summary>
    /// Occurs when a visual aspect of an owner-drawn ListBox changes.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.DrawItem"/> event.
    /// </summary>
    public event DrawItemEventHandler DrawItem;

    private ListBox ListBox
    {
      get { return Control as ListBox; }
    }

    /// <summary>
    /// Gets or sets the width of columns in a multicolumn ListBox.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.ColumnWidth"/> property.
    /// </summary>
    [DefaultValue(0)]
    [Category("Behavior")]
    public int ColumnWidth
    {
      get { return ListBox.ColumnWidth; }
      set { ListBox.ColumnWidth = value; }
    }

    /// <summary>
    /// Gets or sets the drawing mode for the control.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.DrawMode"/> property.
    /// </summary>
    [DefaultValue(DrawMode.Normal)]
    [Category("Behavior")]
    public virtual DrawMode DrawMode
    {
      get { return ListBox.DrawMode; }
      set { ListBox.DrawMode = value; }
    }

    /// <summary>
    /// Gets or sets the height of an item in the ListBox.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.ItemHeight"/> property.
    /// </summary>
    [Category("Behavior")]
    public virtual int ItemHeight
    {
      get { return ListBox.ItemHeight; }
      set { ListBox.ItemHeight = value; }
    }

    /// <summary>
    /// Gets the items of the ListBox.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.Items"/> property.
    /// </summary>
    [Editor(typeof(ItemsEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public ListBox.ObjectCollection Items
    {
      get { return ListBox.Items; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ListBox supports multiple columns.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.MultiColumn"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool MultiColumn
    {
      get { return ListBox.MultiColumn; }
      set { ListBox.MultiColumn = value; }
    }

    /// <summary>
    /// Gets or sets the method in which items are selected in the ListBox.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.SelectionMode"/> property.
    /// </summary>
    [DefaultValue(SelectionMode.One)]
    [Category("Behavior")]
    public SelectionMode SelectionMode
    {
      get { return ListBox.SelectionMode; }
      set { ListBox.SelectionMode = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the items in the ListBox are sorted alphabetically.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.Sorted"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool Sorted
    {
      get { return ListBox.Sorted; }
      set { ListBox.Sorted = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ListBox can recognize and expand tab characters when drawing its strings.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.UseTabStops"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool UseTabStops
    {
      get { return ListBox.UseTabStops; }
      set { ListBox.UseTabStops = value; }
    }

    /// <summary>
    /// Gets or sets the string that contains all items text.
    /// </summary>
    [Browsable(false)]
    public string ItemsText
    {
      get { return Converter.IListToString(Items); }
      set { Converter.StringToIList(value, Items); }
    }

    /// <summary>
    /// Gets or sets the zero-based index of the currently selected item in a ListBox.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.SelectedIndex"/> property.
    /// </summary>
    [Browsable(false)]
    public int SelectedIndex
    {
      get { return ListBox.SelectedIndex; }
      set { ListBox.SelectedIndex = value; }
    }

    /// <summary>
    /// Gets a collection that contains the zero-based indexes of all currently selected items in the ListBox.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.SelectedIndices"/> property.
    /// </summary>
    [Browsable(false)]
    public System.Windows.Forms.ListBox.SelectedIndexCollection SelectedIndices
    {
      get { return ListBox.SelectedIndices; }
    }

    /// <summary>
    /// Gets or sets the currently selected item in the ListBox.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.SelectedItem"/> property.
    /// </summary>
    [Browsable(false)]
    public object SelectedItem
    {
      get { return ListBox.SelectedItem; }
      set { ListBox.SelectedItem = value; }
    }

    /// <summary>
    /// Gets a collection containing the currently selected items in the ListBox.
    /// Wraps the <see cref="System.Windows.Forms.ListBox.SelectedItems"/> property.
    /// </summary>
    [Browsable(false)]
    public ListBox.SelectedObjectCollection SelectedItems
    {
      get { return ListBox.SelectedItems; }
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
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="MeasureItem"/> event.
    /// </summary>
    [Category("Events")]
    public string MeasureItemEvent
    {
      get { return FMeasureItemEvent; }
      set { FMeasureItemEvent = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="DrawItem"/> event.
    /// </summary>
    [Category("Events")]
    public string DrawItemEvent
    {
      get { return FDrawItemEvent; }
      set { FDrawItemEvent = value; }
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
    private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      OnSelectedIndexChanged(e);
    }

    private void ListBox_MeasureItem(object sender, MeasureItemEventArgs e)
    {
      OnMeasureItem(e);
    }

    private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
    {
      OnDrawItem(e);
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
      ListBox.SelectedIndexChanged += new EventHandler(ListBox_SelectedIndexChanged);
      ListBox.MeasureItem += new MeasureItemEventHandler(ListBox_MeasureItem);
      ListBox.DrawItem += new DrawItemEventHandler(ListBox_DrawItem);
    }

    /// <inheritdoc/>
    protected override void DetachEvents()
    {
      base.DetachEvents();
      ListBox.SelectedIndexChanged -= new EventHandler(ListBox_SelectedIndexChanged);
      ListBox.MeasureItem -= new MeasureItemEventHandler(ListBox_MeasureItem);
      ListBox.DrawItem -= new DrawItemEventHandler(ListBox_DrawItem);
    }

    /// <inheritdoc/>
    protected override void FillData(DataSourceBase dataSource, Column column)
    {
      Items.Clear();
      Items.AddRange(GetListOfData(dataSource, column));

      if (Items.Count > 0)
        SelectedIndex = 0;
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      ListBoxBaseControl c = writer.DiffObject as ListBoxBaseControl;
      base.Serialize(writer);

      if (ColumnWidth != c.ColumnWidth)
        writer.WriteInt("ColumnWidth", ColumnWidth);
      if (DrawMode != c.DrawMode)
        writer.WriteValue("DrawMode", DrawMode);
      if (ItemHeight != c.ItemHeight)
        writer.WriteInt("ItemHeight", ItemHeight);
      if (MultiColumn != c.MultiColumn)
        writer.WriteBool("MultiColumn", MultiColumn);
      if (SelectionMode != c.SelectionMode)
        writer.WriteValue("SelectionMode", SelectionMode);
      if (Sorted != c.Sorted)
        writer.WriteBool("Sorted", Sorted);
      if (UseTabStops != c.UseTabStops)
        writer.WriteBool("UseTabStops", UseTabStops);
      if (ItemsText != c.ItemsText)
        writer.WriteStr("ItemsText", ItemsText);
      if (SelectedIndexChangedEvent != c.SelectedIndexChangedEvent)
        writer.WriteStr("SelectedIndexChangedEvent", SelectedIndexChangedEvent);
      if (MeasureItemEvent != c.MeasureItemEvent)
        writer.WriteStr("MeasureItemEvent", MeasureItemEvent);
      if (DrawItemEvent != c.DrawItemEvent)
        writer.WriteStr("DrawItemEvent", DrawItemEvent);
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

    /// <summary>
    /// This method fires the <b>MeasureItem</b> event and the script code connected to the <b>MeasureItemEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnMeasureItem(MeasureItemEventArgs e)
    {
      if (MeasureItem != null)
        MeasureItem(this, e);
      InvokeEvent(MeasureItemEvent, e);
    }

    /// <summary>
    /// This method fires the <b>DrawItem</b> event and the script code connected to the <b>DrawItemEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnDrawItem(DrawItemEventArgs e)
    {
      if (DrawItem != null)
        DrawItem(this, e);
      InvokeEvent(DrawItemEvent, e);
    }
    #endregion
    
  }
}
