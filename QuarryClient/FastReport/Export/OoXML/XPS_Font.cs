using FastReport.Export.TTF;
using FastReport.Utils;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FastReport.Export.OoXML
{
    /// <summary>
    ///  Font container
    /// </summary>
    internal class XPS_Font : OoXMLBase, IDisposable
    {
        #region DLL import
        [DllImport("Gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("Gdi32.dll")]
        private static extern IntPtr DeleteObject(IntPtr hgdiobj);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetFontData(IntPtr hdc, uint dwTable, uint dwOffset, [In, Out] byte[] lpvBuffer, uint cbData);
        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetFontData(IntPtr hdc, uint dwTable, uint dwOffset, [In, Out] IntPtr lpvBuffer, uint cbData);
        #endregion

        #region "Class overrides"
        public override string RelationType { get { return "http://schemas.microsoft.com/xps/2005/06/required-resource"; } }
        public override string ContentType { get { return null; } } //"application/vnd.openxmlformats-officedocument.presentationml.slide+xml"; } }
        public override string FileName { get { return "/Resources/" + GeneratedName + ".odttf"; } }
        //        public override string FileName { get { return "/Resources/" + GeneratedName + ".ttf"; } }
        #endregion

        private Hashtable ListOfUsedGlyphs;

        private FontFamily FontFamily;

        private ExportTTFFont export_font;

        internal float Size;
        internal FontStyle Style;
        internal float Height;
        
        public Guid FontGuid;
        public string GeneratedName;

        internal Font SourceFont 
        { 
            set 
            { 
                FontFamily = value.FontFamily; 
                Size = value.Size; 
                Style = value.Style;
                Height = value.Height;
            } 
        }

        /// <summary>
        ///  Font obfuscation procedure
        /// </summary>
        internal void Obfuscation(byte[] data)
        {
            if (data.Length < 32) throw new Exception("Font header error");

            byte[] guidByteArray = new byte[16];
            string guidString = FontGuid.ToString("N");

            for (int i = 0; i < guidByteArray.Length; i++)
            {
                guidByteArray[i] = Convert.ToByte(guidString.Substring(i * 2, 2), 16);
            }

            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    data[i + j * 16] ^= guidByteArray[15 - i];
                }
            }
        }

        public byte[] ExportFont(XPSExport export)
        {
            float FDpiFX = 96f / DrawUtils.ScreenDpi;
            byte[] result = null;

            using (Font current_font = new Font(this.FontFamily, this.Size * FDpiFX, this.Style))
            {
                try
                {
                    result = export_font.GetFontData();

                    Obfuscation(result);

                    MemoryStream file = new MemoryStream();
                    file.Write(result, 0, result.Length);

                    file.Position = 0;
                    export.Zip.AddStream(ExportUtils.TruncLeadSlash(this.FileName), file);                    
                }
                finally
                {
                }
            }
            return result;
        }

        public XPS_Font(Font f)
        {
            int i, j;
            
            this.FontFamily = f.FontFamily;
            this.Size = f.Size;
            this.Style = f.Style;

            FontGuid = Guid.NewGuid();

            export_font = new ExportTTFFont(f);

            ListOfUsedGlyphs = new Hashtable();

            string Name = FontGuid.ToString("N");
            GeneratedName = "";
            for (i = 0; i < 8; i++) GeneratedName += Name[i];
            GeneratedName += '-';
            for (j = 0; j < 4; j++, i++) GeneratedName += Name[i];
            GeneratedName += '-';
            for (j = 0; j < 4; j++, i++) GeneratedName += Name[i];
            GeneratedName += '-';
            for (j = 0; j < 4; j++, i++) GeneratedName += Name[i];
            GeneratedName += '-';
            for (j = 0; j < 12; j++, i++) GeneratedName += Name[i];
        }

        // cut off --- Samuray
        internal string AddString(string str, bool rtl)
        {
            str = export_font.RemapString(str, rtl);
            foreach (char c in str ) AddGlyph(c);
            return str;
        }
        // end cut off

        private void AddGlyph(char ch)
        {
            ushort symbol = (ushort) ch;
            if (!ListOfUsedGlyphs.ContainsKey(symbol))
            {
                int id = ListOfUsedGlyphs.Count;
                ListOfUsedGlyphs.Add(symbol, id);
            }
        }

        internal string GetXpsIndexes(string text)
        {
            int i;
            StringBuilder indices = new StringBuilder(text.Length * 4);
            indices.Append( "Indices=\"" );

            //float WIDTH_DIVIDER = 10;

            for (i = 0; i < text.Length; i++ )
            {
                indices.Append(((int)text[i]).ToString()).
                    //Append(',').
                    //Append(Converter.ToString(export_font.GetGlyphWidth(text[i]) / WIDTH_DIVIDER)).
                    Append(';');
            }

            indices.Remove(indices.Length - 1, 1);  
            indices.Append("\" ");

            return indices.ToString();
        }

        #region IDisposable Members

        public void Dispose()
        {
            export_font.Dispose();
        }

        #endregion
    }
}
