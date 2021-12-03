using SqlSugar;

namespace Vboot.Core.Module.Sys
{
    [SugarTable("sys_auth_role_menu", TableDescription = "角色菜单对应表")]
    public class SysAuthRoleMenu
    {
        [SugarColumn(ColumnDescription = "角色ID",IsNullable = true)]
        public string rid{ get; set; }
        
        [SugarColumn(ColumnDescription = "菜单ID",IsNullable = true)]
        public string mid{ get; set; }
    }
}