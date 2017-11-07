using System.Text;
using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.Dbf;
using FastReport.Utils;
using System.Globalization;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="DBFExport"/>.
    /// For internal use only.
    /// </summary>
    public partial class DbfExportForm : BaseExportForm
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new instance of the <see cref="DbfExportForm"/> class.
        /// </summary>
        public DbfExportForm()
        {
            InitializeComponent();

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                lblCodepage.Left = gbOptions.Width - lblCodepage.Left - lblCodepage.Width;
                lblCodepage.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbbCodepage.Left = gbOptions.Width - cbbCodepage.Left - cbbCodepage.Width;
                cbbCodepage.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                lblFieldNames.Left = gbOptions.Width - lblFieldNames.Left - lblFieldNames.Width;
                lblFieldNames.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbFieldNames.Left = gbOptions.Width - tbFieldNames.Left - tbFieldNames.Width;
                tbFieldNames.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbDataOnly.Left = gbOptions.Width - cbDataOnly.Left - cbDataOnly.Width;
                cbDataOnly.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from left to right
                cbOpenAfter.Left = ClientSize.Width - cbOpenAfter.Left - cbOpenAfter.Width;
                cbOpenAfter.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from rigth to left
                btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
                btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
            }
        }

        #endregion // Constructors

        #region Protected Methods
    
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            DBFExport dbfExport = Export as DBFExport;
            if (cbbCodepage.SelectedIndex == 0)
                dbfExport.Encoding = Encoding.Default;
            else if (cbbCodepage.SelectedIndex == 1)
                dbfExport.Encoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
            dbfExport.FieldNames = tbFieldNames.Text;
            dbfExport.DataOnly = cbDataOnly.Checked;
        }

        #endregion // Protected Methods

        #region Public Methods
    
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            DBFExport dbfExport = Export as DBFExport;
            if (dbfExport.Encoding == Encoding.Default)
                cbbCodepage.SelectedIndex = 0;
            else if (dbfExport.Encoding == Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage))
                cbbCodepage.SelectedIndex = 1;
            tbFieldNames.Text = dbfExport.FieldNames;
            cbDataOnly.Checked = dbfExport.DataOnly;
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Dbf");
            Text = res.Get("");
            lblCodepage.Text = res.Get("Codepage");                        
            cbbCodepage.Items[0] = res.Get("Default");
            cbbCodepage.Items[1] = res.Get("OEM");
            lblFieldNames.Text = res.Get("FieldNames");
            cbDataOnly.Text = res.Get("DataOnly");
            res = new MyRes("Export,Misc");            
            gbOptions.Text = res.Get("Options");
        }

        #endregion // Public Methods
    }
}