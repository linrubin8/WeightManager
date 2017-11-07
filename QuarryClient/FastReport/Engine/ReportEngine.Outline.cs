using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;

namespace FastReport.Engine
{
  public partial class ReportEngine
  {
    /// <summary>
    /// Creates a new outline element with specified text.
    /// </summary>
    /// <param name="text">Text of element.</param>
    /// <remarks>
    /// After you call this method, the element will be added to the current position in the outline.
    /// The next call to <b>AddOutline</b> will add new element as a child of this element.
    /// To shift the position, use the <see cref="OutlineRoot"/> or 
    /// <see cref="OutlineUp()">OutlineUp</see> methods.
    /// </remarks>
    public void AddOutline(string text)
    {
      AddOutline(text, CurPage, CurY);
    }

    private void AddOutline(string name, int pageNo, float curY)
    {
      PreparedPages.Outline.Add(name, pageNo, curY);
    }

    private void AddBandOutline(BandBase band)
    {
      if (band.Visible && !String.IsNullOrEmpty(band.OutlineExpression) && !band.Repeated)
      {
        AddOutline(Converter.ToString(Report.Calc(band.OutlineExpression)), CurPage, CurY);
        if (!(band is DataBand) && !(band is GroupHeaderBand))
          OutlineUp();
      }
    }
    
    private void AddPageOutline()
    {
      if (!String.IsNullOrEmpty(FPage.OutlineExpression))
        AddOutline(Converter.ToString(Report.Calc(FPage.OutlineExpression)), CurPage, 0);
    }

    /// <summary>
    /// Sets the current outline position to root.
    /// </summary>
    public void OutlineRoot()
    {
      PreparedPages.Outline.LevelRoot();
    }

    /// <summary>
    /// Shifts the current outline position one level up.
    /// </summary>
    public void OutlineUp()
    {
      PreparedPages.Outline.LevelUp();
    }

    private void OutlineUp(BandBase band)
    {
      if (band is DataBand || band is GroupHeaderBand)
      {
        if (!String.IsNullOrEmpty(band.OutlineExpression))
          OutlineUp();
      }  
    }
    
    /// <summary>
    /// Creates a new bookmark with specified name at current position.
    /// </summary>
    /// <param name="name"></param>
    public void AddBookmark(string name)
    {
      if (!String.IsNullOrEmpty(name))
        PreparedPages.Bookmarks.Add(name, CurPage, CurY);
    }
    
    /// <summary>
    /// Gets a page number for the specified bookmark name.
    /// </summary>
    /// <param name="name">Name of bookmark.</param>
    /// <returns>Page number if bookmark with such name found; 0 otherwise.</returns>
    /// <remarks>
    /// Use this method to print the table of contents in your report. Normally it can be done
    /// using bookmarks. 
    /// <note type="caution">
    /// You must set your report to double pass to use this method.
    /// </note>
    /// </remarks>
    public int GetBookmarkPage(string name)
    {
      return PreparedPages.Bookmarks.GetPageNo(name);
    }
  }
}
