using System;
using System.ComponentModel;
using SqlSugar;

namespace Vboot.Core.Module.Sys;

[SugarTable("sys_todo_user", TableDescription = "系统待办完成表")]
[Description("系统待办完成表")]
public class SysTodoDone 
{
    [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, Length = 36)]
    public string id { get; set; }

    [SugarColumn(ColumnDescription = "待办ID", IsNullable = true, Length = 32)]
    public string tid { get; set; }

    [SugarColumn(ColumnDescription = "用户ID", IsNullable = true, Length = 32)]
    public string uid { get; set; }
    
    [SugarColumn(ColumnDescription = "完成时间", IsNullable = true)]
    public DateTime? entim { get; set; } = DateTime.Now;
}