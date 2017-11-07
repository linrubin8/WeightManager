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
using FastReport.Export.Html;

namespace FastReport.Export.Mht
{
    /// <summary>
    /// Represents the MHT export filter.
    /// </summary>
    public class MHTExport : ExportBase
    {
        #region Private fields
        private HTMLExport htmlExport;
        private MyRes Res;
        #endregion

        #region Public properties

        /// <summary>
        /// Enable or disable the pictures in MHT export
        /// </summary>
        public bool Pictures
        {
            get { return htmlExport.Pictures; }
            set { htmlExport.Pictures = value; }
        }

        /// <summary>
        ///  Gets or sets the Wysiwyg quality of export
        /// </summary>
        public bool Wysiwyg
        {
            get { return htmlExport.Wysiwyg; }
            set { htmlExport.Wysiwyg = value; }
        }

        /// <summary>
        /// Gets or sets the image format.
        /// </summary>
        public FastReport.Export.Html.ImageFormat ImageFormat
        {
            get { return htmlExport.ImageFormat; }
            set { htmlExport.ImageFormat = value; }
        }

        #endregion

        #region Protected methods

        /// <inheritdoc/>
        public override bool ShowDialog()
        {
            using (MHTExportForm form = new MHTExportForm())
            {
                form.Init(this);
                return form.ShowDialog() == DialogResult.OK;
            }
        }

        /// <inheritdoc/>
        protected override string GetFileFilter()
        {
        	return new MyRes("FileFilters").Get("MhtFile");
        }

        /// <inheritdoc/>
        protected override void Start()
        {           
            htmlExport.Format = HTMLExportFormat.MessageHTML;
            htmlExport.PageNumbers = PageNumbers;
            htmlExport.SinglePage = true;
            htmlExport.Navigator = false;
            htmlExport.SubFolder = false;
            Report.Export(htmlExport, Stream);
        }
        
        /// <inheritdoc/>
        protected override void Finish()
        {
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MHTExport"/> class.
        /// </summary>
        public MHTExport()
        {
            htmlExport = new HTMLExport();
            Res = new MyRes("Export,Mht");
        }
    }
}
