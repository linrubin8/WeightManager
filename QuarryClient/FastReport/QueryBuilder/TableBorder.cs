using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FastReport.FastQueryBuilder
{
    internal enum BorderPosition
    {
        None,
        Right,
        Bottom,
        Left,
        Top
    }

    internal class TableBorder
    {
        private UserControl table;
        private BorderPosition border;
        public bool IsResize;

        private void SetCursor()
        {
            table.Cursor = Cursors.Default;
            switch (border)
            {
                case BorderPosition.Top:
                case BorderPosition.Bottom:
                    table.Cursor = Cursors.SizeNS;
                    break;
                case BorderPosition.Left:
                case BorderPosition.Right:
                    table.Cursor = Cursors.SizeWE;
                    break;
            }
        }

        public TableBorder(UserControl tbl)
        {
            table = tbl;
        }

        public BorderPosition SelectedBorder
        {
            get
            {
                return border;
            }
            set
            {
                border = value;
                SetCursor();
            }
        }
    }
}
