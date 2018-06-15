using System;
using System.Collections.Generic;
using System.Text;

namespace LB.Controls.Args
{
	public delegate void PromptBizObjCreatedEventHandler( object sender, PromptBizObjCreatedArgs e );

	public enum enPromptBizObjCreatedAction
	{
		/// <summary>
		/// 下拉时读取数据
		/// </summary>
		GetListData,

		/// <summary>
		/// 输入提示时读取数据
		/// </summary>
		InputPromptStep,

		/// <summary>
		/// 可输入提示类型控件，光标离开校验读取数据
		/// </summary>
		InputPromptValidating,

		/// <summary>
		/// TSDataGridView 没有此项。是 TSEditor 赋值时读取数据
		/// </summary>
		TSEditorSetValue
	}

    public class PromptBizObjCreatedArgs
    {
        //private ERPBizObjBase miBizObj = null;
        private string mstrCriteriaCustomer = "";
        private enPromptBizObjCreatedAction mActionType;
        private bool mbUseCriteriaCustomerOnly = false;
        private string mstrCriteriaBase = "";

        public PromptBizObjCreatedArgs( enPromptBizObjCreatedAction actionType, string strCriteriaBase)
        {
            //miBizObj = iBizObj;
            mActionType = actionType;
            mstrCriteriaBase = strCriteriaBase;
        }

        //public ERPBizObjBase PromptBizObj
        //{
        //    get
        //    {
        //        return miBizObj;
        //    }
        //}

        public enPromptBizObjCreatedAction ActionType
        {
            get
            {
                return mActionType;
            }
        }

        /// <summary>
        /// 基类构建的查询条件
        /// </summary>
        public string CriteriaBase
        {
            get
            {
                return mstrCriteriaBase;
            }
            set
            {
                mstrCriteriaBase = value;
            }
        }

        /// <summary>
        /// 自定义查询条件
        /// </summary>
        public string CriteriaCustomer
        {
            get
            {
                return mstrCriteriaCustomer;
            }
            set
            {
                mstrCriteriaCustomer = value;
            }
        }

        /// <summary>
        /// 是否只使用自定义查询条件。是，则忽略基类构建的查询条件 CriteriaBase
        /// </summary>
        public bool UseCriteriaCustomerOnly
        {
            get
            {
                return mbUseCriteriaCustomerOnly;
            }
            set
            {
                mbUseCriteriaCustomerOnly = value;
            }
        }
    }
}
