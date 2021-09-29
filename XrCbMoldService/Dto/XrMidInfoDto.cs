/*
* 命名空间: XrCbMoldService.Dto
*
* 功 能： N/A
* 类 名：XrMidInfoDto.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/9/29 9:47:44  彭政亮 初版
*
* Copyright (c) 2021 Lir Corporation. All rights reserved.
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
    public class XrMidInfoDto
    {
        public string MachineStation { get; set; }

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
