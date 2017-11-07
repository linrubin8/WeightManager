using System;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Cloud.StorageClient.SkyDrive;

namespace FastReport.Cloud.StorageClient.Box
{
    /// <summary>
    /// Represents form of Box storage client.
    /// </summary>
    public partial class BoxStorageClientForm : CloudStorageClientForm
    {
        #region Fields

        private BoxStorageClient client;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxStorageClientForm"/> class.
        /// </summary>
        public BoxStorageClientForm() : base()
        {
            client = new BoxStorageClient();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxStorageClientForm"/> class.
        /// </summary>
        /// <param name="clientInfo">The information about Box client application.</param>
        /// <param name="report">The report template.</param>
        public BoxStorageClientForm(ClientInfo clientInfo, Report report) : base(report)
        {
            client = new BoxStorageClient(clientInfo);
            Init();

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                labelFileType.Left = pgFile.Width - labelFileType.Left - labelFileType.Width;
                labelFileType.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbFileType.Left = pgFile.Width - cbFileType.Left - cbFileType.Width;
                cbFileType.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                buttonSettings.Left = pgFile.Width - buttonSettings.Left - buttonSettings.Width;
                buttonSettings.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelServer.Left = pgProxy.Width - labelServer.Left - labelServer.Width;
                labelServer.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbServer.Left = pgProxy.Width - tbServer.Left - tbServer.Width;
                tbServer.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelColon.Left = pgProxy.Width - labelColon.Left - labelColon.Width;
                labelColon.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbPort.Left = pgProxy.Width - tbPort.Left - tbPort.Width;
                tbPort.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelUsername.Left = pgProxy.Width - labelUsername.Left - labelUsername.Width;
                labelUsername.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbUsername.Left = pgProxy.Width - tbUsername.Left - tbUsername.Width;
                tbUsername.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelPassword.Left = pgProxy.Width - labelPassword.Left - labelPassword.Width;
                labelPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbPassword.Left = pgProxy.Width - tbPassword.Left - tbPassword.Width;
                tbPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

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
            if (client == null)
            {
                client = new BoxStorageClient();
            }

            XmlItem xi = Config.Root.FindItem("BoxCloud").FindItem("StorageSettings");
            string id = xi.GetProp("ClientId");
            string secret = xi.GetProp("ClientSecret");
            if (!String.IsNullOrEmpty(id) && !String.IsNullOrEmpty(secret))
            {
                client.ClientInfo.Id = id;
                client.ClientInfo.Secret = secret;
            }
            client.AuthCode = xi.GetProp("AuthCode");
            string str = xi.GetProp("IsUserAuthorized");
            if (String.IsNullOrEmpty(str))
            {
                client.IsUserAuthorized = false;
            }
            else
            {
                client.IsUserAuthorized = Convert.ToBoolean(str);
            }
            string accessToken = xi.GetProp("AccessToken");
            if (!String.IsNullOrEmpty(accessToken))
            {
                client.AccessToken = accessToken;
            }

            tbServer.Text = xi.GetProp("Server");
            tbPort.Text = xi.GetProp("Port");
            tbUsername.Text = xi.GetProp("Username");
            tbPassword.Text = xi.GetProp("Password");
        }

        /// <inheritdoc/>
        protected override bool Done()
        {
            if (base.Done())
            {
                XmlItem xi = Config.Root.FindItem("BoxCloud").FindItem("StorageSettings");
                xi.SetProp("ClientId", client.ClientInfo.Id);
                xi.SetProp("ClientSecret", client.ClientInfo.Secret);
                xi.SetProp("AuthCode", client.AuthCode);
                xi.SetProp("IsUserAuthorized", client.IsUserAuthorized.ToString());
                xi.SetProp("AccessToken", client.AccessToken);
                xi.SetProp("Server", tbServer.Text);
                xi.SetProp("Port", tbPort.Text);
                xi.SetProp("Username", tbUsername.Text);
                xi.SetProp("Password", tbPassword.Text);
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

            MyRes res = new MyRes("Cloud,Box");
            this.Text = res.Get("");
        }

        #endregion // Public Methods

        #region Events Handlers

        /// <inheritdoc/>
        protected override void btnOk_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (!client.IsUserAuthorized || String.IsNullOrEmpty(client.AccessToken))
            {
                string authorizationUrl = client.GetAuthorizationUrl();
                WebBrowserForm browser = new WebBrowserForm(authorizationUrl);
                browser.ShowDialog();
                client.AuthCode = browser.AuthCode;
                client.IsUserAuthorized = true;
                client.GetAccessToken();
            }
            client.ProxySettings = GetProxySettings();
            bool saved = false;
            try
            {
                client.SaveReport(Report, Exports[cbFileType.SelectedIndex]);
                saved = true;
            }
            catch { }
            if (!saved)
            {
                string authorizationUrl = client.GetAuthorizationUrl();
                WebBrowserForm browser = new WebBrowserForm(authorizationUrl);
                browser.ShowDialog();
                client.AuthCode = browser.AuthCode;
                client.IsUserAuthorized = true;
                client.GetAccessToken();
                client.ProxySettings = GetProxySettings();
                try
                {
                    client.SaveReport(Report, Exports[cbFileType.SelectedIndex]);
                }
                catch (CloudStorageException ex)
                {
                    MessageBox.Show(ex.Message, Res.Get("Messages,Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion // Events Handlers
    }
}

