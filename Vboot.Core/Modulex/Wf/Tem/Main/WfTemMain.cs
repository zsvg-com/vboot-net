using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Wf
{
    [SugarTable("wf_tem_main", TableDescription = "流程模板表")]
    [Description("流程模板表")]
    public class WfTemMain : BaseEntity
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
        
        // [SugarColumn(ColumnDescription = "XML", IsNullable = true,Length=4000)]
        [SugarColumn(ColumnDescription = "XML",ColumnDataType = "longtext", IsNullable = true)]
        public string xml { get; set; }
        
    }
}