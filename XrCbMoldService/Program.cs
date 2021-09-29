using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TwinCAT.Ads;
using XrCbMoldService.DbAccess;
using XrCbMoldService.Driver;
using XrCbMoldService.Dto;

namespace XrCbMoldService
{
    class Program
    {
        /// <summary>
        /// cbmold驱动
        /// </summary>
        private static ChenHsongCbDriver M_ChenHsongCbDriver;
        static void Main(string[] args)
        {
            Console.WriteLine("enter...");
            Console.WriteLine("兴瑞CBModel服务启动");
            //数据加入redis线程
            Task.Factory.StartNew(() =>
            {
                M_ChenHsongCbDriver = new ChenHsongCbDriver();
                //初始化客户端列表
                //new CbMoldClient("5.25.9.166.1.1");
                List<CbMoldInfoDto> temp = M_ChenHsongCbDriver.GetCbMoldStrList();
                foreach (var item in temp)
                {
                    new CbMoldClient(item);
                }
            });

            Console.ReadLine();
        }
    }

    public class CbMoldClient
    {
        /// <summary>
        /// Redis访问
        /// </summary>
        public XrRedisChenHsongDbAccess XRICD;
        /// <summary>
        /// 声明定时器
        /// </summary>
        private Timer timer;
        int iHandle;
        const string VarName = ".rDataToExcel";
        /// <summary>
        /// TcAds客户端
        /// </summary>
        public TcAdsClient tcClient;

        /// <summary>
        /// TcAdsAction
        /// </summary>
        private Action TcAdsAction;
        /// <summary>
        /// AmsNetId
        /// </summary>
        private string AmsNetId;
        /// <summary>
        /// IChen驱动有CBmold部分
        /// </summary>
        private IChenDriver M_IChenDriver;
        /// <summary>
        /// DevName
        /// </summary>
        private string DevName;
        /// <summary>
        /// ConnectFlag
        /// </summary>
        private Boolean ConnectFlag;
        
        public CbMoldClient(CbMoldInfoDto info)
        {
            
            ConnectFlag = false;
            Console.WriteLine(info.DevName.ToString());
            XRICD = new XrRedisChenHsongDbAccess();
            M_IChenDriver = new IChenDriver();
            tcClient = new TcAdsClient();
            AmsNetId = info.TwinCatStr;
            DevName = info.DevName;
            Connect();
            timer = new Timer(new TimerCallback(Read_Execute), null, 2000, 10000);
            TcAdsAction += ReadFromMachine;
        }
        /// <summary>
        /// 创建回调触发方法
        /// </summary>
        /// <param name="o"></param>
        private void Read_Execute(object o)
        {

            //Console.WriteLine("just run now");
            TcAdsAction?.Invoke();
        }
        /// <summary>
        /// 读取机器里面得数据
        /// </summary>
        private void ReadFromMachine()
        {
            if (ConnectFlag)
            {
                //Console.WriteLine(AmsNetId + "读取数据");
                var a = tcClient.ReadAny(iHandle, typeof(int)).ToString();
                Console.WriteLine(AmsNetId + "--->机器编号" + DevName + "数据:" + a);
                GatherDate();
            }

        }
        
        /// <summary>
        /// 链接到设备
        /// </summary>
        /// <returns></returns>
        bool Connect()
        {
            try
            {
                //port
                tcClient.Connect(AmsNetId, 801);
                iHandle = tcClient.CreateVariableHandle(VarName);
                ConnectFlag = true;
                return true;
            }
            catch (Exception ex)
            {
                Log4netHelper.WriteLog($"链接失败{ex.Message}", ex);
                ConnectFlag = false;
                return false;
            }
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        bool DisConnect()
        {
            try
            {
                tcClient.Disconnect();
                tcClient = new TcAdsClient();
                return true;
            }
            catch (Exception ex)
            {
                tcClient = new TcAdsClient();
                Log4netHelper.WriteLog($"链接失败{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 得到数据并处理
        /// </summary>
        /// <returns></returns>
        public void GatherDate()
        {
            //判断链接状态
            AdsStream dataStream = new AdsStream(800 * 4);
            BinaryReader binRead = new BinaryReader(dataStream);
            List<GatherDataModel> TemParList = new List<GatherDataModel>();
            var machineRunState = XRICD.GetOneDto(DevName);
            Console.WriteLine(DevName+"从redis获取数据");


            if (tcClient.IsConnected)
            {
                //read comlpete Array 
                tcClient.Read(iHandle, dataStream);
                dataStream.Position = 0;
                for (int i = 0; i < 800; i++)
                {
                    TemParList.Add(new GatherDataModel()
                    {
                        Function = (i + 1).ToString(),
                        GetherValue = binRead.ReadSingle().ToString(),
                        Name = "00"
                    });
                }
                if (TemParList.Count > 0)
                {
                    //设置设备状态 当是全自动或半自动时 为生产 其他为待机
                    string temMachineStates = TemParList.FirstOrDefault(m => m.Function == "735")?.GetherValue;
                    if (temMachineStates == "1" || temMachineStates == "2")
                    {
                        machineRunState.RunState = MachineState.Run.ToString();
                    }
                    else
                    {
                        machineRunState.RunState = MachineState.Standby.ToString();
                    }

                    //设置产量
                    string QuantityTotal = TemParList.FirstOrDefault(m => m.Function == "472")?.GetherValue;
                    machineRunState.ProductQtySum = Convert.ToInt32(QuantityTotal);


                    //仪表信息赋值
                    foreach (var item in machineRunState.Mates)
                    {
                        if (item.DataItemAddress.Contains("null") == false)
                        {
                            string value = TemParList.FirstOrDefault(f => f.Function == item.DataItemAddress)?.GetherValue;
                            item.Value = value;
                        }
                    }
                }
                else
                {
                    machineRunState.RunState = MachineState.Unconnected.ToString();
                }

                //   Console.WriteLine($"ChenHsongCBControl:{Port} 采集{MachineRunState.RunState.ToString()} {MachineRunState.ProductQty}");
            }
            else
            {
                Connect();
            }
            if (machineRunState.ProductQtySum != 0)
            {
                UpMachineRunState(machineRunState);
            }
        }
        /// <summary>
        /// 上传数据到REALTIME
        /// </summary>
        /// <param name="dto"></param>
        public void UpMachineRunState(MachineRunStateDto dto)
        {
            try
            {
                M_IChenDriver.InsterMachineRealtimeOne(dto);
                Console.WriteLine(DevName+"数据插入oracle");
            }
            catch (Exception ex)
            {
                Log4netHelper.WriteLog("插入oracle异常", ex);
                Console.WriteLine("插入oracle异常");
                //暂时允许出错
            }
        }


    }
}
