using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

public class SysAuthRoleService : BaseMainService<SysAuthRole>, ITransient
{
    public SysAuthRoleService(ISqlSugarRepository<SysAuthRole> repo)
    {
        base.repo = repo;
    }

    public async Task InsertAsync(SysAuthRole role, List<SysAuthRoleOrg> romaps, List<SysAuthRoleMenu> rmmaps)
    {
        using var tran = repo.Context.UseTran();
        await base.InsertAsync(role);
        await repo.Context.Insertable(romaps).ExecuteCommandAsync();
        await repo.Context.Insertable(rmmaps).ExecuteCommandAsync();
        tran.CommitTran();
    }

    public async Task UpdateAsync(SysAuthRole role, List<SysAuthRoleOrg> romaps, List<SysAuthRoleMenu> rmmaps)
    {
        using var tran = repo.Context.UseTran();
        await base.UpdateAsync(role);
        await repo.Context.Deleteable<SysAuthRoleOrg>().Where(it => it.rid == role.id).ExecuteCommandAsync();
        await repo.Context.Insertable(romaps).ExecuteCommandAsync();
        await repo.Context.Deleteable<SysAuthRoleMenu>().Where(it => it.rid == role.id).ExecuteCommandAsync();
        await repo.Context.Insertable(rmmaps).ExecuteCommandAsync();
        tran.CommitTran();
    }
}