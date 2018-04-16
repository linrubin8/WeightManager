using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TS.Web.DBHelper;

namespace TS.Web
{
    /// <summary>
    /// TSMaxUploadDrawInfo 的摘要说明
    /// </summary>
    public class TSMaxUploadDrawInfo : IHttpHandler
    {
        private long UserID = 0;
        private string Compony = "";
        private string Fax = "";
        private string Manager = "";
        private string ManagerMoblie = "";
        private string Agreement = "";
        private string Customer = "";
        private string CustomerAddress = "";
        private DateTime OrderDate = new DateTime();
        private DateTime RequireDate = new DateTime();
        private string Explain = "";
        private string SeriesName = "";
        private string DoorStruct = "";
        private long VrayID = 0;
        private string MissionName = "";
        private string ColorType = "";
        private string ColorCardName = "";
        private string BordureType = "";
        private int ImageWidth = 0;
        private int ImageHeight = 0;
        private string ColorMD5 = "";
        private List<string> safeFileNames = new List<string>();
        private string strReturnJson = "";
        private bool isFirst = true;
        private string sPath = HttpContext.Current.Server.MapPath( "/" );

        public void ProcessRequest( HttpContext context )
        {
            //File.AppendAllText( sPath + "日志.txt", "成功进入服务" );

            safeFileNames.Clear();
            string md5 = "";
            context.Response.ContentType = "application/json";
            context.Response.Cache.SetCacheability( HttpCacheability.NoCache );
            using( var reader = new StreamReader( context.Request.InputStream ) )
            {
                string xmlData = reader.ReadToEnd();
                if( !string.IsNullOrEmpty( xmlData ) )
                {
                    DbConnection con = null;
                    DbTransaction tran = null;
                    try
                    {
                        con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                        con.Open();
                        tran = con.BeginTransaction();
                        JObject Robj = JObject.Parse( xmlData );

                        //File.AppendAllText( sPath + "日志.txt", "尝试读取UserID" );
                        long.TryParse( Robj["UserID"].ToString(), out UserID );
                        //File.AppendAllText( sPath + "日志.txt", "UserID读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取Compony" );
                        Compony = Robj["Compony"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "Compony读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取Fax" );
                        Fax = Robj["Fax"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "Fax读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取Manager" );
                        Manager = Robj["Manager"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "Manager读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取ManagerMoblie" );
                        ManagerMoblie = Robj["ManagerMoblie"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "ManagerMoblie读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取Agreement" );
                        Agreement = Robj["Agreement"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "Agreement读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取Customer" );
                        Customer = Robj["Customer"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "Customer读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取CustomerAddress" );
                        CustomerAddress = Robj["CustomerAddress"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "CustomerAddress读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取OrderDate" );
                        OrderDate = Convert.ToDateTime( Robj["OrderDate"].ToString() );
                        //File.AppendAllText( sPath + "日志.txt", "OrderDate读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取RequireDate" );
                        RequireDate = Convert.ToDateTime( Robj["RequireDate"].ToString() );
                        //File.AppendAllText( sPath + "日志.txt", "RequireDate读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取Explain" );
                        Explain = Robj["Explain"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "Explain读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取SeriesName" );
                        SeriesName = Robj["SeriesName"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "SeriesName读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取DoorStruct" );
                        DoorStruct = Robj["DoorStruct"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "DoorStruct读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取VrayID" );
                        long.TryParse( Robj["VrayID"].ToString(), out VrayID );
                        //File.AppendAllText( sPath + "日志.txt", "VrayID读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取MissionName" );
                        MissionName = Robj["MissionName"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "MissionName读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取ColorType" );
                        ColorType = Robj["ColorType"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "ColorType读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取ColorCardName" );
                        ColorCardName = Robj["ColorCardName"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "ColorCardName读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取BordureType" );
                        BordureType = Robj["BordureType"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "BordureType读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取ImageWidth" );
                        int.TryParse( Robj["ImageWidth"].ToString(),out ImageWidth );
                        //File.AppendAllText( sPath + "日志.txt", "ImageWidth读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取ImageHeight" );
                        int.TryParse( Robj["ImageHeight"].ToString(), out ImageHeight );
                        //File.AppendAllText( sPath + "日志.txt", "ImageHeight读取成功" );
                        //File.AppendAllText( sPath + "日志.txt", "尝试读取ColorMD5" );
                        ColorMD5 = Robj["ColorMD5"].ToString();
                        //File.AppendAllText( sPath + "日志.txt", "ColorMD5读取成功" );

                        string dxfPath = HttpContext.Current.Server.MapPath( "/" ) + @"DrawDxfFolder\CustomerDxf\" + UserID + "\\" + DoorStruct + "\\" + SeriesName + "\\";
                        string copyPath = Path.Combine( HttpRuntime.AppDomainAppPath, "MaxDrawFiles", "BeforeDraw", "DxfFile", SeriesName );

                        string[] filedir = Directory.GetFiles( dxfPath );

                        foreach( string item in filedir )
                        {
                            string[] safeFileName = item.Split( '\\' );
                            safeFileNames.Add( safeFileName[safeFileName.Length - 1] );
                        }

                        if( !Directory.Exists( copyPath ) )
                        {
                            Directory.CreateDirectory( copyPath );
                        }     
                        
                        for( int i = -1; i < filedir.Length; i++ )
                        {
                            if( isFirst )  //色卡
                            {
                                md5 = ColorMD5;

                                try
                                {
                                    insertDB( con, tran, md5, i );
                                }
                                catch( Exception ex )
                                {
                                    File.AppendAllText( HttpContext.Current.Server.MapPath( "/" ) + "日志.txt", ex.Message + "   " + DateTime.Now + "\r\n" );
                                }
                                isFirst = false;
                            }
                            else  //DXF
                            {
                                string dxfMD5 = GetMD5HashFromFile( filedir[i] );
                                md5 = dxfMD5; 
                                try
                                {
                                    insertDB( con, tran, md5, i );
                                }
                                catch( Exception ex )
                                {
                                    File.AppendAllText( HttpContext.Current.Server.MapPath( "/" ) + "日志.txt", ex.Message + "   " + DateTime.Now + "\r\n" );
                                }
                            }
                        }
                    }
                    catch( Exception ex )
                    {
                        strReturnJson = "{\"Message\":\"" + ex.Message + "\"}";
                        if( tran != null )
                        {
                            tran.Rollback();
                        }
                    }
                    finally
                    {
                        if( con != null )
                        {
                            con.Close();
                        }
                    }
                }
            }
            if( string.IsNullOrEmpty( strReturnJson ) )
            {
                strReturnJson = "{\"isContainSameMD5\":\"" + true + "\",\"Message\":\"\",\"noPathImageCardID\":\"-1\"}";
            }
            context.Response.Write( strReturnJson );
        }
        
        private void insertDB( DbConnection con, DbTransaction tran, string md5, int i )
        {
            string strSelectSQL = "select * from [TSMobileProduct].[dbo].[MaxFiles] where MD5 = '" + md5 + "'";

            DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );
            DataTable dtExists = DBHelperSQL.ExecuteQuery( args, strSelectSQL, null );
            long usedFilesID = 0;
            long fileID = 0;


            long sameMD5ID = 0;

            bool isContainSameMD5 = false;
            if( dtExists.Rows.Count > 0 )
            {
                isContainSameMD5 = true;
                long.TryParse( dtExists.Rows[0]["FileID"].ToString(), out sameMD5ID );
            }
            else
            {
                isContainSameMD5 = false;
            }

            string strInsertSQL = "";


            if( isContainSameMD5 )  //如果有相同的MD5, 将MD5所对应的文件ID插入关联表
            {
                if( isFirst )  //如果是上传色卡
                {
                    con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                    con.Open();
                    tran = con.BeginTransaction();

                    strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxUsedFiles](VrayID, FileID, FileName, FileType) values (@VrayID, @FileID, @FileName, @FileType) select @@identity";  //返回关联表ID

                    args = new DALExecuteArgs( "", "", con, tran );

                    DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@UsedFilesID",DbType.Int64 ,usedFilesID),
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,sameMD5ID),
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.Int64 ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,ColorCardName),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.Int32 ,1),  //文件类型(0-5:渲染前,6-10:渲染后) 1.色卡 2.Dxf/Nod/ini 6.3DSMAX文件 7.结果图
                                  };

                    DataTable dt = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm );  //取回关联表ID
                    if( dt != null )
                    {
                        usedFilesID = long.Parse( dt.Rows[0][0].ToString() );
                    }

                    args.DbTrans.Commit();

                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }
                }
                else  //如果是存在于服务器的dxf
                {
                    con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                    con.Open();
                    tran = con.BeginTransaction();

                    strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxUsedFiles](VrayID, FileID, FileName, FileType) values (@VrayID, @FileID, @FileName, @FileType) select @@identity";  //返回关联表ID

                    args = new DALExecuteArgs( "", "", con, tran );

                    DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@UsedFilesID",DbType.Int64 ,usedFilesID),
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,sameMD5ID),
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.Int64 ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,safeFileNames[i]),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.Int32 ,2),  //文件类型(0-5:渲染前,6-10:渲染后) 1.色卡 2.Dxf/Nod/ini 6.3DSMAX文件 7.结果图
                                  };

                    DataTable dt = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm );  //取回关联表ID
                    if( dt != null )
                    {
                        usedFilesID = long.Parse( dt.Rows[0][0].ToString() );
                    }

                    args.DbTrans.Commit();

                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }
                }
            }
            else  //如果没有, 插入MD5到文件表
            {
                if( isFirst )  //如果是上传色卡
                {
                    con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                    con.Open();
                    tran = con.BeginTransaction();

                    strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxFiles] ( MD5 ) values ( @MD5 ) select @@identity";  //返回文件表ID

                    args = new DALExecuteArgs( "", "", con, tran );

                    DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@FilesID",DbType.Int64 ,fileID),
                                     DBHelperSQL.CreateInDbParameter("@MD5",DbType.String ,ColorMD5)
                                  };

                    DataTable dt = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm );  //取回文件表ID
                    if( dt != null )
                    {
                        fileID = long.Parse( dt.Rows[0][0].ToString() );

                        strReturnJson = "{\"isContainSameMD5\":\"" + isContainSameMD5 + "\",\"Message\":\"\",\"noPathImageCardID\":\"" + fileID + "\"}";
                    }

                    args.DbTrans.Commit();

                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }
                }
                else  //如果是存在于服务器的dxf
                {
                    con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                    con.Open();
                    tran = con.BeginTransaction();

                    strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxFiles] ( MD5, FilePath ) values ( @MD5, @FilePath ) select @@identity";  //返回文件表ID

                    args = new DALExecuteArgs( "", "", con, tran );

                    DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@FilesID",DbType.Int64 ,fileID),
                                     DBHelperSQL.CreateInDbParameter("@MD5",DbType.String ,md5),
                                  };

                    DataTable dt = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm );  //取回文件表ID
                    if( dt != null )
                    {
                        fileID = long.Parse( dt.Rows[0][0].ToString() );
                    }

                    args.DbTrans.Commit();

                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }

                    string dxfPath = HttpContext.Current.Server.MapPath( "/" ) + @"DrawDxfFolder\CustomerDxf\" + UserID + "\\" + DoorStruct + "\\" + SeriesName + "\\";
                    string copyPath = Path.Combine( HttpRuntime.AppDomainAppPath, "MaxDrawFiles" );
                    if( !File.Exists( copyPath + "\\" + fileID ) )
                    {
                        File.Copy( dxfPath + "\\" + safeFileNames[i], copyPath + "\\" + fileID );
                    }

                    con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                    con.Open();
                    tran = con.BeginTransaction();

                    strInsertSQL = @"(update [TSMobileProduct].[dbo].[MaxFiles] set FilePath = @FilePath where FilesID = @FilesID) select @@identity";  //返回文件表ID

                    args = new DALExecuteArgs( "", "", con, tran );

                    DbParameter[] parm3 = {
                                     DBHelperSQL.CreateInDbParameter("@FilesID",DbType.Int64 ,fileID),
                                     DBHelperSQL.CreateInDbParameter("@FilePath",DbType.String ,copyPath + "\\" + fileID),
                                  };

                    dt = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm );  //取回文件表ID
                    if( dt != null )
                    {
                        fileID = long.Parse( dt.Rows[0][0].ToString() );
                    }

                    args.DbTrans.Commit();

                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }
                }
            }
            DbParameter[] parm2 = {
                                     DBHelperSQL.CreateInDbParameter("@UsedFilesID",DbType.Int64 ,usedFilesID),
                                     DBHelperSQL.CreateInDbParameter("@UserID",DbType.Int64 ,UserID),
                                     DBHelperSQL.CreateInDbParameter("@Compony",DbType.String ,Compony),
                                     DBHelperSQL.CreateInDbParameter("@Fax",DbType.String ,Fax),
                                     DBHelperSQL.CreateInDbParameter("@Manager",DbType.String ,Manager),
                                     DBHelperSQL.CreateInDbParameter("@ManagerMoblie",DbType.String ,ManagerMoblie),
                                     DBHelperSQL.CreateInDbParameter("@Agreement",DbType.String ,Agreement),
                                     DBHelperSQL.CreateInDbParameter("@Customer",DbType.String ,Customer),
                                     DBHelperSQL.CreateInDbParameter("@CustomerAddress",DbType.String,CustomerAddress),
                                     DBHelperSQL.CreateInDbParameter("@OrderDate",DbType.Date ,OrderDate),
                                     DBHelperSQL.CreateInDbParameter("@RequireDate",DbType.Date ,RequireDate),
                                     DBHelperSQL.CreateInDbParameter("@Explain",DbType.String ,Explain),
                                     DBHelperSQL.CreateInDbParameter("@SeriesName",DbType.String ,SeriesName),
                                     DBHelperSQL.CreateInDbParameter("@DoorStruct",DbType.String ,DoorStruct),
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.Int64 ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@MissionName",DbType.String ,MissionName),
                                     DBHelperSQL.CreateInDbParameter("@ColorType",DbType.String,ColorType),
                                     DBHelperSQL.CreateInDbParameter("@BordureType",DbType.String ,BordureType),
                                     DBHelperSQL.CreateInDbParameter("@ImageWidth",DbType.Int32 ,ImageWidth),
                                     DBHelperSQL.CreateInDbParameter("@ImageHeight",DbType.Int32 ,ImageHeight),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,ColorCardName),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.Int32 ,1),  //文件类型(0-5:渲染前,6-10:渲染后)1.色卡 2.Dxf/Nod/ini 6.3DSMAX文件 7.结果图
                                     DBHelperSQL.CreateInDbParameter("@MD5",DbType.String ,ColorMD5)
                                  };

            if( isContainSameMD5 )  //如果有相同的MD5, 向总表插入订单数据
            {
                if( !isFirst )  //只有第一次进入时向总表插入记录
                {
                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }
                    return;
                }

                con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                con.Open();
                tran = con.BeginTransaction();

                strInsertSQL = @"
                        insert into [TSMobileProduct].[dbo].[MaxMaster](UserID, Compony, Fax, Manager, ManagerMoblie, Agreement, 
                            Customer, CustomerAddress, OrderDate, RequireDate, Explain, SeriesName, DoorStruct, 
                            VrayID, MissionName, ColorType, BordureType, ImageWidth, ImageHeight)
                        values(@UserID, @Compony, @Fax, @Manager, @ManagerMoblie, @Agreement, @Customer,
                            @CustomerAddress, @OrderDate, @RequireDate, @Explain, @SeriesName, 
                            @DoorStruct, @VrayID, @MissionName, @ColorType, @BordureType, @ImageWidth, 
                            @ImageHeight)";

                args = new DALExecuteArgs( "", "", con, tran );

                DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strInsertSQL, true, parm2 );

                args.DbTrans.Commit();

                if( con != null )
                {
                    con.Close();
                    args = null;
                    tran = null;
                }
            }
            else  //如果没有, 将MD5所对应的文件ID插入关联表
            {
                if( isFirst )  //色卡
                {
                    con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                    con.Open();
                    tran = con.BeginTransaction();

                    strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxUsedFiles](VrayID, FileID, FileName, FileType) values (@VrayID, @FileID, @FileName, @FileType) select @@identity";

                    args = new DALExecuteArgs( "", "", con, tran );

                    DbParameter[] parm3 = {
                                     DBHelperSQL.CreateInDbParameter("@UsedFilesID",DbType.Int64 ,usedFilesID),
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,fileID),
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.Int64 ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,ColorCardName),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.Int32 ,1),  //文件类型(0-5:渲染前,6-10:渲染后) 1.色卡 2.Dxf/Nod/ini 6.3DSMAX文件 7.结果图
                                     DBHelperSQL.CreateInDbParameter("@MD5",DbType.String ,ColorMD5),
                                     DBHelperSQL.CreateInDbParameter("@Info",DbType.String ,"0:未开始")
                                  };

                    DataTable dt = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm3 );   //取回关联表ID
                    if( dt != null )
                    {
                        usedFilesID = long.Parse( dt.Rows[0][0].ToString() );
                    }

                    args.DbTrans.Commit();

                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }
                }
                else  //dxf
                {
                    con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                    con.Open();
                    tran = con.BeginTransaction();

                    strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxUsedFiles](VrayID, FileID, FileName, FileType) values (@VrayID, @FileID, @FileName, @FileType) select @@identity";

                    args = new DALExecuteArgs( "", "", con, tran );

                    DbParameter[] parm3 = {
                                     DBHelperSQL.CreateInDbParameter("@UsedFilesID",DbType.Int64 ,usedFilesID),
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,fileID),
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.Int64 ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,safeFileNames[i]),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.Int32 ,2),  //文件类型(0-5:渲染前,6-10:渲染后) 1.色卡 2.Dxf/Nod/ini 6.3DSMAX文件 7.结果图
                                  };

                    DataTable dt = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm3 );   //取回关联表ID
                    if( dt != null )
                    {
                        usedFilesID = long.Parse( dt.Rows[0][0].ToString() );
                    }

                    args.DbTrans.Commit();

                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }
                }


                if( !isFirst )  //只有第一次进入时向总表插入记录
                {
                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }
                    return;
                }

                con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                con.Open();
                tran = con.BeginTransaction();

                //最后再插入总表
                strInsertSQL = @"
                        insert into [TSMobileProduct].[dbo].[MaxMaster](UserID, Compony, Fax, Manager, ManagerMoblie, Agreement, 
                            Customer, CustomerAddress, OrderDate, RequireDate, Explain, SeriesName, DoorStruct, 
                            VrayID, MissionName, ColorType, BordureType, ImageWidth, ImageHeight, Info)
                        values(@UserID, @Compony, @Fax, @Manager, @ManagerMoblie, @Agreement, @Customer,
                            @CustomerAddress, @OrderDate, @RequireDate, @Explain, @SeriesName, @DoorStruct, @VrayID, 
                            @MissionName, @ColorType, @BordureType, @ImageWidth, @ImageHeight, @Info)";

                args = new DALExecuteArgs( "", "", con, tran );

                DbParameter[] parm4 = {
                                     DBHelperSQL.CreateInDbParameter("@UsedFilesID",DbType.Int64 ,usedFilesID),
                                     DBHelperSQL.CreateInDbParameter("@UserID",DbType.Int64 ,UserID),
                                     DBHelperSQL.CreateInDbParameter("@Compony",DbType.String ,Compony),
                                     DBHelperSQL.CreateInDbParameter("@Fax",DbType.String ,Fax),
                                     DBHelperSQL.CreateInDbParameter("@Manager",DbType.String ,Manager),
                                     DBHelperSQL.CreateInDbParameter("@ManagerMoblie",DbType.String ,ManagerMoblie),
                                     DBHelperSQL.CreateInDbParameter("@Agreement",DbType.String ,Agreement),
                                     DBHelperSQL.CreateInDbParameter("@Customer",DbType.String ,Customer),
                                     DBHelperSQL.CreateInDbParameter("@CustomerAddress",DbType.String,CustomerAddress),
                                     DBHelperSQL.CreateInDbParameter("@OrderDate",DbType.Date ,OrderDate),
                                     DBHelperSQL.CreateInDbParameter("@RequireDate",DbType.Date ,RequireDate),
                                     DBHelperSQL.CreateInDbParameter("@Explain",DbType.String ,Explain),
                                     DBHelperSQL.CreateInDbParameter("@SeriesName",DbType.String ,SeriesName),
                                     DBHelperSQL.CreateInDbParameter("@DoorStruct",DbType.String ,DoorStruct),
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.Int64 ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@MissionName",DbType.String ,MissionName),
                                     DBHelperSQL.CreateInDbParameter("@ColorType",DbType.String,ColorType),
                                     DBHelperSQL.CreateInDbParameter("@BordureType",DbType.String ,BordureType),
                                     DBHelperSQL.CreateInDbParameter("@ImageWidth",DbType.Int32 ,ImageWidth),
                                     DBHelperSQL.CreateInDbParameter("@ImageHeight",DbType.Int32 ,ImageHeight),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,ColorCardName),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.Int32 ,1),  //文件类型(0-5:渲染前,6-10:渲染后)1.色卡 2.Dxf/Nod/ini 6.3DSMAX文件 7.结果图
                                     DBHelperSQL.CreateInDbParameter("@MD5",DbType.String ,ColorMD5),
                                     DBHelperSQL.CreateInDbParameter("@Info",DbType.String ,"0:未开始")
                                  };
                DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strInsertSQL, true, parm4 );  //向总表插入订单数据

                args.DbTrans.Commit();

                if( con != null )
                {
                    con.Close();
                    args = null;
                    tran = null;
                }
            }
        }

        /// <summary>
        /// 计算文件MD5
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile( string fileName )
        {
            try
            {
                FileStream file = new FileStream( fileName, FileMode.Open );
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash( file );
                file.Close();
                string strMD5 = BytesToString( retVal );
                return strMD5;
            }
            catch( Exception )
            {
                return "";
            }
        }

        private static string BytesToString( byte[] bytes )
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