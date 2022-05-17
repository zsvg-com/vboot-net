using System;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Yitter.IdGenerator;

namespace Vboot.Core.Modulex.Oa;

[ApiDescriptionSettings("Ext", Tag = "流程管理-流程分类")]
public class OaFlowCateApi : IDynamicApiController
{
    private readonly OaFlowCateService _service;

    // private readonly HttpContext _httpContext;
    //
    // public OaFlowCateApi(IHttpContextAccessor httpContextAccessor,OaFlowCateService service)
    // {
    //     _service = service;
    //     _httpContext = httpContextAccessor.HttpContext;
    // }
    //
    public OaFlowCateApi(OaFlowCateService service)
    {
        _service = service;
    }

    [QueryParameters]
    public async Task<dynamic> GetTree()
    {
        var treeList = await _service.repo.Context
            .SqlQueryable<OaFlowCate>(
                "select id,pid,name,crtim,uptim,notes from oa_flow_cate order by ornum")
            .ToTreeAsync(it => it.children, it => it.pid, null);
        return treeList;
    }

    [QueryParameters]
    public async Task<dynamic> Get()
    {
        var pp = XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<OaFlowCate>()
            .OrderBy(u => u.ornum)
            .Select((t) => new {t.id, t.name, t.notes})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }

    public async Task<OaFlowCate> GetOne(string id)
    {
        var main = await _service.repo.Context.Queryable<OaFlowCate>()
            .Where(it => it.id == id).FirstAsync();
        if (main.pid != null)
        {
            main.panam = await _service.repo.Context.Queryable<OaFlowCate>()
                .Where(it => it.id == main.pid).Select(it => it.name).SingleAsync();
        }

        return main;
    }

    public async Task Post(OaFlowCate cate)
    {
        cate.id = YitIdHelper.NextId() + "";
        await _service.InsertAsync(cate);
    }

    public async Task Put(OaFlowCate cate)
    {
        await _service.UpdateAsync(cate);
    }

    public async Task Delete(string ids)
    {
        var idArr = ids.Split(",");
        foreach (var id in idArr)
        {
            var count = await
                _service.repo.Context.Queryable<OaFlowCate>().Where(it => it.pid == id).CountAsync();
            if (count > 0)
            {
                throw new Exception("有子分类无法删除");
            }
        }

        await _service.DeleteAsync(ids);
    }
}