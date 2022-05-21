using System;
using SqlSugar;

namespace Vboot.Core.Module.Bpm;

[SugarTable("bpm_node_main", TableDescription = "流程节点表")]
public class BpmNodeMain
{
    [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, Length = 36)]
    public string id { get; set; }

    [SugarColumn(ColumnDescription = "当前节点编号", IsNullable = true, Length = 32)]
    public string facno { get; set; }

    [SugarColumn(ColumnDescription = "当前节点编号", IsNullable = true, Length = 126)]
    public string facna { get; set; }

    [SugarColumn(ColumnDescription = "当前节点类型", IsNullable = true, Length = 32)]
    public string facty { get; set; }

    [SugarColumn(ColumnDescription = "流程实例id", IsNullable = true, Length = 32)]
    public string proid { get; set; }

    [SugarColumn(ColumnDescription = "状态", IsNullable = true, Length = 8)]
    public string state { get; set; }

    [SugarColumn(ColumnDescription = "开始时间", IsNullable = true, IsOnlyIgnoreUpdate = true)]
    public DateTime? sttim { get; set; }=DateTime.Now;
}