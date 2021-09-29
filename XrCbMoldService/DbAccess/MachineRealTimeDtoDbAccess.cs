/**
* 命名空间: XrCbMoldService.DbAccess
*
* 功 能： N/A
* 类 名：MachineRealTimeDtoDbAccess.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/2/26 9:51:28  彭政亮 初版
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
using XrCbMoldService.Dto;

namespace XrCbMoldService.DbAccess
{
    public class MachineRealTimeDtoDbAccess : RhOracleRepository<MachineRealTimeDto>
    {
    }
}
