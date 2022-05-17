using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Ass;

namespace Vboot.Core.Module.Ass;

public class AssDictMainService : BaseService<AssDictMain>, ITransient
{
    public AssDictMainService(ISqlSugarRepository<AssDictMain> repo)
    {
        this.repo = repo;
    }
}