using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALDbDescription
    {
        public void Insert(FactoryArgs args,
           out t_BigID DescriptionID, t_String Description)
        {
            DescriptionID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("DescriptionID", DescriptionID, true));
            parms.Add(new LBDbParameter("Description", Description));
            parms.Add(new LBDbParameter("ChangedBy", new t_String(args.LoginName)));

            string strSQL = @"
insert into dbo.DbDescription( Description,ChangedBy, ChangeTime)
values( @Description, @ChangedBy, getdate())

set @DescriptionID = @@identity

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            DescriptionID.SetValueWithObject(parms["DescriptionID"].Value);
        }

        public void Update(FactoryArgs args,
          t_BigID DescriptionID, t_String Description)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("DescriptionID", DescriptionID));
            parms.Add(new LBDbParameter("Description", Description));
            parms.Add(new LBDbParameter("ChangedBy", new t_String(args.LoginName)));

            string strSQL = @"
update dbo.DbDescription
set Description=@Description,   
    ChangedBy=@ChangedBy,  
    ChangeTime=getdate()
where DescriptionID = @DescriptionID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Delete(FactoryArgs args,
          t_BigID DescriptionID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("DescriptionID", DescriptionID));

            string strSQL = @"
delete dbo.DbDescription
where DescriptionID = @DescriptionID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable VerifyExists(FactoryArgs args, t_String Description)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("Description", Description));

            string strSQL = @"
                        select * 
                        from dbo.DbDescription
                        where Description=@Description";
            return DBHelper.ExecuteQuery(args, strSQL,parms);
        }
    }
}
