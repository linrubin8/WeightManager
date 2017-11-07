using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Data;
using System.Collections;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.Data;
using FastReport.TypeConverters;

namespace FastReport.Dialog
{
  /// <summary>
  /// Represents a Windows combo box control.
  /// Wraps the <see cref="System.Windows.Forms.ComboBox"/> control.
  /// </summary>
  public class ComboBoxControl : DataFilterBaseControl
  {
    private ComboBox FComboBox;
    private string FSelectedIndexChangedEvent;
    private string FMeasureItemEvent;
    private string FDrawItemEvent;

    #region Properties
    /// <summary>
    /// Occurs after the selection has been changed.
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.SelectedIndexChanged"/> event.
    /// </summary>
    public event EventHandler SelectedIndexChanged;

    /// <summary>
    /// Occurs each time an owner-drawn <b>ComboBox</b> item needs to be drawn and 
    /// when the sizes of the list items are determined.
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.MeasureItem"/> event.
    /// </summary>
    public event MeasureItemEventHandler MeasureItem;

    /// <summary>
    /// Occurs when a visual aspect of an owner-drawn <b>ComboBox</b> changes. 
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.DrawItem"/> event.
    /// </summary>
    public event DrawItemEventHandler DrawItem;

    /// <summary>
    /// Gets an internal <b>ComboBox</b>.
    /// </summary>
    [Browsable(false)]
    public ComboBox ComboBox
    {
      get { return FComboBox; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether your code or the operating system will handle drawing of elements in the list.
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.DrawMode"/> property.
    /// </summary>
    [DefaultValue(DrawMode.Normal)]
    [Category("Appearance")]
    public DrawMode DrawMode
    {
      get { return ComboBox.DrawMode; }
      set { ComboBox.DrawMode = value; }
    }

    /// <summary>
    /// Gets or sets a value specifying the style of the combo box.
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.DropDownStyle"/> property.
    /// </summary>
    [DefaultValue(ComboBoxStyle.DropDown)]
    [Category("Appearance")]
    public ComboBoxStyle DropDownStyle
    {
      get { return ComboBox.DropDownStyle; }
      set { ComboBox.DropDownStyle = value; }
    }

    /// <summary>
    /// Gets or sets the width of the of the drop-down portion of a combo box.
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.DropDownWidth"/> property.
    /// </summary>
    [Category("Appearance")]
    public int DropDownWidth
    {
      get { return ComboBox.DropDownWidth; }
      set { ComboBox.DropDownWidth = value; }
    }

    /// <summary>
    /// Gets or sets the height in pixels of the drop-down portion of the ComboBox.
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.DropDownHeight"/> property.
    /// </summary>
    [Category("Appearance")]
    public int DropDownHeight
    {
      get { return ComboBox.DropDownHeight; }
      set { ComboBox.DropDownHeight = value; }
    }

    /// <summary>
    /// Gets or sets the height of an item in the combo box.
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.ItemHeight"/> property.
    /// </summary>
    [Category("Appearance")]
    public int ItemHeight
    {
      get { return ComboBox.ItemHeight; }
      set { ComboBox.ItemHeight = value; }
    }

    /// <summary>
    /// Gets a collection of the items contained in this ComboBox.
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.Items"/> property.
    /// </summary>
    [Editor(typeof(ItemsEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public ComboBox.ObjectCollection Items
    {
      get { return ComboBox.Items; }
    }

    /// <summary>
    /// Gets or sets the maximum number of items to be shown in the drop-down portion of the ComboBox.
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.MaxDropDownItems"/> property.
    /// </summary>
    [DefaultValue(8)]
    [Category("Appearance")]
    public int MaxDropDownItems
    {
      get { return ComboBox.MaxDropDownItems; }
      set { ComboBox.MaxDropDownItems = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the items in the combo box are sorted.
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.Sorted"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool Sorted
    {
      get { return ComboBox.Sorted; }
      set { ComboBox.Sorted = value; }
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
    /// Gets or sets the index specifying the currently selected item.
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.SelectedIndex"/> property.
    /// </summary>
    [Browsable(false)]
    public int SelectedIndex
    {
      get { return ComboBox.SelectedIndex; }
      set { ComboBox.SelectedIndex = value; }
    }

    /// <summary>
    /// Gets or sets currently selected item in the ComboBox.
    /// Wraps the <see cref="System.Windows.Forms.ComboBox.SelectedItem"/> property.
    /// </summary>
    [Browsable(false)]
    public object SelectedItem
    {
      get { return ComboBox.SelectedItem; }
      set { ComboBox.SelectedItem = value; }
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
    #endregion
    
    #region Private Methods
    private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      OnSelectedIndexChanged(e);
    }

    private void ComboBox_MeasureItem(object sender, MeasureItemEventArgs e)
    {
      OnMeasureItem(e);
    }

    private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
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
    protected override SelectionPoint[] GetSelectionPoints()
    {
      if (ComboBox.DrawMode == DrawMode.Normal)
        return new SelectionPoint[] { 
          new SelectionPoint(AbsLeft - 2, AbsTop + Height / 2, SizingPoint.LeftCenter),
          new SelectionPoint(AbsLeft + Width + 1, AbsTop + Height / 2, SizingPoint.RightCenter) };
      return base.GetSelectionPoints();
    }

    /// <inheritdoc/>
    protected override void AttachEvents()
    {
      base.AttachEvents();
      ComboBox.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
      ComboBox.MeasureItem += new MeasureItemEventHandler(ComboBox_MeasureItem);
      ComboBox.DrawItem += new DrawItemEventHandler(ComboBox_DrawItem);
    }

    /// <inheritdoc/>
    protected override void DetachEvents()
    {
      base.DetachEvents();
      ComboBox.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
      ComboBox.MeasureItem -= new MeasureItemEventHandler(ComboBox_MeasureItem);
      ComboBox.DrawItem -= new DrawItemEventHandler(ComboBox_DrawItem);
    }

    /// <inheritdoc/>
    protected override void FillData(DataSourceBase dataSource, Column column)
    {
      Items.Clear();
      Items.AddRange(GetListOfData(dataSource, column));

      if (Items.Count > 0)
        SelectedIndex = 0;
    }

    /// <inheritdoc/>
    protected override object GetValue()
    {
      return DropDownStyle == ComboBoxStyle.DropDown ? Text : (string)SelectedItem;
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      ComboBoxControl c = writer.DiffObject as ComboBoxControl;
      base.Serialize(writer);

      if (DrawMode != c.DrawMode)
        writer.WriteValue("DrawMode", DrawMode);
      if (DropDownStyle != c.DropDownStyle)
        writer.WriteValue("DropDownStyle", DropDownStyle);
      if (DropDownWidth != c.DropDownWidth)
        writer.WriteInt("DropDownWidth", DropDownWidth);
      if (DropDownHeight != c.DropDownHeight)
        writer.WriteInt("DropDownHeight", DropDownHeight);
      if (ItemHeight != c.ItemHeight)
        writer.WriteInt("ItemHeight", ItemHeight);
      if (MaxDropDownItems != c.MaxDropDownItems)
        writer.WriteInt("MaxDropDownItems", MaxDropDownItems);
      if (Sorted != c.Sorted)
        writer.WriteBool("Sorted", Sorted);
      if (ItemsText != c.ItemsText)
        writer.WriteStr("ItemsText", ItemsText);
      if (SelectedIndexChangedEvent != c.SelectedIndexChangedEvent)
        writer.WriteStr("SelectedIndexChangedEvent", SelectedIndexChangedEvent);
      if (MeasureItemEvent != c.MeasureItemEvent)
        writer.WriteStr("MeasureItemEvent", MeasureItemEvent);
      if (DrawItemEvent != c.DrawItemEvent)
        writer.WriteStr("DrawItemEvent", DrawItemEvent);
    }

    /// <inheritdoc/>
    public override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      if (DropDownStyle == ComboBoxStyle.DropDown)
        OnFilterChanged();
    }

    /// <summary>
    /// This method fires the <b>SelectedIndexChanged</b> event and the script code connected to the <b>SelectedIndexChangedEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnSelectedIndexChanged(EventArgs e)
    {
      OnFilterChanged();
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

    /// <summary>
    /// Initializes a new instance of the <b>ComboBoxControl</b> class with default settings. 
    /// </summary>
    public ComboBoxControl()
    {
      FComboBox = new ComboBox();
      Control = FComboBox;
      BindableProperty = this.GetType().GetProperty("Text");
    }
  }
}
