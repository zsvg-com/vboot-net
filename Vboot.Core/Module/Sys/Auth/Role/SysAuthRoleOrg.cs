using SqlSugar;

namespace Vboot.Core.Module.Sys
{
    
    [SugarTable("sys_auth_role_org", TableDescription = "角色组织对应表")]
    public class SysAuthRoleOrg
    {
        [SugarColumn(ColumnDescription = "角色ID",IsNullable = true)]
        public string rid{ get; set; }
        
        [SugarColumn(ColumnDescription = "组织架构ID",IsNullable = true)]
        public string oid{ get; set; }
    }
}