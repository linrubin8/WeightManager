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
using LB.Common;

namespace LB.SysConfig.SysConfig
{
    public partial class frmWeightConfigType : LBUIPageBase
    {
        private int _OrgWeightType = 0;//原来磅房类型
        public bool IsChangeWeightType = false;
        public frmWeightConfigType()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.txtWeightType.DataSource = LB.Common.LBConst.GetConstData("WeightType");
            this.txtWeightType.DisplayMember = "ConstText";
            this.txtWeightType.ValueMember = "ConstValue";

            SetFieldValue();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int WeightType = LBConverter.ToInt32(this.txtWeightType.SelectedValue); ;

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("WeightType", enLBDbType.Int32, WeightType));
                parmCol.Add(new LBParameter("MachineName", enLBDbType.String, LoginInfo.MachineName));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14200, parmCol, out dsReturn, out dictValue);
                
                if (_OrgWeightType!= WeightType)
                {
                    IsChangeWeightType = true;
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！修改后需要注销并重新登录系统！");
                    this.Close();
                }
                else
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
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

        private void SetFieldValue()
        {
            DataTable dtDesc = ExecuteSQL.CallView(126, "", "MachineName='" + LoginInfo.MachineName + "'", "");
            if (dtDesc.Rows.Count > 0)
            {
                this.txtWeightType.SelectedValue = dtDesc.Rows[0]["WeightType"];
                _OrgWeightType = LBConverter.ToInt32(dtDesc.Rows[0]["WeightType"]);
            }
        }
    }
}
