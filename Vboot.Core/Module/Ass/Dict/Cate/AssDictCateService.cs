using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Ass
{
    
    public class AssDictCateService : BaseService<AssDictCate>, ITransient
    {
        public AssDictCateService(ISqlSugarRepository<AssDictCate> repo)
        {
            base.repo = repo;
        }
    }
}