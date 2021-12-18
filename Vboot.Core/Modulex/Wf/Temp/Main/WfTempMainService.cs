using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Ass;

namespace Vboot.Core.Modulex.Wf
{
    
    public class WfTempMainService : BaseService<WfTempMain>, ITransient
    {
        public WfTempMainService(ISqlSugarRepository<WfTempMain> repo)
        {
            this.repo = repo;
        }

    }
}