using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.Export;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FastReport.Export.Csv
{
    /// <summary>
    /// Represents the CSV export filter.
    /// </summary>
    public class CSVExport : ExportBase
    {
        #region Constants
        byte[] U_HEADER = { 239, 187, 191 };
        #endregion

        #region Private fields
        private ExportMatrix FMatrix;
        private string FSeparator;
        private Encoding FEncoding;
        private bool FDataOnly;
        private bool FNoQuotes;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or set the resulting file encoding.
        /// </summary>
        public Encoding Encoding
        {
            get { return FEncoding; }
            set { FEncoding = value; }
        }

        /// <summary>
        /// Gets or set the separator character used in csv format.
        /// </summary>
        public string Separator
        {
            get { return FSeparator; }
            set { FSeparator = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether to export the databand rows only.
        /// </summary>
        public bool DataOnly
        {
            get { return FDataOnly; }
            set { FDataOnly = value; }
        }

        /// <summary>
        /// Gets or sets a value that disable quotation marks for text
        /// </summary>
        public bool NoQuotes
        {
            get { return FNoQuotes; }
            set { FNoQuotes = value; }
        }

        #endregion

        #region Private Methods
        private void ExportCsvPage(Stream stream)
        {            
            int i, x, y;
            ExportIEMObject obj;            

            StringBuilder builder = new StringBuilder(FMatrix.Width * 64 * FMatrix.Height);
            for (y = 0; y < FMatrix.Height - 1; y++)
            {
                for (x = 0; x < FMatrix.Width; x++)
                {
                    i = FMatrix.Cell(x, y);
                    if (i != -1)
                    {
                        obj = FMatrix.ObjectById(i);
                        if (obj.Counter == 0)
                        {
                            if (obj.HtmlTags) obj.Text = DeleteHtmlTags(obj.Text);
                            if( ! FNoQuotes )
                                builder.Append("\"").Append(obj.Text).Append("\"");
                            else 
                                builder.Append(obj.Text);
                            obj.Counter = 1;
                        }
                        builder.Append(FSeparator);
                    }
                }

                // remove the last separator in a row
                if (builder.ToString().EndsWith(FSeparator))
                    builder.Remove(builder.Length - 1, 1);
                builder.AppendLine();
            }
            // write the resulting string to a stream            
            byte[] bytes = FEncoding.GetBytes(builder.ToString());            
            stream.Write(bytes, 0, bytes.Length);
        }
        string DeleteHtmlTags(string text)
        {
            return Regex.Replace(text, @"<[^>]*>", String.Empty);
        }
        #endregion        

        #region Protected Methods
        /// <inheritdoc/>
        protected override void Start()
        {
            if (FEncoding == Encoding.UTF8)
                Stream.Write(U_HEADER, 0, 3);
        }

        /// <inheritdoc/>
        protected override void ExportPageBegin(ReportPage page)
        {
            base.ExportPageBegin(page);
            FMatrix = new ExportMatrix();
            FMatrix.Inaccuracy = 0.5f;
            FMatrix.PlainRich = true;
            FMatrix.AreaFill = false;
            FMatrix.CropAreaFill = true;
            FMatrix.Report = Report;
            FMatrix.Images = false;
            FMatrix.WrapText = false;
            FMatrix.DataOnly = FDataOnly;
            FMatrix.ShowProgress = ShowProgress;
            FMatrix.AddPageBegin(page);
        }

        /// <inheritdoc/>
        protected override void ExportBand(Base band)
        {
            base.ExportBand(band);
            if (band.Parent == null) return;
            FMatrix.AddBand(band);
        }

        /// <inheritdoc/>
        protected override void ExportPageEnd(ReportPage page)
        {
            FMatrix.AddPageEnd(page);
            FMatrix.Prepare();
            ExportCsvPage(Stream);
            base.ExportPageEnd(page);
        }

        /// <inheritdoc/>
        protected override string GetFileFilter()
        {
            return new MyRes("FileFilters").Get("CsvFile");
        }
        #endregion

        #region Public Methods
        /// <inheritdoc/>
        public override bool ShowDialog()
        {
          using (CsvExportForm form = new CsvExportForm())
          {
            form.Init(this);
            return form.ShowDialog() == DialogResult.OK;
          }
        }

        /// <inheritdoc/>
        public override void Serialize(FRWriter writer)
        {
          base.Serialize(writer);
          writer.WriteStr("Separator", Separator);
          writer.WriteBool("DataOnly", DataOnly);
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CSVExport"/> class.
        /// </summary>       
        public CSVExport()
        {                         
            FSeparator = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
            FEncoding = Encoding.Default;
            FDataOnly = false;
            FNoQuotes = false;
        }
    }
}
