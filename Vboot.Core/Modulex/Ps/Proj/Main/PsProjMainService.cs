using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Ass;

namespace Vboot.Core.Modulex.Ps
{
    
    public class PsProjMainService : BaseService<PsProjMain>, ITransient
    {
        public PsProjMainService(ISqlSugarRepository<PsProjMain> repo)
        {
            this.repo = repo;
        }

    }
}