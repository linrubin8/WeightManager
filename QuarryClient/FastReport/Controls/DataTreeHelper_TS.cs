using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Data;
using System.Windows.Forms;

namespace FastReport.Controls
{
	internal static partial class DataTreeHelper
	{
		public static void AddColumns( TreeNodeCollection root, ColumnCollection columns, bool enabledOnly, bool showColumns )
		{
			List<TreeNode> nodes = new List<TreeNode>();
			foreach( Column column in columns )
			{
				if( !enabledOnly || column.Enabled )
				{
					TreeNode node = new TreeNode( column.Alias );
					node.Tag = column;
					node.Checked = column.Enabled;

					int imageIndex = column.GetImageIndex();
					node.ImageIndex = imageIndex;
					node.SelectedImageIndex = imageIndex;

					AddColumns( node.Nodes, column.Columns, enabledOnly, showColumns );

					bool isDataSource = column is DataSourceBase;
					bool addNode = showColumns || isDataSource || node.Nodes.Count > 0;

					if( addNode )
					{
						nodes.Add( node );
						//root.Add( node );
					}
				}
			}
			nodes.Sort( new TreeNodeComparer() );
			for( int i = 0, j = nodes.Count; i < j; i++ )
			{
				root.Add( nodes[i] );
			}
		}

		private static void AddParameter( Parameter par, TreeNodeCollection root )
		{
			TreeNode parNode = root.Add( par.Name );
			parNode.Tag = par;
			parNode.ImageIndex = par.Parameters.Count == 0 ? GetTypeImageIndex( par.DataType ) : 234;
			parNode.SelectedImageIndex = parNode.ImageIndex;

			if( par.Parameters.Count > 0 )
			{
				List<Parameter> lstParameter = SortParameters( par.Parameters );
				for( int i = 0, j = lstParameter.Count; i < j; i++ )
				{
					AddParameter( lstParameter[i], parNode.Nodes );
				}
			}
		}

		public static void CreateParametersTree( ParameterCollection parameters, TreeNodeCollection root )
		{
			List<Parameter> lstParameter = SortParameters( parameters );

			for( int i = 0, j = lstParameter.Count; i < j; i++ )
			{
				AddParameter( lstParameter[i], root );
			}
		}

		public static List<Parameter> SortParameters( ParameterCollection parameters )
		{
			List<Parameter> lstParameter = new List<Parameter>();
			foreach( Parameter p in parameters )
			{
				lstParameter.Add( p );
			}
			lstParameter.Sort( new ParameterComparer() );
			return lstParameter;
		}
	}

	public class TreeNodeComparer : IComparer<TreeNode>
	{
		public int Compare( TreeNode x, TreeNode y )
		{
			if( x == null )
			{
				if( y == null )
				{
					// If x is null and y is null, they're
					// equal. 
					return 0;
				}
				else
				{
					// If x is null and y is not null, y
					// is greater. 
					return -1;
				}
			}
			else
			{
				// If x is not null...
				//
				if( y == null )
				// ...and y is null, x is greater.
				{
					return 1;
				}
				else
				{
					return x.Text.CompareTo( y.Text );
				}
			}
		}
	}

	public class ParameterComparer : IComparer<Parameter>
	{
		public int Compare( Parameter x, Parameter y )
		{
			if( x == null )
			{
				if( y == null )
				{
					// If x is null and y is null, they're
					// equal. 
					return 0;
				}
				else
				{
					// If x is null and y is not null, y
					// is greater. 
					return -1;
				}
			}
			else
			{
				// If x is not null...
				//
				if( y == null )
				// ...and y is null, x is greater.
				{
					return 1;
				}
				else
				{
					return x.Name.CompareTo( y.Name );
				}
			}
		}

	}
}
