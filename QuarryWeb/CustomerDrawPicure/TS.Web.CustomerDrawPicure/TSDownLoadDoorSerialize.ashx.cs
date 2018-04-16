using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
    /// TSDownLoadDoorSerialize 的摘要说明
    /// </summary>
    public class TSDownLoadDoorSerialize : IHttpHandler
    {
        public void ProcessRequest( HttpContext context )
        {
            string strReturnJson = "";
            context.Response.ContentType = "application/json";
            context.Response.Cache.SetCacheability( HttpCacheability.NoCache );
            using( var reader = new System.IO.StreamReader( context.Request.InputStream ) )
            {
                string xmlData = reader.ReadToEnd();
                if( !string.IsNullOrEmpty( xmlData ) )
                {
                    try
                    {
                        JObject Robj = JObject.Parse( xmlData );
                        string strUserID = Robj["UserID"].ToString();
                        long lTSUserID;
                        long.TryParse( strUserID, out lTSUserID );
                        string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );
                        CreateFilder( strDrawPicture );
                        
                        if( Robj["ContentBy3DPicID"] != null )
                        {
                            #region -- 新逻辑 --

                            long lContentBy3DPicID = 0;
                            if( Robj["ContentBy3DPicID"] != null )
                            {
                                long.TryParse( Robj["ContentBy3DPicID"].ToString(), out lContentBy3DPicID );
                            }

                            DataTable dtFile  = ReadFileInfo(lTSUserID,lContentBy3DPicID);

                            if( dtFile.Rows.Count > 0 )
                            {
                                DataRow drFile = dtFile.Rows[0];
                                string strPicPath = drFile["PicPath"].ToString().TrimEnd();
                                string strContentFileName = drFile["ContentFileName"].ToString().TrimEnd();
                                string strContentFileExtension = drFile["ContentFileExtension"].ToString().TrimEnd();

                                string strFileFullName_Jpg = Path.Combine( strDrawPicture, strPicPath, strContentFileName + strContentFileExtension );
                                string strFileFullName_Xml = Path.Combine( strDrawPicture, strPicPath, strContentFileName + ".xml" );
                                if( File.Exists( strFileFullName_Jpg ) && File.Exists( strFileFullName_Xml ) )
                                {
                                    string strDoor = File.ReadAllText( strFileFullName_Xml );
                                    string strImage="";
                                    if( File.Exists( strFileFullName_Jpg ) )
                                    {
                                        Image img = Image.FromFile( strFileFullName_Jpg );
                                        using( MemoryStream ms = new MemoryStream() )
                                        {
                                            img.Save( ms, ImageFormat.Jpeg );
                                            byte[] buffer = new byte[ms.Length];
                                            ms.Seek( 0, SeekOrigin.Begin );
                                            ms.Read( buffer, 0, buffer.Length );

                                            strImage = ToHexString( buffer );
                                        }
                                    }
                                    if( strDoor != "" )
                                    {
                                        strReturnJson = "{\"Image\":\""+strImage+"\",\"Door\":\"" + strDoor + "\",\"Message\":\"\"}";
                                    }
                                    else
                                    {
                                        strReturnJson = "{\"Image\":\""+strImage+"\",\"Door\":\"\",\"Message\":\"文件已损坏\"}";
                                    }
                                }
                                else
                                {
                                    strReturnJson = "{\"Image\":\"\",\"Door\":\"\",\"Message\":\"该窗型图不存在！\"}";
                                }
                            }

                            #endregion -- 新逻辑 --
                        }
                        else
                        {
                            #region -- 旧逻辑 --
                            
                            string strFileName = Robj["FileName"].ToString();
                            string strUserFolder = Path.Combine( strDrawPicture, strUserID );//用户ID存放图片文件夹
                            CreateFilder( strUserFolder );
                            if( Robj["FolderData"] != null )
                            {
                                if( Robj["FolderData"].ToString() != "" )
                                {
                                    Newtonsoft.Json.Linq.JArray jarray = (Newtonsoft.Json.Linq.JArray)Robj["FolderData"];
                                    for( int i = 0, j = jarray.Count; i < j; i++ )
                                    {
                                        var obj3 = jarray[i];
                                        if( obj3 == null || obj3.ToString() == "" )
                                        {
                                            continue;
                                        }
                                        Newtonsoft.Json.Linq.JObject detailData = Newtonsoft.Json.Linq.JObject.Parse( obj3.ToString() );

                                        string strFolderName = detailData["FolderName"].ToString();
                                        if( strFolderName != "" )
                                        {
                                            strUserFolder = Path.Combine( strUserFolder, strFolderName );
                                        }
                                    }
                                }
                            }

                            string strFullName_xml = Path.Combine( strUserFolder, strFileName + ".xml" );


                            if( File.Exists( strFullName_xml ) )
                            {
                                string strDoor = File.ReadAllText( strFullName_xml );
                                if( strDoor != "" )
                                {
                                    strReturnJson = "{\"Image\":\"\",\"Door\":\"" + strDoor + "\",\"Message\":\"\"}";
                                }
                                else
                                {
                                    strReturnJson = "{\"Image\":\"\",\"Door\":\"\",\"Message\":\"文件已损坏\"}";
                                }
                            }
                            else
                            {
                                strReturnJson = "{\"Image\":\"\",\"Door\":\"\",\"Message\":\"文件不存在\"}";
                            }

                            #endregion -- 旧逻辑 --
                        }
                    }
                    catch( Exception ex )
                    {
                        strReturnJson= "{\"Image\":\"\",\"Door\":\"\",\"Message\":\""+ex.Message+"\"}";
                        
                    }
                }
            }
            context.Response.Write( strReturnJson );
        }

        private DataTable ReadFileInfo(long lTSUserID, long lContentBy3DPicID)
        {
            DataTable dtChild = null;
            DbConnection con = new SqlConnection(DbConfig.MPCmsConString4Product);
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs("", "", con, tran);
            try
            {
                string strSQL = "select * from dbo.ContentBy3DPic where ContentBy3DPicID={0} and TSUserID={1}";
                strSQL = string.Format( strSQL, lContentBy3DPicID,lTSUserID );
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

        private void CreateFilder( string strPath )
        {
            if( !Directory.Exists( strPath ) )
            {
                Directory.CreateDirectory( strPath );
            }
        }

        private string ToHexString( byte[] bytes )
        {
            if( bytes != null )
            {
                StringBuilder strBuilder = new StringBuilder();
                for( int i = 0; i < bytes.Length; i++ )
                {
                    strBuilder.Append( bytes[i].ToString( "X2" ) );
                }
                return strBuilder.ToString();
            }
            return "";
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