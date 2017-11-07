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

namespace LB.MI
{
    public partial class frmItemType : LBUIPageBase
    {
        private long mlItemTypeID = 0;
        public frmItemType(long lItemTypeID)
        {
            InitializeComponent();
            mlItemTypeID = lItemTypeID;
            btnDelete.Visible = mlItemTypeID > 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ReadFieldValue();
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
                this.VerifyTextBoxIsEmpty();//校验控件值是否为空
                
                int iSPType = mlItemTypeID > 0 ? 20101 : 20100;

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("ItemTypeID", enLBDbType.Int64, mlItemTypeID));
                parmCol.Add(new LBParameter("ItemTypeName", enLBDbType.String, this.txtItemTypeName.Text));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                if (dictValue.ContainsKey("ItemTypeID"))
                {
                    long.TryParse(dictValue["ItemTypeID"].ToString(), out mlItemTypeID);
                }
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
                btnDelete.Visible = mlItemTypeID > 0;
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除物料类型？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlItemTypeID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ItemTypeID", enLBDbType.Int64, mlItemTypeID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(20102, parmCol, out dsReturn, out dictValue);
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        /// <summary>
        /// 读取控件参数值
        /// </summary>
        private void ReadFieldValue()
        {
            if (mlItemTypeID > 0)
            {
                string strSQL = "select * from dbo.DbItemType where ItemTypeID=" + mlItemTypeID;
                DataTable dt = ExecuteSQL.CallDirectSQL(strSQL);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.txtItemTypeName.Text = LBConverter.ToString(dr["ItemTypeName"]);
                    this.txtChangeBy.Text = LBConverter.ToString(dr["ChangeBy"]);
                    this.txtChangeTime.Text = LBConverter.ToString(dr["ChangeTime"]);
                }
            }
        }
    }
}
