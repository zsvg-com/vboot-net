using System;
using System.ComponentModel;
using SqlSugar;

namespace Vboot.Core.Module.Sys;

[SugarTable("oa_flow_rece", TableDescription = "流程使用记录")]
[Description("流程使用记录")]
public class OaFlowRece
{
    [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, Length = 36)]
    public string id { get; set; }

    [SugarColumn(ColumnDescription = "用户ID", Length = 36)]
    public string userid { get; set; }

    [SugarColumn(ColumnDescription = "流程模板ID", Length = 36)]
    public string flowid { get; set; }

    [SugarColumn(ColumnDescription = "最近使用时间", IsNullable = true)]
    public virtual DateTime? uptim { get; set; }
}