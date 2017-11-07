using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.Map.Import.Shp
{
  internal class ShpMapImport : MapImport
  {
    #region Fields
    private MapObject map;
    private Stream stream;
    private MapLayer layer;
    #endregion // Fields

    #region Private Methods
    private void LoadFileHeader()
    {
      byte[] buffer4 = new byte[4];
      byte[] buffer8 = new byte[8];

      stream.Seek(0, SeekOrigin.Begin);
      stream.Read(buffer4, 0, buffer4.Length);
      Array.Reverse(buffer4);
      bool isFileCodeOK = BitConverter.ToInt32(buffer4, 0) == 9994;

      stream.Seek(24, SeekOrigin.Begin);
      stream.Read(buffer4, 0, buffer4.Length);
      Array.Reverse(buffer4);
      bool isFileLengthOK = (BitConverter.ToInt32(buffer4, 0) * 2) == stream.Length;

      stream.Seek(32, SeekOrigin.Begin);
      stream.Read(buffer4, 0, buffer4.Length);
      int shapeType = BitConverter.ToInt32(buffer4, 0);
      bool isShapeTypeOK = shapeType == 1 || shapeType == 3 || shapeType == 5 || shapeType == 8;

      if (isFileCodeOK && isFileLengthOK && isShapeTypeOK)
      {
        if (layer == null)
        {
          layer = new MapLayer();
          map.Layers.Add(layer);
        }
        switch (shapeType)
        {
          case 1:
            layer.Type = LayerType.Point;
            break;
          case 3:
            layer.Type = LayerType.Line;
            break;
          case 5:
            layer.Type = LayerType.Polygon;
            break;
        }
        stream.Seek(36, SeekOrigin.Begin);
        layer.Box.Load(stream);
      }
    }

    private int LoadRecordHeader()
    {
      byte[] buffer4 = new byte[4];
      stream.Read(buffer4, 0, buffer4.Length);
      Array.Reverse(buffer4);
      int recordNumber = BitConverter.ToInt32(buffer4, 0);
      stream.Read(buffer4, 0, buffer4.Length);
      Array.Reverse(buffer4);
      int contentLength = BitConverter.ToInt32(buffer4, 0) * 2;
      return contentLength;
    }

    private ShapePoint LoadPoint()
    {
      ShapePoint point = new ShapePoint();
      point.Load(stream);
      return point;
    }

    private PointD LoadPointD()
    {
      PointD point = new PointD();
      point.Load(stream);
      return point;
    }

    private ShapePolyLine LoadArc()
    {
      ShapePolyLine line = new ShapePolyLine();
      line.Load(stream);
      return line;
    }

    private ShapePolygon LoadPolygon()
    {
      ShapePolygon polygon = new ShapePolygon();
      polygon.Load(stream);
      return polygon;
    }

    private List<ShapePoint> LoadMultiPoint()
    {
      List<ShapePoint> points = new List<ShapePoint>();

      byte[] buffer4 = new byte[4];

      stream.Seek(32, SeekOrigin.Current);
      stream.Read(buffer4, 0, buffer4.Length);
      int numPoints = BitConverter.ToInt32(buffer4, 0);
      for (int i = 0; i < numPoints; i++)
      {
        ShapePoint point = new ShapePoint();
        point.Load(stream);
        points.Add(point);
      }

      return points;
    }

    private void LoadRecordContent(int contentLength)
    {
      byte[] buffer4 = new byte[4];
      stream.Read(buffer4, 0, buffer4.Length);
      int objectType = BitConverter.ToInt32(buffer4, 0);
      if (objectType == 1 && layer.Type == LayerType.Point)
      {
        ShapePoint point = LoadPoint();
        layer.Shapes.Add(point);
      }
      else if (objectType == 3 && layer.Type == LayerType.Line)
      {
        ShapePolyLine line = LoadArc();
        layer.Shapes.Add(line);
      }
      else if (objectType == 5 && layer.Type == LayerType.Polygon)
      {
        ShapePolygon polygon = LoadPolygon();
        layer.Shapes.Add(polygon);
      }
      else if (objectType == 8 && layer.Type == LayerType.Point)
      {
        List<ShapePoint> points = LoadMultiPoint();
        foreach (ShapePoint point in points)
        {
          layer.Shapes.Add(point);
        }
      }
    }

    private void LoadRecord()
    {
      int contentLength = LoadRecordHeader();
      LoadRecordContent(contentLength);
    }

    private void LoadRecords()
    {
      stream.Seek(100, SeekOrigin.Begin);
      while (stream.Position < stream.Length)
      {
        LoadRecord();
      }
    }

    private void ImportMap(MapObject map, Stream stream)
    {
      this.map = map;
      this.stream = stream;
      LoadFileHeader();
      LoadRecords();
    }

    private void ImportDbf(string filename)
    {
      string dbfFilename = Path.ChangeExtension(filename, ".dbf");
      if (File.Exists(dbfFilename))
      {
        DbfFileImport dbfImport = new DbfFileImport();
        dbfImport.ImportFile(layer, dbfFilename);
      }
    }
    #endregion // Private Methods

    #region Protected Methods
    protected override string GetFilter()
    {
      return Res.Get("FileFilters,ShpFile");
    }
    #endregion // Protected Methods

    #region Public Methods
    public override void ImportMap(MapObject map, MapLayer layer, string filename)
    {
      this.layer = layer;
      using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
      {
        ImportMap(map, stream);
      }
      ImportDbf(filename);
    }
    #endregion // Public Methods
  }
}
