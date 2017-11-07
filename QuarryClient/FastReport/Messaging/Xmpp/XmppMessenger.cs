using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Globalization;
using FastReport.Export;
using FastReport.Cloud;
using FastReport.Cloud.StorageClient.FastCloud;
using FastReport.Messaging.Authentication.Sasl;
using FastReport.Utils;

namespace FastReport.Messaging.Xmpp
{
    /// <summary>
    /// Represents the XMPP messenger.
    /// </summary>
    public class XmppMessenger : MessengerBase, IDisposable
    {
        #region Constants

        private const int DEFAULT_PORT_NUMBER = 5222;
        private const int DEFAULT_CHUNK_SIZE = 4096;
        private const string DEFAULT_RESOURCE = "frnet";
        private const string DEFAULT_SESSION_ID = "frnet_session_0";

        #endregion // Constants

        #region Fields

        private string username;
        private string password;
        private string hostname;
        private int port;
        private string sendToUsername;
        private TcpClient client;
        private Stream stream;
        private StreamParser parser;
        private bool connected;
        private bool authenticated;
        private bool disposed;
        private string sessionId;
        private string jidFrom;
        private string jidTo;
        private int chunkSize;

        #endregion // Fields

        #region Properties

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

        /// <summary>
        /// Gets or sets the hostname of XMPP server.
        /// </summary>
        public string Hostname
        {
            get { return hostname; }
            set { hostname = value; }
        }

        /// <summary>
        /// Gets or sets the port number of the XMPP service of the server.
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// Gets or sets the username to send file to.
        /// </summary>
        public string SendToUsername
        {
            get { return sendToUsername; }
            set { sendToUsername = value; }
        }

        /// <summary>
        /// Gets or sets the JID to send from.
        /// </summary>
        public string JidFrom
        {
            get { return jidFrom; }
            set
            {
                jidFrom = value;
                if (!String.IsNullOrEmpty(jidFrom))
                {
                    int jidFromAtPosition = jidFrom.IndexOf("@");
                    this.username = jidFrom.Substring(0, jidFromAtPosition);
                    this.hostname = jidFrom.Substring(jidFromAtPosition + 1, jidFrom.Length - jidFromAtPosition - 1);
                }
            }
        }

        /// <summary>
        /// Gets or set the JID to send to.
        /// </summary>
        public string JidTo
        {
            get { return jidTo; }
            set
            {
                jidTo = value;
                if (!String.IsNullOrEmpty(jidTo))
                {
                    int jidToAtPosition = jidTo.IndexOf("@");
                    this.sendToUsername = jidTo.Substring(0, jidToAtPosition);
                }
            }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppMessenger"/> class.
        /// </summary>
        public XmppMessenger()
        {
            username = "";
            password = "";
            hostname = "";
            port = DEFAULT_PORT_NUMBER;
            sendToUsername = "";
            client = null;
            stream = null;
            parser = null;
            connected = false;
            authenticated = false;
            disposed = false;
            sessionId = DEFAULT_SESSION_ID;
            jidFrom = "";
            jidTo = "";
            chunkSize = DEFAULT_CHUNK_SIZE;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppMessenger"/> class with specified parameters.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <param name="hostname">Hostname.</param>
        /// <param name="port">Port.</param>
        /// <param name="sendToUsername">Username to send file to.</param>
        /// <param name="sendToResource">Send to user's resource.</param>
        public XmppMessenger(string username, string password, string hostname, int port, string sendToUsername, string sendToResource)
        {
            this.username = username;
            this.password = password;
            this.hostname = hostname;
            this.port = port;
            this.sendToUsername = sendToUsername;
            client = null;
            stream = null;
            parser = null;
            connected = false;
            authenticated = false;
            disposed = false;
            sessionId = DEFAULT_SESSION_ID;
            jidFrom = username + "@" + hostname + "/" + DEFAULT_RESOURCE;
            jidTo = sendToUsername + "@" + hostname + "/" + sendToResource;
            chunkSize = DEFAULT_CHUNK_SIZE;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppMessenger"/> class with specified parameters.
        /// </summary>
        /// <param name="jidFrom">User's JID without resource.</param>
        /// <param name="password">User's password.</param>
        /// <param name="jidTo">JID to send to with resource.</param>
        public XmppMessenger(string jidFrom, string password, string jidTo)
        {
            this.port = DEFAULT_PORT_NUMBER;
            client = null;
            stream = null;
            parser = null;
            connected = false;
            authenticated = false;
            disposed = false;
            sessionId = DEFAULT_SESSION_ID;
            if (!String.IsNullOrEmpty(jidFrom))
            {
                jidFrom += "/" + DEFAULT_RESOURCE;
            }
            JidFrom = jidFrom;
            this.password = password;
            JidTo = jidTo;
            chunkSize = DEFAULT_CHUNK_SIZE;
        }

        #endregion // Constructors

        #region Private Methods

        /// <summary>
        /// Sends the specified string to the server.
        /// </summary>
        /// <param name="str">The string to send.</param>
        private void SendString(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            try
            {
                stream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                connected = false;
            }
        }

        /// <summary>
        /// Initiates the stream to the server.
        /// </summary>
        /// <param name="hostname">The hostname.</param>
        /// <returns>The features response of the server.</returns>
        private XmlElement InitiateStream(string hostname)
        {
            XmlElement xml = Xml.CreateElement("stream:stream", "jabber:client");
            Xml.AddAttribute(xml, "from", jidFrom);
            Xml.AddAttribute(xml, "to", hostname);
            Xml.AddAttribute(xml, "version", "1.0");
            Xml.AddAttribute(xml, "xml:lang", "en");
            Xml.AddAttribute(xml, "xmlns", "jabber:client");
            Xml.AddAttribute(xml, "xmlns:stream", "http://etherx.jabber.org/streams");
            string str = Xml.ToXmlString(xml, true, true);
            SendString(str);
            if (parser != null)
            {
                parser.Close();
            }
            parser = new StreamParser(stream, true);
            return parser.ReadNextElement(new List<string>(new string[] {"stream:features"}));
        }

        /// <summary>
        /// Validates the server certificate.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="certificate">X509 certificate.</param>
        /// <param name="chain">The X509 chain.</param>
        /// <param name="sslPolicyErrors">The SSL policy errors.</param>
        /// <returns>True if successfull.</returns>
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Secures the stream by TLS.
        /// </summary>
        /// <param name="hostname">The hostname.</param>
        /// <returns>The features response of the server.</returns>
        private XmlElement StartTls(string hostname)
        {
            XmlElement xml = Xml.CreateElement("starttls", "urn:ietf:params:xml:ns:xmpp-tls");
            SendString("<starttls xmlns='urn:ietf:params:xml:ns:xmpp-tls'/>");
            XmlElement proceed = parser.ReadNextElement(new List<string>(new string[] { "proceed" }));
            SslStream sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
            sslStream.AuthenticateAsClient(hostname);
            stream = sslStream;
            return InitiateStream(hostname);
        }

        /// <summary>
        /// Selects the SASL authentication mechanism.
        /// </summary>
        /// <param name="mechanisms">List of mechanisms.</param>
        /// <returns>The string containing mechanism name.</returns>
        private string SelectMechanism(List<string> mechanisms)
        {
            bool hasPlain = false;
            bool hasDigestMd5 = false;

            foreach (string mechanism in mechanisms)
            {
                if (!hasPlain)
                {
                    hasPlain = mechanism.ToUpper().Contains(PlainMechanism.MECHANISM_NAME);
                }
                if (!hasDigestMd5)
                {
                    hasDigestMd5 = mechanism.ToUpper().Contains(DigestMd5Mechanism.MECHANISM_NAME);
                }
            }

            if (hasDigestMd5)
            {
                return DigestMd5Mechanism.MECHANISM_NAME;
            }
            else if (hasPlain)
            {
                return PlainMechanism.MECHANISM_NAME;
            }
            return null;
        }

        /// <summary>
        /// Authenticates the user on the server using Plain mechanism.
        /// </summary>
        private void AuthenticateUsingPlainMechanism()
        {
            XmlElement xml = Xml.CreateElement("auth", "urn:ietf:params:xml:ns:xmpp-sasl");
            Xml.AddAttribute(xml, "mechanism", PlainMechanism.MECHANISM_NAME);
            Xml.AddText(xml, new PlainMechanism(username, password).GetResponse(""));
            SendString(Xml.ToXmlString(xml, false, true));
            XmlElement element = parser.ReadNextElement(new List<string>(new string[] { "success", "failure" }));
            if (element.Name == "success")
            {
                authenticated = true;
            }
        }

        /// <summary>
        /// Authenticates the user on the server using Digest-MD5 mechanism.
        /// </summary>
        private void AuthenticateUsingDigestMd5Mechanism()
        {
            XmlElement xml = Xml.CreateElement("auth", "urn:ietf:params:xml:ns:xmpp-sasl");
            Xml.AddAttribute(xml, "mechanism", DigestMd5Mechanism.MECHANISM_NAME);
            SendString(Xml.ToXmlString(xml, false, true));
            XmlElement element = parser.ReadNextElement(new List<string>(new string[] { "challenge", "success", "failure" }));
            if (element.Name == "challenge")
            {
                //!!
                authenticated = true;
            }
        }

        /// <summary>
        /// Authenticates the user on the server.
        /// </summary>
        /// <param name="mechanisms">The SASL mechanisms list.</param>
        private void Authenticate(List<string> mechanisms)
        {
            if (mechanisms.Count > 0)
            {
                //string mechanismName = SelectMechanism(mechanisms);
                string mechanismName = PlainMechanism.MECHANISM_NAME;
                switch (mechanismName)
                {
                    case DigestMd5Mechanism.MECHANISM_NAME:
                        AuthenticateUsingDigestMd5Mechanism();
                        break;
                    default:
                        AuthenticateUsingPlainMechanism();
                        break;
                }
            }
        }

        /// <summary>
        /// Setups the connection with the server.
        /// </summary>
        private void SetupConnection()
        {
            XmlElement features = InitiateStream(hostname);
            //if (features.GetElementsByTagName("starttls").Count > 0)
            //{
            //    features = StartTls(hostname);
            //}
            XmlNodeList mechanismsNodes = features.GetElementsByTagName("mechanism");
            List<string> mechanisms = new List<string>();
            foreach (XmlNode node in mechanismsNodes)
            {
                mechanisms.Add(node.InnerText);
            }
            Authenticate(mechanisms);
            if (!authenticated && !Config.WebMode)
            {
                FRMessageBox.Error(Res.Get("Messaging,Xmpp,Errors,InvalidJidOrPassword"));
            }
        }

        /// <summary>
        /// Binds resource and gets the full JID that will be associated with current session.
        /// </summary>
        /// <returns>The full session JID.</returns>
        private string BindResource()
        {
            string jid = null;
            XmlElement xml = Xml.CreateElement("iq", "");
            Xml.AddAttribute(xml, "type", "set");
            Xml.AddAttribute(xml, "id", "bind-0");
            XmlElement bind = Xml.CreateElement("bind", "urn:ietf:params:xml:ns:xmpp-bind");

            XmlElement resource = Xml.CreateElement("resource", "");
            Xml.AddText(resource, DEFAULT_RESOURCE);
            Xml.AddChild(bind, resource);

            Xml.AddChild(xml, bind);
            SendString(Xml.ToXmlString(xml, false, true));
            XmlElement res = parser.ReadNextElement(new List<string>(new string[] { "iq" }));
            XmlNodeList jidNode = res.GetElementsByTagName("jid");
            if (jidNode.Count > 0)
            {
                jid = jidNode[0].InnerText;
            }
            return jid;
        }

        /// <summary>
        /// Opens session between client and server.
        /// </summary>
        /// <returns>The id of the opened session.</returns>
        private string OpenSession()
        {
            SendString("<iq type='set' id='" + DEFAULT_SESSION_ID + "'><session xmlns='urn:ietf:params:xml:ns:xmpp-session'/></iq>");
            XmlElement result = parser.ReadNextElement(new List<string>(new string[] { "iq" }));
            return result.GetAttribute("id"); ;
        }

        /// <summary>
        /// Connects to the server.
        /// </summary>
        private void Connect()
        {
            client = new TcpClient(hostname, port);
            stream = client.GetStream();
            SetupConnection();
            if (authenticated)
            {
                XmlElement features = InitiateStream(hostname);
                XmlNodeList bind = features.GetElementsByTagName("bind");
                if (bind.Count > 0)
                {
                    jidFrom = BindResource();
                    sessionId = OpenSession();
                    if (!String.IsNullOrEmpty(jidFrom) && !String.IsNullOrEmpty(sessionId))
                    {
                        connected = true;
                    }
                }
            }
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="text">The text of the message.</param>
        /// <returns>True if message has been successfully sent.</returns>
        private bool SendMessage(string text)
        {
            XmlElement data = Xml.CreateElement("body", null);
            Xml.AddText(data, text);
            List<XmlElement> list = new List<XmlElement>();
            list.Add(data);
            Message message = new Message(null, "chat", jidFrom, jidTo, sessionId, new CultureInfo("en"), list);
            SendString(message.ToString());
            return true;
        }

        /// <summary>
        /// Sends the presence.
        /// </summary>
        /// <param name="text">The text of the presence.</param>
        /// <returns>True if presence has been successfully sent.</returns>
        private bool SendPresence(string text)
        {
            SendString("<presence><show></show></presence>");
            while (true)
            {
                XmlElement resp = parser.ReadNextElement(new List<string>(new string[] { "iq", "presence" }));
                if (resp == null) break;
            }
            return true;
        }

        /// <summary>
        /// Initiates the In Band Bytestream for sending the file (XEP-0047).
        /// </summary>
        /// <returns>True if bytestream has been successfully initiated.</returns>
        private bool InitiateInBandBytestream()
        {
            XmlElement open = Xml.CreateElement("open", "http://jabber.org/protocol/ibb");
            Xml.AddAttribute(open, "block-size", chunkSize.ToString());
            Xml.AddAttribute(open, "sid", sessionId);
            Xml.AddAttribute(open, "stanza", "message");
            List<XmlElement> list = new List<XmlElement>();
            list.Add(open);
            Iq iq = new Iq(null, "set", jidFrom, jidTo, "ibb1", null, list);
            //Message iq = new Message(null, "set", fullJid, jidTo + "/Miranda", sessionId, null, list);
            string str = iq.ToString();
            //string str2 = "<iq type='set' from='frbot@im.fast-report.com/frnet' to='olegk@im.fast-report.com/psee' id='frnet_session_0' stanza='message'><open sid='mySID' block-size='4096' xmlns='http://jabber.org/protocol/ibb'/></iq>";
            SendString(iq.ToString());
            XmlElement resp = parser.ReadNextElement(new List<string>(new string[] { "iq" }));
            //resp = parser.ReadNextElement(new List<string>(new string[] { "iq" }));
            //resp = parser.ReadNextElement(new List<string>(new string[] { "iq" }));
            resp = parser.ReadNextElement(new List<string>(new string[] { "iq" }));
            resp = parser.ReadNextElement(new List<string>(new string[] { "iq" }));
            while (resp == null)
            {
                resp = parser.ReadNextElement(new List<string>(new string[] { "iq" }));
                resp = parser.ReadNextElement(new List<string>(new string[] { "iq" }));
            }
            if (resp.GetElementsByTagName("service-unavailable").Count > 0 || resp.GetElementsByTagName("feature-not-implemented").Count > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Sends the chunk to the XMPP server.
        /// </summary>
        /// <param name="chunk">The data of the chunk.</param>
        /// <param name="number">The number of the chunk.</param>
        private void SendChunk(byte[] chunk, int number)
        {
            XmlElement data = Xml.CreateElement("data", "http://jabber.org/protocol/ibb");
            Xml.AddAttribute(data, "seq", number.ToString());
            Xml.AddAttribute(data, "sid", sessionId);
            Xml.AddText(data, Convert.ToBase64String(chunk));
            List<XmlElement> list = new List<XmlElement>();
            list.Add(data);
            //Iq iq = new Iq(null, "set", fullJid, jidTo, sessionId, new CultureInfo("en"), list);
            Message iq = new Message(null, null, jidFrom, jidTo, sessionId, null, list);
            SendString(iq.ToString());
            //XmlElement resp = parser.ReadNextElement(new List<string>(new string[] { "iq" }));
        }

        /// <summary>
        /// Sends the file using In Band Bytestream.
        /// </summary>
        /// <param name="ms">The memory stream containing data of the file.</param>
        /// <returns>True if file has been successfully sent.</returns>
        private bool SendFileUsingIbb(MemoryStream ms)
        {
            InitiateInBandBytestream();
            try
            {
                byte[] chunk = new byte[chunkSize];
                int chunksCount = (int)Math.Ceiling((double)ms.Length / chunkSize);
                ms.Position = 0;
                for (int i = 0; i < chunksCount - 1; i++)
                {
                    ms.Read(chunk, 0, chunk.Length);
                    SendChunk(chunk, i);
                }
                byte[] lastChunk = new byte[ms.Length - ((chunksCount - 1) * chunkSize)];
                ms.Read(lastChunk, 0, lastChunk.Length);
                SendChunk(lastChunk, chunksCount - 1);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Sends the file using FastReport Cloud as a proxy server.
        /// </summary>
        /// <param name="report">The report template.</param>
        /// <param name="export">The export filter to export report before sending.</param>
        /// <returns>True if file has been successfully sent.</returns>
        private bool SendFileUsingFastReportCloud(Report report, ExportBase export)
        {
            FastCloudStorageClient client = new FastCloudStorageClient();
            if (ProxySettings != null)
            {
                client.ProxySettings = new CloudProxySettings(FastReport.Cloud.ProxyType.Http, ProxySettings.Server, ProxySettings.Port, ProxySettings.Username, ProxySettings.Password);
                switch (ProxySettings.ProxyType)
                {
                    case ProxyType.Socks4:
                        client.ProxySettings.ProxyType = FastReport.Cloud.ProxyType.Socks4;
                        break;
                    case ProxyType.Socks5:
                        client.ProxySettings.ProxyType = FastReport.Cloud.ProxyType.Socks5;
                        break;
                    default:
                        client.ProxySettings.ProxyType = FastReport.Cloud.ProxyType.Http;
                        break;
                }
            }
            client.AccessToken = "sozTWBzEm9toiTB5J2MA";
            bool saved = false;
            try
            {
                // заменить null на export когда появится поддержка других форматов в облаке
                client.SaveReport(report, null);
                saved = true;
            }
            catch { }
            if (saved)
            {
                SendMessage(client.ReportUrl);
            }
            else
            {
                if (!Config.WebMode)
                {
                    FRMessageBox.Error(Res.Get("Messaging,Xmpp,Errors,UnableToUploadFile"));
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// Disconnects from the server.
        /// </summary>
        private void Disconnect()
        {
            if (connected)
            {
                SendString("</stream:stream>");
                connected = false;
                authenticated = false;
            }
        }

        #endregion // Private Methods

        #region Protected Methods

        /// <inheritdoc/>
        protected override bool Authorize()
        {
            Connect();
            return connected;
        }

        #endregion // Protected Methods

        #region Public Methods

        /// <inheritdoc/>
        public override bool SendReport(Report report, ExportBase export)
        {
            bool result = false;
            Authorize();
            if (connected)
            {
                //using (MemoryStream ms = PrepareToSave(report, export))
                //{
                //    result = SendFileUsingIbb(ms);
                //}
                result = SendFileUsingFastReportCloud(report, export);
                Disconnect();
            }
            return result;
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void Close()
        {
            if (connected)
            {
                Disconnect();
            }
            if (!disposed)
            {
                Dispose();
            }
        }

        /// <summary>
        /// Releases all the resources used by the XMPP messenger.
        /// </summary>
        public void Dispose()
        {
            if (!disposed)
            {
                if (parser != null)
                {
                    parser.Close();
                    parser = null;
                }
                if (client != null)
                {
                    client.Close();
                    client = null;
                }
                disposed = true;
            }
        }

        #endregion // Public Methods
    }
}
