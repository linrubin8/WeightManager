using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using TS.Web.DBHelper;

namespace TS.Web
{
    /// <summary>
    /// TSReadLastPicture 的摘要说明
    /// </summary>
    public class TSReadLastPicture : IHttpHandler
    {
        public void ProcessRequest( HttpContext context )
        {
            string strJson = "";
            context.Response.ContentType = "application/json";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            using (var reader = new System.IO.StreamReader(context.Request.InputStream))
            {
                string xmlData = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(xmlData))
                {
                    try
                    {
                        JObject Robj = JObject.Parse( xmlData );
                        string strUserID = Robj["UserID"].ToString();
                        long lTSUserID;
                        long.TryParse( strUserID, out lTSUserID );
                        //string strFolderData = Robj["FolderData"].ToString();

                        int iCount = 0;
                        if( Robj["Count"] != null )
                        {
                            int.TryParse( Robj["Count"].ToString(), out iCount );
                        }

                        string strDrawPicture=Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );
                        CreateFilder( strDrawPicture );

                        DataTable dtChildFile = ReadChildFileName(lTSUserID, iCount );//读取所有下级文件或文件夹

                        //DirectoryInfo currentFolder = new DirectoryInfo( strCurrentFolder );

                        #region -- 子文件或文件夹清单 --

                        string strChildData = "";
                        foreach( DataRow drChildFile in dtChildFile.Rows )
                        {
                            int iContentType;
                            int.TryParse( drChildFile["ContentType"].ToString(), out iContentType );
                            long lChildContentBy3DPicID;
                            long.TryParse( drChildFile["ContentBy3DPicID"].ToString(), out lChildContentBy3DPicID );

                            string strPicPath = drChildFile["PicPath"].ToString().TrimEnd();
                            string strContentFileName = drChildFile["ContentFileName"].ToString().TrimEnd();
                            string strContentFileExtension = drChildFile["ContentFileExtension"].ToString().TrimEnd();
                            decimal decPicHeight;
                            decimal decPicWidth;
                            decimal.TryParse( drChildFile["PicHeight"].ToString(), out decPicHeight );
                            decimal.TryParse( drChildFile["PicWidth"].ToString(), out decPicWidth );
                            //文件
                            string strFileFullName = Path.Combine( strDrawPicture, strPicPath, strContentFileName + ".png" );
                            if( File.Exists( strFileFullName ) )
                            {
                                Image img = Image.FromFile( strFileFullName );
                                using( MemoryStream ms = new MemoryStream() )
                                {
                                    img.Save( ms, ImageFormat.Png );
                                    byte[] buffer = new byte[ms.Length];
                                    ms.Seek( 0, SeekOrigin.Begin );
                                    ms.Read( buffer, 0, buffer.Length );

                                    string strImg = ToHexString( buffer );
                                    if( strChildData != "" )
                                        strChildData += ",";
                                    strChildData += "{\"ContentBy3DPicID\":\"" + lChildContentBy3DPicID.ToString() + "\",\"Name\":\"" + strContentFileName + ".png" + "\",\"Type\":\"1\",\"Img\":\"" + strImg + "\",\"Height\":\""+decPicHeight+"\",\"Width\":\""+decPicWidth+"\"}";
                                }
                            }
                        }

                        #endregion -- 子文件或文件夹清单 --

                        context.Response.Write(GetJsonMessage("",strChildData));
                    }
                    catch( Exception ex )
                    {
                        context.Response.Write(GetJsonMessage(ex.Message,""));
                    }
                }
            }
        }

        private string ToHexString(byte[] bytes)
        {
            if(bytes!=null)
            {
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length;i++)
                {
                    strBuilder.Append(bytes[i].ToString("X2"));
                }
                return strBuilder.ToString();
            }
            return "";
        }

        private void CreateFilder(string strPath)
        {
            if( !Directory.Exists( strPath ) )
            {
                Directory.CreateDirectory( strPath );
            }
        }

        private DataTable ReadChildFileName(long TSUserID,int ReadCount)
        {
            DataTable dtChild = null;
            DbConnection con = new SqlConnection(DbConfig.MPCmsConString4Product);
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs("", "", con, tran);
            try
            {
                string strSQL = "select top "+ReadCount+" * from dbo.ContentBy3DPic where TSUserID = "+TSUserID+" and ContentType = 1 order by CreateTime desc";
                dtChild = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                tran.Commit();
            }
            catch( Exception ex )
            {
                tran.Rollback();
            }
            finally
            {
                con.Close();
            }
            return dtChild;
        }

        private string GetJsonMessage(string strMsg,string strData)
        {
            return "{\"Message\":\""+strMsg+"\",\"data\":["+strData+"]}";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}