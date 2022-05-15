using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Bpm;
using Yitter.IdGenerator;

namespace Vboot.Core.Modulex.Oa;

[ApiDescriptionSettings("Ext", Tag = "流程管理-流程分类")]
public class OaFlowTempApi : IDynamicApiController
{
    private readonly OaFlowTempService _service;

    public OaFlowTempApi(OaFlowTempService service)
    {
        _service = service;
    }

    [QueryParameters]
    public async Task<dynamic> Get()
    {
        var pp = XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<OaFlowTemp>()
            .OrderBy(u => u.ornum)
            .Select((t) => new {t.id, t.name, t.notes})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }

    public async Task<OaFlowTemp> GetOne(string id)
    {
        var main = await _service.repo.Context.Queryable<OaFlowTemp>()
            .Where(it => it.id == id).FirstAsync();

        main.xml = await _service.repo.Context.Queryable<BpmProcTemp>()
            .Where(it => it.id == main.protd).Select(it => it.xml).SingleAsync();

        return main;
    }

    public async Task Post(OaFlowTemp temp)
    {
        temp.id = YitIdHelper.NextId() + "";
        await _service.Insertx(temp);
    }

    public async Task Put(OaFlowTemp temp)
    {
        await _service.Updatex(temp);
    }

    public async Task Delete(string ids)
    {
        await _service.DeleteAsync(ids);
    }
}