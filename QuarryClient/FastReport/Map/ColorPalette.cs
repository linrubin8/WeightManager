using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Globalization;
using FastReport.TypeConverters;
using FastReport.Utils;

namespace FastReport.Map
{

  /// <summary>
  /// Defines the palette used to display map shapes.
  /// </summary>
  public enum MapPalette
  {
    /// <summary>
    /// No palette used. 
    /// </summary>
    None,

    /// <summary>
    /// Light palette.
    /// </summary>
    Light,
    
    /// <summary>
    /// Pastel palette.
    /// </summary>
    Pastel,
    
    /// <summary>
    /// Grayscale palette.
    /// </summary>
    Grayscale,

    /// <summary>
    /// Earth tones palette.
    /// </summary>
    Earth,
    
    /// <summary>
    /// Sea green palette.
    /// </summary>
    Sea,

    /// <summary>
    /// Bright pastel palette.
    /// </summary>
    BrightPastel
  }

  internal class ColorPalette
  {
    private static Color[] lightPalette = { 
      Color.FromArgb(230, 230, 250), 
      Color.FromArgb(255, 240, 245), 
      Color.FromArgb(255, 218, 185), 
      Color.FromArgb(255, 250, 205), 
      Color.FromArgb(255, 228, 225), 
      Color.FromArgb(240, 255, 240), 
      Color.FromArgb(240, 248, 255), 
      Color.FromArgb(245, 245, 245) };

    private static Color[] pastelPalette = { 
      Color.FromArgb(191, 224, 137), 
      Color.FromArgb(200, 189, 183), 
      Color.FromArgb(255, 250, 156), 
      Color.FromArgb(244, 197, 138), 
      Color.FromArgb(135, 206, 226), 
      Color.FromArgb(230, 160, 150), 
      Color.FromArgb(160, 160, 220), 
      Color.FromArgb(255, 149, 149) };

    private static Color[] grayscalePalette = { 
      Color.FromArgb(200, 200, 200), 
      Color.FromArgb(188, 188, 188), 
      Color.FromArgb(178, 178, 178), 
      Color.FromArgb(168, 168, 168), 
      Color.FromArgb(157, 157, 157), 
      Color.FromArgb(147, 147, 147), 
      Color.FromArgb(136, 136, 136), 
      Color.FromArgb(125, 125, 125) };

    private static Color[] earthPalette = { 
      Color.FromArgb(255, 128, 0), 
      Color.FromArgb(184, 134, 11), 
      Color.FromArgb(192, 64, 0), 
      Color.FromArgb(107, 142, 35), 
      Color.FromArgb(205, 133, 63), 
      Color.FromArgb(192, 192, 0), 
      Color.FromArgb(34, 139, 34), 
      Color.FromArgb(210, 105, 30) };

    private static Color[] seaPalette = { 
      Color.FromArgb(46, 139, 87), 
      Color.FromArgb(102, 205, 170), 
      Color.FromArgb(70, 130, 180), 
      Color.FromArgb(0, 139, 139), 
      Color.FromArgb(95, 158, 160), 
      Color.FromArgb(60, 179, 113), 
      Color.FromArgb(72, 209, 204), 
      Color.FromArgb(143, 188, 139) };

    private static Color[] brightPastelPalette = { 
      Color.FromArgb(65, 140, 240), 
      Color.FromArgb(252, 180, 65), 
      Color.FromArgb(224, 64, 10), 
      Color.FromArgb(5, 100, 146), 
      Color.FromArgb(241, 185, 168), 
      Color.FromArgb(80, 99, 129), 
      Color.FromArgb(255, 227, 130), 
      Color.FromArgb(18, 156, 221) };

    public static Color GetColor(int index, MapPalette palette)
    {
      switch (palette)
      {
        case MapPalette.Light:
          return lightPalette[index % lightPalette.Length];
        case MapPalette.Pastel:
          return pastelPalette[index % pastelPalette.Length];
        case MapPalette.Grayscale:
          return grayscalePalette[index % grayscalePalette.Length];
        case MapPalette.Earth:
          return earthPalette[index % earthPalette.Length];
        case MapPalette.Sea:
          return seaPalette[index % seaPalette.Length];
        case MapPalette.BrightPastel:
          return brightPastelPalette[index % brightPastelPalette.Length];
      }
      return Color.Transparent;
    }
  }
}
