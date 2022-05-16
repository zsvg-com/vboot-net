using System.ComponentModel;
using SqlSugar;

namespace Vboot.Core.Module.Sys
{
    [SugarTable("sys_auth_perm", TableDescription = "权限标识")]
    [Description("权限标识")]
    public class SysAuthPerm
    {
        [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, Length = 64)]
        public string id { get; set; }

        [SugarColumn(ColumnDescription = "上级权限", IsNullable = true, Length = 64)]
        public string pid { get; set; }

        [SugarColumn(ColumnDescription = "权限url", IsNullable = true, Length = 128)]
        public string url { get; set; }

        [SugarColumn(ColumnDescription = "权限代码", IsNullable = true, IsOnlyIgnoreUpdate = true)]
        public long code { get; set; }

        [SugarColumn(ColumnDescription = "权限位", IsNullable = true, IsOnlyIgnoreUpdate = true)]
        public int pos { get; set; }

        [SugarColumn(ColumnDescription = "权限类型", IsNullable = true)]
        public string type { get; set; }
        
        [SugarColumn(ColumnDescription = "是否可用", IsNullable = true)]
        public bool avtag { get; set; } = true;
    }
}