using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Common.Synchronous
{
    public class SynchronousPrice
    {
        public static void SynchronousClientFromServer()
        {
            string strMsg;
            bool bolIsAllSyn;
            SynchronousClientFromServer(out strMsg, out bolIsAllSyn);
            if (!bolIsAllSyn&& strMsg!="")
            {
                throw new Exception(strMsg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static void SynchronousClientFromServer(out string strMsg,out bool bolIsAllSyn)
        {
            strMsg = "";
            bolIsAllSyn = false;
            DataTable dtModifyHeaderServer = null;
            DataTable dtModifyDetailServer = null;
            //读取上次同步数据时间
            DateTime dtPriceSynchronousTime = Convert.ToDateTime("1990-01-01");
            string strPriceSynchronousTime;
            SysConfigValue.GetSysConfig("PriceSynchronousTime", out strPriceSynchronousTime);
            //先读取服务器价格资料
            dtModifyHeaderServer = ExecuteSQL.CallView_Service(115, "", "IsApprove=1 and ApproveTime>='" + strPriceSynchronousTime + "'", "ApproveTime");

            string strModifyBillHeaderIDs = "";
            foreach (DataRow dr in dtModifyHeaderServer.Rows)
            {
                string strModifyBillHeaderID = dr["ModifyBillHeaderID"].ToString();
                if (strModifyBillHeaderIDs != "")
                    strModifyBillHeaderIDs += ",";
                strModifyBillHeaderIDs += strModifyBillHeaderID;
            }

            if (strModifyBillHeaderIDs != "")
            {
                dtModifyDetailServer = ExecuteSQL.CallView_Service(116, "", "ModifyBillHeaderID in (" + strModifyBillHeaderIDs + ")", "");
            }

            foreach (DataRow dr in dtModifyHeaderServer.Rows)
            {
                long lServerModifyBillHeaderID= LBConverter.ToInt64(dr["ModifyBillHeaderID"]);
                DataTable dtCopy = dtModifyHeaderServer.Clone();

                //客户名称
                string strCustomerName_Server = dr["CustomerName"].ToString().TrimEnd();

                try
                {
                    //读取客户ID
                    long lCustomerIDClient;
                    bool bolExistsCustomer = GetCustomerIDFromClient(strCustomerName_Server, out lCustomerIDClient);
                    if (!bolExistsCustomer)//本地不存在该客户
                    {
                        throw new Exception("本地账套不存在该客户名称【" + strCustomerName_Server + "】！");
                    }
                    else
                    {
                        dr["CustomerID"] = lCustomerIDClient;
                        dtModifyDetailServer.DefaultView.RowFilter = "ModifyBillHeaderID = " + lServerModifyBillHeaderID.ToString();
                        foreach (DataRowView drvDetail in dtModifyDetailServer.DefaultView)
                        {
                            string strItemNameServer = drvDetail["ItemName"].ToString().TrimEnd();
                            string strCarNumServer = drvDetail["CarNum"].ToString().TrimEnd();

                            if (strCarNumServer != "")
                            {
                                long lCarIDClient;
                                bool bolExistsCar = GetCarIDFromClient(strCarNumServer, out lCarIDClient);
                                if (!bolExistsCar)
                                {
                                    throw new Exception("本地账套不存在该车牌【" + strCarNumServer + "】！");
                                }
                                drvDetail["CarID"] = lCarIDClient;
                            }

                            if (strItemNameServer != "")
                            {
                                long lItemIDClient;
                                bool bolExistsItem = GetItemIDFromClient(strItemNameServer, out lItemIDClient);
                                if (!bolExistsItem)
                                {
                                    throw new Exception("本地账套不存在该物料【" + strItemNameServer + "】！");
                                }
                                drvDetail["ItemID"] = lItemIDClient;
                            }
                        }
                    }

                    dtCopy.ImportRow(dr);

                    int iSPType = 13600;
                    DataSet dsReturn;
                    DataTable dtOut;
                    ExecuteSQL.CallSP(iSPType, dtCopy, out dsReturn, out dtOut);
                    if (dtOut != null && dtOut.Rows.Count > 0)
                    {
                        if (dtOut.Columns.Contains("ModifyBillHeaderID"))
                        {
                            long lModifyBillHeaderID = LBConverter.ToInt64(dtOut.Rows[0]["ModifyBillHeaderID"]);
                            if (lModifyBillHeaderID > 0)
                            {
                                dtModifyDetailServer.DefaultView.RowFilter = "ModifyBillHeaderID = " + lServerModifyBillHeaderID.ToString();
                                DataTable dtModifyDetail = dtModifyDetailServer.DefaultView.ToTable();
                                foreach (DataRow drDetailClient in dtModifyDetail.Rows)
                                {
                                    drDetailClient["ModifyBillHeaderID"] = lModifyBillHeaderID;
                                }
                                DataTable dtOutDetail;
                                ExecuteSQL.CallSP(13700, dtModifyDetail, out dsReturn, out dtOutDetail);

                                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                                parmCol.Add(new LBParameter("ModifyBillHeaderID", enLBDbType.Int64, lModifyBillHeaderID));
                                
                                Dictionary<string, object> dictValue;
                                ExecuteSQL.CallSP(13603, parmCol, out dsReturn, out dictValue);//审核
                                SysConfigValue.SaveSysConfig("PriceSynchronousTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (strMsg != "")
                        strMsg += "\n";
                    strMsg += ex.Message;
                    bolIsAllSyn = false;
                }
            }
        }

        private static bool GetCustomerIDFromClient(string strCustomerName,out long lCustomerID)
        {
            bool bolExists = true;
            lCustomerID = 0;
            //判断服务器是否存在该客户
            DataTable dtCustomerClient = ExecuteSQL.CallView(112, "CustomerID", "CustomerName='" + strCustomerName + "'", "");
            if (dtCustomerClient.Rows.Count > 0)//本地不存在该客户
            {
                long.TryParse(dtCustomerClient.Rows[0]["CustomerID"].ToString(), out lCustomerID);
                bolExists = true;
            }
            else
            {
                bolExists = false;
            }
            return bolExists;
        }

        private static bool GetItemIDFromClient(string strItemName, out long lItemID)
        {
            bool bolExists = true;
            lItemID = 0;
            //判断服务器是否存在该客户
            DataTable dtItemClient = ExecuteSQL.CallView(203, "ItemID", "ItemName='" + strItemName + "'", "");
            if (dtItemClient.Rows.Count > 0)//本地不存在该客户
            {
                long.TryParse(dtItemClient.Rows[0]["ItemID"].ToString(), out lItemID);
                bolExists = true;
            }
            else
            {
                bolExists = false;
            }
            return bolExists;
        }

        private static bool GetCarIDFromClient(string strCarNum, out long lCarID)
        {
            bool bolExists = true;
            lCarID = 0;
            //判断服务器是否存在该客户
            DataTable dtCar = ExecuteSQL.CallView(117, "CarID", "CarNum='" + strCarNum + "'", "");
            if (dtCar.Rows.Count > 0)//本地不存在该客户
            {
                long.TryParse(dtCar.Rows[0]["CarID"].ToString(), out lCarID);
                bolExists = true;
            }
            else
            {
                bolExists = false;
            }
            return bolExists;
        }
    }
}
