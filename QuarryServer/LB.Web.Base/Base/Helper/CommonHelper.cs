using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

        #region -- 字符串压缩与解压 --
        public static string Compress(string text)
        {
            byte[] buffer = Encoding.Default.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;
            MemoryStream outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        public static string Decompress(string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);
                byte[] buffer = new byte[msgLength];
                ms.Position = 0;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }
                return Encoding.Default.GetString(buffer);
            }
        }
        #endregion
    }
}
