using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Oa
{
    
    public class OaFlowCateService : BaseMainService<OaFlowCate>, ITransient
    {
        public OaFlowCateService(ISqlSugarRepository<OaFlowCate> repo)
        {
            this.repo = repo;
        }

    }
}