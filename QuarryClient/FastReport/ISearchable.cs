using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FastReport.Utils;

namespace FastReport
{
  /// <summary>
  /// Provides the "search" functionality in the preview and designer.
  /// </summary>
  public interface ISearchable
  {
    /// <summary>
    /// Finds the specified text inside the object.
    /// </summary>
    /// <param name="text">Text to find.</param>
    /// <param name="matchCase"><b>true</b> to perform case-sensitive search.</param>
    /// <param name="wholeWord"><b>true</b> to find whole words only.</param>
    /// <returns>Array of character ranges that describes the occurences of text found; 
    /// <b>null</b> if text not found.</returns>
    CharacterRange[] SearchText(string text, bool matchCase, bool wholeWord);
    
    /// <summary>
    /// Draws the highlight to show the text found.
    /// </summary>
    /// <param name="e">Draw event arguments.</param>
    /// <param name="range">Range of characters to highlight.</param>
    void DrawSearchHighlight(FRPaintEventArgs e, CharacterRange range);
  }
}
