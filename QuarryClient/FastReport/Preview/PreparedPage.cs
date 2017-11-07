using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using FastReport.Utils;
using System.Windows.Forms;
using FastReport.Engine;

namespace FastReport.Preview
{
    internal class PreparedPage : IDisposable
    {
        private XmlItem FXmlItem;
        private PreparedPages FPreparedPages;
        private SizeF FPageSize;
        private long FTempFilePosition;
        private bool FUploaded;
        private PreparedPagePosprocessor posprocessor;

        public XmlItem Xml
        {
            get { return FXmlItem; }
            set
            {
                FXmlItem = value;
                value.Parent = null;
            }
        }

        public SizeF PageSize
        {
            get
            {
                if (FPageSize.IsEmpty)
                {
                    ReCalcSizes();
                }
                return FPageSize;
            }
            set
            {
                FPageSize = value;
            }
        }

        public int CurPosition
        {
            get { return FXmlItem.Count; }
        }

        private Report Report
        {
            get { return FPreparedPages.Report; }
        }

        private bool UseFileCache
        {
            get { return Report.UseFileCache; }
        }

        private bool DoAdd(Base c, XmlItem item)
        {
            if (c == null)
                return false;
            ReportEngine engine = Report.Engine;
            bool isRunning = Report.IsRunning && engine != null;
            if (c is ReportComponentBase)
            {
                if (isRunning && !engine.CanPrint(c as ReportComponentBase))
                    return false;
            }

            item = item.Add();
            using (FRWriter writer = new FRWriter(item))
            {
                writer.SerializeTo = SerializeTo.Preview;
                writer.SaveChildren = false;
                writer.BlobStore = FPreparedPages.BlobStore;
                writer.Write(c);
            }
            if (isRunning)
                engine.AddObjectToProcess(c, item);

            if ((c.Flags & Flags.CanWriteChildren) == 0)
            {
                ObjectCollection childObjects = c.ChildObjects;
                foreach (Base obj in childObjects)
                {
                    DoAdd(obj, item);
                }
            }

            return true;
        }

        internal ReportPage ReadPage(Base parent, XmlItem item, bool readchild, FRReader reader)
        {
            ReportPage page = ReadObject(parent, item, false, reader) as ReportPage;
            if (readchild)
                for (int i = 0; i < item.Count; i++)
                {
                    ReadObject(page, item[i], true, reader);
                }
            return page;
        }

        private Base ReadObject(Base parent, XmlItem item, bool readChildren, FRReader reader)
        {
            string objName = item.Name;

            // try to find the object in the dictionary
            Base obj = FPreparedPages.Dictionary.GetObject(objName);

            // object not found, objName is type name
            if (obj == null)
            {
                Type type = RegisteredObjects.FindType(objName);
                if (type == null)
                    return null;
                obj = Activator.CreateInstance(type) as Base;
            }
            obj.SetRunning(true);

            // read object's properties
            if (!item.IsNullOrEmptyProps())
            {
                // since the BlobStore is shared resource, lock it to avoid problems with multi-thread access.
                // this may happen in the html export that uses several threads to export one report.
                lock (reader.BlobStore)
                {
                    reader.Read(obj, item);
                }
            }

            if (readChildren)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    ReadObject(obj, item[i], true, reader);
                }
            }

            obj.Parent = parent;
            return obj;
        }

        public bool AddBand(BandBase band)
        {
            return DoAdd(band, FXmlItem);
        }

        public ReportPage GetPage()
        {
            Load();
            ReportPage page;
            using (FRReader reader = new FRReader(null))
            {
                reader.ReadChildren = false;
                reader.BlobStore = FPreparedPages.BlobStore;
                page = ReadPage(null, FXmlItem, true, reader);
            }
            page.SetReport(FPreparedPages.Report);
            new PreparedPagePosprocessor().Postprocess(page);
            ClearUploadedXml();
            return page;
        }

        internal ReportPage StartGetPage(int index)
        {
            Load();
            ReportPage page;
            using (FRReader reader = new FRReader(null))
            {
                reader.ReadChildren = false;
                reader.BlobStore = FPreparedPages.BlobStore;
                page = ReadPage(null, FXmlItem, false, reader);
                if (!(page.UnlimitedHeight || page.UnlimitedWidth))
                {
                    page.Dispose();
                    page = ReadPage(null, FXmlItem, true, reader);
                    page.SetReport(FPreparedPages.Report);
                    posprocessor = new PreparedPagePosprocessor();
                    posprocessor.Postprocess(page);
                    posprocessor = null;
                }
                else
                {
                    page.SetReport(FPreparedPages.Report);
                    posprocessor = new PreparedPagePosprocessor();
                    posprocessor.PostprocessUnlimited(this,page);
                }
            }
            if (page.MirrorMargins && (index + 1) % 2 == 0)
            {
                float f = page.LeftMargin;
                page.LeftMargin = page.RightMargin;
                page.RightMargin = f;
            }
            return page;
        }


        internal void EndGetPage(ReportPage page)
        {
            if (posprocessor != null) posprocessor = null;
            if (page != null)
                page.Dispose();
            ClearUploadedXml();
        }

        internal IEnumerable<Base> GetPageItems(ReportPage page, bool postprocess)
        {
            if (!(page.UnlimitedHeight || page.UnlimitedWidth))
            {
                foreach (Base child in page.ChildObjects)
                {
                    if (postprocess) yield return child;
                    else
                        using (child)
                            yield return child;
                }
            }
            else
                using (FRReader reader = new FRReader(null))
                {
                    reader.ReadChildren = false;
                    reader.BlobStore = FPreparedPages.BlobStore;
                    for (int i = 0; i < FXmlItem.Count; i++)
                    {
                        if (postprocess) yield return ReadObject(page, FXmlItem[i], true, reader);
                        else
                            using (Base obj = ReadObject(page, FXmlItem[i], true, reader))
                            {
                                using (Base obj2 = posprocessor.PostProcessBandUnlimitedPage(obj))
                                    yield return obj2;
                            }
                    }
                }
        }

      public void Load()
        {
            if (UseFileCache && FUploaded)
            {
                FPreparedPages.TempFile.Position = FTempFilePosition;
                XmlReader reader = new XmlReader(FPreparedPages.TempFile);
                reader.Read(FXmlItem);
            }
        }

        public void ClearUploadedXml()
        {
            if (UseFileCache && FUploaded)
                FXmlItem.Clear();
        }

        public void Upload()
        {
            if (UseFileCache && !FUploaded)
            {
                FPreparedPages.TempFile.Seek(0, SeekOrigin.End);
                FTempFilePosition = FPreparedPages.TempFile.Position;

                XmlWriter writer = new XmlWriter(FPreparedPages.TempFile);
                writer.Write(FXmlItem);

                FXmlItem.Clear();
                FUploaded = true;
            }
        }

        public XmlItem CutObjects(int index)
        {
            XmlItem result = new XmlItem();
            while (FXmlItem.Count > index)
            {
                result.AddItem(FXmlItem[index]);
            }
            return result;
        }

        public void PasteObjects(XmlItem objects, float x, float y)
        {
            if (objects.Count > 0)
            {
                // get the top object's location
                float pastedX = (objects[0].GetProp("l") != "") ?
                  Converter.StringToFloat(objects[0].GetProp("l")) : 0;
                float pastedY = (objects[0].GetProp("t") != "") ?
                  Converter.StringToFloat(objects[0].GetProp("t")) : 0;

                float deltaX = x - pastedX;
                float deltaY = y - pastedY;

                while (objects.Count > 0)
                {
                    XmlItem obj = objects[0];

                    // shift the object's location
                    float objX = (obj.GetProp("l") != "") ?
                      Converter.StringToFloat(obj.GetProp("l")) : 0;
                    float objY = (obj.GetProp("t") != "") ?
                      Converter.StringToFloat(obj.GetProp("t")) : 0;
                    obj.SetProp("l", Converter.ToString(objX + deltaX));
                    obj.SetProp("t", Converter.ToString(objY + deltaY));

                    // add object to a page
                    FXmlItem.AddItem(obj);
                }
            }
        }

        internal string GetName()
        {
            using (FRReader reader = new FRReader(null))
            {
                reader.ReadChildren = false;
                reader.BlobStore = FPreparedPages.BlobStore;
                ReportPage page = ReadObject(null, FXmlItem, false, reader) as ReportPage;
                return page.Name;
            }
        }

        internal void ReCalcSizes()
        {
            XmlItem item = FXmlItem;
            using (FRReader reader = new FRReader(null, item))
            {
                reader.BlobStore = FPreparedPages.BlobStore;
                reader.ReadChildren = false;
                ReportPage page;
                Base obj;
                BandBase obj2;

                page = ReadPage(Report, item, false, reader);
                page.UnlimitedWidthValue = 1000000;
                page.UnlimitedHeightValue = 1000000;
                float maxWidth = 0.0f;
                float maxHeight = 0.0f;
                for (int i = 0; i < item.Count; i++)
                {
                    obj = ReadObject(page, item[i], true, reader);
                    if (obj is BandBase)
                    {
                        obj2 = obj as BandBase;
                        float bandsHeight = obj2.Top + obj2.Height;
                        if (maxHeight < bandsHeight) maxHeight = bandsHeight;
                        float bandWidth = 0.0f;
                        foreach (ComponentBase obj3 in obj2.Objects)
                        {
                            if ((obj3.Anchor & AnchorStyles.Right) == 0 && obj3.Dock == DockStyle.None)
                            {
                                bandWidth = Math.Max(bandWidth, obj3.Left + obj3.Width);
                            }
                        }
                        if (maxWidth < bandWidth) maxWidth = bandWidth;
                    }
                    obj.Dispose();
                }


                if (page.UnlimitedHeight)
                    page.UnlimitedHeightValue = maxHeight + (page.TopMargin + page.BottomMargin) * Units.Millimeters;
                if (page.UnlimitedWidth)
                    page.UnlimitedWidthValue = maxWidth + (page.LeftMargin + page.RightMargin) * Units.Millimeters;


                using (FRWriter writer = new FRWriter(item))
                {
                    writer.SerializeTo = SerializeTo.Preview;
                    writer.SaveChildren = false;
                    writer.BlobStore = FPreparedPages.BlobStore;
                    writer.Write(page);
                }
                //now update bands
                float lastBottom = float.MinValue;
                if (page.UnlimitedWidth)
                    for (int i = 0; i < item.Count; i++)
                    {
                        obj = ReadObject(page, item[i], true, reader);
                        if (obj is BandBase)
                        {

                            obj2 = (BandBase)obj;
                            obj2.Width = maxWidth;
                            obj2.CalcHeight();
                            if (lastBottom > 0)
                            {
                                obj2.Top = lastBottom;
                            }
                            lastBottom = obj2.Top + obj2.Height;
                        }
                        UpdateUnlimitedPage(obj, item[i]);
                        obj.Dispose();
                    }
                else lastBottom = maxHeight;
                if (page.UnlimitedHeight)
                    page.UnlimitedHeightValue = lastBottom + (page.TopMargin + page.BottomMargin) * Units.Millimeters;
                FPageSize = new SizeF(page.WidthInPixels, page.HeightInPixels);

                using (FRWriter writer = new FRWriter(item))
                {
                    writer.SerializeTo = SerializeTo.Preview;
                    writer.SaveChildren = false;
                    writer.BlobStore = FPreparedPages.BlobStore;
                    writer.Write(page);
                }

                page.Dispose();
            }
        }

        void UpdateUnlimitedPage(Base obj, XmlItem item)
        {
            item.Clear();
            using (FRWriter writer = new FRWriter(item))
            {
                writer.SerializeTo = SerializeTo.Preview;
                writer.SaveChildren = false;
                writer.BlobStore = FPreparedPages.BlobStore;
                writer.Write(obj);
            }
            foreach (Base child in obj.ChildObjects)
            {
                UpdateUnlimitedPage(child, item.Add());
            }
        }

        public float GetLastY()
        {
            float result = 0;

            for (int i = 0; i < FXmlItem.Count; i++)
            {
                XmlItem xi = FXmlItem[i];

                BandBase obj = FPreparedPages.Dictionary.GetOriginalObject(xi.Name) as BandBase;
                if (obj != null && !(obj is PageFooterBand) && !(obj is OverlayBand))
                {
                    string s = xi.GetProp("t");
                    float top = (s != "") ? Converter.StringToFloat(s) : obj.Top;
                    s = xi.GetProp("h");
                    float height = (s != "") ? Converter.StringToFloat(s) : obj.Height;

                    if (top + height > result)
                        result = top + height;
                }
            }

            return result;
        }

        public bool ContainsBand(Type bandType)
        {
            for (int i = 0; i < FXmlItem.Count; i++)
            {
                XmlItem xi = FXmlItem[i];

                BandBase obj = FPreparedPages.Dictionary.GetOriginalObject(xi.Name) as BandBase;
                if (obj != null && obj.GetType() == bandType)
                    return true;
            }

            return false;
        }

        public bool ContainsBand(string bandName)
        {
            for (int i = 0; i < FXmlItem.Count; i++)
            {
                XmlItem xi = FXmlItem[i];

                BandBase obj = FPreparedPages.Dictionary.GetOriginalObject(xi.Name) as BandBase;
                if (obj != null && obj.Name == bandName)
                    return true;
            }

            return false;
        }

        public void Dispose()
        {
            FXmlItem.Dispose();
        }

        public PreparedPage(ReportPage page, PreparedPages preparedPages)
        {
            FPreparedPages = preparedPages;
            FXmlItem = new XmlItem();

            // page == null when we load prepared report from a file
            if (page != null)
            {
                using (FRWriter writer = new FRWriter(FXmlItem))
                {
                    writer.SerializeTo = SerializeTo.Preview;
                    writer.SaveChildren = false;
                    writer.Write(page);
                }

                FPageSize = new SizeF(page.WidthInPixels, page.HeightInPixels);                
            }
        }
    }
}
