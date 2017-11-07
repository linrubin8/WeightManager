using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using FastReport.Design;

namespace FastReport
{
  internal class SelectedPictureObjects
  {
    private List<PictureObject> FList;
    private Designer FDesigner;

    public PictureObject First
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

    private List<PictureObject> ModifyList
    {
      get { return FList.FindAll(CanModify); }
    }

    private bool CanModify(PictureObject c)
    {
      return !c.HasRestriction(Restrictions.DontModify);
    }

    public void Update()
    {
      FList.Clear();
      if (FDesigner.SelectedObjects != null)
      {
        foreach (Base c in FDesigner.SelectedObjects)
        {
          if (c is PictureObject)
            FList.Add(c as PictureObject);
        }
      }
    }

    public void SetSizeMode(PictureBoxSizeMode value)
    {
      foreach (PictureObject c in ModifyList)
      {
        c.SizeMode = value;
      }
    }

    public SelectedPictureObjects(Designer designer)
    {
      FDesigner = designer;
      FList = new List<PictureObject>();
    }
  }
}
