using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FastReport.Design;
using FastReport.Format;

namespace FastReport
{
  internal class SelectedTextBaseObjects
  {
    private List<TextObjectBase> FList;
    private Designer FDesigner;

    public TextObjectBase First
    {
      get { return FList.Count > 0 ? FList[0] : null; }
    }

    public int Count
    {
      get { return FList.Count; }
    }

    public bool Enabled
    {
      get
      {
        return Count > 1 || (Count == 1 && CanModify(First));
      }
    }

    private List<TextObjectBase> ModifyList
    {
      get { return FList.FindAll(CanModify); }
    }

    private bool CanModify(TextObjectBase c)
    {
      return !c.HasRestriction(Restrictions.DontModify);
    }

    public void SetAllowExpressions(bool value)
    {
      foreach (TextObjectBase text in ModifyList)
      {
        text.AllowExpressions = value;
      }
    }

    public void SetFormat(FormatCollection value)
    {
      foreach (TextObjectBase text in ModifyList)
      {
        text.Formats.Assign(value);
      }
    }

    public void Update()
    {
      FList.Clear();
      if (FDesigner.SelectedObjects != null)
      {
        foreach (Base c in FDesigner.SelectedObjects)
        {
          if (c is TextObjectBase)
            FList.Add(c as TextObjectBase);
        }
      }
    }

    public SelectedTextBaseObjects(Designer designer)
    {
      FDesigner = designer;
      FList = new List<TextObjectBase>();
    }
  }
}
