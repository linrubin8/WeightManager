using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FastReport.Data.ConnectionEditors
{
  /// <summary>
  /// The base class for all connection editors. This control is used when you edit
  /// the connection in the Data Wizard.
  /// </summary>
  [ToolboxItem(false)]
  public partial class ConnectionEditorBase : UserControl
  {
    /// <summary>
    /// Gets or sets a connection string.
    /// </summary>
    public string ConnectionString
    {
      get { return GetConnectionString(); }
      set { SetConnectionString(value); }
    }

    /// <summary>
    /// This method should construct the connection string from values entered by user.
    /// </summary>
    /// <returns>The connection string.</returns>
    protected virtual string GetConnectionString()
    {
      return "";
    }

    /// <summary>
    /// This method should parse the connection string and fill the user interface elements.
    /// </summary>
    /// <param name="value">The connection string.</param>
    protected virtual void SetConnectionString(string value)
    {
    }
    
    /// <summary>
    /// This method is called when form layout is complete.
    /// </summary>
    public virtual void UpdateLayout()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectionEditorBase"/> class with default settings.
    /// </summary>
    public ConnectionEditorBase()
    {
      InitializeComponent();
    }
  }
}
