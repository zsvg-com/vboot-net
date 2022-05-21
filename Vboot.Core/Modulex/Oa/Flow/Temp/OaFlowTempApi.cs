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

[ApiDescriptionSettings("Ext", Tag = "流程管理-流程模板")]
public class OaFlowTempApi : IDynamicApiController
{
    private readonly OaFlowTempService _service;

    public OaFlowTempApi(OaFlowTempService service)
    {
        _service = service;
    }

    [QueryParameters]
    public async Task<dynamic> Get(string catid, string name)
    {
        var pp = XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<OaFlowTemp, OaFlowCate,SysOrg,SysOrg>((t, c,o,o2)
                => new JoinQueryInfos(JoinType.Left, c.id == t.catid,
                    JoinType.Left, o.id == t.crmid,
                    JoinType.Left, o2.id == t.upmid))
            .OrderBy(t => t.ornum)
            .WhereIF(!string.IsNullOrWhiteSpace(name), t => t.name.Contains(name.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(catid), t => t.catid == catid)
            .Select((t, c,o,o2) => 
                new {t.crtim,t.uptim,t.id, t.name, t.notes, catna = c.name,crman=o.name,upman=o2.name})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        
        
        return RestPageResult.Build(pp.total.Value, items);
    }
    
    
    [QueryParameters]
    public async Task<dynamic> GetList(string catid, string name)
    {
        var list = await _service.repo.Context.Queryable<OaFlowTemp, OaFlowCate>((t, c)
                => new JoinQueryInfos(JoinType.Left, c.id == t.catid))
            .OrderBy(t => t.ornum)
            .WhereIF(!string.IsNullOrWhiteSpace(name), t => t.name.Contains(name.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(catid), t => t.catid == catid)
            .Select((t, c) => 
                new {t.id, t.name, t.notes, catna = c.name})
            .ToListAsync();
        return list;
    }

    public async Task<OaFlowTemp> GetOne(string id)
    {
        var main = await _service.repo.Context.Queryable<OaFlowTemp>()
            .Where(it => it.id == id).FirstAsync();

        var catna = await _service.repo.Context.Queryable<OaFlowCate>()
            .Where(it => it.id == main.catid).Select(it => it.name).SingleAsync();

        main.cate = new ZidName()
        {
            id = main.catid,
            name = catna
        };

        main.prxml = await _service.repo.Context.Queryable<BpmProcTemp>()
            .Where(it => it.id == main.protd).Select(it => it.orxml).SingleAsync();

        return main;
    }
    
    [QueryParameters]
    public async Task<dynamic> GetTree()
    {
        string sql = @"select id,pid,name,'cate' type from oa_flow_cate 
            union all select id,catid as pid,name,'temp' type from oa_flow_temp"; 
        var treeList = await _service.repo.Context.SqlQueryable<Ztree>(sql).
            ToTreeAsync(it=>it.children,it=>it.pid, null);
        return treeList;
    }

    public async Task Post(OaFlowTemp temp)
    {
        temp.id = YitIdHelper.NextId() + "";
        if (temp.cate != null && temp.cate.id != "")
        {
            temp.catid = temp.cate.id;
        }

        await _service.Insertx(temp);
    }

    public async Task Put(OaFlowTemp temp)
    {
        if (temp.cate != null && temp.cate.id != "")
        {
            temp.catid = temp.cate.id;
        }

        await _service.Updatex(temp);
    }

    public async Task Delete(string ids)
    {
        await _service.DeleteAsync(ids);
    }
}