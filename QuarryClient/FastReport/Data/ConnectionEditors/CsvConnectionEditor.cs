using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FastReport.Utils;

namespace FastReport.Data.ConnectionEditors
{
    internal partial class CsvConnectionEditor : ConnectionEditorBase
    {
        #region Constants

        private const int NUMBER_OF_PREVIEW_STRINGS = 7;

        #endregion Constants

        #region Fields

        private Encoding encoding;
        private string separator;

        #endregion Fields

        #region Constructors

        public CsvConnectionEditor()
        {
            InitializeComponent();
            Localize();
            InitCodepagesList();
            InitSeparatorsList();
            encoding = Encoding.Default;
            separator = ";";

            // apply Right to Left layout
            if (Config.RightToLeft)
            {
                RightToLeft = RightToLeft.Yes;

                // move components to other side
                labelSelectCsvFile.Left = gbSelectDatabase.Width - labelSelectCsvFile.Left - labelSelectCsvFile.Width;
                labelSelectCsvFile.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelCodepage.Left = gbSelectDatabase.Width - labelCodepage.Left - labelCodepage.Width;
                labelCodepage.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbxCodepage.Left = gbSelectDatabase.Width - cbxCodepage.Left - cbxCodepage.Width;
                cbxCodepage.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelSeparator.Left = gbSelectDatabase.Width - labelSeparator.Left - labelSeparator.Width;
                labelSeparator.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbxSeparator.Left = gbSelectDatabase.Width - cbxSeparator.Left - cbxSeparator.Width;
                cbxSeparator.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                tbSeparator.Left = gbSelectDatabase.Width - tbSeparator.Left - tbSeparator.Width;
                tbSeparator.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbxFieldNames.Left = gbSelectDatabase.Width - cbxFieldNames.Left - cbxFieldNames.Width;
                cbxFieldNames.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbxRemoveQuotes.Left = gbSelectDatabase.Width - cbxRemoveQuotes.Left - cbxRemoveQuotes.Width;
                cbxRemoveQuotes.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                cbxTryConvertTypes.Left = gbSelectDatabase.Width - cbxTryConvertTypes.Left - cbxTryConvertTypes.Width;
                cbxTryConvertTypes.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelFilePreview.Left = gbSelectDatabase.Width - labelFilePreview.Left - labelFilePreview.Width;
                labelFilePreview.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                labelDataPreview.Left = gbSelectDatabase.Width - labelDataPreview.Left - labelDataPreview.Width;
                labelDataPreview.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            }
        }

        #endregion Constructors

        #region Private Methods

        private void InitCodepagesList()
        {
            MyRes res = new MyRes("ConnectionEditors,Csv,Codepages");
            cbxCodepage.Items.Add(res.Get("Default"));
            cbxCodepage.Items.Add(res.Get("ASCII"));
            cbxCodepage.Items.Add(res.Get("UTF8"));
            cbxCodepage.Items.Add(res.Get("UTF7"));
            cbxCodepage.Items.Add(res.Get("UTF32"));
            cbxCodepage.Items.Add(res.Get("Unicode"));
            cbxCodepage.Items.Add(res.Get("BigEndianUnicode"));
            cbxCodepage.SelectedIndex = 0;
        }

        private void InitSeparatorsList()
        {
            MyRes res = new MyRes("ConnectionEditors,Csv,Separators");
            cbxSeparator.Items.Add(res.Get("Semicolon"));
            cbxSeparator.Items.Add(res.Get("Comma"));
            cbxSeparator.Items.Add(res.Get("Tab"));
            cbxSeparator.Items.Add(res.Get("Other"));
            cbxSeparator.SelectedIndex = 0;
        }

        private int GetSelectedIndexByEncoding(Encoding encoding)
        {
            if (encoding == Encoding.Default)
            {
                return 0;
            }
            else if (encoding == Encoding.ASCII)
            {
                return 1;
            }
            else if (encoding == Encoding.UTF8)
            {
                return 2;
            }
            else if (encoding == Encoding.UTF7)
            {
                return 3;
            }
            else if (encoding == Encoding.UTF32)
            {
                return 4;
            }
            else if (encoding == Encoding.Unicode)
            {
                return 5;
            }
            else if (encoding == Encoding.BigEndianUnicode)
            {
                return 6;
            }
            return 0;
        }

        private Encoding GetEncodingBySelectedIndex(int index)
        {
            Encoding encoding = Encoding.Default;
            switch (index)
            {
                case 0:
                    encoding = Encoding.Default;
                    break;
                case 1:
                    encoding = Encoding.ASCII;
                    break;
                case 2:
                    encoding = Encoding.UTF8;
                    break;
                case 3:
                    encoding = Encoding.UTF7;
                    break;
                case 4:
                    encoding = Encoding.UTF32;
                    break;
                case 5:
                    encoding = Encoding.Unicode;
                    break;
                case 6:
                    encoding = Encoding.BigEndianUnicode;
                    break;
                default:
                    encoding = Encoding.Default;
                    break;
            }
            return encoding;
        }

        private Encoding GetEncodingByName(string encodingName)
        {
            MyRes res = new MyRes("ConnectionEditors,Csv,Codepages");
            if (encodingName == res.Get("Default"))
            {
                return Encoding.Default;
            }
            else if (encodingName == res.Get("ASCII"))
            {
                return Encoding.ASCII;
            }
            else if (encodingName == res.Get("UTF8"))
            {
                return Encoding.UTF8;
            }
            else if (encodingName == res.Get("UTF7"))
            {
                return Encoding.UTF7;
            }
            else if (encodingName == res.Get("UTF32"))
            {
                return Encoding.UTF32;
            }
            else if (encodingName == res.Get("Unicode"))
            {
                return Encoding.Unicode;
            }
            else if (encodingName == res.Get("BigEndianUnicode"))
            {
                return Encoding.BigEndianUnicode;
            }
            return Encoding.UTF8;
        }

        private void Localize()
        {
            MyRes res = new MyRes("ConnectionEditors,Csv");
            gbSelectDatabase.Text = res.Get("ConfigureDatabase");
            labelSelectCsvFile.Text = res.Get("SelectFile");
            labelCodepage.Text = res.Get("Codepage");
            labelSeparator.Text = res.Get("Separator");
            cbxFieldNames.Text = res.Get("FieldNames");
            cbxRemoveQuotes.Text = res.Get("RemoveQuotes");
            cbxTryConvertTypes.Text = res.Get("ConvertTypes");
            labelFilePreview.Text = res.Get("FilePreview");
            labelDataPreview.Text = res.Get("DataPreview");
        }

        private string[] LoadPreviewLines()
        {
            string[] lines = new string[NUMBER_OF_PREVIEW_STRINGS];
            
            StreamReader reader = new StreamReader(tbCsvFile.Text, encoding);
            for (int i = 0; i < NUMBER_OF_PREVIEW_STRINGS; i++)
            {
                bool continueLineLoading = true;
                int loadsCount = 1000;
                while (continueLineLoading)
                {
                    string str = reader.ReadLine();
                    if (!String.IsNullOrEmpty(str) && str.Contains(separator))
                    {
                        lines[i] = str;
                        continueLineLoading = false;
                    }
                    loadsCount--;
                    if (loadsCount == 0)
                    {
                        continueLineLoading = false;
                    }
                }
                if (lines[i] == null)
                {
                    string[] lnsDmp = new string[i];
                    for (int j = 0; j < i; j++)
                        lnsDmp[j] = lines[j];
                    lines = lnsDmp;
                    break;
                }
            }

            return lines;
        }

        private void LoadPreview()
        {
            if (!String.IsNullOrEmpty(tbCsvFile.Text) && !String.IsNullOrEmpty(separator))
            {
                // load file preview
                string[] lines = LoadPreviewLines();
                tbFilePreview.Lines = lines;
                
                if (lines.Length > 0 && !String.IsNullOrEmpty(lines[0]))
                {
                    DataSet ds = new DataSet();

                    // get table name from file name
                    string tableName = Path.GetFileNameWithoutExtension(tbCsvFile.Text);
                    if (String.IsNullOrEmpty(tableName))
                    {
                        tableName = "Table";
                    }

                    DataTable table = new DataTable(tableName);

                    // get values from the first string
                    string[] values = lines[0].Split(separator.ToCharArray());

                    // remove qoutes if needed
                    if (cbxRemoveQuotes.Checked)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            values[i] = values[i].Trim("\"".ToCharArray());
                        }
                    }

                    // add preview table columns
                    for (int i = 0; i < values.Length; i++)
                    {
                        DataColumn column = new DataColumn();
                        column.DataType = typeof(string);

                        // get field names from the first string if needed
                        if (cbxFieldNames.Checked)
                        {
                            column.ColumnName = values[i];
                            column.Caption = values[i];
                        }
                        else
                        {
                            column.ColumnName = CsvDataConnection.DEFAULT_FIELD_NAME + i.ToString();
                            column.Caption = CsvDataConnection.DEFAULT_FIELD_NAME + i.ToString();
                        }

                        table.Columns.Add(column);
                    }

                    // add preview table rows
                    for (int i = cbxFieldNames.Checked ? 1 : 0; i < lines.Length; i++)
                    {
                        // get values from the string
                        values = lines[i].Split(separator.ToCharArray());

                        // remove qoutes if needed
                        if (cbxRemoveQuotes.Checked)
                        {
                            for (int j = 0; j < values.Length; j++)
                            {
                                values[j] = values[j].Trim("\"".ToCharArray());
                            }
                        }

                        // add a new row
                        DataRow row = table.NewRow();
                        int valuesCount = values.Length < table.Columns.Count ? values.Length : table.Columns.Count;
                        for (int k = 0; k < valuesCount; k++)
                        {
                            row[k] = values[k];
                        }
                        table.Rows.Add(row);
                    }

                    ds.Tables.Add(table);
                    dgvTablePreview.AutoGenerateColumns = true;
                    dgvTablePreview.DataSource = ds;
                    dgvTablePreview.Update();
                    dgvTablePreview.DataMember = table.TableName;
                }
            }
        }

        #endregion Private Methods

        #region Protected Methods

        protected override string GetConnectionString()
        {
            CsvConnectionStringBuilder builder = new CsvConnectionStringBuilder();
            builder.CsvFile = tbCsvFile.Text;
            builder.Codepage = GetEncodingBySelectedIndex(cbxCodepage.SelectedIndex).CodePage.ToString();
            builder.Separator = separator;
            builder.FieldNamesInFirstString = cbxFieldNames.Checked.ToString().ToLower();
            builder.RemoveQuotationMarks = cbxRemoveQuotes.Checked.ToString().ToLower();
            builder.ConvertFieldTypes = cbxTryConvertTypes.Checked.ToString().ToLower();
            return builder.ToString();
        }

        protected override void SetConnectionString(string value)
        {
            CsvConnectionStringBuilder builder = new CsvConnectionStringBuilder(value);
            tbCsvFile.Text = builder.CsvFile;
            cbxCodepage.SelectedIndex = GetSelectedIndexByEncoding(Encoding.GetEncoding(Convert.ToInt32(builder.Codepage)));
            separator = builder.Separator;
            cbxFieldNames.Checked = builder.FieldNamesInFirstString.ToLower() == "true";
            cbxRemoveQuotes.Checked = builder.RemoveQuotationMarks.ToLower() == "true";
            cbxTryConvertTypes.Checked = builder.ConvertFieldTypes.ToLower() == "true";
        }

        #endregion Protected Methods

        #region Events Handlers

        private void tbCsvFile_ButtonClick(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = Res.Get("FileFilters,CsvFile");
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tbCsvFile.Text = dialog.FileName;
                    LoadPreview();
                }
            }
        }

        private void cbxEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            encoding = GetEncodingBySelectedIndex(cbxCodepage.SelectedIndex);
            LoadPreview();
        }

        private void cbxSeparator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSeparator.SelectedIndex == cbxSeparator.Items.Count - 1)
            {
                tbSeparator.Enabled = true;
                separator = tbSeparator.Text;
            }
            else
            {
                tbSeparator.Enabled = false;
                switch (cbxSeparator.SelectedIndex)
                {
                    case 0:
                        separator = ";";
                        break;
                    case 1:
                        separator = ",";
                        break;
                    case 2:
                        separator = "\t";
                        break;
                    default:
                        separator = ";";
                        break;
                }
            }
            LoadPreview();
        }

        private void tbSeparator_TextChanged(object sender, EventArgs e)
        {
            if (cbxSeparator.SelectedIndex == cbxSeparator.Items.Count - 1)
            {
                separator = tbSeparator.Text;
                LoadPreview();
            }
        }

        private void cbxFieldNames_CheckedChanged(object sender, EventArgs e)
        {
            LoadPreview();
        }

        private void cbxRemoveQuotes_CheckedChanged(object sender, EventArgs e)
        {
            LoadPreview();
        }

        private void cbxTryConvertTypes_CheckedChanged(object sender, EventArgs e)
        {
            LoadPreview();
        }

        #endregion Events Handlers
    }
}