using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Wf
{
    
    public class WfTemCateService : BaseMainService<WfTemCate>, ITransient
    {
        public WfTemCateService(ISqlSugarRepository<WfTemCate> repo)
        {
            this.repo = repo;
        }

    }
}