using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Ass
{
    
    public class AssDictDataService : BaseService<AssDictData>, ITransient
    {
        public AssDictDataService(ISqlSugarRepository<AssDictData> repo)
        {
            base.repo = repo;
        }

    }
}