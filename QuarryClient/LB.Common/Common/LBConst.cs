using LB.WinFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LB.Common
{
    public class LBConst
    {
        public static DataTable GetConstData(string strFieldName)
        {
            DataTable dtConst = ExecuteSQL.CallView(101, "ConstValue,ConstText", "FieldName='" + strFieldName + "'", "");
            return dtConst;
        }
    }
}
