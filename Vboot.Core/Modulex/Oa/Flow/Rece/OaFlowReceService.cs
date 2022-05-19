using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common.Util;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys;

public class OaFlowReceService : ITransient
{
    public async Task update(List<OaFlowRece> reces)
    {
        string userid = XuserUtil.getUserId();
        List<string> flowidList = new List<string>();
        foreach (var rece in reces)
        {
            rece.flowid = rece.id;
            rece.id = YitIdHelper.NextId() + "";
            rece.userid = userid;
            rece.uptim = DateTime.Now;
            flowidList.Add(rece.flowid);
        }

        //数据库删除本次已传的记录
        await _repo.Context.Deleteable<OaFlowRece>().Where("userid=@userid",new { userid})
            .Where("flowid in (@flowid)", new {flowid = flowidList.ToArray()}).ExecuteCommandAsync();
        
        //删除当前数据库最近10次前的数据
        RefAsync<int> total = 0;
        var items = await _repo.Context.Queryable<OaFlowRece>()
            .Where(it=>it.userid==userid)
            .OrderBy(u => u.uptim, OrderByType.Desc)
            .Select((t) => t.id)
            .ToPageListAsync(2, 10, total);
        if (items.Count > 0)
        {
            await _repo.Context.Deleteable<OaFlowRece>().In(items.ToArray()).ExecuteCommandAsync();
        }

        //插入本次使用的记录
        await _repo.InsertAsync(reces);
        
    }


    public readonly ISqlSugarRepository<OaFlowRece> _repo;

    public OaFlowReceService(ISqlSugarRepository<OaFlowRece> repo)
    {
        _repo = repo;
    }
}