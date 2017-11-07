using System;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.Cloud.StorageClient.Ftp
{
    /// <summary>
    /// Represents form of the FTP storage client.
    /// </summary>
    public partial class FtpStorageClientForm : CloudStorageClientForm
    {
        #region Fields

        private FtpStorageClient client;

        #endregion // Fields

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FtpStorageClientForm"/> class.
        /// </summary>
        public FtpStorageClientForm() : base()
        {
            client = new FtpStorageClient();
            InitializeComponent();
            Localize();
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FtpStorageClientForm"/> class.
        /// </summary>
        /// <param name="server">The FTP server.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="report">The report template.</param>
        public FtpStorageClientForm(string server, string username, string password, Report report) : base(report)
        {
            server = String.IsNullOrEmpty(server) ? "ftp://" : server;
            client = new FtpStorageClient(server, username, password);
            InitializeComponent();
            Localize();
            Init();
            tbFtpServer.Text = server;
            tbFtpUsername.Text = username;

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                labelFtpServer.Left = pgFile.Width - labelFtpServer.Left - labelFtpServer.Width;
                labelFtpServer.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelFtpUsername.Left = pgFile.Width - labelFtpUsername.Left - labelFtpUsername.Width;
                labelFtpUsername.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelFtpPassword.Left = pgFile.Width - labelFtpPassword.Left - labelFtpPassword.Width;
                labelFtpPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelFileType.Left = pgFile.Width - labelFileType.Left - labelFileType.Width;
                labelFileType.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
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

        #endregion // Contructors

        #region Protected Methods

        /// <inheritdoc/>
        protected override void Init()
        {
            base.Init();
            if (client == null)
            {
                client = new FtpStorageClient();
            }

            XmlItem xi = Config.Root.FindItem("FtpServer").FindItem("StorageSettings");
            string server = xi.GetProp("FtpServer");
            string username = xi.GetProp("FtpUsername");
            if (!String.IsNullOrEmpty(server) && !String.IsNullOrEmpty(username))
            {
                client.Server = server;
                client.Username = username;
            }

            tbServer.Text = xi.GetProp("ProxyServer");
            tbPort.Text = xi.GetProp("ProxyPort");
            tbUsername.Text = xi.GetProp("ProxyUsername");
            tbPassword.Text = xi.GetProp("ProxyPassword");
        }

        /// <inheritdoc/>
        protected override bool Done()
        {
            if (base.Done())
            {
                XmlItem xi = Config.Root.FindItem("FtpServer").FindItem("StorageSettings");
                xi.SetProp("FtpServer", tbFtpServer.Text);
                xi.SetProp("FtpUsername", tbFtpUsername.Text);
                xi.SetProp("ProxyServer", tbServer.Text);
                xi.SetProp("ProxyPort", tbPort.Text);
                xi.SetProp("ProxyUsername", tbUsername.Text);
                xi.SetProp("ProxyPassword", tbPassword.Text);
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

            MyRes res = new MyRes("Cloud,Ftp");
            this.Text = res.Get("");
            labelFtpServer.Text = res.Get("FtpServer");
            labelFtpUsername.Text = res.Get("FtpUsername");
            labelFtpPassword.Text = res.Get("FtpPassword");
        }

        #endregion // Public Methods

        #region Events Handlers

        /// <inheritdoc/>
        protected override void btnOk_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            client.Server = tbFtpServer.Text;
            client.Username = tbFtpUsername.Text;
            client.Password = tbFtpPassword.Text;
            client.ProxySettings = GetProxySettings();

            try
            {
                client.SaveReport(Report, Exports[cbFileType.SelectedIndex]);
                MessageBox.Show(Res.Get("Cloud,FTP,SaveSuccess"), Res.Get("Cloud,FTP,Name"));
            }
            catch (CloudStorageException ex)
            {
                MessageBox.Show(ex.Message, Res.Get("Messages,Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion // Events Handlers
    }
}
