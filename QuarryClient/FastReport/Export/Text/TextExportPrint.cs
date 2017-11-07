using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace FastReport.Export.Text
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DOCINFO
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDocName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pOutputFile;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDataType;
    }

    /// <summary>
    /// Represents supplement class for print of any stream directly in printer.
    /// </summary>
    public static class TextExportPrint
    {
        [ DllImport( "winspool.drv",CharSet=CharSet.Unicode,ExactSpelling=false, CallingConvention=CallingConvention.StdCall )] 
        internal static extern long OpenPrinter(string pPrinterName, ref IntPtr phPrinter, int pDefault);
        [ DllImport( "winspool.drv",CharSet=CharSet.Unicode,ExactSpelling=false, CallingConvention=CallingConvention.StdCall )] 
        internal static extern long StartDocPrinter(IntPtr hPrinter, int Level, ref DOCINFO pDocInfo);
        [ DllImport("winspool.drv",CharSet=CharSet.Unicode,ExactSpelling=true, CallingConvention=CallingConvention.StdCall)] 
        internal static extern long StartPagePrinter(IntPtr hPrinter);
        [DllImport("winspool.drv", CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern long WritePrinter(IntPtr hPrinter, byte[] data, int buf, ref int pcWritten);
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern long EndPagePrinter(IntPtr hPrinter);
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern long EndDocPrinter(IntPtr hPrinter);
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern long ClosePrinter(IntPtr hPrinter);
        
        /// <summary>
        /// Prints a stream.
        /// </summary>
        /// <param name="PrinterName">Printer name on which should be print.</param>
        /// <param name="DocName">Document title for printer spooler.</param>
        /// <param name="Copies">Count of copies.</param>
        /// <param name="Stream">Stream that will be printed.</param>
        /// <example>This example demonstrates the printing of Stream.
        /// <code>
        /// TextExportPrint.PrintStream("EPSON FX-1000", "My Report", 1, txtStream)</code>
        /// </example>
        public static void PrintStream(string PrinterName, string DocName, int Copies, Stream Stream)
        {
            System.IntPtr lhPrinter = new System.IntPtr();
            DOCINFO di = new DOCINFO();
            int pcWritten = 0;
            di.pDocName = DocName;            
            di.pDataType = "RAW";            
            OpenPrinter(PrinterName , ref lhPrinter, 0);
            StartDocPrinter(lhPrinter, 1, ref di);
            StartPagePrinter(lhPrinter);
            try
            {
                int buffSize = 2048;
                byte[] buff = new byte[buffSize];
                for (int c = 0; c < Copies; c++)
                {
                    Stream.Position = 0;
                    int i = buffSize;
                    while (i == buffSize)
                    {
                        i = Stream.Read(buff, 0, buffSize);
                        WritePrinter(lhPrinter, buff, i, ref pcWritten);
                    }
                }
            }
            finally
            {
                EndPagePrinter(lhPrinter);
                EndDocPrinter(lhPrinter);
                ClosePrinter(lhPrinter);
            }
        }
    }
}
