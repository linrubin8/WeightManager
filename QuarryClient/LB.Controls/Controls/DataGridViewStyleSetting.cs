using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Controls
{
    public class DataGridViewStyleSetting
    {
        public static void InitDataGridViewStyle(LBDataGridView grdMain)
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();

            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            grdMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            grdMain.BackgroundColor = System.Drawing.SystemColors.Window;
            grdMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            grdMain.ColumnFont = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            grdMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("新宋体", 13F, System.Drawing.FontStyle.Bold);
            grdMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("新宋体", 13F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            grdMain.DefaultCellStyle = dataGridViewCellStyle4;
            grdMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            grdMain.EnableHeadersVisualStyles = false;
            grdMain.Font = new System.Drawing.Font("新宋体", 13F, System.Drawing.FontStyle.Bold);
            grdMain.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            grdMain.HeadFont = new System.Drawing.Font("新宋体", 13F, System.Drawing.FontStyle.Bold);
            grdMain.HeadForeColor = System.Drawing.Color.Empty;
            grdMain.HeadSelectBackColor = System.Drawing.Color.Empty;
            grdMain.HeadSelectForeColor = System.Drawing.Color.Empty;
            grdMain.LineNumberForeColor = System.Drawing.Color.MidnightBlue;
            grdMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            grdMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            grdMain.RowsDefaultCellStyle = dataGridViewCellStyle5;
            grdMain.RowTemplate.Height = 23;
            grdMain.TitleBack = null;
            grdMain.TitleBackColorBegin = System.Drawing.Color.White;
            grdMain.TitleBackColorEnd = System.Drawing.SystemColors.ActiveBorder;
        }
    }
}
