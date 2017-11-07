using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.PropertyGridInternal;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing.Design;
using FastReport.Utils;
using FastReport.TypeEditors;
using FastReport.TypeConverters;
using System.Reflection;
using FastReport.Editor.Syntax.Parsers;
using System.Threading;
using System.Globalization;

namespace FastReport.Controls
{
  internal class FRPropertyGrid : PropertyGrid
  {
    private bool FShowEvents;
    
    [DefaultValue(false)]
    public bool ShowEvents
    {
      get { return FShowEvents; }
      set 
      { 
        FShowEvents = value;
        if (value)
          PropertyTabs.AddTabType(typeof(FREventsTab));
        else
          PropertyTabs.RemoveTabType(typeof(FREventsTab));
      }
    }
    
    internal int SelectedTabIndex
    {
      get { return this.SelectedTab is FRPropertiesTab ? 0 : 1; }
      set
      {
        SetSelectedTab(value);
      }
    }

    // dirty hack, shame on MS!
    private void SetSelectedTab(int selectedTabIndex)
    {
      if ((selectedTabIndex < 0) || (selectedTabIndex >= PropertyTabs.Count))
      {
        throw new ArgumentException("Invalid tab index to select: " + selectedTabIndex);
      }

      FieldInfo buttonsField = typeof(PropertyGrid).GetField("viewTabButtons", BindingFlags.NonPublic | BindingFlags.Instance);
      ToolStripButton[] viewTabButtons = (ToolStripButton[])buttonsField.GetValue(this);
      ToolStripButton viewTabButton = viewTabButtons[selectedTabIndex];

      typeof(PropertyGrid).InvokeMember("OnViewTabButtonClick", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, this, new object[] { viewTabButton, EventArgs.Empty });
    }  
        
    public FRPropertyGrid()
    {
      DrawFlatToolbar = true;
      PropertySort = PropertySort.Alphabetical;
      PropertyTabs.RemoveTabType(typeof(PropertiesTab));
      PropertyTabs.AddTabType(typeof(FRPropertiesTab));
    }

    private class FRPropertyDescriptor : PropertyDescriptor
    {
      private PropertyDescriptor FDescriptor;

      public override string Category
      {
        get 
        {
            return GetCategory();          
        }
      }

      private string GetCategory()
      {
          CultureInfo currentlangUI = Thread.CurrentThread.CurrentUICulture;
          Thread.CurrentThread.CurrentUICulture = Config.EngCultureInfo;
          string category = FDescriptor.Category;
          Thread.CurrentThread.CurrentUICulture = currentlangUI;
          if (Res.StringExists("Properties,Categories," + category))
              return Res.Get("Properties,Categories," + category);
          return category;
      }

      public override string DisplayName
      {
        get
        {
          return FDescriptor.DisplayName;
        }
      }

      public override string Description
      {
        get
        {
          try
          {
            return ReflectionRepository.DescriptionHelper.GetDescription(ComponentType.GetProperty(Name));
          }
          catch
          {
          }
          return FDescriptor.Description;

          //if (Res.StringExists("Properties," + FDescriptor.ComponentType.Name + "." + Name))
          //  return Res.Get("Properties," + FDescriptor.ComponentType.Name + "." + Name);
          //else if (Res.StringExists("Properties," + Name))
          //  return Res.Get("Properties," + Name);
          //return FDescriptor.Description;
        }
      }

      public override Type ComponentType
      {
        get { return FDescriptor.ComponentType; }
      }

      public override bool IsReadOnly
      {
        get { return FDescriptor.IsReadOnly; }
      }

      public override Type PropertyType
      {
        get { return FDescriptor.PropertyType; }
      }

      public override bool CanResetValue(object component)
      {
        return FDescriptor.CanResetValue(component);
      }

      public override object GetValue(object component)
      {
        return FDescriptor.GetValue(component);
      }

      public override void ResetValue(object component)
      {
        FDescriptor.ResetValue(component);
      }

      public override void SetValue(object component, object value)
      {
        FDescriptor.SetValue(component, value);
      }

      public override bool ShouldSerializeValue(object component)
      {
        return FDescriptor.ShouldSerializeValue(component);
      }

      public FRPropertyDescriptor(PropertyDescriptor descriptor) : base(descriptor)
      {
        FDescriptor = descriptor;
      }
    }

    private class FREventDescriptor : FRPropertyDescriptor
    {
      public override string DisplayName
      {
        get { return base.DisplayName.Replace("Event", ""); }
      }

      protected override Attribute[] AttributeArray
      {
        get 
        { 
          return new Attribute[] { 
            new EditorAttribute(typeof(EventEditor), typeof(UITypeEditor)),
            new TypeConverterAttribute(typeof(EventConverter))};
        }
        set { base.AttributeArray = value; }
      }

      public FREventDescriptor(PropertyDescriptor descriptor) : base(descriptor)
      {
      }
    }

    private class FRPropertiesTab : PropertyTab
    {
      #region Public Methods
      public override bool CanExtend(object extendee)
      {
        return true;
      }

      public override PropertyDescriptorCollection GetProperties(
        ITypeDescriptorContext context, object component, Attribute[] attributes)
      {
        return GetProperties(component, attributes);
      }

      public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
      {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(component);

        PropertyDescriptorCollection properties = new PropertyDescriptorCollection(null);
        foreach (PropertyDescriptor prop in props)
        {
          BrowsableAttribute browsable = prop.Attributes[typeof(BrowsableAttribute)] as BrowsableAttribute;
          // skip nonbrowsable properties
          if (browsable != null && browsable.Browsable == false)
            continue;
          // skip properties other than "Restriction" if DontModify flag is set
          if (component is Base && (component as Base).HasRestriction(Restrictions.DontModify) && prop.Name != "Restrictions")
            continue;
          // skip all properties if HideAllProperties flag is set
          if (component is Base && (component as Base).HasRestriction(Restrictions.HideAllProperties))
            continue;
          // skip some Font properties
//          if (component is Font && (prop.Name.StartsWith("Gdi") || prop.Name == "Unit"))
  //          continue;
          // check if property is not an event
          if (!prop.Name.EndsWith("Event"))
            properties.Add(new FRPropertyDescriptor(prop));
        }
        return properties;
      }

      public override Bitmap Bitmap
      {
        get { return Res.GetImage(78); }
      }

      public override string TabName
      {
        get { return Res.Get("Designer,ToolWindow,Properties,PropertiesTab"); }
      }
      #endregion
    }


    private class FREventsTab : PropertyTab
    {
      #region Public Methods
      public override bool CanExtend(object extendee)
      {
        return true;
      }

      public override PropertyDescriptorCollection GetProperties(
        ITypeDescriptorContext context, object component, Attribute[] attributes)
      {
        return GetProperties(component, attributes);
      }

      public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
      {
        PropertyDescriptorCollection props = TypeDescriptor.GetProperties(component);

        PropertyDescriptorCollection properties = new PropertyDescriptorCollection(null);
        foreach (PropertyDescriptor prop in props)
        {
          BrowsableAttribute attr = prop.Attributes[typeof(BrowsableAttribute)] as BrowsableAttribute;
          // skip nonbrowsable properties
          if (attr != null && attr.Browsable == false) continue;
          // check if property is an event
          if (prop.Name.EndsWith("Event"))
            properties.Add(new FREventDescriptor(prop));
        }
        return properties;
      }

      public override Bitmap Bitmap
      {
        get { return Res.GetImage(79); }
      }

      public override string TabName
      {
        get { return Res.Get("Designer,ToolWindow,Properties,EventsTab"); }
      }
      #endregion
    }
  }
}
