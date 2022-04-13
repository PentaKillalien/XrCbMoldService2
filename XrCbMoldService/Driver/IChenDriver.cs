/**
* 命名空间: XrCbMoldService.Driver
*
* 功 能： N/A
* 类 名：IChenDriver.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/2/26 9:35:16  彭政亮 初版
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
    public class IChenDriver
    {
        /// <summary>
        /// Oracle数据访问
        /// </summary>
        private MachineRealTimeDtoDbAccess Mrtdda;
        /// <summary>
        /// 无参构造器
        /// </summary>
        public IChenDriver()
        {
            Mrtdda = new MachineRealTimeDtoDbAccess();
            XrMidInfoGatherDriver driver = new XrMidInfoGatherDriver();
            driver.MidInfos = new Action<List<XrMidInfoDto>>(GetInfos);
        }
        private List<XrMidInfoDto> xrMidInfoDtos = new List<XrMidInfoDto>();
        private void GetInfos(List<XrMidInfoDto> cc) {
            xrMidInfoDtos = cc;
        }
        /// <summary>
        /// 获取关联信息
        /// </summary>
        /// <returns></returns>
        private XrMidInfoDto GetRelationDto(string machineName)
        {
            if (xrMidInfoDtos.Count > 0)
            {
                foreach (var item in xrMidInfoDtos)
                {
                    try
                    {
                        if (item.MachineStation.Equals(machineName))
                        {
                            return item;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{machineName}获取关联信息失败{ex.Message}");
                        Log4netHelper.WriteLog($"{machineName}获取关联信息失败{ex.Message}");
                    }
                    
                }
            }
            return null;


        }
        

        /// <summary>
        /// CbMold数据插入到Oracle
        /// </summary>
        /// <param name="machineRunStateDtos"></param>
        /// <returns></returns>
        public void InsterMachineRealtimeOne(MachineRunStateDto m,string ip)
        {
            //OracleHelper.ExecuteNonQuery("insert into RH_IOTTEST (TA,TB,TC) values ('c2','xiaozhang','c3')");
            //string strSql = "insert into M_IOTMACHINE_REALTIME (FMACHINEID,FSTATUS,FYIELD,FGOOD,FBAD,FDATE,FTIMESTAMP,POWER,FSPM,FPERCOUNT) values ('Rh001','3',0,1,2,to_date('2009-5-7 17:09:37','yyyy-mm-dd HH24:MI:SS'),'157294100085',0,1,2)";
            try
            {

                MachineRealTimeDto dto = new MachineRealTimeDto();
                dto.FMACHINEID = m.DevId;//去掉
                var middto = GetRelationDto(dto.FMACHINEID);
                if (m.RunState == MachineState.Run.ToString())
                {
                    dto.FSTATUS = "1";
                }
                else if (m.RunState == MachineState.PowerOff.ToString())
                {
                    dto.FSTATUS = "3";
                }
                else
                {
                    dto.FSTATUS = "4";
                }

                dto.FYIELD = m.ProductQtySum;
                dto.FGOOD = m.ProductQtySum;
                dto.FBAD = 0;
                dto.FDATE = DateTime.Now;

                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                dto.FTIMESTAMP = Convert.ToInt64(ts.TotalMilliseconds).ToString();   //采集记录的时间戳
                dto.POWER = 0;
                dto.FSPM = 0;
                dto.FPERCOUNT = 0;
                if (middto != null)
                {
                    Console.WriteLine(middto.MachineStation + "************************************");
                    dto.FBILLNOINFO = middto.FBILLNOINFO;
                    dto.FMATERIAL = middto.FMATERIAL;
                    dto.FMATERIALNAME = middto.FMATERIALNAME;
                }

                dto.FIP = ip;
                Mrtdda.Insert(dto);

            }
            catch (Exception ex)
            {
                Log4netHelper.WriteLog("CBmold插入Oracle 失败", ex);
                Console.WriteLine("CBmold-->插入Oracle数据失败");
            }

        }

    }
}
