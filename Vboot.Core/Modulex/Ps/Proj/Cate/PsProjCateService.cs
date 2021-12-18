using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Ps
{
    
    public class PsProjCateService : BaseMainService<PsProjCate>, ITransient
    {
        public PsProjCateService(ISqlSugarRepository<PsProjCate> repo)
        {
            this.repo = repo;
        }

    }
}