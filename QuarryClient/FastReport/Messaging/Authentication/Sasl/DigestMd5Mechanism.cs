using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Messaging.Authentication.Sasl
{
    /// <summary>
    /// The DIGEST-MD5 SASL authentication mechanism.
    /// </summary>
    public class DigestMd5Mechanism : SaslMechanism
    {
        #region Constants

        /// <summary>
        /// The mechanism name.
        /// </summary>
        public const string MECHANISM_NAME = "DIGEST-MD5";

        #endregion // Constants

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DigestMd5Mechanism"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The user's password.</param>
        public DigestMd5Mechanism(string username, string password)
        {
            Name = MECHANISM_NAME;
            Username = username;
            Password = password;
        }

        #endregion // Constructors

        #region Protected Methods

        /// <inheritdoc/>
        protected override byte[] ComputeResponse(byte[] challenge)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion // Protected Methods
    }
}
