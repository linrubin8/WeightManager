using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Common.Synchronous
{
    public class SynchronousItem
    {
        /// <summary>
        /// 读取服务器所有的物料名称
        /// </summary>
        /// <returns></returns>
        public static DataTable ReadAllItemData()
        {
            DataTable dt = ExecuteSQL.CallView_Service(203);
            return dt;
        }


        public static void SynchronousItemFromServer()
        {
            DataTable dtAddItem;
            DataTable dtEditItem;
            CompareClientAndServer(out dtAddItem, out dtEditItem);

            UpdateClientItemData(dtAddItem, dtEditItem);
        }

        /// <summary>
        /// 比较本地与服务器的客户资料，返回差异情况
        /// </summary>
        /// <returns></returns>
        public static void CompareClientAndServer(out DataTable dtAddItem, out DataTable dtEditItem)
        {
            //读取上次同步数据时间
            DateTime dtItemSynchronousTime = Convert.ToDateTime("1990-01-01");
            string strItemSynchronousTime;
            SysConfigValue.GetSysConfig("ItemSynchronousTime", out strItemSynchronousTime);
            if (strItemSynchronousTime != "")
            {
                DateTime.TryParse(strItemSynchronousTime, out dtItemSynchronousTime);
            }

            //先读取服务器客户资料
            DataTable dtServer = ExecuteSQL.CallView_Service(203,"","ChangeTime>='"+ dtItemSynchronousTime.ToString("yyyy-MM-dd HH:mm:ss")+ "'", "ItemID");
            //读取本地客户资料
            DataTable dtClient = ExecuteSQL.CallView(203);

            Dictionary<string, DataRow> dictServer = new Dictionary<string, DataRow>();
            Dictionary<string, DataRow> dictClient = new Dictionary<string, DataRow>();
            foreach(DataRow dr in dtServer.Rows)
            {
                string strKey = dr["ItemName"].ToString().TrimEnd();
                if (!dictServer.ContainsKey(strKey))
                {
                    dictServer.Add(strKey, dr);
                }
            }

            foreach (DataRow dr in dtClient.Rows)
            {
                string strKey = dr["ItemName"].ToString().TrimEnd();
                if (!dictClient.ContainsKey(strKey))
                {
                    dictClient.Add(strKey, dr);
                }
            }

            dtAddItem = dtClient.Clone();
            dtAddItem.TableName = "SP";
            dtEditItem = dtClient.Clone();
            dtEditItem.TableName = "SP";
            foreach (KeyValuePair<string,DataRow> kvServer in dictServer)
            {
                if (!dictClient.ContainsKey(kvServer.Key))
                {
                    dtAddItem.ImportRow(kvServer.Value);
                }
                else
                {
                    DataRow drClient = dictClient[kvServer.Key];
                    foreach (DataColumn dc in dtServer.Columns)
                    {
                        if (!dc.ColumnName.Contains("Time") && dc.ColumnName!= "ItemID" && dtClient.Columns.Contains(dc.ColumnName))
                        {
                            if(kvServer.Value[dc.ColumnName].ToString().TrimEnd() != drClient[dc.ColumnName].ToString().TrimEnd())
                            {
                                dtEditItem.ImportRow(kvServer.Value);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 更新本地物料资料数据
        /// </summary>
        /// <param name="dtAddItem"></param>
        /// <param name="dtEditItem"></param>
        public static void UpdateClientItemData(DataTable dtAddItem, DataTable dtEditItem)
        {
            //添加物料
            DataSet dsReturn;
            DataTable dtOut;
            ExecuteSQL.CallSP(20300, dtAddItem, out dsReturn, out dtOut);

            //更新本地物料
            ExecuteSQL.CallSP(20301, dtEditItem, out dsReturn, out dtOut);

            SysConfigValue.SaveSysConfig("ItemSynchronousTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 将本地物料资料添加至服务器
        /// </summary>
        public static void AddServerItemData(DataTable dtCar, out DataSet dsReturn, out DataTable dtOut)
        {
            //添加客户
            dsReturn = null;
            dtOut = null;
            if (dtCar != null)
            {
                ExecuteSQL.CallSP_Service(20300, dtCar, out dsReturn, out dtOut);
            }
        }

        /// <summary>
        /// 将本地物料类别资料添加至服务器
        /// </summary>
        public static void AddServerItemTypeData(DataTable dtItemType, out DataSet dsReturn, out DataTable dtOut)
        {
            //添加客户
            dsReturn = null;
            dtOut = null;
            if (dtItemType != null)
            {
                ExecuteSQL.CallSP_Service(20500, dtItemType, out dsReturn, out dtOut);
            }
        }
    }
}
