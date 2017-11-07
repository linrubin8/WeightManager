using System;
using System.IO;
using System.Xml;

namespace FastReport.Export.Svg
{
    public partial class SVGExport : ExportBase
    {

        /// <summary>
        /// Add image in Base64
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        /// <param name="imageStream"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void AddImage(string filename, string format, MemoryStream imageStream, float left, float top, float width, float height)
        {
            if (!String.IsNullOrEmpty(filename))
            {
                XmlElement image = doc.CreateElement("image");
                byte[] imageBytes = imageStream.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);

                AppndAttr(image, "xlink", "href", "http://www.w3.org/1999/xlink", "data:image/" + format
                    + "; base64," + base64String);
                AppndAttr(image, "x", FloatToString(left));
                AppndAttr(image, "y", FloatToString(top));
                if (width != 0)
                    AppndAttr(image, "width", FloatToString(width));
                if (height != 0)
                    AppndAttr(image, "height", FloatToString(height));
                g.AppendChild(image);
            }
        }

        /// <summary>
        /// Add image
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void AddImage(string filename, string format, float left, float top, float width, float height)
        {
            if (!String.IsNullOrEmpty(filename))
            {
                XmlElement image = doc.CreateElement("image");
                AppndAttr(image, "xlink", "href", "http://www.w3.org/1999/xlink", filename + "." + format);
                AppndAttr(image, "x", FloatToString(left));
                AppndAttr(image, "y", FloatToString(top));
                if (width != 0)
                    AppndAttr(image, "width", FloatToString(width));
                if (height != 0)
                    AppndAttr(image, "height", FloatToString(height));
                g.AppendChild(image);
            }
        }
    }
}
