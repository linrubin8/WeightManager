using System;
using System.Windows.Forms;
using FastReport.Forms;
using FastReport.Utils;

namespace FastReport.Cloud.StorageClient.FastCloud
{
    /// <summary>
    /// Represents form of FastCloud storage client.
    /// </summary>
    public partial class FastCloudStorageClientSimpleForm : BaseDialogForm
    {
        #region Fields

        private Report report;
        private FastCloudStorageClient client;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FastCloudStorageClientSimpleForm"/> class.
        /// </summary>
        public FastCloudStorageClientSimpleForm()
        {
            this.report = new Report();
            InitializeComponent();
            Localize();
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FastCloudStorageClientSimpleForm"/> class.
        /// </summary>
        /// <param name="report">The report template.</param>
        public FastCloudStorageClientSimpleForm(Report report)
        {
            this.report = report;
            InitializeComponent();
            Localize();
            Init();

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                labelEmail.Left = gbUserDetails.Width - labelEmail.Left - labelEmail.Width;
                labelEmail.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbEmail.Left = gbUserDetails.Width - tbEmail.Left - tbEmail.Width;
                tbEmail.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelUserPassword.Left = gbUserDetails.Width - labelUserPassword.Left - labelUserPassword.Width;
                labelUserPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbUserPassword.Left = gbUserDetails.Width - tbUserPassword.Left - tbUserPassword.Width;
                tbUserPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelServer.Left = gbProxySettings.Width - labelServer.Left - labelServer.Width;
                labelServer.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbServer.Left = gbProxySettings.Width - tbServer.Left - tbServer.Width;
                tbServer.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelColon.Left = gbProxySettings.Width - labelColon.Left - labelColon.Width;
                labelColon.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbPort.Left = gbProxySettings.Width - tbPort.Left - tbPort.Width;
                tbPort.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelUsername.Left = gbProxySettings.Width - labelUsername.Left - labelUsername.Width;
                labelUsername.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbUsername.Left = gbProxySettings.Width - tbUsername.Left - tbUsername.Width;
                tbUsername.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelPassword.Left = gbProxySettings.Width - labelPassword.Left - labelPassword.Width;
                labelPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbPassword.Left = gbProxySettings.Width - tbPassword.Left - tbPassword.Width;
                tbPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from rigth to left
                btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
                btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
            }
        }

        #endregion // Constructors

        #region Private Methods

        /// <summary>
        /// Checks is the string numeric.
        /// </summary>
        /// <param name="str">The checking string.</param>
        /// <returns>True if string is numeric, otherwise false.</returns>
        private bool IsNumeric(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                try
                {
                    Convert.ToInt32(str);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the proxy settings.
        /// </summary>
        /// <returns>The proxy settings.</returns>
        private CloudProxySettings GetProxySettings()
        {
            CloudProxySettings proxySettings = null;
            if (!String.IsNullOrEmpty(tbServer.Text))
            {
                int port = 0;
                if (!IsNumeric(tbPort.Text))
                {
                    FRMessageBox.Error(Res.Get("Cloud,CloudStorage,PortError"));
                }
                else
                {
                    port = Convert.ToInt32(tbPort.Text);
                }
                proxySettings = new CloudProxySettings(ProxyType.Http, tbServer.Text, port, tbUsername.Text, tbPassword.Text);
            }
            return proxySettings;
        }

        private void Init()
        {
            if (client == null)
            {
                client = new FastCloudStorageClient();
            }

            XmlItem xi = Config.Root.FindItem("FastCloud").FindItem("StorageSettings");

            tbEmail.Text = xi.GetProp("Email");
            tbUserPassword.Text = xi.GetProp("UserPassword");
            tbServer.Text = xi.GetProp("Server");
            tbPort.Text = xi.GetProp("Port");
            tbUsername.Text = xi.GetProp("Username");
            tbPassword.Text = xi.GetProp("Password");
            if (!String.IsNullOrEmpty(client.AccessToken))
            {
                client.IsUserAuthorized = true;
            }
        }

        private bool Done()
        {
            XmlItem xi = Config.Root.FindItem("FastCloud").FindItem("StorageSettings");
            xi.SetProp("Email", tbEmail.Text);
            xi.SetProp("UserPassword", tbUserPassword.Text);
            xi.SetProp("Server", tbServer.Text);
            xi.SetProp("Port", tbPort.Text);
            xi.SetProp("Username", tbUsername.Text);
            xi.SetProp("Password", tbPassword.Text);
            return true;
        }

        #endregion //// Private Methods

        #region Public Methods

        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();

            MyRes res = new MyRes("Cloud,FastCloud");
            this.Text = res.Get("");
            this.gbUserDetails.Text = res.Get("UserDetails");
            this.labelEmail.Text = res.Get("Email");
            this.labelUserPassword.Text = res.Get("UserPassword");
        }

        #endregion // Public Methods

        #region Events Handlers

        private void btnOk_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            client.ProxySettings = GetProxySettings();
            client.GetAccessToken(tbEmail.Text, tbUserPassword.Text);
            try
            {
                client.SaveReport(report, null);
            }
            catch (CloudStorageException ex)
            {
                MessageBox.Show(ex.Message, Res.Get("Messages,Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DialogResult = DialogResult.OK;
            Done();
            Close();
        }

        #endregion // Events Handlers
    }
}

