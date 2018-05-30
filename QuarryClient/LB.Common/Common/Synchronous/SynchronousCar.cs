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
                        if (dc.ColumnName.Contains("CarNum") || dc.ColumnName.Contains("CustomerName"))
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
            //将客户清空
            foreach (DataRow dr in dtAddCar.Rows)
            {
                dr["CustomerID"] = DBNull.Value;
            }
            ExecuteSQL.CallSP(13500, dtAddCar, out dsReturn, out dtOut);

            //更新本地客户
            //将客户清空
            foreach (DataRow dr in dtEditCar.Rows)
            {
                dr["CustomerID"] = DBNull.Value;
            }
            ExecuteSQL.CallSP(13501, dtEditCar, out dsReturn, out dtOut);

            SysConfigValue.SaveSysConfig("CarSynchronousTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 将本地车辆资料添加至服务器
        /// </summary>
        public static void AddServerCarData(DataTable dtCar, out DataSet dsReturn, out DataTable dtOut)
        {
            //添加客户
            dsReturn = null;
            dtOut = null;
            if (dtCar != null)
            {
                //将客户清空
                foreach(DataRow dr in dtCar.Rows)
                {
                    dr["CustomerID"] = DBNull.Value;
                }
                ExecuteSQL.CallSP_Service(13500, dtCar, out dsReturn, out dtOut);
            }
        }

        /// <summary>
        /// 比较本地与服务器客户名称的差异，返回需要同步至服务器的客户数据
        /// </summary>
        /// <param name="dtAddCar"></param>
        public static void CompareClientDiffServer(out DataTable dtAddCar)
        {
            //先读取服务器客户资料
            DataTable dtServer = ExecuteSQL.CallView_Service(117, "CarNum", "", "CarID");
            //读取本地客户资料
            DataTable dtClient = ExecuteSQL.CallView(117);

            Dictionary<string, DataRow> dictServer = new Dictionary<string, DataRow>();
            Dictionary<string, DataRow> dictClient = new Dictionary<string, DataRow>();
            foreach (DataRow dr in dtServer.Rows)
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
            foreach (KeyValuePair<string, DataRow> kvClient in dictClient)
            {
                if (!dictServer.ContainsKey(kvClient.Key))
                {
                    dtAddCar.ImportRow(kvClient.Value);
                }
            }
        }
    }
}
