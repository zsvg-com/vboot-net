using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Module.Sys;

namespace Vboot.Core.Module.Pub;

public class PubAuthInitService : ITransient
{
    private readonly ISqlSugarRepository<SysPermMenu> _menuRepo;

    public PubAuthInitService(ISqlSugarRepository<SysPermMenu> menuRepo)
    {
        _menuRepo = menuRepo;
    }

    public async Task InitAllMenu()
    {
        List<SysPermMenu> list = new List<SysPermMenu>();
        SysPermMenu menu1 = new SysPermMenu();
        menu1.id = "Sys";
        menu1.name = "系统管理";
        menu1.code = "Sys";
        menu1.comp = "LAYOUT";
        menu1.path = "/sys";
        menu1.redirect = "/sys/org/user";
        menu1.ornum = 1;
        menu1.icon = "ant-design:setting-outlined";
        menu1.avtag = true;
        menu1.shtag = true;
        menu1.type = "D";
        list.Add(menu1);

        SysPermMenu menu11 = new SysPermMenu();
        menu11.id = "SysOrg";
        menu11.name = "组织架构";
        menu11.code = "SysOrg";
        menu11.comp = "LAYOUT";
        menu11.path = "/sys/org";
        menu11.redirect = "/sys/org/user";
        menu11.ornum = 1;
        menu11.icon = "ant-design:partition-outlined";
        menu11.pid = "Sys";
        menu11.avtag = true;
        menu11.shtag = true;
        menu11.type = "D";
        list.Add(menu11);

        SysPermMenu menu111 = new SysPermMenu();
        menu111.id = "SysOrgDept";
        menu111.name = "部门管理";
        menu111.code = "SysOrgDept";
        menu111.path = "/sys/org/dept";
        menu111.comp = "/sys/org/dept/index.vue";
        menu111.ornum = 1;
        menu111.pid = "SysOrg";
        menu111.catag = false;
        menu111.avtag = true;
        menu111.shtag = true;
        menu111.type = "M";
        list.Add(menu111);

        SysPermMenu menu112 = new SysPermMenu();
        menu112.id = "SysOrgUser";
        menu112.name = "用户管理";
        menu112.code = "SysOrgUser";
        menu112.path = "/sys/org/user";
        menu112.comp = "/sys/org/user/index.vue";
        menu112.ornum = 2;
        menu112.pid = "SysOrg";
        menu112.catag = false;
        menu112.avtag = true;
        menu112.shtag = true;
        menu112.type = "M";
        list.Add(menu112);

        SysPermMenu menu113 = new SysPermMenu();
        menu113.id = "SysOrgPost";
        menu113.name = "岗位管理";
        menu113.code = "SysOrgPost";
        menu113.path = "/sys/org/post";
        menu113.comp = "/sys/org/post/index.vue";
        menu113.ornum = 3;
        menu113.pid = "SysOrg";
        menu113.catag = false;
        menu113.avtag = true;
        menu113.shtag = true;
        menu113.type = "M";
        list.Add(menu113);

        SysPermMenu menu114 = new SysPermMenu();
        menu114.id = "SysOrgGroup";
        menu114.name = "群组管理";
        menu114.code = "SysOrgGroup";
        menu114.path = "/sys/org/group";
        menu114.comp = "/sys/org/group/index.vue";
        menu114.ornum = 4;
        menu114.pid = "SysOrg";
        menu114.catag = false;
        menu114.avtag = true;
        menu114.shtag = true;
        menu114.type = "M";
        list.Add(menu114);

        SysPermMenu menu12 = new SysPermMenu();
        menu12.id = "SysPerm";
        menu12.name = "权限管理";
        menu12.code = "SysPerm";
        menu12.comp = "LAYOUT";
        menu12.path = "/sys/perm";
        menu12.redirect = "/sys/perm/menu";
        menu12.ornum = 2;
        menu12.icon = "ant-design:safety-certificate-outlined";
        menu12.pid = "Sys";
        menu12.avtag = true;
        menu12.shtag = true;
        menu12.type = "D";
        list.Add(menu12);

        SysPermMenu menu121 = new SysPermMenu();
        menu121.id = "SysPermMenu";
        menu121.name = "菜单管理";
        menu121.code = "SysPermMenu";
        menu121.path = "/sys/perm/menu";
        menu121.comp = "/sys/perm/menu/index.vue";
        menu121.ornum = 1;
        menu121.pid = "SysPerm";
        menu121.catag = false;
        menu121.avtag = true;
        menu121.shtag = true;
        menu121.type = "M";
        list.Add(menu121);

        SysPermMenu menu122 = new SysPermMenu();
        menu122.id = "SysPermRole";
        menu122.name = "角色管理";
        menu122.code = "SysPermRole";
        menu122.path = "/sys/perm/role";
        menu122.comp = "/sys/perm/role/index.vue";
        menu122.ornum = 1;
        menu122.pid = "SysPerm";
        menu122.catag = false;
        menu122.avtag = true;
        menu122.shtag = true;
        menu122.type = "M";
        list.Add(menu122);
        await _menuRepo.InsertAsync(list);
    }
}