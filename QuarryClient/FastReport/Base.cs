using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.DevComponents.DotNetBar;
using System.CodeDom.Compiler;

namespace FastReport
{
  /// <summary>
  /// Specifies a set of actions that cannot be performed on the object in the design mode.
  /// </summary>
  [Flags]
  public enum Restrictions
  {
    /// <summary>
    /// Specifies no restrictions.
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Restricts moving the object.
    /// </summary>
    DontMove = 1,

    /// <summary>
    /// Restricts resizing the object.
    /// </summary>
    DontResize = 2,

    /// <summary>
    /// Restricts modifying the object's properties.
    /// </summary>
    DontModify = 4,

    /// <summary>
    /// Restricts editing the object.
    /// </summary>
    DontEdit = 8,

    /// <summary>
    /// Restricts deleting the object.
    /// </summary>
    DontDelete = 16,

    /// <summary>
    /// Hides all properties of the object.
    /// </summary>
    HideAllProperties = 32
  }

  /// <summary>
  /// Specifies a set of actions that can be performed on the object in the design mode.
  /// </summary>
  [Flags]
  public enum Flags
  {
    /// <summary>
    /// Specifies no actions.
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Allows moving the object.
    /// </summary>
    CanMove = 1,

    /// <summary>
    /// Allows resizing the object.
    /// </summary>
    CanResize = 2,

    /// <summary>
    /// Allows deleting the object.
    /// </summary>
    CanDelete = 4,

    /// <summary>
    /// Allows editing the object.
    /// </summary>
    CanEdit = 8,

    /// <summary>
    /// Allows changing the Z-order of an object.
    /// </summary>
    CanChangeOrder = 16,

    /// <summary>
    /// Allows moving the object to another parent.
    /// </summary>
    CanChangeParent = 32,

    /// <summary>
    /// Allows copying the object to the clipboard.
    /// </summary>
    CanCopy = 64,

    /// <summary>
    /// Allows drawing the object.
    /// </summary>
    CanDraw = 128,
    
    /// <summary>
    /// Allows grouping the object.
    /// </summary>
    CanGroup = 256,
    
    /// <summary>
    /// Allows write children in the preview mode by itself.
    /// </summary>
    CanWriteChildren = 512,
    
    /// <summary>
    /// Allows write object's bounds into the report stream.
    /// </summary>
    CanWriteBounds = 1024,
    
    /// <summary>
    /// Allows the "smart tag" functionality.
    /// </summary>
    HasSmartTag = 2048,

    /// <summary>
    /// Specifies that the object's name is global (this is true for all report objects 
    /// such as Text, Picture and so on).
    /// </summary>
    HasGlobalName = 4096,

    /// <summary>
    /// Specifies that the object can display children in the designer's Report Tree window.
    /// </summary>
    CanShowChildrenInReportTree = 8192,

    /// <summary>
    /// Specifies that the object supports mouse wheel in the preview window.
    /// </summary>
    InterceptsPreviewMouseEvents = 16384
  }
  
  /// <summary>
  /// Specifies an origin where the new objects inserted from.
  /// </summary>
  public enum InsertFrom
  {
    /// <summary>
    /// Specifies that a new object was inserted from the "Objects" toolbar or "Insert" menu.
    /// </summary>
    NewObject,
    
    /// <summary>
    /// Specifies that a new object was dragged from the "Dictionary" window.
    /// </summary>
    Dictionary,
    
    /// <summary>
    /// Specifies that a new object was pasted from the clipboard.
    /// </summary>
    Clipboard
  }

  /// <summary>
  /// Represents the root class of the FastReport object's hierarhy.
  /// </summary>
  [ToolboxItem(false)]
  public abstract class Base : Component, IFRSerializable
  {
    #region Fields
    private string FName;
    private Restrictions FRestrictions;
    private Flags FFlags;
    private Base FParent;
    private string FBaseName;
    private bool FIsAncestor;
    private bool FIsDesigning;
    private bool FIsPrinting;
    private bool FIsRunning;
    private Base FOriginalComponent;
    private string FAlias;
    private Report FReport;
    private int FZOrder;
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the name of the object.
    /// </summary>
    /// <remarks>
    ///   <para>Name of the report object must contain alpha, digit, underscore symbols only.
    ///     Data objects such as <b>Variable</b>, <b>TableDataSource</b>
    ///     etc. can have any characters in they names. Each component must have unique
    ///     name.</para>
    /// </remarks>
    /// <example>The following code demonstrates how to find an object by its name:
    /// <code>
    /// TextObject text1 = report1.FindObject("Text1") as TextObject;
    /// </code>
    /// </example>
    /// <exception cref="DuplicateNameException" caption="">Another object with such name exists.</exception>
    /// <exception cref="AncestorException" caption="">Rename an object that was introduced in the ancestor report.</exception>
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Design")]
    [DisplayName("(Name)")]
    public virtual string Name
    {
      get { return FName; }
      set
      {
        if (String.Compare(FName, value, true) == 0)
          return;
        if (value != "" && Report != null && HasFlag(Flags.HasGlobalName))
        {
          Base c = Report.FindObject(value);
          if (c != null && c != this)
            throw new DuplicateNameException(value);
          if (IsAncestor)
            throw new AncestorException(FName);
          if (IsDesigning)
            CheckValidIdent(value);
        }
        SetName(value);
      }
    }
    
    /// <summary>
    /// Gets or sets the flags that restrict some actions in the designer.
    /// </summary>
    /// <remarks>
    /// Use this property to restrict some user actions like move, resize, edit, delete. For example, if
    /// <b>Restriction.DontMove</b> flag is set, user cannot move the object in the designer.
    /// </remarks>
    [Editor(typeof(FlagsEditor), typeof(UITypeEditor))]
    [DefaultValue(Restrictions.None)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Design")]
    public Restrictions Restrictions
    {
      get { return FRestrictions; }
      set { FRestrictions = value; }
    }
    
    /// <summary>
    /// Gets the flags that allow some functionality in the designer.
    /// </summary>
    /// <remarks>
    /// Use this property only if you developing a new FastReport object.
    /// </remarks>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Flags Flags
    {
      get { return FFlags; }
    }

    /// <summary>
    /// Gets or sets the parent of the object.
    /// </summary>
    /// <remarks>
    ///   <para>Each report object must have a parent in order to appear in the report. Parent must be able to
    /// contain objects of such type.</para>
    ///   <para>Another way (preferred) to set a parent is to use specific properties of the parent object. 
    /// For example, the <see cref="Report"/> object has the <see cref="FastReport.Report.Pages"/> collection. 
    /// To add a new page to the report, use the following code: <c>report1.Pages.Add(new ReportPage());</c>
    ///   </para>
    /// </remarks>
    /// <example><code>
    /// Report report1;
    /// ReportPage page = new ReportPage();
    /// page.Parent = report1;
    /// </code></example>
    /// <exception cref="ParentException" caption="">Parent object cannot contain this object.</exception>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Base Parent
    {
      get { return FParent; }
      set
      {
        if (value != FParent)
        {
          if (value != null)
          {
            if (value is IParent)
              (value as IParent).AddChild(this);
          }  
          else
          {
            (FParent as IParent).RemoveChild(this);
          }  
        }
      }
    }

    /// <summary>
    /// The base part of the object's name.
    /// </summary>
    /// <remarks>
    /// This property is used to automatically create unique object's name. See <see cref="CreateUniqueName"/>
    /// </remarks>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string BaseName
    {
      get { return FBaseName; }
      set { FBaseName = value; }
    }

    /// <summary>
    /// Gets the short type name.
    /// </summary>
    /// <remarks>
    /// Returns the short type name, such as "TextObject".
    /// </remarks>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ClassName
    {
      get { return GetType().Name; }
    }

    /// <summary>
    /// Gets reference to the parent <see cref="Report"/> object.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Report Report
    {
      get
      {
        if (this is Report)
          return this as Report;
        if (FReport != null)
          return FReport;  

        if (Parent != null)
          return Parent.Report;
        return null;
      }
    }

    /// <summary>
    /// Gets reference to the parent <see cref="PageBase"/> object.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PageBase Page
    {
      get
      {
        if (this is PageBase)
          return (PageBase)this;

        Base c = Parent;
        while (c != null)
        {
          if (c is PageBase)
            return (PageBase)c;
          c = c.Parent;
        }
        return null;
      }
    }

    /// <summary>
    /// Gets the collection of this object's child objects.
    /// </summary>
    /// <remarks>
    /// This property returns child objects that belongs to this object. For example, <b>Report.ChildObjects</b>
    /// will return only pages that contains in the report, but not page childs such as bands. To return all
    /// child objects, use <see cref="AllObjects"/> property.
    /// </remarks>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ObjectCollection ChildObjects
    {
      get
      {
        ObjectCollection result = new ObjectCollection();
        if (this is IParent)
          (this as IParent).GetChildObjects(result);
        return result;
      }
    }

    /// <summary>
    /// Gets the collection of all child objects.
    /// </summary>
    /// <remarks>
    /// This property returns child objects that belongs to this object and to child objects of this object. 
    /// For example, <b>Report.AllObjects</b> will return all objects that contains in the report - such as 
    /// pages, bands, text objects.
    /// </remarks>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ObjectCollection AllObjects
    {
      get
      {
        ObjectCollection result = new ObjectCollection();
        EnumObjects(this, result);
        return result;
      }
    }

    /// <summary>
    /// Gets or sets the Z-order of the object.
    /// </summary>
    /// <remarks>
    /// The Z-order is also called "creation order". It is the index of an object in the parent's objects list.
    /// For example, put two text objects on a band. First object will have <b>ZOrder</b> = 0, second = 1. Setting the
    /// second object's <b>ZOrder</b> to 0 will move it to the back of the first text object.
    /// </remarks>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ZOrder
    {
      get
      {
        if (FParent != null)
          return (FParent as IParent).GetChildOrder(this);
        return FZOrder;
      }
      set
      {
        if (FParent != null)
          (FParent as IParent).SetChildOrder(this, value);
        else
          FZOrder = value;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the object was introduced in the ancestor report.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsAncestor
    {
      get { return FIsAncestor; }
    }

    /// <summary>
    /// Gets a value indicating whether the object is in the design state.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDesigning
    {
      get { return FIsDesigning; }
    }

    /// <summary>
    /// Gets a value indicating whether the object is currently printing.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsPrinting
    {
      get { return FIsPrinting; }
    }

    /// <summary>
    /// Gets a value indicating whether the object is currently processed by the report engine.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsRunning
    {
      get { return FIsRunning; }
    }

    /// <summary>
    /// Gets a value indicating whether the object is selected in the designer.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool IsSelected
    {
      get { return Report != null && Report.Designer != null && Report.Designer.SelectedObjects.IndexOf(this) != -1; }
    }

    /// <summary>
    /// Gets a value indicating whether one of the object's parent is selected in the designer.
    /// </summary>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsParentSelected
    {
      get
      {
        if (Report.Designer == null)
          return false;
        Base parent = Parent;
        while (parent != null)
        {
          if (Report.Designer.SelectedObjects.IndexOf(parent) != -1)
            return true;
          parent = parent.Parent;
        }
        return false;
      }
    }

    /// <summary>
    /// Gets an original component for this object.
    /// </summary>
    /// <remarks>
    /// This property is used in the preview mode. Each object in the prepared report is bound to its
    /// original (from the report template). This technique is used to minimize the prepared report's size.
    /// </remarks>
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Base OriginalComponent
    {
      get { return FOriginalComponent; }
      set { FOriginalComponent = value; }
    }

    internal string Alias
    {
      get { return FAlias; }
      set { FAlias = value; }
    }
    #endregion

    #region Private Methods
    private void CheckValidIdent(string value)
    {
      if (!CodeGenerator.IsValidLanguageIndependentIdentifier(value))
        throw new NotValidIdentifierException(value);
    }

    private void EnumObjects(Base c, ObjectCollection list)
    {
      if (c != this)
        list.Add(c);
      foreach (Base obj in c.ChildObjects)
        EnumObjects(obj, list);
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Helper method, helps to set a reference-type value to the property.
    /// </summary>
    /// <param name="prop">Old property value.</param>
    /// <param name="value">New property value.</param>
    /// <remarks>
    /// This method is used widely to set a new value to the property that references another FastReport object.
    /// Method deals with the <see cref="Parent"/> property.
    /// </remarks>
    /// <example>This is example of the <c>DataBand.Header</c> property:<code>
    /// public DataHeaderBand Header
    /// {
    ///   get { return FHeader; }
    ///   set
    ///   {
    ///     SetProp(FHeader, value);
    ///     FHeader = value;
    ///   }
    /// }
    /// </code></example>
    protected void SetProp(Base prop, Base value)
    {
      if (prop != value)
      {
        if (prop != null)
          prop.SetParent(null);
        if (value != null)
        {
          value.Parent = null;
          value.SetParent(this);
        }  
      }
    }

    /// <summary>
    /// Checks if two float values are different.
    /// </summary>
    /// <param name="f1">First value.</param>
    /// <param name="f2">Second value.</param>
    /// <returns><c>true</c> if values are not equal.</returns>
    /// <remarks>
    /// This method is needed to compare two float values using some precision (0.001). It is useful
    /// to compare objects' locations and sizes for equality.
    /// </remarks>
    protected bool FloatDiff(float f1, float f2)
    {
      return Math.Abs(f1 - f2) > 0.001;
    }

    /// <summary>
    /// Deserializes nested object properties.
    /// </summary>
    /// <param name="reader">Reader object.</param>
    /// <remarks>
    /// <para>Typically the object serializes all properties to the single xml item:</para>
    /// <code>
    /// &lt;TextObject Name="Text2" Left="18.9" Top="37.8" Width="283.5" Height="28.35"/&gt;
    /// </code>
    /// <para>Some objects like <see cref="DataBand"/> have child objects that serialized in subitems:</para>
    /// <code>
    /// &lt;DataBand Name="Data1" Top="163" Width="718.2" Height="18.9"&gt;
    ///   &lt;TextObject Name="Text3" Left="18.9" Top="37.8" Width="283.5" Height="28.35"/&gt;
    /// &lt;/DataBand&gt;
    /// </code>
    /// <para>To read such subitems, the <c>DeserializeSubItems</c> method is used. Base 
    /// implementation reads the child objects. You may override it to read some specific subitems.</para>
    /// </remarks>
    /// <example>The following code is used to read report's styles:
    /// <code>
    /// protected override void DeserializeSubItems(FRReader reader)
    /// {
    ///   if (String.Compare(reader.ItemName, "Styles", true) == 0)
    ///     reader.Read(Styles);
    ///   else
    ///     base.DeserializeSubItems(reader);
    /// }
    /// </code>
    /// </example>
    protected virtual void DeserializeSubItems(FRReader reader)
    {
      if (reader.ReadChildren)
      {
        Base obj = reader.Read() as Base;
        if (obj != null)
        {
          obj.Parent = this;
          if (IsAncestor && !obj.IsAncestor)
            obj.ZOrder = obj.FZOrder;
        }  
      }
    }

    /// <summary>
    /// Replaces the macros in the given string and returns the new string.
    /// </summary>
    /// <param name="text">The text containing macros.</param>
    /// <returns>The text with macros replaced with its values.</returns>
    protected string ExtractDefaultMacros(string text)
    {
      Dictionary<string, object> macroValues = Report.PreparedPages.MacroValues;

      string[] copyNames = Report.PrintSettings.CopyNames;
      string copyName = "";
      if (copyNames.Length > 0)
      {
        // get zero-based index of printed copy. In the preview, use the first copy name
        int copyIndex = 0;
        if (macroValues.ContainsKey("Copy#"))
          copyIndex = (int)macroValues["Copy#"];
        // get appropriate copy name
        if (copyIndex >= copyNames.Length)
          copyIndex = copyNames.Length - 1;
        copyName = copyNames[copyIndex];
      }

      text = text.Replace("[TOTALPAGES#]", macroValues["TotalPages#"].ToString());
      text = text.Replace("[PAGE#]", macroValues["Page#"].ToString());
      text = text.Replace("[COPYNAME#]", copyName);
      return text;
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        Parent = null;
        Clear();
      }
      base.Dispose(disposing);
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Set object's flags.
    /// </summary>
    /// <param name="flags">Flag to set.</param>
    /// <param name="value"><b>true</b> to set the flag, <b>false</b> to reset.</param>
    public void SetFlags(Flags flags, bool value)
    {
      if (value)
        FFlags |= flags;
      else
        FFlags &= ~flags;
    }

    internal void SetAncestor(bool value)
    {
      FIsAncestor = value;
    }

    internal void SetDesigning(bool value)
    {
      FIsDesigning = value;
    }

    internal void SetPrinting(bool value)
    {
      FIsPrinting = value;
    }
    
    internal void SetRunning(bool value)
    {
      FIsRunning = value; 
    }
    
    /// <summary>
    /// Sets the reference to a Report. 
    /// </summary>
    /// <param name="value">Report to set.</param>
    public void SetReport(Report value)
    {
      FReport = value;
    }

    /// <summary>
    /// Sets the object's name.
    /// </summary>
    /// <remarks>
    ///     This method is for internal use only. It just sets a new name without any checks
    ///     (unlike the <see cref="Name"/> property setter).
    /// </remarks>
    /// <seealso cref="Name">Name Property</seealso>
    /// <param name="value">New name.</param>
    public virtual void SetName(string value)
    {
      FName = value;
    }

    /// <summary>
    /// Sets the object's parent.
    /// </summary>
    /// <remarks>
    /// This method is for internal use only. You can use it if you are developing a new
    /// component for FastReport. Override it to perform some actions when the parent of an
    /// object is changing. This method checks that parent can contain a child.
    /// </remarks>
    /// <exception cref="ParentException" caption="">Parent object cannot contain this object.</exception>
    /// <param name="value">New parent.</param>
    public virtual void SetParent(Base value)
    {
      if (value != null)
        if (!(value is IParent) || !(value as IParent).CanContain(this))
          throw new ParentException(value, this);
      SetParentCore(value);
    }

    /// <summary>
    /// Sets the object's parent.
    /// </summary>
    /// <param name="value">New parent.</param>
    /// <remarks>
    /// This method is for internal use only. You can use it if you are developing a new component for FastReport.
    /// This method does not perform any checks, it just sets the new parent.
    /// </remarks>
    public void SetParentCore(Base value)
    {
      FParent = value;
    }
    
    /// <summary>
    /// Deletes the object in the designer.
    /// </summary>
    /// <remarks>
    /// <para>This method is called when you delete the object in the designer.</para>
    /// <para>Typically this method calls the <see cref="Dispose"/> method to delete the object and all 
    /// its children. You may override it to delete the object only, and keep children.</para>
    /// </remarks>
    public virtual void Delete()
    {
      Dispose();
    }

    /// <summary>
    /// Searches for an object with given name.
    /// </summary>
    /// <param name="name">Name of the object to find.</param>
    /// <returns>Returns a null reference if object is not found</returns>
    /// <example>The following code demonstrates how to find an object by its name:
    /// <code>
    /// TextObject text1 = report1.FindObject("Text1") as TextObject;
    /// if (text1 != null) 
    /// { 
    ///   // object found 
    /// }
    /// </code>
    /// </example>
    public virtual Base FindObject(string name)
    {
      ObjectCollection l = AllObjects;
      foreach (Base c in l)
      {
        if (name == c.Name)
          return c;
      }
      return null;
    }

    /// <summary>
    /// Creates the unique object's name.
    /// </summary>
    /// <remarks>
    /// <para><b>Note:</b> you have to set object's parent before calling this method. Method uses the <see cref="BaseName"/> 
    /// property to create a name.</para>
    /// <para><b>Note:</b> this method may be very slow on a report that contains lots of objects. Consider
    /// using own naming logic in this case.</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// TextObject textObj = new TextObject();
    /// dataBand1.Objects.Add(textObj);
    /// textObj.CreateUniqueName();
    /// </code>
    /// </example>
    public void CreateUniqueName()
    {
      Report report = Report;
      if (report == null)
        return;
        
      string s;
      int i = 1;
      do
      {
        s = FBaseName + i.ToString();
        i++;
      }
      while (report.FindObject(s) != null);
      SetName(s);
    }

    /// <summary>
    /// Clears the object's state.
    /// </summary>
    /// <remarks>
    /// This method also disposes all object's children.
    /// </remarks>
    public virtual void Clear()
    {
      ObjectCollection list = ChildObjects;
      foreach (Base c in list)
      {
        c.Dispose();
      }
    }

    /// <summary>
    /// Serializes the object.
    /// </summary>
    /// <remarks>
    ///   <para>Do not call this method directly. You should override it if you are
    ///     developing a new component for FastReport.</para>
    ///   <para>This method is called when the object needs to save the state. It may happen
    ///     when:</para>
    ///   <list type="bullet">
    ///     <item>
    ///       <description>saving the report to the file or stream;</description>
    ///     </item>
    ///     <item>
    ///       <description>saving the report to the designer's undo buffer;</description>
    ///     </item>
    ///     <item>
    ///       <description>
    ///                 assigning the object to another object using the
    ///                 <see cref="Assign"/> or <see cref="AssignAll(Base)">AssignAll</see> methods;
    ///             </description>
    ///     </item>
    ///     <item>
    ///       <description>saving the object to the designer's clipboard;</description>
    ///     </item>
    ///     <item>
    ///       <description>saving the object to the preview (when run a
    ///             report).</description>
    ///     </item>
    ///   </list>
    /// </remarks>
    /// <param name="writer">Writer object.</param>
    public virtual void Serialize(FRWriter writer)
    {
      Base c = writer.DiffObject as Base;
      if (writer.SerializeTo != SerializeTo.Preview)
      {
        // in the preview mode we don't need to write ItemName and Name properties. Alias is wriiten instead.
        writer.ItemName = FIsAncestor && 
          (writer.SerializeTo == SerializeTo.Report || writer.SerializeTo == SerializeTo.Undo) ? 
          "inherited" : ClassName;
        if (Name != "")
          writer.WriteStr("Name", Name);
        if (Restrictions != c.Restrictions)
          writer.WriteValue("Restrictions", Restrictions);
        if ((writer.SerializeTo == SerializeTo.Report || writer.SerializeTo == SerializeTo.Undo) && 
          !IsAncestor && Parent != null && Parent.IsAncestor)
          writer.WriteInt("ZOrder", ZOrder);
      }    
      if (writer.SaveChildren)
      {
        foreach (Base child in ChildObjects)
        {
          writer.Write(child);
        }
      }  
    }

    /// <summary>
    /// Deserializes the object.
    /// </summary>
    /// <remarks>
    ///   <para>Do not call this method directly. You should override it if you are
    ///     developing a new component for FastReport.</para>
    ///   <para>This method is called when the object needs to restore the state. It may
    ///     happen when:</para>
    ///   <list type="bullet">
    ///     <item>
    ///       <description>loading the report from a file or stream;</description>
    ///     </item>
    ///     <item>
    ///       <description>loading the report from the designer's undo
    ///             buffer;</description>
    ///     </item>
    ///     <item>
    ///       <description>assigning another object to this object using the
    ///         <see cref="Assign"/> or <see cref="AssignAll(Base)">AssignAll</see> methods;</description>
    ///     </item>
    ///     <item>
    ///       <description>loading the object from the designer's
    ///             clipboard;</description>
    ///     </item>
    ///     <item>loading the object from the preview pages.</item>
    ///   </list>
    /// </remarks>
    /// <param name="reader">Reader object.</param>
    public virtual void Deserialize(FRReader reader)
    {
      reader.ReadProperties(this);
      while (reader.NextItem())
      {
        DeserializeSubItems(reader);
      }
    }

        //static int i = 0;

    /// <summary>
    /// Assigns values from another source.
    /// </summary>
    /// <remarks>
    /// <b>Note:</b> this method is relatively slow because it serializes
    /// an object to the xml and then deserializes it.
    /// </remarks>
    /// <param name="source">Source to assign from.</param>
    public void BaseAssign(Base source)
    {
      bool saveAncestor = source.IsAncestor;
      source.SetAncestor(false);

      using (XmlItem xml = new XmlItem())
      using (FRWriter writer = new FRWriter(xml))
      using (FRReader reader = new FRReader(Report, xml))
      {
        writer.SaveChildren = false;
        writer.Write(source, this);
        reader.Read(this);
      }

      Alias = source.Alias;
      OriginalComponent = source.OriginalComponent;
      source.SetAncestor(saveAncestor);
    }

    /// <summary>Copies the contents of another, similar object.</summary>
    /// <remarks>
    ///   <para>Call Assign to copy the properties from another object of the same type. 
    /// The standard form of a call to Assign is</para>
    ///   <para><c>destination.Assign(source);</c></para>
    ///   <para>
    ///         which tells the <b>destination</b> object to copy the contents of the
    ///         <b>source</b> object to itself. In this method, all child objects are
    ///         ignored. If you want to copy child objects, use the
    ///         <see cref="AssignAll(Base)">AssignAll</see> method.
    ///     </para>
    /// </remarks>
    /// <example><code>
    /// Report report1;
    /// Report report2 = new Report();
    /// // copy all report settings, do not copy report objects
    /// report2.Assign(report1);
    /// </code></example>
    /// <seealso cref="AssignAll(Base)">AssignAll Method</seealso>
    /// <param name="source">Source object to copy the contents from.</param>
    public virtual void Assign(Base source)
    {
      Restrictions = source.Restrictions;
      Alias = source.Alias;
      OriginalComponent = source.OriginalComponent;
    }

    /// <summary>Copies the contents (including children) of another, similar object.</summary>
    /// <remarks>
    ///   <para>
    ///         This method is similar to <see cref="Assign"/> method. It copies child
    ///         objects as well.
    ///     </para>
    /// </remarks>
    /// <example><code>
    /// Report report1;
    /// Report report2 = new Report();
    /// // copy all report settings and objects
    /// report2.AssignAll(report1);
    /// </code></example>
    /// <seealso cref="Assign"/>
    /// <param name="source">Source object to copy the state from.</param>
    public void AssignAll(Base source)
    {
      AssignAll(source, false);
    }

    internal void AssignAll(Base source, bool assignName)
    {
      Clear();
      Assign(source);
      if (assignName)
        Name = source.Name;
      
      foreach (Base child in source.ChildObjects)
      {
        Base myChild = Activator.CreateInstance(child.GetType()) as Base;
        myChild.SetReport(Report);
        myChild.AssignAll(child, assignName);
        myChild.SetReport(null);
        myChild.Parent = this;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the object has the specified parent in its parent hierarhy.
    /// </summary>
    /// <param name="obj">Parent object to check.</param>
    /// <returns>Returns <b>true</b> if the object has given parent in its parent hierarhy.</returns>
    public bool HasParent(Base obj)
    {
      Base parent = Parent;
      while (parent != null)
      {
        if (parent == obj)
          return true;
        parent = parent.Parent;
      }
      return false;
    }
    
    /// <summary>
    /// Gets a value indicating whether the object has a specified flag in its <see cref="Flags"/> property.
    /// </summary>
    /// <param name="flag">Flag to check.</param>
    /// <returns><b>true</b> if <b>Flags</b> property contains specified flag.</returns>
    public bool HasFlag(Flags flag)
    {
      return (Flags & flag) > 0;
    }

    /// <summary>
    /// Gets a value indicating whether the object has a specified restriction 
    /// in its <see cref="Restrictions"/> property.
    /// </summary>
    /// <param name="restriction">Restriction to check.</param>
    /// <returns><b>true</b> if <b>Restrictions</b> property contains specified restriction.</returns>
    public bool HasRestriction(Restrictions restriction)
    {
      return (Restrictions & restriction) > 0;
    }

    /// <summary>
    /// Invokes script event.
    /// </summary>
    /// <param name="name">Name of the event to invoke.</param>
    /// <param name="param">Event parameters.</param>
    /// <remarks>
    /// <para>Do not call this method directly. You should use it if you are developing a new component 
    /// for FastReport.</para>
    /// <para>Use this method to call an event handler that is located in the report's script.</para>
    /// </remarks>
    /// <example>Example of the OnBeforePrint method:<code>
    /// public void OnBeforePrint(EventArgs e)
    /// {
    ///   if (BeforePrint != null)
    ///     BeforePrint(this, e);
    ///   InvokeEvent(BeforePrintEvent, e);
    /// }
    /// </code></example>
    public void InvokeEvent(string name, object param)
    {
      if (String.IsNullOrEmpty(name))
        return;
      Report report = Report;
      if (report != null)
        report.InvokeEvent(name, new object[] { this, param });
    }

    /// <summary>
    /// Called before inserting a new object in the designer.
    /// </summary>
    /// <remarks>
    ///   <para>Do not call this method directly. You may override it if you are developing a
    ///     new component for FastReport.</para>
    ///   <para>
    ///         Some objects are registered in the designer several times with the same object
    ///         type, but different flags. For example, the <see cref="ShapeObject"/>
    ///         represents different shapes: rectangle, roundrect, ellipse and so on. All these
    ///         shapes are registered in the designer using flags (the last parameter in this
    ///         code): 
    ///       <code>
    /// RegisteredObjects.Add(typeof(ShapeObject), "ReportPage,Shapes", 108, "Objects,Shapes,Rectangle", 0);
    /// RegisteredObjects.Add(typeof(ShapeObject), "ReportPage,Shapes", 109, "Objects,Shapes,RoundRectangle", 1);
    /// RegisteredObjects.Add(typeof(ShapeObject), "ReportPage,Shapes", 110, "Objects,Shapes,Ellipse", 2);
    /// </code>
    ///     <para>When we put the "Ellipse" object on a band, the designer creates the
    ///         <b>ShapeObject</b> instance and calls its <b>OnBeforeInsert</b> method with
    ///         <b>flags</b> value set to 2. In turn, the <b>OnBeforeInsert</b> method converts the
    ///         int value of the flags to the shape kind:</para>
    ///       <code>
    /// public override void OnBeforeInsert(int flags)
    /// {
    ///   FShape = (ShapeKind)flags;
    /// }
    /// </code>
    ///   </para>
    /// </remarks>
    /// <param name="flags">Object's flags.</param>
    public virtual void OnBeforeInsert(int flags)
    {
    }

    /// <summary>
    /// Called after the new object was inserted in the designer.
    /// </summary>
    /// <remarks>
    /// <para>Do not call this method directly. You may override it if you are developing a new component 
    /// for FastReport.</para>
    /// <para>This method is called when new object is inserted, pasted from clipboard or dragged from
    /// "Dictionary" window. You may override this method if you need to perform some actions when object
    /// is inserted. Typical implementation invokes the object's editor if "Edit after insert" flag is set
    /// in the designer options.</para>
    /// </remarks>
    /// <param name="source">The insertion source.</param>
    public virtual void OnAfterInsert(InsertFrom source)
    {
    }
    
    /// <summary>
    /// Called after all report objects were loaded.
    /// </summary>
    /// <remarks>
    /// Do not call this method directly. You may override it if you are developing a new component 
    /// for FastReport.
    /// </remarks>
    public virtual void OnAfterLoad()
    {
    }

    /// <summary>
    /// Called when the user selects another object in the designer.
    /// </summary>
    /// <remarks>
    /// This method is typically used by the in-place object's editor to check if selection was changed and close
    /// the editor.
    /// </remarks>
    public virtual void SelectionChanged()
    {
    }

    /// <summary>
    /// Gets the object's context menu.
    /// </summary>
    /// <returns>Null reference if object does not have a menu.</returns>
    /// <remarks>
    /// <para>Do not call this method directly. You may override it if you are developing a new component 
    /// for FastReport.</para>
    /// <para>You may use base menu classes such as <see cref="ComponentMenuBase"/>, 
    /// <see cref="ReportComponentBaseMenu"/> to create own context menus.</para>
    /// </remarks>
    public virtual ContextMenuBar GetContextMenu()
    {
      return null;
    }

    /// <summary>
    /// Gets all expressions contained in the object.
    /// </summary>
    /// <returns>Array of expressions or <b>null</b> if object contains no expressions.</returns>
    /// <remarks>
    ///   <para>Do not call this method directly. You may override it if you are developing a
    ///     new component for FastReport.</para>
    ///   <para>
    ///         This method is called by FastReport each time before run a report. FastReport
    ///         do this to collect all expressions and compile them. For example,
    ///         <b>GetExpressions</b> method of the <see cref="TextObject"/> class
    ///         parses the text and returns all expressions found in the text.
    ///     </para>
    /// </remarks>
    public virtual string[] GetExpressions()
    {
      return null;
    }
    
    /// <summary>
    /// Returns a custom code that will be added to the report script before report is run.
    /// </summary>
    /// <returns>A custom script text, if any. Otherwise returns <b>null</b>.</returns>
    /// <remarks>
    /// <para>This method may return any valid code that may be inserted into the report script. Currently it is
    /// used in the TableObject to define the following script methods: Sum, Min, Max, Avg, Count.
    /// </para>
    /// <para>
    /// Note: you must take into account the current script language - C# or VB.Net. You may check it via
    /// <b>Report.ScriptLanguage</b> property.
    /// </para>
    /// </remarks>
    public virtual string GetCustomScript()
    {
      return null;
    }
    
    /// <summary>
    /// Used to extract macros such as "TotalPages#" in the preview mode.
    /// </summary>
    /// <remarks>
    /// This method is used mainly by the <b>TextObject</b> to extract macros and replace it with 
    /// actual values passed in the <b>pageIndex</b> and <b>totalPages</b> parameters. This method
    /// is called automatically when the object is being previewed.
    /// </remarks>
    public virtual void ExtractMacros()
    {
    }
    #endregion
    
    /// <summary>
    /// Initializes a new instance of the <b>Base</b> class with default settings. 
    /// </summary>
    public Base()
    {
      FName = "";
      FAlias = "";
      FBaseName = ClassName;
      FRestrictions = new Restrictions();
      SetFlags(Flags.CanMove | Flags.CanResize | Flags.CanDelete | Flags.CanEdit | Flags.CanChangeOrder |
       Flags.CanChangeParent | Flags.CanCopy | Flags.CanDraw | Flags.CanShowChildrenInReportTree, true);
    }
  }
}