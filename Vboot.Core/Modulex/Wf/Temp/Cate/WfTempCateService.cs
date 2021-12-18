using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Wf
{
    
    public class WfTempCateService : BaseMainService<WfTempCate>, ITransient
    {
        public WfTempCateService(ISqlSugarRepository<WfTempCate> repo)
        {
            this.repo = repo;
        }

    }
}