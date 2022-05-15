using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Oa;

public class OaFlowMainService : BaseMainService<OaFlowMain>, ITransient
{
    
    
    public OaFlowMainService(ISqlSugarRepository<OaFlowMain> repo)
    {
        this.repo = repo;
    }
}