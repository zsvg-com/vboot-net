using System.Collections.Generic;
using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

[SugarTable("sys_org_dept", TableDescription = "系统部门表")]
[Description("系统部门表")]
public class SysOrgDept : BaseMainEntity
{
    [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
    public int ornum { get; set; }

    [SugarColumn(ColumnDescription = "标签", IsNullable = true, Length = 32)]
    public string label { get; set; }

    [SugarColumn(ColumnDescription = "备注", IsNullable = true)]
    public string notes { get; set; }

    [SugarColumn(ColumnDescription = "类别：1机构，2部门", IsNullable = true)]
    public int type { get; set; }

    [SugarColumn(ColumnDescription = "父ID", IsNullable = true, Length = 32)]
    public string pid { get; set; }

    [SqlSugar.SugarColumn(IsIgnore = true)]
    public SysOrg parent { get; set; }

    [SqlSugar.SugarColumn(IsIgnore = true)]
    public List<SysOrgDept> children { get; set; }

    [SugarColumn(ColumnDescription = "层级", IsNullable = true, Length = 1000)]
    public string tier { get; set; }

    [SugarColumn(ColumnDescription = "ldap层级名称", IsNullable = true, Length = 1000)]
    public string ldnam { get; set; }
}