using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LB.WinFunction
{
    public class ProcessStep
    {
        public static DataTable mdtStep = null;
        public static void AddStep(string strName,string strDate)
        {
            if (mdtStep == null)
            {
                mdtStep = new DataTable();
                mdtStep.Columns.Add("StepName", typeof(string));
                mdtStep.Columns.Add("StepTime", typeof(string));
            }
            DataRow drnew = mdtStep.NewRow();
            drnew["StepName"] = strName;
            drnew["StepTime"] = strDate;
            mdtStep.Rows.Add(drnew);
            mdtStep.AcceptChanges();
        }
    }
}
