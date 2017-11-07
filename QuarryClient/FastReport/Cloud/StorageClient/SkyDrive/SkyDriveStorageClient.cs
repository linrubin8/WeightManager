using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using FastReport.Export;
using FastReport.Cloud.OAuth;
using FastReport.Utils;

namespace FastReport.Cloud.StorageClient.SkyDrive
{
    /// <summary>
    /// SkyDrive cloud storage client.
    /// </summary>
    public class SkyDriveStorageClient : CloudStorageClient
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
        /// Initializes a new instance of the <see cref="SkyDriveStorageClient"/> class.
        /// </summary>
        public SkyDriveStorageClient() : base()
        {
            clientInfo = new ClientInfo("", "", "");
            authCode = "";
            accessToken = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkyDriveStorageClient"/> class.
        /// </summary>
        /// <param name="clientInfo">The client info.</param>
        public SkyDriveStorageClient(ClientInfo clientInfo) : base()
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
            data.Add("client_id", clientInfo.Id);
            data.Add("client_secret", clientInfo.Secret);
            data.Add("redirect_uri", @"https://login.live.com/oauth20_desktop.srf");
            data.Add("grant_type", "authorization_code");
            data.Add("code", authCode);
            return Encoding.UTF8.GetBytes(HttpUtils.UrlDataEncode(data));
        }

        #endregion // Private Methods

        #region Public Methods

        /// <summary>
        /// Gets the authorization URL.
        /// </summary>
        /// <returns>The authorization URL string.</returns>
        public string GetAuthorizationUrl()
        {
            return String.Format(@"https://login.live.com/oauth20_authorize.srf?client_id={0}&redirect_uri=https://login.live.com/oauth20_desktop.srf&response_type=code&scope=wl.skydrive_update", clientInfo.Id);
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <returns>The access token value.</returns>
        public string GetAccessToken()
        {
            string uri = String.Format(@"?client_id={0}&client_secret={1}&redirect_uri=https://login.live.com/oauth20_desktop.srf&grant_type=authorization_code&code={2}", clientInfo.Id, clientInfo.Secret, authCode);
            WebRequest request = WebRequest.Create(@"https://login.live.com/oauth20_token.srf");
            request.Method = HttpMethod.Post;
            RequestUtils.SetProxySettings(request, ProxySettings);

            request.ContentType = "application/x-www-form-urlencoded";
            byte[] content = BuildGetAccessTokenRequestContent();
            request.ContentLength = content.Length;
            using (Stream rs = request.GetRequestStream())
            {
                rs.Write(content, 0, content.Length);
            }

            WebResponse response = request.GetResponse();
            accessToken = Parser.ParseSkyDriveToken(response.GetResponseStream());
            return accessToken;
        }

        /// <inheritdoc/>
        public override void SaveReport(Report report, ExportBase export)
        {
            using (MemoryStream ms = PrepareToSave(report, export))
            {
                string url = String.Format(@"https://apis.live.net/v5.0/me/skydrive/files/{0}?access_token={1}", Filename, accessToken);
                try
                {
                    WebRequest request = WebRequest.Create(url);
                    request.Method = HttpMethod.Put;
                    RequestUtils.SetProxySettings(request, ProxySettings);

                    int length = Convert.ToInt32(ms.Length);
                    byte[] buffer = new byte[length];
                    ms.Read(buffer, 0, length);
                    request.ContentLength = buffer.Length;
                    using (Stream rs = request.GetRequestStream())
                    {
                        rs.Write(buffer, 0, length);
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
