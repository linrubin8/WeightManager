using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Design.ToolWindows;

namespace FastReport.Design
{
  /// <summary>
  /// Represents list of registered design plugins.
  /// </summary>
  public static class DesignerPlugins
  {
    internal static List<Type> Plugins = new List<Type>();

    /// <summary>
    /// Adds a new plugin's type.
    /// </summary>
    /// <param name="plugin">The type of a plugin.</param>
    public static void Add(Type plugin)
    {
      if (Plugins.IndexOf(plugin) == -1)
        Plugins.Add(plugin);
    }
  }
}
