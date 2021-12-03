using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Application.Sd.Proj.Main
{
    public class SdProjMainService : BaseMainService<SdProjMain>, ITransient
    {
        public SdProjMainService(ISqlSugarRepository<SdProjMain> repo)
        {
            this.repo = repo;
        }
    }
}