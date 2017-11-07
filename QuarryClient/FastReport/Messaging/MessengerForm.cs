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

namespace FastReport.Messaging
{
    /// <summary>
    /// Represents form of messenger.
    /// </summary>
    public partial class MessengerForm : BaseDialogForm
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
        /// Initializes a new instance of the <see cref="MessengerForm"/> class.
        /// </summary>
        public MessengerForm()
        {
            this.report = new Report();
            InitializeComponent();
            Localize();
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessengerForm"/> class.
        /// </summary>
        /// <param name="report">The report template.</param>
        public MessengerForm(Report report)
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
        protected ProxySettings GetProxySettings()
        {
            ProxySettings proxySettings = null;
            if (!String.IsNullOrEmpty(tbProxyServer.Text))
            {
                int port = 0;
                if (!IsNumeric(tbProxyPort.Text))
                {
                    FRMessageBox.Error(Res.Get("Messaging,MessengerForm,PortError"));
                }
                else
                {
                    port = Convert.ToInt32(tbProxyPort.Text);
                }
                proxySettings = new ProxySettings(tbProxyServer.Text, port, tbProxyUsername.Text, tbProxyPassword.Text, ProxyType.Http);
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
            if (!String.IsNullOrEmpty(tbProxyPort.Text))
            {
                if (!IsNumeric(tbProxyPort.Text))
                {
                    FRMessageBox.Error(Res.Get("Messaging,MessengerForm,PortError"));
                    tbProxyPort.Focus();
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

            MyRes res = new MyRes("Messaging,MessengerForm");
            this.Text = res.Get("");
            pgFile.Text = res.Get("File");
            pgProxy.Text = res.Get("Proxy");
            labelFileType.Text = res.Get("FileType");
            buttonSettings.Text = res.Get("Settings");
            labelProxyServer.Text = res.Get("Server");
            labelProxyUsername.Text = res.Get("Username");
            labelProxyPassword.Text = res.Get("Password");
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
        protected void MessengerForm_FormClosing(object sender, FormClosingEventArgs e)
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