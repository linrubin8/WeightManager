using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Forms;

namespace FastReport.Map.Forms
{
  internal partial class MapEditorControl : UserControl
  {
    #region Fields
    private MapObject FMap;
    private bool FUpdating;
    #endregion // Fields

    #region Properties
    public event EventHandler Changed;

    public MapObject Map
    {
      get { return FMap; }
      set
      {
        FMap = value;
        UpdateControls();
      }
    }
    #endregion // Properties

    #region Private Methods
    private void OnChange()
    {
      if (Changed != null)
        Changed(this, EventArgs.Empty);
    }

    private void UpdateControls()
    {
      FUpdating = true;

      #region General tab
      cbMercator.Checked = Map.MercatorProjection;
      #endregion

      #region Color scale tab
      ColorScale cs = Map.ColorScale;
      cbVisible.Checked = cs.Visible;
      cbHideIfNoData.Checked = cs.HideIfNoData;
      UpdateDockButtons();
      tbTitleText.Text = cs.TitleText;
      tbTitleFont.Text = Converter.ToString(cs.TitleFont);
      cbxTitleColor.Color = cs.TitleColor;
      tbFont.Text = Converter.ToString(cs.Font);
      cbxTextColor.Color = cs.TextColor;
      cbxBorderColor.Color = cs.BorderColor;
      tbFormat.Text = cs.Format;
      tbNoDataText.Text = cs.NoDataText;
      #endregion

      FUpdating = false;
    }

    private void UpdateDockButtons()
    {
      Button[] dockButtons = new Button[] { btnD1, btnD2, btnD3, btnD4, btnD5, btnD6, btnD7, btnD8 };
      int dock = (int)Map.ColorScale.Dock;
      for (int i = 0; i < dockButtons.Length; i++)
      {
        dockButtons[i].BackColor = dock == (int)dockButtons[i].Tag ? Color.Orange : SystemColors.ButtonFace;
      }
    }

    private void Init()
    {
      MyRes res = null;
      MyRes commonRes = new MyRes("Forms,MapEditor,Common");

      #region General tab
      res = new MyRes("Forms,MapEditor,MapEditorControl,General");
      pgGeneral.Text = res.Get("");
      cbMercator.Text = res.Get("Mercator");
      #endregion

      #region Color scale tab
      res = new MyRes("Forms,MapEditor,MapEditorControl,ColorScale");
      pgColorScale.Text = res.Get("");
      cbVisible.Text = commonRes.Get("Visible");
      cbHideIfNoData.Text = res.Get("HideIfNoData");
      
      tabGeneral.Text = res.Get("General");
      btnBorder.Text = res.Get("Border");
      btnBorder.Image = Res.GetImage(36);
      btnFill.Text = res.Get("Fill");
      btnFill.Image = Res.GetImage(38);
      lblDock.Text = commonRes.Get("Dock");
      Button[] dockButtons = new Button[] { btnD1, btnD2, btnD3, btnD4, btnD5, btnD6, btnD7, btnD8 };
      for (int i = 0; i < dockButtons.Length; i++)
      {
        dockButtons[i].Tag = i;
      }
      
      tabTitle.Text = res.Get("Title");
      lblTitleText.Text = commonRes.Get("Text");
      lblTitleFont.Text = commonRes.Get("Font");
      lblTitleColor.Text = commonRes.Get("TextColor");
      tbTitleFont.Image = Res.GetImage(59);

      tabValues.Text = res.Get("Values");
      tbFont.Image = Res.GetImage(59);
      lblFont.Text = commonRes.Get("Font");
      lblTextColor.Text = commonRes.Get("TextColor");
      lblBorderColor.Text = commonRes.Get("BorderColor");
      lblFormat.Text = commonRes.Get("Format");
      lblNoDataText.Text = res.Get("NoDataText");
      #endregion
    }
    #endregion // Private Methods

    #region Internal Methods

    internal void EnableMercatorProtection(bool enable)
    {
        cbMercator.Checked = false;
        cbMercator.Enabled = enable;
    }

    #endregion // Internal Methods

    #region General tab
    private void cbMercator_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Map.MercatorProjection = cbMercator.Checked;
      OnChange();
    }
    #endregion

    #region Color scale tab
    private void cbVisible_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Map.ColorScale.Visible = cbVisible.Checked;
      OnChange();
    }

    private void cbHideIfNoData_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Map.ColorScale.HideIfNoData = cbHideIfNoData.Checked;
      OnChange();
    }

    private void btnBorder_Click(object sender, EventArgs e)
    {
      using (BorderEditorForm form = new BorderEditorForm())
      {
        form.Border = Map.ColorScale.Border;
        if (form.ShowDialog() == DialogResult.OK)
          Map.ColorScale.Border = form.Border;
      }
      OnChange();
    }

    private void btnFill_Click(object sender, EventArgs e)
    {
      using (FillEditorForm form = new FillEditorForm())
      {
        form.Fill = Map.ColorScale.Fill;
        if (form.ShowDialog() == DialogResult.OK)
          Map.ColorScale.Fill = form.Fill;
      }
      OnChange();
    }

    private void btnD1_Click(object sender, EventArgs e)
    {
      int tag = (int)((sender as Button).Tag);
      Map.ColorScale.Dock = (ScaleDock)tag;
      UpdateDockButtons();
      OnChange();
    }

    private void tbTitleText_Leave(object sender, EventArgs e)
    {
      Map.ColorScale.TitleText = tbTitleText.Text;
      OnChange();
    }

    private void tbTitleFont_ButtonClick(object sender, EventArgs e)
    {
      using (FontDialog form = new FontDialog())
      {
        form.Font = Map.ColorScale.TitleFont;
        if (form.ShowDialog() == DialogResult.OK)
        {
          Map.ColorScale.TitleFont = form.Font;
          tbTitleFont.Text = Converter.ToString(form.Font);
        }
      }
      OnChange();
    }

    private void cbxTitleColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Map.ColorScale.TitleColor = cbxTitleColor.Color;
      OnChange();
    }

    private void tbFont_ButtonClick(object sender, EventArgs e)
    {
      using (FontDialog form = new FontDialog())
      {
        form.Font = Map.ColorScale.Font;
        if (form.ShowDialog() == DialogResult.OK)
        {
          Map.ColorScale.Font = form.Font;
          tbFont.Text = Converter.ToString(form.Font);
        }
      }
      OnChange();
    }

    private void cbxTextColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Map.ColorScale.TextColor = cbxTextColor.Color;
      OnChange();
    }

    private void cbxBorderColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Map.ColorScale.BorderColor = cbxBorderColor.Color;
      OnChange();
    }

    private void tbFormat_Leave(object sender, EventArgs e)
    {
      Map.ColorScale.Format = tbFormat.Text;
      OnChange();
    }

    private void tbNoDataText_Leave(object sender, EventArgs e)
    {
      Map.ColorScale.NoDataText = tbNoDataText.Text;
      OnChange();
    }
    #endregion

    public MapEditorControl()
    {
      InitializeComponent();
      Init();
    }
  }
}
