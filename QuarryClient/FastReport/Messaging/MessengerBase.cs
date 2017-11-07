using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FastReport.Export;

namespace FastReport.Messaging
{
    /// <summary>
    /// The base class for all messengers.
    /// </summary>
    public class MessengerBase
    {
        #region Fields

        private string filename;
        private ProxySettings proxySettings;

        #endregion // Fields 

        #region Properties

        /// <summary>
        /// Gets the filename.
        /// </summary>
        public string Filename
        {
            get { return filename; }
            protected set { filename = value; }
        }

        /// <summary>
        /// Gets or sets the proxy settings.
        /// </summary>
        public ProxySettings ProxySettings
        {
            get { return proxySettings; }
            set { proxySettings = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessengerBase"/> class.
        /// </summary>
        public MessengerBase()
        {
            filename = "";
            proxySettings = null;
        }

        #endregion // Constructors

        #region Protected Methods

        /// <summary>
        /// Authorizes the user.
        /// </summary>
        /// <returns>True if user has been successfully authorized.</returns>
        protected virtual bool Authorize()
        {
            return true;
        }

        /// <summary>
        /// Prepares the report before it will be send.
        /// </summary>
        /// <param name="report">The report template.</param>
        /// <param name="export">The export filter.</param>
        /// <returns>Memory stream that contains prepared report.</returns>
        protected MemoryStream PrepareToSave(Report report, ExportBase export)
        {
            MemoryStream stream = new MemoryStream();
            if (export != null)
            {
                export.OpenAfterExport = false;
                if (!export.HasMultipleFiles)
                {
                    export.Export(report, stream);
                }
                else
                {
                    export.ExportAndZip(report, stream);
                }
            }
            else
            {
                report.PreparedPages.Save(stream);
            }

            filename = "Report";
            if (!String.IsNullOrEmpty(report.FileName))
            {
                filename = Path.GetFileNameWithoutExtension(report.FileName);
            }

            string ext = ".fpx";
            if (export != null)
            {
                if (!export.HasMultipleFiles)
                {
                    ext = export.FileFilter.Substring(export.FileFilter.LastIndexOf('.')).ToLower();
                }
                else
                {
                    ext = ".zip";
                }
            }

            filename += ext;
            stream.Position = 0;
            return stream;
        }

        #endregion // Protected Methods

        #region Public Methods

        /// <summary>
        /// Sends the report.
        /// </summary>
        /// <param name="report">The report template that should be sent.</param>
        /// <param name="export">The export filter that should export template before.</param>
        /// <returns>True if report has been successfully sent.</returns>
        public virtual bool SendReport(Report report, ExportBase export)
        {
            return true;
        }

        #endregion // Public Methods
    }
}
