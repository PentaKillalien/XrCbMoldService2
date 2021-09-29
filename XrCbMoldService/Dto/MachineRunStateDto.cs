/**
* 命名空间: XrCbMoldService.Dto
*
* 功 能： N/A
* 类 名：MachineRunStateDto.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/2/26 9:32:54  彭政亮 初版
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
    public class MachineRunStateDto
    {
        /// <summary>
        /// 运行状态
        /// </summary>
        public string RunState { get; set; }
        /// <summary>
        /// ResetStopWatch
        /// </summary>
        public Boolean ResetStopWatch { get; set; }
        /// <summary>
        /// AlaleMsg
        /// </summary>
        public string AlaleMsg { get; set; }
        /// <summary>
        /// Mates下单元
        /// </summary>
        public List<MatesUnit> Mates { get; set; }
        /// <summary>
        /// ShowBodys下单元
        /// </summary>
        public List<ShowBodysUnit> ShowBodys { get; set; }
        /// <summary>
        /// RunRate_Day
        /// </summary>
        public int RunRate_Day { get; set; }
        /// <summary>
        /// RunStartTime
        /// </summary>
        public string RunStartTime { get; set; }
        /// <summary>
        /// ProductQtyHistory
        /// </summary>
        public int ProductQtyHistory { get; set; }
        /// <summary>
        /// ProductQty
        /// </summary>
        public int ProductQty { get; set; }
        /// <summary>
        /// ProductQtySum
        /// </summary>
        public int ProductQtySum { get; set; }
        /// <summary>
        /// GatherCount
        /// </summary>
        public int GatherCount { get; set; }
        /// <summary>
        /// GatherGoodCount
        /// </summary>
        public int GatherGoodCount { get; set; }
        /// <summary>
        /// RhThreadId
        /// </summary>
        public string RhThreadId { get; set; }
        /// <summary>
        /// DR_RunValue
        /// </summary>
        public int DR_RunValue { get; set; }
        /// <summary>
        /// DR_RunSetValue
        /// </summary>
        public int DR_RunSetValue { get; set; }
        /// <summary>
        /// DR_StandbyValue
        /// </summary>
        public int DR_StandbyValue { get; set; }
        /// <summary>
        /// DR_StandbySetValue
        /// </summary>
        public int DR_StandbySetValue { get; set; }
        /// <summary>
        /// DR_AlertValue
        /// </summary>
        public int DR_AlertValue { get; set; }
        /// <summary>
        /// DR_AlertSetValue
        /// </summary>
        public int DR_AlertSetValue { get; set; }
        /// <summary>
        /// DR_PowerOffValue
        /// </summary>
        public int DR_PowerOffValue { get; set; }
        /// <summary>
        /// DR_PowerOffSetValue
        /// </summary>
        public int DR_PowerOffSetValue { get; set; }
        /// <summary>
        /// DevId
        /// </summary>
        public string DevId { get; set; }
        /// <summary>
        /// DevName
        /// </summary>
        public string DevName { get; set; }
        /// <summary>
        /// UpDateTime
        /// </summary>
        public DateTime UpDateTime { get; set; }
        /// <summary>
        /// UpDateTimeStr
        /// </summary>
        public string UpDateTimeStr { get; set; }
        /// <summary>
        /// _id
        /// </summary>
        public string _id { get; set; }
        /// <summary>
        /// UpDateTimsSpan
        /// </summary>
        public int UpDateTimsSpan { get; set; }
        /// <summary>
        /// WorkShopInfoCode
        /// </summary>
        public string WorkShopInfoCode { get; set; }
    }
    /// <summary>
    /// Mates下的单元
    /// </summary>
    public class MatesUnit
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// MaxValue
        /// </summary>
        public string MaxValue { get; set; }
        /// <summary>
        /// MinValue
        /// </summary>
        public string MinValue { get; set; }
        /// <summary>
        /// MateState
        /// </summary>
        public string MateState { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// StandardValue
        /// </summary>
        public string StandardValue { get; set; }
        /// <summary>
        /// DataItemAddress
        /// </summary>
        public string DataItemAddress { get; set; }
        /// <summary>
        /// UpTimeStamp
        /// </summary>
        public string UpTimeStamp { get; set; }
        /// <summary>
        /// UpDateTimeStr
        /// </summary>
        public string UpDateTimeStr { get; set; }
        /// <summary>
        /// Function
        /// </summary>
        public string Function { get; set; }
        /// <summary>
        /// VarType
        /// </summary>
        public string VarType { get; set; }
        /// <summary>
        /// AnalysisType
        /// </summary>
        public string AnalysisType { get; set; }
    }
    /// <summary>
    /// ShowBodys下单元
    /// </summary>
    public class ShowBodysUnit
    {
        /// <summary>
        /// Rhnum
        /// </summary>
        public int RhNum { get; set; }
        /// <summary>
        /// RhTitle
        /// </summary>
        public string RhTitle { get; set; }
        /// <summary>
        /// RhContent
        /// </summary>
        public string RhContent { get; set; }
    }
}
