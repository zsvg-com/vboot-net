using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Ass;

[SugarTable("ass_dict_main", TableDescription = "项目信息表")]
[Description("项目信息表")]
public class AssDictMain : BaseEntity
{
    [SugarColumn(ColumnDescription = "可用标记：1可用，0禁用", IsNullable = true)]
    public bool? avtag { get; set; }

    [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
    public int ornum { get; set; }

    [SugarColumn(ColumnDescription = "类型", IsNullable = true, Length = 32)]
    public string cateid { get; set; }

    [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 64)]
    public string notes { get; set; }

    [SugarColumn(ColumnDescription = "代码", Length = 32)]
    public string code { get; set; }
}