using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.IO.Compression;

namespace FastReport.Utils
{
  /// <summary>
  /// Resource loader class.
  /// </summary>
  public static class ResourceLoader
  {
    /// <summary>
    /// Gets a stream from specified assembly resource.
    /// </summary>
    /// <param name="assembly">Assembly name.</param>
    /// <param name="resource">Resource name.</param>
    /// <returns>Stream object.</returns>
    public static Stream GetStream(string assembly, string resource)
    {
      foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
      {
        AssemblyName name = new AssemblyName(a.FullName);
        if (name.Name == assembly)
        {
          return a.GetManifestResourceStream(assembly + ".Resources." + resource);
        }
      }
      return null;
    }  

    /// <summary>
    /// Gets a stream from FastReport assembly resource.
    /// </summary>
    /// <param name="resource">Resource name.</param>
    /// <returns>Stream object.</returns>
    public static Stream GetStream(string resource)
    {
      return GetStream("FastReport", resource);
    }
    
    /// <summary>
    /// Gets a stream from specified assembly resource and unpacks it.
    /// </summary>
    /// <param name="assembly">Assembly name.</param>
    /// <param name="resource">Resource name.</param>
    /// <returns>Stream object.</returns>
    public static Stream UnpackStream(string assembly, string resource)
    {
      using (Stream packedStream = GetStream(assembly, resource))
      using (Stream gzipStream = new GZipStream(packedStream, CompressionMode.Decompress, true))
      {
        MemoryStream result = new MemoryStream();
        
        byte[] buffer = new byte[4096];
        while (true)
        {
          int bytesRead = gzipStream.Read(buffer, 0, 4096);
          if (bytesRead == 0) break;
          result.Write(buffer, 0, bytesRead);
        }
        
        result.Position = 0;
        return result;
      }
    }

    /// <summary>
    /// Gets a stream from specified FastReport assembly resource and unpacks it.
    /// </summary>
    /// <param name="resource">Resource name.</param>
    /// <returns>Stream object.</returns>
    public static Stream UnpackStream(string resource)
    {
      return UnpackStream("FastReport", resource);
    }

    /// <summary>
    /// Gets a bitmap from specified assembly resource.
    /// </summary>
    /// <param name="assembly">Assembly name.</param>
    /// <param name="resource">Resource name.</param>
    /// <returns>Bitmap object.</returns>
    public static Bitmap GetBitmap(string assembly, string resource)
    {
      using (Stream stream = GetStream(assembly, resource))
      using (Bitmap bmp = new Bitmap(stream))
      {
        // To avoid the requirement to keep a stream alive, we clone a bitmap.
        Bitmap result = ImageHelper.CloneBitmap(bmp);
        return result;
      }
    }

    /// <summary>
    /// Gets a bitmap from specified FastReport assembly resource.
    /// </summary>
    /// <param name="resource">Resource name.</param>
    /// <returns>Bitmap object.</returns>
    public static Bitmap GetBitmap(string resource)
    {
      return GetBitmap("FastReport", resource);
    }

    /// <summary>
    /// Gets a cursor from specified assembly resource.
    /// </summary>
    /// <param name="assembly">Assembly name.</param>
    /// <param name="resource">Resource name.</param>
    /// <returns>Cursor object.</returns>
    public static Cursor GetCursor(string assembly, string resource)
    {
      Stream stream = GetStream(assembly, resource);
      Cursor result = new Cursor(stream);
      stream.Dispose();
      return result;
    }

    /// <summary>
    /// Gets a cursor from specified FastReport assembly resource.
    /// </summary>
    /// <param name="resource">Resource name.</param>
    /// <returns>Cursor object.</returns>
    public static Cursor GetCursor(string resource)
    {
      return GetCursor("FastReport", resource);
    }

    /// <summary>
    /// Gets an icon from specified assembly resource.
    /// </summary>
    /// <param name="assembly">Assembly name.</param>
    /// <param name="resource">Resource name.</param>
    /// <returns>Icon object.</returns>
    public static Icon GetIcon(string assembly, string resource)
    {
      Stream stream = GetStream(assembly, resource);
      Icon result = new Icon(stream);
      stream.Dispose();
      return result;
    }

    /// <summary>
    /// Gets an icon from specified FastReport assembly resource.
    /// </summary>
    /// <param name="resource">Resource name.</param>
    /// <returns>Icon object.</returns>
    public static Icon GetIcon(string resource)
    {
      return GetIcon("FastReport", resource);
    }
  }
}
