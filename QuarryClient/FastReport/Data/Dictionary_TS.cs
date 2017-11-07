using System;
using System.Collections.Generic;
using System.Text;

namespace FastReport.Data
{
	public partial class Dictionary
	{
		internal object FindDataObjectByAlias( string alias )
		{
			DataSourceBase data = this.FindByAlias( alias ) as DataSourceBase;
			if( data == null )
			{
				return null;
			}

			string name = data.Name;
            //连续预览时当前行的数据源在后面，所以从后面开始查找
            for( int j = FRegisteredItems.Count, i = j - 1; i >= 0; i-- )
            {
                RegDataItem item = FRegisteredItems[i];
                if( item.Name == name )
                    return item.Data;
            }
            //foreach( RegDataItem item in FRegisteredItems )
            //{
            //    if( item.Name == name )
            //        return item.Data;
            //}
			return null;
		}
	}
}
