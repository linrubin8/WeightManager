using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FastReport.Utils;
using FastReport.Data;

namespace FastReport.Design
{
  internal class UndoRedo : IDisposable
  {
    private ReportTab FOwner;
    private List<UndoRedoInfo> FUndo;
    private List<UndoRedoInfo> FRedo;

    public Designer Designer
    {
      get { return FOwner.Designer; }
    }
    
    public Report Report
    {
      get { return FOwner.Report; }
    }
    
    public BlobStore BlobStore
    {
      get { return FOwner.BlobStore; }
    }
    
    public int UndoCount
    {
      get { return FUndo.Count; }
    }

    public int RedoCount
    {
      get { return FRedo.Count; }
    }

    public string[] UndoNames
    {
      get
      {
        string[] result = new string[UndoCount];
        for (int i = 0; i < FUndo.Count; i++)
        {
          result[i] = FUndo[i].Name;
        }
        return result;
      }
    }

    public string[] RedoNames
    {
      get
      {
        string[] result = new string[RedoCount];
        for (int i = 0; i < FRedo.Count; i++)
        {
          result[i] = FRedo[i].Name;
        }
        return result;
      }
    }

    private void SaveReport(Stream stream)
    {
      using (FRWriter writer = new FRWriter())
      {
        writer.SerializeTo = SerializeTo.Undo;
        writer.BlobStore = BlobStore;
        writer.Write(Report);
        writer.Save(stream);
      }
    }

    private void LoadReport(Stream stream)
    {
      ParameterCollection saveParams = new ParameterCollection(null);
      saveParams.Assign(Report.Parameters);

      Report.Clear();
      using (FRReader reader = new FRReader(Report))
      {
        stream.Position = 0;
        reader.BlobStore = BlobStore;
        reader.Load(stream);
        reader.Read(Report);
      }

      Report.Parameters.AssignValues(saveParams);
    }

    private string GetUndoActionName(string action, string objName)
    {
      string s = "";
      if (!String.IsNullOrEmpty(objName))
        s = objName;
      else if (Designer.SelectedObjects != null)
      {
        if (Designer.SelectedObjects.Count == 1)
          s = Designer.SelectedObjects[0].Name;
        if (Designer.SelectedObjects.Count > 1)
          s = String.Format(Res.Get("Designer,UndoRedo,NObjects"), Designer.SelectedObjects.Count);
      }  
      return String.Format(Res.Get("Designer,UndoRedo," + action), s);
    }

    public void ClearUndo()
    {
      foreach (UndoRedoInfo info in FUndo)
      {
        info.Dispose();
      }
      FUndo.Clear();
      if (BlobStore != null)
        BlobStore.Clear();
    }
    
    public void ClearRedo()
    {
      foreach (UndoRedoInfo info in FRedo)
      {
        info.Dispose();
      }
      FRedo.Clear();
    }
    
    public void AddUndo(string name, string objName)
    {
      UndoRedoInfo info = new UndoRedoInfo(GetUndoActionName(name, objName));
      SaveReport(info.Stream);
      FUndo.Insert(0, info);
    }

    public void GetUndo(int actionsCount)
    {
      if (actionsCount >= FUndo.Count - 1)
        actionsCount = FUndo.Count - 1;
      UndoRedoInfo info = FUndo[actionsCount];
      LoadReport(info.Stream);

      for (int i = 0; i < actionsCount; i++)
      {
        info = FUndo[0];
        FRedo.Insert(0, info);
        FUndo.Remove(info);
      }
    }

    public void GetRedo(int actionsCount)
    {
      UndoRedoInfo info = FRedo[actionsCount - 1];
      LoadReport(info.Stream);

      for (int i = 0; i < actionsCount; i++)
      {
        info = FRedo[0];
        FUndo.Insert(0, info);
        FRedo.Remove(info);
      }
    }

    public void Dispose()
    {
      ClearUndo();
      ClearRedo();
    }
    
    public UndoRedo(ReportTab tab)
    {
      FOwner = tab;
      FUndo = new List<UndoRedoInfo>();
      FRedo = new List<UndoRedoInfo>();
    }


    private class UndoRedoInfo : IDisposable
    {
      public string Name;
      public MemoryStream Stream;

      public void Dispose()
      {
        Stream.Dispose();
      }

      public UndoRedoInfo(string name)
      {
        Name = name;
        Stream = new MemoryStream();
      }
    }

  }
  
}
