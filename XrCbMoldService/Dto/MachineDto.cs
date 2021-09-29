/**
* 命名空间: XrCbMoldService.Dto
*
* 功 能： N/A
* 类 名：MachineDto.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/2/26 10:06:22  彭政亮 初版
*
* Copyright (c) 2020 Lir Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：宁波瑞辉智能科技有限公司 　　　　　 　　　　　　　    　│
*└──────────────────────────────────┘
*/
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrCbMoldService.Dto
{
    public class MachineDto
    {
        /// <summary>
        /// 唯一Id
        /// </summary>
        public ObjectId _id { get; set; }
        /// <summary>
        /// 驱动Id 在全局中必须唯一
        /// </summary>
        public string DevId { get; set; }
        /// <summary>
        /// 驱动名称
        /// </summary>
        public string DevName { get; set; }
        /// <summary>
        /// 设备类别 PS:前端根据此类别在看板中进行图片展示
        /// </summary>
        public string DevType { get; set; }
        DateTime m_dateTime = new DateTime();
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpDateTime
        {
            get { return m_dateTime; }
            set
            {
                m_dateTime = value == null ? DateTime.Now : value;
                this.UpDateTimeStr = m_dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                this.UpDateTimsSpan = GetNowTimeSpan(m_dateTime);
            }
        }
        /// <summary>
        /// UpDateTimeStr
        /// </summary>
        public string UpDateTimeStr { get; set; }
        /// <summary>
        /// UpDateTimsSpan
        /// </summary>
        public long UpDateTimsSpan { get; set; }
        /// <summary>
        /// 车间代码
        /// </summary>
        public string WorkShopInfoCode { get; set; }
        /// <summary>
        /// 工位代码
        /// </summary>
        /// <summary>
        /// 工位代码
        /// </summary>
        public string WorkStageInfoCode { get; set; }
        /// <summary>
        /// 产线代码
        /// </summary>
        public string ProductShopInfoCode { get; set; }
        /// <summary>
        /// 获取指定时间的时间戳
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        public static long GetNowTimeSpan(DateTime dt)
        {
            try
            {
                var ts = (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                return ts;
            }
            catch
            {
                return -1;
            }

        }
        /// <summary>
        /// 设备Dto
        /// </summary>
        public MachineDto()
        {
            UpDateTime = DateTime.Now;
        }
        /// <summary>
        /// 控制器列表
        /// </summary>
        public List<MachineControlConfigDto> MachineControls { get; set; }


    }
    /// <summary>
    /// MachineControlConfigDto
    /// </summary>
    public class MachineControlConfigDto
    {
        /// <summary>
        /// 控制器编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 控制器型号（类别）
        /// </summary>
        public ControlType ControlType { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 从机地址
        /// </summary>
        public string SlaveID { get; set; }

        /// <summary>
        /// 机架号
        /// </summary>
        public short Rack { get; set; }

        /// <summary>
        /// Cpu槽号
        /// </summary>
        public short Slot { get; set; }

        /// <summary>
        /// 仪表列表
        /// </summary>
        public List<MachineMateDto> Mates { get; set; }

        /// <summary>
        /// 数据项
        /// </summary>
        public List<MachineDataItemDto> DataItems { get; set; }
    }
    /// <summary>
    /// MachineMateDto
    /// </summary>
    public class MachineMateDto
    {
        /// <summary>
        /// 仪表编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 仪表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public string MinValue { get; set; }

        /// <summary>
        /// 仪表状态
        /// </summary>
        public MateState MateState { get; set; } = MateState.Normal;

        /// <summary>
        /// 单位
        /// </summary>
        public string MateUnit { get; set; }

        string m_Value = string.Empty;
        /// <summary>
        /// 实际值
        /// </summary>
        public string Value
        {
            get { return m_Value; }
            set
            {
                m_Value = value;
                this.UpDateTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.UpTimeStamp = GetNowTimeSpan().ToString();
            }
        }
        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetNowTimeSpan()
        {
            try
            {
                var ts = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                return ts;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 标准值
        /// </summary>
        public string StandardValue { get; set; }

        /// <summary>
        /// 数据地址 通过该地址解析数据通道中那个值是该仪表的值 
        /// </summary>
        public string DataItemAddress { get; set; }
        /// <summary>
        /// 值更新时间戳
        /// </summary>
        public string UpTimeStamp { get; set; }
        /// <summary>
        /// 值更新时间戳
        /// </summary>
        public string UpDateTimeStr { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public VarType VarType { get; set; }

        /// <summary>
        /// 数据运算类型
        /// </summary>
        public AnalysisType AnalysisType { get; set; }
    }
    /// <summary>
    /// MachineDataItemDto
    /// </summary>
    public class MachineDataItemDto
    {
        /// <summary>
        /// 地址
        /// </summary>
        public byte BitAdr { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public DataType DataType { get; set; }
        /// <summary>
        /// 寄存器
        /// </summary>
        public int DB { get; set; }
        /// <summary>
        /// 起始地址
        /// </summary>
        public int StartByteAdr { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 值类型
        /// </summary>
        public VarType VarType { get; set; }
    }
}
