using System;
using System.Windows.Forms;
using FastReport.Export;
using FastReport.Export.OoXML;
using FastReport.Utils;

namespace FastReport.Forms
{
    /// <summary>
    /// Form for <see cref="Word2007Export"/>.
    /// For internal use only.
    /// </summary>
    public partial class Word2007ExportForm : BaseExportForm
    {
        /// <inheritdoc/>
        public override void Init(ExportBase export)
        {
            base.Init(export);
            MyRes res = new MyRes("Export,Docx");
            Text = res.Get("");
            Word2007Export ooxmlExport = Export as Word2007Export;
            this.radioButtonTable.Checked = (ooxmlExport.MatrixBased == true);
            cbWysiwyg.Checked = ooxmlExport.Wysiwyg;
            cbRh.SelectedItem = cbRh.Items[0];
        }
        
        /// <inheritdoc/>
        protected override void Done()
        {
            base.Done();
            Word2007Export ooxmlExport = Export as Word2007Export;
            ooxmlExport.MatrixBased = (this.radioButtonTable.Checked == true);
            ooxmlExport.Wysiwyg = cbWysiwyg.Checked;
            if (cbWysiwyg.Checked == true && radioButtonTable.Checked == true)
            {
                MyRes res = new MyRes("Export,Docx");
                ;
                if (cbRh.Text == res.Get("Exactly"))
                    ooxmlExport.RowHeightIs = "exact";
                else if (cbRh.Text == res.Get("Minimum"))
                    ooxmlExport.RowHeightIs = "min";
            }
        }
        
        /// <inheritdoc/>
        public override void Localize()
        {
            base.Localize();
            MyRes res = new MyRes("Export,Misc");
            gbOptions.Text = res.Get("Options");
            radioButtonTable.Text = res.Get("TableBased");
            radioButtonLayers.Text = res.Get("LayerBased");
            cbWysiwyg.Text = res.Get("Wysiwyg");
            res = new MyRes("Export,Docx");
            label1.Text = res.Get("RowHeight");
            cbRh.Items[0] = res.Get("Exactly");
            cbRh.Items[1] = res.Get("Minimum");
        }        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Word2007ExportForm"/> class.
        /// </summary>
        public Word2007ExportForm()
        {
            InitializeComponent();

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                radioButtonTable.Left = gbOptions.Width - radioButtonTable.Left - radioButtonTable.Width;
                radioButtonTable.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                radioButtonLayers.Left = gbOptions.Width - radioButtonLayers.Left - radioButtonLayers.Width;
                radioButtonLayers.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbWysiwyg.Left = gbOptions.Width - cbWysiwyg.Left - cbWysiwyg.Width;
                cbWysiwyg.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                label1.Left = gbOptions.Width - label1.Left - label1.Width;
                label1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbRh.Left = gbOptions.Width - cbRh.Left - cbRh.Width;
                cbRh.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from left to right
                cbOpenAfter.Left = ClientSize.Width - cbOpenAfter.Left - cbOpenAfter.Width;
                cbOpenAfter.Anchor = (AnchorStyles.Top | AnchorStyles.Right);

                // move parent components from rigth to left
                btnOk.Left = ClientSize.Width - btnOk.Left - btnOk.Width;
                btnCancel.Left = ClientSize.Width - btnCancel.Left - btnCancel.Width;
            }
        }

        private void cbWysiwyg_CheckedChanged(object sender, EventArgs e)
        {
            VisHlr();
        }

        private void radioButtonTable_CheckedChanged(object sender, EventArgs e)
        {
            VisHlr();
        }

        void VisHlr()
        {
            if (cbWysiwyg.Checked == true && radioButtonTable.Checked == true)
            {
                cbRh.Visible = true; label1.Visible = true;
            }
            else
            {
                cbRh.Visible = false; label1.Visible = false;
            }
        }

    }
}

