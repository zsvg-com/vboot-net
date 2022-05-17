using System.Collections.Generic;
using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

[SugarTable("sys_org_group", TableDescription = "组织机构群组表")]
[Description("组织机构群组表")]
public class SysOrgGroup : BaseMainEntity
{
    [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 64)]
    public string notes { get; set; }

    [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
    public int ornum { get; set; }


    [SugarColumn(IsIgnore = true)] public List<SysOrg> members { get; set; }
}