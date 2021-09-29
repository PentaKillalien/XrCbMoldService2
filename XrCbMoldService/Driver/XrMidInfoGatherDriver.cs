/*
* 命名空间: XrCbMoldService.Driver
*
* 功 能： N/A
* 类 名：XrMidInfoGatherDriver.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/9/29 9:38:05  彭政亮 初版
*
* Copyright (c) 2021 Lir Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：宁波瑞辉智能科技有限公司 　　　　　 　　　　　　　    　│
*└──────────────────────────────────┘
*/
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XrCbMoldService.Dto;

namespace XrCbMoldService.Driver
{
    public class XrMidInfoGatherDriver
    {
        private ConnectionMultiplexer _conn;
        private IDatabase database;
        private Timer timer;
        private Action GetInfo;
        public Action<List<XrMidInfoDto>> MidInfos;
        public XrMidInfoGatherDriver()
        {
            try
            {
                _conn = ConnectionMultiplexer.Connect("192.168.0.28:6379,allowadmin=true");//初始化
                database = _conn.GetDatabase(0);//指定连接库 1
                timer = new Timer(new TimerCallback(Read_Execute), null, 2000, 10000);
                GetInfo += GetInfos;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void Read_Execute(Object o) {
            GetInfo?.Invoke();
        }

        private void GetInfos() {

            Console.WriteLine("扫描redis");
            try
            {

                var aaa = database.HashGetAll("info").ToDictionary();
                List<XrMidInfoDto> xrMidInfoList = new List<XrMidInfoDto>();
                foreach (var item in aaa)
                {
                    string[] Info_list = item.Value.ToString().Split(';');

                    XrMidInfoDto xrMidInfo = new XrMidInfoDto()
                    {
                        MachineStation = item.Key,
                        FMATERIAL = Info_list[0],
                        FMATERIALNAME = Info_list[1],
                        FBILLNOINFO = Info_list[2]
                    };
                    xrMidInfoList.Add(xrMidInfo);
                    Console.WriteLine("-------------------------------添加数据");
                }
                MidInfos?.Invoke(xrMidInfoList);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                database = _conn.GetDatabase(0);
            }
        }
    }
}
