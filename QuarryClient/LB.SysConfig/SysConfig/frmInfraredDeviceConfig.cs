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
using System.IO.Ports;
using LB.Page.Helper;

namespace LB.SysConfig.SysConfig
{
    public partial class frmInfraredDeviceConfig : LBUIPageBase
    {
        private System.Windows.Forms.Timer mTimer = null;
        public frmInfraredDeviceConfig()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.txtHeaderXType.DataSource = LB.Common.LBConst.GetConstData("DeviceXType");
            this.txtHeaderXType.DisplayMember = "ConstText";
            this.txtHeaderXType.ValueMember = "ConstValue";

            this.txtTailXType.DataSource = LB.Common.LBConst.GetConstData("DeviceXType");
            this.txtTailXType.DisplayMember = "ConstText";
            this.txtTailXType.ValueMember = "ConstValue";

            this.txtHeaderYType.DataSource = LB.Common.LBConst.GetConstData("DeviceYType");
            this.txtHeaderYType.DisplayMember = "ConstText";
            this.txtHeaderYType.ValueMember = "ConstValue";

            this.txtTailYType.DataSource = LB.Common.LBConst.GetConstData("DeviceYType");
            this.txtTailYType.DisplayMember = "ConstText";
            this.txtTailYType.ValueMember = "ConstValue";
            InitSerialData();
            SetFieldValue();

            mTimer = new Timer();
            mTimer.Interval = 100;
            mTimer.Enabled = true;
            mTimer.Tick += MTimer_Tick;
        }

        private void MTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.pnlCarHeader.BackColor = LBInFrareHelper.HeaderClosed ? Color.Red : Color.White;
                this.pnlCarTail.BackColor = LBInFrareHelper.TailClosed ? Color.Red : Color.White;
            }
            catch (Exception ex)
            {

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int HeaderXType = LBConverter.ToInt32(this.txtHeaderXType.SelectedValue);
                int TailXType = LBConverter.ToInt32(this.txtTailXType.SelectedValue);
                int HeaderYType = LBConverter.ToInt32(this.txtHeaderYType.SelectedValue);
                int TailYType = LBConverter.ToInt32(this.txtTailYType.SelectedValue);
                int IsHeaderEffect = this.chkHeaderEffect.Checked ? 1 : 0;
                int IsTailEffect = this.chkTailEffect.Checked ? 1 : 0;
                string strSerialName = this.txtSerialName.SelectedValue==null?"": this.txtSerialName.SelectedValue.ToString();
                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("HeaderXType", enLBDbType.Int32, HeaderXType));
                parmCol.Add(new LBParameter("TailXType", enLBDbType.Int32, TailXType));
                parmCol.Add(new LBParameter("HeaderYType", enLBDbType.Int32, HeaderYType));
                parmCol.Add(new LBParameter("TailYType", enLBDbType.Int32, TailYType));
                parmCol.Add(new LBParameter("IsHeaderEffect", enLBDbType.Boolean, IsHeaderEffect));
                parmCol.Add(new LBParameter("IsTailEffect", enLBDbType.Boolean, IsTailEffect));
                parmCol.Add(new LBParameter("MachineName", enLBDbType.String, LoginInfo.MachineName));
                parmCol.Add(new LBParameter("SerialName", enLBDbType.String, strSerialName));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(14500, parmCol, out dsReturn, out dictValue);

                LBInFrareHelper.InitSerialPort();
                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void SetFieldValue()
        {
            DataTable dtDesc = ExecuteSQL.CallView(134, "", "MachineName='" + LoginInfo.MachineName + "'", "");
            if (dtDesc.Rows.Count > 0)
            {
                this.txtHeaderXType.SelectedValue = dtDesc.Rows[0]["HeaderXType"];
                this.txtTailXType.SelectedValue = dtDesc.Rows[0]["TailXType"];
                this.txtHeaderYType.SelectedValue = dtDesc.Rows[0]["HeaderYType"];
                this.txtTailYType.SelectedValue = dtDesc.Rows[0]["TailYType"];
                this.chkHeaderEffect.Checked = LBConverter.ToBoolean(dtDesc.Rows[0]["IsHeaderEffect"]);
                this.chkTailEffect.Checked = LBConverter.ToBoolean(dtDesc.Rows[0]["IsTailEffect"]);
                this.txtSerialName.SelectedValue = dtDesc.Rows[0]["SerialName"].ToString().TrimEnd();
            }
        }

        #region-- 初始化串口数据 --

        private void InitSerialData()
        {
            DataTable dtPort = new DataTable();
            dtPort.Columns.Add("PortName", typeof(string));

            string[] ArryPort = SerialPort.GetPortNames();
            for (int i = 0; i < ArryPort.Length; i++)
            {
                DataRow drNew = dtPort.NewRow();
                drNew["PortName"] = ArryPort[i];
                dtPort.Rows.Add(drNew);
            }
            this.txtSerialName.DataSource = dtPort.DefaultView;
            this.txtSerialName.DisplayMember = "PortName";
            this.txtSerialName.ValueMember = "PortName";
            if (ArryPort.Length > 0)
                this.txtSerialName.SelectedIndex = 0;
        }

        #endregion

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            frmInfraredDeviceConnectPic frmConnect = new SysConfig.frmInfraredDeviceConnectPic();
            LBShowForm.ShowDialog(frmConnect);
        }
    }
}
