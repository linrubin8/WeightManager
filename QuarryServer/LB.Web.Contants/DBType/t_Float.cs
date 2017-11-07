using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.Contants.DBType
{
	public struct t_Float : ILBDbType
	{
		private decimal? m_Value;

		public string DBTypeName
		{
			get
			{
				return LBDBType.t_Float;
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
				else if( value is decimal )
				{
					m_Value = (decimal)value;
				}
				else
				{
                    decimal lValue;
                    if (!decimal.TryParse(value.ToString(), out lValue))
                    {
                        throw new Exception("Value must be type of decimal.");
                    }
                    m_Value = lValue;
				}
			}
		}

		public void SetValueWithObject( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is decimal )
			{
				m_Value = (decimal)value;
			}
			else
			{
				try
				{
					m_Value = Convert.ToDecimal( value );
				}
				catch
				{
					throw new Exception( "Value must be type of decimal." );
				}
			}
		}

		public decimal? Value
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

		public t_Float( decimal? value )
		{
			m_Value = value;
		}

		public t_Float( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is decimal )
			{
				m_Value = (decimal)value;
			}
			else if( value is int )
			{
				m_Value = Convert.ToDecimal( value );
			}
			else if( value is float )
			{
				m_Value = Convert.ToDecimal( value );
			}
			else
			{
                decimal lValue;
                if (!decimal.TryParse(value.ToString(), out lValue))
                {
                    throw new Exception("Value must be type of decimal.");
                }
                m_Value = lValue;

            }
		}

		public void IsNullToDefineValue( decimal? value )
		{
			if( value is decimal )
			{
				m_Value = (decimal)value;
			}
			else
			{
				throw new Exception( "Value must be type of decimal." );
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
