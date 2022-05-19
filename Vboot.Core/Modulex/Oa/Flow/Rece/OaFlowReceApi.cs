using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Sys;
using Vboot.Core.Modulex.Oa;

namespace Vboot.Core.Module.Oa;

[ApiDescriptionSettings("Ext", Tag = "流程管理-使用记录")]
public class OaFlowReceApi : IDynamicApiController
{
    private readonly OaFlowReceService _service;

    public OaFlowReceApi(OaFlowReceService service)
    {
        _service = service;
    }

    public async Task Post(List<OaFlowRece> reces)
    {
        if (reces != null && reces.Count > 0)
        {
          await  _service.update(reces);
        }
    }

    public async Task<dynamic> GetList()
    {
        string userId = XuserUtil.getUserId();
        var list = await _service._repo.Context.Queryable<OaFlowRece, OaFlowTemp,OaFlowCate>((t, f,c)
                => new JoinQueryInfos(JoinType.Inner, f.id == t.flowid,JoinType.Inner, c.id == f.catid))
            .OrderBy(t => t.uptim, OrderByType.Desc)
            .Where(t => t.userid == userId)
            .Select((t, f,c) => new {id = t.flowid, f.name,catna=c.name})
            .ToListAsync();
        return list;
    }
}