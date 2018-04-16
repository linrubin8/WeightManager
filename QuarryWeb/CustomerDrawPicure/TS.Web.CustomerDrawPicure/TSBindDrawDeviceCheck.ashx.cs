using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TS.Web.DBHelper;

namespace TS.Web
{
    /// <summary>
    /// TSBindDrawDeviceCheck 的摘要说明
    /// </summary>
    public class TSBindDrawDeviceCheck : IHttpHandler
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

                        long lTSUserID;
                        if( Robj["UserID"] != null )
                        {
                            string strUserID = Robj["UserID"].ToString();
                            long.TryParse( strUserID, out lTSUserID );
                        }
                        
                        string strDeviceSerialCode = Robj["DeviceSerialCode"].ToString();
                        string strDeviceType = Robj["DeviceType"].ToString();//设备类型： 0苹果 1安卓
                        string strLoginNameTel = "";
                        if( Robj["Tel"] != null )
                        {
                            strLoginNameTel = Robj["Tel"].ToString();
                        }

                        string strMsg = "";
                        bool bolPass;
                        if( strLoginNameTel == "13823420056" )//用户App审批时免绑定校验过程，直接通过并登陆
                        {
                            bolPass = true;
                        }
                        else
                        {
                            CheckCode( strDeviceSerialCode, strDeviceType, out strMsg, out bolPass );
                        }
                        if( strMsg != "" )
                        {
                            strReturnJson= "{\"Message\":\""+strMsg+"\",\"Pass\":\"0\"}";
                        }
                        else
                        {
                            strReturnJson= "{\"Message\":\"\",\"Pass\":\"1\"}";
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

        private void CheckCode(string strDeviceSerialCode,string strDeviceType,
            out string strMsg,out bool bolPass)
        {
            //strMsg = "";
            //bolPass = true;
            bolPass = false;
            strMsg = "";
            DbConnection con = new SqlConnection(DbConfig.MPCmsConString4WEB);
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs("", "", con, tran);
            try
            {
                string strSQL = @"
                    select top 1 * 
                    from dbo.TSProductInfo4DrawBinding 
                    where IsBinding = 1 and 
                            DeviceSerialCode='{0}' and
                            DeviceType = {1}
                    order by DeadLine desc";
                strSQL = string.Format( strSQL, strDeviceSerialCode,strDeviceType );
                //判断校验码是否正确
                DataTable dtExists = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                //校验码存在并且生效
                if( dtExists.Rows.Count > 0 )
                {
                    DataRow dr = dtExists.Rows[0];
                    if( dr["DeadLine"] != null )
                    {
                        DateTime dtDeadLine;
                        DateTime.TryParse( dr["DeadLine"].ToString(), out dtDeadLine );
                        if( dtDeadLine.Subtract( DateTime.Now ).TotalHours > 0 )
                        {
                            bolPass = true;
                        }
                        else
                        {
                            strMsg = "该产品使用已过期！";
                        }
                    }
                    else
                    {
                        bolPass = true;
                    }
                }
                else
                {
                    strMsg = "该设备未绑定门窗速图";
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


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}