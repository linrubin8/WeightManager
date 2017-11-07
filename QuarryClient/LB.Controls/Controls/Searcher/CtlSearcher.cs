using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Controls.Searcher
{
    public partial class CtlSearcher : UserControl
    {
        private DataGridView mgrdMain;
        public CtlSearcher()
        {
            InitializeComponent();
            

            this.txtColumnName.SelectedIndexChanged += TxtColumnName_SelectedIndexChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.skinLabel1.Top = 9;
            this.lblSearch.Top = 9;
            this.txtColumnName.Top = 9;
            this.txtSearchDropDown.Top = 9;
            this.txtSearchText.Top = 9;
            this.txtSearchText.Size = new Size(this.txtSearchText.Width,28);
            this.skinLabel1.Height = 32;
            this.lblSearch.Height = 32;
            this.Height = 47;
        }

        private void TxtColumnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtColumnName.SelectedItem != null)
                {
                    this.txtSearchDropDown.Visible = false;
                    this.txtSearchText.Visible = false;

                    DataRowView drItem = this.txtColumnName.SelectedItem as DataRowView;
                    string strColumnName = drItem["ColumnName"].ToString().TrimEnd();

                    DataGridViewColumn dgvc = this.mgrdMain.Columns[strColumnName];
                    if (dgvc is DataGridViewCheckBoxColumn)
                    {
                        this.chkIsCheck.Visible = true;
                        this.chkIsCheck.Location = new Point(this.lblSearch.Right + 10, this.chkIsCheck.Top);
                    }
                    else if (dgvc is LBDataGridViewComboBoxColumn)
                    {
                        LBDataGridViewComboBoxColumn lbColumn = dgvc as LBDataGridViewComboBoxColumn;
                        this.txtSearchDropDown.Visible = true;
                        if (lbColumn.DataSource != null)
                        {
                            this.txtSearchDropDown.DataSource = lbColumn.DataSource;
                            this.txtSearchDropDown.DisplayMember = lbColumn.DisplayMember;
                            this.txtSearchDropDown.ValueMember = lbColumn.ValueMember;
                            this.txtSearchDropDown.Location = new Point(this.lblSearch.Right + 10, this.txtSearchDropDown.Top);
                        }
                    }
                    else
                    {
                        this.txtSearchText.Visible = true;
                        this.txtSearchText.Location = new Point(this.lblSearch.Right + 10, this.txtSearchText.Top);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        public void SetGridView(DataGridView grdMain, string DefaultColumnName)
        {
            mgrdMain = grdMain;
            DataTable dtDataSource = null;
            if (grdMain.DataSource is DataTable)
            {
                dtDataSource = grdMain.DataSource as DataTable;
            }
            else if (grdMain.DataSource is DataView)
            {
                dtDataSource = (grdMain.DataSource as DataView).Table;
            }

            DataTable dtColumn = new DataTable();
            dtColumn.Columns.Add("ColumnTitle", typeof(string));
            dtColumn.Columns.Add("ColumnName", typeof(string));

            if (dtDataSource != null)
            {
                bool bolExistsColumnName = false;
                foreach (DataGridViewColumn dgvc in grdMain.Columns)
                {
                    string strColumnName = dgvc.Name;
                    string strColumnTitle = dgvc.HeaderText;

                    if (dgvc.Visible && dtDataSource.Columns.Contains(strColumnName))
                    {
                        DataRow drNew = dtColumn.NewRow();
                        drNew["ColumnTitle"] = strColumnTitle;
                        drNew["ColumnName"] = strColumnName;
                        dtColumn.Rows.Add(drNew);
                        dtColumn.AcceptChanges();

                        if (strColumnName.Equals(DefaultColumnName))
                        {
                            bolExistsColumnName = true;
                        }
                    }
                }

                dtColumn.DefaultView.Sort = "ColumnTitle asc";
                this.txtColumnName.DataSource = dtColumn.DefaultView;
                this.txtColumnName.DisplayMember = "ColumnTitle";
                this.txtColumnName.ValueMember = "ColumnName";

                if (bolExistsColumnName)
                {
                    int iSelectedIndex = -1;
                    int iRowIndex = 0;
                    foreach (DataRowView drv in dtColumn.DefaultView)
                    {
                        if (drv["ColumnName"].ToString().Equals(DefaultColumnName))
                        {
                            iSelectedIndex = iRowIndex;
                            break;
                        }
                        iRowIndex++;
                    }

                    if (iSelectedIndex >= 0)
                    {
                        this.txtColumnName.SelectedIndex = iSelectedIndex;
                    }
                }
            }
        }

        /// <summary>
        /// 设置默认过滤条件
        /// </summary>
        /// <param name="strColumnName"></param>
        /// <param name="strValue"></param>
        public void SetDefaultFilter(string strColumnName,string strValue)
        {
            if (this.mgrdMain.Columns.Contains(strColumnName))
            {
                SetGridView(mgrdMain, strColumnName);
                this.txtSearchText.Text = strValue;
            }
        }

        public string GetFilter()
        {
            string strFilter = "";
            if (this.txtColumnName.SelectedItem != null&& this.txtSearchText.Text.TrimEnd()!="")
            {
                DataRowView drItem = this.txtColumnName.SelectedItem as DataRowView;
                string strColumnName = drItem["ColumnName"].ToString().TrimEnd();

                DataGridViewColumn dgvc = this.mgrdMain.Columns[strColumnName];
                if (dgvc is DataGridViewCheckBoxColumn)
                {
                    strFilter = strColumnName + "=" + (this.chkIsCheck.Checked ? "1" : "0");
                }
                else if (dgvc is LBDataGridViewComboBoxColumn)
                {
                    LBDataGridViewComboBoxColumn lbColumn = dgvc as LBDataGridViewComboBoxColumn;
                    this.txtSearchDropDown.Visible = true;
                    if (lbColumn.DataSource != null)
                    {
                        if (drItem.DataView.Table.Columns[strColumnName].DataType == typeof(int)||
                            drItem.DataView.Table.Columns[strColumnName].DataType == typeof(long)
                            )
                        {
                            if (this.txtSearchDropDown.SelectedValue != null)
                            {
                                strFilter = strColumnName + " = " + this.txtSearchDropDown.SelectedValue;
                            }
                        }
                        else
                        {
                            if (this.txtSearchDropDown.SelectedText != null)
                            {
                                strFilter = strColumnName + " like '%" + this.txtSearchDropDown.SelectedText + "%'";
                            }
                        }
                    }
                }
                else
                {
                    strFilter = strColumnName + " like '%" + this.txtSearchText.Text + "%'";
                }
            }
            return strFilter;
        }
    }
}
