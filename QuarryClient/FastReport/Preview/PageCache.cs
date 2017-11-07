using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;

namespace FastReport.Preview
{
    internal class PageCache : IDisposable
    {
        private List<CacheItem> FPages;
        private PreparedPages FPreparedPages;

        private int ItemIndex(int index)
        {
            for (int i = 0; i < FPages.Count; i++)
            {
                if (FPages[i].Index == index)
                    return i;
            }
            return -1;
        }

        private void RemoveAt(int index)
        {
            CacheItem item = FPages[index];
            item.Dispose();
            item = null;
            FPages.RemoveAt(index);
        }

        public ReportPage Get(int index)
        {
            int i = ItemIndex(index);
            if (i != -1)
            {
                // page exists. Put it on the top of list.
                CacheItem item = FPages[i];
                if (i != 0)
                {
                    FPages.RemoveAt(i);
                    FPages.Insert(0, item);
                }
                return item.Page;
            }

            // add new page on the top of list.
            ReportPage page = FPreparedPages.GetPage(index);

            if (FPreparedPages.Count > FPages.Count)
            {
                FPages.Insert(0, new CacheItem(index, page));

                // remove least used pages.
                while (FPages.Count > Config.PreviewSettings.PagesInCache)
                {
                    RemoveAt(FPages.Count - 1);
                }
            }
            return page;
        }

        public void Remove(int index)
        {
            int i = ItemIndex(index);
            if (i != -1)
                RemoveAt(i);
        }

        public void Clear()
        {
            while (FPages.Count > 0)
            {
                RemoveAt(0);
            }
        }

        public void Dispose()
        {
            Clear();
        }

        public PageCache(PreparedPages preparedPages)
        {
            FPreparedPages = preparedPages;
            FPages = new List<CacheItem>();
        }


        private class CacheItem : IDisposable
        {
            public int Index;
            public ReportPage Page;

            public void Dispose()
            {
                Page.Dispose();
                Page = null;
            }

            public CacheItem(int index, ReportPage page)
            {
                Index = index;
                Page = page;
            }
        }
    }
}
