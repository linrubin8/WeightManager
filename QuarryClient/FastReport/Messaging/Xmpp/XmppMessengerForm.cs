using System;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.Messaging.Xmpp
{
    /// <summary>
    /// Represents the form of the XMPP messenger.
    /// </summary>
    public partial class XmppMessengerForm : MessengerForm
    {
        #region Fields
        
        private XmppMessenger messenger;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppMessengerForm"/> class.
        /// </summary>
        public XmppMessengerForm() : base()
        {
            messenger = new XmppMessenger();
            InitializeComponent();
            Localize();
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmppMessengerForm"/> class with specified parameters.
        /// </summary>
        /// <param name="jidFrom">User's JID without resource.</param>
        /// <param name="password">User's password.</param>
        /// <param name="jidTo">Send to user's JID.</param>
        /// <param name="report">Report template.</param>
        public XmppMessengerForm(string jidFrom, string password, string jidTo, Report report) : base(report)
        {
            messenger = new XmppMessenger(jidFrom, password, jidTo);
            InitializeComponent();
            Localize();
            Init();
            tbJidFrom.Text = jidFrom;
            tbPassword.Text = password;
            tbJidTo.Text = jidTo;

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                labelJidFrom.Left = pgFile.Width - labelJidFrom.Left - labelJidFrom.Width;
                labelJidFrom.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelPassword.Left = pgFile.Width - labelPassword.Left - labelPassword.Width;
                labelPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelJidTo.Left = pgFile.Width - labelJidTo.Left - labelJidTo.Width;
                labelJidTo.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelFileType.Left = pgFile.Width - labelFileType.Left - labelFileType.Width;
                labelFileType.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbFileType.Left = pgFile.Width - cbFileType.Left - cbFileType.Width;
                cbFileType.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                buttonSettings.Left = pgFile.Width - buttonSettings.Left - buttonSettings.Width;
                buttonSettings.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelProxyServer.Left = pgProxy.Width - labelProxyServer.Left - labelProxyServer.Width;
                labelProxyServer.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbProxyServer.Left = pgProxy.Width - tbProxyServer.Left - tbProxyServer.Width;
                tbProxyServer.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelProxyColon.Left = pgProxy.Width - labelProxyColon.Left - labelProxyColon.Width;
                labelProxyColon.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbProxyPort.Left = pgProxy.Width - tbProxyPort.Left - tbProxyPort.Width;
                tbProxyPort.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelProxyUsername.Left = pgProxy.Width - labelProxyUsername.Left - labelProxyUsername.Width;
                labelProxyUsername.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbProxyUsername.Left = pgProxy.Width - tbProxyUsername.Left - tbProxyUsername.Width;
                tbProxyUsername.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelProxyPassword.Left = pgProxy.Width - labelProxyPassword.Left - labelProxyPassword.Width;
                labelProxyPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbProxyPassword.Left = pgProxy.Width - tbProxyPassword.Left - tbProxyPassword.Width;
                tbProxyPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from rigth to left
                btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
                btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
            }
        }

        #endregion // Constructors

        #region Protected Methods

        /// <inheritdoc/>
        protected override void Init()
        {
            base.Init();

            if (messenger == null)
            {
                messenger = new XmppMessenger();
            }

            XmlItem xi = Config.Root.FindItem("XmppMessenger").FindItem("MessengerSettings");
            string jidFrom = xi.GetProp("JidFrom");
            string password = xi.GetProp("Password");
            string jidTo = xi.GetProp("JidTo");
            if (!String.IsNullOrEmpty(jidFrom) && !String.IsNullOrEmpty(password) && !String.IsNullOrEmpty(jidTo))
            {
                messenger.JidFrom = jidFrom;
                messenger.Password = password;
                messenger.JidTo = jidTo;
            }

            tbProxyServer.Text = xi.GetProp("ProxyServer");
            tbProxyPort.Text = xi.GetProp("ProxyPort");
            tbProxyUsername.Text = xi.GetProp("ProxyUsername");
            tbProxyPassword.Text = xi.GetProp("ProxyPassword");
        }

        /// <inheritdoc/>
        protected override bool Done()
        {
            if (base.Done())
            {
                XmlItem xi = Config.Root.FindItem("XmppMessenger").FindItem("MessengerSettings");
                xi.SetProp("JidFrom", tbJidFrom.Text);
                xi.SetProp("Password", tbPassword.Text);
                xi.SetProp("JidTo", tbJidTo.Text);
                xi.SetProp("ProxyServer", tbProxyServer.Text);
                xi.SetProp("ProxyPort", tbProxyPort.Text);
                xi.SetProp("ProxyUsername", tbProxyUsername.Text);
                xi.SetProp("ProxyPassword", tbProxyPassword.Text);
                return true;
            }
            return false;
        }

        #endregion // Protected Methods

        #region Public Methods

        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            InitializeComponent();

            MyRes res = new MyRes("Messaging,Xmpp");
            this.Text = res.Get("");
            labelJidFrom.Text = res.Get("JidFrom");
            labelPassword.Text = res.Get("Password");
            labelJidTo.Text = res.Get("JidTo");
            labelCloudWarning.Text = res.Get("CloudWarning");
        }

        #endregion // Public Methods

        #region Events Handlers

        /// <inheritdoc/>
        protected override void btnOk_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DialogResult = DialogResult.OK;
            messenger.JidFrom = tbJidFrom.Text;
            messenger.Password = tbPassword.Text;
            messenger.JidTo = tbJidTo.Text;
            messenger.ProxySettings = GetProxySettings();
            //messenger.SendReport(Report, Exports[cbFileType.SelectedIndex]);
            messenger.SendReport(Report, null);
            Close();
        }

        #endregion // Events Handlers
    }
}

