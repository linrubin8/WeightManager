using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using FastReport.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;
using System.ComponentModel;
using FastReport.Utils;

namespace FastReport.FastQueryBuilder
{

    internal static class QueryEnums
    {
        public enum JoinTypes { Where, InnerJoin, LeftOuterJoin, RightOuterJoin, FullOuterJoin };
        public static string[] JoinTypesToStr = {"WHERE", "INNER JOIN", 
                "LEFT OUTER JOIN", "RIGHT OUTER JOIN", "FULL OUTER JOIN"};

        public enum WhereTypes { Equal, NotEqual, GreaterOrEqual, Greater, LessOrEqual, Less, Like, NotLike }
        public static string[][] WhereTypesToStr =  {
           new string[] {"=", "<>", ">=", ">", "<=", "<", "LIKE", "NOT LIKE"},
           new string[] {"=",       "<>",          ">=",                ">",         "<=",             "<",      "LIKE", "NOT LIKE"}};
    }
    
    enum SqlFunc { Avg, Count, Min, Max, Sum };

    enum SortTypes { Asc, Desc };

    /// <summary>
    /// For internal use only.
    /// </summary>
    public class Field
    {
        internal Field()
        {
        }

        private string _name;
        /// <summary>
        /// For internal use only.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _alias = string.Empty;
        /// <summary>
        /// For internal use only.
        /// </summary>
        public string Alias
        {
            get { return _alias; }
            set { _alias = value.Trim(); 
            }
        }
     
        private string _filter = string.Empty;
        /// <summary>
        /// For internal use only.
        /// </summary>
        public string Filter
        {
            get { return _filter; }
            set 
            {
                _filter = value.Trim();
                if (_filter == string.Empty)
                    return;

                Regex reg = new Regex("^(=|<|>).*");
                if (reg.IsMatch(_filter))
                    return;
                else
                {
                    if (this.FieldType == _filter.GetType()) // if string
                        _filter = "'" + _filter + "'";
                    _filter = '=' + _filter;
                }
            }
        }

        private bool _group = false;
        /// <summary>
        /// For internal use only.
        /// </summary>
        public bool Group
        {
            get { return _group; }
            set { _group = value;}
        }

        private string _order;
        /// <summary>
        /// For internal use only.
        /// </summary>
        public string Order
        {
            get { return _order; }
            set { _order = value; }
        }

        private string _func;
        /// <summary>
        /// For internal use only.
        /// </summary>
        public string Func
        {
            get { return _func; }
            set { _func = value;}
        }

        internal Type FieldType;
        internal Table Table;

        internal string FullName
        {
            get { return Table.Name + "." + Name; }
        }

        internal string getFullName(string quote)
        {
            if (Name.IndexOfAny(" ~`!@#$%^&*()-+=[]{};:'\",.<>/?\\|".ToCharArray()) != -1)
                return Table.Alias + "." + quote[0] + Name + quote[1];
            else
                return Table.Alias + "." + Name;
        }

        internal string getFullName(string quote, bool printName)
        {
            if (printName)
                return getFullName(quote);
            else
                return Table.Alias + "." + Name;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name + "(" + FieldType.Name + ")";
        }
    }

    internal class Table : ICloneable
    {
        private string name;
        private string alias;
        private TableDataSource originalTable;
        private List<Field> fieldList;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                if (Alias == "" || Alias == null)
                    Alias = name;
            }
        }

        public string Alias
        {
            get
            {
                return alias;
            }
            set
            {
                alias = value;
            }
        }

        public ITableView tableView;
        public List<Field> FieldList
        {
            get
            {
                if (fieldList.Count == 0)
                {
                    if (originalTable.Columns.Count == 0)
                        originalTable.InitSchema();
                  
                    foreach (Column clm in originalTable.Columns)
                    {
                        Field fld = new Field();
                        fld.Name = clm.Name;
                        fld.FieldType = clm.DataType;
                        fld.Table = this;
                        fieldList.Add(fld);
                    }
                }
                return fieldList;
            }
        }

        public Table(TableDataSource tbl)
        {
            originalTable = tbl;
            Name = tbl.TableName;
            fieldList = new List<Field>();
        }
        public override string ToString()
        {
            return Name;
        }

        public string getFullName(string quote)
        {
            if (name.Contains(" "))
              return quote[0] + Name + quote[1];
            else
              return Name;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new Table(originalTable);
        }

        #endregion

        internal string getNameAndAlias()
        {
            return Name + ' ' + Alias;
        }

        internal string getFromName(string quote)
        {
            if (!Name.Contains(quote[0].ToString()))
                return quote[0] + Name + quote[1] + " " + Alias;
            return Name + " " + Alias;
        }

        internal string getFromName(string quote, bool printName)
        {
            if (printName)
                return getFromName(quote);
            else
                return Alias;
        }
    }

    /// <summary>
    /// For internal use only.
    /// </summary>
    public class Link
    {              
        internal Link(Field from, Field to)
        {
            this.From = from;
            this.To = to;
            
        }
        internal Field From;
        internal Field To;

        internal QueryEnums.JoinTypes Join;
        internal QueryEnums.WhereTypes Where;

        /// <summary>
        /// For internal use only.
        /// </summary>
        public string Editor
        {
            get { return Res.Get("Forms,QueryBuilder,Change"); }
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        public string Delete
        {
          get { return Res.Get("Forms,QueryBuilder,Delete"); }
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        public string Name
        {
            get { return From.FullName + " JOIN " + To.FullName; }
        }

        internal void Paint(Control cntr)
        {
            LinkPosition lp1, lp2;

            int cp1 = From.Table.tableView.GetLeft() + From.Table.tableView.GetWidth() / 2;
            int cp2 = To.Table.tableView.GetLeft() + To.Table.tableView.GetWidth() / 2;

            if (cp1 > cp2)
            {
                if ((From.Table.tableView.GetLeft()) < To.Table.tableView.GetLeft() + To.Table.tableView.GetWidth())
                {
                    lp1 = LinkPosition.Right;                    
                    lp2 = LinkPosition.Right;
                }
                else
                {
                    lp1 = LinkPosition.Left;
                    lp2 = LinkPosition.Right;
                }
            }
            else
            {
                if ((From.Table.tableView.GetLeft() + From.Table.tableView.GetWidth()) > To.Table.tableView.GetLeft())
                {
                    lp2 = LinkPosition.Left;
                    lp1 = LinkPosition.Left;
                }
                else
                {
                    lp2 = LinkPosition.Left;
                    lp1 = LinkPosition.Right;
                }
            }

            Point pnt1 = cntr.PointToClient(From.Table.tableView.GetPosition(this.From, lp1));
            Point pnt2 = cntr.PointToClient(To.Table.tableView.GetPosition(this.To, lp2));
            Graphics g2 = cntr.CreateGraphics();
            g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            
            using (Pen pen = new Pen(Color.Black))
            {
                g2.DrawLine(pen, pnt1, pnt2);
                
                Point pnt3 = (lp1 == LinkPosition.Left) ? 
                    new Point(pnt1.X + 8, pnt1.Y) : new Point(pnt1.X - 8, pnt1.Y);
                g2.DrawLine(pen, pnt1, pnt3);

                pnt3 = (lp2 == LinkPosition.Left) ?
                    new Point(pnt2.X + 8, pnt2.Y) : new Point(pnt2.X - 8, pnt2.Y);
                g2.DrawLine(pen, pnt2, pnt3);
            }    
        }
    }

    internal class Query
    {
        public List<Table> TableList = new List<Table>();
        public List<Link>  LinkList  = new List<Link>();
        public List<Field> SelectedFields = new List<Field>();
        public List<Field> GroupedFields = new List<Field>();

        public void deleteTable(Table table)
        {
            for (int i = LinkList.Count - 1; i >= 0; i--)
            {
                if ((LinkList[i].From.Table == table) || (LinkList[i].To.Table == table))
                    LinkList.RemoveAt(i);
            }
            TableList.Remove(table);
        }
    }
}
