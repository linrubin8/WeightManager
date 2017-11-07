using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using LB.WinFunction;
using LB.Page.Helper;
using LB.Common;

namespace LB.SysConfig
{
    public partial class frmBackUpConfig : LBUIPageBase
    {
        public frmBackUpConfig()
        {
            InitializeComponent();
            this.grdMain.DataError += delegate (object sender, DataGridViewDataErrorEventArgs e) { };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.grdMain.AutoGenerateColumns = false;
            this.grdMain.LBLoadConst();
            LoadBackUpConfig();

            this.grdMain.CellDoubleClick += GrdMain_CellDoubleClick;
        }

        private void GrdMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataRowView drvSelect = this.grdMain.Rows[e.RowIndex].DataBoundItem as DataRowView;
                    long lBackUpConfigID = drvSelect["BackUpConfigID"]==DBNull.Value?
                        0: Convert.ToInt64(drvSelect["BackUpConfigID"]);
                    if (lBackUpConfigID > 0)
                    {
                        frmEditBackUp frm = new frmEditBackUp(lBackUpConfigID);
                        LBShowForm.ShowDialog(frm);

                        LoadBackUpConfig();
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        /// <summary>
        /// 读取备份方案数据
        /// </summary>
        private void LoadBackUpConfig()
        {
            DataTable dtBackUp = ExecuteSQL.CallView(108);
            dtBackUp.Columns.Add("BackUpTime", typeof(string));
            foreach(DataRow dr in dtBackUp.Rows)
            {
                string strBackUpTime = "";
                int iBackUpType = dr["BackUpType"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BackUpType"]);
                int iBackUpWeek = dr["BackUpWeek"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BackUpWeek"]);
                int iBackUpHour = dr["BackUpHour"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BackUpHour"]);
                int iBackUpMinu = dr["BackUpMinu"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BackUpMinu"]);

                if (iBackUpType == 0)
                {
                    if (iBackUpWeek == 1)
                    {
                        strBackUpTime = "周一";
                    }
                    else if (iBackUpWeek == 2)
                    {
                        strBackUpTime = "周二";
                    }
                    else if (iBackUpWeek == 3)
                    {
                        strBackUpTime = "周三";
                    }
                    else if (iBackUpWeek == 4)
                    {
                        strBackUpTime = "周四";
                    }
                    else if (iBackUpWeek == 5)
                    {
                        strBackUpTime = "周五";
                    }
                    else if (iBackUpWeek == 6)
                    {
                        strBackUpTime = "周六";
                    }
                    else if (iBackUpWeek == 7)
                    {
                        strBackUpTime = "周日";
                    }
                }
                if (strBackUpTime != "")
                {
                    strBackUpTime += "  ";
                }
                strBackUpTime += iBackUpHour + ":" + iBackUpMinu;
                dr["BackUpTime"] = strBackUpTime;
            }

            this.grdMain.DataSource = dtBackUp.DefaultView;
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

        private void btnReflesh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadBackUpConfig();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmEditBackUp frm = new frmEditBackUp(0);
                LBShowForm.ShowDialog(frm);

                LoadBackUpConfig();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.grdMain.CurrentCell = null;
                this.grdMain.EndEdit();

                bool bolIsDeleted = false;
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认备份方案？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow dgvr in this.grdMain.Rows)
                    {
                        bool bolSelected = LBConverter.ToBoolean(dgvr.Cells["Selected"].Value);
                        if (bolSelected)
                        {
                            DataRowView drv = dgvr.DataBoundItem as DataRowView;
                            long lBackUpConfigID = LBConverter.ToInt64(drv["BackUpConfigID"]);
                            if (lBackUpConfigID > 0)
                            {
                                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                                parmCol.Add(new LBParameter("BackUpConfigID", enLBDbType.Int64, lBackUpConfigID));
                                DataSet dsReturn;
                                Dictionary<string, object> dictValue;
                                ExecuteSQL.CallSP(13202, parmCol, out dsReturn, out dictValue);
                                bolIsDeleted = true;
                            }
                        }
                    }
                    if (bolIsDeleted)
                    {
                        LB.WinFunction.LBCommonHelper.ShowCommonMessage("删除成功！");
                        LoadBackUpConfig();
                    }
                    else
                    {
                        LB.WinFunction.LBCommonHelper.ShowCommonMessage("请选择需要删除的数据行！");
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
