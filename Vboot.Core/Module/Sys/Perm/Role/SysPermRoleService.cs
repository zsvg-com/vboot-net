using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

public class SysPermRoleService : BaseMainService<SysPermRole>, ITransient
{
    public SysPermRoleService(ISqlSugarRepository<SysPermRole> repo)
    {
        base.repo = repo;
    }

    public async Task InsertAsync(SysPermRole role, List<SysPermRoleOrg> romaps, List<SysPermRoleMenu> rmmaps)
    {
        using var tran = repo.Context.UseTran();
        await base.InsertAsync(role);
        await repo.Context.Insertable(romaps).ExecuteCommandAsync();
        await repo.Context.Insertable(rmmaps).ExecuteCommandAsync();
        tran.CommitTran();
    }

    public async Task UpdateAsync(SysPermRole role, List<SysPermRoleOrg> romaps, List<SysPermRoleMenu> rmmaps)
    {
        using var tran = repo.Context.UseTran();
        await base.UpdateAsync(role);
        await repo.Context.Deleteable<SysPermRoleOrg>().Where(it => it.rid == role.id).ExecuteCommandAsync();
        await repo.Context.Insertable(romaps).ExecuteCommandAsync();
        await repo.Context.Deleteable<SysPermRoleMenu>().Where(it => it.rid == role.id).ExecuteCommandAsync();
        await repo.Context.Insertable(rmmaps).ExecuteCommandAsync();
        tran.CommitTran();
    }
}