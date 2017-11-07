using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LB.Controls;
using System.IO.Ports;
using LB.Common;
using LB.WinFunction;

namespace LB.SysConfig.SysConfig
{
    public partial class frmWeightDecive : LBUIPageBase
    {
        private SerialPort _comm = new SerialPort();
        Timer timer = null;
        int iValue = 0;
        public frmWeightDecive()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitComboxDataSource();

            this.txtWeightDeviceType.SelectedValueChanged += TxtWeightDeviceType_SelectedValueChanged;

            //初始化串口信息
            InitSerialData();

            //读取本机对应的串口信息
            LoadMathineSerialInfo();

            //_comm.RtsEnable = true;//根据实际情况吧。
            //_comm.DataReceived += _comm_DataReceived;

            this.FormClosing += FrmWeightDecive_FormClosing;
            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 200;
            timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.lblWeight.Text = LBSerialHelper.WeightValue.ToString();
        }

        private void FrmWeightDecive_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //页面关闭前先检查串口是否已关闭，如果否则强制关闭
                if (_comm.IsOpen)
                {
                    _comm.Close();
                    _comm.Dispose();
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void _comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //Listening = true;//设置标记，说明我已经开始处理数据，一会儿要使用系统UI的。

                if (!_comm.IsOpen)
                    return;
                int n = _comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                received_count += n;//增加接收计数
                _comm.Read(buf, 0, n);//读取缓冲数据

                lstBytes.Add(buf);
                //SetData(buf);

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //<协议解析>
                bool data_1_catched = false;//缓存记录数据是否捕获到
                //1.缓存数据
                buffer.AddRange(buf);
                //2.完整性判断
                while (buffer.Count >= 2)//至少要包含头（2字节）+长度（1字节）+校验（1字节）
                {
                    //请不要担心使用>=，因为>=已经和>,<,=一样，是独立操作符，并不是解析成>和=2个符号
                    //2.1 查找数据头
                    if (buffer[0] == 43 && buffer.Count == 10)
                    {
                        //2.2 探测缓存数据是否有一条数据的字节，如果不够，就不用费劲的做其他验证了
                        //前面已经限定了剩余长度>=4，那我们这里一定能访问到buffer[2]这个长度
                        int len = 8;//数据长度
                        //数据完整判断第一步，长度是否足够
                        //len是数据段长度,4个字节是while行注释的3部分长度
                        if (buffer.Count < len + 2) break;//数据不够的时候什么都不做
                        //这里确保数据长度足够，数据头标志找到，我们开始计算校验
                        //2.3 校验数据，确认数据正确
                        //异或校验，逐个字节异或得到校验码
                        /*byte checksum = 0;
                        for (int i = 0; i < len + 3; i++)//len+3表示校验之前的位置
                        {
                            checksum ^= buffer[i];
                        }
                        if (checksum != buffer[len + 3]) //如果数据校验失败，丢弃这一包数据
                        {
                            buffer.RemoveRange(0, len + 4);//从缓存中删除错误数据
                            continue;//继续下一次循环
                        }*/
                        //至此，已经被找到了一条完整数据。我们将数据直接分析，或是缓存起来一起分析
                        //我们这里采用的办法是缓存一次，好处就是如果你某种原因，数据堆积在缓存buffer中
                        //已经很多了，那你需要循环的找到最后一组，只分析最新数据，过往数据你已经处理不及时
                        //了，就不要浪费更多时间了，这也是考虑到系统负载能够降低。
                        buffer.CopyTo(0, binary_data_1, 0, len + 2);//复制一条完整数据到具体的数据缓存
                        data_1_catched = true;
                        buffer.RemoveRange(0, len + 2);//正确分析一条数据，从缓存中移除数据。
                    }
                    else
                    {
                        //这里是很重要的，如果数据开始不是头，则删除数据
                        buffer.RemoveAt(0);
                    }
                }
                //分析数据
                if (data_1_catched)
                {
                    //我们的数据都是定好格式的，所以当我们找到分析出的数据1，就知道固定位置一定是这些数据，我们只要显示就可以了
                    string data = binary_data_1[1].ToString("X2") + binary_data_1[2].ToString("X2") + binary_data_1[3].ToString("X2") + binary_data_1[4].ToString("X2") +
                        binary_data_1[5].ToString("X2") + binary_data_1[6].ToString("X2") +
                        binary_data_1[7].ToString("X2") + binary_data_1[8].ToString("X2");
                    string m = HexToString(data, System.Text.Encoding.GetEncoding("gbk"));


                    //更新界面
                    //string strValue = m.Substring(0, 6);
                    //this.Invoke((EventHandler)(delegate { this.lblWeight.Text = Convert.ToInt32(strValue).ToString(); }));
                }

                

                if (builder.ToString().Length > 20)
                {
                    builder.Remove(0, builder.Length);//清除字符串构造器的内容
                }

                this.Invoke((EventHandler)(delegate
                {
                //直接按ASCII规则转换成字符串
                builder.Append(Encoding.ASCII.GetString(buf));
                string value = builder.ToString();


                    if (value.Contains("+")&& builder.ToString().Length < 20)
                    {
                        int iIndex = value.IndexOf("+");
                        if (builder.ToString().Length - iIndex >8)
                        {
                            string strValue = value.Substring(iIndex + 1, 6);

                            int.TryParse(strValue, out iValue);
                        }
                    }

                    //if(value.Contains("\u0002")&& value.Contains("\u0003"))
                    //{

                    //}
                    //for (int i = 190; i > 10; i -= 10)
                    //{
                        

                    //    if (value.Contains(i.ToString()))
                    //    {
                    //        iValue = i;
                    //        continue;
                    //    }
                    //}
                }));
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }
        private StringBuilder builder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。
        private List<byte> buffer = new List<byte>(4096);//默认分配1页内存，并始终限制不允许超过
        private byte[] binary_data_1 = new byte[10];//AA 44 05 01 02 03 04 05 EA
        string appPath = "";
        int received_count = 0;
        List<byte[]> lstBytes = new List<byte[]>();
        private string GetValue()
        {
            string strValue = "0";

            int n = _comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
            if (n > 0)
            {

            }
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
            received_count += n;//增加接收计数
            _comm.Read(buf, 0, n);//读取缓冲数据

            lstBytes.Add(buf);
            //SetData(buf);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //<协议解析>
            bool data_1_catched = false;//缓存记录数据是否捕获到
                                        //1.缓存数据
            buffer.AddRange(buf);
            //2.完整性判断
            while (buffer.Count >= 2)//至少要包含头（2字节）+长度（1字节）+校验（1字节）
            {
                //请不要担心使用>=，因为>=已经和>,<,=一样，是独立操作符，并不是解析成>和=2个符号
                //2.1 查找数据头
                if (buffer[0] == 43 && buffer.Count == 10)
                {
                    //2.2 探测缓存数据是否有一条数据的字节，如果不够，就不用费劲的做其他验证了
                    //前面已经限定了剩余长度>=4，那我们这里一定能访问到buffer[2]这个长度
                    int len = 8;//数据长度
                                //数据完整判断第一步，长度是否足够
                                //len是数据段长度,4个字节是while行注释的3部分长度
                    if (buffer.Count < len + 2) break;//数据不够的时候什么都不做
                                                      //这里确保数据长度足够，数据头标志找到，我们开始计算校验
                                                      //2.3 校验数据，确认数据正确
                                                      //异或校验，逐个字节异或得到校验码
                                                      /*byte checksum = 0;
                                                      for (int i = 0; i < len + 3; i++)//len+3表示校验之前的位置
                                                      {
                                                          checksum ^= buffer[i];
                                                      }
                                                      if (checksum != buffer[len + 3]) //如果数据校验失败，丢弃这一包数据
                                                      {
                                                          buffer.RemoveRange(0, len + 4);//从缓存中删除错误数据
                                                          continue;//继续下一次循环
                                                      }*/
                                                      //至此，已经被找到了一条完整数据。我们将数据直接分析，或是缓存起来一起分析
                                                      //我们这里采用的办法是缓存一次，好处就是如果你某种原因，数据堆积在缓存buffer中
                                                      //已经很多了，那你需要循环的找到最后一组，只分析最新数据，过往数据你已经处理不及时
                                                      //了，就不要浪费更多时间了，这也是考虑到系统负载能够降低。
                    buffer.CopyTo(0, binary_data_1, 0, len + 2);//复制一条完整数据到具体的数据缓存
                    data_1_catched = true;
                    buffer.RemoveRange(0, len + 2);//正确分析一条数据，从缓存中移除数据。
                }
                else
                {
                    //这里是很重要的，如果数据开始不是头，则删除数据
                    buffer.RemoveAt(0);
                }
            }
            //分析数据
            if (data_1_catched)
            {
                //我们的数据都是定好格式的，所以当我们找到分析出的数据1，就知道固定位置一定是这些数据，我们只要显示就可以了
                string data = binary_data_1[1].ToString("X2") + binary_data_1[2].ToString("X2") + binary_data_1[3].ToString("X2") + binary_data_1[4].ToString("X2") +
                    binary_data_1[5].ToString("X2") + binary_data_1[6].ToString("X2") +
                    binary_data_1[7].ToString("X2") + binary_data_1[8].ToString("X2");
                string m = HexToString(data, System.Text.Encoding.GetEncoding("gbk"));


                //更新界面
                strValue = m;
            }
            if (strValue.Length > 8)
            {
                strValue =  strValue.Substring(7, 4);
            }
            return strValue;
        }

        public string HexToString(string HexStr, Encoding encode)
        {
            byte[] oribyte = new byte[HexStr.Length / 2];
            for (int i = 0; i < HexStr.Length; i += 2)
            {
                string str = Convert.ToInt32(HexStr.Substring(i, 2), 16).ToString().ToUpper();
                oribyte[i / 2] = Convert.ToByte(HexStr.Substring(i, 2), 16);
            }
            return encode.GetString(oribyte);//得到最
        }

        private void TxtWeightDeviceType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int iWeightDeviceType = LBConverter.ToInt32(this.txtWeightDeviceType.SelectedValue);
                if (iWeightDeviceType > 0)
                {
                    LoadSerialData(iWeightDeviceType);
                }
                SetTextBoxEnable();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #region-- 初始化串口数据 --

        private void InitSerialData()
        {
            DataTable dtPort = new DataTable();
            dtPort.Columns.Add("PortName", typeof(string));

            string[] ArryPort = SerialPort.GetPortNames();
            for (int i = 0; i < ArryPort.Length; i++)
            {
                DataRow drNew = dtPort.NewRow();
                drNew["PortName"] = ArryPort[i];
                dtPort.Rows.Add(drNew);
            }
            this.txtSerialName.DataSource = dtPort.DefaultView;
            this.txtSerialName.DisplayMember = "PortName";
            this.txtSerialName.ValueMember = "PortName";
            if (ArryPort.Length>0)
                this.txtSerialName.SelectedIndex = 0;
        }

        #endregion

        #region -- 初始化控件数据源 --

        private void InitComboxDataSource()
        {
            this.txtWeightDeviceType.DataSource = LB.Common.LBConst.GetConstData("WeightDeviceType");
            this.txtWeightDeviceType.DisplayMember = "ConstText";
            this.txtWeightDeviceType.ValueMember = "ConstValue";

            this.txtDeviceZhenChuLiFangShi.DataSource = LB.Common.LBConst.GetConstData("DeviceFrameType");//帧处理方式
            this.txtDeviceZhenChuLiFangShi.DisplayMember = "ConstText";
            this.txtDeviceZhenChuLiFangShi.ValueMember = "ConstValue";
            this.txtDeviceZhenChuLiFangShi.SelectedValue = 0;

            DataTable dtData = new DataTable();
            dtData.Columns.Add("ConstValue", typeof(int));
            for(int i=0; i < 15; i++)
            {
                DataRow dr = dtData.NewRow();
                dr["ConstValue"] = i;
                dtData.Rows.Add(dr);
            }
            dtData.AcceptChanges();

            this.txtDeviceChongFuChangDu.DataSource = dtData.Copy();
            this.txtDeviceChongFuChangDu.DisplayMember = "ConstValue";
            this.txtDeviceChongFuChangDu.ValueMember = "ConstValue";
            this.txtDeviceChongFuChangDu.SelectedValue = 6;

            this.txtDeviceChongFuWeiZhi.DataSource = dtData.Copy();
            this.txtDeviceChongFuWeiZhi.DisplayMember = "ConstValue";
            this.txtDeviceChongFuWeiZhi.ValueMember = "ConstValue";
            this.txtDeviceChongFuWeiZhi.SelectedValue = 3;

            this.txtDeviceShuJuWei.DataSource = dtData.Copy();
            this.txtDeviceShuJuWei.DisplayMember = "ConstValue";
            this.txtDeviceShuJuWei.ValueMember = "ConstValue";
            this.txtDeviceShuJuWei.SelectedValue = 8;

            this.txtDeviceTingZhiWei.DataSource = dtData;
            this.txtDeviceTingZhiWei.DisplayMember = "ConstValue";
            this.txtDeviceTingZhiWei.ValueMember = "ConstValue";
            this.txtDeviceTingZhiWei.SelectedValue = 1;

            this.txtDeviceZhenChangDu.DataSource = dtData.Copy();
            this.txtDeviceZhenChangDu.DisplayMember = "ConstValue";
            this.txtDeviceZhenChangDu.ValueMember = "ConstValue";
            this.txtDeviceZhenChangDu.SelectedValue = 12;

            this.txtDeviceZhenQiShiBiaoShi.DataSource = dtData.Copy();
            this.txtDeviceZhenQiShiBiaoShi.DisplayMember = "ConstValue";
            this.txtDeviceZhenQiShiBiaoShi.ValueMember = "ConstValue";
            this.txtDeviceZhenQiShiBiaoShi.SelectedValue = 2;

            DataTable dtBTL = new DataTable();
            dtBTL.Columns.Add("ConstValue", typeof(int));
            DataRow drNew = dtBTL.NewRow();
            drNew["ConstValue"] = 1200;
            dtBTL.Rows.Add(drNew);
            drNew = dtBTL.NewRow();
            drNew["ConstValue"] = 2400;
            dtBTL.Rows.Add(drNew);
            drNew = dtBTL.NewRow();
            drNew["ConstValue"] = 4800;
            dtBTL.Rows.Add(drNew);
            drNew = dtBTL.NewRow();
            drNew["ConstValue"] = 9600;
            dtBTL.Rows.Add(drNew);
            this.txtDeviceBoTeLv.DataSource = dtBTL;
            this.txtDeviceBoTeLv.DisplayMember = "ConstValue";
            this.txtDeviceBoTeLv.ValueMember = "ConstValue";
            this.txtDeviceBoTeLv.SelectedValue = 4800;
        }

        #endregion -- 初始化控件数据源 --

        #region -- 按钮事件 --

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int iWeightDeviceType = LBConverter.ToInt32(this.txtWeightDeviceType.SelectedValue);
                string strSerialName = this.txtSerialName.SelectedValue==null?"": this.txtSerialName.SelectedValue.ToString().TrimEnd();
                int DeviceBoTeLv = LBConverter.ToInt32(this.txtDeviceBoTeLv.SelectedValue);
                int DeviceChongFuChangDu = LBConverter.ToInt32(this.txtDeviceChongFuChangDu.SelectedValue);
                int DeviceChongFuWeiZhi = LBConverter.ToInt32(this.txtDeviceChongFuWeiZhi.SelectedValue);
                int DeviceShuJuWei = LBConverter.ToInt32(this.txtDeviceShuJuWei.SelectedValue);
                int DeviceTingZhiWei = LBConverter.ToInt32(this.txtDeviceTingZhiWei.SelectedValue);
                int DeviceZhenChangDu = LBConverter.ToInt32(this.txtDeviceZhenChangDu.SelectedValue);
                int DeviceZhenChuLiFangShi = LBConverter.ToInt32(this.txtDeviceZhenChuLiFangShi.SelectedValue);
                int DeviceZhenQiShiBiaoShi = LBConverter.ToInt32(this.txtDeviceZhenQiShiBiaoShi.SelectedValue);

                LBDbParameterCollection parmCol = new LBDbParameterCollection();
                parmCol.Add(new LBParameter("WeightDeviceType", enLBDbType.Int32, iWeightDeviceType));
                parmCol.Add(new LBParameter("SerialName", enLBDbType.String, strSerialName));
                parmCol.Add(new LBParameter("DeviceBoTeLv", enLBDbType.Int32, DeviceBoTeLv));
                parmCol.Add(new LBParameter("DeviceChongFuChangDu", enLBDbType.Int32, DeviceChongFuChangDu));
                parmCol.Add(new LBParameter("DeviceChongFuWeiZhi", enLBDbType.Int32, DeviceChongFuWeiZhi));
                parmCol.Add(new LBParameter("DeviceShuJuWei", enLBDbType.Int32, DeviceShuJuWei));
                parmCol.Add(new LBParameter("DeviceTingZhiWei", enLBDbType.Int32, DeviceTingZhiWei));
                parmCol.Add(new LBParameter("DeviceZhenChangDu", enLBDbType.Int32, DeviceZhenChangDu));
                parmCol.Add(new LBParameter("DeviceZhenChuLiFangShi", enLBDbType.Int32, DeviceZhenChuLiFangShi));
                parmCol.Add(new LBParameter("DeviceZhenQiShiBiaoShi", enLBDbType.Int32, DeviceZhenQiShiBiaoShi));
                parmCol.Add(new LBParameter("MachineName", enLBDbType.String, LoginInfo.MachineName));
                DataSet dsReturn;
                Dictionary<string, object> dictValue;
                ExecuteSQL.CallSP(13800, parmCol, out dsReturn, out dictValue);

                LB.WinFunction.LBCommonHelper.ShowCommonMessage("保存成功！");

                LBSerialHelper.StartSerial();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(txtSerialName.SelectedValue.ToString()))
                {
                    throw new Exception("请选择串口名称！");
                }

                if (!_comm.IsOpen)
                {
                    //_comm = new SerialPort();
                    //lstBytes = new List<byte[]>();
                    //关闭时点击，则设置好端口，波特率后打开
                    _comm.PortName = txtSerialName.SelectedValue.ToString();
                    _comm.BaudRate = int.Parse(txtDeviceBoTeLv.SelectedValue.ToString());
                    
                    try
                    {
                        _comm.Open();
                    }
                    catch (Exception ex)
                    {
                        //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                        _comm = new SerialPort();
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                LB.WinFunction.LBCommonHelper.DealWithErrorMessage(ex);
            }
        }

        #endregion -- 按钮事件 --

        #region -- 控件状态，如果设备选择自定义则可编辑，否则只读 --

        private void SetTextBoxEnable()
        {
            int iWeightDeviceType = LBConverter.ToInt32(this.txtWeightDeviceType.SelectedValue);
            bool bolEnable = true;
            if (iWeightDeviceType > 0)
            {
                bolEnable = false;
            }

            this.txtDeviceBoTeLv.Enabled = bolEnable;
            this.txtDeviceChongFuChangDu.Enabled = bolEnable;
            this.txtDeviceChongFuWeiZhi.Enabled = bolEnable;
            this.txtDeviceShuJuWei.Enabled = bolEnable;
            this.txtDeviceTingZhiWei.Enabled = bolEnable;
            this.txtDeviceZhenChangDu.Enabled = bolEnable;
            this.txtDeviceZhenChuLiFangShi.Enabled = bolEnable;
            this.txtDeviceZhenQiShiBiaoShi.Enabled = bolEnable;
            this.txtSerialName.Enabled = bolEnable;
        }

        #endregion

        #region -- 加载预设的设备串口数据 --

        private void LoadSerialData(int iWeightDeviceType)
        {
            DataTable dtSerial = ExecuteSQL.CallView(118, "", "WeightDeviceType=" + iWeightDeviceType, "");
            if (dtSerial.Rows.Count > 0)
            {
                DataRow dr = dtSerial.Rows[0];
                this.txtDeviceBoTeLv.PromptText = dr["DeviceBoTeLv"].ToString();
                this.txtDeviceChongFuChangDu.PromptText = dr["DeviceChongFuChangDu"].ToString();
                this.txtDeviceChongFuWeiZhi.PromptText = dr["DeviceChongFuWeiZhi"].ToString();
                this.txtDeviceShuJuWei.PromptText = dr["DeviceShuJuWei"].ToString();
                this.txtDeviceTingZhiWei.PromptText = dr["DeviceTingZhiWei"].ToString();
                this.txtDeviceZhenChangDu.PromptText = dr["DeviceZhenChangDu"].ToString();
                this.txtDeviceZhenChuLiFangShi.PromptText = dr["DeviceZhenChuLiFangShi"].ToString();
                this.txtDeviceZhenQiShiBiaoShi.PromptText = dr["DeviceZhenQiShiBiaoShi"].ToString();
            }
        }

        #endregion

        #region -- 读取本机已保存的串口信息 --

        private void LoadMathineSerialInfo()
        {
            string strMathineName = LoginInfo.MachineName;
            DataTable dtSerial = ExecuteSQL.CallView(120, "", "MachineName='" + strMathineName + "'", "");
            if (dtSerial.Rows.Count > 0)
            {
                DataRow dr = dtSerial.Rows[0];
                long lWeightDeviceUserTypeID = LBConverter.ToInt64(dr["WeightDeviceUserTypeID"]);

                this.txtWeightDeviceType.SelectedValue = dr["WeightDeviceType"];
                string strSerialName = dr["SerialName"].ToString().TrimEnd();
                
                int iWeightDeviceType = LBConverter.ToInt32(this.txtWeightDeviceType.SelectedValue);

                if (iWeightDeviceType == 0)//自定义
                {
                    if (lWeightDeviceUserTypeID > 0)
                    {
                        DataTable dtUserConfig = ExecuteSQL.CallView(119, "", "WeightDeviceUserTypeID=" + lWeightDeviceUserTypeID, "");

                        if (dtUserConfig.Rows.Count > 0)
                        {
                            DataRow drUserConfig = dtUserConfig.Rows[0];
                            this.txtDeviceBoTeLv.SelectedValue = drUserConfig["DeviceBoTeLv"].ToString();
                            this.txtDeviceChongFuChangDu.SelectedValue = drUserConfig["DeviceChongFuChangDu"].ToString();
                            this.txtDeviceChongFuWeiZhi.SelectedValue = drUserConfig["DeviceChongFuWeiZhi"].ToString();
                            this.txtDeviceShuJuWei.SelectedValue = drUserConfig["DeviceShuJuWei"].ToString();
                            this.txtDeviceTingZhiWei.SelectedValue = drUserConfig["DeviceTingZhiWei"].ToString();
                            this.txtDeviceZhenChangDu.SelectedValue = drUserConfig["DeviceZhenChangDu"].ToString();
                            this.txtDeviceZhenChuLiFangShi.SelectedValue = drUserConfig["DeviceZhenChuLiFangShi"].ToString();
                            this.txtDeviceZhenQiShiBiaoShi.SelectedValue = drUserConfig["DeviceZhenQiShiBiaoShi"].ToString();
                            this.txtSerialName.SelectedValue = strSerialName;
                        }
                    }
                }
                else
                {
                    this.txtSerialName.SelectedValue = strSerialName;
                }
            }
        }

        #endregion
    }
}
