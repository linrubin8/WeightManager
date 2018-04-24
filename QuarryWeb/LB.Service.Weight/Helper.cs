using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TS.Web
{
    public class Helper
    {
        public static int ComputeGroup()
        {
            int group = 0;

            if( !Directory.Exists( HttpRuntime.AppDomainAppPath + "\\MaxDrawFiles" ) )
            {
                Directory.CreateDirectory( HttpRuntime.AppDomainAppPath + "\\MaxDrawFiles" );
            }

            string[] groupsCount = Directory.GetDirectories( HttpRuntime.AppDomainAppPath + "\\MaxDrawFiles" );

            if( groupsCount.Length > 0 )
            {
                string[] filesCount = Directory.GetFiles( groupsCount[groupsCount.Length - 1] );

                if( filesCount.Length >= 100 )  //每组里文件超过100个时自动创建新组
                {
                    string[] dirCount = Directory.GetDirectories( HttpRuntime.AppDomainAppPath + "\\MaxDrawFiles" );
                    int.TryParse( dirCount[dirCount.Length - 1], out group );
                    group += 1;
                }
                else
                {
                    string[] dirCount = Directory.GetDirectories( HttpRuntime.AppDomainAppPath + "\\MaxDrawFiles" );
                    int.TryParse( dirCount[dirCount.Length - 1], out group );
                }
            }
            else
            {
                group = 0;
            }
            return group;
        }

        public string IfSameFileNameAutoRename( string FileName )
        {
            string suffix = "";
            string filePath = "";
            FileInfo fi = new FileInfo( FileName );
            suffix = fi.Extension;
            filePath = fi.Directory.ToString();
            string onlyName = Path.GetFileNameWithoutExtension( FileName );
            string[] files = Directory.GetFiles( filePath );
            List<string> sameNameFiles = new List<string>();
            for( int i = 0; i < files.Length; i++ )
            {
                if( files[i].Contains( onlyName ) )
                {
                    sameNameFiles.Add( files[i] );
                }
            }
            if( sameNameFiles.Count > 0 )
            {
                FileName = onlyName + "(" + ( sameNameFiles.Count + 1 ).ToString() + ")" + suffix;
            }
            else
            {
                FileName = onlyName + suffix;
            }
            return FileName;
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

        public static string BytesToString( byte[] bytes )
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

        public static byte[] StringToBytes( string strHexString )
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