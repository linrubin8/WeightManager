using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Common
{
    public class LBConverter
    {
        public static long ToInt64(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return 0;
            }
            else
            {
                long lValue;
                long.TryParse(Value.ToString(), out lValue);
                return lValue;
            }
        }
        public static int ToInt32(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return 0;
            }
            else
            {
                int iValue;
                int.TryParse(Value.ToString(), out iValue);
                return iValue;
            }
        }

        public static decimal ToDecimal(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return 0;
            }
            else
            {
                decimal decValue;
                decimal.TryParse(Value.ToString(), out decValue);
                return decValue;
            }
        }

        public static bool ToBoolean(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return false;
            }
            else
            {
                int iValue;
                int.TryParse(Value.ToString(), out iValue);
                if (iValue > 0)
                    return true;

                bool bolValue;
                bool.TryParse(Value.ToString(), out bolValue);
                return bolValue;
            }
        }

        public static byte ToByte(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return 0;
            }
            else
            {
                byte iValue;
                byte.TryParse(Value.ToString(), out iValue);
                return iValue;
            }
        }

        public static string ToString(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return "";
            }
            else
            {
                return Value.ToString().TrimEnd();
            }
        }

        public static DateTime ToDateTime(object Value)
        {
            if (Value == null || Value == DBNull.Value)
            {
                return DateTime.Now;
            }
            else
            {
                DateTime dt;
                DateTime.TryParse(Value.ToString(), out dt);
                return dt;
            }
        }
    }
}
