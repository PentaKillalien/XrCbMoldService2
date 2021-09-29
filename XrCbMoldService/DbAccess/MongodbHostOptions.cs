/**
* 命名空间: XrCbMoldService.DbAccess
*
* 功 能： N/A
* 类 名：MongodbHostOptions.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/2/26 10:05:08  彭政亮 初版
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

namespace XrCbMoldService.DbAccess
{
    public class MongodbHostOptions : IMongodbHostOptions
    {
        public MongodbHostOptions()
        {
            try
            {
                mongoDbAddress = System.Configuration.ConfigurationManager.AppSettings["MongoDbAddress"];
                //mongoDbAddress = Utility.RhConfig.RhServerConfig.RhDbConfig.FirstOrDefault(m => m.DbType == "MongoDB").DbIp;
            }
            catch (Exception ex)
            {
                throw new Exception("初始化MongoDB连接时发生错误，请检查配置文件是否存在", ex);
            }
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// 库
        /// </summary>
        public string DataBase { get; set; }

        /// <summary>
        /// 表
        /// </summary>
        public string Table { get; set; }

        private static string mongoDbAddress;

        /// <summary>
        /// MongoDB数据库访问字符串
        /// </summary>
        public static string MongoDbAddress { get { return mongoDbAddress; } }

        // public static string MongoDbAddress { get; } = "mongodb://10.40.255.5:27017";
        private static MongodbHostOptions MesMould
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "CptSop",
                Table = "MesMould"
            };
        }

        private static MongodbHostOptions MesSop
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "CptSop",
                Table = "MesSop"
            };
        }

        private static MongodbHostOptions WorkingRecord
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "CptSop",
                Table = "WorkingRecord"
            };
        }

        private static MongodbHostOptions MesOrder
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "CptSop",
                Table = "MesOrder"
            };
        }

        private static MongodbHostOptions MesWorkpiece
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "CptSop",
                Table = "MesWorkpiece"
            };
        }

        private static MongodbHostOptions Machines
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "RhIot",
                Table = "Machines"
            };
        }

        /// <summary>
        /// 驱动数据库
        /// </summary>
        public static MongodbHostOptions Devicers
        {
            get => new MongodbHostOptions()
            {
                Connection = "mongodb://192.168.0.19:27017",
                DataBase = "RhIot",
                Table = "Devicers"
            };
        }

        /// <summary>
        /// 控制器配置信息
        /// </summary>
        public static MongodbHostOptions ControlConfig
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "RhIot",
                Table = "ControlConfig"
            };
        }

        public static MongodbHostOptions MachineRealTime
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "RhIot",
                Table = "MachineRealTime"
            };
        }

        public static MongodbHostOptions RhIotLogs
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "RhIot",
                Table = "RhIotLogs"
            };
        }

        private static MongodbHostOptions MachineRealTest
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "RhIot",
                Table = "MachineRealTest"
            };
        }

        private static MongodbHostOptions MachineRun
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "RhIot",
                Table = "MachineRun"
            };
        }

        private static MongodbHostOptions MachineAlert
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "RhIot",
                Table = "MachineAlert"
            };
        }

        public static MongodbHostOptions PtlLamp
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "RhIot",
                Table = "PtlLamp"
            };
        }

        public static MongodbHostOptions RhIotUser
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "RhIot",
                Table = "RhIotUser"
            };
        }

        /// <summary>
        /// 开机率
        /// </summary>
        public static MongodbHostOptions MachineRate
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "Rate",
                Table = "MachineRate"
            };
        }

        /// <summary>
        /// 每月汇总表
        /// </summary>
        public static MongodbHostOptions MonthlyRate
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "Rate",
                Table = "MonthlyRate"
            };
        }

        /// <summary>
        /// 錯誤圖片表
        /// </summary>
        public static MongodbHostOptions ErrImgRate
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "CptIot",
                Table = "ErrImgRate"
            };
        }

        /// <summary>
        /// 历史记录
        /// </summary>
        public static MongodbHostOptions MachineHistory
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "Rate",
                Table = "MachineHistory"
            };
        }

        /// <summary>
        /// 报警记录
        /// </summary>
        public static MongodbHostOptions AlertRecord
        {
            get => new MongodbHostOptions()
            {
                Connection = MongoDbAddress,
                DataBase = "Record",
                Table = "AlertRecord"
            };
        }
    }
}
