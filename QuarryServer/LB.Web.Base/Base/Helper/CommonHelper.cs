using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LB.Web.Base.Base.Helper
{
    public class CommonHelper
    {
        /// <summary>
        /// 用户级文件保存
        /// </summary>
        /// <param name="p_filePath">文件保存的路径</param>
        /// <param name="p_ufile">文件信息 （字节流）</param>
        /// <returns>是否保存成功</returns>
        public static void SaveFile(string p_filePath, byte[] p_ufile)
        {
            // code
            FileStream p_fw = new FileStream(p_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            p_fw.Write(p_ufile, 0, p_ufile.Length);
            p_fw.Close();
        }

        public static byte[] ReadFile(string fileName)
        {
            FileStream pFileStream = null;
            byte[] pReadByte = new byte[0];
            try
            {
                pFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(pFileStream);
                r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开  
                pReadByte = r.ReadBytes((int)r.BaseStream.Length);
                return pReadByte;
            }
            catch
            {
                return pReadByte;
            }
            finally
            {
                if (pFileStream != null)
                    pFileStream.Close();
            }

        }
    }
}
