using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using TS.Web.DBHelper;

namespace TS.Web
{
    /// <summary>
    /// TSDeleteFolder 的摘要说明
    /// </summary>
    public class TSDeleteFolder : IHttpHandler
    {

        //重命名文件或者文件夹
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
                        string strType = Robj["Type"].ToString();//文件类型：0文件夹  1文件

                        if( Robj["ContentBy3DPicID"] != null )
                        {
                            #region -- 新逻辑 --
                            long lContentBy3DPicID;
                            long.TryParse( Robj["ContentBy3DPicID"].ToString(), out lContentBy3DPicID );

                            if( lContentBy3DPicID > 0 )
                            {
                                string strMsg;
                                DeleteFile( lTSUserID, lContentBy3DPicID, out strMsg );
                                strReturnJson=GetJsonMessage(strMsg);
                            }
                            else
                            {
                                strReturnJson=GetJsonMessage("该文件不存在，无法删除！");
                            }

                            #endregion -- 新逻辑 --
                        }
                        else
                        {
                            #region -- 旧逻辑 --
                            string strDelName = Robj["DelName"].ToString();//原名称
                            string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );
                            string strUserFolder = Path.Combine( strDrawPicture, strUserID );//用户ID存放图片文件夹

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
                                            strUserFolder = Path.Combine( strUserFolder, strFolderName );
                                            strPicPath = Path.Combine( strPicPath, strFolderName );
                                        }
                                    }
                                }
                            }

                            //读取当前文件夹目录的ID

                            if( strType == "1" )
                            {
                                strDelName = strDelName.Substring( 0, strDelName.LastIndexOf( "." ) );
                            }

                            long lContentBy3DPicID;
                            strPicPath = Path.Combine( strPicPath, strDelName);
                            string strMsg;
                            bool bolExists = GetContentBy3DPicIDByPath( lTSUserID, strPicPath,strType, out lContentBy3DPicID,out strMsg );
                            if( bolExists )
                            {
                                DeleteFile( lTSUserID, lContentBy3DPicID, out strMsg );
                                strReturnJson = GetJsonMessage( strMsg );
                            }
                            else
                            {
                                strReturnJson=GetJsonMessage("该文件不存在，无法删除！");
                            }

                            #endregion -- 旧逻辑 --
                        }
                        
                    }
                    catch( Exception ex )
                    {
                        strReturnJson=GetJsonMessage(ex.Message);
                    }
                }
            }
            DALMSSQLHepler.LogHelper( "TSDeleteFolder", strReturnJson );
            context.Response.Write( strReturnJson );
        }

        private void DeleteFile(long TSUserID, long lContentBy3DPicID, out string strMsg)
        {
            strMsg = "";
            DbConnection con = new SqlConnection(DbConfig.MPCmsConString4Product);
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs("", "", con, tran);
            try
            {
                string strSQL = "select * from dbo.ContentBy3DPic where ContentBy3DPicID={0} and TSUserID = {1}";
                strSQL = string.Format( strSQL, lContentBy3DPicID ,TSUserID);
                DataTable dtChild = DBHelperSQL.ExecuteQuery( args, strSQL, null );

                if( dtChild.Rows.Count > 0 )
                {
                    DataRow drChild = dtChild.Rows[0];
                    string strPicPath = drChild["PicPath"].ToString().TrimEnd();
                    string strContentFileName = drChild["ContentFileName"].ToString().TrimEnd();
                    string strContentFileExtension = drChild["ContentFileExtension"].ToString().TrimEnd();
                    int iContentType;
                    int.TryParse( drChild["ContentType"].ToString(), out iContentType );

                    string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture", strPicPath, strContentFileName + strContentFileExtension );

                    strSQL = "delete from dbo.ContentBy3DPic where ContentBy3DPicID={0} and TSUserID = {1}";
                    strSQL = string.Format( strSQL, lContentBy3DPicID, TSUserID );
                    DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strSQL, true, null );


                    if( iContentType == 0 )//删除文件夹
                    {
                        if( Directory.Exists( strDrawPicture ) )
                        {
                            Directory.Delete( strDrawPicture );
                        }
                    }
                    else
                    {
                        if( File.Exists( strDrawPicture ) )
                        {
                            DALMSSQLHepler.LogHelper( "TSDeleteFolder", "准备删除："+strDrawPicture );
                            File.Delete( strDrawPicture );
                            DALMSSQLHepler.LogHelper( "TSDeleteFolder", "删除成功："+strDrawPicture );
                        }
                        else
                        {
                            throw new Exception( "该文件不存在，删除失败！" );
                        }

                        //删除对应的xml
                        string strName = strDrawPicture.Substring( 0, strDrawPicture.LastIndexOf( "." ) );
                        string strDelNameXml = Path.Combine( strDrawPicture, strName + ".xml" );
                        if( File.Exists( strDelNameXml ) )
                        {
                            File.Delete( strDelNameXml );
                        }
                    }
                }

                tran.Commit();
            }
            catch( Exception ex )
            {
                strMsg = ex.Message;
                tran.Rollback();
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// 根据文件夹路径读取当前文件夹的ID，针对移动版画图旧版本
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="strPath"></param>
        private bool GetContentBy3DPicIDByPath( long TSUserID, string strPath ,string strType, out long lContentBy3DPicID,out string strMsg )
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
                    where   TSUserID = {0} and ContentType = {2} and rtrim(PicPath+(case when SUBSTRING(PicPath,len(PicPath),1)='\' then '' else '\' end)+ContentFileName)='{1}'";
                    strSQL = string.Format( strSQL, TSUserID, strPath,strType );
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GetJsonMessage(string strMsg)
        {
            return "{\"Message\":\""+strMsg.Replace("'","")+"\"}";
        }
    }
}