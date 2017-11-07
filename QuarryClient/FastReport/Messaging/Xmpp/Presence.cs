using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Xml;

namespace FastReport.Messaging.Xmpp
{
    /// <summary>
    /// Represents the XMPP Presence.
    /// </summary>
    public class Presence : Stanza
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Presence"/> class with specified parameters.
        /// </summary>
        /// <param name="nspace">The namespace of the presence.</param>
        /// <param name="jidFrom">The JID of the sender.</param>
        /// <param name="jidTo">The JID of the recipient.</param>
        /// <param name="id">The ID of the presence.</param>
        /// <param name="language">The language of the presence.</param>
        /// <param name="data">The data of the presence.</param>
        public Presence(string nspace, string jidFrom, string jidTo, string id, CultureInfo language, List<XmlElement> data)
            : base (nspace, jidFrom, jidTo, id, language, data)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Presence"/> class using specified XmlElement instance.
        /// </summary>
        /// <param name="data">The XmlElement instance using like a data.</param>
        public Presence(XmlElement data) : base (data)
        {
        }

        #endregion // Constructors
    }
}
