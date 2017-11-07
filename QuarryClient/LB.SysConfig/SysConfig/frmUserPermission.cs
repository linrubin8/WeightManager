using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LB.WinFunction;
using LB.Controls;
using System.Xml;
using System.Xml.Serialization;

namespace LB.SysConfig
{
    public partial class frmUserPermission : LBUIPageBase
    {
        private TreeNode mtnTop = null;
        private TreeNode mtnSelectedNode = null;
        private long mlUserID = 0;
        private DataTable mdtUserPermissionData = null;
        private DataTable mdtUserPermission = null;

        public frmUserPermission(long lUserID)
        {
            InitializeComponent();
            this.chkPermissionAll.CheckedChanged += ChkPermissionAll_CheckedChanged;
            this.tvPermission.NodeMouseClick += TvPermission_NodeMouseClick;
            mlUserID = lUserID;
        }

        private void TvPermission_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                this.mtnSelectedNode = e.Node;

                if (this.mtnSelectedNode != null && this.mtnSelectedNode.Tag != null)
                {
                    DataRow drPermission = this.mtnSelectedNode.Tag as DataRow;
                    long lPermissionID = Convert.ToInt64(drPermission["PermissionID"]);

                    mdtUserPermissionData.DefaultView.RowFilter = "isnull(Forbid,0)=0 and PermissionID=" + lPermissionID;

                    mdtUserPermission.DefaultView.RowFilter = "PermissionID=" + lPermissionID;
                    if (mdtUserPermission.DefaultView.Count > 0)
                    {
                        DataRowView drv = mdtUserPermission.DefaultView[0];
                        bool bolHasPermission = drv["HasPermission"] == DBNull.Value ?
                            false : Convert.ToBoolean(drv["HasPermission"]);
                        this.chkPermissionAll.Checked = bolHasPermission;
                    }
                }
                else
                {
                    mdtUserPermissionData.DefaultView.RowFilter = "1=0";
                    this.chkPermissionAll.Checked = false;
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void ChkPermissionAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                PermissionAll();

                if (this.mtnSelectedNode != null && this.mtnSelectedNode.Tag != null)
                {
                    DataRow drPermission = this.mtnSelectedNode.Tag as DataRow;
                    long lPermissionID = Convert.ToInt64(drPermission["PermissionID"]);

                    mdtUserPermission.DefaultView.RowFilter = "PermissionID=" + lPermissionID;
                    if (mdtUserPermission.DefaultView.Count > 0)
                    {
                        DataRowView drv = mdtUserPermission.DefaultView[0];
                        drv["HasPermission"] = this.chkPermissionAll.Checked ? 1 : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            mdtUserPermissionData = GetUserPermissionData();
            mdtUserPermission = GetUserPermission();

            mdtUserPermissionData.DefaultView.RowFilter = "1=0";
            mdtUserPermissionData.DefaultView.Sort = "DetailIndex asc";
            this.grdMain.DataSource = mdtUserPermissionData.DefaultView;

            BuildPermissionTree();
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

            DataTable dtPermission = ExecuteSQL.CallView(103, "", "isnull(Forbid,0)=0", "ParentPermissionID asc");
            foreach (DataRow dr in dtPermission.Rows)
            {
                long lPermissionID = Convert.ToInt64(dr["PermissionID"]);
                long lParentPermissionID = dr["ParentPermissionID"] == DBNull.Value ?
                    0 : Convert.ToInt64(dr["ParentPermissionID"]);
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
            //this.grdMain.DataSource = null;
        }

        /// <summary>
        /// 读取用户权限设置
        /// </summary>
        /// <returns></returns>
        private DataTable GetUserPermissionData()
        {
            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("UserID", enLBDbType.Int64, mlUserID));
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(13100, parmCol, out dsReturn, out dictValue);
            if (dsReturn != null && dsReturn.Tables.Count > 0)
            {
                return dsReturn.Tables[0];
            }
            return null;
        }

        private DataTable GetUserPermission()
        {
            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("UserID", enLBDbType.Int64, mlUserID));
            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(13101, parmCol, out dsReturn, out dictValue);
            if (dsReturn != null && dsReturn.Tables.Count > 0)
            {
                return dsReturn.Tables[0];
            }
            return null;
        }
        #endregion

        #region -- 判断ChkPermissionAll是否勾选，如果是，列表只读，并且勾选所有权限 --

        private void PermissionAll()
        {
            if (this.chkPermissionAll.Checked)
            {
                foreach (DataGridViewRow dgvr in this.grdMain.Rows)
                {
                    dgvr.Cells["HasPermission"].Value = 1;
                }
                this.grdMain.ReadOnly = true;
            }
            else
            {
                this.grdMain.ReadOnly = false;
            }
        }

        #endregion

        //保存授权信息
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.mdtUserPermission != null && this.mdtUserPermissionData != null)
                {
                    this.grdMain.CurrentCell = null;
                    this.grdMain.EndEdit();
                    this.mdtUserPermission.TableName = "UserPermission";
                    this.mdtUserPermissionData.TableName = "UserPermissionData";

                    DataTable dtSPIN = new DataTable();
                    dtSPIN.Columns.Add("UserID", typeof(long));
                    dtSPIN.Columns.Add("DTUserPermission", typeof(DataTable));
                    dtSPIN.Columns.Add("DTUserPermissionData", typeof(DataTable));

                    DataRow drNew = dtSPIN.NewRow();
                    drNew["UserID"] = mlUserID;
                    drNew["DTUserPermission"] = this.mdtUserPermission.Copy();
                    drNew["DTUserPermissionData"] = this.mdtUserPermissionData.Copy();
                    dtSPIN.Rows.Add(drNew);
                    dtSPIN.AcceptChanges();

                    DataTable dtReturn;
                    DataSet dsReturn;
                    ExecuteSQL.CallSP(13102, dtSPIN, out dsReturn, out dtReturn);

                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private static string SerializeDataTableXml(DataTable pDt)
        {
            // 序列化DataTable
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            serializer.Serialize(writer, pDt);
            writer.Close();

            return sb.ToString();
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
    }
}
