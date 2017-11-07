using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LB.Web.Contants.DBType;

namespace LB.Web.Base.Helper
{
	public class LBDbParameter : MarshalByRefObject
	{
		public string ParameterName;
		public ParameterDirection Direction;
        public string LBDBType;

        public object Value;

		public LBDbParameter()
		{
		}

		public LBDbParameter( string parameterName, ILBDbType value )
		{
			ParameterName = parameterName;
			Direction = ParameterDirection.Input;
			Value = value.Value;
            LBDBType = value.DBTypeName.ToString();
        }

		public LBDbParameter( string parameterName, ILBDbType value, bool output )
		{
			ParameterName = parameterName;
			Direction = ParameterDirection.InputOutput;
			Value = value.Value;
            LBDBType = value.GetType().ToString();
        }

		/*public void IsNullToEmptyOrZero()
		{
			if( Value.Value == DBNull.Value || Value == null )
			{
				switch(LBDBType.GetSqlDbType(Value.GetType().ToString()))
				{
					case SqlDbType.NText:
                    case SqlDbType.DateTime:
                    case SqlDbType.VarChar:
                    case SqlDbType.SmallDateTime:
                    case SqlDbType.NText:
                    case SqlDbType.NText:
                        Value = "";
						break;

                    default:
						Value = 0;
						break;
				}
			}
		}

		public void NullIfEmptyOrZero()
		{
			if( Value != DBNull.Value && Value != null )
			{
                switch (LBDBType)
                {
                    case LBDbType.String:
                    case LBDbType.DateTime:
                    case LBDbType.Date:
                    case LBDbType.NText:
                        if ( string.IsNullOrEmpty( Value.ToString().Trim() ) )
						{
							Value = DBNull.Value;
						}
						break;

                    default:
                        try
						{
							decimal decTemp = Convert.ToDecimal( Value );
							if( decTemp == 0 )
							{
								Value = DBNull.Value;
							}
						}
						catch
						{
						}
						break;
				}
			}
		}*/
	}
}
