using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Controls;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.Design.PageDesigners.Page;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.StandardDesigner
{
  internal class StyleToolbar : ToolbarBase
  {
    #region Fields
    public StyleComboBoxItem cbxStyle;
    public ButtonItem btnStyles;
    #endregion

    #region Private Methods
    private void UpdateControls()
    {
      bool enabled = Designer.SelectedReportComponents.Enabled;
      
      cbxStyle.Enabled = enabled;
      cbxStyle.Report = Designer.ActiveReport;
      if (enabled)
        cbxStyle.Style = Designer.SelectedReportComponents.First.Style;
    }

    private void cbxStyle_StyleSelected(object sender, EventArgs e)
    {
      (Designer.ActiveReportTab.ActivePageDesigner as ReportPageDesigner).Workspace.Focus();
      Designer.SelectedReportComponents.SetStyle(cbxStyle.Style);
    }
    #endregion

    #region Public Methods
    public override void SelectionChanged()
    {
      base.SelectionChanged();
      UpdateControls();
    }

    public override void UpdateContent()
    {
      base.UpdateContent();
      UpdateControls();
    }

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Designer,Toolbar,Style");
      Text = res.Get("");

      SetItemText(btnStyles, res.Get("Styles"));
      UpdateContent();
    }
    #endregion

    public StyleToolbar(Designer designer) : base(designer)
    {
      Name = "StyleToolbar";
      
      cbxStyle = new StyleComboBoxItem();
      cbxStyle.Name = "cbxStyleStyle";
      cbxStyle.StyleSelected += cbxStyle_StyleSelected;
      btnStyles = CreateButton("btnStyleStyles", Res.GetImage(87), Designer.cmdReportStyles.Invoke);

      Items.AddRange(new BaseItem[] { cbxStyle, btnStyles, CustomizeItem });
      Localize();
    }

  }

}
