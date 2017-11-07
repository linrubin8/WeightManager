using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Printing;
using FastReport.Preview;
using FastReport.Forms;
using FastReport.Utils;

namespace FastReport.Print
{
	internal partial class Printer
	{
		private Report FReport;

		public void Print( PrinterSettings printerSettings, int curPage )
		{
			if( FReport.PrintSettings.CopyNames.Length > 0 )
			{
				// copy names are set, handle copies in code
				int copies = FReport.PrintSettings.Copies;

				try
				{
					FReport.PrintSettings.Copies = 1;
					for( int copyIndex = 1; copyIndex <= copies; copyIndex++ )
					{
						FReport.PreparedPages.MacroValues["Copy#"] = copyIndex;
						PrintInternal( printerSettings, curPage );
					}
				}
				finally
				{
					FReport.PrintSettings.Copies = copies;
					FReport.PreparedPages.MacroValues.Remove( "Copy#" );
				}
			}
			else
			{
				// just print
				PrintInternal( printerSettings, curPage );
			}
		}

		// Simon: PrintInternal 改为 PrintInternal_inner
		private void PrintInternal_inner( PrinterSettings printerSettings, int curPage )
		{
			using( PrintDocument doc = new PrintDocument() )
			{
				if( printerSettings != null )
					doc.PrinterSettings = printerSettings;

				PrintControllerBase controller = null;
				switch( FReport.PrintSettings.PrintMode )
				{
					case PrintMode.Default:
						controller = new DefaultPrintController( FReport, doc, curPage );
						break;

					case PrintMode.Split:
						controller = new SplitPrintController( FReport, doc, curPage );
						break;

					case PrintMode.Scale:
						controller = new ScalePrintController( FReport, doc, curPage );
						break;
				}

				doc.PrintController = new StandardPrintController();
				doc.PrintPage += new PrintPageEventHandler( controller.PrintPage );
				doc.QueryPageSettings += new QueryPageSettingsEventHandler( controller.QueryPageSettings );
				Duplex duplex = FReport.PrintSettings.Duplex;
				if( duplex != Duplex.Default )
					doc.PrinterSettings.Duplex = duplex;

				try
				{
					// Simon: 触发开始打印 
					OnBeginPrint();

					FReport.SetOperation( ReportOperation.Printing );
					Config.ReportSettings.OnStartProgress( FReport );

					try
					{
						doc.Print();
						Config.ReportSettings.OnReportPrinted( FReport );
					}
					catch
					{
						// error on printing
					}

					// Simon: 触发打印结束 
					OnEndPrint();
				}
				finally
				{
					Config.ReportSettings.OnFinishProgress( FReport );
					FReport.SetOperation( ReportOperation.None );
				}
			}
		}

		public Printer( Report report )
		{
			FReport = report;
		}
	}
}