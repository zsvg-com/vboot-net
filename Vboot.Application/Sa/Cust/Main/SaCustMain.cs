using System.Collections.Generic;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Sys;

namespace Vboot.Application.Sa;

[SugarTable("sa_cust_main", TableDescription = "客户主信息")]
public class SaCustMain : BaseMainEntity
{
    /// <summary>
    /// 客户地址
    /// </summary>
    [SugarColumn(ColumnDescription = "客户地址", IsNullable = true, Length = 255)]
    public string addre { get; set; }
    
    /// <summary>
    /// 经办人ID
    /// </summary>
    [SugarColumn(ColumnName = "opman",ColumnDescription = "经办人ID", IsNullable = true, Length = 36)]
    public string opmid { get; set; }
    
    /// <summary>
    /// 经办人
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public SysOrg opman { get; set; }
    
    /// <summary>
    /// 可查看者
    /// </summary>
    [SugarColumn(IsIgnore =true)]
    public List<SysOrg> viewers { get; set; }
        
    
}