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
    /// TSOpenFolder 的摘要说明
    /// </summary>
    public class TSOpenFolder : IHttpHandler
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

                        long lContentBy3DPicID = 0;
                        if( Robj["FolderData"] != null )
                        {
                            string strPicPath = lTSUserID.ToString();

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
                                            strPicPath = Path.Combine( strPicPath, strFolderName );
                                        }
                                    }
                                }
                            }

                            //读取当前文件夹目录的ID
                            string strMsg;
                            bool bolExists = GetContentBy3DPicIDByPath( lTSUserID, strPicPath, out lContentBy3DPicID, out strMsg );
                        }

                        if( Robj["ContentBy3DPicID"] != null )
                        {
                            long.TryParse( Robj["ContentBy3DPicID"].ToString(), out lContentBy3DPicID );
                        }


                        string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );
                        CreateFilder( strDrawPicture );

                        DataTable dtPath;//当前路径的所有父级文件夹清单
                        string strCurrentFolder = GetFolderPath( lTSUserID, lContentBy3DPicID, out dtPath );//文件存放路径
                        strCurrentFolder = Path.Combine( strDrawPicture, strCurrentFolder );
                        CreateFilder( strCurrentFolder );

                        DataTable dtChildFile = ReadChildFileName( lTSUserID, lContentBy3DPicID );//读取所有下级文件或文件夹

                        DirectoryInfo currentFolder = new DirectoryInfo( strCurrentFolder );

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

                            if( iContentType == 0 )//文件夹
                            {
                                string strFolderFullName = Path.Combine( strDrawPicture, strPicPath, strContentFileName );
                                if( Directory.Exists( strFolderFullName ) )//文件夹存在
                                {
                                    if( strChildData != "" )
                                        strChildData += ",";
                                    strChildData += "{\"ContentBy3DPicID\":\"" + lChildContentBy3DPicID.ToString() + "\",\"Name\":\"" + strContentFileName + "\",\"Type\":\"0\"}";
                                }
                            }
                            else
                            {
                                //文件
                                string strFileFullName = Path.Combine( strDrawPicture, strPicPath, strContentFileName + strContentFileExtension );
                                if( File.Exists( strFileFullName ) )
                                {
                                    Image img = Image.FromFile( strFileFullName );
                                    using( MemoryStream ms = new MemoryStream() )
                                    {
                                        img.Save( ms, ImageFormat.Jpeg );
                                        byte[] buffer = new byte[ms.Length];
                                        ms.Seek( 0, SeekOrigin.Begin );
                                        ms.Read( buffer, 0, buffer.Length );
                                        ms.Dispose();
                                        ms.Close();

                                        img.Dispose();
                                      

                                        string strImg = ToHexString( buffer );
                                        if( strChildData != "" )
                                            strChildData += ",";
                                        strChildData += "{\"ContentBy3DPicID\":\"" + lChildContentBy3DPicID.ToString() + "\",\"Name\":\"" + strContentFileName + strContentFileExtension + "\",\"Type\":\"1\",\"Img\":\"" + strImg + "\",\"Height\":\"" + decPicHeight + "\",\"Width\":\"" + decPicWidth + "\"}";
                                    }
                                }
                            }
                        }

                        #endregion -- 子文件或文件夹清单 --

                        #region -- 当前文件夹路径 --

                        string strFolderPath = "";
                        foreach( DataRow drChildFile in dtPath.Rows )
                        {
                            string strID = drChildFile["PathID"].ToString();
                            string strName = drChildFile["PathName"].ToString();
                            if( strFolderPath != "" )
                                strFolderPath += ",";
                            strFolderPath += "{\"PathID\":\"" + strID + "\",\"PathName\":\"" + strName + "\"}";
                        }

                        #endregion -- 当前文件夹路径 --

                        context.Response.Write( GetJsonMessage( "", strChildData, strFolderPath ) );


                    }
                    catch( Exception ex )
                    {
                        context.Response.Write( GetJsonMessage( ex.Message, "", "" ) );
                    }
                }
            }
        }

        /// <summary>
        /// 根据文件夹路径读取当前文件夹的ID，针对移动版画图旧版本
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="strPath"></param>
        private bool GetContentBy3DPicIDByPath( long TSUserID, string strPath ,out long lContentBy3DPicID,out string strMsg )
        {
            lContentBy3DPicID = 0;
            bool bolExists = false;
            strMsg = "";
            DbConnection con = new SqlConnection( DbConfig.MPCmsConString4Product );
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );
            try
            {
                if( TSUserID.ToString() == strPath )
                {
                    bolExists = true;
                    lContentBy3DPicID = 0;
                    strMsg = "";
                }
                else
                {
                    string strSQL = @"
                    select ContentBy3DPicID 
                    from dbo.ContentBy3DPic 
                    where   TSUserID = {0} and ContentType = 0 and rtrim(PicPath+(case when SUBSTRING(PicPath,len(PicPath),1)='\' then '' else '\' end)+ContentFileName)='{1}'";
                    strSQL = string.Format( strSQL, TSUserID, strPath );
                    DataTable dtResult = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                    if( dtResult.Rows.Count > 0 )
                    {
                        //文件夹名称已存在
                        long.TryParse( dtResult.Rows[0]["ContentBy3DPicID"].ToString(), out lContentBy3DPicID );
                        bolExists = true;
                    }
                }
                tran.Commit();
            }
            catch( Exception ex )
            {
                tran.Rollback();
                strMsg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return bolExists;
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GetFolderPath(long TSUserID,long lContentBy3DPicID,out DataTable dtPath)
        {
            dtPath = new DataTable();
            dtPath.Columns.Add( "PathID", typeof( long ) );
            dtPath.Columns.Add( "PathName", typeof( string ) );
            string strPicPath = "";
            DbConnection con = new SqlConnection(DbConfig.MPCmsConString4Product);
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs("", "", con, tran);
            try
            {
                //获取当前ParentContentBy3DPicID的路径
                ReadFilePath( args, TSUserID, lContentBy3DPicID, ref dtPath,ref strPicPath );

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
            return strPicPath;
        }

        private void ReadFilePath( DALExecuteArgs args, long TSUserID, long lContentBy3DPicID, 
            ref DataTable dtPath,ref string strParentPath )
        {
            try
            {
                if( lContentBy3DPicID == 0 )
                {
                    strParentPath = TSUserID.ToString() + @"\" + strParentPath;
                }
                else
                {

                    //获取当前ParentContentBy3DPicID的路径
                    string strSQL = "select ParentContentBy3DPicID,ContentFileName from dbo.ContentBy3DPic where TSUserID = {0} and ContentBy3DPicID={1} and ContentType = 0";
                    strSQL = string.Format( strSQL, TSUserID, lContentBy3DPicID );
                    DataTable dtContent = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                    if( dtContent.Rows.Count > 0 )
                    {
                        DataRow drContent = dtContent.Rows[0];
                        string strContentFileName = drContent["ContentFileName"].ToString().TrimEnd();
                        long lParentContentBy3DPicID;
                        long.TryParse( drContent["ParentContentBy3DPicID"].ToString(), out lParentContentBy3DPicID );

                        DataRow drNew = dtPath.NewRow();
                        drNew["PathID"] = lContentBy3DPicID;
                        drNew["PathName"] = strContentFileName;
                        dtPath.Rows.InsertAt( drNew, 0 );

                        strParentPath = strContentFileName + @"\" + strParentPath;

                        if( lParentContentBy3DPicID > 0 )
                        {
                            ReadFilePath( args, TSUserID, lParentContentBy3DPicID, ref dtPath, ref strParentPath );
                        }
                    }
                }
            }
            catch( Exception ex )
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable ReadChildFileName(long lTSUserID, long lContentBy3DPicID)
        {
            DataTable dtChild = null;
            DbConnection con = new SqlConnection(DbConfig.MPCmsConString4Product);
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs("", "", con, tran);
            try
            {
                string strSQL = "select * from dbo.ContentBy3DPic where ParentContentBy3DPicID={0} and TSUserID={1}";
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

        private string GetJsonMessage(string strMsg,string strData,string strPath)
        {
            return "{\"Message\":\""+strMsg+"\",\"data\":["+strData+"],\"path\":["+strPath+"]}";
        }
    }
}