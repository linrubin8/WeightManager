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
        public static DataTable ReadUnSynchronousBill()
        {
            string strFilter = "isnull(IsSynchronousToServer,0) = 0 and (IsCancel =1 or SaleCarOutBillID is not null)";
            DataTable dtBill = ExecuteSQL.CallView(125, "", strFilter, "SaleCarOutBillID desc,SaleCarInBillID desc");
            return dtBill;
        }

        public static void SynchronousBillToServer(DataTable dtBill)
        {
            foreach(DataRow dr in dtBill.Rows)
            {
                long lCustomerID =0;
                long lCarID = 0;
                long lItemID = 0;
                long lItemTypeID = 0;


                //客户名称
                string strCustomerName = dr["CustomerName"].ToString().TrimEnd();
                //车辆名称
                string strCarName = dr["CarNum"].ToString().TrimEnd();
                //物料名称
                string strItemName = dr["ItemName"].ToString().TrimEnd();
                //物料分类
                string strItemTypeName = dr["ItemTypeName"].ToString().TrimEnd();
                //入场单号
                string strSaleCarInBillCode = dr["SaleCarInBillCode"].ToString().TrimEnd();
                long lSaleCarInBillID = LBConverter.ToInt32(dr["SaleCarInBillID"]);

                //判断服务器是否存在该客户
                DataTable dtCustomerServer = ExecuteSQL.CallView_Service(112,"CustomerID", "CustomerName='"+ strCustomerName + "'","");
                if (dtCustomerServer.Rows.Count == 0)//服务器不存在该客户
                {
                    DataSet dsOut;
                    DataTable dtOut;
                    DataTable dtCustomerClient = ExecuteSQL.CallView(112, "", "CustomerID=" + dr["CustomerID"].ToString(), "");
                    SynchronousCustomer.AddServerCustomerData(dtCustomerClient,out dsOut,out dtOut);//向服务器添加新客户
                    if (dtOut != null && dtOut.Rows.Count > 0)
                    {
                        foreach (DataColumn dc in dtOut.Columns)
                        {
                            if (dc.ColumnName.Contains("CustomerID"))
                            {
                                lCustomerID= LBConverter.ToInt64(dtOut.Rows[0]["CustomerID"]);
                            }
                        }
                    }
                }
                else
                {
                    lCustomerID = LBConverter.ToInt64(dtCustomerServer.Rows[0]["CustomerID"]);
                }
                //判断服务器是否存在该车辆
                DataTable dtCarServer = ExecuteSQL.CallView_Service(117, "CarID", "CarNum='" + strCarName + "'", "");
                if (dtCarServer.Rows.Count == 0)//服务器不存在该车辆
                {
                    DataSet dsOut;
                    DataTable dtOut;
                    DataTable dtCarClient = ExecuteSQL.CallView(117, "", "CarID=" + dr["CarID"].ToString(), "");
                    SynchronousCar.AddServerCarData(dtCarClient, out dsOut, out dtOut);//添加新客户
                    if (dtOut != null && dtOut.Rows.Count > 0)
                    {
                        foreach (DataColumn dc in dtOut.Columns)
                        {
                            if (dc.ColumnName.Contains("CarID"))
                            {
                                lCarID = LBConverter.ToInt64(dtOut.Rows[0]["CarID"]);
                            }
                        }
                    }
                }
                else
                {
                    lCarID = LBConverter.ToInt64(dtCarServer.Rows[0]["CarID"]);
                }

                //判断服务器是否存在该物料类别
                DataTable dtItemTypeServer = ExecuteSQL.CallDirectSQL_Service("select * from dbo.DbItemType where ItemTypeName='" + strItemTypeName+"'");
                if (dtItemTypeServer.Rows.Count == 0)//服务器不存在该物料类别
                {
                    DataSet dsOut;
                    DataTable dtOut;
                    DataTable dtItemTypeClient = ExecuteSQL.CallDirectSQL("select * from dbo.DbItemType where ItemTypeName='" + strItemTypeName + "'");
                    SynchronousItem.AddServerItemTypeData(dtItemTypeClient, out dsOut, out dtOut);//添加新物料
                    if (dtOut != null && dtOut.Rows.Count > 0)
                    {
                        foreach (DataColumn dc in dtOut.Columns)
                        {
                            if (dc.ColumnName.Contains("ItemTypeID"))
                            {
                                lItemTypeID = LBConverter.ToInt64(dtOut.Rows[0]["ItemTypeID"]);
                            }
                        }
                    }
                }
                else
                {
                    lItemTypeID = LBConverter.ToInt64(dtItemTypeServer.Rows[0]["ItemTypeID"]);
                }

                //判断服务器是否存在该物料
                DataTable dtItemServer = ExecuteSQL.CallView_Service(203, "ItemID", "ItemName='" + strItemName + "'", "");
                if (dtItemServer.Rows.Count == 0)//服务器不存在该物料
                {
                    DataSet dsOut;
                    DataTable dtOut;
                    DataTable dtItemClient = ExecuteSQL.CallView(203, "", "ItemID=" + dr["ItemID"].ToString(), "");
                    dtItemClient.Rows[0]["ItemTypeID"] = lItemTypeID;
                    SynchronousItem.AddServerItemData(dtItemClient, out dsOut, out dtOut);//添加新物料
                    if (dtOut != null && dtOut.Rows.Count > 0)
                    {
                        foreach (DataColumn dc in dtOut.Columns)
                        {
                            if (dc.ColumnName.Contains("ItemID"))
                            {
                                lItemID = LBConverter.ToInt64(dtOut.Rows[0]["ItemID"]);
                            }
                        }
                    }
                }
                else
                {
                    lItemID = LBConverter.ToInt64(dtItemServer.Rows[0]["ItemID"]);
                }

                if(lCustomerID>0 && lCarID>0 && lItemID > 0)
                {
                    dr["CustomerID"] = lCustomerID;
                    dr["CarID"] = lCarID;
                    dr["ItemID"] = lItemID;
                    DataTable dtData = dtBill.Clone();
                    dtData.ImportRow(dr);
                    //同步订单
                    DataSet dsReturn;
                    DataTable dtOut;

                    DataTable dtSP = new DataTable("SPIN");
                    dtSP.Columns.Add("DTInOutBill", typeof(DataTable));
                    dtSP.Rows.Add(dtData);
                    dtSP.AcceptChanges();
                    ExecuteSQL.CallSP_Service(14122, dtSP, out dsReturn, out dtOut);

                    //同步成功后将当前单据的同步状态改为已同步
                    dtSP = new DataTable("SPIN");
                    dtSP.Columns.Add("SaleCarInBillID", typeof(long));
                    dtSP.Rows.Add(lSaleCarInBillID);
                    dtSP.AcceptChanges();
                    ExecuteSQL.CallSP(14123, dtSP, out dsReturn, out dtOut);
                }
                else
                {
                    string strMsg = "";
                    if (lCustomerID == 0)
                    {
                        strMsg +="[客户资料]";
                    }
                    if (lCarID == 0)
                    {
                        strMsg += "[车辆资料]";
                    }
                    if (lItemID == 0)
                    {
                        strMsg += "[物料资料]";
                    }
                    throw new Exception("入场订单["+ strSaleCarInBillCode + "]同步失败！无法同步"+ strMsg);
                }
            }
        }
    }
}
