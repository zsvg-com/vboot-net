using SqlSugar;

namespace Vboot.Core.Common;

public abstract class BaseEntity
{
    [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, Length = 36)]
    public virtual string id { get; set; }

    [SugarColumn(ColumnDescription = "名称", IsNullable = true, Length = 255)]
    public virtual string name { get; set; }
}