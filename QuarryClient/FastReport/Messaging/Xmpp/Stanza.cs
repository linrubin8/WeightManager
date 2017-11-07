using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Xml;

namespace FastReport.Messaging.Xmpp
{
    /// <summary>
    /// Represents the base class for XML stanzas used in XMPP.
    /// </summary>
    public abstract class Stanza
    {
        #region Fields

        private XmlElement data;
        private string jidFrom;
        private string jidTo;
        private string id;
        private CultureInfo language;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets the data of the stanza.
        /// </summary>
        public XmlElement Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// Gets or sets the JID of the sender.
        /// </summary>
        public string JidFrom
        {
            get { return jidFrom; }
            set
            {
                jidFrom = value;
                if (value == null)
                {
                    data.RemoveAttribute("from");
                }
                else
                {
                    data.SetAttribute("from", value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the JID of the recipient.
        /// </summary>
        public string JidTo
        {
            get { return jidTo; }
            set
            {
                jidTo = value;
                if (value == null)
                {
                    data.RemoveAttribute("to");
                }
                else
                {
                    data.SetAttribute("to", value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the ID of the stanza.
        /// </summary>
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                if (value == null)
                {
                    data.RemoveAttribute("id");
                }
                else
                {
                    data.SetAttribute("id", value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the language of the stanza.
        /// </summary>
        public CultureInfo Language
        {
            get { return language; }
            set
            {
                language = value;
                if (value == null)
                {
                    data.RemoveAttribute("xml:lang");
                }
                else
                {
                    data.SetAttribute("xml:lang", value.Name);
                }
            }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Stanza"/> class with specified parameters.
        /// </summary>
        /// <param name="nspace">The namespace of the stanza.</param>
        /// <param name="jidFrom">The JID of the sender.</param>
        /// <param name="jidTo">The JID of the recipient.</param>
        /// <param name="id">The ID of the stanza.</param>
        /// <param name="language">The language of the stanza.</param>
        /// <param name="data">The data of the stanza.</param>
        public Stanza(string nspace, string jidFrom, string jidTo, string id, CultureInfo language, List<XmlElement> data)
        {
            string name = GetType().Name.ToLowerInvariant();
            Data = Xml.CreateElement(name, nspace);
            JidFrom = jidFrom;
            JidTo = jidTo;
            Id = id;
            Language = language;
            foreach (XmlElement e in data)
            {
                if (e != null)
                {
                    Xml.AddChild(Data, e);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Stanza"/> class using specified XmlElement instance.
        /// </summary>
        /// <param name="data">The XmlElement instance using like a data.</param>
        public Stanza(XmlElement data)
        {
            this.data = data;
        }

        #endregion // Constructors

        #region Public Methods

        /// <summary>
        /// Converts stanza to string.
        /// </summary>
        /// <returns>String containing stanza value.</returns>
        public override string ToString()
        {
            return Xml.ToXmlString(data, false, true);
        }

        #endregion // Public Methods
    }
}
