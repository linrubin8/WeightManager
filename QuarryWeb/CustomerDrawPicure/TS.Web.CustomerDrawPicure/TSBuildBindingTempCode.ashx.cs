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
    /// TSBuildBindingTempCode 的摘要说明
    /// </summary>
    public class TSBuildBindingTempCode : IHttpHandler
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
                        string strSoftWareSerialCode = Robj["SoftWareSerialCode"].ToString();
                        int iBindCodeType = 0;//绑定码生成来源：0门窗速图 1销售推广
                        if( Robj["BindCodeType"] != null )
                        {
                            int.TryParse( Robj["BindCodeType"].ToString(), out iBindCodeType );
                        }

                        int iEffectDays = 365;//绑定后产品的使用期限
                        if( Robj["EffectDays"] != null )
                        {
                            int.TryParse( Robj["EffectDays"].ToString(), out iEffectDays );
                        }

                        string strCode;

                        string strMsg = "";
                        BuildTempCode( strSoftWareSerialCode,iBindCodeType,iEffectDays, out strCode, out strMsg );
                        if( strCode != "" )
                        {
                            strReturnJson= "{\"Message\":\""+strMsg+"\",\"Code\":\""+strCode+"\"}";
                        }
                        else
                        {
                            strReturnJson= "{\"Message\":\"生成验证码失败！\",\"Code\":\"\"}";
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

        private void BuildTempCode(string strSoftWareSerialCode,int iBindCodeType,int iEffectDays,out string strCode,out string strMsg)
        {
            strCode = "";
            strMsg = "";
            DbConnection con = new SqlConnection(DbConfig.MPCmsConString4WEB);
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs("", "", con, tran);
            try
            {
                string strRandomCode = "";
                bool bolExists = true;
                do
                {
                    strRandomCode = GetRedomCode();//生成随机码
                    string strSQL = "select 1 from dbo.TSProductInfo4DrawBindTempCode where IsEffect = 1 and BindTempCode='"+strRandomCode+"'";

                    DataTable dtExists = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                    if( dtExists.Rows.Count == 0 )
                    {
                        bolExists = false;
                    }
                    //DataTable dtExists=
                } 
                while( bolExists);

                if( strRandomCode != "" )
                {
                    string strCreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                    string strSQL = @"
                        update dbo.TSProductInfo4DrawBindTempCode
                        set IsEffect = 0
                        where SoftWareSerialCode = '{0}'

                        insert dbo.TSProductInfo4DrawBindTempCode( SoftWareSerialCode, BindTempCode, IsEffect, CreateTime, BindCodeType,EffectDays) values('{0}','{1}',1,'{2}',{3},{4})";
                    strSQL = string.Format( strSQL, strSoftWareSerialCode, strRandomCode,strCreateTime,iBindCodeType,iEffectDays );
                    DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strSQL, true, null );
                }
                strCode = strRandomCode;
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                strMsg = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        private string GetRedomCode()
        {
            string strCode = "";
            string strChar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,s,y,z";
            string[] aryChar = strChar.Split( ',' );

            Random rnd = new Random( Guid.NewGuid().GetHashCode() );

            for( int i = 0; i < 5; i++ )
            {
                strCode += aryChar[rnd.Next( aryChar.Length )];
            }
            return strCode;
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