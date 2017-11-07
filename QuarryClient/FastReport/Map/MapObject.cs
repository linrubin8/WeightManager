using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.IO;
using FastReport.Forms;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.Map.Import.Shp;
using FastReport.Map.Import.Osm;
using FastReport.Map.Forms;

namespace FastReport.Map
{
  /// <summary>
  /// Represents a map object.
  /// </summary>
  public class MapObject : ReportComponentBase, IParent, IHasEditor
  {
    #region Fields
    private float FScale;
    private float FZoom;
    private float FMinZoom;
    private float FMaxZoom;
    private float FOffsetX;
    private float FOffsetY;
    private LayerCollection FLayers;
    private ColorScale FColorScale;
    private bool FIsPanning;
    private bool FPanned;
    private Padding FPadding;
    private bool FMercatorProjection;
    private Point FLastMousePoint;
    private bool FNeedDesignerModify;
    private bool FNeedPreviewPageModify;
    private ShapeBase FHotPoint;
    private int FDoubleClickTickCount;
    private PointF FDoubleClickPos;
    #endregion // Fields

    #region Properties
    /// <summary>
    /// Gets or sets the path to folder containing shapefiles.
    /// </summary>
    /// <remarks>
    /// This property is used by the map editor when selecting a shapefile.
    /// </remarks>
    public static string ShapefileFolder;

    /// <summary>
    /// Gets or sets the map zoom.
    /// </summary>
    [DefaultValue(1.0f)]
    [Category("Appearance")]
    public float Zoom
    {
      get { return FZoom; }
      set
      {
        if (value < MinZoom)
          value = MinZoom;
        if (value > MaxZoom)
          value = MaxZoom;
        FZoom = value;
      }
    }

    /// <summary>
    /// Gets or sets minimum zoom value.
    /// </summary>
    [DefaultValue(1.0f)]
    [Category("Appearance")]
    public float MinZoom
    {
      get { return FMinZoom; }
      set { FMinZoom = value; }
    }

    /// <summary>
    /// Gets or sets maximum zoom value.
    /// </summary>
    [DefaultValue(50f)]
    [Category("Appearance")]
    public float MaxZoom
    {
      get { return FMaxZoom; }
      set { FMaxZoom = value; }
    }

    /// <summary>
    /// Gets or sets the X offset of the map.
    /// </summary>
    [DefaultValue(0f)]
    [Category("Layout")]
    public float OffsetX
    {
      get { return FOffsetX; }
      set { FOffsetX = value; }
    }

    /// <summary>
    /// Gets or sets the Y offset of the map.
    /// </summary>
    [DefaultValue(0f)]
    [Category("Layout")]
    public float OffsetY
    {
      get { return FOffsetY; }
      set { FOffsetY = value; }
    }

    /// <summary>
    /// Gets or sets the value indicating that mercator projection must be used to view the map.
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    public bool MercatorProjection
    {
      get { return FMercatorProjection; }
      set { FMercatorProjection = value; }
    }

    /// <summary>
    /// Gets the color scale settings.
    /// </summary>
    [Category("Appearance")]
    public ColorScale ColorScale
    {
      get { return FColorScale; }
    }

    /// <summary>
    /// Gets or sets a collection of map layers.
    /// </summary>
    [Browsable(false)]
    public LayerCollection Layers
    {
      get { return FLayers; }
      set { FLayers = value; }
    }

    /// <summary>
    /// Gets or sets padding within the map.
    /// </summary>
    [Category("Layout")]
    public Padding Padding
    {
      get { return FPadding; }
      set { FPadding = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool CanGrow
    {
      get { return base.CanGrow; }
      set { base.CanGrow = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool CanShrink
    {
      get { return base.CanShrink; }
      set { base.CanShrink = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new string Style
    {
      get { return base.Style; }
      set { base.Style = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new string EvenStyle
    {
      get { return base.EvenStyle; }
      set { base.EvenStyle = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new StylePriority EvenStylePriority
    {
      get { return base.EvenStylePriority; }
      set { base.EvenStylePriority = value; }
    }

    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new string HoverStyle
    {
      get { return base.HoverStyle; }
      set { base.HoverStyle = value; }
    }

    /// <inheritdoc/>
    public override bool IsSelected
    {
      get
      {
        if (Report == null)
          return false;
        return Report.Designer.SelectedObjects.IndexOf(this) != -1 || IsInternalSelected;
      }
    }

    internal float ScaleG
    {
      get { return FScale * Zoom; }
    }

    internal float OffsetXG
    {
      get { return -((Width * Zoom - Width) / 2) + FOffsetX * Zoom; }
    }

    internal float OffsetYG
    {
      get { return -((Height * Zoom - Height) / 2) + FOffsetY * Zoom; }
    }

    internal ShapeBase HotPoint
    {
      get { return FHotPoint; }
      set 
      { 
        if (FHotPoint != value)
          Page.Refresh();
        FHotPoint = value; 
      }
    }

    private bool IsInternalSelected
    {
      get
      {
        if (Report == null)
          return false;
        SelectedObjectCollection selection = Report.Designer.SelectedObjects;
        return selection.Count > 0 && (
          (selection[0] is MapLayer && (selection[0] as MapLayer).Map == this) ||
          (selection[0] is ShapeBase && (selection[0] as ShapeBase).Map == this));
      }
    }

    private bool IsEmpty
    {
      get { return Layers.Count == 0; }
    }
    #endregion // Properties

    #region Private Methods
    private void DrawLayers(FRPaintEventArgs e)
    {
      Graphics g = e.Graphics;
      RectangleF drawRect = new RectangleF(
        (AbsLeft + Padding.Left) * e.ScaleX,
        (AbsTop + Padding.Top) * e.ScaleY,
        (Width - Padding.Horizontal) * e.ScaleX,
        (Height - Padding.Vertical) * e.ScaleY);
      GraphicsState state = g.Save();
      try
      {
        g.SetClip(drawRect);
        foreach (MapLayer layer in FLayers)
        {
          layer.SetPrinting(IsPrinting);
          layer.Draw(e);
        }
      }
      finally
      {
        g.Restore(state);
      }
    }

    private void DrawScales(FRPaintEventArgs e)
    {
      ColorScale.Data = null;
      
      // find the layer which ColorRanges is set to show in the color scale
      foreach (MapLayer layer in Layers)
      {
        if (layer.ColorRanges.ShowInColorScale)
        {
          ColorScale.Data = layer.ColorRanges;
          break;
        }
      }
      
      if (ColorScale.Visible)
        ColorScale.Draw(e, this);
    }

    private void DrawMap(FRPaintEventArgs e)
    {
      SmoothingMode saveMode = e.Graphics.SmoothingMode;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

      if (Layers.Count == 0)
      {
        if (IsDesigning)
        {
          string s = Res.Get("ComponentsMisc,Map,Hint");
          e.Graphics.DrawString(s, DrawUtils.DefaultReportFont, Brushes.Black,
            new RectangleF(AbsLeft * e.ScaleX, AbsTop * e.ScaleY, Width * e.ScaleX, Height * e.ScaleY),
            e.Cache.GetStringFormat(StringAlignment.Center, StringAlignment.Center, StringTrimming.None, StringFormatFlags.NoClip, 0, 0));
        }
        return;
      }
      
      MapLayer layer = Layers[0];
      double layerWidth = layer.Box.MaxX - layer.Box.MinX;
      double layerHeight = MercatorProjection ?
        CoordinateConverter.ConvertMercator(layer.Box.MaxY) - CoordinateConverter.ConvertMercator(layer.Box.MinY) :
        layer.Box.MaxY - layer.Box.MinY;

      float scaleX = (Width - Padding.Horizontal) / (float)layerWidth;
      float scaleY = (Height - Padding.Vertical) / (float)layerHeight;
      FScale = scaleX < scaleY ? scaleX : scaleY;

      if (IsDesigning)
      {
        SaveState();
        GenerateRandomData();
      }
      
      try
      {
        DrawLayers(e);
        e.Graphics.SmoothingMode = saveMode;

        DrawScales(e);
      }
      finally
      {
        if (IsDesigning)
          RestoreState();
      }
    }

    private void GenerateRandomData()
    {
      if (IsEmpty)
        return;

      foreach (MapLayer layer in Layers)
      {
        layer.InitializeData();

        if (!String.IsNullOrEmpty(layer.SpatialColumn))
        {
          double value = 0;
          foreach (ShapeBase shape in layer.Shapes)
          {
            layer.AddValue(shape.SpatialValue, value);
            value += 50;
          }
        }
        else
        {
          layer.AddValue("1", 0);
          layer.AddValue("2", 1000);
        }

        layer.FinalizeData();
      }
    }

    private void ZoomIn()
    {
      Zoom *= 1.2f;
    }

    private void ZoomOut()
    {
      Zoom /= 1.2f;
    }

    private Base HitTest(PointF point)
    {
      for (int i = Layers.Count - 1; i >= 0; i--)
      {
        ShapeBase shape = Layers[i].HitTest(point);
        if (shape != null)
          return shape;
      }
      return null;
    }
    #endregion // Private Methods

    #region IParent Members
    /// <inheritdoc/>
    public bool CanContain(Base child)
    {
      return child is MapLayer;
    }

    /// <inheritdoc/>
    public void GetChildObjects(ObjectCollection list)
    {
      foreach (MapLayer layer in FLayers)
      {
        list.Add(layer);
      }
    }

    /// <inheritdoc/>
    public void AddChild(Base child)
    {
      if (child is MapLayer)
        FLayers.Add(child as MapLayer);
    }

    /// <inheritdoc/>
    public void RemoveChild(Base child)
    {
      if (child is MapLayer)
        FLayers.Remove(child as MapLayer);
    }

    /// <inheritdoc/>
    public int GetChildOrder(Base child)
    {
      if (child is MapLayer)
        return FLayers.IndexOf(child as MapLayer);
      return 0;
    }

    /// <inheritdoc/>
    public void SetChildOrder(Base child, int order)
    {
      int oldOrder = child.ZOrder;
      if (oldOrder != -1 && order != -1 && order != oldOrder)
      {
        if (child is MapLayer)
        {
          if (order > FLayers.Count)
          {
            order = FLayers.Count;
          }
          if (order >= oldOrder)
          {
            order--;
          }
          FLayers.Remove(child as MapLayer);
          FLayers.Insert(order, child as MapLayer);
        }
      }
    }

    /// <inheritdoc/>
    public void UpdateLayout(float dx, float dy)
    {
      // do nothing
    }

    #endregion // IParent Members

    #region Report Engine
    /// <inheritdoc/>
    public override void SaveState()
    {
      base.SaveState();
      foreach (MapLayer layer in Layers)
      {
        layer.SaveState();
      }
    }

    /// <inheritdoc/>
    public override void RestoreState()
    {
      base.RestoreState();
      foreach (MapLayer layer in Layers)
      {
        layer.RestoreState();
      }
    }
    
    /// <inheritdoc/>
    public override void GetData()
    {
      base.GetData();
      foreach (MapLayer layer in Layers)
      {
        layer.GetData();
      }
    }

    /// <inheritdoc/>
    public override void InitializeComponent()
    {
      base.InitializeComponent();
      foreach (MapLayer layer in Layers)
      {
        layer.InitializeComponent();
      }
    }

    /// <inheritdoc/>
    public override void FinalizeComponent()
    {
      base.FinalizeComponent();
      foreach (MapLayer layer in Layers)
      {
        layer.FinalizeComponent();
      }
    }
    #endregion // Report Engine

    #region Public Methods
    /// <inheritdoc/>
    public override void Assign(Base source)
    {
      base.Assign(source);
      MapObject src = source as MapObject;
      MinZoom = src.MinZoom;
      MaxZoom = src.MaxZoom;
      Zoom = src.Zoom;
      OffsetX = src.OffsetX;
      OffsetY = src.OffsetY;
      Padding = src.Padding;
      MercatorProjection = src.MercatorProjection;
      ColorScale.Assign(src.ColorScale);
    }

    /// <inheritdoc/>
    public override void Draw(FRPaintEventArgs e)
    {
      base.Draw(e);
      DrawMap(e);
      DrawMarkers(e);
      Border.Draw(e, new RectangleF(AbsLeft, AbsTop, Width, Height));
      if (IsDesigning && IsSelected)
        e.Graphics.DrawImage(Res.GetImage(75), (int)(AbsLeft * e.ScaleX + 8), (int)(AbsTop * e.ScaleY - 8));
    }

    /// <inheritdoc/>
    public override void Serialize(FRWriter writer)
    {
      MapObject c = writer.DiffObject as MapObject;
      base.Serialize(writer);

      if (MinZoom != c.MinZoom)
        writer.WriteFloat("MinZoom", MinZoom);
      if (MaxZoom != c.MaxZoom)
        writer.WriteFloat("MaxZoom", MaxZoom);
      if (Zoom != c.Zoom)
        writer.WriteFloat("Zoom", Zoom);
      if (OffsetX != c.OffsetX)
        writer.WriteFloat("OffsetX", OffsetX);
      if (OffsetY != c.OffsetY)
        writer.WriteFloat("OffsetY", OffsetY);
      if (Padding != c.Padding)
        writer.WriteValue("Padding", Padding);
      if (MercatorProjection != c.MercatorProjection)
        writer.WriteBool("MercatorProjection", MercatorProjection);
      ColorScale.Serialize(writer, "ColorScale", c.ColorScale);
    }

    /// <summary>
    /// Loads a map from file.
    /// </summary>
    /// <param name="filename">Name of file that contains a map.</param>
    public void Load(string filename)
    {
      string extension = Path.GetExtension(filename).ToLower();
      if (extension == ".shp")
      {
          using (ShpMapImport import = new ShpMapImport())
          {
              import.ImportMap(this, null, filename);
              if (Layers.Count > 0)
                  Layers[Layers.Count - 1].Simplify(0.03);
              CreateUniqueNames();
          }
      }
      else if (extension == ".osm")
      {
          FMercatorProjection = false;
          using (OsmMapImport import = new OsmMapImport())
          {
              import.ImportMap(this, null, filename);
              if (Layers.Count > 0)
                  Layers[Layers.Count - 1].Simplify(0.03);
              CreateUniqueNames();
          }
      }
    }

    /// <summary>
    /// Creates unique names for all contained objects such as layers, shapes, etc.
    /// </summary>
    public void CreateUniqueNames()
    {
      if (Report != null)
      {
        FastNameCreator nameCreator = new FastNameCreator(Report.AllNamedObjects);
        foreach (MapLayer layer in FLayers)
        {
          if (String.IsNullOrEmpty(layer.Name))
            nameCreator.CreateUniqueName(layer);
          layer.CreateUniqueNames();
        }
      }
    }

    /// <inheritdoc/>
    public override SizeF GetPreferredSize()
    {
      if ((Page as ReportPage).IsImperialUnitsUsed)
        return new SizeF(Units.Inches * 4, Units.Inches * 4f);
      return new SizeF(Units.Millimeters * 80, Units.Millimeters * 80);
    }

    /// <inheritdoc/>
    public bool InvokeEditor()
    {
      using (MapEditorForm form = new MapEditorForm())
      {
        form.Map = this;
        return form.ShowDialog() == DialogResult.OK;
      }
    }

    /// <inheritdoc/>
    public override void OnBeforeInsert(int flags)
    {
      base.OnBeforeInsert(flags);
      // fill is reset by the designer's default formatting tool. Set it back.
      Fill = new SolidFill(Color.Gainsboro);
    }
    #endregion // Public Methods

    #region Designer mouse support
    /// <inheritdoc/>
    public override void HandleMouseHover(FRMouseEventArgs e)
    {
      if (IsSelected && new RectangleF(AbsLeft + 8, AbsTop - 8, 16, 16).Contains(new PointF(e.X, e.Y)))
      {
        e.Handled = true;
        e.Cursor = Cursors.SizeAll;
      }
    }

    /// <inheritdoc/>
    public override void HandleMouseDown(FRMouseEventArgs e)
    {
      // allow doubleclick when polygon is selected
      bool doubleClick = Environment.TickCount - FDoubleClickTickCount < SystemInformation.DoubleClickTime &&
        new PointF(e.X, e.Y).Equals(FDoubleClickPos);
      FDoubleClickTickCount = Environment.TickCount;
      FDoubleClickPos = new PointF(e.X, e.Y);

      if (e.Mode != WorkspaceMode2.None)
        return;

      // check move handle
      HandleMouseHover(e);
      if (e.Handled)
      {
        // do base logic such as selecting/deselecting
        // and return with e.Mode = WorkspaceMode2.Move
        base.HandleMouseDown(e);
        e.Handled = true;
        e.Mode = WorkspaceMode2.Move;
      }
      else if (PointInObject(new PointF(e.X, e.Y)))
      {
        e.Handled = true;

        // hit test polygons
        Base obj = HitTest(new PointF(e.X, e.Y));
        // pass rightclick and doubleclick to the map object
        if (obj == null || doubleClick || e.Button == MouseButtons.Right)
          obj = this;

        SelectedObjectCollection selection = Report.Designer.SelectedObjects;
        if (e.ModifierKeys == Keys.Shift)
        {
          // toggle selection
          if (selection.IndexOf(obj) != -1)
          {
            if (selection.Count > 1)
              selection.Remove(obj);
          }
          else
            selection.Add(obj);
        }
        else
        {
          // select the object if not selected yet
          if (selection.IndexOf(obj) == -1)
          {
            selection.Clear();
            selection.Add(obj);
          }
        }

        e.Mode = WorkspaceMode2.Custom;
        e.ActiveObject = this;
        FIsPanning = true;
        FPanned = false;
        e.Delta = new PointF(0, 0);
      }
    }

    /// <inheritdoc/>
    public override void HandleMouseMove(FRMouseEventArgs e)
    {
      base.HandleMouseMove(e);
      if (!e.Handled && e.Button == MouseButtons.None)
      {
        // don't process if mouse is over move area
        HandleMouseHover(e);
        if (e.Handled)
        {
          e.Handled = false;
          return;
        }
        if (PointInObject(new PointF(e.X, e.Y)))
        {
          e.Handled = true;
        }
        else
        {
          // mouse leave, save changes if any
          if (FNeedDesignerModify)
          {
            Report.Designer.SetModified(this, "Change", Name);
            FNeedDesignerModify = false;
          }
        }
      }

      if (FIsPanning && !IsEmpty)
      {
        OffsetX += e.Delta.X / Zoom;
        OffsetY += e.Delta.Y / Zoom;
        FPanned = true;
      }
    }

    /// <inheritdoc/>
    public override void HandleMouseUp(FRMouseEventArgs e)
    {
      base.HandleMouseUp(e);
      if (FIsPanning)
      {
        if (FPanned)
          FNeedDesignerModify = true;
      }
      FIsPanning = false;
      FPanned = false;
    }

    /// <inheritdoc/>
    public override void HandleMouseWheel(FRMouseEventArgs e)
    {
      if (IsSelected && !IsEmpty)
      {
        if (e.WheelDelta < 0)
          ZoomOut();
        else
          ZoomIn();
        FNeedDesignerModify = true;
        e.Handled = true;
      }
    }
    #endregion

    #region Preview mouse support
    /// <inheritdoc/>
    public override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      FLastMousePoint = e.Location;
      FIsPanning = true;
      FPanned = false;
    }

    /// <inheritdoc/>
    public override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!IsEmpty)
      {
        if (FIsPanning)
        {
          int deltaX = e.X - FLastMousePoint.X;
          int deltaY = e.Y - FLastMousePoint.Y;
          if (Math.Abs(deltaX) > 3 || Math.Abs(deltaY) > 3)
          {
            OffsetX += deltaX / Zoom;
            OffsetY += deltaY / Zoom;
            FPanned = true;
            FLastMousePoint = e.Location;
            FNeedPreviewPageModify = true;
            Page.Refresh();
          }
        }
        else
        {
          if (Hyperlink.Kind == HyperlinkKind.DetailPage || Hyperlink.Kind == HyperlinkKind.DetailReport)
          {
            foreach (MapLayer layer in Layers)
            {
              ShapeBase shape = layer.HitTest(new PointF(e.X + AbsLeft, e.Y + AbsTop));
              if (shape != null && !shape.IsValueEmpty)
              {
                HotPoint = shape;
                Hyperlink.Value = HotPoint.SpatialValue;
                Cursor = Cursors.Hand;
                return;
              }
            }
          
            HotPoint = null;
            Hyperlink.Value = "";
            Cursor = Cursors.Default;
          }
        }
      }
    }

    /// <inheritdoc/>
    public override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      // prevent hyperlink invoke while panning
      if (FPanned)
        Hyperlink.Value = "";
      FIsPanning = false;
      FPanned = false;
    }

    /// <inheritdoc/>
    public override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if (e.Delta < 0)
        ZoomOut();
      else
        ZoomIn();
      FNeedPreviewPageModify = true;
    }

    /// <inheritdoc/>
    public override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      FNeedPreviewPageModify = false;
    }

    /// <inheritdoc/>
    public override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      HotPoint = null;
      if (FNeedPreviewPageModify)
        Page.Modify();
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="MapObject"/> class.
    /// </summary>
    public MapObject()
    {
      FLayers = new LayerCollection(this);
      FZoom = 1;
      FMinZoom = 1;
      FMaxZoom = 50;
      FPadding = new Padding();
      FColorScale = new ColorScale();
      FColorScale.Dock = ScaleDock.BottomLeft;
      FMercatorProjection = true;
      Fill = new SolidFill(Color.Gainsboro);
      SetFlags(Flags.InterceptsPreviewMouseEvents, true);
      FlagProvidesHyperlinkValue = true;
    }

  }
}
