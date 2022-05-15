using System.ComponentModel;
using SqlSugar;

namespace Vboot.Core.Module.Sys
{
    [SugarTable("sys_auth_perm", TableDescription = "权限标识")]
    [Description("权限标识")]
    public class SysAuthPerm
    {
        [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true,Length = 64)]
        public string id { get; set; }
        
        [SugarColumn(ColumnDescription = "权限代码", IsNullable = true)]
        public long code  { get; set; }

        [SugarColumn(ColumnDescription = "权限位", IsNullable = true)]
        public int pos { get; set; }
        
        [SugarColumn(ColumnDescription = "权限类型", IsNullable = true)]
        public string type { get; set; }

    }
}