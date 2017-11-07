using System;
using System.Windows.Forms;
using FastReport.Forms;
using FastReport.Utils;

namespace FastReport.Cloud.StorageClient.GoogleDrive
{
    /// <summary>
    /// Represents the Client Info diabolg form.
    /// </summary>
    public partial class ClientInfoForm : BaseDialogForm
    {
        #region Fields

        private string id;
        private string secret;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets the client ID.
        /// </summary>
        public string Id
        {
            get { return id; }
        }

        /// <summary>
        /// Gets the client secret.
        /// </summary>
        public string Secret
        {
            get { return secret; }
        }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientInfoForm"/> class.
        /// </summary>
        public ClientInfoForm()
        {
            this.id = "";
            this.secret = "";
            InitializeComponent();
            Localize();

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                labelClientId.Left = ClientSize.Width - labelClientId.Left - labelClientId.Width;
                labelClientId.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbClientId.Left = ClientSize.Width - tbClientId.Left - tbClientId.Width;
                tbClientId.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelClientSecret.Left = ClientSize.Width - labelClientSecret.Left - labelClientSecret.Width;
                labelClientSecret.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbClientSecret.Left = ClientSize.Width - tbClientSecret.Left - tbClientSecret.Width;
                tbClientSecret.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

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

            MyRes res = new MyRes("Cloud,SkyDrive");
            this.Text = res.Get("ClientInfoDialog");
            labelClientId.Text = res.Get("ClientId");
            labelClientSecret.Text = res.Get("ClientSecret");
        }

        #endregion // Public Methods

        #region Events Handlers

        private void btnOk_Click(object sender, EventArgs e)
        {
            id = tbClientId.Text;
            secret = tbClientSecret.Text;
            this.Close();
        }

        #endregion // Events Handlers
    }
}