using System.Threading.Tasks;
using Furion.DynamicApiController;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;

namespace Vboot.Application.Sa;

/// <summary>
/// 客户主信息接口
/// </summary>
public class SaCustMainApi : IDynamicApiController
{
    
    [QueryParameters]
    public async Task<dynamic> Get()
    {
        var pp = XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<SaCustMain>()
            .Select((t) => new {t.id, t.name, t.addre, t.crtim, t.uptim})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }
    
    public async Task<SaAgentMain> GetOne(string id)
    {
        var data = await _service.repo.Context.Queryable<SaAgentMain>()
            .Where(it => it.id == id).FirstAsync();
        return data;
    }
    
    
    /// <summary>
    /// 新增客户
    /// </summary>
    public async Task<string> Post(SaCustMain main)
    {
        return await _service.InsertAsync(main);
    }
    
    /// <summary>
    /// 修改客户
    /// </summary>
    public async Task<string> Put(SaCustMain main)
    {
        return await _service.UpdateAsync(main);
    }

    /// <summary>
    /// 删除客户
    /// </summary>
    public async Task Delete(string ids)
    {
        await _service.DeleteAsync(ids);
    }


    private readonly SaCustMainService _service;

    public SaCustMainApi(SaCustMainService service)
    {
        _service = service;
    }
}