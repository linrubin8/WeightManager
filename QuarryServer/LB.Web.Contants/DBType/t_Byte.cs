using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.Contants.DBType
{
	public struct t_Byte : ILBDbType
	{
		private byte? m_Value;

		public string DBTypeName
		{
			get
			{
				return LBDBType.t_Byte;
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
				else if( value is byte )
				{
					m_Value = (byte)value;
				}
				else if( value is int )
				{
					m_Value = (byte)value;
				}
				else
				{
					throw new Exception( "Value must be type of byte." );
				}
			}
		}

		public void SetValueWithObject( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is byte )
			{
				m_Value = (byte)value;
			}
			else
			{
				try
				{
					m_Value = Convert.ToByte( value );
				}
				catch( Exception ex )
				{
					throw new Exception( "Value must be type of byte." );
				}
			}
		}

		public byte? Value
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

		public t_Byte( byte? value )
		{
			m_Value = value;
		}

		public t_Byte( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is byte )
			{
				m_Value = (byte)value;
			}
			else if( value is string )
			{
				m_Value = Convert.ToByte(value);
			}
			else
			{
				try
				{
					m_Value = Convert.ToByte(value);
				}
				catch( Exception ex )
				{
					throw new Exception( "Value must be type of byte." );
				}
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
