using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LB.SmartClient
{
    public partial class frmSmartClient : Form
    {
        //Webservice.LBWebService webService = null;
        System.Windows.Forms.Timer mTimer = null;
        Thread mThread = null;
        enActionStatus mActionStatus = enActionStatus.TestConnect;
        private string mstrCurrentUpdateDLL = "";
        private long mUpdatedFileSize = 0;
        private long mNeedUpdateFileSize = 0;
        private string mstrMessage = "";
        private delegate void DoDataDelegate(object number);

        public frmSmartClient()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            mTimer = new System.Windows.Forms.Timer();
            mTimer.Interval = 200;
            mTimer.Tick += MTimer_Tick;
            
            //LoadServerConfig();//读取服务器配置文件

            btnStart.PerformClick();
            /*mThread = new Thread(StartAction);
            mThread.Start();*/
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            mTimer.Enabled = false;
            if (mThread.IsAlive)
            {
                mThread.Abort();
            }
        }

        int miIndex = 0;
        private void MTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                miIndex++;
                if (miIndex > 8)
                {
                    miIndex = 0;
                }
                if (mActionStatus == enActionStatus.Fail)
                {
                    mstrMessage = "连接服务器失败，请检查服务器地址和端口是否正确！";
                    this.mTimer.Enabled = false;

                    this.lblProcess.Text = mstrMessage;
                }
                else
                {
                    if (mActionStatus == enActionStatus.TestConnect)
                    {
                        mstrMessage = "正在连接服务器";
                    }
                    else if (mActionStatus == enActionStatus.Connected)
                    {
                        mstrMessage = "连接服务器成功";
                    }
                    else if (mActionStatus == enActionStatus.UpdateDLL)
                    {
                        mstrMessage = "正在更新本地程序";
                    }
                    else if (mActionStatus == enActionStatus.Success)
                    {
                        mstrMessage = "连接成功，正在登陆系统";
                        LoginSystem();
                    }
                    this.lblProcess.Text = mstrMessage + "，请稍后" + "".PadLeft(miIndex, '.') + "。" + "".PadRight(8 - miIndex, '.');
                }
            }
            catch (Exception ex)
            {
                this.lblProcess.Text = ex.Message;
                this.mTimer.Enabled = false;
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnSetAddress_Click(object sender, EventArgs e)
        {
            try
            {
                frmSetAddress frm = new SmartClient.frmSetAddress();
                frm.ShowDialog();

                LoadServerConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadServerConfig()
        {
            ServerConfig.BuildDefaultConfigFile();//如果没有ini配置文件，则创建默认
            string strServer;
            string strPort;
            ServerConfig.ReadConfigFile(out strServer, out strPort);
            string strUrl = "";
            if (strPort==""|| strPort == "0")
            {
                SetTxtValue("http://" + strServer + "/LRB");
                strUrl = "http://" + strServer + "/LRB";
            }
            else
            {
                SetTxtValue("http://" + strServer + ":" + strPort + "/LRB");
                strUrl = "http://" + strServer + ":" + strPort + "/LRB";
            }
            
            //string strUrl = "http://" + strServer + ":" + strPort + "//LBWebService.asmx";
            //webService = new Webservice.LBWebService(strUrl);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                mActionStatus = enActionStatus.TestConnect;
                mTimer.Enabled = true;
                mThread = new Thread(StartAction);
                mThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StartAction()
        {
            try
            {
                #region -- 测试服务器连接情况 --
                mActionStatus = enActionStatus.TestConnect;
                //Thread.Sleep(200);
                LoadServerConfig();

                IFaxBusiness.IMyFaxBusiness webservice = null;
                bool bolIsConnected = false;
                if (this.txtServerAddress.Text != "")
                {
                    webservice = RemotingObject.GetWebService();
                    bolIsConnected = webservice.ConnectServer();
                }

                if (bolIsConnected)//连接服务器成功
                {
                    mActionStatus = enActionStatus.Connected;

                    #region -- 获取服务器上的最新程序信息 --
                    DataTable dtServer = webservice.ReadClientFileInfo();
                    DataTable dtClient = GetLocalFile();//获取本地Client程序信息

                    //读取本地程序，与服务器程序进行对比，得出需求更新的程序清单
                    DataTable dtDiff;
                    bool bolIsNeedUpdate;
                    CompareClientInfo(dtClient, dtServer, out dtDiff, out mNeedUpdateFileSize, out bolIsNeedUpdate);

                    DoData(0);
                    SetProgressMaxNum(100);

                    #endregion -- 获取服务器上的最新程序信息 --

                    if (bolIsNeedUpdate)
                    {
                        SetProgressVisible(true);
                        mActionStatus = enActionStatus.UpdateDLL;//更新程序
                        mUpdatedFileSize = 0;

                        string strStartUpPath = Path.Combine(Application.StartupPath, "Client");
                        if (!Directory.Exists(strStartUpPath))
                        {
                            Directory.CreateDirectory(strStartUpPath);
                        }

                        //在Client下创建Ini文件
                        /*string strIniFile = Path.Combine(strStartUpPath, "WebLink.ini");
                        if (!File.Exists(strIniFile))
                        {
                            File.Create(strIniFile);
                        }

                        IniClass iniWebLink = new IniClass(strIniFile);
                        iniWebLink.WriteValue("Link", "url", this.txtServerAddress.Text);
                        */
                        #region -- 分段下单程序 --
                        int iMaxLength = 1024 * 100;
                        foreach (DataRow dr in dtDiff.Rows)
                        {
                            string strFileName = dr["FileName"].ToString().TrimEnd();

                            if (strFileName.Equals("LBTSR.ini"))
                                continue;

                            DateTime dtFileTime = Convert.ToDateTime(dr["FileTime"]);
                            long lFileSize = Convert.ToInt64(dr["FileSize"]);

                            string strFullName = Path.Combine(strStartUpPath, strFileName);
                            mstrCurrentUpdateDLL = strFullName;//正在更新的程序

                            FileInfo fi = new FileInfo(strFullName);

                            if (!Directory.Exists(fi.DirectoryName))
                            {
                                Directory.CreateDirectory(fi.DirectoryName);
                            }

                            string strFullName_temp = strFullName + "_temp";
                            if (File.Exists(strFullName_temp))
                                File.Delete(strFullName_temp);

                            int iSplitCount = (int)Math.Ceiling(lFileSize / (float)iMaxLength);
                            //long lTotalSize = 0;
                            for (int i = 0; i < iSplitCount; i++)
                            {
                                int iPosition = i * iMaxLength;
                                if (lFileSize >= iPosition)
                                {
                                    byte[] bFile;
                                    webservice.ReadFileByte(strFileName, iPosition, iMaxLength,out bFile);
                                    mUpdatedFileSize += bFile.Length;//计算已更新的程序大小
                                    using (FileStream fileStream = new FileStream(strFullName_temp, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Write))
                                    {
                                        fileStream.Position = iPosition;
                                        fileStream.Write(bFile, 0, bFile.Length);

                                        fileStream.Flush();
                                        fileStream.Close();
                                    }

                                    int iPercent = (int)(mUpdatedFileSize / (float)mNeedUpdateFileSize * 100);
                                    DoData(iPercent);
                                }

                                if (i + 1 == iSplitCount)
                                {
                                    if (File.Exists(strFullName_temp))
                                    {
                                        if (File.Exists(strFullName))
                                            File.Delete(strFullName);
                                        File.Move(strFullName_temp, strFullName);
                                        if (File.Exists(strFullName))
                                        {
                                            FileInfo fiFile = new FileInfo(strFullName);
                                            fiFile.LastWriteTime = dtFileTime;
                                        }
                                    }
                                }
                            }
                        }

                        //将INi拷贝到Client目录下
                        string strIniPath = Path.Combine(Application.StartupPath, "WebLink.ini");
                        if (File.Exists(strIniPath))
                        {
                            string strClientIniPath = Path.Combine(Application.StartupPath, @"Client\WebLink.ini");
                            if (File.Exists(strClientIniPath))
                            {
                                File.Delete(strClientIniPath);
                            }
                            File.Copy(strIniPath, strClientIniPath);
                        }
                        DoData(100);
                        #endregion -- 分段下单程序 --
                    }

                    mActionStatus = enActionStatus.Success;
                }
                else
                {
                    mActionStatus = enActionStatus.Fail;
                }
                #endregion -- 测试服务器连接情况 --
            }
            catch (Exception ex)
            {
                mActionStatus = enActionStatus.Fail;
            }
        }

        private void LoginSystem()
        {
            Process processMain = null;
            string strMainForm = Path.Combine(Application.StartupPath, "Client\\LB.MainForm.exe");
            if (File.Exists(strMainForm))
            {
                processMain = new Process();
                processMain.StartInfo.FileName = strMainForm;
                processMain.Start();
                this.mTimer.Enabled = false;
                this.Close();
            }
            else
            {
                throw new Exception("找不到登陆程序！");
            }
        }

        /// <summary>
        /// 获取本地程序文件信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetLocalFile()
        {
            DataTable dtLocal = new DataTable("DLL");
            dtLocal.Columns.Add("FileName", typeof(string));
            dtLocal.Columns.Add("FileTimeStr", typeof(string));
            dtLocal.Columns.Add("FileTime", typeof(DateTime));

            string strClientPath = Path.Combine(Application.StartupPath, "Client");
            GetChildFile(strClientPath, ref dtLocal);
            return dtLocal;
        }

        private void GetChildFile(string strPath,ref DataTable dtFile)
        {
            string strClientPath = Path.Combine(Application.StartupPath, "Client");
            if (!Directory.Exists(strClientPath))
                Directory.CreateDirectory(strClientPath);

            FileInfo[] files = new DirectoryInfo(strPath).GetFiles();
            foreach(FileInfo fi in files)
            {
                if (fi.Name.Contains("LB.SmartClient"))
                {
                    continue;
                }

                if (fi.Name.Contains("WebLink.ini"))
                {
                    continue;
                }
                string strFileName = fi.FullName.Replace(strClientPath, "");
                if (strFileName.StartsWith("\\"))
                {
                    strFileName = strFileName.Substring(1, strFileName.Length - 1);
                }
                DataRow dr = dtFile.NewRow();
                dr["FileName"] = strFileName;
                dr["FileTimeStr"] = fi.LastWriteTime.ToString("yyMMddHHmmss");
                dr["FileTime"] = fi.LastWriteTime;
                dtFile.Rows.Add(dr);
            }

            DirectoryInfo[] directInfos = new DirectoryInfo(strPath).GetDirectories();
            foreach(DirectoryInfo directory in directInfos)
            {
                GetChildFile(directory.FullName, ref dtFile);
            }
        }

        private void CompareClientInfo(DataTable dtClient,DataTable dtServer,out DataTable dtDiff,out long lTotalSize,out bool bolIsNeedUpdate)
        {
            bolIsNeedUpdate = false;
            lTotalSize = 0;
            dtDiff = dtServer.Clone();

            foreach (DataRow dr in dtServer.Rows)
            {
                string strServerFilename = dr["FileName"].ToString().TrimEnd();
                string strFileTime = dr["FileTimeStr"].ToString().TrimEnd();

                string strFilter = "FileName='" + strServerFilename + "' and FileTimeStr='" + strFileTime + "'";
                DataRow[] drAry = dtClient.Select(strFilter);
                if (drAry.Length == 0)
                {
                    lTotalSize += Convert.ToInt64(dr["FileSize"]);
                    dtDiff.ImportRow(dr);
                    bolIsNeedUpdate = true;
                }
            }
        }

        #region-- 更新进度条处理 --

        private void DoData(object number)
        {
            if (skinProgressBar1.InvokeRequired)
            {
                DoDataDelegate d = DoData;
                skinProgressBar1.Invoke(d, number);
            }
            else
            {
                skinProgressBar1.Value = (int)number;
            }
        }

        delegate void SetProgressMaxNumDelegate( int iMaxValue);
        private void SetProgressMaxNum(int Maxnumber)
        {
            if (skinProgressBar1.InvokeRequired)
            {
                skinProgressBar1.Invoke(new SetProgressMaxNumDelegate(SetProgressMaxNum),Maxnumber);
            }
            else
            {
                skinProgressBar1.Maximum = Maxnumber;
            }
        }

        delegate void SetProgressVisibleDelegate(bool bolVisible);
        private void SetProgressVisible(bool bolVisible)
        {
            if (skinProgressBar1.InvokeRequired)
            {
                skinProgressBar1.Invoke(new SetProgressVisibleDelegate(SetProgressVisible), bolVisible);
            }
            else
            {
                skinProgressBar1.Visible = bolVisible;
            }
        }

        delegate void SetTxtValueDelegate(string strValue);
        private void SetTxtValue(string strValue)
        {
            if (this.txtServerAddress.InvokeRequired)
            {
                txtServerAddress.Invoke(new SetTxtValueDelegate(SetTxtValue), strValue);
            }
            else
            {
                txtServerAddress.Text = strValue;
            }
        }
        #endregion
    }

    public enum enActionStatus
    {
        TestConnect,

        Connected,

        UpdateDLL,

        Success,

        Fail
    }
}
