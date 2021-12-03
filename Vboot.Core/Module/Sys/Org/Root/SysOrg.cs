using System.ComponentModel;
using SqlSugar;

namespace Vboot.Core.Module.Sys
{
    [SugarTable("sys_org", TableDescription = "组织架构表")]
    [Description("组织架构表")]
    public class SysOrg
    {
        [SugarColumn(ColumnDescription = "ID", IsPrimaryKey = true, Length = 36)]
        public virtual string id { get; set; }

        [SugarColumn(ColumnDescription = "名称", IsNullable = true, Length = 128)]
        public virtual string name { get; set; }

        public SysOrg()
        {
        }

        public SysOrg(string id)
        {
            this.id = id;
        }

        public SysOrg(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}