using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;

namespace FastReport.Messaging.Xmpp
{
    /// <summary>
    /// The parser for XMPP XML-streams.
    /// </summary>
    internal class StreamParser : IDisposable
    {
        #region Fields

        private XmlReader reader;
        private Stream stream;
        private bool leaveOpen;
        private CultureInfo language;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamParser"/> class.
        /// </summary>
        /// <param name="stream">The stream for parsing.</param>
        /// <param name="leaveOpen">True to leave the stream opened after closing the StreamReader instance.</param>
        public StreamParser(Stream stream, bool leaveOpen)
        {
            this.stream = stream;
            this.leaveOpen = leaveOpen;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            settings.IgnoreProcessingInstructions = true;
            reader = XmlReader.Create(stream, settings);
            ReadRootElement();
        }

        #endregion // Constructors

        #region Private Methods

        /// <summary>
        /// Read the XML stream up to opening "stream:stream" tag.
        /// </summary>
        private void ReadRootElement()
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.XmlDeclaration:
                        break;
                    case XmlNodeType.Element:
                        if (reader.Name == "stream:stream")
                        {
                            string lang = reader.GetAttribute("xml:lang");
                            if (!String.IsNullOrEmpty(lang))
                            {
                                language = new CultureInfo(lang);
                                return;
                            }
                        }
                        break;
                }
            }
        }

        #endregion // Private Methods

        #region Public Methods

        /// <summary>
        /// Reads the next XML element from the stream.
        /// </summary>
        /// <param name="expected">The list of XML elements that are expected.</param>
        /// <returns>The XML element.</returns>
        public XmlElement ReadNextElement(List<string> expected)
        {
            reader.Read();
            using (XmlReader innerReader = reader.ReadSubtree())
            {
                innerReader.Read();
                string xml = innerReader.ReadOuterXml();
                XmlDocument doc = new XmlDocument();
                using (XmlTextReader xtr = new XmlTextReader(new StringReader(xml)))
                {
                    doc.Load(xtr);
                }
                XmlElement element = doc.FirstChild as XmlElement;
                if (expected.Count > 0 && !expected.Contains(element.Name))
                {
                    element = null;
                }
                return element;
            }
        }

        /// <summary>
        /// Closes the stream parser.
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Releases all resources used by the current instance of the <see cref="StreamParser"/> class.
        /// </summary>
        public void Dispose()
        {
            reader.Close();
            if (!leaveOpen)
            {
                stream.Close();
            }
        }

        #endregion // Public Methods
    }
}
