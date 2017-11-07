using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using FastReport.Export;
using FastReport.Cloud.OAuth;
using FastReport.Cloud.StorageClient.SkyDrive;
using FastReport.Utils;

namespace FastReport.Cloud.StorageClient.GoogleDrive
{
    /// <summary>
    /// Google Drive cloud storage client.
    /// </summary>
    public class GoogleDriveStorageClient : CloudStorageClient
    {
        #region Fields

        private ClientInfo clientInfo;
        private string authCode;
        private string accessToken;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the client info.
        /// </summary>
        public ClientInfo ClientInfo
        {
            get { return clientInfo; }
            set { clientInfo = value; }
        }

        /// <summary>
        /// Gets or sets the authorization code.
        /// </summary>
        public string AuthCode
        {
            get { return authCode; }
            set { authCode = value; }
        }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleDriveStorageClient"/> class.
        /// </summary>
        public GoogleDriveStorageClient() : base()
        {
            this.clientInfo = new ClientInfo("", "", "");
            authCode = "";
            accessToken = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleDriveStorageClient"/> class.
        /// </summary>
        /// <param name="clientInfo">The storage client info.</param>
        public GoogleDriveStorageClient(ClientInfo clientInfo) : base()
        {
            this.clientInfo = clientInfo;
            authCode = "";
            accessToken = "";
        }

        #endregion // Constructors

        #region Private Methods

        private byte[] BuildGetAccessTokenRequestContent()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("code", authCode);
            data.Add("client_id", clientInfo.Id);
            data.Add("client_secret", clientInfo.Secret);
            data.Add("redirect_uri", @"urn:ietf:wg:oauth:2.0:oob");
            data.Add("grant_type", "authorization_code");
            return Encoding.UTF8.GetBytes(HttpUtils.UrlDataEncode(data));
        }

        #endregion // Private Methods

        #region Public Methods

        /// <summary>
        /// Gets the authorization URL.
        /// </summary>
        /// <returns>The authorization URL stirng.</returns>
        public string GetAuthorizationUrl()
        {
            return String.Format(@"https://accounts.google.com/o/oauth2/auth?scope={0}&redirect_uri={1}&response_type=code&client_id={2}",
                "https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fdrive.file",
                "urn:ietf:wg:oauth:2.0:oob", clientInfo.Id);
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <returns>The access token string.</returns>
        public string GetAccessToken()
        {
            string uri = String.Format(@"?client_id={0}&client_secret={1}&redirect_uri=urn:ietf:wg:oauth:2.0:oob&grant_type=authorization_code&code={2}", clientInfo.Id, clientInfo.Secret, authCode);
            WebRequest request = WebRequest.Create(@"https://accounts.google.com/o/oauth2/token");
            request.Method = HttpMethod.Post;
            RequestUtils.SetProxySettings(request, ProxySettings);

            request.ContentType = "application/x-www-form-urlencoded";
            byte[] content = BuildGetAccessTokenRequestContent();
            request.ContentLength = content.Length;
            //!!! need exception
            using (Stream rs = request.GetRequestStream())
            {
                rs.Write(content, 0, content.Length);
            }

            WebResponse response = request.GetResponse();
            accessToken = Parser.ParseGoogleDriveToken(response.GetResponseStream());

            return accessToken;
        }

        /// <inheritdoc/>
        public override void SaveReport(Report report, ExportBase export)
        {
            using (MemoryStream ms = PrepareToSave(report, export))
            {
                try
                {
                    string uri = String.Format(@"https://www.googleapis.com/upload/drive/v2/files?uploadType=multipart&access_token={0}", accessToken);
                    WebRequest request = WebRequest.Create(uri);
                    request.Method = HttpMethod.Post;
                    RequestUtils.SetProxySettings(request, ProxySettings);
                    request.ContentType = "multipart/related; boundary=foo_bar_baz";
                    List<byte> content = new List<byte>();

                    StringBuilder sb = new StringBuilder("--foo_bar_baz\r\n");
                    sb.Append("Content-Type: application/json; charset=UTF-8\r\n");
                    sb.Append("\r\n");
                    sb.Append("{\r\n");
                    sb.AppendFormat("\"title\": \"{0}\"\r\n", Filename);
                    sb.Append("}\r\n");
                    sb.Append("\r\n");
                    sb.Append("--foo_bar_baz\r\n");
                    sb.Append("Content-Type: application/octet-stream\r\n");
                    sb.Append("\r\n");
                    content.AddRange(Encoding.UTF8.GetBytes(sb.ToString()));

                    int msLength = Convert.ToInt32(ms.Length);
                    byte[] msBuffer = new byte[msLength];
                    ms.Read(msBuffer, 0, msLength);
                    content.AddRange(msBuffer);

                    sb = new StringBuilder("\r\n");
                    sb.Append("--foo_bar_baz--");
                    content.AddRange(Encoding.UTF8.GetBytes(sb.ToString()));

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
