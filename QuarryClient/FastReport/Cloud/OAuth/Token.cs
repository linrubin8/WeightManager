using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Cloud.OAuth
{
    /// <summary>
    /// Represents the OAuth token credentials.
    /// </summary>
    public class Token
    {
        #region Fields

        private string tokenKey;
        private string tokenSecret;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets the token key.
        /// </summary>
        public string TokenKey
        {
            get { return tokenKey; }
            set { tokenKey = value; }
        }

        /// <summary>
        /// Gets the token secret.
        /// </summary>
        public string TokenSecret
        {
            get { return tokenSecret; }
            set { tokenSecret = value; }
        }

        #endregion //Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="tokenKey">The token key.</param>
        /// <param name="tokenSecret">The token secret.</param>
        public Token(string tokenKey, string tokenSecret)
        {
            this.tokenKey = tokenKey;
            this.tokenSecret = tokenSecret;
        }

        #endregion // Constructors
    }
}
