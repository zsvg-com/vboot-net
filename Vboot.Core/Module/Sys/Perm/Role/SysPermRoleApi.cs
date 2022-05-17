using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys;

[ApiDescriptionSettings("Sys", Tag = "权限管理-角色")]
public class SysPermRoleApi : IDynamicApiController
{
    private readonly SysPermRoleService _service;

    public SysPermRoleApi(
        SysPermRoleService service)
    {
        _service = service;
    }

    [QueryParameters]
    public async Task<dynamic> Get()
    {
        var pp = XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<SysPermRole>()
            .OrderBy(u => u.ornum)
            .Select((t) => new {t.id, t.name, t.notes, t.crtim, t.uptim})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }

    public async Task<SysPermRole> GetOne(string id)
    {
        var role = await _service.repo.Context.Queryable<SysPermRole>()
            .Mapper<SysPermRole, SysOrg, SysPermRoleOrg>(it =>
                ManyToMany.Config(it.rid, it.oid))
            .Mapper<SysPermRole, SysPermMenu, SysPermRoleMenu>(it =>
                ManyToMany.Config(it.rid, it.mid))
            .Where(it => it.id == id).FirstAsync();
        return role;
    }

    public async Task Post(SysPermRole role)
    {
        role.id = YitIdHelper.NextId() + "";
        var romaps = new List<SysPermRoleOrg>();
        foreach (var org in role.orgs)
        {
            romaps.Add(new SysPermRoleOrg {rid = role.id, oid = org.id});
        }

        var rmmaps = new List<SysPermRoleMenu>();
        foreach (var menu in role.menus)
        {
            rmmaps.Add(new SysPermRoleMenu {rid = role.id, mid = menu.id});
        }

        await _service.InsertAsync(role, romaps, rmmaps);
    }

    public async Task PostReperm()
    {
        await _service.repo.Context.Updateable<SysOrgUser>().SetColumns(it => it.retag == false)
            .Where(it => it.retag == true)
            .ExecuteCommandAsync();
    }

    public async Task Put(SysPermRole role)
    {
        var romaps = new List<SysPermRoleOrg>();
        foreach (var org in role.orgs)
        {
            romaps.Add(new SysPermRoleOrg {rid = role.id, oid = org.id});
        }

        var rmmaps = new List<SysPermRoleMenu>();
        foreach (var menu in role.menus)
        {
            rmmaps.Add(new SysPermRoleMenu {rid = role.id, mid = menu.id});
        }

        await _service.UpdateAsync(role, romaps, rmmaps);
    }

    public async Task Delete(string ids)
    {
        var idArr = ids.Split(",");
        await _service.DeleteAsync(ids);
    }
}