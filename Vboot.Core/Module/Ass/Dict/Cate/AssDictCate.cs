using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Ass
{
    [SugarTable("ass_dict_cate", TableDescription = "字典分类表")]
    [Description("字典分类表")]
    public class AssDictCate : BaseEntity
    {
        [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 64)]
        public string notes { get; set; }
    }
}