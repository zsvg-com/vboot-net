using System.Threading.Tasks;
using Furion.DynamicApiController;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Sys;

namespace Vboot.Application.Sa;

/// <summary>
/// 客户主信息接口
/// </summary>
public class SaCustMainApi : IDynamicApiController
{
    private readonly SaCustMainService _service;

    public SaCustMainApi(SaCustMainService service)
    {
        _service = service;
    }

    /// <summary>
    /// 获取客户主信息的分页数据
    /// </summary>
    /// <returns></returns>
    [QueryParameters]
    public async Task<dynamic> Get()
    {
        
        var pp = XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<SaCustMain>()
            .Select((t) => new {t.id, t.name, t.addre, t.crtim, t.uptim})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }

    /// <summary>
    /// 获取单个客户的详细信息
    /// </summary>
    /// <param name="id">客户ID</param>
    /// <returns></returns>
    public async Task<SaCustMain> GetOne(string id)
    {
        var main = await _service.repo.Context.Queryable<SaCustMain>()
            .Mapper<SaCustMain, SysOrg, SaCustMainViewer>(it =>
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
    /// 新增客户
    /// </summary>
    public async Task<string> Post(SaCustMain main)
    {
        return await _service.Insertx(main);
    }

    /// <summary>
    /// 修改客户
    /// </summary>
    public async Task<string> Put(SaCustMain main)
    {
        return await _service.Updatex(main);
    }

    /// <summary>
    /// 删除客户
    /// </summary>
    public async Task Delete(string ids)
    {
        await _service.Deletex(ids);
    }
}