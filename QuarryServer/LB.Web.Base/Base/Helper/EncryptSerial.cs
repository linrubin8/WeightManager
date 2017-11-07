using LB.Web.Base.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LB.Web.Base.Base.Helper
{
    public class EncryptSerial
    {
        public static bool VerifyEncryptString(string strCurrentEncryptString)
        {
            HardwareSerial hardwareInfo = new HardwareSerial();
            string hardDiskID = hardwareInfo.GetHardDiskID();

            hardDiskID += "linrubin"+ hardDiskID;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] palindata = Encoding.Default.GetBytes(hardDiskID);//将要加密的字符串转换为字节数组
            byte[] encryptdata = md5.ComputeHash(palindata);//将字符串加密后也转换为字符数组
            string strEncrypt = Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为加密字符串
            if (strCurrentEncryptString == strEncrypt)
                return true;
            return false;
        }
    }
}
