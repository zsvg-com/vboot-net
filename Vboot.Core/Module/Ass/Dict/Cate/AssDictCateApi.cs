using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;

namespace Vboot.Core.Module.Ass;

[ApiDescriptionSettings("Ass", Tag = "字典分类")]
public class AssDictCateApi : IDynamicApiController
{
    private readonly AssDictCateService _service;

    public AssDictCateApi(AssDictCateService service)
    {
        _service = service;
    }

    [QueryParameters]
    public async Task<dynamic> Get()
    {
        var pp = XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<AssDictCate>()
            .OrderBy(u => u.ornum)
            .Select((t) => new {t.id, t.name, t.notes})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }

    public async Task<AssDictCate> GetOne(string id)
    {
        var cate = await _service.repo.Context.Queryable<AssDictCate>()
            .Where(it => it.id == id).FirstAsync();
        return cate;
    }

    public async Task Post(AssDictCate cate)
    {
        await _service.InsertAsync(cate);
    }

    public async Task Put(AssDictCate cate)
    {
        await _service.UpdateAsync(cate);
    }

    public async Task Delete(string ids)
    {
        var idArr = ids.Split(",");
        await _service.DeleteAsync(idArr);
    }
}