using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.Contants.DBType
{
	public struct t_ID: ILBDbType
	{
		private int? m_Value;

		public string DBTypeName
		{
			get
			{
				return LBDBType.t_ID;
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
				else if( value is int )
				{
					m_Value = (int)value;
				}
				else
                {
                    try
                    {
                        m_Value = Convert.ToInt32( value );
                    }
                    catch
                    {
                        throw new Exception( "Value must be type of int." );
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
			else if( value is int )
			{
				m_Value = (int)value;
			}
			else
            {
                try
                {
                    m_Value = Convert.ToInt32( value );
                }
                catch
                {
                    throw new Exception( "Value must be type of int." );
                }
			}
		}

		public int? Value
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

		public t_ID( int? value )
		{
			m_Value = value;
		}

		public t_ID( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is int )
			{
				m_Value = (int)value;
			}
			else
            {
                try
                {
                    m_Value = Convert.ToInt32( value );
                }
                catch
                {
                    throw new Exception( "Value must be type of int." );
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
