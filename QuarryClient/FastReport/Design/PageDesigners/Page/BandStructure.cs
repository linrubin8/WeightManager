using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.Forms;
using System.Drawing.Drawing2D;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.PageDesigners.Page
{
  internal class BandStructure : Control
  {
    private BandStructureNode FRoot;
    private ReportPageDesigner FPageDesigner;
    private int FOffset;
    private const int WM_PAINT = 0x000F;

    public Button btnConfigure;
    public bool AllowPaint = true;

    public ReportPage Page
    {
      get { return FPageDesigner.Page as ReportPage; }
    }

    public Designer Designer
    {
      get { return FPageDesigner.Designer; }
    }

    public int Offset
    {
      get { return FOffset; }
      set { FOffset = value; }
    }

    private void btnConfigure_Click(object sender, EventArgs e)
    {
      using (ConfigureBandsForm form = new ConfigureBandsForm(Designer))
      {
        form.Page = Page;
        form.ShowDialog();
      }
    }

    private void AddBand(BandBase band, BandStructureNode root)
    {
      if (band == null)
        return;
      BandStructureNode node = root.Add();
      node.AddBand(band);
    }

    private void EnumDataBand(DataBand band, BandStructureNode root)
    {
      if (band == null)
        return;
      BandStructureNode node = root.Add();

      node.AddBand(band.Header);

      node.AddBand(band);
      foreach (BandBase b in band.Bands)
      {
        EnumBand(b, node);
      }

      node.AddBand(band.Footer);
    }

    private void EnumGroupHeaderBand(GroupHeaderBand band, BandStructureNode root)
    {
      if (band == null)
        return;
      BandStructureNode node = root.Add();

      node.AddBand(band.Header);
      node.AddBand(band);
      EnumGroupHeaderBand(band.NestedGroup, node);
      EnumDataBand(band.Data, node);
      node.AddBand(band.GroupFooter);
      node.AddBand(band.Footer);
    }

    private void EnumBand(BandBase band, BandStructureNode root)
    {
      if (band is DataBand)
        EnumDataBand(band as DataBand, root);
      else if (band is GroupHeaderBand)
        EnumGroupHeaderBand(band as GroupHeaderBand, root);
    }

    private void EnumPageBands()
    {
      if (Page == null)
        return;

      if (Page.TitleBeforeHeader)
      {
        AddBand(Page.ReportTitle, FRoot);
        AddBand(Page.PageHeader, FRoot);
      }
      else
      {
        AddBand(Page.PageHeader, FRoot);
        AddBand(Page.ReportTitle, FRoot);
      }
      AddBand(Page.ColumnHeader, FRoot);
      foreach (BandBase b in Page.Bands)
      {
        EnumBand(b, FRoot);
      }
      AddBand(Page.ColumnFooter, FRoot);
      AddBand(Page.ReportSummary, FRoot);
      AddBand(Page.PageFooter, FRoot);
      AddBand(Page.Overlay, FRoot);
    }

    private void EnumNodes(List<BandStructureNode> list, BandStructureNode root)
    {
      if (root != FRoot)
        list.Add(root);
      foreach (BandStructureNode node in root.ChildNodes)
      {
        EnumNodes(list, node);
      }
    }

    private void DrawItems(Graphics g)
    {
      List<BandStructureNode> list = new List<BandStructureNode>();
      EnumNodes(list, FRoot);

      foreach (BandStructureNode node in list)
      {
        int offs = 24 + Offset - 2;

        RectangleF fillRect = new RectangleF(node.Left, node.Top + offs, Width - node.Left - 1, node.Height);
        BandBase bandFill = null;

        foreach (BandBase band in node.Bands)
        {
          bandFill = band;
          if (band is GroupHeaderBand || band is DataBand)
            break;
        }

        // fill node area
        bandFill.DrawBandHeader(g, fillRect, false);
        g.DrawRectangle(SystemPens.ControlDark, node.Left, node.Top + offs, Width - node.Left - 1, node.Height);

        // draw band title
        float scale = ReportWorkspace.Scale;
        using (StringFormat sf = new StringFormat())
        {
          foreach (BandBase band in node.Bands)
          {
            g.DrawLine(SystemPens.ControlDark, node.Left + 8, band.Top * scale + offs, Width, band.Top * scale + offs);

            RectangleF textRect = new RectangleF(node.Left + 4, band.Top * scale + offs + 4,
              Width - (node.Left + 4) - 4, band.Height * scale - 4);
            ObjectInfo info = RegisteredObjects.FindObject(band);
            string text = Res.Get(info.Text);
            if (band.GetInfoText() != "")
              text += ": " + band.GetInfoText();

            float textHeight = DrawUtils.MeasureString(g, text, DrawUtils.Default96Font, textRect, sf).Height;
            TextFormatFlags flags = textHeight > textRect.Height ?
              TextFormatFlags.WordBreak : TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
            TextRenderer.DrawText(g, text, DrawUtils.Default96Font,
              new Rectangle((int)textRect.Left, (int)textRect.Top, (int)textRect.Width, (int)textRect.Height),
              SystemColors.WindowText, flags);

            if (band.IsAncestor)
              g.DrawImage(Res.GetImage(99), (int)(node.Left + Width - 10), (int)(band.Top * scale + offs + 3));
          }
        }
      }
    }

    private bool PointInRect(Point point, Rectangle rect)
    {
      return point.X >= rect.Left && point.X <= rect.Right && point.Y >= rect.Top && point.Y <= rect.Bottom;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (FPageDesigner.Locked)
        return;
      FRoot.Clear();
      EnumPageBands();
      DrawItems(e.Graphics);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (FPageDesigner.Locked)
        return;

      List<BandStructureNode> list = new List<BandStructureNode>();
      EnumNodes(list, FRoot);

      float scale = ReportWorkspace.Scale;
      int offs = 24 + Offset;
      foreach (BandStructureNode node in list)
      {
        foreach (BandBase band in node.Bands)
        {
          if (PointInRect(e.Location, new Rectangle((int)node.Left, (int)(band.Top * scale) + offs,
            Width - (int)node.Left, (int)(band.Height * scale))))
          {
            Designer.SelectedObjects.Clear();
            Designer.SelectedObjects.Add(band);
            Designer.SelectionChanged(null);
            break;
          }
        }
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (FPageDesigner.Locked)
        return;

      if (e.Button == MouseButtons.Right && Designer.SelectedObjects.Count > 0
        && Designer.SelectedObjects[0] is BandBase)
      {
        ContextMenuBar menu = Designer.SelectedObjects[0].GetContextMenu();
        if (menu != null)
        {
          PopupItem item = menu.Items[0] as PopupItem;
          item.PopupMenu(PointToScreen(e.Location));
        }
      }
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      if (Designer.SelectedObjects.Count == 1 && Designer.SelectedObjects[0] is BandBase)
      {
        BandBase band = Designer.SelectedObjects[0] as BandBase;
        if (!band.HasRestriction(Restrictions.DontEdit))
          band.HandleDoubleClick();
      }
    }

    protected override void WndProc(ref Message m)
    {
        if ((m.Msg != WM_PAINT) || (AllowPaint && m.Msg == WM_PAINT))
        {
            base.WndProc(ref m);
        }
    }

    public void Localize()
    {
      btnConfigure.Text = Res.Get("Designer,Workspace,Report,ConfigureBands");
    }

    public BandStructure(ReportPageDesigner pd)
    {
      FPageDesigner = pd;
      FRoot = new BandStructureNode();

      btnConfigure = new Button();
      btnConfigure.Height = 24;
      btnConfigure.Dock = DockStyle.Top;
      btnConfigure.FlatStyle = FlatStyle.Flat;
      btnConfigure.FlatAppearance.BorderColor = SystemColors.ButtonFace;
      btnConfigure.FlatAppearance.BorderSize = 0;
      btnConfigure.Font = DrawUtils.Default96Font;
      btnConfigure.TextAlign = ContentAlignment.MiddleLeft;
      btnConfigure.Cursor = Cursors.Hand;
      btnConfigure.Click += new EventHandler(btnConfigure_Click);
      Controls.Add(btnConfigure);
      Localize();

      SetStyle(ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.DoubleBuffer, true);
      SetStyle(ControlStyles.ResizeRedraw, true);
    }


    private class BandStructureNode
    {
      public float Left;
      public float Top;
      public float Height;
      public List<BandBase> Bands;
      public List<BandStructureNode> ChildNodes;
      public BandStructureNode Parent;

      public void AdjustHeight(float height)
      {
        Height += height;
        if (Parent != null)
          Parent.AdjustHeight(height);
      }

      public BandStructureNode Add()
      {
        BandStructureNode result = new BandStructureNode();
        ChildNodes.Add(result);
        result.Parent = this;
        if (Left == -1)
          result.Left = 7;
        else
          result.Left = Left + 8;
        return result;
      }

      public void AddBand(BandBase band)
      {
        if (band != null)
        {
          if (band.Child != null && band.Child.FillUnusedSpace)
          {
            AddBand(band.Child);
            AddBandInternal(band);
          }
          else
          {
            AddBandInternal(band);
            AddBand(band.Child);
          }
        }
      }

      private void AddBandInternal(BandBase band)
      {
        if (band != null)
        {
          Bands.Add(band);
          float scale = ReportWorkspace.Scale;
          if (Bands.Count == 1)
            Top = band.Top * scale;
          float height = band.Height * scale + 4;
          AdjustHeight(height);
        }
      }

      public void Clear()
      {
        Bands.Clear();
        while (ChildNodes.Count > 0)
        {
          ChildNodes[0].Clear();
          ChildNodes.RemoveAt(0);
        }
      }

      public BandStructureNode()
      {
        Bands = new List<BandBase>();
        ChildNodes = new List<BandStructureNode>();
        Left = -9;
      }
    }
  }
}
