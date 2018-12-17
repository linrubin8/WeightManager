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

namespace LB.MI
{
    public partial class frmCustomerTypeAdd : LBUIPageBase
    {
        public long mlCustomerTypeID;
        public frmCustomerTypeAdd(long lCustomerTypeID)
        {
            InitializeComponent();
            this.mlCustomerTypeID = lCustomerTypeID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetFieldValue();
        }

        private void SetFieldValue()
        {
            if (mlCustomerTypeID > 0)
            {
                DataTable dtDesc = ExecuteSQL.CallView(139, "", "CustomerTypeID=" + mlCustomerTypeID, "");
                if (dtDesc.Rows.Count > 0)
                {
                    this.txtCustomerTypeCode.Text = dtDesc.Rows[0]["CustomerTypeCode"].ToString();
                    this.txtCustomerTypeName.Text = dtDesc.Rows[0]["CustomerTypeName"].ToString();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int iSPType = 14800;
                if (mlCustomerTypeID > 0)
                {
                    iSPType = 14801;
                }

                this.VerifyTextBoxIsEmpty();

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("CustomerTypeID", enLBDbType.Int64, mlCustomerTypeID));
                parmCol.Add(new LBParameter("CustomerTypeCode", enLBDbType.String, this.txtCustomerTypeCode.Text));
                parmCol.Add(new LBParameter("CustomerTypeName", enLBDbType.String, this.txtCustomerTypeName.Text));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                if (dictValue.ContainsKey("CustomerTypeID"))
                {
                    long.TryParse(dictValue["CustomerTypeID"].ToString(), out mlCustomerTypeID);
                }

                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否删除该客户类型？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlCustomerTypeID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("CustomerTypeID", enLBDbType.Int64, mlCustomerTypeID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(14802, parmCol, out dsReturn, out dictValue);
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
