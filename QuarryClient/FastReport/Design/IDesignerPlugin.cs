using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Forms;
using FastReport.Utils;

namespace FastReport.Design
{
  /// <summary>
  /// Provides functionality required for report designer plugins such as toolbars and toolwindows.
  /// </summary>
  public interface IDesignerPlugin
  {
    /// <summary>
    /// Gets the plugin name.
    /// </summary>
    string PluginName
    {
      get;
    }
    
    /// <summary>
    /// Saves the plugin state.
    /// </summary>
    /// <example>This example shows how to save the state:
    /// <code>
    /// public void SaveState()
    /// {
    ///   XmlItem xi = Config.Root.FindItem("Designer").FindItem(Name);
    ///   xi.SetProp("ShowGrid", DialogWorkspace.ShowGrid ? "1" : "0");
    /// }
    /// </code>
    /// </example>
    void SaveState();
    
    /// <summary>
    /// Restores the plugin state.
    /// </summary>
    /// <example>This example shows how to restore the state:
    /// <code>
    /// public void RestoreState()
    /// {
    ///   XmlItem xi = Config.Root.FindItem("Designer").FindItem(Name);
    ///   DialogWorkspace.ShowGrid = xi.GetProp("ShowGrid") != "0";
    /// }
    /// </code>
    /// </example>
    void RestoreState();
    
    /// <summary>
    /// Updates plugin state when current selection was changed.
    /// </summary>
    /// <remarks>
    /// Typically you need to do the same work in the <see cref="SelectionChanged"/> and
    /// <see cref="UpdateContent"/> methods.
    /// </remarks>
    void SelectionChanged();
    
    /// <summary>
    /// Updates plugin state when the report was modified.
    /// </summary>
    /// <remarks>
    /// Typically you need to do the same work in the <see cref="SelectionChanged"/> and
    /// <see cref="UpdateContent"/> methods.
    /// </remarks>
    void UpdateContent();
    
    /// <summary>
    /// Locks the plugin.
    /// </summary>
    /// <remarks>
    /// This method is called by the designer when report is loading. It may be needed to disable 
    /// some operations (like painting) that use the report.
    /// </remarks>
    void Lock();
    
    /// <summary>
    /// Unlocks the plugin.
    /// </summary>
    /// This method is called by the designer when report is loaded. It follows the <b>Lock</b>
    /// method call and must reset the lock.
    void Unlock();
    
    /// <summary>
    /// Localizes the plugin.
    /// </summary>
    /// <remarks>
    /// This method is called by the designer when current localization is changed.
    /// </remarks>
    void Localize();
    
    /// <summary>
    /// Gets an options page that will be used in the Designer Options dialog to edit the plugin options.
    /// </summary>
    /// <returns>The options page, if implemented; otherwise, <b>null</b>.</returns>
    DesignerOptionsPage GetOptionsPage();
    
    /// <summary>
    /// Updates UI style of the plugin.
    /// </summary>
    /// <remarks>
    /// The plugin should update its style according to the designer's <b>UIStyle</b> property.
    /// </remarks>
    void UpdateUIStyle();
  }
}
