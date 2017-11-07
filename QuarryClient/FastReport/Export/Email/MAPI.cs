using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace FastReport.Export.Email
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
  internal class MapiMessage
  {
    public int reserved;
    public string subject;
    public string noteText;
    public string messageType;
    public string dateReceived;
    public string conversationID;
    public int flags;
    public IntPtr originator;
    public int recipCount;
    public IntPtr recips;
    public int fileCount;
    public IntPtr files;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
  internal class MapiRecipDesc
  {
    public int reserved;
    public int recipClass;
    public string name;
    public string address;
    public int eIDSize;
    public IntPtr entryID;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
  internal class MapiFileDesc
  {
    public int reserved;
    public int flags;
    public int position;
    public string pathName;
    public string fileName;
    public IntPtr fileType;
  }

  /// <summary>
  /// Allows to send message using MAPI interface.
  /// </summary>
  public static class MAPI
  {
    #region static
    const int MapiLogonUI = 0x00000001;
    const int MAPI_DIALOG = 0x8;
    const int MAPI_TO = 1;
    const int MAPI_CC = 2;

    [DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
    private static extern int MAPISendMail(IntPtr session, IntPtr uiParam,
        MapiMessage message, int flags, int reserved);
    [DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
    private static extern int MAPILogon(IntPtr hwnd, string profileName, string password,
        int flags, int reserved, ref IntPtr session);
    [DllImport("MAPI32.DLL", CharSet = CharSet.Ansi)]
    private static extern int MAPILogoff(IntPtr session, IntPtr hwnd,
        int flags, int reserved);

    private static string[] errors = new string[] {
        "OK [0]", "User abort [1]", "General MAPI failure [2]", 
                "MAPI login failure [3]", "Disk full [4]", 
                "Insufficient memory [5]", "Access denied [6]", 
                "-unknown- [7]", "Too many sessions [8]", 
                "Too many files were specified [9]", 
                "Too many recipients were specified [10]", 
                "A specified attachment was not found [11]",
        "Attachment open failure [12]", 
                "Attachment write failure [13]", "Unknown recipient [14]", 
                "Bad recipient type [15]", "No messages [16]", 
                "Invalid message [17]", "Text too large [18]", 
                "Invalid session [19]", "Type not supported [20]", 
                "A recipient was specified ambiguously [21]", 
                "Message in use [22]", "Network failure [23]",
        "Invalid edit fields [24]", "Invalid recipients [25]", 
                "Not supported [26]" 
        };

    #endregion

    private static MapiMessage CreateMessage(string[] files, string mailSubject, string mailBody, 
      string[] recipientName, string[] recipientAddress)
    {
      MapiMessage msg = new MapiMessage();
      msg.subject = mailSubject;
      msg.noteText = mailBody;
      if (files.Length > 0)
      {
        msg.fileCount = files.Length;
        msg.files = GetFilesDesc(files);
      }
      if (recipientAddress.Length > 0)
      {
        msg.recipCount = recipientAddress.Length;
        msg.recips = GetRecipDesc(recipientName, recipientAddress);
      }
      return msg;
    }

    private static IntPtr GetFilesDesc(string[] files)
    {
      IntPtr ptra = AllocMemory(typeof(MapiFileDesc), files.Length);

      for (int i = 0; i < files.Length; i++)
      {
        MapiFileDesc fileDesc = new MapiFileDesc();
        fileDesc.position = -1;
        fileDesc.fileName = Path.GetFileName(files[i]);
        fileDesc.pathName = files[i];
        Marshal.StructureToPtr(fileDesc, OffsetPtr(ptra, typeof(MapiFileDesc), files.Length - 1 - i), false);
      }
      return ptra;
    }

    private static IntPtr GetRecipDesc(string[] recipientName, string[] recipientAddress)
    {
      IntPtr ptra = AllocMemory(typeof(MapiRecipDesc), recipientAddress.Length);

      for (int i = 0; i < recipientAddress.Length; i++)
      {
        MapiRecipDesc recipDesc = new MapiRecipDesc();
        recipDesc.recipClass = i == 0 ? MAPI_TO : MAPI_CC;
        if (recipientName.Length > i)
          recipDesc.name = recipientName[i];
        recipDesc.address = recipientAddress[i];
        Marshal.StructureToPtr(recipDesc, OffsetPtr(ptra, typeof(MapiRecipDesc), recipientAddress.Length - 1 - i), false);
      }
      return ptra;
    }

    private static IntPtr AllocMemory(Type structureType, int count)
    {
      return Marshal.AllocHGlobal(Marshal.SizeOf(structureType) * count);
    }

    private static IntPtr OffsetPtr(IntPtr ptr, Type structureType, int offset)
    {
      return (IntPtr)((long)ptr + offset * Marshal.SizeOf(structureType));
    }

    private static void Logoff(IntPtr hwnd, IntPtr session)
    {
      if (session != IntPtr.Zero)
      {
        MAPILogoff(session, hwnd, 0, 0);
        session = IntPtr.Zero;
      }
    }

    private static bool Logon(IntPtr hwnd, ref IntPtr session)
    {
      int error = MAPILogon(hwnd, null, null, 0, 0, ref session);
      if (error != 0)
        error = MAPILogon(hwnd, null, null, MapiLogonUI, 0, ref session);
      return error == 0;
    }

    private static void DisposeMessage(MapiMessage msg)
    {
      FreeMemory(msg.files, typeof(MapiFileDesc), msg.fileCount);
      FreeMemory(msg.recips, typeof(MapiRecipDesc), msg.recipCount);
      msg = null;
    }

    private static void FreeMemory(IntPtr ptr, Type structureType, int count)
    {
      if (ptr != IntPtr.Zero)
      {
        for (int i = 0; i < count; i++)
        {
          Marshal.DestroyStructure(OffsetPtr(ptr, structureType, i), structureType);
        }
        Marshal.FreeHGlobal(ptr);
      }
    }

    /// <summary>
    /// Sends a message.
    /// </summary>
    /// <param name="handle">Parent window handle.</param>
    /// <param name="files">Files to attach.</param>
    /// <param name="mailSubject">Email subject.</param>
    /// <param name="mailBody">Email body.</param>
    /// <param name="recipentName">Recipient names.</param>
    /// <param name="recipientAddress">Recipient addresses.</param>
    /// <returns>Error code. <b>0</b> if operation was completed succesfully.</returns>
    public static int SendMail(IntPtr handle, string[] files, string mailSubject, string mailBody, 
      string[] recipentName, string[] recipientAddress)
    {
      IntPtr session = IntPtr.Zero;
      int error = 0;
      if (Logon(handle, ref session))
      {
        MapiMessage msg = CreateMessage(files, mailSubject, mailBody, recipentName, recipientAddress);
        error = MAPISendMail(session, handle, msg, MAPI_DIALOG, 0);

        Logoff(handle, session);
        DisposeMessage(msg);
      }

      return error;
    }

    /// <summary>
    /// Returns a text describing an error.
    /// </summary>
    /// <param name="error">The error code.</param>
    /// <returns>The text describing an error.</returns>
    public static string GetErrorText(int error)
    {
      if (error <= 26)
        return errors[error];
      return "MAPI error [" + error.ToString() + "]";
    }
  }
}
