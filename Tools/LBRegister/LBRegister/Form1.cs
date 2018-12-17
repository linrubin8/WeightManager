using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace LBRegister
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strRegisterStr = "";
            int iProductType = 0;
            
            #region -- 系统类型 --
            if (this.rbWeight.Checked)
            {
                string strModel = "";
                #region -- 地磅称重系统 --

                #region -- 子系统同步功能 --
                iProductType = 0;
                if (this.cbSynToServer.Checked)
                {
                    //strModel = "RegisterInfoJson={\"SynToServer\":\"1\"}";
                    if (strModel != "")
                        strModel += ",";
                    strModel += "\"SynToServer\":\"1\"";
                }
                else
                {
                    if (strModel != "")
                        strModel += ",";
                    strModel += "\"SynToServer\":\"0\"";
                }
                #endregion -- 子系统同步功能 --

                #region -- 订单移除功能 --

                if (strRegisterStr != "")
                    strRegisterStr += ";";
                if (this.cbRemoveInOutBill.Checked)
                {
                    if (strModel != "")
                        strModel += ",";
                    strModel += "\"RemoveInOutBill\":\"1\"";
                }
                else
                {
                    if (strModel != "")
                        strModel += ",";
                    strModel += "\"RemoveInOutBill\":\"0\"";
                }

                #endregion -- 订单移除功能 --

                #region -- 启用站点限制功能 --
                
                if (this.cbUseSessionLimit.Checked)
                {
                    if (strModel != "")
                        strModel += ",";
                    strModel += "\"UseSessionLimit\":\"1\"";
                }
                else
                {
                    if (strModel != "")
                        strModel += ",";
                    strModel += "\"UseSessionLimit\":\"0\"";
                }

                int iSessionCount = 0;
                int.TryParse(this.txtSessionCount.Text, out iSessionCount);

                if (strModel != "")
                    strModel += ",";
                strModel += "\"SessionLimitCount\":\""+ iSessionCount + "\"";

                #endregion -- 启用站点限制功能 --

                #endregion -- 地磅称重系统 --

                strRegisterStr= "RegisterInfoJson={"+ strModel+"}";
            }
            else if (this.rbGrooveWeight.Checked)
                iProductType = 1;
            else if (this.rbGrooveCount.Checked)
                iProductType = 2;

            if (strRegisterStr != "")
                strRegisterStr += ";";
            strRegisterStr += "ProductType=" + iProductType;

            #endregion
            
            string str = "Register=1;ProductType=" + iProductType + ";DeadLine=" + this.txtDeadLine.Text ;
            if (strRegisterStr != "")
            {
                str += ";";
                str += strRegisterStr;
            }
            this.txtRegister.Text = EncryptAes(str, "linrubin"+this.txtSeries.Text);
            Clipboard.SetText(this.txtRegister.Text);
        }

        #region aes实现

        /// <summary>
        /// Aes加解密钥必须32位
        /// </summary>
        public static string AesKey = "asekey32w";
        /// <summary>
        /// 获取Aes32位密钥
        /// </summary>
        /// <param name="key">Aes密钥字符串</param>
        /// <returns>Aes32位密钥</returns>
        static byte[] GetAesKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Aes密钥不能为空");
            }
            if (key.Length < 32)
            {
                // 不足32补全
                key = key.PadRight(32, '0');
            }
            if (key.Length > 32)
            {
                key = key.Substring(0, 32);
            }
            return Encoding.UTF8.GetBytes(key);
        }
        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥，长度必须32位</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptAes(string source, string key)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = GetAesKey(key);
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
                {
                    byte[] inputBuffers = Encoding.UTF8.GetBytes(source);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    aesProvider.Dispose();
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥，长度必须32位</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptAes(string source, string key)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = GetAesKey(key);
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(source);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return Encoding.UTF8.GetString(results);
                }
            }
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
