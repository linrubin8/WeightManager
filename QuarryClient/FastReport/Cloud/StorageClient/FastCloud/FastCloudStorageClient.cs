using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using FastReport.Export;
using FastReport.Cloud.OAuth;
using FastReport.Utils;

namespace FastReport.Cloud.StorageClient.FastCloud
{
    /// <summary>
    /// FastCloud storage client.
    /// </summary>
    public class FastCloudStorageClient : CloudStorageClient
    {
        #region Constants

        private const string HOST = "https://cloud.fast-report.com";

        #endregion // Constants

        #region Fields

        private string accessToken;
        private string reportUrl;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; }
        }

        /// <summary>
        /// Gets the report URL that can be used to download report from cloud.
        /// </summary>
        public string ReportUrl
        {
            get { return reportUrl; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FastCloudStorageClient"/> class.
        /// </summary>
        public FastCloudStorageClient() : base()
        {
            accessToken = "";
            reportUrl = "";
        }

        #endregion // Constructors

        #region Private Methods

        private byte[] BuildGetAccessTokenRequestContent(string email, string password)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("email", email);
            data.Add("password", password);
            return Encoding.UTF8.GetBytes(HttpUtils.UrlDataEncode(data));
        }

        #endregion // Private Methods

        #region Public Methods

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <returns>The access token string.</returns>
        public string GetAccessToken(string email, string password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(HOST + @"/api/v1/tokens.json");
            request.Method = HttpMethod.Post;
            RequestUtils.SetProxySettings(request, ProxySettings);

            request.ContentType = "application/x-www-form-urlencoded";
            byte[] content = BuildGetAccessTokenRequestContent(email, password);
            request.ContentLength = content.Length;
            using (Stream rs = request.GetRequestStream())
            {
                rs.Write(content, 0, content.Length);
            }

            try
            {
                WebResponse response = request.GetResponse();
                accessToken = Parser.ParseFastCloudToken(response.GetResponseStream());
            }
            catch (Exception ex)
            {
                if (!Config.WebMode)
                {
                    FRMessageBox.Error(ex.Message);
                }
            }

            return accessToken;
        }

        /// <inheritdoc/>
        public override void SaveReport(Report report, ExportBase export)
        {
            using (MemoryStream ms = PrepareToSave(report, null))
            {
                try
                {
                    string uri = String.Format(HOST + @"/api/v1/reports.json?user_token={0}", accessToken);
                    WebRequest request = WebRequest.Create(uri);
                    request.Method = HttpMethod.Post;
                    RequestUtils.SetProxySettings(request, ProxySettings);

                    request.ContentType = "multipart/form-data; boundary=foo_bar_baz";
                    List<byte> content = new List<byte>();

                    StringBuilder sb = new StringBuilder("--foo_bar_baz\r\n");
                    sb.Append(String.Format("Content-Disposition: form-data; name=\"report[frx_file]\"; filename=\"{0}\"\r\n", Filename));
                    sb.Append("Content-Type: application/octet-stream\r\n");
                    sb.Append("\r\n");
                    content.AddRange(Encoding.UTF8.GetBytes(sb.ToString()));

                    int msLength = Convert.ToInt32(ms.Length);
                    byte[] msBuffer = new byte[msLength];
                    ms.Read(msBuffer, 0, msLength);
                    content.AddRange(msBuffer);

                    sb = new StringBuilder("--foo_bar_baz--");
                    content.AddRange(Encoding.UTF8.GetBytes(sb.ToString()));

                    int length = content.Count;
                    byte[] buffer = new byte[length];
                    buffer = content.ToArray();
                    request.ContentLength = buffer.Length;
                    using (Stream rs = request.GetRequestStream())
                    {
                        rs.Write(buffer, 0, buffer.Length);
                    }

                    string response = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                    if (!String.IsNullOrEmpty(response))
                    {
                        int startIndex = response.IndexOf("id\":") + 5;
                        int endIndex = response.IndexOf(",", startIndex);
                        string id = response.Substring(startIndex, endIndex - startIndex - 1);
                        reportUrl = HOST + "/reports/" + id;
                    }
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
