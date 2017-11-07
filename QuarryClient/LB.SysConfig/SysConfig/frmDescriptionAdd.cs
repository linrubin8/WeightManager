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

namespace LB.SysConfig.SysConfig
{
    public partial class frmDescriptionAdd : LBUIPageBase
    {
        public long mlDescriptionID;
        public frmDescriptionAdd(long lDescriptionID)
        {
            InitializeComponent();
            this.mlDescriptionID = lDescriptionID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetFieldValue();
        }

        private void SetFieldValue()
        {
            if (mlDescriptionID > 0)
            {
                DataTable dtDesc = ExecuteSQL.CallView(121, "", "DescriptionID="+ mlDescriptionID, "");
                if(dtDesc.Rows.Count>0)
                    this.txtDescription.Text = dtDesc.Rows[0]["Description"].ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.VerifyTextBoxIsEmpty();

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("DescriptionID", enLBDbType.Int64, mlDescriptionID));
                parmCol.Add(new LBParameter("Description", enLBDbType.String, this.txtDescription.Text));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14000, parmCol, out dsReturn, out dictValue);
                if (dictValue.ContainsKey("DescriptionID"))
                {
                    long.TryParse(dictValue["DescriptionID"].ToString(), out mlDescriptionID);
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否删除该备注信息？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlDescriptionID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("DescriptionID", enLBDbType.Int64, mlDescriptionID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(14001, parmCol, out dsReturn, out dictValue);
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
