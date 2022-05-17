using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Ass;

[SugarTable("ass_dict_data", TableDescription = "字典数据表")]
[Description("字典数据表")]
public class AssDictData : BaseEntity
{
    [SugarColumn(ColumnDescription = "可用标记：1可用，0禁用", IsNullable = true)]
    public bool? avtag { get; set; }

    [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
    public int ornum { get; set; }

    [SugarColumn(ColumnDescription = "分类ID", Length = 36)]
    public string cateid { get; set; }

    [SugarColumn(ColumnDescription = "代码", Length = 36)]
    public string code { get; set; }

    [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 64)]
    public string notes { get; set; }
}