using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FastReport.Export.Pdf
{
    public partial class PDFExport : ExportBase
    {
        byte[] PDF_PK = { 
            0x28, 0xBF, 0x4E, 0x5E, 0x4E, 0x75, 0x8A, 
            0x41, 0x64, 0x00, 0x4E, 0x56, 0xFF, 0xFA, 
            0x01, 0x08, 0x2E, 0x2E, 0x00, 0xB6, 0xD0, 
            0x68, 0x3E, 0x80, 0x2F, 0x0C, 0xA9, 0xFE, 
            0x64, 0x53, 0x69, 0x7A };

        private long FEncBits;
        private byte[] FEncKey;
        private byte[] FOPass;
        private byte[] FUPass;

        private string RC4CryptString(string source, byte[] key, long id)
        {
            byte[] k = new byte[21];
            Array.Copy(key, 0, k, 0, 16);
            k[16] = (byte)id;
            k[17] = (byte)(id >> 8);
            k[18] = (byte)(id >> 16);
            byte[] s = new byte[16];
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            Array.Copy(md5.ComputeHash(k), s, 16);
            RC4 rc4 = new RC4();
            rc4.Start(s);
            byte[] src = ExportUtils.StringToByteArray(source);
            byte[] target = rc4.Crypt(src);
            return ExportUtils.StringFromByteArray(target);
        }

        private void RC4CryptStream(Stream source, Stream target, byte[] key, long id)
        {
            byte[] k = new byte[21];
            Array.Copy(key, 0, k, 0, 16);
            k[16] = (byte)id;
            k[17] = (byte)(id >> 8);
            k[18] = (byte)(id >> 16);

            byte[] s = new byte[16];
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            Array.Copy(md5.ComputeHash(k), s, 16);

            byte[] buffSource = new byte[source.Length];
            source.Position = 0;
            source.Read(buffSource, 0, (int)source.Length);

            RC4 rc4 = new RC4();
            rc4.Start(s);
            byte[] buffTarget = rc4.Crypt(buffSource);
            target.Write(buffTarget, 0, buffTarget.Length);
        }

        private byte[] PadPassword(string password)
        {
            byte[] p = ExportUtils.StringToByteArray(password);
            byte[] result = new byte[32];
            int l = p.Length < 32 ? p.Length : 32;
            for (int i = 0; i < l; i++)
                result[i] = p[i];
            if (l < 32)
                for (int i = l; i < 32; i++)
                    result[i] = PDF_PK[i - l];
            return result;
        }

        private void PrepareKeys()
        {
            FEncBits = -64; // 0xFFFFFFC0;
            if (FAllowPrint)
                FEncBits += 4;
            if (FAllowModify)
                FEncBits += 8;
            if (FAllowCopy)
                FEncBits += 16;
            if (FAllowAnnotate)
                FEncBits += 32;

            // OWNER KEY            
            if (String.IsNullOrEmpty(FOwnerPassword))
                FOwnerPassword = FUserPassword;

            byte[] p = PadPassword(FOwnerPassword);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            md5.Initialize();
            byte[] s = new byte[16];
            md5.TransformBlock(p, 0, 32, p, 0);
            md5.TransformFinalBlock(p, 0, 0);
            Array.Copy(md5.Hash, s, 16);
            for (byte i = 1; i <= 50; i++)
            {
                md5.Initialize();
                Array.Copy(md5.ComputeHash(s), 0, s, 0, 16);
            }

            RC4 rc4 = new RC4();
            p = PadPassword(FUserPassword);
            rc4.Start(s);
            byte[] s1 = rc4.Crypt(p);
            byte[] p1 = new byte[16];
            for (byte i = 1; i <= 19; i++)
            {
                for (byte j = 1; j <= 16; j++)
                    p1[j - 1] = (byte)(s[j - 1] ^ i);
                rc4.Start(p1);
                s1 = rc4.Crypt(s1);
            }
            FOPass = new byte[32];
            Array.Copy(s1, FOPass, 32);

            // ENCRYPTION KEY
            p = PadPassword(FUserPassword);

            md5.Initialize();
            md5.TransformBlock(p, 0, 32, p, 0);
            md5.TransformBlock(FOPass, 0, 32, FOPass, 0);

            byte[] ext = new byte[4];
            ext[0] = (byte)FEncBits;
            ext[1] = (byte)(FEncBits >> 8);
            ext[2] = (byte)(FEncBits >> 16);
            ext[3] = (byte)(FEncBits >> 24);
            md5.TransformBlock(ext, 0, 4, ext, 0);

            byte[] fid = new byte[16];
            for (byte i = 1; i <= 16; i++)
                fid[i - 1] = Convert.ToByte(String.Concat(FFileID[i * 2 - 2], FFileID[i * 2 - 1]), 16);
            md5.TransformBlock(fid, 0, 16, fid, 0);
            md5.TransformFinalBlock(ext, 0, 0);
            Array.Copy(md5.Hash, 0, s, 0, 16);

            for (byte i = 1; i <= 50; i++)
            {
                md5.Initialize();
                Array.Copy(md5.ComputeHash(s), 0, s, 0, 16);
            }
            FEncKey = new byte[16];
            Array.Copy(s, 0, FEncKey, 0, 16);

            // USER KEY
            md5.Initialize();
            md5.TransformBlock(PDF_PK, 0, 32, PDF_PK, 0);
            md5.TransformBlock(fid, 0, 16, fid, 0);
            md5.TransformFinalBlock(fid, 0, 0);
            Array.Copy(md5.Hash, s, 16);

            s1 = new byte[16];
            Array.Copy(FEncKey, s1, 16);

            rc4.Start(s1);
            s = rc4.Crypt(s);

            p1 = new byte[16];
            for (byte i = 1; i <= 19; i++)
            {
                for (byte j = 1; j <= 16; j++)
                    p1[j - 1] = (byte)(s1[j - 1] ^ i);
                rc4.Start(p1);
                s = rc4.Crypt(s);
            }
            FUPass = new byte[32];
            Array.Copy(s, 0, FUPass, 0, 16);
        }

        private string GetEncryptionDescriptor()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/Encrypt <<");
            sb.AppendLine("/Filter /Standard");
            sb.AppendLine("/V 2");
            sb.AppendLine("/R 3");
            sb.AppendLine("/Length 128");
            sb.AppendLine("/P " + FEncBits.ToString());
            sb.Append("/O (");
            EscapeSpecialChar(ExportUtils.StringFromByteArray(FOPass), sb);
            sb.AppendLine(")");
            sb.Append("/U (");
            EscapeSpecialChar(ExportUtils.StringFromByteArray(FUPass), sb);
            sb.AppendLine(")");
            sb.AppendLine(">>");
            return sb.ToString();
        }

    }

    internal class RC4
    {
        private byte[] fKey;

        public void Start(byte[] key)
        {
            byte[] k = new byte[256];
            int l = key.GetLength(0);
            if (key.Length > 0 && l <= 256)
            {
                for (int i = 0; i < 256; i++)
                {
                    fKey[i] = (byte)i;
                    k[i] = key[i % l];
                }
            }

            byte j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (byte)(j + fKey[i] + k[i]);
                byte tmp = fKey[i];
                fKey[i] = fKey[j];
                fKey[j] = tmp;
            }
        }

        public byte[] Crypt(byte[] source)
        {
            byte i = 0;
            byte j = 0;
            int l = source.GetLength(0);
            byte[] result = new byte[l];
            for (int k = 0; k < l; k++)
            {
                i = (byte)(i + 1);
                j = (byte)(j + fKey[i]);
                byte tmp = fKey[i];
                fKey[i] = fKey[j];
                fKey[j] = tmp;
                result[k] = (byte)(source[k] ^ fKey[(byte)(fKey[i] + fKey[j])]);
            }
            return result;
        }

        public RC4()
        {
            fKey = new byte[256];
        }
    }
}
