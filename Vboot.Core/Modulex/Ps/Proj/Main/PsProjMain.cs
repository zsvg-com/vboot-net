using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Ps
{
    [SugarTable("ps_proj_main", TableDescription = "字典信息表")]
    [Description("字典信息表")]
    public class PsProjMain : BaseEntity
    {
        [SugarColumn(ColumnDescription = "可用标记：1可用，0禁用", IsNullable = true)]
        public bool? avtag { get; set; }
        
        [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
        public int ornum { get; set; }
        
        [SugarColumn(ColumnDescription = "类型", IsNullable = true, Length = 32)]
        public string cateid { get; set; }
        
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public string catename { get; set; }

        [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 64)]
        public string notes { get; set; }
        
        
    }
}