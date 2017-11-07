using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Map;
using FastReport.Utils;
using FastReport.Forms;
using System.Drawing;
using System.Reflection;

namespace FastReport.Map.Forms
{
  internal partial class MapEditorForm : BaseDialogForm
  {
    #region Fields
    private MapObject FMap;
    private MapObject FOriginalMap;
    private MapEditorControl FMapEditor;
    private LayerEditorControl FLayerEditor;
    #endregion // Fields

    #region Properties

    public MapObject Map
    {
      get { return FMap; }
      set
      {
        FOriginalMap = value;
        FMap = new MapObject();
        FMap.AssignAll(FOriginalMap, true);
        FMap.SetReport(FOriginalMap.Report);
        FMap.SetDesigning(true);
        foreach (MapLayer layer in FMap.Layers)
        {
          if (!layer.IsShapefileEmbedded)
            layer.LoadShapefile(layer.Shapefile);
        }
        PopulateMapTree(FMap);
      }
    }

    #endregion // Properties

    #region Private Methods
    private void Init()
    {
      btnUp.Image = Res.GetImage(208);
      btnDown.Image = Res.GetImage(209);
      tvMap.ImageList = Res.GetImages();
      pnSample.GetType().GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(pnSample, new object[] { ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true });
    }

    private void PopulateMapTree(object select)
    {
      tvMap.Nodes.Clear();

      TreeNode mapNode = tvMap.Nodes.Add(Res.Get("Forms,MapEditor,Map"));
      mapNode.Tag = FMap;
      mapNode.ImageIndex = 153;
      mapNode.SelectedImageIndex = mapNode.ImageIndex;

      for (int i = 0; i < FMap.Layers.Count; i++)
      {
        MapLayer layer = FMap.Layers[i];
        TreeNode layerNode = mapNode.Nodes.Add(Res.Get("Forms,MapEditor,Layer") + " " + (i + 1).ToString());
        layerNode.Tag = layer;
        layerNode.ImageIndex = 169;
        layerNode.SelectedImageIndex = layerNode.ImageIndex;
        if (select == layer)
          tvMap.SelectedNode = layerNode;
      }

      mapNode.Expand();
      if (select == FMap)
        tvMap.SelectedNode = mapNode;
    }

    private void ShowProperties(object selected)
    {
      if (selected is MapObject)
      {
        if (FMapEditor == null)
        {
          FMapEditor = new MapEditorControl();
          FMapEditor.Location = new Point(pcMap.Right + pcMap.Left, pcMap.Top);
          FMapEditor.Parent = this;
          FMapEditor.Map = FMap;
          FMapEditor.Changed += new EventHandler(RefreshSample);
        }
        if (FLayerEditor != null)
          FLayerEditor.Hide();
        FMapEditor.Show();
      }
      else if (selected is MapLayer)
      {
        if (FLayerEditor == null)
        {
          FLayerEditor = new LayerEditorControl();
          FLayerEditor.Location = new Point(pcMap.Right + pcMap.Left, pcMap.Top);
          FLayerEditor.Parent = this;
          FLayerEditor.Changed += new EventHandler(RefreshSample);
        }
        if (FMapEditor != null)
          FMapEditor.Hide();
        FLayerEditor.Layer = selected as MapLayer;
        FLayerEditor.Show();
      }
    }

    private void RefreshSample()
    {
      pnSample.Refresh();
    }

    private void RefreshSample(object sender, EventArgs e)
    {
      RefreshSample();
    }

    #endregion // Private Methods

    #region Internal Methods

    internal void EnableMercatorProtection(bool enable)
    {
        FMapEditor.EnableMercatorProtection(enable);
    }

    #endregion // Internal Methods

    #region Public Methods

    public override void Localize()
    {
      base.Localize();
      MyRes res = new MyRes("Forms,MapEditor");
      Text = res.Get("");
      btnAdd.Text = res.Get("Add");
      btnDelete.Text = res.Get("Delete");
      lblHint.Text = res.Get("Hint");
    }

    #endregion // Public Methods

    #region Events Handlers
    private void MapEditorForm_Shown(object sender, EventArgs e)
    {
      tvMap.Focus();
    }

    private void MapEditorForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (DialogResult == DialogResult.OK)
      {
        FOriginalMap.AssignAll(Map, true);
        foreach (MapLayer layer in FOriginalMap.Layers)
        {
          if (!layer.IsShapefileEmbedded)
            layer.LoadShapefile(layer.Shapefile);
        }
        FOriginalMap.CreateUniqueNames();
      }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      using (AddLayerForm form = new AddLayerForm())
      {
        form.Map = FMap;
        form.Tag = this;
        if (form.ShowDialog() == DialogResult.OK)
        {
            if (FMap.Layers.Count > 0)
            {
                PopulateMapTree(FMap.Layers[FMap.Layers.Count - 1]);
                RefreshSample();
            }
        }
      }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      MapLayer layer = tvMap.SelectedNode.Tag as MapLayer;
      FMap.Layers.Remove(layer);
      if (FMap.Layers.Count == 0)
      {
          EnableMercatorProtection(true);
      }
      if (FMap.Layers.Count > 0)
      {
          PopulateMapTree(FMap.Layers[FMap.Layers.Count - 1]);
      }
      else
      {
          PopulateMapTree(FMap);
      }
      RefreshSample();
    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      MapLayer layer = tvMap.SelectedNode.Tag as MapLayer;
      int index = FMap.Layers.IndexOf(layer);
      FMap.Layers.RemoveAt(index);
      FMap.Layers.Insert(index - 1, layer);
      PopulateMapTree(layer);
      RefreshSample();
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      MapLayer layer = tvMap.SelectedNode.Tag as MapLayer;
      int index = FMap.Layers.IndexOf(layer);
      FMap.Layers.RemoveAt(index);
      FMap.Layers.Insert(index + 1, layer);
      PopulateMapTree(layer);
      RefreshSample();
    }

    private void tvMap_AfterSelect(object sender, TreeViewEventArgs e)
    {
      object selected = tvMap.SelectedNode.Tag;
      btnDelete.Enabled = selected is MapLayer;
      btnUp.Enabled = selected is MapLayer && tvMap.SelectedNode.Index > 0;
      btnDown.Enabled = selected is MapLayer && tvMap.SelectedNode.Index < FMap.Layers.Count - 1;
      ShowProperties(selected);
    }

    private void pnSample_Paint(object sender, PaintEventArgs e)
    {
      RectangleF saveBounds = FMap.Bounds;
      
      try
      {
        if (FMap.Layers.Count > 0)
        {
          FMap.Bounds = new RectangleF(1, 1, pnSample.Width - 2, pnSample.Height - 2);
          FMap.Draw(new FRPaintEventArgs(e.Graphics, 1, 1, FMap.Report.GraphicCache));
        }
      }
      catch (Exception ex)
      {
        using (StringFormat sf = new StringFormat())
        {
          sf.Alignment = StringAlignment.Center;
          sf.LineAlignment = StringAlignment.Center;
          e.Graphics.DrawString(ex.Message, Font, Brushes.Red, pnSample.DisplayRectangle, sf);
        }
      }
      
      FMap.Bounds = saveBounds;
    }

    #endregion // Events Handlers

    public MapEditorForm()
    {
      InitializeComponent();
      Init();
      Localize();
    }
  }
}
