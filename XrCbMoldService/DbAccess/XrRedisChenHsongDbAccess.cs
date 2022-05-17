/**
* 命名空间: XrCbMoldService.DbAccess
*
* 功 能： N/A
* 类 名：XrRedisChenHsongDbAccess.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/2/26 9:28:47  彭政亮 初版
*
* Copyright (c) 2020 Lir Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：宁波瑞辉智能科技有限公司 　　　　　 　　　　　　　    　│
*└──────────────────────────────────┘
*/
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrCbMoldService.Dto;

namespace XrCbMoldService.DbAccess
{
    public class XrRedisChenHsongDbAccess
    {
        private ConnectionMultiplexer _conn;
        private IDatabase database;
        public XrRedisChenHsongDbAccess()
        {
            try
            {
                _conn = ConnectionMultiplexer.Connect("192.168.0.19:6379,allowadmin=true");//初始化
                database = _conn.GetDatabase(11);//指定连接库 11

            }
            catch (Exception ex)
            {
                Console.WriteLine("Redis服务器连接不上");
                Log4netHelper.WriteLog("Redis服务器连接不上", ex);
            }
        }
        /// <summary>
        /// 根据key获取一个MachineRunStateDto
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public MachineRunStateDto GetOneDto(string key)
        {
            try
            {
                var a = JsonConvert.DeserializeObject<MachineRunStateDto>(database.StringGet(key).ToString());
                Console.WriteLine($"Redis获取{key}");
                return a;
            }
            catch (Exception ex)
            {
                Log4netHelper.WriteLog("转换异常", ex);
                return null;
            }

        }
    }
}
