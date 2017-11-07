using FastReport.Utils;
using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace FastReport.Export.OoXML
{
    /// <summary>
    /// Base class for Microsoft Office 2007 export objects
    /// </summary>
    public class OOExportBase : ExportBase
    {
        private ZipArchive zip;

        /// <summary>
        /// Default XML header
        /// </summary>
        #region Constants
        protected const string xml_header = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";
        #endregion

        #region Properties
        internal ZipArchive Zip { get { return zip; } set { zip = value; } }
        #endregion

        #region Helpers
        internal string Quoted(string p)
        {
            return "\"" + p + "\" ";
        }

        internal string QuotedRoot(string p)
        {
            return "\"/" + p + "\" ";
        }
        #endregion

    }

    /// <summary>
    /// Base class for export Office Open objects
    /// </summary>
    internal abstract class OoXMLBase
    {
        #region Private fileds
        private ArrayList FRelations = new ArrayList();
        private int Id;
        #endregion

        #region Constants
        public const string xml_header = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";
        #endregion

        #region Abstract
        public abstract string RelationType { get; }
        public abstract string ContentType { get; }
        public abstract string FileName { get; }
        #endregion

        #region Helpers
        protected string Quoted(string p)
        {
            return String.Concat("\"", p, "\" ");
        }
        protected string Quoted(long p)
        {
            return String.Concat("\"", p.ToString(), "\" ");
        }
        protected string Quoted(float p)
        {
            return String.Concat("\"",  Converter.ToString(p), "\" ");
        }

        protected string GetDashStyle(System.Drawing.Drawing2D.DashStyle style)
        {
            switch (style)
            {
                case System.Drawing.Drawing2D.DashStyle.Solid:      return "<a:prstDash val=\"solid\"/>";
                case System.Drawing.Drawing2D.DashStyle.Dot:        return "<a:prstDash val=\"dot\"/>";
                case System.Drawing.Drawing2D.DashStyle.Dash:       return "<a:prstDash val=\"dash\"/>";
                case System.Drawing.Drawing2D.DashStyle.DashDot:    return "<a:prstDash val=\"dashDot\"/>";
                case System.Drawing.Drawing2D.DashStyle.DashDotDot: return "<a:prstDash val=\"sysDashDotDot\"/>";
            }
            throw new Exception("Unsupported dash style");
        }

        private string TranslatePath(string source, string dest)
        {
            int j;
            string result = "";
            string[] rel_dir_name = Path.GetDirectoryName(source).Split('\\');
            string[] items_dir_name = Path.GetDirectoryName(dest).Split('\\');

            for (int i = 0; ; i++)
            {
                if( i==rel_dir_name.Length || i==items_dir_name.Length || items_dir_name[i].CompareTo( rel_dir_name[i] ) != 0 )
                {
                    for ( j = i; j < rel_dir_name.Length; j++ ) result += "../";
                    for ( j = i; j< items_dir_name.Length; j++ ) result += items_dir_name[j];
                    break;
                }
            }

            if (result != "") result += "/";

            return result;
        }
        #endregion

        #region Properties
        internal string rId { get { return "rId" + Id.ToString(); } }
        public ArrayList RelationList { get { return FRelations; } }
        #endregion

        #region Protected methods
        protected void ExportRelations( OOExportBase export_base )
        {
            if (FRelations.Count != 0)
            {
                string relation_dir_name = Path.GetDirectoryName(FileName) + "/_rels/";
                string relation_file_name = Path.GetFileName(FileName) + ".rels";
                string related_path = "";

                MemoryStream file = new MemoryStream();
                ExportUtils.WriteLn(file, xml_header);
                ExportUtils.WriteLn(file, "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">");
                foreach (OoXMLBase relation_item in FRelations)
                {
                    related_path = TranslatePath(FileName, relation_item.FileName) + Path.GetFileName(relation_item.FileName);
                    ExportUtils.WriteLn(file, 
                        "<Relationship Id=" + Quoted(relation_item.rId) +
                        "Type=" + Quoted(relation_item.RelationType) +
                        "Target=" + Quoted(related_path) + "/>");
                }
                ExportUtils.WriteLn(file, "</Relationships>");
                file.Position = 0;
                export_base.Zip.AddStream(ExportUtils.TruncLeadSlash(relation_dir_name + relation_file_name), file);
            }
        }
        #endregion

        #region Internal Methods
        internal bool AddRelation( int Id, OoXMLBase related_object )
        {

            if (!FRelations.Contains(related_object))
            {
                related_object.Id = Id;
                FRelations.Add(related_object);
                return true;
            }
            return false;
        }
        #endregion
    }

    /// <summary>
    /// Core document properties
    /// </summary>
    class OoXMLCoreDocumentProperties : OoXMLBase
    {
        #region Class overrides
        public override string RelationType { get { return "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties"; } }
        public override string ContentType { get { return "application/vnd.openxmlformats-package.core-properties+xml"; } }
        public override string FileName { get { return "docProps/core.xml"; } }
        #endregion

        public void Export(OOExportBase OoXML)
        {
            string Title = OoXML.Report.ReportInfo.Name;             
            string Author = OoXML.Report.ReportInfo.Author;
            string Subject = OoXML.Report.ReportInfo.Description;

            if (Author.Length == 0) 
                Author = "FastReport.NET";
            if (Title.Length == 0) 
                Title = Path.GetFileNameWithoutExtension(OoXML.Report.FileName);

            MemoryStream file = new MemoryStream();
            ExportUtils.WriteLn(file, xml_header);
            ExportUtils.WriteLn(file, "<cp:coreProperties xmlns:cp=\"http://schemas.openxmlformats.org/package/2006/metadata/core-properties\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:dcterms=\"http://purl.org/dc/terms/\" xmlns:dcmitype=\"http://purl.org/dc/dcmitype/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            //Out.WriteLine("<dcterms:created xsi:type=\"dcterms:W3CDTF\">2009-06-17T07:33:19Z</dcterms:created>");
            ExportUtils.WriteLn(file, "<dc:title>" + Title + "</dc:title>");
            if (Subject.Length != 0)
                ExportUtils.WriteLn(file, "<dc:subject>" + Subject + "</dc:subject>");
            ExportUtils.WriteLn(file, "<dc:creator>" + Author + "</dc:creator>");
            ExportUtils.WriteLn(file, "</cp:coreProperties>");
            
            file.Position = 0;
            OoXML.Zip.AddStream(ExportUtils.TruncLeadSlash(FileName), file);
        }
    }

    /// <summary>
    /// Core document properties
    /// </summary>
    class OoXMLApplicationProperties : OoXMLBase
    {
        #region Class overrides
        public override string RelationType { get { return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties"; } }
        public override string ContentType { get { return "application/vnd.openxmlformats-officedocument.extended-properties+xml"; } }
        public override string FileName { get { return "docProps/app.xml"; } }
        #endregion

        public void Export(OOExportBase OoXML)
        {
            MemoryStream file = new MemoryStream();

            ExportUtils.WriteLn(file, xml_header);
            ExportUtils.WriteLn(file, "<Properties xmlns=\"http://schemas.openxmlformats.org/officeDocument/2006/extended-properties\" xmlns:vt=\"http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes\">");
            ExportUtils.WriteLn(file, "<DocSecurity>0</DocSecurity>");
            ExportUtils.WriteLn(file, "<ScaleCrop>false</ScaleCrop>");

            // Heading description
            ExportUtils.WriteLn(file, "<HeadingPairs>");
            ExportUtils.WriteLn(file, "<vt:vector size=\"2\" baseType=\"variant\">");
            ExportUtils.WriteLn(file, "<vt:variant>");
            ExportUtils.WriteLn(file, "<vt:lpstr>Worksheets</vt:lpstr>");
            ExportUtils.WriteLn(file, "</vt:variant>");
            ExportUtils.WriteLn(file, "<vt:variant>");
            ExportUtils.WriteLn(file, "<vt:i4>2</vt:i4>");
            ExportUtils.WriteLn(file, "</vt:variant>");
            ExportUtils.WriteLn(file, "</vt:vector>");
            ExportUtils.WriteLn(file, "</HeadingPairs>");

            // Titles description
            ExportUtils.WriteLn(file, "<TitlesOfParts>");
            ExportUtils.WriteLn(file, "<vt:vector size=\"1\" baseType=\"lpstr\">");
            ExportUtils.WriteLn(file, "<vt:lpstr>Лист1</vt:lpstr>");
            ExportUtils.WriteLn(file, "</vt:vector>");
            ExportUtils.WriteLn(file, "</TitlesOfParts>");
            ExportUtils.WriteLn(file, "<LinksUpToDate>false</LinksUpToDate>");
            ExportUtils.WriteLn(file, "<SharedDoc>false</SharedDoc>");
            ExportUtils.WriteLn(file, "<HyperlinksChanged>false</HyperlinksChanged>");
            ExportUtils.WriteLn(file, "<AppVersion>12.0000</AppVersion>");
            ExportUtils.WriteLn(file, "</Properties>");

            file.Position = 0;
            OoXML.Zip.AddStream(ExportUtils.TruncLeadSlash(FileName), file);
        }
    }

    internal class OoXMLThemes : OoXMLBase
    {
        #region Class overrides
        public override string RelationType { get { return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme"; } }
        public override string ContentType { get { return "application/vnd.openxmlformats-officedocument.theme+xml"; } }
        public override string FileName { get { return "ppt/theme/theme1.xml"; } }
        #endregion

        public void Export(OOExportBase OoXML, string ThemeRes, string ThemePath)
        {
            //ResourceSet set = new ResourceSet();

            // get a reference to the current assembly
            Assembly a = Assembly.GetExecutingAssembly();

            // get a list of resource names from the manifest
            string[] resNames = a.GetManifestResourceNames();

            Stream o = a.GetManifestResourceStream("FastReport.Export.OoXML.theme1.xml");

            int length = 4096;
            int bytesRead = 0;
            Byte[] buffer = new Byte[length];

            // write the required bytes
            MemoryStream fs = new MemoryStream();

            do
            {
                bytesRead = o.Read(buffer, 0, length);
                fs.Write(buffer, 0, bytesRead);
            }
            while (bytesRead == length);

            fs.Position = 0;
            OoXML.Zip.AddStream(ExportUtils.TruncLeadSlash(ThemePath), fs);

            o.Dispose();
        }
    }
}
