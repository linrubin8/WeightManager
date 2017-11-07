using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using FastReport.Data;

namespace FastReport.FastQueryBuilder
{
    internal class DataBase
    {
        private Query tableList = new Query();
        public DataConnectionBase dataBase;

        private static int CompareTable(Table x, Table y)
        {
            return x.Name.CompareTo(y.Name);
        }

        public DataBase(DataConnectionBase db)
        {
            dataBase = db;
            try
            {
                dataBase.CreateAllTables(false);
            }
            catch
            {
            }

            foreach (TableDataSource tds in dataBase.Tables)
            {
                Table tbl = new Table(tds);
                tbl.Name = tbl.Name; 
                tableList.TableList.Add(tbl);
            }
            tableList.TableList.Sort(CompareTable);
        }

        public string GetQuotationChars()
        {
            return dataBase.GetQuotationChars();
        }

        public List<Table> GetTableList()
        {
            return tableList.TableList;
        }
    }
}
