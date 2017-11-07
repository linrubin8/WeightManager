using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Web.IRemoting
{
    public delegate void RemotingEventHandler(string msg);
    public interface IWebRemoting
    {
        /*DataSet RunProcedure(int ProcedureType, string strLoginName, byte[] bSerializeValue, byte[] bSerializeDataType,
            out DataTable dtOut, out string ErrorMsg, out bool bolIsError);

        DataTable RunView(int iViewType, string strLoginName, string strFieldNames, string strWhere, string strOrderBy,
            out string ErrorMsg, out bool bolIsError);

        DataTable RunDirectSQL(string strLoginName, string strSQL,
           out string ErrorMsg, out bool bolIsError);

        bool ConnectServer();*/

        void SendRemoting(string msg);
    }
}
