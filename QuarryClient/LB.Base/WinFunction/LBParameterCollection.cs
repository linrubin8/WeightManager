using System;
using System.Collections.Generic;
using System.Text;

namespace LB.WinFunction
{
	public class LBDbParameterCollection : MarshalByRefObject, ICollection<LBParameter>
	{
		private List<LBParameter> Parameters;

		public LBDbParameterCollection()
		{
			Parameters = new List<LBParameter>();
		}

		public LBParameter this[string parameterName]
		{
			get
			{
				foreach(LBParameter parm in this )
				{
					if( parm.ParameterName.Equals( parameterName, StringComparison.CurrentCultureIgnoreCase ) )
					{
						return parm;
					}
				}

				throw new Exception( "Does not contain the LBParameter named " + parameterName + "." );
			}
		}

		public LBParameter this[int index]
		{
			get
			{
				return Parameters[index];
			}
		}

		public void AddRange( IEnumerable<LBParameter> collection )
		{
			Parameters.AddRange( collection );
		}

        #region ICollection<LBParameter> Members

        public void Add(LBParameter item )
		{
			Parameters.Add( item );
		}

		public void Clear()
		{
			Parameters.Clear();
		}

		public bool Contains(LBParameter item )
		{
			return Parameters.Contains( item );
		}

		public bool Contains( string parameterName )
		{
			foreach(LBParameter item in Parameters )
			{
				if( item.ParameterName.Equals( parameterName, StringComparison.CurrentCultureIgnoreCase ) )
				{
					return true;
				}
			}
			return false;
		}

		public void CopyTo(LBParameter[] array, int arrayIndex )
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

		public bool Remove(LBParameter item )
		{
			return Parameters.Remove( item );
		}

        #endregion

        #region IEnumerable<LBParameter> Members

        public IEnumerator<LBParameter> GetEnumerator()
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
