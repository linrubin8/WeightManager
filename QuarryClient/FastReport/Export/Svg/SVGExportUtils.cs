using System;
using System.Drawing;
using System.Xml;

namespace FastReport.Export.Svg
{
    public partial class SVGExport : ExportBase
    {
        #region Graphic
        private Color GetBlendColor(Color c, float Blend)
        {
            return Color.FromArgb(255, (int)Math.Round(c.R + (255 - c.R) * Blend),
                    (int)Math.Round(c.G + (255 - c.G) * Blend),
                    (int)Math.Round(c.B + (255 - c.B) * Blend));
        }
        private string GetStrokeDash(LineStyle lineStyle)
        {
            string strokeDashArray = null;
            switch (lineStyle)
            {
                case LineStyle.Dash: strokeDashArray = "5"; break;
                case LineStyle.Dot: strokeDashArray = "2"; break;
                case LineStyle.DashDot: strokeDashArray = "2 5 2"; break;
                case LineStyle.DashDotDot: strokeDashArray = "2 2 5 2 2"; break;
                case LineStyle.Double: break;
            }
            return strokeDashArray;
        }
        #endregion

        #region XML
        private void AppndAttr(XmlElement element, string attrName, string attrVal)
        {
            nsAttr = doc.CreateAttribute(attrName);
            nsAttr.Value = attrVal;
            element.Attributes.Append(nsAttr);
        }

        private void AppndAttr(XmlElement element, string prefix, string localName, string namespaceURI, string attrVal)
        {
            nsAttr = doc.CreateAttribute(prefix, localName, namespaceURI);
            nsAttr.Value = attrVal;
            element.Attributes.Append(nsAttr);
        }

        /*  private void AddMarginAttrs(float marginLeft, float marginTop, XmlElement element)
          {
              if (marginLeft != 0 || marginTop != 0)
                  AppndAttr(element, "Margin", ExportUtils.FloatToString(marginLeft) + "," + ExportUtils.FloatToString(marginTop));
          }

          private void AddSizeAttrs(float width, float height, XmlElement element)
          {
              if (width != 0)
                  AppndAttr(element, "Width", ExportUtils.FloatToString(width));
              if (height != 0)
                  AppndAttr(element, "Height", ExportUtils.FloatToString(height));
          }

          private void AddStrokeAttrs(string stroke, float strokeThickness, XmlElement element)
          {
              if (stroke != null && stroke != "Black" && stroke != "#ff000000")
                  AppndAttr(element, "Stroke", stroke);
              if (strokeThickness != 0 && strokeThickness != 1)
                  AppndAttr(element, "StrokeThickness", ExportUtils.FloatToString(strokeThickness));
          }

          private void AddXY12Attrs(float x1, float y1, float x2, float y2, XmlElement element)
          {
              if (x1 != 0)
                  AppndAttr(element, "X1", ExportUtils.FloatToString(x1));
              if (y1 != 0)
                  AppndAttr(element, "Y1", ExportUtils.FloatToString(y1));
              if (x2 != 0)
                  AppndAttr(element, "X2", ExportUtils.FloatToString(x2));
              if (y2 != 0)
                  AppndAttr(element, "Y2", ExportUtils.FloatToString(y2));
          }*/
        #endregion
        private string FloatToString(double flt)
        {
            return ExportUtils.FloatToString(flt);
        }
    }
}
