using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LB.Web.Contants.DBType
{
	public struct t_Table : ILBDbType
	{
		private DataTable m_Value;

		public string DBTypeName
		{
			get
			{
				return LBDBType.t_Table;
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
				else if( value is DataTable )
				{
					m_Value = (DataTable)value;
				}
				else
				{
					throw new Exception( "Value must be type of DataTable." );
				}
			}
		}

		public void SetValueWithObject( object value )
		{
            if (value == null || value == DBNull.Value)
			{
				m_Value = null;
			}
			else if( value is DataTable )
			{
				m_Value = (DataTable)value;
			}
			else
			{
				throw new Exception( "Value must be type of DataTable." );
			}
		}

		public DataTable Value
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

		public t_Table( DataTable value )
		{
			m_Value = value;
		}

		public t_Table( object value )
		{
            if (value == null || value == DBNull.Value)
			{
				m_Value = null;
			}
			else if( value is DataTable )
			{
				m_Value = (DataTable)value;
			}
			else
			{
				throw new Exception( "Value must be type of DataTable." );
			}
		}
	}
}
