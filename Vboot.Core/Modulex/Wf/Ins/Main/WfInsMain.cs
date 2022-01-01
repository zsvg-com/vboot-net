using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Wf
{
    [SugarTable("wf_ins_main", TableDescription = "流程实例表")]
    [Description("流程实例表")]
    public class WfInsMain : BaseEntity
    {
        
        [SugarColumn(ColumnDescription = "模板", IsNullable = true, Length = 32)]
        public string temid { get; set; }
        
        [SugarColumn(IsIgnore = true)]
        public string temname { get; set; }

        [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 64)]
        public string notes { get; set; }
        
        // [SugarColumn(ColumnDescription = "XML", IsNullable = true,Length=4000)]
        [SugarColumn(ColumnDescription = "XML",ColumnDataType = "longtext", IsNullable = true)]
        public string xml { get; set; }
        
    }
}