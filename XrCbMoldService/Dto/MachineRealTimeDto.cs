/**
* 命名空间: XrCbMoldService.Dto
*
* 功 能： N/A
* 类 名：MachineRealTimeDto.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/2/26 9:36:06  彭政亮 初版
*
* Copyright (c) 2020 Lir Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：宁波瑞辉智能科技有限公司 　　　　　 　　　　　　　    　│
*└──────────────────────────────────┘
*/
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrCbMoldService.Dto
{
    [SugarTable("M_IOTMACHINE_REALTIME")]//当和数据库名称不一样可以设置别名
    public class MachineRealTimeDto
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string FMACHINEID { get; set; }

        /// <summary>
        /// 状态 1 运行 2空转 3 停机 4待机
        /// </summary>
        public string FSTATUS { get; set; }

        /// <summary>
        /// 总产量
        /// </summary>
        public decimal? FYIELD { get; set; }

        /// <summary>
        /// 良品数量
        /// </summary>
        public decimal? FGOOD { get; set; }

        /// <summary>
        /// 不良品数量
        /// </summary>
        public decimal? FBAD { get; set; }

        /// <summary>
        /// 生成记录的时间
        /// </summary>
        public DateTime FDATE { get; set; }

        /// <summary>
        /// 采集记录的时间戳
        /// </summary>
        public string FTIMESTAMP { get; set; }

        /// <summary>
        /// 电源
        /// </summary>
        public decimal? POWER { get; set; }

        /// <summary>
        /// SPM
        /// </summary>
        public decimal? FSPM { get; set; }

        /// <summary>
        /// 每次计数
        /// </summary>
        public decimal? FPERCOUNT { get; set; }
        /// <summary>
        /// 派工单号
        /// </summary>
        public string FBILLNOINFO { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string FMATERIAL { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string FMATERIALNAME { get; set; }
    }
}
