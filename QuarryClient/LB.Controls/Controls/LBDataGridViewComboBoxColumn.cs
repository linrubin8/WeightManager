using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls
{
    public class LBDataGridViewComboBoxColumn:DataGridViewComboBoxColumn
    {
        private string _LBFieldName = "";


        /// <summary>
        /// 常量字段名称
        /// </summary>
        [Localizable(true)]
        public string FieldName
        {
            get
            {
                return _LBFieldName;
            }
            set
            {
                _LBFieldName = value;
            }
        }

        public override object Clone()
        {
            LBDataGridViewComboBoxColumn MyColumn = base.Clone() as LBDataGridViewComboBoxColumn;
            MyColumn.FieldName = this.FieldName;
            return MyColumn;
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }

            set
            {
                base.CellTemplate = value;
            }
        }
    }
}

