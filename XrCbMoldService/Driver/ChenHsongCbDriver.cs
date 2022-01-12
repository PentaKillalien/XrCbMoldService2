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
                MongoDbHelper<MachineDto> mdh = new MongoDbHelper<MachineDto>(MongodbHostOptions.Devicers);
                List<CbMoldInfoDto> ports = new List<CbMoldInfoDto>();
                var records = mdh.FindAll();

                foreach (var item in records)
                {
                    foreach (var item2 in item.MachineControls)
                    {
                        CbMoldInfoDto a = new CbMoldInfoDto();
                        a.Ip= item2.IpAddress;
                        if (item2.Port != "1")
                        {
                            
                            a.DevName = item.DevId;
                            a.TwinCatStr = item2.Port;
                            ports.Add(a);
                        }
                    }
                }
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
