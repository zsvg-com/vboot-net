using SqlSugar;

namespace Vboot.Core.Module.Sys
{
    [SugarTable("sys_org_post_user", TableDescription = "岗位员工关系表")]
    public class SysOrgPostUser
    {
        [SugarColumn(ColumnDescription = "岗位ID",IsNullable = true)]
        public string pid{ get; set; }
        
        [SugarColumn(ColumnDescription = "用户ID",IsNullable = true)]
        public string uid{ get; set; }
    }
}