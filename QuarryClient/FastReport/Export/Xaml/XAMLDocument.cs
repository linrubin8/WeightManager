using System;
using System.Xml;
using System.IO;

namespace FastReport.Export.XAML
{
    /// <summary>
    /// Contains Dashes enum
    /// </summary>
    internal enum Dashes
    {
        /// <summary>
        /// Specifies the Dash.
        /// </summary>
        Dash,

        /// <summary>
        /// Specifies the Dot.
        /// </summary>
        Dot,

        /// <summary>
        /// Specifies the DashDot.
        /// </summary>
        DashDot,

        /// <summary>
        /// Specifies the DashDotDot.
        /// </summary>
        DashDotDot,

        /// <summary>
        /// Specifies the Double line.
        /// </summary>
        Double
    }

    /// <summary>
    /// XAML generator
    /// </summary>
    internal class XAMLDocument
    {
        #region Private fields
        private XmlAttribute nsAttr;
        private XmlElement root;
        private XmlElement Grid;
        private XmlElement Resources;
        private XmlElement Canvas;
        private XmlElement StackPanel;
        private XmlElement ScrollViewer;
        private XmlDocument doc = new XmlDocument();

        private float width;
        private float height;
        private int pages_count;
        private string fill;
        private bool TextObjectStyle;
        private bool TextObjectAngleStyle;
        private bool LineStyle;
        private bool RectangleStyle;
        private bool EllipseStyle;
        private bool PolygonStyle;
        private bool isScrolled = false; //{ get; set; }
        #endregion      

        #region Private Methods

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
        private void AddMarginAttrs(float marginLeft, float marginTop, XmlElement element)
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
        }


        /// <summary>
        /// Create Window.
        /// </summary>
        private void CreateWindow(string name)
        {
            root = doc.CreateElement("Page");
            AppndAttr(root, "xmlns", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            AppndAttr(root, "xmlns", "x", "http://www.w3.org/2000/xmlns/", "http://schemas.microsoft.com/winfx/2006/xaml");
            AppndAttr(root, "x", "Name", "http://schemas.microsoft.com/winfx/2006/xaml", "Window");
            AppndAttr(root, "Title", "MainWindow");
            AppndAttr(root, "Background", fill);        
            if (!isScrolled)
            {
                AppndAttr(root, "Width", ExportUtils.FloatToString(width));
                AppndAttr(root, "Height", ExportUtils.FloatToString(height * pages_count));
            }
            doc.AppendChild(root);
        }

        ///<summary>
        ///Create Grid.
        /// </summary>
        private void AddGrid()
        {
            Grid = doc.CreateElement("Grid");
            AppndAttr(Grid, "x", "Name", "http://schemas.microsoft.com/winfx/2006/xaml", "LayoutRoot");
            root.AppendChild(Grid);
        }

        ///<summary>
        ///Create Canvas.
        /// </summary>
        private void AddCanvas(int PageNum)
        {
            Canvas = doc.CreateElement("Canvas");
            if (PageNum != 0)
                AppndAttr(Canvas, "Margin", "0, " + ExportUtils.FloatToString(height) + ", 0, 0");
            StackPanel.AppendChild(Canvas);
        }

        /// <summary>
        /// Create StackPanel
        /// </summary>
        private void AddStackPanel(XmlElement _root)
        {
            StackPanel = doc.CreateElement("StackPanel");
            if (isScrolled)
            {
                AppndAttr(StackPanel, "Width", ExportUtils.FloatToString(width));
                AppndAttr(StackPanel, "Height", ExportUtils.FloatToString(height * pages_count));               
            }
            _root.AppendChild(StackPanel);
        }
        /// <summary>
        /// Create StackPanel
        /// </summary>
        private void AddScrollViewer()
        {
            ScrollViewer = doc.CreateElement("ScrollViewer");
            AppndAttr(ScrollViewer, "HorizontalScrollBarVisibility", "auto");
            AppndAttr(ScrollViewer, "VerticalScrollBarVisibility", "auto");
            root.AppendChild(ScrollViewer);
        }

        #region add resources
        ///<summary>
        ///Create Resources tag
        ///</summary>
        private void AddResources()
        {
            Resources = doc.CreateElement("Page.Resources");
            root.AppendChild(Resources);
        }

        ///<summary>
        ///Add resource for TextObject
        ///</summary>
        private void AddResourceTextObject()
        {
            XmlElement Style = doc.CreateElement("Style");
            AppndAttr(Style, "TargetType", "TextBox");
            AppndAttr(Style, "x", "Key", "http://schemas.microsoft.com/winfx/2006/xaml", "rTxt");
            Resources.AppendChild(Style);
            XmlElement Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "Background");
            AppndAttr(Setter, "Value", "{x:Null}");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "BorderBrush");
            AppndAttr(Setter, "Value", "Black");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "BorderThickness");
            AppndAttr(Setter, "Value", "0");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "FontSize");
            AppndAttr(Setter, "Value", "10pt");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "FontFamily");
            AppndAttr(Setter, "Value", "Arial");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "Padding");
            AppndAttr(Setter, "Value", "2,0,2,0");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "TextWrapping");
            AppndAttr(Setter, "Value", "Wrap");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "IsHitTestVisible");
            AppndAttr(Setter, "Value", "false");
            Style.AppendChild(Setter);      
        }

        ///<summary>
        ///Add resource for TextObject with angle
        ///</summary>
        private void AddResourceTextObjectAngle()
        {
            XmlElement Style = doc.CreateElement("Style");
            AppndAttr(Style, "TargetType", "UserControl");
            AppndAttr(Style, "x", "Key", "http://schemas.microsoft.com/winfx/2006/xaml", "rTxtE");
            Resources.AppendChild(Style);
            XmlElement Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "Background");
            AppndAttr(Setter, "Value", "{x:Null}");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "BorderBrush");
            AppndAttr(Setter, "Value", "Black");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "BorderThickness");
            AppndAttr(Setter, "Value", "0");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "FontSize");
            AppndAttr(Setter, "Value", "10pt");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "FontFamily");
            AppndAttr(Setter, "Value", "Arial");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "Padding");
            AppndAttr(Setter, "Value", "2,0,2,0");
            Style.AppendChild(Setter);
            Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "IsHitTestVisible");
            AppndAttr(Setter, "Value", "false");
            Style.AppendChild(Setter);
        }

        ///<summary>
        ///Add resource for Line
        ///</summary>
        private void AddResourceLine()
        {
            XmlElement Style = doc.CreateElement("Style");
            AppndAttr(Style, "TargetType", "Line");
            AppndAttr(Style, "x", "Key", "http://schemas.microsoft.com/winfx/2006/xaml", "rLn");
            Resources.AppendChild(Style);
            XmlElement Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "Stroke");
            AppndAttr(Setter, "Value", "Black");
            Style.AppendChild(Setter);
        }

        ///<summary>
        ///Add resource for Rectangle
        ///</summary>
        private void AddResourceRectangle()
        {
            XmlElement Style = doc.CreateElement("Style");
            AppndAttr(Style, "TargetType", "Rectangle");
            AppndAttr(Style, "x", "Key", "http://schemas.microsoft.com/winfx/2006/xaml", "rRc");
            Resources.AppendChild(Style);
            XmlElement Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "Stroke");
            AppndAttr(Setter, "Value", "Black");
            Style.AppendChild(Setter);
        }

        ///<summary>
        ///Add resource for Ellipse
        ///</summary>
        private void AddResourceEllipse()
        {
            XmlElement Style = doc.CreateElement("Style");
            AppndAttr(Style, "TargetType", "Ellipse");
            AppndAttr(Style, "x", "Key", "http://schemas.microsoft.com/winfx/2006/xaml", "rEl");
            Resources.AppendChild(Style);
            XmlElement Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "Stroke");
            AppndAttr(Setter, "Value", "Black");
            Style.AppendChild(Setter);
        }

        ///<summary>
        ///Add resource for Polygon
        ///</summary>
        private void AddResourcePolygon()
        {
            XmlElement Style = doc.CreateElement("Style");
            AppndAttr(Style, "TargetType", "Polygon");
            AppndAttr(Style, "x", "Key", "http://schemas.microsoft.com/winfx/2006/xaml", "rPl");
            Resources.AppendChild(Style);
            XmlElement Setter = doc.CreateElement("Setter");
            AppndAttr(Setter, "Property", "Stroke");
            AppndAttr(Setter, "Value", "Black");
            Style.AppendChild(Setter);
        }
        #endregion

        private XmlElement CrtTOFrame(float MarginLeft, float MarginTop, float Width, float Height,
        string HorizontalAlignment, string VerticalAlignment, string BorderBrush, float BorderThickness,
        float LeftLine, float TopLine, float RightLine, float BottomLine, string LeftLineDashStile,
        string TopLineDashStile, string RightLineDashStile, string BottomLineDashStile, string colorLeftLine,
        string colorTopLine, string colorRightLine, string colorBottomLine, bool Shadow, string ShadowColor,
        float ShadowWidth, string Background, string BorderLines
        , float PaddingLeft, float PaddingTop,
        float PaddingRight, float PaddingBottom, bool Glass, string colorTop,  string elementName)
        {
            XmlElement element = doc.CreateElement(elementName);           

            MarginLeft = MarginLeft + BorderThickness;
            MarginTop = MarginTop + BorderThickness;
            if (MarginLeft != 0 || MarginTop != 0)
                AppndAttr(element, "Margin", ExportUtils.FloatToString(MarginLeft) + "," +
                    ExportUtils.FloatToString(MarginTop));

            Width = Width + BorderThickness;
            Height = Height + BorderThickness;
            if (Width != 0)
                AppndAttr(element, "Width", ExportUtils.FloatToString(Width));
            if (Height != 0)
                AppndAttr(element, "Height", ExportUtils.FloatToString(Height));
            if (HorizontalAlignment != "Left" && HorizontalAlignment != null && HorizontalAlignment != "" && HorizontalAlignment != "Justify")
                AppndAttr(element, "HorizontalAlignment", HorizontalAlignment);
            if (VerticalAlignment != "Top" && VerticalAlignment != null && VerticalAlignment != "")
                AppndAttr(element, "VerticalAlignment", VerticalAlignment);
            // BorderThickness
            string[] masLines = BorderLines.Split(',', ' ');//names lines borders 
            string AllBorderThickness = null;//set lines
            bool All = false;
            bool None = false;
            bool Left = false;
            bool Right = false;
            bool Top = false;
            bool Bottom = false;
            for (int i = 0; i < masLines.Length; i++)
            {
                if (masLines[i] == "All")
                    All = true;
                if (masLines[i] == "None")
                    None = true;
                if (masLines[i] == "Left")
                    Left = true;
                if (masLines[i] == "Right")
                    Right = true;
                if (masLines[i] == "Top")
                    Top = true;
                if (masLines[i] == "Bottom")
                    Bottom = true;
            }
            if (!None)
            {
                if (Left || All)
                {
                    if (LeftLineDashStile != "Solid" && LeftLineDashStile != "Double")
                        LeftLine = 0;
                    else
                        if (LeftLine == 0) LeftLine = 1;
                    AllBorderThickness = ExportUtils.FloatToString(LeftLine);
                }
                else if (!Left)
                {
                    LeftLine = 0;
                    AllBorderThickness = "0";
                }
                if (Top || All)
                {
                    if (TopLineDashStile != "Solid" && TopLineDashStile != "Double")
                        TopLine = 0;
                    else
                        if (TopLine == 0) TopLine = 1;
                    AllBorderThickness = AllBorderThickness + "," + ExportUtils.FloatToString(TopLine);
                }
                else if (!Top)
                {
                    TopLine = 0;
                    AllBorderThickness = AllBorderThickness + "," + "0";
                }
                if (Right || All)
                {
                    if (RightLineDashStile != "Solid" && RightLineDashStile != "Double")
                        RightLine = 0;
                    else
                        if (RightLine == 0) RightLine = 1;
                    AllBorderThickness = AllBorderThickness + "," + ExportUtils.FloatToString(RightLine);
                }
                else if (!Right)
                {
                    RightLine = 0;
                    AllBorderThickness = AllBorderThickness + "," + "0";
                }
                if (Bottom || All)
                {
                    if (BottomLineDashStile != "Solid" && BottomLineDashStile != "Double")
                        BottomLine = 0;
                    else
                        if (BottomLine == 0) BottomLine = 1;
                    AllBorderThickness = AllBorderThickness + "," + ExportUtils.FloatToString(BottomLine);
                }
                else if (!Bottom)
                {
                    BottomLine = 0;
                    AllBorderThickness = AllBorderThickness + "," + "0";
                }
            }
            if (LeftLine == TopLine && TopLine == RightLine && RightLine == BottomLine && BottomLine == LeftLine)
                AllBorderThickness = ExportUtils.FloatToString(BottomLine);
            if (!None && (Left || Top || Right || Bottom || All) && !(LeftLine == 0 && TopLine == 0 && RightLine == 0 && BottomLine == 0))
                AppndAttr(element, "BorderThickness", AllBorderThickness);
            //Dash---
            if (Left || All)
            {
                if (LeftLineDashStile == "Dash")
                    AddLine(MarginLeft, MarginTop, MarginLeft, MarginTop + Height, colorLeftLine, BorderThickness, Dashes.Dash);
                if (LeftLineDashStile == "Dot")
                    AddLine(MarginLeft, MarginTop, MarginLeft, MarginTop + Height, colorLeftLine, BorderThickness, Dashes.Dot);
                if (LeftLineDashStile == "DashDot")
                    AddLine(MarginLeft, MarginTop, MarginLeft, MarginTop + Height, colorLeftLine, BorderThickness, Dashes.DashDot);
                if (LeftLineDashStile == "DashDotDot")
                    AddLine(MarginLeft, MarginTop, MarginLeft, MarginTop + Height, colorLeftLine, BorderThickness, Dashes.DashDotDot);
                if (LeftLineDashStile == "Double")
                    AddLine(MarginLeft + 4, MarginTop + 4, MarginLeft + 4, MarginTop + Height + 4, colorLeftLine, BorderThickness);
            }

            if (Top || All)
            {
                if (TopLineDashStile == "Dash")
                    AddLine(MarginLeft, MarginTop, MarginLeft + Width, MarginTop, colorTopLine, BorderThickness, Dashes.Dash);
                if (TopLineDashStile == "Dot")
                    AddLine(MarginLeft, MarginTop, MarginLeft + Width, MarginTop, colorTopLine, BorderThickness, Dashes.Dot);
                if (TopLineDashStile == "DashDot")
                    AddLine(MarginLeft, MarginTop, MarginLeft + Width, MarginTop, colorTopLine, BorderThickness, Dashes.DashDot);
                if (TopLineDashStile == "DashDotDot")
                    AddLine(MarginLeft, MarginTop, MarginLeft + Width, MarginTop, colorTopLine, BorderThickness, Dashes.DashDotDot);
                if (TopLineDashStile == "Double")
                    AddLine(MarginLeft, MarginTop, MarginLeft + Width + 4, MarginTop + 4, colorTopLine, BorderThickness);
            }

            if (Right || All)
            {
                if (RightLineDashStile == "Dash")
                    AddLine(MarginLeft + Width, MarginTop, MarginLeft + Width, MarginTop + Height, colorRightLine, BorderThickness, Dashes.Dash);
                if (RightLineDashStile == "Dot")
                    AddLine(MarginLeft + Width, MarginTop, MarginLeft + Width, MarginTop + Height, colorRightLine, BorderThickness, Dashes.Dot);
                if (RightLineDashStile == "DashDot")
                    AddLine(MarginLeft + Width, MarginTop, MarginLeft + Width, MarginTop + Height, colorRightLine, BorderThickness, Dashes.DashDot);
                if (RightLineDashStile == "DashDotDot")
                    AddLine(MarginLeft + Width, MarginTop, MarginLeft + Width, MarginTop + Height, colorRightLine, BorderThickness, Dashes.DashDotDot);
                if (RightLineDashStile == "Double")
                    AddLine(MarginLeft + Width, MarginTop, MarginLeft + Width, MarginTop + Height, colorRightLine, BorderThickness);
            }

            if (Bottom || All)
            {
                if (BottomLineDashStile == "Dash")
                    AddLine(MarginLeft, MarginTop + Height, MarginLeft + Width, MarginTop + Height, colorBottomLine, BorderThickness, Dashes.Dash);
                if (BottomLineDashStile == "Dot")
                    AddLine(MarginLeft, MarginTop + Height, MarginLeft + Width, MarginTop + Height, colorBottomLine, BorderThickness, Dashes.Dot);
                if (BottomLineDashStile == "DashDot")
                    AddLine(MarginLeft, MarginTop + Height, MarginLeft + Width, MarginTop + Height, colorBottomLine, BorderThickness, Dashes.DashDot);
                if (BottomLineDashStile == "DashDotDot")
                    AddLine(MarginLeft, MarginTop + Height, MarginLeft + Width, MarginTop + Height, colorBottomLine, BorderThickness, Dashes.DashDotDot);
                if (BottomLineDashStile == "Double")
                    AddLine(MarginLeft, MarginTop + Height, MarginLeft + Width, MarginTop + Height, colorBottomLine, BorderThickness);
            }
            if (Shadow)
            {
                AddLine(MarginLeft + ShadowWidth, MarginTop + Height + ShadowWidth / 2, MarginLeft + Width + ShadowWidth, MarginTop + Height + ShadowWidth / 2, ShadowColor, ShadowWidth);
                AddLine(MarginLeft + Width + ShadowWidth / 2, MarginTop + ShadowWidth, MarginLeft + Width + ShadowWidth / 2, MarginTop + Height + ShadowWidth, ShadowColor, ShadowWidth);
            }
            if (!None && BorderBrush != "#ff000000" && BorderBrush != null && BorderBrush != "Black")
                AppndAttr(element, "BorderBrush", BorderBrush);
            //Background
            if (Glass)
            {
                XmlElement elementBackground = doc.CreateElement(elementName + ".Background");
                element.AppendChild(elementBackground);

                XmlElement LinearGradientBrush = doc.CreateElement("LinearGradientBrush");
                AppndAttr(LinearGradientBrush, "StartPoint", "0, 0");
                AppndAttr(LinearGradientBrush, "EndPoint", "0,1");
                elementBackground.AppendChild(LinearGradientBrush);

                XmlElement GradientStop = doc.CreateElement("GradientStop");
                AppndAttr(GradientStop, "Color", colorTop);
                AppndAttr(GradientStop, "Offset", "0.5");
                LinearGradientBrush.AppendChild(GradientStop);

                GradientStop = doc.CreateElement("GradientStop");
                AppndAttr(GradientStop, "Color", Background);
                AppndAttr(GradientStop, "Offset", "0.5");
                LinearGradientBrush.AppendChild(GradientStop);
            }
            else
            if (Background != null)
                AppndAttr(element, "Background", Background);            
            //Paddings
            if (PaddingLeft != 2 || PaddingTop != 0 || PaddingRight != 2 || PaddingBottom != 0)
            {
                string Paddings;
                if (PaddingLeft == 0 && PaddingTop == 0 && PaddingRight == 0 && PaddingBottom == 0) Paddings = Convert.ToString(0);
                else if (PaddingLeft == PaddingTop && PaddingLeft == PaddingRight && PaddingLeft == PaddingBottom && PaddingLeft != 0) Paddings = ExportUtils.FloatToString(PaddingLeft);//Convert.ToString(PaddingLeft);
                else
                    Paddings = ExportUtils.FloatToString(PaddingLeft) + "," + ExportUtils.FloatToString(PaddingTop) + ","
                        + ExportUtils.FloatToString(PaddingRight) + "," + ExportUtils.FloatToString(PaddingBottom);

                AppndAttr(element, "Padding", Paddings);
            }
            return element;
        }

        private void AppendTextAttrs(string HorizontalAlignment, string VerticalAlignment, string Text,
        float FontSize, string Foreground, string FontFamily, bool Bold, bool Italic, bool Underline, 
        bool WordWrap, XmlElement element)
        {
            if (Text != null && Text != "")
                AppndAttr(element, "Text", Text);
            if (FontSize != 0 && FontSize != 10)
                AppndAttr(element, "FontSize", ExportUtils.FloatToString(FontSize) + "pt");
            if (Foreground != null && Foreground != "" && Foreground != "#ff000000")
                AppndAttr(element, "Foreground", Foreground);
            if (HorizontalAlignment != "Left" && HorizontalAlignment != null && HorizontalAlignment != "" && HorizontalAlignment != "Justify")
                AppndAttr(element, "HorizontalContentAlignment", HorizontalAlignment);
            if (VerticalAlignment != "Top" && VerticalAlignment != null && VerticalAlignment != "")
                AppndAttr(element, "VerticalContentAlignment", VerticalAlignment);
            if (FontFamily != "Arial" && FontFamily != null && FontFamily != "")
                AppndAttr(element, "FontFamily", FontFamily);
            if (Bold)
                AppndAttr(element, "FontWeight", "Bold");
            if (Italic)
                AppndAttr(element, "FontStyle", "Italic");
            if (Underline)
                AppndAttr(element, "TextDecorations", "Underline");
            if (!WordWrap)
                AppndAttr(element, "TextWrapping", "NoWrap");
        }
#endregion

        ///<summary>
        ///Add TextObject.
        /// </summary>
        internal void AddTextObject(float MarginLeft, float MarginTop, float Width, float Height,
        string HorizontalAlignment, string VerticalAlignment, string BorderBrush, float BorderThickness,
        float LeftLine, float TopLine, float RightLine, float BottomLine, string LeftLineDashStile,
        string TopLineDashStile, string RightLineDashStile, string BottomLineDashStile, string colorLeftLine,
        string colorTopLine, string colorRightLine, string colorBottomLine, bool Shadow, string ShadowColor, 
        float ShadowWidth, string Background, string BorderLines, string Text, float FontSize, string Foreground,
        string FontFamily, bool Bold, bool Italic,bool Underline, float PaddingLeft, float PaddingTop,
        float PaddingRight, float PaddingBottom, bool WordWrap, bool Glass, string colorTop)
        {
            if (TextObjectStyle == true)
            {
                AddResourceTextObject(); TextObjectStyle = false;
            }

            XmlElement TextBox = CrtTOFrame(MarginLeft, MarginTop, Width, Height,
         HorizontalAlignment, VerticalAlignment, BorderBrush, BorderThickness,
         LeftLine, TopLine, RightLine, BottomLine, LeftLineDashStile,
         TopLineDashStile, RightLineDashStile, BottomLineDashStile, colorLeftLine,
         colorTopLine, colorRightLine, colorBottomLine, Shadow, ShadowColor,
         ShadowWidth, Background, BorderLines
        , PaddingLeft, PaddingTop,
         PaddingRight, PaddingBottom, Glass, colorTop, "TextBox");

            AppendTextAttrs(HorizontalAlignment, VerticalAlignment, Text,
         FontSize, Foreground, FontFamily, Bold, Italic, Underline,
         WordWrap, TextBox);
           
            AppndAttr(TextBox, "Style", "{StaticResource rTxt}");
            Canvas.AppendChild(TextBox);
        }

        ///<summary>
        ///Method for add TextObject with angle
        /// </summary>
        internal void AddTextObject(float MarginLeft, float MarginTop, float Width, float Height,
        string HorizontalAlignment, string VerticalAlignment, string BorderBrush, float BorderThickness,
        float LeftLine, float TopLine, float RightLine, float BottomLine, string LeftLineDashStile,
        string TopLineDashStile, string RightLineDashStile, string BottomLineDashStile, string colorLeftLine,
        string colorTopLine, string colorRightLine, string colorBottomLine, bool Shadow, string ShadowColor, float ShadowWidth, string Background, string BorderLines,
        string Text, float FontSize, string Foreground, string FontFamily, bool Bold, bool Italic, bool Underline,
        float PaddingLeft, float PaddingTop, float PaddingRight, float PaddingBottom, bool WordWrap, bool Glass, string colorTop, float Angle)
        {
            if (TextObjectAngleStyle == true)
            {
                AddResourceTextObjectAngle(); TextObjectAngleStyle = false;
            }
            XmlElement UserControl = CrtTOFrame(MarginLeft, MarginTop, Width, Height,
          HorizontalAlignment, VerticalAlignment, BorderBrush, BorderThickness,
          LeftLine, TopLine, RightLine, BottomLine, LeftLineDashStile,
          TopLineDashStile, RightLineDashStile, BottomLineDashStile, colorLeftLine,
          colorTopLine, colorRightLine, colorBottomLine, Shadow, ShadowColor,
          ShadowWidth, Background, BorderLines
         , PaddingLeft, PaddingTop,
          PaddingRight, PaddingBottom, Glass, colorTop, "UserControl");

            if (!string.IsNullOrEmpty(Text))
            {
                XmlElement EndoText = doc.CreateElement("TextBox");
                AppndAttr(EndoText, "Style", "{StaticResource rTxt}");
                AppndAttr(EndoText, "Width", ExportUtils.FloatToString(Width - Math.Max(Math.Max(LeftLine, TopLine),
                    Math.Max(RightLine, BottomLine))));
                AppendTextAttrs(HorizontalAlignment, VerticalAlignment, Text,
         FontSize, Foreground, FontFamily, Bold, Italic, Underline,
         WordWrap, EndoText);

                if (!(PaddingLeft == 2 && PaddingTop == 0 && PaddingRight == 2 & PaddingBottom == 0))
                {
                    string Paddings;
                    if (PaddingLeft == 0 && PaddingTop == 0 && PaddingRight == 0 && PaddingBottom == 0) Paddings = Convert.ToString(0);
                    else if (PaddingLeft == PaddingTop && PaddingLeft == PaddingRight && PaddingLeft == PaddingBottom && PaddingLeft != 0) Paddings = ExportUtils.FloatToString(PaddingLeft);//Convert.ToString(PaddingLeft);
                    else
                        Paddings = ExportUtils.FloatToString(PaddingLeft) + "," + ExportUtils.FloatToString(PaddingTop) + ","
                            + ExportUtils.FloatToString(PaddingRight) + "," + ExportUtils.FloatToString(PaddingBottom);
                    AppndAttr(EndoText, "Padding", "Paddings");
                }
                if (!WordWrap)
                    AppndAttr(EndoText, "TextWrapping", "NoWrap");
                UserControl.AppendChild(EndoText);

                XmlElement TextBoxTransform = doc.CreateElement("TextBox.LayoutTransform");
                EndoText.AppendChild(TextBoxTransform);
                XmlElement TransformGroup = doc.CreateElement("TransformGroup");
                TextBoxTransform.AppendChild(TransformGroup);
                XmlElement RotateTransform = doc.CreateElement("RotateTransform");
                AppndAttr(RotateTransform, "Angle", ExportUtils.FloatToString(Angle));
                TransformGroup.AppendChild(RotateTransform);
            }
            Canvas.AppendChild(UserControl);
        }

        ///<summary>
        ///Add line.
        /// </summary>
        internal void AddLine(float marginLeft, float marginTop, float x2, float y2, string stroke, float strokeThickness)
        {
            if (LineStyle == true)
            {
                AddResourceLine(); LineStyle = false;
            }
            XmlElement Line = doc.CreateElement("Line");
            AppndAttr(Line, "Style", "{StaticResource rLn}");
            AddSizeAttrs(width, height, Line);
            AddXY12Attrs(marginLeft, marginTop, x2, y2, Line);
            AddStrokeAttrs(stroke, strokeThickness, Line);
            Canvas.AppendChild(Line);
        }

        ///<summary>
        ///Add line with dash.
        /// </summary>
        internal void AddLine(float marginLeft, float marginTop, float x2, float y2, string stroke, float strokeThickness, Dashes dash)
        {
            AddLine(marginLeft, marginTop, x2, y2, stroke, strokeThickness);         
            string StrokeDashArray = null;
            switch(dash)
            {
                case Dashes.Dash:       StrokeDashArray = "3";          break;
                case Dashes.Dot:        StrokeDashArray = "1";          break;
                case Dashes.DashDot:    StrokeDashArray = "1 3 1";      break;
                case Dashes.DashDotDot: StrokeDashArray = "1 1 3 1 1";  break;
            }
            if (StrokeDashArray != null)
                AppndAttr((XmlElement)Canvas.LastChild, "StrokeDashArray", StrokeDashArray);
        }
        /// <summary>
        /// Add rectangle.
        /// </summary>
        internal void AddRectangle(float marginLeft, float marginTop, float width, float height,
                                  string stroke, float strokeThickness, string fill, bool rounded)
        {
            if (RectangleStyle == true)
            {
                AddResourceRectangle(); RectangleStyle = false;
            }
            XmlElement Rectangle = doc.CreateElement("Rectangle");
            AppndAttr(Rectangle, "Style", "{StaticResource rRc}");
            AddMarginAttrs(marginLeft + strokeThickness, marginTop + strokeThickness, Rectangle);
            AddSizeAttrs(width, height, Rectangle);
            AddStrokeAttrs(stroke, strokeThickness, Rectangle);
            if (fill != null)
                AppndAttr(Rectangle, "Fill", fill);
            if (rounded)
            {
                AppndAttr(Rectangle, "RadiusX", "10");
                AppndAttr(Rectangle, "RadiusY", "10");
            }
            Canvas.AppendChild(Rectangle);
        }

        /// <summary>
        /// Add ellips.
        /// </summary>
        internal void AddEllipse(float marginLeft, float marginTop, float width, float height,
             string stroke, float strokeThickness, string fill)
        {
            if (EllipseStyle == true)
            {
                AddResourceEllipse(); EllipseStyle = false;
            }
            XmlElement Ellipse = doc.CreateElement("Ellipse");
            AppndAttr(Ellipse, "Style", "{StaticResource rEl}");        
            AddMarginAttrs(marginLeft + strokeThickness, marginTop + strokeThickness, Ellipse);

            AddSizeAttrs(width, height, Ellipse);
            AddStrokeAttrs(stroke, strokeThickness, Ellipse);
            if (fill != null)
                AppndAttr(Ellipse, "Fill", fill);
            Canvas.AppendChild(Ellipse);
        }

        /// <summary>
        /// Add triangle.
        /// </summary>
        internal void AddTriangle(float marginLeft, float marginTop, float width, float height,
             string stroke, float strokeThickness, string fill)
        {
            if (PolygonStyle == true)
            {
                AddResourcePolygon(); PolygonStyle = false;
            }
            XmlElement Polygon = doc.CreateElement("Polygon");
            AppndAttr(Polygon, "Style", "{StaticResource rPl}");
            AddMarginAttrs(marginLeft, marginTop, Polygon);
            AddSizeAttrs(width, height, Polygon);
            string Points;
            float x1 = width/2; 
            float y1 = 0;
            float x2 = width; 
            float y2 = height;
            float x3 = 0; 
            float y3 = height;
            Points = ExportUtils.FloatToString(x1) + "," +
                     ExportUtils.FloatToString(y1) + "," + 
                     ExportUtils.FloatToString(x2) + "," + 
                     ExportUtils.FloatToString(y2) + "," + 
                     ExportUtils.FloatToString(x3) + "," + 
                     ExportUtils.FloatToString(y3);
            AppndAttr(Polygon, "Points", Points);
            AddStrokeAttrs(stroke, strokeThickness, Polygon);
            if (fill != null)
                AppndAttr(Polygon, "Fill", fill);
            Canvas.AppendChild(Polygon);
        }

        /// <summary>
        /// Add Diamond.
        /// </summary>
        internal void AddDiamond(float marginLeft, float marginTop, float width, float height,
             string stroke, float strokeThickness, string fill)
        {
            if (PolygonStyle == true)
            {
                AddResourcePolygon(); PolygonStyle = false;
            }
            XmlElement Polygon = doc.CreateElement("Polygon");
            AppndAttr(Polygon, "Style", "{StaticResource rPl}");
            AddMarginAttrs(marginLeft, marginTop, Polygon);
            AddSizeAttrs(width, height, Polygon);
            string Points;
            float x1 = width / 2; float y1 = 0;
            float x2 = width; float y2 = height / 2;
            float x3 = width / 2; float y3 = height;
            float x4 = 0; float y4 = height / 2;
            Points = ExportUtils.FloatToString(x1) + "," + 
                     ExportUtils.FloatToString(y1) + "," + 
                     ExportUtils.FloatToString(x2) + "," + 
                     ExportUtils.FloatToString(y2) + "," + 
                     ExportUtils.FloatToString(x3) + "," + 
                     ExportUtils.FloatToString(y3) + "," + 
                     ExportUtils.FloatToString(x4) + "," + 
                     ExportUtils.FloatToString(y4);
            AppndAttr(Polygon, "Points", Points);
            AddStrokeAttrs(stroke, strokeThickness, Polygon);
            if (fill != null)
                AppndAttr(Polygon, "Fill", fill);
            Canvas.AppendChild(Polygon);
        }

        /// <summary>
        /// Add image
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        /// <param name="name"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        internal void AddImage(string filename, string format, string name, float left, float top, float width, float height)
        {
            if (!String.IsNullOrEmpty(filename))
            {
                XmlElement Image = doc.CreateElement("Image");
                AppndAttr(Image, "Source", filename + "." + format);             
                if (!string.IsNullOrEmpty(name))
                    AppndAttr(Image, "Name", name);
                AddMarginAttrs(left, top, Image);
                AddSizeAttrs(width, height, Image);
                Canvas.AppendChild(Image);
            }
        }


        /// <summary>
        /// Add image without name
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        internal void AddImage(string filename, string format, float left, float top, float width, float height)
        {
            AddImage(filename, format, null, left, top, width, height);
        }

        /// <summary>
        /// Add page to StackPanel
        /// </summary>
        /// <param name="PageNum"></param>
        internal void AddPage(int PageNum)
        {
            AddCanvas(PageNum);
        }

        /// <summary>
        /// Save xaml file.
        /// </summary>
        internal void Save(string filename)
        {
            doc.Save(filename);
        }

        /// <summary>
        /// Save xaml stream.
        /// </summary>
        internal void Save(Stream stream)
        {
            doc.Save(stream);
        }

        /// <param name="name"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="PagesCount"></param>
        /// <param name="Fill"></param>
        /// <param name="isScrolled"></param>
        internal XAMLDocument(string name, int PagesCount, string Fill, float Width, float Height, bool isScrolled)
        {
            TextObjectStyle = true;
            TextObjectAngleStyle = true;
            LineStyle = true;
            RectangleStyle = true;
            EllipseStyle = true;
            PolygonStyle = true;

            width = Width;
            height = Height;
            pages_count = PagesCount;
            fill = Fill;
            this.isScrolled = isScrolled;
            CreateWindow(name);
            AddResources();
            if(isScrolled)
            {
                AddScrollViewer();
                AddStackPanel(ScrollViewer);
            }
            else
                AddStackPanel(root);
        }
    }
}
