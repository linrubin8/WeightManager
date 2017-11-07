using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Design;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.DevComponents.DotNetBar;

namespace FastReport
{
  /// <summary>
  /// The class introduces some menu items specific 
  /// to the <b>ReportComponentBase</b>.
  /// </summary>
  public class ReportComponentBaseMenu : ComponentMenuBase
  {
    #region Fields
    /// <summary>
    /// The "Can Grow" menu item.
    /// </summary>
    public ButtonItem miCanGrow;

    /// <summary>
    /// The "Can Shrink" menu item.
    /// </summary>
    public ButtonItem miCanShrink;

    /// <summary>
    /// The "Grow to Bottom" menu item.
    /// </summary>
    public ButtonItem miGrowToBottom;

    /// <summary>
    /// The "Hyperlink" menu item.
    /// </summary>
    public ButtonItem miHyperlink;
    #endregion

    #region Private Methods
    private void miCanGrow_Click(object sender, EventArgs e)
    {
      Designer.SelectedReportComponents.SetCanGrow(miCanGrow.Checked);
    }

    private void miCanShrink_Click(object sender, EventArgs e)
    {
      Designer.SelectedReportComponents.SetCanShrink(miCanShrink.Checked);
    }

    private void miGrowToBottom_Click(object sender, EventArgs e)
    {
      Designer.SelectedReportComponents.SetGrowToBottom(miGrowToBottom.Checked);
    }

    private void miHyperlink_Click(object sender, EventArgs e)
    {
      Designer.SelectedReportComponents.InvokeHyperlinkEditor();
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the <b>ReportComponentMenuBase</b> 
    /// class with default settings. 
    /// </summary>
    /// <param name="designer">The reference to a report designer.</param>
    public ReportComponentBaseMenu(Designer designer) : base(designer)
    {
      miCanGrow = CreateMenuItem(Res.Get("ComponentMenu,ReportComponent,CanGrow"), new EventHandler(miCanGrow_Click));
      miCanGrow.AutoCheckOnClick = true;
      miCanGrow.BeginGroup = true;
      miCanShrink = CreateMenuItem(Res.Get("ComponentMenu,ReportComponent,CanShrink"), new EventHandler(miCanShrink_Click));
      miCanShrink.AutoCheckOnClick = true;
      miGrowToBottom = CreateMenuItem(Res.Get("ComponentMenu,ReportComponent,GrowToBottom"), new EventHandler(miGrowToBottom_Click));
      miGrowToBottom.AutoCheckOnClick = true;
      miHyperlink = CreateMenuItem(Res.GetImage(167), Res.Get("ComponentMenu,ReportComponent,Hyperlink"), new EventHandler(miHyperlink_Click));

      int insertPos = Items.IndexOf(miEdit) + 1;
      Items.Insert(insertPos, miHyperlink);
      insertPos = Items.IndexOf(miCut);
      Items.Insert(insertPos, miCanGrow);
      Items.Insert(insertPos + 1, miCanShrink);
      Items.Insert(insertPos + 2, miGrowToBottom);

      if (!miEdit.Visible)
        miHyperlink.BeginGroup = true;
      
      bool enabled = Designer.SelectedReportComponents.Enabled;
      miCanGrow.Enabled = enabled;
      miCanShrink.Enabled = enabled;
      miGrowToBottom.Enabled = enabled;
      
      if (enabled)
      {
        ReportComponentBase first = Designer.SelectedReportComponents.First;
        miCanGrow.Checked = first.CanGrow;
        miCanShrink.Checked = first.CanShrink;
        miGrowToBottom.Checked = first.GrowToBottom;
      }  
    }
  }
}
