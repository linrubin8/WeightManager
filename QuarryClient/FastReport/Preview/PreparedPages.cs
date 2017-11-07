using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Print;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace FastReport.Preview
{
    /// <summary>
    /// Specifies an action that will be performed on <b>PreparedPages.AddPage</b> method call.
    /// </summary>
    public enum AddPageAction
    {
        /// <summary>
        /// Do not add the new prepared page if possible, increment the <b>CurPage</b> instead.
        /// </summary>
        WriteOver,

        /// <summary>
        /// Add the new prepared page.
        /// </summary>
        Add
    }

    /// <summary>
    /// Represents the pages of a prepared report.
    /// </summary>
    /// <remarks>
    /// <para>Prepared page is a page that you can see in the preview window. Prepared pages can be
    /// accessed via <see cref="FastReport.Report.PreparedPages"/> property.</para>
    /// <para>The common scenarios of using this object are:
    /// <list type="bullet">
    ///   <item>
    ///     <description>Working with prepared pages after the report is finished: load 
    ///     (<see cref="Load(string)"/>) or save (<see cref="Save(string)"/>) pages 
    ///     from/to a .fpx file, get a page with specified index to work with its objects 
    ///     (<see cref="GetPage"/>); modify specified page (<see cref="ModifyPage"/>).
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>Using the <see cref="AddPage"/>, <see cref="AddSourcePage"/>, <see cref="AddBand"/>
    ///     methods while report is generating to produce an output.
    ///     </description>
    ///   </item>
    /// </list>
    /// </para>
    /// </remarks>
    [ToolboxItem(false)]
    public class PreparedPages : IDisposable
    {
        #region Fields
        private SourcePages FSourcePages;
        private List<PreparedPage> FPreparedPages;
        private Dictionary FDictionary;
        private Bookmarks FBookmarks;
        private Outline FOutline;
        private BlobStore FBlobStore;
        private int FCurPage;
        private AddPageAction FAddPageAction;
        private Report FReport;
        private PageCache FPageCache;
        private FileStream FTempFile;
        private bool FCanUpload;
        private string FTempFileName;
        private XmlItem FCutObjects;
        private Dictionary<string, object> FMacroValues;
        private int FFirstPassPage;
        private int FFirstPassPosition;
        #endregion

        #region Properties
        internal Report Report
        {
            get { return FReport; }
        }

        internal SourcePages SourcePages
        {
            get { return FSourcePages; }
        }

        internal Dictionary Dictionary
        {
            get { return FDictionary; }
        }

        internal Bookmarks Bookmarks
        {
            get { return FBookmarks; }
        }

        internal Outline Outline
        {
            get { return FOutline; }
        }

        internal BlobStore BlobStore
        {
            get { return FBlobStore; }
        }

        internal FileStream TempFile
        {
            get { return FTempFile; }
        }

        internal Dictionary<string, object> MacroValues
        {
            get { return FMacroValues; }
        }

        internal int CurPosition
        {
            get
            {
                if (CurPage < 0 || CurPage >= Count)
                    return 0;
                return FPreparedPages[CurPage].CurPosition;
            }
        }

        internal int CurPage
        {
            get { return FCurPage; }
            set { FCurPage = value; }
        }

        /// <summary>
        /// Gets the number of pages in the prepared report.
        /// </summary>
        public int Count
        {
            get { return FPreparedPages.Count; }
        }

        /// <summary>
        /// Specifies an action that will be performed on <see cref="AddPage"/> method call.
        /// </summary>
        public AddPageAction AddPageAction
        {
            get { return FAddPageAction; }
            set { FAddPageAction = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the prepared pages can be uploaded to the file cache.
        /// </summary>
        /// <remarks>
        /// <para>This property is used while report is generating.</para>
        /// <para>Default value for this property is <b>true</b>. That means the prepared pages may be uploaded to
        /// the file cache if needed. To prevent this (for example, if you need to access some objects
        /// on previously generated pages), set the property value to <b>false</b>.</para>
        /// </remarks>
        public bool CanUploadToCache
        {
            get { return FCanUpload; }
            set
            {
                FCanUpload = value;
                if (value == true)
                    UploadPages();
            }
        }
        #endregion

        #region Private Methods
        private void UploadPages()
        {
            if (Report.UseFileCache)
            {
                for (int i = 0; i < Count - 1; i++)
                {
                    FPreparedPages[i].Upload();
                }
            }
        }
        #endregion

        #region Protected Methods
        /// <inheritdoc/>
        public void Dispose()
        {
            Clear();
            if (FTempFile != null)
            {
                FTempFile.Dispose();
                FTempFile = null;
                if (File.Exists(FTempFileName))
                    File.Delete(FTempFileName);
            }
            BlobStore.Dispose();
        }
        #endregion

        #region Public Methods
        internal void SetReport(Report report)
        {
            FReport = report;
        }

        /// <summary>
        /// Adds a source page to the prepared pages dictionary.
        /// </summary>
        /// <param name="page">The template page to add.</param>
        /// <remarks>
        /// Call this method before using <b>AddPage</b> and <b>AddBand</b> methods. This method adds
        /// a page to the dictionary that will be used to decrease size of the prepared report.
        /// </remarks>
        public void AddSourcePage(ReportPage page)
        {
            SourcePages.Add(page);
        }

        /// <summary>
        /// Adds a new page.
        /// </summary>
        /// <param name="page">The original (template) page to add.</param>
        /// <remarks>
        /// Call the <see cref="AddSourcePage"/> method before adding a page. This method creates 
        /// a new output page with settings based on <b>page</b> parameter.
        /// </remarks>
        public void AddPage(ReportPage page)
        {
            CurPage++;
            if (CurPage >= Count || AddPageAction != AddPageAction.WriteOver)
            {
                PreparedPage preparedPage = new PreparedPage(page, this);
                FPreparedPages.Add(preparedPage);

                // upload previous page to the file cache if enabled
                if (CanUploadToCache && Count > 1)
                    FPreparedPages[Count - 2].Upload();

                AddPageAction = AddPageAction.WriteOver;
                CurPage = Count - 1;
                Report.Engine.IncLogicalPageNumber();
            }
        }

        /// <summary>
        /// Prints a band with all its child objects.
        /// </summary>
        /// <param name="band">The band to print.</param>
        /// <returns><b>true</b> if band was printed; <b>false</b> if it can't be printed 
        /// on current page due to its <b>PrintOn</b> property value.</returns>
        /// <remarks>
        /// Call the <see cref="AddPage"/> method before adding a band.
        /// </remarks>
        public bool AddBand(BandBase band)
        {
            return FPreparedPages[CurPage].AddBand(band);
        }

        /// <summary>
        /// Gets a page with specified index.
        /// </summary>
        /// <param name="index">Zero-based index of page.</param>
        /// <returns>The page with specified index.</returns>
        public ReportPage GetPage(int index)
        {
            if (index >= 0 && index < FPreparedPages.Count)
            {
                FMacroValues["Page#"] = index + Report.InitialPageNumber;
                FMacroValues["TotalPages#"] = FPreparedPages.Count + Report.InitialPageNumber - 1;
                ReportPage page = FPreparedPages[index].GetPage();
                if (page.MirrorMargins && (index + 1) % 2 == 0)
                {
                    float f = page.LeftMargin;
                    page.LeftMargin = page.RightMargin;
                    page.RightMargin = f;
                }
                return page;
            }
            else
                return null;
        }

        internal PreparedPage GetPreparedPage(int index)
        {
            if (index >= 0 && index < FPreparedPages.Count)
            {
                FMacroValues["Page#"] = index + Report.InitialPageNumber;
                FMacroValues["TotalPages#"] = FPreparedPages.Count + Report.InitialPageNumber - 1;
                return FPreparedPages[index];
            }
            else
                return null;
        }

        internal ReportPage GetCachedPage(int index)
        {
            return FPageCache.Get(index);
        }

        /// <summary>
        /// Gets the size of specified page, in pixels.
        /// </summary>
        /// <param name="index">Index of page.</param>
        /// <returns>the size of specified page, in pixels.</returns>
        public SizeF GetPageSize(int index)
        {
            return FPreparedPages[index].PageSize;
        }

        /// <summary>
        /// Replaces the prepared page with specified one.
        /// </summary>
        /// <param name="index">The index of prepared page to replace.</param>
        /// <param name="newPage">The new page to replace with.</param>
        public void ModifyPage(int index, ReportPage newPage)
        {
            PreparedPage preparedPage = new PreparedPage(newPage, this);
            foreach (Base obj in newPage.ChildObjects)
            {
                if (obj is BandBase)
                    preparedPage.AddBand(obj as BandBase);
            }
            FPreparedPages[index].Dispose();
            FPreparedPages[index] = preparedPage;
            FPageCache.Remove(index);
        }

        /// <summary>
        /// Modify the prepared page with new sizes.
        /// </summary>
        /// <param name="name">The name of prepared page to reSize.</param>
        public void ModifyPageSize(string name)
        {
            foreach (PreparedPage page in FPreparedPages)
            {
                if (String.Equals(name, page.GetName(), StringComparison.InvariantCultureIgnoreCase))
                {
                    page.ReCalcSizes();
                }
            }
        }

        /// <summary>
        /// Removes a page with the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of page to remove.</param>
        public void RemovePage(int index)
        {
            FPreparedPages[index].Dispose();
            FPreparedPages.RemoveAt(index);
        }

        internal void InterleaveWithBackPage(int backPageIndex)
        {
            PreparedPage page = FPreparedPages[backPageIndex];
            int count = backPageIndex - 1;
            for (int i = 0; i < count; i++)
            {
                FPreparedPages.Insert(i * 2 + 1, page);
            }
        }

        internal void ApplyWatermark(Watermark watermark)
        {
            SourcePages.ApplyWatermark(watermark);
            FPageCache.Clear();
        }

        internal void CutObjects(int index)
        {
            FCutObjects = FPreparedPages[CurPage].CutObjects(index);
        }

        internal void PasteObjects(float x, float y)
        {
            FPreparedPages[CurPage].PasteObjects(FCutObjects, x, y);
        }

        internal float GetLastY()
        {
            return FPreparedPages[CurPage].GetLastY();
        }

        internal void PrepareToFirstPass()
        {
            FFirstPassPage = CurPage;
            FFirstPassPosition = CurPosition;
            Outline.PrepareToFirstPass();
        }

        internal void ClearFirstPass()
        {
            Bookmarks.ClearFirstPass();
            Outline.ClearFirstPass();

            // clear all pages after specified FFirstPassPage
            while (FFirstPassPage < Count - 1)
            {
                RemovePage(Count - 1);
            }

            // if position is at begin, clear all pages
            if (FFirstPassPage == 0 && FFirstPassPosition == 0)
                RemovePage(0);

            // delete objects on the FFirstPassPage
            if (FFirstPassPage >= 0 && FFirstPassPage < Count)
                FPreparedPages[FFirstPassPage].CutObjects(FFirstPassPosition);

            CurPage = FFirstPassPage;
        }

        internal bool ContainsBand(Type bandType)
        {
            return FPreparedPages[CurPage].ContainsBand(bandType);
        }

        internal bool ContainsBand(string bandName)
        {
            return FPreparedPages[CurPage].ContainsBand(bandName);
        }

        /// <summary>
        /// Saves prepared pages to a stream.
        /// </summary>
        /// <param name="stream">The stream to save to.</param>
        public void Save(Stream stream)
        {
            stream = Compressor.Compress(stream);

            using (XmlDocument doc = new XmlDocument())
            {
                doc.AutoIndent = true;
                doc.Root.Name = "preparedreport";
                XmlItem pages = doc.Root.Add();
                pages.Name = "pages";

                // attach each page's xml to the doc
                foreach (PreparedPage page in FPreparedPages)
                {
                    page.Load();
                    pages.AddItem(page.Xml);
                }

                XmlItem sourcePages = doc.Root.Add();
                sourcePages.Name = "sourcepages";
                SourcePages.Save(sourcePages);

                XmlItem dictionary = doc.Root.Add();
                dictionary.Name = "dictionary";
                Dictionary.Save(dictionary);

                XmlItem bookmarks = doc.Root.Add();
                bookmarks.Name = "bookmarks";
                Bookmarks.Save(bookmarks);

                doc.Root.AddItem(Outline.Xml);

                XmlItem blobStore = doc.Root.Add();
                blobStore.Name = "blobstore";
                BlobStore.Save(blobStore);

                doc.Save(stream);

                // detach each page's xml from the doc
                foreach (PreparedPage page in FPreparedPages)
                {
                    page.Xml.Parent = null;
                    page.ClearUploadedXml();
                }
                Outline.Xml.Parent = null;
            }

            stream.Dispose();
        }

        /// <summary>
        /// Saves prepared pages to a .fpx file.
        /// </summary>
        /// <param name="fileName">The name of the file to save to.</param>
        public void Save(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                Save(stream);
            }
        }

        /// <summary>
        /// Loads prepared pages from a stream.
        /// </summary>
        /// <param name="stream">The stream to load from.</param>
        public void Load(Stream stream)
        {
            Clear();

            if (!stream.CanSeek)
            {
                MemoryStream tempStream = new MemoryStream();
                FileUtils.CopyStream(stream, tempStream);
                tempStream.Position = 0;
                stream = tempStream;
            }

            if (stream.Length == 0)
                return;

            bool compressed = Compressor.IsStreamCompressed(stream);
            if (compressed)
                stream = Compressor.Decompress(stream, false);

            try
            {
                using (XmlDocument doc = new XmlDocument())
                {
                    doc.Load(stream);

                    XmlItem sourcePages = doc.Root.FindItem("sourcepages");
                    SourcePages.Load(sourcePages);

                    XmlItem dictionary = doc.Root.FindItem("dictionary");
                    Dictionary.Load(dictionary);

                    XmlItem bookmarks = doc.Root.FindItem("bookmarks");
                    Bookmarks.Load(bookmarks);

                    XmlItem outline = doc.Root.FindItem("outline");
                    Outline.Xml = outline;

                    XmlItem blobStore = doc.Root.FindItem("blobstore");
                    BlobStore.LoadDestructive(blobStore);

                    XmlItem pages = doc.Root.FindItem("pages");
                    while (pages.Count > 0)
                    {
                        XmlItem pageItem = pages[0];
                        PreparedPage preparedPage = new PreparedPage(null, this);
                        FPreparedPages.Add(preparedPage);
                        preparedPage.Xml = pageItem;
                        //ModifyPageSize(preparedPage.GetName());
                    }
                }
            }
            finally
            {
                if (compressed)
                    stream.Dispose();
            }
        }

        /// <summary>
        /// Loads prepared pages from a .fpx file.
        /// </summary>
        /// <param name="fileName">The name of the file to load from.</param>
        public void Load(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                Load(stream);
            }
        }

        internal bool Print()
        {
            return Print(1);
        }

        internal bool Print(int curPage)
        {
            PrinterSettings printerSettings = null;
            if (Report.PrintSettings.ShowDialog)
            {
                bool ok = Report.ShowPrintDialog(out printerSettings);
                if (!ok)
                    return false;
            }

            Print(printerSettings, curPage);
            return true;
        }

        internal void Print(PrinterSettings printerSettings, int curPage)
        {
            Printer printer = new Printer(Report);
            printer.Print(printerSettings, curPage);
        }

        /// <summary>
        /// Clears the prepared report's pages.
        /// </summary>
        public void Clear()
        {
            FSourcePages.Clear();
            FPageCache.Clear();
            foreach (PreparedPage page in FPreparedPages)
            {
                page.Dispose();
            }
            FPreparedPages.Clear();
            FDictionary.Clear();
            FBookmarks.Clear();
            FOutline.Clear();
            FBlobStore.Clear();
            FCurPage = 0;
        }

        internal void ClearPageCache()
        {
            FPageCache.Clear();
        }
        #endregion

        /// <summary>
        /// Creates the pages of a prepared report
        /// </summary>
        /// <param name="report"></param>
        public PreparedPages(Report report)
        {
            FReport = report;
            FSourcePages = new SourcePages(this);
            FPreparedPages = new List<PreparedPage>();
            FDictionary = new Dictionary(this);
            FBookmarks = new Bookmarks();
            FOutline = new Outline();
            FBlobStore = new BlobStore(report != null ? report.UseFileCache : false);
            FPageCache = new PageCache(this);
            FMacroValues = new Dictionary<string, object>();
            if (FReport != null && FReport.UseFileCache)
            {
                FTempFileName = Config.CreateTempFile("");
                FTempFile = new FileStream(FTempFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
        }
    }
}
