using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB.Web.ServiceMonitor
{
    public partial class frmWebLinkConfig : Form
    {
        public frmWebLinkConfig()
        {
            InitializeComponent();
        }

        private void frmWebLinkConfig_Load( object sender, EventArgs e )
        {
            try
            {
                WebLinkHelper.GetConfig();

                txtT3LoginName.Text = WebLinkHelper.T3LoginName;
                txtNetOrderUrl.Text = WebLinkHelper.WebServiceURL;
                cbTrantion.SelectedIndex = WebLinkHelper.SynchronType == 0 ? 0 : 1;
                cbSaveInfoUrl.SelectedIndex = WebLinkHelper.SaveInfo == 0 ? 0 : 1;

                WebLinkHelper.GetConnectionMsg( WebLinkHelper.WebConnectionString,
                    txtNetServer, txtNetDB, txtNetUser, txtNetPassword );
                WebLinkHelper.GetConnectionMsg( WebLinkHelper.T3ConnectionString,
                    txtT3Server, txtT3DB, txtT3User, txtT3Password );
            }
            catch( Exception err )
            {
                MonitorHelper.DealWithError( err );
            }
        }

        private void btnOk_Click( object sender, EventArgs e )
        {
            try
            {
                //获取相关网上订单配置信息
                string LoginName = this.txtT3LoginName.Text.ToString().Trim() + "\r\n";
                string OrderUrl = this.txtNetOrderUrl.Text.ToString().Trim() + "\r\n";
                string TrantionType = this.cbTrantion.SelectedItem.ToString();
                string SaveUrl = this.cbSaveInfoUrl.SelectedItem.ToString();
                //网上订单
                string WebServerUrl = "server=" + this.txtNetServer.Text.ToString().Trim() + ";";
                string WebDbName = "database=" + this.txtNetDB.Text.ToString().Trim() + ";";
                string WebUserName = "User ID=" + this.txtNetUser.Text.ToString().Trim() + ";";
                string WebPwd = "Password=" + this.txtNetPassword.Text.ToString().Trim() + ";";
                //M3
                string M3ServerUrl = "server=" + this.txtT3Server.Text.ToString().Trim() + ";";
                string M3DbName = "database=" + this.txtT3DB.Text.ToString().Trim() + ";";
                string M3UserName = "User ID=" + this.txtT3User.Text.ToString().Trim() + ";";
                string M3Pwd = "Password=" + this.txtT3Password.Text.ToString().Trim() + ";\r\n";

                string WebOrdersConfig = WebServerUrl + WebDbName + WebUserName + WebPwd + "TimeOut=30;\r\n";
                string M3OrdersConfig = M3ServerUrl + M3DbName + M3UserName + M3Pwd;

                //判断事务类型
                switch( TrantionType )
                {
                    case "分布式事务":
                        TrantionType = "0\r\n";
                        break;
                    case "网上订单及M3使用不同事务":
                        TrantionType = "1\r\n";
                        break;
                }
                //判断终端信息保存位置
                switch( SaveUrl )
                {
                    case "订单终端信息扩展表":
                        SaveUrl = "0\r\n";
                        break;
                    case "订单单头":
                        SaveUrl = "1\r\n";
                        break;
                }

                //将所有的配置信息放入泛型数组中
                List<string> OrderConfigLists = new List<string>();
                OrderConfigLists.Add( WebOrdersConfig );
                OrderConfigLists.Add( M3OrdersConfig );
                OrderConfigLists.Add( LoginName );
                OrderConfigLists.Add( OrderUrl );
                OrderConfigLists.Add( TrantionType );
                OrderConfigLists.Add( SaveUrl );

                WebLinkHelper.SaveConfig( OrderConfigLists );

                this.Close();
            }
            catch( Exception err )
            {
                MonitorHelper.DealWithError( err );
            }
        }

        private void btnCancle_Click( object sender, EventArgs e )
        {
            try
            {
                this.Close();
            }
            catch( Exception err )
            {
                MonitorHelper.DealWithError( err );
            }
        }
    }
}
