using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common.Util;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys;

public class SysOrgReceService : ITransient
{
    public async Task update(List<SysOrgRece> reces)
    {
        string userid = XuserUtil.getUserId();

        List<string> orgidList = new List<string>();
        foreach (var rece in reces)
        {
            rece.orgid = rece.id;
            rece.id = YitIdHelper.NextId() + "";
            rece.userid = userid;
            rece.uptim = DateTime.Now;
            orgidList.Add(rece.orgid);
        }

        //数据库删除本次已传的记录
        await _repo.Context.Deleteable<SysOrgRece>().Where("userid=@userid",new { userid})
            .Where("orgid in (@orgid)", new {orgid = orgidList.ToArray()}).ExecuteCommandAsync();
        
        //删除当前数据库最近10次前的数据
        RefAsync<int> total = 0;
        var items = await _repo.Context.Queryable<SysOrgRece>()
            .Where(it=>it.userid==userid)
            .OrderBy(u => u.uptim, OrderByType.Desc)
            .Select((t) => t.id)
            .ToPageListAsync(2, 10, total);
        if (items.Count > 0)
        {
            await _repo.Context.Deleteable<SysOrgRece>().In(items.ToArray()).ExecuteCommandAsync();
        }

        //插入本次使用的记录
        await _repo.InsertAsync(reces);
    }


    public readonly ISqlSugarRepository<SysOrgRece> _repo;

    public SysOrgReceService(ISqlSugarRepository<SysOrgRece> repo)
    {
        _repo = repo;
    }
}