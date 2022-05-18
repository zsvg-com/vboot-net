using System;
using System.Threading.Tasks;
using Furion;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Sys;
using Yitter.IdGenerator;

namespace Vboot.Core.Modulex.Oa;

[ApiDescriptionSettings("Ext", Tag = "流程管理-流程实例")]
public class OaFlowMainApi : IDynamicApiController
{
    private readonly OaFlowMainService _service;

    public OaFlowMainApi(OaFlowMainService service)
    {
        _service = service;
    }

    [QueryParameters]
    public async Task<dynamic> Get()
    {
        var pp = XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<OaFlowMain, OaFlowTemp,SysOrg>((t, c,o)
                => new JoinQueryInfos(JoinType.Left, c.id == t.temid,JoinType.Left, o.id == t.crmid))
            .Select((t,c,o) => 
                new {t.id, t.name, t.notes,temna=c.name,crman=o.name,t.crtim})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }

    public async Task<OaFlowMain> GetOne(string id)
    {
        var main = await _service.repo.Context.Queryable<OaFlowMain>()
            .Where(it => it.id == id).FirstAsync();
        return main;
    }

    public async Task Post(OaFlowMain main)
    {
        main.id = YitIdHelper.NextId() + "";
        await _service.InsertAsync(main);
    }

    public async Task Put(OaFlowMain cate)
    {
        await _service.UpdateAsync(cate);
    }
    
    public async Task Delete(string ids)
    {
        await _service.DeleteAsync(ids);
    }

}