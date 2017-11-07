using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace FastReport.Utils
{
  internal class BlobStore : IDisposable
  {
    private List<BlobItem> FList;
    private Dictionary<string, BlobItem> FItems;
    private FileStream FTempFile;
    private string FTempFileName;
    
    public int Count
    {
      get { return FList.Count; }
    }

    internal FileStream TempFile
    {
      get { return FTempFile; }
    }

    public int Add(byte[] stream)
    {
      BlobItem item = new BlobItem(stream, this);
      FList.Add(item);
      return FList.Count - 1;
    }

    public int AddOrUpdate(byte[] stream, string src)
    {
      BlobItem item = new BlobItem(stream, this);
      if (!String.IsNullOrEmpty(src))
      {
        if (FItems.ContainsKey(src))
          return FList.IndexOf(FItems[src]);
        else
        {
          item.Source = src;
          FItems[src] = item;
        }
      }
      FList.Add(item);
      return FList.Count - 1;
    }

    public byte[] Get(int index)
    {
      byte[] stream = FList[index].Stream;
      return stream;
    }

    public string GetSource(int index)
    {
      return FList[index].Source;
    }

   /* public Dictionary<string, byte[]> GetCache()
    {
      Dictionary<string, byte[]> result = new Dictionary<string, byte[]>();
      foreach(BlobItem item in FList)
      {
        if (!String.IsNullOrEmpty(item.Source))
          result[item.Source] = item.Stream;
      }
      return result;
    }*/

    public void Clear()
    {
      foreach (BlobItem b in FList)
      {
        b.Dispose();
      }
      FItems.Clear();
      FList.Clear();
    }

    public void LoadDestructive(XmlItem rootItem)
    {
      Clear();
      // avoid memory fragmentation when loading large amount of big blobs
      for (int i = 0; i < rootItem.Count; i++)
      {
        AddOrUpdate(Convert.FromBase64String(rootItem[i].GetProp("Stream", false)),
          rootItem[i].GetProp("Source"));
        rootItem[i].ClearProps();
      }
    }

    public void Save(XmlItem rootItem)
    {
      foreach (BlobItem item in FList)
      {
        XmlItem xi = rootItem.Add();
        xi.Name = "item";
        xi.SetProp("Stream", Converter.ToXml(item.Stream));
        if (!String.IsNullOrEmpty(item.Source))
          xi.SetProp("Source", Converter.ToXml(item.Source));
        if (TempFile != null)
          item.Dispose();
      }
    }
    
    public void Dispose()
    {
      Clear();
      if (FTempFile != null)
      {
        FTempFile.Dispose();
        FTempFile = null;
        File.Delete(FTempFileName);
      }
    }
    
    public BlobStore(bool useFileCache)
    {
      FList = new List<BlobItem>();
      FItems = new Dictionary<string, BlobItem>();
      if (useFileCache)
      {
        FTempFileName = Config.CreateTempFile("");
        FTempFile = new FileStream(FTempFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
      }
    }


    private class BlobItem : IDisposable
    {
      private byte[] FStream;
      private BlobStore FStore;
      private long FTempFilePosition;
      private long FTempFileSize;
      private string FSrc;

      public byte[] Stream
      {
        get
        {
          if (TempFile != null)
          {
            lock (TempFile)
            {
              TempFile.Seek(FTempFilePosition, SeekOrigin.Begin);
              FStream = new byte[FTempFileSize];
              TempFile.Read(FStream, 0, (int)FTempFileSize);
            }
          }
          return FStream;
        }
      }

      /// <summary>
      /// Source of image, only for inline img tag
      /// </summary>
      public string Source { get { return FSrc; } set { FSrc = value; } }

      public Stream TempFile
      {
        get { return FStore.TempFile; }
      }

      private void ClearStream()
      {
        FStream = null;
      }

      public void Dispose()
      {
        ClearStream();
      }

      public BlobItem(byte[] stream, BlobStore store)
      {
        FStore = store;
        FStream = stream;
        FSrc = null;
        if (TempFile != null)
        {
          TempFile.Seek(0, SeekOrigin.End);
          FTempFilePosition = TempFile.Position;
          FTempFileSize = stream.Length;
          TempFile.Write(stream, 0, (int)FTempFileSize);
          TempFile.Flush();
          ClearStream();
        }
      }
    }

  }
}
