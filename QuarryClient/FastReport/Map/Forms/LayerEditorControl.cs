using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Data;
using FastReport.Controls;
using System.Globalization;

namespace FastReport.Map.Forms
{
  internal partial class LayerEditorControl : UserControl
  {
    #region Fields
    private MapLayer FLayer;
    private bool FUpdating;
    private ColorRangeEditorControl crColorRanges;
    private SizeRangeEditorControl srSizeRanges;
    #endregion // Fields

    #region Properties
    public event EventHandler Changed;

    public MapLayer Layer
    {
      get { return FLayer; }
      set
      {
        FLayer = value;
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

    private void OnChange(object sender, EventArgs e)
    {
      OnChange();
    }

    private void UpdateControls()
    {
      FUpdating = true;

      #region Data tab
      cbxDataSource.Items.Clear();
      cbxDataSource.Items.Add(Res.Get("Misc,None"));
      cbxDataSource.SelectedIndex = 0;

      foreach (Base c in Layer.Report.Dictionary.AllObjects)
      {
        DataSourceBase ds = c as DataSourceBase;
        if (ds != null && ds.Enabled)
        {
          cbxDataSource.Items.Add(ds.Alias);
          if (Layer.DataSource == ds)
            cbxDataSource.SelectedIndex = cbxDataSource.Items.Count - 1;
        }
      }

      tbFilter.Text = Layer.Filter;
      cbxSpatialColumn.Items.Clear();
      cbxSpatialColumn.Items.AddRange(Layer.SpatialColumns.ToArray());
      if (String.IsNullOrEmpty(Layer.SpatialColumn))
      {
          if (Layer.SpatialColumns.Count > 0)
          {
              if (Layer.SpatialColumns.Contains("NAME"))
                  Layer.SpatialColumn = "NAME";
              else
                  Layer.SpatialColumn = Layer.SpatialColumns[0];
          }
      }
      cbxSpatialColumn.SelectedItem = Layer.SpatialColumn;
      tbSpatialValue.Text = Layer.SpatialValue;
      tbLatitudeValue.Text = Layer.LatitudeValue;
      tbLongitudeValue.Text = Layer.LongitudeValue;
      tbLabelValue.Text = Layer.LabelValue;
      tbAnalyticalValue.Text = Layer.AnalyticalValue;
      cbxFunction.SelectedIndex = (int)Layer.Function;
      tbZoomPolygon.Text = Layer.ZoomPolygon;
      panShpLayer.Visible = Layer.SpatialSource == SpatialSource.ShpFile;
      panAppDataLayer.Visible = Layer.SpatialSource == SpatialSource.ApplicationData;
      lblZoomPolygon.Visible = lblZoomPolygonValue.Visible = tbZoomPolygon.Visible = Layer.Type == LayerType.Polygon;
      #endregion

      #region Appearance tab
      cbVisible.Checked = Layer.Visible;
      cbxBorderColor.Color = Layer.DefaultShapeStyle.BorderColor;
      cbxBorderStyle.SelectedIndex = (int)Layer.DefaultShapeStyle.BorderStyle;
      udBorderWidth.Value = (decimal)Layer.DefaultShapeStyle.BorderWidth;
      cbxFillColor.Color = Layer.DefaultShapeStyle.FillColor;
      cbxPalette.SelectedIndex = (int)Layer.Palette;
      #endregion

      #region Color ranges tab
      cbShowInColorScale.Checked = Layer.ColorRanges.ShowInColorScale;
      cbxStartColor.Color = Layer.ColorRanges.StartColor;
      cbxMiddleColor.Color = Layer.ColorRanges.MiddleColor;
      cbxEndColor.Color = Layer.ColorRanges.EndColor;
      udNumberOfRanges.Value = Layer.ColorRanges.RangeCount;
      UpdateColorRanges();
      #endregion

      #region Size ranges tab
      udStartSize.Value = (decimal)Layer.SizeRanges.StartSize;
      udEndSize.Value = (decimal)Layer.SizeRanges.EndSize;
      udSizeRanges.Value = Layer.SizeRanges.RangeCount;
      UpdateSizeRanges();
      #endregion

      #region Labels tab
      rbNone.Checked = Layer.LabelKind == MapLabelKind.None;
      rbName.Checked = Layer.LabelKind == MapLabelKind.Name;
      rbValue.Checked = Layer.LabelKind == MapLabelKind.Value;
      rbNameAndValue.Checked = Layer.LabelKind == MapLabelKind.NameAndValue;
      cbxLabelColumn.Items.Clear();
      cbxLabelColumn.Items.AddRange(Layer.SpatialColumns.ToArray());
      if (String.IsNullOrEmpty(Layer.LabelColumn))
        Layer.LabelColumn = Layer.SpatialColumn;
      cbxLabelColumn.SelectedItem = Layer.LabelColumn;
      cbxLabelColumn.Visible = lblLabelColumn.Visible = Layer.SpatialSource == SpatialSource.ShpFile;
      lblVisibleAtZoom.Visible = udVisibleAtZoom.Visible = Layer.SpatialSource == SpatialSource.ApplicationData;
      udVisibleAtZoom.Value = (decimal)Layer.LabelsVisibleAtZoom;
      tbLabelFormat.Text = Layer.LabelFormat;
      tbFont.Text = Converter.ToString(Layer.DefaultShapeStyle.Font);
      cbxTextColor.Color = Layer.DefaultShapeStyle.TextColor;
      #endregion

      FUpdating = false;
    }

    private void UpdateColorRanges()
    {
      crColorRanges.ColorRanges = Layer.ColorRanges;
      lblStart.Visible = lblEnd.Visible = lblColor.Visible = Layer.ColorRanges.RangeCount > 0;
    }

    private void UpdateSizeRanges()
    {
      srSizeRanges.SizeRanges = Layer.SizeRanges;
      lblStart1.Visible = lblEnd1.Visible = lblSize.Visible = Layer.SizeRanges.RangeCount > 0;
    }

    private void Init()
    {
      MyRes res = null;
      MyRes commonRes = new MyRes("Forms,MapEditor,Common");

      #region Data tab
      res = new MyRes("Forms,MapEditor,LayerEditorControl,Data");
      pgData.Text = res.Get("");
      lblDataSource.Text = res.Get("DataSource");
      lblFilter.Text = res.Get("Filter");
      lblSpatialData.Text = res.Get("SpatialData");
      lblSpatialColumn.Text = res.Get("Column");
      lblSpatialValue.Text = res.Get("Value");
      lblLatitudeValue.Text = res.Get("Latitude");
      lblLongitudeValue.Text = res.Get("Longitude");
      lblLabelValue.Text = res.Get("Label");
      lblAnalyticalData.Text = res.Get("AnalyticalData");
      lblAnalyticalValue.Text = res.Get("Value");
      lblFunction.Text = res.Get("Function");
      lblZoomPolygon.Text = res.Get("ZoomPolygon");
      lblZoomPolygonValue.Text = res.Get("Value");
      tbSpatialValue.Image = Res.GetImage(52);
      tbAnalyticalValue.Image = Res.GetImage(52);
      tbFilter.Image = Res.GetImage(52);
      tbLatitudeValue.Image = Res.GetImage(52);
      tbLongitudeValue.Image = Res.GetImage(52);
      tbLabelValue.Image = Res.GetImage(52);
      tbZoomPolygon.Image = Res.GetImage(52);
      res = new MyRes("Forms,TotalEditor");
      cbxFunction.Items.Clear();
      cbxFunction.Items.AddRange(new object[] { 
        res.Get("Sum"), res.Get("Min"), res.Get("Max"), res.Get("Avg"), res.Get("Count") });
      #endregion

      #region Appearance tab
      res = new MyRes("Forms,MapEditor,LayerEditorControl,Appearance");
      pgAppearance.Text = res.Get("");
      cbVisible.Text = commonRes.Get("Visible");
      lblBorderColor.Text = commonRes.Get("BorderColor");
      lblBorderStyle.Text = commonRes.Get("BorderStyle");
      lblBorderWidth.Text = commonRes.Get("BorderWidth");
      lblFillColor.Text = commonRes.Get("FillColor");
      lblPalette.Text = res.Get("Palette");
      cbxBorderStyle.Items.Clear();
      cbxBorderStyle.Items.AddRange(Enum.GetNames(typeof(DashStyle)));
      cbxPalette.Items.Clear();
      cbxPalette.Items.AddRange(Enum.GetNames(typeof(MapPalette)));
      #endregion

      #region Color ranges tab
      res = new MyRes("Forms,MapEditor,LayerEditorControl,ColorScale");
      pgColorRanges.Text = res.Get("");
      cbShowInColorScale.Text = commonRes.Get("Visible");
      lblStartColor.Text = res.Get("StartColor");
      lblMiddleColor.Text = res.Get("MiddleColor");
      lblEndColor.Text = res.Get("EndColor");
      lblNumberOfRanges.Text = commonRes.Get("NumberOfRanges");
      lblStart.Text = commonRes.Get("Start");
      lblEnd.Text = commonRes.Get("End");
      lblColor.Text = res.Get("Color");
      crColorRanges = new ColorRangeEditorControl();
      crColorRanges.Location = new Point(lblStart.Left, lblStart.Bottom + 10);
      crColorRanges.Size = new Size(pgColorRanges.Width - crColorRanges.Left * 2, pgColorRanges.Height - crColorRanges.Top - crColorRanges.Left);
      crColorRanges.Parent = pgColorRanges;
      crColorRanges.Changed += new EventHandler(OnChange);
      #endregion

      #region Size ranges tab
      res = new MyRes("Forms,MapEditor,LayerEditorControl,SizeRanges");
      pgSizeRanges.Text = res.Get("");
      lblStartSize.Text = res.Get("StartSize");
      lblEndSize.Text = res.Get("EndSize");
      lblSizeRanges.Text = commonRes.Get("NumberOfRanges");
      lblStart1.Text = commonRes.Get("Start");
      lblEnd1.Text = commonRes.Get("End");
      lblSize.Text = res.Get("Size");
      srSizeRanges = new SizeRangeEditorControl();
      srSizeRanges.Location = new Point(lblStart1.Left, lblStart1.Bottom + 10);
      srSizeRanges.Size = new Size(pgSizeRanges.Width - srSizeRanges.Left * 2, pgSizeRanges.Height - srSizeRanges.Top - srSizeRanges.Left);
      srSizeRanges.Parent = pgSizeRanges;
      srSizeRanges.Changed += new EventHandler(OnChange);
      #endregion

      #region Labels tab
      res = new MyRes("Forms,MapEditor,LayerEditorControl,Labels");
      pgLabels.Text = res.Get("");
      lblLabelKind.Text = res.Get("LabelKind");
      rbNone.Text = res.Get("None");
      rbName.Text = res.Get("Name");
      rbValue.Text = res.Get("Value");
      rbNameAndValue.Text = res.Get("NameAndValue");
      lblLabelColumn.Text = res.Get("LabelColumn");
      lblVisibleAtZoom.Text = res.Get("VisibleAtZoom");
      lblLabelFormat.Text = commonRes.Get("Format");
      lblFont.Text = commonRes.Get("Font");
      lblTextColor.Text = commonRes.Get("TextColor");
      tbFont.Image = Res.GetImage(59);
      #endregion
    }
    #endregion // Private Methods

    #region Data tab
    private void cbxDataSource_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      DataSourceBase ds = Layer.Report.GetDataSource((string)cbxDataSource.Items[cbxDataSource.SelectedIndex]);
      Layer.DataSource = ds;
      OnChange();
    }

    private void tbFilter_Leave(object sender, EventArgs e)
    {
      Layer.Filter = tbFilter.Text;
      OnChange();
    }

    private void tbFilter_ButtonClick(object sender, EventArgs e)
    {
      tbFilter.Text = Editors.EditExpression(Layer.Report, tbFilter.Text);
    }

    private void cbxSpatialColumn_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.SpatialColumn = cbxSpatialColumn.SelectedItem == null ? "" : (string)cbxSpatialColumn.SelectedItem;
      OnChange();
    }

    private void tbSpatialValue_Leave(object sender, EventArgs e)
    {
      Layer.SpatialValue = tbSpatialValue.Text;
      OnChange();
    }

    private void tbSpatialValue_ButtonClick(object sender, EventArgs e)
    {
      tbSpatialValue.Text = Editors.EditExpression(Layer.Report, tbSpatialValue.Text);
    }

    private void tbAnalyticalValue_Leave(object sender, EventArgs e)
    {
      Layer.AnalyticalValue = tbAnalyticalValue.Text;
      OnChange();
    }

    private void tbAnalyticalValue_ButtonClick(object sender, EventArgs e)
    {
      tbAnalyticalValue.Text = Editors.EditExpression(Layer.Report, tbAnalyticalValue.Text);
    }

    private void cbxFunction_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.Function = (TotalType)cbxFunction.SelectedIndex;
      OnChange();
    }

    private void tbLatitude_ButtonClick(object sender, EventArgs e)
    {
      tbLatitudeValue.Text = Editors.EditExpression(Layer.Report, tbLatitudeValue.Text);
    }

    private void tbLatitude_Leave(object sender, EventArgs e)
    {
      Layer.LatitudeValue = tbLatitudeValue.Text;
      OnChange();
    }

    private void tbLongitude_ButtonClick(object sender, EventArgs e)
    {
      tbLongitudeValue.Text = Editors.EditExpression(Layer.Report, tbLongitudeValue.Text);
    }

    private void tbLongitude_Leave(object sender, EventArgs e)
    {
      Layer.LongitudeValue = tbLongitudeValue.Text;
      OnChange();
    }

    private void tbLabel_ButtonClick(object sender, EventArgs e)
    {
      tbLabelValue.Text = Editors.EditExpression(Layer.Report, tbLabelValue.Text);
    }

    private void tbLabel_Leave(object sender, EventArgs e)
    {
      Layer.LabelValue = tbLabelValue.Text;
      OnChange();
    }

    private void tbZoomPolygon_ButtonClick(object sender, EventArgs e)
    {
      tbZoomPolygon.Text = Editors.EditExpression(Layer.Report, tbZoomPolygon.Text);
    }

    private void tbZoomPolygon_Leave(object sender, EventArgs e)
    {
      Layer.ZoomPolygon = tbZoomPolygon.Text;
      OnChange();
    }
    #endregion

    #region Appearance tab
    private void cbVisible_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.Visible = cbVisible.Checked;
      OnChange();
    }

    private void cbxBorderColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.DefaultShapeStyle.BorderColor = cbxBorderColor.Color;
      OnChange();
    }

    private void cbxBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.DefaultShapeStyle.BorderStyle = (DashStyle)cbxBorderStyle.SelectedIndex;
      OnChange();
    }

    private void udBorderWidth_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.DefaultShapeStyle.BorderWidth = (float)udBorderWidth.Value;
      OnChange();
    }

    private void cbxFillColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.DefaultShapeStyle.FillColor = cbxFillColor.Color;
      OnChange();
    }

    private void cbxPalette_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.Palette = (MapPalette)cbxPalette.SelectedIndex;
      OnChange();
    }
    #endregion

    #region Color ranges tab
    private void cbShowInColorScale_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.ColorRanges.ShowInColorScale = cbShowInColorScale.Checked;
      OnChange();
    }

    private void cbxStartColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.ColorRanges.StartColor = cbxStartColor.Color;
      OnChange();
    }

    private void cbxMiddleColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.ColorRanges.MiddleColor = cbxMiddleColor.Color;
      OnChange();
    }

    private void cbxEndColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.ColorRanges.EndColor = cbxEndColor.Color;
      OnChange();
    }

    private void udNumberOfRanges_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.ColorRanges.RangeCount = (int)udNumberOfRanges.Value;
      UpdateColorRanges();
      OnChange();
    }
    #endregion

    #region Size ranges tab
    private void udStartSize_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.SizeRanges.StartSize = (float)udStartSize.Value;
      OnChange();
    }

    private void udEndSize_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.SizeRanges.EndSize = (float)udEndSize.Value;
      OnChange();
    }

    private void udSizeRanges_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.SizeRanges.RangeCount = (int)udSizeRanges.Value;
      UpdateSizeRanges();
      OnChange();
    }
    #endregion

    #region Labels tab
    private void rbNone_CheckedChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      if (rbNone.Checked)
        Layer.LabelKind = MapLabelKind.None;
      else if (rbName.Checked)
        Layer.LabelKind = MapLabelKind.Name;
      else if (rbValue.Checked)
        Layer.LabelKind = MapLabelKind.Value;
      else if (rbNameAndValue.Checked)
        Layer.LabelKind = MapLabelKind.NameAndValue;
      OnChange();
    }

    private void cbxLabelColumn_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.LabelColumn = cbxLabelColumn.SelectedItem == null ? "" : (string)cbxLabelColumn.SelectedItem;
      OnChange();
    }

    private void udVisibleAtZoom_ValueChanged(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.LabelsVisibleAtZoom = (float)udVisibleAtZoom.Value;
      OnChange();
    }

    private void tbLabelFormat_Leave(object sender, EventArgs e)
    {
      Layer.LabelFormat = tbLabelFormat.Text;
      OnChange();
    }

    private void tbFont_ButtonClick(object sender, EventArgs e)
    {
      using (FontDialog dialog = new FontDialog())
      {
        dialog.Font = Layer.DefaultShapeStyle.Font;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          Layer.DefaultShapeStyle.Font = dialog.Font;
          tbFont.Text = Converter.ToString(dialog.Font);
          OnChange();
        }
      }
    }

    private void cbxTextColor_ColorSelected(object sender, EventArgs e)
    {
      if (FUpdating)
        return;
      Layer.DefaultShapeStyle.TextColor = cbxTextColor.Color;
      OnChange();
    }
    #endregion

    public LayerEditorControl()
    {
      InitializeComponent();
      Init();
    }
  }
}
