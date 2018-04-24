using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TS.Web
{
    public class DbConfig
    {
        /// <summary>
        /// TSMobileProduct网站数据库连接
        /// </summary>
        private static string MC_CmsConString4WEB;
        public static string MPCmsConString4WEB    //用户注册数据库
        {
            get
            {
                if (string.IsNullOrEmpty(MC_CmsConString4WEB))
                {
                    try
                    {
                        MC_CmsConString4WEB = ConfigurationManager.ConnectionStrings["RegisterConnectionString"].ConnectionString;
                    }
                    catch
                    {
                        throw new Exception("系统缺少连接信息配置<RegisterConnectionString>，或配置有误。");
                    }
                }
                return MC_CmsConString4WEB;
            }
        }

        private static string MC_CmsConString4Product;
        public static string MPCmsConString4Product    //产品数据库
        {
            get
            {
                if (string.IsNullOrEmpty(MC_CmsConString4Product))
                {
                    try
                    {
                        MC_CmsConString4Product = ConfigurationManager.ConnectionStrings["ProductConnectionString"].ConnectionString;
                    }
                    catch
                    {
                        throw new Exception("系统缺少连接信息配置<ProductConnectionString>，或配置有误。");
                    }
                }
                return MC_CmsConString4Product;
            }
        }

    }
}