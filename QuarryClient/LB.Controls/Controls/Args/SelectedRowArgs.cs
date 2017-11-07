using LB.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Controls.Args
{
    public class SelectedRowArgs
    {
        public delegate void SelectedRowEventHandle(SelectedRowArgs e);

        private DataRow _SelectedRow;
        public DataRow SelectedRow
        {
            get
            {
                return _SelectedRow;
            }
            set
            {
                _SelectedRow = value;
            }
        }

        private enBaseInfoType _BaseInfoType = enBaseInfoType.None;
        public enBaseInfoType BaseInfoType
        {
            get
            {
                return _BaseInfoType;
            }
            set
            {
                _BaseInfoType = value;
            }
        }

        public SelectedRowArgs(enBaseInfoType eBaseInfoType, DataRow drSelectedRow)
        {
            BaseInfoType = eBaseInfoType;
            SelectedRow = drSelectedRow;
        }
    }
}
