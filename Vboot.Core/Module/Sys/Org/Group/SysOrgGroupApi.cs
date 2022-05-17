using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys;

[ApiDescriptionSettings("Sys", Tag = "组织架构-群组")]
public class SysOrgGroupApi : IDynamicApiController
{
    private readonly SysOrgGroupService _groupService;

    public SysOrgGroupApi(
        SysOrgGroupService groupService)
    {
        _groupService = groupService;
    }

    [QueryParameters]
    public async Task<dynamic> Get()
    {
        var pp = XreqUtil.GetPp();
        var items = await _groupService.repo.Context.Queryable<SysOrgGroup>()
            .OrderBy(u => u.ornum)
            .Select((t) => new {t.id, t.name, t.notes, t.crtim, t.uptim})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }

    public async Task<SysOrgGroup> GetOne(string id)
    {
        var group = await _groupService.repo.Context.Queryable<SysOrgGroup>()
            .Mapper<SysOrgGroup, SysOrg, SysOrgGroupOrg>(it =>
                ManyToMany.Config(it.gid, it.oid))
            .Where(it => it.id == id).FirstAsync();
        return group;
    }

    public async Task Post(SysOrgGroup group)
    {
        group.id = YitIdHelper.NextId() + "";
        var gmmaps = new List<SysOrgGroupOrg>();
        foreach (var member in group.members)
        {
            gmmaps.Add(new SysOrgGroupOrg {gid = group.id, oid = member.id});
        }

        await _groupService.InsertAsync(group, gmmaps);
    }

    public async Task Put(SysOrgGroup group)
    {
        var gmmaps = new List<SysOrgGroupOrg>();
        foreach (var member in group.members)
        {
            gmmaps.Add(new SysOrgGroupOrg {gid = group.id, oid = member.id});
        }

        await _groupService.UpdateAsync(group, gmmaps);
    }

    public async Task Delete(string ids)
    {
        await _groupService.DeleteAsync(ids);
    }
}