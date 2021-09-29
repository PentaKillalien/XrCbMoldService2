/**
* 命名空间: XrCbMoldService.Dto
*
* 功 能： N/A
* 类 名：MachineState.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/2/26 9:39:17  彭政亮 初版
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
    /// <summary>
    /// 设备状态 PowerOff=停机 Run=运行 Standby=待机  Unconnected=空转 ConnectionEx=异常
    /// </summary>
    public enum MachineState
    {
        /// <summary>
        /// 停机
        /// </summary>
        PowerOff = 3,

        /// <summary>
        /// 运行
        /// </summary>
        Run = 1,

        /// <summary>
        /// 待机
        /// </summary>
        Standby = 4,

        /// <summary>
        /// 空转
        /// </summary>
        Unconnected = 2,

        /// <summary>
        /// 异常
        /// </summary>
        ConnectionEx = 9
    }
    public enum VarType
    {
        /// <summary>
        ///
        /// </summary>
        Bit = 0,

        /// <summary>
        ///
        /// </summary>
        Byte = 1,

        /// <summary>
        ///
        /// </summary>
        Word = 2,

        /// <summary>
        ///
        /// </summary>
        DWord = 3,

        /// <summary>
        ///
        /// </summary>
        Int = 4,

        /// <summary>
        ///
        /// </summary>
        DInt = 5,

        /// <summary>
        ///
        /// </summary>
        Real = 6,

        /// <summary>
        ///
        /// </summary>
        String = 7,

        /// <summary>
        ///
        /// </summary>
        StringEx = 8,

        /// <summary>
        ///
        /// </summary>
        Timer = 9,

        /// <summary>
        ///
        /// </summary>
        Counter = 10
    }
    public enum DataType
    {
        /// <summary>
        ///
        /// </summary>
        Counter = 28,

        /// <summary>
        ///
        /// </summary>
        Timer = 29,

        /// <summary>
        ///
        /// </summary>
        Input = 129,

        /// <summary>
        ///
        /// </summary>
        Output = 130,

        /// <summary>
        ///
        /// </summary>
        Memory = 131,

        /// <summary>
        ///
        /// </summary>
        DataBlock = 132
    }
    public enum MateState
    {
        /// <summary>
        ///
        /// </summary>
        NotEnabled = 0,

        /// <summary>
        ///
        /// </summary>
        Normal = 1,

        /// <summary>
        ///
        /// </summary>
        Alert = 2,
    }
    public enum ControlType
    {
        /// <summary>
        ///
        /// </summary>
        CNC_Fanuc = 0,

        /// <summary>
        ///
        /// </summary>
        CNC_Mitsubishi = 1,

        /// <summary>
        ///
        /// </summary>
        CNC_Syntec = 2,

        /// <summary>
        ///
        /// </summary>
        PLC_Mitsubishi = 3,

        /// <summary>
        ///
        /// </summary>
        PLC_Omron = 4,

        /// <summary>
        ///
        /// </summary>
        PLC_Siemens = 5,

        /// <summary>
        ///
        /// </summary>
        CPT_200 = 6,

        /// <summary>
        ///
        /// </summary>
        CPT_430 = 7,

        /// <summary>
        ///
        /// </summary>
        CPT_700 = 8,

        /// <summary>
        ///
        /// </summary>
        Injection_LS = 9,

        /// <summary>
        ///
        /// </summary>
        Injection_ChenHsong = 10,

        /// <summary>
        ///
        /// </summary>
        Injection_ChenHsongCB = 11,

        /// <summary>
        ///
        /// </summary>
        AmtElectroplate = 12,

        /// <summary>
        ///
        /// </summary>
        S7_200 = 13,

        /// <summary>
        ///
        /// </summary>
        S7_1500 = 14,

        /// <summary>
        ///
        /// </summary>
        ArisPtlXGate = 15,

        /// <summary>
        ///
        /// </summary>
        RhPtl = 16,

        /// <summary>
        ///
        /// </summary>
        RhBroadcast = 17,
    }
    public enum AnalysisType
    {
        /// <summary>
        /// 求和
        /// </summary>
        Sum = 0,

        /// <summary>
        /// 求平均
        /// </summary>
        Average = 1,

        /// <summary>
        /// 累加
        /// </summary>
        accumulator = 2
    }
}
