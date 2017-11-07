using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace FastReport.Utils
{
  /// <summary>
  /// The profiler.
  /// </summary>
  public static class Profiler
  {
    private static long FMemory;
    private static int FTickCount;
    
    /// <summary>
    /// Starts the profiler.
    /// </summary>
    public static void Start()
    {
      FMemory = Process.GetCurrentProcess().PrivateMemorySize64;
      FTickCount = Environment.TickCount;
    }  
    
    /// <summary>
    /// Finishes the profiler and displays results.
    /// </summary>
    public static void Stop()
    {
      MessageBox.Show(((Process.GetCurrentProcess().PrivateMemorySize64 - FMemory) / 1024).ToString() + " Kb" +
        '\r' + (Environment.TickCount - FTickCount).ToString() + " ms");
    }
      
  }
}
