using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Ass;

namespace Vboot.Core.Modulex.Wf
{
    
    public class WfTemMainService : BaseService<WfTemMain>, ITransient
    {
        public WfTemMainService(ISqlSugarRepository<WfTemMain> repo)
        {
            this.repo = repo;
        }

    }
}