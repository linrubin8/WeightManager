using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Export;
using FastReport.Preview;

namespace FastReport.Export.Pdf
{
  internal class PDFExportAnnotation
  {
    public long Reference;
    public string Rect;
    public string Hyperlink;
    public int DestPage;
    public int DestY;
  }

  public partial class PDFExport : ExportBase
  {
    private List<PDFExportAnnotation> FAnnots;
    private StringBuilder FPageAnnots;

    private string GetPageAnnots()
    {
      return "/Annots [" + FPageAnnots.ToString() + "]";
    }

    private void WriteAnnots()
    {
      foreach (PDFExportAnnotation annot in FAnnots)
      {
        FXRef[(int)annot.Reference - 1] = pdf.Position;
        
        WriteLn(pdf, ObjNumber(annot.Reference));
        WriteLn(pdf, "<<");
        WriteLn(pdf, "/Type /Annot");
        WriteLn(pdf, "/Subtype /Link");
                if (isPdfA())
                    WriteLn(pdf, "/F 4");
        WriteLn(pdf, "/Rect [" + annot.Rect + "]");


        if (!isPdfX() && !String.IsNullOrEmpty(annot.Hyperlink))
        {
          WriteLn(pdf, "/BS << /W 0 >>");
          WriteLn(pdf, "/A <<");
          WriteLn(pdf, "/URI (" + annot.Hyperlink + ")");
          WriteLn(pdf, "/Type /Action");
          WriteLn(pdf, "/S /URI");
          WriteLn(pdf, ">>");
        }
        else
        {
          WriteLn(pdf, "/Border [16 16 0]");
          if (annot.DestPage < FPagesRef.Count)
          {
            WriteLn(pdf, "/Dest [" + FPagesRef[annot.DestPage].ToString() +
              " 0 R /XYZ null " + ((int)(FPagesHeights[annot.DestPage] - annot.DestY)).ToString() + " null]");
          }
        }

        WriteLn(pdf, ">>");
        WriteLn(pdf, "endobj");
      }
    }

    private void AddAnnot(ReportComponentBase obj, string rect)
    {
      if ((obj.Hyperlink.Kind == HyperlinkKind.Bookmark ||
        obj.Hyperlink.Kind == HyperlinkKind.PageNumber ||
        obj.Hyperlink.Kind == HyperlinkKind.URL) && !String.IsNullOrEmpty(obj.Hyperlink.Value))
      {
        long reference = UpdateXRef();
        FPageAnnots.AppendLine(ObjNumberRef(reference));
        PDFExportAnnotation annot = new PDFExportAnnotation();
        annot.Reference = reference;
        annot.Rect = rect;
        FAnnots.Add(annot);

        switch (obj.Hyperlink.Kind)
        {
          case HyperlinkKind.URL:
            annot.Hyperlink = obj.Hyperlink.Value;
            break;

          case HyperlinkKind.Bookmark:
            Bookmarks.BookmarkItem bookmark = Report.PreparedPages.Bookmarks.Find(obj.Hyperlink.Value);
            if (bookmark != null)
            {
                annot.DestPage = bookmark.PageNo;
                annot.DestY = (int)(bookmark.OffsetY * PDF_DIVIDER);
            }
            break;

          case HyperlinkKind.PageNumber:
            annot.DestPage = int.Parse(obj.Hyperlink.Value) - 1;
            annot.DestY = 0;
            break;
        }
      }
    }
  }
}
