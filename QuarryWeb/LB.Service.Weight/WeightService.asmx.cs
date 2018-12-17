using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;

namespace LB.Service
{
    /// <summary>
    /// BaseInfoService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class BaseInfoService : System.Web.Services.WebService
    {
        private static IFaxBusiness.IMyFaxBusiness GetWebService()
        {
            IFaxBusiness.IMyFaxBusiness webservice = (IFaxBusiness.IMyFaxBusiness)Activator.GetObject(typeof(IFaxBusiness.IMyFaxBusiness),
             RemotingObject.GetIPAddress());

            return webservice;
        }

        [WebMethod]
        public void CallSP(string LoginName, int iSPType, DataTable dtInput, out DataSet dsReturn, out DataTable dtOut)
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
            string strReturn = webservice.RunProcedure(iSPType,0,false, LoginName, bSerialValue, bSerialDataType, out strdtOut, out strErrorMsg, out bolIsError);
            if (bolIsError)
            {
                throw new Exception(strErrorMsg);
            }
            dsReturn = UnRarDataSet(strReturn);
            dtOut = UnRarDataTable(strdtOut);
        }

        [WebMethod]
        public DataTable CallDirectSQL(string LoginName, string strSQL)
        {
            DataTable dtResult = null;
            IFaxBusiness.IMyFaxBusiness webservice = GetWebService();
            //LBWebService.LBWebService webservice = GetLBWebService();
            string strErrorMsg = "";
            bool bolIsError = false;
            string strReturn = webservice.RunDirectSQL(0, false, LoginName, strSQL, out strErrorMsg, out bolIsError);
            if (bolIsError)
            {
                throw new Exception(strErrorMsg);
            }
            dtResult = UnRarDataTable(strReturn);
            return dtResult;
        }

        [WebMethod]
        public DataTable CallView(string LoginName, int iViewType, string strFieldNames, string strWhere, string strOrderBy)
        {
            DataTable dtResult = null;
            IFaxBusiness.IMyFaxBusiness webservice = GetWebService();
            //LBWebService.LBWebService webservice = GetLBWebService();
            string strErrorMsg = "";
            bool bolIsError = false;
            try
            {
                dtResult = UnRarDataTable(webservice.RunView(iViewType, 0, false, LoginName, strFieldNames, strWhere, strOrderBy, out strErrorMsg, out bolIsError));
                if (bolIsError)
                {
                    throw new Exception(strErrorMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dtResult;
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
            using (MemoryStream stream = new MemoryStream(bs))
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
    }
}
