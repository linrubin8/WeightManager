using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace LB.Common.Camera
{
    public partial class ViewCamera : UserControl
    {
        private int _Port = 0;
        private string _IPAddress = "";
        private string _Account = "";
        private string _Password = "";
        private int user = -1;
        private int playHandle = -1;
        private int _Channel = 0;
        private bool m_bInitSDK = false;

        public int Port
        {
            set
            {
                _Port = value;
            }
            get
            {
                return _Port;
            }
        }
        public string IPAddress
        {
            set
            {
                _IPAddress = value;
            }
            get
            {
                return _IPAddress;
            }
        }
        public string Account
        {
            set
            {
                _Account = value;
            }
            get
            {
                return _Account;
            }
        }
        public string Password
        {
            set
            {
                _Password = value;
            }
            get
            {
                return _Password;
            }
        }

        public bool IsOpen = false;

        public ViewCamera()
        {
            InitializeComponent();
        }

        public void OpenCamera(int lChannel)
        {
            _Channel = lChannel;
            IsOpen = false;
            if (IPAddress == "")
                return;
            if (_Port <= 0)
                return;

            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (user < 0)
            {
                string DVRIPAddress = IPAddress; //设备IP地址或者域名
                Int32 DVRPortNumber = _Port;//设备服务端口号
                string DVRUserName = Account;//设备登录用户名
                string DVRPassword = Password;//设备登录密码

                CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

                //登录设备 Login the device
                user = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if (user < 0)
                {
                    //iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    //str = "NET_DVR_Login_V30 failed, error code= " + iLastErr; //登录失败，输出错误号
                    //MessageBox.Show(str);
                    return;
                }
                else
                {
                    //登录成功
                    if (playHandle < 0)
                    {
                        CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                        lpPreviewInfo.hPlayWnd = this.Handle;//预览窗口
                        lpPreviewInfo.lChannel = lChannel;//预te览的设备通道
                        lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                        lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                        lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                        lpPreviewInfo.dwDisplayBufNum = 15; //播放库播放缓冲区最大缓冲帧数

                        CHCNetSDK.REALDATACALLBACK RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                        IntPtr pUser = new IntPtr();//用户数据

                        //打开预览 Start live view 
                        playHandle = CHCNetSDK.NET_DVR_RealPlay_V40(user, ref lpPreviewInfo, null/*RealData*/, pUser);
                        if (playHandle < 0)
                        {
                            //iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                            //str = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr; //预览失败，输出错误号
                            //MessageBox.Show(str);
                            return;
                        }
                        else
                        {
                            //预览成功
                            IsOpen = true;
                        }
                    }
                }

            }

            //bool tag = HikSDK.NET_DVR_Init();
            //ushort uPort = (ushort)_Port;
            //HikSDK.LPNET_DVR_DEVICEINFO_V301 dev = new HikSDK.LPNET_DVR_DEVICEINFO_V301();
            //user = HikSDK.NET_DVR_Login_V30(IPAddress, uPort, Account, Password, out dev);

            //uint dwReturned = 0; //初始化实际接收的数据长度指针
            //IntPtr lptr;
            //HikSDK.NET_DVR_COMPRESSIONCFG_V30 PICCFG = new HikSDK.NET_DVR_COMPRESSIONCFG_V30();
            //int size = Marshal.SizeOf(PICCFG);//返回对象的大小
            //lptr = Marshal.AllocHGlobal(size);//根据大小分配内存
            //if (HikSDK.NET_DVR_GetDVRConfig(user, 1040, 5, lptr, (uint)size, ref dwReturned))
            //{
            //    PICCFG = (HikSDK.NET_DVR_COMPRESSIONCFG_V30)Marshal.PtrToStructure(lptr, typeof(HikSDK.NET_DVR_COMPRESSIONCFG_V30));
            //}
            ////根据实际设置网络子码流
            //Marshal.FreeHGlobal(lptr);
            //PICCFG.struNetPara.byPicQuality = 0;
            //PICCFG.struNetPara.dwVideoBitrate = 12;
            //PICCFG.struNetPara.byBitrateType = 0;

            ////根据实际设置 主码流
            ////PICCFG.struNormHighRecordPara.byPicQuality = 0;
            ////PICCFG.struNormHighRecordPara.dwVideoBitrate = 12;
            ////PICCFG.struNormHighRecordPara.byBitrateType = 0;

            ////网传子码流 主码流
            //byte[] bytes1;
            //int size2 = Marshal.SizeOf(PICCFG);
            //IntPtr buffer = Marshal.AllocHGlobal(size);
            //try
            //{
            //    Marshal.StructureToPtr(PICCFG, buffer, false);
            //    bytes1 = new byte[size];
            //    Marshal.Copy(buffer, bytes1, 0, size);

            //}
            //finally
            //{
            //    Marshal.FreeHGlobal(buffer);
            //}

            //HikSDK.NET_DVR_CLIENTINFO cl = new HikSDK.NET_DVR_CLIENTINFO();
            //cl.hPlayWnd = this.Handle;
            //cl.lChannel = lChannel;
            //cl.lLinkMode = 1;
            ////return;  /////////*******************************************************如果下载或回放,必须先停止播放
            ////playHandle = HikSDK.NET_DVR_RealPlay(user, ref cl);
            //playHandle = HikSDK.NET_DVR_RealPlay_V30(user, ref cl, null, 1, false);
            //if (playHandle >= 0)
            //{
            //    IsOpen = true;
            //}
        }

        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
        }

        public void CloseCamera()
        {
            if (IsOpen)
            {
                if (playHandle >= 0)
                {
                    CHCNetSDK.NET_DVR_StopRealPlay(playHandle);
                    playHandle = -1;
                }

                //注销登录 Logout the device
                if (user >= 0)
                {
                    CHCNetSDK.NET_DVR_Logout(user);
                    user = -1;
                }
                if (m_bInitSDK == true)
                {
                    CHCNetSDK.NET_DVR_Cleanup();
                }
                //HikSDK.NET_DVR_StopRealPlay(playHandle);
                //HikSDK.NET_DVR_Logout(user);
                //HikSDK.NET_DVR_Cleanup();
            }
        }

        /// <summary>
        /// 获取摄像头截图
        /// </summary>
        /// <returns></returns>
        public byte[] CapturePic()
        {
            byte[] byteImage = null;

            if (IsOpen)
            {
                CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();

                //JEPG抓图，数据保存在缓冲区中 Capture a JPEG picture and save in the buffer
                uint iBuffSize = 2000000; //缓冲区大小需要不小于一张图片数据的大小 The buffer size should not be less than the picture size
                byte[] byJpegPicBuffer = new byte[iBuffSize];
                uint dwSizeReturned = 0;
                if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture_NEW(user, _Channel, ref lpJpegPara, byJpegPicBuffer, iBuffSize, ref dwSizeReturned))
                {
                    uint iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    string str = "摄像头调用失败，无法获取影像图片, error code= " + iLastErr;
                    throw new Exception(str);
                    //DebugInfo(str);
                    return null;
                }
                else
                {
                    byteImage = new byte[dwSizeReturned];
                    for (int i = 0, j = (int)dwSizeReturned; i < j; i++)
                    {
                        byteImage[i] = byJpegPicBuffer[i];
                    }
                    //将缓冲区里的JPEG图片数据写入文件 save the data into a file
                    //string str = Application.StartupPath +@"\"+ name + ".jpg";
                    //FileStream fs = new FileStream(str, FileMode.Create);
                    //int iLen = (int)dwSizeReturned;
                    //fs.Write(byJpegPicBuffer, 0, iLen);
                    //fs.Close();

                    //str = "NET_DVR_CaptureJPEGPicture_NEW succ and save the data in buffer to 'buffertest.jpg'.";
                    //DebugInfo(str);
                }
            }
            return byteImage;
        }
    }
}
