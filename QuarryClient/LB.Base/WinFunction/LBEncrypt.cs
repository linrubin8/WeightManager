using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LB.WinFunction
{
    public class LBEncrypt
    {
        public static string GetDiskID()
        {
            try
            {
                return HardwareInfo.GetCurrentVal();
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }
        ///
        /// DES 加密(数据加密标准，速度较快，适用于加密大量数据的场合)
        ///
        /// 待加密的密文
        /// 加密的密钥
        /// returns
        public static string DESEncrypt(string EncryptString, string EncryptKey)
        {
            if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("密文不得为空")); }
            if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }
            if (EncryptKey.Length != 8) { throw (new Exception("密钥必须为8位")); }
            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            string m_strEncrypt = "";
            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();
            try
            {
                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);
                MemoryStream m_stream = new MemoryStream();
                CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);
                m_cstream.Write(m_btEncryptString, 0, m_btEncryptString.Length);
                m_cstream.FlushFinalBlock();
                m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());
                m_stream.Close(); m_stream.Dispose();
                m_cstream.Close(); m_cstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_DESProvider.Clear(); }
            return m_strEncrypt;
        }
        ///
        /// DES 解密(数据加密标准，速度较快，适用于加密大量数据的场合)
        ///
        /// 待解密的密文
        /// 解密的密钥
        /// returns
        public static string DESDecrypt(string DecryptString, string DecryptKey)
        {
            if (string.IsNullOrEmpty(DecryptString)) { throw (new Exception("密文不得为空")); }
            if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("密钥不得为空")); }
            if (DecryptKey.Length != 8) { throw (new Exception("密钥必须为8位")); }
            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            string m_strDecrypt = "";
            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();
            try
            {
                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);
                MemoryStream m_stream = new MemoryStream();
                CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);
                m_cstream.Write(m_btDecryptString, 0, m_btDecryptString.Length);
                m_cstream.FlushFinalBlock();
                m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());
                m_stream.Close(); m_stream.Dispose();
                m_cstream.Close(); m_cstream.Dispose();
            }
            catch (IOException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (ArgumentException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_DESProvider.Clear(); }
            return m_strDecrypt;
        }

        public string DESEncrypt(object strPassword, string v)
        {
            throw new NotImplementedException();
        }

        public static Dictionary<string,object> GetRegisterModelInfo(string strRegisterJson)
        {
            Dictionary<string, object> dictModel = new Dictionary<string, object>();
            //JObject jo = (JObject)JsonConvert.DeserializeObject(strRegisterJson);
            var o = JObject.Parse(strRegisterJson);

            foreach (JToken child in o.Children())
            {
                var property1 = child as JProperty;
                if (!dictModel.ContainsKey(property1.Name))
                {
                    dictModel.Add(property1.Name, property1.Value);
                }
            }
            return dictModel;
        }

        #region -- Aes加解 --

        public static bool IsRegister = false;
        public static DateTime DeadLine = DateTime.MinValue;
        public static int ProductType = -1;

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
                string strDecrypt = DecryptAes(str, "linrubin" + strDiskID);

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
                            //else if (key.Equals("RegisterInfoJson"))
                            //{
                            //    RegisterInfoJson = value;
                            //}
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

        #endregion -- Aes加解 --
    }

    internal class LBDESEncrypt
    {
        private string iv = "12345678";
        private string key = "12345678";
        private Encoding encoding = new UnicodeEncoding();
        private DES des;

        public LBDESEncrypt()
        {
            des = new DESCryptoServiceProvider();
        }

        /// <summary>
        /// 设置加密密钥
        /// </summary>
        public string EncryptKey
        {
            get { return this.key; }
            set
            {
                this.key = value;
            }
        }

        /// <summary>
        /// 要加密字符的编码模式
        /// </summary>
        public Encoding EncodingMode
        {
            get { return this.encoding; }
            set { this.encoding = value; }
        }

        /// <summary>
        /// 加密字符串并返回加密后的结果
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string EncryptString(string str)
        {
            byte[] ivb = Encoding.ASCII.GetBytes(this.iv);
            byte[] keyb = Encoding.ASCII.GetBytes(this.EncryptKey);//得到加密密钥
            byte[] toEncrypt = this.EncodingMode.GetBytes(str);//得到要加密的内容
            byte[] encrypted;
            ICryptoTransform encryptor = des.CreateEncryptor(keyb, ivb);
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();
            encrypted = msEncrypt.ToArray();
            csEncrypt.Close();
            msEncrypt.Close();
            return this.EncodingMode.GetString(encrypted);
        }
    }
}
