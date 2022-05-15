using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Bpm;

public class BpmProcTempService : BaseMainService<BpmProcTemp>, ITransient
{
    public BpmProcTempService(ISqlSugarRepository<BpmProcTemp> repo)
    {
        this.repo = repo;
    }
}