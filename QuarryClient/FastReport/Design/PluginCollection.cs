using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FastReport.Design
{
  /// <summary>
  /// Represents collection of designer plugins.
  /// </summary>
  /// <remarks>
  /// <para>This class is used in the <b>Designer.Plugins</b> property.</para>
  /// <para>To register own plugin, add its type to the <see cref="DesignerPlugins"/> global collection:
  /// <code>
  /// DesignerPlugins.Add(typeof(MyToolbar));
  /// </code>
  /// </para>
  /// </remarks>
  public class PluginCollection : CollectionBase
  {
    private Designer FDesigner;
    
    internal IDesignerPlugin this[int index]
    {
      get { return List[index] as IDesignerPlugin; }
    }

    internal IDesignerPlugin Add(Type plugin)
    {
      foreach (IDesignerPlugin p in this)
      {
        if (p.GetType() == plugin)
          return p;
      }
      IDesignerPlugin newPlugin = Activator.CreateInstance(plugin, new object[] { FDesigner }) as IDesignerPlugin;
      Add(newPlugin);
      return newPlugin;
    }

    internal void Add(IDesignerPlugin plugin)
    {
      int i = List.IndexOf(plugin);
      if (i == -1)
        List.Add(plugin);
    }

    internal void AddRange(Type[] range)
    {
      if (range == null)
        return;
      foreach (Type t in range)
      {
        Add(t);
      }
    }

    internal void AddRange(IDesignerPlugin[] range)
    {
      if (range == null)
        return;
      foreach (IDesignerPlugin plugin in range)
      {
        Add(plugin);
      }
    }

    internal void Remove(Type plugin)
    {
      for (int i = 0; i < Count; i++)
      {
        IDesignerPlugin p = this[i];
        if (p.GetType() == plugin)
        {
          Remove(p);
          return;
        }  
      }
    }

    internal void Remove(IDesignerPlugin plugin)
    {
      List.Remove(plugin);
      plugin.SaveState();
      if (plugin is IDisposable)
        (plugin as IDisposable).Dispose();
    }

    internal void RemoveRange(Type[] range)
    {
      if (range == null)
        return;
      foreach (Type t in range)
      {
        Remove(t);
      }
    }

    internal void SelectionChanged(object sender)
    {
      foreach (IDesignerPlugin plugin in this)
      {
        if (plugin != sender)
          plugin.SelectionChanged();
      }
    }

    internal void Lock()
    {
      foreach (IDesignerPlugin plugin in this)
      {
        plugin.Lock();
      }
    }

    internal void Unlock()
    {
      foreach (IDesignerPlugin plugin in this)
      {
        plugin.Unlock();
      }
    }

    internal void Update(object sender)
    {
      foreach (IDesignerPlugin plugin in this)
      {
        if (plugin != sender)
          plugin.UpdateContent();
      }
    }

    internal void Localize()
    {
      foreach (IDesignerPlugin plugin in this)
      {
        plugin.Localize();
      }
    }

    internal void UpdateUIStyle()
    {
      foreach (IDesignerPlugin plugin in this)
      {
        plugin.UpdateUIStyle();
      }
    }

    /// <summary>
    /// Finds a plugin by its name.
    /// </summary>
    /// <param name="pluginName">The plugin's name.</param>
    /// <returns>The plugin, if found; otherwise, <b>null</b>.</returns>
    /// <example>This example shows how to find a plugin.
    /// <code>
    /// MessagesWindow window = designer.Plugins.Find("MessagesWindow") as MessagesWindow;
    /// </code>
    /// </example>
    public IDesignerPlugin Find(string pluginName)
    {
      foreach (IDesignerPlugin plugin in this)
      {
        if (plugin.PluginName == pluginName)
          return plugin;
      }
      return null;
    }

    /// <summary>
    /// Finds a plugin by its type name.
    /// </summary>
    /// <param name="typeName">The plugin's type name.</param>
    /// <returns>The plugin, if found; otherwise, <b>null</b>.</returns>
    /// <example>This example shows how to find a plugin.
    /// <code>
    /// MessagesWindow window = designer.Plugins.FindType("MessagesWindow") as MessagesWindow;
    /// </code>
    /// </example>
    public IDesignerPlugin FindType(string typeName)
    {
      foreach (IDesignerPlugin plugin in this)
      {
        if (plugin.GetType().ToString() == typeName)
          return plugin;
      }
      return null;
    }

    internal void SaveState()
    {
      foreach (IDesignerPlugin plugin in this)
      {
        plugin.SaveState();
      }
    }

    internal void RestoreState()
    {
      foreach (IDesignerPlugin plugin in this)
      {
        plugin.RestoreState();
      }
    }

    internal PluginCollection(Designer designer)
      : base()
    {
      FDesigner = designer;
    }
  }
}
