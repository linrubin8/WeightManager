using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Web.Base.Helper
{
	public class LBDbParameterCollection : MarshalByRefObject, ICollection<LBDbParameter>
	{
		private List<LBDbParameter> Parameters;

		public LBDbParameterCollection()
		{
			Parameters = new List<LBDbParameter>();
		}

		public LBDbParameter this[string parameterName]
		{
			get
			{
				foreach(LBDbParameter parm in this )
				{
					if( parm.ParameterName.Equals( parameterName, StringComparison.CurrentCultureIgnoreCase ) )
					{
						return parm;
					}
				}

				throw new Exception( "Does not contain the TSDbParameter named " + parameterName + "." );
			}
		}

		public LBDbParameter this[int index]
		{
			get
			{
				return Parameters[index];
			}
		}

		public void AddRange( IEnumerable<LBDbParameter> collection )
		{
			Parameters.AddRange( collection );
		}

        #region ICollection<DbParameter> Members

        public void Add(LBDbParameter item )
		{
			Parameters.Add( item );
		}

		public void Clear()
		{
			Parameters.Clear();
		}

		public bool Contains(LBDbParameter item )
		{
			return Parameters.Contains( item );
		}

		public bool Contains( string parameterName )
		{
			foreach(LBDbParameter item in Parameters )
			{
				if( item.ParameterName.Equals( parameterName, StringComparison.CurrentCultureIgnoreCase ) )
				{
					return true;
				}
			}
			return false;
		}

		public void CopyTo(LBDbParameter[] array, int arrayIndex )
		{
			Parameters.CopyTo( array, arrayIndex );
		}

		public int Count
		{
			get
			{
				return Parameters.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public bool Remove(LBDbParameter item )
		{
			return Parameters.Remove( item );
		}

        #endregion

        #region IEnumerable<DbParameter> Members

        public IEnumerator<LBDbParameter> GetEnumerator()
		{
			return Parameters.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return Parameters.GetEnumerator();
		}

		#endregion
	}
}
