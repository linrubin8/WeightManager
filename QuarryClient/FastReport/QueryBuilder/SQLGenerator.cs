using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.FastQueryBuilder
{
    class SQLGenerator
    {
        private Query query;
        private List<Table> TableHasAlias = new List<Table>();

        public string qch;

        public SQLGenerator(Query qr)
        {
            query = qr;
        }

        public string getSql()
        {
            TableHasAlias.Clear();
            string result = "";
            result += getSelect();
            result += getFrom();
            result += getWhere();
            result += getGroup();
            result += getOrder();
            return result;
        }

        private string getGroup()
        {
            string result = string.Empty;

            foreach (Field fld in query.GroupedFields)
                result += fld.getFullName(qch) + ", ";

            if (result.Length > 0)
                return "GROUP BY " + result.Substring(0, result.Length - 2) + "\r\n";
            else
                return string.Empty;
        }

        private string getOrder()
        {
            string result = string.Empty;

            foreach (Field fld in query.SelectedFields)
                if (fld.Order != null)
                    result += fld.getFullName(qch) + " " + fld.Order + ", ";

            if (result.Length > 0)
                return "ORDER BY " + result.Substring(0, result.Length - 2) + "\r\n";
            else
                return string.Empty;
        }
        
        private string getSelect()
        {
            if (query.SelectedFields.Count == 0)
                return string.Empty;

            string result = "";
            foreach (Field fld in query.SelectedFields)
            {
                string tmp;
                if (fld.Func != null)
                    tmp = fld.Func + '(' + fld.getFullName(qch) + ')';
                else
                    tmp = fld.getFullName(qch);
                result += tmp;
                if (fld.Alias.Length != 0)
                    result += " AS " + fld.Alias;
                result += ", ";
            }            
            return "SELECT " + result.Remove(result.Length - 2) + "\r\n";
        }

        private bool ThereIsTableInJoin(Table table, List<Link> linkList)
        {
            foreach (Link lnk in linkList)
                if ((lnk.From.Table == table) || (lnk.To.Table == table))
                    return true;
            return false;
        }

        private string getJoin(Link lnk)
        {
            string result = QueryEnums.JoinTypesToStr[(int)lnk.Join] + " " + 
              lnk.To.Table.getFromName(qch, !TableHasAlias.Contains(lnk.To.Table)) + " ON " + 
              lnk.From.getFullName(qch, !TableHasAlias.Contains(lnk.From.Table)) + 
              " " + QueryEnums.WhereTypesToStr[1][(int)lnk.Where] + " " +
              lnk.To.getFullName(qch, !TableHasAlias.Contains(lnk.To.Table)) + " \r\n";
            TableHasAlias.Add(lnk.To.Table);
            return result;
        }

        private string getFrom()
        {
            if (query.TableList.Count == 0)
                return string.Empty;

            string result = "";
           
            List<Table> tableList = new List<Table>();            
            foreach (Table tbl in query.TableList)
                tableList.Add(tbl);

            foreach (Link lnk in query.LinkList)
            {
                if (lnk.Join != QueryEnums.JoinTypes.Where)
                {
                    if (result == string.Empty)
                    {
                        result += lnk.From.Table.getFromName(qch) + " ";
                        tableList.Remove(lnk.From.Table);
                        result += getJoin(lnk);
                    }
                    else
                    {
                        result = "(" + result + ") " + getJoin(lnk);
                    }
                    tableList.Remove(lnk.To.Table);
                }
                else
                {
                    if (result != string.Empty)
                        result += ", ";
                    if (!tableList.Contains(lnk.To.Table))
                    {
                        result += lnk.To.Table.getFromName(qch);
                        tableList.Remove(lnk.To.Table);
                    }
                }
            }
            foreach (Table tbl in tableList)
            {
                if (result != string.Empty)
                    result += ", ";
                result += tbl.getFromName(qch);
            }

            return "FROM " + result + "\r\n";
        }

        private string getWhere()
        {
            string result = string.Empty;
            foreach (Field fld in query.SelectedFields)
                if (fld.Filter != string.Empty)
                    result += fld.getFullName(qch) + fld.Filter + " AND ";

            if (query.LinkList.Count != 0)
            {
                foreach (Link lnk in query.LinkList)
                {
                    string wop = string.Empty;
                    if (lnk.Join == QueryEnums.JoinTypes.Where)
                    {
                        wop = QueryEnums.WhereTypesToStr[1][(int)lnk.Where];
                        result += lnk.From.getFullName(qch) + " " + wop + " " + lnk.To.getFullName(qch) + " AND ";
                    }
                }
            }

            if (result != string.Empty)
            {
                result = result.Substring(0, result.Length - 5);
                return "WHERE " + result + "\r\n";
            }
            else
                return string.Empty;
        }

    }
}
