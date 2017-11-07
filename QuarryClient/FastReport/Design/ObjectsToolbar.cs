using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design
{
  internal class ObjectsToolbar : Bar, IDesignerPlugin
  {
    #region Fields
    private Designer FDesigner;
    private Report FCurrentReport;
    private PageBase FCurrentPage;
    private ObjectInfo FNowInserting;
    public ButtonItem btnSelect;
    #endregion
    
    #region Properties
    internal Designer Designer
    {
      get { return FDesigner; }
    }

    /// <inheritdoc/>
    public string PluginName
    {
      get { return Name; }
    }
    #endregion

    #region Private Methods
    private void DoCreateButtons(ObjectInfo rootItem, SubItemsCollection items)
    {
      foreach (ObjectInfo item in rootItem.Items)
      {
        if (!item.Enabled)
          continue;

        ButtonItem button = new ButtonItem();
        button.Image = item.Image;
        
        string text = Res.TryGet(item.Text);
        if (items == Items)
        {
          button.Tooltip = text;
          button.FixedSize = new Size(25, 25);
        }  
        else
        {
          button.Text = text;
          button.ButtonStyle = eButtonStyle.ImageAndText;
        }  

        if (item.Items.Count > 0)
        {
          // it's a category
          DoCreateButtons(item, button.SubItems);
          button.PopupSide = ePopupSide.Right;
          button.AutoExpandOnClick = true;
          button.FixedSize = new Size(25, 32);
        }
        else
        {
          button.Tag = item;
          button.Click += button_Click;
        }
        
        items.Add(button);
      }
    }

    private void CreateSelectBtn()
    {
      btnSelect = new ButtonItem();
      btnSelect.Image = Res.GetImage(100);
      btnSelect.Click += btnSelect_Click;
      btnSelect.FixedSize = new Size(25, 25);

      Items.Add(btnSelect);
    }

    private void CreateButtons()
    {
      if (Designer.ActiveReport != null && Designer.ActiveReport == FCurrentReport && 
        Designer.ActiveReportTab.ActivePage == FCurrentPage)
        return;
      FCurrentReport = Designer.ActiveReport;
      if (Designer.ActiveReportTab != null)
        FCurrentPage = Designer.ActiveReportTab.ActivePage;
      else
        FCurrentPage = null;

      // delete all buttons except pointer
      int i = 0;
      while (i < Items.Count)
      {
        if (Items[i] == btnSelect)
          i++;
        else
        {
          Items[i].Dispose();
          Items.RemoveAt(i);
        }  
      }

      if (FCurrentPage == null)
      {
        RecalcLayout();
        return;  
      }  
        
      // create object buttons
      ObjectInfo pageItem = RegisteredObjects.FindObject(FCurrentPage);
      if (pageItem != null)
      {
        DoCreateButtons(pageItem, Items);
      }
      
      RecalcLayout();
    }

    private void button_Click(object sender, EventArgs e)
    {
      Designer.FormatPainter = false;
      if (!Designer.cmdInsert.Enabled)
        return;
        
      ResetButtons();
      if (sender is ButtonItem && (sender as ButtonItem).IsOnBar)
        (sender as ButtonItem).Checked = true;
      FNowInserting = (sender as ButtonItem).Tag as ObjectInfo;
      Designer.InsertObject(FNowInserting, InsertFrom.NewObject);
    }

    private void btnSelect_Click(object sender, EventArgs e)
    {
      Designer.FormatPainter = false;
      if (Designer.ActiveReportTab == null)
        return;
      DoClickSelectButton(true);  
    }
    
    private void DoClickSelectButton(bool ignoreMultiInsert)
    {
      if (!btnSelect.Checked)
      {
        if (FNowInserting != null && FNowInserting.MultiInsert && !ignoreMultiInsert)
        {
          Designer.InsertObject(FNowInserting, InsertFrom.NewObject);
        }
        else
        {
          ResetButtons();
          btnSelect.Checked = true;
          Designer.ActiveReportTab.ActivePageDesigner.CancelPaste();
        }
      }
    }
    
    private void ResetButtons()
    {
      foreach (BaseItem item in Items)
      {
        if (item is ButtonItem)  
          (item as ButtonItem).Checked = false;
      }    
    }
    #endregion

    #region Public Methods
    public void ClickSelectButton(bool ignoreMultiInsert)
    {
      DoClickSelectButton(ignoreMultiInsert);
    }
    #endregion

    #region IDesignerPlugin
    /// <inheritdoc/>
    public void SaveState()
    {
    }

    /// <inheritdoc/>
    public void RestoreState()
    {
    }

    /// <inheritdoc/>
    public void SelectionChanged()
    {
      CreateButtons();
    }

    /// <inheritdoc/>
    public void UpdateContent()
    {
      CreateButtons();
    }

    /// <inheritdoc/>
    public void Lock()
    {
    }

    /// <inheritdoc/>
    public void Unlock()
    {
      UpdateContent();
    }

    /// <inheritdoc/>
    public void Localize()
    {
      MyRes res = new MyRes("Designer,Toolbar,Objects");
      Text = res.Get("");
      btnSelect.Tooltip = res.Get("Select");
    }

    /// <inheritdoc/>
    public DesignerOptionsPage GetOptionsPage()
    {
      return null;
    }

    /// <inheritdoc/>
    public void UpdateUIStyle()
    {
    }
    #endregion

    public ObjectsToolbar(Designer designer) : base()
    {
      FDesigner = designer;
      Name = "ObjectsToolbar";
      Font = DrawUtils.Default96Font;
      Dock = DockStyle.Left;
      DockOrientation = eOrientation.Vertical;
      RoundCorners = false;

      CreateSelectBtn();
      Localize();
      Parent = Designer.DotNetBarManager.ToolbarLeftDockSite;
    }
  }

}
