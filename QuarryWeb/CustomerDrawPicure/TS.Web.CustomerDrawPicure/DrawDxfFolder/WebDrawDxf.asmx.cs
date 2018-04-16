using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.IO;
using System.Text;

namespace TS.Web.DrawDxfFolder
{
    /// <summary>
    /// WebDrawDxf 的摘要说明
    /// </summary>
    [WebService( Namespace = "http://tempuri.org/" )]
    [WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1 )]
    [System.ComponentModel.ToolboxItem( false )]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebDrawDxf : System.Web.Services.WebService
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="SeriesName"></param>
        /// <param name="DoorStruct"></param>
        /// <param name="FileName"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Service_DeleteFile( long TSUserID, string SeriesName, string DoorStruct, string FileName, out string Message )
        {
            bool bolSuccess = true;
            Message = "";
            try
            {
                string strSeriesPath = GetSeriesPath( TSUserID, SeriesName, DoorStruct );
                string strFilePath = Path.Combine( strSeriesPath, FileName );

                if( File.Exists( strFilePath ) )
                {
                    File.Delete( strFilePath );
                }
            }
            catch( Exception ex )
            {
                Message = "删除失败。";
                bolSuccess = false;
            }
            return bolSuccess;
        }

        /// <summary>
        /// 恢复默认
        /// </summary>
        /// <param name="TSUserID">用户ID</param>
        /// <param name="SeriesName">系列名称</param>
        /// <param name="DoorStruct"> 结构名称</param>
        /// <param name="FileName">默认文件名，有点</param>
        /// <param name="Message"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Service_RecoverDefault( long TSUserID, string SeriesName, string DoorStruct, string FileName, out string Message )
        {
            Message = "";
            try
            {
                string strDefautlSeriesDxfPath = GetDefaultSeriesPath( TSUserID, DoorStruct );
                string strSeriesPath = GetSeriesPath( TSUserID, SeriesName, DoorStruct );
                string strDefaultFilePath = Path.Combine( strDefautlSeriesDxfPath, FileName );
                string strFilePath = Path.Combine( strSeriesPath, FileName );

                if( File.Exists( strDefaultFilePath ) )
                {
                    File.Copy( strDefaultFilePath, strFilePath, true );
                }
                else if( File.Exists( strFilePath ) )
                {
                    File.Delete( strFilePath );
                }
            }
            catch( Exception ex )
            {
                Message = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 新建型材系列,并复制默认系列文件至创建好的文件夹
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="SeriesName"></param>
        /// <param name="SeriesFileList"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Service_CreateSeriesAndCopy( long TSUserID, string SeriesName, string DoorStruct, out string Message )
        {
            bool bolSuccess = true;
            Message = "";
            try
            {
                string strSeriesDxfPath = Path.Combine( Server.MapPath( "" ), "CustomerDxf", TSUserID.ToString(), DoorStruct, SeriesName );
                if( Directory.Exists( strSeriesDxfPath ) )
                {
                    Message = "该型材系列已经存在。";
                    bolSuccess = false;
                }
                else
                {
                    Directory.CreateDirectory( strSeriesDxfPath );

                    #region -- 复制默认型材系列文件到用户文件夹 --
                    //获取文件名列表
                    DataTable dtFileList;
                    bool getListResult = Service_GetSeriesFileList( 123456, "截面图拼接图", "默认型材系列", out dtFileList );
                    //获取文件
                    DataTable Series;
                    if( getListResult && dtFileList.Rows.Count > 0 )
                    {
                        bool getContent = Service_GetSeriesFileContent( 123456, "截面图拼接图", "默认型材系列", dtFileList, out Series );
                        if( getContent && Series.Rows.Count > 0 )
                        {
                            //写入文件至指定文件夹
                            foreach( DataRow dr in Series.Rows )
                            {
                                string strFileName = dr["FileName"].ToString();
                                string FileContent = dr["FileContent"].ToString();
                                DateTime FileTime = DateTime.Parse( dr["FileTime"].ToString() );
                                Service_SaveFile( TSUserID, SeriesName, DoorStruct, strFileName, FileContent, FileTime, out Message );
                            }
                        }
                    }
                    #endregion

                }
            }
            catch( Exception ex )
            {
                Message = "转移失败";
                bolSuccess = false;
            }
            return bolSuccess;
        }


        /// <summary> 
        /// 读取该用户的所有型材系列
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="AllSeries"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Service_GetAllSeries( long TSUserID, out DataTable AllSeries )
        {
            bool bolSuccess = true;
            AllSeries = new DataTable( "AllSeries" );
            AllSeries.Columns.Add( new DataColumn( "SeriesName" ) );
            AllSeries.Columns.Add( new DataColumn( "DoorStruct" ) );

            try
            {
                string strUserDxfPath = Path.Combine( Server.MapPath( "" ), "CustomerDxf", TSUserID.ToString() );
                string[] strDoorStructs = new string[] { "平开窗", "平开窗(窗纱一体)", "二轨推拉窗", "三轨推拉窗" };
                foreach(string strDoorStruct in strDoorStructs)
                {
                    string strDoorStructPath = Path.Combine( strUserDxfPath, strDoorStruct );
                    if( !Directory.Exists( strDoorStructPath ) )
                    {
                        Directory.CreateDirectory( strDoorStructPath );
                    }
                    foreach( string strPath in Directory.GetDirectories( strDoorStructPath ) )
                    {
                        DirectoryInfo dir = new DirectoryInfo( strPath );
                        DataRow drNew = AllSeries.NewRow();
                        drNew["SeriesName"] = dir.Name;
                        drNew["DoorStruct"] = strDoorStruct;
                        AllSeries.Rows.Add( drNew );
                    }
                }
                AllSeries.AcceptChanges();
            }
            catch( Exception ex )
            {
                bolSuccess = false;
            }
            return bolSuccess;
        }

        /// <summary>
        /// 读取该用户的所有型材系列
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="Series"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Service_GetSeriesFileContent( long TSUserID, string SeriesName, string DoorStruct, DataTable FileTable, out DataTable Series )
        {
            bool bolSuccess = true;
            Series = new DataTable( "Series" );
            Series.Columns.Add( new DataColumn( "FileName" ) );
            Series.Columns.Add( new DataColumn( "FileContent" ) );
            Series.Columns.Add( new DataColumn( "FileTime" ) );

            try
            {
                string strSeriesPath = GetSeriesPath( TSUserID, SeriesName, DoorStruct );
                foreach( DataRow dr in FileTable.Rows )
                {
                    string strFileName = dr["FileName"].ToString();
                    string strFilePath = Path.Combine( strSeriesPath, strFileName );
                    DataRow drNew = Series.NewRow();
                    drNew["FileName"] = strFileName;
                    if( File.Exists( strFilePath ) )
                    {
                        drNew["FileContent"] = BytesToString( File.ReadAllBytes( strFilePath ) );
                        FileInfo file = new FileInfo( strFilePath );
                        drNew["FileTime"] = file.LastWriteTime.ToString( "yyyy-MM-dd HH:mm:ss" );
                    }
                    Series.Rows.Add( drNew );
                }
                Series.AcceptChanges();
            }
            catch( Exception ex )
            {
                bolSuccess = false;
            }
            return bolSuccess;
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="Series"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Service_GetFile( long TSUserID, string SeriesName, string DoorStruct, string FileName, out string FileContent, out DateTime FileTime )
        {
            bool bolSuccess = true;
            FileContent = "";
            FileTime = DateTime.Now;

            try
            {
                string strSeriesPath = GetSeriesPath( TSUserID, SeriesName, DoorStruct );
                string strFilePath = Path.Combine( strSeriesPath, FileName );

                if( File.Exists( strFilePath ) )
                {
                    if( FileName.EndsWith( ".ini" ) || FileName.EndsWith( ".nod" ) )
                    {
                        FileContent = File.ReadAllText( strFilePath );
                    }
                    else
                    {
                        //FileContent = Base64.ToBase64( File.ReadAllBytes( strFilePath ) );
                        FileContent = BytesToString( File.ReadAllBytes( strFilePath ) );
                    }
                    FileInfo file = new FileInfo( strFilePath );
                    FileTime = file.LastWriteTime;
                }
            }
            catch( Exception ex )
            {
                bolSuccess = false;
            }
            return bolSuccess;
        }

        /// <summary>
        /// 读取该用户的所有型材系列文件信息
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="SeriesFileList"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Service_GetSeriesFileList( long TSUserID, string SeriesName, string DoorStruct, out DataTable SeriesFileList )
        {
            bool bolSuccess = true;
            SeriesFileList = new DataTable( "SeriesFileList" );
            SeriesFileList.Columns.Add( new DataColumn( "FileName" ) );
            SeriesFileList.Columns.Add( new DataColumn( "FileTime" ) );
            SeriesFileList.Columns.Add( new DataColumn( "FileLength", typeof( long ) ) );

            try
            {
                string strSeriesPath = GetSeriesPath( TSUserID, SeriesName, DoorStruct );

                foreach( string strPath in Directory.GetFiles( strSeriesPath ) )
                {
                    FileInfo file = new FileInfo( strPath );
                    DataRow drNew = SeriesFileList.NewRow();
                    drNew["FileName"] = file.Name;
                    drNew["FileTime"] = file.LastWriteTime.ToString( "yyyy-MM-dd HH:mm:ss" );
                    drNew["FileLength"] = file.Length;
                    SeriesFileList.Rows.Add( drNew );
                }
                SeriesFileList.AcceptChanges();
            }
            catch( Exception ex )
            {
                bolSuccess = false;
            }
            return bolSuccess;
        }

        /// <summary>
        /// 读取该用户的型材系列文件信息
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="SeriesFileList"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Service_GetSeriesFileInfo( long TSUserID, string SeriesName, string DoorStruct, DataTable FileTable, out DataTable SeriesFileList )
        {
            bool bolSuccess = true;
            SeriesFileList = new DataTable( "SeriesFileList" );
            SeriesFileList.Columns.Add( new DataColumn( "FileName" ) );
            SeriesFileList.Columns.Add( new DataColumn( "FileTime" ) );
            SeriesFileList.Columns.Add( new DataColumn( "FileLength", typeof( long ) ) );

            try
            {
                string strSeriesPath = GetSeriesPath( TSUserID, SeriesName, DoorStruct );

                foreach( DataRow dr in FileTable.Rows )
                {
                    string strFileName = dr["FileName"].ToString();
                    string strFilePath = Path.Combine( strSeriesPath, strFileName );
                    DataRow drNew = SeriesFileList.NewRow();
                    drNew["FileName"] = strFileName;
                    if( File.Exists( strFilePath ) )
                    {
                        FileInfo file = new FileInfo( strFilePath );
                        drNew["FileTime"] = file.LastWriteTime.ToString( "yyyy-MM-dd HH:mm:ss" );
                        drNew["FileLength"] = file.Length;
                    }
                    SeriesFileList.Rows.Add( drNew );
                }
                SeriesFileList.AcceptChanges();
            }
            catch( Exception ex )
            {
                bolSuccess = false;
            }
            return bolSuccess;
        }

        /// <summary>
        /// 新建型材系列(可以复制默认)
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="SeriesName"></param>
        /// <param name="DoorStruct"></param>
        /// <param name="CopyDefault"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Service_CreateSeries( long TSUserID, string SeriesName, string DoorStruct, bool CopyDefault, out string Message )
        {
            Message = "";
            try
            {
                string strSeriesPath = Path.Combine( Server.MapPath( "" ), "CustomerDxf", TSUserID.ToString(), DoorStruct, SeriesName );
                if( Directory.Exists( strSeriesPath ) )
                {
                    Message = "该型材系列已经存在。";
                    return false;
                }
                Directory.CreateDirectory( strSeriesPath );

                if( CopyDefault )
                {
                    //复制用户默认型材系列文件到新型材系列文件夹
                    string strDefautlSeriesDxfPath = GetDefaultSeriesPath( TSUserID, DoorStruct );
                    DirectoryInfo di = new DirectoryInfo( strDefautlSeriesDxfPath );
                    foreach( FileInfo fi in di.GetFiles() )
                    {
                        string strFilePath = Path.Combine( strSeriesPath, fi.Name );
                        fi.CopyTo( strFilePath, true );
                    }
                }
            }
            catch( Exception ex )
            {
                Message = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除型材系列
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="SeriesName"></param>
        /// <param name="SeriesFileList"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Service_DeleteSeries( long TSUserID, string SeriesName, string DoorStruct, out string Message )
        {
            bool bolSuccess = true;
            Message = "";
            try
            {
                string strSeriesDxfPath = Path.Combine( Server.MapPath( "" ), "CustomerDxf", TSUserID.ToString(), DoorStruct, SeriesName );
                if( Directory.Exists( strSeriesDxfPath ) )
                {
                    Directory.Delete( strSeriesDxfPath, true );
                }
                else
                {
                    Message = "该型材系列已经不存在。";
                    bolSuccess = false;
                }
            }
            catch( Exception ex )
            {
                bolSuccess = false;
            }
            return bolSuccess;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="TSUserID"></param>
        /// <param name="SeriesName"></param>
        /// <param name="SeriesFileList"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Service_SaveFile( long TSUserID, string SeriesName, string DoorStruct, string FileName, string FileContent, DateTime FileTime, out string Message )
        {
            bool bolSuccess = true;
            Message = "";
            try
            {
                string strSeriesPath = GetSeriesPath( TSUserID, SeriesName, DoorStruct );
                string strFilePath = Path.Combine( strSeriesPath, FileName );
                if( strFilePath.EndsWith( ".ini" ) || strFilePath.EndsWith( ".nod" ) )
                {
                    File.WriteAllText( strFilePath, FileContent );
                }
                else
                {
                    File.WriteAllBytes( strFilePath, StringToBytes( FileContent ) );
                    //File.WriteAllBytes( strFilePath, Base64.FromBase64( FileContent ) );
                }
                if( File.Exists( strFilePath ) )
                {
                    FileInfo file = new FileInfo( strFilePath );
                    file.LastWriteTime = FileTime;
                }
            }
            catch( Exception ex )
            {
                Message = "上传失败。";
                bolSuccess = false;
            }
            return bolSuccess;
        }

        ///// <summary>
        ///// 保存文件
        ///// </summary>
        ///// <param name="TSUserID"></param>
        ///// <param name="SeriesName"></param>
        ///// <param name="SeriesFileList"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public bool Service_SaveFile( long TSUserID, string SeriesName, string DoorStruct, DataTable FileTable, out string Message )
        //{
        //    bool bolSuccess = true;
        //    Message = "";
        //    try
        //    {
        //        string strSeriesPath = GetSeriesPath( TSUserID, SeriesName, DoorStruct );
        //        foreach( DataRow dr in FileTable.Rows )
        //        {
        //            string strFileName = dr["FileName"].ToString();
        //            string strFileContent = dr["FileContent"].ToString();
        //            string strFilePath = Path.Combine( strSeriesPath, strFileName );
        //            if( strFilePath.EndsWith( ".ini" ) || strFilePath.EndsWith( ".nod" ) )
        //            {
        //                File.WriteAllText( strFilePath, strFileContent );
        //            }
        //            else
        //            {
        //                File.WriteAllBytes( strFilePath, Base64.FromBase64( strFileContent ) );
        //            }
        //            if( File.Exists( strFilePath ) )
        //            {
        //                FileInfo file = new FileInfo( strFilePath );
        //                file.LastWriteTime = DateTime.Parse( dr["FileTime"].ToString() );
        //            }
        //        }
        //    }
        //    catch( Exception ex )
        //    {
        //        Message = "上传失败。";
        //        bolSuccess = false;
        //    }
        //    return bolSuccess;
        //}

        ///// <summary>
        ///// 保存dxf
        ///// </summary>
        ///// <param name="TSUserID"></param>
        ///// <param name="SeriesName"></param>
        ///// <param name="SeriesFileList"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public bool Service_SaveDxf( long TSUserID, string SeriesName, string DoorStruct, string DxfFile, string DxfContent, string IniFile, string IniContent, out string Message )
        //{
        //    bool bolSuccess = true;
        //    Message = "";
        //    try
        //    {
        //        string strSeriesPath = GetSeriesPath( TSUserID, SeriesName, DoorStruct );
        //        string strDxfFilePath = Path.Combine( strSeriesPath, DxfFile );
        //        File.WriteAllBytes( strDxfFilePath, Base64.FromBase64( DxfContent ) );
        //        string strIniFilePath = Path.Combine( strSeriesPath, IniFile );
        //        File.WriteAllBytes( strIniFilePath, Base64.FromBase64( IniContent ) );
        //    }
        //    catch( Exception ex )
        //    {
        //        Message = "上传失败。";
        //        bolSuccess = false;
        //    }
        //    return bolSuccess;
        //}

        ///// <summary>
        ///// 保存dxf
        ///// </summary>
        ///// <param name="TSUserID"></param>
        ///// <param name="SeriesName"></param>
        ///// <param name="SeriesFileList"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public bool Service_SaveDxfNode( long TSUserID, string SeriesName, string DoorStruct, string DxfNodeFile, string DxfNodeContent, out string Message )
        //{
        //    bool bolSuccess = true;
        //    Message = "";
        //    try
        //    {
        //        string strSeriesPath = GetSeriesPath( TSUserID, SeriesName, DoorStruct );
        //        string strDxfFilePath = Path.Combine( strSeriesPath, DxfNodeFile );
        //        File.WriteAllBytes( strDxfFilePath, Base64.FromBase64( DxfNodeContent ) );
        //    }
        //    catch( Exception ex )
        //    {
        //        Message = "上传失败。";
        //        bolSuccess = false;
        //    }
        //    return bolSuccess;
        //}

        private string GetSeriesPath( long TSUserID, string SeriesName, string DoorStruct )
        {
            if( SeriesName == "" )
            {
                throw new Exception( "没有型材系列。" );
            }
            if( DoorStruct == "" )
            {
                throw new Exception( "没有窗型类别。" );
            }
            string strSeriesPath = Path.Combine( Server.MapPath( "" ), "CustomerDxf", TSUserID.ToString(), DoorStruct, SeriesName );
            if( !Directory.Exists( strSeriesPath ) )
            {
                Directory.CreateDirectory( strSeriesPath );
            }
            return strSeriesPath;
        }

        private string GetDefaultSeriesPath( long TSUserID, string DoorStruct )
        {
            if( DoorStruct == "没有窗型类别。" )
            {
                throw new Exception( "" );
            }
            string strDefaultSeriesPath = Path.Combine( Server.MapPath( "" ), "CustomerDxf", TSUserID.ToString(), "Default", DoorStruct );
            if( !Directory.Exists( strDefaultSeriesPath ) )
            {
                Directory.CreateDirectory( strDefaultSeriesPath );
            }
            return strDefaultSeriesPath;
        }

        private string BytesToString( byte[] bytes )
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

        private byte[] StringToBytes( string strHexString )
        {
            strHexString = strHexString.Replace( " ", "" );
            byte[] buffer = new byte[strHexString.Length / 2];
            for( int i = 0; i < strHexString.Length; i += 2 )
            {
                buffer[i / 2] = (byte)Convert.ToByte( strHexString.Substring( i, 2 ), 16 );
            }
            return buffer;
        }
    }
}
