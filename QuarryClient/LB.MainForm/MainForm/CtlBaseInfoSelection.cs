using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LB.Common;
using LB.WinFunction;
using static LB.Controls.Args.SelectedRowArgs;
using LB.Controls.Args;
using LB.Controls;

namespace LB.MainForm
{
    public partial class CtlBaseInfoSelection : UserControl
    {
        public event SelectedRowEventHandle SelectedRowEvent;
        private enBaseInfoType _BaseInfoType= enBaseInfoType.None;
        public enBaseInfoType BaseInfoType
        {
            get
            {
                return _BaseInfoType;
            }
        }

        public CtlBaseInfoSelection()
        {
            InitializeComponent();

            DataGridViewStyleSetting.InitDataGridViewStyle(this.grdCarIn);
            DataGridViewStyleSetting.InitDataGridViewStyle(this.grdCarInfo);
            DataGridViewStyleSetting.InitDataGridViewStyle(this.grdCarOut);
            DataGridViewStyleSetting.InitDataGridViewStyle(this.grdCustomer);
            DataGridViewStyleSetting.InitDataGridViewStyle(this.grdItem);

            this.grdCarIn.Visible = false;
            this.grdCarOut.Visible = false;
            this.grdCustomer.Visible = false;
            this.grdItem.Visible = false;
            this.grdCarInfo.Visible = false;

            this.grdCarIn.CellClick += GrdCar_CellClick;
            this.grdCarOut.CellClick += GrdCarOut_CellClick;
            this.grdCustomer.CellClick += GrdCustomer_CellClick;
            this.grdItem.CellClick += GrdItem_CellClick;
            this.grdCarInfo.CellClick += GrdCarInfo_CellClick;

            this.grdCarIn.CellFormatting += GrdCarIn_CellFormatting;
            this.grdCarOut.CellFormatting += GrdCarOut_CellFormatting;
        }

        private void GrdCarInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (SelectedRowEvent != null)
                    {
                        DataRow drSelect = ((DataRowView)this.grdCarInfo.Rows[e.RowIndex].DataBoundItem).Row;
                        SelectedRowArgs args = new SelectedRowArgs(enBaseInfoType.CarInfo, drSelect);
                        SelectedRowEvent(args);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void GrdCarOut_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    int iCanOutBillCount = LBConverter.ToInt32(this.grdCarOut["CanOutBillCount", e.RowIndex].Value);
                    if (iCanOutBillCount == 0)
                    {
                        e.CellStyle.BackColor = Color.OrangeRed;
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void GrdCarIn_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    int iNotOutBillCount = LBConverter.ToInt32(this.grdCarIn["NotOutBillCount", e.RowIndex].Value);
                    if (iNotOutBillCount > 0)
                    {
                        e.CellStyle.BackColor = Color.OrangeRed;
                    }
                }
            }
            catch(Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void GrdItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (SelectedRowEvent != null)
                    {
                        DataRow drSelect = ((DataRowView)this.grdItem.Rows[e.RowIndex].DataBoundItem).Row;
                        SelectedRowArgs args = new SelectedRowArgs( enBaseInfoType.Item, drSelect);
                        SelectedRowEvent(args);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void GrdCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (SelectedRowEvent != null)
                    {
                        DataRow drSelect = ((DataRowView)this.grdCustomer.Rows[e.RowIndex].DataBoundItem).Row;
                        SelectedRowArgs args = new SelectedRowArgs(enBaseInfoType.Customer, drSelect);
                        SelectedRowEvent(args);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void GrdCarOut_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (SelectedRowEvent != null)
                    {
                        DataRow drSelect = ((DataRowView)this.grdCarOut.Rows[e.RowIndex].DataBoundItem).Row;
                        SelectedRowArgs args = new SelectedRowArgs(enBaseInfoType.CarOut, drSelect);
                        SelectedRowEvent(args);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void GrdCar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (SelectedRowEvent != null)
                    {
                        DataRow drSelect = ((DataRowView)this.grdCarIn.Rows[e.RowIndex].DataBoundItem).Row;
                        SelectedRowArgs args = new SelectedRowArgs(enBaseInfoType.CarIn, drSelect);
                        SelectedRowEvent(args);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        public void ChangeItemType(enBaseInfoType eBaseInfoType)
        {
            _BaseInfoType = eBaseInfoType;
            if (eBaseInfoType== enBaseInfoType.None)
            {
                //this.grdCar.Visible = false;
                //this.grdCustomer.Visible = false;
                //this.grdItem.Visible = false;
            }
            else if (eBaseInfoType == enBaseInfoType.CarIn)
            {
                this.grdCarIn.Visible = true;
                this.grdCarIn.Dock = DockStyle.Fill;
                this.grdCustomer.Visible = false;
                this.grdItem.Visible = false;
                this.grdCarOut.Visible = false;
                this.grdCarInfo.Visible = false;
            }
            else if (eBaseInfoType == enBaseInfoType.CarOut)
            {
                this.grdCarOut.Visible = true;
                this.grdCarOut.Dock = DockStyle.Fill;
                this.grdCustomer.Visible = false;
                this.grdItem.Visible = false;
                this.grdCarIn.Visible = false;
                this.grdCarInfo.Visible = false;
            }
            else if (eBaseInfoType == enBaseInfoType.Customer)
            {
                this.grdCarIn.Visible = false;
                this.grdCarInfo.Visible = false;
                this.grdCustomer.Visible = true;
                this.grdCustomer.Dock = DockStyle.Fill;
                this.grdItem.Visible = false;
                this.grdCarOut.Visible = false;
            }
            else if (eBaseInfoType == enBaseInfoType.Item)
            {
                this.grdCarIn.Visible = false;
                this.grdCustomer.Visible = false;
                this.grdCarInfo.Visible = false;
                this.grdItem.Visible = true;
                this.grdItem.Dock = DockStyle.Fill;
                this.grdCarOut.Visible = false;
            }
            else if (eBaseInfoType == enBaseInfoType.CarInfo)
            {
                this.grdCarIn.Visible = false;
                this.grdCustomer.Visible = false;
                this.grdItem.Visible = false;
                this.grdCarOut.Visible = false;
                this.grdCarInfo.Visible = true;
                this.grdCarInfo.Dock = DockStyle.Fill;
            }
        }

        public void LoadDataSource(string strFilter)
        {
            if (_BaseInfoType == enBaseInfoType.CarIn)
            {
                DataTable dtData = ExecuteSQL.CallView(127, "", strFilter, "");
                this.grdCarIn.DataSource = dtData.DefaultView;
            }
            else if (_BaseInfoType == enBaseInfoType.CarOut)
            {
                DataTable dtData = ExecuteSQL.CallView(127, "", strFilter, "");
                this.grdCarOut.DataSource = dtData.DefaultView;
            }
            else if (_BaseInfoType == enBaseInfoType.Customer)
            {
                DataTable dtData = ExecuteSQL.CallView(112, "", strFilter, "");
                this.grdCustomer.DataSource = dtData.DefaultView;
            }
            else if (_BaseInfoType == enBaseInfoType.Item)
            {
                DataTable dtData = ExecuteSQL.CallView(203, "", strFilter, "");
                this.grdItem.DataSource = dtData.DefaultView;
            }
            else if(_BaseInfoType== enBaseInfoType.CarInfo)
            {
                DataTable dtDetail = ExecuteSQL.CallView(117, "", strFilter, "");
                this.grdCarInfo.DataSource = dtDetail.DefaultView;
            }
        }
    }
    
}
