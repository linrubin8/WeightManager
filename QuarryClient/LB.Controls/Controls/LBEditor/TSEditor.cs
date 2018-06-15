using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using LB.Controls.LBEditor;
using LB.Controls.Args;
using TS.Win.Controls;
using LB.WinFunction;

namespace LB.Controls.LBEditor
{
	public class TSEditor : TSTextBox
	{
		private static bool mbNCodePromtBizObjByConfig = false;

		/// <summary>
		/// 编码项目的输入框，点击提示按钮打开的页面，商业对象号是否用 FieldEntity 的配置。
		/// 对于 v36 是固定使用 301 的，而对于门窗通，就是用 FieldEntity 的配置。
		/// </summary>
		public static bool NCodePromtBizObjByConfig
		{
			get
			{
				return TSEditor.mbNCodePromtBizObjByConfig;
			}
			set
			{
				TSEditor.mbNCodePromtBizObjByConfig = value;
			}
		}

		//private int miNCodeClassID;
		//private int miPromptBizObjID;
		private bool mbIncludeNCodeForbidden = false;
        private bool mbAlwaysPromptItemModel = false;
		public event PromptParmsNeedEventHandler PromptParmsNeed;
		public event PromptBizObjCreatedEventHandler PromptBizObjCreated;
		public event EventHandler PopupGetDataSource;
		public event PromptConstDataEventHandler PromptConstData;

        private int _LBViewType = 0;
        [Description("查询视图号")]
        public int LBViewType
        {
            get
            {
                return _LBViewType;
            }
            set
            {
                _LBViewType = value;
            }
        }

        private string _LBSort = "";
        [Description("查询排序方式")]
        public string LBSort
        {
            get
            {
                return _LBSort;
            }
            set
            {
                _LBSort = value;
            }
        }
        //      public int PromptBizObjID
        //{
        //	get
        //	{
        //		return miPromptBizObjID;
        //	}
        //	set
        //	{
        //		miPromptBizObjID = value;
        //	}
        //}

        //public int NCodeClassID
        //{
        //	get
        //	{
        //		return miNCodeClassID;
        //	}
        //	set
        //	{
        //		miNCodeClassID = value;
        //	}
        //}

        public bool IncludeNCodeForbidden
		{
			get
			{
				return mbIncludeNCodeForbidden;
			}
			set
			{
				mbIncludeNCodeForbidden = value;
			}
		}

        public bool AlwaysPromptItemModel
        {
            get
            {
                return mbAlwaysPromptItemModel;
            }
            set
            {
                mbAlwaysPromptItemModel = value;
            }
        }

		protected virtual void OnPopupGetDataSource( EventArgs e )
		{
			if( PopupGetDataSource != null )
			{
				PopupGetDataSource( this, e );
			}
		}

		public override enPromptButtonType PromptButtonType
		{
			get
			{
				return base.PromptButtonType;
			}
			set
			{
				base.PromptButtonType = value;

				switch( value )
				{
					case enPromptButtonType.GridCellButton:
						break;

					case enPromptButtonType.Prompt:
					case enPromptButtonType.PromptStep:
					case enPromptButtonType.PopupAllowPrompt:
						this.PopupManageButtonVisible = true;
						break;

					case enPromptButtonType.Popup:
					case enPromptButtonType.None:
					default:
						this.PopupManageButtonVisible = false;
						break;
				}
			}
		}

		//protected override void OnPopupAddClicked( HandledEventArgs e )
		//{
		//	base.OnPopupAddClicked( e );

		//	if( !e.Handled )
		//	{
		//		string strText = this.Text.Trim();
		//		this.Text = "";
		//		TS.Win.IUI.IPageApp ipage = GetPromptAddPage( PromptBizObjID, NCodeClassID, strText );
		//		TS.Win.BaseType.RequestPage.ShowDialogPage( ipage );

		//		if( this.PromptButtonType == enPromptButtonType.PopupAllowPrompt )
		//		{
		//			this.DataSource = null;
		//		}
		//	}
		//}

		//protected override void OnPopupManageClicked( HandledEventArgs e )
		//{
		//	base.OnPopupManageClicked( e );

		//	if( !e.Handled )
		//	{
		//		TS.Win.UIControl.PromptClickEventArgs args = new PromptClickEventArgs( enPromptButtonClickType.Prompt, false, this );
		//		OnPromptClick( args );
		//	}
		//}

		public void ReGetPopupDataSource()
		{
			if( this.PromptButtonType == enPromptButtonType.Popup || this.PromptButtonType == enPromptButtonType.PopupAllowPrompt )
			{
				OnPopupGetDataSource( EventArgs.Empty );

				if( this.DataSource == null )
				{
					// 根据 PromptBizObjID 来查询列表数据
					if( this.LBViewType > 0 )
					{
						this.DataSource = this.GetListDataByPrompt();
					}
					else
					{
						this.DataSource = this.GetListDataByConstant( this.Name );
					}
				}
			}
		}

		protected override void OnPromptClick( PromptClickEventArgs args )
		{
			base.OnPromptClick( args );

			if( !args.Handled &&
				args.PromptButtonClickType == enPromptButtonClickType.Popup )
			{
				OnPopupGetDataSource( EventArgs.Empty );

				if( this.DataSource == null )
				{
					// 根据 PromptBizObjID 来查询列表数据
					if( this.LBViewType > 0 )
					{
						this.DataSource = this.GetListDataByPrompt();
					}
					else
					{
						this.DataSource = this.GetListDataByConstant( this.Name );
					}
				}
			}
			//else if( !args.Handled &&
			//	PromptBizObjID > 0 &&
			//	args.PromptButtonClickType == enPromptButtonClickType.Prompt )
			//{
			//	TS.Win.IUI.IPageApp ipage = GetPromptPage( PromptBizObjID, NCodeClassID, false );
			//	args.PromptForm = ipage;
			//	TS.Win.BaseType.RequestPage.ShowDialogPage( ipage );
			//	args.Handled = true;

			//	if( this.PromptButtonType == enPromptButtonType.PopupAllowPrompt )
			//	{
			//		this.DataSource = null;
			//	}
			//}
		}

		//private TS.Win.IUI.IPageApp GetPromptAddPage( int iPromptBizObjID, int iNCodeClassID, string strInputedText )
		//{
		//	TS.Win.IUI.IPageApp ipage = null;
		//	int iPromptPageType = 0;
		//	StringDict parms = new StringDict();

		//	if( iNCodeClassID > 0 )
		//	{
		//		string strNCodeClassName;
		//		BizObjHelper.GetNCodeClassRelateBizObjID( iNCodeClassID, out iPromptBizObjID, out strNCodeClassName );
		//		ERPBizObjBase bizPrompt = new ERPBizObjBase( iPromptBizObjID );
		//		iPromptPageType = bizPrompt.EditPageType;
		//		parms.Add( "NCodeClassID", iNCodeClassID.ToString() );
		//		parms.Add( TS.Win.Constants.PageParamKey.PageTitle, strNCodeClassName );
		//	}
		//	else
		//	{
		//		ERPBizObjBase bizPrompt = new ERPBizObjBase( iPromptBizObjID );
		//		iPromptPageType = bizPrompt.EditPageType;
		//	}

		//	if( iPromptPageType <= 0 )
		//	{
		//		iPromptPageType = TS.Win.PageType.PageConstants.UIDefine;
		//	}

		//	parms.Add( TS.Win.Constants.PageParamKey.BizObjID, iPromptBizObjID.ToString() );
		//	parms.Add( TS.Win.Constants.PageStatusType.KeyName, TS.Win.Constants.PageStatusType.Add );
		//	parms.Add( TS.Win.Constants.PageParamKey.OpenFromEditorPopupAdd, "1" );
		//	if( strInputedText != "" )
		//	{
		//		parms.Add( TS.Win.Constants.PageParamKey.InputedText, strInputedText );
		//	}

		//	int index = 0;
		//	string[] strParms = new string[parms.Count * 2 + 1];
		//	strParms[0] = iPromptPageType.ToString();
		//	foreach( KeyValuePair<string, string> pair in parms )
		//	{
		//		index++;
		//		strParms[index] = pair.Key;

		//		index++;
		//		strParms[index] = pair.Value;
		//	}

		//	ipage = TS.Win.BaseType.RequestPage.GetPageByURLParams( strParms );
		//	ipage.PromptReturn += new PromptReturnEventHandler( ipage_PromptReturn );
		//	return ipage;
		//}

		void ipage_PromptReturn( object sender, PromptReturnArgs e )
		{
			base.ProcessPromptReturn( e );
		}

		//private TS.Win.IUI.IPageApp GetPromptPage(
		//	int iPromptBizObjID, int iNCodeClassID, bool bMultiPrompt )
		//{
		//	TS.Win.IUI.IPageApp ipage = null;
		//	StringDict parms = new StringDict();
		//	int iPromptPageType = 0;

		//	if( iNCodeClassID > 0 )
		//	{
		//		if( !NCodePromtBizObjByConfig )
		//		{
		//			iPromptBizObjID = 301;
		//		}

		//		ERPBizObjBase bizPrompt = new ERPBizObjBase( iPromptBizObjID );
		//		iPromptPageType = bizPrompt.QueryTabPageType;
		//		if( iPromptPageType == 0 )
		//		{
		//			iPromptPageType = TS.Win.PageType.PageConstants.UIQuery;
		//		}

		//		parms.Add( TS.Win.Constants.PageParamKey.BizObjID, iPromptBizObjID.ToString() );
		//		parms.Add( TS.Win.Constants.PageParamKey.PromptMode, "1" );
		//		parms.Add( TS.Win.Constants.PageParamKey.MultiPromptMode, Convert.ToInt32( bMultiPrompt ).ToString() );
		//		parms.Add( TS.Win.Constants.PageParamKey.fTVNCodeClassID, iNCodeClassID.ToString() );
		//	}
		//	else
		//	{
		//		ERPBizObjBase bizPrompt = new ERPBizObjBase( iPromptBizObjID );
		//		iPromptPageType = bizPrompt.QueryTabPageType;
		//		if( iPromptPageType == 0 )
		//		{
		//			iPromptPageType = TS.Win.PageType.PageConstants.UIQuery;
		//		}

		//		parms.Add( TS.Win.Constants.PageParamKey.BizObjID, iPromptBizObjID.ToString() );
		//		parms.Add( TS.Win.Constants.PageParamKey.PromptMode, "1" );
		//		parms.Add( TS.Win.Constants.PageParamKey.MultiPromptMode, Convert.ToInt32( bMultiPrompt ).ToString() );
		//	}

		//	PromptParmsNeedArgs args = new PromptParmsNeedArgs( iPromptPageType, iPromptBizObjID, parms );
		//	OnPromptParmsNeed( args );

		//	int index = 0;
		//	string[] strParms = new string[parms.Count * 2 + 1];
		//	strParms[0] = iPromptPageType.ToString();
		//	foreach( KeyValuePair<string, string> pair in parms )
		//	{
		//		index++;
		//		strParms[index] = pair.Key;

		//		index++;
		//		strParms[index] = pair.Value;
		//	}

		//	ipage = TS.Win.BaseType.RequestPage.GetPageByURLParams( strParms );
		//	return ipage;
		//}

		protected virtual void OnPromptParmsNeed( PromptParmsNeedArgs e )
		{
			if( PromptParmsNeed != null )
			{
				PromptParmsNeed( this, e );
			}
		}

		private DataView GetListDataByConstant( string strFieldName )
		{
			PromptConstDataArgs args = new PromptConstDataArgs( strFieldName, "" );
			OnPromptConstData( args );
            return null;
			//DataTable dt = TS.Win.ConfigData.RequestEntityData.RequestConstConfig( strFieldName, args.CustomerCriteria );
			//if( dt != null )
			//{
			//	return dt.DefaultView;
			//}
			//else
			//{
			//	return null;
			//}
		}

		protected virtual void OnPromptConstData( PromptConstDataArgs e )
		{
			if( PromptConstData != null )
			{
				PromptConstData( this, e );
			}
		}

		private DataView GetListDataByPrompt()
		{
			if( this.LBViewType <= 0 )
			{
				return null;
			}

			// 创建商业对象
			//ERPBizObjBase iBizObj = new ERPBizObjBase( PromptBizObjID );

			//// 查询条件 Visibility <> 0 部分
			//DataTable dtCriteria = iBizObj.GetConfigTable( TS.Win.ConfigData.RequestBizObjData.MC_strBizCriteria );
			//if( !dtCriteria.Columns.Contains( "ListText" ) )
			//{
			//	dtCriteria.Columns.Add( "ListText", typeof( string ) );
			//}
			//if( !dtCriteria.Columns.Contains( "ListTextTo" ) )
			//{
			//	dtCriteria.Columns.Add( "ListTextTo", typeof( string ) );
			//}
			//if( !dtCriteria.Columns.Contains( "ListNCode" ) )
			//{
			//	dtCriteria.Columns.Add( "ListNCode", typeof( string ) );
			//}
			//if( !dtCriteria.Columns.Contains( "ListNCodeTo" ) )
			//{
			//	dtCriteria.Columns.Add( "ListNCodeTo", typeof( string ) );
			//}

			//for( int i = 0, j = dtCriteria.Rows.Count; i < j; i++ )
			//{
			//	DataRow drField = dtCriteria.Rows[i];
			//	bool bVisible = Convert.ToBoolean( drField["Visible"] );
			//	bool bVisibility = Convert.ToBoolean( drField["Visibility"] );
			//	if( bVisible || bVisibility )
			//	{
			//		// DefaultValue
			//		object objDefaultValue = drField["DefaultValue"];
			//		if( objDefaultValue != DBNull.Value )
			//		{
			//			drField["Value"] = objDefaultValue;
			//			drField["ListText"] = objDefaultValue.ToString();
			//		}
			//	}
			//}

			// 触发事件
			// NCodeClassID，增加 NCodeClassID 及 NCodeIsLeaf 条件
			string strCriteriaBase = "";
			//if( NCodeClassID > 0 )
			//{
			//	StringBuilder criteria = new StringBuilder();
			//	TS.Win.BizObj.BizObjHelper.AddServerCriteria( ref criteria, "NCodeClassID=", NCodeClassID.ToString(), false, false );
			//	TS.Win.BizObj.BizObjHelper.AddServerCriteria( ref criteria, "", "NCodeIsLeaf<>0", false, false );

			//	if( !this.IncludeNCodeForbidden )
			//	{
			//		TS.Win.BizObj.BizObjHelper.AddServerCriteria( ref criteria, "", "NCodeForbidden=0", false, false );
			//	}

			//	strCriteriaBase = criteria.ToString();
			//}

			string strCriteriaCustomer = "";
			bool bUseCriteriaCustomerOnly = false;
			if( PromptBizObjCreated != null )
			{
				PromptBizObjCreatedArgs args = new PromptBizObjCreatedArgs(  enPromptBizObjCreatedAction.GetListData, strCriteriaBase );
				PromptBizObjCreated( this, args );
				strCriteriaCustomer = args.CriteriaCustomer;
				bUseCriteriaCustomerOnly = args.UseCriteriaCustomerOnly;
				strCriteriaBase = args.CriteriaBase;
			}

			// 最终的查询条件
			if( !bUseCriteriaCustomerOnly )
			{
				if( !string.IsNullOrEmpty( strCriteriaCustomer ) )
				{
					strCriteriaCustomer = "(" + strCriteriaCustomer + ")";
				}
				if( !string.IsNullOrEmpty( strCriteriaBase ) )
				{
					strCriteriaBase = "(" + strCriteriaBase + ")";
				}
				if( !string.IsNullOrEmpty( strCriteriaCustomer ) && !string.IsNullOrEmpty( strCriteriaBase ) )
				{
					strCriteriaCustomer += " AND ";
				}
				strCriteriaCustomer += strCriteriaBase;
			}

			// 执行查询
			//string strPKey;
			//int ErrorDataBase = 0;
			//enDataProxyError ErrorDataProxy = enDataProxyError.NoError;
			//string ErrorDescription = "";
			//int TotalRecordCount;
			//List<DataColumn> lstFinalAddColumns;

			//DataView dvResult = iBizObj.GetPage(
			//	this.NCodeClassID, false,
			//	iBizObj.TryCache, -1, -1, 0,
			//	true, false, "",
			//	"", strCriteriaCustomer, false, "", null,
			//	out TotalRecordCount, out strPKey, out lstFinalAddColumns,
			//	ref ErrorDataBase, ref ErrorDataProxy, ref ErrorDescription );

			//// 错误处理
			//TS.Win.Helper.WinFunction.ParseDBReturn( ErrorDataBase, ErrorDataProxy, ErrorDescription );

            DataView dvResult= ExecuteSQL.CallView(LBViewType, "", strCriteriaCustomer, LBSort).DefaultView;
            return dvResult;
		}

		private string mstrLastGetDataInputText = null;
		private bool mbResetInputPromptDataNextTime = false;	// 强制重新读取 InputPromptData

		public bool ResetInputPromptDataNextTime
		{
			get
			{
				return mbResetInputPromptDataNextTime;
			}
			set
			{
				mbResetInputPromptDataNextTime = value;
			}
		}

		protected override void OnInputPromptGetData( TSEditorInputPromptGetDataEventArgs e )
		{
			base.OnInputPromptGetData( e );

			if( e.Handled )
			{
				return;
			}

			bool bReGetData = true;

			// 总是重新读取，因为改成了在系统配置中加了配置项，最多提示的行数，如果在上次结果中取则会结果不全
			////if( mstrLastGetDataInputText != null &&
			////    !ResetInputPromptDataNextTime &&
			////    ( mstrLastGetDataInputText == "" || e.InputText.IndexOf( mstrLastGetDataInputText ) >= 0 ) &&
			////    this.DataSource != null )
			////{
			////    DataView dv = new DataView( this.DataSource.Table );
			////    dv.RowFilter = string.Format( "(Convert({0},'System.String') like '%{2}%' or Convert({1},'System.String') like '%{2}%')", this.CodeColumnName, this.TextColumnName, e.InputText );

			////    if( dv.Count > 0 )
			////    {
			////        e.PromptData = dv;
			////        bReGetData = false;
			////    }
			////}

			if( bReGetData )
			{
				#region -- 使用 FieldEntity 指定的配置查询 --

				int iLBViewType = this.LBViewType;
                if (iLBViewType > 0)
                {
                    string strCriteriaBase = "";

                    // 触发事件
                    bool bUseCriteriaCustomerOnly = false;
                    string strCriteriaCustomer = "";
                    if (PromptBizObjCreated != null)
                    {
                        PromptBizObjCreatedArgs args = new PromptBizObjCreatedArgs( enPromptBizObjCreatedAction.InputPromptStep, strCriteriaBase);
                        PromptBizObjCreated(this, args);
                        strCriteriaCustomer = args.CriteriaCustomer;
                        bUseCriteriaCustomerOnly = args.UseCriteriaCustomerOnly;
                        strCriteriaBase = args.CriteriaBase;
                    }

                    // 最终的查询条件
                    if (!bUseCriteriaCustomerOnly)
                    {
                        if (!string.IsNullOrEmpty(strCriteriaCustomer))
                        {
                            strCriteriaCustomer = "(" + strCriteriaCustomer + ")";
                        }
                        if (!string.IsNullOrEmpty(strCriteriaBase))
                        {
                            strCriteriaBase = "(" + strCriteriaBase + ")";
                        }
                        if (!string.IsNullOrEmpty(strCriteriaCustomer) && !string.IsNullOrEmpty(strCriteriaBase))
                        {
                            strCriteriaCustomer += " AND ";
                        }
                        strCriteriaCustomer += strCriteriaBase;
                    }

                    // 执行查询
                    //string strPKey;
                    //int ErrorDataBase = 0;
                    //enDataProxyError ErrorDataProxy = enDataProxyError.NoError;
                    //string ErrorDescription = "";
                    int TotalRecordCount;
                    List<DataColumn> lstFinalAddColumns;

                    // 某些程序通过 SelectedDataRow 读取返回数据，并访问其它非显示的列
                    string strSelectFields = "";
                    foreach (string strColName in this.ListColumns)
                    {
                        strSelectFields += (strSelectFields == "" ? "" : ",") + strColName;
                    }

                    List<string> lstTemp = new List<string>();
                    lstTemp.AddRange(this.ListColumns);

                    if (!string.IsNullOrEmpty(this.IDColumnName) && FindInList(lstTemp, this.IDColumnName) < 0)
                    {
                        strSelectFields += (strSelectFields == "" ? "" : ",") + this.IDColumnName;
                        lstTemp.Add(this.IDColumnName);
                    }

                    if (!string.IsNullOrEmpty(this.CodeColumnName) && FindInList(lstTemp, this.CodeColumnName) < 0)
                    {
                        strSelectFields += (strSelectFields == "" ? "" : ",") + this.CodeColumnName;
                        lstTemp.Add(this.CodeColumnName);
                    }

                    if (!string.IsNullOrEmpty(this.TextColumnName) && FindInList(lstTemp, this.TextColumnName) < 0)
                    {
                        strSelectFields += (strSelectFields == "" ? "" : ",") + this.TextColumnName;
                        lstTemp.Add(this.TextColumnName);
                    }

                    DataView dvResult = ExecuteSQL.CallView(LBViewType, strSelectFields, strCriteriaCustomer, LBSort).DefaultView;

                    // 返回参数
                    e.PromptData = dvResult;

                    mstrLastGetDataInputText = e.InputText;
                }

				#endregion -- 使用 FieldEntity 指定的配置查询 --
			}

			mbResetInputPromptDataNextTime = false;
		}

		protected override void OnInputPromptValidating( TSEditorInputPromptGetDataEventArgs e )
		{
			base.OnInputPromptValidating( e );
			bool bReGetData = true;

			if( bReGetData )
			{
				#region -- 使用 FieldEntity 指定的配置查询 --

				int iPromptBizObjID = this.LBViewType;
                if (iPromptBizObjID > 0)
                {
                    #region -- 基类的查询条件 --

                    string strCriteriaBase = "";

                    //if (e.GetDataByValue)
                    //{
                    //    TS.Win.BizObj.BizObjHelper.AddServerCriteria(
                    //        ref sb, "", string.Format("(cast({0} as nvarchar(100)) = '{1}')", this.IDColumnName, e.Value.ToString().TrimEnd()), false, false);
                    //}
                    //else
                    //{
                    //    if (string.IsNullOrEmpty(this.IDColumnName) ||
                    //        string.IsNullOrEmpty(this.CodeColumnName) ||
                    //        string.IsNullOrEmpty(this.TextColumnName))
                    //    {
                    //        //TS.Win.Helper.WinFunction.DevErrorPrompt(string.Format("请指定商业对象({0})的 CodeColumnName,TextColumnName。", iPromptBizObjID));
                    //    }

                    //    if ((TS.Win.Helper.WinFunction.TSPromptItemModel && iNCodeClassID == TS.Win.Helper.WinFunction.MC_iItemNCodeClassID)
                    //        || (mbAlwaysPromptItemModel && this.ListColumns.Contains(TS.Win.Helper.WinFunction.MC_strItemModelFieldName)))
                    //    {
                    //        TS.Win.BizObj.BizObjHelper.AddServerCriteria(
                    //            ref sb, "",
                    //            string.Format("(cast({0} as nvarchar(100)) = '{2}' or cast({1} as nvarchar(100)) = '{2}' or {3} = '{2}')",
                    //                this.CodeColumnName, this.TextColumnName, e.InputText, TS.Win.Helper.WinFunction.MC_strItemModelFieldName),
                    //            false, false);
                    //    }
                    //    else if (TS.Win.Helper.WinFunction.TSPromptCustomerShortName && iNCodeClassID == TS.Win.Helper.WinFunction.MC_iCustomerClassID)
                    //    {
                    //        TS.Win.BizObj.BizObjHelper.AddServerCriteria(
                    //            ref sb, "",
                    //            string.Format("(cast({0} as nvarchar(100)) = '{2}' or cast({1} as nvarchar(100)) = '{2}' or {3} = '{2}')",
                    //                this.CodeColumnName, this.TextColumnName, e.InputText, TS.Win.Helper.WinFunction.MC_strShortNameFieldName),
                    //            false, false);
                    //    }
                    //    else if (TS.Win.Helper.WinFunction.TSPromptSupplierShortName && iNCodeClassID == TS.Win.Helper.WinFunction.MC_iSupplierClassID)
                    //    {
                    //        TS.Win.BizObj.BizObjHelper.AddServerCriteria(
                    //            ref sb, "",
                    //            string.Format("(cast({0} as nvarchar(100)) = '{2}' or cast({1} as nvarchar(100)) = '{2}' or {3} = '{2}')",
                    //                this.CodeColumnName, this.TextColumnName, e.InputText, TS.Win.Helper.WinFunction.MC_strShortNameFieldName),
                    //            false, false);
                    //    }
                    //    else
                    //    {
                    //        TS.Win.BizObj.BizObjHelper.AddServerCriteria(
                    //            ref sb, "", string.Format("(cast({0} as nvarchar(100)) = '{2}' or cast({1} as nvarchar(100)) = '{2}')", this.CodeColumnName, this.TextColumnName, e.InputText), false, false);
                    //    }
                    //}

                    //strCriteriaBase = sb.ToString();

                    #endregion -- 基类的查询条件 --

                    // 触发事件
                    string strCriteriaCustomer = "";
                    bool bUseCriteriaCustomerOnly = false;
                    if (PromptBizObjCreated != null)
                    {
                        PromptBizObjCreatedArgs args = new PromptBizObjCreatedArgs( enPromptBizObjCreatedAction.InputPromptValidating, strCriteriaBase);
                        PromptBizObjCreated(this, args);
                        strCriteriaCustomer = args.CriteriaCustomer;
                        bUseCriteriaCustomerOnly = args.UseCriteriaCustomerOnly;
                        strCriteriaBase = args.CriteriaBase;
                    }

                    // 最终的查询条件
                    if (!bUseCriteriaCustomerOnly)
                    {
                        if (!string.IsNullOrEmpty(strCriteriaCustomer))
                        {
                            strCriteriaCustomer = "(" + strCriteriaCustomer + ")";
                        }
                        if (!string.IsNullOrEmpty(strCriteriaBase))
                        {
                            strCriteriaBase = "(" + strCriteriaBase + ")";
                        }
                        if (!string.IsNullOrEmpty(strCriteriaCustomer) && !string.IsNullOrEmpty(strCriteriaBase))
                        {
                            strCriteriaCustomer += " AND ";
                        }
                        strCriteriaCustomer += strCriteriaBase;
                    }

                    // 执行查询
                    DataView dvResult = null;
                    try
                    {
                        dvResult = ExecuteSQL.CallView(LBViewType, "", strCriteriaCustomer, LBSort).DefaultView;
                    }
                    catch(Exception ex)
                    {
                        // 返回参数
                        e.PromptData = null;
                        e.SetError(true, ex.Message);
                        return;
                    }

                    // 查找是否有完全匹配
                    // 如果多于一行，则优先查找编码完全匹配的
                    if (dvResult.Count > 1)
                    {
                        dvResult.RowFilter = this.CodeColumnName + "='" + e.InputText + "'";

                        // 如果不是编码完全匹配，则查找名称完全匹配
                        if (dvResult.Count == 0)
                        {
                            dvResult.RowFilter = this.TextColumnName + "='" + e.InputText + "'";

                            // 如果不是名称完全匹配，则查找完全匹配
                            //if (dvResult.Count == 0)
                            //{
                            //    if ((TS.Win.Helper.WinFunction.TSPromptItemModel && iNCodeClassID == TS.Win.Helper.WinFunction.MC_iItemNCodeClassID)
                            //        || (mbAlwaysPromptItemModel && this.ListColumns.Contains(TS.Win.Helper.WinFunction.MC_strItemModelFieldName)))
                            //    {
                            //        dvResult.RowFilter = TS.Win.Helper.WinFunction.MC_strItemModelFieldName + "='" + e.InputText + "'";
                            //    }
                            //    else if (TS.Win.Helper.WinFunction.TSPromptCustomerShortName && iNCodeClassID == TS.Win.Helper.WinFunction.MC_iCustomerClassID)
                            //    {
                            //        dvResult.RowFilter = TS.Win.Helper.WinFunction.MC_strShortNameFieldName + "='" + e.InputText + "'";
                            //    }
                            //    else if (TS.Win.Helper.WinFunction.TSPromptSupplierShortName && iNCodeClassID == TS.Win.Helper.WinFunction.MC_iSupplierClassID)
                            //    {
                            //        dvResult.RowFilter = TS.Win.Helper.WinFunction.MC_strShortNameFieldName + "='" + e.InputText + "'";
                            //    }
                            //}
                        }

                        if (dvResult.Count == 0 && dvResult.RowFilter != "")
                        {
                            dvResult.RowFilter = "";
                        }
                    }

                    // 返回参数
                    e.PromptData = dvResult;

                    mstrLastGetDataInputText = e.InputText;
                }

				#endregion -- 使用 FieldEntity 指定的配置查询 --
			}

			mbResetInputPromptDataNextTime = false;
		}

        protected internal override void OnTSEditorSetValue(TSEditorSetValueEventArgs args)
        {
            base.OnTSEditorSetValue(args);

            #region -- 使用 FieldEntity 指定的配置查询 --

            if (args.ToSetValue == null || args.ToSetValue.ToString().Trim() == "")
            {
                return;
            }

            int iPromptBizObjID = this.LBViewType;
            if (iPromptBizObjID > 0)
            {
                #region -- 基类的查询条件 --

                string strCriteriaBase = "";
                StringBuilder sb = new StringBuilder();
                //int iNCodeClassID = this.NCodeClassID;
                //StringBuilder sb = new StringBuilder();
                //if (iNCodeClassID > 0)
                //{
                //    TS.Win.BizObj.BizObjHelper.AddServerCriteria(ref sb, "NCodeClassID=", iNCodeClassID.ToString(), false, false);
                //    TS.Win.BizObj.BizObjHelper.AddServerCriteria(ref sb, "", "NCodeIsLeaf<>0", false, false);

                //    // 设置值时不判断
                //    ////TS.Win.BizObj.BizObjHelper.AddServerCriteria( ref sb, "", "NCodeForbidden=0", false, false );
                //}

                switch (args.SetByMember)
                {
                    case enTSTextBoxMemberType.ID:
                        //TS.Win.BizObj.BizObjHelper.AddServerCriteria(
                        //    ref sb, this.IDColumnName + "=", args.ToSetValue.ToString(), false, false);
                        sb.AppendLine(this.IDColumnName + "=" + args.ToSetValue.ToString());
                        break;

                    case enTSTextBoxMemberType.Code:
                        //TS.Win.BizObj.BizObjHelper.AddServerCriteria(
                        //    ref sb, this.CodeColumnName + "=", args.ToSetValue.ToString(), true, false);
                        sb.AppendLine(this.CodeColumnName + "='" + args.ToSetValue.ToString() + "'");
                        break;

                    case enTSTextBoxMemberType.Text:
                        //TS.Win.BizObj.BizObjHelper.AddServerCriteria(
                        //    ref sb, this.TextColumnName + "=", args.ToSetValue.ToString(), true, false);
                        sb.AppendLine(this.TextColumnName + "='" + args.ToSetValue.ToString() + "'");
                        break;
                }

                strCriteriaBase = sb.ToString();

                #endregion -- 基类的查询条件 --

                // 触发事件
                string strCriteriaCustomer = "";
                bool bUseCriteriaCustomerOnly = false;
                if (PromptBizObjCreated != null)
                {
                    PromptBizObjCreatedArgs argsPrompt = new PromptBizObjCreatedArgs(enPromptBizObjCreatedAction.TSEditorSetValue, strCriteriaBase);
                    PromptBizObjCreated(this, argsPrompt);
                    strCriteriaCustomer = argsPrompt.CriteriaCustomer;
                    bUseCriteriaCustomerOnly = argsPrompt.UseCriteriaCustomerOnly;
                    strCriteriaBase = argsPrompt.CriteriaBase;
                }

                // 最终的查询条件
                if (!bUseCriteriaCustomerOnly)
                {
                    if (!string.IsNullOrEmpty(strCriteriaCustomer))
                    {
                        strCriteriaCustomer = "(" + strCriteriaCustomer + ")";
                    }
                    if (!string.IsNullOrEmpty(strCriteriaBase))
                    {
                        strCriteriaBase = "(" + strCriteriaBase + ")";
                    }
                    if (!string.IsNullOrEmpty(strCriteriaCustomer) && !string.IsNullOrEmpty(strCriteriaBase))
                    {
                        strCriteriaCustomer += " AND ";
                    }
                    strCriteriaCustomer += strCriteriaBase;
                }

                // 执行查询
                try
                {
                    DataView dvResult = ExecuteSQL.CallView(LBViewType, "", strCriteriaCustomer, LBSort).DefaultView;

                    // 返回参数
                    if (dvResult.Count == 1)
                    {
                        args.ValueDataRow = dvResult[0].Row;
                    }
                }
                catch (Exception ex)
                {

                }
            }

            #endregion -- 使用 FieldEntity 指定的配置查询 --
        }

        internal int FindInList(List<string> lstFields, string strFieldName)
        {
            if (lstFields == null)
            {
                return -1;
            }

            int index = -1;
            for (int i = 0, j = lstFields.Count; i < j; i++)
            {
                string temp = lstFields[i].Trim();
                if (temp.Equals(strFieldName.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
}
