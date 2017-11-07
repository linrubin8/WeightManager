using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Security;

namespace FastReport.Messaging.Xmpp
{
    /// <summary>
    /// Represents a static class to simplify the work with XmlElement instance.
    /// </summary>
    internal static class Xml
    {
        #region Public Methods

        /// <summary>
        /// Creates a new XmlElement instance.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="nspace">The namespace of the element.</param>
        /// <returns>A new instance of the <see cref="XmlElement"/> class.</returns>
        public static XmlElement CreateElement(string name, string nspace)
        {
            return new XmlDocument().CreateElement(name, nspace);
        }

        /// <summary>
        /// Adds the specified child to the end of child nodes of element.
        /// </summary>
        /// <param name="element">The element for add the child to.</param>
        /// <param name="child">The child node to add.</param>
        /// <returns>A XmlElement instance.</returns>
        public static XmlElement AddChild(XmlElement element, XmlElement child)
        {
            XmlNode node = element.OwnerDocument.ImportNode(child, true);
            element.AppendChild(node);
            return element;
        }

        /// <summary>
        /// Adds the attribute to XmlElement with spefied name and value.
        /// </summary>
        /// <param name="element">The element for add the attribute to.</param>
        /// <param name="name">The name of attribute.</param>
        /// <param name="value">The value of attribute.</param>
        /// <returns>A XmlElement instance.</returns>
        public static XmlElement AddAttribute(XmlElement element, string name, string value)
        {
            element.SetAttribute(name, value);
            return element;
        }

        /// <summary>
        /// Adds the specified text to the end of child nodes of element.
        /// </summary>
        /// <param name="element">The element for add the text to.</param>
        /// <param name="text">The text for add.</param>
        /// <returns>A XmlElement instance.</returns>
        public static XmlElement AddText(XmlElement element, string text)
        {
            element.AppendChild(element.OwnerDocument.CreateTextNode(text));
            return element;
        }

        /// <summary>
        /// Converts the XmlElement instance to a string.
        /// </summary>
        /// <param name="element">The element to convert to.</param>
        /// <param name="includeDeclaration">True if needed to include XML declaration.</param>
        /// <param name="leaveOpen">True if needed to leave the tag of an empty element open.</param>
        /// <returns>The XmlElement instance as string.</returns>
        public static string ToXmlString(XmlElement element, bool includeDeclaration, bool leaveOpen)
        {
            StringBuilder sb = new StringBuilder("<" + element.Name);
            if (!String.IsNullOrEmpty(element.NamespaceURI))
            {
                sb.Append(" xmlns='" + element.NamespaceURI + "'");
            }
            foreach (XmlAttribute attribute in element.Attributes)
            {
                if (attribute.Name != "xmlns" && attribute.Value != null)
                {
                    sb.Append(" " + attribute.Name + "='" + SecurityElement.Escape(attribute.Value.ToString()) + "'");
                }
            }
            if (element.IsEmpty)
            {
                sb.Append("/>");
            }
            else
            {
                sb.Append(">");
                foreach (XmlNode child in element.ChildNodes)
                {
                    if (child is XmlElement)
                    {
                        sb.Append(Xml.ToXmlString(child as XmlElement, false, false));
                    }
                    else if (child is XmlText)
                    {
                        sb.Append((child as XmlText).InnerText);
                    }
                }
                sb.Append("</" + element.Name + ">");
            }
            string xml = "";
            if (includeDeclaration)
            {
                //xml = "<?xml version='1.0' encoding='UTF-8'?>";
                xml = "<?xml version='1.0'?>";
            }
            xml += sb.ToString();
            if (leaveOpen)
            {
                xml = Regex.Replace(xml, "/>$", ">");
            }
            return xml;
        }

        #endregion // Public Methods
    }
}
