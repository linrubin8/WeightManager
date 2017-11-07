using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Windows.Forms;
using FastReport.Utils;

namespace FastReport.Data.ConnectionEditors
{
  internal partial class OleDbConnectionEditor : ConnectionEditorBase
  {
    // imported from MS OLE DB Service Component 1.0 Type library
    [Guid("00000550-0000-0010-8000-00AA006D2EA4")]
    [ComImport]
    [TypeLibType((short)4160)]
    [DefaultMember("ConnectionString")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
    internal interface _Connection
    {
      [DispId(5)]
      void Close();
      object Execute([MarshalAs(UnmanagedType.BStr)] string CommandText, [Out] out object RecordsAffected, int Options);
      [DispId(7)]
      int BeginTrans();
      [DispId(8)]
      void CommitTrans();
      [DispId(9)]
      void RollbackTrans();
      [DispId(10)]
      void Open([MarshalAs(UnmanagedType.BStr)] string ConnectionString, [MarshalAs(UnmanagedType.BStr)] string UserID, [MarshalAs(UnmanagedType.BStr)] string Password, int Options);
      [DispId(19)]
      object OpenSchema(object Schema, object Restrictions, object SchemaID);
      [DispId(21)]
      void Cancel();
      int Attributes { [DispId(14)] get; [DispId(14)] set; }
      int CommandTimeout { [DispId(2)] get; [DispId(2)] set; }
      string ConnectionString { [DispId(0)][return: MarshalAs(UnmanagedType.BStr)] get; [DispId(0)] set; }
      int ConnectionTimeout { [DispId(3)] get; [DispId(3)] set; }
      object CursorLocation { [DispId(15)] get; [DispId(15)] set; }
      string DefaultDatabase { [DispId(12)][return: MarshalAs(UnmanagedType.BStr)] get; [DispId(12)] set; }
      object Errors { [DispId(11)] get; }
      object IsolationLevel { [DispId(13)] get; [DispId(13)] set; }
      object Mode { [DispId(16)] get; [DispId(16)] set; }
      object Properties { [DispId(500)] get; }
      string Provider { [DispId(17)][return: MarshalAs(UnmanagedType.BStr)] get; [DispId(17)] set; }
      int State { [DispId(18)] get; }
      string Version { [DispId(4)][return: MarshalAs(UnmanagedType.BStr)] get; }
    }

    [Guid("00000514-0000-0010-8000-00AA006D2EA4")]
    [ComImport]
    [TypeLibType((short)6)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces("ConnectionEvents")]
    internal class ConnectionClass : _Connection
    {
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(7)]
      public virtual extern int BeginTrans();
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x15)]
      public virtual extern void Cancel();
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(5)]
      public virtual extern void Close();
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(8)]
      public virtual extern void CommitTrans();
      [return: MarshalAs(UnmanagedType.Interface)]
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(6)]
      public virtual extern object Execute([In, MarshalAs(UnmanagedType.BStr)] string CommandText, [Optional, MarshalAs(UnmanagedType.Struct)] out object RecordsAffected, [In, Optional, DefaultParameterValue(-1)] int Options);
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(10)]
      public virtual extern void Open([In, Optional, DefaultParameterValue(""), MarshalAs(UnmanagedType.BStr)] string ConnectionString, [In, Optional, DefaultParameterValue(""), MarshalAs(UnmanagedType.BStr)] string UserID, [In, Optional, DefaultParameterValue(""), MarshalAs(UnmanagedType.BStr)] string Password, [In, Optional, DefaultParameterValue(-1)] int Options);
      [return: MarshalAs(UnmanagedType.Interface)]
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x13)]
      public virtual extern object OpenSchema([In] object Schema, [In, Optional, MarshalAs(UnmanagedType.Struct)] object Restrictions, [In, Optional, MarshalAs(UnmanagedType.Struct)] object SchemaID);
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(9)]
      public virtual extern void RollbackTrans();

      [DispId(14)]
      public virtual extern int Attributes { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(14)] get; [param: In] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(14)] set; }
      [DispId(2)]
      public virtual extern int CommandTimeout { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(2)] get; [param: In] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(2)] set; }
      [DispId(0)]
      public virtual extern string ConnectionString { [return: MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0)] set; }
      [DispId(3)]
      public virtual extern int ConnectionTimeout { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(3)] get; [param: In] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(3)] set; }
      [DispId(15)]
      public virtual extern object CursorLocation { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(15)] get; [param: In] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(15)] set; }
      [DispId(12)]
      public virtual extern string DefaultDatabase { [return: MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(12)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(12)] set; }
      [DispId(11)]
      public virtual extern object Errors { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(11)] get; }
      [DispId(13)]
      public virtual extern object IsolationLevel { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(13)] get; [param: In] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(13)] set; }
      [DispId(0x10)]
      public virtual extern object Mode { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x10)] get; [param: In] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x10)] set; }
      [DispId(500)]
      public virtual extern object Properties { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(500)] get; }
      [DispId(0x11)]
      public virtual extern string Provider { [return: MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x11)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x11)] set; }
      [DispId(0x12)]
      public virtual extern int State { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x12)] get; }
      [DispId(4)]
      public virtual extern string Version { [return: MarshalAs(UnmanagedType.BStr)] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(4)] get; }
    }

    [ComImport, TypeLibType((short)0x1040), Guid("2206CCB2-19C1-11D1-89E0-00C04FD7A829")]
    private interface IDataSourceLocator
    {
      [DispId(0x60020000)]
      int hWnd { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x60020000)] get; [param: In] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x60020000)] set; }
      [return: MarshalAs(UnmanagedType.IDispatch)]
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x60020002)]
      object PromptNew();
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x60020003)]
      bool PromptEdit([In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object ppADOConnection);
    }

    [ComImport, TypeLibType((short)2), ClassInterface((short)0), Guid("2206CDB2-19C1-11D1-89E0-00C04FD7A829"), ComConversionLoss]
    private class DataLinksClass : IDataSourceLocator
    {
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x60020003)]
      public virtual extern bool PromptEdit([In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object ppADOConnection);
      [return: MarshalAs(UnmanagedType.IDispatch)]
      [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x60020002)]
      public virtual extern object PromptNew();
      [DispId(0x60020000)]
      public virtual extern int hWnd { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x60020000)] get; [param: In] [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x60020000)] set; }
    }

    private string GetConnectionString(string connectionString)
    {
      DataLinksClass dataLinks = new DataLinksClass();
      _Connection connection = null;

      if (String.IsNullOrEmpty(connectionString))
      {
        connection = dataLinks.PromptNew() as _Connection;
        if (connection == null)
          return "";
        return connection.ConnectionString;
      }

      connection = new ConnectionClass();
      connection.ConnectionString = connectionString;
      object objConnection = connection;
      if (dataLinks.PromptEdit(ref objConnection))
        return connection.ConnectionString;
      return connectionString;
    }

    private void Localize()
    {
      MyRes res = new MyRes("ConnectionEditors,OleDb");
      gbConnection.Text = res.Get("ConnectionString");
      btnBuild.Text = res.Get("Build");
    }

    private void btnBuild_Click(object sender, EventArgs e)
    {
      tbConnection.Text = GetConnectionString(tbConnection.Text);
    }

    protected override string GetConnectionString()
    {
      return tbConnection.Text;
    }

    protected override void SetConnectionString(string value)
    {
      tbConnection.Text = value;
    }

    public override void UpdateLayout()
    {
      tbConnection.Height = gbConnection.Height - tbConnection.Top - tbConnection.Left;
    }

    public OleDbConnectionEditor()
    {
        InitializeComponent();
        Localize();

        // apply Right to Left layout
        if (Config.RightToLeft)
        {
            RightToLeft = RightToLeft.Yes;

            // move components to other side
            btnBuild.Left = gbConnection.Width - btnBuild.Left - btnBuild.Width;
        }
    }
  }
}
