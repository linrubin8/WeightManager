using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;

namespace FastReport.Export.Pdf
{
    public partial class PDFExport : ExportBase
    {
        private class PDFOutlineNode
        {
            public string Text;
            public int Page;
            public float Offset;
            public long Number;
            public int CountTree;
            public int Count;
            public PDFOutlineNode First;
            public PDFOutlineNode Last;
            public PDFOutlineNode Parent;
            public PDFOutlineNode Prev;
            public PDFOutlineNode Next;

            public PDFOutlineNode()
            {
                Text = String.Empty;
                Offset = -1;
                Number = 0;
                Count = 0;
                CountTree = 0;
            }
        }

        private PDFOutlineNode OutlineTree;

        private long BuildOutline(PDFOutlineNode node, XmlItem xml)
        {
            PDFOutlineNode prev = null;
            PDFOutlineNode current = null;
            long currNumber = node.Number;
            for (int i = 0; i < xml.Count; i++)
            {
                int page = 0;
                float offset = 0f;
                
                string s = xml[i].GetProp("Page");
                if (s != "")
                {
                    page = int.Parse(s);
                    s = xml[i].GetProp("Offset");
                    if (s != "")
                        offset = (float)Converter.FromString(typeof(float), s) * PDF_DIVIDER;
                }
                
                // add check of page range
                
                current = new PDFOutlineNode();
                current.Text = xml[i].GetProp("Text");
                current.Page = page;
                current.Offset = offset;
                current.Prev = prev;
                if (prev != null)
                    prev.Next = current;
                else
                    node.First = current;
                prev = current;
                current.Parent = node;
                current.Number = currNumber + 1;
                currNumber = BuildOutline(current, xml[i]);                
                node.Count++;
                node.CountTree += current.CountTree + 1;
            }
            node.Last = current;
            return currNumber;
        }

        private void WriteOutline(PDFOutlineNode item)
        {
            long number;
            if (item.Parent != null)
                number = UpdateXRef();
            else
                number = item.Number;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(ObjNumber(number));
            sb.AppendLine("<<");
            if (item.Text != String.Empty)
            {
                sb.Append("/Title ");
                PrepareString(item.Text, FEncKey, FEncrypted, number, sb);
                sb.AppendLine();
            }
            if (item.Parent != null)
                sb.Append("/Parent ").AppendLine(ObjNumberRef(item.Parent.Number));
            if (item.Count > 0)
                sb.Append("/Count ").AppendLine(item.Count.ToString());
            if (item.First != null)
                sb.Append("/First ").AppendLine(ObjNumberRef(item.First.Number));
            if (item.Last != null)
                sb.Append("/Last ").AppendLine(ObjNumberRef(item.Last.Number));
            if (item.Prev != null)
                sb.Append("/Prev ").AppendLine(ObjNumberRef(item.Prev.Number));
            if (item.Next != null)
                sb.Append("/Next ").AppendLine(ObjNumberRef(item.Next.Number));


            if (item.Page < FPagesRef.Count)
            {
                sb.Append("/Dest [");
                sb.Append(ObjNumberRef(FPagesRef[item.Page]));
                sb.Append(" /XYZ 0 ");
                sb.Append(Math.Round(FPagesHeights[item.Page] - item.Offset).ToString());
                sb.Append(" 0]");               
            }
            sb.AppendLine(">>");
            sb.AppendLine("endobj");
            WriteLn(pdf, sb.ToString());            

            if (item.First != null)
                WriteOutline(item.First);
            if (item.Next != null)
                WriteOutline(item.Next);
        }
    }
}
