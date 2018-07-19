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
    public partial class frmItemBase : LBUIPageBase
    {
        private long mlItemID = 0;
        public frmItemBase(long lItemID)
        {
            InitializeComponent();
            mlItemID = lItemID;
            btnDelete.Visible = mlItemID > 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string strSQL = "select UOMID,UOMName from dbo.DbUOM";
            this.txtUOMID.DataSource = ExecuteSQL.CallDirectSQL(strSQL);//读取备份类型常量列表
            this.txtUOMID.DisplayMember = "UOMName";
            this.txtUOMID.ValueMember = "UOMID";

            strSQL = "select ItemTypeID,ItemTypeName from dbo.DbItemType";
            this.txtItemTypeID.DataSource = ExecuteSQL.CallDirectSQL(strSQL);//读取星期常量列表
            this.txtItemTypeID.DisplayMember = "ItemTypeName";
            this.txtItemTypeID.ValueMember = "ItemTypeID";

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
                
                int iSPType = mlItemID > 0 ? 20301 : 20300;

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("ItemID", enLBDbType.Int64, mlItemID));
                parmCol.Add(new LBParameter("ItemTypeID", enLBDbType.Int64, this.txtItemTypeID.SelectedValue));
                parmCol.Add(new LBParameter("ItemCode", enLBDbType.String, this.txtItemCode.Text));
                parmCol.Add(new LBParameter("K3ItemCode", enLBDbType.String, this.txtK3ItemCode.Text));
                parmCol.Add(new LBParameter("ItemName", enLBDbType.String, this.txtItemName.Text));
                parmCol.Add(new LBParameter("ItemMode", enLBDbType.String, this.txtItemMode.Text));
                parmCol.Add(new LBParameter("ItemRate", enLBDbType.Decimal, LBConverter.ToDecimal(this.txtItemRate.Text)));
                parmCol.Add(new LBParameter("ItemPrice", enLBDbType.Decimal, LBConverter.ToDecimal(this.txtPrice.Text)));
                parmCol.Add(new LBParameter("UOMID", enLBDbType.Int64, this.txtUOMID.SelectedValue));
                parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));
                parmCol.Add(new LBParameter("IsForbid", enLBDbType.Boolean, this.chkIsForbid.Checked));

                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                if (dictValue.ContainsKey("ItemID"))
                {
                    long.TryParse(dictValue["ItemID"].ToString(), out mlItemID);
                }
                if (dictValue.ContainsKey("ItemCode"))
                {
                    this.txtItemCode.Text =dictValue["ItemCode"].ToString();
                }
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
                btnDelete.Visible = mlItemID > 0;
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除物料？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlItemID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("ItemID", enLBDbType.Int64, mlItemID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(20302, parmCol, out dsReturn, out dictValue);
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
            if (mlItemID > 0)
            {
                DataTable dtBackUp = ExecuteSQL.CallView(203, "", "ItemID=" + mlItemID, "");
                if (dtBackUp.Rows.Count > 0)
                {
                    DataRow dr = dtBackUp.Rows[0];
                    string strItemCode = LBConverter.ToString(dr["ItemCode"]);
                    string strK3ItemCode = LBConverter.ToString(dr["K3ItemCode"]);
                    string strItemName = LBConverter.ToString(dr["ItemName"]);
                    string strItemMode = LBConverter.ToString(dr["ItemMode"]);
                    decimal dItemRate = LBConverter.ToDecimal(dr["ItemRate"]);
                    long lUOMID = LBConverter.ToInt32(dr["UOMID"]);
                    long lItemTypeID = LBConverter.ToInt32(dr["ItemTypeID"]);
                    string strDescription = LBConverter.ToString(dr["Description"]);
                    bool bIsForbid = LBConverter.ToBoolean(dr["IsForbid"]);
                    decimal decprice = LBConverter.ToDecimal(dr["ItemPrice"]);

                    this.txtK3ItemCode.Text = strK3ItemCode;
                    this.txtItemCode.Text = strItemCode;
                    this.txtItemName.Text = strItemName;
                    this.txtItemMode.Text = strItemMode;
                    this.txtItemRate.Text = dItemRate.ToString();
                    this.txtUOMID.SelectedValue = lUOMID;
                    this.txtItemTypeID.SelectedValue = lItemTypeID;
                    this.txtDescription.Text = strDescription;
                    this.chkIsForbid.Checked = bIsForbid;
                    this.txtPrice.Text = decprice.ToString("0.000");
                }
            }
        }

        private void skinLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
