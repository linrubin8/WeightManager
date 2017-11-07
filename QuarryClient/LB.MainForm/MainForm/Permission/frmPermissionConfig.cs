using LB.Controls;
using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LB.MainForm.Permission
{
    public partial class frmPermissionConfig : LBUIPageBase
    {
        TreeNode mtnTop = null;
        private TreeNode mtnSelectedNode = null;
        public frmPermissionConfig()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.grdMain.AutoGenerateColumns = false;
            this.grdMain.LBLoadConst();

            BuildPermissionTree();
            this.grdMain.LBCellButtonClick += GrdMain_LBCellButtonClick;
            this.grdMain.CellBeginEdit += GrdMain_CellBeginEdit;
        }

        private void GrdMain_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (this.mtnSelectedNode!=null && this.mtnSelectedNode.Tag != null)
                {
                    DataRow dr = this.mtnSelectedNode.Tag as DataRow;
                    long lPermissionID = Convert.ToInt64(dr["PermissionID"]);
                    if (lPermissionID == 0)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void GrdMain_LBCellButtonClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.grdMain.Columns[e.ColumnIndex].Name.Equals("BtnDelete"))
                {
                    DataGridViewRow dgvr = this.grdMain.Rows[e.RowIndex];
                    if (dgvr.DataBoundItem != null)
                    {
                        DataRowView drv = dgvr.DataBoundItem as DataRowView;
                        long lPermissionDataID = drv["PermissionDataID"] == DBNull.Value ?
                            0 : Convert.ToInt64(drv["PermissionDataID"]);
                        if (lPermissionDataID > 0)
                        {
                            if (LB.WinFunction.LBCommonHelper.ConfirmMessage("确定删除？", "提示", MessageBoxButtons.YesNo) ==
                                 DialogResult.Yes)
                            {
                                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                                parmCol.Add(new LBParameter("PermissionDataID", enLBDbType.Int64, lPermissionDataID));
                                DataSet dsReturn;
                                Dictionary<string, object> dictValue;
                                try
                                {
                                    ExecuteSQL.CallSP(11012, parmCol, out dsReturn, out dictValue);
                                    LoadPermissionData();//刷新数据
                                }
                                catch (Exception ex)
                                {
                                    dgvr.ErrorText = ex.Message;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
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
                if (this.mtnSelectedNode == null&& this.mtnSelectedNode != mtnTop)
                {
                    throw new Exception("请选择权限分类节点！");
                }
                DataRow dr = this.mtnSelectedNode.Tag as DataRow;
                long lPermissionID = Convert.ToInt64(dr["PermissionID"]);
                if (lPermissionID == 0)
                {
                    throw new Exception("请选择权限分类节点！");
                }

                this.grdMain.CurrentCell = null;
                this.grdMain.EndEdit();
                DataView dvResult = this.grdMain.DataSource as DataView;
                //int iRowIndex = 0;
                StringBuilder strError = new StringBuilder();

                foreach (DataGridViewRow dgvr in this.grdMain.Rows)
                {
                    if (dgvr.DataBoundItem == null)
                        continue;

                    DataRowView drv = dgvr.DataBoundItem as DataRowView;

                    if (drv.Row.RowState != DataRowState.Added &&
                       drv.Row.RowState != DataRowState.Modified)
                    {
                        continue;
                    }

                    long lPermissionDataID = drv["PermissionDataID"] == DBNull.Value ?
                        0 : Convert.ToInt64(drv["PermissionDataID"]);
                    string strPermissionCode = drv["PermissionCode"].ToString().TrimEnd();
                    string strPermissionDataName = drv["PermissionDataName"].ToString().TrimEnd();
                    int iPermissionType = drv["PermissionType"] == DBNull.Value ? 0 : Convert.ToInt16(drv["PermissionType"]);
                    int iPermissionSPType = drv["PermissionSPType"] == DBNull.Value ? 0 : Convert.ToInt16(drv["PermissionSPType"]);
                    int iPermissionViewType= drv["PermissionViewType"] == DBNull.Value ? 0 : Convert.ToInt16(drv["PermissionViewType"]);
                    int iDetailIndex = drv["DetailIndex"] == DBNull.Value ? 0 : Convert.ToInt16(drv["DetailIndex"]);
                    int iForbid = drv["Forbid"] == DBNull.Value ? 0 : Convert.ToInt16(drv["Forbid"]); 
                    string strLogFieldName = drv["LogFieldName"].ToString().TrimEnd();

                    if (strPermissionCode != "" && strPermissionDataName != "")
                    {
                        int iSPType = 11010;//Insert
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();

                        if (lPermissionDataID > 0)
                        {
                            parmCol.Add(new LBParameter("PermissionDataID", enLBDbType.Int64, lPermissionDataID));
                            iSPType = 11011;//Insert
                        }
                        else
                        {
                            parmCol.Add(new LBParameter("PermissionDataID", enLBDbType.Int64, lPermissionDataID, true));
                        }

                        parmCol.Add(new LBParameter("PermissionID", enLBDbType.Int64, lPermissionID));
                        parmCol.Add(new LBParameter("PermissionCode", enLBDbType.String, strPermissionCode));
                        parmCol.Add(new LBParameter("PermissionDataName", enLBDbType.String, strPermissionDataName));
                        parmCol.Add(new LBParameter("PermissionType", enLBDbType.Int16, iPermissionType));
                        parmCol.Add(new LBParameter("PermissionSPType", enLBDbType.Int32, iPermissionSPType));
                        parmCol.Add(new LBParameter("PermissionViewType", enLBDbType.Int32, iPermissionViewType));
                        parmCol.Add(new LBParameter("DetailIndex", enLBDbType.Int32, iDetailIndex));
                        parmCol.Add(new LBParameter("LogFieldName", enLBDbType.String, strLogFieldName));
                        parmCol.Add(new LBParameter("Forbid", enLBDbType.Boolean, iForbid));

                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        try
                        {
                            ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                            dgvr.ErrorText = "";
                            if (dictValue.ContainsKey("PermissionDataID"))
                            {
                                drv["PermissionDataID"] = dictValue["PermissionDataID"];
                            }
                        }
                        catch (Exception ex)
                        {
                            dgvr.ErrorText = ex.Message;
                            strError.AppendLine("第" + (dgvr.Index + 1).ToString() + "行保存失败："+ex.Message);
                        }
                    }
                }
                dvResult.Table.AcceptChanges();
                if (strError.ToString() != "")
                    throw new Exception(strError.ToString());
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #region -- 权限树 --

        private void BuildPermissionTree()
        {
            Dictionary<long, TreeNode> dictTreeNode = new Dictionary<long, TreeNode>();
            if (mtnTop == null)
            {
                mtnTop = new TreeNode("所有权限");
                this.tvPermission.Nodes.Add(mtnTop);
            }

            dictTreeNode.Add(0, mtnTop);

            mtnTop.Nodes.Clear();
            this.mtnSelectedNode = null;
            
            DataTable dtPermission = ExecuteSQL.CallView(103,"","", "ParentPermissionID asc");
            foreach(DataRow dr in dtPermission.Rows)
            {
                long lPermissionID = Convert.ToInt64(dr["PermissionID"]);
                long lParentPermissionID = dr["ParentPermissionID"]==DBNull.Value?
                    0: Convert.ToInt64(dr["ParentPermissionID"]);
                string strPermissionName = dr["PermissionName"].ToString().TrimEnd();
                TreeNode tnChild = new TreeNode(strPermissionName);
                tnChild.Tag = dr;
                if (dictTreeNode.ContainsKey(lParentPermissionID))
                {
                    dictTreeNode[lParentPermissionID].Nodes.Add(tnChild);
                }
                else
                {
                    mtnTop.Nodes.Add(tnChild);
                }
                dictTreeNode.Add(lPermissionID, tnChild);
            }
            this.tvPermission.ExpandAll();
            this.grdMain.DataSource = null;
        }

        #endregion

        #region -- 功能权限清单 --

        private void LoadPermissionData()
        {
            if (this.mtnSelectedNode != null && this.mtnSelectedNode.Tag != null)
            {
                DataRow dr = this.mtnSelectedNode.Tag as DataRow;
                long lPermissionID = Convert.ToInt64(dr["PermissionID"]);
                DataTable dtPermissionData = ExecuteSQL.CallView(104, "", "PermissionID=" + lPermissionID, "DetailIndex asc,PermissionCode asc");
                this.grdMain.DataSource = dtPermissionData.DefaultView;
            }
            else
            {
                this.grdMain.DataSource = null;
            }
        }

        #endregion

        private void tvPermission_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void tvPermission_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    this.mtnSelectedNode = e.Node;

                    e.Node.ContextMenuStrip = this.cmsPermission;
                    this.cmsPermission.Show();
                }
                else
                {
                    this.mtnSelectedNode = e.Node;

                    LoadPermissionData();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnAddPermission_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.mtnSelectedNode != null)
                {
                    long lParentPermissionID = 0;
                    if (this.mtnSelectedNode != mtnTop)
                    {
                        DataRow dr = this.mtnSelectedNode.Tag as DataRow;
                        lParentPermissionID = dr["PermissionID"] == DBNull.Value ?
                            0 : Convert.ToInt64(dr["PermissionID"]);
                    }
                    
                    frmAddPermission frmPermission = new Permission.frmAddPermission(lParentPermissionID,0);
                    frmPermission.ShowDialog();
                    BuildPermissionTree();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnEditPermission_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.mtnSelectedNode != null && this.mtnSelectedNode != mtnTop)
                {
                    DataRow dr = this.mtnSelectedNode.Tag as DataRow;
                    long lPermissionID = dr["PermissionID"] == DBNull.Value ?
                        0 : Convert.ToInt64(dr["PermissionID"]);
                    long lParentPermissionID = dr["ParentPermissionID"] == DBNull.Value ?
                        0 : Convert.ToInt64(dr["ParentPermissionID"]);
                    frmAddPermission frmPermission = new Permission.frmAddPermission(lParentPermissionID, lPermissionID);
                    frmPermission.ShowDialog();

                    BuildPermissionTree();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnDeletePermission_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.mtnSelectedNode != null && this.mtnSelectedNode != mtnTop)
                {
                    DataRow dr = this.mtnSelectedNode.Tag as DataRow;
                    long lPermissionID = dr["PermissionID"] == DBNull.Value ?
                        0 : Convert.ToInt64(dr["PermissionID"]);

                    if (LB.WinFunction.LBCommonHelper.ConfirmMessage("确认删除？", "提示", MessageBoxButtons.YesNo) ==
                         DialogResult.Yes)
                    {
                        LBDbParameterCollection parmCollection = new LBDbParameterCollection();
                        parmCollection.Add(new LBParameter("PermissionID", enLBDbType.Int64, lPermissionID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictOut;
                        ExecuteSQL.CallSP(11003, parmCollection, out dsReturn, out dictOut);
                        BuildPermissionTree();
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnMovePermission_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void tvPermission_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

    }
}
