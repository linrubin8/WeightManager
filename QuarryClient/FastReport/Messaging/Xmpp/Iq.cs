using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Globalization;

namespace FastReport.Messaging.Xmpp
{
    /// <summary>
    /// Represents the IQ stanza.
    /// </summary>
    public class Iq : Stanza
    {
        #region Fields

        private string type;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the type of iq.
        /// </summary>
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                if (value == null)
                {
                    Data.RemoveAttribute("type");
                }
                else
                {
                    Data.SetAttribute("type", value);
                }
            }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Iq"/> class with specified parameters.
        /// </summary>
        /// <param name="nspace">The namespace of the iq.</param>
        /// <param name="type">The type of iq.</param>
        /// <param name="jidFrom">The JID of the sender.</param>
        /// <param name="jidTo">The JID of the recipient.</param>
        /// <param name="id">The ID of the iq.</param>
        /// <param name="language">The language of the iq.</param>
        /// <param name="data">The data of the iq.</param>
        public Iq(string nspace, string type, string jidFrom, string jidTo, string id, CultureInfo language, List<XmlElement> data)
            : base(nspace, jidFrom, jidTo, id, language, data)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Iq"/> class using specified XmlElement instance.
        /// </summary>
        /// <param name="data">The XmlElement instance using like a data.</param>
        public Iq(XmlElement data) : base(data)
        {
        }

        #endregion // Constructors
    }
}
