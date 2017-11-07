using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.Contants.DBType
{
	public struct t_SmallID : ILBDbType
	{
		private short? m_Value;

		public string DBTypeName
		{
			get
			{
				return LBDBType.t_SmallID;
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
				else if( value is short ||value is int )
				{
					m_Value = (short)value;
				}
				else
				{
					throw new Exception( "Value must be type of short." );
				}
			}
		}

		public void SetValueWithObject( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is short || value is int)
			{
				m_Value = (short)value;
			}
			else
			{
				throw new Exception( "Value must be type of short." );
			}
		}

		public short? Value
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

		public t_SmallID( short? value )
		{
			m_Value = value;
		}

		public t_SmallID( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is short || value is int )
			{
				m_Value = (short)value;
			}
			else
			{
				throw new Exception( "Value must be type of short." );
			}
		}

		public void IsNullToZero()
		{
			if( m_Value == null )
			{
				m_Value = 0;
			}
		}

		public void NullIfZero()
		{
			if( m_Value == 0 )
			{
				m_Value = null;
			}
		}
	}
}
