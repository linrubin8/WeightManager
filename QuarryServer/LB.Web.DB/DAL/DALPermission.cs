using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace LB.Web.DB.DAL
{
    public class DALPermission
    {
        public void GetUserPermission(FactoryArgs args, t_BigID UserID, t_String PermissionCode,
            out t_String PermissionDataName, out t_String PermissionName, out t_Bool HasPermission)
        {
            PermissionDataName = new t_String();
            PermissionName = new t_String();
            HasPermission = new t_Bool(1);
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("UserID", UserID));
            parms.Add(new LBDbParameter("PermissionCode",  PermissionCode));
            parms.Add(new LBDbParameter("PermissionDataName", PermissionDataName, true));
            parms.Add(new LBDbParameter("PermissionName",  PermissionName, true));
            parms.Add(new LBDbParameter("HasPermission", HasPermission, true));
            string strSQL = @"
declare @PermissionID   bigint
declare @PermissionDataID   bigint

set @HasPermission = 0

select top 1 @PermissionID = d.PermissionID,@PermissionDataName = d.PermissionDataName,
    @PermissionName = p.PermissionName,@PermissionDataID = d.PermissionDataID
from dbo.DbPermissionData d
    inner join dbo.DbPermission p on
       p.PermissionID = d.PermissionID
where PermissionCode= @PermissionCode

select @HasPermission = isnull(HasPermission,0)
from dbo.DbUserPermission p
where PermissionID = @PermissionID and UserID = @UserID

if @HasPermission = 0
begin
    select @HasPermission = isnull(HasPermission,0)
    from dbo.DbUserPermissionData
    where PermissionDataID=@PermissionDataID and UserID = @UserID
end

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            PermissionDataName.SetValueWithObject(parms["PermissionDataName"].Value);
            PermissionName.SetValueWithObject(parms["PermissionName"].Value);
            HasPermission.SetValueWithObject(Convert.ToByte( parms["HasPermission"].Value ));
        }

        /// <summary>
        /// 校验是否超级管理员
        /// </summary>
        /// <param name="args"></param>
        /// <param name="UserID"></param>
        public bool VerifyIsAdmin(FactoryArgs args, t_BigID UserID)
        {
            bool bolIsAdmin = false;
            int UserType=0;
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("UserID",  UserID));
            parms.Add(new LBDbParameter("UserType",new t_ID( UserType),true));
            string strSQL = @"
select @UserType = UserType
from dbo.DbUser
where UserID=@UserID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            UserType = Convert.ToInt32(parms["UserType"].Value);
            if (UserType == 2)
            {
                bolIsAdmin = true;
            }
            return bolIsAdmin;
        }

        public DataTable GetPermission(FactoryArgs args, t_BigID PermissionID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("PermissionID", PermissionID));
            string strSQL = @"
select *
from dbo.DbPermission
where PermissionID=@PermissionID
";
            DataTable dtReturn = DBHelper.ExecuteQuery(args, strSQL, parms);
            return dtReturn;
        }

        public DataTable GetPermissionByName(FactoryArgs args,t_BigID ParentPermissionID, t_String PermissionName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ParentPermissionID", ParentPermissionID));
            parms.Add(new LBDbParameter("PermissionName", PermissionName));
            string strSQL = @"
select *
from dbo.DbPermission
where PermissionName=@PermissionName and ParentPermissionID = @ParentPermissionID
";
            DataTable dtReturn = DBHelper.ExecuteQuery(args, strSQL, parms);
            return dtReturn;
        }

        public DataTable GetChildPermission(FactoryArgs args, t_BigID ParentPermissionID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ParentPermissionID", ParentPermissionID));
            string strSQL = @"
select *
from dbo.DbPermission
where ParentPermissionID=@ParentPermissionID
";
            DataTable dtReturn = DBHelper.ExecuteQuery(args, strSQL, parms);
            return dtReturn;
        }

        public DataTable GetPermissionData(FactoryArgs args, t_BigID PermissionID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("PermissionID", PermissionID));
            string strSQL = @"
select *
from dbo.DbPermissionData
where PermissionID=@PermissionID
";
            DataTable dtReturn = DBHelper.ExecuteQuery(args, strSQL, parms);
            return dtReturn;
        }

        public DataTable GetPermissionDataByCode(FactoryArgs args, t_String PermissionCode)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("PermissionCode",PermissionCode));
            string strSQL = @"
select *
from dbo.DbPermissionData
where rtrim(PermissionCode)=rtrim(@PermissionCode)
";
            DataTable dtReturn = DBHelper.ExecuteQuery(args, strSQL, parms);
            return dtReturn;
        }

        public void InsertPermission(FactoryArgs args, out t_BigID PermissionID, t_BigID ParentPermissionID, t_String PermissionName)
        {
            PermissionID = new t_BigID();
            ParentPermissionID.NullIfZero();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("PermissionID", PermissionID, true));
            parms.Add(new LBDbParameter("PermissionName", PermissionName));
            parms.Add(new LBDbParameter("ParentPermissionID", ParentPermissionID));
            string strSQL = @"
insert into dbo.DbPermission( PermissionName, ParentPermissionID)
values( @PermissionName, @ParentPermissionID)

set @PermissionID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            PermissionID.SetValueWithObject( parms["PermissionID"].Value);
        }


        public void UpdatePermission(FactoryArgs args, t_BigID PermissionID, t_String PermissionName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("PermissionID", PermissionID));
            parms.Add(new LBDbParameter("PermissionName",PermissionName));
            string strSQL = @"
update dbo.DbPermission
set PermissionName = @PermissionName
where PermissionID = @PermissionID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void DeletePermission(FactoryArgs args, t_BigID PermissionID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("PermissionID", PermissionID));
            string strSQL = @"
delete dbo.DbPermission
where PermissionID = @PermissionID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void InsertPermissionData(FactoryArgs args, out t_BigID PermissionDataID, t_BigID PermissionID,
            t_String PermissionCode, t_String PermissionDataName, t_SmallID PermissionType, t_ID PermissionSPType, 
            t_ID PermissionViewType, t_String LogFieldName, t_ID DetailIndex, t_Bool Forbid)
        {
            PermissionDataID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("PermissionDataID", PermissionDataID, true));
            parms.Add(new LBDbParameter("PermissionID", PermissionID));
            parms.Add(new LBDbParameter("PermissionCode", PermissionCode));
            parms.Add(new LBDbParameter("PermissionDataName", PermissionDataName));
            parms.Add(new LBDbParameter("PermissionType", PermissionType));
            parms.Add(new LBDbParameter("PermissionSPType", PermissionSPType));
            parms.Add(new LBDbParameter("PermissionViewType", PermissionViewType));
            parms.Add(new LBDbParameter("LogFieldName", LogFieldName));
            parms.Add(new LBDbParameter("DetailIndex", DetailIndex));
            parms.Add(new LBDbParameter("Forbid", Forbid));
            string strSQL = @"
insert into dbo.DbPermissionData(PermissionID, PermissionCode, PermissionDataName, PermissionType, 
PermissionSPType, PermissionViewType, LogFieldName,DetailIndex, Forbid)
values(@PermissionID, @PermissionCode, @PermissionDataName, @PermissionType, @PermissionSPType,
@PermissionViewType,@LogFieldName,@DetailIndex, @Forbid)

set @PermissionDataID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            PermissionDataID.SetValueWithObject(parms["PermissionDataID"].Value);
        }


        public void UpdatePermissionData(FactoryArgs args, t_BigID PermissionDataID, t_String PermissionCode, 
            t_String PermissionDataName, t_SmallID PermissionType, t_ID PermissionSPType, 
            t_ID PermissionViewType, t_String LogFieldName, t_ID DetailIndex, t_Bool Forbid)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("PermissionDataID",  PermissionDataID));
            parms.Add(new LBDbParameter("PermissionCode", PermissionCode));
            parms.Add(new LBDbParameter("PermissionDataName",  PermissionDataName));
            parms.Add(new LBDbParameter("PermissionType", PermissionType));
            parms.Add(new LBDbParameter("PermissionSPType", PermissionSPType));
            parms.Add(new LBDbParameter("PermissionViewType", PermissionViewType));
            parms.Add(new LBDbParameter("LogFieldName", LogFieldName));
            parms.Add(new LBDbParameter("DetailIndex", DetailIndex));
            parms.Add(new LBDbParameter("Forbid", Forbid));
            string strSQL = @"
update dbo.DbPermissionData
set PermissionCode = @PermissionCode,
    PermissionDataName = @PermissionDataName,
    PermissionType = @PermissionType,
    PermissionSPType = @PermissionSPType,
    LogFieldName = @LogFieldName,
    PermissionViewType = @PermissionViewType,
    DetailIndex = @DetailIndex,
    Forbid = @Forbid
where PermissionDataID = @PermissionDataID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void DeletePermissionData(FactoryArgs args, t_BigID PermissionDataID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("PermissionDataID", PermissionDataID));
            string strSQL = @"
delete dbo.DbPermissionData
where PermissionDataID = @PermissionDataID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }
    }
}