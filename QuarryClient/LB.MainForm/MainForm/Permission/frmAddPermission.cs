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
    public partial class frmAddPermission : LBForm
    {
        private long mParentPermissionID;
        private long mPermissionID;
        public frmAddPermission(long lParentPermissionID,long lPermissionID)
        {
            mParentPermissionID = lParentPermissionID;
            mPermissionID = lPermissionID;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadPermission();
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
                SavePermission();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        /// <summary>
        /// 查询权限名称
        /// </summary>
        private void LoadPermission()
        {
            DataTable dtPerm = ExecuteSQL.CallView(103, "*", "PermissionID=" + this.mPermissionID.ToString(), "");
            if (dtPerm.Rows.Count > 0)
            {
                this.txtPermissionName.Text = dtPerm.Rows[0]["PermissionName"].ToString().TrimEnd();
            }
        }

        private void SavePermission()
        {
            if (this.txtPermissionName.Text == "")
            {
                throw new Exception("权限名称不能为空！");
            }

            int iSPType = 0;

            if (mPermissionID == 0)
            {
                iSPType = 11001;//InsertPermission
            }
            else
            {
                iSPType = 11002;//UpdatePermission
            }

            DataTable dtSPIN = new DataTable();
            dtSPIN.Columns.Add("PermissionID", typeof(long));
            dtSPIN.Columns.Add("PermissionName", typeof(string));
            dtSPIN.Columns.Add("ParentPermissionID", typeof(long));
            DataRow drNew = dtSPIN.NewRow();
            if(mPermissionID>0)
                drNew["PermissionID"] = mPermissionID;
            drNew["PermissionName"] = this.txtPermissionName.Text.TrimEnd();
            if(mParentPermissionID>0)
                drNew["ParentPermissionID"] = mParentPermissionID;
            dtSPIN.Rows.Add(drNew);

            DataSet dsReturn;
            DataTable dtResult;
            ExecuteSQL.CallSP(iSPType, dtSPIN, out dsReturn, out dtResult);
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                long.TryParse(dtResult.Rows[0]["PermissionID"].ToString(), out mPermissionID);
            }
            LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
        }
    }
}
