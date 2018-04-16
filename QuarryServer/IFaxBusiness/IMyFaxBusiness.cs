using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IFaxBusiness
{
    public delegate void FaxEventHandler(string fax);
    public interface IMyFaxBusiness
    {
        void SendFax(string fax);

        string RunProcedure(int ProcedureType, string strLoginName, byte[] bSerializeValue, byte[] bSerializeDataType,
            out string strdtOut, out string ErrorMsg, out bool bolIsError);

        string RunView(int iViewType, string strLoginName, string strFieldNames, string strWhere, string strOrderBy,
            out string ErrorMsg, out bool bolIsError);

        string RunDirectSQL(string strLoginName, string strSQL,
           out string ErrorMsg, out bool bolIsError);

        bool ConnectServer();

        DataTable ReadClientFileInfo();

        void ReadFileByte(string strFileFullName, int iPosition, int iMaxLength, out byte[] bSplitFile);

        void ReadRegister(out bool IsRegister, out int ProductType, out string RegisterInfoJson, out DateTime DeadLine);
    }
}
