using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;

namespace FastReport.Dialog
{
  /// <summary>
  /// Represents a Windows control that enables the user to select a date using a visual monthly calendar display.
  /// Wraps the <see cref="System.Windows.Forms.MonthCalendar"/> control.
  /// </summary>
  public class MonthCalendarControl : DataFilterBaseControl
  {
    private MonthCalendar FMonthCalendar;
    private string FDateChangedEvent;

    #region Properties
    /// <summary>
    /// Occurs when the date selected in the MonthCalendar changes.
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.DateChanged"/> event.
    /// </summary>
    public event DateRangeEventHandler DateChanged;

    /// <summary>
    /// Gets an internal <b>MonthCalendar</b>.
    /// </summary>
    [Browsable(false)]
    public MonthCalendar MonthCalendar
    {
      get { return FMonthCalendar; }
    }

    /// <summary>
    /// Gets or sets the number of columns and rows of months displayed. 
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.CalendarDimensions"/> property.
    /// </summary>
    [Category("Layout")]
    public Size CalendarDimensions
    {
      get { return MonthCalendar.CalendarDimensions; }
      set { MonthCalendar.CalendarDimensions = value; }
    }

    /// <summary>
    /// Gets or sets the first day of the week as displayed in the month calendar.
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.FirstDayOfWeek"/> property.
    /// </summary>
    [DefaultValue(Day.Default)]
    [Category("Data")]
    public Day FirstDayOfWeek
    {
      get { return MonthCalendar.FirstDayOfWeek; }
      set { MonthCalendar.FirstDayOfWeek = value; }
    }

    /// <summary>
    /// Gets or sets the maximum allowable date.
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.MaxDate"/> property.
    /// </summary>
    [Category("Data")]
    public DateTime MaxDate
    {
      get { return MonthCalendar.MaxDate; }
      set { MonthCalendar.MaxDate = value; }
    }

    /// <summary>
    /// Gets or sets the maximum number of days that can be selected in a month calendar control. 
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.MaxSelectionCount"/> property.
    /// </summary>
    [DefaultValue(7)]
    [Category("Data")]
    public int MaxSelectionCount
    {
      get { return MonthCalendar.MaxSelectionCount; }
      set { MonthCalendar.MaxSelectionCount = value; }
    }

    /// <summary>
    /// Gets or sets the minimum allowable date. 
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.MinDate"/> property.
    /// </summary>
    [Category("Data")]
    public DateTime MinDate
    {
      get { return MonthCalendar.MinDate; }
      set { MonthCalendar.MinDate = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the date represented by the <b>TodayDate</b> property is displayed at the bottom of the control.
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.ShowToday"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    public bool ShowToday
    {
      get { return MonthCalendar.ShowToday; }
      set { MonthCalendar.ShowToday = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether today's date is circled. 
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.ShowTodayCircle"/> property.
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    public bool ShowTodayCircle
    {
      get { return MonthCalendar.ShowTodayCircle; }
      set { MonthCalendar.ShowTodayCircle = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the month calendar control displays week numbers (1-52) to the left of each row of days.
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.ShowWeekNumbers"/> property.
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool ShowWeekNumbers
    {
      get { return MonthCalendar.ShowWeekNumbers; }
      set { MonthCalendar.ShowWeekNumbers = value; }
    }

    /// <summary>
    /// Gets or sets the value that is used by MonthCalendar as today's date.
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.TodayDate"/> property.
    /// </summary>
    [Category("Data")]
    public DateTime TodayDate
    {
      get { return MonthCalendar.TodayDate; }
      set { MonthCalendar.TodayDate = value; }
    }

    /// <summary>
    /// Gets or sets the array of DateTime objects that determines which annual days are displayed in bold.
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.AnnuallyBoldedDates"/> property.
    /// </summary>
    [Browsable(false)]
    public DateTime[] AnnuallyBoldedDates
    {
      get { return MonthCalendar.AnnuallyBoldedDates; }
      set { MonthCalendar.AnnuallyBoldedDates = value; }
    }

    /// <summary>
    /// Gets or sets the array of DateTime objects that determines which nonrecurring dates are displayed in bold.
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.BoldedDates"/> property.
    /// </summary>
    [Browsable(false)]
    public DateTime[] BoldedDates
    {
      get { return MonthCalendar.BoldedDates; }
      set { MonthCalendar.BoldedDates = value; }
    }

    /// <summary>
    /// Gets or sets the array of DateTime objects that determine which monthly days to bold. 
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.MonthlyBoldedDates"/> property.
    /// </summary>
    [Browsable(false)]
    public DateTime[] MonthlyBoldedDates
    {
      get { return MonthCalendar.MonthlyBoldedDates; }
      set { MonthCalendar.MonthlyBoldedDates = value; }
    }

    /// <summary>
    /// Gets or sets the end date of the selected range of dates.
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.SelectionEnd"/> property.
    /// </summary>
    [Browsable(false)]
    public DateTime SelectionEnd
    {
      get { return MonthCalendar.SelectionEnd; }
      set { MonthCalendar.SelectionEnd = value; }
    }

    /// <summary>
    /// Gets or sets the selected range of dates for a month calendar control. 
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.SelectionRange"/> property.
    /// </summary>
    [Browsable(false)]
    public SelectionRange SelectionRange
    {
      get { return MonthCalendar.SelectionRange; }
      set { MonthCalendar.SelectionRange = value; }
    }

    /// <summary>
    /// Gets or sets the start date of the selected range of dates.
    /// Wraps the <see cref="System.Windows.Forms.MonthCalendar.SelectionStart"/> property.
    /// </summary>
    [Browsable(false)]
    public DateTime SelectionStart
    {
      get { return MonthCalendar.SelectionStart; }
      set { MonthCalendar.SelectionStart = value; }
    }

    /// <summary>
    /// Gets or sets a script method name that will be used to handle the 
    /// <see cref="DateChanged"/> event.
    /// </summary>
    public string DateChangedEvent
    {
      get { return FDateChangedEvent; }
      set { FDateChangedEvent = value; }
    }
    #endregion

    #region Private Methods
    private void MonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
    {
      OnDateChanged(e);
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
    protected bool ShouldSerializeCalendarDimensions()
    {
      return CalendarDimensions.Width != 1 || CalendarDimensions.Height != 1;
    }

    /// <inheritdoc/>
    protected override SelectionPoint[] GetSelectionPoints()
    {
      return new SelectionPoint[] { new SelectionPoint(AbsLeft - 2, AbsTop - 2, SizingPoint.None) };
    }

    /// <inheritdoc/>
    protected override void AttachEvents()
    {
      base.AttachEvents();
      MonthCalendar.DateChanged += new DateRangeEventHandler(MonthCalendar_DateChanged);
    }

    /// <inheritdoc/>
    protected override void DetachEvents()
    {
      base.DetachEvents();
      MonthCalendar.DateChanged -= new DateRangeEventHandler(MonthCalendar_DateChanged);
    }

    /// <inheritdoc/>
    protected override object GetValue()
    {
      return new DateTime[] { SelectionStart, SelectionEnd };
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      MonthCalendarControl c = writer.DiffObject as MonthCalendarControl;
      base.Serialize(writer);

      if (CalendarDimensions != c.CalendarDimensions)
        writer.WriteValue("CalendarDimensions", CalendarDimensions);
      if (FirstDayOfWeek != c.FirstDayOfWeek)
        writer.WriteValue("FirstDayOfWeek", FirstDayOfWeek);
      if (MaxDate != c.MaxDate)
        writer.WriteValue("MaxDate", MaxDate);
      if (MaxSelectionCount != c.MaxSelectionCount)
        writer.WriteInt("MaxSelectionCount", MaxSelectionCount);
      if (MinDate != c.MinDate)
        writer.WriteValue("MinDate", MinDate);
      if (ShowToday != c.ShowToday)
        writer.WriteBool("ShowToday", ShowToday);
      if (ShowTodayCircle != c.ShowTodayCircle)
        writer.WriteBool("ShowTodayCircle", ShowTodayCircle);
      if (ShowWeekNumbers != c.ShowWeekNumbers)
        writer.WriteBool("ShowWeekNumbers", ShowWeekNumbers);
      if (TodayDate != c.TodayDate)
        writer.WriteValue("TodayDate", TodayDate);
      if (DateChangedEvent != c.DateChangedEvent)
        writer.WriteStr("DateChangedEvent", DateChangedEvent);
    }

    /// <summary>
    /// This method fires the <b>DateChanged</b> event and the script code connected to the <b>DateChangedEvent</b>.
    /// </summary>
    /// <param name="e">Event data.</param>
    public virtual void OnDateChanged(DateRangeEventArgs e)
    {
      OnFilterChanged();
      if (DateChanged != null)
        DateChanged(this, e);
      InvokeEvent(DateChangedEvent, e);
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>MonthCalendarControl</b> class with default settings. 
    /// </summary>
    public MonthCalendarControl()
    {
      FMonthCalendar = new MonthCalendar();
      Control = FMonthCalendar;
    }
  }
}
