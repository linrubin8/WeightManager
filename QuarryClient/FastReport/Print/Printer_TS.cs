using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;

namespace FastReport.Print
{
	internal partial class Printer
	{
		private void PrintInternal( PrinterSettings printerSettings, int curPage )
		{
			PrintInternal_inner( printerSettings, curPage );
		}

		// ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
		// 注意，以下两个方法，需要修改原 PrintInternal 方法，调用以下两个方法！！！！
		// ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
		private void OnBeginPrint()
		{
			// 触发开始打印 
			FReport.OnBeginPrint( EventArgs.Empty );
		}

		private void OnEndPrint()
		{
			// 触发打印结束 
			FReport.OnEndPrint( EventArgs.Empty );
		}
	}
}
