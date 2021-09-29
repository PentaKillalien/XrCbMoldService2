/**
* 命名空间: XrCbMoldService.Dto
*
* 功 能： N/A
* 类 名：GatherDataModel.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/2/26 9:52:15  彭政亮 初版
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

namespace XrCbMoldService.Dto
{
    public class GatherDataModel
    {
        /// <summary>
        /// 无参构造器
        /// </summary>
        public GatherDataModel()
        {
            TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 寄存器
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// 寄存器值
        /// </summary>
        public string GetherValue { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }
    }
}
