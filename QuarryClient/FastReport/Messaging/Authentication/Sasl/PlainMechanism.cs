using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Messaging.Authentication.Sasl
{
    /// <summary>
    /// The PLAIN SASL authentication mechanism.
    /// </summary>
    public class PlainMechanism : SaslMechanism
    {
        #region Constants

        /// <summary>
        /// The mechanism name.
        /// </summary>
        public const string MECHANISM_NAME = "PLAIN";

        #endregion // Constants

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlainMechanism"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The user's password.</param>
        public PlainMechanism(string username, string password)
        {
            Name = MECHANISM_NAME;
            Username = username;
            Password = password;
        }

        #endregion // Constructors

        #region Protected Methods

        /// <summary>
        /// Computes the client response for server challenge.
        /// </summary>
        /// <param name="challenge">The challenge from server. Usually empty for PLAIN mechanism.</param>
        /// <returns>The response from client.</returns>
        protected override byte[] ComputeResponse(byte[] challenge)
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            {
                return null;
            }
            return Encoding.UTF8.GetBytes("\0" + Username + "\0" + Password);
        }

        #endregion // Protected Methods
    }
}
