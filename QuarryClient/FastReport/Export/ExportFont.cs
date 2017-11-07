//#define WITHOUT_UNISCRIBE

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FastReport.Utils;
using System.Drawing.Drawing2D;

namespace FastReport.Export.TTF
{
    /// <summary>
    /// Specifies the export font class.
    /// </summary>
    internal class ExportTTFFont : IDisposable
    {
        #region DLL import
        [DllImport("Gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("Gdi32.dll")]
        private static extern IntPtr DeleteObject(IntPtr hgdiobj);
        [DllImport("Gdi32.dll")]
        private static extern int GetOutlineTextMetrics(IntPtr hdc, int cbData, ref OutlineTextMetric lpOTM);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetGlyphIndices(IntPtr hdc, string lpstr, int c, [In, Out] ushort[] pgi, uint fl);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetFontData(IntPtr hdc, uint dwTable, uint dwOffset, [In, Out] byte[] lpvBuffer, uint cbData);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetFontData(IntPtr hdc, uint dwTable, uint dwOffset, [In, Out] IntPtr lpvBuffer, uint cbData);
#if ! WITHOUT_UNISCRIBE
        [DllImport("usp10.dll")]
        private static extern int ScriptFreeCache(ref IntPtr psc);
        [DllImport("usp10.dll")]
        private static extern int ScriptItemize(
            [MarshalAs(UnmanagedType.LPWStr)] string pwcInChars, int cInChars, int cMaxItems,
            ref SCRIPT_CONTROL psControl, ref SCRIPT_STATE psState, [In, Out] SCRIPT_ITEM[] pItems, ref int pcItems);
        [DllImport("usp10.dll")]
        private static extern int ScriptLayout(
            int cRuns,[MarshalAs(UnmanagedType.LPArray)] byte[] pbLevel,
            [MarshalAs(UnmanagedType.LPArray)] int[] piVisualToLogical,
            [MarshalAs(UnmanagedType.LPArray)] int[] piLogicalToVisual);
        [DllImport("usp10.dll")]
        private static extern int ScriptShape(
            IntPtr hdc, ref IntPtr psc, [MarshalAs(UnmanagedType.LPWStr)] string pwcChars,
            int cChars, int cMaxGlyphs, ref SCRIPT_ANALYSIS psa,
            [Out, MarshalAs(UnmanagedType.LPArray)] ushort[] pwOutGlyphs,
            [Out, MarshalAs(UnmanagedType.LPArray)] ushort[] pwLogClust,
            [Out, MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva, ref int pcGlyphs);
        [DllImport("usp10.dll")]
        private static extern int ScriptPlace(
            IntPtr hdc, ref IntPtr psc, [MarshalAs(UnmanagedType.LPArray)] ushort[] pwGlyphs,
            int cGlyphs, [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva,
            ref SCRIPT_ANALYSIS psa, [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance,
            [Out, MarshalAs(UnmanagedType.LPArray)] GOFFSET[] pGoffset, ref ABC pABC);
        [DllImport("usp10.dll")]
        private static extern int ScriptJustify(
            [MarshalAs(UnmanagedType.LPArray)] SCRIPT_VISATTR[] psva,
            [MarshalAs(UnmanagedType.LPArray)] int[] piAdvance,
            int cGlyphs, int iDx, int iMinKashida,
            [Out, MarshalAs(UnmanagedType.LPArray)] int[] piJustify);
        [DllImport("usp10.dll")]
        private static extern uint ScriptRecordDigitSubstitution(uint lcid, ref SCRIPT_DIGITSUBSTITUTE psds);
        [DllImport("usp10.dll")]
        private static extern int ScriptApplyDigitSubstitution(
            ref SCRIPT_DIGITSUBSTITUTE psds, ref SCRIPT_CONTROL psc, ref SCRIPT_STATE pss);
#endif
        #endregion

        /// <summary>
        /// These fonts not support Bold or Itailc styles
        /// </summary>
        static string[] NeedStyelSimulationList = new string[7]
        {
            "\uff2d\uff33 \u30b4\u30b7\u30c3\u30af",
            "\uff2d\uff33 \uff30\u30b4\u30b7\u30c3\u30af",
            "\uff2d\uff33 \u660e\u671d",
            "\uff2d\uff33 \uff30\u660e\u671d",
            "MS Gothic",
            "MS UI Gothic",
            "Arial Black"
        };

        #region Font Structures
        /// <summary>
        /// Description of SCRIPT_STATE structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]        
        public struct SCRIPT_STATE
        {
            /// <summary>
            /// data
            /// </summary>
            public short data;
            /// <summary>
            /// uBidiLevel
            /// </summary>
            public int uBidiLevel
            {
                get { return data & 0x001F; }
            }
            /// <summary>
            /// SetRtl
            /// </summary>
            public void SetRtl()
            {
                data = 0x801;
            }
        }

        /// <summary>
        /// Description of SCRIPT_ANALYSIS structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]       
        public struct SCRIPT_ANALYSIS
        {
            /// <summary>
            /// data
            /// </summary>
            public short data;
            /// <summary>
            /// state
            /// </summary>
            public SCRIPT_STATE state;
        }

        /// <summary>
        /// Description of SCRIPT_CONTROL structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SCRIPT_CONTROL
        {
            /// <summary>
            /// data
            /// </summary>
            public int data;
        }

        /// <summary>
        /// Description of SCRIPT_DIGITSUBSTITUTE structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SCRIPT_DIGITSUBSTITUTE
        {
            /// <summary>
            /// NationalDigitLanguage
            /// </summary>
            public short NationalDigitLanguage;
            /// <summary>
            /// TraditionalDigitLanguage
            /// </summary>
            public short TraditionalDigitLanguage;
            /// <summary>
            /// DigitSubstitute
            /// </summary>
            public byte DigitSubstitute;
            /// <summary>
            /// dwReserved
            /// </summary>
            public int dwReserved;
        }

        /// <summary>
        /// Description of SCRIPT_ITEM structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SCRIPT_ITEM
        {
            /// <summary>
            /// iCharPos
            /// </summary>
            public int iCharPos;
            /// <summary>
            /// analysis
            /// </summary>
            public SCRIPT_ANALYSIS analysis;
        }

        /// <summary>
        /// Description of SCRIPT_VISATTR structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SCRIPT_VISATTR
        {
            /// <summary>
            /// data
            /// </summary>
            public short data;
        }

        /// <summary>
        /// Description of GOFFSET structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct GOFFSET
        {
            /// <summary>
            /// du
            /// </summary>
            public int du;
            /// <summary>
            /// dv
            /// </summary>
            public int dv;
        }

        /// <summary>
        /// Description of ABC structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ABC
        {
            /// <summary>
            /// abcA
            /// </summary>
            public int abcA;
            /// <summary>
            /// abcB
            /// </summary>
            public int abcB;
            /// <summary>
            /// abcC
            /// </summary>
            public int abcC;
        }

        /// <summary>
        /// Description of FontPanose structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct FontPanose
        {
            public byte bFamilyType;
            public byte bSerifStyle;
            public byte bWeight;
            public byte bProportion;
            public byte bContrast;
            public byte bStrokeVariation;
            public byte ArmStyle;
            public byte bLetterform;
            public byte bMidline;
            public byte bXHeight;
        }

        /// <summary>
        /// Description of FontRect structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct FontRect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        /// <summary>
        /// Description of FontPoint structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct FontPoint
        {
            public int x;
            public int y;
        }

        /// <summary>
        /// Description of FontTextMetric structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct FontTextMetric
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public char tmFirstChar;
            public char tmLastChar;
            public char tmDefaultChar;
            public char tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
        }

        /// <summary>
        /// Description of OutlineTextMetric structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct OutlineTextMetric
        {
            public uint otmSize;
            public FontTextMetric otmTextMetrics;
            public byte otmFiller;
            public FontPanose otmPanoseNumber;
            public uint otmfsSelection;
            public uint otmfsType;
            public int otmsCharSlopeRise;
            public int otmsCharSlopeRun;
            public int otmItalicAngle;
            public uint otmEMSquare;
            public int otmAscent;
            public int otmDescent;
            public uint otmLineGap;
            public uint otmsCapEmHeight;
            public uint otmsXHeight;
            public FontRect otmrcFontBox;
            public int otmMacAscent;
            public int otmMacDescent;
            public uint otmMacLineGap;
            public uint otmusMinimumPPEM;
            public FontPoint otmptSubscriptSize;
            public FontPoint otmptSubscriptOffset;
            public FontPoint otmptSuperscriptSize;
            public FontPoint otmptSuperscriptOffset;
            public uint otmsStrikeoutSize;
            public int otmsStrikeoutPosition;
            public int otmsUnderscoreSize;
            public int otmsUnderscorePosition;
            public string otmpFamilyName;
            public string otmpFaceName;
            public string otmpStyleName;
            public string otmpFullName;
        }
        #endregion

        #region Private variables
        private Bitmap tempBitmap;
        private IntPtr FUSCache;
        private SCRIPT_DIGITSUBSTITUTE FDigitSubstitute;
        private float FDpiFX;

        private List<int> FWidths;
        private List<ushort> FUsedGlyphIndexes;
        private List<ushort> FUsedAlphabetUnicode;
        private OutlineTextMetric FTextMetric;
        private string FName;
        private Font FSourceFont;
        private long FReference;
        private bool FSaved;
        private bool FStyleSimulation;
        private TrueTypeCollection fontCollection;
        private TrueTypeFont fontTTF;

#if WITHOUT_UNISCRIBE
        TrueTypeCollection FFontCollection;
#endif
        #endregion

        #region Public fields

        /// <summary>
        /// Return widths array
        /// </summary>
        public List<int> Widths
        {
            get { return FWidths; }
        }

        /// <summary>
        /// Return array with used glyph indexes - glyphs alphabet
        /// </summary>
        public List<ushort> UsedGlyphIndexes
        {
            get { return FUsedGlyphIndexes; }
        }

        /// <summary>
        /// Return used unicode alphabet
        /// </summary>
        public List<ushort> UsedAlphabetUnicode
        {
            get { return FUsedAlphabetUnicode; }
        }

        /// <summary>
        /// Return text metric structure, need to use after FillOutlineTextMetrix()
        /// </summary>
        public OutlineTextMetric TextMetric
        {
            get { return FTextMetric; }
        }

        /// <summary>
        /// Gets or sets internal font name
        /// </summary>
        public string Name
        {
            get { return FName; }
            set { FName = value; }
        }

        /// <summary>
        /// Return source font used in constructor
        /// </summary>
        public Font SourceFont
        {
            get { return FSourceFont; }
        }

        /// <summary>
        /// Gets or sets internal reference
        /// </summary>
        public long Reference
        {
            get { return FReference; }
            set { FReference = value; }
        }

        /// <summary>
        /// Gets or sets internal property - save flag 
        /// </summary>
        public bool Saved
        {
            get { return FSaved; }
            set { FSaved = value; }
        }

        /// <summary>
        /// True if bold or italic style is not supported by font
        /// </summary>
        public bool NeedStyleSimulation
        {
            get { return FStyleSimulation; }
        }

        #endregion 

        #region Public Methods
        /// <summary>
        /// Run fill outline text metric structure
        /// </summary>
        public void FillOutlineTextMetrix()
        {
            if (FSourceFont != null)
            {
#if ! WITHOUT_UNISCRIBE
                using (Graphics g = Graphics.FromImage(tempBitmap))
                {
                    IntPtr hdc = g.GetHdc();
                    IntPtr f = FSourceFont.ToHfont();
                    try
                    {
                        SelectObject(hdc, f);
                        GetOutlineTextMetrics(hdc, Marshal.SizeOf(typeof(OutlineTextMetric)), ref FTextMetric);
                    }
                    finally
                    {
                        DeleteObject(f);
                        g.ReleaseHdc(hdc);
                    }
                }
#else
                OutlineTextMetric TextMetricTest = new OutlineTextMetric();
                TrueTypeFont font = FFontCollection[FSourceFont];
                font.GetOutlineTextMetrics(ref FTextMetric);
#endif
            }
        }

        /// <summary>
        /// Return glyph width
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int GetGlyphWidth(char c)
        {
            return FWidths[FUsedGlyphIndexes.IndexOf((ushort)c)];
        }


        /// <summary>
        /// Return font file
        /// </summary>
        /// <returns></returns>
        public byte[] GetFontData()
        {
            byte[] result = null;

            if (FSourceFont != null)
            {
#if !WITHOUT_UNISCRIBE
                using (TrueTypeCollection font_collection = new TrueTypeCollection(FSourceFont))
                {
                    TrueTypeFont.TablesID[] skip_list = new TrueTypeFont.TablesID[] 
                    {             
                        TrueTypeFont.TablesID.HorizontakDeviceMetrix,
                        TrueTypeFont.TablesID.DigitalSignature,
                        TrueTypeFont.TablesID.GlyphPosition,  
                        TrueTypeFont.TablesID.EmbedBitmapLocation,
                        TrueTypeFont.TablesID.EmbededBitmapData
                    };

                    foreach (TrueTypeFont f in font_collection.FontCollection)
                    {
                        if (f.FontVersion != 0x00010000)
                            return font_collection.GetRawFontData();
                        f.PrepareFont(skip_list);
                    }

                    TrueTypeFont font = font_collection[FSourceFont];
                    foreach (char ch in FUsedGlyphIndexes) font.AddCharacterToKeepList(ch);
                    result = font.PackFont(FontType.TrueTypeFont, true);
                    if (result == null)
                        result = font_collection.GetRawFontData();

                    //// Debug: Write packed font to file
                    //FileStream stream = new FileStream(@"C:\pres\tmyfont.ttf", FileMode.Create);
                    //stream.Write(result, 0, result.Length);
                    //stream.Close();
                }
#else
                TrueTypeFont font = FFontCollection[FSourceFont];
                foreach (char ch in FUsedGlyphIndexes) font.AddCharacterToKeepList(ch);
                result = font.PackFont(FontType.TrueTypeFont, true);
#endif
            }
            return result;
        }

        /// <summary>
        /// Remap str in glyph indexes. Return string with glyph indexes.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="rtl"></param>
        /// <returns></returns>
        public string RemapString(string str, bool rtl)
        {
            // control chars should not be here...
            str = str.Replace("\r", "").Replace("\n", "");  
            int maxGlyphs = str.Length * 3;
            ushort[] glyphs = new ushort[maxGlyphs];
            int[] widths = new int[maxGlyphs];
            int actualLength = 0;

#if ! WITHOUT_UNISCRIBE
            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                IntPtr hdc = g.GetHdc();
                IntPtr f = FSourceFont.ToHfont();
                try
                {
                    SelectObject(hdc, f);
                    ABC abc;
                    actualLength = GetGlyphIndices(hdc, str, glyphs, widths, rtl, out abc);
                }
                finally
                {
                    DeleteObject(f);                    
                    g.ReleaseHdc(hdc);
                }
            }            
#else
            TrueTypeFont font = FFontCollection[FSourceFont];
            actualLength = font.GetGlyphIndices(str, out glyphs, out widths, false);
#endif
            
            FastString sb = new FastString(actualLength);
            for (int i = 0; i < actualLength; i++)
            {
                ushort c = glyphs[i];
                if (FUsedGlyphIndexes.IndexOf(c) == -1)
                {
                    FUsedGlyphIndexes.Add(c);
                    FWidths.Add(widths[i]);
                    if (actualLength > str.Length) // ligatures found - skip entire string
                        FUsedAlphabetUnicode.Add(FTextMetric.otmTextMetrics.tmDefaultChar);
                    else
                        FUsedAlphabetUnicode.Add(str[(rtl ? actualLength - i - 1 : i)]);
                }
                sb.Append((char)c);
            }
            return sb.ToString();
        }


        public class GlyphTTF
        {
            public GraphicsPath path;
            public float width;

            public float minX;
            public float minY;
            public float maxX;
            public float maxY;
            public float baseLine;

            public GlyphTTF(GraphicsPath aPath, float aWidth)
            {
                path = aPath;
                width = aWidth;
            }

            public GlyphTTF(GraphicsPath aPath, float aWidth, float minX, float minY, float maxX, float maxY) : this(aPath, aWidth)
            {
                this.minX = minX;
                this.minY = minY;
                this.maxX = maxX;
                this.maxY = maxY;
            }

            public GlyphTTF(GraphicsPath aPath, float aWidth, float minX, float minY, float maxX, float maxY, float v) : this(aPath, aWidth, minX, minY, maxX, maxY)
            {
                this.baseLine = v;
            }

            public GlyphTTF(GraphicsPath aPath, float aWidth, float v) : this(aPath, aWidth)
            {
                this.baseLine = v;
            }
        }

        public GlyphTTF[] getGlyphString(string str, bool rtl, float size, out float paddingX, out float paddingY)
        {
            // control chars should not be here...
            str = str.Replace("\r", "").Replace("\n", "");
            int maxGlyphs = str.Length * 3;
            ushort[] glyphs = new ushort[maxGlyphs];
            int[] widths = new int[maxGlyphs];
            int actualLength = 0;
            paddingX = 0;
            paddingY = 0;
            ABC abc;
#if ! WITHOUT_UNISCRIBE
            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                IntPtr hdc = g.GetHdc();
                IntPtr f = FSourceFont.ToHfont();
                try
                {
                    SelectObject(hdc, f);
                    actualLength = GetGlyphIndices(hdc, str, glyphs, widths, rtl, out abc);
                }
                finally
                {
                    DeleteObject(f);
                    g.ReleaseHdc(hdc);
                }
            }
#else
            TrueTypeFont font = FFontCollection[FSourceFont];
            actualLength = font.GetGlyphIndices(str, out glyphs, out widths, false);
#endif

            //StringBuilder sb = new StringBuilder(actualLength);
            List<GlyphTTF> aList = new List<GlyphTTF>();
#if !WITHOUT_UNISCRIBE
            if (fontCollection == null)
            {
                fontCollection = new TrueTypeCollection(FSourceFont);
                TrueTypeFont.TablesID[] skip_list = new TrueTypeFont.TablesID[]
                {
                        TrueTypeFont.TablesID.HorizontakDeviceMetrix,
                        TrueTypeFont.TablesID.DigitalSignature,
                        TrueTypeFont.TablesID.GlyphPosition,
                        TrueTypeFont.TablesID.EmbedBitmapLocation,
                        TrueTypeFont.TablesID.EmbededBitmapData
                };

                foreach (TrueTypeFont f in fontCollection.FontCollection)
                {
                    //if (f.FontVersion != 0x00010000)
                    //    return font_collection.GetRawFontData();
                    f.PrepareFont(skip_list);
                }
            }
            fontTTF = fontCollection[FSourceFont];


            for (int i = 0; i < actualLength; i++)
            {
                ushort c = glyphs[i];
                if (FUsedGlyphIndexes.IndexOf(c) == -1)
                {
                    FUsedGlyphIndexes.Add(c);
                    FWidths.Add(widths[i]);
                    if (actualLength > str.Length) // ligatures found - skip entire string
                        FUsedAlphabetUnicode.Add(FTextMetric.otmTextMetrics.tmDefaultChar);
                    else
                        FUsedAlphabetUnicode.Add(str[(rtl ? actualLength - i - 1 : i)]);
                }
                //float minX, minY, maxX, maxY;
                GraphicsPath aPath = fontTTF.GetGlyph(c, (int)size);
                //if (c == 3) aPath = new GraphicsPath();//space SPACE SPACE!!!! what?

                aList.Add(new GlyphTTF(
                    aPath,
                    widths[i] / FSourceFont.Size * size,
                    fontTTF.baseLine * size / 0.75f));
            }
            paddingX = abc.abcA / FSourceFont.Size * size;
            paddingY = abc.abcC / FSourceFont.Size * size;
#endif
            return aList.ToArray();
        }


        /// <summary>
        /// Return english name of source font
        /// </summary>
        /// <returns></returns>
        public string GetEnglishFontName()
        {
            // get the english name of a font
            string fontName = FSourceFont.FontFamily.GetName(1033);
            FastString Result = new FastString(fontName.Length * 3);
            foreach (char c in fontName)
            {
                switch (c)
                {
                    case ' ':
                    case '%':
                    case '(':
                    case ')':
                    case '<':
                    case '>':
                    case '[':
                    case ']':
                    case '{':
                    case '}':
                    case '/':
                    case '#':
                        Result.Append("#");
                        Result.Append(((int)c).ToString("X2"));
                        break;
                    default:
                        if ((int)c > 126 || (int)c < 32)
                        {
                            Result.Append('#');
                            Result.Append(((int)c).ToString("X2"));
                        }
                        else
                            Result.Append(c);
                        break;
                }
            }
            FastString Style = new FastString(9);
            if ((FSourceFont.Style & FontStyle.Bold) > 0 && !this.FStyleSimulation)
                Style.Append("Bold");
            if ((FSourceFont.Style & FontStyle.Italic) > 0 && !this.FStyleSimulation)
                Style.Append("Italic");
            if (Style.Length > 0)
                Result.Append(",").Append(Style.ToString());
            return Result.ToString();
        }

        /// <summary>
        /// Return PANOSE string
        /// </summary>
        /// <returns></returns>
        public string GetPANOSE()
        {
            //01 05 02 02 04 00 00 00 00 00 00 00
            FastString panose = new FastString(36);
            panose.Append("01 05 ");
            panose.Append(FTextMetric.otmPanoseNumber.bFamilyType.ToString("X")).Append(" ");
            panose.Append(FTextMetric.otmPanoseNumber.bSerifStyle.ToString("X")).Append(" ");
            panose.Append(FTextMetric.otmPanoseNumber.bWeight.ToString("X")).Append(" ");
            panose.Append(FTextMetric.otmPanoseNumber.bProportion.ToString("X")).Append(" ");
            panose.Append(FTextMetric.otmPanoseNumber.bContrast.ToString("X")).Append(" ");
            panose.Append(FTextMetric.otmPanoseNumber.bStrokeVariation.ToString("X")).Append(" ");
            panose.Append(FTextMetric.otmPanoseNumber.ArmStyle.ToString("X")).Append(" ");
            panose.Append(FTextMetric.otmPanoseNumber.bLetterform.ToString("X")).Append(" ");
            panose.Append(FTextMetric.otmPanoseNumber.bMidline.ToString("X")).Append(" ");
            panose.Append(FTextMetric.otmPanoseNumber.bXHeight.ToString("X"));
            return panose.ToString();
        }
#endregion

#region Private methods
#if !WITHOUT_UNISCRIBE

        private int GetGlyphIndices(IntPtr hdc, string text, ushort[] glyphs, int[] widths, bool rtl, out ABC abc)
        {
            abc = new ABC();
            if (String.IsNullOrEmpty(text))
                return 0;

            int destIndex = 0;
            int maxGlyphs = text.Length * 3;
            int maxItems = text.Length * 2;

            List<Run> runs = Itemize(text, rtl, maxItems);
            runs = Layout(runs, rtl);
            foreach (Run run in runs)
            {
                ushort[] tempGlyphs = new ushort[maxGlyphs];
                int[] tempWidths = new int[maxGlyphs];
                int length = GetGlyphs(hdc, run, tempGlyphs, tempWidths, maxGlyphs, rtl,out abc);
                Array.Copy(tempGlyphs, 0, glyphs, destIndex, length);
                Array.Copy(tempWidths, 0, widths, destIndex, length);
                destIndex += length;                
            }

            return destIndex;
        }

        private int GetGlyphs(IntPtr hdc, Run run, ushort[] glyphs, int[] widths, int maxGlyphs, bool rtl, out ABC abc)
        {
            // initialize structures
            SCRIPT_ANALYSIS psa = run.analysis;
            ushort[] pwLogClust = new ushort[maxGlyphs];
            int pcGlyphs = 0;
            SCRIPT_VISATTR[] psva = new SCRIPT_VISATTR[maxGlyphs];
            GOFFSET[] pGoffset = new GOFFSET[maxGlyphs];
            ABC pABC = new ABC();
            // make glyphs
            ScriptShape(hdc, ref FUSCache, run.text, run.text.Length, glyphs.Length, ref psa, glyphs, pwLogClust, psva, ref pcGlyphs);
            // make widths
            ScriptPlace(hdc, ref FUSCache, glyphs, pcGlyphs, psva, ref psa, widths, pGoffset, ref pABC);
            abc = pABC;
            //int[] oldW = new int[widths.Length];
            //Array.Copy(widths, oldW, widths.Length);
            //ScriptJustify(psva, oldW, pcGlyphs, 75, 0, widths);
            return pcGlyphs;
        }

        private List<Run> Itemize(string s, bool rtl, int maxItems)
        {
            SCRIPT_ITEM[] pItems = new SCRIPT_ITEM[maxItems];
            int pcItems = 0;

            // initialize Control and State
            SCRIPT_CONTROL control = new SCRIPT_CONTROL();
            SCRIPT_STATE state = new SCRIPT_STATE();
            if (rtl)
            {
                // this is needed to start paragraph from right
                state.SetRtl();
                // to substitute arabic digits
                ScriptApplyDigitSubstitution(ref FDigitSubstitute, ref control, ref state);
            }

            // itemize
            ScriptItemize(s, s.Length, pItems.Length, ref control, ref state, pItems, ref pcItems);

            // create Run list. Note that ScriptItemize actually returns pcItems+1 items, 
            // so this can be used to calculate char range easily
            List<Run> list = new List<Run>();
            for (int i = 0; i < pcItems; i++)
            {
                string text = s.Substring(pItems[i].iCharPos, pItems[i + 1].iCharPos - pItems[i].iCharPos);
                list.Add(new Run(text, pItems[i].analysis));
            }

            return list;
        }

        private List<Run> Layout(List<Run> runs, bool rtl)
        {
            byte[] pbLevel = new byte[runs.Count];
            int[] piVisualToLogical = new int[runs.Count];

            // build the pbLevel array
            for (int i = 0; i < runs.Count; i++)
                pbLevel[i] = (byte)runs[i].analysis.state.uBidiLevel;

            // layout runs
            ScriptLayout(runs.Count, pbLevel, piVisualToLogical, null);

            // return runs in their visual order
            List<Run> visualRuns = new List<Run>();
            for (int i = 0; i < piVisualToLogical.Length; i++)
                visualRuns.Add(runs[piVisualToLogical[i]]);

            return visualRuns;
        }
#endif
#endregion

        /// <summary>
        /// Create object of ExportTTFFont.
        /// </summary>
        /// <param name="font"></param>
        public ExportTTFFont(Font font)
        {
            FDpiFX = 96f / DrawUtils.ScreenDpi;
            FSourceFont = new Font(font.Name, 750 * FDpiFX, font.Style);
            //FSourceFont = font;
            FSaved = false;
            FTextMetric = new OutlineTextMetric();
            FUsedGlyphIndexes = new List<ushort>();
            FUsedAlphabetUnicode = new List<ushort>();
            FWidths = new List<int>();
            tempBitmap = new Bitmap(1, 1);
            FUSCache = IntPtr.Zero;
            FDigitSubstitute = new SCRIPT_DIGITSUBSTITUTE();
            FStyleSimulation = false;
            foreach (string s in NeedStyelSimulationList)
                if (s == font.Name) { FStyleSimulation = true; break; }

#if !WITHOUT_UNISCRIBE
            ScriptRecordDigitSubstitution(0x0400, ref FDigitSubstitute);
#else
            FFontCollection = new TrueTypeCollection(FSourceFont);
            TrueTypeFont.TablesID[] skip_list = new TrueTypeFont.TablesID[] 
                {             
                    TrueTypeFont.TablesID.HorizontakDeviceMetrix,
                    TrueTypeFont.TablesID.DigitalSignature,
                    TrueTypeFont.TablesID.GlyphPosition,  

                    TrueTypeFont.TablesID.EmbedBitmapLocation,
                    TrueTypeFont.TablesID.EmbededBitmapData
                };

            foreach (TrueTypeFont f in FFontCollection.FontCollection)
            {
                f.PrepareFont(skip_list);
            }
#endif
        }

        /// <summary>
        /// Destructor
        /// </summary>
        public void Dispose()
        {
            // free cache
#if !WITHOUT_UNISCRIBE
            ScriptFreeCache(ref FUSCache);
#else
            FFontCollection.Dispose();
#endif
            tempBitmap.Dispose();
            FSourceFont.Dispose();
            if(fontCollection!=null)
            {
                fontCollection.Dispose();
                fontCollection = null;
            }
        }

#region Private clases
        private class Run
        {
            public SCRIPT_ANALYSIS analysis;
            public string text;

            public Run(string text, SCRIPT_ANALYSIS a)
            {
                this.text = text;
                this.analysis = a;
            }
        }
#endregion
    }
}
