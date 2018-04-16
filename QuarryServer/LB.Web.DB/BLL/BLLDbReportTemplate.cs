using LB.Web.Base.Factory;
using LB.Web.Base.Helper;
using LB.Web.Contants.DBType;
using LB.Web.DB.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Web.DB.BLL
{
    public class BLLDbReportTemplate : IBLLFunction
    {
        private DALDbReportTemplate _DALDbReportTemplate = null;
        public BLLDbReportTemplate()
        {
            _DALDbReportTemplate = new DAL.DALDbReportTemplate();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 12000:
                    strFunName = "Insert";
                    break;
                case 12001:
                    strFunName = "Update";
                    break;
                case 12002:
                    strFunName = "Delete";
                    break;
                case 12003:
                    strFunName = "UpdateReportTemplate";
                    break;
            }
            return strFunName;
        }

        public void Insert(FactoryArgs args, 
            out t_BigID ReportTemplateID,t_String ReportTemplateName,t_DTSmall TemplateFileTime,t_ID TemplateSeq,
            t_String Description,t_Image TemplateData, t_BigID ReportTypeID,
            t_String PrinterName, t_String MachineName,t_Bool IsManualPaperType, t_String PaperType, t_Bool IsManualPaperSize, 
            t_ID PaperSizeHeight, t_ID PaperSizeWidth, t_Bool IsPaperTransverse,t_ID PrintCount)
        {
            ReportTemplateID = new t_BigID();

            using (DataTable dtExistsName = _DALDbReportTemplate.ExistsTemplateName(args, ReportTemplateName,ReportTypeID))
            {
                if (dtExistsName.Rows.Count > 0)//校验上级权限组是否存在
                {
                    throw new Exception("该报表名称已存在！");
                }
                else
                {
                    _DALDbReportTemplate.Insert(args, out ReportTemplateID, ReportTemplateName,
                        TemplateFileTime, TemplateSeq, Description, TemplateData, ReportTypeID,
                        PrinterName, MachineName, IsManualPaperType, PaperType, IsManualPaperSize,
                        PaperSizeHeight, PaperSizeWidth, IsPaperTransverse, PrintCount);
                }
            }
        }

        public void Update(FactoryArgs args,
           t_BigID ReportTemplateID, t_String ReportTemplateName, t_ID TemplateSeq,
           t_String Description, t_BigID ReportTypeID,
            t_String PrinterName, t_String MachineName, t_Bool IsManualPaperType, t_String PaperType, t_Bool IsManualPaperSize,
            t_ID PaperSizeHeight, t_ID PaperSizeWidth, t_Bool IsPaperTransverse, t_ID PrintCount)
        {
            using (DataTable dtExistsName = _DALDbReportTemplate.ExistsTemplateName(args, ReportTemplateName, ReportTypeID))
            {
                if (dtExistsName.Rows.Count > 0)//校验上级权限组是否存在
                {
                    dtExistsName.DefaultView.RowFilter = "ReportTemplateID<>" + ReportTemplateID.Value;
                    if (dtExistsName.DefaultView.Count == 0)
                    {
                        _DALDbReportTemplate.Update(args, ReportTemplateID, ReportTemplateName, TemplateSeq, Description, 
                        PrinterName, MachineName, IsManualPaperType, PaperType, IsManualPaperSize,
                        PaperSizeHeight, PaperSizeWidth, IsPaperTransverse, PrintCount);
                    }
                    else
                    {
                        throw new Exception("该报表名称已存在！");
                    }
                    
                }
                else
                {
                    _DALDbReportTemplate.Update(args, ReportTemplateID, ReportTemplateName, TemplateSeq, Description, 
                        PrinterName, MachineName, IsManualPaperType, PaperType, IsManualPaperSize,
                        PaperSizeHeight, PaperSizeWidth, IsPaperTransverse, PrintCount);
                }
            }
        }

        public void Delete(FactoryArgs args,
           t_BigID ReportTemplateID)
        {
            _DALDbReportTemplate.Delete(args, ReportTemplateID);
        }

        public void UpdateReportTemplate(FactoryArgs args,
           t_BigID ReportTemplateID, t_DTSmall TemplateFileTime,t_Image TemplateData)
        {
            _DALDbReportTemplate.UpdateReportTemplate(args, ReportTemplateID, TemplateFileTime, TemplateData);
        }
    }
}
