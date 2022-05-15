using System.Collections.Generic;
using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Oa
{
    [SugarTable("oa_flow_cate", TableDescription = "OA流程分类表")]
    [Description("OA流程分类表")]
    public class OaFlowCate : BaseMainEntity
    {
        
        [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 64)]
        public string notes { get; set; }

        [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
        public int ornum { get; set; }

        
        [SugarColumn(ColumnDescription = "父ID", IsNullable = true, Length = 32)]
        public string pid { get; set; }
        
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public string panam { get; set; }

        [SqlSugar.SugarColumn(IsIgnore = true)]
        public List<OaFlowCate> children { get; set; }


    }
}