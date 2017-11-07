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
    public class BLLDbReportView : IBLLFunction
    {
        private DALDbReportView _DALDbReportView = null;
        public BLLDbReportView()
        {
            _DALDbReportView = new DAL.DALDbReportView();
        }

        public override string GetFunctionName(int iFunctionType)
        {

            string strFunName = "";
            switch (iFunctionType)
            {
                case 14400:
                    strFunName = "Insert";
                    break;

                case 14401:
                    strFunName = "Update";
                    break;

                case 14402:
                    strFunName = "Delete";
                    break;

                case 14403:
                    strFunName = "InsertField";
                    break;

                case 14404:
                    strFunName = "UpdateField";
                    break;

                case 14405:
                    strFunName = "DeleteField";
                    break;

                case 14406:
                    strFunName = "SaveReportTemplate";
                    break;

                case 14407:
                    strFunName = "GetReportSource";
                    break;
            }
            return strFunName;
        }

        public void Insert(FactoryArgs args,out t_BigID ReportViewID,
            t_String ReportViewName, t_nText ReportDataSource)
        {
            _DALDbReportView.Insert(args, out ReportViewID, ReportViewName, ReportDataSource);
        }

        public void Update(FactoryArgs args, t_BigID ReportViewID,
            t_String ReportViewName, t_nText ReportDataSource)
        {
            _DALDbReportView.Update(args, ReportViewID, ReportViewName, ReportDataSource);
        }

        public void Delete(FactoryArgs args, t_BigID ReportViewID)
        {
            _DALDbReportView.Delete(args,ReportViewID);
        }

        public void InsertField(FactoryArgs args, out t_BigID ReportViewFieldID, t_BigID ReportViewID,
            t_String FieldName, t_ID FieldType,t_String FieldText, t_String FieldFormat)
        {
            _DALDbReportView.InsertField(args, out ReportViewFieldID, ReportViewID,
                FieldName, FieldType, FieldText, FieldFormat);
        }

        public void UpdateField(FactoryArgs args, t_BigID ReportViewFieldID,
            t_String FieldName, t_ID FieldType, t_String FieldText, t_String FieldFormat)
        {
            _DALDbReportView.UpdateField(args, ReportViewFieldID,
                FieldName, FieldType, FieldText, FieldFormat);
        }

        public void DeleteField(FactoryArgs args, t_BigID ReportViewFieldID)
        {
            _DALDbReportView.DeleteField(args, ReportViewFieldID);
        }

        public void SaveReportTemplate(FactoryArgs args, t_BigID ReportViewID,t_BigID ReportTemplateID)
        {
            _DALDbReportView.SaveReportTemplate(args, ReportViewID, ReportTemplateID);
        }

        public void GetReportSource( FactoryArgs args, t_BigID ReportViewID,t_Table DTFieldValue )
        {
            t_String ReportDataSource;
            _DALDbReportView.GetReportViewDataSource(args, ReportViewID, DTFieldValue);
        }
    }
}
