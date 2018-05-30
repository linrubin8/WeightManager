using LB.WinFunction.Args;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace LB.WinFunction
{
    public class ExecuteSQL
    {
        public static event CallSPHandle CallSPEvent;
        
        /// <summary>
        /// 调用存储过程或中间层
        /// </summary>
        /// <param name="iSPType">存储过程号</param>
        /// <param name="dtInput">参数数据</param>
        /// <param name="dsReturn">返回的查询数据</param>
        /// <param name="dtOut">返回的参数值</param>
        public static void CallSP(int iSPType,DataTable dtInput,out DataSet dsReturn,out DataTable dtOut)
        {
            if (string.IsNullOrEmpty(dtInput.TableName))
            {
                dtInput.TableName = "SPIN";
            }

            IFaxBusiness.IMyFaxBusiness webservice = GetWebService();
            //LBWebService.LBWebService webservice = GetLBWebService();
            string strErrorMsg="";
            bool bolIsError=false;
            dsReturn = null;
            dtOut = null;
            List<Dictionary<object, object>> lstDictValue = new List<Dictionary<object, object>>();
            Dictionary<object, object> dictDataType = new Dictionary<object, object>();
            foreach (DataRow dr in dtInput.Rows)
            {
                Dictionary<object, object> dict = new Dictionary<object, object>();
                foreach (DataColumn dc in dtInput.Columns)
                {
                    dict.Add(dc.ColumnName, dr[dc.ColumnName]);
                    if (!dictDataType.ContainsKey(dc.ColumnName))
                    {
                        dictDataType.Add(dc.ColumnName, dc.DataType.ToString());
                    }
                }
                lstDictValue.Add(dict);
            }

            byte[] bSerialValue = SerializeObject(lstDictValue);
            byte[] bSerialDataType = SerializeObject(dictDataType);

            string strdtOut;
            string strReturn = webservice.RunProcedure(iSPType, LoginInfo.LoginName, bSerialValue, bSerialDataType, out strdtOut, out strErrorMsg, out bolIsError);
            if (bolIsError)
            {
                throw new Exception(strErrorMsg);
            }
            dsReturn =UnRarDataSet(strReturn);
            dtOut = UnRarDataTable(strdtOut);
            
            if (CallSPEvent != null)
            {
                CallSPArgs args = new Args.CallSPArgs(iSPType, dtInput);
                CallSPEvent(args);
            }
        }

        private static IFaxBusiness.IMyFaxBusiness GetWebService()
        {
            /*HttpChannel channel = new HttpChannel(2017);
            ChannelServices.RegisterChannel(channel, false);*/
            //IWebRemoting webservice = (IWebRemoting)RemotingObject.GetRemotingObject(typeof(IWebRemoting));
            //IWebRemoting webservice = (IWebRemoting)Activator.GetObject(typeof(IWebRemoting),
            // "http://localhost:2017/LBProject");
            IFaxBusiness.IMyFaxBusiness webservice = (IFaxBusiness.IMyFaxBusiness)Activator.GetObject(typeof(IFaxBusiness.IMyFaxBusiness),
             RemotingObject.GetIPAddress());

            return webservice;
        }

        public static byte[] SerializeObject(object pObj)
        {
            if (pObj == null)
                return null;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, pObj);
            memoryStream.Position = 0;
            byte[] read = new byte[memoryStream.Length];
            memoryStream.Read(read, 0, read.Length);
            memoryStream.Close();
            return read;
        }

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

        /// <summary>
        /// 调用存储过程或中间层
        /// </summary>
        /// <param name="iSPType">存储过程号</param>
        /// <param name="parmCollection">参数数据</param>
        /// <param name="dsReturn">返回的查询数据</param>
        /// <param name="dictReturn">返回的参数值</param>
        public static void CallSP(int iSPType, LBDbParameterCollection parmCollection,out DataSet dsReturn,out Dictionary<string,object> dictReturn)
        {
            dictReturn = new Dictionary<string, object>();
            DataTable dtSPIN = new DataTable("SPIN");
            foreach(LBParameter parm in parmCollection)
            {
                if (!dtSPIN.Columns.Contains(parm.ParameterName))
                {
                    dtSPIN.Columns.Add(parm.ParameterName, LBDbType.GetSqlDbType( parm.LBDBType));
                }
            }

            DataRow drNew = dtSPIN.NewRow();
            foreach (LBParameter parm in parmCollection)
            {
                if(parm.Direction!= ParameterDirection.Output)
                {
                    drNew[parm.ParameterName] = parm.Value;
                }
            }
            dtSPIN.Rows.Add(drNew);
            
            //LBWebService.LBWebService webservice = GetLBWebService();
            string strErrorMsg;
            bool bolIsError;
            DataTable dtOut;

            CallSP(iSPType, dtSPIN, out dsReturn, out dtOut);

            if (dtOut != null && dtOut.Rows.Count > 0)
            {
                foreach(DataColumn dc in dtOut.Columns)
                {
                    if (!dictReturn.ContainsKey(dc.ColumnName))
                    {
                        dictReturn.Add(dc.ColumnName, dtOut.Rows[0][dc.ColumnName]);
                    }
                }
            }
        }

        /// <summary>
        /// 查询视图
        /// </summary>
        /// <param name="iViewType">视图号</param>
        /// <param name="strFieldNames">查询字段</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="strOrderBy">排序</param>
        /// <returns></returns>
        public static DataTable CallView(int iViewType,string strFieldNames,string strWhere ,string strOrderBy)
        {
            DataTable dtResult = null;
            IFaxBusiness.IMyFaxBusiness webservice = GetWebService();
            //LBWebService.LBWebService webservice = GetLBWebService();
            string strErrorMsg="";
            bool bolIsError=false;
            try
            {
                string strReturn = webservice.RunView(iViewType, LoginInfo.LoginName, strFieldNames, strWhere, strOrderBy, out strErrorMsg, out bolIsError);
                if (bolIsError)
                {
                    throw new Exception(strErrorMsg);
                }
                dtResult = UnRarDataTable(strReturn);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dtResult;
        }

        /// <summary>
        /// 无条件查询视图
        /// </summary>
        /// <param name="iViewType">视图号</param>
        /// <returns></returns>
        public static DataTable CallView(int iViewType)
        {
            return CallView(iViewType, "", "", "");
        }
        /// <summary>
        /// 直接执行SQL
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static DataTable CallDirectSQL(string strSQL)
        {
            DataTable dtResult = null;
            IFaxBusiness.IMyFaxBusiness webservice = GetWebService();
            //LBWebService.LBWebService webservice = GetLBWebService();
            string strErrorMsg="";
            bool bolIsError=false;

            string strReturn = webservice.RunDirectSQL(LoginInfo.LoginName, strSQL, out strErrorMsg, out bolIsError);
            if (bolIsError)
            {
                throw new Exception(strErrorMsg);
            }
            dtResult = UnRarDataTable(strReturn);
            return dtResult;
        }

        /// <summary>
        /// 测试连接状态
        /// </summary>
        /// <returns></returns>
        public static bool TestConnectStatus()
        {
            IFaxBusiness.IMyFaxBusiness webservice = GetWebService();
            //LBWebService.LBWebService webservice = GetLBWebService();
            return webservice.ConnectServer();
        }

        //private static LBWebService.LBWebService GetLBWebService()
        //{
        //    string strWebLinkPath = Path.Combine(Application.StartupPath, "WebLink.ini");
        //    IniClass iniClass = new WinFunction.IniClass(strWebLinkPath);
        //    string strLink = iniClass.ReadValue("Link", "url");
        //    LBWebService.LBWebService webservice = new LBWebService.LBWebService(strLink);
        //    return webservice;
        //}

        #region -- 数据解压 --
        //压缩
        private static DataTable UnRarDataTable(string strRatData)
        {
            if (string.IsNullOrEmpty(strRatData))
                return null;
            string strSerial = Decompress(strRatData);
            return DeserializeDataTable(strSerial);
        }
        private static DataSet UnRarDataSet(string strRatData)
        {
            if (string.IsNullOrEmpty(strRatData))
                return null;
            string strSerial = Decompress(strRatData);
            return DeserializeDataSet(strSerial);
        }

        private static string SerializeDataTableXml(DataTable pDt)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            serializer.Serialize(writer, pDt);
            writer.Close();
            return sb.ToString();
        }

        public static DataTable DeserializeDataTable(string pXml)
        {
            if (string.IsNullOrEmpty(pXml))
                return null;

            DataTable dt = new DataTable();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(pXml);
            using(MemoryStream stream = new MemoryStream(bs))
            {
                dt.ReadXml(stream);
            }

            //StringReader strReader = new StringReader(pXml);
            //XmlReader xmlReader = XmlReader.Create(strReader);
            //XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            //DataTable dt = serializer.Deserialize(xmlReader) as DataTable;
            return dt;
        }

        public static DataSet DeserializeDataSet(string pXml)
        {
            if (string.IsNullOrEmpty(pXml))
                return null;

            DataSet dt = new DataSet();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(pXml);
            using (MemoryStream stream = new MemoryStream(bs))
            {
                dt.ReadXml(stream);
            }
            //StringReader strReader = new StringReader(pXml);
            //XmlReader xmlReader = XmlReader.Create(strReader);
            //XmlSerializer serializer = new XmlSerializer(typeof(DataSet));
            //DataSet dt = serializer.Deserialize(xmlReader) as DataSet;
            return dt;
        }

        public static string Compress(string text)
        {
            byte[] buffer = Encoding.Default.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;
            MemoryStream outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        public static string Decompress(string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);
                byte[] buffer = new byte[msgLength];
                ms.Position = 0;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }
                return Encoding.Default.GetString(buffer);
            }
        }

        #endregion

        #region -- 网络接口 --

        public static void CallSP_Service(int iSPType, DataTable dtInput, out DataSet dsReturn, out DataTable dtOut)
        {
            if (string.IsNullOrEmpty(dtInput.TableName))
            {
                dtInput.TableName = "SPIN";
            }

            IFaxBusiness.IMyFaxBusiness webservice = GetWebService();
            string strErrorMsg = "";
            bool bolIsError = false;
            dsReturn = null;
            dtOut = null;
            List<Dictionary<object, object>> lstDictValue = new List<Dictionary<object, object>>();
            Dictionary<object, object> dictDataType = new Dictionary<object, object>();
            foreach (DataRow dr in dtInput.Rows)
            {
                Dictionary<object, object> dict = new Dictionary<object, object>();
                foreach (DataColumn dc in dtInput.Columns)
                {
                    dict.Add(dc.ColumnName, dr[dc.ColumnName]);
                    if (!dictDataType.ContainsKey(dc.ColumnName))
                    {
                        dictDataType.Add(dc.ColumnName, dc.DataType.ToString());
                    }
                }
                lstDictValue.Add(dict);
            }

            byte[] bSerialValue = SerializeObject(lstDictValue);
            byte[] bSerialDataType = SerializeObject(dictDataType);

            string strdtOut;
            string strReturn = webservice.RunProcedure_Service(iSPType, LoginInfo.LoginName, bSerialValue, bSerialDataType, out strdtOut, out strErrorMsg, out bolIsError);
            if (bolIsError)
            {
                throw new Exception(strErrorMsg);
            }
            dsReturn = UnRarDataSet(strReturn);
            dtOut = UnRarDataTable(strdtOut);
            
            if (CallSPEvent != null)
            {
                CallSPArgs args = new Args.CallSPArgs(iSPType, dtInput);
                CallSPEvent(args);
            }
        }

        public static DataTable CallView_Service(int iViewType)
        {
            return CallView_Service(iViewType, "", "", "");
        }

        public static DataTable CallView_Service(int iViewType, string strFieldNames, string strWhere, string strOrderBy)
        {
            DataTable dtResult = null;
            IFaxBusiness.IMyFaxBusiness webservice = GetWebService();
            string strErrorMsg = "";
            bool bolIsError = false;
            try
            {
                string strReturn = webservice.RunView_Service(iViewType, LoginInfo.LoginName, strFieldNames, strWhere, strOrderBy, out strErrorMsg, out bolIsError);

                if (bolIsError)
                {
                    throw new Exception(strErrorMsg);
                }
                dtResult = UnRarDataTable(strReturn);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dtResult;
        }

        public static DataTable CallDirectSQL_Service(string strSQL)
        {
            DataTable dtResult = null;
            IFaxBusiness.IMyFaxBusiness webservice = GetWebService();
            //LBWebService.LBWebService webservice = GetLBWebService();
            string strErrorMsg = "";
            bool bolIsError = false;
            string strReturn = webservice.RunDirectSQL_Service(LoginInfo.LoginName, strSQL, out strErrorMsg, out bolIsError);

            if (bolIsError)
            {
                throw new Exception(strErrorMsg);
            }
            dtResult = UnRarDataTable(strReturn);
            return dtResult;
        }

        #endregion -- 网络接口 --

        //校验软件权限
        public static void ReadRegister(out bool IsRegister,out int ProductType, out DateTime DeadLine ,
            out Dictionary<string, object> dictModel)
        {
            dictModel = new Dictionary<string, object>();
            string RegisterInfoJson;
            IFaxBusiness.IMyFaxBusiness webservice = GetWebService();
            webservice.ReadRegister(out IsRegister,out ProductType,out RegisterInfoJson, out DeadLine);
            if (RegisterInfoJson != "")
            {
                dictModel = LBEncrypt.GetRegisterModelInfo(RegisterInfoJson);
            }
        }
    }
}
