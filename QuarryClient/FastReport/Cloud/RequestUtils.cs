using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace FastReport.Cloud
{
    /// <summary>
    /// Provides utils for the web request.
    /// </summary>
    public static class RequestUtils
    {
        #region Public Methods

        /// <summary>
        /// Sets proxy settings for web request.
        /// </summary>
        /// <param name="request">The web request.</param>
        /// <param name="settings">The cloud proxy settings.</param>
        public static void SetProxySettings(WebRequest request, CloudProxySettings settings)
        {
            if (settings != null)
            {
                StringBuilder proxyAddress = new StringBuilder(settings.Server);
                if (settings.Port != 0)
                {
                    proxyAddress.AppendFormat(":{0}", settings.Port.ToString());
                }
                request.Proxy = new WebProxy(proxyAddress.ToString());
                if (!String.IsNullOrEmpty(settings.Username))
                {
                    request.Proxy.Credentials = new NetworkCredential(settings.Username, settings.Password);
                }
            }
        }

        #endregion // Public Methods
    }
}
