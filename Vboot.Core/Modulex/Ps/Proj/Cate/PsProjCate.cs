using System.Collections.Generic;
using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Ps
{
    [SugarTable("ps_proj_cate", TableDescription = "项目分类表")]
    [Description("项目分类表")]
    public class PsProjCate : BaseMainEntity
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
        public List<PsProjCate> children { get; set; }

        

    }
}