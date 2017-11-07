using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.Contants.DBType
{
	public struct t_String : ILBDbType
	{
		private string m_Value;

		public string DBTypeName
		{
			get
			{
				return LBDBType.t_String;
			}
		}

		object ILBDbType.Value
		{
			get
			{
				return m_Value;
			}
			set
			{
				if( value == null || value == DBNull.Value )
				{
					m_Value = null;
				}
				else if( value is string )
				{
					m_Value = (string)value;
				}
				else
				{
					throw new Exception( "Value must be type of string." );
				}
			}
		}

		public void SetValueWithObject( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is string )
			{
				m_Value = (string)value;
			}
			else
			{
				throw new Exception( "Value must be type of string." );
			}
		}

		public string Value
		{
			get
			{
				return m_Value;
			}
			set
			{
				m_Value = value;
			}
		}

		public t_String( string value )
		{
			m_Value = value;
		}

		public t_String( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is string )
			{
				m_Value = (string)value;
			}
			else
			{
				throw new Exception( "Value must be type of string." );
			}
		}

		public void IsNullToEmpty()
		{
			if( m_Value == null )
			{
				m_Value = string.Empty;
			}
		}

		public void NullIfEmpty()
		{
			if( m_Value != null && m_Value.Trim() == string.Empty )
			{
				m_Value = null;
			}
		}
	}
}
