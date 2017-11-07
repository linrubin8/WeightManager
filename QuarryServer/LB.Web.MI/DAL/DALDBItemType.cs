using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace LB.Web.MI.DAL
{
    public class DALDBItemType
    {
        public void Insert(FactoryArgs args, out t_BigID ItemTypeID, t_String ItemTypeName)
        {
            ItemTypeID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ItemTypeID",  ItemTypeID,true ));
            parms.Add(new LBDbParameter("ItemTypeName",  ItemTypeName));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
insert into dbo.DbItemType( ItemTypeName, ChangeBy, ChangeTime)
values( @ItemTypeName, @ChangeBy, @ChangeTime)

set @ItemTypeID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ItemTypeID.Value = Convert.ToInt64(parms["ItemTypeID"].Value);
        }

        public void Update(FactoryArgs args, t_BigID ItemTypeID, t_String ItemTypeName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ItemTypeID", ItemTypeID));
            parms.Add(new LBDbParameter("ItemTypeName", ItemTypeName));
            parms.Add(new LBDbParameter("ChangeBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("ChangeTime", new t_DTSmall(DateTime.Now)));

            string strSQL = @"
update dbo.DbItemType
set ItemTypeName=@ItemTypeName,
    ChangeBy=@ChangeBy,
    ChangeTime=@ChangeTime 
where ItemTypeID = @ItemTypeID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Delete(FactoryArgs args, t_BigID ItemTypeID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ItemTypeID", ItemTypeID));

            string strSQL = @"
delete dbo.DbItemType
where ItemTypeID = @ItemTypeID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }
    }
}