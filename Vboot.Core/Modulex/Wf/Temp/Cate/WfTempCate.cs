using System.Collections.Generic;
using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Wf
{
    [SugarTable("wf_temp_cate", TableDescription = "流程分类表")]
    [Description("流程分类表")]
    public class WfTempCate : BaseMainEntity
    {
        // [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true,Length = 36)]
        // public string id { get; set; }
        
        [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 64)]
        public string notes { get; set; }

        [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
        public int ornum { get; set; }

        [SugarColumn(ColumnDescription = "父ID", IsNullable = true, Length = 32)]
        public string pid { get; set; }
        
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public string pname { get; set; }

        [SqlSugar.SugarColumn(IsIgnore = true)]
        public List<WfTempCate> children { get; set; }

        

    }
}