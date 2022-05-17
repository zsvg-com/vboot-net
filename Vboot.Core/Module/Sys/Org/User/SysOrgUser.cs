using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

[SugarTable("sys_org_user", TableDescription = "系统用户表")]
[Description("系统用户表")]
public class SysOrgUser : BaseMainEntity
{
    [SugarColumn(ColumnDescription = "部门ID", IsNullable = true, Length = 36)]
    public string deptid { get; set; }

    [SugarColumn(IsIgnore = true)] public SysOrgDept dept { get; set; }

    [SugarColumn(ColumnDescription = "层级", IsNullable = true, Length = 1000)]
    public string tier { get; set; }

    [SugarColumn(ColumnDescription = "职务", IsNullable = true, Length = 255)]
    public string job { get; set; }

    [SugarColumn(ColumnDescription = "用户名", IsNullable = true, Length = 32)]
    public string usnam { get; set; }

    [JsonIgnore]
    [SugarColumn(ColumnDescription = "密码", IsNullable = true, Length = 64, IsOnlyIgnoreUpdate = true)]
    public string pacod { get; set; }

    [SugarColumn(ColumnDescription = "邮箱", IsNullable = true, Length = 64)]
    public string email { get; set; }

    [SugarColumn(ColumnDescription = "手机号", IsNullable = true, Length = 32)]
    public string monum { get; set; }

    [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 255)]
    public string notes { get; set; }

    [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
    public int ornum { get; set; }

    [SugarColumn(ColumnDescription = "ldap层级名称", IsNullable = true, Length = 1000)]
    public string ldnam { get; set; }


    [SugarColumn(ColumnDescription = "准备标记", IsNullable = true)]
    public bool retag { get; set; }


    [SugarColumn(ColumnDescription = "最后登录时间", IsNullable = true)]
    public DateTime lalot { get; set; }

    [SugarColumn(ColumnDescription = "最后登录IP", IsNullable = true, Length = 20)]
    public string laloi { get; set; }
}