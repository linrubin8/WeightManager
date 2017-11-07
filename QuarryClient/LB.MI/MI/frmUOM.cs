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
    public partial class frmUOM : LBUIPageBase
    {
        private long mlUOMID = 0;
        public frmUOM(long lItemTypeID)
        {
            InitializeComponent();
            mlUOMID = lItemTypeID;
            btnDelete.Visible = mlUOMID > 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.txtUOMType.DataSource = LB.Common.LBConst.GetConstData("UOMType");//读取单位类型常量列表
            this.txtUOMType.DisplayMember = "ConstText";
            this.txtUOMType.ValueMember = "ConstValue";

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
                
                int iSPType = mlUOMID > 0 ? 20201 : 20200;

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("UOMID", enLBDbType.Int64, mlUOMID));
                parmCol.Add(new LBParameter("UOMName", enLBDbType.String, this.txtUOMName.Text));
                parmCol.Add(new LBParameter("UOMType", enLBDbType.Int32, this.txtUOMType.SelectedValue));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                if (dictValue.ContainsKey("UOMID"))
                {
                    long.TryParse(dictValue["UOMID"].ToString(), out mlUOMID);
                }
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
                btnDelete.Visible = mlUOMID > 0;
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除计量单位？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlUOMID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("UOMID", enLBDbType.Int64, mlUOMID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(20202, parmCol, out dsReturn, out dictValue);
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
            if (mlUOMID > 0)
            {
                string strSQL = "select * from dbo.DbUOM where UOMID=" + mlUOMID;
                DataTable dt = ExecuteSQL.CallDirectSQL(strSQL);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.txtUOMName.Text = LBConverter.ToString(dr["UOMName"]);
                    this.txtUOMType.SelectedValue = dr["UOMType"];
                }
            }
        }
    }
}
