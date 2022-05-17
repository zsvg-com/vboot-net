using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

public class SysOrgGroupService : BaseMainService<SysOrgGroup>, ITransient
{
    public SysOrgGroupService(ISqlSugarRepository<SysOrgGroup> repo)
    {
        base.repo = repo;
    }

    public async Task InsertAsync(SysOrgGroup group, List<SysOrgGroupOrg> gmmaps)
    {
        using var tran = repo.Context.UseTran();
        await base.InsertAsync(@group);
        await repo.Context.Insertable(gmmaps).ExecuteCommandAsync();
        await repo.Context.Insertable(new SysOrg {id = group.id, name = group.name}).ExecuteCommandAsync();
        tran.CommitTran();
    }


    public async Task UpdateAsync(SysOrgGroup group, List<SysOrgGroupOrg> gmmaps)
    {
        using var tran = repo.Context.UseTran();
        await base.UpdateAsync(@group);
        await repo.Context.Deleteable<SysOrgGroupOrg>().Where(it => it.gid == @group.id).ExecuteCommandAsync();
        await repo.Context.Insertable(gmmaps).ExecuteCommandAsync();
        await repo.Context.Updateable(new SysOrg {id = group.id, name = group.name})
            .UpdateColumns(it => new {it.name}).ExecuteCommandAsync();
        tran.CommitTran();
    }
}