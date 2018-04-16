using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.IO;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using TS.Web.DBHelper;
using System.Security.Cryptography;

namespace TS.Web.DrawDxfFolder
{
    /// <summary>
    /// MaxWebService 的摘要说明
    /// </summary>
    [WebService( Namespace = "http://tempuri.org/" )]
    [WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1 )]
    [System.ComponentModel.ToolboxItem( false )]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class MaxWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public bool UploadFile( long VrayID, byte[] byFile, string MD5, string FileSafeName, out string ErrorMessage )
        {
            File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "进入上传文件方法     " + DateTime.Now + "\r\n" );

            ErrorMessage = "";
            try
            {
                File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "尝试连接数据库     " + DateTime.Now + "\r\n" );

                DbConnection con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                con.Open();
                DbTransaction tran = con.BeginTransaction();

                int group = Helper.ComputeGroup();

                con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                con.Open();
                tran = con.BeginTransaction();

                File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "数据库连接成功, 尝试插入MD5记录     " + DateTime.Now + "\r\n" );

                string strInsertSQL = @"insert into dbo.MaxFiles(MD5) values (@MD5) select @@identity";  //返回文件表ID

                DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );

                DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@MD5",DbType.String ,MD5)
                                  };

                DataTable dt = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm );

                long MD5ID = -1;

                if( dt != null )
                {
                    long.TryParse( dt.Rows[0][0].ToString(), out MD5ID );
                }
                args.DbTrans.Commit();

                File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "MD5插入成功, 渲染ID为:"+ MD5ID + "     " + DateTime.Now + "\r\n" );

                if( con != null )
                {
                    con.Close();
                    args = null;
                    tran = null;
                }

                File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "开始尝试写入文件     " + DateTime.Now + "\r\n" );

                string strMaxDrawFile = Path.Combine( HttpRuntime.AppDomainAppPath, "MaxDrawFiles", group.ToString() );  //文件存放目录

                if( !Directory.Exists( strMaxDrawFile ) )
                {
                    Directory.CreateDirectory( strMaxDrawFile );
                }

                using( FileStream fs = new FileStream( strMaxDrawFile + "\\" + MD5ID.ToString(), FileMode.Create, FileAccess.Write ) )
                {
                    fs.Write( byFile, 0, byFile.Length );  //写入文件

                    File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "文件写入成功, 路径为:"+ strMaxDrawFile + "\\" + MD5ID.ToString() + "     " + DateTime.Now + "\r\n" );
                }

                File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "开始更新数据库文件路径     " + DateTime.Now + "\r\n" );

                con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                con.Open();
                tran = con.BeginTransaction();

                strInsertSQL = @"update dbo.MaxFiles set FilePath = @FilePath where FileID = @FileID";

                args = new DALExecuteArgs( "", "", con, tran );

                DbParameter[] parm2 = {
                                     DBHelperSQL.CreateInDbParameter("@FilePath",DbType.String ,strMaxDrawFile + "\\" + MD5ID.ToString() ),
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,MD5ID ),
                                  };

                DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm2 );

                args.DbTrans.Commit();

                File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "记录更新成功     " + DateTime.Now + "\r\n" );

                if( con != null )
                {
                    con.Close();
                    args = null;
                    tran = null;
                }

                con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                con.Open();
                tran = con.BeginTransaction();

                strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxUsedFiles](VrayID, FileID, [FileName], FileType) values (@VrayID, @FileID, @FileName, @FileType)
                                             update [TSMobileProduct].[dbo].[MaxMaster] set Info = @Info where VrayID = @VrayID";

                args = new DALExecuteArgs( "", "", con, tran );

                int fileType = -1;
                string info = "";
                fileType = 6;
                info = "2:已开始";

                DbParameter[] parm3 = {
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,MD5ID),
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.Int64 ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,FileSafeName),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.Int32 ,fileType),  //文件类型(0-5:渲染前,6-10:渲染后) 1.色卡 2.Dxf/Nod/ini 6.3DSMAX文件 7.结果图
                                     DBHelperSQL.CreateInDbParameter("@Info",DbType.String ,info)
                                  };

                DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strInsertSQL, true, parm3 );  //将对应MD5的ID插入关联表，更新主表状态

                args.DbTrans.Commit();

                if( con != null )
                {
                    con.Close();
                    args = null;
                    tran = null;
                }
                return true;
            }
            catch( Exception ex )
            {
                File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "发生异常!!!!!" + ex.Message + ex.Source + ex.StackTrace + ex.TargetSite + "     " + DateTime.Now + "\r\n" );

                ErrorMessage = ex.Message;
                return false;
            }
        }

        [WebMethod]
        public bool UploadMD5( long VrayID, string MD5, string FileSafeName, string Info, int FileType, out string ErrorMessage )
        {

            File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "进入上传MD5方法     " + DateTime.Now + "\r\n" );

            ErrorMessage = "";
            try
            {
                File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "尝试连接数据库     " + DateTime.Now + "\r\n" );

                SqlConnection con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                con.Open();
                DbTransaction tran = con.BeginTransaction();

                DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );

                File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "数据库连接成功, 开始查询是否存在相同的MD5     " + DateTime.Now + "\r\n" );

                string strSelectSQL = "select * from [TSMobileProduct].[dbo].[MaxFiles] where MD5 = '" + MD5 + "'";
                DataTable dtExists = DBHelperSQL.ExecuteQuery( args, strSelectSQL, null );  //先查是否存在相同的MD5
                long sameMD5ID = 0;

                if( dtExists.Rows.Count > 0 )  //存在
                {
                    File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "查询成功, 存在相同MD5, 开始更新使用信息     " + DateTime.Now + "\r\n" );

                    sameMD5ID = Convert.ToInt64( dtExists.Rows[0]["FileID"] );

                    con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                    con.Open();
                    tran = con.BeginTransaction();

                    string strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxUsedFiles](VrayID, FileID, [FileName], FileType) values (@VrayID, @FileID, @FileName, @FileType)
                                                        update [TSMobileProduct].[dbo].[MaxMaster] set Info = @info where VrayID = @VrayID";

                    args = new DALExecuteArgs( "", "", con, tran );

                    DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.Int64 ,sameMD5ID),
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.Int64 ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,FileSafeName),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.Int32 ,FileType),  //文件类型(0-5:渲染前,6-10:渲染后) 1.色卡 2.Dxf/Nod/ini 6.3DSMAX文件 7.结果图
                                     DBHelperSQL.CreateInDbParameter("@Info",DbType.String ,Info)
                                  };

                    DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strInsertSQL, true, parm );  //将对应MD5的ID插入关联表，更新主表状态

                    args.DbTrans.Commit();

                    File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "使用信息更新成功     " + DateTime.Now + "\r\n" );

                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }
                }
                else
                {
                    File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt", "查询成功, 不存在相同的MD5, 尝试返回需要上传的信息     " + DateTime.Now + "\r\n" );
                    return true;  //返回需要上传文件信息
                }
                return false;
            }
            catch( Exception ex )
            {
                File.AppendAllText( HttpRuntime.AppDomainAppPath + "\\MaxAsmx日志.txt","发生异常!!!!!" + ex.Message + ex.Source + ex.StackTrace + ex.TargetSite + "     " + DateTime.Now + "\r\n" );

                ErrorMessage = ex.Message;
            }
            return false;
        }

        [WebMethod]
        public bool UploadInfo( long VrayID, string info, out string ErrorMessage )
        {
            ErrorMessage = "";
            try
            {
                SqlConnection con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                con.Open();
                DbTransaction tran = con.BeginTransaction();

                DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );


                string strInsertSQL = @"update [TSMobileProduct].[dbo].[MaxMaster] set Info = @info where VrayID = @VrayID";

                args = new DALExecuteArgs( "", "", con, tran );


                DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.Int64 ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@Info",DbType.String ,info)
                                  };

                DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strInsertSQL, true, parm );  //将对应MD5的ID插入关联表，更新主表状态

                args.DbTrans.Commit();

                if( con != null )
                {
                    con.Close();
                    args = null;
                    tran = null;
                }
                return true;
            }
            catch( Exception ex )
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        [WebMethod]
        public bool InsertDrawInfo( long UserID, string Compony, string Fax, string Manager, string ManagerMoblie, string Agreement, string Customer,
            string CustomerAddress, string OrderDate, string RequireDate, string Explain, string SeriesName, string DoorStruct, long VrayID, string MissionName,
            string ColorType, string ColorCardName, string BordureType, int ImageWidth, int ImageHeight, string CustomerPhone, out string ErrorMessage )
        {
            ErrorMessage = "";
            DbConnection con = null;
            DbTransaction tran = null;
            try
            {
                con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                con.Open();
                tran = con.BeginTransaction();

                string strInsertSQL = @"
                        insert into [TSMobileProduct].[dbo].[MaxMaster](UserID, Compony, Fax, Manager, ManagerMoblie, Agreement, 
                            Customer, CustomerAddress, OrderDate, RequireDate, Explain, SeriesName, DoorStruct, 
                            VrayID, MissionName, ColorType, BordureType, ImageWidth, ImageHeight, Info, CustomerPhone)
                        values(@UserID, @Compony, @Fax, @Manager, @ManagerMoblie, @Agreement, @Customer,
                            @CustomerAddress, @OrderDate, @RequireDate, @Explain, @SeriesName, 
                            @DoorStruct, @VrayID, @MissionName, @ColorType, @BordureType, @ImageWidth, 
                            @ImageHeight, @Info, @CustomerPhone)";

                DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );

                DbParameter[] parm2 = {
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
                                     DBHelperSQL.CreateInDbParameter("@Info",DbType.String ,"0:未开始"),
                                     DBHelperSQL.CreateInDbParameter("@CustomerPhone",DbType.String ,CustomerPhone),
                                  };

                DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strInsertSQL, true, parm2 );

                args.DbTrans.Commit();

                if( con != null )
                {
                    con.Close();
                    args = null;
                    tran = null;
                }
                return true;
            }
            catch( Exception ex )
            {
                ErrorMessage = ex.Message;
                return false;
            }
            finally
            {
                if( con != null )
                {
                    con.Close();
                }
            }
        }

        [WebMethod]
        public long SelectSameColorCardMD5orInsert( long VrayID, string ColorCardMD5, string FileName, out string ErrorMessage )
        {
            ErrorMessage = "";
            DbConnection con = null;
            DbTransaction tran = null;
            try
            {
                con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                con.Open();
                tran = con.BeginTransaction();

                DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );
                
                string strInsertSQL = @"select * from [TSMobileProduct].[dbo].[MaxFiles] where MD5 = @MD5";

                args = new DALExecuteArgs( "", "", con, tran );
                
                DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@MD5",DbType.String ,ColorCardMD5)
                                  };

                DataTable dt = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm );  //取回文件ID
                long iFileID = -1;
                if( dt != null && dt.Rows.Count > 0)
                {
                    long.TryParse( dt.Rows[0]["FileID"].ToString(), out iFileID );
                }

                args.DbTrans.Commit();

                if( con != null )
                {
                    con.Close();
                    args = null;
                    tran = null;
                }

                if( iFileID != -1 )  //存在相同MD5
                {
                    con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                    con.Open();
                    tran = con.BeginTransaction();

                    args = new DALExecuteArgs( "", "", con, tran );

                    strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxUsedFiles](VrayID, FileID, FileName, FileType) Values (@VrayID, @FileID, @FileName, @FileType)";

                    args = new DALExecuteArgs( "", "", con, tran );

                    DbParameter[] parm3 = {
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.String ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.String ,iFileID),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,FileName),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.String ,1),
                                  };

                    DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm3 );

                    args.DbTrans.Commit();

                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }
                    return -1;  //不需要上传色卡文件
                }
                else  //不存在相同MD5
                {
                    con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                    con.Open();
                    tran = con.BeginTransaction();

                    args = new DALExecuteArgs( "", "", con, tran );

                    strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxFiles](MD5) Values (@MD5) select @@identity as ID";

                    args = new DALExecuteArgs( "", "", con, tran );

                    DbParameter[] parm2 = {
                                     DBHelperSQL.CreateInDbParameter("@MD5",DbType.String ,ColorCardMD5)
                                  };

                    DataTable dt2 = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm2 );  //取回关联表ID
                    iFileID = -1;
                    if( dt2 != null && dt2.Rows.Count > 0 )
                    {
                        long.TryParse( dt2.Rows[0]["ID"].ToString(), out iFileID );
                    }
                    args.DbTrans.Commit();

                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }

                    if( iFileID != -1 )
                    {
                        con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                        con.Open();
                        tran = con.BeginTransaction();

                        args = new DALExecuteArgs( "", "", con, tran );

                        strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxUsedFiles](VrayID, FileID, FileName, FileType) Values (@VrayID, @FileID, @FileName, @FileType)";

                        args = new DALExecuteArgs( "", "", con, tran );

                        DbParameter[] parm3 = {
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.String ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.String ,iFileID),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,FileName),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.String ,1),
                                  };

                        DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm3 );

                        args.DbTrans.Commit();

                        if( con != null )
                        {
                            con.Close();
                            args = null;
                            tran = null;
                        }
                    }
                    else
                    {
                        ErrorMessage = "数据库不存在相同的MD5, 但MD5记录插入失败";
                    }
                    return iFileID;  //需要上传
                }
            }
            catch( Exception ex )
            {
                ErrorMessage = ex.Message;
                if( tran != null )
                {
                    tran.Rollback();
                }
                return -1;
            }
            finally
            {
                if( con != null )
                {
                    con.Close();
                }
            }
        }

        [WebMethod]
        public bool UploadColorCard( long FileID, Byte[] strColorCardBytes, out string ErrorMessage )
        {
            ErrorMessage = "";
            DbConnection con = null;
            DbTransaction tran = null;
            try
            {
                int group = Helper.ComputeGroup();

                string strMaxDrawFile = Path.Combine( HttpRuntime.AppDomainAppPath, "MaxDrawFiles", group.ToString() );  //色卡存放目录
                if( !Directory.Exists( strMaxDrawFile ) )
                {
                    Directory.CreateDirectory( strMaxDrawFile );
                }

                File.WriteAllBytes( strMaxDrawFile + "\\" + FileID, strColorCardBytes );

                con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                con.Open();
                tran = con.BeginTransaction();

                string strInsertSQL = @"update [TSMobileProduct].[dbo].[MaxFiles] set FilePath = @FilePath where FileID = @FileID";  //返回文件表ID

                DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );

                DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.String, FileID),
                                     DBHelperSQL.CreateInDbParameter("@FilePath",DbType.String, strMaxDrawFile + "\\" + FileID)
                                  };

                DataTable dt = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm );
                args.DbTrans.Commit();
                
                if( con != null )
                {
                    con.Close();
                    args = null;
                    tran = null;
                }
            }
            catch( Exception ex )
            {
                ErrorMessage = ex.Message;
                if( tran != null )
                {
                    tran.Rollback();
                }
                return false;
            }
            finally
            {
                if( con != null )
                {
                    con.Close();
                }
            }
            return true;
        }

        [WebMethod]
        public bool CopyAndUpdateDxf( long VrayID, long UserID, string DoorStruct, string SeriesName, out string ErrorMessage )
        {
            ErrorMessage = "";
            DbConnection con = null;
            DbTransaction tran = null;
            try
            {
                string dxfPath = HttpContext.Current.Server.MapPath( "/" ) + @"DrawDxfFolder\CustomerDxf\" + UserID + "\\" + DoorStruct + "\\" + SeriesName + "\\";
                int group = Helper.ComputeGroup();
                string copyPath = Path.Combine( HttpRuntime.AppDomainAppPath, "MaxDrawFiles", group.ToString() );  //文件存放目录

                string[] dxfDir = Directory.GetFiles( dxfPath );

                for( int i = 0; i < dxfDir.Length; i++ )
                {
                    string DxfMD5 = Helper.GetMD5HashFromFile( dxfDir[i] );

                    con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                    con.Open();
                    tran = con.BeginTransaction();

                    DALExecuteArgs args = new DALExecuteArgs( "", "", con, tran );

                    string strInsertSQL = @"select * from [TSMobileProduct].[dbo].[MaxFiles] where MD5 = @MD5";

                    args = new DALExecuteArgs( "", "", con, tran );

                    DbParameter[] parm = {
                                     DBHelperSQL.CreateInDbParameter("@MD5",DbType.String ,DxfMD5)
                                  };

                    DataTable dt = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm );  //取回关联表ID
                    long iFileID = -1;
                    if( dt != null && dt.Rows.Count > 0 )
                    {
                        long.TryParse( dt.Rows[0]["FileID"].ToString(), out iFileID );
                    }

                    args.DbTrans.Commit();

                    if( con != null )
                    {
                        con.Close();
                        args = null;
                        tran = null;
                    }

                    if( iFileID != -1 )  //存在相同的MD5
                    {
                        string safeFileName = Path.GetFileName( dxfDir[i] );

                        con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                        con.Open();
                        tran = con.BeginTransaction();

                        args = new DALExecuteArgs( "", "", con, tran );

                        strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxUsedFiles](VrayID, FileID, [FileName], FileType) Values (@VrayID, @FileID, @FileName, @FileType)";

                        args = new DALExecuteArgs( "", "", con, tran );

                        DbParameter[] parm3 = {
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.String ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.String ,iFileID),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,safeFileName),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.String ,2)
                                  };

                        DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm3 );  //取回关联表ID

                        args.DbTrans.Commit();

                        if( con != null )
                        {
                            con.Close();
                            args = null;
                            tran = null;
                        }
                    }
                    else
                    {
                        string safeFileName = Path.GetFileName( dxfDir[i] );

                        con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                        con.Open();
                        tran = con.BeginTransaction();

                        args = new DALExecuteArgs( "", "", con, tran );

                        strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxFiles](MD5) Values (@MD5) select @@identity as ID";

                        args = new DALExecuteArgs( "", "", con, tran );

                        DbParameter[] parm3 = {
                                     DBHelperSQL.CreateInDbParameter("@MD5",DbType.String ,DxfMD5)
                                  };

                        DataTable dt3 =  DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm3 );  //取回文件ID

                        string strFileID = "";
                        if( dt3 != null && dt3.Rows.Count > 0 )
                        {
                            strFileID = dt3.Rows[0]["ID"].ToString();
                        }

                        if( !string.IsNullOrEmpty( strFileID ) )
                        {
                            File.Copy( dxfDir[i], copyPath + "\\" + strFileID );
                        }

                        args.DbTrans.Commit();

                        if( con != null )
                        {
                            con.Close();
                            args = null;
                            tran = null;
                        }
                        

                        con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                        con.Open();
                        tran = con.BeginTransaction();

                        args = new DALExecuteArgs( "", "", con, tran );

                        strInsertSQL = @"update [TSMobileProduct].[dbo].[MaxFiles] set FilePath = @FilePath where MD5 = @MD5 
                                         select FileID from [TSMobileProduct].[dbo].[MaxFiles] where MD5 = @MD5";

                        args = new DALExecuteArgs( "", "", con, tran );

                        DbParameter[] parm4 = {
                                     DBHelperSQL.CreateInDbParameter("@MD5",DbType.String, DxfMD5),
                                     DBHelperSQL.CreateInDbParameter("@FilePath",DbType.String, copyPath + "\\" + strFileID)
                                  };

                        DataTable dt4 = DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm4 );  //取回文件ID
                        
                        if( dt4 != null && dt4.Rows.Count > 0 )
                        {
                            strFileID = dt4.Rows[0]["FileID"].ToString();
                        }

                        args.DbTrans.Commit();

                        if( con != null )
                        {
                            con.Close();
                            args = null;
                            tran = null;
                        }


                        con = new SqlConnection( DbConfig.MPCmsConString4Product );  //产品数据库
                        con.Open();
                        tran = con.BeginTransaction();

                        args = new DALExecuteArgs( "", "", con, tran );

                        strInsertSQL = @"insert into [TSMobileProduct].[dbo].[MaxUsedFiles](VrayID, FileID, [FileName], FileType) Values (@VrayID, @FileID, @FileName, @FileType)";

                        args = new DALExecuteArgs( "", "", con, tran );

                        DbParameter[] parm5 = {
                                     DBHelperSQL.CreateInDbParameter("@VrayID",DbType.String ,VrayID),
                                     DBHelperSQL.CreateInDbParameter("@FileID",DbType.String ,strFileID),
                                     DBHelperSQL.CreateInDbParameter("@FileName",DbType.String ,safeFileName),
                                     DBHelperSQL.CreateInDbParameter("@FileType",DbType.String ,2)
                                  };

                        DBHelperSQL.ExecuteQuery( args, strInsertSQL, parm5 );

                        args.DbTrans.Commit();

                        if( con != null )
                        {
                            con.Close();
                            args = null;
                            tran = null;
                        }
                    }
                }

                if( !Directory.Exists( copyPath ) )
                {
                    Directory.CreateDirectory( copyPath );
                }
                return true;
            }
            catch( Exception ex )
            {
                ErrorMessage = ex.Message;
                if( tran != null )
                {
                    tran.Rollback();
                }
                return false;
            }
            finally
            {
                if( con != null )
                {
                    con.Close();
                }
            }
            return false;
        }
    }
}
