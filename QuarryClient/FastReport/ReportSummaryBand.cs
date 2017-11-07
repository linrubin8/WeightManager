using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// Represents a report summary band.
  /// </summary>
  public class ReportSummaryBand : HeaderFooterBandBase
  {
    /// <summary>
    /// This property is not relevant to this class.
    /// </summary>
    [Browsable(false)]
    public new bool RepeatOnEveryPage
    {
      get { return base.RepeatOnEveryPage; }
      set { base.RepeatOnEveryPage = value; }
    }

    /// <inheritdoc/>
    public override ContextMenuBar GetContextMenu()
    {
      HeaderFooterBandBaseMenu menu = new HeaderFooterBandBaseMenu(Report.Designer);
      menu.miRepeatOnEveryPage.Visible = false;
      return menu;
    }
  
  }
}