using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Utils;
using FastReport.DevComponents.DotNetBar.Controls;

namespace FastReport.Design.ToolWindows
{
  /// <summary>
  /// Represents the "Messages" window.
  /// </summary>
  /// <remarks>
  /// To get this window, use the following code:
  /// <code>
  /// Designer designer;
  /// MessagesWindow window = designer.Plugins.FindType("MessagesWindow") as MessagesWindow;
  /// </code>
  /// </remarks>
  public class MessagesWindow : ToolWindowBase
  {
    private ListViewEx FList;

    private void FList_DoubleClick(object sender, EventArgs e)
    {
      ListViewItem item = FList.SelectedItems[0];
      if (item.SubItems.Count > 1)
      {
        Designer.ActiveReportTab.SwitchToCode();
        Designer.ActiveReport.CodeHelper.Locate((int)item.SubItems[1].Tag, (int)item.SubItems[2].Tag);
      }
      else
      {
        Base obj = Designer.ActiveReport.FindObject((string)item.Tag);
        if (obj != null)
        {
          Designer.SelectedObjects.Clear();
          Designer.SelectedObjects.Add(obj);
          Designer.SelectionChanged(null);
        }
      }
    }

    /// <inheritdoc/>
    public override void Localize()
    {
      MyRes res = new MyRes("Designer,ToolWindow,Messages");
      Text = res.Get("");
      
      FList.Columns.Clear();
      FList.Columns.Add(res.Get("Description"));
      FList.Columns.Add(res.Get("Line"));
      FList.Columns.Add(res.Get("Column"));
      FList.Columns[0].Width = 600;
    }

    /// <summary>
    /// Clears the message list.
    /// </summary>
    public void ClearMessages()
    {
      FList.Items.Clear();
    }
    
    /// <summary>
    /// Adds a new message.
    /// </summary>
    /// <param name="description">The message text.</param>
    /// <param name="objName">The name of object related to a message.</param>
    public void AddMessage(string description, string objName)
    {
      ListViewItem item = new ListViewItem();
      item.Text = description;
      item.Tag = objName;
      FList.Items.Add(item);
    }

    /// <summary>
    /// Adds a new script-related message.
    /// </summary>
    /// <param name="description">The message text.</param>
    /// <param name="line">The line of the script.</param>
    /// <param name="column">The column of the script.</param>
    public void AddMessage(string description, int line, int column)
    {
      ListViewItem item = new ListViewItem();
      item.Text = description;
      ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
      if (line != -1)
        subItem.Text = line.ToString();
      subItem.Tag = line;
      item.SubItems.Add(subItem);
      subItem = new ListViewItem.ListViewSubItem();
      if (column != -1)
        subItem.Text = column.ToString();
      subItem.Tag = column;
      item.SubItems.Add(subItem);
      FList.Items.Add(item);
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="MessagesWindow"/> class with default settings.
    /// </summary>
    /// <param name="designer">The report designer.</param>
    public MessagesWindow(Designer designer) : base(designer)
    {
      Name = "MessagesWindow";
      Image = Res.GetImage(70);

      FList = new ListViewEx();
      FList.Dock = DockStyle.Fill;
      FList.BorderStyle = BorderStyle.None;
      FList.FullRowSelect = true;
      FList.View = View.Details;
      FList.HideSelection = false;
      FList.DoubleClick += new EventHandler(FList_DoubleClick);
      
      ParentControl.Controls.Add(FList);
      Localize();
    }
  }
}
