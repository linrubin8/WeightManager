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
    public partial class frmSysConfig : LBUIPageBase
    {
        public frmSysConfig()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.txtSysSaleBillType.DataSource = LB.Common.LBConst.GetConstData("SysSaleBillType");//地磅类型
            this.txtSysSaleBillType.DisplayMember = "ConstText";
            this.txtSysSaleBillType.ValueMember = "ConstValue";

            ReadSysConfigValue();

        }

        private void ReadSysConfigValue()
        {
            DataTable dtConfigValue = ExecuteSQL.CallView(129, "", "", "");
            foreach(DataRow dr in dtConfigValue.Rows)
            {
                string strSysConfigFieldName = dr["SysConfigFieldName"].ToString().TrimEnd();
                int iSysConfigDataType =Convert.ToInt16( dr["SysConfigDataType"]);
                string strSysConfigDefaultValue = dr["SysConfigDefaultValue"].ToString().TrimEnd();
                string strSysConfigValue = dr["SysConfigValue"].ToString().TrimEnd();

                string strConfigValue = strSysConfigValue == "" ? strSysConfigDefaultValue : strSysConfigValue;
                int iValue;
                switch (strSysConfigFieldName)
                {
                    case "SysSaleReceiveOverdue"://入场磅单有效时间(分钟)
                        int.TryParse(strConfigValue, out iValue);
                        this.txtSysSaleReceiveOverdue.Text = iValue.ToString();
                        break;


                    case "SysSaleBillType"://
                        int.TryParse(strConfigValue, out iValue);
                        this.txtSysSaleBillType.SelectedValue = iValue;
                        break;

                    case "AllowPrintInReportCount"://允许补打小票次数
                        int.TryParse(strConfigValue, out iValue);
                        this.txtAllowPrintInReportCount.Text = iValue.ToString();
                        break;

                    case "AllowPrintOutReportCount"://允许补打磅单次数
                        int.TryParse(strConfigValue, out iValue);
                        this.txtAllowPrintOutReportCount.Text = iValue.ToString();
                        break;
                }
            }
        }

        private void SaveSysConfigValue()
        {
            DataTable dtSPIN = new DataTable();
            dtSPIN.Columns.Add("SysConfigFieldName", typeof(string));
            dtSPIN.Columns.Add("SysConfigValue", typeof(string));
            
            DataRow drNew = dtSPIN.NewRow();
            drNew["SysConfigFieldName"] = "SysSaleReceiveOverdue";
            drNew["SysConfigValue"] = this.txtSysSaleReceiveOverdue.Text;
            dtSPIN.Rows.Add(drNew);
            drNew = dtSPIN.NewRow();
            drNew["SysConfigFieldName"] = "SysSaleBillType";
            drNew["SysConfigValue"] = this.txtSysSaleBillType.SelectedValue;
            dtSPIN.Rows.Add(drNew);
            drNew = dtSPIN.NewRow();
            drNew["SysConfigFieldName"] = "AllowPrintInReportCount";
            drNew["SysConfigValue"] = this.txtAllowPrintInReportCount.Text;
            dtSPIN.Rows.Add(drNew);
            drNew = dtSPIN.NewRow();
            drNew["SysConfigFieldName"] = "AllowPrintOutReportCount";
            drNew["SysConfigValue"] = this.txtAllowPrintOutReportCount.Text;
            dtSPIN.Rows.Add(drNew);

            DataSet dsReturn;
            DataTable dtResult;
            ExecuteSQL.CallSP(14300, dtSPIN, out dsReturn, out dtResult);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch(Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSysConfigValue();

                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功");
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
    }
}
