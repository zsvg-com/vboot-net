using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

public class SysLogLoginService : BaseService<SysLogLogin>, ITransient
{
    public SysLogLoginService(ISqlSugarRepository<SysLogLogin> repo)
    {
        this.repo = repo;
    }
}