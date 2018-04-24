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

        }

        /// <summary>
        /// 比较本地与服务器的客户资料，返回差异情况
        /// </summary>
        /// <returns></returns>
        public static void CompareClientAndServer(out DataTable dtAddCustomer,out DataTable dtEditCustomer)
        {
            //先读取服务器客户资料
            DataTable dtServer = ReadAllCustomerName();
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
            
            dtAddCustomer = new DataTable();
            dtEditCustomer = new DataTable();
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
                        if (dc.ColumnName!="CustomerID" && dtClient.Columns.Contains(dc.ColumnName))
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
    }
}
