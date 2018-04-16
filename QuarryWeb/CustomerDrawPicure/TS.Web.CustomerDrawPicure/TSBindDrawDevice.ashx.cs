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
    /// TSBindDrawDevice 的摘要说明
    /// </summary>
    public class TSBindDrawDevice : IHttpHandler
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
                        string strCheckCode = "";
                        if( Robj["CheckCode"] != null )
                        {
                            strCheckCode = Robj["CheckCode"].ToString();
                        }
                        string strDeviceType = Robj["DeviceType"].ToString();//设备类型： 0苹果 1安卓
                        string strBindActionType = Robj["BindActionType"].ToString();//操作类型：0绑定PC画图  1解除PC画图的绑定

                        string strMsg = "";

                        bool bolPass;

                        if( strBindActionType.Equals( "0" ) )
                        {
                            BuildTempCode( strCheckCode, strDeviceSerialCode, strDeviceType, out strMsg, out bolPass );
                        }
                        else
                        {
                            UnBuildTempCode( strDeviceSerialCode, strDeviceType, out strMsg, out bolPass );
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

        private void BuildTempCode(string strCheckCode,string strDeviceSerialCode,string strDeviceType,
            out string strMsg,out bool bolPass)
        {
            bolPass = false;
            strMsg = "";
            DbConnection con = new SqlConnection(DbConfig.MPCmsConString4WEB);
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs("", "", con, tran);
            try
            {
                string strSQL = "select * from dbo.TSProductInfo4DrawBindTempCode where IsEffect = 1 and BindTempCode='{0}' order by DrawBindTempCodeID desc";
                strSQL = string.Format( strSQL, strCheckCode.ToLower() );
                //判断校验码是否正确
                DataTable dtExists = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                //校验码存在并且生效
                if( dtExists.Rows.Count > 0 )
                {
                    DataRow drExists = dtExists.Rows[0];
                    string strSoftWareSerialCode = dtExists.Rows[0]["SoftWareSerialCode"].ToString().TrimEnd();
                    //临时绑定码绑定后有效的天数
                    int iEffectDays;
                    int.TryParse( drExists["EffectDays"].ToString(), out iEffectDays );
                    if( iEffectDays == 0 )
                    {
                        iEffectDays = 365;
                    }
                    int iBindCodeType;//绑定码来源：0门窗速图生成 1销售部生成
                    int.TryParse( drExists["BindCodeType"].ToString(), out iBindCodeType );

                    //判断该产品序列号是否已绑定过其他设备
                    strSQL = "select * from dbo.TSProductInfo4DrawBinding where SoftWareSerialCode='{0}' and IsBinding=1 and DeviceType={1}";
                    strSQL = string.Format( strSQL, strSoftWareSerialCode, strDeviceType );
                    DataTable dtBinding = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                    if( dtBinding.Rows.Count > 0 )
                    {
                        strMsg = "该产品的绑定设备数已超出上限，绑定失败！";
                    }
                    else
                    {
                        if( iBindCodeType == 0 )//门窗速图生成
                        {

                            //判断该产品是否已超出使用期限
                            strSQL = @"select DeadLine
                                        from dbo.TSProductFunctionReg r
	                                        inner join dbo.TSProductFunctionList l on
		                                        l.TSSoftWareProductID = r.TSSoftWareProductID and
		                                        l.SoftWareType = 8 and
		                                        l.FunctionCode = 'Base'
                                        where SoftWareSerialCode = '{0}'";
                            strSQL = string.Format( strSQL, strSoftWareSerialCode );
                            DataTable dtDeadLine = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                            if( dtDeadLine.Rows.Count == 0 )
                            {
                                strMsg = "当前门窗速图未注册，无法绑定并使用！";
                            }
                            else
                            {
                                DataRow dr = dtDeadLine.Rows[0];
                                DateTime dtDate = Convert.ToDateTime( dr["DeadLine"] );
                                if( dtDate.Subtract( DateTime.Now ).TotalHours > 0 )//未过期
                                {
                                    string strBindingDeadLine = DateTime.Now.AddDays( iEffectDays ).ToString( "yyyy-MM-dd HH:mm" );//绑定有效使用期限
                                    //校验通过,将设备号以及产品序列号进行绑定
                                    strSQL = @"
                                        insert dbo.TSProductInfo4DrawBinding( SoftWareSerialCode, DeviceSerialCode, IsBinding, 
                                                    BindingTime, UnBindingTime, DeviceType,DeadLine) 
                                        values('{0}','{1}',1,getdate(),null,{2},'{4}')

                                        update TSProductInfo4DrawBindTempCode set IsEffect = 0 where BindTempCode='{3}'";
                                    strSQL = string.Format( strSQL, strSoftWareSerialCode, strDeviceSerialCode, strDeviceType, strCheckCode.ToLower(), strBindingDeadLine );
                                    DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strSQL, true, null );

                                    bolPass = true;
                                }
                                else
                                {
                                    strMsg = "当前门窗速图已过期，无法绑定并使用！";
                                }
                            }
                        }
                        else
                        {
                            string strBindingDeadLine = DateTime.Now.AddDays( iEffectDays ).ToString( "yyyy-MM-dd HH:mm" );//绑定有效使用期限
                            //校验通过,将设备号以及产品序列号进行绑定
                            strSQL = @"
                                        insert dbo.TSProductInfo4DrawBinding( SoftWareSerialCode, DeviceSerialCode, IsBinding, 
                                                    BindingTime, UnBindingTime, DeviceType,DeadLine) 
                                        values('{0}','{1}',1,getdate(),null,{2},'{4}')

                                        update TSProductInfo4DrawBindTempCode set IsEffect = 0 where BindTempCode='{3}'";
                            strSQL = string.Format( strSQL, strSoftWareSerialCode, strDeviceSerialCode, strDeviceType, strCheckCode.ToLower(), strBindingDeadLine );
                            DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strSQL, true, null );

                            bolPass = true;
                        }
                    }
                }
                else
                {
                    strMsg = "该绑定校验码不存在或者已过期，请在门窗速图重新生成！";
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

        private void UnBuildTempCode(string strDeviceSerialCode,string strDeviceType,
            out string strMsg,out bool bolPass)
        {
            bolPass = false;
            strMsg = "";
            DbConnection con = new SqlConnection(DbConfig.MPCmsConString4WEB);
            con.Open();
            DbTransaction tran = con.BeginTransaction();
            DALExecuteArgs args = new DALExecuteArgs("", "", con, tran);
            try
            {
                //判断该产品序列号是否已绑定过其他设备
                string strSQL = "select * from dbo.TSProductInfo4DrawBinding where IsBinding=1 and DeviceSerialCode='{0}' and DeviceType={1}";
                strSQL = string.Format( strSQL, strDeviceSerialCode, strDeviceType );
                DataTable dtBinding = DBHelperSQL.ExecuteQuery( args, strSQL, null );
                if( dtBinding.Rows.Count > 0 )
                {
                    //解除绑定
                    strSQL = @"
                                    update dbo.TSProductInfo4DrawBinding
                                    set IsBinding = 0,
                                        UnBindingTime = getdate()
                                    where   IsBinding = 1 and 
                                            DeviceSerialCode='{0}' and 
                                            DeviceType={1}";
                    strSQL = string.Format( strSQL, strDeviceSerialCode, strDeviceType );
                    DBHelperSQL.ExecuteNonQuery( args, CommandType.Text, strSQL, true, null );

                    bolPass = true;
                }
                else
                {
                    //strMsg = "该产品未绑定任何设备，无法解除绑定！";
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}