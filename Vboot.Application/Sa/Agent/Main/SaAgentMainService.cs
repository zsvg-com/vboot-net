﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;
using Yitter.IdGenerator;

namespace Vboot.Application.Sa;

public class SaAgentMainService : BaseMainService<SaAgentMain>, ITransient
{
    //新增
    public async Task<string> Insertx(SaAgentMain main)
    {
        main.id = YitIdHelper.NextId() + "";
        var mappings = new List<SaAgentMainViewer>();
        foreach (var viewer in main.viewers)
        {
            mappings.Add(new SaAgentMainViewer {mid = main.id, oid = viewer.id});
        }

        using var tran = repo.Context.UseTran();
        await base.InsertAsync(main);
        await repo.Context.Insertable(mappings).ExecuteCommandAsync();
        tran.CommitTran();
        return main.id;
    }

    //修改
    public async Task<string> Updatex(SaAgentMain main)
    {
        var mappings = new List<SaAgentMainViewer>();
        foreach (var viewer in main.viewers)
        {
            mappings.Add(new SaAgentMainViewer {mid = main.id, oid = viewer.id});
        }

        using var tran = repo.Context.UseTran();
        await base.UpdateAsync(main);
        await repo.Context.Deleteable<SaAgentMainViewer>().Where(it => it.mid == main.id).ExecuteCommandAsync();
        await repo.Context.Insertable(mappings).ExecuteCommandAsync();
        tran.CommitTran();
        return main.id;
    }

    //删除
    public async Task Deletex(string ids)
    {
        using var tran = repo.Context.UseTran();
        var idArr = ids.Split(",");
        await repo.Context.Deleteable<SaAgentMain>().In(idArr).ExecuteCommandAsync();
        for (int i = 0; i < idArr.Length; i++)
        {
            var mid = idArr[i];
            await repo.Context.Deleteable<SaAgentMainViewer>().Where(it => it.mid == mid).ExecuteCommandAsync();
        }

        tran.CommitTran();
    }

    public SaAgentMainService(ISqlSugarRepository<SaAgentMain> repo)
    {
        this.repo = repo;
    }
}