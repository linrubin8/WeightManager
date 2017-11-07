using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Globalization;
using FastReport.Utils;
using FastReport.Forms;

namespace FastReport.Data.ConnectionEditors
{
  internal partial class MsSqlConnectionEditor : ConnectionEditorBase
  {
    private string FConnectionString;

    private void Localize()
    {
      MyRes res = new MyRes("ConnectionEditors,Common");
      lblServer.Text = res.Get("Server");
      gbServerLogon.Text = res.Get("ServerLogon");
      lblUserName.Text = res.Get("UserName");
      lblPassword.Text = res.Get("Password");
      gbDatabase.Text = res.Get("Database");
      rbDatabaseName.Text = res.Get("DatabaseName");
      rbDatabaseFile.Text = res.Get("DatabaseFile");
      
      res = new MyRes("ConnectionEditors,MsSql");
      rbUseWindows.Text = res.Get("UseWindows");
      rbUseSql.Text = res.Get("UseSql");
      cbSavePassword.Text = res.Get("SavePassword");
      btnAdvanced.Text = Res.Get("Buttons,Advanced");
      tbDatabaseFile.Image = Res.GetImage(1);
    }

    private void PopulateServerComboBox()
    {
      DataTable dataSources = null;
      Cursor saveCursor = Cursor.Current;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        dataSources = SqlDataSourceEnumerator.Instance.GetDataSources();
      }
      catch
      {
        return;
      }
      finally
      {
        Cursor.Current = saveCursor;
      }

      List<string> servers = new List<string>();
      foreach (DataRow row in dataSources.Rows)
      {
        string str1 = row["ServerName"].ToString();
        string str2 = row["InstanceName"].ToString();
        servers.Add(String.IsNullOrEmpty(str2) ? str1 : str1 + "\\" + str2);
      }
      servers.Sort();
      
      foreach (string server in servers)
      {
        cbxServer.Items.Add(server);
      }
    }
    
    private void PopulateDatabasesComboBox()
    {
      DataTable table = null;
      IDbConnection currentConnection = null;
      IDataReader reader = null;
      Cursor saveCursor = Cursor.Current;
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        currentConnection = GetCurrentConnection();
        IDbCommand command = currentConnection.CreateCommand();
        command.CommandText = "SELECT name FROM master.dbo.sysdatabases WHERE HAS_DBACCESS(name) = 1 ORDER BY name";
        currentConnection.Open();
        reader = command.ExecuteReader();
        table = new DataTable();
        table.Locale = CultureInfo.CurrentCulture;
        table.Load(reader);
      }
      catch
      {
        return;
      }
      finally
      {
        if (reader != null)
          reader.Dispose();
        if (currentConnection != null)
          currentConnection.Dispose();
        Cursor.Current = saveCursor;
      }
      
      foreach (DataRow row in table.Rows)
      {
        cbxDatabaseName.Items.Add(row["name"]);
      }
    }

    private SqlConnection GetCurrentConnection()
    {
      SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
      builder.DataSource = cbxServer.Text;
      if (rbUseWindows.Checked)
        builder.IntegratedSecurity = true;
      else
      {
        builder.UserID = tbUserName.Text;
        builder.Password = tbPassword.Text;
      }
      builder.Pooling = false;
      return new SqlConnection(builder.ToString());
    }

    private void ServerOrLoginChanged(object sender, EventArgs e)
    {
      cbxDatabaseName.Items.Clear();
      UpdateControls(sender, e);
    }
    
    private void UpdateControls(object sender, EventArgs e)
    {
      bool loginEnabled = rbUseSql.Checked;
      lblUserName.Enabled = loginEnabled;
      lblPassword.Enabled = loginEnabled;
      tbUserName.Enabled = loginEnabled;
      tbPassword.Enabled = loginEnabled;
      cbSavePassword.Enabled = loginEnabled;
      gbDatabase.Enabled = cbxServer.Text != "" && (rbUseWindows.Checked || tbUserName.Text != "");
      cbxDatabaseName.Enabled = rbDatabaseName.Checked;
      tbDatabaseFile.Enabled = rbDatabaseFile.Checked;
    }

    private void cbxServer_DropDown(object sender, EventArgs e)
    {
      if (cbxServer.Items.Count == 0)
        PopulateServerComboBox();
      if (cbxServer.Items.Count == 0)
        cbxServer.Items.Add("");
    }

    private void cbxDatabaseName_DropDown(object sender, EventArgs e)
    {
      if (cbxDatabaseName.Items.Count == 0)
        PopulateDatabasesComboBox();
      if (cbxDatabaseName.Items.Count == 0)
        cbxDatabaseName.Items.Add("");
    }

    private void tbDatabaseFile_ButtonClick(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog())
      {
        dialog.Filter = Res.Get("FileFilters,MdfFile");
        dialog.DefaultExt = ".mdf";
        if (dialog.ShowDialog() == DialogResult.OK)
          tbDatabaseFile.Text = dialog.FileName;
      }
    }

    private void btnAdvanced_Click(object sender, EventArgs e)
    {
      using (AdvancedConnectionPropertiesForm form = new AdvancedConnectionPropertiesForm())
      {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConnectionString);
        builder.BrowsableConnectionString = false;
        form.AdvancedProperties = builder;

        if (form.ShowDialog() == DialogResult.OK)
          ConnectionString = form.AdvancedProperties.ToString();
      }
    }

    protected override string GetConnectionString()
    {
      SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(FConnectionString);
      builder.DataSource = cbxServer.Text;
      builder.IntegratedSecurity = rbUseWindows.Checked;
      if (builder.IntegratedSecurity)
      {
        builder.UserID = "";
        builder.Password = "";
      }
      else
      {
        builder.UserID = tbUserName.Text;
        builder.Password = tbPassword.Text;
      }
      builder.PersistSecurityInfo = cbSavePassword.Checked;
      if (rbDatabaseName.Checked)
      {
        builder.InitialCatalog = cbxDatabaseName.Text;
        builder.AttachDBFilename = "";
      }  
      else
      {
        builder.AttachDBFilename = tbDatabaseFile.Text;
        builder.InitialCatalog = "";
      }  
      return builder.ToString();
    }

    protected override void SetConnectionString(string value)
    {
      FConnectionString = value;
      SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(value);
      if (String.IsNullOrEmpty(value))
        builder.IntegratedSecurity = true;
      cbxServer.Text = builder.DataSource;
      rbUseWindows.Checked = builder.IntegratedSecurity;
      rbUseSql.Checked = !rbUseWindows.Checked;
      tbUserName.Text = builder.UserID;
      tbPassword.Text = builder.Password;
      cbSavePassword.Checked = builder.PersistSecurityInfo;
      if (String.IsNullOrEmpty(builder.AttachDBFilename))
      {
        rbDatabaseName.Checked = true;
        cbxDatabaseName.Text = builder.InitialCatalog;
      }
      else
      {
        rbDatabaseFile.Checked = true;
        tbDatabaseFile.Text = builder.AttachDBFilename;
      }
      UpdateControls(this, EventArgs.Empty);
    }

    public MsSqlConnectionEditor()
    {
        InitializeComponent();
        Localize();

        // apply Right to Left layout
        if (Config.RightToLeft)
        {
            RightToLeft = RightToLeft.Yes;

            // move components to other side
            lblServer.Left = ClientSize.Width - lblServer.Left - lblServer.Width;
            lblServer.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            rbUseWindows.Left = gbServerLogon.Width - rbUseWindows.Left - rbUseWindows.Width;
            rbUseWindows.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            rbUseSql.Left = gbServerLogon.Width - rbUseSql.Left - rbUseSql.Width;
            rbUseSql.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblUserName.Left = gbServerLogon.Width - lblUserName.Left - lblUserName.Width;
            lblUserName.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbUserName.Left = gbServerLogon.Width - tbUserName.Left - tbUserName.Width;
            tbUserName.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            lblPassword.Left = gbServerLogon.Width - lblPassword.Left - lblPassword.Width;
            lblPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbPassword.Left = gbServerLogon.Width - tbPassword.Left - tbPassword.Width;
            tbPassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbSavePassword.Left = gbServerLogon.Width - cbSavePassword.Left - cbSavePassword.Width;
            cbSavePassword.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            btnAdvanced.Left = ClientSize.Width - btnAdvanced.Left - btnAdvanced.Width;
            rbDatabaseName.Left = gbDatabase.Width - rbDatabaseName.Left - rbDatabaseName.Width;
            rbDatabaseName.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            cbxDatabaseName.Left = gbDatabase.Width - cbxDatabaseName.Left - cbxDatabaseName.Width;
            cbxDatabaseName.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            rbDatabaseFile.Left = gbDatabase.Width - rbDatabaseFile.Left - rbDatabaseFile.Width;
            rbDatabaseFile.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            tbDatabaseFile.Left = gbDatabase.Width - tbDatabaseFile.Left - tbDatabaseFile.Width;
            tbDatabaseFile.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
        }
    }
  }
}

