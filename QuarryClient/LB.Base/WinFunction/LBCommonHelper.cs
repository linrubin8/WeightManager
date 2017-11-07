using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LB.WinFunction
{
    public class LBCommonHelper
    {
        public static void DealWithErrorMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, "错误");
        }

        public static void ShowCommonMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, "提示信息");
        }

        public static void ShowCommonMessage(string text)
        {
            MessageBox.Show(text, "提示信息");
        }

        public static DialogResult ConfirmMessage(string strMsg, string caption, MessageBoxButtons buttons)
        {
            return MessageBox.Show(strMsg, caption, buttons);
        }
        
        public static decimal ToDecimal(object objValue)
        {
            if (objValue == null)
                return 0;
            decimal dValue = 0;
            decimal.TryParse(objValue.ToString(), out dValue);
            return dValue;
        }

        public static decimal ToByte(object objValue)
        {
            if (objValue == null)
                return 0;
            byte bValue = 0;
            byte.TryParse(objValue.ToString(), out bValue);
            return bValue;
        }

        public static int ToInt(object objValue)
        {
            if (objValue == null)
                return 0;
            int iValue = 0;
            int.TryParse(objValue.ToString(), out iValue);
            return iValue;
        }

        public static long ToLong(object objValue)
        {
            if (objValue == null)
            {
                return 0;
            }
            long lValue = 0;
            long.TryParse(objValue.ToString(), out lValue);
            return lValue;
        }

        public static float ToFloat(object objValue)
        {
            if (objValue == null)
            {
                return 0;
            }
            float fValue = 0;
            float.TryParse(objValue.ToString(), out fValue);
            return fValue;
        }

        public static bool ToBool(object objValue)
        {
            if (objValue == null)
                return false;
            bool bValue = false;
            if (bool.TryParse(objValue.ToString(), out bValue))
            {
                return bValue;
            }
            return Convert.ToBoolean(ToInt(objValue));
        }
    }
}
