using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.Common
{
    public class LBErrorLog
    {
        public static void InsertErrorLog(string strlog)
        {
            try
            {
                LBDbParameterCollection parms = new LBDbParameterCollection();
                parms.Add(new LBParameter("ErrorLogMsg", enLBDbType.String, strlog+" "+DateTime.Now.ToString("yyMMdd HH:mm:ss")));
                DataSet dsReturn; 
                Dictionary<string, object> dictResult;
                ExecuteSQL.CallSP(20000, parms, out dsReturn, out dictResult);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
