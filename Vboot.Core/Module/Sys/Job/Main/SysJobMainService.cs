using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys
{
    
    public class SysJobMainService : BaseMainService<SysJobMain>, ITransient
    {
        public SysJobMainService(ISqlSugarRepository<SysJobMain> repo)
        {
            this.repo = repo;
        }

    }
}