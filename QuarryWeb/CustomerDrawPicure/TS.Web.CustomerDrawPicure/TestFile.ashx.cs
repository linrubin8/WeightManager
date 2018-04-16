using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TS.Web
{
    /// <summary>
    /// TestFile 的摘要说明
    /// </summary>
    public class TestFile : IHttpHandler
    {

        public void ProcessRequest( HttpContext context )
        {
            string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );

            context.Response.ContentType = "application/json";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            using( var reader = new System.IO.StreamReader( context.Request.InputStream ) )
            {
                string xmlData = reader.ReadToEnd();
                if( xmlData != "0" )
                {
                    string strFile = Path.Combine( strDrawPicture, "test.jpg" );

                    byte[] buffer = GetBytes(xmlData);
                    using( FileStream fs = new FileStream( strFile, FileMode.Create ) )
                    {
                        fs.Write( buffer, 0, buffer.Length );
                        fs.Flush();
                        fs.Close();
                        fs.Dispose();
                    }
                }
                else
                {
                    string strFile = Path.Combine( strDrawPicture, "test.jpg" );
                    File.Delete(strFile);
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}