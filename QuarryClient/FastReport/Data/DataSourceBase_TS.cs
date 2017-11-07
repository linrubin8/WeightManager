using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Utils;
using System.ComponentModel;

namespace FastReport.Data
{
	public abstract partial class DataSourceBase
	{
		private string FDescription = "";
		private int FRelateBizObjID = 0;

		/// <summary>
		/// 备注
		/// </summary>
		[ReadOnly( true )]
		public string Description
		{
			get
			{
				return FDescription;
			}
			set
			{
				FDescription = value;
			}
		}

		public void SetDescription( string value )
		{
			FDescription = value;
		}

		/// <summary>
		/// 记录数据源关联的 BizObjID。当查询模式不是查询所有字段时，当前数据源需要根据字段设置查询以及合并。
		/// </summary>
		[ReadOnly( true )]
		public int RelateBizObjID
		{
			get
			{
				return FRelateBizObjID;
			}
			set
			{
				FRelateBizObjID = value;
			}
		}

		public void SetRelateBizObjID( int value )
		{
			FRelateBizObjID = value;
		}

		/// <inheritdoc/>
		public override void Serialize( FRWriter writer )
		{
			base.Serialize( writer );
			if( Enabled )
				writer.WriteBool( "Enabled", Enabled );
			if( ForceLoadData )
				writer.WriteBool( "ForceLoadData", ForceLoadData );
			if( !string.IsNullOrEmpty( Description ) )
				writer.WriteStr( "Description", Description );

			writer.WriteInt( "RelateBizObjID", RelateBizObjID );
		}
	}
}
