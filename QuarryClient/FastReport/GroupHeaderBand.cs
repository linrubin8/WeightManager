using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.Data;
using FastReport.TypeEditors;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// Specifies a sort order.
  /// </summary>
  /// <remarks>
  /// This enumeration is used in the group header and in the "Matrix" object.
  /// </remarks>
  public enum SortOrder 
  { 
    /// <summary>
    /// Specifies no sort (natural order).
    /// </summary>
    None, 
    
    /// <summary>
    /// Specifies an ascending sort order.
    /// </summary>
    Ascending,

    /// <summary>
    /// Specifies a descending sort order.
    /// </summary>
    Descending
  }

  /// <summary>
  /// Represents a group header band.
  /// </summary>
  /// <remarks>
  /// A simple group consists of one <b>GroupHeaderBand</b> and the <b>DataBand</b> that is set 
  /// to the <see cref="Data"/> property. To create the nested groups, use the <see cref="NestedGroup"/> property.
  /// <note type="caution">
  /// Only the last nested group can have data band.
  /// </note>
  /// <para/>Use the <see cref="Condition"/> property to set the group condition. The <see cref="SortOrder"/>
  /// property can be used to set the sort order for group's data rows. You can also use the <b>Sort</b>
  /// property of the group's <b>DataBand</b> to specify additional sort.
  /// </remarks>
  /// <example>This example shows how to create nested groups.
  /// <code>
  /// ReportPage page = report.Pages[0] as ReportPage;
  /// 
  /// // create the main group
  /// GroupHeaderBand mainGroup = new GroupHeaderBand();
  /// mainGroup.Height = Units.Millimeters * 10;
  /// mainGroup.Name = "MainGroup";
  /// mainGroup.Condition = "[Orders.CustomerName]";
  /// // add a group to the page
  /// page.Bands.Add(mainGroup);
  /// 
  /// // create the nested group
  /// GroupHeaderBand nestedGroup = new GroupHeaderBand();
  /// nestedGroup.Height = Units.Millimeters * 10;
  /// nestedGroup.Name = "NestedGroup";
  /// nestedGroup.Condition = "[Orders.OrderDate]";
  /// // add it to the main group
  /// mainGroup.NestedGroup = nestedGroup;
  /// 
  /// // create a data band
  /// DataBand dataBand = new DataBand();
  /// dataBand.Height = Units.Millimeters * 10;
  /// dataBand.Name = "GroupData";
  /// dataBand.DataSource = report.GetDataSource("Orders");
  /// // connect the databand to the nested group
  /// nestedGroup.Data = dataBand;
  /// </code>
  /// </example>
  public class GroupHeaderBand : HeaderFooterBandBase, IHasEditor
  {
    #region Fields
    private GroupHeaderBand FNestedGroup;
    private DataBand FData;
    private GroupFooterBand FGroupFooter;
    private DataHeaderBand FHeader;
    private DataFooterBand FFooter;
    private string FCondition;
    private SortOrder FSortOrder;
    private bool FKeepTogether;
    private bool FResetPageNumber;
    private object FGroupValue;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets a nested group.
    /// </summary>
    /// <remarks>
    /// Use this property to create nested groups.
    /// <note type="caution">
    /// Only the last nested group can have data band.
    /// </note>
    /// </remarks>
    /// <example>
    /// This example demonstrates how to create a group with nested group.
    /// <code>
    /// ReportPage page;
    /// GroupHeaderBand group = new GroupHeaderBand();
    /// group.NestedGroup = new GroupHeaderBand();
    /// group.NestedGroup.Data = new DataBand();
    /// page.Bands.Add(group);
    /// </code>
    /// </example>
    [Browsable(false)]
    public GroupHeaderBand NestedGroup
    {
      get { return FNestedGroup; }
      set 
      {
        SetProp(FNestedGroup, value);
        FNestedGroup = value;
      }
    }

    /// <summary>
    /// Gets or sets the group data band.
    /// </summary>
    /// <remarks>
    /// Use this property to add a data band to a group. Note: only the last nested group can have Data band.
    /// </remarks>
    /// <example>
    /// This example demonstrates how to add a data band to a group.
    /// <code>
    /// ReportPage page;
    /// GroupHeaderBand group = new GroupHeaderBand();
    /// group.Data = new DataBand();
    /// page.Bands.Add(group);
    /// </code>
    /// </example>
    [Browsable(false)]
    public DataBand Data
    {
      get { return FData; }
      set
      {
        SetProp(FData, value);
        FData = value;
      }
    }

    /// <summary>
    /// Gets or sets a group footer.
    /// </summary>
    [Browsable(false)]
    public GroupFooterBand GroupFooter
    {
      get { return FGroupFooter; }
      set
      {
        SetProp(FGroupFooter, value);
        FGroupFooter = value;
      }
    }

    /// <summary>
    /// Gets or sets a header band.
    /// </summary>
    [Browsable(false)]
    public DataHeaderBand Header
    {
      get { return FHeader; }
      set
      {
        SetProp(FHeader, value);
        FHeader = value;
      }
    }

    /// <summary>
    /// Gets or sets a footer band.
    /// </summary>
    /// <remarks>
    /// To access a group footer band, use the <see cref="GroupFooter"/> property.
    /// </remarks>
    [Browsable(false)]
    public DataFooterBand Footer
    {
      get { return FFooter; }
      set
      {
        SetProp(FFooter, value);
        FFooter = value;
      }
    }

    /// <summary>
    /// Gets or sets the group condition.
    /// </summary>
    /// <remarks>
    /// This property can contain any valid expression. When running a report, this expression is calculated 
    /// for each data row. When the value of this condition is changed, FastReport starts a new group.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string Condition
    {
      get { return FCondition; }
      set { FCondition = value; }
    }
    
    /// <summary>
    /// Gets or sets the sort order.
    /// </summary>
    /// <remarks>
    /// FastReport can sort data rows automatically using the <see cref="Condition"/> value.
    /// </remarks>
    [DefaultValue(SortOrder.Ascending)]
    [Category("Behavior")]
    public SortOrder SortOrder
    {
      get { return FSortOrder; }
      set { FSortOrder = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that the group should be printed together on one page.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool KeepTogether
    {
      get { return FKeepTogether; }
      set { FKeepTogether = value; }
    }

    /// <summary>
    /// Gets or sets a value that determines whether to reset the page numbers when this group starts print.
    /// </summary>
    /// <remarks>
    /// Typically you should set the <see cref="BandBase.StartNewPage"/> property to <b>true</b> as well.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool ResetPageNumber
    {
      get { return FResetPageNumber; }
      set { FResetPageNumber = value; }
    }

    internal DataSourceBase DataSource
    {
      get 
      { 
        DataBand dataBand = GroupDataBand;
        return dataBand == null ? null : dataBand.DataSource;
      }  
    }
    
    internal DataBand GroupDataBand
    {
      get
      {
        GroupHeaderBand group = this;
        while (group != null)
        {
          if (group.Data != null)
            return group.Data;
          group = group.NestedGroup;
        }
        return null;
      }  
    }
    #endregion

    #region IParent
    /// <inheritdoc/>
    public override void GetChildObjects(ObjectCollection list)
    {
      base.GetChildObjects(list);
      if (!IsRunning)
      {
        list.Add(FHeader);
        list.Add(FNestedGroup);
        list.Add(FData);
        list.Add(FGroupFooter);
        list.Add(FFooter);
      }
    }

    /// <inheritdoc/>
    public override bool CanContain(Base child)
    {
      return base.CanContain(child) || 
        (child is DataBand && FNestedGroup == null && FData == null) || 
        (child is GroupHeaderBand && FNestedGroup == null && FData == null) || 
        child is GroupFooterBand || child is DataHeaderBand || child is DataFooterBand;
    }

    /// <inheritdoc/>
    public override void AddChild(Base child)
    {
      if (IsRunning)
      {
        base.AddChild(child);
        return;
      }
      
      if (child is GroupHeaderBand)
        NestedGroup = child as GroupHeaderBand;
      else if (child is DataBand)
        Data = child as DataBand;
      else if (child is GroupFooterBand)
        GroupFooter = child as GroupFooterBand;
      else if (child is DataHeaderBand)
        Header = child as DataHeaderBand;
      else if (child is DataFooterBand)
        Footer = child as DataFooterBand;
      else
        base.AddChild(child);
    }

    /// <inheritdoc/>
    public override void RemoveChild(Base child)
    {
      base.RemoveChild(child);
      if (IsRunning)
        return;

      if (child is GroupHeaderBand && FNestedGroup == child)
        NestedGroup = null;
      if (child is DataBand && FData == child as DataBand)
        Data = null;
      if (child is GroupFooterBand && FGroupFooter == child)
        GroupFooter = null;
      if (child is DataHeaderBand && FHeader == child)
        Header = null;
      if (child is DataFooterBand && FFooter == child)
        Footer = null;
    }
    #endregion

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      
      GroupHeaderBand src = source as GroupHeaderBand;
      Condition = src.Condition;
      SortOrder = src.SortOrder;
      KeepTogether = src.KeepTogether;
      ResetPageNumber = src.ResetPageNumber;
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      GroupHeaderBand c = writer.DiffObject as GroupHeaderBand;
      base.Serialize(writer);
      if (writer.SerializeTo == SerializeTo.Preview)
        return;
      
      if (Condition != c.Condition)
        writer.WriteStr("Condition", Condition);
      if (SortOrder != c.SortOrder)
        writer.WriteValue("SortOrder", SortOrder);
      if (KeepTogether != c.KeepTogether)
        writer.WriteBool("KeepTogether", KeepTogether);
      if (ResetPageNumber != c.ResetPageNumber)
        writer.WriteBool("ResetPageNumber", ResetPageNumber);
    }

    /// <inheritdoc/>
    public override void Delete()
    {
      if (!CanDelete)
        return;
      // remove only this band, keep its subbands
      BandBase nextBand = null;
      if (NestedGroup != null)
        nextBand = NestedGroup;
      else if (Data != null)
        nextBand = Data;
      nextBand.Parent = null;
      Base parent = Parent;
      int zOrder = ZOrder;
      Dispose();
      nextBand.Parent = parent;
      nextBand.ZOrder = zOrder;
    }

    internal override string GetInfoText()
    {
      string condition = Condition;
      condition = condition.Replace("[", "");
      condition = condition.Replace("]", "");
      if (DataHelper.IsValidColumn(Report.Dictionary, condition))
      {
        string[] parts = condition.Split(new char[] { '.' });
        return parts[parts.Length - 1];
      }  
      return Condition;
    }

    /// <inheritdoc/>
    public bool InvokeEditor()
    {
      using (GroupBandEditorForm form = new GroupBandEditorForm(this))
      {
        return form.ShowDialog() == DialogResult.OK;
      }
    }

    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      return new string[] { Condition };
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      return new GroupHeaderBandMenu(Report.Designer);
    }

    internal override bool IsEmpty()
    {
      if (NestedGroup != null)
        return NestedGroup.IsEmpty();
      else if (Data != null)
        return Data.IsEmpty();
      return base.IsEmpty();
    }
    
    internal void InitDataSource()
    {
      DataBand dataBand = GroupDataBand;
      GroupHeaderBand group = this;
      int index = 0;
      // insert group sort to the databand
      while (group != null)
      {
        if (group.SortOrder != SortOrder.None)
        {
          dataBand.Sort.Insert(index, new Sort(group.Condition, group.SortOrder == SortOrder.Descending));
          index++;
        }
        group = group.NestedGroup;
      }
      
      dataBand.InitDataSource();
    }
    
    internal void FinalizeDataSource()
    {
      DataBand dataBand = GroupDataBand;
      GroupHeaderBand group = this;
      // remove group sort from the databand
      while (group != null)
      {
        if (group.SortOrder != SortOrder.None)
          dataBand.Sort.RemoveAt(0);
        group = group.NestedGroup;
      }
    }
    
    internal void ResetGroupValue()
    {
      FGroupValue = Report.Calc(Condition);
    }

    internal bool GroupValueChanged()
    {
      object value = Report.Calc(Condition);
      if (FGroupValue == null)
      {
        if (value == null)
          return false;
        return true;  
      }
      return !FGroupValue.Equals(value);
    }
    #endregion
    
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupHeaderBand"/> class with default settings.
    /// </summary>
    public GroupHeaderBand()
    {
      FCondition = "";
      FSortOrder = SortOrder.Ascending;
    }
  }
}