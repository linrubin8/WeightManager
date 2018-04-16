using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LB.Web.Encrypt
{
    public class LBEncrypt
    {
        public static bool IsRegister = false;
        public static DateTime DeadLine = DateTime.MinValue;
        public static int ProductType=-1;
        public static string RegisterInfoJson = "";

        public static void Decrypt()
        {
            DeadLine = DateTime.MinValue;
            IsRegister = false;
            string str = "";

            string strIniPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LBTSR.ini");
            if (!File.Exists(strIniPath))
            {
                IsRegister = false;
                return;
            }
            else
            {
                IniClass ini = new IniClass(strIniPath);
                str = ini.ReadValue("TSR", "value");

                if (str == "" || str == null)
                {
                    IsRegister = false;
                    return;
                }
            }

            string strDiskID = GetDiskID();//硬盘ID
            //string strKey = "lin123ru456bin";
            //string strDesKey = strKey + strDiskID;
            try
            {
                string strDecrypt = DecryptAes(str, "linrubin"+strDiskID);

                string[] strSplits = strDecrypt.Split(';');
                foreach (string strSplit in strSplits)
                {
                    if (strSplit.Contains("="))
                    {
                        string[] strKeyValues = strSplit.Split('=');
                        if (strKeyValues.Length == 2)
                        {
                            string key = strKeyValues[0];
                            string value = strKeyValues[1];

                            if (key.Equals("Register"))
                            {
                                if (value.Equals("1"))
                                {
                                    IsRegister = true;
                                }
                            }
                            else if (key.Equals("DeadLine"))
                            {
                                DateTime.TryParse(value, out DeadLine);
                            }
                            else if (key.Equals("ProductType"))
                            {
                                int.TryParse(value, out ProductType);
                            }
                            else if (key.Equals("RegisterInfoJson"))
                            {
                                RegisterInfoJson = value;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 读取本地注册码
        /// </summary>
        /// <returns></returns>
        public static string GetRegisterCode()
        {
            string str = "";
            string strIniPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LBTSR.ini");
            if (File.Exists(strIniPath))
            {
                IniClass ini = new IniClass(strIniPath);
                str = ini.ReadValue("TSR", "value");
            }
            return str;
        }

        public static string GetDiskID()
        {
            try
            {
                return HardwareInfo.GetCurrentVal();
                //ManagementClass cimObject = new ManagementClass("Win32_PhysicalMedia");
                //ManagementObjectCollection moc = cimObject.GetInstances();
                //Dictionary<string, string> dict = new Dictionary<string, string>();
                //foreach (ManagementObject mo in moc)
                //{
                //    string tag = mo.Properties["Tag"].Value.ToString().ToLower().Trim();
                //    string hdId = (string)mo.Properties["SerialNumber"].Value ?? string.Empty;
                //    hdId = hdId.Trim();
                //    dict.Add(tag, hdId);
                //}
                //cimObject = new ManagementClass("Win32_OperatingSystem");
                //moc = cimObject.GetInstances();
                //string currentSysRunDisk = string.Empty;
                //foreach (ManagementObject mo in moc)
                //{
                //    currentSysRunDisk = Regex.Match(mo.Properties["Name"].Value.ToString().ToLower(), @"harddisk\d+").Value;
                //}
                //var results = dict.Where(x => Regex.IsMatch(x.Key, @"physicaldrive" + Regex.Match(currentSysRunDisk, @"\d+$").Value));
                //if (results.Any()) return results.ElementAt(0).Value;
                return "";
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }

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

    }
}
