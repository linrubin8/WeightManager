using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LB.WinFunction;
using LB.Controls;

namespace LB.SysConfig.SysConfig
{
    public partial class frmCarTareManager : LBUIPageBase
    {
        private string _strCarNum="";
        public frmCarTareManager(string strCarNum)
        {
            _strCarNum = strCarNum;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadDataSource();

            this.ctlSearcher1.SetGridView(this.grdMain, "CarNum");
            if (_strCarNum != "")
            {
                this.ctlSearcher1.SetDefaultFilter("CarNum", _strCarNum);
                LoadDataSource();
            }
        }

        private void LoadDataSource()
        {
            string strFilter = this.ctlSearcher1.GetFilter();
            DataTable dtUser = ExecuteSQL.CallView(125, "", strFilter, "BillDateIn desc");
            this.grdMain.DataSource = dtUser.DefaultView;

        }

        private void btnReflesh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDataSource();
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
    }
}
