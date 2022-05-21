using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Bpm;
using Vboot.Core.Module.Sys;
using Yitter.IdGenerator;

namespace Vboot.Core.Modulex.Oa;

[ApiDescriptionSettings("Ext", Tag = "流程管理-流程实例")]
public class OaFlowMainApi : IDynamicApiController
{
    private readonly OaFlowMainService _service;

    private readonly BpmTaskMainService _taskMainService;

    public OaFlowMainApi(OaFlowMainService service,
        BpmTaskMainService taskMainService)
    {
        _service = service;
        _taskMainService = taskMainService;
    }

    [QueryParameters]
    public async Task<dynamic> Get(string name)
    {
        var pp = XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<OaFlowMain, OaFlowTemp, SysOrg>((t, c, o)
                => new JoinQueryInfos(JoinType.Left, c.id == t.temid, JoinType.Left, o.id == t.crmid))
            .WhereIF(!string.IsNullOrWhiteSpace(name), t => t.name.Contains(name.Trim()))
            .OrderBy(t => t.crtim, OrderByType.Desc)
            .Select((t, c, o) =>
                (dynamic) new {t.id, t.name, t.notes, temna = c.name, crman = o.name, t.crtim, t.state})
            // .OrderBy(t => t.crtim, OrderByType.Desc)
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        
        _taskMainService.FindCurrentExmen(items);
        return RestPageResult.Build(pp.total.Value, items);
    }

    public async Task<OaFlowMain> GetOne(string id)
    {
        var main = await _service.repo.Context.Queryable<OaFlowMain>()
            .Where(it => it.id == id).FirstAsync();
        return main;
    }

    [MyUnitOfWork]
    public async Task Post(OaFlowMain main)
    {
        await _service.Insertx(main);
    }

    [MyUnitOfWork]
    public async Task Put(OaFlowMain cate)
    {
        await _service.Updatex(cate);
    }

    public async Task Delete(string ids)
    {
        await _service.DeleteAsync(ids);
    }
}