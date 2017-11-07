using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;

namespace FastReport.Preview
{
  internal class PreviewPages : CollectionBase
  {
    private int FMaxWidth;

    private PageItem this[int index]
    {
      get { return List[index] as PageItem; }
    }

    private int Add(PageItem value)
    {
      return List.Add(value);
    }

    public void AddPage(SizeF pageSize, float zoom)
    {
      PageItem item = new PageItem();
      item.Width = (int)Math.Round(pageSize.Width * zoom);
      item.Height = (int)Math.Round(pageSize.Height * zoom);
      Add(item);
    }

    // Layouts the pages in the preview window: calculates each page's offset. 
    public void LayoutPages(int clientWidth, Point pageOffset)
    {
      FMaxWidth = 0;
      int curY = pageOffset.Y;
      int i = 0;

      // Note that offset between page and window edges is pageOffset; gap between pages is 15
      
      while (i < Count)
      {
        int j = i;
        int curX = 0;
        int maxY = 0;

        // find series of pages that will fit in the clientWidth,
        // also calculate max height of series
        while (j < Count)
        {
          PageItem item = this[j];
          // check the width, allow at least one iteration
          if (curX > 0 && curX + item.Width > clientWidth)
            break;
          item.OffsetX = curX;
          item.OffsetY = curY;
          curX += item.Width + 15;
          if (item.Height > maxY)
            maxY = item.Height;
          j++;
        }

        if (curX > FMaxWidth)
          FMaxWidth = curX;

        // center series horizontally
        int offs = (clientWidth - curX + 15) / 2;
        if (offs < pageOffset.X)
          offs = pageOffset.X;
        while (i < j)
        {
          this[i].OffsetX += offs;
          i++;
        }

        curY += maxY + 15;
      }
      
      FMaxWidth -= 15 - pageOffset.X;
    }
    
    private int QuickSearch(int offsetY, bool useAdd)
    {
      int i0 = 0;
      int i1 = Count - 1;

      while (i0 <= i1)
      {
        int i = (i0 + i1) / 2;
        int add = useAdd ? this[i].Height / 5 : 0;
        if (this[i].OffsetY <= offsetY + add)
          i0 = i + 1;
        else
          i1 = i - 1;
      }
      if (i1 < 0)
        i1 = 0;
      return i1;
    }
    
    private int GetFirstPageInRow(int index)
    {
      int offs = this[index].OffsetY;
      while (index > 0)
      {
        if (this[index - 1].OffsetY != offs)
          break;
        index--;
      }
      return index;
    }

    // Finds first visible page which OffsetY is near offsetY. This method is used when scrolling the
    // preview window to find current page number.
    public int FindPage(int offsetY)
    {
      int index = QuickSearch(offsetY, true);
      return GetFirstPageInRow(index);
    }

    // Finds first visible page which OffsetY is near offsetY. This method is used when drawing the page.
    public int FindFirstVisiblePage(int offsetY)
    {
      int index = QuickSearch(offsetY, false);
      return GetFirstPageInRow(index);
    }

    // Finds last visible page which OffsetY is near offsetY. This method is used when drawing the page.
    public int FindLastVisiblePage(int offsetY)
    {
      return QuickSearch(offsetY, false);
    }

    // Finds the page which is clicked by the mouse.
    public int FindPage(int offsetX, int offsetY)
    {
      int lastIndex = QuickSearch(offsetY, false);
      if (this.Count <= lastIndex) return lastIndex;
      int firstIndex = GetFirstPageInRow(lastIndex);

      // find exact page
      for (int i = firstIndex; i <= lastIndex; i++)
      {
        PageItem item = this[i];
        if (new Rectangle(item.OffsetX, item.OffsetY, item.Width, item.Height).Contains(
          new Point(offsetX, offsetY)))
          return i;
      }
      return firstIndex;
    }

    // Returns the page bounds.
    public Rectangle GetPageBounds(int index)
    {
      if (Count <= index) return Rectangle.Empty;
      PageItem item = this[index];
      return new Rectangle(item.OffsetX, item.OffsetY, item.Width, item.Height);
    }

    // Returns max size of the pages. Used to determine the scroll range. 
    public Size GetMaxSize(Point pageOffset)
    {
      if (Count == 0)
        return Size.Empty;
      
      int maxHeight = 0;
      int pageNo = Count - 1;
      
      while (pageNo >= 0 && IsSameRow(pageNo, Count - 1))
      {
        int pageBottom = this[pageNo].OffsetY + this[pageNo].Height;
        if (pageBottom > maxHeight)
          maxHeight = pageBottom;
        pageNo--;
      }
      
      return new Size(FMaxWidth + pageOffset.X, maxHeight + pageOffset.Y);
    }
    
    // Returns true if two pages are in the same row (OffsetY is the same)
    public bool IsSameRow(int page1, int page2)
    {
      if (page1 < 0 || page2 < 0 || page1 >= Count || page2 >= Count)
        return false;
      return this[page1].OffsetY == this[page2].OffsetY;  
    }

    // Swaps two pages in preview. Should be used to place pages from right to left.
    public void SwapPageBounds(int pageIndex1, int pageIndex2)
    {
        PageItem temp = new PageItem();
        PageItem page1 = this[pageIndex1];
        PageItem page2 = this[pageIndex2];

        if (page1.OffsetX < page2.OffsetX)
        {
            temp.OffsetX = page1.OffsetX;
            page1.OffsetX = page2.OffsetX;
            page2.OffsetX = temp.OffsetX;
        }
    }


    private class PageItem
    {
      public int Height;
      public int Width;
      public int OffsetX;
      public int OffsetY;
    }
  }
}
