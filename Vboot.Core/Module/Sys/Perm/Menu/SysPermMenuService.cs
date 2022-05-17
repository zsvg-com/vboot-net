using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

public class SysPermMenuService : BaseMainService<SysPermMenu>, ITransient
{
    public SysPermMenuService(ISqlSugarRepository<SysPermMenu> repo)
    {
        base.repo = repo;
    }
}