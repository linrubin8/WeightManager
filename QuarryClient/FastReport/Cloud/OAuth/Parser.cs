using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FastReport.Cloud.OAuth
{
    /// <summary>
    /// Represents parser for parse OAuth responses.
    /// </summary>
    public static class Parser
    {
        #region Private Methods

        private static Dictionary<string, string> ParseParameters(Stream stream)
        {
            Dictionary<string, string> parsedParameters = new Dictionary<string, string>();
            StreamReader reader = new StreamReader(stream);
            string data = reader.ReadToEnd();
            if (!String.IsNullOrEmpty(data))
            {
                string[] parameters = data.Split('&');
                foreach (string param in parameters)
                {
                    string[] pair = param.Split('=');
                    parsedParameters.Add(pair[0], pair[1]);
                }
            }
            return parsedParameters;
        }

        private static string ClearJsonString(string str)
        {
            str = str.Trim();
            int firstIndex = str.IndexOf("\"");
            int lastIndex = str.LastIndexOf("\"");
            if (firstIndex != -1 && lastIndex != -1)
            {
                str = str.Substring(firstIndex + 1, lastIndex - firstIndex - 1);
            }
            return str;
        }

        private static Dictionary<string, string> ParseJsonParameters(Stream stream)
        {
            Dictionary<string, string> parsedParameters = new Dictionary<string, string>();
            StreamReader reader = new StreamReader(stream);
            string data = reader.ReadToEnd();
            if (!String.IsNullOrEmpty(data))
            {
                string[] lines = data.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    int index = line.IndexOf('{');
                    if (index > -1)
                    {
                        line = line.Remove(index, 1);
                    }

                    index = line.IndexOf('}');
                    if (index > -1)
                    {
                        line = line.Remove(index, 1);
                    }

                    string[] pairs = line.Split(',');
                    for (int j = 0; j < pairs.Length; j++)
                    {
                        string[] pair = pairs[j].Split(':');
                        if (pair.Length == 2)
                        {
                            string key = ClearJsonString(pair[0]);
                            string value = ClearJsonString(pair[1]);
                            parsedParameters.Add(key, value);
                        }
                    }
                }
            }
            return parsedParameters;
        }

        #endregion // Private Methods

        #region Public Methods

        /// <summary>
        /// Parses token information in stream.
        /// </summary>
        /// <param name="stream">The stream for parse.</param>
        /// <returns>The OAuth token.</returns>
        public static Token ParseToken(Stream stream)
        {
            Dictionary<string, string> parameters = ParseParameters(stream);
            return new Token(parameters["oauth_token"], parameters["oauth_token_secret"]);
        }

        /// <summary>
        /// Parses token information in stream for SkyDrive.
        /// </summary>
        /// <param name="stream">The stream for parse.</param>
        /// <returns>The SkyDrive access token.</returns>
        public static string ParseSkyDriveToken(Stream stream)
        {
            Dictionary<string, string> parameters = ParseJsonParameters(stream);
            return Uri.UnescapeDataString(parameters["access_token"]);
        }

        /// <summary>
        /// Parses token information in stream for Google Drive.
        /// </summary>
        /// <param name="stream">The stream for parse.</param>
        /// <returns>The Google Drive access token.</returns>
        public static string ParseGoogleDriveToken(Stream stream)
        {
            Dictionary<string, string> parameters = ParseJsonParameters(stream);
            return Uri.UnescapeDataString(parameters["access_token"]);
        }

        /// <summary>
        /// Parses token information in stream for FastCloud.
        /// </summary>
        /// <param name="stream">The stream for parse.</param>
        /// <returns>The FastCloud access token.</returns>
        public static string ParseFastCloudToken(Stream stream)
        {
            Dictionary<string, string> parameters = ParseJsonParameters(stream);
            return Uri.UnescapeDataString(parameters["token"]);
        }

        #endregion // Public Methods
    }
}
