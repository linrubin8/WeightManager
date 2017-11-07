using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.TypeEditors;

namespace FastReport.Map
{
  /// <summary>
  /// The base class for shape objects such as <see cref="ShapePoint"/>, <see cref="ShapePolyLine"/> and <see cref="ShapePolygon"/>.
  /// </summary>
  public class ShapeBase : Base, ICustomTypeDescriptor
  {
    #region Fields
    private bool FVisible;
    private bool FUseCustomStyle;
    private ShapeStyle FCustomStyle;
    private float FCenterOffsetX;
    private float FCenterOffsetY;
    private float FShapeOffsetX;
    private float FShapeOffsetY;
    private float FShapeScale;
    private ShapeSpatialData FSpatialData;
    private double FValue;
    private double FSaveValue;
    private int FShapeIndex;
    #endregion // Fields

    #region Properties
    /// <summary>
    /// Gets or sets the shape visibility.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool Visible
    {
      get { return FVisible; }
      set { FVisible = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that custom shape style is used.
    /// </summary>
    /// <remarks>
    /// If this property is <b>false</b>, the layer's DefaultShapeStyle is used.
    /// </remarks>
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool UseCustomStyle
    {
      get { return FUseCustomStyle; }
      set 
      { 
        FUseCustomStyle = value;
        if (value)
          FCustomStyle = new ShapeStyle();
        else
          FCustomStyle = null;
      }
    }

    /// <summary>
    /// Gets a custom shape style.
    /// </summary>
    /// <remarks>
    /// To use this property, first set the <see cref="UseCustomStyle"/> property to <b>true</b>.
    /// </remarks>
    [Category("Appearance")]
    public ShapeStyle CustomStyle
    {
      get { return FCustomStyle; }
    }

    /// <summary>
    /// Gets or sets the center point X offset.
    /// </summary>
    /// <remarks>
    /// Use this property to adjust the label's position.
    /// </remarks>
    [Category("Appearance")]
    [DefaultValue(0f)]
    public float CenterOffsetX
    {
      get { return FCenterOffsetX; }
      set { FCenterOffsetX = value; }
    }

    /// <summary>
    /// Gets or sets the center point Y offset.
    /// </summary>
    /// <remarks>
    /// Use this property to adjust the label's position.
    /// </remarks>
    [Category("Appearance")]
    [DefaultValue(0f)]
    public float CenterOffsetY
    {
      get { return FCenterOffsetY; }
      set { FCenterOffsetY = value; }
    }

    /// <summary>
    /// Gets or sets the shape X offset.
    /// </summary>
    /// <remarks>
    /// Use this property to adjust the shape position.
    /// </remarks>
    [Category("Appearance")]
    [DefaultValue(0f)]
    public float ShapeOffsetX
    {
      get { return FShapeOffsetX; }
      set { FShapeOffsetX = value; }
    }

    /// <summary>
    /// Gets or sets the shape Y offset.
    /// </summary>
    /// <remarks>
    /// Use this property to adjust the shape position.
    /// </remarks>
    [Category("Appearance")]
    [DefaultValue(0f)]
    public float ShapeOffsetY
    {
      get { return FShapeOffsetY; }
      set { FShapeOffsetY = value; }
    }

    /// <summary>
    /// Gets or sets the scale factor for this shape.
    /// </summary>
    /// <remarks>
    /// Use this property to adjust the shape size.
    /// </remarks>
    [Category("Appearance")]
    [DefaultValue(1f)]
    public float ShapeScale
    {
      get { return FShapeScale; }
      set { FShapeScale = value; }
    }

    /// <summary>
    /// Gets or sets the spatial data associated with this shape.
    /// </summary>
    [Browsable(false)]
    public ShapeSpatialData SpatialData
    {
      get { return FSpatialData; }
      set { FSpatialData = value; }
    }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DefaultValue(double.NaN)]
    [Category("Data")]
    public double Value
    {
      get { return FValue; }
      set { FValue = value; }
    }

    /// <summary>
    /// Gets a reference to the parent Map object.
    /// </summary>
    [Browsable(false)]
    public MapObject Map
    {
      get { return Layer.Map; }
    }

    /// <summary>
    /// Gets a reference to the parent Layer object.
    /// </summary>
    [Browsable(false)]
    public MapLayer Layer
    {
      get { return Parent as MapLayer; }
    }

    internal int ShapeIndex
    {
      get { return FShapeIndex; }
      set { FShapeIndex = value; }
    }

    internal bool IsValueEmpty
    {
      get { return double.IsNaN(Value); }
    }

    internal string SpatialValue
    {
      get { return SpatialData.GetValue(Layer.SpatialColumn); }
    }
    #endregion // Properties

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      ShapeBase src = source as ShapeBase;
      Visible = src.Visible;
      SpatialData.Assign(src.SpatialData);
      Value = src.Value;
      CenterOffsetX = src.CenterOffsetX;
      CenterOffsetY = src.CenterOffsetY;
      ShapeOffsetX = src.ShapeOffsetX;
      ShapeOffsetY = src.ShapeOffsetY;
      ShapeScale = src.ShapeScale;
      UseCustomStyle = src.UseCustomStyle;
      if (UseCustomStyle)
        CustomStyle.Assign(src.CustomStyle);
    }

    /// <summary>
    /// Draws the shape.
    /// </summary>
    /// <param name="e">Object that provides a data for paint event.</param>
    public virtual void Draw(FRPaintEventArgs e)
    {
    }

    /// <summary>
    /// Draws the label.
    /// </summary>
    /// <param name="e">Object that provides a data for paint event.</param>
    public virtual void DrawLabel(FRPaintEventArgs e)
    {
    }

    /// <summary>
    /// Checks if the shape is under cursor.
    /// </summary>
    /// <param name="point">The cursor coordinates.</param>
    /// <returns><b>true</b> if the cursor is over the shape.</returns>
    public virtual bool HitTest(PointF point)
    {
      return false;
    }

    /// <summary>
    /// Reduces the number of points in the shape.
    /// </summary>
    /// <param name="accuracy">The accuracy value.</param>
    public virtual void Simplify(double accuracy)
    {
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      ShapeBase c = writer.DiffObject as ShapeBase;
      base.Serialize(writer);

      if (Visible != c.Visible)
        writer.WriteBool("Visible", Visible);
      if (!SpatialData.IsEqual(c.SpatialData))
        writer.WriteValue("SpatialData", SpatialData);
      if (Value != c.Value)
        writer.WriteDouble("Value", Value);
      if (CenterOffsetX != c.CenterOffsetX)
        writer.WriteFloat("CenterOffsetX", CenterOffsetX);
      if (CenterOffsetY != c.CenterOffsetY)
        writer.WriteFloat("CenterOffsetY", CenterOffsetY);
      if (ShapeOffsetX != c.ShapeOffsetX)
        writer.WriteFloat("ShapeOffsetX", ShapeOffsetX);
      if (ShapeOffsetY != c.ShapeOffsetY)
        writer.WriteFloat("ShapeOffsetY", ShapeOffsetY);
      if (ShapeScale != c.ShapeScale)
        writer.WriteFloat("ShapeScale", ShapeScale);
      if (UseCustomStyle != c.UseCustomStyle)
        writer.WriteBool("UseCustomStyle", UseCustomStyle);
      if (UseCustomStyle)
        CustomStyle.Serialize(writer, "CustomStyle", c.CustomStyle != null ? c.CustomStyle : new ShapeStyle());
    }

    /// <summary>
    /// Initializes a component before running a report.
    /// </summary>
    public virtual void InitializeComponent()
    {
    }

    /// <summary>
    /// Finalizes a component before running a report.
    /// </summary>
    public virtual void FinalizeComponent()
    {
    }

    /// <summary>
    /// Saves the state of this component.
    /// </summary>
    public virtual void SaveState()
    {
      FSaveValue = Value;
    }

    /// <summary>
    /// Restores the state of this component.
    /// </summary>
    public virtual void RestoreState()
    {
      Value = FSaveValue;
    }
    #endregion // Public Methods

    #region ICustomTypeDescriptor Members
    /// <inheritdoc/>
    public PropertyDescriptorCollection GetProperties()
    {
      return GetProperties(null);
    }

    /// <inheritdoc/>
    public PropertyDescriptorCollection GetProperties(Attribute[] attr)
    {
      PropertyDescriptorCollection typeProps = TypeDescriptor.GetProperties(this.GetType(), attr);
      PropertyDescriptorCollection properties = new PropertyDescriptorCollection(null);
      foreach (PropertyDescriptor desc in typeProps)
      {
        properties.Add(desc);
      }
      foreach (string key in SpatialData.GetKeys())
      {
        properties.Add(new MetadataPropertyDescriptor(SpatialData, key, attr));
      } 
      return properties;
    }

    /// <inheritdoc/>
    public String GetClassName()
    {
      return TypeDescriptor.GetClassName(this, true);
    }

    /// <inheritdoc/>
    public AttributeCollection GetAttributes()
    {
      return TypeDescriptor.GetAttributes(this, true);
    }

    /// <inheritdoc/>
    public String GetComponentName()
    {
      return TypeDescriptor.GetComponentName(this, true);
    }

    /// <inheritdoc/>
    public TypeConverter GetConverter()
    {
      return TypeDescriptor.GetConverter(this, true);
    }

    /// <inheritdoc/>
    public EventDescriptor GetDefaultEvent()
    {
      return TypeDescriptor.GetDefaultEvent(this, true);
    }

    /// <inheritdoc/>
    public PropertyDescriptor GetDefaultProperty()
    {
      return TypeDescriptor.GetDefaultProperty(this, true);
    }

    /// <inheritdoc/>
    public object GetEditor(Type editorBaseType)
    {
      return TypeDescriptor.GetEditor(this, editorBaseType, true);
    }

    /// <inheritdoc/>
    public EventDescriptorCollection GetEvents(Attribute[] attributes)
    {
      return TypeDescriptor.GetEvents(this, attributes, true);
    }

    /// <inheritdoc/>
    public EventDescriptorCollection GetEvents()
    {
      return TypeDescriptor.GetEvents(this, true);
    }

    /// <inheritdoc/>
    public object GetPropertyOwner(PropertyDescriptor pd)
    {
      return this;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ShapeBase"/> class.
    /// </summary>
    public ShapeBase()
    {
      FVisible = true;
      FSpatialData = new ShapeSpatialData();
      FValue = double.NaN;
      FShapeScale = 1;
    }
  }
}
