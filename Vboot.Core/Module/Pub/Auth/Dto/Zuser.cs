using System.Collections.Generic;

namespace Vboot.Core.Module.Pub
{
    public class Zuser
    {
        public string id { get; set; }

        public string name { get; set; }

        public string usnam { get; set; }

        public string perms { get; set; } //权限集,用于验证URL权限 比较下哪个方式快0
        
        // public long[] permArr{ get; set; }//权限集,用于验证URL权限 比较下哪个方式快1

        // public List<string> permList{ get; set; }//权限集,用于验证URL权限 比较下哪个方式快2

        public string conds { get; set; } //组织架构集，用户ID，所有上级部门ID,岗位ID,群组ID

        public Zuser()
        {
        }

        public Zuser(string id, string name, string usnam)
        {
            this.id = id;
            this.name = name;
            this.usnam = usnam;
        }
    }
}