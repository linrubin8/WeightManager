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
        private int _Port=0;
        private string _IPAddress = "";
        private string _Account = "";
        private string _Password = "";
        private int user = 0;
        private int playHandle = 0;

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
            IsOpen = false;
            if (IPAddress == "")
                return;
            if (_Port <= 0)
                return;

            bool tag = HikSDK.NET_DVR_Init();
            ushort uPort = (ushort)_Port;
            HikSDK.LPNET_DVR_DEVICEINFO_V301 dev = new HikSDK.LPNET_DVR_DEVICEINFO_V301();
            user = HikSDK.NET_DVR_Login_V30(IPAddress, uPort, Account, Password, out dev);

            uint dwReturned = 0; //初始化实际接收的数据长度指针
            IntPtr lptr;
            HikSDK.NET_DVR_COMPRESSIONCFG_V30 PICCFG = new HikSDK.NET_DVR_COMPRESSIONCFG_V30();
            int size = Marshal.SizeOf(PICCFG);//返回对象的大小
            lptr = Marshal.AllocHGlobal(size);//根据大小分配内存
            if (HikSDK.NET_DVR_GetDVRConfig(user, 1040, 5, lptr, (uint)size, ref dwReturned))
            {
                PICCFG = (HikSDK.NET_DVR_COMPRESSIONCFG_V30)Marshal.PtrToStructure(lptr, typeof(HikSDK.NET_DVR_COMPRESSIONCFG_V30));
            }
            //根据实际设置网络子码流
            Marshal.FreeHGlobal(lptr);
            PICCFG.struNetPara.byPicQuality = 0;
            PICCFG.struNetPara.dwVideoBitrate = 12;
            PICCFG.struNetPara.byBitrateType = 0;

            //根据实际设置 主码流
            //PICCFG.struNormHighRecordPara.byPicQuality = 0;
            //PICCFG.struNormHighRecordPara.dwVideoBitrate = 12;
            //PICCFG.struNormHighRecordPara.byBitrateType = 0;

            //网传子码流 主码流
            byte[] bytes1;
            int size2 = Marshal.SizeOf(PICCFG);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(PICCFG, buffer, false);
                bytes1 = new byte[size];
                Marshal.Copy(buffer, bytes1, 0, size);

            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            HikSDK.NET_DVR_CLIENTINFO cl = new HikSDK.NET_DVR_CLIENTINFO();
            cl.hPlayWnd = this.Handle;
            cl.lChannel = lChannel;
            cl.lLinkMode = 1;
            //return;  /////////*******************************************************如果下载或回放,必须先停止播放
            //playHandle = HikSDK.NET_DVR_RealPlay(user, ref cl);
            playHandle = HikSDK.NET_DVR_RealPlay_V30(user, ref cl, null, 1, false);
            if (playHandle >= 0)
            {
                IsOpen = true;
            }
        }

        public void CloseCamera()
        {
            if (IsOpen)
            {
                HikSDK.NET_DVR_StopRealPlay(playHandle);
                HikSDK.NET_DVR_Logout(user);
                HikSDK.NET_DVR_Cleanup();
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
                HikSDK.LPNET_DVR_JPEGPARA lpJpegPara = new HikSDK.LPNET_DVR_JPEGPARA();

                //JEPG抓图，数据保存在缓冲区中 Capture a JPEG picture and save in the buffer
                uint iBuffSize = 2000000; //缓冲区大小需要不小于一张图片数据的大小 The buffer size should not be less than the picture size
                byte[] byJpegPicBuffer = new byte[iBuffSize];
                uint dwSizeReturned = 0;
                if (!HikSDK.NET_DVR_CaptureJPEGPicture_NEW(user, 1, ref lpJpegPara, byJpegPicBuffer, iBuffSize, ref dwSizeReturned))
                {
                    uint iLastErr = HikSDK.NET_DVR_GetLastError();
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
