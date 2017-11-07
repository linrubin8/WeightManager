using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.Design.ImportPlugins.ListAndLabel
{
    /// <summary>
    /// Represents the List and Label import plugin.
    /// </summary>
    public class ListAndLabelImportPlugin : ImportPlugin
    {
        #region Fields

        private ReportPage page;
        //private ComponentBase component;
        //private Base parent;
        private string textLL;
        private Font defaultFont;
        private Color defaultTextColor;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAndLabelImportPlugin"/> class.
        /// </summary>
        public ListAndLabelImportPlugin() : base()
        {
            textLL = "";
            defaultFont = new Font("Arial", 10.0f, FontStyle.Regular);
            defaultTextColor = Color.Black;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAndLabelImportPlugin"/> class with a specified designer.
        /// </summary>
        /// <param name="designer">The report designer.</param>
        public ListAndLabelImportPlugin(Designer designer) : base(designer)
        {
            textLL = "";
            defaultFont = new Font("Arial", 10.0f, FontStyle.Regular);
            defaultTextColor = Color.Black;
        }

        #endregion // Constructors

        #region Private Methods

        private string GetValueLL(string str)
        {
            int index = textLL.IndexOf(str) + str.Length + 1;
            int length = textLL.IndexOf("\r\n", index) - index;
            return textLL.Substring(index, length);
        }

        private string GetValueLL(string str, int startIndex)
        {
            int index = textLL.IndexOf(str, startIndex) + str.Length + 1;
            int length = textLL.IndexOf("\r\n", index) - index;
            return textLL.Substring(index, length);
        }

        private string RemoveQuotes(string str)
        {
            return str.Replace("\"", "");
        }

        private void LoadReportInfo()
        {
            int index = textLL.IndexOf("Text=", textLL.IndexOf("[Description]")) + 5;
            int length = textLL.IndexOf("\r\n", index) - index;
            Report.ReportInfo.Description = textLL.Substring(index, length);
        }

        private void LoadPageSettings()
        {
            page.PaperWidth = UnitsConverter.LLUnitsToMillimeters(GetValueLL("PaperFormat.cx"));
            page.PaperHeight = UnitsConverter.LLUnitsToMillimeters(GetValueLL("PaperFormat.cy"));
            page.Landscape = UnitsConverter.ConvertPaperOrientation(GetValueLL("PaperFormat.Orientation"));
            page.TopMargin = page.LeftMargin = page.RightMargin = page.BottomMargin = 0.0f;
        }

        private void LoadDefaultFont()
        {
            if (UnitsConverter.ConvertBool(GetValueLL("DefaultFont/Default")))
            {
                string fontFamily = GetValueLL("DefaultFont/FaceName");
                float fontSize = Convert.ToSingle(GetValueLL("DefaultFont/Size"));
                FontStyle fontStyle = FontStyle.Regular;
                if (UnitsConverter.ConvertBool(GetValueLL("DefaultFont/Bold")))
                {
                    fontStyle |= FontStyle.Bold;
                }
                if (UnitsConverter.ConvertBool(GetValueLL("DefaultFont/Italic")))
                {
                    fontStyle |= FontStyle.Italic;
                }
                if (UnitsConverter.ConvertBool(GetValueLL("DefaultFont/Underline")))
                {
                    fontStyle |= FontStyle.Underline;
                }
                if (UnitsConverter.ConvertBool(GetValueLL("DefaultFont/Strikeout")))
                {
                    fontStyle |= FontStyle.Strikeout;
                }
                defaultFont = new Font(fontFamily, fontSize, fontStyle);
                defaultTextColor = Color.FromName(GetValueLL("DefaultFont/Color=LL.Color"));
            }
        }

        private List<int> GetAllObjectsLL()
        {
            List<int> list = new List<int>();
            int firstIndex = textLL.IndexOf("[Object]");
            int lastIndex = textLL.LastIndexOf("[Object]");
            int currentIndex = firstIndex;
            if (currentIndex >= 0)
            {
                do
                {
                    list.Add(currentIndex);
                    currentIndex = textLL.IndexOf("[Object]", currentIndex + 1);
                }
                while (currentIndex < lastIndex);
            }
            if (firstIndex != lastIndex)
            {
                list.Add(lastIndex);
            }
            return list;
        }

        private void LoadComponent(int startIndex, ComponentBase comp)
        {
            comp.Name = GetValueLL("Identifier", startIndex);
            if (String.IsNullOrEmpty(comp.Name))
            {
                comp.CreateUniqueName();
            }
            comp.Left = UnitsConverter.LLUnitsToPixels(GetValueLL("Position/Left", startIndex));
            comp.Top = UnitsConverter.LLUnitsToPixels(GetValueLL("Position/Top", startIndex));
            comp.Width = UnitsConverter.LLUnitsToPixels(GetValueLL("Position/Width", startIndex));
            comp.Height = UnitsConverter.LLUnitsToPixels(GetValueLL("Position/Height", startIndex));
        }

        private Font LoadFont(int startIndex)
        {
            int index = textLL.IndexOf("[Font]", startIndex);
            if (!UnitsConverter.ConvertBool(GetValueLL("Default", index)))
            {
                string fontFamily = RemoveQuotes(GetValueLL("FaceName", index));
                float fontSize = Convert.ToSingle(GetValueLL("Size", index));
                FontStyle fontStyle = FontStyle.Regular;
                if (UnitsConverter.ConvertBool(GetValueLL("Bold", index)))
                {
                    fontStyle |= FontStyle.Bold;
                }
                if (UnitsConverter.ConvertBool(GetValueLL("Italic", index)))
                {
                    fontStyle |= FontStyle.Italic;
                }
                if (UnitsConverter.ConvertBool(GetValueLL("Underline", index)))
                {
                    fontStyle |= FontStyle.Underline;
                }
                if (UnitsConverter.ConvertBool(GetValueLL("Strikeout", index)))
                {
                    fontStyle |= FontStyle.Strikeout;
                }
                return new Font(fontFamily, fontSize, fontStyle);
            }
            return defaultFont;
        }

        private void LoadBorder(int startIndex, Border border)
        {
            if (UnitsConverter.ConvertBool(GetValueLL("Frame/Left/Line", startIndex)))
            {
                border.Lines |= BorderLines.Left;
                border.LeftLine.Color = Color.FromName(GetValueLL("Frame/Left/Line/Color=LL.Color", startIndex));
                border.LeftLine.Style = UnitsConverter.ConvertLineType(GetValueLL("Frame/Left/Line/LineType", startIndex));
                border.LeftLine.Width = UnitsConverter.LLUnitsToPixels(GetValueLL("Frame/Left/LineWidth", startIndex));
            }
            if (UnitsConverter.ConvertBool(GetValueLL("Frame/Top/Line", startIndex)))
            {
                border.Lines |= BorderLines.Top;
                border.TopLine.Color = Color.FromName(GetValueLL("Frame/Top/Line/Color=LL.Color", startIndex));
                border.TopLine.Style = UnitsConverter.ConvertLineType(GetValueLL("Frame/Top/Line/LineType", startIndex));
                border.TopLine.Width = UnitsConverter.LLUnitsToPixels(GetValueLL("Frame/Top/LineWidth", startIndex));
            }
            if (UnitsConverter.ConvertBool(GetValueLL("Frame/Right/Line", startIndex)))
            {
                border.Lines |= BorderLines.Right;
                border.RightLine.Color = Color.FromName(GetValueLL("Frame/Right/Line/Color=LL.Color", startIndex));
                border.RightLine.Style = UnitsConverter.ConvertLineType(GetValueLL("Frame/Right/Line/LineType", startIndex));
                border.RightLine.Width = UnitsConverter.LLUnitsToPixels(GetValueLL("Frame/Right/LineWidth", startIndex));
            }
            if (UnitsConverter.ConvertBool(GetValueLL("Frame/Bottom/Line", startIndex)))
            {
                border.Lines |= BorderLines.Bottom;
                border.BottomLine.Color = Color.FromName(GetValueLL("Frame/Bottom/Line/Color=LL.Color", startIndex));
                border.BottomLine.Style = UnitsConverter.ConvertLineType(GetValueLL("Frame/Bottom/Line/LineType", startIndex));
                border.BottomLine.Width = UnitsConverter.LLUnitsToPixels(GetValueLL("Frame/Bottom/LineWidth", startIndex));
            }
        }

        private void LoadTextObject(int startIndex, TextObject textObj)
        {
            LoadComponent(startIndex, textObj);
            textObj.Font = LoadFont(startIndex);
            textObj.TextColor = defaultTextColor;
            int fontIndex = textLL.IndexOf("[Font]", startIndex);
            if (!UnitsConverter.ConvertBool(GetValueLL("Default", fontIndex)))
            {
                textObj.TextColor = Color.FromName(GetValueLL("Color=LL.Color", fontIndex));
            }
            textObj.HorzAlign = UnitsConverter.ConvertTextAlign(GetValueLL("Align", fontIndex));
            textObj.Text = RemoveQuotes(GetValueLL("Text", fontIndex));
            LoadBorder(startIndex, textObj.Border);
        }

        private void LoadLineObject(int startIndex, LineObject lineObj)
        {
            LoadComponent(startIndex, lineObj);
            int colorIndex = textLL.IndexOf("FgColor", startIndex);
            lineObj.Border.Color = Color.FromName(GetValueLL("FgColor=LL.Color", colorIndex));
            lineObj.Border.Style = UnitsConverter.ConvertLineType(GetValueLL("LineType", colorIndex));
            lineObj.Border.Width = UnitsConverter.LLUnitsToPixels(GetValueLL("Width", colorIndex));
            if (lineObj.Width > 0 || lineObj.Height > 0)
            {
                lineObj.Diagonal = true;
            }
            LoadBorder(startIndex, lineObj.Border);
        }

        private void LoadShapeObject(int startIndex, ShapeObject shapeObj)
        {
            LoadComponent(startIndex, shapeObj);
            int colorIndex = textLL.IndexOf("FgColor", startIndex);
            shapeObj.Border.Color = Color.FromName(GetValueLL("FgColor=LL.Color", colorIndex));
            shapeObj.Border.Width = UnitsConverter.LLUnitsToPixels(GetValueLL("Width", colorIndex));
            shapeObj.Border.Style = UnitsConverter.ConvertLineType(GetValueLL("LineType", colorIndex));
            shapeObj.FillColor = Color.FromName(GetValueLL("BkColor=LL.Color", colorIndex));
        }

        private void LoadRectangle(int startIndex, ShapeObject shapeObj)
        {
            LoadShapeObject(startIndex, shapeObj);
            float curve = UnitsConverter.ConvertRounding(GetValueLL("Rounding", startIndex));
            if (curve == 0)
            {
                shapeObj.Shape = ShapeKind.Rectangle;
            }
            else
            {
                shapeObj.Shape = ShapeKind.RoundRectangle;
                shapeObj.Curve = curve;
            }
        }

        private void LoadEllipse(int startIndex, ShapeObject shapeObj)
        {
            LoadShapeObject(startIndex, shapeObj);
            shapeObj.Shape = ShapeKind.Ellipse;
        }

        private void LoadPictureObject(int startIndex, PictureObject pictureObj)
        {
            LoadComponent(startIndex, pictureObj);
            if (UnitsConverter.ConvertBool(GetValueLL("OriginalSize", startIndex)))
            {
                pictureObj.SizeMode = PictureBoxSizeMode.Normal;
            }
            if (Convert.ToInt32(GetValueLL("Alignment", startIndex)) == 0)
            {
                pictureObj.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            if (UnitsConverter.ConvertBool(GetValueLL("bIsotropic", startIndex)))
            {
                pictureObj.SizeMode = PictureBoxSizeMode.AutoSize;
            }
            else
            {
                pictureObj.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            string filename = GetValueLL("Filename", startIndex);
            if (!String.IsNullOrEmpty(filename))
            {
                pictureObj.ImageLocation = filename;
            }
            LoadBorder(startIndex, pictureObj.Border);
        }

        private void LoadObjects()
        {
            DataBand band = ComponentsFactory.CreateDataBand(page);
            band.Height = page.PaperHeight * Units.Millimeters;
            List<int> objects = GetAllObjectsLL();
            foreach (int index in objects)
            {
                string objectName = GetValueLL("ObjectName", index);
                switch (objectName)
                {
                    case "Text":
                        TextObject textObj = ComponentsFactory.CreateTextObject("", band);
                        LoadTextObject(index, textObj);
                        break;
                    case "Line":
                        LineObject lineObj = ComponentsFactory.CreateLineObject("", band);
                        LoadLineObject(index, lineObj);
                        break;
                    case "Rectangle":
                        ShapeObject rectangle = ComponentsFactory.CreateShapeObject("", band);
                        LoadRectangle(index, rectangle);
                        break;
                    case "Ellipse":
                        ShapeObject ellipse = ComponentsFactory.CreateShapeObject("", band);
                        LoadEllipse(index, ellipse);
                        break;
                    case "Picture":
                        PictureObject pictureObj = ComponentsFactory.CreatePictureObject("", band);
                        LoadPictureObject(index, pictureObj);
                        break;
                }
            }
        }

        private void LoadReport()
        {
            page = ComponentsFactory.CreateReportPage(Report);
            LoadReportInfo();
            LoadPageSettings();
            LoadDefaultFont();
            LoadObjects();
        }

        #endregion // Private Methods

        #region Protected Methods

        ///<inheritdoc/>
        protected override string GetFilter()
        {
            return new FastReport.Utils.MyRes("FileFilters").Get("ListAndLabelFiles");
        }

        #endregion // Protected Methods

        #region Public Methods

        ///<inheritdoc/>
        public override void LoadReport(Report report, string filename)
        {
            Report = report;
            Report.Clear();
            textLL = File.ReadAllText(filename);
            LoadReport();
            page = null;
        }

        #endregion // Public Methods
    }
}
