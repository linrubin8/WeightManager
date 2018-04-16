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
using TS.Win.DoorDesign;
using TS.Win.UIControl;

namespace TS.Web
{
    /// <summary>
    /// TSCustomerDrawPicture 的摘要说明
    /// </summary>
    public class TSCustomerDrawPicture : IHttpHandler
    {

        public void ProcessRequest( HttpContext context )
        {
           string strReturnJson = "";
            context.Response.ContentType = "application/json";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            using (var reader = new System.IO.StreamReader(context.Request.InputStream))
            {
                string xmlData = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(xmlData))
                {
                    try
                    {
                        string strFileName = DateTime.Now.ToString( "yyMMddmmss" );

                        JObject Robj = JObject.Parse( xmlData );
                        string strUserID = Robj["UserID"].ToString();
                        long lTSUserID;
                        long.TryParse( strUserID, out lTSUserID );
                        string strJpeg = Robj["DoorJpeg"].ToString();
                        string strDoor = Robj["DoorImg"].ToString();

                        if( Robj["ParentContentBy3DPicID"] != null )
                        {
                            #region -- 新逻辑 --

                            float decPicHeight = 0;
                            float decPicWidth = 0;
                            if( Robj["PicHeight"] != null )
                                float.TryParse( Robj["PicHeight"].ToString(), out decPicHeight );
                            if( Robj["PicWidth"] != null )
                                float.TryParse( Robj["PicWidth"].ToString(), out decPicWidth );

                            long lParentContentBy3DPicID = 0;
                            if( Robj["ParentContentBy3DPicID"] != null )
                            {
                                long.TryParse( Robj["ParentContentBy3DPicID"].ToString(), out lParentContentBy3DPicID );
                            }

                            //float fPicHeight;
                            //    float fPicWidth;
                            //    GetDoorSize(ref strDoor, out fPicHeight, out fPicWidth );

                            string strMsg;
                            bool bolPass;
                            long lContentBy3DPicID;
                            CreateFile( lTSUserID, lParentContentBy3DPicID, strFileName,strJpeg,strDoor, out lContentBy3DPicID,
                                    decPicHeight, decPicWidth, out strMsg, out bolPass );
                            DALMSSQLHepler.LogHelper( "TSCustomerDrawPicture", strMsg );
                            context.Response.Write(GetJsonMessage(0, strMsg) );
                            #endregion -- 新逻辑 --
                        }
                        else
                        {
                            #region -- 旧逻辑 --

                            string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );
                            CreateFilder( strDrawPicture );

                            string strCurrentFolder = Path.Combine( strDrawPicture, lTSUserID.ToString() );//文件存放路径
                            CreateFilder( strCurrentFolder );

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
                            long lParentContentBy3DPicID = 0;
                            string strMsg;
                            bool bolExists = GetContentBy3DPicIDByPath( lTSUserID, strPicPath, out lParentContentBy3DPicID,out strMsg );

                            if( bolExists )
                            {
                                float fPicHeight;
                                float fPicWidth;
                                GetDoorSize(ref strDoor, out fPicHeight, out fPicWidth );
           
                                bool bolPass;
                                long lContentBy3DPicID;
                                CreateFile( lTSUserID, lParentContentBy3DPicID, strFileName,strJpeg,strDoor, out lContentBy3DPicID,
                                        fPicHeight, fPicWidth, out strMsg, out bolPass );

                                DALMSSQLHepler.LogHelper( "TSCustomerDrawPicture", strMsg );
                                context.Response.Write(GetJsonMessage(0, strMsg) );
                            }
                            else
                            {
                                context.Response.Write(GetJsonMessage(0, "该文件夹不存在") );
                            }


                            //string strJpgName = strFileName + ".jpg";
                            //string strXmlName = strFileName + ".xml";

                            //string strFullName_jpg = Path.Combine( strCurrentFolder, strJpgName );
                            //string strFullName_xml = Path.Combine( strCurrentFolder, strXmlName );

                            //byte[] bufferJpeg = GetBytes( strJpeg );
                            //byte[] bufferDoor = GetBytes( strDoor );

                            //string strWindow = CommonFuntion.Zip( Base64.ToBase64( bufferDoor ) );

                            //using( MemoryStream ms = new MemoryStream( bufferJpeg ) )
                            //{
                            //    Image img = Image.FromStream( ms );
                            //    img.Save( strFullName_jpg, ImageFormat.Jpeg );
                            //    img.Dispose();

                            //    File.AppendAllText( strFullName_xml, strDoor );
                            //    ms.Dispose();
                            //    ms.Close();

                            //    context.Response.Write(GetJsonMessage(0, ""));
                            //}

                            

                            //decimal decPicHeight = 0;
                            //decimal decPicWidth = 0;
                            //if( Robj["PicHeight"] != null )
                            //    decimal.TryParse( Robj["PicHeight"].ToString(), out decPicHeight );
                            //if( Robj["PicWidth"] != null )
                            //    decimal.TryParse( Robj["PicWidth"].ToString(), out decPicWidth );

                            //string strMsg;
                            //bool bolPass;
                            //long lContentBy3DPicID;
                            //CreateFile_Old( lTSUserID, 0, strFileName,strJpeg,strDoor, out lContentBy3DPicID,
                            //        out strMsg, out bolPass );

                            //context.Response.Write(GetJsonMessage(0, strMsg) );

                            #endregion -- 旧逻辑 --
                        }
                    }
                    catch( Exception ex )
                    {
                        DALMSSQLHepler.LogHelper( "TSCustomerDrawPicture", ex.Message );
                        context.Response.Write(GetJsonMessage(0, ex.Message) );
                    }
                }
            }

        }

        byte[] GetBytes( string strHexString )
        {
            strHexString = strHexString.Replace(" ","");
            byte[] buffer = new byte[strHexString.Length/2];
            for( int i = 0; i < strHexString.Length; i+=2 )
            {
                buffer[i / 2] = (byte)Convert.ToByte( strHexString.Substring( i, 2 ), 16 );
            }
            return buffer;
        }

        private void CreateFilder(string strPath)
        {
            if( !Directory.Exists( strPath ) )
            {
                Directory.CreateDirectory( strPath );
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
                     strMsg = "该文件名称已存在！";
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

        private void CreateFile(long TSUserID, long ParentContentBy3DPicID,string strNewFileName,string strJpeg,string strDoor,
            out long ContentBy3DPicID,
            float PicHeight,float PicWidth,
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
                strSQL = string.Format( strSQL, strNewFileName, ParentContentBy3DPicID,TSUserID );
                DataTable dtResult = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                if( dtResult.Rows.Count > 0 )
                {
                    //文件夹名称已存在
                    strMsg = "该文件夹名称已存在！";
                    bolPass = false;
                }
                else
                {
                    string strPicPath = GetFolderPath( TSUserID, ParentContentBy3DPicID );

                    strSQL = @"
                        insert into ContentBy3DPic(TSUserID,  PicPath, JsonText, CreateBy, CreateTime, PicName, 
                            PicHeight, PicWidth, ContentType, ParentContentBy3DPicID, ContentFileName,ContentFileExtension)
                        values(@TSUserID,@PicPath,'',@CreateBy,getdate(),'',@PicHeight,@PicWidth,1,@ParentContentBy3DPicID,@ContentFileName,'.jpg')
SET @ContentBy3DPicID= @@identity
";

                    DbParameter[] parm = {
                                     DBHelperSQL .CreateInDbParameter("@ContentBy3DPicID",DbType.Int64 ,ContentBy3DPicID,true),
                                     DBHelperSQL .CreateInDbParameter("@TSUserID",DbType.Int64 ,TSUserID),
                                     DBHelperSQL .CreateInDbParameter("@PicPath",DbType.String ,strPicPath),
                                     DBHelperSQL .CreateInDbParameter("@PicHeight",DbType.Decimal ,PicHeight),
                                     DBHelperSQL .CreateInDbParameter("@PicWidth",DbType.Decimal ,PicWidth),
                                     DBHelperSQL .CreateInDbParameter("@CreateBy",DbType.String ,TSUserID.ToString()),
                                     DBHelperSQL .CreateInDbParameter("@ParentContentBy3DPicID",DbType.Int64 ,ParentContentBy3DPicID),
                                     DBHelperSQL .CreateInDbParameter("@ContentFileName",DbType.String,strNewFileName)
                                  };
                    DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strSQL, true, parm );
                    ContentBy3DPicID = Convert.ToInt64( parm[0].Value != null ? parm[0].Value : "0" );

                    
                    #region 生成文件 --
                    //string strFileName = DateTime.Now.ToString( "yyMMddmmss" );
                    string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );
                    CreateFilder( strDrawPicture );

                    string strCurrentFolder =Path.Combine(strDrawPicture, GetFolderPath( TSUserID, ParentContentBy3DPicID ));//文件存放路径

                    CreateFilder( strCurrentFolder );

                    string strJpgName = strNewFileName + ".jpg";
                    string strPngName = strNewFileName + ".png";
                    string strXmlName = strNewFileName + ".xml";

                    string strFullName_jpg = Path.Combine( strCurrentFolder, strJpgName );
                    string strFullName_xml = Path.Combine( strCurrentFolder, strXmlName );
                    string strFullName_png = Path.Combine( strCurrentFolder, strPngName );

                    byte[] bufferJpeg = GetBytes( strJpeg );
                    byte[] bufferDoor = GetBytes( strDoor );

                    string strWindow = CommonFuntion.Zip( Base64.ToBase64( bufferDoor ) );

                    if( File.Exists( strFullName_jpg ) )
                        File.Delete( strFullName_jpg );
                    if( File.Exists( strFullName_xml ) )
                        File.Delete( strFullName_xml );

                    using( MemoryStream ms = new MemoryStream( bufferJpeg ) )
                    {
                        Image img = Image.FromStream( ms );
                        img.Save( strFullName_jpg, ImageFormat.Jpeg );
                        img.Dispose();

                        img = Image.FromStream( ms );
                        img.Save( strFullName_png, ImageFormat.Png );
                        img.Dispose();

                        File.AppendAllText( strFullName_xml, strDoor );
                        ms.Dispose();
                        ms.Close();
                    }

                    #endregion

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

        private void CreateFile_Old(long TSUserID, long ParentContentBy3DPicID,string strNewFileName,string strJpeg,string strDoor,
            out long ContentBy3DPicID,
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
                strSQL = string.Format( strSQL, strNewFileName, ParentContentBy3DPicID,TSUserID );
                DataTable dtResult = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                if( dtResult.Rows.Count > 0 )
                {
                    //文件夹名称已存在
                    strMsg = "该文件夹名称已存在！";
                    bolPass = false;
                }
                else
                {
                    float PicHeight;
                    float PicWidth;
                    this.GetDoorSize( ref strDoor, out PicHeight, out PicWidth );

                    string strPicPath = GetFolderPath( TSUserID, ParentContentBy3DPicID );

                    strSQL = @"
                        insert into ContentBy3DPic(TSUserID,  PicPath, JsonText, CreateBy, CreateTime, PicName, 
                            PicHeight, PicWidth, ContentType, ParentContentBy3DPicID, ContentFileName,ContentFileExtension)
                        values(@TSUserID,@PicPath,'',@CreateBy,getdate(),'',@PicHeight,@PicWidth,1,@ParentContentBy3DPicID,@ContentFileName,'.jpg')

SET @ContentBy3DPicID= @@identity";

                    DbParameter[] parm = {
                                     DBHelperSQL .CreateInDbParameter("@ContentBy3DPicID",DbType.Int64 ,ContentBy3DPicID,true),
                                     DBHelperSQL .CreateInDbParameter("@TSUserID",DbType.Int64 ,TSUserID),
                                     DBHelperSQL .CreateInDbParameter("@PicPath",DbType.String ,strPicPath),
                                     DBHelperSQL .CreateInDbParameter("@PicHeight",DbType.Decimal ,PicHeight),
                                     DBHelperSQL .CreateInDbParameter("@PicWidth",DbType.Decimal ,PicWidth),
                                     DBHelperSQL .CreateInDbParameter("@CreateBy",DbType.String ,TSUserID.ToString()),
                                     DBHelperSQL .CreateInDbParameter("@ParentContentBy3DPicID",DbType.Int64 ,ParentContentBy3DPicID),
                                     DBHelperSQL .CreateInDbParameter("@ContentFileName",DbType.String,strNewFileName)
                                  };
                    DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strSQL, true, parm );
                    ContentBy3DPicID = Convert.ToInt64( parm[0].Value != null ? parm[0].Value : "0" );


                    #region 生成文件 --
                    //string strFileName = DateTime.Now.ToString( "yyMMddmmss" );
                    string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );
                    CreateFilder( strDrawPicture );

                    string strCurrentFolder = Path.Combine( strDrawPicture, GetFolderPath( TSUserID, ParentContentBy3DPicID ) );//文件存放路径

                    CreateFilder( strCurrentFolder );

                    string strJpgName = strNewFileName + ".jpg";
                    string strXmlName = strNewFileName + ".xml";

                    string strFullName_jpg = Path.Combine( strCurrentFolder, strJpgName );
                    string strFullName_xml = Path.Combine( strCurrentFolder, strXmlName );

                    byte[] bufferJpeg = GetBytes( strJpeg );
                    byte[] bufferDoor = GetBytes( strDoor );

                    string strWindow = CommonFuntion.Zip( Base64.ToBase64( bufferDoor ) );

                    if( File.Exists( strFullName_jpg ) )
                        File.Delete( strFullName_jpg );
                    if( File.Exists( strFullName_xml ) )
                        File.Delete( strFullName_xml );

                    FileStream fs = new FileStream( strFullName_jpg, FileMode.Create );
                    fs.Write( bufferJpeg, 0, bufferJpeg.Length );
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();

                    File.AppendAllText( strFullName_xml, strDoor );

                    #endregion

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

        private string GetFolderPath(long TSUserID,long lContentBy3DPicID)
        {
            string strPicPath = "";
            DbConnection con = new SqlConnection(DbConfig.MPCmsConString4Product);
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs("", "", con, tran);
            try
            {
                //获取当前ParentContentBy3DPicID的路径
                string strSQL = "select * from dbo.ContentBy3DPic where ContentBy3DPicID={0} and ContentType = 0";
                strSQL = string.Format( strSQL, lContentBy3DPicID );
                DataTable dtContent = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                if( dtContent.Rows.Count > 0 )
                {
                    strPicPath =Path.Combine( dtContent.Rows[0]["PicPath"].ToString(),dtContent.Rows[0]["ContentFileName"].ToString());
                }
                else
                {
                    strPicPath = TSUserID.ToString() + @"\";
                }

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


        private void GetDoorSize(ref string strWindow,out float Height,out float Width)
        {
           
            byte[] bufferDoor = GetBytes( strWindow );
            MemoryStream ms = new MemoryStream(bufferDoor);
            DoorPage page = new DoorPage();
            page.RequestDoorParms += page_RequestDoorParms;
            page.Load( ms );

            SizeF size = page.GetComboSize();
            Height = size.Height;
            Width = size.Width;

            //将窗型图的MaterialID的值清0
            List<DoorFrame> lstFrames = new List<DoorFrame>();
            page.CollectAllFrames( page, ref lstFrames );

            foreach( DoorItem item in page.ChildItems )
            {
                if( item is DoorConnecter )
                {
                    ( (DoorConnecter)item ).MaterialID = 0;
                }
            }

            foreach( DoorFrame frame in lstFrames )
            {
                foreach( FrameSegment segment in frame.Segments )
                {
                    segment.MaterialID = 0;
                }

                List<DoorItem> lstStiles = new List<DoorItem>();
                frame.CollectAllStile( frame.ChildItems, lstStiles );
                foreach( DoorItem item in lstStiles )
                {
                    if( item is DoorStile )
                    {
                        ((DoorStile)item).MaterialID = 0;
                    }
                }
            }

            ms = new MemoryStream();
            page.Save( ms );
            strWindow =ToHexString( ms.ToArray());
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

        void page_RequestDoorParms( object sender, RequestDoorParmsEventArgs e )
        {
            try
            {


                 DataTable StructMaterialList = new DataTable();
                StructMaterialList.Columns.Add( "FactorySeriesMaterialID", typeof( int ) );
                StructMaterialList.Columns.Add( "BOMDoorStructType", typeof( int ) );
                StructMaterialList.Columns.Add( "SeriesMaterialName", typeof( string ) );
                StructMaterialList.Columns.Add( "MaterialWidth", typeof( float ) );
                StructMaterialList.Columns.Add( "MaterialMaxWidth", typeof( float ) );
                StructMaterialList.Columns.Add( "MaterialWidthSnd", typeof( float ) );
                StructMaterialList.Columns.Add( "ImpactionWidth", typeof( float ) );
                StructMaterialList.Columns.Add( "ImpactionWidthSnd", typeof( float ) );
                StructMaterialList.Columns.Add( "IsDefault", typeof( byte ) );
                StructMaterialList.Columns.Add( "ChildItemNCodeName", typeof( string ) );
                StructMaterialList.Columns.Add( "IsVisibleSeriesMaterialName", typeof( bool ) );
                StructMaterialList.Columns.Add( "PressBaseInstallType", typeof( int ) );
                StructMaterialList.Columns.Add( "DisplayWidth", typeof( float ) );		// 显示尺寸，前
                StructMaterialList.Columns.Add( "DisplayWidthSnd", typeof( float ) );	// 显示尺寸，后

                for( int i = 1; i < 300; i++ )
                {
                    DataRow drNew = StructMaterialList.NewRow();
                    drNew["FactorySeriesMaterialID"] = 1;
                    drNew["BOMDoorStructType"] = i;
                    drNew["MaterialWidth"] = 60;
                    drNew["MaterialMaxWidth"] = 60;
                    drNew["MaterialWidthSnd"] = 60;
                    drNew["DisplayWidth"] = 60;
                    drNew["DisplayWidthSnd"] = 60;
                    StructMaterialList.Rows.Add( drNew );
                }
                StructMaterialList.AcceptChanges();
                e.Frame.DoorParms.AddFrameStickData( StructMaterialList );

            }
            catch( Exception ex )
            {

            }
        }

        private string GetJsonMessage(long lContentBy3DPicID,string strMsg)
        {
            return "{\"Message\":\""+strMsg+"\",\"ContentBy3DPicID\":\""+lContentBy3DPicID.ToString()+"\"}";
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