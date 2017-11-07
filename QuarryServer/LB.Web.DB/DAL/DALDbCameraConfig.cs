using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALDbCameraConfig
    {
        public void Insert(FactoryArgs args,
           out t_BigID CameraConfigID, t_String MachineName,
           t_String IPAddress1, t_ID Port1, t_String Account1, t_String Password1, t_Bool UseCamera1,
           t_String IPAddress2, t_ID Port2, t_String Account2, t_String Password2, t_Bool UseCamera2,
           t_String IPAddress3, t_ID Port3, t_String Account3, t_String Password3, t_Bool UseCamera3,
           t_String IPAddress4, t_ID Port4, t_String Account4, t_String Password4, t_Bool UseCamera4)
        {
            CameraConfigID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CameraConfigID", CameraConfigID, true));
            parms.Add(new LBDbParameter("MachineName", MachineName));
            parms.Add(new LBDbParameter("IPAddress1", IPAddress1));
            parms.Add(new LBDbParameter("Port1", Port1));
            parms.Add(new LBDbParameter("Account1", Account1));
            parms.Add(new LBDbParameter("Password1", Password1));
            parms.Add(new LBDbParameter("IPAddress2", IPAddress2));
            parms.Add(new LBDbParameter("Port2", Port2));
            parms.Add(new LBDbParameter("Account2", Account2));
            parms.Add(new LBDbParameter("Password2", Password2));
            parms.Add(new LBDbParameter("IPAddress3", IPAddress3));
            parms.Add(new LBDbParameter("Port3", Port3));
            parms.Add(new LBDbParameter("Account3", Account3));
            parms.Add(new LBDbParameter("Password3", Password3));
            parms.Add(new LBDbParameter("IPAddress4", IPAddress4));
            parms.Add(new LBDbParameter("Port4", Port4));
            parms.Add(new LBDbParameter("Account4", Account4));
            parms.Add(new LBDbParameter("Password4", Password4));
            parms.Add(new LBDbParameter("ChangedBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("UseCamera1", UseCamera1));
            parms.Add(new LBDbParameter("UseCamera2", UseCamera2));
            parms.Add(new LBDbParameter("UseCamera3", UseCamera3));
            parms.Add(new LBDbParameter("UseCamera4", UseCamera4));

            string strSQL = @"
insert into dbo.DbCameraConfig( MachineName, IPAddress1, Port1, Account1, Password1, 
IPAddress2, Port2, Account2, Password2, IPAddress3, Port3, Account3, Password3, 
IPAddress4, Port4, Account4, Password4, ChangedBy, ChangeTime,
UseCamera1,UseCamera2,UseCamera3,UseCamera4)
values( @MachineName, @IPAddress1, @Port1, @Account1, @Password1, 
@IPAddress2, @Port2, @Account2, @Password2, @IPAddress3, @Port3, @Account3, @Password3, 
@IPAddress4, @Port4, @Account4, @Password4, @ChangedBy, getdate(),
@UseCamera1,@UseCamera2,@UseCamera3,@UseCamera4)

set @CameraConfigID = @@identity

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            CameraConfigID.SetValueWithObject(parms["CameraConfigID"].Value);
        }

        public void Update(FactoryArgs args,
          t_BigID CameraConfigID, t_String MachineName,
          t_String IPAddress1, t_ID Port1, t_String Account1, t_String Password1, t_Bool UseCamera1,
          t_String IPAddress2, t_ID Port2, t_String Account2, t_String Password2, t_Bool UseCamera2,
          t_String IPAddress3, t_ID Port3, t_String Account3, t_String Password3, t_Bool UseCamera3,
          t_String IPAddress4, t_ID Port4, t_String Account4, t_String Password4, t_Bool UseCamera4)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("CameraConfigID", CameraConfigID));
            parms.Add(new LBDbParameter("MachineName", MachineName));
            parms.Add(new LBDbParameter("IPAddress1", IPAddress1));
            parms.Add(new LBDbParameter("Port1", Port1));
            parms.Add(new LBDbParameter("Account1", Account1));
            parms.Add(new LBDbParameter("Password1", Password1));
            parms.Add(new LBDbParameter("IPAddress2", IPAddress2));
            parms.Add(new LBDbParameter("Port2", Port2));
            parms.Add(new LBDbParameter("Account2", Account2));
            parms.Add(new LBDbParameter("Password2", Password2));
            parms.Add(new LBDbParameter("IPAddress3", IPAddress3));
            parms.Add(new LBDbParameter("Port3", Port3));
            parms.Add(new LBDbParameter("Account3", Account3));
            parms.Add(new LBDbParameter("Password3", Password3));
            parms.Add(new LBDbParameter("IPAddress4", IPAddress4));
            parms.Add(new LBDbParameter("Port4", Port4));
            parms.Add(new LBDbParameter("Account4", Account4));
            parms.Add(new LBDbParameter("Password4", Password4));
            parms.Add(new LBDbParameter("ChangedBy", new t_String(args.LoginName)));
            parms.Add(new LBDbParameter("UseCamera1", UseCamera1));
            parms.Add(new LBDbParameter("UseCamera2", UseCamera2));
            parms.Add(new LBDbParameter("UseCamera3", UseCamera3));
            parms.Add(new LBDbParameter("UseCamera4", UseCamera4));

            string strSQL = @"
update dbo.DbCameraConfig
set MachineName=@MachineName, 
    IPAddress1=@IPAddress1,  
    Port1=@Port1,  
    Account1=@Account1,  
    Password1=@Password1, 
    IPAddress2=@IPAddress2,  
    Port2=@Port2,  
    Account2=@Account2,  
    Password2=@Password2,  
    IPAddress3=@IPAddress3,  
    Port3=@Port3,  
    Account3=@Account3,  
    Password3=@Password3, 
    IPAddress4=@IPAddress4,  
    Port4=@Port4,  
    Account4=@Account4,  
    Password4=@Password4,  
    ChangedBy=@ChangedBy,  
    ChangeTime=getdate(),
    UseCamera1 = @UseCamera1,
    UseCamera2 = @UseCamera2,
    UseCamera3 = @UseCamera3,
    UseCamera4 = @UseCamera4
    
where CameraConfigID = @CameraConfigID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable VerifyExists(FactoryArgs args, t_String MachineName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("MachineName", MachineName));

            string strSQL = @"
                        select * 
                        from dbo.DbCameraConfig
                        where MachineName=@MachineName";
            return DBHelper.ExecuteQuery(args, strSQL,parms);
        }
    }
}
