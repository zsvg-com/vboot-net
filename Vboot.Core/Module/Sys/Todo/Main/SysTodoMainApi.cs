using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Sys;

namespace Vboot.Core.Module.Sys;

[ApiDescriptionSettings("Sys", Tag = "消息管理-待办")]
public class SysTodoMainApi : IDynamicApiController
{
    private readonly SysTodoMainService _service;

    public SysTodoMainApi(SysTodoMainService service)
    {
        _service = service;
    }

    [QueryParameters]
    public async Task<dynamic> Get(string name)
    {
        var pp = XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<SysTodoMain, SysTodoUser,SysOrg>((t, u,o)
                => new JoinQueryInfos(JoinType.Inner, u.tid == t.id,JoinType.Inner, o.id == u.uid))
            .WhereIF(!string.IsNullOrWhiteSpace(name), t => t.name.Contains(name.Trim()))
            .Select((t,u,o) => 
                new {t.id, t.name,exman=o.name,t.crtim,t.link})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }

    public async Task<SysTodoMain> GetOne(string id)
    {
        var cate = await _service.repo.Context.Queryable<SysTodoMain>()
            .Where(it => it.id == id).FirstAsync();
        return cate;
    }

    public async Task Post(SysTodoMain main)
    {
        await _service.InsertAsync(main);
    }

    public async Task Put(SysTodoMain cate)
    {
        await _service.UpdateAsync(cate);
    }

    public async Task Delete(string ids)
    {
        await _service.DeleteAsync(ids);
    }
}