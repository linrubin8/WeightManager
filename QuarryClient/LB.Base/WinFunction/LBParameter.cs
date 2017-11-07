using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LB.WinFunction
{
	public class LBParameter : MarshalByRefObject
	{
		public string ParameterName;
		public enLBDbType LBDBType;
		public ParameterDirection Direction;
		public Object Value;

		public LBParameter()
		{
		}

		public LBParameter( string parameterName, enLBDbType dbtype,object value )
		{
			ParameterName = parameterName;
            LBDBType = dbtype;
			Direction = ParameterDirection.Input;
			Value = value;
		}

		public LBParameter( string parameterName, enLBDbType dbtype, object value, bool output )
		{
			ParameterName = parameterName;
            LBDBType = dbtype;
			Direction = ParameterDirection.InputOutput;
			Value = value;
		}
        
		public void IsNullToEmptyOrZero()
		{
			if( Value == DBNull.Value || Value == null )
			{
				switch(LBDBType)
				{
					case enLBDbType.String:
                    case enLBDbType.DateTime:
                    case enLBDbType.Date:
                    case enLBDbType.NText:
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
                    case enLBDbType.String:
                    case enLBDbType.DateTime:
                    case enLBDbType.Date:
                    case enLBDbType.NText:
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
		}
	}
}
