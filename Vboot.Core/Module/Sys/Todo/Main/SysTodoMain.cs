using System;
using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

[SugarTable("sys_todo_main", TableDescription = "系统待办待阅表")]
[Description("系统待办待阅表")]
public class SysTodoMain : BaseEntity
{
    //1：待办；2：待阅
    [SugarColumn(ColumnDescription = "类型", IsNullable = true, Length = 8)]
    public string type { get; set; }

    [SugarColumn(ColumnDescription = "紧急度", IsNullable = true, Length = 8)]
    public string level { get; set; }

    [SugarColumn(ColumnDescription = "模型分类", IsNullable = true, Length = 128)]
    public string modca { get; set; }

    [SugarColumn(ColumnDescription = "模型ID", IsNullable = true, Length = 32)]
    public string modid { get; set; }

    [SugarColumn(ColumnDescription = "链接", IsNullable = true, Length = 512)]
    public string link { get; set; }

    [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 256)]
    public string notes { get; set; }

    [SugarColumn(ColumnDescription = "创建者Id", IsNullable = true, Length = 36)]
    public string crman { get; set; }

    [SugarColumn(ColumnDescription = "创建时间", IsNullable = true, IsOnlyIgnoreUpdate = true)]
    public DateTime? crtim { get; set; } = DateTime.Now;
}