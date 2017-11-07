using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Controls
{
  internal class ColorSelector : UserControl
  {
    private TabStrip FTabs;
    private List<ColorTabBase> FColorTabs;
    private Panel pnTransparency;
    private Label lblTransparency;
    private TrackBar trbTransparency;
    private Color FColor;
    
    public event EventHandler ColorSelected;
    
    public Color Color
    {
      get { return FColor; }
      set 
      {
        if (value.A == 0)
          value = Color.Transparent;
        FColor = value;
        if (FColorTabs[1].SelectColor(value))
          FTabs.SelectedTabIndex = 1;
        else if (FColorTabs[2].SelectColor(value))
          FTabs.SelectedTabIndex = 2;
        else
        {
          FColorTabs[0].SelectColor(value);
          FTabs.SelectedTabIndex = 0;
        }  
        if (value.A == 0)
          trbTransparency.Value = 0;
        else
          trbTransparency.Value = 255 - value.A;
      }
    }

    private void FTabs_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
    {
      Control c = FColorTabs[FTabs.SelectedTabIndex];
      foreach (ColorTabBase tab in FColorTabs)
      {
        tab.Visible = c == tab;
      }
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      foreach (ColorTabBase c in FColorTabs)
      {
        c.Location = new Point(0, FTabs.Height);
        c.Size = new Size(Width, Height - FTabs.Height - 23);
      }
      pnTransparency.Location = new Point(0, Height - 23);
      pnTransparency.Size = new Size(Width, 23);
      lblTransparency.Location = new Point(4, 4);
      trbTransparency.Location = new Point(lblTransparency.Width + 4, 4);
      trbTransparency.Size = new Size(Width - trbTransparency.Left - 3, 19);
    }
    
    public void SelectColor(Color color)
    {
      int transparency = trbTransparency.Value;
      if (transparency == 255)
        FColor = Color.Transparent;
      else if (transparency == 0)
        FColor = color;
      else  
        FColor = Color.FromArgb(255 - transparency, color);
      if (ColorSelected != null)
        ColorSelected(this, EventArgs.Empty);
    }

    public void Localize()
    {
      MyRes res = new MyRes("Designer,ColorSelector");
      
      FTabs.Tabs[0].Text = res.Get("CustomColors");
      FTabs.Tabs[1].Text = res.Get("WebColors");
      FTabs.Tabs[2].Text = res.Get("SystemColors");
      lblTransparency.Text = res.Get("Transparency");
    }
    
    public void SetStyle(UIStyle style)
    {
      eTabStripStyle st = UIStyleUtils.GetTabStripStyle1(style);
      if (st == eTabStripStyle.VS2005Dock)
        st = eTabStripStyle.VS2005;
      FTabs.Style = st;
      Color color = UIStyleUtils.GetControlColor(style);
      pnTransparency.BackColor = color;
      BackColor = color;
      FTabs.BackColor = color;
    }
    
    public ColorSelector()
    {
      FColorTabs = new List<ColorTabBase>();
      FColorTabs.AddRange(new ColorTabBase[] {
        new CustomColorsTab(this), new WebColorsTab(this), new SystemColorsTab(this) });
      
      FTabs = new TabStrip();
      FTabs.CanReorderTabs = false;
      FTabs.Tabs.Add(new TabItem());
      FTabs.Tabs.Add(new TabItem());
      FTabs.Tabs.Add(new TabItem());
      FTabs.SelectedTabChanged += new TabStrip.SelectedTabChangedEventHandler(FTabs_SelectedTabChanged);
      FTabs.Height = 25;
      FTabs.Dock = DockStyle.Top;
      FTabs.TabAlignment = eTabStripAlignment.Top;
      FTabs.ShowFocusRectangle = false;

      lblTransparency = new Label();
      lblTransparency.AutoSize = true;
      
      trbTransparency = new TrackBar();
      trbTransparency.Minimum = 0;
      trbTransparency.Maximum = 255;
      trbTransparency.TickStyle = TickStyle.None;
      trbTransparency.AutoSize = false;

      pnTransparency = new Panel();
      pnTransparency.Controls.AddRange(new Control[] { lblTransparency, trbTransparency });

      Controls.AddRange(new Control[] { FTabs, pnTransparency });
      foreach (ColorTabBase c in FColorTabs)
      {
        Controls.Add(c);
      }

      Localize();
      Size = new Size(197, 275);
      SetStyle(Config.UIStyle);
    }
  }
  
  internal class ColorTabBase : UserControl
  {
    public virtual bool SelectColor(Color color)
    {
      return false;
    }
  }
  
  internal class CustomColorsTab : ColorTabBase
  {
    private ColorSelector FSelector;
    private int FHighlight;
    private int size = 24;
    private int gap = 5;
    private static Color[] ColorTable = new Color[] {
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 192, 192),
        Color.FromArgb(255, 224, 192),
        Color.FromArgb(255, 255, 192),
        Color.FromArgb(192, 255, 192),
        Color.FromArgb(192, 255, 255),
        Color.FromArgb(192, 192, 255),
        Color.FromArgb(255, 192, 255),
        Color.FromArgb(224, 224, 224),
        Color.FromArgb(255, 128, 128),
        Color.FromArgb(255, 192, 128),
        Color.FromArgb(255, 255, 128),
        Color.FromArgb(128, 255, 128),
        Color.FromArgb(128, 255, 255),
        Color.FromArgb(128, 128, 255),
        Color.FromArgb(255, 128, 255),
        Color.FromArgb(192, 192, 192),
        Color.FromArgb(255, 0, 0),
        Color.FromArgb(255, 128, 0),
        Color.FromArgb(255, 255, 0),
        Color.FromArgb(0, 255, 0),
        Color.FromArgb(0, 255, 255),
        Color.FromArgb(0, 0, 255),
        Color.FromArgb(255, 0, 255),
        Color.FromArgb(128, 128, 128),
        Color.FromArgb(192, 0, 0),
        Color.FromArgb(192, 64, 0),
        Color.FromArgb(192, 192, 0),
        Color.FromArgb(0, 192, 0),
        Color.FromArgb(0, 192, 192),
        Color.FromArgb(0, 0, 192),
        Color.FromArgb(192, 0, 192),
        Color.FromArgb(64, 64, 64),
        Color.FromArgb(128, 0, 0),
        Color.FromArgb(128, 64, 0),
        Color.FromArgb(128, 128, 0),
        Color.FromArgb(0, 128, 0),
        Color.FromArgb(0, 128, 128),
        Color.FromArgb(0, 0, 128),
        Color.FromArgb(128, 0, 128),
        Color.FromArgb(0, 0, 0),
        Color.FromArgb(64, 0, 0),
        Color.FromArgb(128, 64, 64),
        Color.FromArgb(64, 64, 0),
        Color.FromArgb(0, 64, 0),
        Color.FromArgb(0, 64, 64),
        Color.FromArgb(0, 0, 64),
        Color.FromArgb(64, 0, 64),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255),
        Color.FromArgb(255, 255, 255) };

    private void ChooseOtherColor()
    {
      ColorDialog c = new ColorDialog();
      c.FullOpen = true;
      c.Color = FSelector.Color;
      if (c.ShowDialog() == DialogResult.OK)
      {
        int findIndex = -1;
        for (int i = 48; i < 64; i++)
        {
          if (ColorTable[i] == Color.FromArgb(255, 255, 255))
          {
            findIndex = i;
            break;
          }
        }
        if (findIndex == -1)
        {
          for (int i = 49; i < 64; i++)
          {
            ColorTable[i - 1] = ColorTable[i];
          }
          findIndex = 63;
        }
        ColorTable[findIndex] = c.Color;
        Refresh();
        FSelector.SelectColor(c.Color);
      }
    }
    
    private void DrawHighlight(Graphics g, Rectangle rect)
    {
      rect.Inflate(3, 3);
      Brush brush = new SolidBrush(Color.FromArgb(193, 210, 238));
      g.FillRectangle(brush, rect);
      Pen pen = new Pen(Color.FromArgb(49, 106, 197));
      g.DrawRectangle(pen, rect);
      brush.Dispose();
      pen.Dispose();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      Rectangle rect;
      
      for (int x = 0; x < 8; x++)
      {
        for (int y = 0; y < 8; y++)
        {
          int index = x + y * 8;
          Brush b = new SolidBrush(ColorTable[index]);
          rect = new Rectangle(x * size + gap, y * size + gap, size - gap - 1, size - gap - 1);
          if (FHighlight == index)
            DrawHighlight(g, rect);
          g.FillRectangle(b, rect);
          g.DrawRectangle(SystemPens.ControlDark, rect);
          b.Dispose();
        }
      }
      
      rect = new Rectangle(gap, 8 * size + gap, 8 * size - gap - 1, 24);
      if (FHighlight == 64)
        DrawHighlight(g, rect);
      g.FillRectangle(SystemBrushes.ButtonFace, rect);
      g.DrawRectangle(SystemPens.ControlDark, rect);
      TextRenderer.DrawText(g, Res.Get("Designer,ColorSelector,Other"), Font, rect, SystemColors.WindowText,
        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      int x = (e.X - gap) / size;
      int y = (e.Y - gap) / size;
      FHighlight = -1;
      if (x >= 0 && x < 8 && y >= 0 && y < 8)
        FHighlight = x + y * 8;
      else if (x >= 0 && x < 8 && y == 8)
        FHighlight = 64;
      Refresh();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (FHighlight == 64)
        ChooseOtherColor();
      else if (FHighlight != -1)
        FSelector.SelectColor(ColorTable[FHighlight]);
    }

    public override bool SelectColor(Color color)
    {
      for (int i = 0; i < ColorTable.Length; i++)
      {
        Color c = ColorTable[i];
        if (c.R == color.R && c.G == color.G && c.B == color.B)
        {
          FHighlight = i;
          return true;
        }  
      }
      return false;
    }
    
    public CustomColorsTab(ColorSelector selector) : base()
    {
      FSelector = selector;
      SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      BackColor = SystemColors.Window;
      FHighlight = -1;
    }
  }

  internal class KnownColorTab : ColorTabBase
  {
    protected ColorSelector FSelector;
    protected ListBox lbxColors;
    protected Color[] ColorTable;

    private void lbxColors_DrawItem(object sender, DrawItemEventArgs e)
    {
      e.DrawBackground();
      Graphics g = e.Graphics;
      if (e.Index >= 0)
      {
        Color c = ColorTable[e.Index];
        Brush b = new SolidBrush(c);
        Rectangle rect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, 21, e.Bounds.Height - 4);
        g.FillRectangle(b, rect);
        g.DrawRectangle(SystemPens.ControlDark, rect);
        b.Dispose();
        TextRenderer.DrawText(g, c.Name, e.Font, new Point(e.Bounds.X + 26, e.Bounds.Y), e.ForeColor);
      }
    }

    private void lbxColors_Click(object sender, EventArgs e)
    {
      FSelector.SelectColor(ColorTable[lbxColors.SelectedIndex]);
    }

    protected void PopulateList()
    {
      for (int i = 0; i < ColorTable.Length; i++)
      {
        lbxColors.Items.Add(i);
        if (ColorTable[i] == FSelector.Color)
          lbxColors.SelectedIndex = i;
      }  
    }

    public override bool SelectColor(Color color)
    {
      for (int i = 0; i < ColorTable.Length; i++)
      {
        Color c = ColorTable[i];
        if (c == color || (c.A != 0 && c.R == color.R && c.G == color.G && c.B == color.B))
        {
          lbxColors.SelectedIndex = i;
          return true;
        }  
      }
      return false;
    }

    public KnownColorTab(ColorSelector selector) : base()
    {
      FSelector = selector;
      Padding = new System.Windows.Forms.Padding(1);
      BackColor = SystemColors.Window;
      lbxColors = new ListBox();
      lbxColors.ItemHeight = DrawUtils.DefaultItemHeight;
      lbxColors.IntegralHeight = false;
      lbxColors.Dock = DockStyle.Fill;
      lbxColors.BorderStyle = BorderStyle.None;
      lbxColors.DrawMode = DrawMode.OwnerDrawFixed;
      lbxColors.DrawItem += new DrawItemEventHandler(lbxColors_DrawItem);
      lbxColors.Click += new EventHandler(lbxColors_Click);
      Controls.Add(lbxColors);
    }
  }

  internal class WebColorsTab : KnownColorTab
  {
    public WebColorsTab(ColorSelector selector) : base(selector)
    {
      ColorTable = new Color[] {
        Color.Transparent,
        Color.Black,
        Color.DimGray,
        Color.Gray,
        Color.DarkGray,
        Color.Silver,
        Color.LightGray,
        Color.Gainsboro,
        Color.WhiteSmoke,
        Color.White,
        Color.RosyBrown,
        Color.IndianRed,
        Color.Brown,
        Color.Firebrick,
        Color.LightCoral,
        Color.Maroon,
        Color.DarkRed,
        Color.Red,
        Color.Snow,
        Color.MistyRose,
        Color.Salmon,
        Color.Tomato,
        Color.DarkSalmon,
        Color.Coral,
        Color.OrangeRed,
        Color.LightSalmon,
        Color.Sienna,
        Color.SeaShell,
        Color.Chocolate,
        Color.SaddleBrown,
        Color.SandyBrown,
        Color.PeachPuff,
        Color.Peru,
        Color.Linen,
        Color.Bisque,
        Color.DarkOrange,
        Color.BurlyWood,
        Color.Tan,
        Color.AntiqueWhite,
        Color.NavajoWhite,
        Color.BlanchedAlmond,
        Color.PapayaWhip,
        Color.Moccasin,
        Color.Orange,
        Color.Wheat,
        Color.OldLace,
        Color.FloralWhite,
        Color.DarkGoldenrod,
        Color.Goldenrod,
        Color.Cornsilk,
        Color.Gold,
        Color.Khaki,
        Color.LemonChiffon,
        Color.PaleGoldenrod,
        Color.DarkKhaki,
        Color.Beige,
        Color.LightGoldenrodYellow,
        Color.Olive,
        Color.Yellow,
        Color.LightYellow,
        Color.Ivory,
        Color.OliveDrab,
        Color.YellowGreen,
        Color.DarkOliveGreen,
        Color.GreenYellow,
        Color.Chartreuse,
        Color.LawnGreen,
        Color.DarkSeaGreen,
        Color.ForestGreen,
        Color.LimeGreen,
        Color.LightGreen,
        Color.PaleGreen,
        Color.DarkGreen,
        Color.Green,
        Color.Lime,
        Color.Honeydew,
        Color.SeaGreen,
        Color.MediumSeaGreen,
        Color.SpringGreen,
        Color.MintCream,
        Color.MediumSpringGreen,
        Color.MediumAquamarine,
        Color.Aquamarine,
        Color.Turquoise,
        Color.LightSeaGreen,
        Color.MediumTurquoise,
        Color.DarkSlateGray,
        Color.PaleTurquoise,
        Color.Teal,
        Color.DarkCyan,
        Color.Aqua,
        Color.Cyan,
        Color.LightCyan,
        Color.Azure,
        Color.DarkTurquoise,
        Color.CadetBlue,
        Color.PowderBlue,
        Color.LightBlue,
        Color.DeepSkyBlue,
        Color.SkyBlue,
        Color.LightSkyBlue,
        Color.SteelBlue,
        Color.AliceBlue,
        Color.DodgerBlue,
        Color.SlateGray,
        Color.LightSlateGray,
        Color.LightSteelBlue,
        Color.CornflowerBlue,
        Color.RoyalBlue,
        Color.MidnightBlue,
        Color.Lavender,
        Color.Navy,
        Color.DarkBlue,
        Color.MediumBlue,
        Color.Blue,
        Color.GhostWhite,
        Color.SlateBlue,
        Color.DarkSlateBlue,
        Color.MediumSlateBlue,
        Color.MediumPurple,
        Color.BlueViolet,
        Color.Indigo,
        Color.DarkOrchid,
        Color.DarkViolet,
        Color.MediumOrchid,
        Color.Thistle,
        Color.Plum,
        Color.Violet,
        Color.Purple,
        Color.DarkMagenta,
        Color.Magenta,
        Color.Fuchsia,
        Color.Orchid,
        Color.MediumVioletRed,
        Color.DeepPink,
        Color.HotPink,
        Color.LavenderBlush,
        Color.PaleVioletRed,
        Color.Crimson,
        Color.Pink,
        Color.LightPink };
      PopulateList();  
    }
  }
  
  internal class SystemColorsTab : KnownColorTab
  {
    public SystemColorsTab(ColorSelector selector) : base(selector)
    {
      ColorTable = new Color[] {
        SystemColors.ActiveBorder,
        SystemColors.ActiveCaption,
        SystemColors.ActiveCaptionText,
        SystemColors.AppWorkspace,
        SystemColors.Control,
        SystemColors.ControlDark,
        SystemColors.ControlDarkDark,
        SystemColors.ControlLight,
        SystemColors.ControlLightLight,
        SystemColors.ControlText,
        SystemColors.Desktop,
        SystemColors.GrayText,
        SystemColors.Highlight,
        SystemColors.HighlightText,
        SystemColors.HotTrack,
        SystemColors.InactiveBorder,
        SystemColors.InactiveCaption,
        SystemColors.InactiveCaptionText,
        SystemColors.Info,
        SystemColors.InfoText,
        SystemColors.Menu,
        SystemColors.MenuText,
        SystemColors.ScrollBar,
        SystemColors.Window,
        SystemColors.WindowFrame,
        SystemColors.WindowText };
      PopulateList();
    }
  }
}
