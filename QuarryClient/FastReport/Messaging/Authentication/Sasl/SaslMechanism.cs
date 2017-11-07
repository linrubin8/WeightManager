using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Messaging.Authentication.Sasl
{
    /// <summary>
    /// The base abstarct class for all SASL mechanisms.
    /// </summary>
    public abstract class SaslMechanism
    {
        #region Fields

        private string name;
        private string username;
        private string password;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the name of mechanism.
        /// </summary>
        public string Name
        {
            get { return name; }
            protected set { name = value; }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaslMechanism"/> class.
        /// </summary>
        public SaslMechanism()
        {
            name = "";
            username = "";
            password = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaslMechanism"/> class with specified parameters.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The user's password.</param>
        public SaslMechanism(string username, string password)
        {
            this.name = "";
            this.username = username;
            this.password = password;
        }

        #endregion // Constructors

        #region Protected Methods

        /// <summary>
        /// Computes client response for server challenge.
        /// </summary>
        /// <param name="challenge">The server challenge.</param>
        /// <returns>The client response.</returns>
        protected abstract byte[] ComputeResponse(byte[] challenge);

        #endregion // Protected Methods

        #region Public Methods

        /// <summary>
        /// Gets the base64-encoded client response fo the server challenge.
        /// </summary>
        /// <param name="challenge">The base64-string containing server challenge.</param>
        /// <returns>The base64-string containing client response.</returns>
        public string GetResponse(string challenge)
        {
            byte[] data = Convert.FromBase64String(challenge);
            byte[] response = ComputeResponse(data);
            return Convert.ToBase64String(response);
        }

        /// <summary>
        /// Gets the client response for the server challenge.
        /// </summary>
        /// <param name="challenge">Byte array containing server challenge.</param>
        /// <returns>Byte array containing client response.</returns>
        public byte[] GetResponse(byte[] challenge)
        {
            return ComputeResponse(challenge);
        }

        #endregion // Public Methods
    }
}
