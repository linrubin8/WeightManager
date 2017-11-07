using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Cloud.StorageClient;

namespace FastReport.Cloud.StorageClient.FastCloud
{
    /// <summary>
    /// Represents form of FastCloud storage client.
    /// </summary>
    public partial class FastCloudStorageClientForm : CloudStorageClientForm
    {
        #region Fields

        private FastCloudStorageClient client;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FastCloudStorageClientForm"/> class.
        /// </summary>
        public FastCloudStorageClientForm() : base()
        {
            client = new FastCloudStorageClient();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FastCloudStorageClientForm"/> class.
        /// </summary>
        /// <param name="report">The report template.</param>
        public FastCloudStorageClientForm(Report report) : base(report)
        {
            client = new FastCloudStorageClient();
            Init();
        }

        #endregion // Constructors

        #region Protected Methods

        /// <inheritdoc/>
        protected override void Init()
        {
            base.Init();
            if (client == null)
            {
                client = new FastCloudStorageClient();
            }

            XmlItem xi = Config.Root.FindItem("FastCloud").FindItem("StorageSettings");

            client.IsUserAuthorized = true;

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
                XmlItem xi = Config.Root.FindItem("FastCloud").FindItem("StorageSettings");
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

            MyRes res = new MyRes("Cloud,FastCloud");
            this.Text = res.Get("");
        }

        #endregion // Public Methods

        #region Events Handlers

        /// <inheritdoc/>
        protected override void btnOk_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            client.ProxySettings = GetProxySettings();
            client.GetAccessToken(@"test@fast-report.com", "testtest");
            try
            {
                client.SaveReport(Report, Exports[cbFileType.SelectedIndex]);
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

