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

namespace LB.SysConfig
{
    public partial class frmEditBackUp : LBUIPageBase
    {
        private long mlBackUpConfigID;
        public frmEditBackUp(long lBackUpConfigID)
        {
            InitializeComponent();
            mlBackUpConfigID = lBackUpConfigID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.txtBackUpType.SelectedValueChanged += TxtBackUpType_SelectedValueChanged;

            this.txtBackUpType.DataSource = LB.Common.LBConst.GetConstData("BackUpType");//读取备份类型常量列表
            this.txtBackUpType.DisplayMember = "ConstText";
            this.txtBackUpType.ValueMember = "ConstValue";

            this.txtBackUpWeek.DataSource = LB.Common.LBConst.GetConstData("BackUpWeek");//读取星期常量列表
            this.txtBackUpWeek.DisplayMember = "ConstText";
            this.txtBackUpWeek.ValueMember = "ConstValue";

            ReadFieldValue();
        }

        private void TxtBackUpType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int iBackUpType = LBConverter.ToInt32(this.txtBackUpType.SelectedValue);
                if (iBackUpType == 1)//每天备份，将周设置隐藏
                {
                    this.lblBackUpWeek.Visible = false;
                    this.txtBackUpWeek.Visible = false;
                }
                else
                {
                    this.lblBackUpWeek.Visible = true;
                    this.txtBackUpWeek.Visible = true;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.VerifyTextBoxIsEmpty();//校验控件值是否为空

                int iBackUpFileMaxNum;
                int.TryParse(this.txtBackUpFileMaxNum.Text.TrimEnd(), out iBackUpFileMaxNum);
                if (iBackUpFileMaxNum <= 0)
                {
                    LB.WinFunction.LBCommonHelper.ShowCommonMessage("[最大备份帐套数必须大于0]");
                    return;
                }

                int iSPType = mlBackUpConfigID > 0 ? 13201 : 13200;

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("BackUpConfigID", enLBDbType.Int64, mlBackUpConfigID));
                parmCol.Add(new LBParameter("BackUpType", enLBDbType.Int32, this.txtBackUpType.SelectedValue));
                parmCol.Add(new LBParameter("BackUpWeek", enLBDbType.Int32, this.txtBackUpWeek.SelectedValue));
                parmCol.Add(new LBParameter("BackUpHour", enLBDbType.Int32, this.txtBackUpHour.Text));
                parmCol.Add(new LBParameter("BackUpMinu", enLBDbType.Int32, this.txtBackUpMinu.Text));
                parmCol.Add(new LBParameter("IsEffect", enLBDbType.Boolean, this.chkIsEffect.Checked));
                parmCol.Add(new LBParameter("BackUpFileMaxNum", enLBDbType.Int32, iBackUpFileMaxNum));
                parmCol.Add(new LBParameter("BackUpName", enLBDbType.String, this.txtBackUpName.Text));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(iSPType, parmCol, out dsReturn, out dictValue);
                if (dictValue.ContainsKey("BackUpConfigID"))
                {
                    long.TryParse(dictValue["BackUpConfigID"].ToString(), out mlBackUpConfigID);
                }
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
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
                if (LB.WinFunction.LBCommonHelper.ConfirmMessage("是否确认删除备份方案？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (mlBackUpConfigID > 0)
                    {
                        LBDbParameterCollection parmCol = new LBDbParameterCollection();
                        parmCol.Add(new LBParameter("BackUpConfigID", enLBDbType.Int64, mlBackUpConfigID));
                        DataSet dsReturn;
                        Dictionary<string, object> dictValue;
                        ExecuteSQL.CallSP(13202, parmCol, out dsReturn, out dictValue);
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
            if (mlBackUpConfigID > 0)
            {
                DataTable dtBackUp = ExecuteSQL.CallView(108,"", "BackUpConfigID="+ mlBackUpConfigID,"");
                if (dtBackUp.Rows.Count > 0)
                {
                    DataRow drBackUp = dtBackUp.Rows[0];
                    int iBackUpType = LBConverter.ToInt32(drBackUp["BackUpType"]);
                    int iBackUpWeek = LBConverter.ToInt32(drBackUp["BackUpWeek"]);
                    int iBackUpHour = LBConverter.ToInt32(drBackUp["BackUpHour"]);
                    int iBackUpMinu = LBConverter.ToInt32(drBackUp["BackUpMinu"]);
                    bool bolIsEffect = LBConverter.ToBoolean(drBackUp["IsEffect"]);
                    int iBackUpFileMaxNum = LBConverter.ToInt32(drBackUp["BackUpFileMaxNum"]);
                    string strBackUpPath= LBConverter.ToString(drBackUp["BackUpPath"]);
                    string strBackUpName = LBConverter.ToString(drBackUp["BackUpName"]);

                    this.txtBackUpFileMaxNum.Text = iBackUpFileMaxNum.ToString();
                    this.txtBackUpHour.Text = iBackUpHour.ToString();
                    this.txtBackUpMinu.Text = iBackUpMinu.ToString();
                    this.txtBackUpWeek.SelectedValue = iBackUpWeek;
                    this.txtBackUpType.SelectedValue = iBackUpType;
                    this.txtBackUpName.Text = strBackUpName;
                    this.chkIsEffect.Checked = bolIsEffect;
                }
            }
        }

    }
}
