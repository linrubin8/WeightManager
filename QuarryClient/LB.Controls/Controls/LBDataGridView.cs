using CCWin.SkinControl;
using LB.Common;
using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls
{
    public partial class LBDataGridView : SkinDataGridView
    {
        public event DataGridViewCellEventHandler LBCellButtonClick;

        public LBDataGridView()
        {
            InitializeComponent();
            LBInitializeComponent();
        }

        public LBDataGridView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            LBInitializeComponent();
        }

        private void LBInitializeComponent()
        {
            this.CellClick += LBDataGridView_CellClick;
            this.CellPainting += LBDataGridView_CellPainting;

        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    //e.Handled = true;
                    //Rectangle rect = new Rectangle(e.CellBounds.X+1, e.CellBounds.Y+1, e.CellBounds.Width-2, e.CellBounds.Height-1);
                    //Brush brush = new SolidBrush(e.CellStyle.BackColor);
                    //e.Graphics.FillRectangle(brush, rect);

                    Color cellColor = e.CellStyle.BackColor;
                    if (this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        cellColor = e.CellStyle.SelectionBackColor;
                    }

                    using (LinearGradientBrush brush = new LinearGradientBrush(e.CellBounds, cellColor,
                        cellColor, LinearGradientMode.ForwardDiagonal))
                    {
                        e.Graphics.FillRectangle(brush, e.CellBounds);
                        Rectangle border = e.CellBounds;
                        border.Offset(new Point(-1, -1));
                        e.Graphics.DrawRectangle(Pens.Gray, border);
                        bool bolIsHidden = ValidateCurrentCellValueIsHidden(this.Columns[e.ColumnIndex].Name, e.RowIndex);
                        if (!bolIsHidden)
                        {
                            if (this.Columns[e.ColumnIndex].DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
                            {
                                string strCellValue = e.Value == null ? "" : e.Value.ToString();
                                if (this.Columns[e.ColumnIndex].ValueType == typeof(decimal))
                                {
                                    decimal value;
                                    decimal.TryParse(strCellValue, out value);
                                    strCellValue = value.ToString(this.Columns[e.ColumnIndex].DefaultCellStyle.Format);
                                }
                                else if (this.Columns[e.ColumnIndex].ValueType == typeof(long)|| this.Columns[e.ColumnIndex].ValueType == typeof(int))
                                {
                                    long value;
                                    long.TryParse(strCellValue, out value);
                                }

                                if (strCellValue != "")
                                {
                                    SizeF fontSize = e.Graphics.MeasureString(strCellValue, this.Font);
                                    if (e.CellBounds.Width > fontSize.Width)
                                    {
                                        Rectangle rectFont = new Rectangle((int)(e.CellBounds.X + e.CellBounds.Width - fontSize.Width), e.CellBounds.Y,
                                            (int)(fontSize.Width), (int)(fontSize.Height));
                                        
                                        Brush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
                                        e.Graphics.DrawString(strCellValue, e.CellStyle.Font, fontBrush,
                                            new PointF((int)(e.CellBounds.X + e.CellBounds.Width - fontSize.Width-10), e.CellBounds.Y+(e.CellBounds.Height- fontSize.Height)/2));
                                        //e.PaintContent(rectFont);
                                    }
                                    else
                                    {
                                        e.PaintContent(e.CellBounds);
                                    }
                                }
                                else
                                {
                                    e.PaintContent(e.CellBounds);
                                }
                                //Rectangle rectFont = 
                            }
                            else
                            {
                                e.PaintContent(e.CellBounds);
                            }
                        }
                    }

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                //LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void LBDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        private void LBDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (this[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell)
                    {
                        DataGridViewColumn dc = this.Columns[e.ColumnIndex];
                        if (dc is LBDataGridViewButtonColumn)
                        {
                            LBDataGridViewButtonColumn buttonColumn = dc as LBDataGridViewButtonColumn;
                            if (buttonColumn.LBPermissionCode != "")
                            {
                                try
                                {
                                    LBPermission.VerifyUserPermission(buttonColumn.Text,buttonColumn.LBPermissionCode);
                                }
                                catch (Exception ex)
                                {
                                    LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
                                    return;
                                }
                            }
                        }
                        if (LBCellButtonClick != null)
                            LBCellButtonClick(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        public void LBLoadConst()
        {
            foreach(DataGridViewColumn dc in this.Columns)
            {
                if(dc is LBDataGridViewComboBoxColumn)
                {
                    LBDataGridViewComboBoxColumn lbComboBox = dc as LBDataGridViewComboBoxColumn;
                    if(lbComboBox.FieldName!=""&& lbComboBox.FieldName != null)
                    {
                        try
                        {
                            lbComboBox.DataSource = GetConstData(lbComboBox.FieldName);
                            lbComboBox.DisplayMember = "ConstText";
                            lbComboBox.ValueMember = "ConstValue";
                            lbComboBox.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        private static DataTable GetConstData(string strFieldName)
        {
            DataTable dtConst = ExecuteSQL.CallView(101, "ConstValue,ConstText", "FieldName='" + strFieldName + "'", "");
            return dtConst;
        }

        private List<string> mlstHiddelValidateColumn = new List<string>();
        /// <summary>
        /// 隐藏相同信息的列值
        /// </summary>
        public void HiddenSaveColumnValue(params string[] columns)
        {
            List<string> lstColumns = new List<string>();
            foreach(string strColumn in columns)
            {
                if (this.Columns.Contains(strColumn))
                {
                    lstColumns.Add(strColumn);
                    mlstHiddelValidateColumn.Add(strColumn);
                }
            }

            //bool bolIsFirst = true;//是否第一个匹配行
            string strFirshValue = "";
            foreach (DataGridViewRow dgvr in this.Rows)
            {
                string strKeyValue = "";
                foreach (string strColunn in lstColumns)
                {
                    if (strKeyValue != "")
                        strKeyValue += ";";
                    strKeyValue += dgvr.Cells[strColunn].Value.ToString().TrimEnd();
                }
                if(strFirshValue!= strKeyValue)
                {
                    strFirshValue = strKeyValue;
                    //bolIsFirst = true;
                }
                else
                {
                    foreach (string strColunn in lstColumns)
                    {
                        //dgvr.Cells[strColunn].Value = DBNull.Value;
                    }
                }
            }
        }

        private bool ValidateCurrentCellValueIsHidden(string strColumnName,int iRowIndex)
        {
            bool bolIsSame = false;
            if (iRowIndex>0 && mlstHiddelValidateColumn.Contains(strColumnName))
            {
                bool bolIsAllSame = true;
                int iPreRowIndex = iRowIndex - 1;
                foreach(string strName in mlstHiddelValidateColumn)
                {
                    if(this.Rows[iRowIndex].Cells[strName].Value.ToString() != this.Rows[iPreRowIndex].Cells[strName].Value.ToString())
                    {
                        bolIsAllSame = false;
                        break;
                    }
                }

                if (bolIsAllSame)
                {

                }
                bolIsSame = bolIsAllSame;
            }
            return bolIsSame;
        }
    }
}
