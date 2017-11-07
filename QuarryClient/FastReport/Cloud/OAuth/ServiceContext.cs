using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Cloud.OAuth
{
    /// <summary>
    /// Represents the service provider.
    /// </summary>
    public class ServiceContext
    {
        #region Fields

        private string requestTokenUrl;
        private string userAuthorizationUrl;
        private string callbackUrl;
        private string accessTokenUrl;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets the request token URL.
        /// </summary>
        public string RequestTokenUrl
        {
            get { return requestTokenUrl; }
        }

        /// <summary>
        /// Gets the user authorization URL.
        /// </summary>
        public string UserAuthorizationUrl
        {
            get { return userAuthorizationUrl; }
        }

        /// <summary>
        /// Gets the callback URL.
        /// </summary>
        public string CallbackUrl
        {
            get { return callbackUrl; }
        }

        /// <summary>
        /// Gets the access token URL.
        /// </summary>
        public string AccessTokenUrl
        {
            get { return accessTokenUrl; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContext"/> class with a specified parameters.
        /// </summary>
        /// <param name="requestTokenUrl">The request token URL.</param>
        /// <param name="userAuthorizationUrl">The user authorization URL.</param>
        /// <param name="callbackUrl">The callback URL.</param>
        /// <param name="accessTokenUrl">The access token URL.</param>
        public ServiceContext(string requestTokenUrl, string userAuthorizationUrl, string callbackUrl, string accessTokenUrl)
        {
            this.requestTokenUrl = requestTokenUrl;
            this.userAuthorizationUrl = userAuthorizationUrl;
            this.callbackUrl = callbackUrl;
            this.accessTokenUrl = accessTokenUrl;
        }

        #endregion // Constructors
    }
}
