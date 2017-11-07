using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastReport.Forms;

namespace FastReport.Cloud.StorageClient
{
    /// <summary>
    /// Represents the base form for cloud storage web browsers.
    /// </summary>
    public partial class WebBrowserFormBase : BaseDialogForm
    {
        #region Fields

        private string url;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the url string.
        /// </summary>
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initizlizes a new instance of the <see cref="WebBrowserFormBase"/> class.
        /// </summary>
        public WebBrowserFormBase()
        {
            url = "";
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebBrowserFormBase"/> class.
        /// </summary>
        /// <param name="url">The url string.</param>
        public WebBrowserFormBase(string url)
        {
            this.url = url;
            InitializeComponent();
        }

        #endregion // Constructors

        #region Events Handlers

        /// <summary>
        /// Handle the web browser form shown event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        protected void WebBrowserFormBase_Shown(object sender, EventArgs e)
        {
            wbBrowser.Navigate(url);
        }

        #endregion // Events Handlers
    }
}