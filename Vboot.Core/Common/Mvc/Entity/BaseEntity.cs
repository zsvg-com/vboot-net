using SqlSugar;

namespace Vboot.Core.Common
{
    public abstract class BaseEntity
    {
        [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, Length = 36)]
        public virtual string id { get; set; }

        [SugarColumn(ColumnDescription = "名称", IsNullable = true, Length = 255)]
        public virtual string name { get; set; }

        [SugarColumn(ColumnDescription = "可用标记：1可用，0禁用", IsNullable = true)]
        public virtual bool? avtag { get; set; }
        
        [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
        public int ornum { get; set; }
    }
}