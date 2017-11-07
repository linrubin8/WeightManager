using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using FastReport.Data;
using System.Collections;

namespace FastReport.FastQueryBuilder
{
    internal class Core
    {
        private IQueryDesigner queryDesigner;
        private DataBase dataBase;
        private List<Table> tableList = new List<Table>();
        private DialogResult dialogResult;        
        public Query query = new Query();
        private bool useJoin = true;

        public Core(IQueryDesigner qd, DataBase db)
        {
            dataBase = db;
            queryDesigner = qd;
            queryDesigner.OnOk += OnOk;
            queryDesigner.OnCancel += OnCancel;
            queryDesigner.OnGetTableList += OnGetTableList;
            queryDesigner.OnAddTable += OnAddTable;
            queryDesigner.OnGenerateSQL += OnGenerateSQL;
            queryDesigner.OnRunSQL += OnRunSQL;
            queryDesigner.Fields = query.SelectedFields;
            queryDesigner.Groups = query.GroupedFields;
            queryDesigner.Links = query.LinkList;
        }

        public bool UseJoin
        {
            get { return useJoin; }
            set { useJoin = value; }
        }

        public DialogResult DesignQuery()
        {
            queryDesigner.DesignQuery();
            return dialogResult;
        }

        private void OnOk(object sender, EventArgs e)
        {
            dialogResult = DialogResult.OK;
            queryDesigner.Close();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            dialogResult = DialogResult.Cancel;
            queryDesigner.Close();
        }

        private void OnGetTableList(object sender, EventArgs e)
        {
            List<Table> str = dataBase.GetTableList();
            queryDesigner.DoFillTableList(str);
        }

        private void OnAddTable(object sender, AddTableEventArgs e)
        {
            Table tbl = (Table)e.table.Clone();          
            tbl.Alias = GetUniqueAlias(tbl);
            TableView tv = queryDesigner.DoAddTable(tbl, e.position);
            tv.OnAddLink += OnAddLink;
            tv.OnSelectField += OnSelectField;
            tv.OnDeleteTable += OnDeleteTable;
            query.TableList.Add(tbl);
        }

        private bool hasDublicate(string alias)
        {
            foreach (Table t in query.TableList)
                if (t.Alias == alias)
                    return true;
            return false;
        }

        private string GetUniqueAlias(Table tbl)
        {
            string al = tbl.Name[0].ToString().ToUpper();
            if (al[0] < 'A' || al[0] > 'Z')
              al = "A";
            int n = 1;

            while (hasDublicate(al))
            {
                al = tbl.Name[0] + n.ToString();
                n++;
            }
            return al;
        }

        private void OnDeleteTable(object sender, AddTableEventArgs e)
        {
            for (int i = query.SelectedFields.Count - 1; i >= 0; i--)
            {
                if (query.SelectedFields[i].Table == e.table)
                    query.SelectedFields.RemoveAt(i);
            }
            query.deleteTable(e.table);
            queryDesigner.Fields = query.SelectedFields;
            queryDesigner.DoRefreshLinks();
        }

        private void OnAddLink(object sender, AddLinkEventArgs e)
        {
            Link lnk;
            if (LinkHasFrom(query.LinkList, e.FieldTo.Table))
                lnk = new Link(e.FieldTo, e.FieldFrom);
            else
                lnk = new Link(e.FieldFrom, e.FieldTo);

            if (LinkHas(query.LinkList, lnk))
                return;

            if (UseJoin)
              lnk.Join = QueryEnums.JoinTypes.InnerJoin;
            else 
              lnk.Join = QueryEnums.JoinTypes.Where;

            lnk.Where = QueryEnums.WhereTypes.Equal;
            query.LinkList.Add(lnk);
            queryDesigner.DoRefreshLinks();
        }

        private bool LinkHas(List<Link> list, Link lnk)
        {
            foreach (Link link in list)
            {
                if ((link.From == lnk.From) && (link.To == lnk.To))
                    return true;
            }
            return false;
        }

        private bool LinkHasFrom(List<Link> list, Table from)
        {
            foreach (Link link in list)
            {
                if ((link.From.Table == from))
                    return true;
            }
            return false;
        }

        private void OnSelectField(object sender, CheckFieldEventArgs e)
        {
            if (e.value)
            {
                query.SelectedFields.Add(e.field);
                queryDesigner.Fields = query.SelectedFields;
            }
            else
            {
                query.SelectedFields.Remove(e.field);
                queryDesigner.Fields = query.SelectedFields;
            }
        }

        private void OnGenerateSQL(object sender, EventArgs e)
        {
            SQLGenerator sql = new SQLGenerator(query);
            sql.qch = dataBase.GetQuotationChars();

            queryDesigner.SQLText = sql.getSql();

            #region Debug
            //queryDesigner.SQLText += "\n\n\n\n/*Tables:\n";
            //foreach (Table tbl in query.TableList)
            //{
            //    queryDesigner.SQLText += tbl.Name + "\n";  
            //}
            //queryDesigner.SQLText += "\nLinks:\n";
            //foreach (Link lnk in query.LinkList)
            //{
            //    queryDesigner.SQLText += lnk.From.Table + ":" + lnk.From.Name + " => " + lnk.To.Table + ":" + lnk.To.Name + "\n";
            //}
            //queryDesigner.SQLText += "\nFields:\n";
            //foreach (Field fld in query.SelectedFields)
            //{
            //    queryDesigner.SQLText += fld.Table.ToString() + '.' + fld.Name + "\n";
            //}
            //queryDesigner.SQLText += "*/";
            #endregion
        }
        
        private void OnRunSQL(object sender, EventArgs e)
        {
            SQLGenerator sql = new SQLGenerator(query);                        
            sql.qch = dataBase.GetQuotationChars();
            string SQL;

            if (queryDesigner.SQLText == string.Empty)
                SQL = sql.getSql();
            else
                SQL = queryDesigner.SQLText;

            DataTable table = new DataTable();
            DataConnectionBase dataConnection = dataBase.dataBase;
            DbConnection conn = dataConnection.GetConnection();
            try
            {
              dataConnection.OpenConnection(conn);
              using (DbDataAdapter adapter = dataConnection.GetAdapter(SQL, conn, new CommandParameterCollection(null)))
              {
                table.Clear();
                adapter.Fill(table);
              }
            }
            finally
            {
              dataConnection.DisposeConnection(conn);
            }
            queryDesigner.DataSource = table;
        }

        public string GetSql()
        {
            OnGenerateSQL(null, null);
            return queryDesigner.SQLText;
        }
    }
}
