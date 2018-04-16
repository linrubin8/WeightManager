using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TS.Web
{
    /// <summary>
    /// TestDeleteFile 的摘要说明
    /// </summary>
    public class TestDeleteFile : IHttpHandler
    {

        public void ProcessRequest( HttpContext context )
        {
            string strDrawPicture = Path.Combine( HttpRuntime.AppDomainAppPath, "CustomerPicture" );

            context.Response.ContentType = "application/json";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            using( var reader = new System.IO.StreamReader( context.Request.InputStream ) )
            {
                string strFile = Path.Combine( strDrawPicture, "test.jpg" );
                File.Delete(strFile);
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