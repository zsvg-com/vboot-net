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
    private string type { get; set; }
}