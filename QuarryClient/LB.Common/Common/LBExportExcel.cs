using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Common
{
    public class LBExportExcel
    {
        /// <summary>  
        /// 导出速度最快  
        /// </summary>  
        /// <param name="list"><列名，数据></param>  
        /// <param name="filepath"></param>  
        /// <returns></returns>  
        public static bool ExcelExport(DataTable dtResult,string strSheetName)
        {
            bool bSuccess = true;
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Excel(*.xls)|*.xls|All Files|*.*";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            fileDialog.ShowDialog();

            if (fileDialog.FileName != "")
            {
                Microsoft.Office.Interop.Excel.Application appexcel = new Microsoft.Office.Interop.Excel.Application();
                System.Reflection.Missing miss = System.Reflection.Missing.Value;
                appexcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbookdata = null;
                Microsoft.Office.Interop.Excel.Worksheet worksheetdata = null;
                Microsoft.Office.Interop.Excel.Range rangedata;

                workbookdata = appexcel.Workbooks.Add();

                //设置对象不可见  
                appexcel.Visible = false;
                appexcel.DisplayAlerts = false;
                try
                {
                    //foreach (var lv in list)
                    {
                        //var keys = lv.Key as List<string>;
                        //var values = lv.Value as List<IList<object>>;



                        worksheetdata = (Microsoft.Office.Interop.Excel.Worksheet)workbookdata.Worksheets.Add(miss, workbookdata.ActiveSheet);

                        for (int i = 0; i < dtResult.Columns.Count - 1; i++)
                        {
                            //给工作表赋名称  
                            worksheetdata.Name = strSheetName;//列名的第一个数据位表名  
                            worksheetdata.Cells[1, i + 1] = dtResult.Columns[i].ColumnName;
                        }

                        //因为第一行已经写了表头，所以所有数据都应该从a2开始  
                        rangedata = worksheetdata.get_Range("a2", miss);
                        Microsoft.Office.Interop.Excel.Range xlrang = null;

                        //irowcount为实际行数，最大行  
                        int irowcount = dtResult.Rows.Count;
                        int iparstedrow = 0, icurrsize = 0;

                        //ieachsize为每次写行的数值，可以自己设置  
                        int ieachsize = 10000;

                        //icolumnaccount为实际列数，最大列数  
                        int icolumnaccount = dtResult.Columns.Count - 1;

                        //在内存中声明一个ieachsize×icolumnaccount的数组，ieachsize是每次最大存储的行数，icolumnaccount就是存储的实际列数  
                        object[,] objval = new object[ieachsize, icolumnaccount];
                        icurrsize = ieachsize;

                        while (iparstedrow < irowcount)
                        {
                            if ((irowcount - iparstedrow) < ieachsize)
                                icurrsize = irowcount - iparstedrow;

                            //用for循环给数组赋值  
                            for (int i = 0; i < icurrsize; i++)
                            {
                                for (int j = 0; j < icolumnaccount; j++)
                                {
                                    var v = dtResult.Rows[i + iparstedrow][j];
                                    objval[i, j] = v != null ? v.ToString() : "";
                                }
                            }
                            string X = "A" + ((int)(iparstedrow + 2)).ToString();
                            string col = "";
                            if (icolumnaccount <= 26)
                            {
                                col = ((char)('A' + icolumnaccount - 1)).ToString() + ((int)(iparstedrow + icurrsize + 1)).ToString();
                            }
                            else
                            {
                                col = ((char)('A' + (icolumnaccount / 26 - 1))).ToString() + ((char)('A' + (icolumnaccount % 26 - 1))).ToString() + ((int)(iparstedrow + icurrsize + 1)).ToString();
                            }
                            xlrang = worksheetdata.get_Range(X, col);
                            xlrang.NumberFormat = "@";
                            // 调用range的value2属性，把内存中的值赋给excel  
                            xlrang.Value2 = objval;
                            iparstedrow = iparstedrow + icurrsize;
                        }
                    }
                    ((Microsoft.Office.Interop.Excel.Worksheet)workbookdata.Worksheets["Sheet1"]).Delete();
                    ((Microsoft.Office.Interop.Excel.Worksheet)workbookdata.Worksheets["Sheet2"]).Delete();
                    ((Microsoft.Office.Interop.Excel.Worksheet)workbookdata.Worksheets["Sheet3"]).Delete();
                    //保存工作表  
                    workbookdata.SaveAs(fileDialog.FileName, miss, miss, miss, miss, miss, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss);
                    workbookdata.Close(false, miss, miss);
                    appexcel.Workbooks.Close();
                    appexcel.Quit();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbookdata);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(appexcel.Workbooks);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(appexcel);
                    GC.Collect();

                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("导出成功！");
                }
                catch (Exception ex)
                {
                    //ErrorMsg = ex.Message;
                    bSuccess = false;
                }
                finally
                {
                    if (appexcel != null)
                    {
                        //ExcelImportHelper.KillSpecialExcel(appexcel);
                    }
                }
            }
            return bSuccess;
        }
    }
}
