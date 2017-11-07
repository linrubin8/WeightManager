using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.Globalization;
using System.Security.Cryptography;

namespace FastReport.Cloud.OAuth
{
    /// <summary>
    /// API for OAuth protocol.
    /// </summary>
    public class Auth
    {
        #region Constants

        private const string UnreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.~_-";

        #endregion // Constants

        #region Private Methods

        private string GenerateTimestamp()
        {
            DateTime startPoint = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = DateTime.UtcNow - startPoint;
            return Convert.ToInt64(span.TotalSeconds).ToString();
        }

        private string GenerateNonce()
        {
            Random rand = new Random();
            return rand.Next().ToString();
        }

        private List<RequestParameter> GetOAuthRequestParameters(string query)
        {
            List<RequestParameter> result = new List<RequestParameter>();
            if (query.StartsWith("?"))
            {
                query = query.Remove(0, 1);
            }
            if (!string.IsNullOrEmpty(query))
            {
                string[] parameters = query.Split('&');
                foreach (string parameter in parameters)
                {
                    if (!string.IsNullOrEmpty(parameter) && !parameter.StartsWith("oauth_"))
                    {
                        if (parameter.IndexOf('=') >= 0)
                        {
                            string[] p = parameter.Split('=');
                            result.Add(new RequestParameter(p[0], p[1]));
                        }
                        else
                        {
                            result.Add(new RequestParameter(parameter, ""));
                        }
                    }
                }
            }
            return result;
        }

        private string UrlEncode(string str)
        {
            string result = "";
            if (!string.IsNullOrEmpty(str))
            {
                StringBuilder sb = new StringBuilder(str.Length);
                for (int i = 0; i < str.Length; i++)
                {
                    if (UnreservedChars.IndexOf(str[i]) >= 0)
                    {
                        sb.Append(str[i]);
                    }
                    else
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(str[i].ToString());
                        for (int j = 0; j < bytes.Length; j++)
                        {
                            sb.AppendFormat(CultureInfo.InvariantCulture, "%{0:X2}", bytes[j]);
                        }
                    }
                }
                result = sb.ToString();
            }
            return result;
        }

        private string GenerateSignatureBase(Uri uri, ConsumerContext consumer, Token token, string httpMethod, string timestamp, string nonce, out string normUrl, out string normParameters)
        {
            normUrl = "";
            normParameters = "";

            List<RequestParameter> parameters = GetOAuthRequestParameters(uri.Query);
            parameters.Add(new RequestParameter("oauth_version", "1.0"));
            parameters.Add(new RequestParameter("oauth_nonce", nonce));
            parameters.Add(new RequestParameter("oauth_timestamp", timestamp));
            parameters.Add(new RequestParameter("oauth_signature_method", consumer.SignatureMethod));
            parameters.Add(new RequestParameter("oauth_consumer_key", consumer.ConsumerKey));
            if (!string.IsNullOrEmpty(token.TokenKey))
            {
                parameters.Add(new RequestParameter("oauth_token", token.TokenKey));
            }
            parameters.Sort(new RequestParameterComparer());
            normParameters = NormalizeParameters(parameters);

            normUrl = string.Format("{0}://{1}", uri.Scheme, uri.Host);
            if (!((uri.Scheme == "http" && uri.Port == 80) || (uri.Scheme == "https" && uri.Port == 443)))
            {
                normUrl += ":" + uri.Port;
            }
            normUrl += uri.AbsolutePath;

            StringBuilder signatureBase = new StringBuilder();
            signatureBase.AppendFormat("{0}&", httpMethod);
            signatureBase.AppendFormat("{0}&", UrlEncode(normUrl));
            signatureBase.AppendFormat("{0}", UrlEncode(normParameters));

            return signatureBase.ToString();
        }

        private string GenerateHashSignature(string signatureBase, HashAlgorithm alg)
        {
            string result = "";
            if (!string.IsNullOrEmpty(signatureBase))
            {
                byte[] buff = Encoding.UTF8.GetBytes(signatureBase);
                byte[] hash = alg.ComputeHash(buff);
                result = Convert.ToBase64String(hash);
            }
            return result;
        }

        private string GenerateSignature(Uri uri, ConsumerContext consumer, Token token, string httpMethod, string timestamp, string nonce, out string normUrl, out string normParameters)
        {
            normUrl = "";
            normParameters = "";
            string result = "";
            StringBuilder sb = new StringBuilder();

            switch (consumer.SignatureMethod)
            {
                case SignatureMethod.Plaintext:
                    sb.AppendFormat("{0}&{1}", consumer.ConsumerSecret, token.TokenSecret);
                    result = sb.ToString();
                    break;

                case SignatureMethod.HmacSha1:
                    string signatureBase = GenerateSignatureBase(uri, consumer, token, httpMethod, timestamp, nonce, out normUrl, out normParameters);
                    using (HMACSHA1 func = new HMACSHA1())
                    {
                        func.Key = Encoding.UTF8.GetBytes(string.Format("{0}&{1}", UrlEncode(consumer.ConsumerSecret), UrlEncode(token.TokenSecret)));
                        result = GenerateHashSignature(signatureBase, func);
                    }
                    break;

                default:
                    throw new ArgumentException("Unknown signature method", "signatureMethod");
            }

            return result;
        }

        private string NormalizeParameters(List<RequestParameter> parameters)
        {
            StringBuilder np = new StringBuilder();
            foreach (RequestParameter p in parameters)
            {
                np.AppendFormat("{0}={1}", p.Name, p.Value);
                np.Append("&");
            }
            np.Remove(np.Length - 1, 1);
            return np.ToString();
        }

        #endregion // Private Methods

        #region Public Methods

        /// <summary>
        /// Builds signed URL.
        /// </summary>
        /// <param name="baseTokenUrl">The base token URL.</param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="consumer">The consumer context.</param>
        /// <param name="token">The request token.</param>
        /// <returns>Signed URL.</returns>
        public string BuildSignedUrl(string baseTokenUrl, string method, ConsumerContext consumer, Token token)
        {
            string normUrl = "";
            string normParameters = "";

            string nonce = GenerateNonce();
            string timestamp = GenerateTimestamp();

            string signature = GenerateSignature(new Uri(baseTokenUrl), consumer, token, method, timestamp, nonce, out normUrl, out normParameters);
            signature = HttpUtils.UrlEncode(signature);

            StringBuilder sb = new StringBuilder(normUrl);
            sb.AppendFormat("?");
            sb.AppendFormat(normParameters);
            sb.AppendFormat("&oauth_signature={0}", signature);

            return sb.ToString();
        }

        #endregion // Public Methods
    }

    /// <summary>
    /// Represents the signature method.
    /// </summary>
    public static class SignatureMethod
    {
        #region Constants

        /// <summary>
        /// Signature method PLAINTEXT.
        /// </summary>
        public const string Plaintext = "PLAINTEXT";

        /// <summary>
        /// Signature method HMAC-SHA1.
        /// </summary>
        public const string HmacSha1 = "HMAC-SHA1";

        /// <summary>
        /// Signature method RSA-SHA1.
        /// </summary>
        public const string RsaSha1 = "RSA-SHA1";

        #endregion // Constants
    }
}
