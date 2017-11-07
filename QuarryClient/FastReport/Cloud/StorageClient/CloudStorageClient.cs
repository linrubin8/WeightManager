using System;
using System.IO;
using FastReport.Export;
using FastReport.Utils;

namespace FastReport.Cloud.StorageClient
{
    /// <summary>
    /// The base class for all cloud storage clients.
    /// </summary>
    public abstract class CloudStorageClient
    {
        #region Fields

        private string filename;
        private bool isUserAuthorized;
        private CloudProxySettings proxySettings;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        protected string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        /// <summary>
        /// Gets or set the information is user authorized or not.
        /// </summary>
        public bool IsUserAuthorized
        {
            get { return isUserAuthorized; }
            set { isUserAuthorized = value; }
        }

        /// <summary>
        /// Gets or sets the proxy settings of a client.
        /// </summary>
        public CloudProxySettings ProxySettings
        {
            get { return proxySettings; }
            set { proxySettings = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudStorageClient"/> class.
        /// </summary>
        public CloudStorageClient()
        {
            filename = "";
            isUserAuthorized = false;
            proxySettings = null;
        }

        #endregion // Constructors

        #region Protected Methods

        /// <summary>
        /// Prepares report before it will be saved to cloud storage.
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
                    ext = export.FileFilter.Substring(export.FileFilter.LastIndexOf('.'));
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
        /// Saves the report to cloud storage.
        /// </summary>
        /// <param name="report">The report template that should be saved.</param>
        /// <param name="export">The export filter that should export template before.</param>
        /// <exception cref="CloudStorageException"/>
        public abstract void SaveReport(Report report, ExportBase export);

        #endregion // Public Methods
    }
}
