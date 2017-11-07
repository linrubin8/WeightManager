using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LB.Web.Contants.DBType
{
	public struct t_Object : ILBDbType
	{
		private Object m_Value;

		public string DBTypeName
		{
			get
			{
				return LBDBType.t_Object;
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
                if (value == null || value == DBNull.Value)
				{
					m_Value = null;
				}
				else if( value is Object )
				{
					m_Value = (Object)value;
				}
				else
				{
					throw new Exception( "Value must be type of Object." );
				}
			}
		}

		public void SetValueWithObject( object value )
		{
            if (value == null || value == DBNull.Value)
			{
				m_Value = null;
			}
			else if( value is Object )
			{
				m_Value = (Object)value;
			}
			else
			{
				throw new Exception( "Value must be type of Object." );
			}
		}

		public Object Value
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

        public t_Object(Object value)
		{
            if (value == null || value == DBNull.Value)
			{
				m_Value = null;
			}
            else if (value is Object)
			{
                m_Value = (Object)value;
			}
			else
			{
                throw new Exception("Value must be type of Object.");
			}
		}
	}
}
