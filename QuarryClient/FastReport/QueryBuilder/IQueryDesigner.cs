using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;

namespace FastReport.FastQueryBuilder
{
    internal class AddTableEventArgs : EventArgs
    {
        public AddTableEventArgs(Table table, Point position)
        {
            if (table == null)
                new Exception("The table should not be nill");
            this.table = table;
            this.position = position;
        }
        public Table table;
        public Point position;
    }
  
    internal delegate void AddTableEventHandler(object sender, AddTableEventArgs ate);

    internal class AddLinkEventArgs : EventArgs
    {
        public AddLinkEventArgs(Field from, Field to)
        {
            this.FieldFrom = from;
            this.FieldTo = to;
        }
        public Field FieldFrom;
        public Field FieldTo;
    }

    internal delegate void AddLinkEventHandler(object sender, AddLinkEventArgs ate);

    internal class CheckFieldEventArgs : EventArgs
    {
        public CheckFieldEventArgs(Field field)
        {
            if (field == null)
                new Exception("The field should not be nill");
            this.field = field;
        }
        public Field field;
        public bool value;
    }

    internal delegate void CheckFieldEventHandler(object sender, CheckFieldEventArgs ate);
    
    internal interface IQueryDesigner
    {
        event EventHandler OnOk;

        event EventHandler OnCancel;

        event EventHandler OnGetTableList;        

        event AddTableEventHandler OnAddTable;

        event EventHandler OnGenerateSQL;

        event EventHandler OnRunSQL;
    
        void DesignQuery();

        void Close();

        void DoFillTableList(List<Table> tl);

        void DoRefreshLinks();

        TableView DoAddTable(Table table, Point position);

        List<Link> Links
        {
            get;
            set;
        }

        List<Field> Fields
        {
            get;
            set;
        }

        List<Field> Groups
        {
            get;
            set;
        }

        string SQLText
        {
            get;
            set;
        }

        object DataSource
        {
            get;
            set;
        }

    }
}
