using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Globalization;

namespace FastReport.Map.Import.Osm
{
    internal class OsmMapImport : MapImport
    {
        #region Fields

        private MapObject map;
        private MapLayer layer;
        private XmlDocument document;
        private Dictionary<ulong, OsmNode> nodes;
        private Dictionary<ulong, OsmWay> ways;
        private Dictionary<ulong, OsmRelation> relations;
        private double normalScale;
        private BoundingBox realOsmBox;

        #endregion // Fields

        #region Constructors

        public OsmMapImport()
        {
            map = new MapObject();
            document = new XmlDocument();
            nodes = new Dictionary<ulong, OsmNode>();
            ways = new Dictionary<ulong, OsmWay>();
            relations = new Dictionary<ulong, OsmRelation>();
            normalScale = 0.0;
            realOsmBox = new BoundingBox();
            map.MercatorProjection = false;
        }

        #endregion // Constructors

        #region Private Methods

        private double CalculateNormalScale()
        {
            double scaleByWidth = map.Width / (realOsmBox.MaxX - realOsmBox.MinX);
            double scaleByHeight = map.Height / (realOsmBox.MaxY - realOsmBox.MinY);
            return scaleByWidth < scaleByHeight ? scaleByWidth : scaleByHeight;
        }

        private void NormalizeLayerBox()
        {
            layer.Box.MinX = 0.0;
            layer.Box.MinY = 0.0;
            layer.Box.MaxX = (realOsmBox.MaxX - realOsmBox.MinX) * normalScale;
            layer.Box.MaxY = (realOsmBox.MaxY - realOsmBox.MinY) * normalScale;
        }

        private double ConvertToDouble(string value)
        {
            return Double.Parse(value, CultureInfo.InvariantCulture);
        }

        private void LoadBounds()
        {
            XmlNode bounds = document.GetElementsByTagName("bounds")[0];
            realOsmBox.MinX = ConvertToDouble(bounds.Attributes["minlon"].Value);
            realOsmBox.MinY = ConvertToDouble(bounds.Attributes["minlat"].Value);
            realOsmBox.MaxX = ConvertToDouble(bounds.Attributes["maxlon"].Value);
            realOsmBox.MaxY = ConvertToDouble(bounds.Attributes["maxlat"].Value);
            normalScale = CalculateNormalScale();
        }

        private void NormalizeNodes()
        {
            foreach (OsmNode node in nodes.Values)
            {
                node.Lon = (node.Lon - realOsmBox.MinX) * normalScale;
                node.Lat = (node.Lat - realOsmBox.MinY) * normalScale;
            }
        }

        private void LoadNodes()
        {
            foreach (XmlNode node in document.GetElementsByTagName("node"))
            {
                ulong id = Convert.ToUInt64(node.Attributes["id"].Value);
                double lon = ConvertToDouble(node.Attributes["lon"].Value);
                double lat = ConvertToDouble(node.Attributes["lat"].Value);
                Dictionary<string, string> tags = new Dictionary<string, string>();
                foreach (XmlNode tagNode in node.ChildNodes)
                {
                    if (tagNode.Name == "tag")
                    {
                        tags.Add(tagNode.Attributes["k"].Value, tagNode.Attributes["v"].Value);
                    }
                }
                nodes.Add(id, new OsmNode(lat, lon, tags));
            }
            NormalizeNodes();
        }

        private void LoadWays()
        {
            foreach (XmlNode node in document.GetElementsByTagName("way"))
            {
                ulong id = Convert.ToUInt64(node.Attributes["id"].Value);
                List<ulong> refs = new List<ulong>();
                Dictionary<string, string> tags = new Dictionary<string, string>();
                foreach (XmlNode refNode in node.ChildNodes)
                {
                    if (refNode.Name == "nd")
                    {
                        refs.Add(Convert.ToUInt64(refNode.Attributes["ref"].Value));
                    }
                    if (refNode.Name == "tag")
                    {
                        tags.Add(refNode.Attributes["k"].Value, refNode.Attributes["v"].Value);
                    }
                }
                ways.Add(id, new OsmWay(refs, tags));
            }
        }

        private void LoadRelations()
        {
            foreach (XmlNode node in document.GetElementsByTagName("relation"))
            {
                ulong id = Convert.ToUInt64(node.Attributes["id"].Value);
                List<ulong> nodeRefs = new List<ulong>();
                List<ulong> wayRefs = new List<ulong>();
                foreach (XmlNode refNode in node.ChildNodes)
                {
                    if (refNode.Name == "member")
                    {
                        switch (refNode.Attributes["type"].Value)
                        {
                            case "node":
                                nodeRefs.Add(Convert.ToUInt64(refNode.Attributes["ref"].Value));
                                break;
                            case "way":
                                wayRefs.Add(Convert.ToUInt64(refNode.Attributes["ref"].Value));
                                break;
                        }
                    }
                }
                relations.Add(id, new OsmRelation(nodeRefs, wayRefs));
            }
        }

        private void ConvertWays()
        {
            List<ShapePolyLine> lines = new List<ShapePolyLine>();
            List<ShapePolygon> polygons = new List<ShapePolygon>();
            foreach (OsmWay way in ways.Values)
            {
                bool isPolygon = false;
                ShapePolygon shape = null;
                if (way.NodeRefs[0] == way.NodeRefs[way.NodeRefs.Count - 1])
                {
                    shape = new ShapePolygon();
                    isPolygon = true;
                }
                else
                {
                    shape = new ShapePolyLine();
                    isPolygon = false;
                }
                shape.Parts.Clear();
                PointD[] part = new PointD[way.NodeRefs.Count];
                shape.Parts.Add(part);
                for (int i = 0; i < way.NodeRefs.Count; i++)
                {
                    PointD point = new PointD();
                    point.X = nodes[way.NodeRefs[i]].Lon;
                    point.Y = nodes[way.NodeRefs[i]].Lat;
                    part[i] = point;
                    nodes[way.NodeRefs[i]].HasParent = true;
                }
                foreach (KeyValuePair<string, string> tag in way.Tags)
                {
                    shape.SpatialData.SetValue(tag.Key, tag.Value);
                }
                if (isPolygon)
                {
                    polygons.Add((ShapePolygon)shape);
                }
                else
                {
                    lines.Add((ShapePolyLine)shape);
                }
            }
            if (lines.Count > 0)
            {
                layer = new MapLayer();
                NormalizeLayerBox();
                layer.Type = LayerType.Line;
                map.Layers.Add(layer);
                foreach (ShapePolyLine line in lines)
                {
                    line.Box = layer.Box;
                    layer.Shapes.Add(line);
                }
            }
            if (polygons.Count > 0)
            {
                layer = new MapLayer();
                NormalizeLayerBox();
                layer.Type = LayerType.Polygon;
                map.Layers.Add(layer);
                foreach (ShapePolygon polygon in polygons)
                {
                    polygon.Box = layer.Box;
                    layer.Shapes.Add(polygon);
                }
            }
        }

        private void ConvertNodes()
        {
            layer = new MapLayer();
            NormalizeLayerBox();
            layer.Type = LayerType.Point;
            map.Layers.Add(layer);
            foreach (OsmNode node in nodes.Values)
            {
                if (!node.HasParent)
                {
                    ShapePoint point = new ShapePoint();
                    point.X = node.Lon;
                    point.Y = node.Lat;
                    foreach (KeyValuePair<string, string> tag in node.Tags)
                    {
                        point.SpatialData.SetValue(tag.Key, tag.Value);
                    }
                    layer.Shapes.Add(point);
                }
            }
        }

        private void LoadMap()
        {
            LoadBounds();
            LoadNodes();
            LoadWays();
            ConvertWays();
            ConvertNodes();
        }

        #endregion // Private Methods

        #region Public Methods

        public override void ImportMap(MapObject map, MapLayer layer, string filename)
        {
            try
            {
                this.map = map;
                this.layer = layer;
                document.Load(filename);
                LoadMap();
            }
            catch
            {
                if (!FastReport.Utils.Config.WebMode)
                {
                    MessageBox.Show(new FastReport.Utils.MyRes("Messages").Get("WrongFileFormat") + ".");
                }
            }
        }

        #endregion // Public Methods
    }
}
