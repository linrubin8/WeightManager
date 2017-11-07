using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.Contants.DBType
{
	public struct t_BigID : ILBDbType
	{
		private long? m_Value;

		public string DBTypeName
		{
			get
			{
				return LBDBType.t_BigID;
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
                if (value == null || value == DBNull.Value || value.ToString() == "")
				{
					m_Value = null;
				}
				else if( value is long )
				{
					m_Value = (long)value;
				}
				else
				{
					try
					{
						m_Value = Convert.ToInt64( value );
					}
					catch
					{
						throw new Exception( "Value must be type of long." );
					}
				}
			}
		}

		public void SetValueWithObject( object value )
		{
            if (value == null || value == DBNull.Value || value.ToString() == "")
			{
				m_Value = null;
			}
			else if( value is long )
			{
				m_Value = (long)value;
			}
            else if (value.ToString() != "")
            {
                long lValue;
                bool bolIsLong = long.TryParse(value.ToString(), out lValue);
                if (bolIsLong)
                {
                    m_Value = lValue;
                }
                else
                {
                    throw new Exception("Value must be type of long.");
                }
            }
			else
			{
				throw new Exception( "Value must be type of long." );
			}
		}

		public long? Value
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

		public t_BigID( long? value )
		{
			m_Value = value;
		}

		public t_BigID( object value )
		{
            if (value == null || value == DBNull.Value || value.ToString() == "")
			{
				m_Value = null;
			}
			else if( value is long  )
			{
				m_Value = (long)value;
			}
			else if( value is int )
			{
				m_Value = Convert.ToInt64( value );
            }
            else
            {
                long lValue;
                if(!long.TryParse(value.ToString(), out lValue))
                {
                    throw new Exception("Value must be type of long.");
                }
                m_Value = lValue;
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
