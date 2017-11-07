using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.Dialog;

namespace FastReport.Design.PageDesigners.Dialog
{
  internal class DialogPageDesigner : PageDesignerBase
  {
    #region Fields
    private DialogWorkspace FWorkspace;
    #endregion

    #region Properties
    public DialogPage DialogPage
    {
      get { return Page as DialogPage; }
    }
    #endregion
    
    #region Private Methods
    private void UpdateName()
    {
        if (Page != null)//need refactor
        {
            if (Page.Name == "")
                Text = Page.ClassName + (Page.ZOrder + 1).ToString();
            else
                Text = Page.Name;
        }
    }
    #endregion
    
    #region Public Methods
    public override void PageActivated()
    {
      base.PageActivated();

      // avoid "bad form owner" bug
      Form designerForm = Designer.FindForm();
      if (designerForm == null || !designerForm.Visible)
        return;

      Size clientSize = DialogPage.Form.ClientSize;
      DialogPage.Form.StartPosition = FormStartPosition.Manual;
      DialogPage.Form.Location = new Point(-10000, -10000);
      DialogPage.Form.ClientSize = clientSize;
      DialogPage.Form.Show();
      Focus();
    }

    public override void PageDeactivated()
    {
      base.PageDeactivated();
      DialogPage.Form.Hide();
      DialogPage.Form.Owner = null;
    }

    public override void Paste(ObjectCollection list, InsertFrom source)
    {
      FWorkspace.Paste(list, source);
    }

    public override void CancelPaste()
    {
      FWorkspace.CancelPaste();
    }

    public override void SelectAll()
    {
      Designer.SelectedObjects.Clear();
      foreach (Base c in Page.AllObjects)
      {
        Designer.SelectedObjects.Add(c);
      }
      if (Designer.SelectedObjects.Count == 0)
        Designer.SelectedObjects.Add(Page);
      Designer.SelectionChanged(null);
    }
    #endregion

    #region IDesignerPlugin
    public override void SaveState()
    {
      XmlItem xi = Config.Root.FindItem("Designer").FindItem(Name);
      xi.SetProp("ShowGrid", DialogWorkspace.ShowGrid ? "1" : "0");
      xi.SetProp("SnapToGrid", DialogWorkspace.SnapToGrid ? "1" : "0");
      xi.SetProp("SnapSize", DialogWorkspace.Grid.SnapSize.ToString());
    }

    public override void RestoreState()
    {
      XmlItem xi = Config.Root.FindItem("Designer").FindItem(Name);
      DialogWorkspace.ShowGrid = xi.GetProp("ShowGrid") == "1";
      DialogWorkspace.SnapToGrid = xi.GetProp("SnapToGrid") != "0";
      string size = xi.GetProp("SnapSize");
      if (size == "")
        size = "4";
      DialogWorkspace.Grid.SnapSize = int.Parse(size);
    }

    public override void SelectionChanged()
    {
      base.SelectionChanged();
      UpdateContent();
    }

    public override void UpdateContent()
    {
      if (Locked)
        return;
      base.UpdateContent();
      UpdateName();
      FWorkspace.Refresh();
    }

    public override DesignerOptionsPage GetOptionsPage()
    {
      return new DialogPageOptions(this);
    }
    #endregion

    public DialogPageDesigner(Designer designer) : base(designer)
    {
      Name = "Dialog";
      
      FWorkspace = new DialogWorkspace(this);
      Controls.Add(FWorkspace);
      
      BackColor = SystemColors.Window;
      AutoScroll = true;
    }
  }
}
