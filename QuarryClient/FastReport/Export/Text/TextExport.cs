using FastReport.Forms;
using FastReport.Preview;
using FastReport.Table;
using FastReport.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace FastReport.Export.Text
{
    /// <summary>
    /// Represents the text export.
    /// </summary>
    public class TextExport : ExportBase
    {
        #region Constants
        private const float XDiv = 7.5f;
        private const float YDiv = 4f;

        private const float XDivAdv = 0.1015f;
        private const float YDivAdv = 0.06f;
        private const float CalculateStep = 0.1f;
        private const float CalculateStepBack = 0.01f;

        private const float divPaperX = 4.8f;
        private const float divPaperY = 3.6f;
        private const int CalcIterations = 10;
        byte[] U_HEADER = { 239, 187, 191 };
        string[] ref_frames = {
            // 0 left, 1 top, 2 left-up, 3 right-top, 4 right-down, 5 left-down, 6 left-t, 
            // 7 right-t,  8 up-t, 9 down-t, 10 cross
            "|-+++++++++",
            "\u2502\u2500\u250c\u2510\u2518\u2514\u251c\u2524\u252c\u2534\u253c" };
        #endregion

        #region Private fields
        private bool FPageBreaks;
        private MyRes Res;
        private bool FFrames;
        private bool FTextFrames;
        private bool FEmptyLines;
        private int FScreenWidth;
        private int FScreenHeight;
        private StringBuilder screen;
        private float FScaleX;
        private float FScaleY;
        private Encoding FEncoding;
        private bool FDataOnly;
        private bool FPreviewMode;
        private int FPageWidth;
        private int FPageHeight;
        private bool FDataSaved;
        private bool FDataLossBreak;
        private string FFrameChars;
        private List<TextExportPrinterType> FPrinterTypes;
        private int FPrinterType;
        private bool FPrintAfterExport;
        private string FPrinterName;
        private int FCopies;
        private bool FAvoidDataLoss;
        #endregion

        #region Properties

        /// <summary>
        /// Enable or disable the Data loss avoiding. 
        /// Auto calculation of ScaleX and ScaleY will be launched when dialogue window will be off.        
        /// </summary>
        public bool AvoidDataLoss
        {
            get { return FAvoidDataLoss; }
            set { FAvoidDataLoss = value; }
        }

        /// <summary>
        /// Gets or sets the count of copies for printing of results.
        /// </summary>
        public int Copies
        {
            get { return FCopies; }
            set { FCopies = value; }
        }

        /// <summary>
        /// Gets or sets the printer name for printing of results.
        /// </summary>
        public string PrinterName
        {
            get { return FPrinterName; }
            set { FPrinterName = value; }
        }

        /// <summary>
        /// Enable or disable the printing results after export.
        /// </summary>
        public bool PrintAfterExport
        {
            get { return FPrintAfterExport; }
            set { FPrintAfterExport = value; }
        }

        /// <summary>
        /// Gets or sets the active index of registered printer type.
        /// </summary>
        public int PrinterType
        {
            get { return FPrinterType; }
            set { FPrinterType = value; }
        }

        /// <summary>
        /// Gets or sets the list of printer types. <see cref="TextExportPrinterType"/>
        /// </summary>
        public List<TextExportPrinterType> PrinterTypes
        {
            get { return FPrinterTypes; }
            set { FPrinterTypes = value; }
        }

        /// <summary>
        /// Gets or sets the scale by X axis for correct text objects placement.
        /// </summary>
        public float ScaleX
        {
            get { return FScaleX; }
            set { FScaleX = value; }
        }

        /// <summary>
        /// Gets or sets the scale by Y axis for correct text objects placement.
        /// </summary>
        public float ScaleY
        {
            get { return FScaleY; }
            set { FScaleY = value; }
        }

        /// <summary>
        /// Gets or sets the encoding of resulting document. 
        /// </summary>
        /// <example>
        /// Windows ANSI encoding
        /// <code>TextExport.Encoding = Encoding.Default;</code>
        /// Unicode UTF-8 encoding
        /// <code>TextExport.Encoding = Encoding.UTF8;</code>
        /// OEM encoding for current system locale sessings
        /// <code>TextExport.Encoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage);</code>
        /// </example>
        public Encoding Encoding
        {
            get { return FEncoding; }
            set { FEncoding = value; }
        }

        /// <summary>
        /// Enable or disable the data only output without any headers. Default value is false.
        /// </summary>
        public bool DataOnly
        {
            get { return FDataOnly; }
            set { FDataOnly = value; }
        }

        /// <summary>
        /// Enable or disable the breaks of pages in resulting document. Default value is true.
        /// </summary>
        public bool PageBreaks
        {
            get { return FPageBreaks; }
            set { FPageBreaks = value; }
        }

        /// <summary>
        /// Enable or disable frames in resulting document. Default value is true.
        /// </summary>
        public bool Frames
        {
            get { return FFrames; }
            set { FFrames = value; }
        }

        /// <summary>
        /// Enable or disable the text (non graphic) frames in resulting document. Default value is false.
        /// </summary>
        public bool TextFrames
        {
            get { return FTextFrames; }
            set { FTextFrames = value; }
        }

        /// <summary>
        /// Enable or disable the output of empty lines in resulting document. Default value is false.
        /// </summary>
        public bool EmptyLines
        {
            get { return FEmptyLines; }
            set { FEmptyLines = value; }
        }

        internal bool DataSaved
        {
            get { return FDataSaved; }
        }

        internal bool DataLossBreak
        {
            get { return FDataLossBreak; }
            set { FDataLossBreak = value; }
        }

        internal int PageHeight
        {
            get { return FPageHeight; }
        }

        internal int PageWidth
        {
            get { return FPageWidth; }
        }

        internal bool PreviewMode
        {
            get { return FPreviewMode; }
            set { FPreviewMode = value; }
        }
        #endregion

        #region Private Methods

        private char ScreenGet(int x, int y)
        {
            if ((x < FScreenWidth) && (y < FScreenHeight) &&
                (x >= 0) && (y >= 0))
                return screen[FScreenWidth * y + x];
            else
                return ' ';
        }

        private void ScreenType(int x, int y, char c)
        {
            if ((x < FScreenWidth) && (y < FScreenHeight) &&
                (x >= 0) && (y >= 0))
            {
                if (c != ' ')
                {
                    char current = screen[FScreenWidth * y + x];
                    if (current != ' ' && !(FFrames && IsFrame(current) && IsFrame(c)))
                        FDataSaved = false;
                    screen[FScreenWidth * y + x] = c;
                }
            }
            else
                if (c != ' ')
                    FDataSaved = false;
        }

        private bool IsFrame(char c)
        {
            return c == ' ' ? false : (FFrameChars.IndexOf(c) != -1);
        }

        private StringBuilder AlignStr(string s, HorzAlign align, int width)
        {
            if (align == HorzAlign.Right)
                return RightStr(s, width - 1);
            else if (align == HorzAlign.Center)
                return CenterStr(s, width - 1);
            else
                return LeftStr(s, width - 1);
        }

        private StringBuilder LeftStr(string s, int width)
        {
            return AddCharR(' ', s, width);
        }

        private StringBuilder AddCharR(char p, string s, int width)
        {
            width = width > 0 ? width : 0;
            StringBuilder result = new StringBuilder(width);
            if (s.Length < width)
                return result.Append(s).Append(new String(p, width - s.Length));
            else
                return result.Append(s);
        }

        private StringBuilder CenterStr(string s, int width)
        {
            if (width < s.Length)
                width = s.Length;
            StringBuilder result = new StringBuilder(width);
            if (s.Length < width)
            {
                result.Append(new String(' ', (int)(width / 2) - (int)(s.Length / 2))).Append(s);
                result.Append(new String(' ', width - result.Length));
            }
            else
                result.Append(s);
            return result;
        }

        private StringBuilder RightStr(string s, int width)
        {
            return AddChar(' ', s, width);
        }

        private StringBuilder AddChar(char p, string s, int width)
        {
            width = width > 0 ? width : 0;
            StringBuilder result = new StringBuilder(width);
            if (s.Length < width)
                result.Append(new String(p, width - s.Length)).Append(s);
            else
                result.Append(s);
            return result;
        }

        private void ScreenString(int x, int y, string s)
        {
            for (int i = 0; i < s.Length; i++)
                ScreenType(x + i, y, s[i]);
        }

        private void InitScreen()
        {
            screen = new StringBuilder(FScreenWidth * FScreenHeight);
            screen.Append(' ', FScreenWidth * FScreenHeight);
        }

        private void DrawLineObject(LineObject lineObject)
        {
            if (lineObject.Width == 0 || lineObject.Height == 0)
            {
                int d = FFrames ? 1 : 0;
                int curx = (int)Math.Round(lineObject.AbsLeft * FScaleX * XDivAdv) + d;
                int cury = (int)Math.Round(lineObject.AbsTop * YDivAdv * FScaleY) + d;
                int cury2 = (int)Math.Floor((lineObject.AbsTop + lineObject.Height) * FScaleY * YDivAdv) + d;
                int curx2 = (int)Math.Floor((lineObject.AbsLeft + lineObject.Width) * FScaleX * XDivAdv) + d;
                int height = cury2 - cury;
                int width = curx2 - curx;
                if (lineObject.Width == 0)
                    for (int i = 0; i < height; i++)
                        ScreenType(curx, cury + i, FFrameChars[0]);
                else if (lineObject.Height == 0)
                    for (int i = 0; i < width; i++)
                        ScreenType(curx + i, cury, FFrameChars[1]);
            }
        }

        private void DrawTextObject(TextObject textObject)
        {
            int linesBefore = 0;
            int d = FFrames ? 1 : 0;
            int curx = (int)(textObject.AbsLeft * FScaleX * XDivAdv) + d;
            int cury = (int)(textObject.AbsTop * YDivAdv * FScaleY) + d;
            int cury2 = (int)Math.Floor((textObject.AbsTop + textObject.Height) * FScaleY * YDivAdv) + d;
            int curx2 = (int)Math.Floor((textObject.AbsLeft + textObject.Width) * FScaleX * XDivAdv) + d;
            int height = cury2 - cury;
            int width = curx2 - curx;

            List<string> lines = WrapTextObject(textObject);

            if (textObject.VertAlign == VertAlign.Bottom)
                linesBefore = height - lines.Count;
            else if (textObject.VertAlign == VertAlign.Center)
                linesBefore = (int)((height - lines.Count) / 2);

            for (int i = 0; i < lines.Count; i++)
            {
                string s = AlignStr(lines[i], textObject.HorzAlign, width).ToString();
                ScreenString(curx, cury + i + linesBefore, s);
                if (FDataLossBreak && !FDataSaved)
                    return;
            }
            if (FFrames)
            {
                if ((textObject.Border.Lines & BorderLines.Left) > 0)
                    for (int i = 0; i < height; i++)
                        ScreenType(curx - 1, cury + i, FFrameChars[0]);
                if ((textObject.Border.Lines & BorderLines.Right) > 0)
                    for (int i = 0; i < height; i++)
                        ScreenType(curx + width - 1, cury + i, FFrameChars[0]);
                if ((textObject.Border.Lines & BorderLines.Top) > 0)
                    for (int i = 0; i < width; i++)
                        ScreenType(curx + i, cury - 1, FFrameChars[1]);
                if ((textObject.Border.Lines & BorderLines.Bottom) > 0)
                    for (int i = 0; i < width; i++)
                        ScreenType(curx + i, cury + height - 1, FFrameChars[1]);
            }
        }

        private List<string> WrapTextObject(TextObject obj)
        {
            float FDpiFX = 96f / DrawUtils.ScreenDpi;
            List<string> result = new List<string>();
            DrawText drawer = new DrawText();            
            using(Bitmap b = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(b))
            using (Font f = new Font(obj.Font.Name, obj.Font.Size * FDpiFX, obj.Font.Style))
            {
                float h = f.Height - f.Height / 4;
                float memoWidth = obj.Width - obj.Padding.Horizontal;

                string text = obj.Text;

                float memoHeight = drawer.CalcHeight(text, g, f,
                    memoWidth, obj.Height - obj.Padding.Vertical,
                    obj.HorzAlign, obj.LineHeight, obj.ForceJustify, obj.RightToLeft, obj.WordWrap, obj.Trimming);

                float y, prevy = 0;
                StringBuilder line = new StringBuilder(256);
                foreach (Paragraph par in drawer.Paragraphs)
                {
                    foreach (Word word in par.Words)
                    {
                        if (!word.Visible)
                            break;
                        y = word.Top + 1;
                        if (prevy == 0)
                            prevy = y;
                        if (y != prevy)
                        {
                            result.Add(line.ToString());
                            line.Length = 0;
                            prevy = y;
                        }
                        line.Append(word.Text).Append(' ');
                    }
                }
                result.Add(line.ToString());
            }
            return result;
        }
        #endregion

        #region Protected Methods

        /// <inheritdoc/>
        public override bool ShowDialog()
        {
            using (TextExportForm form = new TextExportForm())
            {
                form.Init(this);
                return form.ShowDialog() == DialogResult.OK;
            }
        }

        /// <inheritdoc/>
        protected override void Start()
        {
            if (FAvoidDataLoss)
                CalculateScale(null);
            if (FPrinterType >= 0 && FPrinterType < FPrinterTypes.Count)
                foreach (TextExportPrinterCommand command in FPrinterTypes[FPrinterType].Commands)
                    if (command.Active)
                        foreach (byte esc in command.SequenceOn)
                            Stream.WriteByte(esc);
            if (Encoding == Encoding.UTF8)
                Stream.Write(U_HEADER, 0, 3);
        }

        /// <inheritdoc/>
        protected override void Finish()
        {
            if (FPrinterType >= 0 && FPrinterType < FPrinterTypes.Count)
                foreach (TextExportPrinterCommand command in FPrinterTypes[FPrinterType].Commands)
                    if (command.Active)
                        foreach (byte esc in command.SequenceOff)
                            Stream.WriteByte(esc);
            if (FPrintAfterExport)
                TextExportPrint.PrintStream(PrinterName, Report.ReportInfo.Name.Length == 0 ? "FastReport .NET Text export" : Report.ReportInfo.Name, Copies, Stream);
        }


        /// <inheritdoc/>
        protected override string GetFileFilter()
        {
            return new MyRes("FileFilters").Get("TxtFile");
        }

        #endregion

        /// <inheritdoc/>
        protected override void ExportPageBegin(ReportPage page)
        {
            base.ExportPageBegin(page);
            if (FFrames)
                FFrameChars = FTextFrames ? ref_frames[0] : ref_frames[1];

            FPageWidth = FPageHeight = 0;
            FDataSaved = true;
            FScreenWidth = (int)Math.Floor(ExportUtils.GetPageWidth(page) * divPaperX * XDivAdv * FScaleX);
            FScreenHeight = (int)Math.Floor(ExportUtils.GetPageHeight(page) * divPaperY * YDivAdv * FScaleY);
            InitScreen();
        }

        private void ExportObject(Base c)
        {
            if (c is ReportComponentBase)
            {
                ReportComponentBase obj = c as ReportComponentBase;
                if (FDataOnly && (obj.Parent == null || !(obj.Parent is DataBand)))
                    return;
                if (obj is TableCell)
                    return;
                else
                    if (obj is TableBase)
                {
                    TableBase table = obj as TableBase;
                    using (TextObject tableback = new TextObject())
                    {
                        tableback.Border = table.Border;
                        tableback.Fill = table.Fill;
                        tableback.FillColor = table.FillColor;
                        tableback.Left = table.AbsLeft;
                        tableback.Top = table.AbsTop;
                        float tableWidth = 0;
                        for (int i = 0; i < table.ColumnCount; i++)
                            tableWidth += table[i, 0].Width;
                        tableback.Width = (tableWidth < table.Width) ? tableWidth : table.Width;
                        tableback.Height = table.Height;
                        DrawTextObject(tableback);
                    }
                    float y = 0;
                    for (int i = 0; i < table.RowCount; i++)
                    {
                        float x = 0;
                        for (int j = 0; j < table.ColumnCount; j++)
                        {
                            if (!table.IsInsideSpan(table[j, i]))
                            {
                                TableCell textcell = table[j, i];
                                textcell.Left = x;
                                textcell.Top = y;
                                DrawTextObject(textcell);
                            }
                            x += (table.Columns[j]).Width;
                        }
                        y += (table.Rows[i]).Height;
                    }
                }
                else if (obj is TextObject)
                    DrawTextObject(obj as TextObject);
                else if (obj is LineObject && FFrames)
                    DrawLineObject(obj as LineObject);
                //if (FDataLossBreak && !FDataSaved)
                //    return;
            }
        }

        /// <inheritdoc/>
        protected override void ExportBand(Base band)
        {
            base.ExportBand(band);
            if (band.Parent == null) return;
            ExportObject(band);
            foreach (Base c in band.AllObjects)
            {
                ExportObject(c);
            }
        }

        private StringBuilder builder;
        /// <inheritdoc/>
        protected override void ExportPageEnd(ReportPage page)
        {
            builder = new StringBuilder(FScreenHeight * FScreenWidth);
            for (int y = 0; y < FScreenHeight; y++)
            {
                bool empty = true;
                StringBuilder buf = new StringBuilder(FScreenWidth);
                for (int x = 0; x < FScreenWidth; x++)
                {
                    char c = ScreenGet(x, y);
                    if (FFrames && (c == ' ' || IsFrame(c))) // && c != ' ' && IsFrame(c)
                    {
                        bool up = ScreenGet(x, y - 1) == FFrameChars[0];
                        bool down = ScreenGet(x, y + 1) == FFrameChars[0];
                        bool left = ScreenGet(x - 1, y) == FFrameChars[1];
                        bool right = ScreenGet(x + 1, y) == FFrameChars[1];
                        if (down && right && !left && !up)
                            c = FFrameChars[2];
                        else if (down && !right && left && !up)
                            c = FFrameChars[3];
                        else if (!down && !right && left && up)
                            c = FFrameChars[4];
                        else if (!down && right && !left && up)
                            c = FFrameChars[5];
                        else if (down && right && !left && up)
                            c = FFrameChars[6];
                        else if (down && !right && left && up)
                            c = FFrameChars[7];
                        else if (down && right && left && !up)
                            c = FFrameChars[8];
                        else if (!down && right && left && up)
                            c = FFrameChars[9];
                        else if (up && down && left && right)
                            c = FFrameChars[10];
                        else if (up && down && !left && !right)
                            c = FFrameChars[0];
                        else if (!up && !down && left && right)
                            c = FFrameChars[1];
                    }
                    buf.Append(c);
                    if (c != ' ' && (!FFrames || c != FFrameChars[0]))
                        empty = false;
                }
                if (!empty || FEmptyLines)
                {
                    string s = buf.ToString().TrimEnd((char)32);
                    builder.AppendLine(s);
                    if (s.Length > FPageWidth)
                        FPageWidth = s.Length;
                    FPageHeight++;
                }
            }
            if (FPageBreaks)
                builder.AppendLine("\u000c");
            if (!FPreviewMode)
            {
                byte[] bytes = FEncoding.GetBytes(builder.ToString());
                Stream.Write(bytes, 0, bytes.Length);
            }
        }

        #region Internal methods
        internal string ExportPage(int pageNo)
        {
            PreparedPage ppage = Report.PreparedPages.GetPreparedPage(pageNo);
            ReportPage page = null;
            try
            {
                page = ppage.StartGetPage(pageNo);
                ExportPageBegin(page);
                foreach (Base obj in ppage.GetPageItems(page, false))
                    ExportBand(obj);
                ExportPageEnd(page);
            }
            finally
            {
                ppage.EndGetPage(page);
            }

            if (FPreviewMode)
                return builder.ToString();
            else
                return String.Empty;
        }

        internal void CalculateScale(ProgressForm progress)
        {
            bool oldPreviewMode = FPreviewMode;
            FDataLossBreak = true;
            FPreviewMode = true;
            float initX = CalculateStep;
            float initY = CalculateStep;
            for (int p = 0; p < Report.PreparedPages.Count; p++)
            {
                if (progress != null && progress.Aborted)
                    break;
                ExportPage(p);
                int j = CalcIterations;
                while (!FDataSaved && --j > 0)
                {
                    if (progress != null && progress.Aborted)
                        break;
                    int i = CalcIterations;
                    float oldX = ScaleX;
                    while (!FDataSaved && --i > 0)
                    {
                        if (progress != null && progress.Aborted)
                            break;
                        FScaleX += CalculateStep;
                        ExportPage(p);
                    }
                    i = CalcIterations;
                    while (!FDataSaved && --i > 0)
                    {
                        if (progress != null && progress.Aborted)
                            break;
                        FScaleY += CalculateStep;
                        ExportPage(p);
                    }

                    if (FDataSaved && i < CalcIterations)
                    {
                        i = CalcIterations;
                        FScaleX = oldX;
                        ExportPage(p);
                        while (!FDataSaved && --i > 0)
                        {
                            if (progress != null && progress.Aborted)
                                break;
                            FScaleX += CalculateStep;
                            ExportPage(p);
                        }
                    }
                }
                if (FDataSaved && FFrames)
                {
                    int i = CalcIterations;
                    float oldY = FScaleY;
                    while (FDataSaved && --i > 0)
                    {
                        if (progress != null && progress.Aborted)
                            break;
                        oldY = FScaleY;
                        FScaleY -= CalculateStepBack;
                        if (FScaleY < initY)
                            break;
                        ExportPage(p);
                    }
                    FScaleY = oldY;
                    FDataSaved = true;

                    i = CalcIterations;
                    float oldX = FScaleX;
                    while (FDataSaved && --i > 0)
                    {
                        if (progress != null && progress.Aborted)
                            break;
                        oldX = FScaleX;
                        FScaleX -= CalculateStepBack;
                        if (FScaleX < initX)
                            break;
                        ExportPage(p);
                    }
                    FScaleX = oldX;
                    FDataSaved = true;
                }
                initX = FScaleX;
                initY = FScaleY;
                if (j == 0)
                    break;
            }
            FDataLossBreak = false;
            FPreviewMode = oldPreviewMode;
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TextExport"/> class.
        /// </summary>       
        public TextExport()
        {
            FPageBreaks = true;
            FEmptyLines = false;
            FFrames = true;
            FTextFrames = false;
            FEncoding = Encoding.UTF8;
            FDataOnly = false;
            FScaleX = 1.0f;
            FScaleY = 1.0f;
            FPreviewMode = false;
            FDataLossBreak = false;
            FAvoidDataLoss = true;
            FPrinterTypes = new List<TextExportPrinterType>();

            TextExportPrinterType printer = new TextExportPrinterType();
            printer.Name = "Epson ESC/P2";
            FPrinterTypes.Add(printer);

            TextExportPrinterCommand command = new TextExportPrinterCommand();
            command.Name = "Reset";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(64);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Normal";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(120);
            command.SequenceOn.Add(0);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Pica";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(120);
            command.SequenceOn.Add(1);
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(107);
            command.SequenceOn.Add(0);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Elite";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(120);
            command.SequenceOn.Add(1);
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(107);
            command.SequenceOn.Add(1);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Condenced";
            command.SequenceOn.Add(15);
            command.SequenceOff.Add(18);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Bold";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(71);
            command.SequenceOff.Add(27);
            command.SequenceOff.Add(72);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Italic";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(52);
            command.SequenceOff.Add(27);
            command.SequenceOff.Add(53);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Wide";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(87);
            command.SequenceOn.Add(1);
            command.SequenceOff.Add(27);
            command.SequenceOff.Add(87);
            command.SequenceOff.Add(0);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "12cpi";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(77);
            command.SequenceOff.Add(27);
            command.SequenceOff.Add(80);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Linefeed 1/8\"";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(48);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Linefeed 7/72\"";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(49);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Linefeed 1/6\"";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(50);
            printer.Commands.Add(command);

            printer = new TextExportPrinterType();
            printer.Name = "HP PCL";
            FPrinterTypes.Add(printer);

            command = new TextExportPrinterCommand();
            command.Name = "Reset";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(69);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Landscape";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(38);
            command.SequenceOn.Add(108);
            command.SequenceOn.Add(49);
            command.SequenceOn.Add(79);
            command.SequenceOff.Add(27);
            command.SequenceOff.Add(38);
            command.SequenceOff.Add(108);
            command.SequenceOff.Add(48);
            command.SequenceOff.Add(79);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Italic";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(40);
            command.SequenceOn.Add(115);
            command.SequenceOn.Add(49);
            command.SequenceOn.Add(83);
            command.SequenceOff.Add(27);
            command.SequenceOff.Add(40);
            command.SequenceOff.Add(115);
            command.SequenceOff.Add(48);
            command.SequenceOff.Add(83);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Bold";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(40);
            command.SequenceOn.Add(115);
            command.SequenceOn.Add(51);
            command.SequenceOn.Add(66);
            command.SequenceOff.Add(27);
            command.SequenceOff.Add(40);
            command.SequenceOff.Add(115);
            command.SequenceOff.Add(48);
            command.SequenceOff.Add(66);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Draft EconoMode";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(40);
            command.SequenceOn.Add(115);
            command.SequenceOn.Add(49);
            command.SequenceOn.Add(81);
            command.SequenceOff.Add(27);
            command.SequenceOff.Add(40);
            command.SequenceOff.Add(115);
            command.SequenceOff.Add(50);
            command.SequenceOff.Add(81);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Condenced";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(40);
            command.SequenceOn.Add(115);
            command.SequenceOn.Add(49);
            command.SequenceOn.Add(50);
            command.SequenceOn.Add(72);
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(38);
            command.SequenceOn.Add(108);
            command.SequenceOn.Add(56);
            command.SequenceOn.Add(68);
            command.SequenceOff.Add(27);
            command.SequenceOff.Add(40);
            command.SequenceOff.Add(115);
            command.SequenceOff.Add(49);
            command.SequenceOff.Add(48);
            command.SequenceOff.Add(72);
            printer.Commands.Add(command);

            printer = new TextExportPrinterType();
            printer.Name = "Canon/IBM";
            FPrinterTypes.Add(printer);

            command = new TextExportPrinterCommand();
            command.Name = "Reset";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(64);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Normal";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(120);
            command.SequenceOn.Add(0);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Pica";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(48);
            command.SequenceOn.Add(73);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Elite";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(56);
            command.SequenceOn.Add(73);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Condenced";
            command.SequenceOn.Add(15);
            command.SequenceOff.Add(18);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Bold";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(71);
            command.SequenceOff.Add(27);
            command.SequenceOff.Add(72);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "Italic";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(52);
            command.SequenceOff.Add(27);
            command.SequenceOff.Add(53);
            printer.Commands.Add(command);

            command = new TextExportPrinterCommand();
            command.Name = "12cpi";
            command.SequenceOn.Add(27);
            command.SequenceOn.Add(77);
            command.SequenceOff.Add(27);
            command.SequenceOff.Add(80);
            printer.Commands.Add(command);

            FPrinterType = 0;
            FCopies = 1;

            OpenAfterExport = false;
            FPrintAfterExport = false;

            Res = new MyRes("Export,Misc");
        }
    }

    /// <summary>
    /// Represents the printer command class
    /// </summary>
    public class TextExportPrinterCommand
    {
        private List<byte> FSequenceOn;
        private List<byte> FSequenceOff;
        private string FName;
        private bool FActive;

        /// <summary>
        /// Gets or sets the active state of command. Default value is false.
        /// </summary>
        public bool Active
        {
            get { return FActive; }
            set { FActive = value; }
        }

        /// <summary>
        /// Gets or sets the command name.
        /// </summary>
        public string Name
        {
            get { return FName; }
            set { FName = value; }
        }

        /// <summary>
        /// Gets or sets the list of "on sequence". 
        /// </summary>
        public List<byte> SequenceOn
        {
            get { return FSequenceOn; }
            set { FSequenceOn = value; }
        }

        /// <summary>
        /// Gets or sets the list of "off sequence". 
        /// </summary>
        public List<byte> SequenceOff
        {
            get { return FSequenceOff; }
            set { FSequenceOff = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextExportPrinterCommand"/> class.
        /// </summary>
        public TextExportPrinterCommand()
        {
            FSequenceOn = new List<byte>();
            FSequenceOff = new List<byte>();
            FActive = false;
        }
    }

    /// <summary>
    /// Represents of the printer type class.
    /// </summary>
    public class TextExportPrinterType
    {
        private List<TextExportPrinterCommand> FCommands;
        private string FName;

        /// <summary>
        /// Gets or sets the printer name.
        /// </summary>
        public string Name
        {
            get { return FName; }
            set { FName = value; }
        }

        /// <summary>
        /// Gets or sets the list of printer commands. <see cref="TextExportPrinterCommand"/>
        /// </summary>
        public List<TextExportPrinterCommand> Commands
        {
            get { return FCommands; }
            set { FCommands = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextExportPrinterType"/> class.
        /// </summary>
        public TextExportPrinterType()
        {
            FCommands = new List<TextExportPrinterCommand>();
        }
    }
}
