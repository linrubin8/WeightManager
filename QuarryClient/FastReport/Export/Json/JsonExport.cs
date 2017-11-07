using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FastReport.Forms;
using FastReport.Table;
using FastReport.Utils;

namespace FastReport.Export.Json
{
    /// <summary>
    /// Represents the JSON export filter.
    /// </summary>
    public class JsonExport : ExportBase
    {
        #region Fields
        Encoding encoding;
        int indent;
        string indention = "  ";
        int pageNo = 0;
        #endregion

        #region Private Methods
        void write(Stream target, string data)
        {
            if (string.IsNullOrEmpty(data))
                return;

            data = appendIndention(data);

            byte[] bytes = encoding.GetBytes(data + Environment.NewLine);
            target.Write(bytes, 0, bytes.Length);
        }

        string appendIndention(string str)
        {
            for (int i = 0; i < indent; i++)
                str = indention + str;
            return str;
        }

        string comma(int i, int length)
        {
            return i < length - 1 ? "," : "";
        }

        string wrap(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "\"\"";

            char c = '\0';
            int i;
            int len = s.Length;
            StringBuilder sb = new StringBuilder(len + 4);
            string t;

            for (i = 0; i < len; i += 1)
            {
                c = s[i];
                switch (c)
                {
                    case '\\':
                    case '"':
                        sb.Append('\\');
                        sb.Append(c);
                        break;
                    case '/':
                        sb.Append('\\');
                        sb.Append(c);
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    default:
                        if (c < ' ')
                        {
                            t = "000" + String.Format("X", c);
                            sb.Append("\\u" + t.Substring(t.Length - 4));
                        }
                        else {
                            sb.Append(c);
                        }
                        break;
                }
            }

            return "\"" +  sb.ToString() + "\"";
        }

        void exportObj(Base c, List<string> items)
        {
            if (c is ReportComponentBase)
            {
                ReportComponentBase obj = c as ReportComponentBase;

                //if (FDataOnly && (obj.Parent == null || !(obj.Parent is DataBand)))
                //continue;

                //if (obj is TableCell)
                //continue;

                if (obj is TextObject)
                {
                    TextObject text = obj as TextObject;
                    if (!string.IsNullOrEmpty(text.Text))
                        items.Add("{\"TextObject\":" + wrap(text.Text) + "}");
                }
                else if (obj is PictureObject)
                {
                    PictureObject pic = obj as PictureObject;
                    if (!string.IsNullOrEmpty(pic.ImageLocation))
                        items.Add("{\"PictureObject\":" + wrap(pic.ImageLocation) + "}");
                }
                else if (obj is RichObject)
                {
                    RichObject rich = obj as RichObject;
                    if (!string.IsNullOrEmpty(rich.Text))
                        items.Add("{\"RichObject\":" + wrap(rich.Text) + "}");
                }
                else if (obj is HtmlObject)
                {
                    HtmlObject html = obj as HtmlObject;
                    if (!string.IsNullOrEmpty(html.Text))
                        items.Add("{\"HtmlObject\":" + wrap(html.Text) + "}");
                }
                else if (obj is TableBase)
                {
                    StringBuilder tableData = new StringBuilder();
                    tableData.Append("{\"TableObject\":[");
                    indent++;

                    TableBase table = obj as TableBase;
                    for (int i = 0; i < table.RowCount; i++)
                    {
                        string row = "[";

                        for (int j = 0; j < table.ColumnCount; j++)
                        {
                            TableCell cell = table[j, i];
                            row += wrap(cell.Text) + comma(j, table.ColumnCount);
                        }

                        row += "]" + comma(i, table.RowCount);
                        tableData.Append(Environment.NewLine + appendIndention(row));
                    }

                    indent--;
                    tableData.Append(Environment.NewLine + appendIndention("]}"));
                    items.Add(tableData.ToString());
                }
            }
        }
        #endregion

        #region Protected Methods
        /// <inheritdoc/>
        protected override void Start()
        {
            pageNo = 0;

            write(Stream, "{");
            indent++;

            write(Stream, "\"report\": [");
            indent++;
        }

        /// <inheritdoc/>
        protected override void Finish()
        {
            indent--;
            write(Stream, "]");

            indent--;
            write(Stream, "}");
        }

        /// <inheritdoc/>
        protected override string GetFileFilter()
        {
            return new MyRes("FileFilters").Get("JsonFile");
        }

        /// <inheritdoc/>
        protected override void ExportPageBegin(ReportPage page)
        {
            base.ExportPageBegin(page);

            write(Stream, "{");
            indent++;

            write(Stream, "\"page\":\"" + (pageNo + 1) + "\",");

            write(Stream, "\"items\":[");
            indent++;
        }
        
        /// <inheritdoc/>
        protected override void ExportPageEnd(ReportPage page)
        {
            base.ExportPageEnd(page);

            indent--;
            write(Stream, "]");

            indent--;
            write(Stream, "}" + comma(pageNo, Pages.Length));

            pageNo++;
        }

        /// <inheritdoc/>
        protected override void ExportBand(Base band)
        {
            base.ExportBand(band);
            if (band.Parent == null) return;
            List<string> items = new List<string>();

            exportObj(band, items);
            foreach (Base c in band.AllObjects)
                exportObj(c, items);

            for (int i = 0; i < items.Count; i++)
            {
                string item = items[i];

                if (string.IsNullOrEmpty(item))
                    continue;

                write(Stream, item + comma(i, items.Count));
            }
        }
        #endregion

        #region Public Methods
        /// <inheritdoc/>
        public override bool ShowDialog()
        {
            using (JsonExportForm form = new JsonExportForm())
            {
                form.Init(this);
                return form.ShowDialog() == DialogResult.OK;
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonExport"/> class.
        /// </summary>       
        public JsonExport()
        {
            encoding = Encoding.Unicode;
        }
    }
}
