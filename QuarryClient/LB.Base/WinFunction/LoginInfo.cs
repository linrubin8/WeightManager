using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LB.WinFunction
{
    public class LoginInfo
    {
        private static string _LoginName="";
        public static string LoginName
        {
            get
            {
                return _LoginName;
            }
            set
            {
                _LoginName = value;
            }
        }

        private static int _UserID=0;
        public static int UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }

        private static int _UserType = 0;
        /// <summary>
        /// 用户类型：0地磅文员 1办公室文员 2系统管理员
        /// </summary>
        public static int UserType
        {
            get
            {
                return _UserType;
            }
            set
            {
                _UserType = value;
            }
        }

        private static DateTime _LoginTime = DateTime.Now;
        public static DateTime LoginTime
        {
            get
            {
                return _LoginTime;
            }
            set
            {
                _LoginTime = value;
            }
        }

        private static long mlSessionID = 0;
        public static long SessionID
        {
            get
            {
                return mlSessionID;
            }
            set
            {
                mlSessionID = value;
            }
        }


        public static string MachineIP
        {
            get
            {
                //事先不知道ip的个数，数组长度未知，因此用StringCollection储存  
                IPAddress[] localIPs;
                localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                StringCollection IpCollection = new StringCollection();
                foreach (IPAddress ip in localIPs)
                {
                    //根据AddressFamily判断是否为ipv4,如果是InterNetWorkV6则为ipv6  
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        IpCollection.Add(ip.ToString());
                }
                string[] IpArray = new string[IpCollection.Count];
                IpCollection.CopyTo(IpArray, 0);
                //return IpArray;

                string strIPAddress = "";
                /*string hostName = Dns.GetHostName();//本机名     
                System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6   
                if (addressList.Length > 0)
                {
                    strIPAddress = addressList[0].ToString();
                }*/
                if (IpArray.Length > 0)
                {
                    strIPAddress = IpArray[0];
                }
                return strIPAddress;
            }
        }

        private static string mMachineSeries = "";
        /// <summary>
        /// 电脑序列号
        /// </summary>
        public static string MachineSeries
        {
            get
            {
                if (mMachineSeries == "")
                {
                    mMachineSeries = HardwareInfo.GetCurrentVal();
                }
                return mMachineSeries;
            }
        }

        public static string MachineName
        {
            get
            {
                return Dns.GetHostName();//本机名     
            }
        }

        private static bool _IsVerifySuccess = false;
        /// <summary>
        /// 是否校验用户密码成功
        /// </summary>
        public static bool IsVerifySuccess
        {
            get
            {
                return _IsVerifySuccess;
            }
            set
            {
                _IsVerifySuccess = value;
            }
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public static string EncryptPassword(string strPassword)
        {
            return LBEncrypt.DESEncrypt(strPassword, "linrubin");
        }

        /// <summary>
        /// 校验登录密码
        /// </summary>
        /// <param name="strLoginName"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public static bool VerifyLogin(string strLoginName,string strPassword)
        {
            SessionID = 0;
            _IsVerifySuccess = false;
            bool bolIsLogin = false;
            DataTable dtUser = GetUserInfo(strLoginName);
            if (dtUser.Rows.Count == 0)
            {
                throw new Exception("该用户名称不存在！");
            }
            else
            {
                string strUserPassword = dtUser.Rows[0]["UserPassword"].ToString().TrimEnd();
                int iUserID = Convert.ToInt32(dtUser.Rows[0]["UserID"]);
                int iUserType = Convert.ToInt32(dtUser.Rows[0]["UserType"]);
                //string strImputPassword = LBEncrypt.DESEncrypt(strPassword, "linrubin");
                if (LBEncrypt.DESEncrypt(strPassword, "linrubin") == strUserPassword)
                {
                    bolIsLogin = true;
                    _UserID = iUserID;
                    _IsVerifySuccess = true;
                    _LoginName = strLoginName;
                    _LoginTime = DateTime.Now;
                    _IsVerifySuccess = true;
                    _UserType = iUserType;

                    bool bolIsOverFlow;
                    SessionID = GetSessionID(out bolIsOverFlow);

                    if (bolIsOverFlow)
                    {
                        throw new Exception("登录站点数超过系统限定的数量，登录失败！");
                    }
                }
                else
                {
                    throw new Exception("密码不正确，请重新输入！");
                }
            }

            return bolIsLogin;
        }

        /// <summary>
        /// 校验密码是否正确
        /// </summary>
        /// <param name="strLoginName"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public static bool VerifyPassword(int iUserID, string strPassword)
        {
            bool bolIsVerify = false;
            DataTable dtUser = GetUserInfo(iUserID);
            if (dtUser.Rows.Count == 0)
            {
                throw new Exception("该用户名称不存在！");
            }
            string strUserPassword = dtUser.Rows[0]["UserPassword"].ToString().TrimEnd();

            if (LBEncrypt.DESEncrypt(strPassword, "linrubin") == strUserPassword)
            {
                bolIsVerify = true;
            }
            return bolIsVerify;
        }


        private static DataTable GetUserInfo(string strLoginName)
        {
            DataTable dtUser = ExecuteSQL.CallView(100,false, "UserID,UserPassword,UserType", "LoginName='" + strLoginName + "'", "");
            return dtUser;
        }

        private static DataTable GetUserInfo(int iUserID)
        {
            DataTable dtUser = ExecuteSQL.CallView(100, "UserID,UserPassword", "UserID=" + iUserID, "");
            return dtUser;
        }

        private static long GetSessionID(out bool bolIsOverFlow)
        {
            long lSessionID = 0;
            bolIsOverFlow = false;
            LBDbParameterCollection parmCol = new LBDbParameterCollection();
            parmCol.Add(new LBParameter("ClientIP", enLBDbType.String, MachineIP));
            parmCol.Add(new LBParameter("ClientSerial", enLBDbType.String, MachineSeries));

            DataSet dsReturn;
            Dictionary<string, object> dictValue;
            ExecuteSQL.CallSP(30000,false, parmCol, out dsReturn, out dictValue);
            if (dictValue.ContainsKey("SessionID"))
            {
                long.TryParse(dictValue["SessionID"].ToString(),out lSessionID);
            }
            if (dictValue.ContainsKey("IsOverFlow"))
            {
                if (dictValue["IsOverFlow"].ToString() == "1")
                    bolIsOverFlow = true;
            }
            
            return lSessionID;
        }
    }
}
