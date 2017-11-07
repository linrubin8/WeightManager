using System;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Text;

namespace FastReport.Export.Pdf
{
    internal class PDFMetaData
    {
        private XmlDocument metadata;
        private string creator;
        private string description;
        private string title;
        private string producer;
        private string keywords;
        private string createdate;
        private string documentId;
        private string instanceId;
        private string part;
        private string conformance;
        private string zugferd;

        public string MetaDataString
        {
            get 
            {
                return String.Format(metadata.InnerXml, Creator, Description, Title, 
                    CreateDate, Keywords, Producer, DocumentID, InstanceID, part, conformance, zugferd);
            }
        }

        public string Creator 
        { 
            get { return creator; }
            set { creator = value; }
        }

        public string Description 
        {
            get { return description; }
            set { description = value; }
        }

        public string Title 
        {
            get { return title; }
            set { title = value; }
        }

        public string Producer 
        { 
            get { return producer; }
            set { producer = value; }
        }

        public string Keywords 
        {
            get { return keywords; }
            set { keywords = value; }
        }

        public string CreateDate 
        {
            get { return createdate; }
            set { createdate = value; }
        }

        public string DocumentID 
        {
            get { return documentId; }
            set { documentId = value; } 
        }

        public string InstanceID 
        {
            get { return instanceId; }
            set { instanceId = value; } 
        }

        public string Part
        {
            get { return part; }
            set { part = value; }
        }

        public string Conformance
        {
            get { return conformance; }
            set { conformance = value; }
        }

        public string ZUGFeRD
        {
            get { return zugferd;  }
            set { zugferd = value; }
        }

        public PDFMetaData()
        {
            metadata = new XmlDocument();
            // get a reference to the current assembly
            Assembly a = Assembly.GetExecutingAssembly();
            // get a list of resource names from the manifest
            using(Stream stream = a.GetManifestResourceStream("FastReport.Export.Pdf.MetaData.xml"))
                using(XmlTextReader reader = new XmlTextReader(stream))
                    metadata.Load(reader);
        }

        /// 
        /// <param name="filename">File name without extentions, for example "MetaDataX"</param>
        public PDFMetaData(string filename)
        {
            metadata = new XmlDocument();
            // get a reference to the current assembly
            Assembly a = Assembly.GetExecutingAssembly();
            // get a list of resource names from the manifest
            using (Stream stream = a.GetManifestResourceStream(
                String.Format("FastReport.Export.Pdf.{0}.xml",filename)))
            using (XmlTextReader reader = new XmlTextReader(stream))
                metadata.Load(reader);
        }
    }
}
