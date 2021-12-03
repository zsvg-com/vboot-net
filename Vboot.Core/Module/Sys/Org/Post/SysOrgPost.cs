using System.Collections.Generic;
using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys
{
    [SugarTable("sys_org_post", TableDescription = "系统岗位表")]
    [Description("系统岗位表")]
    public class SysOrgPost : BaseMainEntity
    {
        [SugarColumn(ColumnDescription = "部门ID",IsNullable = true,Length = 36)]
        public string deptid { get; set; }
       
        [SugarColumn(IsIgnore = true)] 
        public SysOrgDept dept { get; set; }
        
        [SugarColumn(ColumnDescription = "层级",IsNullable = true,Length = 1000)]
        public string tier { get; set; }

        
        [SugarColumn(ColumnDescription = "备注",IsNullable = true,Length = 64)]
        public string notes { get; set; }
        
        [SugarColumn(ColumnDescription = "排序号",IsNullable = true)]
        public int ornum { get; set; }
        
           
        [SugarColumn(ColumnDescription = "ldap层级名称",IsNullable = true,Length = 1000)]
        public string ldnam{ get; set; }
        
        [SugarColumn(IsIgnore =true)]
        public List<SysOrg> users { get; set; }

    }
}