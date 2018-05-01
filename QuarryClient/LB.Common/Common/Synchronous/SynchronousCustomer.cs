using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Common.Synchronous
{
    public class SynchronousCustomer
    {
        /// <summary>
        /// 读取服务器所有的客户名称
        /// </summary>
        /// <returns></returns>
        public static DataTable ReadAllCustomerName()
        {
            DataTable dt = ExecuteSQL.CallView_Service(112);
            return dt;
        }


        public static void SynchronousCustomerFromServer()
        {
            DataTable dtAddCustomer;
            DataTable dtEditCustomer;
            CompareClientAndServer(out dtAddCustomer, out dtEditCustomer);

            UpdateClientCustomerData(dtAddCustomer, dtEditCustomer);

            SysConfigValue.SaveSysConfig("CustomerSynchronousTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 比较本地与服务器的客户资料，返回差异情况
        /// </summary>
        /// <returns></returns>
        public static void CompareClientAndServer(out DataTable dtAddCustomer,out DataTable dtEditCustomer)
        {
            //读取上次同步数据时间
            DateTime dtCustomerSynchronousTime = Convert.ToDateTime("1990-01-01");
            string strCustomerSynchronousTime;
            SysConfigValue.GetSysConfig("CustomerSynchronousTime", out strCustomerSynchronousTime);
            if (strCustomerSynchronousTime != "")
            {
                DateTime.TryParse(strCustomerSynchronousTime, out dtCustomerSynchronousTime);
            }

            //先读取服务器客户资料
            DataTable dtServer = ExecuteSQL.CallView_Service(112,"","ChangeTime>='"+ dtCustomerSynchronousTime .ToString("yyyy-MM-dd HH:mm:ss")+ "'","CustomerID");
            //读取本地客户资料
            DataTable dtClient = ExecuteSQL.CallView(112);

            Dictionary<string, DataRow> dictServer = new Dictionary<string, DataRow>();
            Dictionary<string, DataRow> dictClient = new Dictionary<string, DataRow>();
            foreach(DataRow dr in dtServer.Rows)
            {
                string strKey = dr["CustomerName"].ToString().TrimEnd();
                if (!dictServer.ContainsKey(strKey))
                {
                    dictServer.Add(strKey, dr);
                }
            }

            foreach (DataRow dr in dtClient.Rows)
            {
                string strKey = dr["CustomerName"].ToString().TrimEnd();
                if (!dictClient.ContainsKey(strKey))
                {
                    dictClient.Add(strKey, dr);
                }
            }

            dtAddCustomer = dtClient.Clone();
            dtAddCustomer.TableName = "SP";
            dtEditCustomer = dtClient.Clone();
            dtEditCustomer.TableName = "SP";
            foreach (KeyValuePair<string,DataRow> kvServer in dictServer)
            {
                if (!dictClient.ContainsKey(kvServer.Key))
                {
                    dtAddCustomer.ImportRow(kvServer.Value);
                }
                else
                {
                    DataRow drClient = dictClient[kvServer.Key];
                    foreach (DataColumn dc in dtServer.Columns)
                    {
                        if (!dc.ColumnName.Contains("Time") && dc.ColumnName!="CustomerID" && dtClient.Columns.Contains(dc.ColumnName))
                        {
                            if(kvServer.Value[dc.ColumnName].ToString().TrimEnd() != drClient[dc.ColumnName].ToString().TrimEnd())
                            {
                                dtEditCustomer.ImportRow(kvServer.Value);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 更新本地客户资料数据
        /// </summary>
        /// <param name="dtAddCustomer"></param>
        /// <param name="dtEditCustomer"></param>
        public static void UpdateClientCustomerData(DataTable dtAddCustomer, DataTable dtEditCustomer)
        {
            //添加客户
            DataSet dsReturn;
            DataTable dtOut;
            if (dtAddCustomer != null)
            {
                ExecuteSQL.CallSP(13400, dtAddCustomer, out dsReturn, out dtOut);
            }

            //更新本地客户
            if (dtEditCustomer != null)
            {
                ExecuteSQL.CallSP(13401, dtEditCustomer, out dsReturn, out dtOut);
            }
        }

        /// <summary>
        /// 将本地客户资料添加至服务器
        /// </summary>
        public static void AddServerCustomerData(DataTable dtCustomer,out DataSet dsReturn,out DataTable dtOut)
        {
            //添加客户
            dsReturn = null;
            dtOut = null;
            if (dtCustomer != null)
            {
                ExecuteSQL.CallSP_Service(13400, dtCustomer, out dsReturn, out dtOut);
            }
        }
    }
}
