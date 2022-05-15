using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Bpm;

public class BpmProcMainService : BaseMainService<BpmProcMain>, ITransient
{
    
    
    
    
    
    
    
    
    public BpmProcMainService(ISqlSugarRepository<BpmProcMain> repo)
    {
        this.repo = repo;
    }
}