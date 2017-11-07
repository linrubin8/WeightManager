using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALDbReportView
    {
        public void Insert(FactoryArgs args, out t_BigID ReportViewID,
            t_String ReportViewName, t_nText ReportDataSource)
        {
            ReportViewID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportViewID", ReportViewID,true));
            parms.Add(new LBDbParameter("ReportViewName", ReportViewName));
            parms.Add(new LBDbParameter("ReportDataSource", ReportDataSource));
            string strSQL = @"
insert dbo.DbReportView( ReportViewName,ReportDataSource)
values(@ReportViewName,@ReportDataSource)

set @ReportViewID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ReportViewID.SetValueWithObject(parms["ReportViewID"].Value);
        }

        public void Update(FactoryArgs args, t_BigID ReportViewID,
            t_String ReportViewName, t_nText ReportDataSource)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportViewID", ReportViewID));
            parms.Add(new LBDbParameter("ReportViewName", ReportViewName));
            parms.Add(new LBDbParameter("ReportDataSource", ReportDataSource));
            string strSQL = @"
update dbo.DbReportView
set ReportViewName=@ReportViewName,
    ReportDataSource=@ReportDataSource
where ReportViewID = @ReportViewID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Delete(FactoryArgs args, t_BigID ReportViewID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportViewID", ReportViewID));
            string strSQL = @"
delete dbo.DbReportViewField
where ReportViewID = @ReportViewID

delete dbo.DbReportView
where ReportViewID = @ReportViewID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void InsertField(FactoryArgs args, out t_BigID ReportViewFieldID, t_BigID ReportViewID,
            t_String FieldName, t_ID FieldType,t_String FieldText, t_String FieldFormat)
        {
            ReportViewFieldID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportViewFieldID", ReportViewFieldID, true));
            parms.Add(new LBDbParameter("ReportViewID", ReportViewID));
            parms.Add(new LBDbParameter("FieldName", FieldName));
            parms.Add(new LBDbParameter("FieldType", FieldType));
            parms.Add(new LBDbParameter("FieldText", FieldText));
            parms.Add(new LBDbParameter("FieldFormat", FieldFormat));
            string strSQL = @"
insert dbo.DbReportViewField( ReportViewID,FieldName,FieldType,FieldText,FieldFormat)
values(@ReportViewID,@FieldName,@FieldType,@FieldText,@FieldFormat)

set @ReportViewFieldID = @@identity
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ReportViewFieldID.SetValueWithObject(parms["ReportViewFieldID"].Value);
        }

        public void UpdateField(FactoryArgs args, t_BigID ReportViewFieldID,
            t_String FieldName, t_ID FieldType, t_String FieldText, t_String FieldFormat)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportViewFieldID", ReportViewFieldID));
            parms.Add(new LBDbParameter("FieldName", FieldName));
            parms.Add(new LBDbParameter("FieldType", FieldType));
            parms.Add(new LBDbParameter("FieldText", FieldText));
            parms.Add(new LBDbParameter("FieldFormat", FieldFormat));
            string strSQL = @"
update dbo.DbReportViewField
set FieldName=@FieldName,
    FieldType=@FieldType,
    FieldText = @FieldText,
    FieldFormat = @FieldFormat
where ReportViewFieldID = @ReportViewFieldID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void DeleteField(FactoryArgs args, t_BigID ReportViewFieldID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportViewFieldID", ReportViewFieldID));
            string strSQL = @"
delete dbo.DbReportViewField
where ReportViewFieldID = @ReportViewFieldID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void SaveReportTemplate(FactoryArgs args, t_BigID ReportViewID, t_BigID ReportTemplateID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportViewID", ReportViewID));
            parms.Add(new LBDbParameter("ReportTemplateID", ReportTemplateID));
            string strSQL = @"
update dbo.DbReportView
set ReportTemplateID=@ReportTemplateID
where ReportViewID = @ReportViewID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void GetReportView(FactoryArgs args, t_BigID ReportViewID,out t_String ReportDataSource)
        {
            ReportDataSource = new t_String();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportViewID", ReportViewID));
            parms.Add(new LBDbParameter("ReportDataSource", ReportDataSource,true));
            string strSQL = @"
select @ReportDataSource = ReportDataSource
from dbo.DbReportView
where ReportViewID = @ReportViewID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void GetReportViewDataSource(FactoryArgs args, t_BigID ReportViewID, t_Table DTFieldValue)
        {
            DataView dvFieldValue =new DataView( DTFieldValue.Value );
            t_String ReportDataSource = new t_String();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportViewID", ReportViewID));
            parms.Add(new LBDbParameter("ReportDataSource", ReportDataSource, true));
            string strSQL = @"
select @ReportDataSource = ReportDataSource
from dbo.DbReportView
where ReportViewID = @ReportViewID
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ReportDataSource.SetValueWithObject(parms["ReportDataSource"].Value);

            DataTable dtField = this.GetReportViewField(args, ReportViewID);
            parms = new LBDbParameterCollection();
            foreach(DataRow drField in dtField.Rows)
            {
                int iFieldType = drField["FieldType"] == DBNull.Value ? 
                    0 : Convert.ToInt32(drField["FieldType"]);
                string strFieldName = drField["FieldName"].ToString().TrimEnd();
                string strValue = "";
                dvFieldValue.RowFilter = "FieldName='"+ strFieldName + "'";
                if (dvFieldValue.Count > 0)
                {
                    strValue = dvFieldValue[0]["FieldValue"].ToString();
                }

                if (iFieldType == 0)
                {
                    parms.Add(new LBDbParameter(strFieldName, new t_String(strValue)));
                }
                else if (iFieldType == 1)
                {
                    decimal decValue;
                    decimal.TryParse(strValue, out decValue);
                    parms.Add(new LBDbParameter(strFieldName, new t_Decimal(decValue)));
                }
                else if (iFieldType == 2)
                {
                    DateTime dtValue;
                    DateTime.TryParse(strValue, out dtValue);
                    if(strValue=="")
                        parms.Add(new LBDbParameter(strFieldName, new t_DTSmall()));
                    else
                        parms.Add(new LBDbParameter(strFieldName, new t_DTSmall(dtValue)));
                }
                else
                {
                    parms.Add(new LBDbParameter(strFieldName, new t_String(strValue)));
                }
            }
            string strSQLSource = ReportDataSource.Value.ToString();
            args.SelectResult = DBHelper.ExecuteQuery(args, strSQLSource, parms);
        }

        public DataTable GetReportViewField(FactoryArgs args, t_BigID ReportViewID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportViewID", ReportViewID));
            string strSQL = @"
select *
from dbo.DbReportViewField
where ReportViewID = @ReportViewID
";
           return DBHelper.ExecuteQuery(args, strSQL, parms);
        }
    }
}
