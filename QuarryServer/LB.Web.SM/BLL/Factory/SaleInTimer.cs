using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LB.Web.SM.BLL.Factory
{
    public class SaleInTimer
    {
        public static void SaleCarInBillCancel()
        {
            //查询所有未生成出场记录的单据
            string strSQL = @"
                    declare @SysSaleReceiveOverdue int
                    declare @SysSaleReceiveOverdueStr varchar(100)
                    select @SysSaleReceiveOverdueStr = case when isnull(v.SysConfigValue,'')='' then f.SysConfigDefaultValue else isnull(v.SysConfigValue,'') end
                    from dbo.DbSysConfigField f     
                        left outer join dbo.DbSysConfigValue v on
                            f.SysConfigFieldName = v.SysConfigFieldName
                    where f.SysConfigFieldName = 'SysSaleReceiveOverdue'

                    if isnumeric(@SysSaleReceiveOverdueStr)=1
                    begin
                        set @SysSaleReceiveOverdue = cast(@SysSaleReceiveOverdueStr as int)
                    end

                    update i
                    set IsCancel = 1,
                        CancelBy='系统自动作废',
                        CancelDesc='系统(超时订单系统自动作废)',
                        CancelTime=GETDATE()
                    from dbo.SaleCarInBill i
                    where i.SaleCarInBillID not in (select SaleCarInBillID from dbo.SaleCarOutBill) and
                        isnull(IsCancel,0) = 0 and
                        DATEADD(mi, @SysSaleReceiveOverdue,isnull(CancelByDate,BillDate))<= GETDATE()";

            FactoryArgs args = new FactoryArgs(DBHelper.DBName, "系统",0, false, null, null);
            string strConn = GetConnectionStr();
            DBHelper.Provider = new DBMSSQL();
            SQLServerDAL.GetConnectionString = strConn;
            SqlConnection con = new SqlConnection(SQLServerDAL.GetConnectionString);
            string strDBName = con.Database;
            DBMSSQL.InitSettings(5000, con.DataSource, strDBName, true, "", "");

            DBHelper.ExecuteQueryUnCommitted(args, strSQL);
        }

        public static string GetConnectionStr()
        {
            string strConn = "";
            if (!DBHelper.LoginSecure)
                strConn = "Server=" + DBHelper.DBServer + ";Database=" + DBHelper.DBName + ";Trusted_Connection=Yes;Connect Timeout=90;";
            else
                strConn = "Server=" + DBHelper.DBServer + ";Database=" + DBHelper.DBName + ";User ID=" + DBHelper.DBUer + ";Password=" + DBHelper.DBPw + ";Trusted_Connection=Yes;Connect Timeout=90;";
            return strConn;
        }
    }
}
