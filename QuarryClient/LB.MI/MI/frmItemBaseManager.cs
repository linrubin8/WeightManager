using LB.Controls;
using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LB.Page.Helper;
using LB.Common;

namespace LB.MI
{
    public partial class frmItemBaseManager : LBUIPageBase
    {
        //返回数据按钮是否可见
        public bool NeedReturn
        {
            set
            {
                this.btnReturn.Visible = value;
            }
        }
        public frmItemBaseManager()
        {
            InitializeComponent();
            this.btnReturn.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BuildTree();
            //加载视图配置数据源
            ReSearchData();
            
            btnAddItemType.Click += btnAddItemType_Click;
            btnEditItemType.Click += btnEditItemType_Click;
            btnDeleteItemType.Click += btnDeleteItemType_Click;
            btnAddItemBase.Click += BtnAddItemBase_Click;
            btnEditItemBase.Click += btnEditItemBase_Click;
            btnDeleteItemBase.Click += btnDeleteItemBase_Click;
            btnReflush.Click += btnReflush_Click;
            btnTableSetting.Click += btnTableSetting_Click;
            btnSort.Click += btnSort_Click;
            tvItemType.AfterSelect += this.tvItemType_AfterSelect;
            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;
        }

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataRowView drvSelect = this.grdMain.Rows[e.RowIndex].DataBoundItem as DataRowView;
                    long lItemID = LBConverter.ToInt64(drvSelect["ItemID"]);
                    if (lItemID > 0)
                    {
                        frmItemBase frm = new frmItemBase(lItemID);
                        LBShowForm.ShowDialog(frm);

                        ReSearchData();
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            try
            {
                ReSearchData();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnTableSetting_Click(object sender, EventArgs e)
        {
            try
            {
                ReSearchData();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnReflush_Click(object sender, EventArgs e)
        {
            try
            {
                ReSearchData();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnDeleteItemBase_Click(object sender, EventArgs e)
        {
            try
            {
                ReSearchData();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnEditItemBase_Click(object sender, EventArgs e)
        {
            try
            {
                long lItemID = 0;
                if (grdMain.CurrentRow != null)
                {
                    DataRowView drv = grdMain.CurrentRow.DataBoundItem as DataRowView;
                    lItemID = LBConverter.ToInt64(drv["ItemID"]);
                }
                if (lItemID == 0)
                {
                    return;
                }
                frmItemBase frmIB = new frmItemBase(lItemID);
                LBShowForm.ShowDialog(frmIB);
                ReSearchData();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void BtnAddItemBase_Click(object sender, EventArgs e)
        {
            try
            {
                frmItemBase frmIB = new frmItemBase(0);
                LBShowForm.ShowDialog(frmIB);
                ReSearchData();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnDeleteItemType_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvItemType.SelectedNode == null || tvItemType.SelectedNode.Tag == null)
                {
                    return;
                }
                DataRow dr = tvItemType.SelectedNode.Tag as DataRow;
                long lItemTypeID = LBConverter.ToInt64(dr["ItemTypeID"]);
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认物料类型？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (lItemTypeID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ItemTypeID", enLBDbType.Int64, lItemTypeID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(20102, parmCol, out dsReturn, out dictValue);
                    }
                    BuildTree();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnEditItemType_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvItemType.SelectedNode == null || tvItemType.SelectedNode.Tag == null)
                {
                    return;
                }
                DataRow dr = tvItemType.SelectedNode.Tag as DataRow;
                frmItemType frm = new frmItemType(LBConverter.ToInt64(dr["ItemTypeID"]));
                LBShowForm.ShowDialog(frm);
                BuildTree();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnAddItemType_Click(object sender, EventArgs e)
        {
            try
            {
                frmItemType frm = new frmItemType(0);
                LBShowForm.ShowDialog(frm);
                BuildTree();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ReSearchData();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //this.grdMain.CurrentCell = null;
                //this.grdMain.EndEdit();
                //DataView dvResult = this.grdMain.DataSource as DataView;
                
                //foreach (DataGridViewRow dgvr in this.grdMain.Rows)
                //{
                //    if (dgvr.DataBoundItem == null)
                //        continue;

                //    DataRowView drv = dgvr.DataBoundItem as DataRowView;

                //    if(drv.Row.RowState!= DataRowState.Added &&
                //       drv.Row.RowState != DataRowState.Modified)
                //    {
                //        continue;
                //    }

                //    long lSysViewTypeID = drv["SysViewTypeID"] == DBNull.Value ? 
                //        0 : Convert.ToInt64(drv["SysViewTypeID"]);
                //    string strSysViewType = drv["SysViewType"].ToString().TrimEnd();
                //    string strSysViewName = drv["SysViewName"].ToString().TrimEnd();

                //    if (strSysViewType != "" && strSysViewName != "")
                //    {
                //        int iSPType = 9000;//Insert
                //        LBDbParameterCollection parmCol = new LBDbParameterCollection();

                //        if (lSysViewTypeID > 0)
                //        {
                //            parmCol.Add(new LBParameter("SysViewTypeID", enLBDbType.Int64, lSysViewTypeID));
                //            iSPType = 9001;//Update
                //        }
                //        else
                //        {
                //            parmCol.Add(new LBParameter("SysViewTypeID", enLBDbType.Int64, lSysViewTypeID,true));
                //        }

                //        parmCol.Add(new LBParameter("SysViewType", enLBDbType.String, strSysViewType));
                //        parmCol.Add(new LBParameter("SysViewName", enLBDbType.String, strSysViewName));

                //        DataSet dsReturn;
                //        Dictionary<string, object> dictValue;
                //        try
                //        {
                //            ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                //            dgvr.ErrorText = "";
                //            if (dictValue.ContainsKey("SysViewTypeID"))
                //            {
                //                drv["SysViewTypeID"] = dictValue["SysViewTypeID"];
                //            }
                //        }
                //        catch(Exception ex)
                //        {
                //            dgvr.ErrorText = ex.Message;
                //        }
                //    }
                //}
                //dvResult.Table.AcceptChanges();
                //LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void ReSearchData()
        {
            string strFilter = "";
            string strSQL = "select * from dbo.Db_v_ItemBase";
            if (tvItemType.SelectedNode != null && tvItemType.SelectedNode.Parent != null)
            {
                DataRow dr = tvItemType.SelectedNode.Tag as DataRow;
                strFilter = "ItemTypeID = "+ dr["ItemTypeID"].ToString();
            }
            if (this.txtFilter.Text.TrimEnd() != "")
            {
                strFilter += string.Format(@"ItemTypeName like '%{0}%'
or ItemCode like '%{0}%'
or ItemName like '%{0}%'
or ItemMode like '%{0}%'
or UOMName like '%{0}%'
or Description like '%{0}%'", this.txtFilter.Text.TrimEnd());
            }
            if (strFilter != "")
            {
                strSQL += " where " + strFilter;
            }

            DataTable dtView = ExecuteSQL.CallDirectSQL(strSQL);
            this.grdMain.DataSource = dtView.DefaultView;
        }

        private void BuildTree()
        {
            tvItemType.Nodes.Clear();
            string strSQL = "select ItemTypeID,ItemTypeName from dbo.DbItemType";
            DataTable dt = ExecuteSQL.CallDirectSQL(strSQL);
            TreeNode tnTop = new TreeNode("物料分类");
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = new TreeNode(dr["ItemTypeName"].ToString().TrimEnd());
                tn.Tag = dr;
                tnTop.Nodes.Add(tn);
            }
            tvItemType.Nodes.Add(tnTop);
            tnTop.ExpandAll();
        }

        private void tvItemType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                ReSearchData();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #region -- 返回数据行 --
        public List<DataRow> LstReturn = new List<DataRow>();

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                this.grdMain.EndEdit();
                this.grdMain.CurrentCell = null;
                foreach(DataGridViewRow dgvr in this.grdMain.Rows)
                {
                    bool bolSelected = LBConverter.ToBoolean(dgvr.Cells["LBSelected"].Value);
                    if (bolSelected)
                    {
                        DataRowView drv = dgvr.DataBoundItem as DataRowView;
                        LstReturn.Add(drv.Row);
                    }
                }

                if (LstReturn.Count == 0)
                {
                    throw new Exception("请选择有效的物料行！");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- 返回数据行 --
    }
}
