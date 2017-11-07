using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALUserPermission
    {
        public DataTable GetUserPermissionData(FactoryArgs args, t_BigID UserID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("UserID", UserID));
            string strSQL = @"
select p.PermissionID,p.PermissionDataID,p.DetailIndex,p.Forbid,p.PermissionDataName, d.*
from dbo.DbPermissionData p
	left outer join dbo.DbUserPermissionData d on
		p.PermissionDataID = d.PermissionDataID and
        UserID = @UserID
";
            DataTable dtReturn = DBHelper.ExecuteQuery(args, strSQL, parms);
            return dtReturn;
        }

        public DataTable GetUserPermission(FactoryArgs args, t_BigID UserID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("UserID", UserID));
            string strSQL = @"
select p.PermissionID,p.Forbid, d.*
from dbo.DbPermission p
	left outer join dbo.DbUserPermission d on
		p.PermissionID = d.PermissionID and
        UserID = @UserID
";
            DataTable dtReturn = DBHelper.ExecuteQuery(args, strSQL, parms);
            return dtReturn;
        }

        public void DeleteUserPermission(FactoryArgs args, t_BigID UserID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("UserID", UserID));
            string strSQL = @"
delete dbo.DbUserPermission
where UserID=@UserID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void DeleteUserPermissionData(FactoryArgs args, t_BigID UserID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("UserID", UserID));
            string strSQL = @"
delete dbo.DbUserPermissionData
where UserID=@UserID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void InsertUserPermissionData(FactoryArgs args, t_BigID UserID,t_BigID PermissionDataID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("UserID", UserID));
            parms.Add(new LBDbParameter("PermissionDataID", PermissionDataID));
            parms.Add(new LBDbParameter("ChangedBy", new t_String(args.LoginName)));
            string strSQL = @"
insert dbo.DbUserPermissionData(UserID, PermissionDataID, ChangedBy, ChangeTime, HasPermission)
values(@UserID, @PermissionDataID, @ChangedBy, getdate(), 1)
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void InsertUserPermission(FactoryArgs args, t_BigID UserID, t_BigID PermissionID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("UserID", UserID));
            parms.Add(new LBDbParameter("PermissionID", PermissionID));
            parms.Add(new LBDbParameter("ChangedBy", new t_String(args.LoginName)));
            string strSQL = @"
insert dbo.DbUserPermission(UserID, PermissionID, ChangedBy, ChangeTime, HasPermission)
values(@UserID, @PermissionID, @ChangedBy, getdate(), 1)
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }
    }
}
