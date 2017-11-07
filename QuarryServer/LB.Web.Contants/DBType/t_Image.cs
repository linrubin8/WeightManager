using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.Contants.DBType
{
	public struct t_Image : ILBDbType
	{
		private byte[] m_Value;

		public string DBTypeName
		{
			get
			{
				return LBDBType.t_Image;
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
				else if( value is byte[] )
				{
					m_Value = (byte[])value;
				}
				else
				{
					throw new Exception( "Value must be type of byte[]." );
				}
			}
		}

		public void SetValueWithObject( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is byte[] )
			{
				m_Value = (byte[])value;
			}
			else
			{
				throw new Exception( "Value must be type of byte[]." );
			}
		}

		public byte[] Value
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

		public t_Image( byte[] value )
		{
			m_Value = value;
		}

		public t_Image( object value )
		{
			if( value == null || value == DBNull.Value )
			{
				m_Value = null;
			}
			else if( value is byte[] )
			{
				m_Value = (byte[])value;
			}
			else
			{
				throw new Exception( "Value must be type of byte[]." );
			}
		}

	}
}
