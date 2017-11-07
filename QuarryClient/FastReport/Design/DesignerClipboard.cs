using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using FastReport.Utils;
using FastReport.Data;

namespace FastReport.Design
{
  internal class DesignerClipboard
  {
    private Designer FDesigner;
    
    public bool CanPaste
    {
      get 
      { 
        try
        {
          return Clipboard.ContainsData("FastReport"); 
        }
        catch
        {
          return false;
        }
      }
    }

    public void Cut()
    {
      Copy();
      FDesigner.cmdDelete.Invoke();
    }
    
    public void Copy()
    {
      // Things that we should consider:
      // - when copy an object that is selected, we must save its absolute position instead of relative Top, Left.
      //   This is necessary when copying several objects that have different parents.
      // - when copy a parent object and its child, and both are selected, we must exclude a child from the 
      //   selected objects. This is necessary to avoid duplicate copy of the child object - it will be
      //   copied by its parent.
      
      if (FDesigner.SelectedObjects == null)
        return;
      using (ClipboardParent parent = new ClipboardParent())
      {
        Hashtable bounds = new Hashtable();
        parent.PageType = FDesigner.SelectedObjects[0].Page.GetType();
        foreach (Base c in FDesigner.Objects)
        {
          if (c.IsSelected)
          {
            if (c.HasFlag(Flags.CanCopy))
            {
              if (!parent.Contains(c))
              {
                if (c is ComponentBase)
                {
                  ComponentBase c1 = c as ComponentBase;
                  bounds[c1] = c1.Bounds;
                  c1.SetDesigning(false);
                  c1.Left = c1.AbsLeft;
                  c1.Top = c1.AbsTop;
                }  
                parent.Objects.Add(c);
              }  
            }  
          }    
        }
        using (MemoryStream stream = new MemoryStream())
        using (FRWriter writer = new FRWriter())
        {
          writer.SerializeTo = SerializeTo.Clipboard;
          writer.Write(parent);
          writer.Save(stream);
          Clipboard.SetData("FastReport", stream);
        }
        
        // restore components' state
        foreach (Base c in parent.Objects)
        {
          if (c is ComponentBase)
          {
            ComponentBase c1 = c as ComponentBase;
            if (bounds[c1] != null)
              c1.Bounds = (RectangleF)bounds[c1];
            c1.SetDesigning(true);
          }
        }
      }
    }
    
    public void Paste()
    {
      if (!CanPaste)
        return;
      using (ClipboardParent parent = new ClipboardParent())
      using (MemoryStream stream = Clipboard.GetData("FastReport") as MemoryStream)
      using (FRReader reader = new FRReader(null))
      {
        reader.Load(stream);
        reader.Read(parent);
        
        PageBase page = FDesigner.ActiveReportTab.ActivePage;
        if (page.GetType() == parent.PageType)
        {
          // prepare to create unique name
          ObjectCollection allObjects = page.Report.AllNamedObjects;
          // prepare a list of existing names
          Hashtable names = new Hashtable();
          foreach (Base c in allObjects)
          {
            names[c.Name] = 0;
          }

          // since we are trying to preserve pasted object's name, add all names to the 
          // allObjects list to guarantee that FastNameCreator will work correctly
          foreach (Base c in parent.AllObjects)
          {
            allObjects.Add(c);
            // there is an existing object with the same name. Clear the name to indicate
            // that we should create an unique name for this object
            if (names.ContainsKey(c.Name))
              c.Name = "";
          }
          
          FastNameCreator nameCreator = new FastNameCreator(allObjects);
          
          FDesigner.SelectedObjects.Clear();
          foreach (Base c in parent.Objects)
          {
            c.Parent = FDesigner.ActiveReportTab.ActivePageDesigner.GetParentForPastedObjects();
            if (c.Name == "")
              nameCreator.CreateUniqueName(c);
            
            // reset group index
            if (c is ComponentBase)
              (c as ComponentBase).GroupIndex = 0;
            
            FDesigner.Objects.Add(c);
            if (c is IParent)
            {
              foreach (Base c1 in c.AllObjects)
              {
                if (c1.Name == "")
                  nameCreator.CreateUniqueName(c1);
                FDesigner.Objects.Add(c1);
              }
            }
            FDesigner.SelectedObjects.Add(c);
          }
          FDesigner.ActiveReportTab.ActivePageDesigner.Paste(parent.Objects, InsertFrom.Clipboard);
        }
      }
    }
    
    public DesignerClipboard(Designer designer)
    {
      FDesigner = designer;
    }
    
    static DesignerClipboard()
    {
      RegisteredObjects.Add(typeof(ClipboardParent), "", 0);
    }


    private class ClipboardParent : Base
    {
      private ObjectCollection FObjects;
      private Type FPageType;

      public ObjectCollection Objects
      {
        get { return FObjects; }
      }

      public new ObjectCollection AllObjects
      {
        get
        {
          ObjectCollection result = new ObjectCollection();
          foreach (Base c in Objects)
            EnumObjects(c, result);
          return result;
        }
      }

      public Type PageType
      {
        get { return FPageType; }
        set { FPageType = value; }
      }

      private void EnumObjects(Base c, ObjectCollection list)
      {
        if (c != this)
          list.Add(c);
        foreach (Base obj in c.ChildObjects)
          EnumObjects(obj, list);
      }

      
      public bool Contains(Base obj)
      {
        return AllObjects.Contains(obj);  
      }

      public override void Assign(Base source)
      {
        BaseAssign(source);
      }
      
      public override void Serialize(FRWriter writer)
      {
        writer.ItemName = Name;
        writer.WriteStr("PageType", FPageType.Name);
        foreach (Base c in FObjects)
        {
          writer.Write(c);
        }
      }

      public override void Deserialize(FRReader reader)
      {
        FPageType = RegisteredObjects.FindType(reader.ReadStr("PageType"));
        while (reader.NextItem())
        {
          Base c = reader.Read() as Base;
          if (c != null)
            FObjects.Add(c);
        }
      }

      public ClipboardParent()
      {
        FObjects = new ObjectCollection();
        Name = "ClipboardParent";
      }
    }
  }
}
