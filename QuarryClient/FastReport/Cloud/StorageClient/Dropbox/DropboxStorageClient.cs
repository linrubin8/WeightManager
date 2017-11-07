using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using FastReport.Export;
using FastReport.Utils;

namespace FastReport.Cloud.StorageClient.Dropbox
{
    /// <summary>
    /// Dropbox cloud storage client.
    /// </summary>
    public class DropboxStorageClient : CloudStorageClient
    {
        #region Constants
        
        /// <summary>
        /// The base URL for files_put command.
        /// </summary>
        public const string FILES_PUT_URL_BASE = "https://content.dropboxapi.com/2/files/upload";

        #endregion // Constants

        #region Fields
        
        private string accessToken;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the application access token.
        /// </summary>
        public string AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DropboxStorageClient"/> class.
        /// </summary>
        public DropboxStorageClient() : base()
        {
            accessToken = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DropboxStorageClient"/> class.
        /// </summary>
        /// <param name="accessToken">The Dropbox application access token.</param>
        public DropboxStorageClient(string accessToken) : base()
        {
            this.accessToken = accessToken;
        }

        #endregion // Constructors

        #region Public Methods

        /// <inheritdoc/>
        public override void SaveReport(Report report, ExportBase export)
        {
            using (MemoryStream ms = PrepareToSave(report, export))
            {
                try
                {
                    HttpWebRequest request = WebRequest.Create(FILES_PUT_URL_BASE) as HttpWebRequest;
                    request.Method = HttpMethod.Post;
                    RequestUtils.SetProxySettings(request, ProxySettings);
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    request.Headers.Add("Dropbox-API-Arg", "{\"path\": \"/" + Filename + "\",\"mode\": \"overwrite\",\"autorename\": false,\"mute\": false}");
                    request.ContentType = "application/octet-stream";

                    int msLength = Convert.ToInt32(ms.Length);
                    byte[] msBuffer = new byte[msLength];
                    ms.Read(msBuffer, 0, msLength);
                    List<byte> content = new List<byte>();
                    content.AddRange(msBuffer);
                    int length = content.Count;
                    byte[] buffer = new byte[length];
                    buffer = content.ToArray();
                    request.ContentLength = buffer.Length;

                    using (Stream rs = request.GetRequestStream())
                    {
                        rs.Write(buffer, 0, buffer.Length);
                    }

                    WebResponse response = request.GetResponse();
                    Stream stream = response.GetResponseStream();
                }
                catch (Exception ex)
                {
                    throw new CloudStorageException(ex.Message, ex);
                }
            }
        }

        #endregion // Public Methods
    }
}
