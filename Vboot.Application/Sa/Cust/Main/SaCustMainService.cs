using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Application.Sa;

public class SaCustMainService : BaseMainService<SaCustMain>, ITransient
{
    public SaCustMainService(ISqlSugarRepository<SaCustMain> repo)
    {
        this.repo = repo;
    }
}