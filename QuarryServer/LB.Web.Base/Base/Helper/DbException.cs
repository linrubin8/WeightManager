using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.Base.Helper
{
	public class DbException : Exception
	{
		//private enDataProxyError m_ErrorDataProxy = 0;
		//public enDataProxyError ErrorDataProxy
		//{
		//    get
		//    {
		//        return m_ErrorDataProxy;
		//    }
		//}

		private int m_ErrorDataBase = 0;
		public int ErrorDataBase
		{
			get
			{
				return m_ErrorDataBase;
			}
		}

		private string m_ErrorDescription = "";
		public string ErrorDescription
		{
			get
			{
				return m_ErrorDescription;
			}
		}

		//private int m_ErrorRowIndex = 0;
		//public int ErrorRowIndex
		//{
		//    get
		//    {
		//        return m_ErrorRowIndex;
		//    }
		//}

		public DbException( int customErrorID )
		{
			m_ErrorDataBase = 80000;
			m_ErrorDescription = customErrorID.ToString();
		}

		public DbException( int customErrorID, string msg1 )
		{
			m_ErrorDataBase = 80001;
			m_ErrorDescription = string.Format( "{0}|{1}", customErrorID, msg1 );
		}

		public DbException( int customErrorID, string msg1, string msg2 )
		{
			m_ErrorDataBase = 80002;
			m_ErrorDescription = string.Format( "{0}|{1}|{2}", customErrorID, msg1, msg2 );
		}

		public DbException( int customErrorID, string msg1, string msg2, string msg3 )
		{
			m_ErrorDataBase = 80003;
			m_ErrorDescription = string.Format( "{0}|{1}|{2}|{3}", customErrorID, msg1, msg2, msg3 );
		}

		public DbException( int customErrorID, string msg1, string msg2, string msg3, string msg4 )
		{
			m_ErrorDataBase = 80004;
			m_ErrorDescription = string.Format( "{0}|{1}|{2}|{3}|{4}", customErrorID, msg1, msg2, msg3, msg4 );
		}
	}
}
