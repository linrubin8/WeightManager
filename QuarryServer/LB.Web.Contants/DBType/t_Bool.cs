using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.Contants.DBType
{
	public struct t_Bool : ILBDbType
	{
		private byte? m_Value;

		public string DBTypeName
		{
			get
			{
				return LBDBType.t_Bool;
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
					m_Value = ( (byte)0 );
				}
				else if( value is bool )
				{

					m_Value = ( (bool)value ) ? ( (byte)1 ) : ( (byte)0 );
				}
				else if( value is int )
				{
					m_Value = (byte)value;
				}
				else if( value is byte )
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
				m_Value = ( (byte)0 );
			}
			else if( value is bool )
			{

				m_Value = ( (bool)value ) ? ( (byte)1 ) : ( (byte)0 );
			}
			else if( value is int )
			{
				m_Value = ( (int)value==1 ) ? ( (byte)1 ) : ( (byte)0 );
			}
			else if( value is byte )
			{
				m_Value = (byte)value;
			}
			else
			{
				throw new Exception( "Value must be type of byte." );
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

		public t_Bool( byte? value )
		{
			m_Value = value;
		}

		public t_Bool( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
				//m_Value = ( (byte)0 );
			}
			else if( value is bool )
			{
				m_Value = ( (bool)value ) ? ( (byte)1 ) : ( (byte)0 );
            }
            else if (value is byte)
            {
                m_Value = ((byte)value==1) ? ((byte)1) : ((byte)0);
            }
            else if( value is int )
			{
				if( (int)value == 0 )
				{
					m_Value = 0;
				}
				else
				{
					m_Value = 1;
				}
			}
			else
			{
				throw new Exception( "Value must be type of byte." );
			}
		}

		public void IsNullToZero()
		{
			if( m_Value == null )
			{
				m_Value = 0;
			}
		}

	}
}

