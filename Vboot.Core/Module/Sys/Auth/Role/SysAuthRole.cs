using System.Collections.Generic;
using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

[SugarTable("sys_auth_role", TableDescription = "权限管理角色表")]
[Description("权限管理角色表")]
public class SysAuthRole : BaseMainEntity
{
    [SugarColumn(ColumnDescription = "分类", IsNullable = true, Length = 32)]
    public string type { get; set; }

    [SugarColumn(ColumnDescription = "标签", IsNullable = true, Length = 32)]
    public string label { get; set; }

    [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 64)]
    public string notes { get; set; }

    [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
    public int ornum { get; set; }

    [SugarColumn(IsIgnore = true)] public List<SysAuthMenu> menus { get; set; }

    [SugarColumn(IsIgnore = true)] public List<SysOrg> orgs { get; set; }
}