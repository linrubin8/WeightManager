using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace FastReport.Utils
{
  /// <summary>
  /// Contains methods used to crypt/decrypt a data.
  /// </summary>
  public static class Crypter
  {
    private static string FDefaultPassword = typeof(Crypter).FullName;
    
    /// <summary>
    /// Sets the password that is used to crypt connection strings stored in a report.
    /// </summary>
    /// <remarks>
    /// See the <see cref="FastReport.Data.DataConnectionBase.ConnectionString"/> property for more details.
    /// </remarks>
    public static string DefaultPassword
    {
      set { FDefaultPassword = value; }
    }
    
    /// <summary>
    /// Crypts a stream using specified password.
    /// </summary>
    /// <param name="dest">The destination stream that will receive the crypted data.</param>
    /// <param name="password">The password.</param>
    /// <returns>The stream that you need to write to.</returns>
    /// <remarks>
    /// Pass the stream you need to write to, to the <b>dest</b> parameter. Write your data to the 
    /// stream that this method returns. When you close this stream, the <b>dest</b> stream will be
    /// closed too and contains the crypted data.
    /// </remarks>
    public static Stream Encrypt(Stream dest, string password)
    {
      PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, Encoding.UTF8.GetBytes("Salt"));
      RijndaelManaged rm = new RijndaelManaged();
      rm.Padding = PaddingMode.ISO10126;
      ICryptoTransform encryptor = rm.CreateEncryptor(pdb.GetBytes(16), pdb.GetBytes(16));

      // write "rij" signature
      dest.Write(new byte[] { 114, 105, 106 }, 0, 3);
      return new CryptoStream(dest, encryptor, CryptoStreamMode.Write);
    }

    /// <summary>
    /// Decrypts a stream using specified password.
    /// </summary>
    /// <param name="source">Stream that contains crypted data.</param>
    /// <param name="password">The password.</param>
    /// <returns>The stream that contains decrypted data.</returns>
    /// <remarks>
    /// You should read from the stream that this method returns.
    /// </remarks>
    public static Stream Decrypt(Stream source, string password)
    {
      PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, Encoding.UTF8.GetBytes("Salt"));
      RijndaelManaged rm = new RijndaelManaged();
      rm.Padding = PaddingMode.ISO10126;
      ICryptoTransform decryptor = rm.CreateDecryptor(pdb.GetBytes(16), pdb.GetBytes(16));

      // check "rij" signature
      int byte1 = source.ReadByte();
      int byte2 = source.ReadByte();
      int byte3 = source.ReadByte();
      if (byte1 == 114 && byte2 == 105 && byte3 == 106)
        return new CryptoStream(source, decryptor, CryptoStreamMode.Read);
      source.Position -= 3;
      return null;
    }
    
    /// <summary>
    /// Checks if the stream contains a crypt signature.
    /// </summary>
    /// <param name="stream">Stream to check.</param>
    /// <returns><b>true</b> if stream is crypted.</returns>
    public static bool IsStreamEncrypted(Stream stream)
    {
      // check "rij" signature
      int byte1 = stream.ReadByte();
      int byte2 = stream.ReadByte();
      int byte3 = stream.ReadByte();
      stream.Position -= 3;
      return byte1 == 114 && byte2 == 105 && byte3 == 106;
    }
    
    /// <summary>
    /// Encrypts the string using the default password.
    /// </summary>
    /// <param name="data">String to encrypt.</param>
    /// <returns>The encrypted string.</returns>
    /// <remarks>
    /// The password used to encrypt a string can be set via <see cref="DefaultPassword"/> property.
    /// You also may use the <see cref="EncryptString(string, string)"/> method if you want to
    /// specify another password.
    /// </remarks>
    public static string EncryptString(string data)
    {
      return EncryptString(data, FDefaultPassword);
    }

    /// <summary>
    /// Encrypts the string using specified password.
    /// </summary>
    /// <param name="data">String to encrypt.</param>
    /// <param name="password">The password.</param>
    /// <returns>The encrypted string.</returns>
    public static string EncryptString(string data, string password)
    {
      if (String.IsNullOrEmpty(data) || String.IsNullOrEmpty(password))
        return data;

      using (MemoryStream stream = new MemoryStream())
      {
        using (Stream cryptedStream = Encrypt(stream, password))
        {
          byte[] bytes = Encoding.UTF8.GetBytes(data);
          cryptedStream.Write(bytes, 0, bytes.Length);
        }  
          
        return "rij" + Convert.ToBase64String(stream.ToArray());
      }
    }

    /// <summary>
    /// Decrypts the string using the default password.
    /// </summary>
    /// <param name="data">String to decrypt.</param>
    /// <returns>The decrypted string.</returns>
    /// <remarks>
    /// The password used to decrypt a string can be set via <see cref="DefaultPassword"/> property.
    /// You also may use the <see cref="DecryptString(string, string)"/> method if you want to
    /// specify another password.
    /// </remarks>
    public static string DecryptString(string data)
    {
      return DecryptString(data, FDefaultPassword);
    }

    /// <summary>
    /// Decrypts the string using specified password.
    /// </summary>
    /// <param name="data">String to decrypt.</param>
    /// <param name="password">The password.</param>
    /// <returns>The decrypted string.</returns>
    public static string DecryptString(string data, string password)
    {
      if (String.IsNullOrEmpty(data) || String.IsNullOrEmpty(password) || !data.StartsWith("rij"))
        return data;

      data = data.Substring(3);
      using (Stream stream = Converter.FromString(typeof(Stream), data) as Stream)
      {
        using (Stream decryptedStream = Decrypt(stream, password))
        {
          byte[] bytes = new byte[data.Length];
          int bytesRead = decryptedStream.Read(bytes, 0, bytes.Length);
          return Encoding.UTF8.GetString(bytes, 0, bytesRead);
        }
      }
    }

    private static string defaultDESkey = "FastReport .NET 2016";

    /// <summary>
    /// Computes hash of specified stream. Initial position in stream will be saved.
    /// </summary>
    /// <param name="input">Initial stream</param>
    /// <returns></returns>
    public static string ComputeHash(Stream input)
    {
        //MACTripleDES des = new MACTripleDES(Encoding.UTF8.GetBytes(defaultDESkey)); //FIPS HMACSHA1 
        HMACSHA1 des = new HMACSHA1(Encoding.UTF8.GetBytes(defaultDESkey));
        long prevPos = input.Position;
        input.Position = 0;
        byte[] hash = des.ComputeHash(input);
        input.Position = prevPos;
        return BitConverter.ToString(hash).Replace("-", String.Empty);
    }

    /// <summary>
    /// Computes hash of specified array. 
    /// </summary>
    /// <param name="input">Initial array</param>
    /// <returns></returns>
    public static string ComputeHash(byte[] input)
    {
        //MACTripleDES des = new MACTripleDES(Encoding.UTF8.GetBytes(defaultDESkey));
        HMACSHA1 des = new HMACSHA1(Encoding.UTF8.GetBytes(defaultDESkey));
        byte[] hash = des.ComputeHash(input);        
        return BitConverter.ToString(hash).Replace("-", String.Empty);
    }

    /// <summary>
    /// Computes hash of specified array. 
    /// </summary>
    /// <param name="input">Initial array</param>
    /// <returns></returns>
    public static string ComputeHash(string input)
    {
        //MACTripleDES des = new MACTripleDES(Encoding.UTF8.GetBytes(defaultDESkey));
        HMACSHA1 des = new HMACSHA1(Encoding.UTF8.GetBytes(defaultDESkey));        
        byte[] hash = des.ComputeHash(Encoding.UTF8.GetBytes(input));     
        return BitConverter.ToString(hash).Replace("-", String.Empty);
    }
  }
}
