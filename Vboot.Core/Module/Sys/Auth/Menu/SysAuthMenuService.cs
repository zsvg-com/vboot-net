using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

public class SysAuthMenuService : BaseMainService<SysAuthMenu>, ITransient
{
    public SysAuthMenuService(ISqlSugarRepository<SysAuthMenu> repo)
    {
        base.repo = repo;
    }
}