using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Cloud.StorageClient.SkyDrive
{
    /// <summary>
    /// Represents the information about SkyDrive application.
    /// </summary>
    public class ClientInfo
    {
        #region Fields

        private string name;
        private string id;
        private string secret;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        public string Secret
        {
            get { return secret; }
            set { secret = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientInfo"/> class.
        /// </summary>
        /// <param name="name">The client name.</param>
        /// <param name="id">The client ID.</param>
        /// <param name="secret">The client secret.</param>
        public ClientInfo(string name, string id, string secret)
        {
            this.name = name;
            this.id = id;
            this.secret = secret;
        }

        #endregion // Constructors
    }
}
