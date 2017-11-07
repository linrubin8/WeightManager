using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Printing;

namespace FastReport.Utils
{
  internal static class PrinterUtils
  {
    [DllImport("winspool.drv", EntryPoint = "DocumentPropertiesW")]
    private static extern int DocumentProperties(IntPtr hWnd, IntPtr hPrinter, [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName, IntPtr pDevMode, IntPtr devModeIn, int fMode);
    [DllImport("winspool.drv")]
    private static extern int OpenPrinter(string pPrinterName, out IntPtr hPrinter, IntPtr pDefault);
    [DllImport("winspool.drv")]
    private static extern int ClosePrinter(IntPtr phPrinter);
    [DllImport("kernel32.dll")]
    private static extern IntPtr GlobalLock(IntPtr hMem);
    [DllImport("kernel32.dll")]
    private static extern int GlobalUnlock(IntPtr hMem);
    [DllImport("kernel32.dll")]
    private static extern int GlobalFree(IntPtr hMem);

    private const int DM_PROMPT = 4;
    private const int DM_OUT_BUFFER = 2;
    private const int DM_IN_BUFFER = 8;

    public static void ShowPropertiesDialog(PrinterSettings printerSettings)
    {
            try
            {
                IntPtr hDevMode = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);
                IntPtr handle;
                OpenPrinter(printerSettings.PrinterName, out handle, IntPtr.Zero);
                IntPtr pDevMode = GlobalLock(hDevMode);
                int result = DocumentProperties(IntPtr.Zero, handle, printerSettings.PrinterName, pDevMode, pDevMode, DM_IN_BUFFER | DM_PROMPT | DM_OUT_BUFFER);
                GlobalUnlock(hDevMode);

                if (result == 1)
                {
                    printerSettings.SetHdevmode(hDevMode);
                    printerSettings.DefaultPageSettings.SetHdevmode(hDevMode);
                }
                ClosePrinter(handle);
            }
            catch
            {
                // wrong printer
            }
    }
  }  
}
