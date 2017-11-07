using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FastReport.Data;

namespace FastReport.FastQueryBuilder
{
    internal class QueryBuilder
    {
        private Core core;

        public QueryBuilder(DataConnectionBase dcb)
        {
            core = new Core(new QueryDesigner(), new DataBase(dcb));
        }
    
        public DialogResult DesignQuery()
        {
            return core.DesignQuery();
        }

        public string GetSql()
        {
            return core.GetSql();
        }

        public bool UseJoin
        {
            get { return core.UseJoin; }
            set { core.UseJoin = value; }
        }
    }
}
