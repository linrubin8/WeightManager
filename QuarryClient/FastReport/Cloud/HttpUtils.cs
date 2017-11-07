using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FastReport.Cloud
{
    /// <summary>
    /// Static class that contains HTTP utilities.
    /// </summary>
    internal static class HttpUtils
    {
        #region Public Methods

        /// <summary>
        /// Encodes the URL string.
        /// </summary>
        /// <param name="str">The URL string.</param>
        /// <returns>The encoded URL string.</returns>
        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            StringReader reader = new StringReader(str);
            int charCode = reader.Read();
            while (charCode != -1)
            {
                if (((charCode >= 48) && (charCode <= 57)) || ((charCode >= 65) && (charCode <= 90)) || ((charCode >= 97) && (charCode <= 122)))
                {
                    sb.Append((char)charCode);
                }
                else if (charCode == 32)
                {
                    sb.Append("+");
                }
                else
                {
                    sb.AppendFormat("%{0:x2}", charCode);
                }
                charCode = reader.Read();
            }
            return sb.ToString();
        }

        /// <summary>
        /// Encodes the dictionary with URL parameters.
        /// </summary>
        /// <param name="data">The dictionary with parameters.</param>
        /// <returns>The encoded string.</returns>
        public static string UrlDataEncode(Dictionary<string, string> data)
        {
            StringBuilder sb = new StringBuilder();
            string format = "{0}={1}";
            bool first = true;
            foreach (KeyValuePair<string, string> pair in data)
            {
                sb.AppendFormat(format, pair.Key, pair.Value);
                if (first)
                {
                    format = "&{0}={1}";
                    first = false;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Decodes the URL string.
        /// </summary>
        /// <param name="str">The URL string.</param>
        /// <returns>The decoded URL string.</returns>
        public static string UrlDecode(string str)
        {
            return Uri.UnescapeDataString(str);
        }

        #endregion // Public Methods
    }
}
