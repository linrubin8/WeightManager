using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Globalization;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.Data;
using FastReport.Map.Import.Shp;
using System.IO;

namespace FastReport.Map
{
  /// <summary>
  /// Specifies the type of objects that layer contains.
  /// </summary>
  public enum LayerType
  {
    /// <summary>
    /// The layer contains points.
    /// </summary>
    Point,

    /// <summary>
    /// The layer contains lines.
    /// </summary>
    Line,

    /// <summary>
    /// The layer contains polygons.
    /// </summary>
    Polygon
  }

  /// <summary>
  /// Specifies the spatial source for the layer.
  /// </summary>
  public enum SpatialSource
  {
    /// <summary>
    /// Source is ESRI shapefile.
    /// </summary>
    ShpFile,

    /// <summary>
    /// Source is a latitude/longitude/name provided by an application.
    /// </summary>
    ApplicationData
  }
  
  /// <summary>
  /// Determines how map labels are displayed.
  /// </summary>
  public enum MapLabelKind
  {
    /// <summary>
    /// No label displayed.
    /// </summary>
    None,
    
    /// <summary>
    /// The shape name is displayed.
    /// </summary>
    Name,
    
    /// <summary>
    /// The value is displayed.
    /// </summary>
    Value,
    
    /// <summary>
    /// Both name and value displayed.
    /// </summary>
    NameAndValue
  }

  /// <summary>
  /// Represents a map layer.
  /// </summary>
  public class MapLayer : Base, IParent, ICustomTypeDescriptor
  {
    #region Fields

    private LayerType FType;
    private SpatialSource FSpatialSource;
    private string FShapefile;
    private bool FVisible;
    private BoundingBox FBox;
    private ShapeCollection FShapes;
    private ShapeStyle FDefaultShapeStyle;
    private MapPalette FPalette;
    private DataSourceBase FDataSource;
    private string FFilter;
    // used if SpatialSource is ShpFile
    private string FSpatialColumn;
    private string FSpatialValue;
    // used if SpatialSource is ApplicationData
    private string FLatitudeValue;
    private string FLongitudeValue;
    private string FLabelValue;
    //
    private string FAnalyticalValue;
    private string FLabelColumn;
    private MapLabelKind FLabelKind;
    private string FLabelFormat;
    private TotalType FFunction;
    private ColorRanges FColorRanges;
    private SizeRanges FSizeRanges;
    private float FAccuracy;
    private float FLabelsVisibleAtZoom;
    private string FZoomPolygon;

    private SortedList<string, double> FValues;
    private SortedList<string, int> FCounts;
    #endregion // Fields

    #region Properties

    /// <summary>
    /// Gets or sets a type of layer.
    /// </summary>
    [Browsable(false)]
    public LayerType Type
    {
      get { return FType; }
      set { FType = value; }
    }

    /// <summary>
    /// Gets or sets the spatial source for the layer.
    /// </summary>
    [DefaultValue(SpatialSource.ShpFile)]
    [Category("Data")]
    public SpatialSource SpatialSource
    {
      get { return FSpatialSource; }
      set 
      { 
        FSpatialSource = value;
        if (value == SpatialSource.ApplicationData)
        {
          LabelColumn = "NAME";
          SpatialColumn = "LOCATION";
          Box.MinX = -180;
          Box.MaxX = 180;
          Box.MinY = -90;
          Box.MaxY = 83.623031;
        }
      }
    }

    /// <summary>
    /// Gets or sets the name of ESRI shapefile.
    /// </summary>
    public string Shapefile
    {
      get { return FShapefile; }
      set { FShapefile = value; }
    }

    /// <summary>
    /// Gets or sets the data source.
    /// </summary>
    [Category("Data")]
    public DataSourceBase DataSource
    {
      get { return FDataSource; }
      set { FDataSource = value; }
    }

    /// <summary>
    /// Gets or sets the datasource filter expression.
    /// </summary>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string Filter
    {
      get { return FFilter; }
      set { FFilter = value; }
    }

    /// <summary>
    /// Gets or sets spatial column name.
    /// </summary>
    /// <remarks>
    /// This property is used if the <see cref="SpatialSource"/> is set to <b>ShpFile</b>.
    /// </remarks>
    [Editor(typeof(SpatialColumnEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string SpatialColumn
    {
      get { return FSpatialColumn; }
      set { FSpatialColumn = value; }
    }

    /// <summary>
    /// Gets or sets an expression that returns spatial value.
    /// </summary>
    /// <remarks>
    /// This property is used if the <see cref="SpatialSource"/> is set to <b>ShpFile</b>.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string SpatialValue
    {
      get { return FSpatialValue; }
      set { FSpatialValue = value; }
    }

    /// <summary>
    /// Gets or sets an expression that returns latitude value.
    /// </summary>
    /// <remarks>
    /// This property is used if the <see cref="SpatialSource"/> is set to <b>ApplicationData</b>.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string LatitudeValue
    {
      get { return FLatitudeValue; }
      set { FLatitudeValue = value; }
    }

    /// <summary>
    /// Gets or sets an expression that returns longitude value.
    /// </summary>
    /// <remarks>
    /// This property is used if the <see cref="SpatialSource"/> is set to <b>ApplicationData</b>.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string LongitudeValue
    {
      get { return FLongitudeValue; }
      set { FLongitudeValue = value; }
    }

    /// <summary>
    /// Gets or sets an expression that returns label value.
    /// </summary>
    /// <remarks>
    /// This property is used if the <see cref="SpatialSource"/> is set to <b>ApplicationData</b>.
    /// </remarks>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string LabelValue
    {
      get { return FLabelValue; }
      set { FLabelValue = value; }
    }
    
    /// <summary>
    /// Gets or sets an expression that returns analytical value.
    /// </summary>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string AnalyticalValue
    {
      get { return FAnalyticalValue; }
      set { FAnalyticalValue = value; }
    }

    /// <summary>
    /// Gets or sets label's column name.
    /// </summary>
    [Editor(typeof(SpatialColumnEditor), typeof(UITypeEditor))]
    [Category("Appearance")]
    public string LabelColumn
    {
      get { return FLabelColumn; }
      set { FLabelColumn = value; }
    }

    /// <summary>
    /// Gets or sets a value that determines how map labels are displayed.
    /// </summary>
    [DefaultValue(MapLabelKind.Name)]
    [Category("Appearance")]
    public MapLabelKind LabelKind
    {
      get { return FLabelKind; }
      set { FLabelKind = value; }
    }

    /// <summary>
    /// Gets or sets the format of label's value.
    /// </summary>
    [Category("Appearance")]
    public string LabelFormat
    {
      get { return FLabelFormat; }
      set { FLabelFormat = value; }
    }

    /// <summary>
    /// Gets or sets the map accuracy. Lower value is better, but slower.
    /// </summary>
    [DefaultValue(2f)]
    [Category("Appearance")]
    public float Accuracy
    {
      get { return FAccuracy; }
      set { FAccuracy = value; }
    }

    /// <summary>
    /// Gets or sets the value that determines the labels visiblity at a certain zoom value.
    /// </summary>
    [DefaultValue(1f)]
    [Category("Appearance")]
    public float LabelsVisibleAtZoom
    {
      get { return FLabelsVisibleAtZoom; }
      set { FLabelsVisibleAtZoom = value; }
    }

    /// <summary>
    /// Gets or sets the aggregate function.
    /// </summary>
    [Category("Data")]
    [DefaultValue(TotalType.Sum)]
    public TotalType Function
    {
      get { return FFunction; }
      set { FFunction = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating that the layer is visible.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool Visible
    {
      get { return FVisible; }
      set { FVisible = value; }
    }

    /// <summary>
    /// Gets or sets a bounding box of layer.
    /// </summary>
    [Browsable(false)]
    public BoundingBox Box
    {
      get { return FBox; }
      set { FBox = value; }
    }

    /// <summary>
    /// Gets a collection of map objects.
    /// </summary>
    [Browsable(false)]
    public ShapeCollection Shapes
    {
      get { return FShapes; }
    }

    /// <summary>
    /// Gets the default style of shapes in this layer.
    /// </summary>
    [Category("Appearance")]
    public ShapeStyle DefaultShapeStyle
    {
      get { return FDefaultShapeStyle; }
    }

    /// <summary>
    /// Gets or sets the palette used to highlight shapes.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(MapPalette.None)]
    public MapPalette Palette
    {
      get { return FPalette; }
      set { FPalette = value; }
    }

    /// <summary>
    /// Gets the color ranges used to highlight shapes based on analytical value.
    /// </summary>
    [Category("Appearance")]
    public ColorRanges ColorRanges
    {
      get { return FColorRanges; }
    }

    /// <summary>
    /// Gets the size ranges used to draw points based on analytical value.
    /// </summary>
    [Category("Appearance")]
    public SizeRanges SizeRanges
    {
      get { return FSizeRanges; }
    }

    /// <summary>
    /// Gets or sets the expression that returns the name of polygon to zoom.
    /// </summary>
    [Editor(typeof(ExpressionEditor), typeof(UITypeEditor))]
    [Category("Data")]
    public string ZoomPolygon
    {
      get { return FZoomPolygon; }
      set { FZoomPolygon = value; }
    }

    /// <summary>
    /// Gets or sets the bounding box as a string.
    /// </summary>
    [Browsable(false)]
    public string BoxAsString
    {
      get { return Box.GetAsString(); }
      set { Box.SetAsString(value); }
    }

    /// <summary>
    /// Gets a reference to the Map object.
    /// </summary>
    [Browsable(false)]
    public MapObject Map
    {
      get { return Parent as MapObject; }
    }

    internal List<string> SpatialColumns
    {
      get
      {
        if (Shapes.Count > 0)
          return Shapes[0].SpatialData.GetKeys();
        return new List<string>();
      }
    }

    internal bool IsShapefileEmbedded
    {
      get { return String.IsNullOrEmpty(Shapefile); }
    }
    #endregion // Properties

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      MapLayer src = source as MapLayer;
      Shapefile = src.Shapefile;
      Type = src.Type;
      SpatialSource = src.SpatialSource;
      DataSource = src.DataSource;
      Filter = src.Filter;
      SpatialColumn = src.SpatialColumn;
      SpatialValue = src.SpatialValue;
      Function = src.Function;
      LatitudeValue = src.LatitudeValue;
      LongitudeValue = src.LongitudeValue;
      LabelValue = src.LabelValue;
      AnalyticalValue = src.AnalyticalValue;
      LabelColumn = src.LabelColumn;
      LabelKind = src.LabelKind;
      LabelFormat = src.LabelFormat;
      Accuracy = src.Accuracy;
      LabelsVisibleAtZoom = src.LabelsVisibleAtZoom;
      Visible = src.Visible;
      ZoomPolygon = src.ZoomPolygon;
      Box.Assign(src.Box);
      DefaultShapeStyle.Assign(src.DefaultShapeStyle);
      Palette = src.Palette;
      ColorRanges.Assign(src.ColorRanges);
      SizeRanges.Assign(src.SizeRanges);
    }

    /// <summary>
    /// Draws the layer.
    /// </summary>
    /// <param name="e">The drawing parameters.</param>
    public void Draw(FRPaintEventArgs e)
    {
      if (Visible)
      {
        for (int i = 0; i < Shapes.Count; i++)
        {
          ShapeBase shape = Shapes[i];
          shape.ShapeIndex = i;
          shape.SetPrinting(IsPrinting);
          if (Map.IsDesigning || shape.Visible)
            shape.Draw(e);
        }

        if (LabelKind != MapLabelKind.None && (Map.Zoom >= LabelsVisibleAtZoom))
        {
          foreach (ShapeBase shape in Shapes)
          {
            if (Map.IsDesigning || shape.Visible)
              shape.DrawLabel(e);
          }
        }
      }
    }

    /// <summary>
    /// Finds the shape under cursor.
    /// </summary>
    /// <param name="point">The cursor coordinates.</param>
    /// <returns>The <b>ShapeBase</b> object if found.</returns>
    public ShapeBase HitTest(PointF point)
    {
      if (Visible)
      {
        foreach (ShapeBase shape in Shapes)
        {
          if (shape.HitTest(point))
            return shape;
        }
      }
      return null;
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      MapLayer c = writer.DiffObject as MapLayer;
      base.Serialize(writer);

      if (Shapefile != c.Shapefile)
      {
        // when saving to the report file, convert absolute path to the shapefile to relative path
        // (based on the reportfile path).
        string value = Shapefile;
        if (writer.SerializeTo == SerializeTo.Report && Report != null && !String.IsNullOrEmpty(Report.FileName))
          value = FileUtils.GetRelativePath(Shapefile, Path.GetDirectoryName(Report.FileName));
        writer.WriteStr("Shapefile", value);
      }
      if (Type != c.Type)
        writer.WriteValue("Type", Type);
      if (SpatialSource != c.SpatialSource)
        writer.WriteValue("SpatialSource", SpatialSource);
      if (DataSource != null)
        writer.WriteRef("DataSource", DataSource);
      if (Filter != c.Filter)
        writer.WriteStr("Filter", Filter);
      if (SpatialColumn != c.SpatialColumn)
        writer.WriteStr("SpatialColumn", SpatialColumn);
      if (SpatialValue != c.SpatialValue)
        writer.WriteStr("SpatialValue", SpatialValue);
      if (Function != c.Function)
          writer.WriteValue("Function", Function);
      if (LatitudeValue != c.LatitudeValue)
        writer.WriteStr("LatitudeValue", LatitudeValue);
      if (LongitudeValue != c.LongitudeValue)
        writer.WriteStr("LongitudeValue", LongitudeValue);
      if (LabelValue != c.LabelValue)
        writer.WriteStr("LabelValue", LabelValue);
      if (AnalyticalValue != c.AnalyticalValue)
        writer.WriteStr("AnalyticalValue", AnalyticalValue);
      if (LabelColumn != c.LabelColumn)
        writer.WriteStr("LabelColumn", LabelColumn);
      if (LabelKind != c.LabelKind)
        writer.WriteValue("LabelKind", LabelKind);
      if (LabelFormat != c.LabelFormat)
        writer.WriteStr("LabelFormat", LabelFormat);
      if (Accuracy != c.Accuracy)
        writer.WriteFloat("Accuracy", Accuracy);
      if (LabelsVisibleAtZoom != c.LabelsVisibleAtZoom)
        writer.WriteFloat("LabelsVisibleAtZoom", LabelsVisibleAtZoom);
      if (Visible != c.Visible)
        writer.WriteBool("Visible", Visible);
      if (ZoomPolygon != c.ZoomPolygon)
        writer.WriteStr("ZoomPolygon", ZoomPolygon);
      if (BoxAsString != c.BoxAsString)
        writer.WriteStr("BoxAsString", BoxAsString);
      DefaultShapeStyle.Serialize(writer, "DefaultShapeStyle", c.DefaultShapeStyle);
      if (Palette != c.Palette)
        writer.WriteValue("Palette", Palette);
      ColorRanges.Serialize(writer, "ColorRanges");
      SizeRanges.Serialize(writer, "SizeRanges");

      if (writer.SerializeTo == SerializeTo.Preview && !IsShapefileEmbedded)
      {
        // write children by itself
        foreach (ShapeBase shape in Shapes)
        {
          writer.Write(shape);
        }
      }
    }

    /// <summary>
    /// Creates unique names for all contained objects such as points, lines, polygons, etc.
    /// </summary>
    public void CreateUniqueNames()
    {
      if (Report != null)
      {
        FastNameCreator nameCreator = new FastNameCreator(Report.AllNamedObjects);
        if (String.IsNullOrEmpty(Name))
          nameCreator.CreateUniqueName(this);
        foreach (ShapeBase shape in Shapes)
        {
          if (String.IsNullOrEmpty(shape.Name))
            nameCreator.CreateUniqueName(shape);
        }
      }
    }

    /// <summary>
    /// Reduces the number of points in the shapes in this layer.
    /// </summary>
    /// <param name="accuracy">The accuracy value.</param>
    public void Simplify(double accuracy)
    {
      if (accuracy <= 0)
        return;
      foreach (ShapeBase shape in Shapes)
      {
        shape.Simplify(accuracy);
      }
    }

    private void Zoom_Shape(string value)
    {
      foreach (ShapeBase shape in Shapes)
      {
        if (shape.SpatialValue == value)
        {
          ShapePolygon shapePoly = shape as ShapePolygon;
          using (Bitmap bmp = new Bitmap(1, 1))
          using (Graphics g = Graphics.FromImage(bmp))
          using (GraphicCache cache = new GraphicCache())
          {
            FRPaintEventArgs args = new FRPaintEventArgs(g, 1, 1, cache);
            Map.OffsetX = 0;
            Map.OffsetY = 0;
            Map.Zoom = 1;
            Map.Draw(args);
            // this will calculate largestBoundsRect
            shapePoly.GetGraphicsPath(args).Dispose();
            RectangleF largestBoundsRect = shapePoly.largestBoundsRect;
            float distanceX = (Map.AbsLeft + Map.Width / 2) - (largestBoundsRect.Left + largestBoundsRect.Width / 2);
            float distanceY = (Map.AbsTop + Map.Height / 2) - (largestBoundsRect.Top + largestBoundsRect.Height / 2);
            float width = Map.Width - Map.Padding.Horizontal;
            float height = Map.Height - Map.Padding.Vertical;
            float zoomX = width / largestBoundsRect.Width;
            float zoomY = height / largestBoundsRect.Height;
            float zoom = Math.Min(zoomX, zoomY) * 0.95f;
            Map.OffsetX = distanceX;
            Map.OffsetY = distanceY;
            Map.Zoom = zoom;
          }
          break;
        }
      }
    }

    /// <summary>
    /// Loads the layer contents from ESRI shapefile (*.shp/*.dbf).
    /// </summary>
    /// <param name="fileName">The file name.</param>
    public void LoadShapefile(string fileName)
    {
      using (ShpMapImport import = new ShpMapImport())
      {
        import.ImportMap(Map, this, fileName);
        Simplify(0.03);
        CreateUniqueNames();
      }
    }

    /// <inheritdoc/>
    public override void OnAfterLoad()
    {
      // convert relative path to the shapefile to absolute path (based on the reportfile path).
      if (String.IsNullOrEmpty(Shapefile))
        return;

      if (!Path.IsPathRooted(Shapefile) && Report != null && !String.IsNullOrEmpty(Report.FileName))
        Shapefile = Path.GetDirectoryName(Report.FileName) + "\\" + Shapefile;
      LoadShapefile(Shapefile);
    }
    #endregion // Public Methods

    #region IParent Members
    /// <inheritdoc/>
    public bool CanContain(Base child)
    {
      return child is ShapeBase;
    }

    /// <inheritdoc/>
    public void GetChildObjects(ObjectCollection list)
    {
      if (IsShapefileEmbedded)
      {
        foreach (ShapeBase shape in Shapes)
        {
          list.Add(shape);
        }
      }
    }

    /// <inheritdoc/>
    public void AddChild(Base child)
    {
      Shapes.Add(child as ShapeBase);
    }

    /// <inheritdoc/>
    public void RemoveChild(Base child)
    {
      Shapes.Remove(child as ShapeBase);
    }

    /// <inheritdoc/>
    public int GetChildOrder(Base child)
    {
      return Shapes.IndexOf(child as ShapeBase);
    }

    /// <inheritdoc/>
    public void SetChildOrder(Base child, int order)
    {
      int oldOrder = child.ZOrder;
      if (oldOrder != -1 && order != -1 && oldOrder != order)
      {
        if (order > Shapes.Count)
          order = Shapes.Count;
        if (oldOrder <= order)
          order--;
        Shapes.Remove(child as ShapeBase);
        Shapes.Insert(order, child as ShapeBase);
      }
    }

    /// <inheritdoc/>
    public void UpdateLayout(float dx, float dy)
    {
      // do nothing
    }
    #endregion

    #region Report Engine
    internal void SaveState()
    {
      ColorRanges.SaveState();
      SizeRanges.SaveState();
      foreach (ShapeBase shape in Shapes)
      {
        shape.SaveState();
      }
    }

    internal void RestoreState()
    {
      ColorRanges.RestoreState();
      SizeRanges.RestoreState();
      foreach (ShapeBase shape in Shapes)
      {
        shape.RestoreState();
      }
    }
    
    internal void InitializeComponent()
    {
      foreach (ShapeBase shape in Shapes)
      {
        shape.InitializeComponent();
      }
    }

    internal void FinalizeComponent()
    {
      foreach (ShapeBase shape in Shapes)
      {
        shape.FinalizeComponent();
      }
    }

    /// <inheritdoc/>
    public override string[] GetExpressions()
    {
      List<string> expressions = new List<string>();

      if (!String.IsNullOrEmpty(Filter))
        expressions.Add(Filter);
      if (!String.IsNullOrEmpty(SpatialValue))
        expressions.Add(SpatialValue);
      if (!String.IsNullOrEmpty(LatitudeValue))
        expressions.Add(LatitudeValue);
      if (!String.IsNullOrEmpty(LongitudeValue))
        expressions.Add(LongitudeValue);
      if (!String.IsNullOrEmpty(LabelValue))
        expressions.Add(LabelValue);
      if (!String.IsNullOrEmpty(AnalyticalValue))
        expressions.Add(AnalyticalValue);
      if (!String.IsNullOrEmpty(ZoomPolygon))
        expressions.Add(ZoomPolygon);
      
      return expressions.ToArray();
    }

    internal void GetData()
    {
      InitializeData();

      if (DataSource != null)
      {
        DataSource.Init(Filter);
        DataSource.First();

        while (DataSource.HasMoreRows)
        {
          if (SpatialSource == SpatialSource.ShpFile)
          {
            if (!String.IsNullOrEmpty(SpatialValue) && !String.IsNullOrEmpty(AnalyticalValue))
            {
              object spatialValue = Report.Calc(SpatialValue);
              object analyticalValue = Report.Calc(AnalyticalValue);
              if (spatialValue != null && !(spatialValue is DBNull) &&
                analyticalValue != null && !(analyticalValue is DBNull))
              {
                AddValue(spatialValue.ToString(), Convert.ToDouble(analyticalValue));
              }
            }
          }
          else
          {
            if (!String.IsNullOrEmpty(LatitudeValue) && !String.IsNullOrEmpty(LongitudeValue) && 
              !String.IsNullOrEmpty(LabelValue) && !String.IsNullOrEmpty(AnalyticalValue))
            {
              object latitudeValue = Report.Calc(LatitudeValue);
              object longitudeValue = Report.Calc(LongitudeValue);
              object labelValue = Report.Calc(LabelValue);
              object analyticalValue = Report.Calc(AnalyticalValue);
              if (latitudeValue != null && !(latitudeValue is DBNull) &&
                longitudeValue != null && !(longitudeValue is DBNull) &&
                labelValue != null && !(labelValue is DBNull) &&
                analyticalValue != null && !(analyticalValue is DBNull))
              {
                AddValue(Convert.ToDouble(latitudeValue), Convert.ToDouble(longitudeValue), labelValue.ToString(), Convert.ToDouble(analyticalValue));
              }
            }
          }

          DataSource.Next();
        }
      }
      
      FinalizeData();
    }

    internal void InitializeData()
    {
      FValues.Clear();
      FCounts.Clear();
      if (SpatialSource == SpatialSource.ApplicationData)
        Shapes.Clear();
    }

    internal void FinalizeData()
    {
      double min = 1e10;
      double max = -1e10;
      IList<string> spatialValues = FValues.Keys;

      // finalize avg calculation, find min and max
      for (int i = 0; i < spatialValues.Count; i++)
      {
        string spatialValue = spatialValues[i];
        double analyticalValue = FValues[spatialValue];
        if (Function == TotalType.Avg)
        {
          analyticalValue = analyticalValue / FCounts[spatialValue];
          FValues[spatialValue] = analyticalValue;
        }

        if (analyticalValue < min)
          min = analyticalValue;
        if (analyticalValue > max)
          max = analyticalValue;
      }

      if (spatialValues.Count > 0)
      {
        ColorRanges.Fill(min, max);
        SizeRanges.Fill(min, max);
      }

      // set shape values
      foreach (ShapeBase shape in Shapes)
      {
        string spatialValue = shape.SpatialValue;
        if (FValues.ContainsKey(spatialValue))
          shape.Value = FValues[spatialValue];
      }

      if (!String.IsNullOrEmpty(ZoomPolygon))
      {
        object zoomShape = Report.Calc(ZoomPolygon);
        if (zoomShape != null && !(zoomShape is DBNull))
          Zoom_Shape(zoomShape.ToString());
      }
    }

    /// <summary>
    /// Adds application provided data.
    /// </summary>
    /// <param name="latitude">Latitude value.</param>
    /// <param name="longitude">Longitude value.</param>
    /// <param name="name">The name displayed as a label.</param>
    /// <param name="analyticalValue">Analytical value.</param>
    /// <remarks>
    /// Use this method if the <see cref="SpatialSource"/> is set to <b>ApplicationData</b>.
    /// </remarks>
    public void AddValue(double latitude, double longitude, string name, double analyticalValue)
    {
      string spatialValue = latitude.ToString(CultureInfo.InvariantCulture.NumberFormat) + "," +
        longitude.ToString(CultureInfo.InvariantCulture.NumberFormat);
      
      if (!FValues.ContainsKey(spatialValue))
      {
        ShapePoint point = new ShapePoint();
        point.X = longitude;
        point.Y = latitude;
        point.SpatialData.SetValue("NAME", name);
        point.SpatialData.SetValue("LOCATION", spatialValue);
        Shapes.Add(point);
      }

      AddValue(spatialValue, analyticalValue);
    }
    
    /// <summary>
    /// Adds a spatial/analytical value pair to the list.
    /// </summary>
    /// <param name="spatialValue">The spatial value.</param>
    /// <param name="analyticalValue">The analytical value.</param>
    /// <remarks>
    /// Use this method if the <see cref="SpatialSource"/> is set to <b>ShpFile</b>.
    /// </remarks>
    public void AddValue(string spatialValue, double analyticalValue)
    {
      if (!FValues.ContainsKey(spatialValue))
      {
        if (Function == TotalType.Count)
          analyticalValue = 1;
        FValues.Add(spatialValue, analyticalValue);
        FCounts.Add(spatialValue, 1);
        return;
      }

      double value = FValues[spatialValue];
      switch (Function)
      {
        case TotalType.Sum:
        case TotalType.Avg:
          value += analyticalValue;
          break;

        case TotalType.Max:
          if (analyticalValue > value)
            value = analyticalValue;
          break;

        case TotalType.Min:
          if (analyticalValue < value)
            value = analyticalValue;
          break;

        case TotalType.Count:
          value++;
          break;
      }

      FValues[spatialValue] = value;
      FCounts[spatialValue]++;
    }
    #endregion

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
        bool skip = false;
        if (SpatialSource == SpatialSource.ShpFile)
        {
          skip = desc.Name == "LatitudeValue" || desc.Name == "LongitudeValue" || 
            desc.Name == "LabelValue" || desc.Name == "LabelsVisibleAtZoom";
        }
        else if (SpatialSource == SpatialSource.ApplicationData)
        {
          skip = desc.Name == "SpatialColumn" || desc.Name == "SpatialValue" || 
            desc.Name == "LabelColumn" || desc.Name == "Accuracy";
        }

        if (!skip)
          properties.Add(desc);
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
    /// Initializes a new instance of the <see cref="MapLayer"/> class.
    /// </summary>
    public MapLayer()
    {
      FVisible = true;
      FShapefile = "";
      FShapes = new ShapeCollection(this);
      FBox = new BoundingBox();
      FValues = new SortedList<string, double>();
      FCounts = new SortedList<string, int>();
      FFunction = TotalType.Sum;
      FFilter = "";
      FSpatialColumn = "";
      FSpatialValue = "";
      FLatitudeValue = "";
      FLongitudeValue = "";
      FLabelValue = "";
      FAnalyticalValue = "";
      FLabelColumn = "";
      FLabelFormat = "";
      FDefaultShapeStyle = new ShapeStyle();
      FColorRanges = new ColorRanges();
      FSizeRanges = new SizeRanges();
      FLabelKind = MapLabelKind.Name;
      FAccuracy = 2;
      FLabelsVisibleAtZoom = 1;
      FZoomPolygon = "";
      BaseName = "Layer";
      SetFlags(Flags.CanShowChildrenInReportTree, false);
    }
  }
}
