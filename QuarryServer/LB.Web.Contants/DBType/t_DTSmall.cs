using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.Contants.DBType
{
	public struct t_DTSmall : ILBDbType
	{
		private DateTime? m_Value;

		public string DBTypeName
		{
			get
			{
				return LBDBType.t_DTSmall;
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
				else if( value is DateTime )
				{
					m_Value = (DateTime)value;
				}
				else
				{
					try
					{
						m_Value = Convert.ToDateTime( value );
					}
					catch
					{
						throw new Exception( "Value must be type of DateTime." );
					}
				}
			}
		}

		public void SetValueWithObject( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is DateTime )
			{
				m_Value = (DateTime)value;
			}
			else
			{
				throw new Exception( "Value must be type of DateTime." );
			}
		}

	
		public DateTime? Value
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

		public t_DTSmall( DateTime? value )
		{
			m_Value = value;
		}

		public t_DTSmall( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is DateTime )
			{
				m_Value = (DateTime)value;
			}
			else
            {
                DateTime dt;
                if (!DateTime.TryParse(value.ToString(), out dt))
                {
                    throw new Exception("Value must be type of DateTime.");
                }
                m_Value = dt;

            }
		}
	}
}
