using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Common.Synchronous
{
    public class SynchronousBill
    {
        /// <summary>
        /// 读取未同步且已完成的入场单
        /// </summary>
        /// <returns></returns>
        public DataTable ReadUnSynchronousBill()
        {
            string strFilter = "IsSynchronousToServer = 0 and (IsCancel =1 or SaleCarOutBillID is not null)";
            DataTable dtBill = ExecuteSQL.CallView(125, "", strFilter, "SaleCarOutBillID desc,SaleCarInBillID desc");
            return dtBill;
        }

        public void SynchronousBillToServer(DataTable dtBill)
        {
            foreach(DataRow dr in dtBill.Rows)
            {
                //客户名称
                string strCustomerName = dr["CustomerName"].ToString().TrimEnd();
                //车辆名称
                string strCarName = dr["CarName"].ToString().TrimEnd();
                //物料名称
                string strItemName = dr["ItemName"].ToString().TrimEnd();

                //判断服务器是否存在该客户
                DataTable dtCustomerServer = ExecuteSQL.CallView_Service(112,"CustomerID", "CustomerName='"+ strCustomerName + "'","");
                if (dtCustomerServer.Rows.Count == 0)//服务器不存在该客户
                {
                    DataTable dtCustomerClient = ExecuteSQL.CallView(112, "", "CustomerID=" + dr["CustomerID"].ToString(), "");
                    SynchronousCustomer.UpdateClientCustomerData(dtCustomerClient, null);//将
                }
            }
        }
    }
}
