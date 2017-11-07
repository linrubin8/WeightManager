using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Text;


namespace LB.Web.DB.DAL
{
    public class DALDbSystemConst
    {
        public void GetConstValue(FactoryArgs args, t_String FieldName,
            t_String ConstValue, out t_String ConstText)
        {
            ConstText = new t_String();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("FieldName", FieldName));
            parms.Add(new LBDbParameter("ConstValue", ConstValue));
            parms.Add(new LBDbParameter("ConstText", ConstText,true));
            string strSQL = @"
                select @ConstText = ConstText
                from dbo.DbSystemConst
                where FieldName=@FieldName and ConstValue = @ConstValue
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ConstText.SetValueWithObject(parms["ConstText"].Value);
        }
    }
}
