﻿using IFaxBusiness;
using LB.Web.Base.Base.Helper;
using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.Remoting.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LB.Web.Remoting
{
    public class WebRemoting: MarshalByRefObject, IMyFaxBusiness
    {
        public static event FaxEventHandler FaxSendedEvent;

        public void SendFax(string fax)
        {
            if (FaxSendedEvent != null)
            {
                FaxSendedEvent(fax);
            }
        }
        private static string mstrDBName = "";
        public static string DBName
        {
            get
            {
                return mstrDBName;
            }
        }
        private static string mstrServer = "";
        private static string mstrDBUser = "";
        private static string mstrDBPw = "";
        private static bool mbolLoginSecure = false;
        private static string mstrWebServiceAddress = "";
        public static void SetRemotingInfo(string strDBName, string strServer,bool bolLoginSecure,string strDBUser,string strDBPw,string strWebServiceAddress)
        {
            LB.Web.Encrypt.LBEncrypt.Decrypt();//校验注册信息
            mstrDBName = strDBName;
            mstrServer = strServer;
            mbolLoginSecure = bolLoginSecure;
            mstrDBUser = strDBUser;
            mstrDBPw = strDBPw;
            mstrWebServiceAddress = strWebServiceAddress;

            DBHelper.DBName = strDBName;
            DBHelper.DBServer = strServer;
            DBHelper.LoginSecure = bolLoginSecure;
            DBHelper.DBUer = strDBUser;
            DBHelper.DBPw = strDBPw;
        }

        //public static event RemotingEventHandler RemotingSendedEvent;

        #region

        //public void SendRemoting(string msg)
        //{
        //    if (RemotingSendedEvent != null)
        //    {
        //        RemotingSendedEvent(msg);
        //    }
        //}

        public override object InitializeLifetimeService()
        {
            return null;
        }

        #endregion

        public string RunProcedure(int ProcedureType, long SessionID, bool IsNeedSession, string strLoginName, byte[] bSerializeValue, byte[] bSerializeDataType,
            out string strOut, out string ErrorMsg, out bool bolIsError)
        {
            strOut = "";
            DataTable dtOut = null;
            bolIsError = false;
            DataSet dsReturn = null;
            ErrorMsg = "";
            try
            {
                string strConn = GetConnectionStr();
                SQLServerDAL.GetConnectionString = strConn;

                DataTable dtParmValue = new DataTable("SPIN");
                List<Dictionary<object, object>> lstDictValue = DeserializeObject(bSerializeValue) as List<Dictionary<object, object>>;
                Dictionary<object, object> dictDataType = DeserializeObject(bSerializeDataType) as Dictionary<object, object>;

                foreach (KeyValuePair<object, object> keyvalue in dictDataType)
                {
                    dtParmValue.Columns.Add(keyvalue.Key.ToString(), GetType(keyvalue.Value.ToString()));
                }

                foreach (Dictionary<object, object> dictValue in lstDictValue)
                {
                    DataRow drNew = dtParmValue.NewRow();
                    foreach (KeyValuePair<object, object> keyvalue in dictValue)
                    {
                        drNew[keyvalue.Key.ToString()] = keyvalue.Value;
                    }
                    dtParmValue.Rows.Add(drNew);
                }
                dtParmValue.AcceptChanges();

                DBHelper.Provider = new DBMSSQL();
                SqlConnection con = new SqlConnection(SQLServerDAL.GetConnectionString);
                string strDBName = con.Database;
                DBMSSQL.InitSettings(5000, con.DataSource, strDBName, true, "", "");
                con.Close();

                //LBFactory factory = new LBFactory();
                //IBLLFunction function = factory.GetAssemblyFunction(ProcedureType);
                IBLLFunction function = DBHelper.GetFunctionMethod(ProcedureType);
                if (function == null)
                {
                    #region -- 调用存储过程 --

                    DataTable dtView = SQLServerDAL.Query("select * from dbo.SysSPType where SysSPType=" + ProcedureType);
                    if (dtView.Rows.Count > 0)
                    {
                        DataRow drView = dtView.Rows[0];
                        string strSysSPName = drView["SysSPName"].ToString().TrimEnd();
                        SQLServerDAL.ExecuteProcedure(strSysSPName, dtParmValue, out dtOut, out dsReturn);
                    }
                    else
                    {
                        throw new Exception("存储过程号【" + ProcedureType + "】不存在！");
                    }

                    #endregion
                }
                else
                {
                    #region -- 调用中间层程序方法 --
                    
                    string strMethod = function.GetFunctionName(ProcedureType);
                    string str = function.ToString();

                    string strLoad = str.Substring(0, str.IndexOf('.', 8));
                    Assembly s = Assembly.Load(strLoad);
                    Type tpe = s.GetType(str);


                    //调用GetName方法
                    MethodInfo method = tpe.GetMethod(strMethod);

                    Dictionary<string, string> dictParmType = new Dictionary<string, string>();
                    ParameterInfo[] parameterInfos = method.GetParameters();
                    foreach (ParameterInfo parmInfo in parameterInfos)
                    {
                        if (parmInfo.ParameterType.Name != "FactoryArgs")
                        {
                            string strParmTypeName = parmInfo.ParameterType.Name.Replace("&", "");
                            if (!dictParmType.ContainsKey(strParmTypeName))
                            {
                                dictParmType.Add(parmInfo.Name, strParmTypeName);
                            }
                        }
                    }

                    int iRowIndex = 0;

                    if (dtParmValue == null || dtParmValue.Rows.Count == 0)
                        return null;
                    
                    FactoryArgs factoryArgs = new FactoryArgs(strDBName, strLoginName, SessionID, IsNeedSession, null, null);

                    #region -- 校验Session --

                    if (factoryArgs.SessionID > 0)
                    {
                        bool bolExistsSession = SessionTimer.VerifySession(factoryArgs);
                        if (IsNeedSession && !bolExistsSession)
                        {
                            bolIsError = true;
                            ErrorMsg = "长时间未操作或者被其他人逼退，请重新登录！";
                            return "";
                        }
                    }

                    #endregion -- 校验Session --

                    foreach (DataRow drParmValue in dtParmValue.Rows)
                    {
                        //获取需要传入的参数
                        ParameterInfo[] parms = method.GetParameters();

                        Dictionary<int, string> dictOutFieldName = new Dictionary<int, string>();
                        object[] objValue = new object[parms.Length];
                        int iParmIndex = 0;
                        foreach (ParameterInfo ss in parms)
                        {
                            string strParmName = ss.Name;
                            if (ss.ParameterType == typeof(FactoryArgs))
                            {
                                objValue[iParmIndex] = factoryArgs;
                            }
                            else if (ss.Attributes != ParameterAttributes.Out)
                            {
                                if (dtParmValue.Columns.Contains(strParmName))
                                {
                                    DataColumn dc = dtParmValue.Columns[strParmName];
                                    object value = null;
                                    if (dc.DataType == typeof(long) ||
                                        dc.DataType == typeof(decimal) ||
                                        dc.DataType == typeof(float) ||
                                        dc.DataType == typeof(double) ||
                                        dc.DataType == typeof(int) ||
                                        dc.DataType == typeof(byte) ||
                                        dc.DataType == typeof(bool))
                                    {
                                        if (drParmValue[strParmName] == DBNull.Value)
                                        {
                                            if (dictParmType.ContainsKey(strParmName))
                                            {
                                                ILBDbType lbType = LBDBType.GetILBDbType(dictParmType[strParmName]);
                                                value = lbType;
                                            }
                                            else
                                            {
                                                value = new t_Decimal();
                                            }
                                        }
                                        else
                                        {
                                            if (dictParmType.ContainsKey(strParmName))
                                            {
                                                ILBDbType lbType = LBDBType.GetILBDbType(dictParmType[strParmName]);
                                                lbType.SetValueWithObject(drParmValue[strParmName]);
                                                value = lbType;
                                            }
                                            else
                                            {
                                                value = new t_Decimal(drParmValue[strParmName]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (dictParmType.ContainsKey(strParmName))
                                        {
                                            ILBDbType lbType = LBDBType.GetILBDbType(dictParmType[strParmName]);
                                            lbType.SetValueWithObject(drParmValue[strParmName]);
                                            value = lbType;
                                        }
                                        else
                                        {
                                            value = new t_String(drParmValue[strParmName]);
                                        }
                                    }
                                    objValue[iParmIndex] = value;

                                }
                            }
                            else
                            {
                                if (dictParmType.ContainsKey(strParmName))
                                {
                                    ILBDbType lbType = LBDBType.GetILBDbType(dictParmType[strParmName]);
                                    lbType.SetValueWithObject(null);
                                    objValue[iParmIndex] = lbType;
                                }

                                if (dtOut == null)
                                {
                                    dtOut = new DataTable("Out");
                                }
                                if (!dtOut.Columns.Contains(strParmName))
                                {
                                    dtOut.Columns.Add(strParmName, typeof(object));
                                }
                                dictOutFieldName.Add(iParmIndex, strParmName);
                            }

                            iParmIndex++;
                        }

                        if (dtOut != null)
                        {
                            dtOut.Rows.Add(dtOut.NewRow());
                        }

                        //获取Car对象
                        object obj = s.CreateInstance(str);

                        //如果有返回值接收下
                        method.Invoke(obj, objValue);
                        int iobjReturnIndex = 0;
                        foreach (object objReturn in objValue)
                        {
                            if (objReturn is FactoryArgs)
                            {
                                FactoryArgs args = (FactoryArgs)objReturn;
                                if (args.SelectResult != null)
                                {
                                    if (dsReturn == null)
                                    {
                                        dsReturn = new DataSet("DSResult");
                                    }
                                    args.SelectResult.TableName = "Return" + iRowIndex.ToString();
                                    dsReturn.Tables.Add(args.SelectResult.Copy());
                                }
                            }
                            if (dictOutFieldName.ContainsKey(iobjReturnIndex))
                            {
                                if (objReturn is ILBDbType)
                                {
                                    ILBDbType lbtype = objReturn as ILBDbType;
                                    if (lbtype.Value != null)
                                    {
                                        dtOut.Rows[0][dictOutFieldName[iobjReturnIndex]] = lbtype.Value;
                                    }
                                }
                            }
                            iobjReturnIndex++;
                        }
                        iRowIndex++;
                    }

                    #endregion -- 调用中间层程序方法 --
                }

                strOut = RarDataTable(dtOut);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ErrorMsg = ex.InnerException.Message;
                else
                    ErrorMsg = ex.Message;
                bolIsError = true;
                return "";
            }
            return RarDataSet(dsReturn);
        }

        public string RunView(int iViewType, long SessionID, bool IsNeedSession, string strLoginName, string strFieldNames, string strWhere, string strOrderBy,
            out string ErrorMsg, out bool bolIsError)
        {
            VerifyEncrypt();
            DataTable dtReturn = null;
            bolIsError = false;
            ErrorMsg = "";
            //BackUpHelper.StartBackUp(AppDomain.CurrentDomain.BaseDirectory);
            //LogHelper.WriteLog("正在调用" + iViewType.ToString());
            try
            {
                string strConn = GetConnectionStr();
                SQLServerDAL.GetConnectionString = strConn;
                //LogHelper.WriteLog(SQLServerDAL.GetConnectionString);
                DataTable dtView = SQLServerDAL.Query("select * from dbo.SysViewType where SysViewType=" + iViewType);
                //LogHelper.WriteLog("查询语句成功！");
                if (dtView.Rows.Count == 0)
                {
                    //LogHelper.WriteLog("查询出错！视图号：【" + iViewType + "】不存在！");
                    throw new Exception("查询出错！视图号：【" + iViewType + "】不存在！");
                }
                //LogHelper.WriteLog("SysViewName");
                string strSysViewName = dtView.Rows[0]["SysViewName"].ToString().TrimEnd();
                //LogHelper.WriteLog(strSysViewName);
                DataTable dtViewExists = SQLServerDAL.Query(@"
select * from sysobjects 
where id = object_id(N'[" + strSysViewName + @"]')
");
                if (dtViewExists.Rows.Count == 0)
                {
                    throw new Exception("查询出错！视图名称：【" + strSysViewName + "】不存在！");
                }

                string strFields = string.IsNullOrEmpty(strFieldNames) ? "*" : strFieldNames;
                //LogHelper.WriteLog(strFields);
                strWhere = string.IsNullOrEmpty(strWhere) ? "" : "where " + strWhere;
                strOrderBy = string.IsNullOrEmpty(strOrderBy) ? "" : "Order By " + strOrderBy;
                string strSQL = @"
select {0}
from {1}
{2}
{3}
";
                //LogHelper.WriteLog(strSQL);
                strSQL = string.Format(strSQL, strFields, strSysViewName, strWhere, strOrderBy);
                dtReturn = SQLServerDAL.Query(strSQL);
                dtReturn.TableName = "Result";
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ErrorMsg = ex.InnerException.Message;
                else
                    ErrorMsg = ex.Message;
                //LogHelper.WriteLog(ErrorMsg);
                bolIsError = true;
                return "";
            }
            return RarDataTable(dtReturn);
        }

        public string RunDirectSQL(long SessionID, bool IsNeedSession, string strLoginName, string strSQL,
            out string ErrorMsg, out bool bolIsError)
        {
            DataTable dtReturn = null;
            bolIsError = false;
            ErrorMsg = "";

            try
            {
                string strConn = GetConnectionStr();
                SQLServerDAL.GetConnectionString = strConn;
                dtReturn = SQLServerDAL.Query(strSQL);
                dtReturn.TableName = "Result";
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ErrorMsg = ex.InnerException.Message;
                else
                    ErrorMsg = ex.Message;
                bolIsError = true;
                return "";
            }
            return RarDataTable(dtReturn);
        }

        public DataTable ReadClientFileInfo()
        {
            return ClientHelper.GetLocalFile();
        }

        public void ReadFileByte(string strFileFullName, int iPosition, int iMaxLength, out byte[] bSplitFile)
        {
            bSplitFile = null;
            //int iMaxLength = 2048;//每次最大的下载长度
            string strStartUp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Client");
            string strFullName = Path.Combine(strStartUp, strFileFullName);
            if (File.Exists(strFullName))
            {
                using (FileStream fileStream = new FileStream(strFullName, FileMode.Open, FileAccess.Read))
                {
                    fileStream.Seek(iPosition, SeekOrigin.Begin);

                    if (fileStream.Length - iPosition > iMaxLength)
                    {
                        bSplitFile = new byte[iMaxLength];
                    }
                    else
                    {
                        bSplitFile = new byte[fileStream.Length - iPosition];
                    }

                    fileStream.Read(bSplitFile, 0, bSplitFile.Length);
                    fileStream.Close();
                }
            }
        }


        /// <summary>
        /// 动态加载所有BLL
        /// </summary>
        /// <param name="iProcedureType"></param>
        /// <returns></returns>
        public static void LoadAllBLLFunction()
        {
            string strBasePath = AppDomain.CurrentDomain.BaseDirectory;
            //LogHelper.WriteLog("搜索路径" + strBasePath);
            string[] strfis = Directory.GetFiles(strBasePath);
            //LogHelper.WriteLog("搜索到" + strfis.Length.ToString()+"个DLL程序");
            foreach (string strFile in strfis)
            {
                FileInfo fi = new FileInfo(strFile);
                string strDLLName = fi.Name;
                //LogHelper.WriteLog("加载" + strDLLName + "");
                if (strDLLName.StartsWith("LB.Web.") && fi.Extension.ToLower().Contains("dll"))
                {
                    Assembly s = Assembly.LoadFrom(strFile);
                    //LogHelper.WriteLog("已Load：" + fi.Name.Replace(fi.Extension, "") + "");
                    Type tpe = s.GetType(fi.Name.Replace(fi.Extension, "") + ".BLL.Factory.AssemblyStart", false, true);

                    //Type[] types = s.GetTypes();
                    //foreach(Type t in types)
                    //{
                    //    //if(t.FullName.Contains("AssemblyStart"))
                    //        LogHelper.WriteLog(strDLLName+":"+t.Namespace+"  "+ t.FullName);
                    //}

                    if (tpe != null)
                    {
                        //获取对象
                        object obj = Activator.CreateInstance(tpe, true);

                        //调用非静态方法  
                        MethodInfo method = tpe.GetMethod("AssemblyStart");

                        if (method != null)
                        {
                            int result = (int)tpe.InvokeMember("AssemblyStart", BindingFlags.InvokeMethod, null, obj, null);
                           // LogHelper.WriteLog("成功加载：" + strDLLName);
                        }
                    }
                }
            }

            SessionTimer.StartListenSession();
        }

        public static string GetConnectionStr()
        {
            string strConn = "";
            if(!mbolLoginSecure)
                strConn=  "Server=" + mstrServer + ";Database=" + mstrDBName + ";Trusted_Connection=Yes;Connect Timeout=90;";
            else
                strConn = "Server=" + mstrServer + ";Database=" + mstrDBName + ";User ID="+mstrDBUser+";Password="+mstrDBPw+";Trusted_Connection=Yes;Connect Timeout=90;";

            //EventLog log = new EventLog();
            //log.WriteEntry("连接字符串=" + strConn, EventLogEntryType.Information);
            return strConn;
        }

        public bool ConnectServer()
        {
            return true;
        }

        public static void StopServer()
        {
            DBHelper.StopServer();
            SessionTimer.StopListenSession();
        }

        //反序列化
        public static object DeserializeObject(byte[] pBytes)
        {
            object newOjb = null;
            if (pBytes == null)
            {
                return newOjb;
            }

            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(pBytes);
            memoryStream.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            newOjb = formatter.Deserialize(memoryStream);
            memoryStream.Close();

            return newOjb;
        }

        private static Type GetType(string strType)
        {
            if (strType.Contains("DataTable"))
            {
                return typeof(DataTable);
            }
            return Type.GetType(strType, true, true); ;
        }

        private void VerifyEncrypt()
        {
            
           
        }

        public void ReadRegister(out bool IsRegister, out int ProductType, out string RegisterInfoJson, out DateTime DeadLine)
        {
            IsRegister = LB.Web.Encrypt.LBEncrypt.IsRegister;
            DeadLine = LB.Web.Encrypt.LBEncrypt.DeadLine;
            ProductType = LB.Web.Encrypt.LBEncrypt.ProductType;
            RegisterInfoJson = LB.Web.Encrypt.LBEncrypt.RegisterInfoJson;
        }

        //调用网络接口
        public string RunProcedure_Service(int ProcedureType, string strLoginName, byte[] bSerializeValue, byte[] bSerializeDataType,
            out string strOut, out string ErrorMsg, out bool bolIsError)
        {
            strOut = "";
            ErrorMsg = "";
            bolIsError = false;
            DataSet dsReturn = null;

            WebSerivce.BaseInfoService service = new WebSerivce.BaseInfoService();
            service.Url = mstrWebServiceAddress;
            try
            {
                DataTable dtParmValue = new DataTable("SPIN");
                List<Dictionary<object, object>> lstDictValue = DeserializeObject(bSerializeValue) as List<Dictionary<object, object>>;
                Dictionary<object, object> dictDataType = DeserializeObject(bSerializeDataType) as Dictionary<object, object>;

                foreach (KeyValuePair<object, object> keyvalue in dictDataType)
                {
                    dtParmValue.Columns.Add(keyvalue.Key.ToString(), GetType(keyvalue.Value.ToString()));
                }

                foreach (Dictionary<object, object> dictValue in lstDictValue)
                {
                    DataRow drNew = dtParmValue.NewRow();
                    foreach (KeyValuePair<object, object> keyvalue in dictValue)
                    {
                        drNew[keyvalue.Key.ToString()] = keyvalue.Value;
                    }
                    dtParmValue.Rows.Add(drNew);
                }
                dtParmValue.AcceptChanges();


                DataTable dtOut;
                dsReturn = service.CallSP(strLoginName, ProcedureType, dtParmValue, out dtOut);
                strOut = RarDataTable(dtOut);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ErrorMsg = ex.InnerException.Message;
                else
                    ErrorMsg = ex.Message;
                bolIsError = true;
                return "";
            }
            return RarDataSet(dsReturn);
        }

        public string RunView_Service(int iViewType, string strLoginName, string strFieldNames, string strWhere, string strOrderBy,
            out string ErrorMsg, out bool bolIsError)
        {
            ErrorMsg = "";
            bolIsError = false;
            WebSerivce.BaseInfoService service = new WebSerivce.BaseInfoService();
            service.Url = mstrWebServiceAddress;
            DataTable dtReturn = null;
            try
            {
                dtReturn = service.CallView(strLoginName, iViewType, strFieldNames, strWhere, strOrderBy);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ErrorMsg = ex.InnerException.Message;
                else
                    ErrorMsg = ex.Message;
                bolIsError = true;
                return "";
            }
            return RarDataTable(dtReturn);
        }

        public string RunDirectSQL_Service(string strLoginName, string strSQL,
           out string ErrorMsg, out bool bolIsError)
        {
            ErrorMsg = "";
            bolIsError = false;
            WebSerivce.BaseInfoService service = new WebSerivce.BaseInfoService();
            service.Url = mstrWebServiceAddress;
            DataTable dtReturn = null;
            try
            {
                dtReturn = service.CallDirectSQL(strLoginName, strSQL);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ErrorMsg = ex.InnerException.Message;
                else
                    ErrorMsg = ex.Message;
                bolIsError = true;
                return "";
            }
            return RarDataTable(dtReturn);
        }

        #region -- 数据压缩 --
        //压缩
        private static string RarDataTable(DataTable dt)
        {
            string strSerial = SerializeDataTableXml(dt);
            return CommonHelper.Compress(strSerial);
        }
        private static string RarDataSet(DataSet ds)
        {
            string strSerial = SerializeDataSetXml(ds);
            return CommonHelper.Compress(strSerial);
        }

        private static string SerializeDataTableXml(DataTable pDt)
        {
            string strSerial = "";
            StringBuilder sb = new StringBuilder();
            if (pDt != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    pDt.WriteXml(stream, XmlWriteMode.WriteSchema);
                    byte[] bs = stream.ToArray();
                    strSerial = System.Text.Encoding.UTF8.GetString(bs, 0, bs.Length);
                }
                //XmlWriter writer = XmlWriter.Create(sb);
                //pDt.WriteXml(stream);
                ////XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
                ////serializer.Serialize(writer, pDt);
                //writer.Close();
            }
            return strSerial;
        }

        private static string SerializeDataSetXml(DataSet pDs)
        {
            string strSerial = "";
            StringBuilder sb = new StringBuilder();
            if (pDs != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    //pDs.WriteXml(stream, XmlWriteMode.WriteSchema);
                    pDs.WriteXml(stream, XmlWriteMode.WriteSchema);
                    byte[] bs = stream.ToArray();
                    strSerial = System.Text.Encoding.UTF8.GetString(bs, 0, bs.Length);
                }
                //XmlWriter writer = XmlWriter.Create(sb);
                //XmlSerializer serializer = new XmlSerializer(typeof(DataSet));
                //serializer.Serialize(writer, pDs);
                //writer.Close();
            }
            return strSerial;
        }

        public static DataTable DeserializeDataTable(string pXml)
        {
            StringReader strReader = new StringReader(pXml);
            XmlReader xmlReader = XmlReader.Create(strReader);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            DataTable dt = serializer.Deserialize(xmlReader) as DataTable;
            return dt;
        }



        #endregion

        #region -- Session接口 --
        public void TakeSession(long SessionID)
        {
            DBHelper.Provider = new DBMSSQL();
            SqlConnection con = new SqlConnection(SQLServerDAL.GetConnectionString);
            string strDBName = con.Database;
            DBMSSQL.InitSettings(5000, con.DataSource, strDBName, true, "", "");
            con.Close();
            SessionTimer.TakeSession(SessionID);
        }

        public void LogOutSession(long SessionID)
        {
            DBHelper.Provider = new DBMSSQL();
            SqlConnection con = new SqlConnection(SQLServerDAL.GetConnectionString);
            string strDBName = con.Database;
            DBMSSQL.InitSettings(5000, con.DataSource, strDBName, true, "", "");
            con.Close();
            SessionTimer.LogOutSession(SessionID);
        }
        #endregion
    }
}
