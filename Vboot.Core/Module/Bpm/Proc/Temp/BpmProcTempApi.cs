using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;

namespace Vboot.Core.Module.Bpm;

[ApiDescriptionSettings("Bpm", Tag = "流程引擎-模板")]
public class BpmProcTempApi : IDynamicApiController
{
    private readonly BpmProcTempService _service;

    public BpmProcTempApi(BpmProcTempService service)
    {
        _service = service;
    }

    [QueryParameters]
    public async Task<dynamic> Get()
    {
        var pp=XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<BpmProcTemp>()
            .OrderBy(t => t.crtim, OrderByType.Desc)
            .Select((t) => new {t.id, t.name, t.crtim})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }

    public async Task<BpmProcTemp> GetOne(string id)
    {
        var main = await _service.SingleAsync(id);
        return main;
    }

    public async Task<string> Post(BpmProcTemp main)
    {
        return await _service.InsertAsync(main);
    }

    public async Task<string> Put(BpmProcTemp main)
    {
        return await _service.UpdateAsync(main);
    }

    public async Task Delete(string ids)
    {
        await _service.DeleteAsync(ids);
    }
}