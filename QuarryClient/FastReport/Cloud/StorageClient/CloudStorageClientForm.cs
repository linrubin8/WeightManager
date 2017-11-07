using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport.Forms;
using FastReport.Export;
using FastReport.Utils;
using FastReport.Cloud.OAuth;

namespace FastReport.Cloud.StorageClient
{
    /// <summary>
    /// Represents form of Dropbox storage client.
    /// </summary>
    public partial class CloudStorageClientForm : BaseDialogForm
    {
        #region Fields

        private Report report;
        private List<ExportBase> exports;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the report template.
        /// </summary>
        public Report Report
        {
            get { return report; }
            set { report = value; }
        }

        /// <summary>
        /// Gets or sets the list of exports.
        /// </summary>
        public List<ExportBase> Exports
        {
            get { return exports; }
            set { exports = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudStorageClientForm"/> class.
        /// </summary>
        public CloudStorageClientForm()
        {
            this.report = new Report();
            InitializeComponent();
            Localize();
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudStorageClientForm"/> class.
        /// </summary>
        /// <param name="report">The report template.</param>
        public CloudStorageClientForm(Report report)
        {
            this.report = report;
            InitializeComponent();
            Localize();
            Init();
        }

        #endregion // Constructors

        #region Protected Methods

        /// <summary>
        /// Initializes the list of exports.
        /// </summary>
        protected void InitExports()
        {
            exports = new List<ExportBase>();
            List<ObjectInfo> list = new List<ObjectInfo>();
            cbFileType.Items.Clear();
            RegisteredObjects.Objects.EnumItems(list);
            cbFileType.Items.Add(Res.Get("Preview,SaveNative"));
            exports.Add(null);
            foreach (ObjectInfo info in list)
            {
                if (info.Object != null && info.Object.IsSubclassOf(typeof(ExportBase)))
                {
                    cbFileType.Items.Add(Res.TryGet(info.Text));
                    exports.Add(Activator.CreateInstance(info.Object) as ExportBase);
                }
            }
            cbFileType.SelectedIndex = 0;
        }

        /// <summary>
        /// Gets the proxy settings.
        /// </summary>
        /// <returns>The proxy settings.</returns>
        protected CloudProxySettings GetProxySettings()
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

        /// <summary>
        /// Initializes the component.
        /// </summary>
        protected virtual void Init()
        {
            InitExports();
        }

        /// <summary>
        /// Checks is the string numeric.
        /// </summary>
        /// <param name="str">The checking string.</param>
        /// <returns>True if string is numeric, otherwise false.</returns>
        protected bool IsNumeric(string str)
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
        /// Finishes the form work.
        /// </summary>
        /// <returns>Returns true if work has been successfully finished, otherwise false.</returns>
        protected virtual bool Done()
        {
            if (!String.IsNullOrEmpty(tbPort.Text))
            {
                if (!IsNumeric(tbPort.Text))
                {
                    FRMessageBox.Error(Res.Get("Cloud,CloudStorage,PortError"));
                    tbPort.Focus();
                    return false;
                }
            }

            return true;
        }

        #endregion // Protected Methods

        #region Public Methods

        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();

            MyRes res = new MyRes("Cloud,CloudStorage");
            this.Text = res.Get("");
            pgFile.Text = res.Get("File");
            pgProxy.Text = res.Get("Proxy");
            labelFileType.Text = res.Get("FileType");
            buttonSettings.Text = res.Get("Settings");
            labelServer.Text = res.Get("Server");
            labelUsername.Text = res.Get("Username");
            labelPassword.Text = res.Get("Password");
        }

        #endregion // Public Methods

        #region Events Handlers

        /// <summary>
        /// SelectedIndexChanged event handler for ComboBox File Type.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        protected void cbFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonSettings.Enabled = cbFileType.SelectedIndex != 0;
        }

        /// <summary>
        /// Click event handler for Button Settings.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        protected void buttonSettings_Click(object sender, EventArgs e)
        {
            ExportBase export = exports[cbFileType.SelectedIndex];
            export.SetReport(report);
            export.ShowDialog();
        }

        /// <summary>
        /// FormClosing event handler for CloudStorageClientForm.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        protected void CloudStorageClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (!Done())
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Click event handler for button OK.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        protected virtual void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion // Events Handlers
    }
}