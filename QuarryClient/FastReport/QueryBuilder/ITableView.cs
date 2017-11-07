using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FastReport.FastQueryBuilder
{
    internal enum LinkPosition 
    {
        Left = 1,
        Right = 2
    }

    internal interface ITableView
    {
        event EventHandler OnChangeAlias;

        event CheckFieldEventHandler OnSelectField;

        event AddLinkEventHandler OnAddLink;

        event AddTableEventHandler OnDeleteTable;

        Table Table
        {
            get;
            set;
        }

        void SetTableName(string tableName);

        void SetTabeleAlias();

        void DoAddLink();

        Point GetPosition(Field field, LinkPosition lp);
        int GetLeft();
        int GetWidth();
    }
}
