using System;
using SqlSugar;

namespace Vboot.Core.Module.Bpm;

[SugarTable("bpm_node_hist", TableDescription = "流程节点历史表")]
public class BpmNodeHist
{
    [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, Length = 36)]
    public string id { get; set; }

    [SugarColumn(ColumnDescription = "当前节点编号", IsNullable = true, Length = 32)]
    public string facno { get; set; }

    [SugarColumn(ColumnDescription = "当前节点编号", IsNullable = true, Length = 126)]
    public string facna { get; set; }

    [SugarColumn(ColumnDescription = "当前节点类型", IsNullable = true, Length = 32)]
    public string facty { get; set; }

    [SugarColumn(ColumnDescription = "目标节点ID", IsNullable = true, Length = 32)]
    public string tarno { get; set; }

    [SugarColumn(ColumnDescription = "目标节点名称", IsNullable = true, Length = 32)]
    public string tarna { get; set; }

    [SugarColumn(ColumnDescription = "流程实例id", IsNullable = true, Length = 32)]
    public string proid { get; set; }

    [SugarColumn(ColumnDescription = "状态", IsNullable = true, Length = 8)]
    public string state { get; set; }

    [SugarColumn(ColumnDescription = "开始时间", IsNullable = true, IsOnlyIgnoreUpdate = true)]
    public DateTime? sttim { get; set; }

    [SugarColumn(ColumnDescription = "结束时间", IsNullable = true)]
    public DateTime? entim { get; set; }
}