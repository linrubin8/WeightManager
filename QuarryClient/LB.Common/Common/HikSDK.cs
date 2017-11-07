using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LB.Common
{
    public class HikSDK
    {
        public const string dllName = @"HCNetSDK.dll";


        [DllImport(dllName)]
        public static extern bool NET_DVR_Init();
        [DllImport(dllName)]
        public static extern bool NET_DVR_Cleanup();
        [DllImport(dllName)]
        public static extern int NET_DVR_Login_V30(string sDVRIP, ushort wDVRPort, string sUserName, string sPassword, out LPNET_DVR_DEVICEINFO_V301 lpDeviceInfo);
        [DllImport(dllName)]
        public static extern bool NET_DVR_Logout(int lUserID);
        [DllImport(dllName)]
        public static extern int NET_DVR_RealPlay(int lUserID, ref NET_DVR_CLIENTINFO lpClientInfo);
        /// <summary>
        /// 5.1.2   启动客户端实时预览[可选connect是否在线程中处理]
        ///     说明
        ///         不阻塞：设备应答请求连接就认为连接成功，如果发生码流接收失败、播放失败等情况以预览异常的方式告知应用层。在循环播放的时候可以减短停顿的时间。与原来的NET_DVR_RealPlay功能一致。
        ///         阻塞：直到播放成功才返回成功给应用层。
        ///     NET_DVR_API LONG __stdcall NET_DVR_RealPlay_V30(LONG lUserID, LPNET_DVR_CLIENTINFO lpClientInfo, void(CALLBACK *fRealDataCallBack_V30) (LONG lRealHandle, DWORD dwDataType, BYTE *pBuffer, DWORD dwBufSize, void* pUser) = NULL, void* pUser = NULL, BOOL bBlocked = FALSE);
        /// </summary>
        /// <param name="lUserID">[in]NET_DVR_Login或者NET_DVR_Login_V30的返回值</param>
        /// <param name="lpClientInfo">[in]NET_DVR_CLIENTINFO结构的指针</param>
        /// <param name="fRealDataCallBack_V30">[in]视频数据回调函数</param>
        /// <param name="pUser">[in]用户数据</param>
        /// <param name="bBlocked">[in]请求视频过程是否阻塞：0－否；1－是</param>
        /// <returns>-1表示失败，其他值作为NET_DVR_StopRealPlay等函数的参数</returns>
        [DllImport(dllName)]
        public static extern int NET_DVR_RealPlay_V30(int lUserID, ref NET_DVR_CLIENTINFO lpClientInfo, RealDataCallBack_V30 fRealDataCallBack_V30, int pUser, bool bBlocked);

        [DllImport(dllName)]
        public static extern bool NET_DVR_StopRealPlay(int lRealHandle);
        [DllImport(dllName)]
        public static extern int NET_DVR_GetFileByTime(int lUserID, int lChannel, ref LPNET_DVR_TIME lpStartTime, ref LPNET_DVR_TIME lpStopTime, string sSavedFileName);
        [DllImport(dllName)]
        public static extern int NET_DVR_GetDownloadPos(int lFileHandle);
        [DllImport(dllName)]
        public static extern bool NET_DVR_StopGetFile(int lFileHandle);

        [DllImport(dllName)]
        public static extern int NET_DVR_PlayBackByTime(int lUserID, int lChannel, ref LPNET_DVR_TIME lpStartTime, ref LPNET_DVR_TIME lpStopTime, IntPtr hWnd);
        [DllImport(dllName)]
        public static extern bool NET_DVR_PlayBackControl(int lPlayHandle, uint dwControlCode, uint dwInValue, out uint LPOutValue);

        [DllImport(dllName)]
        public static extern bool NET_DVR_PlayBackSaveData(int lPlayHandle, string sFileName);
        [DllImport(dllName)]
        public static extern bool NET_DVR_StopPlayBackSave(int lPlayHandle);

        [DllImport(dllName)]
        public static extern bool NET_DVR_CaptureJPEGPicture_NEW(int lUserID,int lChannel, ref LPNET_DVR_JPEGPARA lpJpegPara,
            byte[] sJpegPicBuffer, uint dwPicSize, ref uint lpSizeReturned);

        [DllImport(dllName)]
        public static extern uint NET_DVR_GetLastError();

        /// <summary>
        /// 获取硬盘录像机的参数
        ///     NET_DVR_API BOOL __stdcall NET_DVR_GetDVRConfig(LONG lUserID, DWORD dwCommand,LONG lChannel, LPVOID lpOutBuffer, DWORD dwOutBufferSize, LPDWORD lpBytesReturned);
        /// </summary>
        /// <param name="lUserID">[in]NET_DVR_Login或者NET_DVR_Login_V30的返回值</param>
        /// <param name="dwCommand">[in]参数类型，见下表宏定义</param>
        /// <param name="lChannel">[in]通道号，如果不是通道参数，lChannel不用,置为-1即可</param>
        /// <param name="lpOutBuffer">[out]存放输出参数的缓冲区</param>
        /// <param name="dwOutBufferSize">[out]缓冲区的大小</param>
        /// <param name="lpBytesReturned">[out]实际返回的缓冲区大小</param>
        /// <returns></returns>
        [DllImportAttribute(dllName, EntryPoint = "NET_DVR_GetDVRConfig", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool NET_DVR_GetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpOutBuffer, uint dwOutBufferSize, ref uint lpBytesReturned);


        /// <summary>
        /// void(CALLBACK *fRealDataCallBack_V30) (LONG lRealHandle, DWORD dwDataType, BYTE *pBuffer, DWORD dwBufSize, void* pUser)
        /// </summary>
        /// <param name="lRealHandle">NET_DVR_RealPlay_V30返回值</param>
        /// <param name="dwDataType">
        ///     数据类型
        ///     #define NET_DVR_SYSHEAD     1       系统头数据
        ///     #define NET_DVR_STREAMDATA  2       流数据/视频数据
        ///     #define NET_DVR_AUDIODATA   3       音频数据
        /// </param>
        /// <param name="pBuffer">存放数据的缓冲区指针</param>
        /// <param name="dwBufSize">缓冲区的大小</param>
        /// <param name="pUser">输入的用户数据</param>
        public delegate void RealDataCallBack_V30(int lRealHandle, uint dwDataType, byte[] pBuffer, uint dwBufSize, IntPtr pUser);

        public struct NET_DVR_COMPRESSIONCFG_V30
        {
            /// <summary>
            /// 本结构长度
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 主码流(录像) 对应8000的普通
            /// </summary>
            public NET_DVR_COMPRESSION_INFO_V30 struNormHighRecordPara;
            /// <summary>
            /// 保留 public char reserveData[28];
            /// </summary>
            public NET_DVR_COMPRESSION_INFO_V30 struRes;
            /// <summary>
            /// 事件触发录像压缩参数
            /// </summary>
            public NET_DVR_COMPRESSION_INFO_V30 struEventRecordPara;
            /// <summary>
            /// 网传(子码流)
            /// </summary>
            public NET_DVR_COMPRESSION_INFO_V30 struNetPara;
        }

        public struct NET_DVR_COMPRESSION_INFO_V30
        {
            /// <summary>
            /// 码流类型 0-视频流, 1-复合流, 表示事件压缩参数时最高位表示是否启用压缩参数
            /// </summary>
            public byte byStreamType;
            /// <summary>
            /// 分辨率0-DCIF 1-CIF, 2-QCIF, 3-4CIF, 4-2CIF 5（保留）16-VGA（640*480） 17-UXGA（1600*1200） 18-SVGA （800*600）19-HD720p（1280*720）20-XVGA  21-HD900p
            /// </summary>
            public byte byResolution;
            /// <summary>
            /// 码率类型 0:定码率，1:变码率
            /// </summary>
            public byte byBitrateType;
            /// <summary>
            /// 图象质量 0-最好 1-次好 2-较好 3-一般 4-较差 5-差
            /// </summary>
            public byte byPicQuality;
            /// <summary>
            /// 视频码率 0-保留 1-16K 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K
            /// 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K
            /// 最高位(31位)置成1表示是自定义码流, 0-30位表示码流值。
            /// </summary>
            public uint dwVideoBitrate;
            /// <summary>
            /// 帧率 0-全部; 1-1/16; 2-1/8; 3-1/4; 4-1/2; 5-1; 6-2; 7-4; 8-6; 9-8; 10-10; 11-12; 12-16; 13-20; V2.0版本中新加14-15; 15-18; 16-22;
            /// </summary>
            public uint dwVideoFrameRate;
            /// <summary>
            /// I帧间隔
            /// </summary>
            public ushort wIntervalFrameI;
            /// <summary>
            /// 0-BBP帧; 1-BP帧; 2-单P帧
            /// 2006-08-11 增加单P帧的配置接口，可以改善实时流延时问题
            /// </summary>
            public byte byIntervalBPFrame;
            /// <summary>
            /// 保留
            /// </summary>
            public byte byres1;
            /// <summary>
            /// 视频编码类型 0 hik264;1标准h264; 2标准mpeg4;
            /// </summary>
            public byte byVideoEncType;
            /// <summary>
            /// 音频编码类型 0－OggVorbis
            /// </summary>
            public byte byAudioEncType;
            /// <summary>
            /// 这里保留音频的压缩参数
            ///    public byte  byres[10];
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] byres;
        }

        public struct NET_DVR_COMPRESSION_INFO
        {
            /// <summary>
            /// 码流类型0-视频流,1-复合流,表示压缩参数时最高位表示是否启用压缩参数
            /// </summary>
            public byte byStreamType;
            /// <summary>
            /// 分辨率0-DCIF 1-CIF, 2-QCIF, 3-4CIF, 4-2CIF, 5-2QCIF(352X144)(车载专用)
            /// </summary>
            public byte byResolution;
            /// <summary>
            /// 码率类型0:变码率，1:定码率
            /// </summary>
            public byte byBitrateType;
            /// <summary>
            /// 图象质量 0-最好 1-次好 2-较好 3-一般 4-较差 5-差
            /// </summary>
            public byte byPicQuality;
            /// <summary>
            /// 视频码率 0-保留 1-16K(保留) 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K
            /// 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K
            /// 最高位(31位)置成1表示是自定义码流, 0-30位表示码流值(MIN-32K MAX-8192K)。
            /// </summary>
            public uint dwVideoBitrate;
            /// <summary>
            /// 帧率 0-全部; 1-1/16; 2-1/8; 3-1/4; 4-1/2; 5-1; 6-2; 7-4; 8-6; 9-8; 10-10; 11-12; 12-16; 13-20;
            /// </summary>
            public uint dwVideoFrameRate;
        }
        /// <summary>
        /// 通道压缩参数
        ///     NET_DVR_COMPRESSIONCFG, *LPNET_DVR_COMPRESSIONCFG;
        /// </summary>
        public struct NET_DVR_COMPRESSIONCFG
        {
            /// <summary>
            /// 此结构的大小
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// 录像/事件触发录像
            /// </summary>
            public NET_DVR_COMPRESSION_INFO struRecordPara;
            /// <summary>
            /// 网传/保留
            /// </summary>
            public NET_DVR_COMPRESSION_INFO struNetPara;
        }

        public struct NET_DVR_CLIENTINFO
        {
            public int lChannel;
            public int lLinkMode;
            public IntPtr hPlayWnd;
            public string sMultiCastIP;
        }
        public struct LPNET_DVR_DEVICEINFO_V301
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
            public byte[] sSerialNumber;
            public byte byAlarmInPortNum;
            public byte byAlarmOutPortNum;
            public byte byDiskNum;
            public byte byDVRType;
            public byte byChanNum;
            public byte byStartChan;
            public byte byAudioChanNum;
            public byte byIPChanNum;
            public byte byZeroChanNum;
            public byte byMainProto;
            public byte bySubProto;
            public byte bySupport;
            public byte bySupport1;
            public byte byRes1;
            public int wDevType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] byRes2;
        }
        public struct LPNET_DVR_TIME
        {
            public uint dwYear;
            public uint dwMonth;
            public uint dwDay;
            public uint dwHour;
            public uint dwMinute;
            public uint dwSecond;

        }

        public struct LPNET_DVR_JPEGPARA
        {
            ushort wPicSize;
            ushort wPicQuality;
        }
    }

}
