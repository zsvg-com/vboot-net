using System.Threading.Tasks;
using Furion.DynamicApiController;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Sys;

namespace Vboot.Application.Sa;

/// <summary>
/// 代理商主信息接口
/// </summary>
public class SaAgentMainApi : IDynamicApiController
{
    private readonly SaAgentMainService _service;

    public SaAgentMainApi(SaAgentMainService service)
    {
        _service = service;
    }

    /// <summary>
    /// 获取代理商主信息的分页数据
    /// </summary>
    /// <returns></returns>
    [QueryParameters]
    public async Task<dynamic> Get()
    {
        _service.JsTest();
        var pp = XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<SaAgentMain>()
            .Select((t) => new {t.id, t.name, t.addre, t.crtim, t.uptim})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }
    
    /// <summary>
    /// 获取单个代理商的详细信息
    /// </summary>
    /// <param name="id">代理商ID</param>
    /// <returns></returns>
    public async Task<SaAgentMain> GetOne(string id)
    {
        var main = await _service.repo.Context.Queryable<SaAgentMain>()
            .Mapper<SaAgentMain, SysOrg, SaAgentMainViewer>(it =>
                ManyToMany.Config(it.mid, it.oid))
            .Where(it => it.id == id).FirstAsync();
        if (main.opmid != null)
        {
            main.opman = await _service.repo.Context.Queryable<SysOrg>()
                .Where(it => it.id == main.opmid).SingleAsync();
        }
        return main;
    }

    /// <summary>
    /// 新增代理商
    /// </summary>
    public async Task<string> Post(SaAgentMain main)
    {
        return await _service.Insertx(main);
    }

    /// <summary>
    /// 修改代理商
    /// </summary>
    public async Task<string> Put(SaAgentMain main)
    {
        return await _service.Updatex(main);
    }

    /// <summary>
    /// 删除代理商
    /// </summary>
    public async Task Delete(string ids)
    {
        await _service.Deletex(ids);
    }
}