using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Oa;

[SugarTable("oa_flow_temp", TableDescription = "OA流程模板表")]
[Description("OA流程模板表")]
public class OaFlowTemp : BaseMainEntity
{
    [SugarColumn(ColumnDescription = "分类ID", IsNullable = true, Length = 32)]
    public string catid { get; set; }

    [SugarColumn(IsIgnore = true)] public ZidName cate { get; set; }

    [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 128)]
    public string notes { get; set; }

    [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
    public int ornum { get; set; }

    [SugarColumn(ColumnDescription = "全局流程模板ID", IsNullable = true, Length = 32)]
    public string protd { get; set; }

    [SugarColumn(IsIgnore = true)] public string prxml { get; set; }

    [SugarColumn(ColumnDescription = "vform", ColumnDataType = "varchar(max)", IsNullable = true)]
    public string vform { get; set; }
}