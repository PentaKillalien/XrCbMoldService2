/**
* 命名空间: XrCbMoldService.DbAccess
*
* 功 能： N/A
* 类 名：RhOracleRepository.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/2/26 9:50:34  彭政亮 初版
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

namespace XrCbMoldService.DbAccess
{
    public class RhOracleRepository<T> : SimpleClient<T> where T : class, new()
    {
        public RhOracleRepository(ISqlSugarClient context = null) : base(context)//注意这里要有默认值等于null
        {
            if (context == null)
            {
                base.Context = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.Oracle,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                    ConnectionString = @"DATA SOURCE=192.168.0.43:1521/cptmes;PASSWORD=zxec11;PERSIST SECURITY INFO=True;USER ID=ZXEC"//连接符字串
                });
            }
        }
    }
}
