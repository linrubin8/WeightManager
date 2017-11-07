using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls
{
    public class LBDataGridViewButtonColumn: DataGridViewButtonColumn
    {
        private string _LBPermissionCode = "";
        /// <summary>
        /// 权限校验码
        /// </summary>
        [Description("权限校验码")]
        public string LBPermissionCode
        {
            get
            {
                return _LBPermissionCode;
            }
            set
            {
                _LBPermissionCode = value;
            }
        }

        public LBDataGridViewButtonColumn()
        {
            this.CellTemplate = new LBDataGridViewButtonCell();
        }

        public override object Clone()
        {
            LBDataGridViewButtonColumn MyColumn = base.Clone() as LBDataGridViewButtonColumn;
            MyColumn.LBPermissionCode = this.LBPermissionCode;
            return MyColumn;
        }
    }
}
