using System;
using System.ComponentModel;
using SqlSugar;

namespace Vboot.Core.Module.Sys;

[SugarTable("sys_org_rece", TableDescription = "组织架构使用记录")]
[Description("组织架构使用记录")]
public class SysOrgRece
{
    [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, Length = 36)]
    public string id { get; set; }

    [SugarColumn(ColumnDescription = "用户ID", Length = 36)]
    public string userid { get; set; }

    [SugarColumn(ColumnDescription = "组织架构ID", Length = 36)]
    public string orgid { get; set; }

    [SugarColumn(ColumnDescription = "最近使用时间", IsNullable = true)]
    public virtual DateTime? uptim { get; set; }
}