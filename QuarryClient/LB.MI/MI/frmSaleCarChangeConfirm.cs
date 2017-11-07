using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.MI.MI
{
    public partial class frmSaleCarChangeConfirm : UserControl
    {
        private ChangeInfo _changeInfo = null;
        public frmSaleCarChangeConfirm(ChangeInfo changeInfo)
        {
            InitializeComponent();
            _changeInfo = changeInfo;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            StringBuilder strChange = new StringBuilder();
            if (_changeInfo.NewItemID != _changeInfo.OldItemID)
            {
                if (strChange.ToString() != "")
                    strChange.AppendLine();
                strChange.AppendLine(_changeInfo.OldItemName + " -> " + _changeInfo.NewItemName);
            }
            if (_changeInfo.NewCustomerID != _changeInfo.OldCustomerID)
            {
                if (strChange.ToString() != "")
                    strChange.AppendLine();
                strChange.AppendLine(_changeInfo.OldCustomerName + " -> " + _changeInfo.NewCustomerName);
            }

            if (_changeInfo.SaleCarOutBillID > 0)
            {
                if(_changeInfo.OldReceiveType!= _changeInfo.NewReceiveType)
                {
                    if (strChange.ToString() != "")
                        strChange.AppendLine();
                    strChange.AppendLine(_changeInfo.OldReceiveName + " -> " + _changeInfo.NewReceiveName);
                }
                if (_changeInfo.OldCalculateType != _changeInfo.NewCalculateType)
                {
                    if (strChange.ToString() != "")
                        strChange.AppendLine();
                    strChange.AppendLine(_changeInfo.OldCalculteName + " -> " + _changeInfo.NewCalculteName);
                }
                if (_changeInfo.OldPrice != _changeInfo.NewPrice)
                {
                    if (strChange.ToString() != "")
                        strChange.AppendLine();
                    strChange.AppendLine(_changeInfo.OldPrice.ToString("0.000") + " -> " + _changeInfo.NewPrice.ToString("0.000"));
                }
                if (_changeInfo.OldAmount != _changeInfo.NewAmount)
                {
                    if (strChange.ToString() != "")
                        strChange.AppendLine();
                    strChange.AppendLine(_changeInfo.OldAmount.ToString("0.00") + " -> " + _changeInfo.NewAmount.ToString("0.00"));
                }

                this.lblChange.Text = strChange.ToString();

                this.gbReceive.Visible = IsVisibleReceiveChange();
            }
        }

        private bool IsVisibleReceiveChange()
        {
            if (_changeInfo.SaleCarOutBillID > 0)
            {
                if(_changeInfo.NewAmount!=_changeInfo.OldAmount|| _changeInfo.NewCustomerID != _changeInfo.OldCustomerID)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public class ChangeInfo
    {
        public long SaleCarInBillID;
        public long SaleCarOutBillID;
        public long NewItemID;
        public long OldItemID;
        public long NewCustomerID;
        public long OldCustomerID;
        public int NewReceiveType;
        public int OldReceiveType;
        public int NewCalculateType;
        public int OldCalculateType;
        public string NewItemName;
        public string OldItemName;
        public string NewCustomerName;
        public string OldCustomerName;
        public string NewReceiveName;
        public string OldReceiveName;
        public string NewCalculteName;
        public string OldCalculteName;
        public decimal NewAmount;
        public decimal NewPrice;
        public decimal OldAmount;
        public decimal OldPrice;

        public ChangeInfo(long lSaleCarInBillID, long lSaleCarOutBillID,
            long lNewItemID, long lNewCustomerID, int iNewReceiveType, int iNewCalculateType,
            string strNewItemName, string strNewCustomerName, string strNewReceiveName, string strNewCalculteName,
            decimal decNewAmount, decimal decNewPrice,
            long lOldItemID, long lOldCustomerID, int iOldReceiveType, int iOldCalculateType,
            string strOldItemName, string strOldCustomerName, string strOldReceiveName, string strOldCalculteName,
            decimal decOldAmount, decimal decOldPrice)
        {
            SaleCarInBillID = lSaleCarInBillID;
            SaleCarOutBillID = lSaleCarOutBillID;
            NewItemID = lNewItemID;
            NewCustomerID = lNewCustomerID;
            NewReceiveType = iNewReceiveType;
            NewCalculateType = iNewCalculateType;
            NewItemName = strNewItemName;
            NewCustomerName = strNewCustomerName;
            NewReceiveName = strNewReceiveName;
            NewCalculteName = strNewCalculteName;
            NewAmount = decNewAmount;
            NewPrice = decNewPrice;
            OldItemID = lOldItemID;
            OldCustomerID = lOldCustomerID;
            OldReceiveType = iOldReceiveType;
            OldCalculateType = iOldCalculateType;
            OldItemName = strOldItemName;
            OldCustomerName = strOldCustomerName;
            OldReceiveName = strOldReceiveName;
            OldCalculteName = strOldCalculteName;
            OldAmount = decOldAmount;
            OldPrice = decOldPrice;

        }
    }
}
