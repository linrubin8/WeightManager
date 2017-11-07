using LB.Web.Base.Base.Helper;
using LB.Web.Base.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.SM.BLL.Factory
{
    public class AssemblyStart
    {
        System.Timers.Timer mtimer;
        public AssemblyStart()
        {
            DBHelper.GetBLLObjectMethodEevent += DBHelper_GetBLLObjectMethodEevent;
            DBHelper.StopServerEvent += DBHelper_StopServerEvent;
            mtimer = new System.Timers.Timer();
            mtimer.Interval = 30000;
            mtimer.Elapsed += Mtimer_Elapsed;
            mtimer.Enabled = true;
        }

        private void DBHelper_StopServerEvent(EventArgs args)
        {
            if (mtimer != null&& mtimer.Enabled)
            {
                mtimer.Enabled = false;
            }
        }

        private void Mtimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                SaleInTimer.SaleCarInBillCancel();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
            }
        }

        private void DBHelper_GetBLLObjectMethodEevent(Base.Base.Factory.GetBLLObjectEventArgs args)
        {
            int iSPType = args.SPType;
            if (iSPType == 0 || args.BLLFunction != null)
            {
                return;
            }

            switch (iSPType)
            {
                case 13600:
                case 13601:
                case 13602:
                case 13603:
                case 13604:
                case 13605:
                case 13606:
                case 13607:
                case 13608:
                    args.BLLFunction = new BLLModifyBillHeader();
                    break;

                case 13700:
                case 13701:
                case 13702:
                    args.BLLFunction = new BLLModifyBillDetail();
                    break;

                case 14100:
                case 14101:
                case 14102:
                case 14103:
                case 14104:
                case 14105:
                case 14106:
                case 14107:
                case 14108:
                case 14109:
                case 14110:
                case 14111:
                case 14112:
                case 14113:
                case 14114:
                case 14115:
                case 14116:
                case 14117:
                case 14118:
                case 14119:
                case 14120:
                    args.BLLFunction = new BLLSaleCarInOutBill();
                    break;

                case 30000:
                case 30001:
                case 30002:
                case 30003:
                case 30004:
                case 30005:
                    args.BLLFunction = new BLLSaleCarReturnBill();
                    break;
            }
        }
    }
}
