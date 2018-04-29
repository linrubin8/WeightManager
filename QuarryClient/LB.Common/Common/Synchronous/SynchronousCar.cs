using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Common.Synchronous
{
    public class SynchronousCar
    {
        /// <summary>
        /// 读取服务器所有的客户名称
        /// </summary>
        /// <returns></returns>
        public static DataTable ReadAllCustomerCar()
        {
            DataTable dt = ExecuteSQL.CallView_Service(117);
            return dt;
        }


        public static void SynchronousCarFromServer()
        {
            DataTable dtAddCar;
            DataTable dtEditCar;
            CompareClientAndServer(out dtAddCar, out dtEditCar);

            UpdateClientCarData(dtAddCar, dtEditCar);
        }

        /// <summary>
        /// 比较本地与服务器的客户资料，返回差异情况
        /// </summary>
        /// <returns></returns>
        public static void CompareClientAndServer(out DataTable dtAddCar, out DataTable dtEditCar)
        {
            //读取上次同步数据时间
            DateTime dtCarSynchronousTime = Convert.ToDateTime("1990-01-01");
            string strCarSynchronousTime;
            SysConfigValue.GetSysConfig("CarSynchronousTime", out strCarSynchronousTime);
            if (strCarSynchronousTime != "")
            {
                DateTime.TryParse(strCarSynchronousTime, out dtCarSynchronousTime);
            }

            //先读取服务器客户资料
            DataTable dtServer = ExecuteSQL.CallView_Service(117,"","ChangeTime>='"+ dtCarSynchronousTime.ToString("yyyy-MM-dd HH:mm:ss")+ "'","CarID");
            //读取本地客户资料
            DataTable dtClient = ExecuteSQL.CallView(117);

            Dictionary<string, DataRow> dictServer = new Dictionary<string, DataRow>();
            Dictionary<string, DataRow> dictClient = new Dictionary<string, DataRow>();
            foreach(DataRow dr in dtServer.Rows)
            {
                string strKey = dr["CarNum"].ToString().TrimEnd();
                if (!dictServer.ContainsKey(strKey))
                {
                    dictServer.Add(strKey, dr);
                }
            }

            foreach (DataRow dr in dtClient.Rows)
            {
                string strKey = dr["CarNum"].ToString().TrimEnd();
                if (!dictClient.ContainsKey(strKey))
                {
                    dictClient.Add(strKey, dr);
                }
            }

            dtAddCar = dtClient.Clone();
            dtAddCar.TableName = "SP";
            dtEditCar = dtClient.Clone();
            dtEditCar.TableName = "SP";
            foreach (KeyValuePair<string,DataRow> kvServer in dictServer)
            {
                if (!dictClient.ContainsKey(kvServer.Key))
                {
                    dtAddCar.ImportRow(kvServer.Value);
                }
                else
                {
                    DataRow drClient = dictClient[kvServer.Key];
                    foreach (DataColumn dc in dtServer.Columns)
                    {
                        if (!dc.ColumnName.Contains("Time") && dc.ColumnName!= "CarID" && dtClient.Columns.Contains(dc.ColumnName))
                        {
                            if(kvServer.Value[dc.ColumnName].ToString().TrimEnd() != drClient[dc.ColumnName].ToString().TrimEnd())
                            {
                                dtEditCar.ImportRow(kvServer.Value);
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
        public static void UpdateClientCarData(DataTable dtAddCar, DataTable dtEditCar)
        {
            //添加客户
            DataSet dsReturn;
            DataTable dtOut;
            ExecuteSQL.CallSP(13500, dtAddCar, out dsReturn, out dtOut);

            //更新本地客户
            ExecuteSQL.CallSP(13501, dtEditCar, out dsReturn, out dtOut);

            SysConfigValue.SaveSysConfig("CarSynchronousTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
