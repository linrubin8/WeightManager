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
    /// TSRenameFolder 的摘要说明
    /// </summary>
    public class TSRenameFolder : IHttpHandler
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
                        int iType;
                        int.TryParse( strType, out iType );
                        
                        string strDirName = Robj["DirName"].ToString();//新名称
                        string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );
                        CreateFilder( strDrawPicture );
                        string strUserFolder = Path.Combine( strDrawPicture, strUserID );//用户ID存放图片文件夹
                        CreateFilder( strUserFolder );

                        if( Robj["ContentBy3DPicID"] != null )
                        {
                            #region -- 新逻辑 --

                            long lContentBy3DPicID = 0;
                            if( Robj["ContentBy3DPicID"] != null )
                            {
                                long.TryParse( Robj["ContentBy3DPicID"].ToString(), out lContentBy3DPicID );
                            }

                            RenameFile( lTSUserID, lContentBy3DPicID, iType, strDirName, out strReturnJson );

                            #endregion -- 新逻辑 --
                        }
                        else
                        {
                            #region -- 旧逻辑 --
                            string strSrcName = Robj["SrcName"].ToString();//原名称

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

                            strPicPath = Path.Combine( strPicPath, strSrcName );

                            //读取当前文件夹目录的ID
                            long lContentBy3DPicID;
                            string strMsg;
                            bool bolExists = GetContentBy3DPicIDByPath( lTSUserID, strPicPath,strType, out lContentBy3DPicID,out strMsg );
                            if( bolExists )
                            {
                                if( strType == "1" )//重命名文件夹
                                {
                                    strDirName = strDirName + ".jpg";
                                }
                                RenameFile( lTSUserID, lContentBy3DPicID, iType, strDirName, out strReturnJson );
                            }
                            else
                            {
                                if( strType == "0" )//重命名文件夹
                                {
                                    strReturnJson = "{\"Message\":\"该文件夹不存在\"}";
                                }
                                else
                                {
                                    strReturnJson = "{\"Message\":\"该文件不存在\"}";
                                }
                            }

                            //if( strType == "0" )//重命名文件夹
                            //{
                            //    string strSrcPath = Path.Combine( strUserFolder, strSrcName );
                            //    string strDirPath = Path.Combine( strUserFolder, strDirName );
                            //    if( !Directory.Exists( strSrcPath ) )
                            //    {
                            //        strReturnJson = "{\"Message\":\"该文件夹不存在\"}";
                            //    }
                            //    else if( Directory.Exists( strDirPath ) )
                            //    {
                            //        strReturnJson = "{\"Message\":\"该名称已存在\"}";
                            //    }
                            //    else
                            //    {
                            //        Directory.Move( strSrcPath, strDirPath );
                            //        strReturnJson = "{\"Message\":\"\"}";
                            //    }
                            //}
                            //else
                            //{
                            //    string strSrcFileJpg = Path.Combine( strUserFolder, strSrcName+".jpg" );
                            //    string strDirFileJpg = Path.Combine( strUserFolder, strDirName );
                            //    string strSrcFileXml = Path.Combine( strUserFolder, strSrcName.Replace( ".jpg", "" ) + ".xml" );
                            //    string strDirFileXml = Path.Combine( strUserFolder, strDirName.Replace( ".jpg", "" ) + ".xml" );
                            //    if( !File.Exists( strSrcFileJpg ) || !File.Exists( strSrcFileXml ) )
                            //    {
                            //        strReturnJson = "{\"Message\":\"该文件不存在\"}";
                            //    }
                            //    else if( File.Exists( strDirFileJpg ) || File.Exists( strDirFileXml ) )
                            //    {
                            //        strReturnJson = "{\"Message\":\"该名称已存在\"}";
                            //    }
                            //    else
                            //    {
                            //        File.Move( strSrcFileJpg, strDirFileJpg );
                            //        File.Move( strSrcFileXml, strDirFileXml );
                            //        strReturnJson = "{\"Message\":\"\"}";
                            //    }
                            //}

                            #endregion -- 旧逻辑 --
                        }
                    }
                    catch( Exception ex )
                    {
                        strReturnJson= "{\"Message\":\""+ex.Message+"\"}";
                        
                    }
                }
            }
            context.Response.Write( strReturnJson );
        }

        private void RenameFile( long lTSUserID, long lContentBy3DPicID, int iType, string strNewName, out string strMsg )
        {
            strMsg = "";
            string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );
            CreateFilder( strDrawPicture );

            DataTable dtChild = null;
            DbConnection con = new SqlConnection( DbConfig.MPCmsConString4Product );
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );
            try
            {
                string strSQL = "select * from dbo.ContentBy3DPic where ContentBy3DPicID={0} and TSUserID={1}";
                strSQL = string.Format( strSQL, lContentBy3DPicID, lTSUserID );
                dtChild = DBHelperSQL.ExecuteQuery( args, strSQL, null );

                if( dtChild.Rows.Count > 0 )
                {
                    DataRow drChild = dtChild.Rows[0];
                    string strPicPath = drChild["PicPath"].ToString().TrimEnd();
                    int iContentType = Convert.ToInt32( drChild["ContentType"] );
                    string strContentFileName = drChild["ContentFileName"].ToString().TrimEnd();
                    string strContentFileExtension = drChild["ContentFileExtension"].ToString().TrimEnd();

                    if( iContentType == 1 )
                    {
                        strNewName=strNewName.Substring( 0, strNewName.LastIndexOf( "." ));
                    }

                    strSQL = @"
                        update ContentBy3DPic
                        set ContentFileName = @ContentFileName
                        where ContentBy3DPicID = @ContentBy3DPicID";

                    DbParameter[] parm = {
                                     DBHelperSQL .CreateInDbParameter("@ContentBy3DPicID",DbType.Int64 ,lContentBy3DPicID),
                                     DBHelperSQL .CreateInDbParameter("@ContentFileName",DbType.String,strNewName)
                                  };
                    DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strSQL, true, parm );

                    if( iContentType == 0 )//文件夹
                    {
                        string strOldFileFullName = Path.Combine( strDrawPicture, strPicPath, strContentFileName );
                        string strNewFileFullName = Path.Combine( strDrawPicture, strPicPath, strNewName );

                        if( !Directory.Exists( strOldFileFullName ) )
                        {
                            throw new Exception( "该文件夹不存在" );
                        }
                        else if( Directory.Exists( strNewFileFullName ) )
                        {
                            throw new Exception( "该文件夹名称已存在" );
                        }
                        else
                        {
                            Directory.Move( strOldFileFullName, strNewFileFullName );
                        }
                    }
                    else
                    {
                        string strOldFileFullName_Jpg = Path.Combine( strDrawPicture, strPicPath, strContentFileName + strContentFileExtension );
                        string strOldFileFullName_Xml = Path.Combine( strDrawPicture, strPicPath, strContentFileName + ".xml" );
                        string strNewFileFullName_Jpg = Path.Combine( strDrawPicture, strPicPath, strNewName + strContentFileExtension );
                        string strNewFileFullName_Xml = Path.Combine( strDrawPicture, strPicPath, strNewName + ".xml" );

                        if( !File.Exists( strOldFileFullName_Jpg ) || !File.Exists( strOldFileFullName_Xml ) )
                        {
                            throw new Exception( "该文件不存在" );
                        }
                        else if( File.Exists( strNewFileFullName_Jpg ) || File.Exists( strNewFileFullName_Xml ) )
                        {
                            throw new Exception( "该文件名称已存在" );
                        }
                        else
                        {
                            File.Move( strOldFileFullName_Jpg, strNewFileFullName_Jpg );
                            File.Move( strOldFileFullName_Xml, strNewFileFullName_Xml );
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

            strMsg = "{\"Message\":\"" + strMsg + "\"}";
        }

        /// <summary>
        /// 根据文件夹路径读取当前文件夹的ID，针对移动版画图旧版本
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="strPath"></param>
        private bool GetContentBy3DPicIDByPath( long TSUserID, string strPath ,string strType,out long lContentBy3DPicID,out string strMsg )
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
    }
}