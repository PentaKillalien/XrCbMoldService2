/**
* 命名空间: XrCbMoldService.Driver
*
* 功 能： N/A
* 类 名：ChenHsongCbDriver.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/2/26 9:24:09  彭政亮 初版
*
* Copyright (c) 2020 Lir Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：宁波瑞辉智能科技有限公司 　　　　　 　　　　　　　    　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrCbMoldService.DbAccess;
using XrCbMoldService.Dto;

namespace XrCbMoldService.Driver
{
    public class ChenHsongCbDriver
    {
        /// <summary>
        /// 从Mongo里面得到CbMold的列表
        /// </summary>
        /// <returns></returns>
        public List<CbMoldInfoDto> GetCbMoldStrList()
        {
            try
            {
                //MongoDbHelper<MachineDto> mdh = new MongoDbHelper<MachineDto>(MongodbHostOptions.Devicers);
                List<CbMoldInfoDto> ports = new List<CbMoldInfoDto>();
                //var records = mdh.FindAll();

                //foreach (var item in records)
                //{
                //    foreach (var item2 in item.MachineControls)
                //    {
                //        CbMoldInfoDto a = new CbMoldInfoDto();
                //        a.Ip= item2.IpAddress;
                //        if (item2.Port != "1")
                //        {

                //            a.DevName = item.DevId;
                //            a.TwinCatStr = item2.Port;
                //            ports.Add(a);
                //        }
                //    }
                //}

                ports.Add(new CbMoldInfoDto() { 
                     DevName= "CXD093",
                      Ip= "192.168.101.35",
                       TwinCatStr= "5.26.231.6.1.1"
                });
                ports.Add(new CbMoldInfoDto()
                {
                    DevName = "CXD094",
                    Ip = "192.168.101.36",
                    TwinCatStr = "5.36.127.82.1.1"
                });
                ports.Add(new CbMoldInfoDto()
                {
                    DevName = "CXD100",
                    Ip = "192.168.101.42",
                    TwinCatStr = "5.25.9.166.1.1"
                });
                ports.Add(new CbMoldInfoDto()
                {
                    DevName = "CXC067",
                    Ip = "192.168.101.45",
                    TwinCatStr = "5.36.147.204.1.1"
                });
                ports.Add(new CbMoldInfoDto()
                {
                    DevName = "CXC072",
                    Ip = "192.168.101.17",
                    TwinCatStr = "5.26.231.0.1.1"
                });
                ports.Add(new CbMoldInfoDto()
                {
                    DevName = "CXC074",
                    Ip = "192.168.101.15",
                    TwinCatStr = "5.37.193.62.1.1"
                });
                ports.Add(new CbMoldInfoDto()
                {
                    DevName = "CXC075",
                    Ip = "192.168.101.14",
                    TwinCatStr = "5.41.67.158.1.1"
                });
                ports.Add(new CbMoldInfoDto()
                {
                    DevName = "CXC076",
                    Ip = "192.168.101.13",
                    TwinCatStr = "5.38.150.40.1.1"
                });
                ports.Add(new CbMoldInfoDto()
                {
                    DevName = "CXC077",
                    Ip = "192.168.101.12",
                    TwinCatStr = "5.36.147.86.1.1"
                });
                ports.Add(new CbMoldInfoDto()
                {
                    DevName = "CXC078",
                    Ip = "192.168.101.11",
                    TwinCatStr = "5.26.230.228.1.1"
                });

                Console.WriteLine(ports.Count);
                return ports;
            }
            catch (Exception ex)
            {
                Log4netHelper.WriteLog("从Mongo获取失败", ex);
                return null;
            }
        }
    }
}
