using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.DAL
{
    public class DALDbReportTemplate
    {
        public DataTable ExistsTemplateName(FactoryArgs args, t_String ReportTemplateName,t_BigID ReportTypeID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportTemplateName", ReportTemplateName));
            parms.Add(new LBDbParameter("ReportTypeID", ReportTypeID));
            string strSQL = @"
select *
from dbo.DbReportTemplate
where rtrim(ReportTemplateName)=rtrim(@ReportTemplateName) and
        ReportTypeID = @ReportTypeID
";
            DataTable dtReturn = DBHelper.ExecuteQuery(args, strSQL, parms);
            return dtReturn;
        }

        public void Insert(FactoryArgs args,
           out t_BigID ReportTemplateID, t_String ReportTemplateName, t_DTSmall TemplateFileTime, t_ID TemplateSeq,
           t_String Description,t_Image TemplateData, t_BigID ReportTypeID,
            t_String PrinterName, t_String MachineName, t_Bool IsManualPaperType, t_String PaperType, t_Bool IsManualPaperSize,
            t_ID PaperSizeHeight, t_ID PaperSizeWidth, t_Bool IsPaperTransverse, t_ID PrintCount)
        {
            ReportTemplateID = new t_BigID();
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportTemplateID", ReportTemplateID, true));
            parms.Add(new LBDbParameter("ReportTemplateName",  ReportTemplateName));
            parms.Add(new LBDbParameter("TemplateFileTime", TemplateFileTime));
            parms.Add(new LBDbParameter("TemplateSeq",  TemplateSeq));
            parms.Add(new LBDbParameter("Description",  Description));
            parms.Add(new LBDbParameter("TemplateData", TemplateData));
            parms.Add(new LBDbParameter("ReportTypeID",  ReportTypeID));

            parms.Add(new LBDbParameter("PrinterName", PrinterName));
            parms.Add(new LBDbParameter("MachineName", MachineName));
            parms.Add(new LBDbParameter("IsManualPaperType", IsManualPaperType));
            parms.Add(new LBDbParameter("PaperType", PaperType));
            parms.Add(new LBDbParameter("IsManualPaperSize", IsManualPaperSize));
            parms.Add(new LBDbParameter("PaperSizeHeight", PaperSizeHeight));
            parms.Add(new LBDbParameter("PaperSizeWidth", PaperSizeWidth));
            parms.Add(new LBDbParameter("IsPaperTransverse", IsPaperTransverse));
            parms.Add(new LBDbParameter("PrintCount", PrintCount));
            string strSQL = @"
insert into dbo.DbReportTemplate( ReportTemplateName,ReportTemplateNameExt, TemplateFileTime,TemplateSeq,Description,TemplateData,ReportTypeID)
values( @ReportTemplateName,'.frx', @TemplateFileTime,@TemplateSeq,@Description,@TemplateData,@ReportTypeID)

set @ReportTemplateID = @@identity

insert dbo.DbPrinterConfig( ReportTemplateID, PrinterName, MachineName, IsManualPaperType, 
PaperType, IsManualPaperSize, PaperSizeHeight, PaperSizeWidth, IsPaperTransverse,PrintCount)
values(@ReportTemplateID, @PrinterName, @MachineName, @IsManualPaperType, 
@PaperType, @IsManualPaperSize, @PaperSizeHeight, @PaperSizeWidth, @IsPaperTransverse,@PrintCount)
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
            ReportTemplateID.SetValueWithObject(parms["ReportTemplateID"].Value);
        }

        public void Update(FactoryArgs args,
           t_BigID ReportTemplateID, t_String ReportTemplateName, t_DTSmall TemplateFileTime, t_ID TemplateSeq,
           t_String Description, t_Image TemplateData, t_BigID ReportTypeID,
            t_String PrinterName, t_String MachineName, t_Bool IsManualPaperType, t_String PaperType, t_Bool IsManualPaperSize,
            t_ID PaperSizeHeight, t_ID PaperSizeWidth, t_Bool IsPaperTransverse, t_ID PrintCount)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportTemplateID", ReportTemplateID));
            parms.Add(new LBDbParameter("ReportTemplateName", ReportTemplateName));
            parms.Add(new LBDbParameter("TemplateSeq", TemplateSeq));
            parms.Add(new LBDbParameter("Description", Description));

            parms.Add(new LBDbParameter("PrinterName", PrinterName));
            parms.Add(new LBDbParameter("MachineName", MachineName));
            parms.Add(new LBDbParameter("IsManualPaperType", IsManualPaperType));
            parms.Add(new LBDbParameter("PaperType", PaperType));
            parms.Add(new LBDbParameter("IsManualPaperSize", IsManualPaperSize));
            parms.Add(new LBDbParameter("PaperSizeHeight", PaperSizeHeight));
            parms.Add(new LBDbParameter("PaperSizeWidth", PaperSizeWidth));
            parms.Add(new LBDbParameter("IsPaperTransverse", IsPaperTransverse));
            parms.Add(new LBDbParameter("PrintCount", PrintCount));
            parms.Add(new LBDbParameter("TemplateData", TemplateData));
            parms.Add(new LBDbParameter("TemplateFileTime", TemplateFileTime));
            string strSQL = @"
update dbo.DbReportTemplate
set ReportTemplateName = @ReportTemplateName, 
    TemplateSeq=@TemplateSeq,
    Description=@Description,
    TemplateData = isnull(@TemplateData,TemplateData),
    TemplateFileTime = (case when @TemplateData is null then TemplateFileTime else @TemplateFileTime end)
where ReportTemplateID = @ReportTemplateID

if not exists(select 1 from dbo.DbPrinterConfig where ReportTemplateID = @ReportTemplateID and MachineName = @MachineName)
begin
    insert dbo.DbPrinterConfig( ReportTemplateID, PrinterName, MachineName, IsManualPaperType, 
    PaperType, IsManualPaperSize, PaperSizeHeight, PaperSizeWidth, IsPaperTransverse,PrintCount)
    values(@ReportTemplateID, @PrinterName, @MachineName, @IsManualPaperType, 
    @PaperType, @IsManualPaperSize, @PaperSizeHeight, @PaperSizeWidth, @IsPaperTransverse,@PrintCount)
end
else
begin
    update dbo.DbPrinterConfig
    set PrinterName = @PrinterName,
        IsManualPaperType = @IsManualPaperType,
        PaperType = @PaperType,
        IsManualPaperSize = @IsManualPaperSize,
        PaperSizeHeight = @PaperSizeHeight,
        PaperSizeWidth = @PaperSizeWidth,
        IsPaperTransverse = @IsPaperTransverse,
        PrintCount = @PrintCount
    where ReportTemplateID = @ReportTemplateID and MachineName = @MachineName
end
";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void UpdateReportTemplate(FactoryArgs args,
           t_BigID ReportTemplateID, t_DTSmall TemplateFileTime,t_Image TemplateData)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportTemplateID", ReportTemplateID));
            parms.Add(new LBDbParameter("TemplateFileTime", TemplateFileTime));
            parms.Add(new LBDbParameter("TemplateData", TemplateData));
            string strSQL = @"
update dbo.DbReportTemplate
set TemplateFileTime=@TemplateFileTime,
    TemplateData=isnull(@TemplateData,TemplateData)
where ReportTemplateID = @ReportTemplateID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public void Delete(FactoryArgs args,
          t_BigID ReportTemplateID)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("ReportTemplateID", ReportTemplateID));
            string strSQL = @"
delete dbo.DbPrinterConfig
where ReportTemplateID = @ReportTemplateID

delete dbo.DbReportTemplate
where ReportTemplateID = @ReportTemplateID

";
            DBHelper.ExecuteNonQuery(args, System.Data.CommandType.Text, strSQL, parms, false);
        }

        public DataTable GetReportTemplateByType(FactoryArgs args, t_BigID ReportTypeID, t_String MachineName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("MachineName", MachineName));
            parms.Add(new LBDbParameter("ReportTypeID", ReportTypeID));
            string strSQL = @"
select *
from dbo.DbReportTemplate d
    left outer join dbo.DbPrinterConfig p on
        p.ReportTemplateID = d.ReportTemplateID and
        p.MachineName = @MachineName
where d.ReportTypeID = @ReportTypeID
order by d.TemplateFileTime desc
";
            DataTable dtReturn = DBHelper.ExecuteQuery(args, strSQL, parms);
            return dtReturn;
        }

        public DataTable GetReportTemplateByID(FactoryArgs args, t_BigID ReportTemplateID, t_String MachineName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("MachineName", MachineName));
            parms.Add(new LBDbParameter("ReportTemplateID", ReportTemplateID));
            string strSQL = @"
select *
from dbo.DbReportTemplate d
    left outer join dbo.DbPrinterConfig p on
        p.ReportTemplateID = d.ReportTemplateID and
        p.MachineName = @MachineName
where d.ReportTemplateID = @ReportTemplateID
order by d.TemplateFileTime desc
";
            DataTable dtReturn = DBHelper.ExecuteQuery(args, strSQL, parms);
            return dtReturn;
        }

        public DataTable GetReportTemplateByID4Print(FactoryArgs args, t_BigID ReportTemplateID, t_String MachineName)
        {
            LBDbParameterCollection parms = new LBDbParameterCollection();
            parms.Add(new LBDbParameter("MachineName", MachineName));
            parms.Add(new LBDbParameter("ReportTemplateID", ReportTemplateID));
            string strSQL = @" 
if exists(  select 1 
            from dbo.DbReportTemplate d
                inner join dbo.DbPrinterConfig p on
                    p.ReportTemplateID = d.ReportTemplateID and
                    p.MachineName = @MachineName
            where d.ReportTemplateID = @ReportTemplateID)
begin
    select *
    from dbo.DbReportTemplate d
        left outer join dbo.DbPrinterConfig p on
            p.ReportTemplateID = d.ReportTemplateID and
            p.MachineName = @MachineName
    where d.ReportTemplateID = @ReportTemplateID
    order by d.TemplateFileTime desc
end
else
begin
    select top 1 *
    from dbo.DbReportTemplate d
        left outer join dbo.DbPrinterConfig p on
            p.ReportTemplateID = d.ReportTemplateID
    where d.ReportTemplateID = @ReportTemplateID
    order by p.PrinterConfigID desc
end
";
            DataTable dtReturn = DBHelper.ExecuteQuery(args, strSQL, parms);
            return dtReturn;
        }
    }
}
