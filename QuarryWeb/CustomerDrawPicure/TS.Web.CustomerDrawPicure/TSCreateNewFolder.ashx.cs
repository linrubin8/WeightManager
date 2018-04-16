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
    /// TSCreateNewFolder 的摘要说明
    /// </summary>
    public class TSCreateNewFolder : IHttpHandler
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
                        string strNewFolderName = Robj["NewFolderName"].ToString();
                        long lParentContentBy3DPicID = 0;
                         string strMsg;
                        if( Robj["ParentContentBy3DPicID"] != null )
                        {
                            long.TryParse( Robj["ParentContentBy3DPicID"].ToString(), out lParentContentBy3DPicID );

                            #region -- 新逻辑 --
                            //判断该文件名是否已存在
                           
                            bool bolExists = VerifyFolderNameExists( lTSUserID, lParentContentBy3DPicID, strNewFolderName, out strMsg );
                            if( bolExists )
                            {
                                strReturnJson = GetJsonMessage( 0, strMsg );
                            }
                            else
                            {
                                string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );
                                CreateFilder( strDrawPicture );
                                string strUserFolder = Path.Combine( strDrawPicture, strUserID );//用户ID存放图片文件夹
                                CreateFilder( strUserFolder );


                                string strNewPath = Path.Combine( strUserFolder, strNewFolderName );

                                if( !Directory.Exists( strNewPath ) )
                                {
                                    Directory.CreateDirectory( strNewPath );

                                    long lContentBy3DPicID = 0;
                                    bool bolPass;
                                    CreateFolder( lTSUserID, lParentContentBy3DPicID, strNewFolderName, out lContentBy3DPicID, out strMsg, out bolPass );

                                    if( lContentBy3DPicID == 0 )//说明添加失败，则将本地新建了的文件夹删除
                                    {
                                        Directory.Delete( strNewPath );
                                    }

                                    strReturnJson = GetJsonMessage( lContentBy3DPicID, strMsg );
                                }
                                else
                                {
                                    strReturnJson = GetJsonMessage( 0, "该名称已存在" );
                                }
                            }

                            #endregion -- 新逻辑 --
                        }
                        else
                        {
                            #region -- 旧逻辑 --

                            string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );
                            CreateFilder( strDrawPicture );
                            string strUserFolder = Path.Combine( strDrawPicture, strUserID );//用户ID存放图片文件夹
                            CreateFilder( strUserFolder );

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
                                            strUserFolder = Path.Combine( strUserFolder, strFolderName );
                                        }
                                    }
                                }
                            }

                            string strNewPath = Path.Combine( strUserFolder, strNewFolderName );

                            //读取当前文件夹目录的ID
                            bool bolExists = GetContentBy3DPicIDByPath( lTSUserID, strPicPath, out lParentContentBy3DPicID,out strMsg );
                            if( bolExists )
                            {
                                if( !Directory.Exists( strNewPath ) )
                                {
                                    Directory.CreateDirectory( strNewPath );

                                    long lContentBy3DPicID = 0;
                                    bool bolPass;
                                    CreateFolder( lTSUserID, lParentContentBy3DPicID, strNewFolderName, out lContentBy3DPicID, out strMsg, out bolPass );

                                    if( lContentBy3DPicID == 0 )//说明添加失败，则将本地新建了的文件夹删除
                                    {
                                        Directory.Delete( strNewPath );
                                    }

                                    strReturnJson = GetJsonMessage( lContentBy3DPicID, strMsg );
                                }
                                else
                                {
                                    strReturnJson = GetJsonMessage( 0, "该名称已存在" );
                                }
                            }
                            else
                            {
                                context.Response.Write(GetJsonMessage(0, "当前文件夹不存在") );
                            }

                            //if( !Directory.Exists( strNewPath ) )
                            //{
                            //    Directory.CreateDirectory( strNewPath );
                            //    strReturnJson = GetJsonMessage( 0, "" );
                            //}
                            //else
                            //{
                            //    strReturnJson = GetJsonMessage( 0, "该名称已存在" );
                            //}

                            #endregion -- 旧逻辑 --
                        }

                    }
                    catch( Exception ex )
                    {
                        strReturnJson = GetJsonMessage( 0, ex.Message );

                    }
                }
            }
            context.Response.Write( strReturnJson );
        }

         private bool VerifyFolderNameExists( long TSUserID, long ParentContentBy3DPicID, string strNewFolderName,
             out string strMsg )
         {
             bool bolExists = false;
             strMsg = "";
             DbConnection con = new SqlConnection( DbConfig.MPCmsConString4Product );
             con.Open();
             DbTransaction tran = con.BeginTransaction();
             DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );
             try
             {
                 string strSQL = "select * from dbo.ContentBy3DPic where ContentFileName='{0}' and ParentContentBy3DPicID={1} and TSUserID = {2}";
                 strSQL = string.Format( strSQL, strNewFolderName, ParentContentBy3DPicID, TSUserID );
                 DataTable dtResult = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                 if( dtResult.Rows.Count > 0 )
                 {
                     //文件夹名称已存在
                     strMsg = "该文件夹名称已存在！";
                     bolExists = false;
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

        private void CreateFolder(long TSUserID, long ParentContentBy3DPicID,string strNewFolderName,out long ContentBy3DPicID,
            out string strMsg,out bool bolPass)
        {
            ContentBy3DPicID = 0;
            bolPass = false;
            strMsg = "";
            DbConnection con = new SqlConnection(DbConfig.MPCmsConString4Product);
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs("", "", con, tran);
            try
            {
                string strSQL = "select * from dbo.ContentBy3DPic where ContentFileName='{0}' and ParentContentBy3DPicID={1} and TSUserID = {2}";
                strSQL = string.Format( strSQL, strNewFolderName, ParentContentBy3DPicID,TSUserID );
                DataTable dtResult = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                if( dtResult.Rows.Count > 0 )
                {
                    //文件夹名称已存在
                    strMsg = "该文件夹名称已存在！";
                    bolPass = false;
                }
                else
                {
                    string strPicPath = "";
                    //获取当前ParentContentBy3DPicID的路径
                    strSQL = "select * from dbo.ContentBy3DPic where ContentBy3DPicID={0}";
                    strSQL = string.Format( strSQL,  ParentContentBy3DPicID );
                    DataTable dtContent = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                    if( dtContent.Rows.Count > 0 )
                    {
                        string strPicPathTemp=dtContent.Rows[0]["PicPath"].ToString();
                        string strContentFileNameTemp=dtContent.Rows[0]["ContentFileName"].ToString();

                        strPicPath = strPicPathTemp+
                            (strPicPathTemp.Substring(strPicPathTemp.Length-1,1)==@"\"?"":@"\")+
                            strContentFileNameTemp;
                    }
                    else
                    {
                        strPicPath = TSUserID.ToString()+@"\";
                    }

                    strSQL = @"
                        insert into ContentBy3DPic(TSUserID,  PicPath, JsonText, CreateBy, CreateTime, PicName, 
                            PicHeight, PicWidth, ContentType, ParentContentBy3DPicID, ContentFileName)
                        values(@TSUserID,@PicPath,'',@CreateBy,getdate(),'',null,null,0,@ParentContentBy3DPicID,@ContentFileName)
                        SET @ContentBy3DPicID= @@identity
                        ";

                    DbParameter[] parm = {
                                     DBHelperSQL .CreateInDbParameter("@ContentBy3DPicID",DbType.Int64 ,ContentBy3DPicID,true),
                                     DBHelperSQL .CreateInDbParameter("@TSUserID",DbType.Int64 ,TSUserID),
                                     DBHelperSQL .CreateInDbParameter("@PicPath",DbType.String ,strPicPath),
                                     DBHelperSQL .CreateInDbParameter("@CreateBy",DbType.String ,TSUserID.ToString()),
                                     DBHelperSQL .CreateInDbParameter("@ParentContentBy3DPicID",DbType.Int64 ,ParentContentBy3DPicID),
                                     DBHelperSQL .CreateInDbParameter("@ContentFileName",DbType.String,strNewFolderName)
                                  };
                    DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strSQL, true, parm );
                    ContentBy3DPicID = Convert.ToInt64(parm[0].Value != null ? parm[0].Value : "0");
                    strMsg = "";
                    bolPass = true;
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

        private void CreateFilder( string strPath )
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

        private string GetJsonMessage(long lContentBy3DPicID,string strMsg)
        {
            return "{\"Message\":\""+strMsg+"\",\"ContentBy3DPicID\":\""+lContentBy3DPicID.ToString()+"\"}";
        }
    }
}