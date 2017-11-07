using System;
using System.Windows.Forms;
using FastReport.Forms;
using FastReport.Utils;

namespace FastReport.Cloud.StorageClient.Dropbox
{
    /// <summary>
    /// Represents the Application Info diabolg form.
    /// </summary>
    public partial class ApplicationInfoForm : BaseDialogForm
    {
        #region Fields
    
        private string accessToken;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets the access token.
        /// </summary>
        public string AccessToken
        {
            get { return accessToken; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInfoForm"/> class.
        /// </summary>
        public ApplicationInfoForm()
        {
            accessToken = "";
            InitializeComponent();
            Localize();

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                labelAccessToken.Left = ClientSize.Width - labelAccessToken.Left - labelAccessToken.Width;
                labelAccessToken.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbAccessToken.Left = ClientSize.Width - tbAccessToken.Left - tbAccessToken.Width;
                tbAccessToken.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from rigth to left
                btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
                btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
            }
        }

        #endregion // Constructors

        #region Public Methods

        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();

            MyRes res = new MyRes("Cloud,Dropbox");
            this.Text = res.Get("ApplicationInfoDialog");
            labelAccessToken.Text = res.Get("AccessToken");
        }

        #endregion // Public Methods

        #region Events Handlers

        private void btnOk_Click(object sender, EventArgs e)
        {
            accessToken = tbAccessToken.Text;
            this.Close();
        }

        #endregion // Events Handlers
    }
}