using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using FastReport.Utils;
using FastReport.Forms;
using FastReport.Dialog;
using FastReport.Design.ToolWindows;
using FastReport.Data;
using FastReport.DevComponents.DotNetBar;

namespace FastReport.Design.PageDesigners.Dialog
{
  internal class DialogWorkspace : UserControl
  {
    #region Fields
    private PageDesignerBase FPageDesigner;
    private Designer FDesigner;
    private WorkspaceMode1 FMode1;
    private WorkspaceMode2 FMode2;
    private bool FMouseDown;
    private bool FMouseMoved;
    private PointF FLastMousePoint;
    private RectangleF FSelectionRect;
    private FRMouseEventArgs FEventArgs;
    private System.Windows.Forms.ToolTip FToolTip;
    private Guides FGuides;
    private InsertFrom FInsertionSource;
    #endregion

    #region Properties
    public static Grid Grid = new Grid();
    public static bool ShowGrid;
    public static bool SnapToGrid;

    public Designer Designer
    {
      get { return FDesigner; }
    }

    public DialogPage Page
    {
      get { return FPageDesigner.Page as DialogPage; }
    }

    public bool Locked
    {
      get { return FPageDesigner.Locked; }
    }
    
    public Report Report
    {
      get { return Page.Report; }
    }

    public Point Offset
    {
      get
      {
        Form form = Page.Form;
        Point offset = new Point(0, 0);
        offset = form.PointToScreen(offset);
        offset.X -= form.Left - 10;
        offset.Y -= form.Top - 10;
        return offset;
      }
    }
    
    private GraphicCache GraphicCache
    {
      get { return Report.GraphicCache; }
    }
    #endregion

    #region Private Methods
    private void UpdateName()
    {
      if (Page.Name == "")
        Text = Page.ClassName + (Page.ZOrder + 1).ToString();
      else
        Text = Page.Name;
    }

    private void DrawSelectionRect(Graphics g)
    {
      RectangleF rect = NormalizeSelectionRect();
      Brush b = GraphicCache.GetBrush(Color.FromArgb(80, SystemColors.Highlight));
      g.FillRectangle(b, rect);
      Pen pen = GraphicCache.GetPen(SystemColors.Highlight, 1, DashStyle.Dash);
      g.DrawRectangle(pen, rect.Left, rect.Top, rect.Width, rect.Height);
    }

    private RectangleF NormalizeSelectionRect()
    {
      RectangleF result = FSelectionRect;
      if (FSelectionRect.Left > FSelectionRect.Right)
      {
        result.X = FSelectionRect.Right;
        result.Width = -FSelectionRect.Width;
      }
      if (FSelectionRect.Top > FSelectionRect.Bottom)
      {
        result.Y = FSelectionRect.Bottom;
        result.Height = -FSelectionRect.Height;
      }
      return result;
    }

    private RectangleF GetSelectedRect()
    {
      RectangleF result = new RectangleF(100000, 100000, -100000, -100000);
      foreach (Base obj in Designer.SelectedObjects)
      {
        if (obj is ComponentBase)
        {
          ComponentBase c = obj as ComponentBase;
          if (c.Left < result.Left)
            result.X = c.Left;
          if (c.Right > result.Right)
            result.Width = c.Right - result.Left;
          if (c.Top < result.Top)
            result.Y = c.Top;
          if (c.Bottom > result.Bottom)
            result.Height = c.Bottom - result.Top;
        }
        else if (obj is DialogPage)
        {
          result = new RectangleF(0, 0, Page.Width, Page.Height);
        }
      }
      return result;
    }

    private bool CheckGridStep()
    {
      bool al = SnapToGrid;
      if (ModifierKeys == Keys.Alt)
        al = !al;

      bool result = true;
      float kx = FEventArgs.Delta.X;
      float ky = FEventArgs.Delta.Y;
      if (al)
      {
        result = kx >= Grid.SnapSize || kx <= -Grid.SnapSize || ky >= Grid.SnapSize || ky <= -Grid.SnapSize;
        if (result)
        {
          kx = (int)(kx / Grid.SnapSize) * Grid.SnapSize;
          ky = (int)(ky / Grid.SnapSize) * Grid.SnapSize;
        }
      }
      else
      {
        result = kx != 0 || ky != 0;
      }
      FEventArgs.Delta.X = kx;
      FEventArgs.Delta.Y = ky;
      return result;
    }

    private void CloneSelectedObjects()
    {
      ObjectCollection list = new ObjectCollection();
      foreach (Base c in Designer.Objects)
      {
        if (Designer.SelectedObjects.IndexOf(c) != -1)
          if (c.HasFlag(Flags.CanCopy))
            list.Add(c);
      }
      Designer.SelectedObjects.Clear();
      foreach (Base c in list)
      {
        Base clone = Activator.CreateInstance(c.GetType()) as Base;
        clone.Assign(c);
        clone.Name = "";
        clone.Parent = c.Parent;
        clone.CreateUniqueName();
        Designer.Objects.Add(clone);
        Designer.SelectedObjects.Add(clone);
      }
    }

    private void InvokeEditor()
    {
      if (Designer.SelectedObjects.Count == 1 && Designer.SelectedObjects[0] is ComponentBase)
        (Designer.SelectedObjects[0] as ComponentBase).HandleDoubleClick();
    }

    private void MoveSelectedObjects(int xDir, int yDir, bool smooth)
    {
      foreach (Base obj in Designer.SelectedObjects)
      {
        if (obj is ComponentBase && !(obj is PageBase))
        {
          ComponentBase c = obj as ComponentBase;
          c.Left += smooth ? xDir : xDir * Grid.SnapSize;
          c.Top += smooth ? yDir : yDir * Grid.SnapSize;
          c.CheckParent(true);
        }
      }
      Refresh();
      Designer.SetModified(this, "Move");
    }

    private void ResizeSelectedObjects(int xDir, int yDir, bool smooth)
    {
      foreach (Base obj in Designer.SelectedObjects)
      {
        if (obj is ComponentBase && !(obj is PageBase))
        {
          ComponentBase c = obj as ComponentBase;
          c.Width += smooth ? xDir : xDir * Grid.SnapSize;
          c.Height += smooth ? yDir : yDir * Grid.SnapSize;
          c.CheckNegativeSize(FEventArgs);
        }
      }
      Refresh();
      Designer.SetModified(this, "Size");
    }

    private void SelectNextObject(bool forward)
    {
      int index = 0;
      if (Designer.SelectedObjects.Count != 0)
        index = Designer.Objects.IndexOf(Designer.SelectedObjects[0]);
      index += forward ? 1 : -1;
      if (index < 0)
        index = Designer.Objects.Count - 1;
      if (index > Designer.Objects.Count - 1)
        index = 0;
      Designer.SelectedObjects.Clear();
      Designer.SelectedObjects.Add(Designer.Objects[index]);
      Designer.SelectionChanged(null);
    }

    private void ShowToolTip(string text, int x, int y)
    {
      if (FToolTip == null)
        FToolTip = new System.Windows.Forms.ToolTip();
      if (FToolTip != null)
        FToolTip.Show(text, this, x, y);
    }

    private void HideToolTip()
    {
      if (FToolTip != null)
      {
        FToolTip.Hide(this);
        FToolTip.Dispose();
        FToolTip = null;
      }
    }

    private void UpdateStatusBar()
    {
      RectangleF selectedRect = GetSelectedRect();
      bool IsPageOrReportSelected = Designer.SelectedObjects.IsPageSelected || Designer.SelectedObjects.IsReportSelected;
      string location = IsPageOrReportSelected ? "" :
        selectedRect.Left.ToString() + "; " + selectedRect.Top.ToString();
      string size = IsPageOrReportSelected ? "" :
        selectedRect.Width.ToString() + "; " + selectedRect.Height.ToString();
      Designer.ShowStatus(location, size, "");
    }

    private void AfterDragDrop(Point location)
    {
      DataFilterBaseControl control = Designer.SelectedObjects[0] as DataFilterBaseControl;

      if (control is TextBoxControl)
      {
        // show menu with different control types
        ContextMenuStrip menu = new ContextMenuStrip();
        menu.Renderer = Config.DesignerSettings.ToolStripRenderer;

        Type[] controlTypes = new Type[] {
        typeof(TextBoxControl), typeof(MaskedTextBoxControl),
        typeof(ComboBoxControl), typeof(CheckedListBoxControl),
        typeof(ListBoxControl), typeof(DataSelectorControl) };

        foreach (Type controlType in controlTypes)
        {
          ObjectInfo info = RegisteredObjects.FindObject(controlType);
          ToolStripMenuItem item = new ToolStripMenuItem(Res.Get(info.Text), Res.GetImage(info.ImageIndex));
          menu.Items.Add(item);
          item.Tag = controlType;
          item.Click += new EventHandler(ControlTypeItem_Click);
        }

        menu.Show(this, new Point((int)control.AbsLeft + Offset.X, (int)control.AbsBottom + Offset.Y));
      }
      else
      {
        // look for another controls of same type in a form that bound to the same DataColumn
        // and setup the FilterOperation
        foreach (Base c in Page.AllObjects)
        {
          if (c != control && c.GetType() == control.GetType() &&
            (c as DataFilterBaseControl).DataColumn == control.DataColumn)
          {
            (c as DataFilterBaseControl).FilterOperation = FilterOperation.GreaterThanOrEqual;
            control.FilterOperation = FilterOperation.LessThanOrEqual;
            Designer.SetModified(this, "Change");
            break;
          }  
        }
      }
    }

    private void ControlTypeItem_Click(object sender, EventArgs e)
    {
      DataFilterBaseControl control = Designer.SelectedObjects[0] as DataFilterBaseControl;
      Type controlType = (sender as ToolStripMenuItem).Tag as Type;
      DataFilterBaseControl newControl = Activator.CreateInstance(controlType) as DataFilterBaseControl;
      
      newControl.Parent = control.Parent;
      newControl.Location = control.Location;
      newControl.DataColumn = control.DataColumn;
      newControl.ReportParameter = control.ReportParameter;
      
      control.Dispose();
      newControl.CreateUniqueName();
      
      Designer.SelectedObjects.Clear();
      Designer.SelectedObjects.Add(newControl);
      Designer.SetModified(this, "Insert");
    }
    #endregion

    #region Protected Methods
    protected override void Dispose(bool disposing)
    {
      CancelPaste();
      base.Dispose(disposing);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (Locked)
        return;
      Size = new Size((int)Page.Width + 20, (int)Page.Height + 20);
      Graphics g = e.Graphics;
      FRPaintEventArgs paintArgs = new FRPaintEventArgs(g, 1, 1, GraphicCache);

      // check if workspace is active (in the mdi mode). 
      ObjectCollection objects = Designer.Objects;
      if (Designer.ActiveReport != Report)
      {
        objects = Page.AllObjects;
        objects.Add(Page);
      }

      // draw form
      Page.SetDesigning(true);
      g.DrawImage(Page.FormBitmap, 10, 10);

      g.TranslateTransform(Offset.X, Offset.Y);
      if (ShowGrid)
        Grid.Draw(e.Graphics, new Rectangle(0, 0, (int)Page.ClientSize.Width, (int)Page.ClientSize.Height));

      // draw objects
      foreach (Base obj in objects)
      {
        if (obj is ComponentBase)
        {
          obj.SetDesigning(true);
          (obj as ComponentBase).Draw(paintArgs);
        }
      }
      // draw selection
      if (!FMouseDown && Designer.ActiveReport == Report)
      {
        foreach (Base obj in Designer.SelectedObjects)
        {
          if (obj is ComponentBase)
            (obj as ComponentBase).DrawSelection(paintArgs);
        }
      }
      FGuides.Draw(g);
      if (FMode2 == WorkspaceMode2.SelectionRect)
        DrawSelectionRect(g);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (Locked)
        return;

      ObjectCollection selectedObjects = new ObjectCollection();
      Designer.SelectedObjects.CopyTo(selectedObjects);

      FMouseDown = true;
      FMouseMoved = false;
      FEventArgs.X = e.X - Offset.X;
      FEventArgs.Y = e.Y - Offset.Y;
      FEventArgs.Button = e.Button;
      FEventArgs.ModifierKeys = ModifierKeys;
      FEventArgs.Handled = false;

      if (FMode2 == WorkspaceMode2.None)
      {
        // find an object under the mouse
        for (int i = Designer.Objects.Count - 1; i >= 0; i--)
        {
          ComponentBase c = Designer.Objects[i] as ComponentBase;
          if (c != null)
          {
            c.HandleMouseDown(FEventArgs);
            if (FEventArgs.Handled)
            {
              FMode2 = FEventArgs.Mode;
              break;
            }
          }
        }
      }
      else if (FEventArgs.ActiveObject != null)
      {
        FEventArgs.ActiveObject.HandleMouseDown(FEventArgs);
      }

      if (!selectedObjects.Equals(Designer.SelectedObjects))
        Designer.SelectionChanged(FPageDesigner);

      FLastMousePoint.X = FEventArgs.X;
      FLastMousePoint.Y = FEventArgs.Y;
      FSelectionRect = new RectangleF(FEventArgs.X, FEventArgs.Y, 0, 0);
      FGuides.CreateVirtualGuides();
      Refresh();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (Locked || Designer.ActiveReport != Report)
        return;

      FEventArgs.X = e.X - Offset.X;
      FEventArgs.Y = e.Y - Offset.Y;
      FEventArgs.Button = FMode1 == WorkspaceMode1.Insert ? MouseButtons.Left : e.Button;
      FEventArgs.Delta = new PointF(FEventArgs.X - FLastMousePoint.X, FEventArgs.Y - FLastMousePoint.Y);

      if (e.Button == MouseButtons.None && FMode1 == WorkspaceMode1.Select)
      {
        Cursor = Cursors.Default;
        FMode2 = WorkspaceMode2.None;
        FEventArgs.Cursor = Cursor;
        FEventArgs.Mode = FMode2;
        FEventArgs.ActiveObject = null;
        FEventArgs.Handled = false;

        // check object's sizing points
        for (int i = Designer.Objects.Count - 1; i >= 0; i--)
        {
          ComponentBase c = Designer.Objects[i] as ComponentBase;
          if (c != null)
          {
            c.HandleMouseMove(FEventArgs);
            if (FEventArgs.Handled)
            {
              Cursor = FEventArgs.Cursor;
              FMode2 = FEventArgs.Mode;
              FEventArgs.ActiveObject = c;
              break;
            }
          }
        }

        // no sizing points found, check hover
        if (!FEventArgs.Handled)
        {
          for (int i = Designer.Objects.Count - 1; i >= 0; i--)
          {
            ComponentBase c = Designer.Objects[i] as ComponentBase;
            if (c != null)
            {
              c.HandleMouseHover(FEventArgs);
              if (FEventArgs.Handled)
              {
                Cursor = FEventArgs.Cursor;
                break;
              }
            }
          }
        }
      }
      else if (FMode2 == WorkspaceMode2.Move || FMode2 == WorkspaceMode2.Size)
      {
        if (!CheckGridStep())
          return;

        // if insert is on and user press the mouse button and move the mouse, resize selected objects
        if (FMode1 == WorkspaceMode1.Insert && e.Button == MouseButtons.Left && !FMouseMoved)
        {
          FMode2 = WorkspaceMode2.Size;
          foreach (Base c in Designer.SelectedObjects)
          {
            if (c is ComponentBase)
            {
              (c as ComponentBase).Width = 0;
              (c as ComponentBase).Height = 0;
            }
          }
          FEventArgs.SizingPoint = SizingPoint.RightBottom;
        }

        // ctrl was pressed, clone selected objects
        if (FMode1 == WorkspaceMode1.Select && ModifierKeys == Keys.Control && !FMouseMoved)
          CloneSelectedObjects();

        FMouseMoved = true;
        FEventArgs.Mode = FMode2;
        FEventArgs.Handled = false;
        // first serve the active object
        if (FEventArgs.ActiveObject != null)
          FEventArgs.ActiveObject.HandleMouseMove(FEventArgs);

        // if active object does not reset the handled flag, serve other objects
        if (!FEventArgs.Handled)
        {
          foreach (Base c in Designer.Objects)
          {
            if (c is ComponentBase)
            {
              if (c != FEventArgs.ActiveObject)
                (c as ComponentBase).HandleMouseMove(FEventArgs);
            }
          }
        }

        FLastMousePoint.X += FEventArgs.Delta.X;
        FLastMousePoint.Y += FEventArgs.Delta.Y;
        FGuides.CheckVirtualGuides();

        // show tooltip with object's location/size
        string s = "";
        RectangleF selectedRect = GetSelectedRect();
        if (FMode2 == WorkspaceMode2.Move)
          s = selectedRect.Left.ToString() + "; " + selectedRect.Top.ToString();
        else if (FMode2 == WorkspaceMode2.Size)
          s = selectedRect.Width.ToString() + "; " + selectedRect.Height.ToString();
        ShowToolTip(s, e.X + 20, e.Y + 20);

        Refresh();
      }
      else if (FMode2 == WorkspaceMode2.SelectionRect)
      {
        FSelectionRect = new RectangleF(FSelectionRect.Left, FSelectionRect.Top,
          FEventArgs.X - FSelectionRect.Left, FEventArgs.Y - FSelectionRect.Top);
        Refresh();
      }
      else if (FMode2 == WorkspaceMode2.Custom)
      {
        if (!CheckGridStep())
          return;

        FMouseMoved = true;
        FEventArgs.Mode = FMode2;
        if (FEventArgs.ActiveObject != null)
          FEventArgs.ActiveObject.HandleMouseMove(FEventArgs);

        FLastMousePoint.X += FEventArgs.Delta.X;
        FLastMousePoint.Y += FEventArgs.Delta.Y;
        Refresh();
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (Locked)
        return;

      ObjectCollection selectedObjects = new ObjectCollection();
      Designer.SelectedObjects.CopyTo(selectedObjects);
      HideToolTip();

      FEventArgs.X = e.X - Offset.X;
      FEventArgs.Y = e.Y - Offset.Y;
      FEventArgs.Button = e.Button;
      FEventArgs.Mode = FMode2;
      FEventArgs.Handled = false;

      if (FMode2 == WorkspaceMode2.Move || FMode2 == WorkspaceMode2.Size)
      {
        // serve all objects
        for (int i = 0; i < Designer.Objects.Count; i++)
        {
          ComponentBase c = Designer.Objects[i] as ComponentBase;
          if (c != null)
            c.HandleMouseUp(FEventArgs);
        }
        // remove objects with zero size
        if (FMode1 == WorkspaceMode1.Insert)
        {
          int i = 0;
          while (i < Designer.SelectedObjects.Count)
          {
            ComponentBase c = Designer.SelectedObjects[i] as ComponentBase;
            if (c != null && c.Width == 0 && c.Height == 0)
            {
              Designer.Objects.Remove(c);
              Designer.SelectedObjects.Remove(c);
              c.Dispose();
              i--;
            }
            i++;
          }
        }
        if (FMode1 != WorkspaceMode1.Select)
        {
          // during OnInsert call current context may be changed
          WorkspaceMode1 saveMode = FMode1;
          FMode1 = WorkspaceMode1.Select;
          ObjectCollection insertedObjects = new ObjectCollection();
          Designer.SelectedObjects.CopyTo(insertedObjects);
          foreach (Base c in insertedObjects)
          {
            c.OnAfterInsert(FInsertionSource);
          }
          FMode1 = saveMode;
        }

        // check if we actually move a mouse after we clicked it
        if (FMouseMoved || FMode1 == WorkspaceMode1.Insert)
          Designer.SetModified(FPageDesigner,
            FMode1 == WorkspaceMode1.Insert ? "Insert" : FMode2 == WorkspaceMode2.Move ? "Move" : "Size");
      }
      else if (FMode2 == WorkspaceMode2.SelectionRect)
      {
        FEventArgs.SelectionRect = NormalizeSelectionRect();
        if (FEventArgs.ActiveObject != null)
          FEventArgs.ActiveObject.HandleMouseUp(FEventArgs);
      }
      else if (FMode2 == WorkspaceMode2.Custom)
      {
        if (FEventArgs.ActiveObject != null)
          FEventArgs.ActiveObject.HandleMouseUp(FEventArgs);
      }

      bool needReset = FMode1 == WorkspaceMode1.Insert;
      if (!selectedObjects.Equals(Designer.SelectedObjects) || needReset)
        Designer.SelectionChanged(FPageDesigner);

      FMouseDown = false;
      FMode1 = WorkspaceMode1.Select;
      FMode2 = WorkspaceMode2.None;
      FGuides.ClearVirtualGuides();
      Refresh();
      if (needReset)
        Designer.ResetObjectsToolbar(false);

      if (e.Button == MouseButtons.Right && Designer.SelectedObjects.Count > 0)
      {
        ContextMenuBar menu = Designer.SelectedObjects[0].GetContextMenu();
        if (menu != null)
        {
          PopupItem item = menu.Items[0] as PopupItem;
          item.PopupMenu(PointToScreen(e.Location));
        }  
      }
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      base.OnMouseDoubleClick(e);
      
      if (e.Button == MouseButtons.Left)
        InvokeEditor();
    }

    protected override bool IsInputKey(Keys keyData)
    {
      return (keyData & Keys.Up) != 0 || (keyData & Keys.Down) != 0 ||
        (keyData & Keys.Left) != 0 || (keyData & Keys.Right) != 0 ||
        (keyData & Keys.Tab) != 0;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      
      int xDir = 0;
      int yDir = 0;
      
      switch (e.KeyCode)
      {
        case Keys.Escape:
          Designer.ResetObjectsToolbar(true);
          CancelPaste();
          Designer.SelectedObjects.Clear();
          Designer.SelectedObjects.Add(Page);
          Designer.SelectionChanged(null);
          break;

        case Keys.Enter:
          InvokeEditor();
          break;

        case Keys.Insert:
          if ((e.Modifiers & Keys.Control) != 0)
            Designer.cmdCopy.Invoke();
          else if ((e.Modifiers & Keys.Shift) != 0)
            Designer.cmdPaste.Invoke();
          break;

        case Keys.Delete:
          if ((e.Modifiers & Keys.Shift) == 0)
            Designer.cmdDelete.Invoke();
          else
            Designer.cmdCut.Invoke();
          break;

        case Keys.Up:
          yDir = -1;
          break;

        case Keys.Down:
          yDir = 1;
          break;

        case Keys.Left:
          xDir = -1;
          break;

        case Keys.Right:
          xDir = 1;
          break;

        case Keys.Tab:
          SelectNextObject((e.Modifiers & Keys.Shift) == 0);
          break;
      }

      if ((e.Modifiers & Keys.Control) != 0)
      {
        switch (e.KeyCode)
        {
          case Keys.C:
            Designer.cmdCopy.Invoke();
            break;

          case Keys.X:
            Designer.cmdCut.Invoke();
            break;

          case Keys.V:
            Designer.cmdPaste.Invoke();
            break;

          case Keys.A:
            Designer.cmdSelectAll.Invoke();
            break;
        }
      }

      if (xDir != 0 || yDir != 0)
      {
        bool smooth = (e.Modifiers & Keys.Control) != 0;
        if ((e.Modifiers & Keys.Shift) != 0)
          ResizeSelectedObjects(xDir, yDir, smooth);
        else
          MoveSelectedObjects(xDir, yDir, smooth);
      }
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);
      if (e.KeyChar != 13 && e.KeyChar != 27)
      {
        PropertiesWindow window = Designer.Plugins.Find("PropertiesWindow") as PropertiesWindow;
        if (window.Visible)
          window.TypeChar(e.KeyChar);
      }
    }

    protected override void OnDragOver(DragEventArgs drgevent)
    {
      base.OnDragOver(drgevent);
      if (!Designer.cmdInsert.Enabled)
      {
        drgevent.Effect = DragDropEffects.None;
        return;
      }

      if (FMode1 != WorkspaceMode1.DragDrop)
      {
        DictionaryWindow.DraggedItem item = DictionaryWindow.DragUtils.GetOne(drgevent);
        if (item == null)
          return;

        Type dataType = null;
        if (item.Obj is Column)
          dataType = (item.Obj as Column).DataType;
        else if (item.Obj is Parameter)
          dataType = (item.Obj as Parameter).DataType;  
        
        // determine type of control to insert
        Type controlType = typeof(NumericUpDownControl);
        if (dataType == typeof(string) || dataType == typeof(char))
          controlType = typeof(TextBoxControl);
        else if (dataType == typeof(DateTime) || dataType == typeof(TimeSpan))
          controlType = typeof(DateTimePickerControl);
        else if (dataType == typeof(bool))
          controlType = typeof(CheckBoxControl);
        else if (dataType == typeof(byte[]))
          controlType = null;

        if (controlType == null)
        {
          drgevent.Effect = DragDropEffects.None;
          return;
        }

        // create label and control
        bool needCreateLabel = controlType != typeof(CheckBoxControl);
        if (needCreateLabel)
        {
          Designer.InsertObject(new ObjectInfo[] { 
            RegisteredObjects.FindObject(controlType),
            RegisteredObjects.FindObject(typeof(LabelControl)) }, InsertFrom.Dictionary);
        }
        else
        {
          Designer.InsertObject(RegisteredObjects.FindObject(controlType), InsertFrom.Dictionary);
        }
        
        // setup control datafiltering
        DataFilterBaseControl control = Designer.SelectedObjects[0] as DataFilterBaseControl;
        control.Top += (16 / Grid.SnapSize) * Grid.SnapSize;
        if (item.Obj is Column)
          control.DataColumn = item.Text;
        else if (item.Obj is Parameter)
          control.ReportParameter = item.Text;

        // setup label text
        string labelText = "";
        if (item.Obj is Column)
          labelText = (item.Obj as Column).Alias;
        else if (item.Obj is Parameter)
        {
          labelText = (item.Obj as Parameter).Description;
          if (String.IsNullOrEmpty(labelText))
            labelText = (item.Obj as Parameter).Name;
        }
        
        if (needCreateLabel)
        {
          LabelControl label = Designer.SelectedObjects[1] as LabelControl;
          label.Text = labelText;
        }
        else
          control.Text = labelText;

        FEventArgs.DragSource = control;
      }

      FMode1 = WorkspaceMode1.DragDrop;
      Point pt = PointToClient(new Point(drgevent.X, drgevent.Y));
      OnMouseMove(new MouseEventArgs(MouseButtons.Left, 0, pt.X, pt.Y, 0));
      drgevent.Effect = drgevent.AllowedEffect;
    }

    protected override void OnDragDrop(DragEventArgs drgevent)
    {
      base.OnDragDrop(drgevent);
      DictionaryWindow.DraggedItem item = DictionaryWindow.DragUtils.GetOne(drgevent);
      if (item == null)
        return;
      Point pt = PointToClient(new Point(drgevent.X, drgevent.Y));
      OnMouseUp(new MouseEventArgs(MouseButtons.Left, 0, pt.X, pt.Y, 0));
      if (FEventArgs.DragSource != null)
        AfterDragDrop(pt);
    }

    protected override void OnDragLeave(EventArgs e)
    {
      base.OnDragLeave(e);
      CancelPaste();
    }
    #endregion

    #region Public Methods
    public void Paste(ObjectCollection list, InsertFrom source)
    {
      FInsertionSource = source;
      // find left-top edge of inserted objects
      float minLeft = 100000;
      float minTop = 100000;
      foreach (Base c in list)
      {
        if (c is ComponentBase)
        {
          ComponentBase c1 = c as ComponentBase;
          if (c1.Left < minLeft)
            minLeft = c1.Left;
          if (c1.Top < minTop)
            minTop = c1.Top;
        }
      }
      foreach (Base c in list)
      {
        // correct the left-top
        if (c is ComponentBase)
        {
          ComponentBase c1 = c as ComponentBase;
          c1.Left -= minLeft + Grid.SnapSize * 1000;
          c1.Top -= minTop + Grid.SnapSize * 1000;
        }
      }
      FMode1 = WorkspaceMode1.Insert;
      FMode2 = WorkspaceMode2.Move;
      FEventArgs.ActiveObject = null;
      OnMouseDown(new MouseEventArgs(MouseButtons.Left, 0,
        Offset.X + 10 - Grid.SnapSize * 1000, Offset.Y + 10 - Grid.SnapSize * 1000, 0));
    }

    public void CancelPaste()
    {
      if (FMode1 == WorkspaceMode1.Insert)
      {
        while (Designer.SelectedObjects.Count > 0)
        {
          Base c = Designer.SelectedObjects[0];
          Designer.SelectedObjects.Remove(c);
          Designer.Objects.Remove(c);
          c.Dispose();
        }
        FMode1 = WorkspaceMode1.Select;
        FMode2 = WorkspaceMode2.None;
        OnMouseUp(new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
      }
    }

    public override void Refresh()
    {
      UpdateStatusBar();
      base.Refresh();
    }

    #endregion

    public DialogWorkspace(PageDesignerBase pageDesigner)
    {
      FPageDesigner = pageDesigner;
      FDesigner = pageDesigner.Designer;

      FGuides = new Guides(pageDesigner);
      FEventArgs = new FRMouseEventArgs();
      //ShowGrid = true;
      AllowDrop = true;
      SnapToGrid = true;
      BackColor = SystemColors.Window;
      SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }
  }
}
