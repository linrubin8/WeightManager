using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace LB.Web.ServiceMonitor
{
	class MonitorHelper
	{
		public static void DealWithError( Exception ex )
		{
			System.Windows.Forms.MessageBox.Show( ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error );
		}

		public static void ShowNormalMessage( string msg )
		{
			System.Windows.Forms.MessageBox.Show( msg, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information );
		}

        public static DialogResult ShowConfirmMessage( string msg )
        {
            return System.Windows.Forms.MessageBox.Show( msg, "确认信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
        }
	}
}
