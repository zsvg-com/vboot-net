using System.Collections.Generic;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Sys;

namespace Vboot.Application.Sa;

[SugarTable("sa_agent_main", TableDescription = "代理商主信息")]
public class SaAgentMain : BaseMainEntity
{
    /// <summary>
    /// 代理商地址
    /// </summary>
    [SugarColumn(ColumnDescription = "代理商地址", IsNullable = true, Length = 255)]
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